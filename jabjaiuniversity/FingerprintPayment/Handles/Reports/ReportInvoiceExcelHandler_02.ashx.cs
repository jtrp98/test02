using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using Jabjai.Common;
using System.Globalization;

namespace FingerprintPayment.Handles.Reports
{
    /// <summary>
    /// Summary description for ReportInvoiceExcelHandler
    /// </summary>
    public class ReportInvoiceExcelHandler_02 : IHttpHandler, IRequiresSessionState
    {

        int _rowStartRecord = 5;
        int _rowCounting = 1;
        int _cellCounting = 6;
        decimal _totalGrantTotal = 0;
        decimal _totalPaid = 0;
        string _rowHeaderTitle = "A1:K1";
        string _rowHeaderReportType = "A2:K2";
        string _rowHeaderExportDate = "A3:K3";
        string _rowHeaderExportDateMonth = "H3:L3";
        string schoolName = "";
        PaymentLogic paymentLogic = new PaymentLogic();

        public void ProcessRequest(HttpContext context)
        {
            var jsonString = context.Request.QueryString["filter"];
            using (ExcelPackage excel = new ExcelPackage())
            {
                using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                {
                    JWTToken token = new JWTToken();
                    var userData = new JWTToken().UserData;
                    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                    DateTime StartDate = DateTime.Today, EndDate = DateTime.Today;

                    Dictionary<string, string> filters = new Dictionary<string, string>();
                    int schoolId = userData.CompanyID;   //#HardcodeSchoolId
                    Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);
                    ReportInvoice data = JsonConvert.DeserializeObject<ReportInvoice>(jsonString, new JsonSerializerSettings
                    {
                        DateFormatString = "dd/MM/yyyy"
                    });

                    using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
                    {

                        using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                        {
                            var f_school = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == schoolId);
                            schoolName = f_school.sCompany;
                        }

                        excel.Workbook.Worksheets.Add("รายรับรายบุคคล");
                        //excel.Workbook.Worksheets.Add("ตามประเภทบัญชี");
                        var worksheet = excel.Workbook.Worksheets["รายรับรายบุคคล"];
                        //var worksheet2 = excel.Workbook.Worksheets["ตามประเภทบัญชี"];

                        #region Render Personal Data

                        worksheet.Cells[_rowHeaderTitle].Merge = true;
                        worksheet.Cells[_rowHeaderTitle].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[_rowHeaderTitle].Value = $"โรงเรียน {schoolName}";

                        worksheet.Cells[_rowHeaderReportType].Merge = true;
                        worksheet.Cells[_rowHeaderReportType].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[_rowHeaderReportType].Value = "รายงานประจำวันที่ " + SetTitle(data);

                        if (data.ReportType == (int)EnumReportType.Day)
                        {
                            worksheet.Cells[_rowHeaderExportDate].Merge = true;
                            worksheet.Cells[_rowHeaderExportDate].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            worksheet.Cells[_rowHeaderExportDate].Value = $"พิมพ์วันที่ : {DateTime.Now.ToString("dd/MM/yyyy เวลา:HH:mm:ss")}";
                        }
                        else
                        {
                            worksheet.Cells[_rowHeaderExportDateMonth].Merge = true;
                            worksheet.Cells[_rowHeaderExportDateMonth].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            worksheet.Cells[_rowHeaderExportDateMonth].Value = $"พิมพ์วันที่ : {DateTime.Now.ToString("dd/MM/yyyy เวลา:HH:mm:ss")}";
                        }

                        List<PaymentDTO> paymentDTO = new List<PaymentDTO>();
                        string sqlCondition = "";
                        string sqlCondition2 = "";
                        string sqlQuery = "";

                        using (Jabjai.DataAccess.PeakContext peakContext = new Jabjai.DataAccess.PeakContext())
                        {
                            switch (data.ReportType)
                            {
                                case 1:
                                    if (!string.IsNullOrEmpty(data.ReportDate))
                                    {
                                        DateTime filterStartDate = DateTime.ParseExact(data.ReportDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                                        StartDate = filterStartDate;
                                        EndDate = filterStartDate;
                                        sqlCondition += string.Format(@" AND (pp.paymentDate >= '{0:yyyy-MM-dd} 00:00:00' AND pp.paymentDate <= '{0:yyyy-MM-dd} 23:59:00') ", filterStartDate);
                                    }
                                    break;
                                case 2:
                                    if (data.ReportMonth != null)
                                    {
                                        StartDate = new DateTime(data.ReportYear ?? DateTime.Today.Year, data.ReportMonth ?? DateTime.Today.Month, 1);
                                        EndDate = StartDate.AddMonths(1).AddDays(-1);// new DateTime(data.ReportYear ?? DateTime.Today.Year, data.ReportMonth ?? DateTime.Today.Month + 1, 1);
                                        sqlCondition += string.Format(@" AND (MONTH(pp.paymentDate) = {0} AND YEAR(pp.paymentDate) = {1}) ", data.ReportMonth, data.ReportYear);
                                    }
                                    break;
                                case 3:
                                    if (data.ReportYear != null)
                                    {
                                        StartDate = new DateTime((data.ReportYear.HasValue ? data.ReportYear.Value : DateTime.Today.Year), 1, 1);
                                        EndDate = StartDate.AddYears(1).AddDays(-1);
                                        sqlCondition += string.Format(@" AND YEAR(pp.paymentDate) = {0} ", data.ReportYear);
                                    }
                                    break;
                            }

                            if (!string.IsNullOrEmpty(data.PaymentMethodId))
                            {
                                sqlCondition += string.Format(@" AND p.paymentMethodId = '{0}' ", data.PaymentMethodId);
                            }

                            if (!string.IsNullOrEmpty(data.TermId))
                            {
                                sqlCondition2 += string.Format(@" AND i.nTerm = '{0}' ", data.TermId);
                            }

                            sqlQuery = string.Format(@"
SELECT A.RowNumber 'No', A.*,(
	SELECT * FROM (
		SELECT ROW_NUMBER() OVER (ORDER BY TB0.nPaymentID) AS R,
		TB0.sPayment,TB0.nPaymentID,SUM(TB0.amount) AS amount
		FROM 
		(
			SELECT A1.sPayment,ISNULL(C.amount,0)- ISNULL(C.Discount,0) AS amount,A1.nPaymentID
			FROM Product AS A1
			LEFT OUTER JOIN TInvoices_Detail AS B ON A1.nPaymentID = B.nPaymentID AND invoices_Id = A.InvoiceId AND ISNULL(B.IsDelete,0) = 0	
			LEFT OUTER JOIN Payment AS C ON B.invoicesDetail_Id = C.invoicesDetail_Id  AND paidpayment_id = A.paidpayment_id AND C.paidpayment_id = A.paidpayment_id
			WHERE school_id = {1} AND ISNULL(Del,0) = 0 
		) TB0
		GROUP BY TB0.sPayment,TB0.nPaymentID
	) T1
	FOR JSON AUTO
) AS PRODUCT_ITEM,
(
	SELECT SUM(ProductType_0) AS ProductType_0,SUM(ProductType_1) AS ProductType_1 FROM (
		SELECT A1.sPayment,
		CASE WHEN A1.ProductType = 0 THEN (C.amount - ISNULL(C.Discount,0)) ELSE 0 END ProductType_0,
		CASE WHEN A1.ProductType = 1 THEN (C.amount - ISNULL(C.Discount,0)) ELSE 0 END ProductType_1
		FROM Product AS A1
		LEFT OUTER JOIN TInvoices_Detail AS B ON A1.nPaymentID = B.nPaymentID AND invoices_Id = A.InvoiceId 	
		LEFT OUTER JOIN Payment AS C ON B.invoicesDetail_Id = C.invoicesDetail_Id  AND paidpayment_id = A.paidpayment_id AND C.paidpayment_id = A.paidpayment_id
		WHERE school_id = {1} AND ISNULL(Del,0) = 0 	
	) T1
	FOR JSON AUTO
) AS PRODUCT_GROUP
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY CreatedDate) AS RowNumber, T.*
    FROM (
		SELECT p.paymentDate 'PaymentDate', ISNULL(p.ReceiptNo, i.code) 'InvoiceNumber', i.StudentCode, i.StudentName, i.SubLevel 'StudentSubLevel', i.SubLevel2 'StudentSubLevel2', i.Term 'StudentTerm', i.TermYear 'StudentTermYear', i.GrandTotal, p.Amount, p.Payee, ISNULL(BK.BankName + ' - ','') + pm.PaymentMethodName 'PaymentMethod',P.paidpayment_id,P.InvoiceId,p.CreatedDate--,FORMAT(p.CreatedDate,'dd/MM/yyyy HH:mm:ss') AS CreatedDate
		FROM
		(
			SELECT pp.paidpayment_id, pp.paymentDate, pp.InvoiceId, pp.Payee, pp.ReceiptNo, p.paymentMethodId, SUM(p.amount) - ISNULL(paid_Discount,0) 'Amount',pp.CreatedDate
			FROM Payment p LEFT JOIN Paid_Payment pp ON p.paidpayment_id = pp.paidpayment_id
            INNER JOIN TInvoices_Detail AS B ON B.invoicesDetail_Id = P.invoicesDetail_Id
			INNER JOIN Product AS PR ON PR.nPaymentID = B.nPaymentID AND ISNULL(PR.Del,0) = 0
			WHERE pp.[status] IS NULL {0} AND B.IsDelete = 0
			GROUP BY pp.paidpayment_id, pp.paymentDate, pp.InvoiceId, pp.Payee, pp.ReceiptNo, p.paymentMethodId,pp.paid_Discount,pp.CreatedDate
		) p 
        LEFT JOIN TInvoices i ON p.InvoiceId = i.invoices_Id
        LEFT JOIN PaymentMethod pm ON p.paymentMethodId = pm.PaymentMethodId
		LEFT JOIN Bank AS BK ON BK.BankId = pm.BankId
		WHERE i.school_id = {1} {2} AND ISNULL(i.isDel,0) = 0
    ) AS T
) AS A
ORDER BY A.RowNumber
", sqlCondition, schoolId, sqlCondition2);

                            List<PaymentDTO> listPayment = peakContext.Database.SqlQuery<PaymentDTO>(sqlQuery).ToList();

                            // Prepare data
                            foreach (var l in listPayment.OrderBy(o => o.CreatedDate))
                            {
                                l.PaymentDateString = l.PaymentDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                            }

                            paymentDTO = listPayment;
                        }

                        var headerString = paymentDTO.FirstOrDefault().PRODUCT_ITEM;
                        //List<FOOTER> Header = JsonConvert.DeserializeObject<List<FOOTER>>(headerString);
                        List<FOOTER> Header = new List<FOOTER>();

                        if (data.ItemType == "1")
                        {
                            Header.Add(new FOOTER { sPayment = "ค่าธรรมเนียมเรียน", nPaymentID = 0 });
                            Header.Add(new FOOTER { sPayment = "ค่าธรรมเนียมอื่นๆ", nPaymentID = 0 });
                        }
                        else
                        {
                            Header = (from a in peakengine.Products.Where(w => w.school_id == userData.CompanyID)
                                      where (a.Del ?? false) == false
                                      orderby a.nPaymentID
                                      select new FOOTER
                                      {
                                          sPayment = a.sPayment,
                                          nPaymentID = a.nPaymentID
                                      }).ToList();
                        }

                        TB_FOOTER b_FOOTER = new TB_FOOTER();
                        StudentLogic logic = new StudentLogic(entities);

                        var Trem = logic.GetTermDATA(StartDate, new JWTToken.userData { CompanyID = schoolId });
                        var f_year = entities.TYears.FirstOrDefault(f => f.nYear == Trem.nYear);
                        var q_term = entities.TTerms.Where(w => w.nYear == Trem.nYear && w.cDel != "1").ToList();

                        //b_FOOTER.footer_0 = GetFOOTER(peakengine, string.Format("{0:yyyy-MM-dd}", Trem.dStart), string.Format("{0:yyyy-MM-dd}", Trem.dEnd), schoolId, sqlCondition);
                        //b_FOOTER.wording_0 = "รวมเงินภาคเรียนที่ " + Trem.sTerm;
                        //b_FOOTER.footer_SUM0 = b_FOOTER.footer_0.Where(w => w.nPaymentID > 0).Sum(s => s.amount);

                        //b_FOOTER.footer_1 = GetFOOTER(peakengine, string.Format("{0:yyyy-MM-dd}", q_term.Min(m => m.dStart)), string.Format("{0:yyyy-MM-dd}", q_term.Max(m => m.dEnd)), schoolId, sqlCondition);
                        //b_FOOTER.wording_1 = "รวมเงินปีการศึกษา " + f_year.numberYear;
                        //b_FOOTER.footer_SUM1 = b_FOOTER.footer_1.Where(w => w.nPaymentID > 0).Sum(s => s.amount);

                        b_FOOTER.footer_2 = GetFOOTER(peakengine, string.Format("{0:yyyy-MM-dd}", StartDate), string.Format("{0:yyyy-MM-dd 23:59:59}", EndDate), schoolId, sqlCondition, sqlCondition2);
                        b_FOOTER.wording_2 = "รวมเงินทั้งหมด";
                        b_FOOTER.footer_SUM2 = b_FOOTER.footer_2.Where(w => w.nPaymentID > 0).Sum(s => s.amount);

                        if (data.ReportType == (int)EnumReportType.Day)
                        {

                            SetHeader(ref worksheet, Header);

                            foreach (PaymentDTO payment in paymentDTO)
                            {
                                SetRecord(ref worksheet, payment, data.ItemType, Header);
                            }

                            //SetFooter(ref worksheet, b_FOOTER.footer_0, b_FOOTER.wording_0, b_FOOTER.footer_SUM0, data.ItemType);
                            //SetFooter(ref worksheet, b_FOOTER.footer_1, b_FOOTER.wording_1, b_FOOTER.footer_SUM1, data.ItemType);
                            SetFooter(ref worksheet, b_FOOTER.footer_2, b_FOOTER.wording_2, b_FOOTER.footer_SUM2, data.ItemType);
                        }
                        else
                        {
                            SetHeader(ref worksheet, Header);

                            foreach (PaymentDTO payment in paymentDTO)
                            {
                                SetRecord(ref worksheet, payment, data.ItemType, Header);
                            }

                            //SetFooter(ref worksheet, b_FOOTER.footer_0, b_FOOTER.wording_0, b_FOOTER.footer_SUM0, data.ItemType);
                            //SetFooter(ref worksheet, b_FOOTER.footer_1, b_FOOTER.wording_1, b_FOOTER.footer_SUM1, data.ItemType);
                            SetFooter(ref worksheet, b_FOOTER.footer_2, b_FOOTER.wording_2, b_FOOTER.footer_SUM2, data.ItemType);
                        }
                        #endregion

                        #region Render Accounting Data

                        //worksheet2.Cells[_rowHeaderTitle].Merge = true;
                        //worksheet2.Cells[_rowHeaderTitle].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //worksheet2.Cells[_rowHeaderTitle].Value = $"โรงเรียน {schoolName}";
                        //worksheet2.Cells[_rowHeaderReportType].Merge = true;
                        //worksheet2.Cells[_rowHeaderReportType].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //worksheet2.Cells[_rowHeaderReportType].Value = "รายงานตามประเภทบัญชีประจำ" + SetTitle(data);
                        //worksheet2.Cells[_rowHeaderExportDate].Merge = true;
                        //worksheet2.Cells[_rowHeaderExportDate].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        //worksheet2.Cells[_rowHeaderExportDate].Value = $"พิมพ์วันที่ : {DateTime.Now.ToString("dd/MM/yyyy เวลา:HH:mm:ss")}";
                        //_rowStartRecord = 5;
                        //_rowCounting = 1;
                        //_cellCounting = 6;
                        //_totalGrantTotal = 0;
                        //_totalPaid = 0;
                        //SetAccountingHeader(ref worksheet2);

                        //foreach (PaymentDTO payment in paymentDTO)
                        //{
                        //    SetAccountingRecord(ref worksheet2, payment);
                        //}
                        //SetAccountingFooter(ref worksheet2);
                        #endregion

                        worksheet.Cells.AutoFitColumns();
                        //worksheet2.Cells.AutoFitColumns();
                        context.Response.Clear();
                        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        context.Response.AddHeader(
                                  "content-disposition",
                                  string.Format("attachment;  filename={0}", "รายงานประจำ" + SetTitle(data) + ".xlsx"));
                        context.Response.BinaryWrite(excel.GetAsByteArray());
                    }
                }
            }
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private List<FOOTER> GetFOOTER(PeakengineEntities peakContext, string filterStartDate, string filterEndtDate, int schoolId, string sqlCondition, string sqlCondition2)
        {
            string sqlQuery = @"

DECLARE @school_id INT = {0}
DECLARE @paymentDate_START DATETIME =  '{1:yyyy-MM-dd}' 
DECLARE @paymentDate_END DATETIME = '{2:yyyy-MM-dd}'

SELECT A1.sPayment,SUM(ISNULL(C.Amount,0) - ISNULL(C.Discount,0)) AS amount,A1.nPaymentID,A1.ProductType,COUNT(*)
FROM Product AS A1
LEFT OUTER JOIN
(
	SELECT P.Amount,nPaymentID,P.Discount  
	FROM TInvoices_Detail AS B 
	LEFT OUTER JOIN TInvoices AS i ON B.invoices_Id = i.invoices_Id
	INNER JOIN Paid_Payment AS PP ON PP.InvoiceId = i.invoices_Id AND PP.paymentDate BETWEEN @paymentDate_START AND @paymentDate_END 
	INNER JOIN Payment AS P ON PP.paidpayment_id = P.paidpayment_id AND B.invoicesDetail_Id = P.invoicesDetail_Id
	WHERE PP.school_id = @school_id AND B.IsDelete = 0 AND ISNULL(P.isDel,0) = 0 AND pp.[status] IS NULL {3} {4} AND ISNULL(i.isDel,0) = 0
) AS C ON C.nPaymentID = A1.nPaymentID
WHERE A1.school_id = @school_id AND ISNULL(A1.Del,0) = 0 
GROUP BY A1.sPayment,A1.nPaymentID,A1.ProductType
ORDER BY A1.nPaymentID

";
            List<Q_FOOTER> q_FOOTERs = peakContext.Database.SqlQuery<Q_FOOTER>(string.Format(sqlQuery, schoolId, filterStartDate, filterEndtDate, sqlCondition, sqlCondition2)).ToList();

            List<FOOTER> FOOTER = (from a in q_FOOTERs
                                   select new FOOTER
                                   {
                                       nPaymentID = a.nPaymentID,
                                       amount = a.amount,
                                       sPayment = a.sPayment
                                   }).ToList();

            FOOTER.Add(new Reports.FOOTER
            {
                nPaymentID = 0,
                amount = q_FOOTERs.Where(w => w.ProductType == 0).Sum(s => s.amount),
                sPayment = "ProductType_0"
            });

            FOOTER.Add(new Reports.FOOTER
            {
                nPaymentID = 0,
                amount = q_FOOTERs.Where(w => w.ProductType == 1).Sum(s => s.amount),
                sPayment = "ProductType_1"
            });

            return FOOTER;
        }


