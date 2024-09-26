using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiMainClass;
using JabjaiEntity.DB;
using System.Web.Script.Services;
using System.Web.Services;
using FluentDateTime;
using System.Globalization;
using System.Data.Entity;


namespace FingerprintPayment.Report
{
    public partial class reportCancelTopupMoney : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                var userData = GetUserData();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                    hdfschoolname.Value = tCompany.sCompany;
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_data(Search search)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            var userData = GetUserData();
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
            List<day_data> day_data = new List<day_data>();

            string header_text = "";
            if (search.sort_type == 2)
            {
                header_text = "ยอดขาย ปี " + search.dStart.Value.ToString("yyyy ", new CultureInfo("th-th"));
                search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("01/01/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));
                search.dEnd = search.dStart.Value.NextYear().AddDays(-1.0);

                for (int i = 0; search.dStart.Value.AddMonths(i) <= search.dEnd.Value; i++)
                {
                    day_data.Add(new day_data
                    {
                        lable = search.dStart.Value.AddMonths(i).ToString(),
                        values = search.dStart.Value.AddMonths(i)
                    });
                }

                var q1 = (from a1 in db.TMoneys.Where(w => w.SchoolID == userData.CompanyID)

                          where a1.dayCancal >= search.dStart && a1.dayCancal <= search.dEnd.Value
                          && (!search.emp_id.HasValue || search.emp_id == a1.sEmp)
                          group a1 by new
                          {
                              a1.dayCancal.Value.Month
                          }
                          into gb
                          select new
                          {
                              dayCancel = gb.Key.Month,
                              nMoney = gb.Sum(s => s.nMoney)
                          }).ToList();

                var q = (from a in day_data

                         join b in q1 on a.values.Month equals b.dayCancel into jab
                         from jb in jab.DefaultIfEmpty()

                         group jb by new
                         {
                             values = a.values.ToString("dd/MM/yyyy", new CultureInfo("en-us")),
                             lable = a.values.ToString("MMM-yy", new CultureInfo("th-th"))
                         }
                         into gb
                         select new views01
                         {
                             lable = gb.Key.lable,
                             values = gb.Key.values,
                             CnMoney = gb.Sum(s => s == null ? 0 : s.nMoney)
                         }).ToList();

                return new header_reports
                {
                    header_text = header_text,
                    data = q,
                    report_type = search.sort_type
                };
            }
            else
            {
                if (search.sort_type == 2)
                {
                    search.dEnd = search.dStart.Value.Next(DayOfWeek.Sunday);
                    search.dStart = search.dEnd.Value.Previous(DayOfWeek.Monday);

                    header_text = "ยกเลิกการเติมเงิน วันที่ " + search.dStart.Value.ToString("dd MMM ", new CultureInfo("th-th")) + " - " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));

                }
                else if (search.sort_type == 1)
                {
                    search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("01/MM/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));
                    search.dEnd = search.dStart.Value.NextMonth().AddDays(-1.0);
                    header_text = "ยกเลิกการเติมเงิน เดือน " + search.dStart.Value.ToString("MMMM yyyy ", new CultureInfo("th-th"));
                }
                else
                {
                    if (search.dStart == null && search.dEnd == null)
                    {
                        search.dStart = DateTime.Today;
                        search.dEnd = search.dStart.Value.NextDay();
                    }

                    header_text = "ยกเลิกการเติมเงิน วันที่ " + search.dStart.Value.ToString("dd mmm ", new CultureInfo("th-th")) + " - " + search.dEnd.Value.ToString("dd mmm yyyy", new CultureInfo("th-th"));
                }

                for (double i = 0; search.dStart.Value.AddDays(i) <= search.dEnd.Value; i++)
                {
                    day_data.Add(new day_data
                    {
                        lable = search.dStart.Value.AddDays(i).ToString(),
                        values = search.dStart.Value.AddDays(i)
                    });
                }

                search.dEnd = search.dEnd.Value.AddDays(1.0);
                int SchoolID = userData.CompanyID;

                string SQL = $@" 
SELECT  [MoneyID]
,[dMoney]
,[sID]
,[nMoney]
,[cType]
,[sEmp]
,[nBalance]
,[topup_type]
,CONVERT(date,[dayCancal]) AS dayCancal
,[UserCancel]
,[TopupId]
,[SchoolID]
,[CreatedBy]
,[UpdatedBy]
,[CreatedDate]
,[UpdatedDate]
,[cDel]
,[CardHistoryID]
,[ReferenceID]
,[UpdatedTime]
,[Insurance]
,[ChargeID]
,[DeviceID]
,[PaymentInfo]
,SourceIP
,CheckID
FROM  TMoney 
WHERE dayCancal BETWEEN '{search.dStart.Value.ToString("yyyyMMdd")}' 
AND '{search.dEnd.Value.ToString("yyyyMMdd")}' AND ( 0 = {search.emp_id ?? 0} 
OR sEmp =  {search.emp_id ?? 0}) AND  SchoolID = {userData.CompanyID} ";
                var money = db.Database.SqlQuery<TMoney>(SQL).ToList();

                //var money = (from a1 in db.TMoneys.Where(w => w.SchoolID == SchoolID)
                //             where a1.dayCancal >= search.dStart && a1.dayCancal <= search.dEnd.Value
                //             && (!search.emp_id.HasValue || search.emp_id == a1.sEmp)
                //             select a1).ToList();

                var q1 = (from a1 in money
                          group a1 by new
                          {
                              a1.dayCancal.Value
                          }
                          into gb
                          select new
                          {
                              dayCancel = gb.Key.Value,
                              nMoney = gb.Sum(s => s.nMoney)
                          }).ToList();

                var q = (from a in day_data

                         join b in q1 on a.values equals b.dayCancel into jab
                         from jb in jab.DefaultIfEmpty()

                         group jb by new
                         {
                             values = a.values.ToString("dd/MM/yyyy", new CultureInfo("en-us")),
                             lable = a.values.ToString(search.sort_type == 0 ? "dddd" : "dd MMM yy", new CultureInfo("th-th"))
                         }
                         into gb
                         select new views01
                         {
                             lable = gb.Key.lable,
                             values = gb.Key.values,
                             CnMoney = gb.Sum(s => s == null ? 0 : s.nMoney)
                         }).ToList();

                return new header_reports
                {
                    header_text = header_text,
                    data = q,
                    report_type = search.sort_type
                };
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_detail(Search search)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            var userData = GetUserData();
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
            List<day_data> day_data = new List<day_data>();

            search.dEnd = search.dStart.Value.AddDays(1.0);

            string SQL = $@" 
SELECT * FROM 
TMoney 
WHERE dayCancal BETWEEN '{search.dStart.Value.ToString("yyyyMMdd")}'  
AND '{search.dEnd.Value.ToString("yyyyMMdd")}' AND ( 0 = {search.emp_id ?? 0} 
OR sEmp =  {search.emp_id ?? 0}) AND  SchoolID = {userData.CompanyID} ";
            var q = db.Database.SqlQuery<TMoney>(SQL).ToList();

            //var q = (from a1 in db.TMoneys.Where(w => w.SchoolID == userData.CompanyID)

            //         where a1.dayCancal >= search.dStart && a1.dayCancal <= search.dEnd.Value
            //         && (!search.emp_id.HasValue || search.emp_id == a1.sEmp)
            //         select a1).ToList();

            var q_user = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID)
                          join b in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
                          join c in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
                          select new
                          {
                              b.nTermSubLevel2,
                              class_name = c.SubLevel + " / " + b.nTSubLevel2,
                              a.sName,
                              a.sLastname,
                              a.sID,
                              a.sStudentID
                          }).ToList();

            var q1 = (from a1 in q

                      join b1 in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) 
                      on a1.UserCancel equals b1.sEmp into ja1b1
                      from jb1 in ja1b1.DefaultIfEmpty()

                      join c1 in q_user on new { user_id = a1.sID, user_type = a1.cType }
                      equals new { user_id = c1.sID, user_type = "0" } into ja1c1
                      from jc1 in ja1c1.DefaultIfEmpty()

                      join c2 in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on new { user_id = a1.sID, user_type = a1.cType }
                      equals new { user_id = c2.sEmp, user_type = "1" } into ja1c2
                      from jc2 in ja1c2.DefaultIfEmpty()

                      join c3 in (from a2 in db.TBackupCards
                                  join b2 in db.TBackupCardHistories on new { CardID = a2.CardID, a2.SchoolID } equals new { CardID = b2.CardID.Value, b2.SchoolID }
                                  where a2.SchoolID == userData.CompanyID
                                  select new
                                  {
                                      b2.CardHistoryID,
                                      b2.UserName,
                                      a2.CardName
                                  })

                                  on new { user_id = a1.CardHistoryID ?? new Guid(), user_type = a1.cType } equals new { user_id = c3.CardHistoryID, user_type = "2" } into ja1jc3
                      from jc3 in ja1jc3.DefaultIfEmpty()

                      select new views02
                      {
                          money = a1.nMoney,
                          time = a1.dayCancal.Value.ToString("HH:mm:ss"),
                          //emp_name = jb1 == null ? "School Bright Admin" : jb1.sName + " " + jb1.sLastname,
                          emp_name = jb1 == null ? "" : jb1.sName + " " + jb1.sLastname,
                          user_name = jc1 != null ? (jc1.sStudentID + "-" + jc1.sName + " " + jc1.sLastname) :
                          (jc2 != null ? jc2.sName + " " + jc2.sLastname :
                          (jc3 != null ? jc3.CardName + " - " + jc3.UserName + " ( บัตรสำรอง )" : "")),

                          class_name = jc1 != null ? jc1.class_name : " ",
                          sStudentID = jc1 != null ? jc1.sStudentID : " ",
                          SellID = a1.MoneyID
                      }).ToList();

            return new header_reports
            {
                data = q1,
                header_text = "รายงานยกเลิกเติมเงิน วันที่ " + search.dStart.Value.ToString("dd MMMM yyyy", new CultureInfo("th-th")),
                report_type = search.sort_type
            };
        }


        public class day_data
        {
            public string header_text { get; set; }
            public string lable { get; set; }
            public DateTime values { get; set; }
        }

        public class Search
        {
            public int sort_type { get; set; }
            public string type { get; set; }
            public DateTime? dStart { get; set; }
            public DateTime? dEnd { get; set; }
            public int? emp_id { get; set; }
        }

        public class views01
        {
            public decimal? CnMoney { get; set; }
            public string lable { get; set; }
            public string values { get; set; }
        }

        public class views02
        {
            public string emp_name { get; set; }
            public string user_name { get; set; }
            public string class_name { get; set; }
            public string time { get; set; }
            public decimal? money { get; set; }
            public string sStudentID { get; set; }
            public string user_nam1 { get; set; }
            public int SellID { get; set; }
        }

        public class header_reports
        {
            public string header_text { get; set; }
            public object data { get; set; }
            public int report_type { get; set; }
        }


    }

}