using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using MasterEntity;
using JabjaiEntity.DB;
using JabjaiMainClass;

namespace FingerprintPayment.Report
{
    public partial class historylogin : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                // Get current year
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTerm = studentLogic.GetTermId(UserData);
                int yearID = 0;
                var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
                if (term != null)
                {
                    yearID = term.nYear.Value;
                }

                var listYear = en.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false).OrderByDescending(x => x.numberYear).ToList();
                foreach (var l in listYear)
                {
                    if (l.nYear == yearID)
                    {
                        this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }
                    else
                    {
                        this.ltrYear.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }

                    if (yearID == 0) yearID = l.nYear;
                }

                if (yearID != 0)
                {
                    var listTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yearID && w.cDel == null).OrderByDescending(o => o.nTerm).ToList();
                    foreach (var l in listTerm)
                    {
                        if (l.nTerm.Trim() == currentTerm)
                        {
                            this.ltrTerm.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                        }
                        else
                        {
                            this.ltrTerm.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                        }
                    }
                }

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object LoadHistoryLogin(Search search)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                string _condition = "";
                if (search.searchType == "1")
                {
                    _condition += " AND B1.Fd_LoginDate IS NOT NULL";
                }
                else if (search.searchType == "2")
                {
                    _condition += " AND B1.Fd_LoginDate IS NULL";
                }

                if (!string.IsNullOrEmpty(search.StudentName))
                {
                    _condition += " AND A.sID = " + search.StudentName;
                }

                string SQL = string.Format(@"

DECLARE @Fd_SchoolID INT = {2}; 
DECLARE @nTermSubLevel2 INT = {1}; 
DECLARE @nTerm NVARCHAR(20) = '{0}'; 

SELECT  A.sName,A.sLastname,A.sStudentID,A.nStudentNumber,'' AS Fd_LoginHistoryID,
ISNULL(B1.Fd_Version,'') As Fd_Version,ISNULL(ISNULL(B1.System,B2.System),'') AS System,
ISNULL(FORMAT(B1.Fd_LoginDate,'dd/MM/yyyy HH:mm:ss น.'),ISNULL(FORMAT(B2.Fd_LoginDate,'dd/MM/yyyy HH:mm:ss น.'),'')) AS Fd_LoginDate,
B1.Fd_LoginDate AS _LoginDate,ISNULL(B1.Imei,B2.Imei) AS Imei,LEN(ISNULL(A.nStudentNumber,'')) AS LEN_Number,
ISNULL(B1.Fd_UserID,B2.Fd_UserID) AS Fd_UserID,ISNULL(B1.Fd_MBBrand,B2.Fd_MBBrand) AS Fd_MBBrand
FROM JabjaiSchoolSingleDB.dbo.TB_StudentViews AS A 
LEFT OUTER JOIN 
(	
	SELECT Fd_UserID,Imei,Fd_Token,MAX(Fd_LoginDate) AS Fd_LoginDate,Fd_MBBrand,Fd_Version,System,Fd_SchoolID
	FROM TB_LoginHistory 
	GROUP BY Fd_UserID,Imei,Fd_Token,Fd_MBBrand,Fd_Version,System,Fd_SchoolID
) AS B1
ON A.sID = B1.Fd_UserID AND A.SchoolID = B1.Fd_SchoolID
LEFT OUTER JOIN 
(	
	SELECT UserID Fd_UserID,Imei,Token AS Fd_Token,
	MAX(Fd_LoginDate) AS Fd_LoginDate,Fd_MBBrand,System,SchoolID AS Fd_SchoolID
	FROM TB_FCM 
	GROUP BY UserID,Imei,Token,Fd_MBBrand,System,SchoolID
) AS B2
ON A.sID = B2.Fd_UserID AND A.SchoolID = B2.Fd_SchoolID

WHERE A.nTerm = @nTerm AND A.nTermSubLevel2 = @nTermSubLevel2 AND ISNULL(A.cDel,'0') != '1' {3}
ORDER BY A.nStudentNumber,LEN(A.sStudentID),A.sStudentID

", search.TermID, search.nTermSubLevel2, userData.CompanyID, _condition
);

                var q_SqlQuery = dbmaster.Database.SqlQuery<SqlQuery>(SQL).AsQueryable().ToList();
                var q = (from a in q_SqlQuery
                         group a by new
                         {
                             a.sName,
                             a.sLastname,
                             a.sStudentID,
                             a.nStudentNumber,
                             a.LEN_StudentID
                         } into gb
                         select new TM_Reports
                         {
                             nStudentNumber = gb.Key.nStudentNumber,
                             sStudentID = gb.Key.sStudentID,
                             sLastname = gb.Key.sLastname,
                             sName = gb.Key.sName,
                             LEN_StudentID = gb.Key.LEN_StudentID,
                             loginData = (from b in gb
                                          orderby b._LoginDate descending
                                          select new TM_Reports.LoginData
                                          {
                                              Fd_LoginDate = b.Fd_LoginDate,
                                              Fd_LoginHistoryID = b.Fd_LoginHistoryID,
                                              Fd_Version = b.Fd_Version,
                                              Imei = b.Imei,
                                              System = b.System,
                                              Fd_MBBrand = b.Fd_MBBrand
                                          }).ToList()
                         }).ToList();

                return (from a in q
                        orderby a.nStudentNumber, a.LEN_StudentID, a.sStudentID
                        select a).ToList();
            }
        }


        [WebMethod]
        public static List<TM_Search> GetStudentName(string keyword, string termID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string sqlQuery = string.Format(@"
SELECT sName+' '+sLastname AS StudentName,sStudentID AS StudentCode,sID AS UserID
FROM TB_StudentViews
WHERE (cDel IS NULL OR cDel = 'G') AND SchoolID = {0} AND nTermSubLevel2  = {1} AND nTerm='{2}'
--GROUP BY sName, sLastname,
ORDER BY sName, sLastname", schoolID, keyword, termID);
                List<TM_Search> result = dbschool.Database.SqlQuery<TM_Search>(sqlQuery).ToList();

                return result;
            }
        }


        public class TM_Search
        {
            public string StudentName { get; set; }
            public string StudentCode { get; set; }
            public int UserID { get; set; }
        }

        public class Search
        {
            public string TermID { get; set; }
            public int nTermSubLevel2 { get; set; }
            public string StudentName { get; set; }
            public string searchType { get; set; }
        }

        public class TM_Reports
        {
            public string sName { get; set; }
            public string sLastname { get; set; }
            public string sStudentID { get; set; }
            public int? nStudentNumber { get; set; }
            public List<LoginData> loginData { get; set; }
            public int LEN_StudentID { get; set; }
            public class LoginData
            {
                public string Fd_LoginHistoryID { get; set; }
                public string Fd_Version { get; set; }
                public string System { get; set; }
                public string Fd_LoginDate { get; set; }
                public string Imei { get; set; }
                public string Fd_MBBrand { get; set; } = "";
            }
        }

        public class SqlQuery
        {
            public string sName { get; set; }
            public string sLastname { get; set; }
            public string sStudentID { get; set; }
            public int? nStudentNumber { get; set; }
            public string Fd_LoginHistoryID { get; set; }
            public string Fd_Version { get; set; }
            public string Fd_MBBrand { get; set; }
            public string System { get; set; }
            public string Fd_LoginDate { get; set; }
            public DateTime? _LoginDate { get; set; }
            public string Imei { get; set; }
            public int LEN_StudentID { get; set; }
        }
    }
}