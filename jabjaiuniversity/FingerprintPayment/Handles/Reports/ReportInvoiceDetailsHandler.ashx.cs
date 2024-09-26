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
    public class ReportInvoiceDetailsHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string jsonString = string.Empty;
            int schoolId = 3;   //#HardcodeSchoolId
            BaseResult<DTResult<PaymentDTO>> result = new BaseResult<DTResult<PaymentDTO>>();
            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
            {
                try
                {
                    Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);
                    StudentLogic logic = new StudentLogic(entities);

                    using (var reader = new StreamReader(context.Request.InputStream))
                    {
                        jsonString = reader.ReadToEnd();
                    }

                    var dTParameters = JsonConvert.DeserializeObject<DTParameters>(jsonString, new JsonSerializerSettings
                    {
                        DateFormatString = "dd/MM/yyyy"
                    });

                    string sqlCondition = "";
                    string sqlCondition2 = "";
                    string sqlQueryCount = "";
                    string sqlQuery = "";

                    using (Jabjai.DataAccess.PeakContext peakContext = new Jabjai.DataAccess.PeakContext())
                    {

                        string paramReportDate = dTParameters.GetSearchValueByColumnName("ReportDate");
                        string paramReportMonth = dTParameters.GetSearchValueByColumnName("ReportMonth");
                        string paramReportYear = dTParameters.GetSearchValueByColumnName("ReportYear");

                        string paramPaymentMethodId = dTParameters.GetSearchValueByColumnName("PaymentMethodId");
                        string paramTermId = dTParameters.GetSearchValueByColumnName("TermId");
                        DateTime StartDate = DateTime.Today, EndDate = DateTime.Today;

                        if (!string.IsNullOrEmpty(paramReportDate))
                        {
                            DateTime filterStartDate = DateTime.ParseExact(paramReportDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                            StartDate = filterStartDate;
                            EndDate = filterStartDate;
                            sqlCondition += string.Format(@" AND (pp.paymentDate BETWEEN '{0:yyyy-MM-dd} 00:00:00' AND '{0:yyyy-MM-dd} 23:59:00') ", filterStartDate);
                        }
                        if (!string.IsNullOrEmpty(paramReportMonth))
                        {
                            int month = Convert.ToInt32(paramReportMonth);
                            int year = Convert.ToInt32(paramReportYear);

                            StartDate = new DateTime(year, month, 1);
                            EndDate = StartDate.AddMonths(1).AddDays(-1);
                            sqlCondition += string.Format(@" AND (MONTH(pp.paymentDate) = {0} AND YEAR(pp.paymentDate) = {1}) ", month, year);
                        }
                        if (!string.IsNullOrEmpty(paramReportYear))
                        {
                            int year = Convert.ToInt32(paramReportYear);
                            StartDate = new DateTime(year, 1, 1);
                            EndDate = StartDate.AddYears(1).AddDays(-1);
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
                                orderBy = "CreatedDate " + dTParameters.Order[0].Dir;
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
SELECT A.RowNumber 'No', A.*,(
	SELECT * FROM (
		SELECT '' AS S,ISNULL(C.amount,0)- ISNULL(C.Discount,0) AS A,A1.nPaymentID AS D,
		ROW_NUMBER() OVER (ORDER BY A1.nPaymentID) AS RowNum
		FROM Product AS A1
		LEFT OUTER JOIN TInvoices_Detail AS B ON A1.nPaymentID = B.nPaymentID AND invoices_Id = A.InvoiceId AND ISNULL(B.IsDelete,0) = 0  	
		LEFT OUTER JOIN Payment AS C ON B.invoicesDetail_Id = C.invoicesDetail_Id  AND paidpayment_id = A.paidpayment_id AND C.paidpayment_id = A.paidpayment_id
		WHERE school_id = {2} AND ISNULL(A1.Del,0) = 0 	
	) T1
	FOR JSON AUTO
) AS PRODUCT_ITEM,
'' AS FOOTER_0,
'' AS FOOTER_1,
'' AS FOOTER_2,
(
	SELECT ISNULL(SUM(ProductType_0),0) AS ProductType_0,ISNULL(SUM(ProductType_1),0) AS ProductType_1 FROM (
		SELECT A1.sPayment,
		CASE WHEN A1.ProductType = 0 THEN (C.amount - ISNULL(C.Discount,0)) ELSE 0 END ProductType_0,
		CASE WHEN A1.ProductType = 1 THEN (C.amount - ISNULL(C.Discount,0)) ELSE 0 END ProductType_1
		FROM Product AS A1
		LEFT OUTER JOIN TInvoices_Detail AS B ON A1.nPaymentID = B.nPaymentID AND invoices_Id = A.InvoiceId 	
		LEFT OUTER JOIN Payment AS C ON B.invoicesDetail_Id = C.invoicesDetail_Id  AND paidpayment_id = A.paidpayment_id AND C.paidpayment_id = A.paidpayment_id
		WHERE school_id = {2} AND ISNULL(Del,0) = 0 
	) T1
	FOR JSON AUTO
) AS PRODUCT_GROUP
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT p.paymentDate 'PaymentDate', ISNULL(p.ReceiptNo, i.code) 'InvoiceNumber', i.StudentCode, i.StudentName,ISNULL(i.Fd_NewTermSubLevel,i.SubLevel) 'StudentSubLevel',ISNULL(i.Fd_NewTermSubLevel2,i.SubLevel2) 'StudentSubLevel2', i.Term 'StudentTerm', i.TermYear 'StudentTermYear', i.GrandTotal, p.Amount, p.Payee, ISNULL(BK.BankName + ' - ','') + pm.PaymentMethodName 'PaymentMethod',P.paidpayment_id,P.InvoiceId,p.CreatedDate--,FORMAT(p.CreatedDate,'dd/MM/yyyy HH:mm:ss') AS CreatedDate
		FROM
		(
			SELECT pp.paidpayment_id, pp.paymentDate, pp.InvoiceId, pp.Payee, pp.ReceiptNo, p.paymentMethodId, SUM(p.amount) - ISNULL(paid_Discount,0) 'Amount',pp.CreatedDate
			FROM Payment p LEFT JOIN Paid_Payment pp ON p.paidpayment_id = pp.paidpayment_id    
            INNER JOIN TInvoices_Detail AS B ON B.invoicesDetail_Id = P.invoicesDetail_Id
			INNER JOIN Product AS PR ON PR.nPaymentID = B.nPaymentID AND ISNULL(PR.Del,0) = 0
			WHERE pp.[status] IS NULL {1} AND B.IsDelete = 0
			GROUP BY pp.paidpayment_id, pp.paymentDate, pp.InvoiceId, pp.Payee, pp.ReceiptNo, p.paymentMethodId,pp.paid_Discount,pp.CreatedDate
		) p 
        LEFT JOIN TInvoices i ON p.InvoiceId = i.invoices_Id
        LEFT JOIN PaymentMethod pm ON p.paymentMethodId = pm.PaymentMethodId
		LEFT JOIN Bank AS BK ON BK.BankId = pm.BankId
		WHERE i.school_id = {2} {3} AND ISNULL(i.isDel,0) = 0
    ) AS T
) AS A
WHERE RowNumber >= {4} AND RowNumber < {5}
ORDER BY A.RowNumber
", orderBy, sqlCondition, schoolId, sqlCondition2, lowerBound, upperBound);

                        int resultCount = peakContext.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                        List<PaymentDTO> listPayment = peakContext.Database.SqlQuery<PaymentDTO>(sqlQuery).ToList();

                        // Prepare data
                        foreach (var l in listPayment)
                        {
                            l.PaymentDateString = l.PaymentDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        }

                        TB_FOOTER b_FOOTER = new TB_FOOTER();

                        var Trem = logic.GetTermDATA(StartDate, new JWTToken.userData { CompanyID = schoolId });
                        var f_year = entities.TYears.FirstOrDefault(f => f.nYear == Trem.nYear);
                        var q_term = entities.TTerms.Where(w => w.nYear == Trem.nYear && w.cDel != "1").ToList();

                        b_FOOTER.footer_2 = GetFOOTER(peakContext, string.Format("{0:yyyy-MM-dd}", StartDate), string.Format("{0:yyyy-MM-dd}", EndDate), schoolId, sqlCondition, sqlCondition2);
                        b_FOOTER.wording_2 = "รวมเงินทั้งหมด";
                        b_FOOTER.footer_SUM2 = b_FOOTER.footer_2.Where(w => w.nPaymentID > 0).Sum(s => s.amount);

                        //b_FOOTER.footer_0 = GetFOOTER(peakContext, string.Format("{0:yyyy-MM-dd}", Trem.dStart), string.Format("{0:yyyy-MM-dd}", Trem.dEnd), schoolId, sqlCondition);
                        //b_FOOTER.wording_0 = "รวมเงินภาคเรียนที่ " + Trem.sTerm;
                        //b_FOOTER.footer_SUM0 = b_FOOTER.footer_0.Where(w => w.nPaymentID > 0).Sum(s => s.amount);

                        //b_FOOTER.footer_1 = GetFOOTER(peakContext, string.Format("{0:yyyy-MM-dd}", q_term.Min(m => m.dStart)), string.Format("{0:yyyy-MM-dd}", q_term.Max(m => m.dEnd)), schoolId, sqlCondition);
                        //b_FOOTER.wording_1 = "รวมเงินปีการศึกษา " + f_year.numberYear;
                        //b_FOOTER.footer_SUM1 = b_FOOTER.footer_1.Where(w => w.nPaymentID > 0).Sum(s => s.amount);

                        result.Result = new DTResult<PaymentDTO>();
                        result.Result.data = listPayment;
                        result.Result.footer = new JavaScriptSerializer().Serialize(b_FOOTER);
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
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(result.Result));
            context.Response.End();
        }

        private List<FOOTER> GetFOOTER(Jabjai.DataAccess.PeakContext peakContext, string filterStartDate, string filterEndtDate, int schoolId, string sqlCondition, string sqlCondition2)
        {
            string sqlQuery = @"

DECLARE @school_id INT = {0}
DECLARE @paymentDate_START DATETIME =  '{1:yyyy-MM-dd}' 
DECLARE @paymentDate_END DATETIME = DATEADD(dd,1,'{2:yyyy-MM-dd}')

SELECT '' AS sPayment,SUM(ISNULL(C.Amount,0) - ISNULL(C.Discount,0)) AS amount,A1.nPaymentID,A1.ProductType,COUNT(*)
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class TB_FOOTER
    {
        public List<FOOTER> footer_0 { get; set; }
        public string wording_0 { get; set; }
        public decimal? footer_SUM0 { get; set; }
        public List<FOOTER> footer_1 { get; set; }
        public string wording_1 { get; set; }
        public decimal? footer_SUM1 { get; set; }
        public List<FOOTER> footer_2 { get; set; }
        public string wording_2 { get; set; }
        public decimal? footer_SUM2 { get; set; }
    }

    public class FOOTER
    {
        public string sPayment { get; set; }
        public int nPaymentID { get; set; }
        public decimal amount { get; set; }
    }

    public class Q_FOOTER
    {
        public int? ProductType { get; set; }
        public string sPayment { get; set; }
        public int nPaymentID { get; set; }
        public decimal amount { get; set; }
    }
}