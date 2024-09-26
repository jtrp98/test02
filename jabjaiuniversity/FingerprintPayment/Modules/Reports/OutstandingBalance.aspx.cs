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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FingerprintPayment.Modules.Reports
{
    public partial class OutstandingBalance : StudentGateway
    {
        public string YearsJson { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

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
                    //this.ltrTerm.Text += "<option selected=\"selected\" value=\"\">ทั้งหมด</option>";

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

            List<TR_OutstandingBalance> rss = GetOutstandingBalances(search, userData);

            return new { Data = rss, Error = errorMessage };
        }

        private static List<TR_OutstandingBalance> GetOutstandingBalances(ISearch search, JWTToken.userData userData)
        {
            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                string SQL = string.Format(@"SELECT A.sName + ' ' + A.sLastname AS StudentName,B.code,P1.AccountingId,P1.sPayment,ISNULL(C.OutstandingAmount,P1.Pirce) AS OutstandingAmount,C.GrandTotal,
B.Term AS Term,B.TermYear,A.SubLevel,A.nTSubLevel2,A.sStudentID,B.GrandTotal AS Fb_GrandTotal,B.paymentAmount AS Fb_PaymentAmount,B.OutstandingAmount AS Fb_OutstandingAmount,
ISNULL(B.ManualDiscount,0) AS Fb_Discount
FROM JabjaiSchoolSingleDB.dbo.TB_StudentViews AS A
INNER JOIN [PeakengineSingleDB].[dbo].TInvoices AS B ON B.student_id = A.sID AND B.school_id = A.schoolid 
INNER JOIN [PeakengineSingleDB].[dbo].TInvoices_Detail AS C ON B.invoices_Id = C.invoices_Id
INNER JOIN [PeakengineSingleDB].[dbo].Product AS P1 ON C.nPaymentID = P1.nPaymentID
WHERE ISNULL(C.IsDelete,0) = 0 AND ISNULL(B.isDel,0) = 0 AND A.nTermSubLevel2 = {1} AND B.invoices_status = 'Approve' AND A.nTerm = '{0}'
", search.TermId, search.SubLevel2Id);

                var f_term = entities.TTerms.FirstOrDefault(f => f.nTerm == search.TermId);
                SQL += " AND B.nTerm IN (SELECT nTerm FROM JabjaiSchoolSingleDB.dbo.TTerm WHERE dStart <= '" + f_term.dStart.Value.ToString("yyyyMMdd") + "' AND SchoolID = " + userData.CompanyID + ") ";

                //if (!string.IsNullOrEmpty(search.StudentCode)) SQL += string.Format("AND (A.sStudentID LIKE '%{0}%' AND A.sName + ' ' + A.sLastname Like '%{0}%') ", search.StudentCode);
                if (!string.IsNullOrEmpty(search.StudentCode)) SQL += string.Format(" AND A.sID = {0} ", search.StudentCode);

                var q = entities.Database.SqlQuery<TB_OutstandingBalance>(SQL).ToList();

                List<TR_OutstandingBalance> rss = (from a in q
                                                   group a by new { a.StudentName, a.nTSubLevel2, a.sStudentID, a.SubLevel } into gb1
                                                   select new TR_OutstandingBalance
                                                   {
                                                       sStudentID = gb1.Key.sStudentID,
                                                       StudentName = gb1.Key.StudentName,
                                                       //Fb_PaymentAmount = gb1.Sum(s => (s.GrandTotal ?? 0) - ((s.OutstandingAmount ?? 0) < 0 ? 0 : (s.OutstandingAmount ?? 0))),
                                                       //Fb_OutstandingAmount = gb1.Sum(s => (s.OutstandingAmount ?? 0))  ,
                                                       //Fb_GrandTotal = gb1.Sum(s => (s.GrandTotal ?? 0)),
                                                       SubLevel = gb1.Key.SubLevel,
                                                       nTSubLevel2 = gb1.Key.nTSubLevel2,
                                                       term_Datas = (from a1 in gb1
                                                                     group a1 by new { a1.TermYear, a1.Term } into gb2
                                                                     orderby gb2.Key.TermYear, gb2.Key.Term
                                                                     select new TR_OutstandingBalance.Term_Data
                                                                     {
                                                                         TermYear = gb2.Key.TermYear,
                                                                         Term = gb2.Key.Term,
                                                                         TermRowsNumber = gb2.Count(),
                                                                         invoices_Data = (from a2 in gb2
                                                                                          group a2 by new { a2.code, a2.Fb_Discount } into gb3
                                                                                          orderby gb3.Key.code
                                                                                          select new TR_OutstandingBalance.Term_Data.Invoices_Data
                                                                                          {
                                                                                              Invoices_Code = gb3.Key.code,
                                                                                              InvoicesRowsNumber = gb3.Count(),
                                                                                              Discount = gb3.Key.Fb_Discount,
                                                                                              GrandTotal = gb3.Sum(s => s.GrandTotal),
                                                                                              OutstandingAmount = gb3.Sum(s => s.OutstandingAmount ?? 0),
                                                                                              invoices_Items = (from a3 in gb3
                                                                                                                select new TR_OutstandingBalance.Term_Data.Invoices_Item
                                                                                                                {
                                                                                                                    sPayment = a3.sPayment,
                                                                                                                    OutstandingAmount = a3.OutstandingAmount ?? 0,
                                                                                                                    AccountingId = a3.AccountingId,
                                                                                                                    GrandTotal = a3.GrandTotal ?? 0,
                                                                                                                }).ToList(),
                                                                                          }).ToList()
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
                    string Worksheets = "รายงานสรุปยอดค้างชำระเงิน";
                    excel.Workbook.Worksheets.Add(Worksheets);

                    var worksheet = excel.Workbook.Worksheets[Worksheets];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
         
                    var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                    string ColspanHeader = "A{0}:I{0}";
                    string textHeader_reports = "รายงานสรุปยอดค้างชำระเงิน";
                    int rowsStart = 1;

                    SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, textHeader_reports, 14, ExcelHorizontalAlignment.Center);

                    List<TR_OutstandingBalance> data = GetOutstandingBalances(search, userData);

                    var f_year = jabJaiEntities.TYears.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.nYear == search.YearId);
                    var setting = jabJaiEntities.TBehaviorSettings.Where(w => w.SchoolID == tCompany.nCompany).FirstOrDefault();
                    //SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, "ปีการศึกษา : " + f_year.numberYear, null, ExcelHorizontalAlignment.Right);

                    SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, "พิมพ์วันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")) + " เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Left);

                    rowsStart += 1;
                    foreach (var StudentData in data)
                    {
                        SetTableRows(worksheet.Cells[rowsStart, 1], true, "รหัสนักเรียน :", ExcelHorizontalAlignment.Right);
                        SetTableRows(worksheet.Cells[string.Format("B{0}:E{0}", rowsStart++)], true, StudentData.sStudentID, ExcelHorizontalAlignment.Left);

                        SetTableRows(worksheet.Cells[rowsStart, 1], true, "ชื่อ-นามสกุล :", ExcelHorizontalAlignment.Right);
                        SetTableRows(worksheet.Cells[string.Format("B{0}:E{0}", rowsStart++)], true, StudentData.StudentName, ExcelHorizontalAlignment.Left);

                        SetTableRows(worksheet.Cells[rowsStart, 1], true, "ชั้นเรียน :", ExcelHorizontalAlignment.Right);
                        SetTableRows(worksheet.Cells[string.Format("B{0}:E{0}", rowsStart++)], true, StudentData.SubLevel + " / " + StudentData.nTSubLevel2, ExcelHorizontalAlignment.Left);

                        string[] strHeader = { "ปี:เทอม", "เลขที่ใบแจ้งหนี้	", "ลำดับ", "รายการค่าธรรมเนียม", "จำนวนเงิน" };
                        int Columuns = 1;
                        foreach (string str in strHeader)
                        {
                            SetTableHeader(worksheet.Cells[rowsStart, Columuns++], false, str, ExcelHorizontalAlignment.Center);
                        }

                        rowsStart += 1;
                        int _rowIndex = 1;
                        decimal Fb_Discount = 0;
                        foreach (var TermData in StudentData.term_Datas)
                        {
                            SetTableRows(worksheet.Cells[rowsStart, 1, rowsStart + TermData.TermRowsNumber - 1, 1], true, TermData.Term + "/" + TermData.TermYear, ExcelHorizontalAlignment.Center);

                            foreach (var InvoicesData in TermData.invoices_Data)
                            {
                                SetTableRows(worksheet.Cells[rowsStart, 2, rowsStart + InvoicesData.InvoicesRowsNumber - 1, 2], true, InvoicesData.Invoices_Code, ExcelHorizontalAlignment.Center);
                                foreach (var InvoicesItem in InvoicesData.invoices_Items)
                                {
                                    SetTableRows(worksheet.Cells[rowsStart, 3], true, string.Format("{0}.", _rowIndex++), ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[rowsStart, 4], true, InvoicesItem.sPayment, ExcelHorizontalAlignment.Left);
                                    SetTableRows(worksheet.Cells[rowsStart, 5], true, string.Format("{0:#,0.00}", InvoicesItem.GrandTotal), ExcelHorizontalAlignment.Right);
                                    rowsStart += 1;

                                }

                                Fb_Discount += InvoicesData.Discount;
                            }
                        }

                        SetTableRows(worksheet.Cells[string.Format("A{0}:D{0}", rowsStart)], true, "รวม", ExcelHorizontalAlignment.Right);
                        SetTableRows(worksheet.Cells[rowsStart++, 5], true, string.Format("{0:#,0.00}", StudentData.Fb_GrandTotal), ExcelHorizontalAlignment.Right);

                        SetTableRows(worksheet.Cells[string.Format("A{0}:D{0}", rowsStart)], true, "ส่วนลด", ExcelHorizontalAlignment.Right);
                        SetTableRows(worksheet.Cells[rowsStart++, 5], true, string.Format("{0:#,0.00}", Fb_Discount), ExcelHorizontalAlignment.Right);

                        SetTableRows(worksheet.Cells[string.Format("A{0}:D{0}", rowsStart)], true, "รับชำระแล้ว", ExcelHorizontalAlignment.Right);
                        SetTableRows(worksheet.Cells[rowsStart++, 5], true, string.Format("{0:#,0.00}", StudentData.Fb_PaymentAmount ?? 0), ExcelHorizontalAlignment.Right);

                        SetTableRows(worksheet.Cells[string.Format("A{0}:D{0}", rowsStart)], true, "ยอดหนี้คงเหลือ", ExcelHorizontalAlignment.Right);
                        SetTableRows(worksheet.Cells[rowsStart++, 5], true, string.Format("{0:#,0.00}", StudentData.Fb_OutstandingAmount ?? 0), ExcelHorizontalAlignment.Right);
                        rowsStart += 1;

                    }

                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(1).Width = 13;
                    worksheet.Column(2).Width = 18;
                    worksheet.Column(3).Width = 11;
                    worksheet.Column(4).Width = 45;
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
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            }
        }

        #region Export PDF
        // Bold
        private static BaseFont bf_bold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew_bold-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private static Font h1 = new Font(bf_bold, 14);
        private static Font bold = new Font(bf_bold, 12);
        private static Font smallBold = new Font(bf_bold, 8);

        // Normal
        private static BaseFont bf_normal = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private static Font normal = new Font(bf_normal, 10);
        private static Font smallNormal = new Font(bf_normal, 8);
        private static Font small = new Font(bf_normal, 8, 0, new BaseColor(System.Drawing.Color.White));
        private static Font smallNormal_Black = new Font(bf_normal, 8, 0, new BaseColor(System.Drawing.Color.Black));
        private static Font Header_smallNormal = new Font(bf_normal, 12, 0 /*,new BaseColor(255, 255, 255)*/);

        [ScriptMethod()]
        [WebMethod]
        public static void ExportPDF(ISearch search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                List<TR_OutstandingBalance> data = GetOutstandingBalances(search, userData);

                Document pdfDoc = new Document(PageSize.A4, 10, 5, 20, 50);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);

                pdfDoc.AddAuthor("Me");
                pdfDoc.Open();

                PdfPCell TableCell;
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                float[] WidthPercentage = new float[] { 10, 20, 10, 49, 10 };
                table.SetTotalWidth(WidthPercentage);

       
                var tCompany = db.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                TableCell = new PdfPCell(new Phrase(tCompany.sCompany, h1));
                TableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                TableCell.Border = Rectangle.NO_BORDER;
                TableCell.Colspan = 5;
                table.AddCell(TableCell);

                TableCell = new PdfPCell(new Phrase("รายงานสรุปยอดค้างชำระเงิน", h1));
                TableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                TableCell.Border = Rectangle.NO_BORDER;
                TableCell.Colspan = 5;
                table.AddCell(TableCell);

                TableCell = new PdfPCell(new Phrase("พิมพ์วันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")) + " เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), smallNormal));
                TableCell.HorizontalAlignment = Element.ALIGN_LEFT;
                TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                TableCell.Border = Rectangle.NO_BORDER;
                TableCell.Colspan = 5;
                table.AddCell(TableCell);

                TableCell = new PdfPCell(new Phrase(" ", smallNormal));
                TableCell.HorizontalAlignment = Element.ALIGN_LEFT;
                TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                TableCell.Border = Rectangle.NO_BORDER;
                TableCell.Colspan = 5;
                table.AddCell(TableCell);

                pdfDoc.Add(table);

                foreach (var studentData in data)
                {
                    table = new PdfPTable(5);
                    table.WidthPercentage = 100;
                    WidthPercentage = new float[] { 10, 20, 10, 49, 10 };
                    table.SetTotalWidth(WidthPercentage);

                    string[] HeaderText = { "ปี:เทอม", "เลขที่ใบแจ้งหนี้", "ลำดับ", "รายการค่าธรรมเนียม", "จำนวนเงิน" };
                    decimal? Discount = 0;

                    TableCell = new PdfPCell(new Phrase("", smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Border = Rectangle.NO_BORDER;
                    TableCell.Colspan = 5;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase("รหัสนักเรียน : ", smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase(studentData.sStudentID, smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Border = Rectangle.NO_BORDER;
                    TableCell.Colspan = 4;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase("ชื่อ-นามสกุล : ", smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase(studentData.StudentName, smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Border = Rectangle.NO_BORDER;
                    TableCell.Colspan = 4;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase("ชั้นเรียน : ", smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Border = Rectangle.NO_BORDER;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase(studentData.SubLevel + " / " + studentData.nTSubLevel2, smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Border = Rectangle.NO_BORDER;
                    TableCell.Colspan = 4;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase(" ", smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Border = Rectangle.NO_BORDER;
                    TableCell.Colspan = 5;
                    table.AddCell(TableCell);

                    foreach (var _text in HeaderText)
                    {
                        TableCell = new PdfPCell(new Phrase(_text, small));
                        TableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        TableCell.BackgroundColor = new BaseColor(0, 51, 122, 183);
                        table.AddCell(TableCell);
                    }

                    int invoices_ItemIndex = 1;
                    for (int j = 0; studentData.term_Datas.Count() > j; j++)
                    {
                        var TremData = studentData.term_Datas[j];
                        TableCell = new PdfPCell(new Phrase(TremData.TermYear + " / " + TremData.Term, smallNormal));
                        TableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        TableCell.Rowspan = TremData.TermRowsNumber;
                        TableCell.Border = Rectangle.ALIGN_BASELINE;

                        table.AddCell(TableCell);

                        for (int i = 0; TremData.invoices_Data.Count() > i; i++)
                        {
                            var invoices_Data = TremData.invoices_Data[i];
                            //if (i == 0)
                            //{
                            TableCell = new PdfPCell(new Phrase(invoices_Data.Invoices_Code, smallNormal));
                            TableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            TableCell.Rowspan = invoices_Data.InvoicesRowsNumber;
                            TableCell.Border = Rectangle.ALIGN_BASELINE;
                            table.AddCell(TableCell);
                            //}

                            Discount += invoices_Data.Discount;
                            foreach (var invoices_Item in invoices_Data.invoices_Items)
                            {
                                table.AddCell(AddCell(new PdfPCell(new Phrase(invoices_ItemIndex.ToString(), smallBold)), Element.ALIGN_CENTER));
                                table.AddCell(AddCell(new PdfPCell(new Phrase(invoices_Item.sPayment, smallBold)), Element.ALIGN_LEFT));
                                table.AddCell(AddCell(new PdfPCell(new Phrase((invoices_Item.GrandTotal ?? 0).ToString("#,#0.00"), smallBold)), Element.ALIGN_RIGHT));

                                invoices_ItemIndex += 1;
                            }
                        }
                    }

                    TableCell = new PdfPCell(new Phrase("รวม", smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Colspan = 4;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase((studentData.Fb_GrandTotal ?? 0).ToString("#,#0.00"), smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase("ส่วนลด", smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Colspan = 4;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase((Discount ?? 0).ToString("#,#0.00"), smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase("รับชำระแล้ว", smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Colspan = 4;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase((studentData.Fb_PaymentAmount ?? 0).ToString("#,#0.00"), smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase("ยอดหนี้คงเหลือ", smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    TableCell.Colspan = 4;
                    table.AddCell(TableCell);

                    TableCell = new PdfPCell(new Phrase((studentData.Fb_OutstandingAmount ?? 0).ToString("#,#0.00"), smallNormal));
                    TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(TableCell);

                    pdfDoc.Add(table);
                    pdfDoc.NewPage();
                }

                pdfDoc.Close();

                // Create PDF document
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

            }
        }

        private static PdfPCell AddCell(PdfPCell pdfPCell, int horizontalAlignment)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //TableCell.Border = Rectangle.ALIGN_BASELINE;
            return TableCell;
        }

        private PdfPCell AddCell(PdfPCell pdfPCell, int horizontalAlignment, int colspan, int rowspan)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.Rowspan = rowspan;
            TableCell.Colspan = colspan;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            TableCell.Border = Rectangle.NO_BORDER;
            return TableCell;
        }

        private PdfPCell AddCellHeader(PdfPCell pdfPCell, int horizontalAlignment, int colspan, int rowspan)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.Rowspan = rowspan;
            TableCell.Colspan = colspan;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.Border = Rectangle.NO_BORDER;
            return TableCell;
        }

        #endregion

        [WebMethod]
        public static List<StudentList> GetStudentName(string q, string termID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname AS User_Name, sID AS User_Id
FROM TB_StudentViews
WHERE (cDel IS NULL OR cDel = 'G') AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%') AND nTerm='{2}'
GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, q, termID);
                List<StudentList> result = dbschool.Database.SqlQuery<StudentList>(sqlQuery).ToList();

                return result;
            }
        }

        public class StudentList
        {
            public string User_Name { get; set; }
            public int User_Id { get; set; }
        }

        public class ISearch
        {
            public string TermId { get; set; }
            public string SubLevel2Id { get; set; }
            public string StudentCode { get; set; }
            public int YearId { get; set; }
            public string keyword { get; set; }
        }

        public class TB_OutstandingBalance
        {
            public string StudentName { get; set; }
            public string code { get; set; }
            public string AccountingId { get; set; }
            public string sPayment { get; set; }
            public decimal? OutstandingAmount { get; set; }
            public decimal? GrandTotal { get; set; }
            public string Term { get; set; }
            public string TermYear { get; set; }
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public string sStudentID { get; set; }
            public decimal? Fb_GrandTotal { get; set; }
            public decimal? Fb_OutstandingAmount { get; set; }
            public decimal? Fb_PaymentAmount { get; set; }
            public decimal Fb_Discount { get; set; }
        }

        public class TR_OutstandingBalance
        {
            public string StudentName { get; set; }
            public string code { get; set; }
            public string AccountingId { get; set; }
            public string sPayment { get; set; }
            public decimal? OutstandingAmount { get; set; }
            public decimal? GrandTotal { get; set; }
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public string sStudentID { get; set; }

            public decimal? Discount
            {
                get
                {
                    decimal _OutstandingAmount = 0;
                    foreach (var TremData in this.term_Datas)
                    {
                        foreach (var _InvoicesData in TremData.invoices_Data)
                        {
                            _OutstandingAmount += _InvoicesData.Discount;
                        }
                    }

                    return _OutstandingAmount;
                }
            }
            public decimal? Fb_GrandTotal
            {
                get
                {
                    decimal _OutstandingAmount = 0;
                    foreach (var TremData in this.term_Datas)
                    {
                        foreach (var _InvoicesData in TremData.invoices_Data)
                        {
                            _OutstandingAmount += (_InvoicesData.GrandTotal ?? 0);
                        }
                    }

                    return _OutstandingAmount;
                }
            }

            public decimal? Fb_OutstandingAmount
            {
                get
                {
                    decimal _OutstandingAmount = 0;
                    foreach (var TremData in this.term_Datas)
                    {
                        foreach (var _InvoicesData in TremData.invoices_Data)
                        {
                            _OutstandingAmount += _InvoicesData.OutstandingAmount - _InvoicesData.Discount;
                        }
                    }

                    return _OutstandingAmount;
                }
            }
            public decimal? Fb_PaymentAmount
            {
                get
                {
                    decimal _PaymentAmount = 0;
                    foreach (var TremData in this.term_Datas)
                    {
                        foreach (var _InvoicesData in TremData.invoices_Data)
                        {
                            _PaymentAmount += (_InvoicesData.GrandTotal ?? 0) - _InvoicesData.OutstandingAmount;
                        }
                    }

                    return _PaymentAmount;
                }
            }

            public List<Term_Data> term_Datas { get; set; }
            public class Term_Data
            {
                public string TermYear { get; set; }
                public string Term { get; set; }
                public int TermRowsNumber { get; set; }
                public decimal Discount { get; set; }

                public List<Invoices_Data> invoices_Data { get; set; }
                //public List<Invoices_Item> invoices_Items { get; set; }

                public class Invoices_Item
                {
                    public string sPayment { get; set; }
                    public decimal? OutstandingAmount { get; set; }
                    public string AccountingId { get; set; }
                    public decimal? GrandTotal { get; set; }
                }

                public class Invoices_Data
                {
                    public string Invoices_Code { get; set; }
                    public List<Invoices_Item> invoices_Items { get; set; }
                    public int InvoicesRowsNumber { get; set; }
                    public decimal Discount { get; set; }
                    public decimal OutstandingAmount { get; set; }
                    public decimal? GrandTotal { get; set; }
                }
            }
        }
    }
}