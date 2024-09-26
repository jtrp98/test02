using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.Transcript
{
    /// <summary>
    /// Summary description for LogicTranscriptMain
    /// </summary>
    public class LogicTranscriptMain : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();

            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
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
                            , _db
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

        }



        private List<GetStdHistory> GetDate(string nYear, string nSubLV, string nClassroom, string sStd, JabJaiEntities _db)
        {

            var query = (from viewStd in _db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID)
                        join tTerm in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on viewStd.nTerm equals tTerm.nTerm
                        join y in _db.TYears.Where(w => w.SchoolID == userData.CompanyID) on tTerm.nYear equals y.nYear
                        select new GetStdHistory
                        {
                            sID = viewStd.sID,
                            fullName = viewStd.sName + " " + viewStd.sLastname,
                            nYear = y.nYear,
                            nTSubLevel = viewStd.nTSubLevel,
                            nTermSubLevel2 = viewStd.nTermSubLevel2,
                            sStudentID = viewStd.sStudentID,
                        }).AsQueryable().ToList();

            if (!String.IsNullOrEmpty(nYear))
            {
                var intYear = Int16.Parse(nYear);
                query = query.Where(s => s.nYear == intYear).ToList();
            }
            if (!String.IsNullOrEmpty(nSubLV))
            {
                var intSubLV = Int16.Parse(nSubLV);
                query = query.Where(s => s.nTSubLevel == intSubLV).ToList();
            }
            if (!String.IsNullOrEmpty(nClassroom))
            {
                var intClassroom = Int16.Parse(nClassroom);
                query = query.Where(s => s.nTermSubLevel2 == intClassroom).ToList();
            }

            if (!String.IsNullOrEmpty(sStd))
            {
                query = query.Where(s => (s.sStudentID.StartsWith(sStd) || s.fullName.Contains(sStd))).ToList();
            }

            query = query.GroupBy(n => n.sID).Select(g => g.FirstOrDefault()).ToList();
            var listQuery = query.ToList();

            return listQuery;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}