using FluentDateTime;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    public partial class Withdrawal : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

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
            var userData = GetUserData();

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
            List<day_data> day_data = new List<day_data>();

            string header_text = "";
            if (search.sort_type == 2)
            {

                header_text = "รายงานการถอนเงินปี " + search.dStart.Value.ToString("yyyy ", new CultureInfo("th-th"));
                search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("01/01/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));
                search.dEnd = search.dStart.Value.NextYear().AddDays(-1.0);

                for (int i = 0; search.dStart.Value.AddMonths(i) <= search.dEnd.Value; i++)
                {
                    day_data.Add(new day_data
                    {
                        lable = search.dStart.Value.AddMonths(i).ToString(),
                        values = search.dStart.Value.AddMonths(i),
                    });
                }

                var q1 = (from a1 in db.TWithdrawals.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                          where a1.dCanCel == null && !string.IsNullOrEmpty(a1.sWithdrawalType)
                           && a1.dWithdrawal >= search.dStart && a1.dWithdrawal <= search.dEnd.Value
                           && (string.IsNullOrEmpty(search.type) || a1.sWithdrawalType.Contains(search.type))
                           && (!search.emp_id.HasValue || search.emp_id == a1.userAdd)
                          group a1 by new { a1.dWithdrawal.Value.Month, a1.sWithdrawalType } into gb
                          select new
                          {
                              dMoney = gb.Key.Month,
                              nMoney = gb.Sum(s => s.nMoney),
                              gb.Key.sWithdrawalType
                          }).ToList();

                var q = (from a in day_data
                         join b in q1 on a.values.Month equals b.dMoney into jab

                         from jb in jab.DefaultIfEmpty()

                         group jb by new { values = a.values.ToString("dd/MM/yyyy", new CultureInfo("en-us")), lable = a.values.ToString("MMM-yy", new CultureInfo("th-th")), Withdrawal_type = jb == null ? "" : jb.sWithdrawalType }
                         into gb
                         select new views01
                         {
                             website = gb.Key.Withdrawal_type == "" ? 0 : gb.Where(w => w.sWithdrawalType.Contains("WB")).Sum(s => s.nMoney ?? 0),
                             mobile = gb.Key.Withdrawal_type == "" ? 0 : gb.Where(w => w.sWithdrawalType.Contains("MB")).Sum(s => s.nMoney ?? 0),
                             lable = gb.Key.lable,
                             values = gb.Key.values,
                             Withdrawal_type = gb.Key.Withdrawal_type
                         }).ToList();

                return new header_reports { header_text = header_text, data = q, report_type = search.sort_type };
            }
            else
            {
                if (search.sort_type == 0)
                {
                    search.dEnd = search.dStart.Value.Next(DayOfWeek.Sunday);
                    search.dStart = search.dEnd.Value.Previous(DayOfWeek.Monday);

                    header_text = "รายงานการถอนเงิน วันที่ " + search.dStart.Value.ToString("dd MMM ", new CultureInfo("th-th")) + " - " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));
                }
                else if (search.sort_type == 1)
                {
                    search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("01/MM/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));
                    search.dEnd = search.dStart.Value.NextMonth().AddDays(-1.0);
                    header_text = "รายงานการถอนเงินเดือน " + search.dStart.Value.ToString("MMMM yyyy ", new CultureInfo("th-th"));
                }
                else
                {
                    header_text = "รายงานการถอนเงิน วันที่ " + search.dStart.Value.ToString("dd MMM ", new CultureInfo("th-th")) + " - " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));
                }

                for (double i = 0; search.dStart.Value.AddDays(i) <= search.dEnd.Value; i++)
                {
                    day_data.Add(new day_data
                    {
                        lable = search.dStart.Value.AddDays(i).ToString(),
                        values = search.dStart.Value.AddDays(i),
                    });
                }

                search.dEnd = search.dEnd.Value.AddDays(1.0);

                var q1 = (from a1 in db.TWithdrawals.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                          where a1.dCanCel == null && !string.IsNullOrEmpty(a1.sWithdrawalType)
                           && a1.dWithdrawal >= search.dStart && a1.dWithdrawal <= search.dEnd.Value
                           && (string.IsNullOrEmpty(search.type) || a1.sWithdrawalType.Contains(search.type))
                           && (!search.emp_id.HasValue || search.emp_id == a1.userAdd)
                          group a1 by new { DbFunctions.TruncateTime(a1.dWithdrawal).Value, a1.sWithdrawalType } into gb
                          select new
                          {
                              dMoney = gb.Key.Value,
                              nMoney = gb.Sum(s => s.nMoney),
                              gb.Key.sWithdrawalType
                          }).ToList();

                var q = (from a in day_data
                         join b in q1 on a.values equals b.dMoney into jab

                         from jb in jab.DefaultIfEmpty()

                         group jb by new { values = a.values.ToString("dd/MM/yyyy", new CultureInfo("en-us")), lable = a.values.ToString(search.sort_type == 0 ? "dddd" : "dd MMM yy", new CultureInfo("th-th")), Withdrawal_type = jb == null ? "" : jb.sWithdrawalType }
                         into gb
                         select new views01
                         {
                             website = gb.Key.Withdrawal_type == "" ? 0 : gb.Where(w => w.sWithdrawalType.Contains("WB")).Sum(s => s.nMoney ?? 0),
                             mobile = gb.Key.Withdrawal_type == "" ? 0 : gb.Where(w => w.sWithdrawalType.Contains("MB")).Sum(s => s.nMoney ?? 0),
                             lable = gb.Key.lable,
                             values = gb.Key.values,
                             Withdrawal_type = gb.Key.Withdrawal_type
                         }).ToList();

                return new header_reports { header_text = header_text, data = q, report_type = search.sort_type };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_detail(Search search)
        {
            var userData = GetUserData();

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
            List<day_data> day_data = new List<day_data>();

            search.dEnd = search.dStart.Value.AddDays(1.0);
            //var q = (from a1 in db.TWithdrawals
            //         where a1.dCanCel == null && !string.IsNullOrEmpty(a1.sWithdrawalType)
            //                 && a1.dWithdrawal >= search.dStart && a1.dWithdrawal <= search.dEnd.Value
            //                 && (string.IsNullOrEmpty(search.type) || a1.sWithdrawalType.StartsWith(search.type))
            //                 && (!search.emp_id.HasValue || search.emp_id == a1.userAdd)
            //         select a1).ToList();

            //var q_user = (from a in db.TUsers.Where(w => w.SchoolID == userData.CompanyID)
            //              join b in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
            //              join c in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
            //              select new
            //              {
            //                  b.nTermSubLevel2,
            //                  class_name = c.SubLevel + " / " + b.nTSubLevel2,
            //                  a.sName,
            //                  a.sLastname,
            //                  a.sID,
            //                  a.sStudentID
            //              }).ToList();

            //var q1 = (from a1 in q

            //          join b1 in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a1.userAdd equals b1.sEmp into ja1b1
            //          from jb1 in ja1b1.DefaultIfEmpty()

            //          join c1 in q_user on new { user_id = a1.UserID.Value, user_type = a1.userType } equals new { user_id = c1.sID, user_type = "0" } into ja1c1
            //          from jc1 in ja1c1.DefaultIfEmpty()

            //          join c2 in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on new { user_id = a1.UserID.Value, user_type = a1.userType } equals new { user_id = c2.sEmp, user_type = "1" } into ja1c2
            //          from jc2 in ja1c2.DefaultIfEmpty()

            //          orderby a1.dWithdrawal
            //          select new views02
            //          {
            //              money = a1.nMoney,
            //              time = a1.dWithdrawal.Value.ToString("HH:mm"),
            //              emp_name = jb1 == null ? "" : jb1.sName + " " + jb1.sLastname,
            //              user_name = jc1 == null && ja1c2 == null ? "" : jc1 != null ? jc1.sStudentID + "-" + jc1.sName + " " + jc1.sLastname : jc2.sName + " " + jc2.sLastname,
            //              class_name = jc1 != null ? jc1.class_name : ""
            //          }).ToList();

            string SQL = string.Format(@"DECLARE @P4 INT = {0};
DECLARE @P0 DATETIME = '{1:d} 00:00';
DECLARE @P1 DATETIME = '{1:d} 23:59:59';
DECLARE @P2 INT = {3};
DECLARE @P3 INT = {4};
DECLARE @P5 VARCHAR(20) = '{5}';

SELECT A.nMoney As money,A.dWithdrawal,
CASE WHEN EMP_0.sEmp IS NULL THEN '' ELSE EMP_0.sName + ' ' + EMP_0.sLastname END emp_name,
CASE WHEN EMP_1.sEmp IS NOT NULL THEN EMP_1.sName + ' ' + EMP_1.sLastname 
WHEN U_0.sID IS NOT NULL THEN U_0.sStudentID + ' ' + U_0.sName + ' ' + U_0.sLastname
WHEN E0.CardHistoryID IS NOT NULL THEN E1.CardName + ' - ' + E0.UserName + ' ( บัตรสำรอง ) '
ELSE '' END AS user_name,
CASE WHEN U_0.sID IS NOT NULL THEN SubLevel + ' / ' + b.nTSubLevel2 ELSE '' END class_name
FROM TWithdrawal AS A
LEFT OUTER JOIN TEmployees AS EMP_0 ON A.userAdd = EMP_0.sEmp AND A.SchoolID = EMP_0.SchoolID
LEFT OUTER JOIN TEmployees AS EMP_1 ON A.UserID = EMP_1.sEmp AND A.SchoolID = EMP_1.SchoolID
LEFT OUTER JOIN TUser AS U_0 ON A.UserID = U_0.sID AND A.SchoolID = U_0.SchoolID
LEFT OUTER JOIN TTermSubLevel2 AS B ON U_0.nTermSubLevel2 = B.nTermSubLevel2 AND A.SchoolID = B.SchoolID
LEFT OUTER JOIN TSubLevel AS D ON B.nTSubLevel = D.nTSubLevel AND A.SchoolID = D.SchoolID
LEFT OUTER JOIN TBackupCardHistory AS E0 ON A.CardHistoryID = E0.CardHistoryID AND A.SchoolID = E0.SchoolID
LEFT OUTER JOIN TBackupCard AS E1 ON E0.CardID = E1.CardID AND A.SchoolID = E1.SchoolID
WHERE dWithdrawal BETWEEN @P0 AND @P1 AND(userAdd = @P2 OR @P2 = 0) AND (U_0.sID = @P3 OR @P3 = 0)
AND A.SchoolID = @P4 AND (A.sWithdrawalType = @P5 OR @P5 = '') ORDER BY A.dWithdrawal ",
userData.CompanyID, search.dStart, search.dEnd, search.emp_id ?? 0, 0, search.type);

            var q1 = db.Database.SqlQuery<views02>(SQL).ToList();

            return new header_reports
            {
                data = q1,
                header_text = "ยอดถอนเงิน วันที่ " + search.dStart.Value.ToString("dd MMMM yyyy", new CultureInfo("th-th")),
                report_type = search.sort_type
            };
        }

        public class day_data
        {
            public string lable { get; set; }
            public DateTime values { get; set; }
        }

        public class header_reports
        {
            public string header_text { get; set; }
            public object data { get; set; }
            public int report_type { get; set; }
        }

        public class views01
        {
            public decimal? website { get; set; }
            public decimal? mobile { get; set; }
            public string lable { get; set; }
            public string Withdrawal_type { get; set; }
            public string values { get; set; }
        }

        public class views02
        {

            public string emp_name { get; set; }
            public string user_name { get; set; }
            public string class_name { get; set; }
            public string time
            {
                get
                {
                    return string.Format("{0:HH:mm:ss}", dWithdrawal);
                }
            }
            public decimal? money { get; set; }

            public DateTime dWithdrawal { get; set; }
        }

        public class Search
        {
            public int sort_type { get; set; }
            public string type { get; set; }
            public DateTime? dStart { get; set; }
            public DateTime? dEnd { get; set; }
            public int? emp_id { get; set; }
        }
    }
}