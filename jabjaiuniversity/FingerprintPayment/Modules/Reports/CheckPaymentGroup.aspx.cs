using Jabjai.BusinessLogic;
using Jabjai.BusinessLogic.Master;
using Jabjai.Object;
using Jabjai.Object.Entity.Jabjai;
using JabjaiMainClass;
using MasterEntity;
using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.StudentInfo.CsCode;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace FingerprintPayment.Modules.Reports
{
    public partial class CheckPaymentGroup : StudentGateway
    {
        public string YearsJson { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read);

                // Get current year
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTerm = studentLogic.GetTermId(UserData);
                int yearID = 0;
                var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
                if (term != null)
                {
                    yearID = term.nYear.Value;
                }

                var listYear = en.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false).OrderByDescending(x => x.numberYear).ToList();
                foreach (var l in listYear)
                {
                    if (l.nYear == yearID)
                    {
                        this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }
                    else
                    {
                        this.ltrYear.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }

                    if (yearID == 0) yearID = l.nYear;
                }

                if (yearID != 0)
                {
                    var listTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yearID && w.cDel == null).OrderByDescending(o => o.nTerm).ToList();
                    foreach (var l in listTerm)
                    {
                        if (l.nTerm.Trim() == currentTerm)
                        {
                            this.ltrTerm.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                        }
                        else
                        {
                            this.ltrTerm.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                        }
                    }
                }

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                }

                //var products = peakengine.Products.Where(w => w.school_id == schoolID && (w.Del ?? false) == false).ToList();
                //foreach (var l in products)
                //{
                //    this.ltrProduct.Text += string.Format(@"<option value=""{0}"" >{1}</option>", l.nPaymentID, l.sPayment);
                //}
            }
        }

        [ScriptMethod()]
        [WebMethod]
        public static object LoadProduct(string TermID, string subLevelID)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                string SQL = string.Format(@"
DECLARE @nTSubLevel INT = {0};
DECLARE @nTerm NVARCHAR(20) = '{1}';
DECLARE @SchoolID INT = {2};

SELECT P1.nPaymentID AS id,P1.sPayment AS name
FROM
(SELECT sID,nTerm,nTSubLevel,SchoolID 
FROM JabjaiSchoolSingleDB.dbo.TB_StudentViews 
WHERE SchoolID = @SchoolID AND nTSubLevel = @nTSubLevel AND nTerm = @nTerm)AS A
INNER JOIN PeakengineSingleDB.dbo.TInvoices AS B
ON A.sID = B.student_id AND A.SchoolID = B.school_id AND A.nTerm = B.nTerm

INNER JOIN PeakengineSingleDB.dbo.TInvoices_Detail AS C
ON B.invoices_Id = C.invoices_Id 

INNER JOIN PeakengineSingleDB.dbo.Product AS P1
ON P1.nPaymentID = C.nPaymentID

WHERE A.nTSubLevel = @nTSubLevel AND A.nTerm = @nTerm
GROUP BY P1.nPaymentID,P1.sPayment
", subLevelID, TermID, userData.CompanyID);

                var q = entities.Database.SqlQuery<TM_Product>(SQL).ToList();
                string errorMessage = "";

                return new { Data = q, Error = errorMessage };
            }

        }

        [ScriptMethod()]
        [WebMethod]
        public static object ReturnList(ISearch search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            string errorMessage = "";
            List<TR_CheckPaymentGroup> rss = GetCheckPaymentGroup(search, userData);

            return new { Data = rss, Error = errorMessage };
        }

        private static List<TR_CheckPaymentGroup> GetCheckPaymentGroup(ISearch search, JWTToken.userData userData)
        {

            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                string _filter = "", _filter2 = "";
                if (search.dateEnd.HasValue && search.dateStart.HasValue)
                {
                    _filter += string.Format(" AND paymentDate BETWEEN '{0:MM/dd/yyyy}' AND '{1:MM/dd/yyyy 23:59:59}' ", search.dateStart, search.dateEnd);
                }
                else if (search.dateStart.HasValue)
                {
                    _filter += string.Format(" AND paymentDate BETWEEN '{0:MM/dd/yyyy}' AND '{0:MM/dd/yyyy 23:59:59}' ", search.dateStart, search.dateStart);
                }

                if (search.PaymentType == "0")
                {
                    _filter2 += " AND ISNULL(DE.amount,0) <> 0 ";
                }
                else if (search.PaymentType == "1")
                {
                    _filter2 += " AND ISNULL(DE.amount,0) = 0 ";
                }

                string SQL = string.Format(@"
SELECT A.sStudentID,A.sID,A.nTerm,A.sTerm,A.numberYear,A.nYear,A.sName,A.sLastname,
C.GrandTotal,DE.amount AS OutstandingAmount,DE.ReceiptNo,
A.nTermSubLevel2,A.SubLevel,A.nTSubLevel2,DE.invoicesDetail_Id,B.invoices_Id
FROM
JabjaiSchoolSingleDB.dbo.TB_StudentViews AS A
INNER JOIN PeakengineSingleDB.dbo.TInvoices AS B
ON A.sID = B.student_id AND A.SchoolID = B.school_id AND A.nTerm = B.nTerm

INNER JOIN PeakengineSingleDB.dbo.TInvoices_Detail AS C
ON B.invoices_Id = C.invoices_Id
LEFT OUTER JOIN (
	SELECT D.InvoiceId,E.invoicesDetail_Id,ReceiptNo,E.amount 
	FROM PeakengineSingleDB.dbo.Paid_Payment AS D
	LEFT OUTER JOIN PeakengineSingleDB.dbo.Payment AS E 
	ON E.paidpayment_id = D.paidpayment_id
	WHERE ISNULL(D.isDel,0) = 0 AND D.status IS NULL AND ISNULL(E.isDel,0) = 0 {3}
) AS DE ON DE.InvoiceId = B.invoices_Id AND DE.invoicesDetail_Id = C.invoicesDetail_Id

WHERE ISNULL(C.IsDelete,0) = 0 AND ISNULL(B.isDel,0) = 0 
AND A.nTermSubLevel2 IN ({0}) AND A.nTerm = '{1}' AND C.nPaymentID = {2} {4}

ORDER BY LEN(A.nTSubLevel2),A.nTSubLevel2

", search.SubLevel2Id, search.TermId, search.Product, _filter, _filter2);

                //if (!string.IsNullOrEmpty(search.StudentId)) SQL += " AND A.sID = " + search.StudentId;

                var q = entities.Database.SqlQuery<TB_CheckPaymentGroup>(SQL).ToList();


                List<TR_CheckPaymentGroup> rss = (from a in q
                                                  group a by new { a.nTermSubLevel2, a.nTSubLevel2, a.SubLevel } into gb1
                                                  select new TR_CheckPaymentGroup
                                                  {
                                                      nTermSubLevel2 = gb1.Key.nTermSubLevel2,
                                                      nTSubLevel2 = gb1.Key.nTSubLevel2,
                                                      SubLevel = gb1.Key.SubLevel,
                                                      Term_Data = (from a1 in gb1
                                                                   group a1 by new { a1.sName, a1.sLastname, a1.sStudentID, a1.sID } into gb2
                                                                   //where string.IsNullOrEmpty(search.PaymentType) || (
                                                                   //    (search.PaymentType == "0" && gb2.Sum(s => s.GrandTotal) - gb2.Sum(s => s.OutstandingAmount) > 0) ||
                                                                   //    (search.PaymentType == "1" && gb2.Sum(s => s.GrandTotal) - gb2.Sum(s => s.OutstandingAmount) == 0)
                                                                   //)
                                                                   select new TR_CheckPaymentGroup.term_Data
                                                                   {
                                                                       StudentName = gb2.Key.sName + " " + gb2.Key.sLastname,
                                                                       StudentID = gb2.Key.sStudentID,
                                                                       GrandTotal = (from g1 in q
                                                                                     where g1.sID == gb2.Key.sID
                                                                                     group g1 by new { g1.invoices_Id, g1.GrandTotal } into g1b
                                                                                     select new
                                                                                     {
                                                                                         g1b.Key.invoices_Id,
                                                                                         g1b.Key.GrandTotal
                                                                                     }).Sum(s => s.GrandTotal),

                                                                       OutstandingAmount = (from g1 in q
                                                                                            where g1.sID == gb2.Key.sID
                                                                                            group g1 by new { g1.invoices_Id, g1.GrandTotal } into g1b
                                                                                            select new
                                                                                            {
                                                                                                g1b.Key.invoices_Id,
                                                                                                GrandTotal = g1b.Key.GrandTotal - g1b.Sum(s => s.OutstandingAmount),
                                                                                            }).Sum(s => s.GrandTotal),

                                                                       //OutstandingAmount = gb2.Sum(s => s.GrandTotal) - gb2.Sum(s => s.OutstandingAmount),
                                                                       Paid_PaymentAmount = gb2.Sum(s => s.OutstandingAmount),
                                                                       ReceiptNo = (from a2 in gb2
                                                                                    where !string.IsNullOrEmpty(a2.ReceiptNo)
                                                                                    select a2.ReceiptNo
                                                                                    ).ToList()
                                                                   }).ToList()
                                                  }).ToList();

                return rss;
            }
        }

        [ScriptMethod()]
        [WebMethod]
        public static void ExportExcel(ISearch search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    string Worksheets = "Worksheets";
                    excel.Workbook.Worksheets.Add(Worksheets);

                    var worksheet = excel.Workbook.Worksheets[Worksheets];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                   
                    var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                    string ColspanHeader = "A{0}:H{0}";
                    string textHeader_reports = "รายงานตรวจสอบรายชื่อตามกลุ่มชำระเงิน";
                    int rowsStart = 1;

                    SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, textHeader_reports, 14, ExcelHorizontalAlignment.Center);

                    List<TR_CheckPaymentGroup> data = GetCheckPaymentGroup(search, userData);

                    //var f_year = jabJaiEntities.TYears.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.nYear == search.YearId);
                    SetHeader(worksheet, string.Format("A{0}:B{0}", rowsStart), true, "ปีการศึกษา : ", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, string.Format("C{0}:D{0}", rowsStart), true, search.filter[0], null, ExcelHorizontalAlignment.Left);
                    SetHeader(worksheet, string.Format("E{0}:F{0}", rowsStart), true, "เทอม : ", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, string.Format("G{0}:H{0}", rowsStart++), true, search.filter[1], null, ExcelHorizontalAlignment.Left);

                    SetHeader(worksheet, string.Format("A{0}:B{0}", rowsStart), true, "ระดับชั้น : ", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, string.Format("C{0}:D{0}", rowsStart), true, search.filter[2], null, ExcelHorizontalAlignment.Left);
                    SetHeader(worksheet, string.Format("E{0}:F{0}", rowsStart), true, "ชั้นเรียน : ", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, string.Format("G{0}:H{0}", rowsStart++), true, search.filter[3], null, ExcelHorizontalAlignment.Left);

                    SetHeader(worksheet, string.Format("A{0}:B{0}", rowsStart), true, "ค้นหารายการ : ", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, string.Format("C{0}:D{0}", rowsStart), true, search.filter[4], null, ExcelHorizontalAlignment.Left);
                    SetHeader(worksheet, string.Format("E{0}:F{0}", rowsStart), true, "สถานะ : ", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, string.Format("G{0}:H{0}", rowsStart++), true, search.filter[5], null, ExcelHorizontalAlignment.Left);

                    SetHeader(worksheet, string.Format("A{0}:B{0}", rowsStart), true, "วันที่ : ", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, string.Format("C{0}:D{0}", rowsStart), true, search.filter[6], null, ExcelHorizontalAlignment.Left);
                    SetHeader(worksheet, string.Format("E{0}:F{0}", rowsStart), true, "ถึงวันที่ : ", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, string.Format("G{0}:H{0}", rowsStart++), true, search.filter[7], null, ExcelHorizontalAlignment.Left);

                    var setting = jabJaiEntities.TBehaviorSettings.Where(w => w.SchoolID == tCompany.nCompany).FirstOrDefault();
                    //SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, "ปีการศึกษา : " + f_year.numberYear, null, ExcelHorizontalAlignment.Right);

                    SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, "พิมพ์วันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")) + " เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Right);

                    rowsStart += 1;
                    int Columuns = 1;
                    string[] strHeader = { "ลำดับ	", "ระดับชั้น", "รหัสนักเรียน", "ชื่อ-นามสกุล", "ยอดเรียกเก็บ	", "ยอดค้างชำระ", "ยอดชำระ", "หมายเหตุ" };

                    foreach (string str in strHeader)
                    {
                        SetTableHeader(worksheet.Cells[rowsStart, Columuns++], false, str, ExcelHorizontalAlignment.Center);
                    }

                    rowsStart += 1;
                    int _rowsClass = 1;
                    decimal? GrandTotal = 0, OutstandingAmount = 0, Paid_PaymentAmount = 0;
                    foreach (var ClassData in data)
                    {
                        if (ClassData.Term_Data.Count() == 0) continue;
                        SetTableRows(worksheet.Cells[rowsStart, 1, rowsStart + ClassData.Term_Data.Count() - 1, 1], true, string.Format("{0}.", _rowsClass++), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[rowsStart, 2, rowsStart + ClassData.Term_Data.Count() - 1, 2], true, ClassData.SubLevel + "/" + ClassData.nTSubLevel2, ExcelHorizontalAlignment.Center);

                        foreach (var StudentData in ClassData.Term_Data)
                        {
                            string ReceiptNo = "";
                            if (StudentData.ReceiptNo.Count() > 0)
                            {
                                foreach (var _ReceiptNo in StudentData.ReceiptNo)
                                {
                                    if (!string.IsNullOrEmpty(ReceiptNo)) ReceiptNo += ",";
                                    ReceiptNo += _ReceiptNo;
                                }
                                ReceiptNo = "ใบเสร็จ " + ReceiptNo;
                            }

                            SetTableRows(worksheet.Cells[rowsStart, 3], true, StudentData.StudentID, ExcelHorizontalAlignment.Right);
                            SetTableRows(worksheet.Cells[rowsStart, 4], true, StudentData.StudentName, ExcelHorizontalAlignment.Right);
                            SetTableRows(worksheet.Cells[rowsStart, 5], true, string.Format("{0:#,0.00}", StudentData.GrandTotal ?? 0), ExcelHorizontalAlignment.Right);
                            SetTableRows(worksheet.Cells[rowsStart, 6], true, string.Format("{0:#,0.00}", StudentData.OutstandingAmount ?? 0), ExcelHorizontalAlignment.Right);
                            SetTableRows(worksheet.Cells[rowsStart, 7], true, string.Format("{0:#,0.00}", StudentData.Paid_PaymentAmount ?? 0), ExcelHorizontalAlignment.Right);
                            SetTableRows(worksheet.Cells[rowsStart, 8], true, ReceiptNo, ExcelHorizontalAlignment.Right);

                            GrandTotal += StudentData.GrandTotal ?? 0;
                            OutstandingAmount += StudentData.OutstandingAmount ?? 0;
                            Paid_PaymentAmount += StudentData.Paid_PaymentAmount ?? 0;

                            rowsStart += 1;
                        }
                    }

                    SetTableRows(worksheet.Cells[string.Format("A{0}:D{0}", rowsStart)], true, "รวม", ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[rowsStart, 5], true, string.Format("{0:#,0.00}", GrandTotal), ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[rowsStart, 6], true, string.Format("{0:#,0.00}", OutstandingAmount), ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[rowsStart, 7], true, string.Format("{0:#,0.00}", Paid_PaymentAmount), ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[rowsStart++, 8], true, "", ExcelHorizontalAlignment.Right);

                    SetTableRows(worksheet.Cells[string.Format("A{0}:D{0}", rowsStart)], true, "ร้อยละ", ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[rowsStart, 5], true, string.Format("{0:#,0.00} %", 100), ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[rowsStart, 6], true, string.Format("{0:#,0.00} %", GrandTotal == 0 ? 100 : (OutstandingAmount * 100) / GrandTotal), ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[rowsStart, 7], true, string.Format("{0:#,0.00} %", GrandTotal == 0 ? 0 : (Paid_PaymentAmount * 100) / GrandTotal), ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[rowsStart++, 8], true, "", ExcelHorizontalAlignment.Right);

                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(1).Width = 13;
                    worksheet.Column(2).Width = 18;
                    worksheet.Column(3).Width = 11;
                    worksheet.Column(4).Width = 28;
                    worksheet.Column(5).Width = 15;

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

        private static void SetHeader(ExcelWorksheet excelWorksheet, string Cells, bool Merge, string strValues, int? fontSize, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = excelWorksheet.Cells[Cells])
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Font.Size = fontSize ?? 10;
            }
        }

        private static void SetTableHeader(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                rng.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
            }
        }
        private static void SetTableRows(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            }
        }

        public class ISearch
        {
            public string TermId { get; set; }
            public string SubLevel2Id { get; set; }
            public string StudentId { get; set; }
            public string Product { get; set; }
            public string PaymentType { get; set; }
            public string DateStart { get; set; }
            public string DateEnd { get; set; }
            internal DateTime? dateStart
            {
                get
                {
                    if (!string.IsNullOrEmpty(this.DateStart))
                    {
                        try
                        {
                            return DateTime.ParseExact(this.DateStart, "dd/MM/yyyy", new CultureInfo("th-TH"));
                        }
                        catch
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            internal DateTime? dateEnd
            {
                get
                {
                    if (!string.IsNullOrEmpty(this.DateEnd))
                    {
                        try
                        {
                            return DateTime.ParseExact(this.DateEnd, "dd/MM/yyyy", new CultureInfo("th-TH"));
                        }
                        catch
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            public string[] filter { get; set; }
            //public string StudentId { get; set; }
        }

        public class TR_CheckPaymentGroup
        {
            public int sID { get; set; }
            public string nTerm { get; set; }
            public string sTerm { get; set; }
            public int numberYear { get; set; }
            public int? nYear { get; set; }
            public int nTermSubLevel2 { get; set; }
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public List<term_Data> Term_Data { get; set; }

            public class term_Data
            {
                public string StudentName { get; set; }
                public string StudentID { get; set; }
                public decimal? GrandTotal { get; set; }
                public decimal? OutstandingAmount { get; set; }
                public decimal? Paid_PaymentAmount { get; set; }
                public List<string> ReceiptNo { get; set; }
            }
        }

        public class TB_CheckPaymentGroup
        {
            public string sStudentID { get; set; }
            public string sName { get; set; }
            public string sLastname { get; set; }
            public int sID { get; set; }
            public string nTerm { get; set; }
            public string sTerm { get; set; }
            public int numberYear { get; set; }
            public int? nYear { get; set; }
            public decimal? GrandTotal { get; set; }
            public decimal? OutstandingAmount { get; set; }
            public string ReceiptNo { get; set; }
            public int nTermSubLevel2 { get; set; }
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public Nullable<int> invoices_Id { get; set; }
        }

        public class TM_Product
        {
            public string name { get; set; }
            public int id { get; set; }
        }
    }
}