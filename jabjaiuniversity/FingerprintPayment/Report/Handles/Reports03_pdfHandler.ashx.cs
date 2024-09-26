using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Newtonsoft.Json;
using System;
using System.Web;
using FingerprintPayment.Report.Models;
using FingerprintPayment.Report.Functions.Reports_03;
using System.Web.SessionState;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using AjaxControlToolkit.MaskedEditValidatorCompatibility;

namespace FingerprintPayment.Report.Handles
{
    /// <summary>
    /// Summary description for Reports03_pdfHandler
    /// </summary>
    public class Reports03_pdfHandler : IHttpHandler, IRequiresSessionState
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

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken.userData();
                if (token.CheckToken(HttpContext.Current))
                {
                    userData = token.getTokenValues(HttpContext.Current);
                }

                // Create PDF document
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Example.pdf");

                Document pdfDoc = new Document(PageSize.A4, 10, 5, 20, 50);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.AddAuthor("Me");

                pdfDoc.Open();

                int report_type;
                int countName = 0;
                List<ReportsData_03Models> type_03 = new List<ReportsData_03Models>();
                List<ReportsData_03type03> type_0303 = new List<ReportsData_03type03>();
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    //string entities = "JabJaiEntities";
                    string entities = context.Session["sEntities"].ToString();
                    var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(f_company,ConnectionDB.Read)))
                    {
                        var jsonString = new StreamReader(context.Request.InputStream).ReadToEnd();
                        Search search = JsonConvert.DeserializeObject<Search>(jsonString);

                        report_type = search.report_type;

                        DateTime dStart = DateTime.Today, dEnd = DateTime.Today;
                        if (search.sort_type == 0)
                        {
                            search.dEnd = search.dStart.Value.AddDays(1);
                        }
                        else if (search.sort_type == 1)
                        {
                            search.dEnd = search.dStart.Value.AddMonths(1).AddDays(-1);
                        }
                        else if (search.sort_type == 2)
                        {
                            //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                            var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                            search.dStart = f_term.dStart.Value;
                            search.dEnd = f_term.dEnd.Value;
                        }

                        if (report_type == 2)
                        {
                            type_03 = ReportsType_03.getData(search, dbschool, search.dStart.Value, search.dEnd.Value, userData);
                            pdfDoc.Add(GetHeader(f_company.sCompany
                                , "รายงานสรุปสถิติการมาโรงเรียนประจำวันที่ " + search.dStart.Value.ToString("dd MMM yyyy", new CultureInfo("th-th")) + "  ถึงวันที่ " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"))
                                , "", "", ""));
                        }
                        else /*if(report_type == 3)*/
                        {
                            type_0303 = ReportsType_0303.getData(search, dbschool, search.dStart.Value, search.dEnd.Value, search.term_id, userData);


                            var std = (from u in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                       where u.sID == search.student_id
                                       select new
                                       {
                                           name = u.sName,
                                           lastname = u.sLastname,
                                           stdCode = u.sStudentID
                                       }).ToList();

                            countName = std.Count();

                            if (countName == 0)
                            {
                                pdfDoc.Add(GetHeader(f_company.sCompany
                                    , "รายงานสรุปสถิติการมาโรงเรียนประจำวันที่ " + search.dStart.Value.ToString("dd MMM yyyy", new CultureInfo("th-th")) + "  ถึงวันที่ " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"))
                                    , "", "", ""));
                            }
                            else
                            {
                                pdfDoc.Add(GetHeader(f_company.sCompany
                                    , "รายงานสรุปสถิติการมาโรงเรียนประจำวันที่ " + search.dStart.Value.ToString("dd MMM yyyy", new CultureInfo("th-th")) + "  ถึงวันที่ " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"))
                                    , std[0].name, std[0].lastname, std[0].stdCode));
                            }


                        }

                    }
                }

                //PdfContentByte cb = pdfWriter.DirectContent;
                ////cb.Rectangle(pdfDoc.PageSize.Width - 90f, 830f, 250f, 50f);
                //cb.Stroke();

                //Rectangle rect = new Rectangle(pdfDoc.PageSize.Width - 200f, 800f, pdfDoc.PageSize.Width - 10, 850f);
                //ColumnText ct = new ColumnText(cb);

                //ct.SetSimpleColumn(rect);
                //ct.AddElement(new Paragraph("This is the text added in the rectangle", smallNormal));
                //ct.Go();




                if (report_type == 2)
                {
                    foreach (var levelData in type_03)
                    {
                        pdfDoc.Add(GetHeaderDetail(levelData.levelname, levelData.level2name));
                        pdfDoc.Add(GetBody(levelData.studentDatas));
                        pdfDoc.NewPage();
                    }
                }
                else /*if(report_type == 3)*/
                {
                    foreach (var levelData in type_0303)
                    {
                        pdfDoc.Add(GetHeaderDetail(levelData.levelname, levelData.level2name));
                        pdfDoc.Add(GetBody0303(levelData.studentDatas));

                    }

                    if (countName == 1)
                    {
                        pdfDoc.Add(Licen());
                    }

                    pdfDoc.NewPage();

                }



                pdfDoc.Close();

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
                //HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
        }

        private PdfPTable GetHeader(string school_name, string report_name, string name, string lastname, string code)
        {

            PdfPTable headerTable = new PdfPTable(1);
            headerTable.WidthPercentage = 100;
            headerTable.AddCell(AddCell(new PdfPCell(new Phrase(school_name, h1)), Element.ALIGN_CENTER));
            headerTable.AddCell(AddCell(new PdfPCell(new Phrase(report_name, bold)), Element.ALIGN_CENTER));
            if (name != "")
            {
                headerTable.AddCell(AddCell(new PdfPCell(new Phrase("ชื่อ  " + name + "  นามสกุล  " + lastname + "  เลขประจำตัว  " + code, bold)), Element.ALIGN_CENTER));
            }

            headerTable.AddCell(AddCell(new PdfPCell(new Phrase(string.Format("พิมพ์วันที่ : {0} ", DateTime.Now.ToString("dd MMM yyyy", new CultureInfo("th-th")) + (string.Format(" เวลา : {0:HH:mm:ss}", DateTime.Now))), smallBold)), Element.ALIGN_RIGHT));
            //headerTable.AddCell(AddCell(new PdfPCell(new Phrase(string.Format("เวลา : {0:HH:mm:ss}", DateTime.Now), smallBold)), Element.ALIGN_RIGHT));

            return headerTable;
        }



        private PdfPCell AddCell(PdfPCell pdfPCell, int horizontalAlignment)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.VerticalAlignment = Element.ALIGN_BOTTOM;
            TableCell.Border = Rectangle.NO_BORDER;
            return TableCell;
        }
        private PdfPCell AddCell(PdfPCell pdfPCell, int horizontalAlignment, int colspan, int rowspan)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.Rowspan = rowspan;
            TableCell.Colspan = colspan;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.VerticalAlignment = Element.ALIGN_TOP;
            return TableCell;
        }
        private PdfPTable GetHeaderDetail(string levelName, string className)
        {
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 530f;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;

            float[] tableWidths = new float[2];
            tableWidths[0] = 265;
            tableWidths[1] = 265;

            table.SetWidths(tableWidths);
            table.LockedWidth = true;

            Chunk blank = new Chunk(" ", normal);

            Phrase p = new Phrase();

            p.Add(new Chunk("ระดับชั้นเรียน : ", bold));
            p.Add(new Chunk(blank));
            p.Add(new Chunk(levelName, normal));

            PdfPCell cell0 = new PdfPCell(p);
            cell0.Border = Rectangle.NO_BORDER;

            table.AddCell(cell0);

            p = new Phrase();

            p.Add(new Chunk("ชั้นเรียน : ", bold));
            p.Add(new Chunk(blank));
            p.Add(new Chunk(className, normal));

            cell0 = new PdfPCell(p);
            cell0.Border = Rectangle.NO_BORDER;

            table.AddCell(cell0);

            return table;
        }

        private PdfPTable GetBody(List<ReportsData_03Models.StudentData> studentDatas)
        {
            PdfPTable table = new PdfPTable(8);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;

            float[] WidthPercentage = new float[] { 6, 13, 21, 14, 11, 11, 11, 11 };
            table.SetTotalWidth(WidthPercentage);
            //table.TotalWidth = 630f;
            //table.HorizontalAlignment = 0;
            //table.SpacingAfter = 20;
            //headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            foreach (var str in new string[] { "ลำดับ", "เลขประจำตัว", "ชื่อ-นามสกุล", "วัน/เดือน/ปี", "สาย", "ขาด", "ลากิจ", "ลาป่วย" })
            {
                table.AddCell(setCell(new CellProperty
                {
                    Text = str,
                    Font = Header_smallNormal,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    //backgroundColor = new CellProperty.Color
                    //{
                    //    Red = 51,
                    //    Green = 122,
                    //    Blue = 183,
                    //}
                }));
                //table.AddCell(AddCell(new PdfPCell(new Phrase(str, smallBold)), Element.ALIGN_CENTER, 1, 1));
            }

            int indexRows = 1;
            List<string> arraryString = new List<string>() { "3", "1", "11", "10" };
            foreach (var studentData in studentDatas)
            {
                int DayLength = studentData.scanDatas.Count(c => arraryString.Contains(c.Scan_Status.Trim()));
                int scanStatus_0 = 0, scanStatus_1 = 0, scanStatus_2 = 0, scanStatus_3 = 0;
                if (DayLength == 0)
                {
                    table.AddCell(setCell(new CellProperty { Text = indexRows.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                    table.AddCell(setCell(new CellProperty { Text = studentData.Student_Id, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                    table.AddCell(setCell(new CellProperty { Text = studentData.Student_Name, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }));
                    table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                    table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                    table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                    table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                    table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                }
                else
                {
                    int rowsIndex = 0;
                    foreach (var scanData in studentData.scanDatas.Where(c => arraryString.Contains(c.Scan_Status.Trim())))
                    {
                        if (rowsIndex == 0)
                        {
                            table.AddCell(AddCell(new PdfPCell(new Phrase(indexRows.ToString(), smallNormal)), Element.ALIGN_CENTER, 1, DayLength));
                            table.AddCell(AddCell(new PdfPCell(new Phrase(studentData.Student_Id, smallNormal)), Element.ALIGN_CENTER, 1, DayLength));
                            table.AddCell(AddCell(new PdfPCell(new Phrase(studentData.Student_Name, smallNormal)), Element.ALIGN_LEFT, 1, DayLength));

                            table.AddCell(setCell(new CellProperty { Text = scanData.Scan_Date, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                            table.AddCell(setCell(new CellProperty { Text = scanData.Scan_Status == "1" ? "1" : "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                            table.AddCell(setCell(new CellProperty { Text = scanData.Scan_Status == "3" ? "1" : "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                            table.AddCell(setCell(new CellProperty { Text = scanData.Scan_Status == "10" ? "1" : "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                            table.AddCell(setCell(new CellProperty { Text = scanData.Scan_Status == "11" ? "1" : "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        }
                        else
                        {
                            table.AddCell(setCell(new CellProperty { Text = scanData.Scan_Date, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                            table.AddCell(setCell(new CellProperty { Text = scanData.Scan_Status == "1" ? "1" : "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                            table.AddCell(setCell(new CellProperty { Text = scanData.Scan_Status == "3" ? "1" : "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                            table.AddCell(setCell(new CellProperty { Text = scanData.Scan_Status == "10" ? "1" : "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                            table.AddCell(setCell(new CellProperty { Text = scanData.Scan_Status == "11" ? "1" : "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        }

                        switch (scanData.Scan_Status.Trim())
                        {
                            case "1": scanStatus_0 += 1; break;
                            case "3": scanStatus_1 += 1; break;
                            case "10": scanStatus_2 += 1; break;
                            case "11": scanStatus_3 += 1; break;
                        }

                        rowsIndex++;
                    }
                }
                //var backgroundColor = new CellProperty.Color { Red = 238, Green = 238, Blue = 238 };
                table.AddCell(setCell(new CellProperty { Text = "รวม", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 4 }));
                table.AddCell(setCell(new CellProperty { Text = scanStatus_0.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                table.AddCell(setCell(new CellProperty { Text = scanStatus_1.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                table.AddCell(setCell(new CellProperty { Text = scanStatus_2.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                table.AddCell(setCell(new CellProperty { Text = scanStatus_3.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                indexRows++;
            }

            return table;
        }

        private PdfPTable GetBody0303(List<ReportsData_03type03.StudentData> studentDatas)
        {
            PdfPTable table = new PdfPTable(12);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;

            //float[] WidthPercentage = new float[] { 8, 14, 14, 14, 12, 12, 12, 12 };
            float[] WidthPercentage = new float[] { 4, 9, 16, 6, 6, 8, 7, 7, 8, 13, 10, 8 };
            table.SetTotalWidth(WidthPercentage);
            //table.TotalWidth = 630f;
            //table.HorizontalAlignment = 0;
            //table.SpacingAfter = 20;
            //headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            foreach (var str in new string[] { "#", "เลขประจำตัว", "ชื่อ-นามสกุล", "สาย", "ขาด", "ขาดครึ่งวัน", "ลากิจ", "ลาป่วย", "ไม่ได้เช็คชื่อ", "ตัดคะแนนสาย/ขาด", "ตัดคะแนนอื่นๆ", "รวมคะแนน" })
            {
                table.AddCell(setCell(new CellProperty
                {
                    Text = str,
                    Font = Header_smallNormal,
                    HorizontalAlignment = Element.ALIGN_CENTER,

                    //backgroundColor = new CellProperty.Color
                    //{
                    //    Red = 51,
                    //    Green = 122,
                    //    Blue = 183,
                    //}
                }));
                //table.AddCell(AddCell(new PdfPCell(new Phrase(str, smallBold)), Element.ALIGN_CENTER, 1, 1));
            }

            int indexRows = 1;
            List<string> arraryString = new List<string>() { "3", "1", "11", "10" };
            foreach (var studentData in studentDatas)
            {
                int DayLength = studentData.scanDatas.Count(c => arraryString.Contains(c.Scan_Status.Trim()));
                int scanStatus_0 = 0, scanStatus_1 = 0, scanStatus_1_1 = 0, scanStatus_2 = 0, scanStatus_3 = 0, scanStatus_4 = 0;

                decimal behavAuto = studentData.behavAuto;
                decimal behavManual = studentData.behavManual;
                decimal behav = studentData.behaviorScore;
                bool Alert = studentData.Alert;
                int _Late = 0, _Absence = 0, _Absence_Half = 0, _Errand = 0, _Sick = 0, _UncheckName = 0;

                BaseColor backgroundColor = new BaseColor(255, 255, 255);
                BaseColor fontColor = new BaseColor(0, 0, 0);

                foreach (var scanData in studentData.scanDatas.Where(c => arraryString.Contains(c.Scan_Status.Trim())))
                {
                    switch (scanData.Scan_Status.Trim())
                    {
                        case "1": scanStatus_0 += 1; break;
                        //case "3": scanStatus_1 += 1; break;
                        case "3":
                            {
                                if (scanData.timeIn == "")
                                {
                                    scanStatus_1 += 1;
                                }
                                else scanStatus_1_1 += 1;
                                break;
                            }
                        case "10": scanStatus_2 += 1; break;
                        case "11": scanStatus_3 += 1; break;
                        case "99": scanStatus_4 += 1; break;
                    }

                    _Late += scanData.Late;
                    _Absence += scanData.Absence;
                    _Absence_Half += scanData.Absence_Half;
                    _Errand += scanData.Errand;
                    _Sick += scanData.Sick;
                    _UncheckName += scanData.UncheckName;
                }

                if (studentData.StudentStatus == 2/* || studentData.scanDatas.Count(c => arraryString.Contains(c.Scan_Status.Trim())) == 0*/)
                {
                    backgroundColor = new BaseColor(190, 190, 190);
                    fontColor = new BaseColor(0, 0, 0);

                    table.AddCell(setCell(new CellProperty { Text = indexRows.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = studentData.Student_Id, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = studentData.Student_Name, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = "0" + "/(" + _Late + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = "0" + "/(" + _Absence + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = "0" + "/(" + _Absence_Half + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = "0" + "/(" + _Sick + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = "0" + "/(" + _Errand + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = "0" + "/(" + _UncheckName + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = "0", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = "0", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = "0", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                }
                else
                {
                    if (studentData.Alert)
                    {
                        backgroundColor = new BaseColor(238, 213, 210);
                        fontColor = new BaseColor(0, 0, 0);
                    }

                    table.AddCell(setCell(new CellProperty { Text = indexRows.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = studentData.Student_Id, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = studentData.Student_Name, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = scanStatus_0.ToString() + "/(" + _Late + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = scanStatus_1.ToString() + "/(" + _Absence + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = scanStatus_1_1.ToString() + "/(" + _Absence_Half + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = scanStatus_2.ToString() + "/(" + _Sick + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = scanStatus_3.ToString() + "/(" + _Errand + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = scanStatus_4.ToString() + "/(" + _UncheckName + ")", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = behavAuto.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = behavManual.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = behav.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                }

                indexRows++;
            }

            return table;
        }

        private PdfPTable Licen()
        {
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;


            for (int i = 0; i < 10; i++)
            {
                table.AddCell(AddCell(new PdfPCell(new Phrase(" ", normal)), Element.ALIGN_CENTER));
                table.AddCell(AddCell(new PdfPCell(new Phrase(" ", normal)), Element.ALIGN_CENTER));
            }

            // table.AddCell(setCell(new CellProperty { Text = "asdasd", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
            table.AddCell(AddCell(new PdfPCell(new Phrase("", normal)), Element.ALIGN_CENTER));
            table.AddCell(AddCell(new PdfPCell(new Phrase("ลงชื่อ ............................................. หัวหน้าระดับชั้น / ผู้ช่วยระดับชั้น", normal)), Element.ALIGN_LEFT));

            table.AddCell(AddCell(new PdfPCell(new Phrase("", normal)), Element.ALIGN_CENTER));
            table.AddCell(AddCell(new PdfPCell(new Phrase("ลงชื่อ ............................................. หัวหน้าฝ่ายปกครอง", normal)), Element.ALIGN_LEFT));

            table.AddCell(AddCell(new PdfPCell(new Phrase("", normal)), Element.ALIGN_CENTER));
            table.AddCell(AddCell(new PdfPCell(new Phrase("ลงวันที่ ............../............../..............", normal)), Element.ALIGN_LEFT));

            return table;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private PdfPCell setCell(CellProperty property)
        {
            PdfPCell TableCell = new PdfPCell(new Phrase(property.Text, property.Font));
            TableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            TableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_BOTTOM;
            TableCell.Border = property.Border ?? Rectangle.BOX;
            TableCell.Colspan = property.Colspan ?? 1;
            TableCell.Rowspan = property.Rowspan ?? 1;
            if (property.backgroundColor != null) TableCell.BackgroundColor = new BaseColor(property.backgroundColor.Red, property.backgroundColor.Green, property.backgroundColor.Blue);

            return TableCell;
        }

        private PdfPCell setCell(CellProperty property, BaseColor backgroundColor, BaseColor fontColor)
        {
            var font = property.Font;
            font.SetColor(fontColor.R, fontColor.G, fontColor.B);

            PdfPCell TableCell = new PdfPCell(new Phrase(property.Text, smallNormal_Black));
            TableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            TableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_BOTTOM;
            TableCell.Border = property.Border ?? Rectangle.BOX;
            TableCell.Colspan = property.Colspan ?? 1;
            TableCell.Rowspan = property.Rowspan ?? 1;
            TableCell.BackgroundColor = backgroundColor;

            return TableCell;
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
            public Color backgroundColor { get; set; }
            public class Color
            {
                public int Red { get; set; }
                public int Green { get; set; }
                public int Blue { get; set; }
            }
        }
    }
}