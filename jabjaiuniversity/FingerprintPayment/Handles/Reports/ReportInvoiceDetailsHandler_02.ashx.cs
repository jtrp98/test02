using Jabjai.BusinessLogic;
using Jabjai.Common;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Globalization;

namespace FingerprintPayment.Handles.Reports
{
    /// <summary>
    /// Summary description for ReportInvoiceDetailsHandler
    /// </summary>
    public class ReportInvoiceDetailsHandler_02 : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string jsonString = string.Empty;
            int schoolId = 3;   //#HardcodeSchoolId
            BaseResult<DTResult<PaymentDTO>> result = new BaseResult<DTResult<PaymentDTO>>();

            try
            {
                Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);

                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    jsonString = reader.ReadToEnd();
                }

                var dTParameters = JsonConvert.DeserializeObject<DTParameters>(jsonString, new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy"
                });

                using (Jabjai.DataAccess.PeakContext peakContext = new Jabjai.DataAccess.PeakContext())
                {
                    string sqlCondition = "";
                    string sqlCondition2 = "";
                    string sqlQueryCount = "";
                    string sqlQuery = "";

                    string paramReportDate = dTParameters.GetSearchValueByColumnName("ReportDate");
                    string paramReportMonth = dTParameters.GetSearchValueByColumnName("ReportMonth");
                    string paramReportYear = dTParameters.GetSearchValueByColumnName("ReportYear");

                    string paramPaymentMethodId = dTParameters.GetSearchValueByColumnName("PaymentMethodId");
                    string paramTermId = dTParameters.GetSearchValueByColumnName("TermId");

                    if (!string.IsNullOrEmpty(paramReportDate))
                    {
                        DateTime filterStartDate = DateTime.ParseExact(paramReportDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                        sqlCondition += string.Format(@" AND (pp.paymentDate >= '{0:yyyy-MM-dd} 00:00:00' AND pp.paymentDate <= '{0:yyyy-MM-dd} 23:59:00') ", filterStartDate);
                    }
                    if (!string.IsNullOrEmpty(paramReportMonth))
                    {
                        int month = Convert.ToInt32(paramReportMonth);
                        int year = Convert.ToInt32(paramReportYear);
                        sqlCondition += string.Format(@" AND (MONTH(pp.paymentDate) = {0} AND YEAR(pp.paymentDate) = {1}) ", month, year);
                    }
                    if (!string.IsNullOrEmpty(paramReportYear))
                    {
                        int year = Convert.ToInt32(paramReportYear);
                        sqlCondition += string.Format(@" AND YEAR(pp.paymentDate) = {0} ", year);
                    }

                    if (!string.IsNullOrEmpty(paramPaymentMethodId))
                    {
                        sqlCondition += string.Format(@" AND p.paymentMethodId = '{0}' ", paramPaymentMethodId);
                    }

                    if (!string.IsNullOrEmpty(paramTermId))
                    {
                        sqlCondition2 += string.Format(@" AND i.nTerm = '{0}' ", paramTermId);
                    }

                    int lowerBound = dTParameters.Start + 1;
                    int upperBound = lowerBound + dTParameters.Length;

                    string orderByColumn = dTParameters.Columns[dTParameters.Order[0].Column].Name;
                    string orderBy = "";
                    switch (orderByColumn)
                    {
                        case "PaymentDateString":
                            orderBy = "PaymentDate " + dTParameters.Order[0].Dir;
                            break;
                        default:
                            orderBy = orderByColumn + " " + dTParameters.Order[0].Dir;
                            break;
                    }

                    sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM
(
	SELECT pp.paidpayment_id, pp.paymentDate, pp.InvoiceId, pp.Payee, pp.ReceiptNo, p.paymentMethodId, SUM(p.amount) 'Amount'
	FROM Payment p LEFT JOIN Paid_Payment pp ON p.paidpayment_id = pp.paidpayment_id
	WHERE pp.[status] IS NULL {0}
	GROUP BY pp.paidpayment_id, pp.paymentDate, pp.InvoiceId, pp.Payee, pp.ReceiptNo, p.paymentMethodId
) p 
LEFT JOIN TInvoices i ON p.InvoiceId = i.invoices_Id
LEFT JOIN PaymentMethod pm ON p.paymentMethodId = pm.PaymentMethodId
WHERE i.school_id = {1} {2}", sqlCondition, schoolId, sqlCondition2);

                    sqlQuery = string.Format(@"
SELECT A.RowNumber 'No', A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT p.paymentDate 'PaymentDate', ISNULL(p.ReceiptNo, i.code) 'InvoiceNumber', i.StudentCode, i.StudentName, i.SubLevel 'StudentSubLevel', i.SubLevel2 'StudentSubLevel2', i.Term 'StudentTerm', i.TermYear 'StudentTermYear', i.GrandTotal, p.Amount, p.Payee, pm.PaymentMethodName 'PaymentMethod'
		FROM
		(
			SELECT pp.paidpayment_id, pp.paymentDate, pp.InvoiceId, pp.Payee, pp.ReceiptNo, p.paymentMethodId, SUM(p.amount) 'Amount'
			FROM Payment p LEFT JOIN Paid_Payment pp ON p.paidpayment_id = pp.paidpayment_id
			WHERE pp.[status] IS NULL {1}
			GROUP BY pp.paidpayment_id, pp.paymentDate, pp.InvoiceId, pp.Payee, pp.ReceiptNo, p.paymentMethodId
		) p 
        LEFT JOIN TInvoices i ON p.InvoiceId = i.invoices_Id
        LEFT JOIN PaymentMethod pm ON p.paymentMethodId = pm.PaymentMethodId
		WHERE i.school_id = {2} {3}
    ) AS T
) AS A
WHERE RowNumber >= {4} AND RowNumber < {5}", orderBy, sqlCondition, schoolId, sqlCondition2, lowerBound, upperBound);

                    int resultCount = peakContext.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                    List<PaymentDTO> listPayment = peakContext.Database.SqlQuery<PaymentDTO>(sqlQuery).ToList();

                    // Prepare data
                    foreach (var l in listPayment)
                    {
                        l.PaymentDateString = l.PaymentDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    }

                    result.Result = new DTResult<PaymentDTO>();
                    result.Result.data = listPayment;
                    result.Result.draw = dTParameters.Draw;
                    result.Result.recordsFiltered = resultCount;
                    result.Result.recordsTotal = resultCount;
                }
            }
            catch (Exception ex)
            {
                result.Message = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
                context.Response.StatusCode = 500;
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(result.Result));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}