using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using SchoolBright.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.Globalization;
using JabjaiSchoolGradeEntity;
using FingerprintPayment.Helper;
using SchoolBright.DTO.Parameters;

namespace FingerprintPayment.grade
{
    public partial class GradeRankingExport : System.Web.UI.Page
    {
        // Bold
        private static BaseFont bf_bold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew_bold-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private Font h1 = new Font(bf_bold, 14);
        private Font bold = new Font(bf_bold, 12);
        private Font smallBold = new Font(bf_bold, 10);

        // Normal
        private static BaseFont bf_normal = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private Font normal = new Font(bf_normal, 10);
        private Font smallNormal = new Font(bf_normal, 8);
        private Font smallNormal_Black = new Font(bf_normal, 8, 0, new BaseColor(System.Drawing.Color.Black));
        private Font Header_smallNormal = new Font(bf_normal, 8, 0 /*,new BaseColor(255, 255, 255)*/);

        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        private JWTToken.userData userData = new JWTToken.userData();
        private string txtclass2 = string.Empty;
        private string planname2 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                JWTToken token = new JWTToken();
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
                {

                    //btnExport.Click += new EventHandler(ExportToExcel_ScoreKeb);
                    //btnExport4.Click += new EventHandler(ExportToExcel_7030);

                    //btnExportOriginalScore.Click += new EventHandler(ExportToExcel_OriginalScore);
                    //btnExportOriginalScorePDF.Click += new EventHandler(ExportToPDF_OriginalScore);

                    //btnchewut.Click += new EventHandler(ExportToExcel_Chewut);
                    //btnbehavior.Click += new EventHandler(ExportToExcel_Behavior);
                    //btnReadWirte.Click += new EventHandler(ExportToExcel_ReadWirte);
                    //btnSamattana.Click += new EventHandler(ExportToExcel_Samattana);

                    //btnExportPDF.Click += new EventHandler(ExportToPDF_ScoreKeb);
                    //btnExportPDF2.Click += new EventHandler(ExportToPDF2_ScoreKeb);
                    btnExportPDF2.Click += new EventHandler(ExportToPDF2_Ranking);
                }
            }
            catch (Exception ex)
            {

            }
        }

       

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ExportToPDF2_Ranking(object sender, EventArgs e)
        {
            try
            {
                // Get parameter
                //year=2563 & idlv=1202 & idlv2=2559 & term=1 & id=73217 & mode=1 & PlanCourseId=190108 & print=8
                string nTerms = Request.QueryString["nTerms"];
                string nTermSubLevel2s = Request.QueryString["nTermSubLevel2s"];
                int nYear = int.Parse(Request.QueryString["nYear"]);
                bool IsPrimary = bool.Parse(Request.QueryString["IsPrimary"]);
                string numberYear = Request.QueryString["numberYear"];
                string sublevel = Request.QueryString["sublevel"];
                string planname = Request.QueryString["planname"];
                string sublevel2 = Request.QueryString["sublevel2"];
                string startyearterm = Request.QueryString["startyearterm"];
                string endyearterm = Request.QueryString["endyearterm"];
                GradeRequest request = new GradeRequest() {
                    nTerms = nTerms.Split(',').ToList(),
                    nTermSubLevel2s = Array.ConvertAll(nTermSubLevel2s.Split(','), int.Parse).ToList(),
                    nYear = nYear,
                    IsPrimary = IsPrimary,
                    SchoolId = userData.CompanyID
                };
                var studentRanking = ServiceHelper.GetStudentRankingByGPA(request);


                // Create PDF document
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", string.Format(@"attachment; filename=GradeRankingReport {0}.pdf", DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("th-TH"))));

                Document pdfDoc = new Document(PageSize.A4, 12, 12, 20, 50);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.AddAuthor("School Bright");

                pdfDoc.Open();

                // Set width column
                float[] widthPDFHeaderPercentage = new float[] { 50, 50 };
                float[] widthPDFTablePercentage = new float[] { 3f, 8.25f, 16.175f, 2.75f, 2.75f, 2.75f, 2.75f, 3.2f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f };

                Paragraph paragraph = new Paragraph("รายงานผลการเรียนระเบียนสะสม", h1);
                Paragraph blank = new Paragraph(" ", normal);
                paragraph.Alignment = Element.ALIGN_CENTER;

                // Generate PDF Header
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.HorizontalAlignment = 0;
                headerTable.SpacingAfter = 10;

                headerTable.SetTotalWidth(widthPDFHeaderPercentage);



                headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("ปีการศึกษา {0}", numberYear), Font = normal, HorizontalAlignment = Element.ALIGN_LEFT }));
                headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("ระดับชั้นเรียน {0}", sublevel), Font = normal, HorizontalAlignment = Element.ALIGN_RIGHT }));

                headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("แผน {0}", planname), Font = normal, HorizontalAlignment = Element.ALIGN_LEFT }));
                headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("ชั้นเรียน {0}", sublevel2), Font = normal, HorizontalAlignment = Element.ALIGN_RIGHT }));

                headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("ปีเริ่มต้น {0}", startyearterm), Font = normal, HorizontalAlignment = Element.ALIGN_LEFT }));
                headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("ปีสิ้นสุด {0}", endyearterm), Font = normal, HorizontalAlignment = Element.ALIGN_RIGHT }));



                var columnCount = studentRanking.RankingInfoColumn.Count + 2;
                // Generate PDF Table
                PdfPTable pdfTable = new PdfPTable(columnCount);

                int numberPerPage = 23;
                int rowIdx = 1;
                int rowNumber = 1;

                foreach (var ss in studentRanking.RankingInfo)
                {
                    
                    if (rowIdx == 1)
                    {
                        pdfDoc.Add(paragraph);
                        pdfDoc.Add(blank);

                        pdfDoc.Add(headerTable);

                        // Generate Header Table
                        pdfTable = GetHeaderCellTable1(columnCount, widthPDFTablePercentage, studentRanking.RankingInfoColumn);
                    }

                    pdfTable.AddCell(SetCell(new CellProperty { Text = ss.RowNumber.ToString(),Width=10, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
                    pdfTable.AddCell(SetCell(new CellProperty { Text = ss.Ranking.ToString(), Width = 10, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));

                    foreach (var c in studentRanking.RankingInfoColumn)
                    {
                        
                        var rankingInfo = new Dictionary<string, dynamic>(ss);
                        if (c == "Name")
                        {
                            pdfTable.AddCell(SetCell(new CellProperty { Text = rankingInfo[c], Width = 30, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT, Height = 22f }));
                        }
                        else
                        {
                            pdfTable.AddCell(SetCell(new CellProperty { Text = rankingInfo[c], Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
                        }
                    }


                    if (rowIdx >= numberPerPage || rowNumber >= studentRanking.RankingInfo.Count)
                    {
                        pdfDoc.Add(pdfTable);

                        pdfDoc.NewPage();

                        rowIdx = 0;
                    }

                    rowIdx++;
                    rowNumber++;
                }

                pdfDoc.Close();

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.Flush(); // Sends all currently buffered output to the client.
                Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private PdfPTable GetHeaderCellTable1(int allColumn, float[] widthPDFTablePercentage, List<string> columns)
        {
            PdfPTable headerCellTable = new PdfPTable(allColumn);
            headerCellTable.WidthPercentage = 100;
            headerCellTable.HorizontalAlignment = 0;
            headerCellTable.SpacingAfter = 10;

            float[] widthArray = new float[allColumn];
            //headerCellTable.SetTotalWidth(widthPDFTablePercentage);

            // Generate Header Table
            headerCellTable.AddCell(SetCell(new CellProperty { Text = "ลำดับ", Font = smallNormal,Width = 10, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 1 }));

            headerCellTable.AddCell(SetCell(new CellProperty { Text = "อันดับ", Font = smallNormal, Width = 10, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 1 }));
            widthArray[0] = 3;
            widthArray[1] = 3;
            var columnStart = 2;

            foreach (var c in columns)
            {
                widthArray[columnStart] = 3;
                var header = c;
                if (c == "StudentId") header = "รหัสนักเรียน";
                if (c == "Name") header = "ชื่อ";
                if (c == "FinalGPA") header = "ผลการเรียนสะสม";
                if (c == "SubLevel2") header = "ชั้นเรียน";
                if (c == "Name")
                {
                    widthArray[columnStart] = 5;
                    headerCellTable.AddCell(SetCell(new CellProperty { Text = header,Width= 30, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT, Rowspan = 1 }));
                }
                else
                {
                    headerCellTable.AddCell(SetCell(new CellProperty { Text = header, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT, Rowspan = 1 }));
                }
                columnStart++;
            }
            headerCellTable.SetTotalWidth(widthArray);
            return headerCellTable;
        }

        private PdfPCell SetCell(CellProperty property)
        {
            Phrase elements = new Phrase(property.Text, property.Font);

            PdfPCell tableCell = new PdfPCell(elements);
            tableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            tableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_MIDDLE;
            tableCell.Border = property.Border ?? Rectangle.BOX;
            tableCell.Colspan = property.Colspan ?? 1;
            tableCell.Rowspan = property.Rowspan ?? 1;
            tableCell.Rotation = property.Rotation ?? 0;
           
        
            if (property.Height != null) tableCell.FixedHeight = property.Height.Value;
            if (property.PaddingLeft != null) tableCell.PaddingLeft = property.PaddingLeft.Value;
            if (property.PaddingBottom != null) tableCell.PaddingBottom = property.PaddingBottom.Value;
            if (property.PaddingTop != null) tableCell.PaddingTop = property.PaddingTop.Value;

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
            public float? PaddingTop { get; set; }
            public Color BackgroundColor { get; set; }

            public float? Width { get; set; }
            public bool NoWrap { get; set; }
            public class Color
            {
                public int Red { get; set; }
                public int Green { get; set; }
                public int Blue { get; set; }
            }
        }

       

        class studentlist2
        {
            public int? sID { get; set; }
            public string sName { get; set; }
            public string note { get; set; }
            public bool toggleOn { get; set; }
            public string txtg1 { get; set; }
            public string txtg2 { get; set; }
            public string txtg3 { get; set; }
            public string txtg4 { get; set; }
            public string txtg5 { get; set; }
            public string txtg6 { get; set; }
            public string txtg7 { get; set; }
            public string txtg8 { get; set; }
            public string txtg9 { get; set; }
            public string txtg10 { get; set; }
            public string txtg11 { get; set; }
            public string txtg12 { get; set; }
            public string txtg13 { get; set; }
            public string txtg14 { get; set; }
            public string txtg15 { get; set; }
            public string txtg16 { get; set; }
            public string txtg17 { get; set; }
            public string txtg18 { get; set; }
            public string txtg19 { get; set; }
            public string txtg20 { get; set; }
            public string txtgmid { get; set; }
            public string txtglate { get; set; }
            public string number { get; set; }
            public string txtGoodBehavior { get; set; }
            public string txtGoodReading { get; set; }
            public string txtGoodSamattana { get; set; }
            public string txtSpecial { get; set; }
            public string txtb1 { get; set; }
            public string txtb2 { get; set; }
            public string txtb3 { get; set; }
            public string txtb4 { get; set; }
            public string txtb5 { get; set; }
            public string txtb6 { get; set; }
            public string txtb7 { get; set; }
            public string txtb8 { get; set; }
            public string txtb9 { get; set; }
            public string txtb10 { get; set; }
            public string txtchewut1 { get; set; }
            public string txtchewut2 { get; set; }
            public string txtchewut3 { get; set; }
            public string txtchewut4 { get; set; }
            public string txtchewut5 { get; set; }
            public string txtchewut6 { get; set; }
            public string txtchewut7 { get; set; }
            public string txtchewut8 { get; set; }
            public string txtchewut9 { get; set; }
            public string txtchewut10 { get; set; }
            public string txtchewut11 { get; set; }
            public string txtchewut12 { get; set; }
            public string txtchewut13 { get; set; }
            public string txtchewut14 { get; set; }
            public string txtchewut15 { get; set; }
            public string txtchewut16 { get; set; }
            public string txtchewut17 { get; set; }
            public string txtchewut18 { get; set; }
            public string txtchewut19 { get; set; }
            public string txtchewut20 { get; set; }
            public string txtfinal1 { get; set; }
            public string txtfinal2 { get; set; }
            public string txtfinal3 { get; set; }
            public string txtfinal4 { get; set; }
            public string txtfinal5 { get; set; }
            public string txtfinal6 { get; set; }
            public string txtfinal7 { get; set; }
            public string txtfinal8 { get; set; }
            public string txtfinal9 { get; set; }
            public string txtfinal10 { get; set; }
            public string txtmid1 { get; set; }
            public string txtmid2 { get; set; }
            public string txtmid3 { get; set; }
            public string txtmid4 { get; set; }
            public string txtmid5 { get; set; }
            public string txtmid6 { get; set; }
            public string txtmid7 { get; set; }
            public string txtmid8 { get; set; }
            public string txtmid9 { get; set; }
            public string txtmid10 { get; set; }
            public string txtreadwrite1 { get; set; }
            public string txtreadwrite2 { get; set; }
            public string txtreadwrite3 { get; set; }
            public string txtreadwrite4 { get; set; }
            public string txtreadwrite5 { get; set; }

            public string txtsamattana1 { get; set; }
            public string txtsamattana2 { get; set; }
            public string txtsamattana3 { get; set; }
            public string txtsamattana4 { get; set; }
            public string txtsamattana5 { get; set; }

            public int? sort2 { get; set; }
            public int sort1int { get; set; }
            public string sort1txt { get; set; }
            public string getgrade { get; set; }
            public string totalquiz { get; set; }
            public string totalBeforeMidTerm { get; set; }
            public string totalAfterMidTerm { get; set; }
            public string total100 { get; set; }
            public string totalfinal { get; set; }
            public string totalmid { get; set; }
            public string studentid { get; set; }
            public string gradeprint { get; set; }
            public string get70 { get; set; }

            public int nStudentNumber { get; set; }
        }
    }

}