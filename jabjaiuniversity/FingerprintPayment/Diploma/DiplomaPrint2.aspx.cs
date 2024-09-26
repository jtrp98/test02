using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Diploma
{
    public partial class DiplomaPrint2 : System.Web.UI.Page
    {
        protected DiplomaQuery diplomaQuery = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int sID = Convert.ToInt32(string.IsNullOrEmpty(Request.QueryString["sid"]) ? "0" : Request.QueryString["sid"]);
                int classroomID = Convert.ToInt32(string.IsNullOrEmpty(Request.QueryString["cid"]) ? "0" : Request.QueryString["cid"]);
                LoadDiplomaPrint(sID, classroomID);
            }
        }

        private void LoadDiplomaPrint(int sID, int classroomID)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            int schoolID = userData.CompanyID;

            string query = string.Format(@"
SELECT ISNULL(tl.titleDescription, u.sStudentTitle)+u.sName+' '+u.sLastname 'FullNameStudent', u.dBirth 'BirthDate', sh.DayAdd 'GraduationApprovalDate', u.DiplomaCode
, c.sCompany 'SchoolName', c.sOwner 'Owner', c.sProvince 'Province'
, ISNULL(tl2.titleDescription, e.sTitle)+e.sName+' '+e.sLastname 'FullNameSchoolDirector1', c.SchoolHeadName+' '+c.SchoolHeadLastname 'FullNameSchoolDirector2'
, sl.SubLevel
, ISNULL(tl3.titleDescription, e2.sTitle)+e2.sName+' '+e2.sLastname 'FullNameRegistrar'
FROM TUser u 
LEFT JOIN TTitleList tl ON u.SchoolID = tl.SchoolID AND u.sStudentTitle = CAST(tl.nTitleid AS VARCHAR(10))
LEFT JOIN TStudentHIstory sh ON u.SchoolID = sh.SchoolID AND u.sID = sh.sID
LEFT JOIN JabjaiMasterSingleDB.dbo.TCompany c ON u.SchoolID = c.nCompany
LEFT JOIN TEmployees e ON c.nCompany = e.SchoolID AND c.nSchoolHeadid = e.sEmp
LEFT JOIN TTitleList tl2 ON c.nCompany = tl2.SchoolID AND e.sTitle = CAST(tl2.nTitleid AS VARCHAR(10))
LEFT JOIN TTermSubLevel2 tsl ON sh.SchoolID = tsl.SchoolID AND sh.nTermSubLevel2_OLD = tsl.nTermSubLevel2
LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel
LEFT JOIN TEmployees e2 ON c.nCompany = e2.SchoolID AND c.nRegistraDirectorid = e2.sEmp
LEFT JOIN TTitleList tl3 ON c.nCompany = tl3.SchoolID AND e2.sTitle = CAST(tl3.nTitleid AS VARCHAR(10))
WHERE u.sID={0} AND sh.nTermSubLevel2_OLD={1}", sID, classroomID);

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                diplomaQuery = en.Database.SqlQuery<DiplomaQuery>(query).FirstOrDefault();
                if (diplomaQuery != null)
                {
                    if (!string.IsNullOrEmpty(diplomaQuery.DiplomaCode))
                    {
                        diplomaQuery.DiplomaCode = ConvertNumberToThaiNumber(diplomaQuery.DiplomaCode);
                    }
                    if (diplomaQuery.BirthDate != null)
                    {
                        diplomaQuery.BirthDateDay = ConvertNumberToThaiNumber(diplomaQuery.BirthDate?.ToString("d ").Trim());
                        diplomaQuery.BirthDateMonth = diplomaQuery.BirthDate?.ToString("MMMM", new CultureInfo("th-TH"));
                        diplomaQuery.BirthDateYear = ConvertNumberToThaiNumber(diplomaQuery.BirthDate?.ToString("yyyy", new CultureInfo("th-TH")));
                    }
                    else
                    {
                        diplomaQuery.BirthDateDay = "-";
                        diplomaQuery.BirthDateMonth = "-";
                        diplomaQuery.BirthDateYear = "-";
                    }

                    if (diplomaQuery.GraduationApprovalDate != null)
                    {
                        diplomaQuery.GraduationApprovalDateDay = ConvertNumberToThaiNumber(diplomaQuery.GraduationApprovalDate?.ToString("d ").Trim());
                        diplomaQuery.GraduationApprovalDateMonth = diplomaQuery.GraduationApprovalDate?.ToString("MMMM", new CultureInfo("th-TH"));
                        diplomaQuery.GraduationApprovalDateYear = ConvertNumberToThaiNumber(diplomaQuery.GraduationApprovalDate?.ToString("yyyy", new CultureInfo("th-TH")));
                    }
                    else
                    {
                        diplomaQuery.GraduationApprovalDateDay = "-";
                        diplomaQuery.GraduationApprovalDateMonth = "-";
                        diplomaQuery.GraduationApprovalDateYear = "-";
                    }
                }
            }
        }

        private string ConvertNumberToThaiNumber(string number)
        {
            string[] thaiNumberSymbol = { "๐", "๑", "๒", "๓", "๔", "๕", "๖", "๗", "๘", "๙" };
            string thaiNumber = "";

            foreach (var c in number)
            {
                thaiNumber += thaiNumberSymbol[int.Parse(c.ToString())];
            }

            return thaiNumber;
        }

        public class DiplomaQuery
        {
            public string FullNameStudent { get; set; }
            public DateTime? BirthDate { get; set; }
            public string BirthDateDay { get; set; }
            public string BirthDateMonth { get; set; }
            public string BirthDateYear { get; set; }
            public DateTime? GraduationApprovalDate { get; set; }
            public string GraduationApprovalDateDay { get; set; }
            public string GraduationApprovalDateMonth { get; set; }
            public string GraduationApprovalDateYear { get; set; }
            public string DiplomaCode { get; set; }
            public string SchoolName { get; set; }
            public string Owner { get; set; }
            public string Province { get; set; }
            public string FullNameSchoolDirector1 { get; set; }
            public string FullNameSchoolDirector2 { get; set; }
            public string SubLevel { get; set; }
            public string FullNameRegistrar { get; set; }
        }
    }
}