using JabjaiEntity.DB;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    public partial class ReportTraining : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_detail(Search search)
        {
            var userData = GetUserData();

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return null;
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {

                if (search.sort_type == 1)
                {
                    search.dEnd = search.dStart.Value.AddMonths(1).AddDays(-1);
                }
                else if (search.sort_type == 2)
                {
                    search.dEnd = search.dStart.Value.AddYears(1).AddDays(-1);
                }

                string SQL = "";

                SQL = string.Format(@"
DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:yyyyMMdd} 00:00';
DECLARE @DAYEND DATETIME = '{2:yyyyMMdd} 23:59:59';
DECLARE @sID INT = {3};

SELECT *
FROM (
	SELECT 1 'Type' , dMoney 'Date', nMoney  , CASE WHEN topup_type IN ('WBS1','DBS1') THEN 'WBS1' ELSE topup_type END 'tType' , '' 'Remark', '' 'Shop'
	FROM JabjaiSchoolSingleDB.dbo.TMoney 
	WHERE dMoney BETWEEN @DAYSTART AND @DAYEND 
	AND (@sID = 0 OR sID = @sID)  
	AND SchoolID = @SchoolID 
	AND dayCancal IS NULL 
	AND ISNULL(cDel,0) = 0

	UNION 
	SELECT 1 'Type'  , dMoney 'Date', nMoney  , CASE WHEN topup_type IN ('WBS1','DBS1') THEN 'WBS1' ELSE topup_type END 'tType' , '' 'Remark', '' 'Shop'
	FROM JabjaiSchoolHistory.dbo.TMoney 
	WHERE dMoney BETWEEN @DAYSTART AND @DAYEND 
	AND (@sID = 0 OR sID = @sID)  
	AND SchoolID = @SchoolID 
	AND dayCancal IS NULL 
	AND ISNULL(cDel,0) = 0

	UNION

	SELECT 2 'Type'  , dayCancal 'Date', nMoney , CASE WHEN topup_type IN ('WBS1','DBS1') THEN 'WBS1' ELSE topup_type END 'tType' , '' 'Remark', '' 'Shop'
	FROM JabjaiSchoolSingleDB.dbo.TMoney 
	WHERE dayCancal BETWEEN @DAYSTART AND @DAYEND 
	AND (@sID = 0 OR sID = @sID)  
	AND SchoolID = @SchoolID 
	AND dayCancal IS NOT NULL 
	AND ISNULL(cDel,0) = 0

	UNION 

	SELECT 2 'Type'  , dayCancal 'Date' , nMoney , CASE WHEN topup_type IN ('WBS1','DBS1') THEN 'WBS1' ELSE topup_type END 'tType' , '' 'Remark', '' 'Shop'
	FROM JabjaiSchoolHistory.dbo.TMoney 
	WHERE dayCancal BETWEEN @DAYSTART AND @DAYEND 
	AND (@sID = 0 OR sID = @sID)  
	AND SchoolID = @SchoolID 
	AND dayCancal IS NOT NULL 
	AND ISNULL(cDel,0) = 0

	UNION

	Select 3 'Type' , dWithdrawal 'Date' ,nMoney, sWithdrawalType 'tType' , '' 'Remark', '' 'Shop'
	-- select *  
	FROM JabjaiSchoolSingleDB.dbo.TWithdrawal 
	WHERE  1=1
	and SchoolID = @SchoolID 
	AND dWithdrawal BETWEEN  @DAYSTART AND @DAYEND
	AND dCanCel IS NULL AND  ISNULL(cDel,0) = 0
	AND (@sID = 0 OR UserID = @sID)

	UNION

	SELECT 4 'Type' , A.dSell 'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop'
	FROM JabjaiSchoolSingleDB.dbo.TSell A
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE A.dayCancal IS NULL 
	AND A.SchoolID = @SchoolID AND A.dSell BETWEEN @DAYSTART AND @DAYEND  
	AND (A.sID = @sID OR A.sID2 = @sID OR @sID = 0 ) 

    UNION

	SELECT 5 'Type' , A.dayCancal 'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop'
	FROM JabjaiSchoolSingleDB.dbo.TSell A
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE A.dayCancal IS NOT NULL 
	AND A.SchoolID = @SchoolID 
	AND A.dayCancal BETWEEN @DAYSTART AND @DAYEND  
	AND (A.sID = @sID OR A.sID2 = @sID OR @sID = 0 ) 
)T
ORDER BY T.Date asc

", userData.CompanyID, search.dStart, search.dEnd, search.user_id);

                //                SQL = string.Format(@"DECLARE @SCHOOLID INT = {0}
                //DECLARE @DATE_START DATETIME = '{1:yyyyMMdd} 00:00:00'
                //DECLARE @DATE_END DATETIME = '{2:yyyyMMdd} 23:59:59'

                //SELECT A.SchoolID,A.sEmp,A.sName + ' ' + A.sLastname AS Employees_Name,ISNULL(SUM(B.nMoney),0) AS SUM_MONEY 
                //FROM TEmployees AS A 
                //LEFT OUTER JOIN TMoney AS B ON A.sEmp = B.sEmp AND A.SchoolID = B.SchoolID AND B.dMoney BETWEEN @DATE_START AND @DATE_END AND B.dayCancal IS NULL AND B.cDel = 0
                //WHERE A.SchoolID = @SCHOOLID AND ISNULL(A.cDel,0) = 0
                //GROUP BY A.SchoolID,A.sEmp,A.SchoolID,A.sName,A.sLastname
                //HAVING ISNULL(SUM(B.nMoney),0) > 0
                //", userData.CompanyID, search.dStart, search.dEnd);

                //                SQL = string.Format(@"
                //DECLARE @SchoolID INT = {0};
                //DECLARE @DAYSTART DATETIME = '{1:yyyyMMdd} 00:00';
                //DECLARE @DAYEND DATETIME = '{2:yyyyMMdd} 23:59:59';
                //DECLARE @sID INT = {3};


                //SELECT *
                //FROM (
                //	SELECT 1 'Type' , dMoney 'Date', nMoney  , CASE WHEN topup_type IN ('WBS1','DBS1') THEN 'WBS1' ELSE topup_type END 'tType' , '' 'Remark', '' 'Shop'
                //	FROM JabjaiSchoolSingleDB.dbo.TMoney A
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TBackupCardHistory B ON A.SchoolID = B.SchoolID AND A.CardHistoryID = B.CardHistoryID
                //	WHERE dMoney BETWEEN @DAYSTART AND @DAYEND 
                //	AND (@sID = 0 OR sID = @sID OR B.UserID = @sID)  
                //	AND A.SchoolID = @SchoolID 
                //	AND dayCancal IS NULL 
                //	AND ISNULL(A.cDel,0) = 0

                //	UNION 
                //	SELECT 1 'Type'  , dMoney 'Date', nMoney  , CASE WHEN topup_type IN ('WBS1','DBS1') THEN 'WBS1' ELSE topup_type END 'tType' , '' 'Remark', '' 'Shop'
                //	FROM JabjaiSchoolHistory.dbo.TMoney 
                //	WHERE dMoney BETWEEN @DAYSTART AND @DAYEND 
                //	AND (@sID = 0 OR sID = @sID)  
                //	AND SchoolID = @SchoolID 
                //	AND dayCancal IS NULL 
                //	AND ISNULL(cDel,0) = 0

                //	UNION

                //	SELECT 2 'Type'  , dayCancal 'Date', nMoney , CASE WHEN topup_type IN ('WBS1','DBS1') THEN 'WBS1' ELSE topup_type END 'tType' , '' 'Remark', '' 'Shop'
                //	FROM JabjaiSchoolSingleDB.dbo.TMoney A
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TBackupCardHistory B ON A.SchoolID = B.SchoolID AND A.CardHistoryID = B.CardHistoryID
                //	WHERE dayCancal BETWEEN @DAYSTART AND @DAYEND 
                //	AND (@sID = 0 OR sID = @sID OR B.UserID = @sID)  
                //	AND A.SchoolID = @SchoolID 
                //	AND dayCancal IS NOT NULL 
                //	AND ISNULL(A.cDel,0) = 0

                //	UNION 

                //	SELECT 2 'Type'  , dayCancal 'Date' , nMoney , CASE WHEN topup_type IN ('WBS1','DBS1') THEN 'WBS1' ELSE topup_type END 'tType' , '' 'Remark', '' 'Shop'
                //	FROM JabjaiSchoolHistory.dbo.TMoney 
                //	WHERE dayCancal BETWEEN @DAYSTART AND @DAYEND 
                //	AND (@sID = 0 OR sID = @sID)  
                //	AND SchoolID = @SchoolID 
                //	AND dayCancal IS NOT NULL 
                //	AND ISNULL(cDel,0) = 0

                //	UNION

                //	Select 3 'Type' , dWithdrawal 'Date' ,nMoney, sWithdrawalType 'tType' , '' 'Remark', '' 'Shop'
                //	-- select *  
                //	FROM JabjaiSchoolSingleDB.dbo.TWithdrawal A
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TBackupCardHistory B ON A.SchoolID = B.SchoolID AND A.CardHistoryID = B.CardHistoryID
                //	WHERE  1=1
                //	and A.SchoolID = @SchoolID 
                //	AND dWithdrawal BETWEEN  @DAYSTART AND @DAYEND
                //	AND dCanCel IS NULL AND  ISNULL(A.cDel,0) = 0
                //	AND (@sID = 0 OR A.UserID = @sID OR B.UserID = @sID)

                //	UNION

                //	SELECT 4 'Type' , A.dSell 'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop'
                //	FROM JabjaiSchoolSingleDB.dbo.TSell A
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TBackupCardHistory E ON A.SchoolID = E.SchoolID AND A.CardID = E.CardHistoryID
                //	WHERE A.dayCancal IS NULL 
                //	AND A.SchoolID = @SchoolID AND A.dSell BETWEEN @DAYSTART AND @DAYEND  
                //	AND (A.sID = @sID OR A.sID2 = @sID OR E.UserID = @sID  OR @sID = 0 ) 

                //    UNION

                //	SELECT 5 'Type' , A.dayCancal 'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop'
                //	FROM JabjaiSchoolSingleDB.dbo.TSell A
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
                //	LEFT JOIN JabjaiSchoolSingleDB.dbo.TBackupCardHistory E ON A.SchoolID = E.SchoolID AND A.CardID = E.CardHistoryID
                //	WHERE A.dayCancal IS NOT NULL 
                //	AND A.SchoolID = @SchoolID 
                //	AND A.dayCancal BETWEEN @DAYSTART AND @DAYEND  
                //	AND (A.sID = @sID OR A.sID2 = @sID OR E.UserID = @sID OR @sID = 0 ) 
                //)T
                //ORDER BY T.Date asc

                //", userData.CompanyID, search.dStart, search.dEnd, search.user_id);

                var q = db.Database.SqlQuery<TransactionLog>(SQL).ToList();


                SQL = string.Format(@"
DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:yyyyMMdd} 00:00';
DECLARE @DAYEND DATETIME = '{2:yyyyMMdd} 23:59:59';
DECLARE @sID INT = {3};

SELECT top 1 *
FROM(
	SELECT *
	FROM (
		Select top 1 A.sMessage , B.user_type , A.dSend 
		FROM JabjaiSchoolSingleDB.dbo.TMessageBox A
		JOIN JabjaiSchoolSingleDB.dbo.TMessage_User B ON A.nMessageID = B.message_id 
		WHERE A.SchoolID = @SchoolID AND B.SchoolID = @SchoolID
		AND A.cDel = 0
		AND A.nType in (2,3,12)
		AND B.UserID = @sID
        AND A.dSend < @DAYSTART
		--AND A.dSend BETWEEN @DAYSTART AND @DAYEND 
		ORDER BY A.dSend DESC
	)T1

	UNION 

	SELECT *
	FROM (
		Select top 1 A.sMessage , B.user_type , A.dSend 
		FROM JabjaiSchoolHistory.dbo.TMessageBox A
		JOIN JabjaiSchoolHistory.dbo.TMessage_User B ON A.nMessageID = B.message_id 
		WHERE A.SchoolID = @SchoolID  AND B.SchoolID = @SchoolID
		AND A.cDel = 0
		AND A.nType in (2,3,12)
		AND B.UserID = @sID
        AND A.dSend < @DAYSTART
		--AND A.dSend BETWEEN @DAYSTART AND @DAYEND 
		ORDER BY A.dSend DESC
	)T2
)T
ORDER BY T.dSend DESC
", userData.CompanyID, search.dStart, search.dEnd, search.user_id);

                var d = db.Database.SqlQuery<CurrentBalance>(SQL).FirstOrDefault();
                string last = "";
                dynamic userBalance;
                if (d != null)
                {
                    last = d.sMessage?.Split(',').ElementAtOrDefault(1);

                    if (d.user_type == 0)//student
                    {
                        var currentBalance = db.TUser
                              .Where(o => o.SchoolID == userData.CompanyID && o.sID == search.user_id)
                              .Select(o => new { id = o.sID, balance = o.nMoney ?? 0, name = o.sName + " " + o.sLastname })
                              .FirstOrDefault();

                        userBalance = new { type = 2, info = currentBalance, last = last };
                    }
                    else
                    {
                        var currentBalance = db.TEmployees
                          .Where(o => o.SchoolID == userData.CompanyID && o.sEmp == search.user_id)
                          .Select(o => new { id = o.sEmp, balance = o.nMoney ?? 0, name = o.sName + " " + o.sLastname })
                          .FirstOrDefault();

                        userBalance = new { type = 1, info = currentBalance, last = last };
                    }
                }
                else
                {
                    last = "0";

                    var currentBalance = db.TEmployees
                        .Where(o => o.SchoolID == userData.CompanyID && o.sEmp == search.user_id)
                        .Select(o => new { id = o.sEmp, balance = o.nMoney ?? 0, name = o.sName + " " + o.sLastname })
                        .FirstOrDefault();

                    if(currentBalance != null)
                    {
                        userBalance = new { type = 1, info = currentBalance, last = last };
                    }
                    else
                    {
                        currentBalance = db.TUser
                            .Where(o => o.SchoolID == userData.CompanyID && o.sID == search.user_id)
                            .Select(o => new { id = o.sID, balance = o.nMoney ?? 0, name = o.sName + " " + o.sLastname })
                            .FirstOrDefault();

                        userBalance = new { type = 2, info = currentBalance, last = last };
                    }
                }
                              
                return new { data = q, user = userBalance };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void exportExcel(Search search)
        {
            dynamic q = reports_detail(search);

            var d = q.data as List<TransactionLog>;
            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                var f_company = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == userData.CompanyID);

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานข้อมูลการใช้บัตร");
                    var worksheet = excel.Workbook.Worksheets["รายงานข้อมูลการใช้บัตร"];
                    int rowsIndex = 1, rowsData = 1;
                    //decimal SUM_MONEY = 0, SUM_WD = 0, NET = 0;

                    string _format = "รายงานข้อมูลการใช้บัตร (รายละเอียด)";
                    string _format2 = $"จากวันที่ {search.dStart?.ToString("dd MMM", new CultureInfo("th-TH"))} ถึง {search.dEnd?.ToString("dd MMM yyyy", new CultureInfo("th-TH"))}";

                    //switch (search.sort_type)
                    //{
                    //    case 1: _format = "รายงานสรุปยอดเติมเงินประจำเดือน MMM yyyy"; break;
                    //    case 2: _format = "รายงานสรุปยอดเติมเงินประจำปี yyyy"; break;
                    //    case 3:
                    //        if (search.dStart != search.dEnd)
                    //        {
                    //            _format = "รายงานสรุปยอดเติมเงินประจำวันที่ dd/MM/yyyy - " + search.dEnd.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th"));
                    //        }
                    //        else
                    //        {
                    //            _format = "รายงานสรุปยอดเติมเงินประจำวันที่ dd/MM/yyyy ";
                    //        }
                    //        break;
                    //}

                    dynamic _usr = q.user;


                    string txt1 = "";
                    switch (_usr.type)
                    {
                        case 1:
                            txt1 = $"ชื่อ { _usr.info.name } (ครู) 	ยอดเงินคงเหลือ {_usr.info.balance}";
                            break;
                        case 2:
                            txt1 = $"ชื่อ { _usr.info.name } (นักเรียน) ยอดเงินคงเหลือ {_usr.info.balance}";
                            break;
                        default:
                            break;
                    }

                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 6], true, f_company.sCompany, 10, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 6], true, _format, 10, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 6], true, _format2, 10, ExcelHorizontalAlignment.Center);
                    // SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 5], true, search.dStart.Value.ToString(_format, new CultureInfo("th-th")), 10, ExcelHorizontalAlignment.Center);
                    //SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 4], true, DateTime.Now.ToString("พิมพ์วันที่ : dd MMMM yyyy HH.mm น.", new CultureInfo("th-th")), 10, ExcelHorizontalAlignment.Right);

                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 6], true, txt1, 10, ExcelHorizontalAlignment.Right);

                    SetTableHeader(worksheet.Cells[rowsIndex, 1], false, "ลำดับ", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 2], false, "วันที่/เวลา", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 3], false, "ประเภทการใช้", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 4], false, "ร้านค้า", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 5], false, "จำนวนเงิน", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex++, 6], false, "ยอดคงเหลือ", ExcelHorizontalAlignment.Center);

                    decimal? _last = Decimal.Parse(_usr.last + "");
                    foreach (var r in d)
                    {
                        var _remark = "";
                        var _money = r.nMoney;
                        switch (r.Type)
                        {
                            case 1:
                                _remark = "เติมเงิน";
                                _last += _money;
                                break;
                            case 2:
                                _remark = "ยกเลิกเติมเงิน";
                                _last -= _money;
                                break;
                            case 3:
                                _remark = "ถอนเงิน";
                                _last -= _money;
                                break;
                            case 4:
                                _remark = "ซื้อสินค้า";
                                _last -= _money;
                                break;
                            case 5:
                                _remark = "ยกเลิกซื้อสินค้า";
                                _last += _money;
                                break;
                            default:
                                break;
                        }

                        SetTableRows(worksheet.Cells[rowsIndex, 1], false, string.Format("{0}.", rowsData++), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 2], false, r.Date?.ToString("dd/MM/yyyy HH:mm"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 3], false, _remark, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 4], false, r.Shop, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 5], false, r.nMoney?.ToString("#,#0.##"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 6], false, _last?.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);

                        rowsIndex++;
                    }

                    //foreach (var data in q)
                    //{
                    //    //SetTableRows(worksheet.Cells[rowsIndex, 1], false, string.Format("{0}.", rowsData++), ExcelHorizontalAlignment.Center);
                    //    //SetTableRows(worksheet.Cells[rowsIndex, 2], false, data.Employees_Name, ExcelHorizontalAlignment.Center);
                    //    //SetTableRows(worksheet.Cells[rowsIndex, 3], false, data.SUM_MONEY.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                    //    //SetTableRows(worksheet.Cells[rowsIndex, 4], false, data.SUM_WD.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                    //    //SetTableRows(worksheet.Cells[rowsIndex, 5], false, data.NET.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                    //    //SUM_MONEY += data.SUM_MONEY;
                    //    //SUM_WD += data.SUM_WD;
                    //    //NET += data.NET;

                    //    rowsIndex++;
                    //}

                    //SetTableRows(worksheet.Cells[rowsIndex, 1, rowsIndex, 2], true, "รวม", ExcelHorizontalAlignment.Right);
                    //SetTableRows(worksheet.Cells[rowsIndex, 3], true, SUM_MONEY.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                    //SetTableRows(worksheet.Cells[rowsIndex, 4], true, SUM_WD.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                    //SetTableRows(worksheet.Cells[rowsIndex, 5], true, NET.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);

                    worksheet.Column(1).Width = 8;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 20;
                    worksheet.Column(6).Width = 20;

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                    HttpContext.Current.Response.ContentType = "application/text";
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
                }
            }
        }

        private static void SetTableRows(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.WrapText = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Font.Size = 8;
            }
        }

        private static void SetTableHeader(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.WrapText = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Font.Color.SetColor(Color.White);
                rng.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
                rng.Style.Font.Size = 8;
            }
        }

        private static void SetHeader(ExcelRange Cells, bool Merge, string strValues, int? fontSize, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Font.Size = fontSize ?? 10;
            }
        }

        public class Search
        {
            public int sort_type { get; set; }
            //public string type { get; set; }
            public DateTime? dStart { get; set; }
            public DateTime? dEnd { get; set; }
            //public int? shop_id { get; set; }
            public int? user_id { get; set; }
            public int? emp_id { get; set; }
        }

        public class TM_Topup_Group
        {
            public int SchoolID { get; set; }
            public int sEmp { get; set; }
            public string Employees_Name { get; set; }
            public decimal SUM_MONEY { get; set; }
            public decimal SUM_WD { get; set; }
            public decimal NET { get; set; }
        }

        public class TransactionLog

        {
            public int Type { get; set; }
            public DateTime? Date { get; set; }
            public decimal? nMoney { get; set; }
            public string tType { get; set; }
            public string Remark { get; set; }
            public string Shop { get; set; }
        }

        private class CurrentBalance
        {
            public string sMessage { get; set; }
            public int? user_type { get; set; }
            public DateTime? dSend { get; set; }
        }
    }
}