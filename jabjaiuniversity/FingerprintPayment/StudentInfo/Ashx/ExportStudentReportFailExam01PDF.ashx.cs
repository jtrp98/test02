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
    /// Summary description for ExportStudentReportFailExam01PDF
    /// </summary>
    public class ExportStudentReportFailExam01PDF : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("content-disposition", string.Format(@"attachment; filename=StudentReportFailExam01 {0}.pdf", DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("th-TH"))));

            int yearID = int.Parse(string.IsNullOrEmpty(context.Request["year"]) ? "0" : context.Request["year"]);
            string termID = (string)context.Request["term"];
            string grade = (string)context.Request["grade"];

            var reportTitle = "รายชื่อนักเรียนขาดสอบ/ร/มส./มผ.";
            if (!string.IsNullOrEmpty(grade))
            {
                string[] gradeList = grade.Remove(0, 1).Split(',');
                reportTitle = "รายชื่อนักเรียน";
                for (int i = 0; i < gradeList.Length; i++)
                {
                    switch (gradeList[i])
                    {
                        case "0": reportTitle += "สอบตก"; break;
                        case "ร": reportTitle += (i == 0 ? "ติด" : "/") + gradeList[i]; break;
                        case "มส":
                        case "มผ": reportTitle += (i == 0 ? "ติด" : "/") + gradeList[i] + "."; break;
                    }
                }
            }

            HeaderReport headerReport = new HeaderReport();
            headerReport.ReportTitle = reportTitle;

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
            List<EntityStudentReportFailExam01> reportData = new List<EntityStudentReportFailExam01>();

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionBySchoolId(userData.CompanyID.ToString())))
            {
                var yearObj = en.TYears.Where(w => w.SchoolID == userData.CompanyID && w.nYear == yearID).FirstOrDefault();
                if (yearObj != null)
                {
                    headerReport.Year = yearObj.numberYear.Value;
                }

                var termObj = en.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == termID).FirstOrDefault();
                if (termObj != null)
                {
                    headerReport.Term = termObj.sTerm;
                }

                string sqlCondition = "";
                string sqlCondition2 = "";
                string sqlCondition3 = "";

                //if (yearID != 0) { sqlCondition += string.Format(@" AND y.nYear = {0}", yearID); }
                if (!string.IsNullOrEmpty(termID)) { sqlCondition += string.Format(@" AND nTerm = '{0}'", termID); sqlCondition2 = string.Format(@" AND g.nTerm = '{0}'", termID); }
                if (!string.IsNullOrEmpty(grade))
                {
                    sqlCondition3 = string.Format(@" AND gd.getGradeLabel IN ('{0}') ", grade.Remove(0, 1).Replace(",", "','"));
                }
                else
                {
                    sqlCondition3 = " AND gd.getGradeLabel IN ('0', 'ร', 'มส', 'มผ') ";
                }

                string sqlQueryFilter = string.Format(@"
SELECT ROW_NUMBER() OVER(ORDER BY Level, ClassRoom, StudentID) AS RowNumber, DENSE_RANK() OVER(PARTITION BY Level, ClassRoom ORDER BY Level, ClassRoom, StudentID) AS [No], T.*
FROM (
    SELECT gd.sID, sv.sStudentID 'StudentID', sv.SubLevel 'Level', sv.nTSubLevel2 'ClassRoom', sv.sStudentID+'-'+sv.titleDescription+sv.sName+' '+sv.sLastname 'Name'
	, (CASE WHEN p.courseCode IS NOT NULL AND p.courseCode <> '' THEN p.courseCode+'' ELSE '' END)+'-'+p.sPlaneName+N'/ผล-'+gd.getGradeLabel 'SubjectResultGrade'
    , p.courseCode 'CourseCode'
	--, sv.numberYear, sv.sTerm, sv.nTerm
	--, sv.sStudentID, sv.nTSubLevel, sv.nTermSubLevel2, sv.numberYear, sv.sTerm, sv.nTerm, g.sPlaneID
	FROM TGradeDetail gd 
	LEFT JOIN TGrade g ON gd.SchoolID = g.SchoolID AND gd.nGradeId = g.nGradeId AND g.cDel=0
	LEFT JOIN TB_StudentViews sv ON gd.SchoolID = sv.SchoolID AND gd.sID = sv.sID AND g.nTerm = sv.nTerm
	LEFT JOIN TPlane p ON g.SchoolID = p.SchoolID AND g.sPlaneID = p.sPlaneID
	WHERE gd.SchoolID={0} AND gd.cDel=0 {3} 
	AND gd.sID IN (SELECT DISTINCT sID FROM TB_StudentViews WHERE SchoolID={0} {1}) {2}
	--ORDER BY sv.SubLevel, sv.nTSubLevel2, sv.sStudentID
) AS T
ORDER BY T.Level, T.ClassRoom, T.StudentID", userData.CompanyID, sqlCondition, sqlCondition2, sqlCondition3);

                reportData = en.Database.SqlQuery<EntityStudentReportFailExam01>(sqlQueryFilter).ToList();
            }

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities())
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
            float[] widthPDFHeaderPercentage = new float[] { 13f, 15f, 15f, 15f, 15f, 12f };
            float[] widthPDFTablePercentage = new float[] { 5f, 30f, 65f };

            // School Logo
            Image schoolLogo = Image.GetInstance(headerReport.SchoolLogoUrl);
            schoolLogo.ScaleToFit(256, 74);
            schoolLogo.SetAbsolutePosition(20, pdfDoc.PageSize.Height - 94);


            // Generate PDF Table
            PdfPTable pdfTable = new PdfPTable(7);

            int pageIdx = 1;
            int numberPerPage = 33;
            int numberAppendRow = 0;
            int rowIdx = 1;
            int rowNumber = 1;

            string level = "";
            string classRoom = "";
            string levelClassRoomLabel = "";

            string studentNo = "";
            string studentNoLabel = "";
            string studentNameLabel = "";

            foreach (var r in reportData)
            {
                if (rowIdx == 1)
                {
                    pdfDoc.Add(schoolLogo);

                    pdfDoc.Add(blank);

                    headerReport.Page = pageIdx;

                    pdfDoc.Add(GetHeaderReport(6, widthPDFHeaderPercentage, headerReport, font14Bold, font10Bold, font9Italic));

                    // Generate Header Table
                    pdfTable = GetHeaderCellTable(3, widthPDFTablePercentage, font9Bold);

                    pageIdx++;
                }

                if (level != r.Level.ToString() || classRoom != r.ClassRoom.ToString())
                {
                    level = r.Level.ToString();
                    classRoom = r.ClassRoom.ToString();

                    levelClassRoomLabel = string.Format(@"ชั้น : {0}   ห้อง : {1}", r.Level, r.ClassRoom);

                    // Blank Row
                    pdfTable.AddCell(SetCell(new CellProperty { Text = "", Font = font9, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 3, Height = 15f }));

                    pdfTable.AddCell(SetCell(new CellProperty { Text = "", Font = font9, HorizontalAlignment = Element.ALIGN_RIGHT, Height = 18f }));
                    pdfTable.AddCell(SetCell(new CellProperty { Text = levelClassRoomLabel, Font = font9Bold, HorizontalAlignment = Element.ALIGN_LEFT, Height = 18f }));
                    pdfTable.AddCell(SetCell(new CellProperty { Text = "", Font = font9, HorizontalAlignment = Element.ALIGN_LEFT, Height = 18f }));

                    studentNo = "";

                    numberAppendRow += 2;
                }

                if (studentNo != r.No.ToString())
                {
                    studentNo = r.No.ToString();
                    studentNoLabel = r.No.ToString() + ".";
                    studentNameLabel = r.Name;
                }

                pdfTable.AddCell(SetCell(new CellProperty { Text = studentNoLabel, Font = font9, HorizontalAlignment = Element.ALIGN_RIGHT, Height = 18f }));
                pdfTable.AddCell(SetCell(new CellProperty { Text = studentNameLabel, Font = font9, HorizontalAlignment = Element.ALIGN_LEFT, Height = 18f }));
                pdfTable.AddCell(SetCell(new CellProperty { Text = r.SubjectResultGrade, Font = font9, HorizontalAlignment = Element.ALIGN_LEFT, Height = 18f }));

                if ((rowIdx + numberAppendRow) >= numberPerPage || rowNumber >= reportData.Count)
                {
                    if (rowNumber >= reportData.Count)
                    {
                        int studentCount = reportData.GroupBy(g => g.sID).Count();

                        // Insert Footer
                        pdfTable.AddCell(SetCell(new CellProperty { Text = "", Font = font9Bold, HorizontalAlignment = Element.ALIGN_RIGHT, Height = 22f }));
                        pdfTable.AddCell(SetCell(new CellProperty { Text = "รวมทั้งหมด ", Font = font9Bold, HorizontalAlignment = Element.ALIGN_RIGHT, Height = 22f }));
                        pdfTable.AddCell(SetCell(new CellProperty { Text = studentCount.ToString() + " คน", Font = font9Bold, HorizontalAlignment = Element.ALIGN_LEFT, Height = 22f }));

                        // Last Row
                        pdfTable.AddCell(SetCell(new CellProperty { Text = "จบรายงาน", Font = font9Bold, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 3, Height = 22f }));
                    }

                    pdfDoc.Add(pdfTable);

                    pdfDoc.NewPage();

                    rowIdx = 0;
                    numberAppendRow = 0;
                }

                rowIdx++;
                rowNumber++;
                studentNoLabel = "";
                studentNameLabel = "";
                levelClassRoomLabel = "";
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

            headerTable.AddCell(SetCell(new CellProperty { Text = "", Font = font10Bold, HorizontalAlignment = Element.ALIGN_LEFT, Rowspan = 3 }));
            headerTable.AddCell(SetCell(new CellProperty { Text = headerReport.SchoolName, Font = font10Bold, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4, Height = 21f }));
            headerTable.AddCell(SetCell(new CellProperty { Text = "REP#: RG42", Font = font9Italic, HorizontalAlignment = Element.ALIGN_LEFT, Height = 21f }));

            headerTable.AddCell(SetCell(new CellProperty { Text = headerReport.ReportTitle, Font = font14Bold, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4, Height = 22f }));
            headerTable.AddCell(SetCell(new CellProperty { Text = string.Format(@"หน้า {0}", headerReport.Page), Font = font9Italic, HorizontalAlignment = Element.ALIGN_LEFT, Height = 22f }));

            headerTable.AddCell(SetCell(new CellProperty { Text = string.Format(@"ประจำ ภาคเรียนที่ {0}", headerReport.Term), Font = font10Bold, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2, Height = 21f }));
            headerTable.AddCell(SetCell(new CellProperty { Text = string.Format(@" ปีการศึกษา {0}", headerReport.Year), Font = font10Bold, HorizontalAlignment = Element.ALIGN_LEFT, Colspan = 2, Height = 21f }));
            headerTable.AddCell(SetCell(new CellProperty { Text = "", Font = font10Bold, HorizontalAlignment = Element.ALIGN_LEFT }));

            return headerTable;
        }

        private PdfPTable GetHeaderCellTable(int allColumn, float[] widthPDFTablePercentage, Font font9Bold)
        {
            PdfPTable headerCellTable = new PdfPTable(allColumn);
            headerCellTable.WidthPercentage = 100;
            headerCellTable.HorizontalAlignment = 0;
            headerCellTable.SpacingAfter = 10;

            headerCellTable.SetTotalWidth(widthPDFTablePercentage);

            // Generate Header Table
            headerCellTable.AddCell(SetCell(new CellProperty { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, Text = "ลำดับ", Font = font9Bold, HorizontalAlignment = Element.ALIGN_CENTER, Height = 25f }));
            headerCellTable.AddCell(SetCell(new CellProperty { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, Text = "ชื่อ-นามสกุล", Font = font9Bold, HorizontalAlignment = Element.ALIGN_LEFT, Height = 25f }));
            headerCellTable.AddCell(SetCell(new CellProperty { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER, Text = "วิชาที่ขาดสอบ", Font = font9Bold, HorizontalAlignment = Element.ALIGN_LEFT, Height = 25f }));

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
            public string Term { get; set; }
            public int Year { get; set; }
            public string Level { get; set; }
            public string ClassRoom { get; set; }
            public string Teacher { get; set; }
            public string Subject { get; set; }
            public int Page { get; set; }
        }
    }
}