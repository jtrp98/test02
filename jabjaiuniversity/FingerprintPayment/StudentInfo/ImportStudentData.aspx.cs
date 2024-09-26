using Amazon.Runtime.Internal.Util;
using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class ImportStudentData : StudentGateway
    {
        //public static decimal StudentImportPercentage = 0;

        protected string LevelData = "";
        protected string ClassRoomData = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["StudentImportPercentage"] = 0M;

            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                // Get current year
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTerm = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });
                int yearID = 0;
                var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
                if (term != null)
                {
                    yearID = term.nYear.Value;
                }

                var listYear = en.TYears.Where(w => w.SchoolID == schoolID).OrderByDescending(x => x.numberYear).ToList();
                var listTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList();
                foreach (var l in listYear)
                {
                    int termCount = listTerm.Where(w => w.nYear == l.nYear).Count();
                    if (l.nYear == yearID)
                    {
                        this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"" data-term-count=""{2}"">{1}</option>", l.nYear, l.numberYear, termCount);
                    }
                    else
                    {
                        if (yearID == 0)
                        {
                            this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"" data-term-count=""{2}"">{1}</option>", l.nYear, l.numberYear, termCount);
                        }
                        else
                        {
                            this.ltrYear.Text += string.Format(@"<option value=""{0}"" data-term-count=""{2}"">{1}</option>", l.nYear, l.numberYear, termCount);
                        }
                    }

                    if (yearID == 0) yearID = l.nYear;
                }

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    LevelData += string.Format(@", {{'levelID':{0}, 'levelName':'{1}'}}", l.nTSubLevel, l.SubLevel);
                }
                if (!string.IsNullOrEmpty(LevelData)) LevelData = LevelData.Remove(0, 2);

                string query = string.Format(@"
SELECT r.id, r.name, r.lid
FROM
(
	SELECT t.nTermSubLevel2 'id', s.SubLevel + ' / ' + t.nTSubLevel2 'name', s.nTSubLevel 'lid'
	, s.SubLevel 'sort1', (CASE WHEN ISNUMERIC(t.nTSubLevel2) = 1 THEN RIGHT('0000' + t.nTSubLevel2, 5) ELSE t.nTSubLevel2 END) 'sort2'
	FROM TTermSubLevel2 t 
	LEFT JOIN TSubLevel s ON t.nTSubLevel = s.nTSubLevel 
	WHERE t.SchoolID = {0} AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1
) r
ORDER BY r.sort1, r.sort2", schoolID);
                List<EntityDropdown2> classRooms = en.Database.SqlQuery<EntityDropdown2>(query).ToList();
                var jsonClassRoom = new JavaScriptSerializer().Serialize(classRooms);
                ClassRoomData = jsonClassRoom.Substring(1, jsonClassRoom.Length - 2);
            }
        }

        [WebMethod(EnableSession = true)]
        public static string UpdateMonitor()
        {
            bool isComplete = false;
            decimal percentage = 0;

            percentage = Convert.ToDecimal(HttpContext.Current.Session["StudentImportPercentage"].ToString());
            if (percentage >= 100)
            {
                isComplete = true;
            }

            var result = new { isComplete, percentage };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string UploadAndPrepareData(string byteData, int? year, int level, int classroom, string sheetData)
        {
            bool success = true;
            string code = "200";
            string message = "Success to upload data.";

            int ProgressStep = 1;

            HttpContext.Current.Session["StudentImportPercentage"] = 0M;

            List<StudentImportData> listStudent = new List<StudentImportData>();
            List<EducationData> listEducation = new List<EducationData>();
            List<FamilyData> listFamily = new List<FamilyData>();
            List<HealthData> listHealth = new List<HealthData>();

            List<ProcessData> processDatas = new List<ProcessData>();

            var contactData = new { limitInContact = 0, currentNumber = 0, remainingNumber = 0, excessNumber = 0 };
            int newRowImport = 0;

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            int userID = userData.UserID;
            bool[] sheetsBeProcess = sheetData.ToCharArray().Select(s => s == '1').ToArray(); // [0:fix]ข้อมูลนักเรียน, [1]ข้อมูลทางด้านการศึกษา, [2]ข้อมูลผู้ปกครอง, [3]ข้อมูลสุขภาพ

            byte[] bytes = Convert.FromBase64String(byteData);
            string fileName = string.Format(@"{0}_{1}_{2}_{3}.xls", schoolID, userID, classroom, DateTime.Now.Ticks);
            string filePath = HttpContext.Current.Server.MapPath("~/Upload/Student/" + fileName);

            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Student")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Student"));
                }

                // Save file in File folder.
                File.WriteAllBytes(filePath, bytes);

                ProgressStep++; // [2] Complete upload excel file
                HttpContext.Current.Session["StudentImportPercentage"] = 5M;


                FileInfo fileInfo = new FileInfo(filePath);
                using (var excelPackage = new ExcelPackage(fileInfo))
                {
                    string[] sheetNames = new string[] { "ข้อมูลนักเรียน", "ข้อมูลทางด้านการศึกษา", "ข้อมูลผู้ปกครอง", "ข้อมูลสุขภาพ" };
                    int[] headerRows = new int[] { 2, 1, 2, 1 };

                    DataSet dataSet = excelPackage.ToDataSet(sheetNames, headerRows);

                    #region Set Columns Name & Student Map Data

                    //### Tab ข้อมูลนักเรียน
                    string tableName = sheetNames[0];

                    // [A0] เลขที่
                    // [B1] ระดับชั้น
                    // [C2] รหัสนักเรียน - required

                    // [D3] คำนำหน้า - required
                    // [E4] เพศ - required
                    // [F5] ชื่อ - required
                    // [G6] นามสกุล - required
                    // [H7] วันเดือนปีเกิด - required
                    // [I8] ชื่อเล่น
                    // [J9] Firstname
                    // [K10] Lastname
                    // [L11] เลขบัตรประชาชน - warning
                    // [M12] เชื้อชาติ
                    // [N13] สัญชาติ
                    // [O14] ศาสนา
                    // [P15] เป็นบุตรคนที่
                    // [Q16] เบอร์โทรศัพท์
                    // [R17] อีเมล์
                    // [AA26] ชื่อ อื่นๆ
                    // [AB27] นามสกุล อื่น ๆ
                    // [AC28] ชื่อเล่นอังกฤษ
                    // [AD29] วันที่เข้าเรียน - warning
                    // [AE30] วันที่
                    // [AF31] สถานะ
                    // [AG32] ความพิการ
                    // [AH33] ความด้อยโอกาส
                    // [AI34] มีพี่น้องทั้งหมด
                    // [AJ35] พี่น้องในโรงเรียน
                    // [AK36] รายละเอียดอื่น ๆ
                    // [AL37] หมายเหตุ
                    // [AM38] ประเภทการเดินทาง
                    // [AN39] ยอดใช้จ่ายต่อวัน - warning
                    // [AO40] ชื่อหอพัก

                    dataSet.Tables[tableName].Columns[0].ColumnName = "StudentNumber"; // int?
                    dataSet.Tables[tableName].Columns[11].ColumnName = "StudentIdentification"; // length = 13
                    dataSet.Tables[tableName].Columns[2].ColumnName = "StudentID";
                    dataSet.Tables[tableName].Columns[29].ColumnName = "StudentMoveInDate"; // DateTime?
                    dataSet.Tables[tableName].Columns[3].ColumnName = "StudentTitle";
                    dataSet.Tables[tableName].Columns[4].ColumnName = "StudentGender";
                    dataSet.Tables[tableName].Columns[5].ColumnName = "StudentFirstNameTh";
                    dataSet.Tables[tableName].Columns[6].ColumnName = "StudentLastNameTh";
                    dataSet.Tables[tableName].Columns[8].ColumnName = "StudentNickNameTh";
                    dataSet.Tables[tableName].Columns[28].ColumnName = "StudentNickNameEn";
                    dataSet.Tables[tableName].Columns[9].ColumnName = "StudentFirstNameEn";
                    dataSet.Tables[tableName].Columns[10].ColumnName = "StudentLastNameEn";
                    dataSet.Tables[tableName].Columns[26].ColumnName = "StudentFirstNameOther";
                    dataSet.Tables[tableName].Columns[27].ColumnName = "StudentLastNameOther";
                    dataSet.Tables[tableName].Columns[7].ColumnName = "StudentBirthday"; // DateTime?
                    dataSet.Tables[tableName].Columns[16].ColumnName = "StudentPhone";
                    dataSet.Tables[tableName].Columns[17].ColumnName = "StudentEmail";
                    dataSet.Tables[tableName].Columns[1].ColumnName = "StudentClassRoom"; // int?
                    dataSet.Tables[tableName].Columns[12].ColumnName = "StudentRace";
                    dataSet.Tables[tableName].Columns[13].ColumnName = "StudentNation";
                    dataSet.Tables[tableName].Columns[14].ColumnName = "StudentReligion";
                    dataSet.Tables[tableName].Columns[32].ColumnName = "DisabilityCode";
                    dataSet.Tables[tableName].Columns[33].ColumnName = "DisadvantageCode";
                    dataSet.Tables[tableName].Columns[34].ColumnName = "StudentSonTotal"; // int?
                    dataSet.Tables[tableName].Columns[15].ColumnName = "StudentSonNumber"; // int?
                    dataSet.Tables[tableName].Columns[35].ColumnName = "StudentBrethrenStudyHere"; // int?
                    dataSet.Tables[tableName].Columns[39].ColumnName = "nMax"; // decimal?
                    dataSet.Tables[tableName].Columns[38].ColumnName = "JourneyType"; //int? // ไป-กลับ, ประจำ
                    dataSet.Tables[tableName].Columns[40].ColumnName = "DormitoryName"; // กรณี ประเภทการเดินทาง=ประจำ
                    dataSet.Tables[tableName].Columns[36].ColumnName = "Note2";

                    //#ที่อยู่ตามทะเบียนบ้าน
                    // [S18] บ้านเลขที่ - warning
                    // [T19] ซอย
                    // [U20] ถนน
                    // [V21] หมู่
                    // [W22] แขวง/ตำบล - warning
                    // [X23] เขต/อำเภอ - warning
                    // [Y24] จังหวัด - warning
                    // [Z25] รหัสไปรษณีย์ - warning

                    // [AP41] รหัสประจำบ้าน
                    // [AQ42] เบอร์โทรศัพท์บ้าน
                    // [AR43] สถานที่เกิดระบุที่เกิด(รพ.)
                    // [AS44] สถานที่เกิด(จังหวัด)
                    // [AT45] สถานที่เกิดเขต/อำเภอ
                    // [AU46] สถานที่เกิดแขวงตำบล

                    dataSet.Tables[tableName].Columns[41].ColumnName = "RegisterHomeCode";
                    dataSet.Tables[tableName].Columns[18].ColumnName = "RegisterHomeNo";
                    dataSet.Tables[tableName].Columns[21].ColumnName = "RegisterHomeMoo";
                    dataSet.Tables[tableName].Columns[19].ColumnName = "RegisterHomeSoi";
                    dataSet.Tables[tableName].Columns[20].ColumnName = "RegisterHomeRoad";
                    dataSet.Tables[tableName].Columns[22].ColumnName = "RegisterHomeTombon"; // int?
                    dataSet.Tables[tableName].Columns[23].ColumnName = "RegisterHomeAmphoe"; // int?
                    dataSet.Tables[tableName].Columns[24].ColumnName = "RegisterHomeProvince"; // int?
                    dataSet.Tables[tableName].Columns[25].ColumnName = "RegisterHomePostalCode";
                    dataSet.Tables[tableName].Columns[42].ColumnName = "RegisterHomePhone";
                    dataSet.Tables[tableName].Columns[43].ColumnName = "BornFrom";
                    dataSet.Tables[tableName].Columns[44].ColumnName = "BornFromProvince"; // int?
                    dataSet.Tables[tableName].Columns[45].ColumnName = "BornFromAmphoe"; // int?
                    dataSet.Tables[tableName].Columns[46].ColumnName = "BornFromTombon"; // int?

                    //#ที่อยู่ปัจจุบัน
                    // [AV47] บ้านเลขที่ 1 - warning
                    // [AW48] ซอย 1
                    // [AX49] ถนน 1
                    // [AY50] หมู่ 1
                    // [AZ51] แขวง/ตำบล 1 - warning
                    // [BA52] เขต/อำเภอ 1 - warning
                    // [BB53] จังหวัด 1 - warning
                    // [BC54] รหัสไปรษณีย์ 1 - warning
                    // [BD55] เบอร์โทรศัพท์บ้าน 1
                    // [BE56] พักอาศัยอยู่กับ(คำนำหน้า)
                    // [BF57] พักอาศัยอยู่กับ(ชื่อ)
                    // [BG58] พักอาศัยอยู่กับ(นาทสกุล)
                    // [BH59] ที่ติดต่อฉุกเฉิน/โทรศัพท์
                    // [BI60] พักอาศัยอยู่กับ(อีเมล์)
                    // [BJ61] เพื่อนใกล้บ้าน(ชื่อ)
                    // [BK62] เพื่อนใกล้บ้าน(นามสกุล)
                    // [BL63] เพื่อนใกล้บ้าน(โทรศัพท์/มือถือ)
                    // [BM64] ลักษณะบ้านที่อยู่

                    dataSet.Tables[tableName].Columns[47].ColumnName = "HomeNo";
                    dataSet.Tables[tableName].Columns[50].ColumnName = "HomeMoo";
                    dataSet.Tables[tableName].Columns[48].ColumnName = "HomeSoi";
                    dataSet.Tables[tableName].Columns[49].ColumnName = "HomeRoad";
                    dataSet.Tables[tableName].Columns[51].ColumnName = "HomeTombon";
                    dataSet.Tables[tableName].Columns[52].ColumnName = "HomeAmphoe";
                    dataSet.Tables[tableName].Columns[53].ColumnName = "HomeProvince";
                    dataSet.Tables[tableName].Columns[54].ColumnName = "HomePostalCode";
                    dataSet.Tables[tableName].Columns[55].ColumnName = "HomePhone";
                    dataSet.Tables[tableName].Columns[56].ColumnName = "HomeStayWithTitle"; // int?
                    dataSet.Tables[tableName].Columns[57].ColumnName = "HomeStayWithName";
                    dataSet.Tables[tableName].Columns[58].ColumnName = "HomeStayWithLast";
                    dataSet.Tables[tableName].Columns[59].ColumnName = "HomeStayWithEmergencyCall";
                    dataSet.Tables[tableName].Columns[60].ColumnName = "HomeStayWithEmergencyEmail";
                    dataSet.Tables[tableName].Columns[61].ColumnName = "HomeFriendName";
                    dataSet.Tables[tableName].Columns[62].ColumnName = "HomeFriendLastName";
                    dataSet.Tables[tableName].Columns[63].ColumnName = "HomeFriendPhone";
                    dataSet.Tables[tableName].Columns[64].ColumnName = "HomeHomeType"; // int?

                    listStudent = dataSet.Tables[tableName].DataTableToList<StudentImportData>().Where(w => !string.IsNullOrEmpty(w.StudentID)).ToList();
                    ProgressStep++; // [3] Complete excel to object (Tab ข้อมูลนักเรียน)
                    HttpContext.Current.Session["StudentImportPercentage"] = 10M;


                    //### Tab ข้อมูลทางการศึกษา
                    tableName = sheetNames[1];

                    // [A0] เลขที่
                    // [B1] รหัสนักเรียน
                    // [C2] ชื่อ

                    // [D3] สถานศึกษาเดิม - warning
                    // [E4] แขวง/ตำบล - warning
                    // [F5] เขต/อำเภอ - warning
                    // [G6] จังหวัด - warning
                    // [H7] วุฒิการศึกษา - warning
                    // [I8] หน่วยกิตการเรียนที่ได้ (GPA) - warning
                    // [J9] หน่วยกิต/หน่วยการเรียน
                    // [K10] เหตุผลที่ย้าย - warning

                    dataSet.Tables[tableName].Columns[1].ColumnName = "StudentID";
                    dataSet.Tables[tableName].Columns[3].ColumnName = "OldSchoolName";
                    dataSet.Tables[tableName].Columns[4].ColumnName = "OldSchoolTombon";
                    dataSet.Tables[tableName].Columns[5].ColumnName = "OldSchoolAmphoe";
                    dataSet.Tables[tableName].Columns[6].ColumnName = "OldSchoolProvince";
                    dataSet.Tables[tableName].Columns[7].ColumnName = "OldSchoolGraduated";
                    dataSet.Tables[tableName].Columns[8].ColumnName = "OldSchoolGPA";
                    dataSet.Tables[tableName].Columns[9].ColumnName = "Credit"; // decimal? 
                    dataSet.Tables[tableName].Columns[10].ColumnName = "MoveOutReason";

                    if (sheetsBeProcess[1]) listEducation = dataSet.Tables[tableName].DataTableToList<EducationData>();
                    ProgressStep++; // [4] Complete excel to object (Tab ข้อมูลทางการศึกษา)
                    HttpContext.Current.Session["StudentImportPercentage"] = 15M;


                    //### Tab ข้อมูลผู้ปกครอง
                    tableName = sheetNames[2];

                    // ข้อมูลผู้ปกครอง
                    // [A0] เลขที่
                    // [B1] รหัสนักเรียน
                    // [C2] ชื่อ

                    // [D3] คำนำหน้า - required
                    // [E4] ชื่อ - required
                    // [F5] นามสกุล - required
                    // [G6] ความสัมพันธ์ - warning
                    // [H7] รหัสบัตรประชาชน
                    // [I8] เชื้อชาติ
                    // [J9] สัญชาติ
                    // [K10] ศาสนา
                    // [L11] เบอร์โทรศัพท์ 1 (บ้าน) - warning
                    // [M12] เบอร์โทรศัพท์ 2 (มือถือ)
                    // [N13] เบอร์โทรศัพท์ 3 (ที่ทำงาน)
                    // [O14] อีเมล์
                    // [P15] บ้านเลขที่
                    // [Q16] ซอย
                    // [R17] ถนน
                    // [S18] หมู่
                    // [T19] แขวง/ตำบล
                    // [U20] เขต/อำเภอ
                    // [V21] จังหวัด
                    // [W22] รหัสไปรษณีย์
                    // [X23] ชื่อ อังกฤษ
                    // [Y24] นามสกุล อังกฤษ
                    // [Z25] วันเกิด
                    // [AA26] วุฒิการศึกษา - warning
                    // [AB27] อาชีพ
                    // [AC28] สถานที่ทำงาน
                    // [AD29] สถานะทางครอบครัว - warning
                    // [AE30] เบิกค่าเล่าเรียน - warning
                    // [AF31] รายได้ต่อเดือน

                    dataSet.Tables[tableName].Columns[1].ColumnName = "StudentID";
                    dataSet.Tables[tableName].Columns[3].ColumnName = "ParentTitle";
                    dataSet.Tables[tableName].Columns[4].ColumnName = "ParentName";
                    dataSet.Tables[tableName].Columns[5].ColumnName = "ParentLastName";
                    dataSet.Tables[tableName].Columns[23].ColumnName = "ParentNameEn";
                    dataSet.Tables[tableName].Columns[24].ColumnName = "ParentNameLastEn";
                    dataSet.Tables[tableName].Columns[25].ColumnName = "ParentBirthday"; // DateTime?
                    dataSet.Tables[tableName].Columns[7].ColumnName = "ParentIdentification";
                    dataSet.Tables[tableName].Columns[8].ColumnName = "ParentRace";
                    dataSet.Tables[tableName].Columns[9].ColumnName = "ParentNation";
                    dataSet.Tables[tableName].Columns[10].ColumnName = "ParentReligion";
                    dataSet.Tables[tableName].Columns[26].ColumnName = "ParentGraduated"; // int?
                    dataSet.Tables[tableName].Columns[15].ColumnName = "ParentHomeNo";
                    dataSet.Tables[tableName].Columns[16].ColumnName = "ParentSoi";
                    dataSet.Tables[tableName].Columns[17].ColumnName = "ParentRoad";
                    dataSet.Tables[tableName].Columns[18].ColumnName = "ParentMoo";
                    dataSet.Tables[tableName].Columns[19].ColumnName = "ParentTombon";
                    dataSet.Tables[tableName].Columns[20].ColumnName = "ParentAmphoe";
                    dataSet.Tables[tableName].Columns[21].ColumnName = "ParentProvince";
                    dataSet.Tables[tableName].Columns[22].ColumnName = "ParentPostalCode";
                    dataSet.Tables[tableName].Columns[6].ColumnName = "ParentRelate";
                    dataSet.Tables[tableName].Columns[30].ColumnName = "ParentRequestStudyMoney"; // int?
                    dataSet.Tables[tableName].Columns[29].ColumnName = "ParentStatus"; // int?
                    dataSet.Tables[tableName].Columns[27].ColumnName = "ParentJob";
                    dataSet.Tables[tableName].Columns[28].ColumnName = "ParentWorkPlace";
                    dataSet.Tables[tableName].Columns[31].ColumnName = "ParentIncome"; // double?
                    dataSet.Tables[tableName].Columns[11].ColumnName = "ParentPhone";
                    dataSet.Tables[tableName].Columns[12].ColumnName = "ParentPhone2";
                    dataSet.Tables[tableName].Columns[13].ColumnName = "ParentPhone3";


                    // ข้อมูลมารดา
                    // [AG32] คำนำหน้า - required
                    // [AH33] ชื่อ - required
                    // [AI34] นามสกุล - required
                    // [AJ35] รหัสบัตรประชาชน
                    // [AK36] เชื้อชาติ
                    // [AL37] สัญชาติ
                    // [AM38] ศาสนา
                    // [AN39] เบอร์โทรศัพท์ 1 (บ้าน) - warning
                    // [AO40] เบอร์โทรศัพท์ 2 (มือถือ)
                    // [AP41] เบอร์โทรศัพท์ 3 (ที่ทำงาน)
                    // [AQ42] อีเมล์
                    // [AR43] บ้านเลขที่
                    // [AS44] ซอย
                    // [AT45] ถนน
                    // [AU46] หมู่
                    // [AV47] แขวง/ตำบล
                    // [AW48] เขต/อำเภอ
                    // [AX49] จังหวัด
                    // [AY50] รหัสไปรษณีย์
                    // [AZ51] ชื่อ อังกฤษ
                    // [BA52] นามสกุล อังกฤษ
                    // [BB53] วันเกิด
                    // [BC54] วุฒิการศึกษา - warning
                    // [BD55] อาชีพ
                    // [BE56] สถานที่ทำงาน
                    // [BF57] รายได้ต่อเดือน

                    dataSet.Tables[tableName].Columns[32].ColumnName = "MotherTitle";
                    dataSet.Tables[tableName].Columns[33].ColumnName = "MotherName";
                    dataSet.Tables[tableName].Columns[34].ColumnName = "MotherLastName";
                    dataSet.Tables[tableName].Columns[51].ColumnName = "MotherNameEn";
                    dataSet.Tables[tableName].Columns[52].ColumnName = "MotherNameLastEn";
                    dataSet.Tables[tableName].Columns[53].ColumnName = "MotherBirthday"; // DateTime?
                    dataSet.Tables[tableName].Columns[35].ColumnName = "MotherIdentification";
                    dataSet.Tables[tableName].Columns[36].ColumnName = "MotherRace";
                    dataSet.Tables[tableName].Columns[37].ColumnName = "MotherNation";
                    dataSet.Tables[tableName].Columns[38].ColumnName = "MotherReligion";
                    dataSet.Tables[tableName].Columns[54].ColumnName = "MotherGraduated"; // int?
                    dataSet.Tables[tableName].Columns[43].ColumnName = "MotherHomeNo";
                    dataSet.Tables[tableName].Columns[44].ColumnName = "MotherSoi";
                    dataSet.Tables[tableName].Columns[45].ColumnName = "MotherRoad";
                    dataSet.Tables[tableName].Columns[46].ColumnName = "MotherMoo";
                    dataSet.Tables[tableName].Columns[47].ColumnName = "MotherTombon";
                    dataSet.Tables[tableName].Columns[48].ColumnName = "MotherAmphoe";
                    dataSet.Tables[tableName].Columns[49].ColumnName = "MotherProvince";
                    dataSet.Tables[tableName].Columns[50].ColumnName = "MotherPostalCode";
                    dataSet.Tables[tableName].Columns[55].ColumnName = "MotherJob";
                    dataSet.Tables[tableName].Columns[56].ColumnName = "MotherWorkPlace";
                    dataSet.Tables[tableName].Columns[57].ColumnName = "MotherIncome"; // double?
                    dataSet.Tables[tableName].Columns[39].ColumnName = "MotherPhone";
                    dataSet.Tables[tableName].Columns[40].ColumnName = "MotherPhone2";
                    dataSet.Tables[tableName].Columns[41].ColumnName = "MotherPhone3";


                    // ข้อมูลบิดา
                    // [BG58] คำนำหน้า - required
                    // [BH59] ชื่อ - required
                    // [BI60] นามสกุล - required
                    // [BJ61] รหัสบัตรประชาชน
                    // [BK62] เชื้อชาติ
                    // [BL63] สัญชาติ
                    // [BM64] ศาสนา
                    // [BN65] เบอร์โทรศัพท์ 1 (บ้าน) - warning
                    // [BO66] อีเมล์
                    // [BP67] บ้านเลขที่
                    // [BQ68] ซอย
                    // [BR69] ถนน
                    // [BS70] หมู่
                    // [BT71] แขวง/ตำบล
                    // [BU72] เขต/อำเภอ
                    // [BV73] จังหวัด
                    // [BW74] รหัสไปรษณีย์
                    // [BX75] ชื่อ อังกฤษ
                    // [BY76] นามสกุล อังกฤษ
                    // [BZ77] วันเกิด
                    // [CA78] เบอร์โทรศัพท์ 2 (มือถือ)
                    // [CB79] เบอร์โทรศัพท์ 3 (ที่ทำงาน)
                    // [CC80] วุฒิการศึกษา - warning
                    // [CD81] อาชีพ
                    // [CE82] สถานที่ทำงาน
                    // [CF83] รายได้ต่อเดือน

                    dataSet.Tables[tableName].Columns[58].ColumnName = "FatherTitle";
                    dataSet.Tables[tableName].Columns[59].ColumnName = "FatherName";
                    dataSet.Tables[tableName].Columns[60].ColumnName = "FatherLastName";
                    dataSet.Tables[tableName].Columns[75].ColumnName = "FatherNameEn";
                    dataSet.Tables[tableName].Columns[76].ColumnName = "FatherNameLastEn";
                    dataSet.Tables[tableName].Columns[77].ColumnName = "FatherBirthday"; // DateTime?
                    dataSet.Tables[tableName].Columns[61].ColumnName = "FatherIdentification";
                    dataSet.Tables[tableName].Columns[62].ColumnName = "FatherRace";
                    dataSet.Tables[tableName].Columns[63].ColumnName = "FatherNation";
                    dataSet.Tables[tableName].Columns[64].ColumnName = "FatherReligion";
                    dataSet.Tables[tableName].Columns[80].ColumnName = "FatherGraduated"; // int?
                    dataSet.Tables[tableName].Columns[67].ColumnName = "FatherHomeNo";
                    dataSet.Tables[tableName].Columns[68].ColumnName = "FatherSoi";
                    dataSet.Tables[tableName].Columns[69].ColumnName = "FatherRoad";
                    dataSet.Tables[tableName].Columns[70].ColumnName = "FatherMoo";
                    dataSet.Tables[tableName].Columns[71].ColumnName = "FatherTombon";
                    dataSet.Tables[tableName].Columns[72].ColumnName = "FatherAmphoe";
                    dataSet.Tables[tableName].Columns[73].ColumnName = "FatherProvince";
                    dataSet.Tables[tableName].Columns[74].ColumnName = "FatherPostalCode";
                    dataSet.Tables[tableName].Columns[81].ColumnName = "FatherJob";
                    dataSet.Tables[tableName].Columns[82].ColumnName = "FatherWorkPlace";
                    dataSet.Tables[tableName].Columns[83].ColumnName = "FatherIncome"; // double?
                    dataSet.Tables[tableName].Columns[65].ColumnName = "FatherPhone";
                    dataSet.Tables[tableName].Columns[78].ColumnName = "FatherPhone2";
                    dataSet.Tables[tableName].Columns[79].ColumnName = "FatherPhone3";

                    if (sheetsBeProcess[2]) listFamily = dataSet.Tables[tableName].DataTableToList<FamilyData>();
                    ProgressStep++; // [5] Complete excel to object (Tab ข้อมูลผู้ปกครอง)
                    HttpContext.Current.Session["StudentImportPercentage"] = 20M;


                    //### Tab ข้อมูลสุขภาพ
                    tableName = sheetNames[3];

                    // [A0] เลขที่
                    // [B1] รหัสนักเรียน                    
                    // [C2] ชื่อ

                    // [D3] ส่วนสูง
                    // [E4] น้ำหนัก
                    // [F5] กรุ๊ปเลือด - warning
                    // [G6] แพ้อาหาร
                    // [H7] แพ้ยา
                    // [I8] โรคประจำตัว
                    // [J9] โรคร้ายแรง
                    // [K10] อื่นๆ 

                    dataSet.Tables[tableName].Columns[1].ColumnName = "StudentID";
                    dataSet.Tables[tableName].Columns[3].ColumnName = "Height"; // decimal? 
                    dataSet.Tables[tableName].Columns[4].ColumnName = "Weight"; // decimal? 
                    dataSet.Tables[tableName].Columns[5].ColumnName = "HealthBlood";
                    dataSet.Tables[tableName].Columns[6].ColumnName = "HealthSickFood";
                    dataSet.Tables[tableName].Columns[7].ColumnName = "HealthSickDrug";
                    dataSet.Tables[tableName].Columns[8].ColumnName = "HealthSickCongenital";
                    dataSet.Tables[tableName].Columns[9].ColumnName = "HealthSickDanger";
                    dataSet.Tables[tableName].Columns[10].ColumnName = "HealthSickOther";

                    if (sheetsBeProcess[3]) listHealth = dataSet.Tables[tableName].DataTableToList<HealthData>();
                    ProgressStep++; // [6] Complete excel to object (Tab ข้อมูลสุขภาพ)
                    HttpContext.Current.Session["StudentImportPercentage"] = 25M;


                    #endregion

                }
                // End using (var excelPackage = new ExcelPackage(fileInfo))
            }
            catch (Exception err)
            {
                // 501 : Error upload excel file
                // 502 : Error excel to object (Tab ข้อมูลนักเรียน)
                // 503 : Error excel to object (Tab ข้อมูลทางการศึกษา)
                // 504 : Error excel to object (Tab ข้อมูลผู้ปกครอง)
                // 505 : Error excel to object (Tab ข้อมูลสุขภาพ)

                success = false;
                code = "50" + ProgressStep.ToString();
                message = "error[" + code + "(" + ErrorCodeMessage(code) + ")]: " + err.Message + ", :line " + ComFunction.GetLineNumberError(err);

                string logMessagePattern = @"[SchoolEntities:{0}], [Filename:{1}], [ErrorLine:{2}], [ErrorMessage:{3}]";
                string errorMessage = err.Message;
                string innerExceptionMessage = "";
                while (err.InnerException != null) { innerExceptionMessage += ", " + err.InnerException.Message; err = err.InnerException; }
                string logMessageDebug = string.Format(logMessagePattern, userData.Entities, fileName, ComFunction.GetLineNumberError(err), errorMessage + ", " + innerExceptionMessage);

                int? schID = schoolID;
                int? empID = userID;

                ComFunction.InsertLogDebug(schID, null, empID, logMessageDebug);
            }

            if (success)
            {
                // Load Data from MasterDB
                List<province> listProvince = new List<province>();
                List<amphur> listAmphur = new List<amphur>();
                List<district> listDistrict = new List<district>();
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var schoolObj = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                    if (schoolObj != null)
                    {
                        schoolID = schoolObj.nCompany;

                        listProvince = dbMaster.provinces.ToList();
                        listAmphur = dbMaster.amphurs.ToList();
                        listDistrict = dbMaster.districts.ToList();
                    }
                }

                // Load Data from SchoolDB
                province provinceObj = new province();
                amphur amphurObj = new amphur();
                district districtObj = new district();

                TTitleList titleObj = new TTitleList();

                // Race - เชื้อชาติ
                List<TMasterData> listMasterData9 = new List<TMasterData>();
                // Nation - สัญชาติ
                List<TMasterData> listMasterData3 = new List<TMasterData>();
                // Religion - ศาสนา
                List<TMasterData> listMasterData6 = new List<TMasterData>();
                // Disability - ความพิการ (default:99)
                List<TMasterData> listMasterData7 = new List<TMasterData>();
                // Disadvantage - ความด้อยโอกาส (default:99)
                List<TMasterData> listMasterData8 = new List<TMasterData>();

                // Title
                List<TTitleList> listTitle = new List<TTitleList>();

                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    listMasterData9 = dbSchool.TMasterDatas.Where(w => w.MasterType == "9").ToList();
                    listMasterData3 = dbSchool.TMasterDatas.Where(w => w.MasterType == "3").ToList();
                    listMasterData6 = dbSchool.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                    listMasterData7 = dbSchool.TMasterDatas.Where(w => w.MasterType == "7").ToList();
                    listMasterData8 = dbSchool.TMasterDatas.Where(w => w.MasterType == "8").ToList();

                    listTitle = dbSchool.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && w.workStatus == "working").ToList();
                }


                // TODO: Get last student number in classroom


                // Prepare data
                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    int rowIndex = 1;
                    foreach (var studentData in listStudent)
                    {
                        //Thread.Sleep(3000);
                        HttpContext.Current.Session["StudentImportPercentage"] = 25M + Convert.ToDecimal((rowIndex / (listStudent.Count * 1M)) * 75);

                        double stepLog = 0;

                        ProcessData processData = new ProcessData { No = studentData.StudentNumber, Code = studentData.StudentID, Name = studentData.StudentFirstNameTh, Lastname = studentData.StudentLastNameTh, IDCardNumber = studentData.StudentIdentification, Errors = new List<Error>() };

                        int tryParseIntResult = 0;
                        decimal tryParseDecimalResult = 0M;
                        double tryParseDoubleResult = 0D;

                        try
                        {
                            // Prepare Student Data
                            //#ประวัติส่วนตัว
                            stepLog++; // [1] Prepare Student Data #ประวัติส่วนตัว

                            //StudentNumber // required, validate data
                            //if (!int.TryParse(studentData.StudentNumber, out tryParseIntResult)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: A]: Required student number" });
                            if (studentData.StudentNumber == null) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: A]: กรุณาระบุข้อมูล(เลขที่นักเรียน)" });

                            // TODO: Set student number

                            studentData.StudentIdentification = studentData.StudentIdentification.Replace(" ", "").Trim(); // required, validate data
                            if (string.IsNullOrEmpty(studentData.StudentIdentification)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: L]: กรุณาระบุข้อมูล(เลขบัตรประชาชน)" });
                            if ((studentData.StudentIdentification.Length > 13 || studentData.StudentIdentification.Length < 7) && !string.IsNullOrEmpty(studentData.StudentIdentification)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: L]: ข้อมูลไม่ถูกต้อง(เลขบัตรประชาชน)" });

                            studentData.StudentID = studentData.StudentID.Trim(); // required
                            if (string.IsNullOrEmpty(studentData.StudentID)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: C]: กรุณาระบุข้อมูล(รหัสนักเรียน (Username))" });
                            if (studentData.StudentID.Length > 20) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: C]: ข้อมูลไม่ถูกต้อง(รหัสนักเรียน (Username) ยาวเกิน 20 ตัวอักษร)" });

                            if (!string.IsNullOrEmpty(studentData.StudentID))
                            {
                                Match match = Regex.Match(studentData.StudentID, @"^[A-Za-z0-9]+$");
                                if (!match.Success)
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: C]: ข้อมูลไม่ถูกต้อง(รหัสนักเรียน (Username))" });
                                }
                            }

                            if (!string.IsNullOrEmpty(studentData.StudentMoveInDate))
                            {
                                if (double.TryParse(studentData.StudentMoveInDate, out double doubleDate))
                                {
                                    studentData.dStudentMoveInDate = DateTime.FromOADate(doubleDate);
                                }
                                else
                                {
                                    studentData.dStudentMoveInDate = HelperFunctions.StringToDateTime(studentData.StudentMoveInDate); // required, validate data
                                    if (studentData.dStudentMoveInDate == null) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: AD]: ข้อมูลไม่ถูกต้อง(วันที่เข้าเรียน)" });
                                }
                            }
                            else processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: AD]: กรุณาระบุข้อมูล(วันที่เข้าเรียน)" });

                            studentData.StudentTitle = studentData.StudentTitle.Trim(); // required
                            if (!string.IsNullOrEmpty(studentData.StudentTitle))
                            {
                                titleObj = listTitle.Where(w => w.titleDescription == studentData.StudentTitle).FirstOrDefault();
                                if (titleObj != null)
                                {
                                    studentData.StudentTitle = titleObj.nTitleid.ToString();
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: D]: ข้อมูลไม่ถูกต้อง(คำนำหน้า)" });
                                }
                            }
                            else
                            {
                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: D]: กรุณาระบุข้อมูล(คำนำหน้า)" });
                            }

                            if (!string.IsNullOrEmpty(studentData.StudentGender))
                            {
                                switch (studentData.StudentGender) // required, validate data
                                {
                                    case "ชาย": studentData.StudentGender = "0"; break;
                                    case "หญิง": studentData.StudentGender = "1"; break;
                                    default:
                                        studentData.StudentGender = null;
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: E]: ข้อมูลไม่ถูกต้อง(เพศ)" }); break;
                                }
                            }
                            else
                            {
                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: E]: กรุณาระบุข้อมูล(เพศ)" });
                            }
                            studentData.StudentFirstNameTh = studentData.StudentFirstNameTh.Trim(); // required
                            if (string.IsNullOrEmpty(studentData.StudentFirstNameTh)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: F]: กรุณาระบุข้อมูล(ชื่อ)" });

                            studentData.StudentLastNameTh = studentData.StudentLastNameTh.Trim(); // required
                            if (string.IsNullOrEmpty(studentData.StudentLastNameTh)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: G]: กรุณาระบุข้อมูล(นามสกุล)" });

                            studentData.StudentNickNameTh = studentData.StudentNickNameTh.Trim();
                            studentData.StudentNickNameEn = studentData.StudentNickNameEn.Trim();
                            studentData.StudentFirstNameEn = studentData.StudentFirstNameEn.Trim();
                            studentData.StudentLastNameEn = studentData.StudentLastNameEn.Trim();
                            studentData.StudentFirstNameOther = studentData.StudentFirstNameOther.Trim();
                            studentData.StudentLastNameOther = studentData.StudentLastNameOther.Trim();
                            if (!string.IsNullOrEmpty(studentData.StudentBirthday))
                            {
                                if (double.TryParse(studentData.StudentBirthday, out double doubleDate))
                                {
                                    studentData.dStudentBirthday = DateTime.FromOADate(doubleDate);
                                    studentData.dStudentBirthday = HelperFunctions.StringToDateTime(studentData.dStudentBirthday?.ToString("dd/MM/yyyy"));
                                }
                                else
                                {
                                    studentData.dStudentBirthday = HelperFunctions.StringToDateTime(studentData.StudentBirthday); // required, validate data
                                    if (studentData.dStudentBirthday == null) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: H]: ข้อมูลไม่ถูกต้อง(วันเดือนปีเกิด)" });
                                }
                            }
                            else processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: H]: กรุณาระบุข้อมูล(วันเดือนปีเกิด)" });

                            studentData.StudentPhone = studentData.StudentPhone.Trim();
                            if (!string.IsNullOrEmpty(studentData.StudentPhone))
                            {
                                Match match = Regex.Match(studentData.StudentPhone, @"^[0-9]{9,10}$");
                                if (!match.Success)
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: Q]: ข้อมูลไม่ถูกต้อง(เบอร์โทรศัพท์)" });
                                }
                            }

                            // Check exist username from db.
                            int checkExistUsername = mctx.TUsers.Where(w => w.nCompany == schoolID && w.cDel == null && ((w.username ?? "") != "" && (w.username != "-") && w.username == studentData.StudentID) && (w.sName != studentData.StudentFirstNameTh || w.sLastname != studentData.StudentLastNameTh || (w.sIdentification != studentData.StudentIdentification && w.sIdentification != "" && studentData.StudentIdentification != ""))).Count();
                            if (checkExistUsername != 0)
                            {
                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: C]: ข้อมูลห้ามซ้ำ(รหัสนักเรียน (Username))" });
                                processData.InDB = false; // insert case
                            }

                            if (processData.InDB == null)
                            {
                                // Check status in db
                                var checkExistStudentID = ctx.TUser.Where(w => w.sStudentID == studentData.StudentID && w.SchoolID == schoolID && (w.cDel ?? "0") == "0").Count();
                                if (checkExistStudentID != 0)
                                {
                                    processData.InDB = true; // update case
                                }
                                else
                                {
                                    processData.InDB = false; // insert case
                                }
                            }


                            studentData.StudentEmail = studentData.StudentEmail.Trim();
                            //StudentClassRoom
                            studentData.StudentRace = studentData.StudentRace.Trim();
                            if (!string.IsNullOrEmpty(studentData.StudentRace))
                            {
                                var raceObj = listMasterData9.Where(w => w.MasterDes == studentData.StudentRace).FirstOrDefault();
                                if (raceObj != null)
                                {
                                    studentData.StudentRace = raceObj.MasterCode;
                                }
                                else
                                {
                                    studentData.StudentRace = null; // warning
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: M]: ข้อมูลไม่ถูกต้อง(เชื้อชาติ)" });
                                }
                            }
                            else
                            {
                                studentData.StudentRace = null;
                            }
                            studentData.StudentNation = studentData.StudentNation.Trim();
                            if (!string.IsNullOrEmpty(studentData.StudentNation))
                            {
                                var nationObj = listMasterData3.Where(w => w.MasterDes == studentData.StudentNation).FirstOrDefault();
                                if (nationObj != null)
                                {
                                    studentData.StudentNation = nationObj.MasterCode;
                                }
                                else
                                {
                                    studentData.StudentNation = null; // warning
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: N]: ข้อมูลไม่ถูกต้อง(สัญชาติ)" });
                                }
                            }
                            else
                            {
                                studentData.StudentNation = null;
                            }
                            studentData.StudentReligion = studentData.StudentReligion.Trim();
                            if (!string.IsNullOrEmpty(studentData.StudentReligion))
                            {
                                var religionObj = listMasterData6.Where(w => w.MasterDes == studentData.StudentReligion).FirstOrDefault();
                                if (religionObj != null)
                                {
                                    studentData.StudentReligion = religionObj.MasterCode;
                                }
                                else
                                {
                                    studentData.StudentReligion = null; // warning
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: O]: ข้อมูลไม่ถูกต้อง(ศาสนา)" });
                                }
                            }
                            else
                            {
                                studentData.StudentReligion = null;
                            }
                            studentData.DisabilityCode = studentData.DisabilityCode.Trim();
                            if (!string.IsNullOrEmpty(studentData.DisabilityCode))
                            {
                                var disabilityObj = listMasterData7.Where(w => w.MasterDes == studentData.DisabilityCode).FirstOrDefault();
                                if (disabilityObj != null)
                                {
                                    studentData.DisabilityCode = disabilityObj.MasterCode;
                                }
                                else
                                {
                                    studentData.DisabilityCode = "99";
                                }
                            }
                            else
                            {
                                studentData.DisabilityCode = "99";
                            }
                            studentData.DisadvantageCode = studentData.DisadvantageCode.Trim();
                            if (!string.IsNullOrEmpty(studentData.DisadvantageCode))
                            {
                                var disadvantageObj = listMasterData8.Where(w => w.MasterDes == studentData.DisadvantageCode).FirstOrDefault();
                                if (disadvantageObj != null)
                                {
                                    studentData.DisadvantageCode = disadvantageObj.MasterCode;
                                }
                                else
                                {
                                    studentData.DisadvantageCode = "99";
                                }
                            }
                            else
                            {
                                studentData.DisadvantageCode = "99";
                            }
                            if (!string.IsNullOrEmpty(studentData.StudentSonTotal)) // validate data
                            {
                                if (int.TryParse(studentData.StudentSonTotal, out tryParseIntResult))
                                {
                                    studentData.iStudentSonTotal = tryParseIntResult;
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: AI]: ข้อมูลไม่ถูกต้อง(มีพี่น้องทั้งหมด)" });
                                }
                            }
                            if (!string.IsNullOrEmpty(studentData.StudentSonNumber)) // validate data
                            {
                                if (int.TryParse(studentData.StudentSonNumber, out tryParseIntResult))
                                {
                                    studentData.iStudentSonNumber = tryParseIntResult;
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: P]: ข้อมูลไม่ถูกต้อง(เป็นบุตรคนที่)" });
                                }
                            }
                            studentData.StudentBrethrenStudyHere = studentData.StudentBrethrenStudyHere.Replace("คน", "").Trim();
                            if (!string.IsNullOrEmpty(studentData.StudentBrethrenStudyHere)) // validate data
                            {
                                if (studentData.StudentBrethrenStudyHere == "ไม่มี")
                                {
                                    studentData.iStudentBrethrenStudyHere = 0;
                                }
                                else
                                {
                                    if (int.TryParse(studentData.StudentBrethrenStudyHere, out tryParseIntResult))
                                    {
                                        studentData.iStudentBrethrenStudyHere = tryParseIntResult;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: AJ]: ข้อมูลไม่ถูกต้อง(พี่น้องในโรงเรียน)" });
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(studentData.nMax)) // required, validate data
                            {
                                if (decimal.TryParse(studentData.nMax, out tryParseDecimalResult))
                                {
                                    studentData.dnMax = tryParseDecimalResult;
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: AN]: ข้อมูลไม่ถูกต้อง(ยอดใช้จ่ายต่อวัน)" });
                                }
                            }
                            else
                            {
                                processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: AN]: กรุณาระบุข้อมูล(ยอดใช้จ่ายต่อวัน)" });

                                studentData.dnMax = 0m;
                            }
                            switch (studentData.JourneyType) // validate data
                            {
                                case "ไป-กลับ": studentData.iJourneyType = 1; break;
                                case "ประจำ": studentData.iJourneyType = 2; break;
                                default:
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: AM]: ข้อมูลไม่ถูกต้อง(ประเภทการเดินทาง)" });
                                    break;
                            }
                            studentData.DormitoryName = studentData.DormitoryName.Trim();
                            //Note2
                            if (!string.IsNullOrEmpty(studentData.RegisterHomeNo)) studentData.Address += "บ้านเลขที่ " + studentData.RegisterHomeNo;
                            if (!string.IsNullOrEmpty(studentData.RegisterHomeSoi)) studentData.Address += " ซอย " + studentData.RegisterHomeSoi;
                            if (!string.IsNullOrEmpty(studentData.RegisterHomeRoad)) studentData.Address += " ถนน " + studentData.RegisterHomeRoad;
                            if (!string.IsNullOrEmpty(studentData.RegisterHomeMoo)) studentData.Address += " หมู่ " + studentData.RegisterHomeMoo;

                            //#ที่อยู่ตามทะเบียนบ้าน
                            stepLog++; // [2] Prepare Student Data #ที่อยู่ตามทะเบียนบ้าน

                            studentData.RegisterHomeCode = studentData.RegisterHomeCode.Trim();
                            studentData.RegisterHomeNo = studentData.RegisterHomeNo.Trim(); // required
                            if (string.IsNullOrEmpty(studentData.RegisterHomeNo)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: S]: กรุณาระบุข้อมูล(บ้านเลขที่)" });

                            studentData.RegisterHomeMoo = studentData.RegisterHomeMoo.Trim();
                            studentData.RegisterHomeSoi = studentData.RegisterHomeSoi.Trim();
                            studentData.RegisterHomeRoad = studentData.RegisterHomeRoad.Trim();

                            studentData.RegisterHomeProvince = studentData.RegisterHomeProvince.Replace("ฯ", "").Trim();
                            if (!string.IsNullOrEmpty(studentData.RegisterHomeProvince))
                            {
                                provinceObj = listProvince.Where(w => w.PROVINCE_NAME.Contains(studentData.RegisterHomeProvince)).FirstOrDefault();
                                if (provinceObj != null)
                                {
                                    studentData.iRegisterHomeProvince = provinceObj.PROVINCE_ID; // required, validate data
                                    amphurObj = listAmphur.Where(w => (w.AMPHUR_NAME.Contains(studentData.RegisterHomeAmphoe.Trim()) || w.AMPHUR_NAME.Contains(studentData.RegisterHomeAmphoe.Trim().Replace("เขต", ""))) && w.PROVINCE_ID == provinceObj.PROVINCE_ID).FirstOrDefault();
                                    if (amphurObj != null)
                                    {
                                        studentData.iRegisterHomeAmphoe = amphurObj.AMPHUR_ID; // required, validate data
                                        districtObj = listDistrict.Where(w => (w.DISTRICT_NAME.Contains(studentData.RegisterHomeTombon.Trim()) || w.DISTRICT_NAME.Contains(studentData.RegisterHomeTombon.Trim().Replace("เขต", ""))) && w.AMPHUR_ID == amphurObj.AMPHUR_ID).FirstOrDefault();
                                        if (districtObj != null)
                                        {
                                            studentData.iRegisterHomeTombon = districtObj.DISTRICT_ID; // required, validate data
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(studentData.RegisterHomeTombon.Trim()))
                                            {
                                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: W]: ข้อมูลไม่ถูกต้อง(แขวง/ตำบล)" });
                                            }
                                            else
                                            {
                                                processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: W]: ข้อมูลไม่ถูกต้อง(แขวง/ตำบล)" });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomeAmphoe.Trim()))
                                        {
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: X]: ข้อมูลไม่ถูกต้อง(เขต/อำเภอ)" });
                                        }
                                        else
                                        {
                                            processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: X]: กรุณาระบุข้อมูล(เขต/อำเภอ)" });
                                        }
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: Y]: ข้อมูลไม่ถูกต้อง(จังหวัด)" });
                                }
                            }
                            else
                            {
                                processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: Y]: กรุณาระบุข้อมูล(จังหวัด)" });
                            }

                            studentData.RegisterHomePostalCode = studentData.RegisterHomePostalCode.Trim(); // required, validate data
                            if (string.IsNullOrEmpty(studentData.RegisterHomePostalCode)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: Z]: กรุณาระบุข้อมูล(รหัสไปรษณีย์)" });
                            if (studentData.RegisterHomePostalCode.Length != 5 && studentData.RegisterHomePostalCode.Length > 1) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: Z]: ข้อมูลไม่ถูกต้อง(รหัสไปรษณีย์)" });

                            studentData.RegisterHomePhone = studentData.RegisterHomePhone.Trim();
                            studentData.BornFrom = studentData.BornFrom.Trim();

                            studentData.BornFromProvince = studentData.BornFromProvince.Replace("ฯ", "").Trim();
                            if (!string.IsNullOrEmpty(studentData.BornFromProvince))
                            {
                                provinceObj = listProvince.Where(w => w.PROVINCE_NAME.Contains(studentData.BornFromProvince)).FirstOrDefault();
                                if (provinceObj != null)
                                {
                                    studentData.iBornFromProvince = provinceObj.PROVINCE_ID; // required, validate data
                                    amphurObj = listAmphur.Where(w => w.AMPHUR_NAME.Contains(studentData.BornFromAmphoe.Trim()) && w.PROVINCE_ID == provinceObj.PROVINCE_ID).FirstOrDefault();
                                    if (amphurObj != null && !string.IsNullOrEmpty(studentData.BornFromAmphoe.Trim()))
                                    {
                                        studentData.iBornFromAmphoe = amphurObj.AMPHUR_ID; // required, validate data
                                        districtObj = listDistrict.Where(w => w.DISTRICT_NAME.Contains(studentData.BornFromTombon.Trim()) && w.AMPHUR_ID == amphurObj.AMPHUR_ID).FirstOrDefault();
                                        if (districtObj != null && !string.IsNullOrEmpty(studentData.BornFromTombon.Trim()))
                                        {
                                            studentData.iBornFromTombon = districtObj.DISTRICT_ID; // required, validate data
                                        }
                                        else
                                        {
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: AU]: ข้อมูลไม่ถูกต้อง(สถานที่เกิดแขวงตำบล)" });
                                        }
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: AT]: ข้อมูลไม่ถูกต้อง(สถานที่เกิดเขต/อำเภอ)" });
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: AS]: ข้อมูลไม่ถูกต้อง(สถานที่เกิด(จังหวัด))" });
                                }
                            }
                            //else
                            //{
                            //    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: AS]: กรุณาระบุข้อมูล(สถานที่เกิด(จังหวัด))" });
                            //}


                            //#ที่อยู่ปัจจุบัน
                            stepLog++; // [3] Prepare Student Data #ที่อยู่ปัจจุบัน

                            studentData.HomeNo = studentData.HomeNo.Trim(); // required
                            if (string.IsNullOrEmpty(studentData.HomeNo)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: AV]: กรุณาระบุข้อมูล(บ้านเลขที่ 1)" });

                            studentData.HomeMoo = studentData.HomeMoo.Trim();
                            studentData.HomeSoi = studentData.HomeSoi.Trim();
                            studentData.HomeRoad = studentData.HomeRoad.Trim();

                            studentData.HomeProvince = studentData.HomeProvince.Replace("ฯ", "").Trim();
                            if (!string.IsNullOrEmpty(studentData.HomeProvince))
                            {
                                provinceObj = listProvince.Where(w => w.PROVINCE_NAME.Contains(studentData.HomeProvince)).FirstOrDefault();
                                if (provinceObj != null)
                                {
                                    studentData.iHomeProvince = provinceObj.PROVINCE_ID; // required, validate data
                                    amphurObj = listAmphur.Where(w => (w.AMPHUR_NAME.Contains(studentData.HomeAmphoe.Trim()) || w.AMPHUR_NAME.Contains(studentData.HomeAmphoe.Trim().Replace("เขต", ""))) && w.PROVINCE_ID == provinceObj.PROVINCE_ID).FirstOrDefault();
                                    if (amphurObj != null && !string.IsNullOrEmpty(studentData.HomeAmphoe.Trim()))
                                    {
                                        studentData.iHomeAmphoe = amphurObj.AMPHUR_ID; // required, validate data
                                        districtObj = listDistrict.Where(w => (w.DISTRICT_NAME.Contains(studentData.HomeTombon.Trim()) || w.DISTRICT_NAME.Contains(studentData.HomeTombon.Trim().Replace("เขต", ""))) && w.AMPHUR_ID == amphurObj.AMPHUR_ID).FirstOrDefault();
                                        if (districtObj != null && !string.IsNullOrEmpty(studentData.HomeTombon.Trim()))
                                        {
                                            studentData.iHomeTombon = districtObj.DISTRICT_ID; // required, validate data
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(studentData.HomeTombon.Trim()))
                                            {
                                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: AZ]: ข้อมูลไม่ถูกต้อง(แขวง/ตำบล 1)" });
                                            }
                                            else
                                            {
                                                processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: AZ]: กรุณาระบุข้อมูล(แขวง/ตำบล 1)" });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(studentData.HomeAmphoe.Trim()))
                                        {
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: BA]: ข้อมูลไม่ถูกต้อง(เขต/อำเภอ 1)" });
                                        }
                                        else
                                        {
                                            processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: BA]: กรุณาระบุข้อมูล(เขต/อำเภอ 1)" });
                                        }
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: BB]: ข้อมูลไม่ถูกต้อง(จังหวัด 1)" });
                                }
                            }
                            else
                            {
                                processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: BB]: กรุณาระบุข้อมูล(จังหวัด 1)" });
                            }

                            studentData.HomePostalCode = studentData.HomePostalCode.Trim(); // required, validate data
                            if (string.IsNullOrEmpty(studentData.HomePostalCode)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลนักเรียน: BC]: กรุณาระบุข้อมูล(รหัสไปรษณีย์ 1)" });
                            if (studentData.HomePostalCode.Length != 5 && studentData.HomePostalCode.Length > 1) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: BC]: ข้อมูลไม่ถูกต้อง(รหัสไปรษณีย์ 1)" });

                            studentData.HomePhone = studentData.HomePhone.Trim();
                            studentData.HomeStayWithTitle = studentData.HomeStayWithTitle.Trim(); // validate data
                            if (!string.IsNullOrEmpty(studentData.HomeStayWithTitle))
                            {
                                titleObj = listTitle.Where(w => w.titleDescription == studentData.HomeStayWithTitle).FirstOrDefault();
                                if (titleObj != null)
                                {
                                    studentData.iHomeStayWithTitle = titleObj.nTitleid;
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: BE]: ข้อมูลไม่ถูกต้อง(พักอาศัยอยู่กับ(คำนำหน้า))" });
                                }
                            }

                            studentData.HomeStayWithName = studentData.HomeStayWithName.Trim();
                            studentData.HomeStayWithLast = studentData.HomeStayWithLast.Trim();
                            studentData.HomeStayWithEmergencyCall = studentData.HomeStayWithEmergencyCall.Trim();
                            studentData.HomeStayWithEmergencyEmail = studentData.HomeStayWithEmergencyEmail.Trim();
                            studentData.HomeFriendName = studentData.HomeFriendName.Trim();
                            studentData.HomeFriendLastName = studentData.HomeFriendLastName.Trim();
                            studentData.HomeFriendPhone = studentData.HomeFriendPhone.Trim();
                            studentData.HomeHomeType = studentData.HomeHomeType.Trim(); // validate data
                            if (!string.IsNullOrEmpty(studentData.HomeHomeType))
                            {
                                switch (studentData.HomeHomeType)
                                {
                                    case "บ้านตัวเอง":
                                    case "บ้านของตัวเอง": studentData.iHomeHomeType = 1; break;
                                    case "บ้านญาติ": studentData.iHomeHomeType = 2; break;
                                    case "บ้านเช่า": studentData.iHomeHomeType = 3; break;
                                    case "บ้านพักราชการ": studentData.iHomeHomeType = 4; break;
                                    case "หอพักโรงเรียน": studentData.iHomeHomeType = 5; break;
                                    default:
                                        studentData.iHomeHomeType = 0;
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลนักเรียน: BM]: ข้อมูลไม่ถูกต้อง(ลักษณะบ้านที่อยู่)" });
                                        break;
                                }
                            }


                            // Prepare Education Data
                            stepLog++; // [4] Prepare Education Data

                            EducationData educationData = listEducation.Where(w => w.StudentID == studentData.StudentID).FirstOrDefault();
                            if (educationData != null)
                            {
                                educationData.OldSchoolName = educationData.OldSchoolName.Trim(); // required
                                if (string.IsNullOrEmpty(educationData.OldSchoolName)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลทางการศึกษา: D]: กรุณาระบุข้อมูล(สถานศึกษาเดิม)" });

                                educationData.OldSchoolProvince = educationData.OldSchoolProvince.Replace("ฯ", "").Trim();
                                if (!string.IsNullOrEmpty(educationData.OldSchoolProvince))
                                {
                                    provinceObj = listProvince.Where(w => w.PROVINCE_NAME.Contains(educationData.OldSchoolProvince)).FirstOrDefault();
                                    if (provinceObj != null)
                                    {
                                        educationData.iOldSchoolProvince = provinceObj.PROVINCE_ID;
                                        amphurObj = listAmphur.Where(w => (w.AMPHUR_NAME.Contains(educationData.OldSchoolAmphoe.Trim()) || w.AMPHUR_NAME.Contains(educationData.OldSchoolAmphoe.Trim().Replace("เขต", ""))) && w.PROVINCE_ID == provinceObj.PROVINCE_ID).FirstOrDefault();
                                        if (amphurObj != null && !string.IsNullOrEmpty(educationData.OldSchoolAmphoe.Trim()))
                                        {
                                            educationData.iOldSchoolAmphoe = amphurObj.AMPHUR_ID;
                                            districtObj = listDistrict.Where(w => (w.DISTRICT_NAME.Contains(educationData.OldSchoolTombon.Trim()) || w.DISTRICT_NAME.Contains(educationData.OldSchoolTombon.Trim().Replace("เขต", ""))) && w.AMPHUR_ID == amphurObj.AMPHUR_ID).FirstOrDefault();
                                            if (districtObj != null && !string.IsNullOrEmpty(educationData.OldSchoolTombon.Trim()))
                                            {
                                                educationData.iOldSchoolTombon = districtObj.DISTRICT_ID;
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(educationData.OldSchoolTombon.Trim()))
                                                {
                                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลทางการศึกษา: E]: ข้อมูลไม่ถูกต้อง(แขวง/ตำบล)" });
                                                }
                                                else
                                                {
                                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลทางการศึกษา: E]: กรุณาระบุข้อมูล(แขวง/ตำบล)" });
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(educationData.OldSchoolAmphoe.Trim()))
                                            {
                                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลทางการศึกษา: F]: ข้อมูลไม่ถูกต้อง(เขต/อำเภอ)" });
                                            }
                                            else
                                            {
                                                processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลทางการศึกษา: F]: กรุณาระบุข้อมูล(เขต/อำเภอ)" });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลทางการศึกษา: G]: ข้อมูลไม่ถูกต้อง(จังหวัด)" });
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลทางการศึกษา: G]: กรุณาระบุข้อมูล(จังหวัด)" });
                                }

                                educationData.OldSchoolGraduated = educationData.OldSchoolGraduated.Trim(); // required
                                if (string.IsNullOrEmpty(educationData.OldSchoolGraduated)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลทางการศึกษา: H]: กรุณาระบุข้อมูล(วุฒิการศึกษา)" });

                                if (!string.IsNullOrEmpty(educationData.Credit)) // validate data
                                {
                                    if (decimal.TryParse(educationData.Credit, out tryParseDecimalResult))
                                    {
                                        educationData.dCredit = tryParseDecimalResult;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลทางการศึกษา: J]: ข้อมูลไม่ถูกต้อง(หน่วยกิต/หน่วยการเรียน)" });
                                    }
                                }

                                educationData.OldSchoolGPA = educationData.OldSchoolGPA.Trim(); // required
                                if (string.IsNullOrEmpty(educationData.OldSchoolGPA)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลทางการศึกษา: I]: กรุณาระบุข้อมูล(หน่วยกิตการเรียนที่ได้ (GPA))" });

                                educationData.MoveOutReason = educationData.MoveOutReason.Trim(); // warning
                                if (string.IsNullOrEmpty(educationData.MoveOutReason)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลทางการศึกษา: K]: กรุณาระบุข้อมูล(เหตุผลที่ย้าย)" });

                                studentData.EducationData = educationData;
                            }


                            // Prepare Family Data
                            FamilyData familyData = listFamily.Where(w => w.StudentID == studentData.StudentID).FirstOrDefault();
                            if (familyData != null)
                            {
                                //#ข้อมูลผู้ปกครอง
                                stepLog++; // [5] Prepare Family Data #ข้อมูลผู้ปกครอง

                                familyData.ParentTitle = familyData.ParentTitle.Trim(); // required, validate data
                                if (!string.IsNullOrEmpty(familyData.ParentTitle))
                                {
                                    titleObj = listTitle.Where(w => w.titleDescription == familyData.ParentTitle).FirstOrDefault();
                                    if (titleObj != null)
                                    {
                                        familyData.iParentTitle = titleObj.nTitleid;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: D]: ข้อมูลไม่ถูกต้อง(คำนำหน้า)" });
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: D]: กรุณาระบุข้อมูล(คำนำหน้า)" });
                                }

                                familyData.ParentName = familyData.ParentName.Trim(); // required
                                if (string.IsNullOrEmpty(familyData.ParentName)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: E]: กรุณาระบุข้อมูล(ชื่อ)" });

                                familyData.ParentLastName = familyData.ParentLastName.Trim(); // required
                                if (string.IsNullOrEmpty(familyData.ParentLastName)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: F]: กรุณาระบุข้อมูล(นามสกุล)" });

                                familyData.ParentNameEn = familyData.ParentNameEn.Trim();
                                familyData.ParentNameLastEn = familyData.ParentNameLastEn.Trim();
                                if (!string.IsNullOrEmpty(familyData.ParentBirthday)) // validate data
                                {
                                    if (double.TryParse(familyData.ParentBirthday, out double doubleDate))
                                    {
                                        familyData.dParentBirthday = DateTime.FromOADate(doubleDate);
                                    }
                                    else
                                    {
                                        familyData.dParentBirthday = HelperFunctions.StringToDateTime(familyData.ParentBirthday);
                                        if (familyData.dParentBirthday == null) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: Z]: ข้อมูลไม่ถูกต้อง(วันเกิด)" });
                                    }
                                }

                                familyData.ParentIdentification = familyData.ParentIdentification.Trim();

                                familyData.ParentRace = familyData.ParentRace.Trim();
                                if (!string.IsNullOrEmpty(familyData.ParentRace))
                                {
                                    var raceObj = listMasterData9.Where(w => w.MasterDes == familyData.ParentRace).FirstOrDefault();
                                    if (raceObj != null)
                                    {
                                        familyData.ParentRace = raceObj.MasterCode;
                                    }
                                    else
                                    {
                                        familyData.ParentRace = null; // validate data
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: I]: ข้อมูลไม่ถูกต้อง(เชื้อชาติ)" });
                                    }
                                }
                                else
                                {
                                    familyData.ParentRace = null;
                                }
                                familyData.ParentNation = familyData.ParentNation.Trim();
                                if (!string.IsNullOrEmpty(familyData.ParentNation))
                                {
                                    var nationObj = listMasterData3.Where(w => w.MasterDes == familyData.ParentNation).FirstOrDefault();
                                    if (nationObj != null)
                                    {
                                        familyData.ParentNation = nationObj.MasterCode;
                                    }
                                    else
                                    {
                                        familyData.ParentNation = null; // validate data
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: J]: ข้อมูลไม่ถูกต้อง(สัญชาติ)" });
                                    }
                                }
                                else
                                {
                                    familyData.ParentNation = null;
                                }
                                familyData.ParentReligion = familyData.ParentReligion.Trim();
                                if (!string.IsNullOrEmpty(familyData.ParentReligion))
                                {
                                    var religionObj = listMasterData6.Where(w => w.MasterDes == familyData.ParentReligion).FirstOrDefault();
                                    if (religionObj != null)
                                    {
                                        familyData.ParentReligion = religionObj.MasterCode;
                                    }
                                    else
                                    {
                                        familyData.ParentReligion = null; // validate data
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: K]: ข้อมูลไม่ถูกต้อง(ศาสนา)" });
                                    }
                                }
                                else
                                {
                                    familyData.ParentReligion = null;
                                }
                                familyData.ParentGraduated = familyData.ParentGraduated.Trim(); // validate data
                                if (!string.IsNullOrEmpty(familyData.ParentGraduated))
                                {
                                    switch (familyData.ParentGraduated)
                                    {
                                        case "ต่ำกว่าประถมศึกษา": familyData.iParentGraduated = 1; break;
                                        case "ประถมศึกษา": familyData.iParentGraduated = 2; break;
                                        case "มัธยมศึกษาหรือเทียบเท่า": familyData.iParentGraduated = 3; break;
                                        case "ปริญญาตรี หรือเทียบเท่า":
                                        case "ปริญญาตรีหรือเทียบเท่า": familyData.iParentGraduated = 4; break;
                                        case "ปริญญาโท": familyData.iParentGraduated = 5; break;
                                        case "สูงกว่าปริญญาโท": familyData.iParentGraduated = 6; break;
                                        default:
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AA]: ข้อมูลไม่ถูกต้อง(วุฒิการศึกษา)" });
                                            break;
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลผู้ปกครอง: AA]: กรุณาระบุข้อมูล(วุฒิการศึกษา)" });
                                }

                                familyData.ParentHomeNo = familyData.ParentHomeNo.Trim();
                                familyData.ParentSoi = familyData.ParentSoi.Trim();
                                familyData.ParentRoad = familyData.ParentRoad.Trim();
                                familyData.ParentMoo = familyData.ParentMoo.Trim();

                                familyData.ParentProvince = familyData.ParentProvince.Replace("ฯ", "").Trim();
                                if (!string.IsNullOrEmpty(familyData.ParentProvince))
                                {
                                    provinceObj = listProvince.Where(w => w.PROVINCE_NAME.Contains(familyData.ParentProvince)).FirstOrDefault();
                                    if (provinceObj != null)
                                    {
                                        familyData.iParentProvince = provinceObj.PROVINCE_ID; // validate data
                                        amphurObj = listAmphur.Where(w => (w.AMPHUR_NAME.Contains(familyData.ParentAmphoe.Trim()) || w.AMPHUR_NAME.Contains(familyData.ParentAmphoe.Trim().Replace("เขต", ""))) && w.PROVINCE_ID == provinceObj.PROVINCE_ID).FirstOrDefault();
                                        if (amphurObj != null && !string.IsNullOrEmpty(familyData.ParentAmphoe.Trim()))
                                        {
                                            familyData.iParentAmphoe = amphurObj.AMPHUR_ID; // validate data
                                            districtObj = listDistrict.Where(w => (w.DISTRICT_NAME.Contains(familyData.ParentTombon.Trim()) || w.DISTRICT_NAME.Contains(familyData.ParentTombon.Trim().Replace("เขต", ""))) && w.AMPHUR_ID == amphurObj.AMPHUR_ID).FirstOrDefault();
                                            if (districtObj != null && !string.IsNullOrEmpty(familyData.ParentTombon.Trim()))
                                            {
                                                familyData.iParentTombon = districtObj.DISTRICT_ID; // validate data
                                            }
                                            else
                                            {
                                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: T]: ข้อมูลไม่ถูกต้อง(แขวง/ตำบล)" });
                                            }
                                        }
                                        else
                                        {
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: U]: ข้อมูลไม่ถูกต้อง(เขต/อำเภอ)" });
                                        }
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: V]: ข้อมูลไม่ถูกต้อง(จังหวัด)" });
                                    }
                                }


                                familyData.ParentPostalCode = familyData.ParentPostalCode.Trim();
                                familyData.ParentRelate = familyData.ParentRelate.Trim();
                                familyData.ParentRequestStudyMoney = familyData.ParentRequestStudyMoney.Trim(); // validate data
                                if (!string.IsNullOrEmpty(familyData.ParentRequestStudyMoney))
                                {
                                    switch (familyData.ParentRequestStudyMoney)
                                    {
                                        case "เบิกได้": familyData.iParentRequestStudyMoney = 1; break;
                                        case "เบิกไม่ได้": familyData.iParentRequestStudyMoney = 0; break;
                                        default:
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AE]: ข้อมูลไม่ถูกต้อง(เบิกค่าเล่าเรียน)" });
                                            break;
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลผู้ปกครอง: AE]: กรุณาระบุข้อมูล(เบิกค่าเล่าเรียน)" });
                                }

                                familyData.ParentStatus = familyData.ParentStatus.Trim(); // required, validate data
                                if (!string.IsNullOrEmpty(familyData.ParentStatus))
                                {
                                    switch (familyData.ParentStatus)
                                    {
                                        case "บิดามารดาอยู่ด้วยกัน": familyData.iParentStatus = 1; break;
                                        case "บิดามารดาแยกกันอยู่": familyData.iParentStatus = 2; break;
                                        case "บิดามารดาหย่าร้าง": familyData.iParentStatus = 3; break;
                                        case "บิดาถึงแก่กรรม": familyData.iParentStatus = 4; break;
                                        case "มารดาถึงแก่กรรม": familyData.iParentStatus = 5; break;
                                        case "บิดามารดาถึงแก่กรรม": familyData.iParentStatus = 6; break;
                                        case "บิดามารดาแต่งงานใหม่": familyData.iParentStatus = 7; break;
                                        default:
                                            familyData.iParentStatus = 0;
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AD]: ข้อมูลไม่ถูกต้อง(สถานะทางครอบครัว)" });
                                            break;
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลผู้ปกครอง: AD]: กรุณาระบุข้อมูล(สถานะทางครอบครัว)" });
                                }

                                familyData.ParentJob = familyData.ParentJob.Trim();
                                familyData.ParentWorkPlace = familyData.ParentWorkPlace.Trim();
                                familyData.ParentIncome = familyData.ParentIncome.Replace("-", "").Replace(" ", "");
                                if (!string.IsNullOrEmpty(familyData.ParentIncome)) // validate data
                                {
                                    if (double.TryParse(familyData.ParentIncome, out tryParseDoubleResult))
                                    {
                                        familyData.dParentIncome = tryParseDoubleResult;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AF]: ข้อมูลไม่ถูกต้อง(รายได้ต่อเดือน)" });
                                    }
                                }

                                familyData.ParentPhone = familyData.ParentPhone.Trim(); // required
                                if (string.IsNullOrEmpty(familyData.ParentPhone)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลผู้ปกครอง: L]: กรุณาระบุข้อมูล(เบอร์โทรศัพท์ 1)" });

                                familyData.ParentPhone2 = familyData.ParentPhone2.Trim();
                                familyData.ParentPhone3 = familyData.ParentPhone3.Trim();

                                //#ข้อมูลมารดา
                                stepLog++; // [6] Prepare Family Data #ข้อมูลมารดา

                                familyData.MotherTitle = familyData.MotherTitle.Trim(); // required, validate data
                                if (!string.IsNullOrEmpty(familyData.MotherTitle))
                                {
                                    titleObj = listTitle.Where(w => w.titleDescription == familyData.MotherTitle).FirstOrDefault();
                                    if (titleObj != null)
                                    {
                                        familyData.iMotherTitle = titleObj.nTitleid;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AG]: ข้อมูลไม่ถูกต้อง(คำนำหน้า)" });
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AG]: กรุณาระบุข้อมูล(คำนำหน้า)" });
                                }

                                familyData.MotherName = familyData.MotherName.Trim(); // required
                                if (string.IsNullOrEmpty(familyData.MotherName)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AH]: กรุณาระบุข้อมูล(ชื่อ)" });

                                familyData.MotherLastName = familyData.MotherLastName.Trim(); // required
                                if (string.IsNullOrEmpty(familyData.MotherLastName)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AI]: กรุณาระบุข้อมูล(นามสกุล)" });

                                familyData.MotherNameEn = familyData.MotherNameEn.Trim();
                                familyData.MotherNameLastEn = familyData.MotherNameLastEn.Trim();
                                if (!string.IsNullOrEmpty(familyData.MotherBirthday)) // validate data
                                {
                                    if (double.TryParse(familyData.MotherBirthday, out double doubleDate))
                                    {
                                        familyData.dMotherBirthday = DateTime.FromOADate(doubleDate);
                                    }
                                    else
                                    {
                                        familyData.dMotherBirthday = HelperFunctions.StringToDateTime(familyData.MotherBirthday);
                                        if (familyData.dMotherBirthday == null) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BB]: ข้อมูลไม่ถูกต้อง(วันเกิด)" });
                                    }
                                }

                                familyData.MotherIdentification = familyData.MotherIdentification.Trim();

                                familyData.MotherRace = familyData.MotherRace.Trim();
                                if (!string.IsNullOrEmpty(familyData.MotherRace))
                                {
                                    var raceObj = listMasterData9.Where(w => w.MasterDes == familyData.MotherRace).FirstOrDefault();
                                    if (raceObj != null)
                                    {
                                        familyData.MotherRace = raceObj.MasterCode;
                                    }
                                    else
                                    {
                                        familyData.MotherRace = null; // validate data
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AK]: ข้อมูลไม่ถูกต้อง(เชื้อชาติ)" });
                                    }
                                }
                                else
                                {
                                    familyData.MotherRace = null;
                                }
                                familyData.MotherNation = familyData.MotherNation.Trim();
                                if (!string.IsNullOrEmpty(familyData.MotherNation))
                                {
                                    var nationObj = listMasterData3.Where(w => w.MasterDes == familyData.MotherNation).FirstOrDefault();
                                    if (nationObj != null)
                                    {
                                        familyData.MotherNation = nationObj.MasterCode;
                                    }
                                    else
                                    {
                                        familyData.MotherNation = null; // validate data
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AL]: ข้อมูลไม่ถูกต้อง(สัญชาติ)" });
                                    }
                                }
                                else
                                {
                                    familyData.MotherNation = null;
                                }
                                familyData.MotherReligion = familyData.MotherReligion.Trim();
                                if (!string.IsNullOrEmpty(familyData.MotherReligion))
                                {
                                    var religionObj = listMasterData6.Where(w => w.MasterDes == familyData.MotherReligion).FirstOrDefault();
                                    if (religionObj != null)
                                    {
                                        familyData.MotherReligion = religionObj.MasterCode;
                                    }
                                    else
                                    {
                                        familyData.MotherReligion = null; // validate data
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AM]: ข้อมูลไม่ถูกต้อง(ศาสนา)" });
                                    }
                                }
                                else
                                {
                                    familyData.MotherReligion = null;
                                }

                                familyData.MotherGraduated = familyData.MotherGraduated.Trim(); // validate data
                                if (!string.IsNullOrEmpty(familyData.MotherGraduated))
                                {
                                    switch (familyData.MotherGraduated)
                                    {
                                        case "ต่ำกว่าประถมศึกษา": familyData.iMotherGraduated = 1; break;
                                        case "ประถมศึกษา": familyData.iMotherGraduated = 2; break;
                                        case "มัธยมศึกษาหรือเทียบเท่า": familyData.iMotherGraduated = 3; break;
                                        case "ปริญญาตรี หรือเทียบเท่า":
                                        case "ปริญญาตรีหรือเทียบเท่า": familyData.iMotherGraduated = 4; break;
                                        case "ปริญญาโท": familyData.iMotherGraduated = 5; break;
                                        case "สูงกว่าปริญญาโท": familyData.iMotherGraduated = 6; break;
                                        default:
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BC]: ข้อมูลไม่ถูกต้อง(วุฒิการศึกษา)" });
                                            break;
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลผู้ปกครอง: BC]: กรุณาระบุข้อมูล(วุฒิการศึกษา)" });
                                }

                                familyData.MotherHomeNo = familyData.MotherHomeNo.Trim();
                                familyData.MotherSoi = familyData.MotherSoi.Trim();
                                familyData.MotherRoad = familyData.MotherRoad.Trim();
                                familyData.MotherMoo = familyData.MotherMoo.Trim();

                                familyData.MotherProvince = familyData.MotherProvince.Replace("ฯ", "").Trim();
                                if (!string.IsNullOrEmpty(familyData.MotherProvince))
                                {
                                    provinceObj = listProvince.Where(w => w.PROVINCE_NAME.Contains(familyData.MotherProvince)).FirstOrDefault();
                                    if (provinceObj != null)
                                    {
                                        familyData.iMotherProvince = provinceObj.PROVINCE_ID; // validate data
                                        amphurObj = listAmphur.Where(w => (w.AMPHUR_NAME.Contains(familyData.MotherAmphoe.Trim()) || w.AMPHUR_NAME.Contains(familyData.MotherAmphoe.Trim().Replace("เขต", ""))) && w.PROVINCE_ID == provinceObj.PROVINCE_ID).FirstOrDefault();
                                        if (amphurObj != null && !string.IsNullOrEmpty(familyData.MotherAmphoe.Trim()))
                                        {
                                            familyData.iMotherAmphoe = amphurObj.AMPHUR_ID; // validate data
                                            districtObj = listDistrict.Where(w => (w.DISTRICT_NAME.Contains(familyData.MotherTombon.Trim()) || w.DISTRICT_NAME.Contains(familyData.MotherTombon.Trim().Replace("เขต", ""))) && w.AMPHUR_ID == amphurObj.AMPHUR_ID).FirstOrDefault();
                                            if (districtObj != null && !string.IsNullOrEmpty(familyData.MotherTombon.Trim()))
                                            {
                                                familyData.iMotherTombon = districtObj.DISTRICT_ID; // validate data
                                            }
                                            else
                                            {
                                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AV]: ข้อมูลไม่ถูกต้อง(แขวง/ตำบล)" });
                                            }
                                        }
                                        else
                                        {
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AW]: ข้อมูลไม่ถูกต้อง(เขต/อำเภอ)" });
                                        }
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: AX]: ข้อมูลไม่ถูกต้อง(จังหวัด)" });
                                    }
                                }

                                familyData.MotherPostalCode = familyData.MotherPostalCode.Trim();
                                familyData.MotherJob = familyData.MotherJob.Trim();
                                familyData.MotherWorkPlace = familyData.MotherWorkPlace.Trim();
                                familyData.MotherIncome = familyData.MotherIncome.Replace("-", "").Replace(" ", "");
                                if (!string.IsNullOrEmpty(familyData.MotherIncome)) // validate data
                                {
                                    if (double.TryParse(familyData.MotherIncome, out tryParseDoubleResult))
                                    {
                                        familyData.dMotherIncome = tryParseDoubleResult;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BF]: ข้อมูลไม่ถูกต้อง(รายได้ต่อเดือน)" });
                                    }
                                }

                                familyData.MotherPhone = familyData.MotherPhone.Trim(); // required
                                if (string.IsNullOrEmpty(familyData.MotherPhone)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลผู้ปกครอง: AN]: กรุณาระบุข้อมูล(เบอร์โทรศัพท์ 1)" });

                                familyData.MotherPhone2 = familyData.MotherPhone2.Trim();
                                familyData.MotherPhone3 = familyData.MotherPhone3.Trim();

                                //#ข้อมูลบิดา
                                stepLog++; // [7] Prepare Family Data #ข้อมูลบิดา

                                familyData.FatherTitle = familyData.FatherTitle.Trim(); // required, validate data
                                if (!string.IsNullOrEmpty(familyData.FatherTitle))
                                {
                                    titleObj = listTitle.Where(w => w.titleDescription == familyData.FatherTitle).FirstOrDefault();
                                    if (titleObj != null)
                                    {
                                        familyData.iFatherTitle = titleObj.nTitleid;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BG]: ข้อมูลไม่ถูกต้อง(คำนำหน้า)" });
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BG]: กรุณาระบุข้อมูล(คำนำหน้า)" });
                                }

                                familyData.FatherName = familyData.FatherName.Trim(); // required
                                if (string.IsNullOrEmpty(familyData.FatherName)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BH]: กรุณาระบุข้อมูล(ชื่อ)" });

                                familyData.FatherLastName = familyData.FatherLastName.Trim(); // required
                                if (string.IsNullOrEmpty(familyData.FatherLastName)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BI]: กรุณาระบุข้อมูล(นามสกุล)" });

                                familyData.FatherNameEn = familyData.FatherNameEn.Trim();
                                familyData.FatherNameLastEn = familyData.FatherNameLastEn.Trim();
                                if (!string.IsNullOrEmpty(familyData.FatherBirthday)) // validate data
                                {
                                    if (double.TryParse(familyData.FatherBirthday, out double doubleDate))
                                    {
                                        familyData.dFatherBirthday = DateTime.FromOADate(doubleDate);
                                    }
                                    else
                                    {
                                        familyData.dFatherBirthday = HelperFunctions.StringToDateTime(familyData.FatherBirthday);
                                        if (familyData.dFatherBirthday == null) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BZ]: ข้อมูลไม่ถูกต้อง(วันเกิด)" });
                                    }
                                }
                                familyData.FatherIdentification = familyData.FatherIdentification.Trim();

                                familyData.FatherRace = familyData.FatherRace.Trim();
                                if (!string.IsNullOrEmpty(familyData.FatherRace))
                                {
                                    var raceObj = listMasterData9.Where(w => w.MasterDes == familyData.FatherRace).FirstOrDefault();
                                    if (raceObj != null)
                                    {
                                        familyData.FatherRace = raceObj.MasterCode;
                                    }
                                    else
                                    {
                                        familyData.FatherRace = null; // validate data
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BK]: ข้อมูลไม่ถูกต้อง(เชื้อชาติ)" });
                                    }
                                }
                                else
                                {
                                    familyData.FatherRace = null;
                                }
                                familyData.FatherNation = familyData.FatherNation.Trim();
                                if (!string.IsNullOrEmpty(familyData.FatherNation))
                                {
                                    var nationObj = listMasterData3.Where(w => w.MasterDes == familyData.FatherNation).FirstOrDefault();
                                    if (nationObj != null)
                                    {
                                        familyData.FatherNation = nationObj.MasterCode;
                                    }
                                    else
                                    {
                                        familyData.FatherNation = null; // validate data
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BL]: ข้อมูลไม่ถูกต้อง(สัญชาติ)" });
                                    }
                                }
                                else
                                {
                                    familyData.FatherNation = null;
                                }
                                familyData.FatherReligion = familyData.FatherReligion.Trim();
                                if (!string.IsNullOrEmpty(familyData.FatherReligion))
                                {
                                    var religionObj = listMasterData6.Where(w => w.MasterDes == familyData.FatherReligion).FirstOrDefault();
                                    if (religionObj != null)
                                    {
                                        familyData.FatherReligion = religionObj.MasterCode;
                                    }
                                    else
                                    {
                                        familyData.FatherReligion = null; // validate data
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BM]: ข้อมูลไม่ถูกต้อง(ศาสนา)" });
                                    }
                                }
                                else
                                {
                                    familyData.FatherReligion = null;
                                }

                                familyData.FatherGraduated = familyData.FatherGraduated.Trim(); // validate data
                                if (!string.IsNullOrEmpty(familyData.FatherGraduated))
                                {
                                    switch (familyData.FatherGraduated)
                                    {
                                        case "ต่ำกว่าประถมศึกษา": familyData.iFatherGraduated = 1; break;
                                        case "ประถมศึกษา": familyData.iFatherGraduated = 2; break;
                                        case "มัธยมศึกษาหรือเทียบเท่า": familyData.iFatherGraduated = 3; break;
                                        case "ปริญญาตรี หรือเทียบเท่า":
                                        case "ปริญญาตรีหรือเทียบเท่า": familyData.iFatherGraduated = 4; break;
                                        case "ปริญญาโท": familyData.iFatherGraduated = 5; break;
                                        case "สูงกว่าปริญญาโท": familyData.iFatherGraduated = 6; break;
                                        default:
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: CC]: ข้อมูลไม่ถูกต้อง(วุฒิการศึกษา)" });
                                            break;
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลผู้ปกครอง: CC]: กรุณาระบุข้อมูล(วุฒิการศึกษา)" });
                                }

                                familyData.FatherHomeNo = familyData.FatherHomeNo.Trim();
                                familyData.FatherSoi = familyData.FatherSoi.Trim();
                                familyData.FatherRoad = familyData.FatherRoad.Trim();
                                familyData.FatherMoo = familyData.FatherMoo.Trim();

                                familyData.FatherProvince = familyData.FatherProvince.Replace("ฯ", "").Trim();
                                if (!string.IsNullOrEmpty(familyData.FatherProvince))
                                {
                                    provinceObj = listProvince.Where(w => w.PROVINCE_NAME.Contains(familyData.FatherProvince)).FirstOrDefault();
                                    if (provinceObj != null)
                                    {
                                        familyData.iFatherProvince = provinceObj.PROVINCE_ID; // validate data
                                        amphurObj = listAmphur.Where(w => (w.AMPHUR_NAME.Contains(familyData.FatherAmphoe.Trim()) || w.AMPHUR_NAME.Contains(familyData.FatherAmphoe.Trim().Replace("เขต", ""))) && w.PROVINCE_ID == provinceObj.PROVINCE_ID).FirstOrDefault();
                                        if (amphurObj != null && !string.IsNullOrEmpty(familyData.FatherAmphoe.Trim()))
                                        {
                                            familyData.iFatherAmphoe = amphurObj.AMPHUR_ID; // validate data
                                            districtObj = listDistrict.Where(w => (w.DISTRICT_NAME.Contains(familyData.FatherTombon.Trim()) || w.DISTRICT_NAME.Contains(familyData.FatherTombon.Trim().Replace("เขต", ""))) && w.AMPHUR_ID == amphurObj.AMPHUR_ID).FirstOrDefault();
                                            if (districtObj != null && !string.IsNullOrEmpty(familyData.FatherTombon.Trim()))
                                            {
                                                familyData.iFatherTombon = districtObj.DISTRICT_ID; // validate data
                                            }
                                            else
                                            {
                                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BT]: ข้อมูลไม่ถูกต้อง(แขวง/ตำบล)" });
                                            }
                                        }
                                        else
                                        {
                                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BU]: ข้อมูลไม่ถูกต้อง(เขต/อำเภอ)" });
                                        }
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: BV]: ข้อมูลไม่ถูกต้อง(จังหวัด)" });
                                    }
                                }

                                familyData.FatherPostalCode = familyData.FatherPostalCode.Trim();
                                familyData.FatherJob = familyData.FatherJob.Trim();
                                familyData.FatherWorkPlace = familyData.FatherWorkPlace.Trim();
                                familyData.FatherIncome = familyData.FatherIncome.Replace("-", "").Replace(" ", ""); // validate data
                                if (!string.IsNullOrEmpty(familyData.FatherIncome))
                                {
                                    if (double.TryParse(familyData.FatherIncome, out tryParseDoubleResult))
                                    {
                                        familyData.dFatherIncome = tryParseDoubleResult;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลผู้ปกครอง: CF]: ข้อมูลไม่ถูกต้อง(รายได้ต่อเดือน)" });
                                    }
                                }

                                familyData.FatherPhone = familyData.FatherPhone.Trim(); // required
                                if (string.IsNullOrEmpty(familyData.FatherPhone)) processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลผู้ปกครอง: BN]: กรุณาระบุข้อมูล(เบอร์โทรศัพท์ 1)" });

                                familyData.FatherPhone2 = familyData.FatherPhone2.Trim();
                                familyData.FatherPhone3 = familyData.FatherPhone3.Trim();


                                studentData.FamilyData = familyData;
                            }


                            // Prepare Health Data
                            stepLog++; // [8] Prepare Health Data

                            HealthData healthData = listHealth.Where(w => w.StudentID == studentData.StudentID).FirstOrDefault();
                            if (healthData != null)
                            {
                                if (!string.IsNullOrEmpty(healthData.Height)) // validate data
                                {
                                    if (decimal.TryParse(healthData.Height, out tryParseDecimalResult))
                                    {
                                        healthData.dHeight = tryParseDecimalResult;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลสุขภาพ: D]: ข้อมูลไม่ถูกต้อง(ส่วนสูง)" });
                                    }
                                }
                                if (!string.IsNullOrEmpty(healthData.Weight)) // validate data
                                {
                                    if (decimal.TryParse(healthData.Weight, out tryParseDecimalResult))
                                    {
                                        healthData.dWeight = tryParseDecimalResult;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลสุขภาพ: E]: ข้อมูลไม่ถูกต้อง(น้ำหนัก)" });
                                    }
                                }

                                healthData.HealthBlood = healthData.HealthBlood.Trim(); // required, validate data
                                if (!string.IsNullOrEmpty(healthData.HealthBlood))
                                {
                                    string[] bloodList = new string[] { "A", "A+", "A-", "B", "B+", "B-", "AB", "AB+", "AB-", "O", "O+", "O-" };
                                    if (!bloodList.Contains(healthData.HealthBlood.ToUpper()))
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลสุขภาพ: F]: ข้อมูลไม่ถูกต้อง(กรุ๊ปเลือด)" });
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลสุขภาพ: F]: กรุณาระบุข้อมูล(กรุ๊ปเลือด)" });
                                }

                                studentData.HealthData = healthData;
                            }
                        }
                        catch (Exception ex)
                        {
                            processData.Errors.Add(new Error { Code = stepLog.ToString("000"), Log = PrepareDataStepLogMessage(stepLog), Message = ex.Message });
                            processData.Status = "error";
                        }

                        processDatas.Add(processData);

                        rowIndex++;
                    }


                    // Get Contact Data
                    StudentLogic studentLogic = new StudentLogic(ctx);
                    string currentTermID = studentLogic.GetTermId(userData);

                    // Count all student study current term
                    string query = string.Format(@"
SELECT COUNT(*)
FROM TB_StudentViews 
WHERE SchoolID={0} AND ISNULL(cDel, '0')='0' AND nTerm='{1}' 
AND ISNULL(nStudentStatus, 0)=0 AND ISNULL(moveInDate, CONVERT(DATE, GETDATE())) <= CONVERT(DATE, GETDATE())
AND sID IN (SELECT sID FROM JabjaiMasterSingleDB.dbo.TUser WHERE nCompany={0} AND cType = '0')", schoolID, currentTermID);
                    var currentNumber = ctx.Database.SqlQuery<int>(query).FirstOrDefault();

                    // Get limit amount student on contact
                    var limitInContact = 0;
                    TContact contact = mctx.TContact.Where(w => w.SchoolID == schoolID && w.IsDelete == false).FirstOrDefault();
                    if (contact != null && contact.StudentCount != null)
                    {
                        limitInContact = (int)contact.StudentCount;
                    }

                    var remainingNumber = limitInContact - currentNumber;
                    var excessNumber = remainingNumber < 0 ? Math.Abs(remainingNumber) : 0;
                    remainingNumber = remainingNumber < 0 ? 0 : remainingNumber;

                    contactData = new { limitInContact, currentNumber, remainingNumber, excessNumber };
                    //
                }

                HttpContext.Current.Session["StudentImportPercentage"] = 100M;
                //Thread.Sleep(2000);


                // Final check status
                int warningCountSum = 0;
                int errorCountSum = 0;
                foreach (var processData in processDatas)
                {
                    int warningCount = processData.Errors.Where(w => w.Status == "warning").Count();
                    int errorCount = processData.Errors.Where(w => w.Status == "error").Count();

                    warningCountSum += warningCount;
                    errorCountSum += errorCount;

                    if (errorCount == 0)
                    {
                        processData.Status = "ready";
                    }
                    else
                    {
                        processData.Status = "error";
                    }

                    if (!(bool)processData.InDB) newRowImport++;
                }

                // Store new row import to session
                HttpContext.Current.Session["NewRowImport_" + schoolID + "_" + userID] = newRowImport;

                // For all check status
                if (errorCountSum == 0)
                {
                    if (warningCountSum != 0) code = "201"; // ready + warning
                }
                else
                {
                    code = "202";
                    message = "Some data is missing or some data is inaccurate.";
                }

                //try
                //{
                //    if (!File.Exists(filePath))
                //    {
                //        File.Delete(filePath);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    success = false;
                //    code = "203";
                //    message = "The file cannot be deleted.[" + ex.Message + "]";
                //}

                HttpContext.Current.Session["ImportStudentData_" + schoolID + "_" + userID + "_" + fileName] = listStudent;

            }

            // success[true], 200 : ready
            // success[true], 201 : ready + warning
            // success[true], 202 : Some data is missing or some data is inaccurate.
            // success[true], 203 : The file cannot be deleted.
            // success[false], 501 : Error upload excel file
            // success[false], 502 : Error excel to object (Tab ข้อมูลนักเรียน)
            // success[false], 503 : Error excel to object (Tab ข้อมูลทางการศึกษา)
            // success[false], 504 : Error excel to object (Tab ข้อมูลผู้ปกครอง)
            // success[false], 505 : Error excel to object (Tab ข้อมูลสุขภาพ)

            var result = new { success, code, message, processDatas, uploadedFileName = fileName, contactData, newRowImport };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string SaveData(string fileName, int? year, int level, int classroom, string sheetData, string[] readyCodes)
        {
            bool success = true;
            string code = "200";
            string message = "Successfully imported student data.";

            List<ProcessData> processDatas = new List<ProcessData>();

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            int userID = userData.UserID;
            bool[] sheetsBeProcess = sheetData.ToCharArray().Select(s => s == '1').ToArray(); // [0:fix]ข้อมูลนักเรียน, [1]ข้อมูลทางด้านการศึกษา, [2]ข้อมูลผู้ปกครอง, [3]ข้อมูลสุขภาพ

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    // Get Contact Data & Check user limit
                    StudentLogic studentLogic = new StudentLogic(dbSchool);
                    string currentTermID = studentLogic.GetTermId(userData);

                    // Count all student study current term
                    string query = string.Format(@"
SELECT COUNT(*)
FROM TB_StudentViews 
WHERE SchoolID={0} AND ISNULL(cDel, '0')='0' AND nTerm='{1}' 
AND ISNULL(nStudentStatus, 0)=0 AND ISNULL(moveInDate, CONVERT(DATE, GETDATE())) <= CONVERT(DATE, GETDATE())
AND sID IN (SELECT sID FROM JabjaiMasterSingleDB.dbo.TUser WHERE nCompany={0} AND cType = '0')", schoolID, currentTermID);
                    var currentNumber = dbSchool.Database.SqlQuery<int>(query).FirstOrDefault();

                    // Get limit amount student on contact
                    var limitInContact = 0;
                    TContact contact = dbMaster.TContact.Where(w => w.SchoolID == schoolID && w.IsDelete == false).FirstOrDefault();
                    if (contact != null && contact.StudentCount != null)
                    {
                        limitInContact = (int)contact.StudentCount;
                    }

                    var remainingNumber = limitInContact - currentNumber;
                    var excessNumber = remainingNumber < 0 ? Math.Abs(remainingNumber) : 0;
                    remainingNumber = remainingNumber < 0 ? 0 : remainingNumber;

                    var contactData = new { limitInContact, currentNumber, remainingNumber, excessNumber };

                    var newRowImport = (int)(HttpContext.Current.Session["NewRowImport_" + schoolID + "_" + userID] ?? 0);

                    if (!(limitInContact > 0 && (remainingNumber - newRowImport) > 0 || newRowImport == 0))
                    {
                        return JsonConvert.SerializeObject(new { success = false, code = "501", message = "Unable to add student data, because the number of students exceeded the limit.", contactData });
                    }
                    //


                    List<StudentImportData> listStudent = (List<StudentImportData>)HttpContext.Current.Session["ImportStudentData_" + schoolID + "_" + userID + "_" + fileName];

                    listStudent = listStudent.Where(w => readyCodes.Contains(w.StudentID)).ToList();

                    var studentIDs = String.Join("', '", listStudent.Select(s => s.StudentID));

                    List<StudyYearData> studyYearDatas = new List<StudyYearData>();
                    List<GraduatedData> graduatedDatas = new List<GraduatedData>();

                    // Get study year data
                    query = string.Format(@"
SELECT u.sID, u.sStudentID 'StudentID'
, SUM(CASE WHEN y.numberYear<{0} THEN 1 ELSE 0 END) 'PreviousYear'
, SUM(CASE WHEN y.numberYear={0} AND ISNULL(sch.nStudentStatus, 0)=0 THEN 1 ELSE 0 END) 'StudyInYear'
, SUM(CASE WHEN y.numberYear>{0} AND ISNULL(sch.nStudentStatus, 0)=0 THEN 1 ELSE 0 END) 'StudyNextYear'
, SUM(CASE WHEN y.numberYear={0} THEN 1 ELSE 0 END) 'CountStudyInYear'
FROM TUser u 
LEFT JOIN TStudentClassroomHistory sch ON u.SchoolID=sch.SchoolID AND u.sID=sch.sID
LEFT JOIN TTerm t ON sch.SchoolID=t.SchoolID AND sch.nTerm=t.nTerm
LEFT JOIN TYear y ON t.SchoolID=y.SchoolID AND t.nYear=y.nYear
WHERE u.SchoolID={1} AND ISNULL(u.cDel, '0')='0' AND sch.cDel=0 AND u.sStudentID IN ('{2}')
GROUP BY u.sID, u.sStudentID", year, schoolID, studentIDs);
                    studyYearDatas = dbSchool.Database.SqlQuery<StudyYearData>(query).ToList();

                    // Get graduated data
                    query = string.Format(@"
SELECT u.sID, u.sStudentID 'StudentID', COUNT(*) 'Count'
FROM TUser u LEFT JOIN TStudentHIstory sh ON u.SchoolID=sh.SchoolID AND u.sID=sh.sID
WHERE u.SchoolID={0} AND ISNULL(u.cDel, '0')='G' AND sh.cDel=0 AND sh.StudentStatus='G' AND u.sStudentID IN ('{1}')
GROUP BY u.sID, u.sStudentID", schoolID, studentIDs);
                    graduatedDatas = dbSchool.Database.SqlQuery<GraduatedData>(query).ToList();


                    HttpContext.Current.Session["StudentImportPercentage"] = 0M;

                    int rowIndex = 1;
                    foreach (var studentData in listStudent)
                    {
                        HttpContext.Current.Session["StudentImportPercentage"] = Convert.ToDecimal((rowIndex / (listStudent.Count * 1M)) * 100);

                        //// Check ready code
                        //var isExist = Array.Exists(readyCodes, x => x == studentData.StudentID);
                        //if (!isExist) continue;

                        double stepLog = 0;

                        ProcessData processData = new ProcessData { No = studentData.StudentNumber, Code = studentData.StudentID, Name = studentData.StudentFirstNameTh, Lastname = studentData.StudentLastNameTh, IDCardNumber = studentData.StudentIdentification, Errors = new List<Error>() };


                        // Check StudentID with status Studying or Graduated
                        var graduatedData = graduatedDatas.Where(w => w.StudentID == studentData.StudentID).FirstOrDefault();
                        var studyYearData = studyYearDatas.Where(w => w.StudentID == studentData.StudentID).FirstOrDefault();

                        if ((graduatedData == null || graduatedData.Count == 0) && (studyYearData == null || (studyYearData != null && ((studyYearData.PreviousYear == 0 && studyYearData.StudyInYear == 0 && studyYearData.StudyNextYear == 0) || (studyYearData.StudyInYear > 0 && studyYearData.StudyInYear == studyYearData.CountStudyInYear) || (studyYearData.StudyInYear == 0 && studyYearData.StudyNextYear > 0)))))
                        {

                            try
                            {
                                // Save Student Data
                                string sPassword = "999999"; // RandomNumber(dbMaster);

                                var userMasterObj = dbMaster.TUsers.Where(w => w.username == studentData.StudentID && w.nCompany == schoolID && w.cType == "0" && (w.cDel == null || w.cDel == "0")).FirstOrDefault();
                                if (userMasterObj == null)
                                {
                                    stepLog = 1; // Step : New master user
                                    processData.Method = "insert";
                                    userMasterObj = new MasterEntity.TUser
                                    {
                                        nCompany = schoolID,
                                        sName = studentData.StudentFirstNameTh,
                                        sLastname = studentData.StudentLastNameTh,
                                        sIdentification = studentData.StudentIdentification,
                                        cSex = studentData.StudentGender,
                                        sPhone = studentData.StudentPhone,
                                        sEmail = studentData.StudentEmail,
                                        sPassword = sPassword,
                                        dBirth = studentData.dStudentBirthday,
                                        cType = "0",
                                        sAddress = studentData.Address,
                                        AMPHUR_ID = studentData.iRegisterHomeAmphoe,
                                        DISTRICT_ID = studentData.iRegisterHomeTombon,
                                        PROVINCE_ID = studentData.iRegisterHomeProvince,
                                        username = studentData.StudentID,
                                        userpassword = studentData.dStudentBirthday.Value.ToString("ddMMyyyy"),
                                        PasswordHash = ComFunction.HashSHA1(studentData.dStudentBirthday.Value.ToString("ddMMyyyy")),
                                        UseEncryptPassword = false,
                                        dCreate = DateTime.Now.FixSecondAndMillisecond(0, 101),
                                        dUpdate = DateTime.Now.FixSecondAndMillisecond(0, 101)
                                    };

                                    dbMaster.TUsers.Add(userMasterObj);

                                    dbMaster.SaveChanges();

                                    userMasterObj.nSystemID = userMasterObj.sID;
                                }
                                else
                                {
                                    stepLog = 2; // Step : Modify master user
                                    processData.Method = "update";
                                    if (!string.IsNullOrEmpty(studentData.StudentFirstNameTh)) userMasterObj.sName = studentData.StudentFirstNameTh;
                                    if (!string.IsNullOrEmpty(studentData.StudentLastNameTh)) userMasterObj.sLastname = studentData.StudentLastNameTh;
                                    if (!string.IsNullOrEmpty(studentData.StudentIdentification)) userMasterObj.sIdentification = studentData.StudentIdentification;
                                    if (!string.IsNullOrEmpty(studentData.StudentGender)) userMasterObj.cSex = studentData.StudentGender;
                                    if (!string.IsNullOrEmpty(studentData.StudentPhone)) userMasterObj.sPhone = studentData.StudentPhone;
                                    if (!string.IsNullOrEmpty(studentData.StudentEmail)) userMasterObj.sEmail = studentData.StudentEmail;
                                    if (studentData.dStudentBirthday != null) userMasterObj.dBirth = studentData.dStudentBirthday;
                                    if (!string.IsNullOrEmpty(studentData.Address)) userMasterObj.sAddress = studentData.Address;
                                    if (studentData.iRegisterHomeAmphoe != null) userMasterObj.AMPHUR_ID = studentData.iRegisterHomeAmphoe;
                                    if (studentData.iRegisterHomeTombon != null) userMasterObj.DISTRICT_ID = studentData.iRegisterHomeTombon;
                                    if (studentData.iRegisterHomeProvince != null) userMasterObj.PROVINCE_ID = studentData.iRegisterHomeProvince;
                                    if (studentData.dStudentBirthday != null && !userMasterObj.UseEncryptPassword)
                                    {
                                        userMasterObj.userpassword = studentData.dStudentBirthday.Value.ToString("ddMMyyyy");
                                        userMasterObj.PasswordHash = ComFunction.HashSHA1(studentData.dStudentBirthday.Value.ToString("ddMMyyyy"));
                                    }
                                    userMasterObj.cDel = null;
                                    userMasterObj.dUpdate = DateTime.Now.FixSecondAndMillisecond(0, 102);
                                }

                                dbMaster.SaveChanges();

                                var userSchoolObj = dbSchool.TUser.Where(w => w.SchoolID == schoolID && w.sID == userMasterObj.sID).FirstOrDefault();
                                if (userSchoolObj == null)
                                {
                                    stepLog = 3; // Step : New school user
                                    userSchoolObj = new JabjaiEntity.DB.TUser
                                    {
                                        sID = userMasterObj.sID,
                                        sStudentID = studentData.StudentID,
                                        moveInDate = studentData.dStudentMoveInDate,
                                        nStudentNumber = studentData.StudentNumber,
                                        nTermSubLevel2 = classroom,
                                        cSex = studentData.StudentGender,
                                        sStudentTitle = studentData.StudentTitle,
                                        sName = studentData.StudentFirstNameTh,
                                        sLastname = studentData.StudentLastNameTh,
                                        sStudentNameEN = studentData.StudentFirstNameEn,
                                        sStudentLastEN = studentData.StudentLastNameEn,
                                        sStudentNameOther = studentData.StudentFirstNameOther,
                                        sStudentLastOther = studentData.StudentLastNameOther,
                                        sNickName = studentData.StudentNickNameTh,
                                        sNickNameEN = studentData.StudentNickNameEn,
                                        dBirth = studentData.dStudentBirthday,
                                        sIdentification = studentData.StudentIdentification,
                                        sStudentRace = studentData.StudentRace,
                                        sStudentNation = studentData.StudentNation,
                                        sStudentReligion = studentData.StudentReligion,
                                        sPhone = studentData.StudentPhone,
                                        sEmail = studentData.StudentEmail,
                                        nSonNumber = studentData.iStudentSonNumber,
                                        Note2 = studentData.Note2,
                                        DisabilityCode = studentData.DisabilityCode,
                                        DisadvantageCode = studentData.DisadvantageCode,
                                        nMax = studentData.dnMax,
                                        JourneyType = studentData.iJourneyType,
                                        DormitoryName = studentData.DormitoryName,

                                        dUpdate = DateTime.Now.FixSecondAndMillisecond(0, 101),
                                        CreatedBy = userData.UserID,
                                        CreatedDate = DateTime.Now.FixSecondAndMillisecond(0, 101),
                                        cType = "0",
                                        nMoney = 0,
                                        nPicversion = 0,

                                        // ที่อยู่ปัจจุบัน (จาก Tab ข้อมูลนักเรียน)
                                        sStudentHomeRegisterCode = studentData.RegisterHomeCode, // ฟิลด์ในส่วนของที่อยู่ตามทะเบียนบ้าน
                                        sStudentHomeNumber = studentData.HomeNo,
                                        sStudentSoy = studentData.HomeSoi,
                                        sStudentMuu = studentData.HomeMoo,
                                        sStudentRoad = studentData.HomeRoad,
                                        sStudentProvince = studentData.iHomeProvince?.ToString(),
                                        sStudentAumpher = studentData.iHomeAmphoe.ToString(),
                                        sStudentTumbon = studentData.iHomeTombon.ToString(),
                                        sStudentPost = studentData.HomePostalCode,
                                        sStudentHousePhone = studentData.HomePhone,
                                        sAddress = studentData.Address,

                                        SchoolID = schoolID
                                    };

                                    // Save Education Data
                                    if (studentData.EducationData != null)
                                    {
                                        stepLog = 4; // Step : education data
                                        if (!string.IsNullOrEmpty(studentData.EducationData.OldSchoolName)) userSchoolObj.oldSchoolName = studentData.EducationData.OldSchoolName;
                                        if (studentData.EducationData.iOldSchoolProvince != null) userSchoolObj.oldSchoolProvince = studentData.EducationData.iOldSchoolProvince?.ToString();
                                        if (studentData.EducationData.iOldSchoolAmphoe != null) userSchoolObj.oldSchoolAumpher = studentData.EducationData.iOldSchoolAmphoe?.ToString();
                                        if (studentData.EducationData.iOldSchoolTombon != null) userSchoolObj.oldSchoolTumbon = studentData.EducationData.iOldSchoolTombon?.ToString();
                                        if (!string.IsNullOrEmpty(studentData.EducationData.OldSchoolGraduated)) userSchoolObj.oldSchoolGraduated = studentData.EducationData.OldSchoolGraduated;
                                        if (studentData.EducationData.dCredit != null) userSchoolObj.Credit = studentData.EducationData.dCredit;
                                        if (!string.IsNullOrEmpty(studentData.EducationData.OldSchoolGPA)) userSchoolObj.oldSchoolGPA2 = studentData.EducationData.OldSchoolGPA;
                                        if (!string.IsNullOrEmpty(studentData.EducationData.MoveOutReason)) userSchoolObj.moveOutReason = studentData.EducationData.MoveOutReason;
                                    }
                                    //

                                    dbSchool.TUser.Add(userSchoolObj);
                                }
                                else
                                {
                                    stepLog = 5; // Step : Modify school user
                                    if (!string.IsNullOrEmpty(studentData.StudentID)) userSchoolObj.sStudentID = studentData.StudentID;
                                    if (studentData.StudentNumber != null) userSchoolObj.nStudentNumber = studentData.StudentNumber;
                                    userSchoolObj.nTermSubLevel2 = classroom;
                                    if (!string.IsNullOrEmpty(studentData.StudentGender)) userSchoolObj.cSex = studentData.StudentGender;
                                    if (!string.IsNullOrEmpty(studentData.StudentTitle)) userSchoolObj.sStudentTitle = studentData.StudentTitle;
                                    if (!string.IsNullOrEmpty(studentData.StudentFirstNameTh)) userSchoolObj.sName = studentData.StudentFirstNameTh;
                                    if (!string.IsNullOrEmpty(studentData.StudentLastNameTh)) userSchoolObj.sLastname = studentData.StudentLastNameTh;
                                    if (!string.IsNullOrEmpty(studentData.StudentFirstNameEn)) userSchoolObj.sStudentNameEN = studentData.StudentFirstNameEn;
                                    if (!string.IsNullOrEmpty(studentData.StudentLastNameEn)) userSchoolObj.sStudentLastEN = studentData.StudentLastNameEn;
                                    if (!string.IsNullOrEmpty(studentData.StudentFirstNameOther)) userSchoolObj.sStudentNameOther = studentData.StudentFirstNameOther;
                                    if (!string.IsNullOrEmpty(studentData.StudentLastNameOther)) userSchoolObj.sStudentLastOther = studentData.StudentLastNameOther;
                                    if (!string.IsNullOrEmpty(studentData.StudentNickNameTh)) userSchoolObj.sNickName = studentData.StudentNickNameTh;
                                    if (!string.IsNullOrEmpty(studentData.StudentNickNameEn)) userSchoolObj.sNickNameEN = studentData.StudentNickNameEn;
                                    if (studentData.dStudentBirthday != null) userSchoolObj.dBirth = studentData.dStudentBirthday;
                                    if (!string.IsNullOrEmpty(studentData.StudentIdentification)) userSchoolObj.sIdentification = studentData.StudentIdentification;
                                    if (!string.IsNullOrEmpty(studentData.StudentRace)) userSchoolObj.sStudentRace = studentData.StudentRace;
                                    if (!string.IsNullOrEmpty(studentData.StudentNation)) userSchoolObj.sStudentNation = studentData.StudentNation;
                                    if (!string.IsNullOrEmpty(studentData.StudentReligion)) userSchoolObj.sStudentReligion = studentData.StudentReligion;
                                    if (!string.IsNullOrEmpty(studentData.StudentPhone)) userSchoolObj.sPhone = studentData.StudentPhone;
                                    if (!string.IsNullOrEmpty(studentData.StudentEmail)) userSchoolObj.sEmail = studentData.StudentEmail;
                                    if (studentData.iStudentSonNumber != null) userSchoolObj.nSonNumber = studentData.iStudentSonNumber;
                                    if (!string.IsNullOrEmpty(studentData.Note2)) userSchoolObj.Note2 = studentData.Note2;
                                    if (!string.IsNullOrEmpty(studentData.DisabilityCode)) userSchoolObj.DisabilityCode = studentData.DisabilityCode;
                                    if (!string.IsNullOrEmpty(studentData.DisadvantageCode)) userSchoolObj.DisadvantageCode = studentData.DisadvantageCode;
                                    if (studentData.dnMax != null) userSchoolObj.nMax = studentData.dnMax;
                                    if (studentData.iJourneyType != null) userSchoolObj.JourneyType = studentData.iJourneyType;
                                    if (!string.IsNullOrEmpty(studentData.DormitoryName)) userSchoolObj.DormitoryName = studentData.DormitoryName;
                                    userSchoolObj.dUpdate = DateTime.Now.FixSecondAndMillisecond(0, 102);
                                    userSchoolObj.UpdatedBy = userData.UserID;
                                    userSchoolObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(0, 102);

                                    userSchoolObj.nPicversion = 0;
                                    userSchoolObj.cDel = null;
                                    userSchoolObj.nStudentStatus = null;

                                    // ที่อยู่ปัจจุบัน (จาก Tab ข้อมูลนักเรียน)
                                    if (!string.IsNullOrEmpty(studentData.RegisterHomeCode)) userSchoolObj.sStudentHomeRegisterCode = studentData.RegisterHomeCode; // ฟิลด์ในส่วนของที่อยู่ตามทะเบียนบ้าน
                                    if (!string.IsNullOrEmpty(studentData.HomeNo)) userSchoolObj.sStudentHomeNumber = studentData.HomeNo;
                                    if (!string.IsNullOrEmpty(studentData.HomeSoi)) userSchoolObj.sStudentSoy = studentData.HomeSoi;
                                    if (!string.IsNullOrEmpty(studentData.HomeMoo)) userSchoolObj.sStudentMuu = studentData.HomeMoo;
                                    if (!string.IsNullOrEmpty(studentData.HomeRoad)) userSchoolObj.sStudentRoad = studentData.HomeRoad;
                                    if (studentData.iHomeProvince != null) userSchoolObj.sStudentProvince = studentData.iHomeProvince?.ToString();
                                    if (studentData.iHomeAmphoe != null) userSchoolObj.sStudentAumpher = studentData.iHomeAmphoe.ToString();
                                    if (studentData.iHomeTombon != null) userSchoolObj.sStudentTumbon = studentData.iHomeTombon.ToString();
                                    if (!string.IsNullOrEmpty(studentData.HomePostalCode)) userSchoolObj.sStudentPost = studentData.HomePostalCode;
                                    if (!string.IsNullOrEmpty(studentData.HomePhone)) userSchoolObj.sStudentHousePhone = studentData.HomePhone;
                                    if (!string.IsNullOrEmpty(studentData.Address)) userSchoolObj.sAddress = studentData.Address;

                                    // Save Education Data
                                    if (studentData.EducationData != null && sheetsBeProcess[1])
                                    {
                                        stepLog = 6; // Step : Education data
                                        if (!string.IsNullOrEmpty(studentData.EducationData.OldSchoolName)) userSchoolObj.oldSchoolName = studentData.EducationData.OldSchoolName;
                                        if (studentData.EducationData.iOldSchoolProvince != null) userSchoolObj.oldSchoolProvince = studentData.EducationData.iOldSchoolProvince?.ToString();
                                        if (studentData.EducationData.iOldSchoolAmphoe != null) userSchoolObj.oldSchoolAumpher = studentData.EducationData.iOldSchoolAmphoe?.ToString();
                                        if (studentData.EducationData.iOldSchoolTombon != null) userSchoolObj.oldSchoolTumbon = studentData.EducationData.iOldSchoolTombon?.ToString();
                                        if (!string.IsNullOrEmpty(studentData.EducationData.OldSchoolGraduated)) userSchoolObj.oldSchoolGraduated = studentData.EducationData.OldSchoolGraduated;
                                        if (studentData.EducationData.dCredit != null) userSchoolObj.Credit = studentData.EducationData.dCredit;
                                        if (!string.IsNullOrEmpty(studentData.EducationData.OldSchoolGPA)) userSchoolObj.oldSchoolGPA2 = studentData.EducationData.OldSchoolGPA;
                                        if (!string.IsNullOrEmpty(studentData.EducationData.MoveOutReason)) userSchoolObj.moveOutReason = studentData.EducationData.MoveOutReason;
                                    }
                                    //
                                }

                                dbSchool.SaveChanges();

                                // ADD STUDENT HISTORY 
                                var listTerm = (from a in dbSchool.TTerms
                                                join b in dbSchool.TYears on a.nYear equals b.nYear
                                                where b.numberYear == year && b.SchoolID == schoolID && a.cDel == null
                                                select a.nTerm.Trim()
                                              ).ToList();

                                var listStudentClassroomHistory = dbSchool.TStudentClassroomHistories.Where(c => c.sID == userMasterObj.sID && listTerm.Contains(c.nTerm)).ToList();
                                foreach (var termID in listTerm)
                                {
                                    stepLog = 7; // Step : Student classroom history data
                                    var StudentClassroomHistoryObj = listStudentClassroomHistory.FirstOrDefault(f => f.nTerm.Trim() == termID.Trim());
                                    if (StudentClassroomHistoryObj is null)
                                    {
                                        dbSchool.TStudentClassroomHistories.Add(new TStudentClassroomHistory
                                        {
                                            sID = userMasterObj.sID,
                                            nTerm = termID,
                                            nTermSubLevel2 = classroom,
                                            SchoolID = schoolID,
                                            nStudentStatus = 0,
                                            nStudentNumber = studentData.StudentNumber,
                                            MoveInDate = studentData.dStudentMoveInDate,
                                            CreatedBy = userData.UserID,
                                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(0, 101)
                                        });
                                    }
                                    else
                                    {
                                        StudentClassroomHistoryObj.nTermSubLevel2 = classroom;
                                        StudentClassroomHistoryObj.nStudentNumber = studentData.StudentNumber;
                                        if (studentData.dStudentMoveInDate != null) StudentClassroomHistoryObj.MoveInDate = studentData.dStudentMoveInDate;
                                        StudentClassroomHistoryObj.cDel = false;
                                        StudentClassroomHistoryObj.UpdatedBy = userData.UserID;
                                        StudentClassroomHistoryObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(0, 102);
                                    }
                                }

                                dbSchool.SaveChanges();
                                //

                                // Save Family Data
                                if (studentData.FamilyData != null && sheetsBeProcess[2])
                                {
                                    var familyProfileObj = dbSchool.TFamilyProfiles.Where(w => w.sID == userMasterObj.sID && w.SchoolID == schoolID).FirstOrDefault();
                                    if (familyProfileObj == null)
                                    {
                                        stepLog = 8; // Step : New family data
                                        familyProfileObj = new TFamilyProfile
                                        {
                                            // จาก Tab ข้อมูลนักเรียน
                                            nSonTotal = studentData.iStudentSonTotal,
                                            nRelativeStudyHere = studentData.iStudentBrethrenStudyHere,

                                            // ข้อมูลผู้ปกครอง
                                            sFamilyTitle = studentData.FamilyData.iParentTitle?.ToString(),
                                            sFamilyName = studentData.FamilyData.ParentName,
                                            sFamilyLast = studentData.FamilyData.ParentLastName,
                                            sFamilyNameEN = studentData.FamilyData.ParentNameEn,
                                            sFamilyLastEN = studentData.FamilyData.ParentNameLastEn,
                                            dFamilyBirthDay = studentData.FamilyData.dParentBirthday,
                                            sFamilyIdCardNumber = studentData.FamilyData.ParentIdentification,
                                            sFamilyRace = studentData.FamilyData.ParentRace,
                                            sFamilyNation = studentData.FamilyData.ParentNation,
                                            sFamilyReligion = studentData.FamilyData.ParentReligion,
                                            sFamilyGraduated = studentData.FamilyData.iParentGraduated,
                                            sFamilyHomeNumber = studentData.FamilyData.ParentHomeNo,
                                            sFamilySoy = studentData.FamilyData.ParentSoi,
                                            sFamilyMuu = studentData.FamilyData.ParentMoo,
                                            sFamilyRoad = studentData.FamilyData.ParentRoad,
                                            sFamilyProvince = studentData.FamilyData.iParentProvince?.ToString(),
                                            sFamilyAumpher = studentData.FamilyData.iParentAmphoe?.ToString(),
                                            sFamilyTumbon = studentData.FamilyData.iParentTombon?.ToString(),
                                            sFamilyPost = studentData.FamilyData.ParentPostalCode,

                                            sFamilyRelate = studentData.FamilyData.ParentRelate,
                                            nFamilyRequestStudyMoney = studentData.FamilyData.iParentRequestStudyMoney,
                                            familyStatus = studentData.FamilyData.iParentStatus,

                                            sFamilyJob = studentData.FamilyData.ParentJob,
                                            nFamilyIncome = studentData.FamilyData.dParentIncome,
                                            sFamilyWorkPlace = studentData.FamilyData.ParentWorkPlace,
                                            sPhoneOne = studentData.FamilyData.ParentPhone,
                                            sPhoneTwo = studentData.FamilyData.ParentPhone2,
                                            sPhoneThree = studentData.FamilyData.ParentPhone3,


                                            // ข้อมูลมารดา
                                            sMotherTitle = studentData.FamilyData.iMotherTitle?.ToString(),
                                            sMotherFirstName = studentData.FamilyData.MotherName,
                                            sMotherLastName = studentData.FamilyData.MotherLastName,
                                            sMotherNameEN = studentData.FamilyData.MotherNameEn,
                                            sMotherLastEN = studentData.FamilyData.MotherNameLastEn,
                                            dMotherBirthDay = studentData.FamilyData.dMotherBirthday, //
                                            sMotherIdCardNumber = studentData.FamilyData.MotherIdentification,
                                            sMotherRace = studentData.FamilyData.MotherRace,
                                            sMotherNation = studentData.FamilyData.MotherNation,
                                            sMotherReligion = studentData.FamilyData.MotherReligion,
                                            sMotherGraduated = studentData.FamilyData.iMotherGraduated,
                                            sMotherHomeNumber = studentData.FamilyData.MotherHomeNo,
                                            sMotherSoy = studentData.FamilyData.MotherSoi,
                                            sMotherMuu = studentData.FamilyData.MotherMoo,
                                            sMotherRoad = studentData.FamilyData.MotherRoad,
                                            sMotherProvince = studentData.FamilyData.iMotherProvince?.ToString(),
                                            sMotherAumpher = studentData.FamilyData.iMotherAmphoe?.ToString(),
                                            sMotherTumbon = studentData.FamilyData.iMotherTombon?.ToString(),
                                            sMotherPost = studentData.FamilyData.MotherPostalCode,

                                            sMotherJob = studentData.FamilyData.MotherJob,
                                            nMotherIncome = studentData.FamilyData.dMotherIncome,
                                            sMotherWorkPlace = studentData.FamilyData.MotherWorkPlace,
                                            sMotherPhone = studentData.FamilyData.MotherPhone,
                                            sMotherPhone2 = studentData.FamilyData.MotherPhone2,
                                            sMotherPhone3 = studentData.FamilyData.MotherPhone3,


                                            // ข้อมูลบิดา
                                            sFatherTitle = studentData.FamilyData.iFatherTitle?.ToString(),
                                            sFatherFirstName = studentData.FamilyData.FatherName,
                                            sFatherLastName = studentData.FamilyData.FatherLastName,
                                            sFatherNameEN = studentData.FamilyData.FatherNameEn,
                                            sFatherLastEN = studentData.FamilyData.FatherNameLastEn,
                                            dFatherBirthDay = studentData.FamilyData.dFatherBirthday,
                                            sFatherIdCardNumber = studentData.FamilyData.FatherIdentification,
                                            sFatherRace = studentData.FamilyData.FatherRace,
                                            sFatherNation = studentData.FamilyData.FatherNation,
                                            sFatherReligion = studentData.FamilyData.FatherReligion,
                                            sFatherGraduated = studentData.FamilyData.iFatherGraduated,
                                            sFatherHomeNumber = studentData.FamilyData.FatherHomeNo,
                                            sFatherSoy = studentData.FamilyData.FatherSoi,
                                            sFatherMuu = studentData.FamilyData.FatherMoo,
                                            sFatherRoad = studentData.FamilyData.FatherRoad,
                                            sFatherProvince = studentData.FamilyData.iFatherProvince?.ToString(),
                                            sFatherAumpher = studentData.FamilyData.iFatherAmphoe?.ToString(),
                                            sFatherTumbon = studentData.FamilyData.iFatherTombon?.ToString(),
                                            sFatherPost = studentData.FamilyData.FatherPostalCode,

                                            sFatherJob = studentData.FamilyData.FatherJob,
                                            nFatherIncome = studentData.FamilyData.dFatherIncome,
                                            sFatherWorkPlace = studentData.FamilyData.FatherWorkPlace,
                                            sFatherPhone = studentData.FamilyData.FatherPhone,
                                            sFatherPhone2 = studentData.FamilyData.FatherPhone2,
                                            sFatherPhone3 = studentData.FamilyData.FatherPhone3,


                                            // ที่อยู่ตามทะเบียนบ้าน
                                            houseRegistrationNumber = studentData.RegisterHomeNo,
                                            houseRegistrationSoy = studentData.RegisterHomeSoi,
                                            houseRegistrationMuu = studentData.RegisterHomeMoo,
                                            houseRegistrationRoad = studentData.RegisterHomeRoad,
                                            houseRegistrationProvince = studentData.iRegisterHomeProvince,
                                            houseRegistrationAumpher = studentData.iRegisterHomeAmphoe,
                                            houseRegistrationTumbon = studentData.iRegisterHomeTombon,
                                            houseRegistrationPost = studentData.RegisterHomePostalCode,
                                            houseRegistrationPhone = studentData.RegisterHomePhone,
                                            bornFrom = studentData.BornFrom,
                                            bornFromProvince = studentData.iBornFromProvince,
                                            bornFromAumpher = studentData.iBornFromAmphoe,
                                            bornFromTumbon = studentData.iBornFromTombon,


                                            // ที่อยู่ปัจจุบัน (จาก Tab ข้อมูลนักเรียน)
                                            stayWithTitle = studentData.iHomeStayWithTitle,
                                            stayWithName = studentData.HomeStayWithName,
                                            stayWithLast = studentData.HomeStayWithLast,
                                            stayWithEmergencyCall = studentData.HomeStayWithEmergencyCall,
                                            stayWithEmail = studentData.HomeStayWithEmergencyEmail,
                                            friendName = studentData.HomeFriendName,
                                            friendLastName = studentData.HomeFriendLastName,
                                            friendPhone = studentData.HomeFriendPhone,
                                            HomeType = studentData.iHomeHomeType,


                                            sID = userMasterObj.sID,
                                            sDeleted = "false",
                                            SchoolID = schoolID,
                                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(0, 1)
                                        };

                                        dbSchool.TFamilyProfiles.Add(familyProfileObj);
                                    }
                                    else
                                    {
                                        stepLog = 9; // Step : Modify family data
                                                     // จาก Tab ข้อมูลนักเรียน
                                        if (studentData.iStudentSonTotal != null) familyProfileObj.nSonTotal = studentData.iStudentSonTotal;
                                        if (studentData.iStudentBrethrenStudyHere != null) familyProfileObj.nRelativeStudyHere = studentData.iStudentBrethrenStudyHere;

                                        stepLog = 9.1; // Step : Modify family data (Parent)
                                                       // ข้อมูลผู้ปกครอง
                                        if (studentData.FamilyData.iParentTitle != null) familyProfileObj.sFamilyTitle = studentData.FamilyData.iParentTitle?.ToString();
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentName)) familyProfileObj.sFamilyName = studentData.FamilyData.ParentName;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentLastName)) familyProfileObj.sFamilyLast = studentData.FamilyData.ParentLastName;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentNameEn)) familyProfileObj.sFamilyNameEN = studentData.FamilyData.ParentNameEn;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentNameLastEn)) familyProfileObj.sFamilyLastEN = studentData.FamilyData.ParentNameLastEn;
                                        if (studentData.FamilyData.dParentBirthday != null) familyProfileObj.dFamilyBirthDay = studentData.FamilyData.dParentBirthday;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentIdentification)) familyProfileObj.sFamilyIdCardNumber = studentData.FamilyData.ParentIdentification;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentRace)) familyProfileObj.sFamilyRace = studentData.FamilyData.ParentRace;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentNation)) familyProfileObj.sFamilyNation = studentData.FamilyData.ParentNation;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentReligion)) familyProfileObj.sFamilyReligion = studentData.FamilyData.ParentReligion;
                                        if (studentData.FamilyData.iParentGraduated != null) familyProfileObj.sFamilyGraduated = studentData.FamilyData.iParentGraduated;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentHomeNo)) familyProfileObj.sFamilyHomeNumber = studentData.FamilyData.ParentHomeNo;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentSoi)) familyProfileObj.sFamilySoy = studentData.FamilyData.ParentSoi;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentMoo)) familyProfileObj.sFamilyMuu = studentData.FamilyData.ParentMoo;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentRoad)) familyProfileObj.sFamilyRoad = studentData.FamilyData.ParentRoad;
                                        if (studentData.FamilyData.iParentProvince != null) familyProfileObj.sFamilyProvince = studentData.FamilyData.iParentProvince?.ToString();
                                        if (studentData.FamilyData.iParentAmphoe != null) familyProfileObj.sFamilyAumpher = studentData.FamilyData.iParentAmphoe?.ToString();
                                        if (studentData.FamilyData.iParentTombon != null) familyProfileObj.sFamilyTumbon = studentData.FamilyData.iParentTombon?.ToString();
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentPostalCode)) familyProfileObj.sFamilyPost = studentData.FamilyData.ParentPostalCode;

                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentRelate)) familyProfileObj.sFamilyRelate = studentData.FamilyData.ParentRelate;
                                        if (studentData.FamilyData.iParentRequestStudyMoney != null) familyProfileObj.nFamilyRequestStudyMoney = studentData.FamilyData.iParentRequestStudyMoney;
                                        if (studentData.FamilyData.iParentStatus != null) familyProfileObj.familyStatus = studentData.FamilyData.iParentStatus;

                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentJob)) familyProfileObj.sFamilyJob = studentData.FamilyData.ParentJob;
                                        if (studentData.FamilyData.dParentIncome != null) familyProfileObj.nFamilyIncome = studentData.FamilyData.dParentIncome;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentWorkPlace)) familyProfileObj.sFamilyWorkPlace = studentData.FamilyData.ParentWorkPlace;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentPhone)) familyProfileObj.sPhoneOne = studentData.FamilyData.ParentPhone;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentPhone2)) familyProfileObj.sPhoneTwo = studentData.FamilyData.ParentPhone2;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.ParentPhone3)) familyProfileObj.sPhoneThree = studentData.FamilyData.ParentPhone3;

                                        stepLog = 9.2; // Step : Modify family data (Mother)
                                                       // ข้อมูลมารดา
                                        if (studentData.FamilyData.iMotherTitle != null) familyProfileObj.sMotherTitle = studentData.FamilyData.iMotherTitle?.ToString();
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherName)) familyProfileObj.sMotherFirstName = studentData.FamilyData.MotherName;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherLastName)) familyProfileObj.sMotherLastName = studentData.FamilyData.MotherLastName;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherNameEn)) familyProfileObj.sMotherNameEN = studentData.FamilyData.MotherNameEn;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherNameLastEn)) familyProfileObj.sMotherLastEN = studentData.FamilyData.MotherNameLastEn;
                                        if (studentData.FamilyData.dMotherBirthday != null) familyProfileObj.dMotherBirthDay = studentData.FamilyData.dMotherBirthday;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherIdentification)) familyProfileObj.sMotherIdCardNumber = studentData.FamilyData.MotherIdentification;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherRace)) familyProfileObj.sMotherRace = studentData.FamilyData.MotherRace;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherNation)) familyProfileObj.sMotherNation = studentData.FamilyData.MotherNation;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherReligion)) familyProfileObj.sMotherReligion = studentData.FamilyData.MotherReligion;
                                        if (studentData.FamilyData.iMotherGraduated != null) familyProfileObj.sMotherGraduated = studentData.FamilyData.iMotherGraduated;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherHomeNo)) familyProfileObj.sMotherHomeNumber = studentData.FamilyData.MotherHomeNo;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherSoi)) familyProfileObj.sMotherSoy = studentData.FamilyData.MotherSoi;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherMoo)) familyProfileObj.sMotherMuu = studentData.FamilyData.MotherMoo;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherRoad)) familyProfileObj.sMotherRoad = studentData.FamilyData.MotherRoad;
                                        if (studentData.FamilyData.iMotherProvince != null) familyProfileObj.sMotherProvince = studentData.FamilyData.iMotherProvince?.ToString();
                                        if (studentData.FamilyData.iMotherAmphoe != null) familyProfileObj.sMotherAumpher = studentData.FamilyData.iMotherAmphoe?.ToString();
                                        if (studentData.FamilyData.iMotherTombon != null) familyProfileObj.sMotherTumbon = studentData.FamilyData.iMotherTombon?.ToString();
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherPostalCode)) familyProfileObj.sMotherPost = studentData.FamilyData.MotherPostalCode;

                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherJob)) familyProfileObj.sMotherJob = studentData.FamilyData.MotherJob;
                                        if (studentData.FamilyData.dMotherIncome != null) familyProfileObj.nMotherIncome = studentData.FamilyData.dMotherIncome;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherWorkPlace)) familyProfileObj.sMotherWorkPlace = studentData.FamilyData.MotherWorkPlace;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherPhone)) familyProfileObj.sMotherPhone = studentData.FamilyData.MotherPhone;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherPhone2)) familyProfileObj.sMotherPhone2 = studentData.FamilyData.MotherPhone2;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.MotherPhone3)) familyProfileObj.sMotherPhone3 = studentData.FamilyData.MotherPhone3;

                                        stepLog = 9.3; // Step : Modify family data (Father)
                                                       // ข้อมูลบิดา
                                        if (studentData.FamilyData.iFatherTitle != null) familyProfileObj.sFatherTitle = studentData.FamilyData.iFatherTitle?.ToString();
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherName)) familyProfileObj.sFatherFirstName = studentData.FamilyData.FatherName;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherLastName)) familyProfileObj.sFatherLastName = studentData.FamilyData.FatherLastName;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherNameEn)) familyProfileObj.sFatherNameEN = studentData.FamilyData.FatherNameEn;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherNameLastEn)) familyProfileObj.sFatherLastEN = studentData.FamilyData.FatherNameLastEn;
                                        if (studentData.FamilyData.dFatherBirthday != null) familyProfileObj.dFatherBirthDay = studentData.FamilyData.dFatherBirthday;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherIdentification)) familyProfileObj.sFatherIdCardNumber = studentData.FamilyData.FatherIdentification;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherRace)) familyProfileObj.sFatherRace = studentData.FamilyData.FatherRace;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherNation)) familyProfileObj.sFatherNation = studentData.FamilyData.FatherNation;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherReligion)) familyProfileObj.sFatherReligion = studentData.FamilyData.FatherReligion;
                                        if (studentData.FamilyData.iFatherGraduated != null) familyProfileObj.sFatherGraduated = studentData.FamilyData.iFatherGraduated;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherHomeNo)) familyProfileObj.sFatherHomeNumber = studentData.FamilyData.FatherHomeNo;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherSoi)) familyProfileObj.sFatherSoy = studentData.FamilyData.FatherSoi;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherMoo)) familyProfileObj.sFatherMuu = studentData.FamilyData.FatherMoo;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherRoad)) familyProfileObj.sFatherRoad = studentData.FamilyData.FatherRoad;
                                        if (studentData.FamilyData.iFatherProvince != null) familyProfileObj.sFatherProvince = studentData.FamilyData.iFatherProvince?.ToString();
                                        if (studentData.FamilyData.iFatherAmphoe != null) familyProfileObj.sFatherAumpher = studentData.FamilyData.iFatherAmphoe?.ToString();
                                        if (studentData.FamilyData.iFatherTombon != null) familyProfileObj.sFatherTumbon = studentData.FamilyData.iFatherTombon?.ToString();
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherPostalCode)) familyProfileObj.sFatherPost = studentData.FamilyData.FatherPostalCode;

                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherJob)) familyProfileObj.sFatherJob = studentData.FamilyData.FatherJob;
                                        if (studentData.FamilyData.dFatherIncome != null) familyProfileObj.nFatherIncome = studentData.FamilyData.dFatherIncome;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherWorkPlace)) familyProfileObj.sFatherWorkPlace = studentData.FamilyData.FatherWorkPlace;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherPhone)) familyProfileObj.sFatherPhone = studentData.FamilyData.FatherPhone;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherPhone2)) familyProfileObj.sFatherPhone2 = studentData.FamilyData.FatherPhone2;
                                        if (!string.IsNullOrEmpty(studentData.FamilyData.FatherPhone3)) familyProfileObj.sFatherPhone3 = studentData.FamilyData.FatherPhone3;


                                        // ที่อยู่ตามทะเบียนบ้าน
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomeNo)) familyProfileObj.houseRegistrationNumber = studentData.RegisterHomeNo;
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomeSoi)) familyProfileObj.houseRegistrationSoy = studentData.RegisterHomeSoi;
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomeMoo)) familyProfileObj.houseRegistrationMuu = studentData.RegisterHomeMoo;
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomeRoad)) familyProfileObj.houseRegistrationRoad = studentData.RegisterHomeRoad;
                                        if (studentData.iRegisterHomeProvince != null) familyProfileObj.houseRegistrationProvince = studentData.iRegisterHomeProvince;
                                        if (studentData.iRegisterHomeAmphoe != null) familyProfileObj.houseRegistrationAumpher = studentData.iRegisterHomeAmphoe;
                                        if (studentData.iRegisterHomeTombon != null) familyProfileObj.houseRegistrationTumbon = studentData.iRegisterHomeTombon;
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomePostalCode)) familyProfileObj.houseRegistrationPost = studentData.RegisterHomePostalCode;
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomePhone)) familyProfileObj.houseRegistrationPhone = studentData.RegisterHomePhone;
                                        if (!string.IsNullOrEmpty(studentData.BornFrom)) familyProfileObj.bornFrom = studentData.BornFrom;
                                        if (studentData.iBornFromProvince != null) familyProfileObj.bornFromProvince = studentData.iBornFromProvince;
                                        if (studentData.iBornFromAmphoe != null) familyProfileObj.bornFromAumpher = studentData.iBornFromAmphoe;
                                        if (studentData.iBornFromTombon != null) familyProfileObj.bornFromTumbon = studentData.iBornFromTombon;


                                        // ที่อยู่ปัจจุบัน (จาก Tab ข้อมูลนักเรียน)
                                        if (studentData.iHomeStayWithTitle != null) familyProfileObj.stayWithTitle = studentData.iHomeStayWithTitle;
                                        if (!string.IsNullOrEmpty(studentData.HomeStayWithName)) familyProfileObj.stayWithName = studentData.HomeStayWithName;
                                        if (!string.IsNullOrEmpty(studentData.HomeStayWithLast)) familyProfileObj.stayWithLast = studentData.HomeStayWithLast;
                                        if (!string.IsNullOrEmpty(studentData.HomeStayWithEmergencyCall)) familyProfileObj.stayWithEmergencyCall = studentData.HomeStayWithEmergencyCall;
                                        if (!string.IsNullOrEmpty(studentData.HomeStayWithEmergencyEmail)) familyProfileObj.stayWithEmail = studentData.HomeStayWithEmergencyEmail;
                                        if (!string.IsNullOrEmpty(studentData.HomeFriendName)) familyProfileObj.friendName = studentData.HomeFriendName;
                                        if (!string.IsNullOrEmpty(studentData.HomeFriendLastName)) familyProfileObj.friendLastName = studentData.HomeFriendLastName;
                                        if (!string.IsNullOrEmpty(studentData.HomeFriendPhone)) familyProfileObj.friendPhone = studentData.HomeFriendPhone;
                                        if (studentData.iHomeHomeType != null) familyProfileObj.HomeType = studentData.iHomeHomeType;


                                        familyProfileObj.sDeleted = "false";
                                        familyProfileObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(0, 1);
                                    }

                                    dbSchool.SaveChanges();
                                }
                                else
                                {
                                    var familyProfileObj = dbSchool.TFamilyProfiles.Where(w => w.sID == userMasterObj.sID && w.SchoolID == schoolID).FirstOrDefault();
                                    if (familyProfileObj == null)
                                    {
                                        stepLog = 10; // Step : New family data(no tab family data)
                                        familyProfileObj = new TFamilyProfile
                                        {
                                            // จาก Tab ข้อมูลนักเรียน
                                            nSonTotal = studentData.iStudentSonTotal,
                                            nRelativeStudyHere = studentData.iStudentBrethrenStudyHere,

                                            // ที่อยู่ตามทะเบียนบ้าน
                                            houseRegistrationNumber = studentData.RegisterHomeNo,
                                            houseRegistrationSoy = studentData.RegisterHomeSoi,
                                            houseRegistrationMuu = studentData.RegisterHomeMoo,
                                            houseRegistrationRoad = studentData.RegisterHomeRoad,
                                            houseRegistrationProvince = studentData.iRegisterHomeProvince,
                                            houseRegistrationAumpher = studentData.iRegisterHomeAmphoe,
                                            houseRegistrationTumbon = studentData.iRegisterHomeTombon,
                                            houseRegistrationPost = studentData.RegisterHomePostalCode,
                                            houseRegistrationPhone = studentData.RegisterHomePhone,
                                            bornFrom = studentData.BornFrom,
                                            bornFromProvince = studentData.iBornFromProvince,
                                            bornFromAumpher = studentData.iBornFromAmphoe,
                                            bornFromTumbon = studentData.iBornFromTombon,

                                            // ที่อยู่ปัจจุบัน (จาก Tab ข้อมูลนักเรียน)
                                            stayWithTitle = studentData.iHomeStayWithTitle,
                                            stayWithName = studentData.HomeStayWithName,
                                            stayWithLast = studentData.HomeStayWithLast,
                                            stayWithEmergencyCall = studentData.HomeStayWithEmergencyCall,
                                            stayWithEmail = studentData.HomeStayWithEmergencyEmail,
                                            friendName = studentData.HomeFriendName,
                                            friendLastName = studentData.HomeFriendLastName,
                                            friendPhone = studentData.HomeFriendPhone,
                                            HomeType = studentData.iHomeHomeType,

                                            sID = userMasterObj.sID,
                                            sDeleted = "false",
                                            SchoolID = schoolID,
                                            CreatedBy = userData.UserID,
                                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(0, 101)
                                        };

                                        dbSchool.TFamilyProfiles.Add(familyProfileObj);
                                    }
                                    else
                                    {
                                        stepLog = 11; // Step : Modify family data(no tab family data)
                                                      // จาก Tab ข้อมูลนักเรียน
                                        if (studentData.iStudentSonTotal != null) familyProfileObj.nSonTotal = studentData.iStudentSonTotal;
                                        if (studentData.iStudentBrethrenStudyHere != null) familyProfileObj.nRelativeStudyHere = studentData.iStudentBrethrenStudyHere;

                                        // ที่อยู่ตามทะเบียนบ้าน
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomeNo)) familyProfileObj.houseRegistrationNumber = studentData.RegisterHomeNo;
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomeSoi)) familyProfileObj.houseRegistrationSoy = studentData.RegisterHomeSoi;
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomeMoo)) familyProfileObj.houseRegistrationMuu = studentData.RegisterHomeMoo;
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomeRoad)) familyProfileObj.houseRegistrationRoad = studentData.RegisterHomeRoad;
                                        if (studentData.iRegisterHomeProvince != null) familyProfileObj.houseRegistrationProvince = studentData.iRegisterHomeProvince;
                                        if (studentData.iRegisterHomeAmphoe != null) familyProfileObj.houseRegistrationAumpher = studentData.iRegisterHomeAmphoe;
                                        if (studentData.iRegisterHomeTombon != null) familyProfileObj.houseRegistrationTumbon = studentData.iRegisterHomeTombon;
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomePostalCode)) familyProfileObj.houseRegistrationPost = studentData.RegisterHomePostalCode;
                                        if (!string.IsNullOrEmpty(studentData.RegisterHomePhone)) familyProfileObj.houseRegistrationPhone = studentData.RegisterHomePhone;
                                        if (!string.IsNullOrEmpty(studentData.BornFrom)) familyProfileObj.bornFrom = studentData.BornFrom;
                                        if (studentData.iBornFromProvince != null) familyProfileObj.bornFromProvince = studentData.iBornFromProvince;
                                        if (studentData.iBornFromAmphoe != null) familyProfileObj.bornFromAumpher = studentData.iBornFromAmphoe;
                                        if (studentData.iBornFromTombon != null) familyProfileObj.bornFromTumbon = studentData.iBornFromTombon;

                                        // ที่อยู่ปัจจุบัน (จาก Tab ข้อมูลนักเรียน)
                                        if (studentData.iHomeStayWithTitle != null) familyProfileObj.stayWithTitle = studentData.iHomeStayWithTitle;
                                        if (!string.IsNullOrEmpty(studentData.HomeStayWithName)) familyProfileObj.stayWithName = studentData.HomeStayWithName;
                                        if (!string.IsNullOrEmpty(studentData.HomeStayWithLast)) familyProfileObj.stayWithLast = studentData.HomeStayWithLast;
                                        if (!string.IsNullOrEmpty(studentData.HomeStayWithEmergencyCall)) familyProfileObj.stayWithEmergencyCall = studentData.HomeStayWithEmergencyCall;
                                        if (!string.IsNullOrEmpty(studentData.HomeStayWithEmergencyEmail)) familyProfileObj.stayWithEmail = studentData.HomeStayWithEmergencyEmail;
                                        if (!string.IsNullOrEmpty(studentData.HomeFriendName)) familyProfileObj.friendName = studentData.HomeFriendName;
                                        if (!string.IsNullOrEmpty(studentData.HomeFriendLastName)) familyProfileObj.friendLastName = studentData.HomeFriendLastName;
                                        if (!string.IsNullOrEmpty(studentData.HomeFriendPhone)) familyProfileObj.friendPhone = studentData.HomeFriendPhone;
                                        if (studentData.iHomeHomeType != null) familyProfileObj.HomeType = studentData.iHomeHomeType;

                                        familyProfileObj.sDeleted = "false";
                                        familyProfileObj.UpdatedBy = userData.UserID;
                                        familyProfileObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(0, 102);
                                    }

                                    dbSchool.SaveChanges();
                                }
                                //

                                // Save Health Data
                                if (studentData.HealthData != null && sheetsBeProcess[3])
                                {
                                    var HealthInfoObj = dbSchool.TStudentHealthInfoes.Where(w => w.sID == userMasterObj.sID).FirstOrDefault();
                                    if (HealthInfoObj == null)
                                    {
                                        stepLog = 12; // Step : New health info

                                        int HealthID = 1;

                                        if (dbSchool.TStudentHealthInfoes.Count(w => w.SchoolID == schoolID) > 0)
                                        {
                                            HealthID = dbSchool.TStudentHealthInfoes.Where(w => w.SchoolID == schoolID).Max(m => m.nHealthID) + 1;
                                        }

                                        HealthInfoObj = new TStudentHealthInfo
                                        {
                                            nHealthID = HealthID,
                                            sID = userMasterObj.sID,
                                            sBlood = studentData.HealthData.HealthBlood,
                                            sSickFood = studentData.HealthData.HealthSickFood,
                                            sSickDrug = studentData.HealthData.HealthSickDrug,
                                            sSickNormal = studentData.HealthData.HealthSickCongenital,
                                            sSickDanger = studentData.HealthData.HealthSickDanger,
                                            sSickOther = studentData.HealthData.HealthSickOther,
                                            sDeleted = "false",
                                            SchoolID = schoolID,
                                            CreatedBy = userData.UserID,
                                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(0, 101)
                                        };

                                        dbSchool.TStudentHealthInfoes.Add(HealthInfoObj);
                                    }
                                    else
                                    {
                                        stepLog = 13; // Step : Modify health info
                                        if (!string.IsNullOrEmpty(studentData.HealthData.HealthBlood)) HealthInfoObj.sBlood = studentData.HealthData.HealthBlood;
                                        if (!string.IsNullOrEmpty(studentData.HealthData.HealthSickFood)) HealthInfoObj.sSickFood = studentData.HealthData.HealthSickFood;
                                        if (!string.IsNullOrEmpty(studentData.HealthData.HealthSickDrug)) HealthInfoObj.sSickDrug = studentData.HealthData.HealthSickDrug;
                                        if (!string.IsNullOrEmpty(studentData.HealthData.HealthSickCongenital)) HealthInfoObj.sSickNormal = studentData.HealthData.HealthSickCongenital;
                                        if (!string.IsNullOrEmpty(studentData.HealthData.HealthSickDanger)) HealthInfoObj.sSickDanger = studentData.HealthData.HealthSickDanger;
                                        if (!string.IsNullOrEmpty(studentData.HealthData.HealthSickOther)) HealthInfoObj.sSickOther = studentData.HealthData.HealthSickOther;
                                        HealthInfoObj.sDeleted = "false";
                                        HealthInfoObj.UpdatedBy = userData.UserID;
                                        HealthInfoObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(0, 102);
                                    }

                                    dbSchool.SaveChanges();

                                    var levelObj = dbSchool.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == classroom && f.SchoolID == schoolID);

                                    if (dbSchool.TStudentHealthGrowths.Count(c => c.nHealthID == HealthInfoObj.nHealthID && c.SchoolID == schoolID && c.nTSubLevel == levelObj.nTSubLevel) == 0)
                                    {
                                        stepLog = 14; // Step : New health growth
                                        foreach (var nMonth in new List<int> { 2, 5, 8, 11 })
                                        {
                                            dbSchool.TStudentHealthGrowths.Add(new TStudentHealthGrowth
                                            {
                                                nHealthID = HealthInfoObj.nHealthID,
                                                nMonth = nMonth,
                                                SchoolID = schoolID,
                                                nTSubLevel = levelObj.nTSubLevel,
                                                Height = studentData.HealthData.dHeight,
                                                Weight = studentData.HealthData.dWeight,
                                                CreatedDate = DateTime.Now.FixSecondAndMillisecond(0, 101)
                                            });
                                        }
                                    }
                                    else
                                    {
                                        stepLog = 15; // Step : Modify health growth
                                        var listHealthGrowth = dbSchool.TStudentHealthGrowths.Where(c => c.nHealthID == HealthInfoObj.nHealthID && c.SchoolID == schoolID && c.nTSubLevel == levelObj.nTSubLevel);
                                        foreach (var healthGrowth in listHealthGrowth)
                                        {
                                            if (studentData.HealthData.dHeight != null) healthGrowth.Height = studentData.HealthData.dHeight;
                                            if (studentData.HealthData.dWeight != null) healthGrowth.Weight = studentData.HealthData.dWeight;
                                            healthGrowth.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(0, 102);
                                        }
                                    }

                                    dbSchool.SaveChanges();

                                }
                                //



                                try
                                {
                                    JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(schoolID, userMasterObj.sID + "", "0", "");
                                    UpdateMemory memory = new UpdateMemory(userData.AuthKey, userData.AuthValue);
                                    memory.Student(userSchoolObj, userMasterObj);
                                }
                                catch { }

                                processData.Status = string.Format(@"success[{0}]", processData.Method);
                            }
                            catch (TransactionAbortedException ex)
                            {


                                processData.Errors.Add(new Error { Code = stepLog.ToString("000"), Log = SaveDataStepLogMessage(stepLog), Message = ex.Message, Status = "error" });
                                processData.Status = "error";
                            }
                            catch (DbEntityValidationException ex)
                            {


                                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                                {
                                    // Get entry
                                    DbEntityEntry entry = item.Entry;
                                    switch (entry.State)
                                    {
                                        case EntityState.Added:
                                            entry.State = EntityState.Detached;
                                            break;
                                        case EntityState.Modified:
                                            entry.CurrentValues.SetValues(entry.OriginalValues);
                                            entry.State = EntityState.Unchanged;
                                            break;
                                        case EntityState.Deleted:
                                            entry.State = EntityState.Unchanged;
                                            break;
                                    }
                                }

                                processData.Errors.Add(new Error { Code = stepLog.ToString("000"), Log = SaveDataStepLogMessage(stepLog), Message = ex.Message, Status = "error" });
                                processData.Status = "error";
                            }
                            catch (Exception ex)
                            {


                                processData.Errors.Add(new Error { Code = stepLog.ToString("000"), Log = SaveDataStepLogMessage(stepLog), Message = ex.Message, Status = "error" });
                                processData.Status = "error";
                            }

                        }
                        else
                        {
                            processData.Errors.Add(new Error { Code = stepLog.ToString("000"), Log = SaveDataStepLogMessage(stepLog), Message = string.Format(@"Found data already in system. [GRADUATED: {0}, PREVYEAR: {1}, INYEAR: {2}, NEXTYEAR: {3}, CINYEAR: {4}]", (graduatedData == null ? 0 : graduatedData.Count), (studyYearData == null ? 0 : studyYearData.PreviousYear), (studyYearData == null ? 0 : studyYearData.StudyInYear), (studyYearData == null ? 0 : studyYearData.StudyNextYear), (studyYearData == null ? 0 : studyYearData.CountStudyInYear)), Status = "error" });
                            processData.Status = "error";
                        }

                        processDatas.Add(processData);

                        rowIndex++;
                    }
                    // End loop foreach (var studentData in listStudent)

                    // Final check status
                    int errorCount = processDatas.Where(w => w.Status == "error").Count();
                    if (errorCount > 0)
                    {
                        success = false;
                        code = "500";
                        message = "Some data is missing or some data is inaccurate.";
                    }

                    HttpContext.Current.Session["StudentImportPercentage"] = 100M;
                }
            }

            // Log
            database.InsertLog(userData.UserID.ToString(), "นำเข้าข้อมูลนักเรียน [file: " + fileName + ", year: " + year + ", classroom: " + classroom + "]", HttpContext.Current.Request, 14, 2, 0, schoolID);

            var result = new { success, code, message, processDatas };

            return JsonConvert.SerializeObject(result);
        }

        private static string RandomNumber(JabJaiMasterEntities dbMaster)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            var q_user = (from a in dbMaster.TUsers
                          where string.IsNullOrEmpty(a.sFinger)
                          select new
                          {
                              a.sPassword
                          }).ToList();

            do
            {
                numIterations = rand.Next(100000, 999999);

            } while (q_user.Where(w => w.sPassword == numIterations.ToString()).ToList().Count > 0);

            return numIterations.ToString();
        }

        private static string SaveDataStepLogMessage(double stepLog)
        {
            string stepLogMessage = "";
            switch (stepLog)
            {
                case 0: stepLogMessage = "Found Student ID with status Studying or Graduated."; break;
                case 1: stepLogMessage = "In the process of adding master user data."; break;
                case 2: stepLogMessage = "In the process of editing master user data."; break;
                case 3: stepLogMessage = "In the process of adding school user data."; break;
                case 4: stepLogMessage = "In the process of adding school user data(Education data)."; break;
                case 5: stepLogMessage = "In the process of editing school user data."; break;
                case 6: stepLogMessage = "In the process of editing school user data(Education data)."; break;
                case 7: stepLogMessage = "In the process of student classroom history data."; break;
                case 8: stepLogMessage = "In the process of adding family data."; break;
                case 9: stepLogMessage = "In the process of editing family data."; break;
                case 9.1: stepLogMessage = "In the process of editing family data(Parent)."; break;
                case 9.2: stepLogMessage = "In the process of editing family data(Mother)."; break;
                case 9.3: stepLogMessage = "In the process of editing family data(Father)."; break;
                case 10: stepLogMessage = "In the process of adding family data(No data on tab family)."; break;
                case 11: stepLogMessage = "In the process of editing family data(No data on tab family)."; break;
                case 12: stepLogMessage = "In the process of adding health info data."; break;
                case 13: stepLogMessage = "In the process of editing health info data."; break;
                case 14: stepLogMessage = "In the process of adding health growth data."; break;
                case 15: stepLogMessage = "In the process of editing health growth data."; break;
                default: break;
            }
            return stepLogMessage;
        }

        private static string PrepareDataStepLogMessage(double stepLog)
        {
            string stepLogMessage = "";
            switch (stepLog)
            {
                case 0: stepLogMessage = "Initial Loop."; break;
                case 1: stepLogMessage = "Prepare Student Data #ประวัติส่วนตัว."; break;
                case 2: stepLogMessage = "Prepare Student Data #ที่อยู่ตามทะเบียนบ้าน."; break;
                case 3: stepLogMessage = "Prepare Student Data #ที่อยู่ปัจจุบัน."; break;
                case 4: stepLogMessage = "Prepare Education Data."; break;
                case 5: stepLogMessage = "Prepare Family Data #ข้อมูลผู้ปกครอง."; break;
                case 6: stepLogMessage = "Prepare Family Data #ข้อมูลมารดา."; break;
                case 7: stepLogMessage = "Prepare Family Data #ข้อมูลบิดา."; break;
                case 8: stepLogMessage = "Prepare Health Data."; break;
                default: break;
            }
            return stepLogMessage;
        }

        private static string ErrorCodeMessage(string code)
        {
            string stepLogMessage = "";
            switch (code)
            {
                case "501": stepLogMessage = "Error upload excel file"; break;
                case "502": stepLogMessage = "Error prepare data to object set (Tab ข้อมูลนักเรียน)"; break;
                case "503": stepLogMessage = "Error prepare data to object set (Tab ข้อมูลทางการศึกษา)"; break;
                case "504": stepLogMessage = "Error prepare data to object set (Tab ข้อมูลผู้ปกครอง)"; break;
                case "505": stepLogMessage = "Error prepare data to object set (Tab ข้อมูลสุขภาพ)"; break;
                default: break;
            }
            return stepLogMessage;
        }

    }

    public class ProcessData
    {
        [JsonProperty(PropertyName = "no")]
        public int? No { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public string Lastname { get; set; }

        [JsonProperty(PropertyName = "idCardNumber")]
        public string IDCardNumber { get; set; }

        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "status")]
        // ready(Prepare Data), success(Save Data), warning, error
        public string Status { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public List<Error> Errors { get; set; }

        [JsonProperty(PropertyName = "inDB")]
        // true: update, false: new
        public bool? InDB { get; set; }
    }

    public class Error
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "log")]
        public string Log { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "status")]
        // warning, error 
        public string Status { get; set; }
    }

    public class StudentImportData
    {
        public int? StudentNumber { get; set; }
        public string StudentIdentification { get; set; }
        public string StudentID { get; set; }
        public DateTime? dStudentMoveInDate { get; set; }
        public string StudentMoveInDate { get; set; }
        public string StudentTitle { get; set; }
        public string StudentGender { get; set; }
        public string StudentFirstNameTh { get; set; }
        public string StudentLastNameTh { get; set; }
        public string StudentNickNameTh { get; set; }
        public string StudentNickNameEn { get; set; }
        public string StudentFirstNameEn { get; set; }
        public string StudentLastNameEn { get; set; }
        public string StudentFirstNameOther { get; set; }
        public string StudentLastNameOther { get; set; }
        public DateTime? dStudentBirthday { get; set; }
        public string StudentBirthday { get; set; }
        public string StudentPhone { get; set; }
        public string StudentEmail { get; set; }
        public int? iStudentClassRoom { get; set; }
        public string StudentClassRoom { get; set; }
        public string StudentRace { get; set; }
        public string StudentNation { get; set; }
        public string StudentReligion { get; set; }
        public string DisabilityCode { get; set; }
        public string DisadvantageCode { get; set; }
        public int? iStudentSonTotal { get; set; }
        public string StudentSonTotal { get; set; }
        public int? iStudentSonNumber { get; set; }
        public string StudentSonNumber { get; set; }
        public int? iStudentBrethrenStudyHere { get; set; }
        public string StudentBrethrenStudyHere { get; set; }
        public decimal? dnMax { get; set; }
        public string nMax { get; set; }
        public int? iJourneyType { get; set; }
        public string JourneyType { get; set; }
        public string DormitoryName { get; set; }
        public string Note2 { get; set; }

        public string Address { get; set; }
        public string RegisterHomeCode { get; set; }
        public string RegisterHomeNo { get; set; }
        public string RegisterHomeMoo { get; set; }
        public string RegisterHomeSoi { get; set; }
        public string RegisterHomeRoad { get; set; }
        public int? iRegisterHomeTombon { get; set; }
        public string RegisterHomeTombon { get; set; }
        public int? iRegisterHomeAmphoe { get; set; }
        public string RegisterHomeAmphoe { get; set; }
        public int? iRegisterHomeProvince { get; set; }
        public string RegisterHomeProvince { get; set; }
        public string RegisterHomePostalCode { get; set; }
        public string RegisterHomePhone { get; set; }
        public string BornFrom { get; set; }
        public int? iBornFromProvince { get; set; }
        public string BornFromProvince { get; set; }
        public int? iBornFromAmphoe { get; set; }
        public string BornFromAmphoe { get; set; }
        public int? iBornFromTombon { get; set; }
        public string BornFromTombon { get; set; }

        public string HomeNo { get; set; }
        public string HomeMoo { get; set; }
        public string HomeSoi { get; set; }
        public string HomeRoad { get; set; }
        public int? iHomeTombon { get; set; }
        public string HomeTombon { get; set; }
        public int? iHomeAmphoe { get; set; }
        public string HomeAmphoe { get; set; }
        public int? iHomeProvince { get; set; }
        public string HomeProvince { get; set; }
        public string HomePostalCode { get; set; }
        public string HomePhone { get; set; }
        public int? iHomeStayWithTitle { get; set; }
        public string HomeStayWithTitle { get; set; }
        public string HomeStayWithName { get; set; }
        public string HomeStayWithLast { get; set; }
        public string HomeStayWithEmergencyCall { get; set; }
        public string HomeStayWithEmergencyEmail { get; set; }
        public string HomeFriendName { get; set; }
        public string HomeFriendLastName { get; set; }
        public string HomeFriendPhone { get; set; }
        public int? iHomeHomeType { get; set; }
        public string HomeHomeType { get; set; }

        public EducationData EducationData { get; set; }
        public FamilyData FamilyData { get; set; }
        public HealthData HealthData { get; set; }
    }

    public class EducationData
    {
        public string StudentID { get; set; }
        public string OldSchoolName { get; set; }
        public int? iOldSchoolTombon { get; set; }
        public string OldSchoolTombon { get; set; }
        public int? iOldSchoolAmphoe { get; set; }
        public string OldSchoolAmphoe { get; set; }
        public int? iOldSchoolProvince { get; set; }
        public string OldSchoolProvince { get; set; }
        public string OldSchoolGraduated { get; set; }
        public decimal? dCredit { get; set; }
        public string Credit { get; set; }
        public string OldSchoolGPA { get; set; }
        public string MoveOutReason { get; set; }
    }

    public class FamilyData
    {
        public string StudentID { get; set; }
        public int? iParentTitle { get; set; }
        public string ParentTitle { get; set; }
        public string ParentName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentNameEn { get; set; }
        public string ParentNameLastEn { get; set; }
        public DateTime? dParentBirthday { get; set; }
        public string ParentBirthday { get; set; }
        public string ParentIdentification { get; set; }
        public string ParentRace { get; set; }
        public string ParentNation { get; set; }
        public string ParentReligion { get; set; }
        public int? iParentGraduated { get; set; }
        public string ParentGraduated { get; set; }
        public string ParentHomeNo { get; set; }
        public string ParentSoi { get; set; }
        public string ParentRoad { get; set; }
        public string ParentMoo { get; set; }
        public int? iParentTombon { get; set; }
        public string ParentTombon { get; set; }
        public int? iParentAmphoe { get; set; }
        public string ParentAmphoe { get; set; }
        public int? iParentProvince { get; set; }
        public string ParentProvince { get; set; }
        public string ParentPostalCode { get; set; }
        public string ParentRelate { get; set; }
        public int? iParentRequestStudyMoney { get; set; }
        public string ParentRequestStudyMoney { get; set; }
        public int? iParentStatus { get; set; }
        public string ParentStatus { get; set; }
        public string ParentJob { get; set; }
        public string ParentWorkPlace { get; set; }
        public double? dParentIncome { get; set; }
        public string ParentIncome { get; set; }
        public string ParentPhone { get; set; }
        public string ParentPhone2 { get; set; }
        public string ParentPhone3 { get; set; }

        public int? iMotherTitle { get; set; }
        public string MotherTitle { get; set; }
        public string MotherName { get; set; }
        public string MotherLastName { get; set; }
        public string MotherNameEn { get; set; }
        public string MotherNameLastEn { get; set; }
        public DateTime? dMotherBirthday { get; set; }
        public string MotherBirthday { get; set; }
        public string MotherIdentification { get; set; }
        public string MotherRace { get; set; }
        public string MotherNation { get; set; }
        public string MotherReligion { get; set; }
        public int? iMotherGraduated { get; set; }
        public string MotherGraduated { get; set; }
        public string MotherHomeNo { get; set; }
        public string MotherSoi { get; set; }
        public string MotherRoad { get; set; }
        public string MotherMoo { get; set; }
        public int? iMotherTombon { get; set; }
        public string MotherTombon { get; set; }
        public int? iMotherAmphoe { get; set; }
        public string MotherAmphoe { get; set; }
        public int? iMotherProvince { get; set; }
        public string MotherProvince { get; set; }
        public string MotherPostalCode { get; set; }
        public string MotherJob { get; set; }
        public string MotherWorkPlace { get; set; }
        public double? dMotherIncome { get; set; }
        public string MotherIncome { get; set; }
        public string MotherPhone { get; set; }
        public string MotherPhone2 { get; set; }
        public string MotherPhone3 { get; set; }

        public int? iFatherTitle { get; set; }
        public string FatherTitle { get; set; }
        public string FatherName { get; set; }
        public string FatherLastName { get; set; }
        public string FatherNameEn { get; set; }
        public string FatherNameLastEn { get; set; }
        public DateTime? dFatherBirthday { get; set; }
        public string FatherBirthday { get; set; }
        public string FatherIdentification { get; set; }
        public string FatherRace { get; set; }
        public string FatherNation { get; set; }
        public string FatherReligion { get; set; }
        public int? iFatherGraduated { get; set; }
        public string FatherGraduated { get; set; }
        public string FatherHomeNo { get; set; }
        public string FatherSoi { get; set; }
        public string FatherRoad { get; set; }
        public string FatherMoo { get; set; }
        public int? iFatherTombon { get; set; }
        public string FatherTombon { get; set; }
        public int? iFatherAmphoe { get; set; }
        public string FatherAmphoe { get; set; }
        public int? iFatherProvince { get; set; }
        public string FatherProvince { get; set; }
        public string FatherPostalCode { get; set; }
        public string FatherJob { get; set; }
        public string FatherWorkPlace { get; set; }
        public double? dFatherIncome { get; set; }
        public string FatherIncome { get; set; }
        public string FatherPhone { get; set; }
        public string FatherPhone2 { get; set; }
        public string FatherPhone3 { get; set; }
    }

    public class HealthData
    {
        public string StudentID { get; set; }
        public decimal? dHeight { get; set; }
        public string Height { get; set; }
        public decimal? dWeight { get; set; }
        public string Weight { get; set; }
        public string HealthBlood { get; set; }
        public string HealthSickFood { get; set; }
        public string HealthSickDrug { get; set; }
        public string HealthSickCongenital { get; set; }
        public string HealthSickDanger { get; set; }
        public string HealthSickOther { get; set; }
    }

    public class StudyYearData
    {
        public int sID { get; set; }
        public string StudentID { get; set; }
        public int PreviousYear { get; set; }
        public int StudyInYear { get; set; }
        public int StudyNextYear { get; set; }
        public int CountStudyInYear { get; set; }
    }

    public class GraduatedData
    {
        public int sID { get; set; }
        public string StudentID { get; set; }
        public int Count { get; set; }
    }

}