        private string SetTitle(ReportInvoice data)
        {

            switch (data.ReportType)
            {
                case (int)EnumReportType.Day:
                    return "วันที่ " + DateTime.ParseExact(data.ReportDate + " 00:00", "dd/MM/yyyy HH:mm", new CultureInfo("en-US")).ToString("dd/MM/yyyy");
                case (int)EnumReportType.Month:
                    return "เดือน " + GetMonthName(data.ReportMonth.Value) + " ปี " + (data.ReportYear.Value + 543);
                case (int)EnumReportType.Year:
                    return "ปี " + (data.ReportYear.Value + 543);
            }
            return "";
        }

        private string GetMonthName(int month)
        {
            switch (month)
            {
                case 1: return "มกราคม";
                case 2: return "กุมภาพันธ์";
                case 3: return "มีนาคม";
                case 4: return "เมษายน";
                case 5: return "พฤษภาคม";
                case 6: return "มิถุนายน";
                case 7: return "กรกฏาคม";
                case 8: return "สิงหาคม";
                case 9: return "กันยายน";
                case 10: return "ตุลาคม";
                case 11: return "พฤศจิกายน";
                default: return "ธันวาคม";
            }
        }

        #region Set Personal Don't Show Day
        private void SetHeader(ref ExcelWorksheet sheet, List<FOOTER> Header)
        {
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Font.Bold = true;
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            int _col = 1;
            sheet.Cells[_rowStartRecord, _col++].Value = "ลำดับ";
            sheet.Cells[_rowStartRecord, _col++].Value = "วันที่";
            sheet.Cells[_rowStartRecord, _col++].Value = "เลขที่ใบเสร็จ";
            sheet.Cells[_rowStartRecord, _col++].Value = "รหัสนักเรียน";
            sheet.Cells[_rowStartRecord, _col++].Value = "ชื่อ-สกุล";
            sheet.Cells[_rowStartRecord, _col++].Value = "ชั้นเรียน";
            sheet.Cells[_rowStartRecord, _col++].Value = "ภาคเรียน";
            //sheet.Cells[$"G{_rowStartRecord}"].Value = "ยอดเต็ม";
            sheet.Cells[_rowStartRecord, _col++].Value = "ยอดรับชำระ";
            foreach (var item in Header.OrderBy(o => o.nPaymentID))
            {
                sheet.Cells[_rowStartRecord, _col].Style.Font.Bold = true;
                sheet.Cells[_rowStartRecord, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                sheet.Cells[_rowStartRecord, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells[_rowStartRecord, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells[_rowStartRecord, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[_rowStartRecord, _col++].Value = item.sPayment;
            }

            //sheet.Cells[_rowStartRecord, _col++, _rowStartRecord, _col++].Merge = true;
            sheet.Cells[_rowStartRecord, _col].Style.Font.Bold = true;
            sheet.Cells[_rowStartRecord, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[_rowStartRecord, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_rowStartRecord, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_rowStartRecord, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[_rowStartRecord, _col++].Value = "รับชำระโดย";

            sheet.Cells[_rowStartRecord, _col].Style.Font.Bold = true;
            sheet.Cells[_rowStartRecord, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[_rowStartRecord, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_rowStartRecord, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_rowStartRecord, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[_rowStartRecord, _col++].Value = "ช่องทางการชำระ";

            sheet.Cells[_rowStartRecord, _col].Style.Font.Bold = true;
            sheet.Cells[_rowStartRecord, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[_rowStartRecord, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_rowStartRecord, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_rowStartRecord, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[_rowStartRecord, _col++].Value = "วันที่ทำรายการ";
        }

        private void SetRecord(ref ExcelWorksheet sheet, PaymentDTO data, string ItemType, List<FOOTER> HEADER_ITEM)
        {
            sheet.Cells[$"A{_cellCounting}:G{_cellCounting}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_cellCounting}:G{_cellCounting}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:G{_cellCounting}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}"].Value = _rowCounting;
            sheet.Cells[$"A{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            sheet.Cells[$"B{_cellCounting}"].Value = data.PaymentDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th"));
            sheet.Cells[$"B{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            sheet.Cells[$"C{_cellCounting}"].Value = data.InvoiceNumber;
            sheet.Cells[$"C{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            sheet.Cells[$"D{_cellCounting}"].Value = data.StudentCode;
            sheet.Cells[$"D{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            sheet.Cells[$"E{_cellCounting}"].Value = data.StudentName;
            sheet.Cells[$"E{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            sheet.Cells[$"F{_cellCounting}"].Value = data.StudentSubLevel + "/" + data.StudentSubLevel2;
            sheet.Cells[$"F{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            sheet.Cells[$"G{_cellCounting}"].Value = data.StudentTerm + "/" + data.StudentTermYear;
            sheet.Cells[$"G{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            sheet.Cells[$"H{_cellCounting}"].Value = data.Amount.Value;//.ToString("N2");
            sheet.Cells[$"H{_cellCounting}"].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells[$"H{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            int _col = 9;
            if (ItemType == "1")
            {
                var _GROUP = JsonConvert.DeserializeObject<List<TB_GROUP>>(data.PRODUCT_GROUP).FirstOrDefault();

                sheet.Cells[_cellCounting, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                sheet.Cells[_cellCounting, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells[_cellCounting, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells[_cellCounting, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sheet.Cells[_cellCounting, _col].Value = _GROUP.ProductType_0;//.ToString("N2");
                sheet.Cells[_cellCounting, _col++].Style.Numberformat.Format = "#,##0.00";

                sheet.Cells[_cellCounting, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                sheet.Cells[_cellCounting, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells[_cellCounting, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells[_cellCounting, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sheet.Cells[_cellCounting, _col].Value = _GROUP.ProductType_1;//.ToString("N2");
                sheet.Cells[_cellCounting, _col++].Style.Numberformat.Format = "#,##0.00";
            }
            else
            {
                List<FOOTER> PRODUCT_ITEM = JsonConvert.DeserializeObject<List<FOOTER>>(data.PRODUCT_ITEM);
                foreach (var dataItem in (from a in PRODUCT_ITEM
                                          join b in HEADER_ITEM on a.nPaymentID equals b.nPaymentID
                                          orderby b.nPaymentID
                                          select a))
                {
                    sheet.Cells[_cellCounting, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    sheet.Cells[_cellCounting, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[_cellCounting, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[_cellCounting, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    sheet.Cells[_cellCounting, _col].Value = dataItem.amount;//.ToString("N2");
                    // Set the cell's style to currency
                    sheet.Cells[_cellCounting, _col++].Style.Numberformat.Format = "#,##0.00";
                }
            }


            sheet.Cells[_cellCounting, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[_cellCounting, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_cellCounting, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_cellCounting, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            sheet.Cells[_cellCounting, _col++].Value = data.Payee;

            //sheet.Cells[$"H{_cellCounting}:J{_cellCounting}"].Merge = true;
            sheet.Cells[_cellCounting, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[_cellCounting, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_cellCounting, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_cellCounting, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            sheet.Cells[_cellCounting, _col++].Value = data.PaymentMethod;

            sheet.Cells[_cellCounting, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[_cellCounting, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_cellCounting, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_cellCounting, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            sheet.Cells[_cellCounting, _col].Value = data.CreatedDate.Value.ToString("dd MMM yyyy HH:mm:ss น.", new CultureInfo("th-th"));

            _totalGrantTotal += data.GrandTotal;
            _totalPaid += data.Amount.Value;
            _cellCounting++;
            _rowCounting++;
        }

        private void SetFooter(ref ExcelWorksheet sheet)
        {
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Merge = true;
            sheet.Cells[$"F{_cellCounting}"].Style.Font.Bold = true;
            sheet.Cells[$"F{_cellCounting}"].Value = "รวม";
            //sheet.Cells[$"G{_cellCounting}"].Value = _totalGrantTotal.ToString("N2");
            sheet.Cells[$"G{_cellCounting}"].Value = _totalPaid.ToString("N2");
            sheet.Cells[$"H{_cellCounting}:J{_cellCounting}"].Merge = true;
        }

        private void SetFooter(ref ExcelWorksheet sheet, List<FOOTER> _FOOTER, string wording, decimal? footer_SUM, string ItemType)
        {
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Merge = true;
            sheet.Cells[$"F{_cellCounting}"].Style.Font.Bold = true;
            sheet.Cells[$"G{_cellCounting}"].Value = wording;
            //sheet.Cells[$"G{_cellCounting}"].Value = _totalGrantTotal.ToString("N2");
            sheet.Cells[$"H{_cellCounting}"].Value = footer_SUM.Value;//.ToString("N2");
            sheet.Cells[$"H{_cellCounting}"].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells[$"H{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            int _col = 9;
            if (ItemType == "1")
            {
                var f1 = _FOOTER.FirstOrDefault(f => f.sPayment == "ProductType_0");
                var f2 = _FOOTER.FirstOrDefault(f => f.sPayment == "ProductType_1");

                sheet.Cells[_cellCounting, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                sheet.Cells[_cellCounting, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells[_cellCounting, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells[_cellCounting, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sheet.Cells[_cellCounting, _col].Value = f1.amount;//.ToString("N2");
                sheet.Cells[_cellCounting, _col++].Style.Numberformat.Format = "#,##0.00";

                sheet.Cells[_cellCounting, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                sheet.Cells[_cellCounting, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells[_cellCounting, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells[_cellCounting, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sheet.Cells[_cellCounting, _col].Value = f2.amount;//.ToString("N2");
                sheet.Cells[_cellCounting, _col++].Style.Numberformat.Format = "#,##0.00";

            }
            else
            {
                foreach (var _item in _FOOTER.Where(w => w.nPaymentID > 0).OrderBy(o => o.nPaymentID))
                {
                    sheet.Cells[_cellCounting, _col].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    sheet.Cells[_cellCounting, _col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[_cellCounting, _col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[_cellCounting, _col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    sheet.Cells[_cellCounting, _col].Value = _item.amount;//.ToString("N2");
                    sheet.Cells[_cellCounting, _col++].Style.Numberformat.Format = "#,##0.00";
                }
            }

            sheet.Cells[_cellCounting, _col, _cellCounting, _col + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[_cellCounting, _col, _cellCounting, _col + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[_cellCounting, _col, _cellCounting, _col + 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_cellCounting, _col, _cellCounting, _col + 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[_cellCounting, _col, _cellCounting, _col + 2].Merge = true;
            _cellCounting++;
        }
        #endregion

        #region Set Personal Show Day

        private void SetHeaderShowDay(ref ExcelWorksheet sheet, List<FOOTER> Header)
        {
            sheet.Cells[$"A{_rowStartRecord}:L{_rowStartRecord}"].Style.Font.Bold = true;
            sheet.Cells[$"A{_rowStartRecord}:L{_rowStartRecord}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_rowStartRecord}:L{_rowStartRecord}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{_rowStartRecord}:L{_rowStartRecord}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_rowStartRecord}:L{_rowStartRecord}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_rowStartRecord}"].Value = "ลำดับ";
            sheet.Cells[$"B{_rowStartRecord}"].Value = "วันที่";
            sheet.Cells[$"C{_rowStartRecord}"].Value = "เลขที่ใบเสร็จ";
            sheet.Cells[$"D{_rowStartRecord}"].Value = "รหัสนักเรียน";
            sheet.Cells[$"E{_rowStartRecord}"].Value = "ชื่อ-สกุล";
            sheet.Cells[$"F{_rowStartRecord}"].Value = "ชั้นเรียน";
            sheet.Cells[$"G{_rowStartRecord}"].Value = "ภาคเรียน";
            //sheet.Cells[$"H{_rowStartRecord}"].Value = "ยอดเต็ม";
            sheet.Cells[$"H{_rowStartRecord}"].Value = "ยอดรับชำระ";
            sheet.Cells[$"I{_rowStartRecord}:K{_rowStartRecord}"].Merge = true;
            sheet.Cells[$"I{_rowStartRecord}:K{_rowStartRecord}"].Value = "รับชำระโดย";
            sheet.Cells[$"L{_rowStartRecord}"].Value = "ช่องทางการชำระ";
            sheet.Cells[$"L{_rowStartRecord}"].Value = "วันที่ทำรายการ";
        }

        private void SetRecordShowDay(ref ExcelWorksheet sheet, PaymentDTO data)
        {
            sheet.Cells[$"A{_cellCounting}:L{_cellCounting}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_cellCounting}:L{_cellCounting}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:L{_cellCounting}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}"].Value = _rowCounting;
            sheet.Cells[$"B{_cellCounting}"].Value = data.PaymentDate.HasValue ? data.PaymentDate.Value.ToString("dd/MM/yyyy") : "";
            sheet.Cells[$"C{_cellCounting}"].Value = data.InvoiceNumber;
            sheet.Cells[$"D{_cellCounting}"].Value = data.StudentCode;
            sheet.Cells[$"E{_cellCounting}"].Value = data.StudentName;
            sheet.Cells[$"F{_cellCounting}"].Value = data.StudentSubLevel + "/" + data.StudentSubLevel2;
            sheet.Cells[$"G{_cellCounting}"].Value = data.StudentTerm + "/" + data.StudentTermYear;
            //sheet.Cells[$"H{_cellCounting}"].Value = data.GrandTotal.ToString("N2");
            sheet.Cells[$"H{_cellCounting}"].Value = data.Amount.Value.ToString("N2");
            sheet.Cells[$"I{_cellCounting}:K{_cellCounting}"].Merge = true;
            sheet.Cells[$"I{_cellCounting}:K{_cellCounting}"].Value = data.Payee;
            sheet.Cells[$"L{_cellCounting}"].Value = data.PaymentMethod;
            _totalGrantTotal += data.GrandTotal;
            _totalPaid += data.Amount.Value;
            _cellCounting++;
            _rowCounting++;
        }

        private void SetFooterShowDay(ref ExcelWorksheet sheet)
        {
            sheet.Cells[$"A{_cellCounting}:L{_cellCounting}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_cellCounting}:L{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{_cellCounting}:L{_cellCounting}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:L{_cellCounting}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Merge = true;
            //sheet.Cells[$"H{_cellCounting}"].Value = _totalGrantTotal.ToString("N2");
            sheet.Cells[$"H{_cellCounting}"].Value = _totalPaid.ToString("N2");
            sheet.Cells[$"F{_cellCounting}"].Style.Font.Bold = true;
            sheet.Cells[$"G{_cellCounting}"].Value = "รวม";
            sheet.Cells[$"I{_cellCounting}:K{_cellCounting}"].Merge = true;
        }
        #endregion  


        #region Set Accounting Don't Show Day
        private void SetAccountingHeader(ref ExcelWorksheet sheet)
        {
            sheet.Cells[$"A{_rowStartRecord}:E{_rowStartRecord}"].Style.Font.Bold = true;
            sheet.Cells[$"A{_rowStartRecord}:E{_rowStartRecord}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_rowStartRecord}:E{_rowStartRecord}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_rowStartRecord}:E{_rowStartRecord}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_rowStartRecord}:E{_rowStartRecord}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{_rowStartRecord}"].Value = "ลำดับ";
            sheet.Cells[$"B{_rowStartRecord}"].Value = "รหัสบัญชี";
            sheet.Cells[$"C{_rowStartRecord}"].Value = "ประเภทบัญชี";
            sheet.Cells[$"D{_rowStartRecord}"].Value = "รายการ";
            sheet.Cells[$"E{_rowStartRecord}"].Value = "จำนวนเงิน";
        }

        private void SetAccountingRecord(ref ExcelWorksheet sheet, PaymentDTO data)
        {
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}"].Value = _rowCounting;
            sheet.Cells[$"B{_cellCounting}"].Value = data.AccountingCode;
            sheet.Cells[$"C{_cellCounting}"].Value = data.AccountingName;
            sheet.Cells[$"D{_cellCounting}"].Value = data.ProductName;
            sheet.Cells[$"E{_cellCounting}"].Value = data.Amount.Value.ToString("N2");
            _totalGrantTotal += data.GrandTotal;
            _totalPaid += data.Amount.Value;
            _cellCounting++;
            _rowCounting++;
        }

        private void SetAccountingFooter(ref ExcelWorksheet sheet)
        {
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:C{_cellCounting}"].Merge = true;
            sheet.Cells[$"E{_cellCounting}"].Value = _totalPaid.ToString("N2");
            sheet.Cells[$"D{_cellCounting}"].Style.Font.Bold = true;
            sheet.Cells[$"D{_cellCounting}"].Value = "รวม";
        }
        #endregion
        /*
        #region Set Accounting Show Day

        private void SetAccountingHeaderShowDay(ref ExcelWorksheet sheet)
        {
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Font.Bold = true;
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_rowStartRecord}"].Value = "ลำดับ";
            sheet.Cells[$"B{_rowStartRecord}"].Value = "วันที่";
            sheet.Cells[$"C{_rowStartRecord}"].Value = "เลขที่ใบเสร็จ";
            sheet.Cells[$"D{_rowStartRecord}"].Value = "รหัสนักเรียน";
            sheet.Cells[$"E{_rowStartRecord}"].Value = "ชื่อ-สกุล";
            sheet.Cells[$"F{_rowStartRecord}"].Value = "ชั้นเรียน";
            sheet.Cells[$"G{_rowStartRecord}"].Value = "ภาคเรียน";
            sheet.Cells[$"H{_rowStartRecord}"].Value = "ยอดเต็ม";
            sheet.Cells[$"I{_rowStartRecord}"].Value = "ยอดรับชำระ";
            sheet.Cells[$"J{_rowStartRecord}:K{_rowStartRecord}"].Merge = true;
            sheet.Cells[$"J{_rowStartRecord}:K{_rowStartRecord}"].Value = "รับชำระโดย";
        }

        private void SetAccountingRecordShowDay(ref ExcelWorksheet sheet, PaymentDTO data)
        {
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}"].Value = _rowCounting;
            sheet.Cells[$"B{_cellCounting}"].Value = data.PaymentDate.HasValue ? data.PaymentDate.Value.ToString("dd/MM/yyyy") : "";
            sheet.Cells[$"C{_cellCounting}"].Value = data.InvoiceNumber;
            sheet.Cells[$"D{_cellCounting}"].Value = data.StudentCode;
            sheet.Cells[$"E{_cellCounting}"].Value = data.StudentName;
            sheet.Cells[$"F{_cellCounting}"].Value = data.StudentSubLevel + "/" + data.StudentSubLevel2;
            sheet.Cells[$"G{_cellCounting}"].Value = data.StudentTerm + "/" + data.StudentTermYear;
            sheet.Cells[$"H{_cellCounting}"].Value = data.GrandTotal.ToString("N2");
            sheet.Cells[$"I{_cellCounting}"].Value = data.Amount.Value.ToString("N2");
            sheet.Cells[$"J{_cellCounting}:K{_cellCounting}"].Merge = true;
            sheet.Cells[$"J{_cellCounting}:K{_cellCounting}"].Value = data.Payee;
            _totalGrantTotal += data.GrandTotal;
            _totalPaid += data.Amount.Value;
            _cellCounting++;
            _rowCounting++;
        }

        private void SetAccountingFooterShowDay(ref ExcelWorksheet sheet)
        {
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:E{_cellCounting}"].Merge = true;
            sheet.Cells[$"F{_cellCounting}"].Style.Font.Bold = true;
            sheet.Cells[$"G{_cellCounting}"].Value = "รวม";
            sheet.Cells[$"J{_cellCounting}:K{_cellCounting}"].Merge = true;
        }
        #endregion  */

        public class TB_GROUP
        {
            public decimal ProductType_0 { get; set; }
            public decimal ProductType_1 { get; set; }
        }

    }
}