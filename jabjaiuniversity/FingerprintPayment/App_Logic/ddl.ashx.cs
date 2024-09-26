using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for ddl
    /// </summary>
    public class ddl : IHttpHandler , IRequiresSessionState
    {

        //JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read);

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) {
                string _mode = fcommon.ReplaceInjection(context.Request["Mode"]);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, object> row;

                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

                switch (_mode)
                {

                    //rows = GetListstudent(fcommon.ReplaceInjection(context.Request["nelevel"])
                    //        , fcommon.ReplaceInjection(context.Request["nsublevel"])
                    //        , fcommon.ReplaceInjection(context.Request["nameSearch"]));

                    #region listYear
                    case "listYear":
                        List<listYear> lYear = new List<listYear>();
                        lYear = GetListYear();
                        foreach (var year in lYear)
                        {
                            row = new Dictionary<string, object>();
                            row.Add("nYear", year.nYear);
                            row.Add("numberYear", year.numberYear);
                            rows.Add(row);
                        }

                        break;
                    #endregion
                    #region listSubLV
                    case "listSubLV":
                        List<listSubLV> lSubLV = new List<listSubLV>();
                        lSubLV = GetListSubLV();
                        foreach (var subLV in lSubLV)
                        {
                            row = new Dictionary<string, object>();
                            row.Add("nSubLV", subLV.nSubLV);
                            row.Add("shortNameSubLV", subLV.shortNameSubLV);
                            rows.Add(row);
                        }

                        break;
                    #endregion
                    #region listClassroom
                    case "listClassroom":
                        List<listClassroom> lClassroom = new List<listClassroom>();
                        lClassroom = GetListClassroom(fcommon.ReplaceInjection(context.Request["SubLVID"]));
                        foreach (var room in lClassroom)
                        {
                            row = new Dictionary<string, object>();
                            row.Add("nTermSubLV", room.nTermSubLV);
                            row.Add("nameClass", room.nameClass);
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

        private class listYear
        {

            public int nYear { get; set; }
            public int? numberYear { get; set; }
        }
        private List<listYear> GetListYear()
        {

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) {

                var query = from year in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                            orderby year.numberYear descending
                            select new listYear
                            {
                                nYear = year.nYear,
                                numberYear = year.numberYear,
                            };

                query = query.OrderByDescending(x => x.numberYear);

                var listQuery = query.ToList();

                return listQuery;
            }
        }

        private class listSubLV
        {

            public int nSubLV { get; set; }
            public string shortNameSubLV { get; set; }
        }

        private List<listSubLV> GetListSubLV()
        {

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) {

                var query = from subLV in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID)
                            where subLV.nWorkingStatus == 1 && subLV.isGraduate == true
                            select new listSubLV
                            {
                                nSubLV = subLV.nTSubLevel,
                                shortNameSubLV = subLV.SubLevel
                            };

                var listQuery = query.ToList();

                return listQuery;
            }
        }

        private class listClassroom
        {

            public int nTermSubLV { get; set; }
            public string nameClass { get; set; }
        }

        private List<listClassroom> GetListClassroom(string SubLVID)
        {
            var stringSubLVID = Int16.Parse(SubLVID);
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                var query = from subLV in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID)
                            join termSubLV in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on subLV.nTSubLevel equals termSubLV.nTSubLevel
                            where termSubLV.nTSubLevel == stringSubLVID && termSubLV.nWorkingStatus == 1
                            select new listClassroom
                            {
                                nTermSubLV = termSubLV.nTermSubLevel2,
                                nameClass = subLV.SubLevel + "/" + termSubLV.nTSubLevel2
                            };

                var listQuery = query.ToList();

                return listQuery;
            }
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