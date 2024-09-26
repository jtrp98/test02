using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Report;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace FingerprintPayment.Handles.Reports
{
    public class ReportInvoiceHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var jsonString = context.Request.Form["filter"];
            int schoolId = userData.CompanyID;   //#HardcodeSchoolId
            BaseResult<List<ReportInvoiceOutput>> result = new BaseResult<List<ReportInvoiceOutput>>();
            result.Result = new List<ReportInvoiceOutput>();
            try
            {
                Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);

                ReportInvoice data = JsonConvert.DeserializeObject<ReportInvoice>(jsonString, new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy"
                });

                using (Jabjai.DataAccess.PeakContext peakContext = new Jabjai.DataAccess.PeakContext())
                {
                    string sqlCondition = "";
                    string unionWord = "UNION";
                    string setLeftData = "";
                    string sqlQuery = "";

                    if (!string.IsNullOrEmpty(data.PaymentMethodId))
                    {
                        sqlCondition += string.Format(@" AND pp.paidpayment_id IN (SELECT paidpayment_id FROM Payment WHERE paymentMethodId='{0}') ", data.PaymentMethodId);
                    }

                    if (!string.IsNullOrEmpty(data.TermId))
                    {
                        sqlCondition += string.Format(@" AND i.nTerm = '{0}' ", data.TermId);
                    }

                    if (data.ReportType == (int)EnumReportType.Day)
                    {
                        DateTime startDate = DateTime.ParseExact(data.ReportDateRange.Split('-')[0].Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
                        DateTime endDate = startDate.AddDays(6);
                        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                        {
                            if (date == endDate) unionWord = "";
                            setLeftData += string.Format(@"SELECT CAST('{0:M/d/yyyy}' AS DATETIME) 'paymentDate' {1}
    ", date, unionWord);
                        }

                        sqlQuery = string.Format(@"
SELECT CONVERT(VARCHAR, d.paymentDate, 103) 'Date', ISNULL(a.SumAmount, 0) 'TotalPaid'
FROM
(
	{0}
) d LEFT JOIN 
(
	SELECT pp.paymentDate, SUM(pp.Amount) 'SumAmount'
	FROM Paid_Payment pp LEFT JOIN TInvoices i ON pp.InvoiceId = i.invoices_Id
	WHERE pp.paymentDate BETWEEN '{1:M/d/yyyy}' AND '{2:M/d/yyyy}' AND i.school_id = {3} {4} AND pp.status IS NULL AND ISNULL(pp.isDel,0) = 0
	GROUP BY pp.paymentDate
) a ON d.paymentDate = a.paymentDate
ORDER BY d.paymentDate
", setLeftData, startDate, endDate, schoolId, sqlCondition);
                    }
                    else if (data.ReportType == (int)EnumReportType.Month)
                    {
                        DateTime startDate = DateTime.ParseExact(string.Format(@"01/{0:D2}/{1}", data.ReportMonth, data.ReportYear), "dd/MM/yyyy", new CultureInfo("en-US"));
                        DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                        {
                            if (date == endDate) unionWord = "";
                            setLeftData += string.Format(@"SELECT CAST('{0:M/d/yyyy}' AS DATETIME) 'paymentDate' {1}
    ", date, unionWord);
                        }

                        sqlQuery = string.Format(@"
SELECT CONVERT(VARCHAR, d.paymentDate, 103) 'Date', ISNULL(a.SumAmount, 0) 'TotalPaid'
FROM
(
	{0}
) d LEFT JOIN 
(
	SELECT pp.paymentDate, SUM(pp.Amount) 'SumAmount'
	FROM Paid_Payment pp LEFT JOIN TInvoices i ON pp.InvoiceId = i.invoices_Id
	WHERE MONTH(pp.paymentDate) = {1} AND YEAR(pp.paymentDate) = {2} AND i.school_id = {3} {4} AND pp.status IS NULL AND ISNULL(pp.isDel,0) = 0
	GROUP BY pp.paymentDate
) a ON d.paymentDate = a.paymentDate
ORDER BY d.paymentDate
", setLeftData, data.ReportMonth, data.ReportYear, schoolId, sqlCondition);
                    }
                    else
                    {
                        for (int m = 1; m <= 12; m++)
                        {
                            if (m == 12) unionWord = "";
                            setLeftData += string.Format(@"SELECT {0} 'Month', {1} 'Year' {2}
    ", m, data.ReportYear, unionWord);
                        }

                        sqlQuery = string.Format(@"
SELECT '1/' + CAST(m.Month AS VARCHAR(2)) + '/' + CAST(m.Year AS VARCHAR(4)) 'Date', ISNULL(a.SumAmount, 0) 'TotalPaid'
FROM
(
	{0}
) m LEFT JOIN 
(
	SELECT MONTH(pp.paymentDate) 'Month', YEAR(pp.paymentDate) 'Year', SUM(pp.Amount) 'SumAmount'
	FROM Paid_Payment pp LEFT JOIN TInvoices i ON pp.InvoiceId = i.invoices_Id
	WHERE YEAR(pp.paymentDate) = {1} AND i.school_id = {2} {3} AND pp.status IS NULL AND ISNULL(pp.isDel,0) = 0
	GROUP BY MONTH(pp.paymentDate), YEAR(pp.paymentDate)
) a ON m.Month = a.Month
ORDER BY m.Month
", setLeftData, data.ReportYear, schoolId, sqlCondition);
                    }
                    var chartData = peakContext.Database.SqlQuery<ReportInvoiceOutput>(sqlQuery).ToList();

                    result.Result = chartData;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = "500";
                result.Message = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ" + ex.Message;
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(result));
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

    public class ReportInvoice
    {
        public int AccountingId { get; set; }
        public int ReportType { get; set; }
        public string ReportDate { get; set; }
        public string ReportDateRange { get; set; }
        public int? ReportYear { get; set; }
        public int? ReportMonth { get; set; }
        public string PaymentMethodId { get; set; }
        public string TermId { get; set; }
        public string ItemType { get; set; }

    }

    public class ReportInvoiceOutput
    {
        public string Date { get; set; }
        public decimal TotalPaid { get; set; }
    }
}