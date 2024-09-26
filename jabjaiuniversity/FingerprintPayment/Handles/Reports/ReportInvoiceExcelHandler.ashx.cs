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

namespace FingerprintPayment.Handles.Reports
{
    /// <summary>
    /// Summary description for ReportInvoiceExcelHandler
    /// </summary>
    public class ReportInvoiceExcelHandler : IHttpHandler, IRequiresSessionState
    {

        int _rowStartRecord = 5;
        int _rowCounting = 1;
        int _cellCounting = 6;
        decimal _totalGrantTotal = 0;
        decimal _totalPaid = 0;
        string _rowHeaderTitle = "A1:K1";
        string _rowHeaderReportType = "A2:K2";
        string _rowHeaderExportDate = "H3:K3";
        string _rowHeaderExportDateMonth = "H3:L3";
        string schoolName = "";
        PaymentLogic paymentLogic = new PaymentLogic();

        public void ProcessRequest(HttpContext context)
        {
            var jsonString = context.Request.QueryString["filter"];
            using (ExcelPackage excel = new ExcelPackage())
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                Dictionary<string, string> filters = new Dictionary<string, string>();
                int schoolId = userData.CompanyID;   //#HardcodeSchoolId
                Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);
                ReportInvoice data = JsonConvert.DeserializeObject<ReportInvoice>(jsonString, new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy"
                });

