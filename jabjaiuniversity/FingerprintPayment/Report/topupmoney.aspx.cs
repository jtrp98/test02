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
using Amazon.Runtime;
using Microsoft.IdentityModel.Tokens;

namespace FingerprintPayment.Report
{
    public partial class topupmoney : BehaviorGateway
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
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)), userData);
                    hdfschoolname.Value = tCompany.sCompany;
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_data(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
            List<day_data> day_data = new List<day_data>();

            string header_text = "";
            if (search.sort_type == 2)
            {

                header_text = "ยอดเติมเงินปี " + search.dStart.Value.ToString("yyyy ", new CultureInfo("th-th"));
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


                string topup_type = "";
                string topup_type2 = "";

                if (search.type == "WB")
                {
                    topup_type += " AND topup_type IN ('WBS1','DBS1','WB01')";
                    topup_type2 += " AND sWithdrawalType IN ('WBS01','WB01')";
                }
                else if (search.type == "MB")
                {
                    topup_type += " AND topup_type IN ('MBS1')";
                    topup_type2 += " AND sWithdrawalType IN ('MBS01')";
                }
                else if (search.type == "KIOSK'")
                {
                    topup_type += " AND topup_type IN ('KIOSK'')";
                    topup_type2 += " AND sWithdrawalType IN ('KIOSK'')";
                }
                var oldDate = new DateTime(DateTime.Now.Year, 05, 01);
                string SQL = string.Format(@"
DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} {8}';
DECLARE @DAYEND DATETIME = '{2:d} {9}';
DECLARE @sID INT = {3};
DECLARE @emp_id INT = {5};

SELECT * FROM (
SELECT MoneyID,dMoney,sID,nMoney + ISNULL(Insurance,0) AS nMoney,cType,sEmp,nBalance,CASE WHEN topup_type IN ('WBS1','DBS1','WB01') THEN 'WBS1' WHEN topup_type = 'KIOSK' THEN 'KIOSK' ELSE topup_type END 'topup_type',
dayCancal,UserCancel,TopupId,SchoolID,CardHistoryID,
CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,cDel FROM dbo.{7}
WHERE dMoney BETWEEN @DAYSTART AND @DAYEND AND (@sID = 0 OR sID = @sID) {4} AND topup_type NOT IN ('STS1','BEXP')
AND (@emp_id = 0 OR sEmp = @emp_id) AND SchoolID = @SchoolID AND dayCancal IS NULL AND ISNULL(cDel,0) = 0

UNION  

SELECT MoneyID,dMoney,sID,nMoney,cType,sEmp,nBalance,CASE WHEN topup_type IN ('WBS1','DBS1','WB01') THEN 'WBS1' WHEN topup_type = 'KIOSK' THEN 'KIOSK' ELSE topup_type END 'topup_type',
dayCancal,UserCancel,TopupId,SchoolID,CONVERT(uniqueidentifier ,null) AS CardHistoryID,
CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,cDel FROM JabjaiSchoolHistory.dbo.TMoney
WHERE dMoney BETWEEN @DAYSTART AND @DAYEND AND (@sID = 0 OR sID = @sID) {4} AND topup_type NOT IN ('STS1','BEXP') AND ISNULL(cDel,0) = 0
AND (@emp_id = 0 OR sEmp = @emp_id) AND SchoolID = @SchoolID AND dayCancal IS NULL) TB 

UNION

Select 0 'MoneyID',dWithdrawal AS dMoney, ISNULL(UserID ,0) 'sID',nMoney, userType 'cType', userAdd 'sEmp',nBalance, 'WD' 'topup_type', 
 dCanCel 'dayCancal',userCancel,null 'TopupId',SchoolID, CardHistoryID,
	CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,cDel 
-- select *  
FROM TWithdrawal 
WHERE  1=1
and SchoolID = @SchoolID 
AND dWithdrawal BETWEEN  @DAYSTART AND @DAYEND
AND dCanCel IS NULL AND  ISNULL(cDel,0) = 0
AND (@sID = 0 OR UserID = @sID)
AND (@emp_id = 0 OR userAdd = @emp_id)
{6}
ORDER BY  dMoney
",
                    userData.CompanyID
                    , search.dStart
                    , search.dEnd,
                    0
                    , topup_type
                    , search.emp_id ?? 0
                    , topup_type2
                    , search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney"
                    , string.IsNullOrEmpty(search.time1) ? "00:00" : search.time1
                    , string.IsNullOrEmpty(search.time2) ? "23:59" : search.time2);

                var q_money = db.Database.SqlQuery<TC_Money>(SQL).ToList();

                var q1 = (from a1 in q_money
                          group a1 by new { a1.dMoney.Month/*, a1.topup_type*/ } into gb
                          select new
                          {
                              dMoney = gb.Key.Month,
                              //website = gb.Sum(s => s.nMoney),                          
                              website = gb.Where(w => w.topup_type.Contains("WB")).Sum(s => s.nMoney ?? 0),
                              mobile = gb.Where(w => w.topup_type.Contains("MB")).Sum(s => s.nMoney ?? 0),
                              withdraw = gb.Where(w => w.topup_type.Contains("WD")).Sum(s => s.nMoney ?? 0),
                              KIOSK = gb.Where(w => w.topup_type.Contains("KIOSK")).Sum(s => s.nMoney ?? 0),
                              //gb.Key.topup_type
                          }).ToList();

                var q = (from a in day_data
                         join b in q1 on a.values.Month equals b.dMoney into jab

                         from jb in jab.DefaultIfEmpty()

                         group jb by new
                         {
                             values = a.values.ToString("dd/MM/yyyy", new CultureInfo("en-us")),
                             lable = a.values.ToString("MMM-yy", new CultureInfo("th-th")),
                             //topup_type = jb == null ? "" : jb.topup_type 
                         }
                         into gb
                         select new views01
                         {
                             website = gb.Sum(i => i?.website) ?? 0,
                             mobile = gb.Sum(i => i?.mobile) ?? 0,
                             KIOSK = gb.Sum(i => i?.KIOSK) ?? 0,
                             withdraw = gb.Sum(i => i?.withdraw) ?? 0,
                             //website = gb.Key.topup_type == "" ? 0 : gb.Where(w => w.topup_type.Contains("WB")).Sum(s => s.nMoney ?? 0),
                             //mobile = gb.Key.topup_type == "" ? 0 : gb.Where(w => w.topup_type.Contains("MB")).Sum(s => s.nMoney ?? 0),
                             //withdraw = gb.Key.topup_type == "" ? 0 : gb.Where(w => w.topup_type.Contains("WD")).Sum(s => s.nMoney ?? 0),
                             lable = gb.Key.lable,
                             values = gb.Key.values,
                             //topup_type = gb.Key.topup_type
                         }).ToList();

                return new header_reports { header_text = header_text, data = q, report_type = search.sort_type };
            }
            else
            {
                if (search.sort_type == 0)
                {
                    search.dEnd = search.dStart.Value.Next(DayOfWeek.Sunday);
                    search.dStart = search.dEnd.Value.Previous(DayOfWeek.Monday);

                    header_text = "ยอดเติมเงิน วันที่ " + search.dStart.Value.ToString("dd MMM ", new CultureInfo("th-th")) + " - " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));
                }
                else if (search.sort_type == 1)
                {
                    search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("01/MM/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));
                    search.dEnd = search.dStart.Value.NextMonth().AddDays(-1.0);
                    header_text = "ยอดเติมเงินเดือน " + search.dStart.Value.ToString("MMMM yyyy ", new CultureInfo("th-th"));
                }
                else
                {
                    header_text = "ยอดเติมเงิน วันที่ " + search.dStart.Value.ToString("dd MMM ", new CultureInfo("th-th")) + " - " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));
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



                string topup_type = "";
                string topup_type2 = "";

                if (search.type == "WB")
                {
                    topup_type += " AND topup_type IN ('WBS1','DBS1','WB01')";
                    topup_type2 += " AND sWithdrawalType IN ('WBS01')";
                }
                else if (search.type == "MB")
                {
                    topup_type += " AND topup_type IN ('MBS1')";
                    topup_type2 += " AND sWithdrawalType IN ('MBS01')";
                }
                else if (search.type == "KIOSK")
                {
                    topup_type += " AND topup_type IN ('KIOSK')";
                    topup_type2 += " AND sWithdrawalType IN ('KIOSK')";
                }
                var oldDate = new DateTime(DateTime.Now.Year, 05, 01);
                string SQL = string.Format(@"
DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} {8}';
DECLARE @DAYEND DATETIME = '{2:d} {9}';
DECLARE @sID INT = {3};
DECLARE @emp_id INT = {5};

