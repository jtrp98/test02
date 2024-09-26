using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Diploma
{
    public partial class DiplomaReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static string LoadDiplomaReportData(int yearId, int levelId)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            int schoolID = userData.CompanyID;

            bool success = true;
            string message = "Save Successfully";

            DiplomaReportData diplomaReportData = new DiplomaReportData();
            diplomaReportData.StudentList = new List<StudentListModel>();

            try
            {
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    // School Data & Signature Data
                    string query = string.Format(@"
SELECT c.sCompany 'SchoolName', c.sOwner 'Owner'
, (CASE WHEN c.sTumbon IS NOT NULL AND c.sTumbon <> '' THEN (CASE WHEN c.sProvince LIKE '%กรุงเทพ%' THEN 'เเขวง'+c.sTumbon ELSE 'ตำบล'+c.sTumbon END) ELSE '' END)+' '
+(CASE WHEN c.sAumpher IS NOT NULL AND c.sAumpher <> '' THEN (CASE WHEN c.sProvince LIKE '%กรุงเทพ%' THEN 'เขต'+c.sAumpher ELSE 'อำเภอ'+c.sAumpher END) ELSE '' END)+' '
+(CASE WHEN c.sProvince IS NOT NULL AND c.sProvince <> '' THEN 'จังหวัด'+c.sProvince ELSE '' END) 'Address'
, ISNULL(tl2.titleDescription, e.sTitle)+e.sName+' '+e.sLastname 'FullNameSchoolDirector1', c.SchoolHeadName+' '+c.SchoolHeadLastname 'FullNameSchoolDirector2'
, ISNULL(tl3.titleDescription, e2.sTitle)+e2.sName+' '+e2.sLastname 'FullNameRegistrar'
FROM JabjaiMasterSingleDB.dbo.TCompany c
LEFT JOIN TEmployees e ON c.nCompany = e.SchoolID AND c.nSchoolHeadid = e.sEmp
LEFT JOIN TTitleList tl2 ON c.nCompany = tl2.SchoolID AND e.sTitle = CAST(tl2.nTitleid AS VARCHAR(10))
LEFT JOIN TEmployees e2 ON c.nCompany = e2.SchoolID AND c.nRegistraDirectorid = e2.sEmp
LEFT JOIN TTitleList tl3 ON c.nCompany = tl3.SchoolID AND e2.sTitle = CAST(tl3.nTitleid AS VARCHAR(10))
WHERE c.nCompany={0}", schoolID);
                    SchoolData schoolData = en.Database.SqlQuery<SchoolData>(query).FirstOrDefault();
                    if (schoolData != null)
                    {
                        diplomaReportData.School = schoolData.SchoolName;
                        diplomaReportData.Address = schoolData.Address.Trim();
                        diplomaReportData.Owner = schoolData.Owner;
                        diplomaReportData.Registrar = schoolData.FullNameRegistrar;
                        diplomaReportData.Director = string.IsNullOrEmpty(schoolData.FullNameSchoolDirector1) ? schoolData.FullNameSchoolDirector2 : schoolData.FullNameSchoolDirector1;
                    }

                    // Education Data
                    var levelObj = en.TSubLevels.Where(w => w.nTSubLevel == levelId).FirstOrDefault();
                    if (levelObj != null)
                    {
                        diplomaReportData.Level = "ชั้น" + levelObj.fullName;
                    }

                    var yearObj = en.TYears.Where(w => w.nYear == yearId).FirstOrDefault();
                    if (yearObj != null)
                    {
                        diplomaReportData.Year = yearObj.numberYear?.ToString();
                    }

                    // Student Data
                    query = string.Format(@"
SELECT CAST(ROW_NUMBER() OVER(PARTITION BY sh.DayAdd ORDER BY sh.DayAdd, CAST(IIF(ISNUMERIC(u.DiplomaCode) = 1, u.DiplomaCode, 9999) AS INT), sl.MasterCode, tsl.nTSubLevel2, sch.nStudentNumber) AS INT) 'No', u.DiplomaCode, ISNULL(tl.titleDescription, u.sStudentTitle)+u.sName+' '+u.sLastname 'Fullname', u.dBirth 'Birthday', sh.DayAdd 'GraduateDate', '' 'Note'
, u.cSex 'Sex'--, sl.MasterCode, tsl.nTSubLevel2, sch.nStudentNumber, sh.nTerm
FROM TStudentHIstory sh 
INNER JOIN TUser u ON sh.SchoolID = u.SchoolID AND sh.sID = u.sID --AND sh.nTermSubLevel2_OLD = u.nTermSubLevel2
LEFT JOIN TTermSubLevel2 tsl ON sh.SchoolID = tsl.SchoolID AND sh.nTermSubLevel2_OLD = tsl.nTermSubLevel2
LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel
LEFT JOIN TTerm t ON sh.SchoolID = t.SchoolID AND sh.nTerm = t.nTerm
LEFT JOIN TYear y ON t.SchoolID = y.SchoolID AND t.nYear = y.nYear
LEFT JOIN TStudentClassroomHistory sch ON sh.SchoolID = u.SchoolID AND sh.sID = sch.sID AND t.nTerm = sch.nTerm 
LEFT JOIN TTitleList tl ON u.SchoolID = tl.SchoolID AND u.sStudentTitle = CAST(tl.nTitleid AS VARCHAR(10))
WHERE sh.SchoolID={0} AND (ISNULL(u.nStudentStatus, 0)<>2 OR ISNULL(sch.nStudentStatus, 0)<>2) AND ISNULL(sch.nStudentStatus, 0)=4
AND sh.nTerm=(SELECT TOP 1 nTerm FROM TTerm where nYear={1} AND sTerm IN ('1', '2') AND cDel IS NULL ORDER BY dStart DESC) AND sl.nTSubLevel={2} AND sh.cDel=0
ORDER BY sh.DayAdd, CAST(IIF(ISNUMERIC(u.DiplomaCode) = 1, u.DiplomaCode, 9999) AS INT), sl.MasterCode, tsl.nTSubLevel2, sch.nStudentNumber", schoolID, yearId, levelId);
                    List<StudentListModel> studentListModels = en.Database.SqlQuery<StudentListModel>(query).ToList();
                    if (studentListModels != null && studentListModels.Count > 0)
                    {
                        // Graduate Set
                        diplomaReportData.Sets = new List<SetModel>();

                        int setIndex = 1;
                        List<DateTime> graduateDateSets = studentListModels.GroupBy(g => g.GraduateDate).Select(s => (DateTime)s.Key).ToList();
                        foreach (var d in graduateDateSets)
                        {
                            var set = new SetModel { No = setIndex++, PageAmount = 1 };

                            set.StudentList = studentListModels.Where(w => w.GraduateDate == d).ToList();

                            // Calculate Page Amount
                            // Page 1: 13 Rows, Page 2 to N: 24 Rows
                            if (set.StudentList.Count > 13)
                            {
                                int remainingRows = set.StudentList.Count - 13;

                                set.PageAmount += (remainingRows / 24) + (remainingRows % 24 > 0 ? 1 : 0);
                            }

                            diplomaReportData.PageAmount += set.PageAmount;

                            // Set Graduate Date
                            var studentForGraduateDateObj = set.StudentList.FirstOrDefault();
                            set.GraduateDate = studentForGraduateDateObj.GraduateDate != null ? studentForGraduateDateObj.GraduateDate : DateTime.Now;
                            set.GraduateDay = set.GraduateDate?.ToString("d ").Trim();
                            set.GraduateMonth = set.GraduateDate?.ToString("MMMM", new CultureInfo("th-TH"));
                            set.GraduateYear = set.GraduateDate?.ToString("yyyy", new CultureInfo("th-TH"));

                            set.MaleAmount = set.StudentList.Count(c => c.Sex == "0");
                            set.FemaleAmount = set.StudentList.Count(c => c.Sex == "1");
                            set.TotalAmount = set.MaleAmount + set.FemaleAmount;

                            // Convert number to thai number (Student Data)
                            foreach (var l in set.StudentList)
                            {
                                l.NoThai = l.No.ToString();
                                l.DiplomaCodeThai = l.DiplomaCode ?? "";
                                l.BirthdayThai = l.Birthday?.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                                l.GraduateDateThai = l.GraduateDate?.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                            }

                            diplomaReportData.Sets.Add(set);
                        }

                    }

                    // Convert number to thai number (Report Data)
                    diplomaReportData.LevelThai = diplomaReportData.Level;
                    diplomaReportData.YearThai = diplomaReportData.Year;
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            return JsonConvert.SerializeObject(new { success, message, diplomaReportData });
        }

        private static string ConvertNumberToThaiNumber(string number)
        {
            if (string.IsNullOrEmpty(number)) return "";

            string[] thaiNumberSymbol = { "๐", "๑", "๒", "๓", "๔", "๕", "๖", "๗", "๘", "๙" };
            string thaiNumber = "";

            foreach (var c in number)
            {
                if (!int.TryParse(c.ToString(), out int r))
                {
                    thaiNumber += c.ToString();
                }
                else
                {
                    thaiNumber += thaiNumberSymbol[int.Parse(c.ToString())];
                }
            }

            return thaiNumber;
        }

        public class SchoolData
        {
            public string SchoolName { get; set; }
            public string Owner { get; set; }
            public string Address { get; set; }
            public string FullNameSchoolDirector1 { get; set; }
            public string FullNameSchoolDirector2 { get; set; }
            public string FullNameRegistrar { get; set; }
        }

        public class DiplomaReportData
        {
            [JsonProperty(PropertyName = "level")]
            public string Level { get; set; }

            [JsonProperty(PropertyName = "year")]
            public string Year { get; set; }

            [JsonProperty(PropertyName = "school")]
            public string School { get; set; }

            [JsonProperty(PropertyName = "address")]
            public string Address { get; set; }

            [JsonProperty(PropertyName = "owner")]
            public string Owner { get; set; }

            [JsonProperty(PropertyName = "registrar")]
            public string Registrar { get; set; }

            [JsonProperty(PropertyName = "director")]
            public string Director { get; set; }

            [JsonProperty(PropertyName = "studentList")]
            public List<StudentListModel> StudentList { get; set; }

            [JsonProperty(PropertyName = "sets")]
            public List<SetModel> Sets { get; set; }


            [JsonProperty(PropertyName = "levelThai")]
            public string LevelThai { get; set; }

            [JsonProperty(PropertyName = "yearThai")]
            public string YearThai { get; set; }


            [JsonProperty(PropertyName = "pageAmount")]
            public int PageAmount { get; set; }
        }

        public class StudentListModel
        {
            [JsonProperty(PropertyName = "no")]
            public int No { get; set; }

            [JsonProperty(PropertyName = "diplomaCode")]
            public string DiplomaCode { get; set; }

            [JsonProperty(PropertyName = "fullname")]
            public string Fullname { get; set; }

            [JsonProperty(PropertyName = "birthday")]
            public DateTime? Birthday { get; set; }

            [JsonProperty(PropertyName = "graduateDate")]
            public DateTime? GraduateDate { get; set; }

            [JsonProperty(PropertyName = "note")]
            public string Note { get; set; }

            [JsonProperty(PropertyName = "sex")]
            public string Sex { get; set; }


            [JsonProperty(PropertyName = "noThai")]
            public string NoThai { get; set; }

            [JsonProperty(PropertyName = "diplomaCodeThai")]
            public string DiplomaCodeThai { get; set; }

            [JsonProperty(PropertyName = "birthdayThai")]
            public string BirthdayThai { get; set; }

            [JsonProperty(PropertyName = "graduateDateThai")]
            public string GraduateDateThai { get; set; }
        }

        public class SetModel
        {
            [JsonProperty(PropertyName = "no")]
            public int No { get; set; }

            [JsonProperty(PropertyName = "pageAmount")]
            public int PageAmount { get; set; }

            [JsonProperty(PropertyName = "graduateDate")]
            public DateTime? GraduateDate { get; set; }

            [JsonProperty(PropertyName = "graduateDay")]
            public string GraduateDay { get; set; }

            [JsonProperty(PropertyName = "graduateMonth")]
            public string GraduateMonth { get; set; }

            [JsonProperty(PropertyName = "graduateYear")]
            public string GraduateYear { get; set; }

            [JsonProperty(PropertyName = "studentList")]
            public List<StudentListModel> StudentList { get; set; }

            [JsonProperty(PropertyName = "maleAmount")]
            public int MaleAmount { get; set; }

            [JsonProperty(PropertyName = "femaleAmount")]
            public int FemaleAmount { get; set; }

            [JsonProperty(PropertyName = "totalAmount")]
            public int TotalAmount { get; set; }
        }
    }
}