using FingerprintPayment.Class;
using JabjaiEntity.DB;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public partial class ReportTransLog : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object ReportBackupCard(Search search)
        {
            var userData = GetUserData();
            var now = DateTime.Now.Date;


            DateTime oDate1, oDate2;
            var isValid1 = DateTime.TryParseExact(search.sStart, "yyyyMMdd", new CultureInfo("th-TH"), DateTimeStyles.None, out oDate1);
            var isValid2 = DateTime.TryParseExact(search.sEnd, "yyyyMMdd", new CultureInfo("th-TH"), DateTimeStyles.None, out oDate2);

            search.dStart = oDate1;
            search.dEnd = oDate2;

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var card = db.TBackupCards
                      .Where(o => o.CardID == search.card_id && o.cDel == false && o.SchoolID == userData.CompanyID)
                      .AsNoTracking()
                      .FirstOrDefault();

                var q = from a in db.TBackupCards.Where(o => o.CardID == search.card_id && o.cDel == false && o.SchoolID == userData.CompanyID)
                        from b in db.TBackupCardHistories.Where(o => o.CardID == a.CardID && o.SchoolID == userData.CompanyID
                            //&& (o.BorrowingDate >= search.dStart || (o.ReturnDate ?? now) <= search.dEnd)
                            )//***
                             //from c in db.TUsers.Where( o => o.sID == b.UserID && o.SchoolID == userData.CompanyID).DefaultIfEmpty()
                             //from e in db.TEmployees.Where(o => o.sEmp == b.UserID && o.SchoolID == userData.CompanyID).DefaultIfEmpty()

                        select new
                        {
                            b.CardID,
                            b.CardHistoryID,
                            b.BorrowingDate,
                            b.ReturnDate,
                            b.UserType,
                            b.UserID,
                            b.UserName,
                            b.Insurance,
                        };

                var d = q.ToList();
                //var minDate = d.Min(o => o.BorrowingDate);
                var cardHisList = d.Select(o => o.CardHistoryID).ToList();

                if (cardHisList.Count > 0)
                {
                    string arr = string.Join(",", cardHisList.Select(o => $"'{o.ToString()}'"));
                    string SQL = "";
                    var oldDate = new DateTime(DateTime.Now.Year, 05, 01);


                    SQL = string.Format($@"

SELECT *
FROM (
	SELECT 1 'Type' , dMoney 'Date', nMoney  , CASE WHEN topup_type IN ('WBS1','DBS1','WB01') THEN 'WBS1' ELSE topup_type END 'tType' , 
    '' 'Remark', '' 'Shop', null 'RemarkDate' , A.CardHistoryID
	FROM JabjaiSchoolSingleDB.dbo.{(search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney")} A    
	WHERE dMoney BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'
	AND A.SchoolID = {userData.CompanyID} 
    AND A.CardHistoryID  in ({arr})
	--AND dayCancal IS NULL 
	AND ISNULL(A.cDel,0) = 0

	UNION ALL

	SELECT 1 'Type'  , dMoney 'Date', nMoney  , CASE WHEN topup_type IN ('WBS1','DBS1','WB01') THEN 'WBS1' ELSE topup_type END 'tType' , 
    '' 'Remark', '' 'Shop',  null 'RemarkDate' , A.CardHistoryID
	FROM JabjaiSchoolHistory.dbo.TMoney  A   
	WHERE dMoney BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}' 
	AND A.SchoolID = {userData.CompanyID} 
    AND A.CardHistoryID in ({arr})
	--AND dayCancal IS NULL 
	AND ISNULL(A.cDel,0) = 0

	UNION ALL

	SELECT 2 'Type'  , dayCancal 'Date', nMoney , CASE WHEN topup_type IN ('WBS1','DBS1','WB01') THEN 'WBS1' ELSE topup_type END 'tType' , 
    ''  'Remark' , '' 'Shop', null 'RemarkDate', A.CardHistoryID
	FROM JabjaiSchoolSingleDB.dbo.{(search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney")} A
   
	WHERE dayCancal BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}' 
	AND A.SchoolID = {userData.CompanyID} 
    AND A.CardHistoryID in ({arr})
	AND A.dayCancal IS NOT NULL 
	AND ISNULL(A.cDel,0) = 0

	UNION ALL

	SELECT 2 'Type'  , dayCancal 'Date' , nMoney , CASE WHEN topup_type IN ('WBS1','DBS1','WB01') THEN 'WBS1' ELSE topup_type END 'tType' , 
    '' 'Remark', '' 'Shop', null 'RemarkDate' , A.CardHistoryID

	FROM JabjaiSchoolHistory.dbo.TMoney A
   
	WHERE dayCancal BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}' 
    AND A.CardHistoryID in ({arr})
	AND A.SchoolID = {userData.CompanyID} 
	AND A.dayCancal IS NOT NULL 
	AND ISNULL(A.cDel,0) = 0

	UNION ALL

	Select 3 'Type' , dWithdrawal 'Date' ,nMoney, sWithdrawalType 'tType' , '' 'Remark', '' 'Shop', null 'RemarkDate' , CardHistoryID
	-- select *  
	FROM JabjaiSchoolSingleDB.dbo.TWithdrawal 
	WHERE  1=1
	and SchoolID = {userData.CompanyID} 
	AND dWithdrawal BETWEEN  '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'
	--AND dCanCel IS NULL 
    AND  ISNULL(cDel,0) = 0
    AND CardHistoryID in ({arr})

	UNION ALL

	SELECT 4 'Type' , ISNULL(A.UpdatedTime, A.dSell) 'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', CASE WHEN A.UpdatedTime IS NOT NULL AND DATEDIFF(second, A.dSell, A.UpdatedTime) > 3  THEN A.dSell ELSE null END 'RemarkDate' , A.CardID 'CardHistoryID'
	FROM JabjaiSchoolSingleDB.dbo.TSell A
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE 1=1 --A.dayCancal IS NULL 
	AND A.SchoolID = {userData.CompanyID} 
    AND ( A.dSell BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  OR A.UpdatedTime BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  )

    AND A.CardID in ({arr})

    UNION ALL

	SELECT 4 'Type' , ISNULL(A.UpdatedTime, A.dSell) 'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', CASE WHEN A.UpdatedTime IS NOT NULL AND DATEDIFF(second, A.dSell, A.UpdatedTime) > 3 THEN A.dSell ELSE null END 'RemarkDate', A.CardID 'CardHistoryID'
	FROM JabjaiSchoolHistory.dbo.TSell A
	LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE 1=1 --A.dayCancal IS NULL 
	AND A.SchoolID = {userData.CompanyID} 	
    AND ( A.dSell BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  OR A.UpdatedTime BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  ) 

    AND A.CardID in ({arr})
    {(search.dStart <= oldDate ? $@"
    UNION ALL

	SELECT 4 'Type' , ISNULL(A.UpdatedTime, A.dSell) 'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', CASE WHEN A.UpdatedTime IS NOT NULL AND DATEDIFF(second, A.dSell, A.UpdatedTime) > 3 THEN A.dSell ELSE null END 'RemarkDate', A.CardID 'CardHistoryID'
	FROM JabjaiSchoolHistory.dbo.TSell_Backup A
	LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail_Backup B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE 1=1 --A.dayCancal IS NULL 
	AND A.SchoolID = {userData.CompanyID} 	
    AND ( A.dSell BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  OR A.UpdatedTime BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  ) 

    AND A.CardID in ({arr})" : "")}
   

    UNION ALL

	SELECT 5 'Type' ,  A.dayCancal  'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', null 'RemarkDate' , A.CardID 'CardHistoryID'
	FROM JabjaiSchoolSingleDB.dbo.TSell A
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE A.dayCancal IS NOT NULL 
	AND A.SchoolID = {userData.CompanyID} 
	AND A.dayCancal BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  

    AND A.CardID in ({arr})

    UNION ALL

	SELECT 5 'Type' ,  A.dayCancal  'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', null 'RemarkDate' , A.CardID 'CardHistoryID'
	FROM JabjaiSchoolHistory.dbo.TSell A
	LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE A.dayCancal IS NOT NULL 
	AND A.SchoolID = {userData.CompanyID} 
	AND A.dayCancal BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  

    AND A.CardID in ({arr})

     {(search.dStart <= oldDate ? $@"
    UNION ALL

	SELECT 5 'Type' ,  A.dayCancal  'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', null 'RemarkDate' , A.CardID 'CardHistoryID'
	FROM JabjaiSchoolHistory.dbo.TSell_Backup A
	LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail_Backup B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE A.dayCancal IS NOT NULL 
	AND A.SchoolID = {userData.CompanyID} 
	AND A.dayCancal BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  

    AND A.CardID in ({arr})" : "")}
)T
ORDER BY T.Date asc

", userData.CompanyID, search.dStart, search.dEnd, arr);

                    var data1 = db.Database.SqlQuery<TransactionLog>(SQL).ToList();
                    var onlyCardShow = data1.Select(o => o.CardHistoryID).Distinct().ToList();

                    foreach (var item in d.Where(o => onlyCardShow.Contains(o.CardHistoryID)))
                    {
                        if (item.BorrowingDate?.Date >= search.dStart.Date)
                            data1.Add(new TransactionLog()
                            {
                                CardHistoryID = item.CardHistoryID,
                                Date = item.BorrowingDate,
                                nMoney = null,
                                Type = 6,
                                Remark = "",
                                RemarkDate = null,
                                Shop = "",
                                tType = "BRW",
                                User = "",
                                Insurance = item.Insurance,
                            });

                        if (item.ReturnDate.HasValue && item.ReturnDate?.Date <= search.dEnd)
                            data1.Add(new TransactionLog()
                            {
                                CardHistoryID = item.CardHistoryID,
                                Date = item.ReturnDate,
                                nMoney = null,
                                Type = 7,
                                Remark = "",
                                RemarkDate = null,
                                Shop = "",
                                tType = "RTN",
                                User = "",
                                Insurance = item.Insurance,
                            });
                    }

                    var result = from a in d
                                 from b in data1.Where(o => o.CardHistoryID == a.CardHistoryID)
                                 orderby b.Date ascending
                                 select new TransactionLog
                                 {
                                     Date = b.Date,
                                     nMoney = b.nMoney,
                                     Remark = b.Remark,
                                     RemarkDate = b.RemarkDate,
                                     Shop = b.Shop,
                                     tType = b.tType,
                                     Type = b.Type,
                                     User = $"{GetUserType(a.UserType, a.UserName)}",
                                     Insurance = a.Insurance,
                                 };

                    var cardhis = d.OrderByDescending(o => o.BorrowingDate).Select(o => o.CardHistoryID).FirstOrDefault();

                    if (search.dStart == DateTime.Now.Date)
                    {
                        var date = search.dStart.AddDays(-1);
                        SQL = string.Format(@"
DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:yyyyMMdd} 00:00';
DECLARE @DAYEND DATETIME = '{2:yyyyMMdd} 23:59:59';


SELECT  Balance 'OpeningBalance'

FROM [JabjaiSchoolSingleDB].[dbo].[UserDailyBalanceTempCard]

WHERE SchoolID = @SchoolID
AND CardHistoryID = '{3}'
AND BusinessDate = @DAYSTART

", userData.CompanyID, date, search.dEnd, cardhis);
                    }
                    else
                    {
                        SQL = string.Format(@"
DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:yyyyMMdd} 00:00';
DECLARE @DAYEND DATETIME = '{2:yyyyMMdd} 23:59:59';

SELECT  OpeningBalance 

FROM [JabjaiSchoolSingleDB].[dbo].[UserDailyBalanceTempCard]

WHERE SchoolID = @SchoolID
AND CardHistoryID =  '{3}'
AND BusinessDate = @DAYSTART

", userData.CompanyID, search.dStart, search.dEnd, cardhis);

                    }

                    var daily = db.Database.SqlQuery<CurrentBalance>(SQL).FirstOrDefault();

                    return new
                    {
                        data = result.ToList(),
                        user = new
                        {
                            type = 3,
                            info = new { id = 0, balance = card.Money ?? 0, name = card.CardName },
                            last = (daily?.OpeningBalance) ?? 0,
                        }
                    };
                }

                return new
                {
                    data = new { },
                    user = new
                    {
                        type = 3,
                        info = new { id = 0, balance = card.Money ?? 0, name = card.CardName },
                        last = 0
                    }
                };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void exportTempExcel(Search search)
        {
            dynamic q = ReportBackupCard(search);

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
                    string _format2 = $"จากวันที่ {search.dStart.ToString("dd MMM", new CultureInfo("th-TH"))} ถึง {search.dEnd.ToString("dd MMM yyyy", new CultureInfo("th-TH"))}";

                    dynamic _usr = q.user;


                    string txt1 = "";
                    txt1 = $"บัตร {_usr.info.name} ยอดเงินคงเหลือ {_usr.info.balance}";

                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 9], true, f_company.sCompany, 10, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 9], true, _format, 10, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 9], true, _format2, 10, ExcelHorizontalAlignment.Center);
                    // SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 5], true, search.dStart.Value.ToString(_format, new CultureInfo("th-th")), 10, ExcelHorizontalAlignment.Center);
                    //SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 4], true, DateTime.Now.ToString("พิมพ์วันที่ : dd MMMM yyyy HH.mm น.", new CultureInfo("th-th")), 10, ExcelHorizontalAlignment.Right);

                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 9], true, txt1, 10, ExcelHorizontalAlignment.Right);

                    SetTableHeader(worksheet.Cells[rowsIndex, 1], false, "ลำดับ", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 2], false, "ประเภทผู้ยืม", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 3], false, "วันที่/เวลา", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 4], false, "ประเภทการใช้", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 5], false, "ร้านค้า", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 6], false, "จำนวนเงิน", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 7], false, "ยอดคงเหลือ", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 8], false, "ค่าประกัน", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex++, 9], false, "หมายเหตุ", ExcelHorizontalAlignment.Center);

                    decimal? _last = Decimal.Parse(_usr.last + "");
                    foreach (var r in d)
                    {
                        var _remark = "";
                        var _remark2 = "";

                        var _insurance = Decimal.Parse(r.Insurance + "");
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
                                _last = _last - _money + _insurance;
                                _insurance = 0;
                                break;
                            case 4:
                                _remark = "ซื้อสินค้า";
                                _last -= _money;
                                break;
                            case 5:
                                _remark = "ยกเลิกซื้อสินค้า";
                                _last += _money;
                                break;
                            case 6:
                                _remark = "ยืมบัตร";
                                _last = 0;
                                break;
                            case 7:
                                _remark = "คืนบัตร";
                                _last = 0;
                                break;
                            default:
                                break;
                        }

                        if (r.RemarkDate.HasValue)
                        {
                            _remark2 = "เวลาซื้อจริง วันที่ " + r.RemarkDate?.ToString("dd/MM/yyyy HH:mm");
                        }

                        SetTableRows(worksheet.Cells[rowsIndex, 1], false, string.Format("{0}.", rowsData++), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 2], false, r.User, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 3], false, r.Date?.ToString("dd/MM/yyyy HH:mm"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 4], false, _remark, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 5], false, r.Shop, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 6], false, (r.Type == 6 || r.Type == 7) ? "" : r.nMoney?.ToString("#,#0.##"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 7], false, (r.Type == 6 || r.Type == 7) ? "" : _last?.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 8], false, (r.Type == 6 || r.Type == 7) ? "" : _insurance + "", ExcelHorizontalAlignment.Center);

                        SetTableRows(worksheet.Cells[rowsIndex, 9], false, _remark2, ExcelHorizontalAlignment.Center);
                        rowsIndex++;
                    }


                    worksheet.Column(1).Width = 8;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 20;
                    worksheet.Column(6).Width = 20;
                    worksheet.Column(7).Width = 20;
                    worksheet.Column(8).Width = 20;
                    worksheet.Column(9).Width = 20;

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

        private static string GetUserType(int? userType, string userName)
        {
            string type = "";
            switch (userType)
            {
                case 0:
                    type = "นักเรียน";
                    break;

                case 1:
                    type = "ครู";
                    break;

                case 2:
                    type = "บุคคลภายนอก";
                    break;

                default:
                    break;
            }

            return $"{userName} ({type})";
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
                //if (search.sort_type == 1)
                //{
                //    search.dEnd = search.dStart.Value.AddMonths(1).AddDays(-1);
                //}
                //else if (search.sort_type == 2)
                //{
                //    search.dEnd = search.dStart.Value.AddYears(1).AddDays(-1);
                //}

                //DateTime oDate1, oDate2;
                //var isValid1 = DateTime.TryParseExact(search.sStart, "yyyyMMdd", new CultureInfo("th-TH"), DateTimeStyles.None, out oDate1);
                //var isValid2 = DateTime.TryParseExact(search.sEnd, "yyyyMMdd", new CultureInfo("th-TH"), DateTimeStyles.None, out oDate2);

                //search.dStart = oDate1;
                //search.dEnd = oDate2;

                string SQL = "";

                //DECLARE @SchoolID INT = 819;
                //DECLARE @DAYSTART DATETIME = '20200101 00:00';
                //DECLARE @DAYEND DATETIME = '20220504 23:59:59';
                //DECLARE @sID INT = 379193;
                var oldDate = new DateTime(DateTime.Now.Year, 05, 01);
                SQL = string.Format($@"

SELECT *
FROM (
	SELECT A.Type, A.Date, A.nMoney, CASE WHEN A.topup_type IN ('WBS1','DBS1') THEN 'WBS1' ELSE A.topup_type END 'tType', 
    CASE WHEN A.topup_type IN ('MBS1') THEN  B.Type + B.ReferenceNo  ELSE '' END  'Remark', '' 'Shop', null 'RemarkDate', '' 'DeviceID', '' 'Barcode', '' 'NFCOrEncrypt'
	, B.ChargeID, ISNULL(B.TxCount, 0) 'TxCount', B.txCreated , TRIM(B.txRemark) 'txRemark'
	FROM 
	(
		SELECT 1 'Type', dMoney 'Date', nMoney, topup_type, ChargeID
		FROM JabjaiSchoolSingleDB.dbo.{(search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney")}
		WHERE dMoney BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}' AND ({search.user_id} = 0 OR sID = {search.user_id}) AND SchoolID = {userData.CompanyID} AND CardHistoryID is null AND ISNULL(cDel,0) = 0
		UNION ALL
		SELECT 1 'Type', dMoney 'Date', nMoney, topup_type, ChargeID
		FROM JabjaiSchoolHistory.dbo.TMoney 
		WHERE dMoney BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}' AND ({search.user_id} = 0 OR sID = {search.user_id}) AND SchoolID = {userData.CompanyID} AND CardHistoryID is null AND ISNULL(cDel,0) = 0
        UNION ALL
		SELECT 2 'Type', dayCancal 'Date', nMoney, topup_type, ChargeID
		FROM JabjaiSchoolSingleDB.dbo.{(search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney")} 
		WHERE dayCancal BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}' AND ({search.user_id} = 0 OR sID = {search.user_id}) AND SchoolID = {userData.CompanyID} AND CardHistoryID is null AND dayCancal IS NOT NULL AND ISNULL(cDel,0) = 0
		UNION ALL
		SELECT 2 'Type', dayCancal 'Date', nMoney, topup_type, ChargeID
		FROM JabjaiSchoolHistory.dbo.TMoney 
		WHERE dayCancal BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}' AND ({search.user_id} = 0 OR sID = {search.user_id}) AND SchoolID = {userData.CompanyID} AND CardHistoryID is null AND dayCancal IS NOT NULL AND ISNULL(cDel,0) = 0
	) A
    LEFT JOIN (
		SELECT Type, ChargeID, ReferenceNo, MAX(Count) 'TxCount' ,  Remark 'txRemark', txCreated 
		FROM
		(
		
			SELECT 'KBank/' 'Type',ChargeID, ReferenceNo, 1 'Count',
	         (CASE WHEN DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 10 AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(tx1.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(tx1.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END) 'Remark' , tx1.CreateDate 'txCreated'
			FROM JabjaiSchoolSingleDB.dbo.KTransaction tx1
			WHERE SchoolID={userData.CompanyID} AND CreateDate BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  AND ChargeID IS NOT NULL

			UNION ALL
			SELECT 'KBank/' 'Type',ChargeID, ReferenceNo, 1 'Count',
               (CASE WHEN DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 10 AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(tx1.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(tx1.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx1.CreateDate, tx1.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END)  'Remark' , tx1.CreateDate 'txCreated'
			FROM JabjaiSchoolHistory.dbo.KTransaction tx1
			WHERE SchoolID={userData.CompanyID} AND CreateDate BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  AND ChargeID IS NOT NULL

	        UNION ALL
			SELECT 'KTB/' 'Type', tx3.Ref2 'ChargeID', tx3.Ref2 'ReferenceNo', 1 'Count' , 
			 (CASE WHEN CAST(tx3.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx3.CreateDate, tx3.UpdatedDate) > 5 THEN 'BOT3AM' ELSE '' END)  
+' '+ (CASE WHEN CAST(tx3.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx3.CreateDate, tx3.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) <= 2 AND DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) >= -2) THEN 'BANKDELAY' ELSE '' END)  
+' '+ (CASE WHEN CAST(tx3.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx3.CreateDate, tx3.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) > 2 OR DATEDIFF(SECOND, ptx3.CreateDate, tx3.UpdatedDate) < -2) THEN 'BOTMANUAL' ELSE '' END) 'Remark' , tx3.CreateDate 'txCreated'
			FROM JabjaiSchoolSingleDB.dbo.KTBTransaction tx3
			LEFT JOIN JabjaiSchoolSingleDB.dbo.KTBPaymentTransaction ptx3 ON tx3.SchoolID = ptx3.SchoolID AND tx3.Ref2 = ptx3.Ref2 AND ptx3.RespCode = 0
			WHERE tx3.SchoolID={userData.CompanyID} AND tx3.CreateDate BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  AND tx3.Ref2 IS NOT NULL
		) A
		GROUP BY Type, ChargeID, ReferenceNo,Remark,txCreated
	) B ON A.ChargeID = B.ChargeID
	UNION ALL
	Select 3 'Type' , dWithdrawal 'Date' ,nMoney, sWithdrawalType 'tType' , '' 'Remark', '' 'Shop', null 'RemarkDate' , '' 'DeviceID' ,'' 'Barcode' ,'' 'NFCOrEncrypt', '' 'ChargeID', 0 'TxCount', null 'txCreated' , null 'txRemark'
	-- select *  
	FROM JabjaiSchoolSingleDB.dbo.TWithdrawal 
	WHERE  1=1
	and SchoolID = {userData.CompanyID} 
	AND dWithdrawal BETWEEN  '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'
	--AND dCanCel IS NULL 
    AND  ISNULL(cDel,0) = 0
    AND ISNULL(CardHistoryID , CONVERT(UNIQUEIDENTIFIER, 0x0)) = CONVERT(UNIQUEIDENTIFIER, 0x0)
	AND ({search.user_id} = 0 OR UserID = {search.user_id})

	UNION ALL

	SELECT 4 'Type' , ISNULL(A.UpdatedTime, A.dSell) 'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', CASE WHEN A.UpdatedTime IS NOT NULL AND DATEDIFF(second, A.dSell, A.UpdatedTime) > 3  THEN A.dSell ELSE null END 'RemarkDate', ISNULL(A.DeviceID,'') 'DeviceID' , ISNULL(A.Barcode,'') 'Barcode' , ISNULL(A.NFCOrEncrypt,'') 'NFCOrEncrypt', '' 'ChargeID', 0 'TxCount', null 'txCreated' , null 'txRemark'
	FROM JabjaiSchoolSingleDB.dbo.TSell A
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE 1=1 --A.dayCancal IS NULL 
	AND A.SchoolID = {userData.CompanyID} 
    AND ( A.dSell BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  OR A.UpdatedTime BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  )
	AND (A.sID = {search.user_id} OR A.sID2 = {search.user_id} OR {search.user_id} = 0 ) 
    AND ( A.CardID is null OR A.CardID = '00000000-0000-0000-0000-000000000000' )

    UNION ALL

	SELECT 4 'Type' , ISNULL(A.UpdatedTime, A.dSell) 'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', CASE WHEN A.UpdatedTime IS NOT NULL AND DATEDIFF(second, A.dSell, A.UpdatedTime) > 3 THEN A.dSell ELSE null END 'RemarkDate' , ISNULL(A.DeviceID,'') 'DeviceID' , ISNULL(A.Barcode,'') 'Barcode' , ISNULL(A.NFCOrEncrypt,'') 'NFCOrEncrypt', '' 'ChargeID', 0 'TxCount', null 'txCreated' , null 'txRemark'
	FROM JabjaiSchoolHistory.dbo.TSell A
	LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE 1=1 --A.dayCancal IS NULL 
	AND A.SchoolID = {userData.CompanyID} 	
    AND ( A.dSell BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  OR A.UpdatedTime BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  ) 
	AND (A.sID = {search.user_id} OR A.sID2 = {search.user_id} OR {search.user_id} = 0 ) 
    AND ( A.CardID is null OR A.CardID = '00000000-0000-0000-0000-000000000000' )

    {(search.dStart <= oldDate ? $@"
    UNION ALL

    SELECT 4 'Type' , ISNULL(A.UpdatedTime, A.dSell) 'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', CASE WHEN A.UpdatedTime IS NOT NULL AND DATEDIFF(second, A.dSell, A.UpdatedTime) > 3 THEN A.dSell ELSE null END 'RemarkDate' , ISNULL(A.DeviceID,'') 'DeviceID' , ISNULL(A.Barcode,'') 'Barcode' , ISNULL(A.NFCOrEncrypt,'') 'NFCOrEncrypt', '' 'ChargeID', 0 'TxCount', null 'txCreated' , null 'txRemark'
	FROM JabjaiSchoolHistory.dbo.TSell_Backup A
	LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail_Backup B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE 1=1 --A.dayCancal IS NULL 
	AND A.SchoolID = {userData.CompanyID} 	
    AND ( A.dSell BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  OR A.UpdatedTime BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  ) 
	AND (A.sID = {search.user_id} OR A.sID2 = {search.user_id} OR {search.user_id} = 0 ) 
    AND ( A.CardID is null OR A.CardID = '00000000-0000-0000-0000-000000000000' )" : "")}

    UNION ALL

	SELECT 5 'Type' ,  A.dayCancal  'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', null 'RemarkDate', ISNULL(A.DeviceID,'') 'DeviceID' , ISNULL(A.Barcode,'') 'Barcode' , ISNULL(A.NFCOrEncrypt,'') 'NFCOrEncrypt', '' 'ChargeID', 0 'TxCount', null 'txCreated' , null 'txRemark'
	FROM JabjaiSchoolSingleDB.dbo.TSell A
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE A.dayCancal IS NOT NULL 
	AND A.SchoolID = {userData.CompanyID} 
	AND A.dayCancal BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  
	AND (A.sID = {search.user_id} OR A.sID2 = {search.user_id} OR {search.user_id} = 0 ) 
    AND ( A.CardID is null OR A.CardID = '00000000-0000-0000-0000-000000000000' )

    UNION ALL

	SELECT 5 'Type' ,  A.dayCancal  'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', null 'RemarkDate' , ISNULL(A.DeviceID,'') 'DeviceID' , ISNULL(A.Barcode,'') 'Barcode' , ISNULL(A.NFCOrEncrypt,'') 'NFCOrEncrypt', '' 'ChargeID', 0 'TxCount', null 'txCreated' , null 'txRemark'
	FROM JabjaiSchoolHistory.dbo.TSell A
	LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE A.dayCancal IS NOT NULL 
	AND A.SchoolID = {userData.CompanyID} 
	AND A.dayCancal BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}' 
	AND (A.sID = {search.user_id} OR A.sID2 = {search.user_id} OR {search.user_id} = 0 ) 
    AND ( A.CardID is null OR A.CardID = '00000000-0000-0000-0000-000000000000' )

   {(search.dStart <= oldDate ? $@"
    UNION ALL

    SELECT 5 'Type' ,  A.dayCancal  'Date' ,coalesce(B.nPrice * B.nNumber,A.nTotal) 'nTotal', 'BUY' 'tType' , ISNULL(C.sProduct,'-') 'Remark'  , ISNULL(D.shop_name,'') 'Shop', null 'RemarkDate' , ISNULL(A.DeviceID,'') 'DeviceID' , ISNULL(A.Barcode,'') 'Barcode' , ISNULL(A.NFCOrEncrypt,'') 'NFCOrEncrypt', '' 'ChargeID', 0 'TxCount', null 'txCreated' , null 'txRemark'
	FROM JabjaiSchoolHistory.dbo.TSell_Backup A
	LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail_Backup B ON  A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TProduct C ON  B.nProduct = C.nProductID AND B.SchoolID = C.SchoolID
	LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop D ON  A.shop_id = D.shop_id AND A.SchoolID = D.SchoolID
	WHERE A.dayCancal IS NOT NULL 
	AND A.SchoolID = {userData.CompanyID} 
	AND A.dayCancal BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{search.dEnd.ToString("yyyyMMdd 23:59:59")}'  
	AND ( A.sID = {search.user_id} OR A.sID2 = {search.user_id} OR {search.user_id} = 0 ) 
    AND ( A.CardID is null OR A.CardID = '00000000-0000-0000-0000-000000000000' ) " : "")}	
)T
ORDER BY T.Date asc
"
//, userData.CompanyID, search.dStart.ToString("yyyyMMdd 00:00"), search.dEnd, search.user_id 
//, (search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney")
//, (search.dStart <= oldDate ? @"" : "")
//, (search.dStart <= oldDate ? @"" : "")
);

                var q = db.Database.SqlQuery<TransactionLog>(SQL).ToList();

                var _date = search.dStart;

                if (_date == DateTime.Now.Date)
                {
                    SQL = string.Format(@"
DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:yyyyMMdd} 00:00';
DECLARE @DAYEND DATETIME = '{2:yyyyMMdd} 23:59:59';
DECLARE @sID INT = {3};

SELECT sID , sStudentID , sName + ' ' + sLastname 'FullName'  ,cType 'user_type' , Balance 'OpeningBalance' , Balance

FROM [JabjaiSchoolSingleDB].[dbo].[UserDailyBalance]

WHERE SchoolID = @SchoolID
AND sID = @sID
AND BusinessDate = @DAYSTART

", userData.CompanyID, _date.AddDays(-1), search.dEnd, search.user_id);
                }
                else
                {
                    SQL = string.Format(@"
DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:yyyyMMdd} 00:00';
DECLARE @DAYEND DATETIME = '{2:yyyyMMdd} 23:59:59';
DECLARE @sID INT = {3};

SELECT sID , sStudentID , sName + ' ' + sLastname 'FullName'  ,cType 'user_type' , OpeningBalance , Balance

FROM [JabjaiSchoolSingleDB].[dbo].[UserDailyBalance]

WHERE SchoolID = @SchoolID
AND sID = @sID
AND BusinessDate = @DAYSTART

", userData.CompanyID, search.dStart, search.dEnd, search.user_id);

                }

                //                SQL = string.Format(@"
                //DECLARE @SchoolID INT = {0};
                //DECLARE @DAYSTART DATETIME = '{1:yyyyMMdd} 00:00';
                //DECLARE @DAYEND DATETIME = '{2:yyyyMMdd} 23:59:59';
                //DECLARE @sID INT = {3};

                //SELECT sID , sStudentID , sName + ' ' + sLastname 'FullName'  ,cType 'user_type' , OpeningBalance , Balance

                //FROM [JabjaiSchoolSingleDB].[dbo].[UserDailyBalance]

                //WHERE SchoolID = @SchoolID
                //AND sID = @sID
                //AND BusinessDate = @DAYSTART

                //", userData.CompanyID, search.dStart, search.dEnd, search.user_id);

                var d = db.Database.SqlQuery<CurrentBalance>(SQL).FirstOrDefault();
                string last = "";
                dynamic userBalance;
                if (d != null)
                {
                    last = d.OpeningBalance + "";//d.sMessage?.Split(',').ElementAtOrDefault(1);

                    if (d.user_type == "0")//student
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

                    if (currentBalance != null)
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
                    string _format2 = $"จากวันที่ {search.dStart.ToString("dd MMM", new CultureInfo("th-TH"))} ถึง {search.dEnd.ToString("dd MMM yyyy", new CultureInfo("th-TH"))}";

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
                            txt1 = $"ชื่อ {_usr.info.name} (ครู) 	ยอดเงินคงเหลือ {_usr.info.balance}";
                            break;
                        case 2:
                            txt1 = $"ชื่อ {_usr.info.name} (นักเรียน) ยอดเงินคงเหลือ {_usr.info.balance}";
                            break;
                        default:
                            break;
                    }

                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 9], true, f_company.sCompany, 10, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 9], true, _format, 10, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 9], true, _format2, 10, ExcelHorizontalAlignment.Center);
                    // SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 5], true, search.dStart.Value.ToString(_format, new CultureInfo("th-th")), 10, ExcelHorizontalAlignment.Center);
                    //SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 4], true, DateTime.Now.ToString("พิมพ์วันที่ : dd MMMM yyyy HH.mm น.", new CultureInfo("th-th")), 10, ExcelHorizontalAlignment.Right);

                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 9], true, txt1, 10, ExcelHorizontalAlignment.Right);

                    SetTableHeader(worksheet.Cells[rowsIndex, 1], false, "ลำดับ", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 2], false, "วันที่/เวลา", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 3], false, "ประเภทการใช้", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 4], false, "ร้านค้า", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 5], false, "จำนวนเงิน", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 6], false, "ยอดคงเหลือ", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 7], false, "DeviceID", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 8], false, "QRCode/NFC", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex++, 9], false, "หมายเหตุ", ExcelHorizontalAlignment.Center);

                    decimal? _last = Decimal.Parse(_usr.last + "");
                    foreach (var r in d)
                    {
                        var _remark = "";
                        var _remark2 = "";

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

                        if (r.RemarkDate.HasValue)
                        {
                            _remark2 = "เวลาซื้อจริง วันที่ " + r.RemarkDate?.ToString("dd/MM/yyyy HH:mm");
                        }
                        var _code = "";
                        if (!string.IsNullOrEmpty(r.Barcode))
                        {
                            _code = "QRCode : " + r.Barcode;
                        }
                        else if (!string.IsNullOrEmpty(r.NFCOrEncrypt))
                        {
                            _code = "NFC : " + r.NFCOrEncrypt;
                        }


                        //if (data_Values.Barcode)
                        //{
                        //    _code = 'Barcode : ' + data_Values.Barcode;
                        //}
                        //else if (data_Values.NFCOrEncrypt)
                        //{
                        //    _code = 'NFC : ' + data_Values.NFCOrEncrypt;
                        //}


                        SetTableRows(worksheet.Cells[rowsIndex, 1], false, string.Format("{0}.", rowsData++), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 2], false, r.Date?.ToString("dd/MM/yyyy HH:mm"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 3], false, _remark, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 4], false, r.Shop, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 5], false, r.nMoney?.ToString("#,#0.##"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 6], false, _last?.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 7], false, r.DeviceID, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 8], false, _code, ExcelHorizontalAlignment.Center);

                        SetTableRows(worksheet.Cells[rowsIndex, 9], false, _remark2, ExcelHorizontalAlignment.Center);
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
                    worksheet.Column(7).Width = 20;
                    worksheet.Column(8).Width = 20;
                    worksheet.Column(9).Width = 30;
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
            public Guid card_id { get; set; }
            public int sort_type { get; set; }
            //public string type { get; set; }
            public DateTime dStart { get; set; }
            public DateTime dEnd { get; set; }
            //public int? shop_id { get; set; }
            public int? user_id { get; set; }
            public int? emp_id { get; set; }
            public string sStart { get; set; }
            public string sEnd { get; set; }
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
            public Guid? CardHistoryID { get; set; }
            public int Type { get; set; }
            public DateTime? Date { get; set; }
            public string DateTH
            {
                get
                {
                    return Date?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                }
            }
            public DateTime? RemarkDate { get; set; }
            public string RemarkDateTH
            {
                get
                {
                    return RemarkDate?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                }
            }
            public string Remark { get; set; }
            public decimal? nMoney { get; set; }
            public string tType { get; set; }
            public string Shop { get; set; }
            public string User { get; set; }
            public decimal? Insurance { get; internal set; }
            public string DeviceID { get; set; }
            public string Barcode { get; set; }
            public string NFCOrEncrypt { get; set; }
            public string ChargeID { get; set; }
            public int? TxCount { get; set; }
            public string txRemark { get; set; }
            public DateTime? txCreated { get; set; }
            public string txCreatedTH
            {
                get
                {
                    return txCreated?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                }
            }
        }

        private class CurrentBalance
        {

            public int sID { get; set; }
            public string sStudentID { get; set; }
            public string FullName { get; set; }
            public int cType { get; set; }
            public decimal? OpeningBalance { get; set; }
            public decimal? Balance { get; set; }
            public string user_type { get; set; }

            //public string sMessage { get; set; }
            //public DateTime? dSend { get; set; }
        }
    }
}