                using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var f_school = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == schoolId);
                    schoolName = f_school.sCompany;
                }

                excel.Workbook.Worksheets.Add("รายรับรายบุคคล");
                excel.Workbook.Worksheets.Add("ตามประเภทบัญชี");
                var worksheet = excel.Workbook.Worksheets["รายรับรายบุคคล"];
                var worksheet2 = excel.Workbook.Worksheets["ตามประเภทบัญชี"];

                #region Render Personal Data

                worksheet.Cells[_rowHeaderTitle].Merge = true;
                worksheet.Cells[_rowHeaderTitle].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[_rowHeaderTitle].Value = $"โรงเรียน {schoolName}"; ;
                worksheet.Cells[_rowHeaderReportType].Merge = true;
                worksheet.Cells[_rowHeaderReportType].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[_rowHeaderReportType].Value = "รายงานประจำ" + SetTitle(data);

                if (data.ReportType == (int)EnumReportType.Day)
                {
                    worksheet.Cells[_rowHeaderExportDate].Merge = true;
                    worksheet.Cells[_rowHeaderExportDate].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    worksheet.Cells[_rowHeaderExportDate].Value = $"พิมพ์วันที่ : {DateTime.Now.ToString("dd/MM/yyyy เวลา:HH:mm:ss")}";
                }
                else
                {
                    worksheet.Cells[_rowHeaderExportDateMonth].Merge = true;
                    worksheet.Cells[_rowHeaderExportDateMonth].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    worksheet.Cells[_rowHeaderExportDateMonth].Value = $"พิมพ์วันที่ : {DateTime.Now.ToString("dd/MM/yyyy เวลา:HH:mm:ss")}";
                }

                List<PaymentDTO> paymentDTO = new List<PaymentDTO>();
                using (Jabjai.DataAccess.PeakContext peakContext = new Jabjai.DataAccess.PeakContext())
                {
                    string sqlCondition = "";
                    string sqlCondition2 = "";
                    string sqlQuery = "";

                    switch (data.ReportType)
                    {
                        case 1:
                            if (!string.IsNullOrEmpty(data.ReportDate))
                            {
                                DateTime filterStartDate = DateTime.ParseExact(data.ReportDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                                sqlCondition += string.Format(@" AND (pp.paymentDate >= '{0:yyyy-MM-dd} 00:00:00' AND pp.paymentDate <= '{0:yyyy-MM-dd} 23:59:00') ", filterStartDate);
                            }
                            break;
                        case 2:
                            if (data.ReportMonth != null)
                            {
                                sqlCondition += string.Format(@" AND (MONTH(pp.paymentDate) = {0} AND YEAR(pp.paymentDate) = {1}) ", data.ReportMonth, data.ReportYear);
                            }
                            break;
                        case 3:
                            if (data.ReportYear != null)
                            {
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
		SELECT A1.sPayment,ISNULL(C.amount,0) AS amount,A1.nPaymentID
		FROM Product AS A1
		LEFT OUTER JOIN TInvoices_Detail AS B ON A1.nPaymentID = B.nPaymentID AND invoices_Id = A.InvoiceId 	
		LEFT OUTER JOIN Payment AS C ON B.invoicesDetail_Id = C.invoicesDetail_Id  AND paidpayment_id = A.paidpayment_id
		WHERE school_id = 307 AND ISNULL(Del,0) = 0 	
	) T1
	FOR JSON AUTO
) AS PRODUCT_ITEM,
(
	SELECT SUM(ProductType_0) AS ProductType_0,SUM(ProductType_1) AS ProductType_1 FROM (
		SELECT A1.sPayment,
		CASE WHEN A1.ProductType = 0 THEN C.amount ELSE 0 END ProductType_0,
		CASE WHEN A1.ProductType = 1 THEN C.amount ELSE 0 END ProductType_1
		FROM Product AS A1
		LEFT OUTER JOIN TInvoices_Detail AS B ON A1.nPaymentID = B.nPaymentID AND invoices_Id = A.InvoiceId 	
		LEFT OUTER JOIN Payment AS C ON B.invoicesDetail_Id = C.invoicesDetail_Id  AND paidpayment_id = A.paidpayment_id
		WHERE school_id = 307 AND ISNULL(Del,0) = 0 	
	) T1
	FOR JSON AUTO
) AS PRODUCT_GROUP
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY PaymentDate) AS RowNumber, T.*
    FROM (
		SELECT p.paymentDate 'PaymentDate', ISNULL(p.ReceiptNo, i.code) 'InvoiceNumber', i.StudentCode, i.StudentName, i.SubLevel 'StudentSubLevel', i.SubLevel2 'StudentSubLevel2', i.Term 'StudentTerm', i.TermYear 'StudentTermYear', i.GrandTotal, p.Amount, p.Payee, pm.PaymentMethodName 'PaymentMethod',P.paidpayment_id,P.InvoiceId
		FROM
		(
			SELECT pp.paidpayment_id, pp.paymentDate, pp.InvoiceId, pp.Payee, pp.ReceiptNo, p.paymentMethodId, SUM(p.amount) - ISNULL(paid_Discount,0) 'Amount'
			FROM Payment p LEFT JOIN Paid_Payment pp ON p.paidpayment_id = pp.paidpayment_id
			WHERE pp.[status] IS NULL {0}
			GROUP BY pp.paidpayment_id, pp.paymentDate, pp.InvoiceId, pp.Payee, pp.ReceiptNo, p.paymentMethodId,pp.paid_Discount
		) p 
        LEFT JOIN TInvoices i ON p.InvoiceId = i.invoices_Id
        LEFT JOIN PaymentMethod pm ON p.paymentMethodId = pm.PaymentMethodId
		WHERE i.school_id = {1} {2}
    ) AS T
) AS A
", sqlCondition, schoolId, sqlCondition2);

                    List<PaymentDTO> listPayment = peakContext.Database.SqlQuery<PaymentDTO>(sqlQuery).ToList();

                    // Prepare data
                    foreach (var l in listPayment)
                    {
                        l.PaymentDateString = l.PaymentDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    }

                    paymentDTO = listPayment;
                }

                if (data.ReportType == (int)EnumReportType.Day)
                {
                    SetHeader(ref worksheet);

                    foreach (PaymentDTO payment in paymentDTO)
                    {
                        SetRecord(ref worksheet, payment);
                    }
                    SetFooter(ref worksheet);
                }
                else
                {
                    SetHeaderShowDay(ref worksheet);

                    foreach (PaymentDTO payment in paymentDTO)
                    {
                        SetRecordShowDay(ref worksheet, payment);
                    }
                    SetFooterShowDay(ref worksheet);
                }
                #endregion

                #region Render Accounting Data

                worksheet2.Cells[_rowHeaderTitle].Merge = true;
                worksheet2.Cells[_rowHeaderTitle].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet2.Cells[_rowHeaderTitle].Value = $"โรงเรียน {schoolName}";
                worksheet2.Cells[_rowHeaderReportType].Merge = true;
                worksheet2.Cells[_rowHeaderReportType].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet2.Cells[_rowHeaderReportType].Value = "รายงานตามประเภทบัญชีประจำ" + SetTitle(data);
                worksheet2.Cells[_rowHeaderExportDate].Merge = true;
                worksheet2.Cells[_rowHeaderExportDate].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet2.Cells[_rowHeaderExportDate].Value = $"พิมพ์วันที่ : {DateTime.Now.ToString("dd/MM/yyyy เวลา:HH:mm:ss")}";
                _rowStartRecord = 5;
                _rowCounting = 1;
                _cellCounting = 6;
                _totalGrantTotal = 0;
                _totalPaid = 0;
                SetAccountingHeader(ref worksheet2);

                foreach (PaymentDTO payment in paymentDTO)
                {
                    SetAccountingRecord(ref worksheet2, payment);
                }
                SetAccountingFooter(ref worksheet2);
                #endregion

                worksheet.Cells.AutoFitColumns();
                worksheet2.Cells.AutoFitColumns();
                context.Response.Clear();
                context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                context.Response.AddHeader(
                          "content-disposition",
                          string.Format("attachment;  filename={0}", "รายงานประจำ" + SetTitle(data) + ".xlsx"));
                context.Response.BinaryWrite(excel.GetAsByteArray());
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
        private void SetHeader(ref ExcelWorksheet sheet)
        {
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Font.Bold = true;
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_rowStartRecord}:K{_rowStartRecord}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[$"A{_rowStartRecord}"].Value = "ลำดับ";
            sheet.Cells[$"B{_rowStartRecord}"].Value = "เลขที่ใบเสร็จ";
            sheet.Cells[$"C{_rowStartRecord}"].Value = "รหัสนักเรียน";
            sheet.Cells[$"D{_rowStartRecord}"].Value = "ชื่อ-สกุล";
            sheet.Cells[$"E{_rowStartRecord}"].Value = "ชั้นเรียน";
            sheet.Cells[$"F{_rowStartRecord}"].Value = "ภาคเรียน";
            //sheet.Cells[$"G{_rowStartRecord}"].Value = "ยอดเต็ม";
            sheet.Cells[$"G{_rowStartRecord}"].Value = "ยอดรับชำระ";
            sheet.Cells[$"H{_rowStartRecord}:J{_rowStartRecord}"].Merge = true;
            sheet.Cells[$"H{_rowStartRecord}:J{_rowStartRecord}"].Value = "รับชำระโดย";
            sheet.Cells[$"K{_rowStartRecord}"].Value = "ช่องทางการชำระ";
        }

        private void SetRecord(ref ExcelWorksheet sheet, PaymentDTO data)
        {
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}:K{_cellCounting}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sheet.Cells[$"A{_cellCounting}"].Value = _rowCounting;
            sheet.Cells[$"B{_cellCounting}"].Value = data.InvoiceNumber;
            sheet.Cells[$"C{_cellCounting}"].Value = data.StudentCode;
            sheet.Cells[$"D{_cellCounting}"].Value = data.StudentName;
            sheet.Cells[$"E{_cellCounting}"].Value = data.StudentSubLevel + "/" + data.StudentSubLevel2;
            sheet.Cells[$"F{_cellCounting}"].Value = data.StudentTerm + "/" + data.StudentTermYear;
            //sheet.Cells[$"G{_cellCounting}"].Value = data.GrandTotal.ToString("N2");
            sheet.Cells[$"G{_cellCounting}"].Value = data.Amount.Value.ToString("N2");
            sheet.Cells[$"H{_cellCounting}:J{_cellCounting}"].Merge = true;
            sheet.Cells[$"H{_cellCounting}:J{_cellCounting}"].Value = data.Payee;
            sheet.Cells[$"K{_cellCounting}"].Value = data.PaymentMethod;
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
        #endregion

        #region Set Personal Show Day

        private void SetHeaderShowDay(ref ExcelWorksheet sheet)
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
    }
}