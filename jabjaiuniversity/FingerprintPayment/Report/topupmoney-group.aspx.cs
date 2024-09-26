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
    public partial class topupmoney_group : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static List<TM_Topup_Group> reports_detail(Search search)
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

                //                SQL = string.Format(@"DECLARE {userData.CompanyID} INT = {0}
                //DECLARE '{search.dStart.ToString("yyyyMMdd 00:00")}' DATETIME = '{1:yyyyMMdd} 00:00:00'
                //DECLARE '{ search.dEnd?.ToString("yyyyMMdd 23:59:59") }' DATETIME = '{2:yyyyMMdd} 23:59:59'

                //SELECT A.SchoolID,A.sEmp,A.sName + ' ' + A.sLastname AS Employees_Name,ISNULL(SUM(B.nMoney),0) AS SUM_MONEY 
                //FROM TEmployees AS A 
                //LEFT OUTER JOIN TMoney AS B ON A.sEmp = B.sEmp AND A.SchoolID = B.SchoolID AND B.dMoney BETWEEN '{search.dStart.ToString("yyyyMMdd 00:00")}' AND '{ search.dEnd?.ToString("yyyyMMdd 23:59:59") }' AND B.dayCancal IS NULL AND B.cDel = 0
                //WHERE A.SchoolID = {userData.CompanyID} AND ISNULL(A.cDel,0) = 0
                //GROUP BY A.SchoolID,A.sEmp,A.SchoolID,A.sName,A.sLastname
                //HAVING ISNULL(SUM(B.nMoney),0) > 0
                //", userData.CompanyID, search.dStart, search.dEnd);
                var oldDate = new DateTime(DateTime.Now.Year, 05, 01);                      

                SQL = ($@"
SELECT * 
FROM(
	SELECT A.SchoolID,A.sEmp,A.sName + ' ' + A.sLastname AS Employees_Name , 
	ISNULL(B.nMoney,0) AS SUM_MONEY ,
	ISNULL(C.nMoney,0) AS SUM_WD ,
	CONVERT(decimal(18,2),ISNULL(B.nMoney,0.0)) - CONVERT(decimal(18,2),ISNULL( C.nMoney,0.0)) AS NET

	FROM TEmployees AS A 
	LEFT JOIN (
		SELECT  TT1.sEmp , TT1.SchoolID,  SUM(TT1.nMoney) 'nMoney'
		FROM (
			SELECT sEmp , SchoolID,  SUM(ISNULL(nMoney,0)) + SUM(ISNULL(Insurance,0)) 'nMoney'
			FROM {(search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney")} 
			WHERE SchoolID = {userData.CompanyID} AND dMoney BETWEEN '{search.dStart?.ToString("yyyyMMdd 00:00")}' AND '{ search.dEnd?.ToString("yyyyMMdd 23:59:59") }' AND dayCancal IS NULL AND cDel = 0 --and semp = 349230 AND topup_type NOT IN ('MBS1' ,'KIOSK') 
			GROUP BY sEmp , SchoolID 
			UNION 
			SELECT sEmp , SchoolID,  SUM(ISNULL(nMoney,0)) + SUM(ISNULL(Insurance,0)) 'nMoney'
			FROM JabjaiSchoolHistory.dbo.TMoney 
			WHERE SchoolID = {userData.CompanyID} AND dMoney BETWEEN '{search.dStart?.ToString("yyyyMMdd 00:00")}' AND '{ search.dEnd?.ToString("yyyyMMdd 23:59:59") }' AND dayCancal IS NULL AND cDel = 0 --and semp = 349230 AND topup_type NOT IN ('MBS1' ,'KIOSK') 
			GROUP BY sEmp , SchoolID			
		)TT1
		Group by sEmp , SchoolID
	)B ON A.SchoolID = {userData.CompanyID} AND  A.sEmp = B.sEmp  AND A.SchoolID = B.SchoolID

	LEFT JOIN (
		SELECT userAdd , SchoolID,  ISNULL(SUM(nMoney),0) 'nMoney'
		FROM TWithdrawal 
		WHERE SchoolID = {userData.CompanyID} AND dWithdrawal BETWEEN '{search.dStart?.ToString("yyyyMMdd 00:00")}' AND '{ search.dEnd?.ToString("yyyyMMdd 23:59:59") }' AND dCanCel IS NULL AND cDel = 0
		Group by userAdd , SchoolID
	)C ON A.SchoolID = {userData.CompanyID} AND  A.sEmp = C.userAdd  AND A.SchoolID = C.SchoolID
	--WHERE A.SchoolID = {userData.CompanyID} --AND ISNULL(A.cDel,0) = 0 --AND (ISNULL((B.nMoney),0) > 0 OR ISNULL((C.nMoney),0) > 0) 

	UNION

	SELECT  0 'sEmp' , 0 'SchoolID'  , TT2.Employees_Name,   SUM(TT2.SUM_MONEY) 'SUM_MONEY', SUM(TT2.SUM_WD) 'SUM_WD' , SUM(TT2.NET) 'NET'
	FROM (
		SELECT 'QR Code พร้อมเพย์' 'Employees_Name' ,  ISNULL(SUM(nMoney),0) 'SUM_MONEY' , 0 'SUM_WD' ,  ISNULL(SUM(nMoney),0) 'NET'
		FROM {(search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney")} 
		WHERE SchoolID = {userData.CompanyID} AND dMoney BETWEEN '{search.dStart?.ToString("yyyyMMdd 00:00")}' AND '{ search.dEnd?.ToString("yyyyMMdd 23:59:59") }' AND dayCancal IS NULL AND cDel = 0 AND topup_type IN ('MBS1') 
			
		UNION 

		SELECT 'QR Code พร้อมเพย์' 'Employees_Name' ,  ISNULL(SUM(nMoney),0) 'SUM_MONEY' , 0 'SUM_WD' ,  ISNULL(SUM(nMoney),0) 'NET'
		FROM JabjaiSchoolHistory.dbo.TMoney 
		WHERE SchoolID = {userData.CompanyID} AND dMoney BETWEEN '{search.dStart?.ToString("yyyyMMdd 00:00")}' AND '{ search.dEnd?.ToString("yyyyMMdd 23:59:59") }' AND dayCancal IS NULL AND cDel = 0 AND topup_type IN ('MBS1') 
			
	)TT2
	Group by TT2.Employees_Name 
		
	UNION

	SELECT  0 'sEmp' , 0 'SchoolID'  , TT3.Employees_Name,   SUM(TT3.SUM_MONEY) 'SUM_MONEY', SUM(TT3.SUM_WD) 'SUM_WD' , SUM(TT3.NET) 'NET'
	FROM (
		SELECT 'ตู้เติมเงิน' 'Employees_Name' , ISNULL(SUM(nMoney),0) 'SUM_MONEY' , 0 'SUM_WD' ,  ISNULL(SUM(nMoney),0) 'NET'
		FROM {(search.dStart <= oldDate ? "V_Money_And_Backup" : "TMoney")} 
		WHERE SchoolID = {userData.CompanyID} AND dMoney BETWEEN '{search.dStart?.ToString("yyyyMMdd 00:00")}' AND '{ search.dEnd?.ToString("yyyyMMdd 23:59:59") }' AND dayCancal IS NULL AND cDel = 0 AND topup_type IN ('KIOSK') 
			
		UNION 

		SELECT 'ตู้เติมเงิน' 'Employees_Name' , ISNULL(SUM(nMoney),0) 'SUM_MONEY' , 0 'SUM_WD' ,  ISNULL(SUM(nMoney),0) 'NET'
		FROM JabjaiSchoolHistory.dbo.TMoney 
		WHERE SchoolID = {userData.CompanyID} AND dMoney BETWEEN '{search.dStart?.ToString("yyyyMMdd 00:00")}' AND '{ search.dEnd?.ToString("yyyyMMdd 23:59:59") }' AND dayCancal IS NULL AND cDel = 0 AND topup_type IN ('KIOSK') 
			
	)TT3
	Group by TT3.Employees_Name 

)T
WHERE (T.SUM_MONEY > 0 OR T.SUM_WD > 0)  
ORDER BY T.SchoolID  desc, T.Employees_Name asc
");

                var q = db.Database.SqlQuery<TM_Topup_Group>(SQL).ToList();

                return q;
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
                    excel.Workbook.Worksheets.Add("รายงานเติมเงินแบบรวมทุกคน");
                    var worksheet = excel.Workbook.Worksheets["รายงานเติมเงินแบบรวมทุกคน"];
                    int rowsIndex = 1, rowsData = 1;
                    decimal SUM_MONEY = 0, SUM_WD = 0, NET = 0;

                    string _format = "รายงานสรุปยอดเติมเงินประจำวันที่ dd/MM/yyyy";

                    switch (search.sort_type)
                    {
                        case 1: _format = "รายงานสรุปยอดเติมเงินประจำเดือน MMM yyyy"; break;
                        case 2: _format = "รายงานสรุปยอดเติมเงินประจำปี yyyy"; break;
                        case 3:
                            if (search.dStart != search.dEnd)
                            {
                                _format = "รายงานสรุปยอดเติมเงินประจำวันที่ dd/MM/yyyy - " + search.dEnd.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th"));
                            }
                            else
                            {
                                _format = "รายงานสรุปยอดเติมเงินประจำวันที่ dd/MM/yyyy ";
                            }
                            break;
                    }


                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 5], true, f_company.sCompany, 10, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 5], true, search.dStart.Value.ToString(_format, new CultureInfo("th-th")), 10, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet.Cells[rowsIndex, 1, rowsIndex++, 5], true, DateTime.Now.ToString("พิมพ์วันที่ : dd MMMM yyyy HH.mm น.", new CultureInfo("th-th")), 10, ExcelHorizontalAlignment.Right);

                    SetTableHeader(worksheet.Cells[rowsIndex, 1], false, "ลำดับ", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 2], false, "ผู้ทำรายการ", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 3], false, "ยอดรับ", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex, 4], false, "ยอดถอน", ExcelHorizontalAlignment.Center);
                    SetTableHeader(worksheet.Cells[rowsIndex++, 5], false, "ยอดคงเหลือ", ExcelHorizontalAlignment.Center);

                    foreach (var data in q)
                    {
                        SetTableRows(worksheet.Cells[rowsIndex, 1], false, string.Format("{0}.", rowsData++), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 2], false, data.Employees_Name, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 3], false, data.SUM_MONEY.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 4], false, data.SUM_WD.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsIndex, 5], false, data.NET.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                        SUM_MONEY += data.SUM_MONEY;
                        SUM_WD += data.SUM_WD;
                        NET += data.NET;

                        rowsIndex++;
                    }

                    SetTableRows(worksheet.Cells[rowsIndex, 1, rowsIndex, 2], true, "รวม", ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[rowsIndex, 3], true, SUM_MONEY.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, 4], true, SUM_WD.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsIndex, 5], true, NET.ToString("#,#0.00"), ExcelHorizontalAlignment.Center);

                    worksheet.Column(1).Width = 8;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 20;

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

        public class TM_Topup_Group
        {
            public int SchoolID { get; set; }
            public int sEmp { get; set; }
            public string Employees_Name { get; set; }
            public decimal SUM_MONEY { get; set; }
            public decimal SUM_WD { get; set; }
            public decimal NET { get; set; }
        }
    }
}