SELECT * FROM (
SELECT MoneyID,CONVERT(DATETIME,FORMAT(dMoney,'dd/MM/yyyy'),103) AS dMoney,sID,nMoney + ISNULL(Insurance,0) AS nMoney,cType,sEmp,nBalance,
CASE WHEN topup_type IN ('WBS1','DBS1','WB01') THEN 'WBS1' ELSE topup_type END 'topup_type',
dayCancal,UserCancel,TopupId,SchoolID,CardHistoryID,
CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,cDel FROM dbo.{7}
WHERE dMoney BETWEEN @DAYSTART AND @DAYEND AND (@sID = 0 OR sID = @sID) {4} AND topup_type NOT IN ('STS1','BEXP')
AND (@emp_id = 0 OR sEmp = @emp_id) AND SchoolID = @SchoolID AND dayCancal IS NULL AND ISNULL(cDel,0) = 0

UNION 

SELECT MoneyID,CONVERT(DATETIME,FORMAT(dMoney,'dd/MM/yyyy'),103) AS dMoney,sID,nMoney,cType,sEmp,nBalance,
CASE WHEN topup_type IN ('WBS1','DBS1','WB01') THEN 'WBS1' ELSE topup_type END 'topup_type',
dayCancal,UserCancel,TopupId,SchoolID,CONVERT(uniqueidentifier ,null) AS CardHistoryID,
CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,cDel FROM JabjaiSchoolHistory.dbo.TMoney
WHERE dMoney BETWEEN @DAYSTART AND @DAYEND AND (@sID = 0 OR sID = @sID) {4} AND topup_type NOT IN ('STS1','BEXP') AND ISNULL(cDel,0) = 0
AND (@emp_id = 0 OR sEmp = @emp_id) AND SchoolID = @SchoolID AND dayCancal IS NULL) TB  

