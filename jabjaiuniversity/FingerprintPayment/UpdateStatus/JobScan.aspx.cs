using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using FingerprintPayment.Report.Functions.Reports_03;
using FingerprintPayment.Report.Models;

namespace FingerprintPayment.UpdateStatus
{
    public partial class JobScan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            //SqlConnection _conn = fcommon.ConfigSqlConnection(userData.Entities);
            if (!this.IsPostBack)
            {
                //fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear where SchoolID = '" + userData.CompanyID + "' order by numberYear desc", "", "nYear", "numberYear");
                //ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                //using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                //{
                //    string sEntities = Session["sEntities"].ToString();
                //    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                //    using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                //    {
                //        var q = QueryDataBases.SubLevel_Query.GetData(entities, userData);
                //        fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                //        hdfschoolname.Value = tCompany.sCompany;
                //    }
                //}
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object returnlist(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    DateTime dStart = DateTime.Today, dEnd = DateTime.Today;

                    //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                    var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                    dStart = f_term.dStart.Value;
                    dEnd = f_term.dEnd.Value;

                    //if (DateTime.Now < dEnd) dEnd = DateTime.Now;

                    return ReportsType_01.getData(search, dbschool, dStart, dEnd, false, userData, false);
                }
            }
        }

        [ScriptMethod()]
        [WebMethod]
        public static string updateData(updateScanData updateScanData)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var user_id = HttpContext.Current.Session["sEmpID"].ToString();
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {
                try
                {
                    var q_term = (from a in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID)
                                  where a.nTerm.Trim() == updateScanData.term_id.Trim() && a.cDel == null
                                  select new
                                  {
                                      a.dStart,
                                      a.dEnd
                                  }).FirstOrDefault();

                    if (q_term == null) return "fail";
                    var q_log = (from a in dbschool.TLogUserTimeScans
                                 join b in dbschool.TUser on a.sID equals b.sID
                                 where a.LogDate >= q_term.dStart && a.LogDate <= q_term.dEnd
                                 && a.LogType.Trim() == "0" && b.nTermSubLevel2 == updateScanData.level2_id
                                 select a).ToList();

                    int i = 0;
                    TimeSpan LogLearnTime = new TimeSpan(0, 0, 0);
                    foreach (var student_data in updateScanData.updateUsers)
                    {
                        var add_log = new List<TLogUserTimeScan>();
                        int? nTeacherId = null;
                        foreach (var LogUpdate in student_data.updateScanLogs)
                        {
                            DateTime LogLearnDate = q_term.dStart.Value.AddDays(i);
                            int nPlaneDay = (int)LogLearnDate.DayOfWeek;
                            if (LogLearnDate > DateTime.Today) continue;
                            else if (LogLearnDate == DateTime.Today)
                            {
                                nTeacherId = int.Parse(user_id);
                            }

                            var f_log = q_log.FirstOrDefault(w => w.sID == student_data.student_id && w.LogDate == LogUpdate.date_log);
                            if (f_log != null && f_log.LogScanStatus == LogUpdate.status_log) continue;
                            dbschool.UpdateLogStatus(student_data.student_id, nTeacherId, "0", LogUpdate.status_log, LogUpdate.date_log, f_log != null ? (f_log.LogTime ?? LogLearnTime) : LogLearnTime);
                        }
                        //if (add_log.Count() > 0) dbschool.TLogUserTimeScans.AddRange(add_log);
                    }

                    dbschool.SaveChanges();
                    if (updateScanData.level2_id.HasValue)
                    {
                        var f_class = QueryDataBases.SubLevel2_Query.GetRoom(dbschool, updateScanData.level2_id.Value, userData);
                        database.InsertLog(user_id, "แก้ไขเช็คชื่อย้อนหลังการมาโรงเรียน ห้อง " + f_class.classRoom_name
                              , entities, HttpContext.Current.Request, 1, SystemAction.Edit, SystemType.WebSite);
                    }
                    else
                    {
                        var f_user = updateScanData.updateUsers.FirstOrDefault();
                        var f_student = dbschool.TUser.FirstOrDefault(f => f.sID == f_user.student_id);

                        database.InsertLog(user_id,
                            "แก้ไขเช็คชื่อย้อนหลังการมาโรงเรียน ของ " + f_student.sName + " " + f_student.sLastname
                            , entities, HttpContext.Current.Request, 1, SystemAction.Edit, SystemType.WebSite);
                    }

                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }

                return "Success";
            }
        }


        public class updateScanData
        {
            public string term_id { get; set; }
            public int? level2_id { get; set; }
            public string plane_id { get; set; }
            public List<updateUser> updateUsers { get; set; }
        }

        public class updateUser
        {
            public int student_id { get; set; }
            public List<updateScanLog> updateScanLogs { get; set; }
        }

        public class updateScanLog
        {
            public string status_log { get; set; }
            public DateTime date_log { get; set; }
            public int schedule_id { get; set; }
        }

    }
}