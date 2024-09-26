using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JabjaiMainClass;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.Globalization;
using Newtonsoft.Json;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.StudentInfo.Ashx
{
    /// <summary>
    /// Summary description for ExportStudentResignationReportPDF
    /// </summary>
    public class ExportStudentResignationReportPDF : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("content-disposition", string.Format(@"attachment; filename=StudentResignationReport {0}.pdf", DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("th-TH"))));

            string startDate = (string)context.Request["startDate"];
            string endDate = (string)context.Request["endDate"];

            HeaderReport headerReport = new HeaderReport();
            headerReport.ReportTitle = "รายงานนักเรียนลาออก";

            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                throw new Exception();
            }

            // Load Data
            List<EntityStudentResignationReport> reportData = new List<EntityStudentResignationReport>();

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionBySchoolId(userData.CompanyID.ToString(), ConnectionDB.Read)))
            {
                string sqlCondition = "";

                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                {
                    bool bStartDate = DateTime.TryParseExact(startDate.Trim(), "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime dStartDate);
                    bool bEndDate = DateTime.TryParseExact(endDate.Trim(), "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime dEndDate);

                    if (bStartDate && bEndDate)
                    {
                        headerReport.StartDate = startDate.Trim();
                        headerReport.EndDate = endDate.Trim();

                        sqlCondition += string.Format(@" AND A.DropOutDate BETWEEN '{0} 00:00:00' AND '{1} 23:59:59'", dStartDate.ToString("yyyy-MM-dd"), dEndDate.ToString("yyyy-MM-dd"));
                    }
                }

                string sqlQueryFilter = string.Format(@"
SELECT ROW_NUMBER() OVER(ORDER BY T.numberYear, T.sTerm, T.Level, T.ClassRoom, T.No) AS RowNumber, T.*
FROM (
    SELECT A.SubLevel 'Level', A.nTSubLevel2 'ClassRoom', A.GroupNo, A.sStudentID 'StudentID', A.Fullname, A.numberYear, A.nTerm, B.CurriculumName, B.PlanName, A.DropOutDate, A.Note, A.sTerm
    , ROW_NUMBER()OVER(PARTITION BY A.numberYear, A.sTerm, A.SubLevel, A.nTSubLevel2 ORDER BY A.sStudentID ASC) 'No', A.SubLevel+' / '+A.nTSubLevel2 'LevelClassRoom', CAST(A.numberYear AS VARCHAR(10))+' / '+A.nTerm+' / '+ISNULL(B.CurriculumName, '')+' / '+ISNULL(B.PlanName, '') 'YearTermCurriculumPlan'
    --, A.SchoolID, A.sID, A.nTermSubLevel2, A.nStudentStatus, A.sTerm, A.MoveOutDate, A.DayQuit
    FROM
    (
	    SELECT sch.SchoolID, sch.sID, sch.nTerm, sch.nTermSubLevel2, sch.nStudentStatus, sch.MoveOutDate, t.sTerm, y.numberYear
	    , ROW_NUMBER()OVER(PARTITION BY sch.sID ORDER BY y.numberYear ASC, t.sTerm ASC) 'GroupNo'
	    , u.DayQuit, (CASE WHEN sch.MoveOutDate IS NULL THEN u.DayQuit ELSE sch.MoveOutDate END) 'DropOutDate', u.Note
	    , u.sStudentID, ISNULL(tl.titleDescription, u.sStudentTitle)+u.sName+' '+u.sLastname 'Fullname', sl.SubLevel, tsl.nTSubLevel2
	    FROM TStudentClassroomHistory sch 
	    LEFT JOIN TTerm t ON sch.SchoolID=t.SchoolID AND sch.nTerm=t.nTerm
	    LEFT JOIN TYear y ON t.SchoolID=y.SchoolID AND t.nYear=y.nYear
	    LEFT JOIN TUser u ON sch.SchoolID=u.SchoolID AND sch.sID=u.sID
	    LEFT JOIN TTitleList tl ON u.SchoolID=tl.SchoolID AND u.sStudentTitle=CAST(tl.nTitleid AS VARCHAR(10))
	    LEFT JOIN TTermSubLevel2 tsl ON sch.SchoolID=tsl.SchoolID AND sch.nTermSubLevel2=tsl.nTermSubLevel2
	    LEFT JOIN TSubLevel sl ON tsl.SchoolID=sl.SchoolID AND tsl.nTSubLevel=sl.nTSubLevel
	    WHERE sch.SchoolID={0} AND sch.nStudentStatus=2 AND ISNULL(sch.cDel, 0)=0
    ) A 
    LEFT JOIN 
    (
	    SELECT c.SchoolId, t.nTerm, ptsl.nTermSubLevel2, c.CurriculumName, p.PlanName
	    FROM TCurriculum c
	    INNER JOIN TPlan p ON c.CurriculumId = p.CurriculumId AND c.SchoolId = p.SchoolID
	    INNER JOIN TPlanTermSubLevel2 ptsl ON p.PlanId = ptsl.PlanId AND p.SchoolID = ptsl.SchoolID
	    INNER JOIN TTerm t ON t.nYear = c.nYear
	    WHERE c.SchoolId={0} AND ISNULL(c.cDel, 0)=0 AND ISNULL(p.cDel, 0)=0 AND ISNULL(ptsl.cDel, 0)=0
    ) B ON A.SchoolID=B.SchoolId AND A.nTerm=B.nTerm AND A.nTermSubLevel2=B.nTermSubLevel2
    WHERE A.GroupNo = 1 {1}
    --ORDER BY A.numberYear, A.sTerm, A.SubLevel, A.nTSubLevel2
) AS T", userData.CompanyID, sqlCondition);

                reportData = en.Database.SqlQuery<EntityStudentResignationReport>(sqlQueryFilter).ToList();
            }

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var schoolObj = dbMaster.TCompanies.Where(w => w.nCompany == userData.CompanyID).FirstOrDefault();
                if (schoolObj != null)
                {
                    headerReport.SchoolLogoUrl = schoolObj.sImage;
                    headerReport.SchoolName = schoolObj.sCompany;
                }
            }

            // Bold
            BaseFont baseFontBold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew_bold-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font14Bold = new Font(baseFontBold, 14);
            Font font10Bold = new Font(baseFontBold, 10);
            Font font9Bold = new Font(baseFontBold, 9);

            BaseFont baseFontItalic = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew_italic-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font9Italic = new Font(baseFontItalic, 9);

            // Normal
            BaseFont baseFont = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font10 = new Font(baseFont, 10);
            Font font9 = new Font(baseFont, 9);
            Font font8 = new Font(baseFont, 8);

            Document pdfDoc = new Document(PageSize.A4, 15, 15, 20, 50);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, context.Response.OutputStream);

            ITextEvents e = new ITextEvents();
            pdfWriter.PageEvent = e;

            pdfDoc.AddAuthor("School Bright");

            pdfDoc.Open();

            Paragraph blank = new Paragraph(" ", font10);

            // Render Data

            // Set width column
            float[] widthPDFHeaderPercentage = new float[] { 12, 35, 4, 37, 12 };
            float[] widthPDFTablePercentage = new float[] { 5f, 12f, 23f, 30f, 10f, 20f };

            // School Logo
            Image schoolLogo = Image.GetInstance(headerReport.SchoolLogoUrl);
            schoolLogo.ScaleToFit(256, 74);
            schoolLogo.SetAbsolutePosition(20, pdfDoc.PageSize.Height - 94);


            // Generate PDF Table
            PdfPTable pdfTable = new PdfPTable(6);

            int pageIdx = 1;
            int numberPerPage = 33;
            int rowIdx = 1;
            int rowNumber = 1;

            string level = "";
            string classRoom = "";

            foreach (var r in reportData)
            {
                if (rowIdx == 1)
                {
                    pdfDoc.Add(schoolLogo);

                    pdfDoc.Add(blank);

                    headerReport.Page = pageIdx;

                    pdfDoc.Add(GetHeaderReport(5, widthPDFHeaderPercentage, headerReport, font14Bold, font10Bold, font9Italic));

                    // Generate Header Table
                    pdfTable = GetHeaderCellTable(6, widthPDFTablePercentage, font10Bold);

                    pageIdx++;
                }

                // Insert Level/Classroom
                if (level != r.Level?.ToString() || classRoom != r.ClassRoom?.ToString())
                {
                    // Level / Classroom Row
                    pdfTable.AddCell(SetCell(new CellProperty { Text = "", Font = font9, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2, Height = 15f }));

                    pdfTable.AddCell(SetCell(new CellProperty { Text = string.Format(@"ชั้น : {0}   ห้อง : {1}", r.Level, r.ClassRoom), Font = font9, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 4, Height = 15f }));

                    level = r.Level;
                    classRoom = r.ClassRoom;

                    rowIdx++;
                }

                pdfTable.AddCell(SetCell(new CellProperty { Text = r.No.ToString(), Font = font9, HorizontalAlignment = Element.ALIGN_CENTER, Height = 18f }));
                pdfTable.AddCell(SetCell(new CellProperty { Text = r.StudentID, Font = font9, HorizontalAlignment = Element.ALIGN_CENTER, Height = 18f }));
                pdfTable.AddCell(SetCell(new CellProperty { Text = r.Fullname, Font = font9, HorizontalAlignment = Element.ALIGN_LEFT, Height = 18f }));
                pdfTable.AddCell(SetCell(new CellProperty { Text = r.YearTermCurriculumPlan, Font = font9, HorizontalAlignment = Element.ALIGN_RIGHT, Height = 18f }));
                pdfTable.AddCell(SetCell(new CellProperty { Text = (r.DropOutDate == null ? "" : r.DropOutDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))), Font = font9, HorizontalAlignment = Element.ALIGN_CENTER, Height = 18f }));
                pdfTable.AddCell(SetCell(new CellProperty { Text = r.Note, Font = font9, HorizontalAlignment = Element.ALIGN_LEFT, Height = 18f }));

                if (rowIdx >= numberPerPage || rowNumber >= reportData.Count)
                {
                    if (rowNumber >= reportData.Count)
                    {
                        // Insert Footer
                        pdfTable.AddCell(SetCell(new CellProperty { Text = "รวมทั้งหมด", Font = font9Bold, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3, Height = 22f }));
                        pdfTable.AddCell(SetCell(new CellProperty { Text = reportData.Count.ToString(), Font = font9Bold, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
                        pdfTable.AddCell(SetCell(new CellProperty { Text = "คน", Font = font9Bold, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2, Height = 22f }));

                        pdfTable.AddCell(SetCell(new CellProperty { Text = "จบรายงาน", Font = font9Bold, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 6, Height = 22f }));
                    }

                    pdfDoc.Add(pdfTable);

                    pdfDoc.NewPage();

                    rowIdx = 0;
                }

                rowIdx++;
                rowNumber++;
            }

            if (reportData.Count == 0)
            {
                pdfDoc.Add(new Paragraph("No Data!"));
            }

            pdfDoc.Close();

            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Write(pdfDoc);
            context.Response.Flush(); // Sends all currently buffered output to the client.
            context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            context.Response.End();
        }

        public bool IsReusable { get { return false; } }

        private PdfPTable GetHeaderReport(int allColumn, float[] widthPDFHeaderPercentage, HeaderReport headerReport, Font font14Bold, Font font10Bold, Font font9Italic)
        {
            // Generate PDF Header
            PdfPTable headerTable = new PdfPTable(allColumn);
            headerTable.WidthPercentage = 100;
            headerTable.HorizontalAlignment = 0;
            headerTable.SpacingAfter = 10;

            headerTable.SetTotalWidth(widthPDFHeaderPercentage);

            headerTable.AddCell(SetCell(new CellProperty { Text = "", Font = font10Bold, HorizontalAlignment = Element.ALIGN_LEFT, Rowspan = 4 }));
            headerTable.AddCell(SetCell(new CellProperty { Text = headerReport.SchoolName, Font = font10Bold, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 3, Height = 21f }));
            headerTable.AddCell(SetCell(new CellProperty { Text = "REP#: RU16", Font = font9Italic, HorizontalAlignment = Element.ALIGN_LEFT, Height = 21f }));

            headerTable.AddCell(SetCell(new CellProperty { Text = headerReport.ReportTitle, Font = font14Bold, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 3, Height = 22f }));
            headerTable.AddCell(SetCell(new CellProperty { Text = string.Format(@"หน้า {0}", headerReport.Page), Font = font9Italic, HorizontalAlignment = Element.ALIGN_LEFT, Height = 22f }));

            headerTable.AddCell(SetCell(new CellProperty { Text = string.Format(@"ตั้งแต่วันที่ {0}   ถึงวันที่ {1}", string.IsNullOrEmpty(headerReport.StartDate) ? "-" : headerReport.StartDate, string.IsNullOrEmpty(headerReport.EndDate) ? "-" : headerReport.EndDate), Font = font10Bold, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 3, Height = 21f }));
            headerTable.AddCell(SetCell(new CellProperty { Text = "", Font = font9Italic, HorizontalAlignment = Element.ALIGN_LEFT, Height = 22f }));

            return headerTable;
        }

        private PdfPTable GetHeaderCellTable(int allColumn, float[] widthPDFTablePercentage, Font font10Bold)
        {
            PdfPTable headerCellTable = new PdfPTable(allColumn);
            headerCellTable.WidthPercentage = 100;
            headerCellTable.HorizontalAlignment = 0;
            headerCellTable.SpacingAfter = 10;

            headerCellTable.SetTotalWidth(widthPDFTablePercentage);

            // Generate Header Table
            headerCellTable.AddCell(SetCell(new CellProperty { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, Text = "ลำดับ", Font = font10Bold, HorizontalAlignment = Element.ALIGN_CENTER, Height = 25f }));
            headerCellTable.AddCell(SetCell(new CellProperty { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, Text = "เลขประจำตัว", Font = font10Bold, HorizontalAlignment = Element.ALIGN_CENTER, Height = 25f }));
            headerCellTable.AddCell(SetCell(new CellProperty { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, Text = "ชื่อ", Font = font10Bold, HorizontalAlignment = Element.ALIGN_CENTER, Height = 25f }));
            headerCellTable.AddCell(SetCell(new CellProperty { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, Text = "ปีการศึกษา / ภาคเรียน / แผนก / แผนที่ลาออก", Font = font10Bold, HorizontalAlignment = Element.ALIGN_CENTER, Height = 25f }));
            headerCellTable.AddCell(SetCell(new CellProperty { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, Text = "วันที่ลาออก", Font = font10Bold, HorizontalAlignment = Element.ALIGN_CENTER, Height = 25f }));
            headerCellTable.AddCell(SetCell(new CellProperty { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, Text = "สาเหตุที่ลาออก", Font = font10Bold, HorizontalAlignment = Element.ALIGN_CENTER, Height = 25f }));

            return headerCellTable;
        }

        private PdfPCell SetCell(CellProperty property)
        {
            PdfPCell tableCell = new PdfPCell(new Phrase(property.Text, property.Font));
            tableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            tableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_MIDDLE;
            tableCell.Border = property.Border ?? Rectangle.NO_BORDER;
            tableCell.Colspan = property.Colspan ?? 1;
            tableCell.Rowspan = property.Rowspan ?? 1;
            tableCell.Rotation = property.Rotation ?? 0;

            if (property.Height != null) tableCell.FixedHeight = property.Height.Value;
            if (property.PaddingLeft != null) tableCell.PaddingLeft = property.PaddingLeft.Value;
            if (property.PaddingBottom != null) tableCell.PaddingBottom = property.PaddingBottom.Value;

            if (property.BackgroundColor != null) tableCell.BackgroundColor = new BaseColor(property.BackgroundColor.Red, property.BackgroundColor.Green, property.BackgroundColor.Blue);

            return tableCell;
        }

        public class CellProperty
        {
            public string Text { get; set; }
            public Font Font { get; set; }
            public int? Rowspan { get; set; }
            public int? Colspan { get; set; }
            public int? HorizontalAlignment { get; set; }
            public int? VerticalAlignment { get; set; }
            public int? Border { get; set; }
            public int? Rotation { get; set; }
            public float? Height { get; set; }
            public float? PaddingLeft { get; set; }
            public float? PaddingBottom { get; set; }
            public Color BackgroundColor { get; set; }
            public class Color
            {
                public int Red { get; set; }
                public int Green { get; set; }
                public int Blue { get; set; }
            }
        }

        public class HeaderReport
        {
            public string SchoolLogoUrl { get; set; }
            public string SchoolName { get; set; }
            public string ReportTitle { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public int Page { get; set; }
        }
    }

}