UNION

Select 0 'MoneyID',CONVERT(DATETIME,FORMAT(dWithdrawal,'dd/MM/yyyy'),103) AS  'dMoney', ISNULL(UserID ,0) 'sID',nMoney, userType 'cType', userAdd 'sEmp',nBalance, 'WD' 'topup_type', 
 dCanCel 'dayCancal',userCancel,null 'TopupId',SchoolID, CardHistoryID,
	CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,cDel 
-- select *  
FROM TWithdrawal 
WHERE  1=1
and SchoolID = @SchoolID 
AND dWithdrawal BETWEEN  @DAYSTART AND @DAYEND
AND dCanCel IS NULL AND  ISNULL(cDel,0) = 0
AND (@sID = 0 OR UserID = @sID)
AND (@emp_id = 0 OR userAdd = @emp_id)
{6}
ORDER BY  dMoney ",
                userData.CompanyID
                , search.dStart
                , search.dEnd
                , 0
                , topup_type
                , search.emp_id ?? 0
                , topup_type2
                , search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney"
                , string.IsNullOrEmpty(search.time1) ? "00:00" : search.time1
                , string.IsNullOrEmpty(search.time2) ? "23:59" : search.time2);

                var q_money = db.Database.SqlQuery<TC_Money>(SQL).ToList();

                var q1 = (from a1 in q_money
                          group a1 by new { a1.dMoney, a1.topup_type } into gb
                          select new
                          {
                              dMoney = gb.Key.dMoney,
                              nMoney = gb.Sum(s => s.nMoney),
                              topup_type = gb.Key.topup_type,
                          }).ToList();

                var q = (from a in day_data
                         join b in q1 on a.values equals b.dMoney into jab

                         from jb in jab.DefaultIfEmpty()

                         group jb by new { values = a.values.ToString("dd/MM/yyyy", new CultureInfo("en-us")), lable = a.values.ToString(search.sort_type == 0 ? "dddd" : "dd MMM yy", new CultureInfo("th-th")), topup_type = jb == null ? "" : jb.topup_type }
                         into gb
                         select new views01
                         {
                             website = gb.Key.topup_type == "" ? 0 : gb.Where(w => w.topup_type.Contains("WB")).Sum(s => s.nMoney ?? 0),
                             mobile = gb.Key.topup_type == "" ? 0 : gb.Where(w => w.topup_type.Contains("MB")).Sum(s => s.nMoney ?? 0),
                             withdraw = gb.Key.topup_type == "" ? 0 : gb.Where(w => w.topup_type.Contains("WD")).Sum(s => s.nMoney ?? 0),
                             KIOSK = gb.Key.topup_type == "" ? 0 : gb.Where(w => w.topup_type.Contains("KIOSK")).Sum(s => s.nMoney ?? 0),
                             lable = gb.Key.lable,
                             values = gb.Key.values,
                             //topup_type = gb.Key.topup_type
                         }).ToList();

                var q2 = (from a in q
                          group a by new { a.lable, a.values } into gb
                          select new views01
                          {
                              website = gb.Sum(s => s.website ?? 0),
                              mobile = gb.Sum(s => s.mobile ?? 0),
                              withdraw = gb.Sum(s => s.withdraw),
                              KIOSK = gb.Sum(s => s.KIOSK ?? 0),
                              lable = gb.Key.lable,
                              values = gb.Key.values,
                          }).ToList();

                return new header_reports { header_text = header_text, data = q2, report_type = search.sort_type };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_detail(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
            List<day_data> day_data = new List<day_data>();

            //search.dEnd = search.dStart.Value.AddDays(1.0);
            string topup_type = "";
            string topup_type2 = "";

            if (search.type == "WB")
            {
                topup_type += " AND topup_type IN ('WBS1','DBS1','WB01')";
                topup_type2 += " AND sWithdrawalType IN ('WBS01')";
            }
            else if (search.type == "MB")
            {
                topup_type += " AND topup_type IN ('MBS1')";
                topup_type2 += " AND sWithdrawalType IN ('MBS01')";
            }
            else if (search.type == "KIOSK")
            {
                topup_type += " AND topup_type IN ('KIOSK')";
                topup_type2 += " AND sWithdrawalType IN ('KIOSK')";
            }

            var oldDate = new DateTime(DateTime.Now.Year, 05, 01);
            string SQL = string.Format(@"
DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} {8}';
DECLARE @DAYEND DATETIME = '{2:d} {9}';
DECLARE @sID INT = {3};
DECLARE @emp_id INT = {5};

SELECT * 
FROM (

	SELECT MoneyID,dMoney,ISNULL(sID,0)'sID',A.nMoney + ISNULL(Insurance,0) AS nMoney,A.cType,A.sEmp,nBalance,CASE WHEN topup_type IN ('WBS1','DBS1','WB01') THEN 'WBS1' ELSE topup_type END 'topup_type',
	dayCancal,UserCancel,TopupId,A.SchoolID,CardHistoryID, A.CreatedBy,A.UpdatedBy,A.CreatedDate,A.UpdatedDate,A.cDel ,
	CASE WHEN dayCancal IS NULL THEN 'N' ELSE 'Y' END 'cancle_status',
	CASE WHEN A.UserCancel = 99999 THEN 'System Adjusted' ELSE ( CASE WHEN B.sEmp IS NULL THEN  '' ELSE B.sName + ' ' + B.sLastname END ) END AS 'cancle_name',
    (CASE WHEN topup_type IN ('MBS1') THEN 
		CASE WHEN tx1.ChargeID is not null THEN 'KBank/' ELSE '' END +
		CASE WHEN tx2.ChargeID is not null THEN 'KBank/' ELSE '' END +
		CASE WHEN tx3.Ref2 is not null THEN 'KTB/' ELSE '' END 
	ELSE '' END)+''+
	(CASE WHEN topup_type IN ('MBS1') THEN COALESCE(tx1.ReferenceNo,tx2.ReferenceNo,A.ChargeID,'-') ELSE A.deviceid END)  'refno1',
	(CASE WHEN topup_type IN ('MBS1') THEN 
(CASE WHEN DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 10 AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(tx1.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(tx1.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END) 
		+ ' ' +
		(CASE WHEN DATEDIFF(MINUTE, tx2.CreateDate, tx2.UpdatedDate) > 10 AND DATEDIFF(MINUTE, tx2.CreateDate, tx2.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(tx2.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx2.CreateDate, tx2.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(tx2.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx2.CreateDate, tx2.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END) 
		+ ' '+
		 (CASE WHEN CAST(tx3.UpdatedDate AS TIME) BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx3.CreateDate, tx3.UpdatedDate) > 5 THEN 'BOT8AM' ELSE '' END)  
		+' '+ (CASE WHEN CAST(tx3.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx3.CreateDate, tx3.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) <= 2 AND DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) >= -2) THEN 'BANKDELAY' ELSE '' END)  
		+' '+ (CASE WHEN CAST(tx3.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx3.CreateDate, tx3.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) > 2 OR DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) < -2) THEN 'BOTMANUAL' ELSE '' END) 
	ELSE '' END) 'refno2'	,COALESCE(tx1.CreateDate,tx2.CreateDate,tx3.CreateDate) 'txCreated'

	FROM dbo.{7} A
	LEFT JOIN TEmployees B ON A.UserCancel = B.sEmp AND A.SchoolID = B.SchoolID
    LEFT JOIN JabjaiSchoolSingleDB.dbo.KTransaction tx1 ON A.ChargeID = tx1.ChargeID AND A.SchoolID = tx1.SchoolID
    LEFT JOIN JabjaiSchoolHistory.dbo.KTransaction tx2 ON A.ChargeID = tx2.ChargeID AND A.SchoolID = tx2.SchoolID
    LEFT JOIN JabjaiSchoolSingleDB.dbo.KTBTransaction tx3 ON A.ChargeID = tx3.Ref2 AND A.SchoolID = tx3.SchoolID
    LEFT JOIN JabjaiSchoolSingleDB.dbo.KTBPaymentTransaction ptx3 ON tx3.SchoolID = ptx3.SchoolID AND tx3.Ref2 = ptx3.Ref2 AND ptx3.RespCode = 0
		
	WHERE dMoney BETWEEN @DAYSTART AND @DAYEND AND (@sID = 0 OR sID = @sID)  AND topup_type NOT IN ('STS1','BEXP')
	AND (@emp_id = 0 OR A.sEmp = @emp_id) AND A.SchoolID = @SchoolID  
    AND ISNULL(A.cDel,0) = 0 
    {4}
	--and dayCancal is not null

	UNION 
	
	SELECT MoneyID,dMoney,ISNULL(sID,0)'sID',A.nMoney,A.cType,A.sEmp,nBalance,CASE WHEN topup_type IN ('WBS1','DBS1','WB01') THEN 'WBS1' ELSE topup_type END 'topup_type',
	dayCancal,UserCancel,TopupId,A.SchoolID,CONVERT(uniqueidentifier ,null) AS CardHistoryID,
	A.CreatedBy,A.UpdatedBy,A.CreatedDate,A.UpdatedDate,A.cDel,
	CASE WHEN dayCancal IS NULL THEN 'N' ELSE 'Y' END 'cancle_status',
	CASE WHEN A.UserCancel = 99999 THEN 'System Adjusted' ELSE ( CASE WHEN B.sEmp IS NULL THEN  '' ELSE B.sName + ' ' + B.sLastname END ) END AS 'cancle_name',
     (CASE WHEN topup_type IN ('MBS1') THEN 
		CASE WHEN tx1.ChargeID is not null THEN 'KBank/' ELSE '' END +
		CASE WHEN tx2.ChargeID is not null THEN 'KBank/' ELSE '' END +
		CASE WHEN tx3.Ref2 is not null THEN 'KTB/' ELSE '' END 
	ELSE '' END)+''+
	(CASE WHEN topup_type IN ('MBS1') THEN COALESCE(tx1.ReferenceNo,tx2.ReferenceNo,A.ChargeID,'-') ELSE A.deviceid END)  'refno1',
	(CASE WHEN topup_type IN ('MBS1') THEN 
		(CASE WHEN DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 10 AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(tx1.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(tx1.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END) 
		+ ' ' +
		(CASE WHEN DATEDIFF(MINUTE, tx2.CreateDate, tx2.UpdatedDate) > 10 AND DATEDIFF(MINUTE, tx2.CreateDate, tx2.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(tx2.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx2.CreateDate, tx2.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(tx2.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx2.CreateDate, tx2.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END) 
		+ ' '+
		 (CASE WHEN CAST(tx3.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx3.CreateDate, tx3.UpdatedDate) > 5 THEN 'BOT3AM' ELSE '' END)  
		+' '+ (CASE WHEN CAST(tx3.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx3.CreateDate, tx3.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) <= 2 AND DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) >= -2) THEN 'BANKDELAY' ELSE '' END)  
		+' '+ (CASE WHEN CAST(tx3.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx3.CreateDate, tx3.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) > 2 OR DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) < -2) THEN 'BOTMANUAL' ELSE '' END) 
	ELSE '' END) 'refno2'	,COALESCE(tx1.CreateDate,tx2.CreateDate,tx3.CreateDate) 'txCreated'

	FROM JabjaiSchoolHistory.dbo.TMoney A
	LEFT JOIN TEmployees B ON A.UserCancel = B.sEmp AND A.SchoolID = B.SchoolID
LEFT JOIN JabjaiSchoolSingleDB.dbo.KTransaction tx1 ON A.ChargeID = tx1.ChargeID AND A.SchoolID = tx1.SchoolID
    LEFT JOIN JabjaiSchoolHistory.dbo.KTransaction tx2 ON A.ChargeID = tx2.ChargeID AND A.SchoolID = tx2.SchoolID
    LEFT JOIN JabjaiSchoolSingleDB.dbo.KTBTransaction tx3 ON A.ChargeID = tx3.Ref2 AND A.SchoolID = tx3.SchoolID
    LEFT JOIN JabjaiSchoolSingleDB.dbo.KTBPaymentTransaction ptx3 ON tx3.SchoolID = ptx3.SchoolID AND tx3.Ref2 = ptx3.Ref2 AND ptx3.RespCode = 0

	WHERE dMoney BETWEEN @DAYSTART AND @DAYEND AND (@sID = 0 OR sID = @sID)  AND topup_type NOT IN ('STS1','BEXP') AND ISNULL(A.cDel,0) = 0
	AND (@emp_id = 0 OR A.sEmp = @emp_id) AND A.SchoolID = @SchoolID {4}
	--and dayCancal is not null

) TB  

UNION

Select 0 'MoneyID', dWithdrawal 'dMoney', ISNULL(UserID ,0) 'sID',nMoney, userType 'cType', userAdd 'sEmp',nBalance, sWithdrawalType 'topup_type', 
 dCanCel 'dayCancal',userCancel,null 'TopupId',SchoolID, CardHistoryID,
	CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,cDel ,	'N' 'cancle_status' , '' 'cancle_name' , '' 'refno1' , '' 'refno2' ,null 'txCreated'

FROM TWithdrawal 
WHERE  1=1
and SchoolID = @SchoolID 
AND dWithdrawal BETWEEN  @DAYSTART AND @DAYEND
AND dCanCel IS NULL AND  ISNULL(cDel,0) = 0
AND (@sID = 0 OR UserID = @sID)
AND (@emp_id = 0 OR userAdd = @emp_id)
{6} 
ORDER BY  dMoney 
",
userData.CompanyID,
search.dStart,
search.dStart,
0,
topup_type,
search.emp_id ?? 0,
topup_type2,
search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney",
string.IsNullOrEmpty(search.time1) ? "00:00" : search.time1,
 string.IsNullOrEmpty(search.time2) ? "23:59" : search.time2
);

            var q = db.Database.SqlQuery<TC_Money>(SQL).ToList();
            var studentLogic = new StudentLogic(db);
            var TremData = studentLogic.GetTermDATA(search.dStart ?? DateTime.Today, userData);

            var _studentId = q.Where(s => s.cType == "0").Select(s => s.sID).ToList();

            var q_user = (from b in db.TUser.Where(w => w.SchoolID == userData.CompanyID && _studentId.Contains(w.sID))
                          from a in db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == TremData.nTerm && w.sID == b.sID).DefaultIfEmpty()
                              //where a.nTerm == TremData.nTerm && _studentId.Contains(a.sID) 
                          select new
                          {
                              nTermSubLevel2 = a != null ? a.nTermSubLevel2 : 0,
                              class_name = a != null ? a.SubLevel + " / " + a.nTSubLevel2 : "",
                              b.sName,
                              b.sLastname,
                              b.sID,
                              b.sStudentID,
                              dStart = a != null ? a.dStart : null,
                          }).AsQueryable().ToList();

            if (_studentId.Count() != q_user.Count())
            {
                _studentId.RemoveAll(r => q_user.Select(s => s.sID).ToList().Contains(r));
                var _temp = (from a in db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID)
                             where _studentId.Contains(a.sID)
                             select new
                             {
                                 a.nTermSubLevel2,
                                 class_name = a.SubLevel + " / " + a.nTSubLevel2,
                                 a.sName,
                                 a.sLastname,
                                 a.sID,
                                 a.sStudentID,
                                 a.dStart
                             }).AsQueryable().ToList();

                foreach (var _id in (from a in _studentId
                                     group a by a into gb
                                     select gb.Key))
                {
                    var _data = _temp.OrderByDescending(o => o.dStart).FirstOrDefault(f => f.sID == _id);
                    if (_data != null)
                    {
                        q_user.Add(_data);
                    }
                }
            }

            var q_CardBackup = (from a2 in db.TBackupCards
                                join b2 in db.TBackupCardHistories on new { CardID = a2.CardID, a2.SchoolID } equals new { CardID = b2.CardID ?? new Guid(), b2.SchoolID }
                                where a2.SchoolID == userData.CompanyID
                                select new
                                {
                                    b2.CardHistoryID,
                                    b2.UserName,
                                    a2.CardName
                                }).ToList();

            var tEmpList = db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList();

            //var x = (from a in q
            //             //join b in tEmpList.Where(o => o.sEmp == a.sEmp).DefaultIfEmpty()
            //         join b in tEmpList
            //          on a.sEmp equals b.sEmp into ja1b1
            //         from jb1 in ja1b1.DefaultIfEmpty()
            //         select new
            //         {
            //             a.nMoney,
            //             name = jb1?.sName,
            //         })
            //         .ToList();

            var q1 = (from a1 in q

                          //from jb1 in db.TEmployees.Where(o => o.SchoolID == userData.CompanyID && o.sEmp == a1.sEmp).DefaultIfEmpty(
                      join b1 in tEmpList
                       on a1.sEmp equals b1.sEmp into ja1b1
                      from jb1 in ja1b1.DefaultIfEmpty()

                      join c1 in q_user
                      on new { user_id = a1.sID, user_type = a1.cType } equals new { user_id = c1.sID, user_type = "0" } into ja1c1
                      from jc1 in ja1c1.DefaultIfEmpty()

                      join c2 in tEmpList
                      on new { user_id = a1.sID, user_type = a1.cType } equals new { user_id = c2.sEmp, user_type = "1" } into ja1c2
                      from jc2 in ja1c2.DefaultIfEmpty()

                      join c3 in q_CardBackup

                      on new { user_id = a1.CardHistoryID ?? Guid.Empty } equals new { user_id = c3.CardHistoryID } into ja1jc3
                      from jc3 in ja1jc3.DefaultIfEmpty()

                      select new views02
                      {
                          money = a1.nMoney,
                          time = a1.dMoney.ToString("HH:mm:ss"),
                          emp_name = jb1 == null ? "" : jb1.sName + " " + jb1.sLastname,
                          user_name = jc3 != null ? (jc3.CardName + " - " + jc3.UserName + " ( บัตรสำรอง )") :
                              (jc2 != null ? jc2.sName + " " + jc2.sLastname :
                              (jc1 != null ? (jc1.sStudentID + "-" + jc1.sName + " " + jc1.sLastname) : "")),
                          //user_name = jc1 != null ? (jc1.sStudentID + "-" + jc1.sName + " " + jc1.sLastname) :
                          //    (jc2 != null ? jc2.sName + " " + jc2.sLastname :
                          //    (jc3 != null ? jc3.CardName + " - " + jc3.UserName + " ( บัตรสำรอง )" : "")),
                          class_name = jc1 != null ? jc1.class_name : "",
                          type = a1.MoneyID == 0 ? 2 : 1,
                          cardName = jc3?.CardName,
                          dCancle = a1.dayCancal?.ToString("dd/MM/yyyy HH:mm:ss"),
                          cancle_name = a1.cancle_name,
                          cancle_status = a1.cancle_status,
                          refno = a1.refno1  +(string.IsNullOrWhiteSpace(a1.refno2) ? "" : " " + (a1.refno2?.Trim() + "<br>ยอดตกน้ำวันที่ "+ a1.txCreated?.ToString("dd/MM/yyyy HH:mm:ss"))),                          
                          topup_type = a1.topup_type
                  

                      }).ToList();

            return new header_reports
            {
                data = q1,
                header_text = "ยอดเติมเงิน วันที่ " + search.dStart.Value.ToString("dd MMMM yyyy", new CultureInfo("th-th")),
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
            public decimal? KIOSK { get; set; }
            public string lable { get; set; }
            public string topup_type { get; set; }
            public string values { get; set; }
            public decimal withdraw { get; internal set; }
        }

        public class views02
        {

            public string emp_name { get; set; }
            public string user_name { get; set; }
            public string class_name { get; set; }
            public string time { get; set; }
            public decimal? money { get; set; }
            public int type { get; set; }

            public string cancle_status { get; set; }
            public string dCancle { get; internal set; }
            public string cancle_name { get; internal set; }
            public string refno { get; set; }
            public string remark
            {
                get
                {
                    if (cancle_status == "Y")
                    {
                        return "ยกเลิก " + string.Format("{0:dd/MM/yyyy HH:mm:ss}", dCancle) + "_" + cancle_name;
                    }

                    if (!string.IsNullOrEmpty(cardName))
                    {
                        return $"ชื่อบัตร_{cardName}";
                    }

                    return "";
                }
            }

            public string topup_type { get; internal set; }
            public string cardName { get; internal set; }
            public string refno1 { get; internal set; }
            public string refno2 { get; internal set; }
            public string txCreated { get; internal set; }
        }

        public class Search
        {
            public int sort_type { get; set; }
            public string type { get; set; }
            public DateTime? dStart { get; set; }
            public DateTime? dEnd { get; set; }
            public int? emp_id { get; set; }
            public string time1 { get; set; }
            public string time2 { get; set; }
        }

        public class TC_Money
        {
            public int MoneyID { get; set; }
            public System.DateTime dMoney { get; set; }
            public int sID { get; set; }
            public Nullable<decimal> nMoney { get; set; }
            public string cType { get; set; }
            public Nullable<int> sEmp { get; set; }
            public Nullable<decimal> nBalance { get; set; }
            public string topup_type { get; set; }
            public Nullable<System.DateTime> dayCancal { get; set; }
            public Nullable<int> UserCancel { get; set; }
            public string TopupId { get; set; }
            public int SchoolID { get; set; }
            public Nullable<int> CreatedBy { get; set; }
            public Nullable<int> UpdatedBy { get; set; }
            public Nullable<System.DateTime> CreatedDate { get; set; }
            public Nullable<System.DateTime> UpdatedDate { get; set; }
            public bool cDel { get; set; }
            public Nullable<System.Guid> CardHistoryID { get; set; }
            public string refno1 { get; set; }
            public string refno2 { get; set; }
            public string cancle_status { get; set; }
            public string cancle_name { get; set; }

            public DateTime? txCreated { get; set; }
            //public string txCreatedTH
            //{
            //    get
            //    {
            //        return txCreated?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
            //    }
            //}
        }
    }
}