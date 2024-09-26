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
    public partial class Sales_group : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static List<TM_Sales_Group> reports_detail(Search search)
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
                var oldDate = new DateTime(DateTime.Now.Year, 05, 01);
                List<TM_Sales_Group> q = new List<TM_Sales_Group>();
               
                SQL = ($@"
SELECT *
FROM (
	SELECT T.SchoolID,T.shop_id,T.shop_name 
	, ISNULL(SUM(T.nMoney1),0)  'SUM_MONEY1' 
	, ISNULL( SUM(T.nMoney2) , 0) 'SUM_MONEY2'

	FROM (
		SELECT A.SchoolID,A.shop_id,A.shop_name --,ISNULL(SUM(B.nTotal), 0) AS SUM_MONEY
		,CASE WHEN ISNULL(PaymentType,0) NOT IN (3) THEN 
			ISNULL((C.nPrice * C.nNumber) , B.nTotal) 
			- case WHEN B.dayCancal  BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN ISNULL((C.nPrice * C.nNumber) , B.nTotal)   ELSE 0 END 
			- case WHEN ISNULL(B.UpdatedTime,B.dSell) BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN 0 ELSE ISNULL((C.nPrice * C.nNumber) , B.nTotal)   END 	
		END 'nMoney1' 
		,CASE WHEN PaymentType IN (3) THEN  
			ISNULL((C.nPrice * C.nNumber) , B.nTotal) 
			- case WHEN B.dayCancal  BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN ISNULL((C.nPrice * C.nNumber) , B.nTotal)   ELSE 0 END 
			- case WHEN ISNULL(B.UpdatedTime,B.dSell) BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN 0 ELSE ISNULL((C.nPrice * C.nNumber) , B.nTotal)   END 	
		END 'nMoney2' 
		, B.dayCancal , B.dSell , B.UpdatedTime
		FROM TShop AS A
		LEFT OUTER JOIN TSell AS B ON A.shop_id = B.shop_id AND A.SchoolID = B.SchoolID 
			AND (B.dayCancal BETWEEN '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59' OR B.dSell BETWEEN '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59' OR B.UpdatedTime BETWEEN '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'   )
			AND B.cDel = 0
        LEFT JOIN TSell_Detail C ON B.SchoolID = C.SchoolID AND B.sSellID = C.nSell AND  ISNULL(B.cDel,0) = 0 
		WHERE A.SchoolID = {userData.CompanyID} AND A.cDel = 0   
	)T
	GROUP BY T.SchoolID,T.shop_id,T.shop_name

	UNION 

	SELECT T.SchoolID,T.shop_id,T.shop_name 
	, ISNULL(SUM(T.nMoney1),0) 'SUM_MONEY1' 
	, ISNULL( SUM(T.nMoney2) , 0) 'SUM_MONEY2'

	FROM (
		SELECT A.SchoolID,A.shop_id,A.shop_name --,ISNULL(SUM(B.nTotal), 0) AS SUM_MONEY
		,CASE WHEN ISNULL(PaymentType,0) NOT IN (3) THEN 
			ISNULL((C.nPrice * C.nNumber) , B.nTotal) 
			- case WHEN B.dayCancal  BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN ISNULL((C.nPrice * C.nNumber) , B.nTotal)   ELSE 0 END 
			- case WHEN ISNULL(B.UpdatedTime,B.dSell) BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN 0 ELSE ISNULL((C.nPrice * C.nNumber) , B.nTotal)   END 	
		END 'nMoney1' 
		,CASE WHEN PaymentType IN (3) THEN  
			ISNULL((C.nPrice * C.nNumber) , B.nTotal)  
			- case WHEN B.dayCancal  BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN ISNULL((C.nPrice * C.nNumber) , B.nTotal)   ELSE 0 END 
			- case WHEN ISNULL(B.UpdatedTime,B.dSell) BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN 0 ELSE ISNULL((C.nPrice * C.nNumber) , B.nTotal)   END 	
		END 'nMoney2' 
		, B.dayCancal , B.dSell , B.UpdatedTime
		FROM TShop AS A
		LEFT OUTER JOIN JabjaiSchoolHistory.dbo.TSell AS B ON A.shop_id = B.shop_id AND A.SchoolID = B.SchoolID 
		  AND (B.dayCancal BETWEEN '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59' OR B.dSell BETWEEN '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59' OR B.UpdatedTime BETWEEN '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'   )
			AND B.cDel = 0
        LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail C ON B.SchoolID = C.SchoolID AND B.sSellID = C.nSell AND  ISNULL(B.cDel,0) = 0 
		WHERE A.SchoolID = {userData.CompanyID} AND A.cDel = 0  
	)T
    GROUP BY T.SchoolID,T.shop_id,T.shop_name

    {(search.dStart <= oldDate ?
    $@"
    UNION 

	SELECT T.SchoolID,T.shop_id,T.shop_name 
	, ISNULL(SUM(T.nMoney1),0) 'SUM_MONEY1' 
	, ISNULL( SUM(T.nMoney2) , 0) 'SUM_MONEY2'

	FROM (
		SELECT A.SchoolID,A.shop_id,A.shop_name --,ISNULL(SUM(B.nTotal), 0) AS SUM_MONEY
		,CASE WHEN ISNULL(PaymentType,0) NOT IN (3) THEN 
			ISNULL((C.nPrice * C.nNumber) , B.nTotal) 
			- case WHEN B.dayCancal  BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN ISNULL((C.nPrice * C.nNumber) , B.nTotal)   ELSE 0 END 
			- case WHEN ISNULL(B.UpdatedTime,B.dSell) BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN 0 ELSE ISNULL((C.nPrice * C.nNumber) , B.nTotal)   END 	
		END 'nMoney1' 
		,CASE WHEN PaymentType IN (3) THEN  
			ISNULL((C.nPrice * C.nNumber) , B.nTotal)  
			- case WHEN B.dayCancal  BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN ISNULL((C.nPrice * C.nNumber) , B.nTotal)   ELSE 0 END 
			- case WHEN ISNULL(B.UpdatedTime,B.dSell) BETWEEN  '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'  THEN 0 ELSE ISNULL((C.nPrice * C.nNumber) , B.nTotal)   END 	
		END 'nMoney2' 
		, B.dayCancal , B.dSell , B.UpdatedTime
		FROM TShop AS A
		LEFT OUTER JOIN JabjaiSchoolHistory.dbo.TSell_Backup AS B ON A.shop_id = B.shop_id AND A.SchoolID = B.SchoolID 
		  AND (B.dayCancal BETWEEN '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59' OR B.dSell BETWEEN '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59' OR B.UpdatedTime BETWEEN '{search.dStart?.ToString("yyyyMMdd")} 00:00:00' AND '{search.dEnd?.ToString("yyyyMMdd")} 23:59:59'   )
			AND B.cDel = 0
        LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail_Backup C ON B.SchoolID = C.SchoolID AND B.sSellID = C.nSell AND  ISNULL(B.cDel,0) = 0 
		WHERE A.SchoolID = {userData.CompanyID} AND A.cDel = 0  
	)T
    GROUP BY T.SchoolID,T.shop_id,T.shop_name" : "")}
)T

ORDER BY T.shop_name
");

                q.AddRange(db.Database.SqlQuery<TM_Sales_Group>(SQL).ToList());

                return (from a in q
                        group a by new { a.SchoolID, a.shop_id, a.shop_name } into gb
                        select new TM_Sales_Group
                        {
                            shop_name = gb.Key.shop_name,
                            shop_id = gb.Key.shop_id,
                            SchoolID = gb.Key.SchoolID,
                            SUM_MONEY1 = gb.Sum(s => s.SUM_MONEY1),
                            SUM_MONEY2 = gb.Sum(s => s.SUM_MONEY2)
                        }).ToList();
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void exportExcel(Search search)
        {
            var q = reports_detail(search);
          


            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                var f_company = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == userData.CompanyID);

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานการขายแบบรวมร้านค้า");
                    var worksheet = excel.Workbook.Worksheets["รายงานการขายแบบรวมร้านค้า"];
                    int rowsIndex = 1, rowsData = 1;
                    decimal SUM_MONEY1 = 0;
                    decimal SUM_MONEY2 = 0;
                    string _format = "รายงานสรุปยอดขายประจำวันที่ dd/MM/yyyy";

                    switch (search.sort_type)
                    {
                        case 1: _format = "รายงานสรุปยอดขายประจำเดือน MMM yyyy"; break;
                        case 2: _format = "รายงานสรุปยอดขายประจำปี yyyy"; break;
                        case 3:
                            if (search.dStart != search.dEnd)
                            {
                                _format = "รายงานสรุปยอดขายประจำวันที่ dd/MM/yyyy - " + search.dEnd.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th"));
                            }
                            else
                            {
                                _format = "รายงานสรุปยอดขายประจำวันที่ dd/MM/yyyy ";
                            }
                            break;

                    }


                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 4], true, f_company.sCompany, 10, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 4], true, search.dStart.Value.ToString(_format, new CultureInfo("th-th")), 10, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 4], true, DateTime.Now.ToString("พิมพ์วันที่ : dd MMMM yyyy HH.mm น.", new CultureInfo("th-th")), 10, ExcelHorizontalAlignment.Right);

                    SetTableHeader(worksheet.Cells[rowsIndex, 1, rowsIndex + 1, 1], true, "ลำดับ", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 2, rowsIndex + 1, 2], true, "ร้าน", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 3, rowsIndex, 4], true, "ยอดขาย", ExcelHorizontalAlignment.Center);
                    rowsIndex++;
                    SetTableHeader(worksheet.Cells[rowsIndex, 3], false, "ระบบภายในโรงเรียน", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex++, 4], false, "QR Code พร้อมเพย์", ExcelHorizontalAlignment.Center);

                    foreach (var data in q)
                    {
                        SetTableRows(worksheet.Cells[rowsIndex, 1], false, string.Format("{0}.", rowsData++), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 2], false, data.shop_name, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 3], false, data.SUM_MONEY1.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 4], false, data.SUM_MONEY2.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);

                        SUM_MONEY1 += data.SUM_MONEY1;
                        SUM_MONEY2 += data.SUM_MONEY2;
                        rowsIndex++;
                    }

                    SetTableRows(worksheet.Cells[rowsIndex, 1, rowsIndex, 2], true, "รวม", ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[rowsIndex, 3], true, SUM_MONEY1.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, 4], true, SUM_MONEY2.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);

                    worksheet.Column(1).Width = 8;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 25;
                    worksheet.Column(4).Width = 25;

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
            //public int? user_id { get; set; }
            public int? emp_id { get; set; }
        }

        public class TM_Sales_Group
        {
            public int SchoolID { get; set; }
            public int shop_id { get; set; }
            public string shop_name { get; set; }
            public decimal SUM_MONEY1 { get; set; }
            public decimal SUM_MONEY2 { get; set; }
        }
    }
}