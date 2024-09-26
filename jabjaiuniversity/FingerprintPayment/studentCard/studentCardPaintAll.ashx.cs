using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Web.DynamicData;
using System.Drawing;
using System.IO;
using FingerprintPayment.Helper;


namespace FingerprintPayment.studentCard
{
    /// <summary>
    /// Summary description for studentCardPaintAll
    /// </summary>
    public class studentCardPaintAll : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            dynamic rss = new JObject();
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

           
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                var q_usermaster = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();

                var q_title = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                string idlv = fcommon.ReplaceInjection(context.Request["idlv"]);
                string idlv2 = fcommon.ReplaceInjection(context.Request["idlv2"]);
                int? idlv2n = int.Parse(idlv2);
                string term = fcommon.ReplaceInjection(context.Request["term"]);

                //string wE = fcommon.ReplaceInjection(context.Request["wE"]);
                //string tC = fcommon.ReplaceInjection(context.Request["tC"]);

                List<stdDetail> stdDetailList = new List<stdDetail>();
                stdDetail stdDetail = new stdDetail();

                //var tuser = _db.TUsers.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n && (w.cDel ?? "0") != "1" && (w.nStudentStatus ?? null) == 0).ToList();
                //var UserList = _db.Database.SqlQuery<JabjaiEntity.DB.TUser>($"SELECT * FROM TUser WHERE SchoolID = {userData.CompanyID} AND ISNULL(cDel,'0') = '0' AND ISNULL(nStudentStatus, 0) = 0").ToList();
                //var tuser = UserList.Where(w => w.nTermSubLevel2 == idlv2n).ToList();

                //StudentLogic studentLogic = new StudentLogic(_db);
                //var termID = studentLogic.GetTermId(userData);

                string sqlCondition = "";

                if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND a.nTerm = '{0}'", term.Trim()); }
                if (!string.IsNullOrEmpty(idlv2)) { sqlCondition += string.Format(@" AND a.nTermSubLevel2 = {0}", idlv2); }

                string sqlQueryFilter = string.Format(@"
SELECT *
FROM TB_StudentViews a
WHERE a.cDel IS NULL AND ISNULL(a.nStudentStatus, 0) = 0 AND a.SchoolID = {0} {1}
", userData.CompanyID, sqlCondition);

                List<TB_StudentViews> tB_StudentViews = _db.Database.SqlQuery<TB_StudentViews>(sqlQueryFilter).ToList();


                //var sql = $"SELECT * FROM TUser U JOIN TStudentClassroomHistory SCH ON U.sID = SCH.sID WHERE U.SchoolID = {userData.CompanyID} AND U.cDel IS NULL AND ISNULL(U.nStudentStatus, 0) = 0 AND SCH.nTerm = '{termID}' " + (!string.IsNullOrEmpty(idlv2) ? " AND U.nTermSubLevel2 = " + idlv2 : "");

                //var tuser = _db.Database.SqlQuery<JabjaiEntity.DB.TUser>(sql).ToList();

                foreach (var data1 in tB_StudentViews)
                {
                    var check1 = q_usermaster.Where(w => w.nSystemID == data1.sID).FirstOrDefault();
                    if (check1 != null)
                    {
                        stdDetail = new stdDetail();
                        stdDetail.fullName = Common.geTitelName(q_title, data1.sStudentTitle) + data1.sName + " " + data1.sLastname;
                        stdDetail.firstName = data1.sName;
                        stdDetail.sID = data1.sID;
                        stdDetail.studentnumber = data1.nStudentNumber == null ? 0 : data1.nStudentNumber;
                        stdDetailList.Add(stdDetail);
                    }
                }

                var newSortList4 = stdDetailList.OrderBy(x => x.studentnumber).ToList();

                //var checkGrade = (from std in newSortList4
                //                  join h in _db.TStudentHIstories.Where(w => w.SchoolID == userData.CompanyID)
                //                  on std.sID equals h.sID
                //                  where h.StudentStatus == "G"
                //                  select std).ToList();

                //for (int i = 0; i < checkGrade.Count(); i++)
                //{
                //    var itemToRemove = newSortList4.Single(r => r.sID == checkGrade[i].sID);
                //    newSortList4.Remove(itemToRemove);
                //}


                rss = new JArray(
                    from a in newSortList4
                    select new JObject(
                        new JProperty("fullName", a.fullName),
                        new JProperty("sID", a.sID),
                        new JProperty("studentnumber", a.studentnumber)
                        ));

                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(rss);
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        protected class stdDetail
        {
            public int sID { get; set; }
            public string fullName { get; set; }
            public string firstName { get; set; }
            public int? studentnumber { get; set; }
        }
    }


}