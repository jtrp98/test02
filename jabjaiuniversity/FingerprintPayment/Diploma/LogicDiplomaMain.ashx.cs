using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.Diploma
{
    /// <summary>
    /// Summary description for LogicDiplomaMain
    /// </summary>
    public class LogicDiplomaMain : IHttpHandler, IRequiresSessionState
    {

        

        public void ProcessRequest(HttpContext context)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) {
                string _mode = fcommon.ReplaceInjection(context.Request["Mode"]);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, object> row;

                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

                switch (_mode)
                {
                    #region Search
                    case "Search":
                        List<GetStdHistory> lSearch = new List<GetStdHistory>();
                        lSearch = GetDate(fcommon.ReplaceInjection(context.Request["nYear"])
                            , fcommon.ReplaceInjection(context.Request["nSubLV"])
                            , fcommon.ReplaceInjection(context.Request["nClassroom"])
                            , fcommon.ReplaceInjection(context.Request["sStd"])
                            );

                        var noRow = 0;
                        foreach (var std in lSearch)
                        {
                            row = new Dictionary<string, object>();
                            noRow = noRow + 1;
                            row.Add("noRow", noRow);
                            row.Add("sID", std.sID);
                            row.Add("fullName", std.fullName);
                            row.Add("sStudentID", std.sStudentID);
                            row.Add("ClassroomID", std.nTermSubLevel2);
                            rows.Add(row);
                        }

                        break;
                        #endregion
                }
                context.Response.Expires = -1;
                context.Response.ContentType = "application/json";

                context.Response.Write(serializer.Serialize(rows));

                context.Response.End();
            }
        }

        private class GetStdHistory
        {

            public int sID { get; set; }
            public string fullName { get; set; }
            public int nYear { get; set; }
            public int nTSubLevel { get; set; }
            public int nTermSubLevel2 { get; set; }
            public string sStudentID { get; set; }
            public int? nStudentNumber { get; set; }

        }



        private List<GetStdHistory> GetDate(string nYear, string nSubLV, string nClassroom, string sStd)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                var condition = "";
                if (!string.IsNullOrEmpty(nYear))
                {
                    condition += string.Format(@" AND y.nYear={0}", nYear);
                }
                if (!string.IsNullOrEmpty(nSubLV))
                {
                    condition += string.Format(@" AND sl.nTSubLevel={0}", nSubLV);
                }
                if (!string.IsNullOrEmpty(nClassroom))
                {
                    condition += string.Format(@" AND sh.nTermSubLevel2_OLD={0}", nClassroom);
                }
                if (!string.IsNullOrEmpty(sStd))
                {
                    condition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%')", sStd);
                }

                var query = string.Format(@"
SELECT DISTINCT sh.sID, sh.nTermSubLevel2_OLD 'nTermSubLevel2', u.sName+' '+u.sLastname 'fullName', u.sStudentID, u.nStudentNumber, sl.nTSubLevel, y.nYear
--, CAST(IIF(ISNUMERIC(u.DiplomaCode) = 1, u.DiplomaCode, 9999) AS INT)
FROM TStudentHIstory sh 
INNER JOIN TUser u ON sh.SchoolID = u.SchoolID AND sh.sID = u.sID --AND sh.nTermSubLevel2_OLD = u.nTermSubLevel2
LEFT JOIN TTermSubLevel2 tsl ON sh.SchoolID = tsl.SchoolID AND sh.nTermSubLevel2_OLD = tsl.nTermSubLevel2
LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel
LEFT JOIN TTerm t ON sh.SchoolID = t.SchoolID AND sh.nTerm = t.nTerm
LEFT JOIN TYear y ON t.SchoolID = y.SchoolID AND t.nYear = y.nYear
LEFT JOIN TStudentClassroomHistory sch ON sh.SchoolID = u.SchoolID AND sh.sID = sch.sID AND t.nTerm = sch.nTerm 
WHERE sh.SchoolID={0} AND (ISNULL(u.nStudentStatus, 0) <> 2 OR ISNULL(sch.nStudentStatus, 0) <> 2) AND ISNULL(sch.nStudentStatus, 0) = 4 AND sh.cDel=0 {1}
ORDER BY u.sStudentID", userData.CompanyID, condition);
                // ORDER BY CAST(IIF(ISNUMERIC(u.DiplomaCode) = 1, u.DiplomaCode, 9999) AS INT), nStudentNumber, u.sStudentID

                var listQuery = _db.Database.SqlQuery<GetStdHistory>(query).ToList();

                return listQuery;
            }
        }

        public bool IsReusable { get { return false; } }
    }
}