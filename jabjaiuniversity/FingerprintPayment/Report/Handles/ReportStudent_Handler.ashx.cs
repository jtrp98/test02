using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.SessionState;
using static FingerprintPayment.Report.studentreports;


namespace FingerprintPayment.Report.Handles
{
    /// <summary>
    /// Summary description for Reports03_exportHandler
    /// </summary>
    public class ReportStudent_Handler : IHttpHandler, IRequiresSessionState
    {
        JWTToken.userData userData;
        TCompany school;
        public void ProcessRequest(HttpContext context)
        {

            string rtype = context.Request["rtype"] + "";
            int? year = ToNullableInt(context.Request["year"] + "");
            var term = context.Request["term"] + "";
            int? lvl1 = ToNullableInt(context.Request["lvl1"] + "");
            var lvl2 = context.Request["lvl2"] + "";
            var name = context.Request["name"] + "";
            var type = ToNullableInt(context.Request["type"] + "");
            var _cols = context.Request["cols"] + "";
            var _status = context.Request["status"] + "";

            var cols = new List<string>();
            if (!string.IsNullOrEmpty(_cols))
            {
                cols = _cols.Split(',').ToList();
            }

            var status = new List<int>();
            if (!string.IsNullOrEmpty(_status))
            {
                status = _status.Split(',').Select(o => o.ToNumber<int>())
                    .Where(o => o.HasValue)
                    .Select(o => o.Value)
                    .ToList();
            }
            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                throw new Exception();
            }

            school = new TCompany();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                school = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
            }

            if (rtype == "excelAll")
            {
                ExcelPackage excel = new ExcelPackage();

                switch (type)
                {
                    case 1:
                        excel = Report1ALL(year, term, status);
                        break;
                    default:
                        break;
                }

                context.Response.Clear();
                context.Response.AddHeader("content-disposition", "attachment; filename=รายงาน_รายชื่อนักเรียน_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                context.Response.ContentType = "application/text";
                context.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                context.Response.BinaryWrite(excel.GetAsByteArray());
                context.Response.Flush(); // Sends all currently buffered output to the client.
                context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
            }
            else if (rtype == "excel")
            {
                ExcelPackage excel = new ExcelPackage();

                switch (type)
                {
                    case 1:
                        excel = Report1(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 2:
                    case 12:
                        excel = Report2(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 3:
                        excel = Report3(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 4:
                        excel = Report4(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 5:
                        excel = Report5(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 6:
                        excel = Report6(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 7:
                        excel = Report7(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 8:
                        excel = Report8(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 9:
                        excel = Report9(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 13:
                        excel = Report13(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 14:
                        excel = Report14(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 15:
                        excel = Report15(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 16:
                        excel = Report16(year, term, lvl1, lvl2, name, cols, status);
                        break;
                    default:
                        break;
                }


                context.Response.Clear();
                context.Response.AddHeader("content-disposition", "attachment; filename=รายงาน_รายชื่อนักเรียน_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                context.Response.ContentType = "application/text";
                context.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                context.Response.BinaryWrite(excel.GetAsByteArray());
                context.Response.Flush(); // Sends all currently buffered output to the client.
                context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
            }
            else if (rtype == "pdf")
            {
                var path = context.Server.MapPath("~/Fonts/THSarabun.ttf");
                FontFactory.Register(path, "thsarabun1");

                string filename = $"รายงาน_รายชื่อนักเรียน_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";

                byte[] doc = null;

                switch (type)
                {
                    //    case 1:
                    //        doc = ReportPDF1(year, term, lvl1, lvl2, name, cols);
                    //        break;

                    case 2:
                        doc = ReportPDF2(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 3:
                        doc = ReportPDF3(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 4:
                        doc = ReportPDF4(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 5:
                        doc = ReportPDF5(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 6:
                        doc = ReportPDF6(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 7:
                        doc = ReportPDF7(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 8:
                        doc = ReportPDF8(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 12:
                        doc = ReportPDF12(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 13:
                        doc = ReportPDF13(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 14:
                        doc = ReportPDF14(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 15:
                        doc = ReportPDF15(year, term, lvl1, lvl2, name, cols, status);
                        break;

                    case 16:
                        doc = ReportPDF16(year, term, lvl1, lvl2, name, cols, status);
                        break;
                    default:
                        break;
                }

                //using (MemoryStream ms = new MemoryStream())
                //{
                //    //FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
                //    Document doc = //new Document(PageSize.A4, 25, 25, 30, 30);
                //    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                //    doc.Open();


                //    //SetPdfHeader(doc,
                //    //     , cols
                //    //    , $"{r.level1}/{r.level2} ปีการศึกษา {term}/{year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                //    //    , 14
                //    //    , "ข้อมูลนักเรียน");
                //    //PdfPTable table = new PdfPTable(3);
                //    //var jpg = iTextSharp.text.Image.GetInstance(imageURL);


                //    doc.Close();
                //    writer.Close();
                //}

                context.Response.Clear();
                context.Response.AddHeader("content-disposition", "attachment; filename=รายงาน_รายชื่อนักเรียน_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
                context.Response.ContentType = "application/pdf";
                context.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                context.Response.BinaryWrite(doc);
                context.Response.Flush(); // Sends all currently buffered output to the client.
                context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

            }
        }

        #region PDF Report

        private byte[] ReportPDF2(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport2(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 120, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {

                                    writer1.CloseStream = false;

                                    SetPdfHeader1(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ข้อมูลนักเรียน");

                                    doc1.Open();

                                    PdfPTable table1 = new PdfPTable(6);
                                    table1.WidthPercentage = 99;
                                    //SetCell(worksheet.Cells[6, 1, 6, 13], isMerge: true, text: "บัญชีรายชื่อนักเรียน/นักศึกษา");
                                    table1.AddCell(SetCellPDF(
                                          text: "บัญชีรายชื่อนักเรียน/นักศึกษา",
                                          colspan: 6,
                                          fontSize: 16,
                                          fontStyle: iTextSharp.text.Font.BOLD,
                                          vetical: Element.ALIGN_MIDDLE,
                                          horizotal: Element.ALIGN_CENTER,
                                          border: iTextSharp.text.Rectangle.NO_BORDER
                                          ));

                                    table1.AddCell(SetCellPDF(
                                        text: "ระดับชั้น/ห้อง",
                                          border: iTextSharp.text.Rectangle.NO_BORDER
                                        ));

                                    table1.AddCell(SetCellPDF(
                                       text: $"{r.level1}/{r.level2}",
                                         border: iTextSharp.text.Rectangle.NO_BORDER
                                       ));

                                    table1.AddCell(SetCellPDF(
                                     text: $"รอบ",
                                       border: iTextSharp.text.Rectangle.NO_BORDER
                                     ));

                                    table1.AddCell(SetCellPDF(
                                        text: $" ",
                                          border: iTextSharp.text.Rectangle.NO_BORDER
                                        ));

                                    table1.AddCell(SetCellPDF(
                                       text: $"อาจารย์ที่ปรึกษา",
                                       horizotal: Element.ALIGN_CENTER,
                                       border: iTextSharp.text.Rectangle.NO_BORDER
                                       ));

                                    table1.AddCell(SetCellPDF(
                                       text: $"{r.teacher}",
                                       horizotal: Element.ALIGN_CENTER,
                                       border: iTextSharp.text.Rectangle.NO_BORDER
                                       ));

                                    //table1.AddCell(SetCellPDF(
                                    //     text: " ",
                                    //     colspan: 6,
                                    //     border: iTextSharp.text.Rectangle.NO_BORDER
                                    //     ));

                                    doc1.Add(table1);

                                    PdfPTable table2 = new PdfPTable(14 + cols.Count);
                                    table2.HeaderRows = 2;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 6, 10, 20, 15 });
                                    //float _width = 50f / (10);

                                    for (int i = 1; i <= 9; i++)
                                    {
                                        lstWidth.Add(4);
                                    }

                                    //lstWidth.Add(12);

                                    float _width = (100 - lstWidth.Sum()) / (cols.Count + 1);

                                    for (int i = 1; i <= cols.Count + 1; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());
                                    table2.AddCell(SetCellPDF(text: "เลขที่", horizotal: Element.ALIGN_CENTER, rowspan: 2));
                                    table2.AddCell(SetCellPDF(text: "รหัส", horizotal: Element.ALIGN_CENTER, rowspan: 2));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ", horizotal: Element.ALIGN_CENTER, rowspan: 2));
                                    table2.AddCell(SetCellPDF(text: "สกุล", horizotal: Element.ALIGN_CENTER, rowspan: 2));
                                    table2.AddCell(SetCellPDF(text: " ", colspan: 9));
                                    table2.AddCell(SetCellPDF(text: "หมายเหตุ", rowspan: 2));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i], rowspan: 2));
                                    }

                                    for (int i = 1; i <= 9; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: " ", fontSize: 12));
                                    }

                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];
                                        table2.AddCell(SetCellPDF(text: s.no + ""));
                                        table2.AddCell(SetCellPDF(text: s.code + ""));
                                        table2.AddCell(SetCellPDF(text: s.title + " " + s.fname, horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.lname + "", horizotal: Element.ALIGN_LEFT));

                                        for (int i = 0; i < 9; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }

                                        table2.AddCell(SetCellPDF(text: " "));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }
                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF12(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport2(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 40, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {

                                    writer1.CloseStream = false;

                                    SetPdfHeader2(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ข้อมูลนักเรียน");

                                    doc1.Open();

                                    //PdfPTable table1 = new PdfPTable(6);
                                    //table1.WidthPercentage = 99;
                                    ////SetCell(worksheet.Cells[6, 1, 6, 13], isMerge: true, text: "บัญชีรายชื่อนักเรียน/นักศึกษา");
                                    //table1.AddCell(SetCellPDF(
                                    //      text: "บัญชีรายชื่อนักเรียน/นักศึกษา",
                                    //      colspan: 6,
                                    //      fontSize: 14,
                                    //      fontStyle: iTextSharp.text.Font.BOLD,
                                    //      vetical: Element.ALIGN_MIDDLE,
                                    //      horizotal: Element.ALIGN_CENTER,
                                    //      border: iTextSharp.text.Rectangle.NO_BORDER
                                    //      ));

                                    //table1.AddCell(SetCellPDF(
                                    //    fontSize: 14,
                                    //    text: "ระดับชั้น/ห้อง",
                                    //      border: iTextSharp.text.Rectangle.NO_BORDER
                                    //    ));

                                    //table1.AddCell(SetCellPDF(
                                    //    fontSize: 14,
                                    //    text: $"{r.level1}/{r.level2}",
                                    //     border: iTextSharp.text.Rectangle.NO_BORDER
                                    //   ));

                                    //table1.AddCell(SetCellPDF(
                                    //    fontSize: 14,
                                    //    text: $"รอบ",
                                    //   border: iTextSharp.text.Rectangle.NO_BORDER
                                    // ));

                                    //table1.AddCell(SetCellPDF(
                                    //    fontSize: 14,
                                    //    text: $" ",
                                    //      border: iTextSharp.text.Rectangle.NO_BORDER
                                    //    ));

                                    //table1.AddCell(SetCellPDF(
                                    //   fontSize: 14,
                                    //   text: $"อาจารย์ที่ปรึกษา",
                                    //   horizotal: Element.ALIGN_CENTER,
                                    //   border: iTextSharp.text.Rectangle.NO_BORDER
                                    //   ));

                                    //table1.AddCell(SetCellPDF(
                                    //    fontSize: 14,
                                    //   text: $"{r.teacher}",
                                    //   horizotal: Element.ALIGN_CENTER,
                                    //   border: iTextSharp.text.Rectangle.NO_BORDER
                                    //   ));

                                    ////table1.AddCell(SetCellPDF(
                                    ////     text: " ",
                                    ////     colspan: 6,
                                    ////     border: iTextSharp.text.Rectangle.NO_BORDER
                                    ////     ));

                                    //doc1.Add(table1);

                                    PdfPTable table2 = new PdfPTable(13 + cols.Count);
                                    table2.HeaderRows = 2;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 6, 10, 36 });
                                    //float _width = 50f / (10);

                                    for (int i = 1; i <= 9; i++)
                                    {
                                        lstWidth.Add(4);
                                    }

                                    //lstWidth.Add(12);

                                    float _width = 20 / (cols.Count + 1);

                                    for (int i = 1; i <= cols.Count + 1; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());
                                    table2.AddCell(SetCellPDF(
                                       fontSize: 12,
                                       text: "เลขที่",
                                       horizotal: Element.ALIGN_CENTER,
                                       rowspan: 2
                                       ));

                                    table2.AddCell(SetCellPDF(
                                      fontSize: 12,
                                     text: "รหัส",
                                     horizotal: Element.ALIGN_CENTER,
                                     rowspan: 2
                                     ));

                                    table2.AddCell(SetCellPDF(
                                     fontSize: 12,
                                     text: "ชื่อ-สกุล",
                                     horizotal: Element.ALIGN_CENTER,
                                     rowspan: 2
                                     ));

                                    table2.AddCell(SetCellPDF(
                                       fontSize: 12,
                                        text: " ",
                                        colspan: 9
                                        ));

                                    table2.AddCell(SetCellPDF(
                                         fontSize: 12,
                                        text: "หมายเหตุ",
                                        rowspan: 2
                                        ));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(
                                            fontSize: 12,
                                           text: cols[i],
                                           rowspan: 2
                                           ));
                                    }

                                    for (int i = 1; i <= 9; i++)
                                    {
                                        table2.AddCell(SetCellPDF(
                                            text: " ",
                                            fontSize: 12
                                           ));
                                    }

                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];

                                        table2.AddCell(SetCellPDF(
                                            fontSize: 12,
                                            text: s.no + ""
                                        ));

                                        table2.AddCell(SetCellPDF(
                                            fontSize: 12,
                                            text: s.code + ""
                                        ));

                                        table2.AddCell(SetCellPDF(
                                             fontSize: 12,
                                             text: s.name + "",
                                             horizotal: Element.ALIGN_LEFT
                                       ));

                                        for (int i = 0; i < 9; i++)
                                        {
                                            table2.AddCell(SetCellPDF(
                                                fontSize: 12,
                                                text: " "
                                               ));
                                        }

                                        table2.AddCell(SetCellPDF(
                                            fontSize: 12,
                                            text: " "
                                        ));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(
                                                fontSize: 12,
                                                text: " "
                                            ));
                                        }
                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF3(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 120, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {
                                    writer1.CloseStream = false;

                                    SetPdfHeader1(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ใบรายชื่อ");

                                    doc1.Open();

                                    PdfPTable table2 = new PdfPTable(7 + cols.Count);
                                    table2.HeaderRows = 1;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 5, 10, 15, 15, 10, 12, 15, });

                                    float _width = (100 - lstWidth.Sum()) / (cols.Count + 1);

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());

                                    table2.AddCell(SetCellPDF(text: "เลขที่"));
                                    table2.AddCell(SetCellPDF(text: "เลขประจำตัวนักเรียน"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ"));
                                    table2.AddCell(SetCellPDF(text: "สกุล"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อเล่น"));
                                    table2.AddCell(SetCellPDF(text: "วันเกิด"));
                                    table2.AddCell(SetCellPDF(text: "เลขบัตรประชาชน"));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i]));
                                    }

                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];

                                        table2.AddCell(SetCellPDF(text: s.no + ""));
                                        table2.AddCell(SetCellPDF(text: s.code + ""));
                                        table2.AddCell(SetCellPDF(text: s.title + " " + s.fname, horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.lname + "", horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.nick + ""));
                                        table2.AddCell(SetCellPDF(text: s.date));
                                        table2.AddCell(SetCellPDF(text: s.id));
                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }

                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF4(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 120, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {
                                    writer1.CloseStream = false;

                                    SetPdfHeader1(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ใบรายชื่อ");

                                    doc1.Open();

                                    PdfPTable table2 = new PdfPTable(5 + cols.Count);
                                    table2.HeaderRows = 1;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 5, 18, 30, 10 });

                                    var col = (1 + cols.Count);
                                    float _width = 37f / col;

                                    for (int i = 0; i < col; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());

                                    //table2.AddCell(SetCellPDF(text: "ลำดับ"));
                                    table2.AddCell(SetCellPDF(text: "เลขที่"));
                                    table2.AddCell(SetCellPDF(text: "เลขประจำตัวนักเรียน"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ-สกุล"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อเล่น"));
                                    table2.AddCell(SetCellPDF(text: "วันเกิด"));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i]));
                                    }

                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];

                                        //table2.AddCell(SetCellPDF(text: (row + 1) + ""));
                                        table2.AddCell(SetCellPDF(text: s.no + ""));
                                        table2.AddCell(SetCellPDF(text: s.code + ""));
                                        table2.AddCell(SetCellPDF(text: s.name + "", horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.nick + ""));
                                        table2.AddCell(SetCellPDF(text: s.date));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }

                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF13(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 120, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {
                                    writer1.CloseStream = false;

                                    SetPdfHeader1(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ใบรายชื่อ");

                                    doc1.Open();

                                    PdfPTable table2 = new PdfPTable(7 + cols.Count);
                                    table2.HeaderRows = 1;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 5, 16, 28, 10, 12, 8, 9 });

                                    float _width = (100 - lstWidth.Sum()) / (cols.Count + 1);

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());

                                    //table2.AddCell(SetCellPDF(text: "ลำดับ"));
                                    table2.AddCell(SetCellPDF(text: "เลขที่"));
                                    table2.AddCell(SetCellPDF(text: "เลขประจำตัวนักเรียน"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ-สกุล"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อเล่น"));
                                    table2.AddCell(SetCellPDF(text: "วันเกิด"));
                                    table2.AddCell(SetCellPDF(text: "อายุ(ปี)"));
                                    table2.AddCell(SetCellPDF(text: "อายุ(เดือน)"));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i]));
                                    }

                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];

                                        //table2.AddCell(SetCellPDF(text: (row + 1) + ""));
                                        table2.AddCell(SetCellPDF(text: s.no + ""));
                                        table2.AddCell(SetCellPDF(text: s.code + ""));
                                        table2.AddCell(SetCellPDF(text: s.name, horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.nick + ""));
                                        table2.AddCell(SetCellPDF(text: s.date));
                                        table2.AddCell(SetCellPDF(text: s.ageYear + ""));
                                        table2.AddCell(SetCellPDF(text: s.ageMonth + ""));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }

                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF5(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 120, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {
                                    writer1.CloseStream = false;

                                    SetPdfHeader1(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ใบรายชื่อ");

                                    doc1.Open();

                                    PdfPTable table2 = new PdfPTable(5 + cols.Count);
                                    table2.HeaderRows = 1;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 5, 18, 20, 15, 10 });

                                    // var col = (cols.Count);                      
                                    float _width = (100 - lstWidth.Sum()) / (cols.Count + 1);

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());

                                    //table2.AddCell(SetCellPDF(text: "ลำดับ"));
                                    table2.AddCell(SetCellPDF(text: "เลขที่"));
                                    table2.AddCell(SetCellPDF(text: "เลขประจำตัวนักเรียน"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ"));
                                    table2.AddCell(SetCellPDF(text: "สกุล"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อเล่น"));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i]));
                                    }

                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];

                                        table2.AddCell(SetCellPDF(text: s.no + ""));
                                        table2.AddCell(SetCellPDF(text: s.code + ""));
                                        table2.AddCell(SetCellPDF(text: s.title + " " + s.fname, horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.lname, horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.nick + ""));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }
                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF6(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var studentList = GetDataReport6(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 120, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {
                                    writer1.CloseStream = false;

                                    SetPdfHeader1(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ใบรายชื่อ");

                                    doc1.Open();

                                    PdfPTable table2 = new PdfPTable(10 + cols.Count);
                                    table2.HeaderRows = 1;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 5 });

                                    var col = (9 + cols.Count);
                                    float _width = 95f / col;

                                    for (int i = 0; i < col; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());

                                    //table2.AddCell(SetCellPDF(text: "ลำดับ"));
                                    table2.AddCell(SetCellPDF(text: "เลขที่"));
                                    table2.AddCell(SetCellPDF(text: "เลขประจำตัวนักเรียน"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ-สกุล"));
                                    table2.AddCell(SetCellPDF(text: "เลขบัตรประชาชน"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ-สกุล (มารดา)"));
                                    table2.AddCell(SetCellPDF(text: "เลขบัตรประชาชน (มารดา)"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ-สกุล (บิดา)"));
                                    table2.AddCell(SetCellPDF(text: "เลขบัตรประชาชน (บิดา)"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ-สกุล (ผู้ปกครอง)"));
                                    table2.AddCell(SetCellPDF(text: "เลขบัตรประชาชน (ผู้ปกครอง)"));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i]));
                                    }

                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];

                                        table2.AddCell(SetCellPDF(text: s.no + ""));
                                        table2.AddCell(SetCellPDF(text: s.code + ""));
                                        table2.AddCell(SetCellPDF(text: s.name + "", horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.id + ""));
                                        table2.AddCell(SetCellPDF(text: s.mother + "", horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.motherid + ""));
                                        table2.AddCell(SetCellPDF(text: s.father + "", horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.fatherid + ""));
                                        table2.AddCell(SetCellPDF(text: s.family + "", horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.familyid + ""));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }
                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF7(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 120, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {
                                    writer1.CloseStream = false;

                                    SetPdfHeader1(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ใบรายชื่อ");

                                    doc1.Open();

                                    PdfPTable table2 = new PdfPTable(4 + cols.Count);
                                    table2.HeaderRows = 1;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 5, 18, 30, 15 });

                                    float _width = (100 - lstWidth.Sum()) / (cols.Count + 1);

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());

                                    //table2.AddCell(SetCellPDF(text: "ลำดับ"));
                                    table2.AddCell(SetCellPDF(text: "เลขที่"));
                                    table2.AddCell(SetCellPDF(text: "เลขประจำตัวนักเรียน"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ-สกุล"));
                                    table2.AddCell(SetCellPDF(text: "เลขบัตรประชาชน"));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i]));
                                    }

                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];

                                        table2.AddCell(SetCellPDF(text: s.no + ""));
                                        table2.AddCell(SetCellPDF(text: s.code + ""));
                                        table2.AddCell(SetCellPDF(text: s.name + "", horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.id + ""));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }
                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF8(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 120, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {
                                    writer1.CloseStream = false;

                                    SetPdfHeader1(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ใบรายชื่อ");

                                    doc1.Open();

                                    PdfPTable table2 = new PdfPTable(3 + cols.Count);
                                    table2.HeaderRows = 1;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 5, 18, 30 });

                                    var col = (cols.Count);
                                    float _width = 47f / col;

                                    for (int i = 0; i < col; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());

                                    table2.AddCell(SetCellPDF(text: "เลขที่"));
                                    table2.AddCell(SetCellPDF(text: "เลขประจำตัวนักเรียน"));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ-สกุล"));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i]));
                                    }

                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];

                                        table2.AddCell(SetCellPDF(text: s.no + ""));
                                        table2.AddCell(SetCellPDF(text: s.code + ""));
                                        table2.AddCell(SetCellPDF(text: s.name + "", horizotal: Element.ALIGN_LEFT));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }
                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF14(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport2(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 120, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {

                                    writer1.CloseStream = false;

                                    SetPdfHeader1(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ข้อมูลนักเรียน");

                                    doc1.Open();

                                    PdfPTable table1 = new PdfPTable(6);
                                    table1.WidthPercentage = 99;
                                    //SetCell(worksheet.Cells[6, 1, 6, 13], isMerge: true, text: "บัญชีรายชื่อนักเรียน/นักศึกษา");
                                    table1.AddCell(SetCellPDF(
                                          text: "บัญชีรายชื่อนักเรียน/นักศึกษา",
                                          colspan: 6,
                                          fontSize: 16,
                                          fontStyle: iTextSharp.text.Font.BOLD,
                                          vetical: Element.ALIGN_MIDDLE,
                                          horizotal: Element.ALIGN_CENTER,
                                          border: iTextSharp.text.Rectangle.NO_BORDER
                                          ));

                                    table1.AddCell(SetCellPDF(
                                        text: "ระดับชั้น/ห้อง",
                                          border: iTextSharp.text.Rectangle.NO_BORDER
                                        ));

                                    table1.AddCell(SetCellPDF(
                                       text: $"{r.level1}/{r.level2}",
                                         border: iTextSharp.text.Rectangle.NO_BORDER
                                       ));

                                    table1.AddCell(SetCellPDF(
                                     text: $"รอบ",
                                       border: iTextSharp.text.Rectangle.NO_BORDER
                                     ));

                                    table1.AddCell(SetCellPDF(
                                        text: $" ",
                                          border: iTextSharp.text.Rectangle.NO_BORDER
                                        ));

                                    table1.AddCell(SetCellPDF(
                                       text: $"อาจารย์ที่ปรึกษา",
                                       horizotal: Element.ALIGN_CENTER,
                                       border: iTextSharp.text.Rectangle.NO_BORDER
                                       ));

                                    table1.AddCell(SetCellPDF(
                                       text: $"{r.teacher}",
                                       horizotal: Element.ALIGN_CENTER,
                                       border: iTextSharp.text.Rectangle.NO_BORDER
                                       ));

                                    //table1.AddCell(SetCellPDF(
                                    //     text: " ",
                                    //     colspan: 6,
                                    //     border: iTextSharp.text.Rectangle.NO_BORDER
                                    //     ));

                                    doc1.Add(table1);

                                    PdfPTable table2 = new PdfPTable(13 + cols.Count);
                                    table2.HeaderRows = 1;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 6, 10, 30 });
                                    //float _width = 50f / (10);

                                    for (int i = 1; i <= 9; i++)
                                    {
                                        lstWidth.Add(4);
                                    }

                                    //lstWidth.Add(12);

                                    float _width = 20 / (cols.Count + 1);

                                    for (int i = 1; i <= cols.Count + 1; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());
                                    table2.AddCell(SetCellPDF(text: "เลขที่", horizotal: Element.ALIGN_CENTER, rowspan: 2));
                                    table2.AddCell(SetCellPDF(text: "รหัส", horizotal: Element.ALIGN_CENTER, rowspan: 2));
                                    table2.AddCell(SetCellPDF(text: "ชื่อภาษาอังกฤษ", horizotal: Element.ALIGN_CENTER, rowspan: 2));
                                    table2.AddCell(SetCellPDF(text: " ", colspan: 9));
                                    table2.AddCell(SetCellPDF(text: "หมายเหตุ", rowspan: 2));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i], rowspan: 2));
                                    }

                                    for (int i = 1; i <= 9; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: " ", fontSize: 12));
                                    }

                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];
                                        table2.AddCell(SetCellPDF(text: s.no + ""));
                                        table2.AddCell(SetCellPDF(text: s.code + ""));
                                        table2.AddCell(SetCellPDF(text: s.nameEn + "", horizotal: Element.ALIGN_LEFT));

                                        for (int i = 0; i < 9; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }

                                        table2.AddCell(SetCellPDF(text: " "));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }
                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF15(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var studentList = GetDataReport15(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4, 20, 20, 120, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {

                                    writer1.CloseStream = false;

                                    SetPdfHeader1(writer1, doc1
                                        , cols
                                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                        , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                        , 14
                                        , "ข้อมูลนักเรียน");

                                    doc1.Open();

                                    PdfPTable table1 = new PdfPTable(2);
                                    table1.WidthPercentage = 99;
                                    //SetCell(worksheet.Cells[6, 1, 6, 13], isMerge: true, text: "บัญชีรายชื่อนักเรียน/นักศึกษา");
                                    table1.AddCell(SetCellPDF(
                                          text: "ใบรายชื่อ (ที่อยู่)",
                                          colspan: 2,
                                          fontSize: 16,
                                          fontStyle: iTextSharp.text.Font.BOLD,
                                          vetical: Element.ALIGN_MIDDLE,
                                          horizotal: Element.ALIGN_CENTER,
                                          border: iTextSharp.text.Rectangle.NO_BORDER
                                          ));

                                    table1.AddCell(SetCellPDF(
                                        text: "ระดับชั้น/ห้อง",
                                          border: iTextSharp.text.Rectangle.NO_BORDER
                                        ));

                                    table1.AddCell(SetCellPDF(
                                       text: $"{r.level1}/{r.level2}",
                                         border: iTextSharp.text.Rectangle.NO_BORDER
                                       ));


                                    doc1.Add(table1);

                                    PdfPTable table2 = new PdfPTable(5 + cols.Count);
                                    table2.HeaderRows = 1;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 5, 7, 15, 13, 30 });

                                    float _width = (100 - lstWidth.Sum()) / (cols.Count + 1);

                                    for (int i = 1; i < cols.Count + 1; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());
                                    table2.AddCell(SetCellPDF(text: "เลขที่", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "รหัส", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "นามสกุล", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "ที่อยู่", horizotal: Element.ALIGN_CENTER));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i], horizotal: Element.ALIGN_CENTER));
                                    }


                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];
                                        table2.AddCell(SetCellPDF(text: s.no + ""));
                                        table2.AddCell(SetCellPDF(text: s.code + ""));
                                        table2.AddCell(SetCellPDF(text: s.title + " " + s.fname + "", horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: s.lname + "", horizotal: Element.ALIGN_LEFT));
                                        table2.AddCell(SetCellPDF(text: $"{s.number} {s.soy} {s.muu} {s.road} {s.tumbon} {s.aumpher} {s.province} {s.post} {s.phone}", horizotal: Element.ALIGN_LEFT));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }
                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }

        private byte[] ReportPDF16(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var studentList = GetDataReport16(year, term, lvl1, lvl2, name, dbschool, status);

                Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();
                    //PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    //doc.Open();

                    //var copy = new PdfCopyFields(finalStream);

                    if (studentList.Count > 0)
                    {
                        //PdfPTable table = new PdfPTable(3);
                        //var jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        foreach (var r in studentList)
                        {
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                Document doc1 = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                                using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                                {

                                    writer1.CloseStream = false;

                                    //SetPdfHeader1(writer1, doc1
                                    //    , cols
                                    //    , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}"
                                    //    , $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                                    //    , 14
                                    //    , "ข้อมูลนักเรียน"
                                    //    ,true);

                                    doc1.Open();

                                    PdfPTable table = new PdfPTable(3);
                                    //table.TotalWidth = document.PageSize.Width - 20f;
                                    //table.WidthPercentage = 99f;                
                                    table.WidthPercentage = 100;
                                    table.PaddingTop = 0;
                                    table.SetTotalWidth(new float[] { 20, 60, 20 });
                                    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                    if (!string.IsNullOrEmpty(school.sImage))
                                    {
                                        var jpg = iTextSharp.text.Image.GetInstance(school.sImage);
                                        jpg.Alignment = Element.ALIGN_CENTER;
                                        jpg.PaddingTop = 10;
                                        jpg.ScaleAbsolute(70, 70);
                                        PdfPCell cell = new PdfPCell(jpg);
                                        cell.Rowspan = 7;
                                        cell.Border = 0;
                                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                        table.AddCell(cell);
                                    }
                                    else
                                    {
                                        table.AddCell(SetCellPDF(
                                         text: " ",
                                         border: iTextSharp.text.Rectangle.NO_BORDER,
                                         rowspan: 7
                                         ));
                                    }

                                    //tableCol = tableCol - 1;
                                    //var addColLength = addedCol.Count;

                                    table.AddCell(SetCellPDF(
                                        text: school.sCompany,
                                        horizotal: Element.ALIGN_LEFT,
                                        fontSize: 16,
                                        border: iTextSharp.text.Rectangle.NO_BORDER
                                        ));

                                    table.AddCell(SetCellPDF(
                                      text: "ข้อมูลนักเรียน",
                                      fontSize: 16,
                                      horizotal: Element.ALIGN_CENTER,
                                      fontStyle: iTextSharp.text.Font.BOLD,
                                       border: iTextSharp.text.Rectangle.NO_BORDER,
                                      rowspan: 7
                                      ));

                                    table.AddCell(SetCellPDF(
                                       text: school.sAddress,
                                       horizotal: Element.ALIGN_LEFT,
                                        border: iTextSharp.text.Rectangle.NO_BORDER
                                       ));

                                    table.AddCell(SetCellPDF(
                                      text: $"โทรศัพท์ : {school.sPhoneOne + (string.IsNullOrEmpty(school.sPhoneTwo) ? "" : " ," + school.sPhoneTwo)}  โทรสาร : {school.sFax}",
                                      horizotal: Element.ALIGN_LEFT,
                                       border: iTextSharp.text.Rectangle.NO_BORDER
                                      ));

                                    table.AddCell(SetCellPDF(
                                       text: $"{(string.IsNullOrEmpty(school.sWebsite) ? "" : school.sWebsite + " ")}",
                                       horizotal: Element.ALIGN_LEFT,
                                       border: iTextSharp.text.Rectangle.NO_BORDER
                                      ));

                                    table.AddCell(SetCellPDF(
                                      text: $"อีเมล์ : {school.sEmailOne + (string.IsNullOrEmpty(school.sEmailTwo) ? "" : " ," + school.sEmailTwo)}",
                                      horizotal: Element.ALIGN_LEFT,
                                      border: iTextSharp.text.Rectangle.NO_BORDER
                                    ));

                                    table.AddCell(SetCellPDF(
                                       text: $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year}",
                                       horizotal: Element.ALIGN_LEFT,
                                       border: iTextSharp.text.Rectangle.NO_BORDER
                                    ));

                                    table.AddCell(SetCellPDF(
                                      text: $"ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน",
                                      horizotal: Element.ALIGN_LEFT,
                                      border: iTextSharp.text.Rectangle.NO_BORDER
                                     ));

                                    table.AddCell(SetCellPDF(
                                        text: "",
                                        colspan: 3,
                                        horizotal: Element.ALIGN_LEFT,
                                        border: iTextSharp.text.Rectangle.NO_BORDER
                                    ));

                                    doc1.Add(table);

                                    //PdfPTable table1 = new PdfPTable(2);
                                    //table1.PaddingTop = 0;
                                    //table1.WidthPercentage = 99;
                                    ////SetCell(worksheet.Cells[6, 1, 6, 13], isMerge: true, text: "บัญชีรายชื่อนักเรียน/นักศึกษา");
                                    //table1.AddCell(SetCellPDF(
                                    //      text: "บัญชีรายชื่อนักเรียนเเบบย่อ",
                                    //      colspan: 2,
                                    //      fontSize: 16,
                                    //      fontStyle: iTextSharp.text.Font.BOLD,
                                    //      vetical: Element.ALIGN_MIDDLE,
                                    //      horizotal: Element.ALIGN_CENTER,
                                    //      border: iTextSharp.text.Rectangle.NO_BORDER
                                    //      ));

                                    //table1.AddCell(SetCellPDF(
                                    //    text: "ระดับชั้น/ห้อง",
                                    //      border: iTextSharp.text.Rectangle.NO_BORDER
                                    //    ));

                                    //table1.AddCell(SetCellPDF(
                                    //   text: $"{r.level1}/{r.level2}",
                                    //     border: iTextSharp.text.Rectangle.NO_BORDER
                                    //   ));

                                    //doc1.Add(table1);                             

                                    PdfPTable table2 = new PdfPTable(11 + cols.Count);
                                    table2.HeaderRows = 2;
                                    table2.WidthPercentage = 99f;
                                    var lstWidth = new List<float>(new float[] { 7, 12, 10, 10, 12, 10, 10, 8, 8, 12, 8 });

                                    float _width = (100 - lstWidth.Sum()) / (cols.Count + 1);

                                    for (int i = 1; i < cols.Count + 1; i++)
                                    {
                                        lstWidth.Add(_width);
                                    }

                                    table2.SetTotalWidth(lstWidth.ToArray());
                                    table2.AddCell(SetCellPDF(text: $"รหัส{Environment.NewLine}นักเรียน", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "ชื่อ-นามสกุล", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "วัน/เดือน/ปีเกิด", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "บ้านเกิด", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "ชื่อบิดาและมารดา", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: $"อาชีพของบิดา{Environment.NewLine}และมารดา", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "โรงเรียนเดิม", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "เหตุที่ย้าย", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "วันที่เข้าเรียน", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "ที่อยู่ปัจุบัน", horizotal: Element.ALIGN_CENTER));
                                    table2.AddCell(SetCellPDF(text: "วุฒิการศึกษา", horizotal: Element.ALIGN_CENTER));

                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        table2.AddCell(SetCellPDF(text: cols[i], horizotal: Element.ALIGN_CENTER));
                                    }


                                    //row data
                                    for (int row = 0; row < r.students.Count; row++)
                                    {
                                        var s = r.students[row];
                                        table2.AddCell(SetCellPDF(text: s.code + "", fontSize: 14));
                                        table2.AddCell(SetCellPDF(text: s.name, horizotal: Element.ALIGN_LEFT, fontSize: 14));
                                        table2.AddCell(SetCellPDF(text: s.birthDay, horizotal: Element.ALIGN_LEFT, fontSize: 14));
                                        table2.AddCell(SetCellPDF(text: $"{s.tumbonHome}{Environment.NewLine}{s.aumpherHome}{Environment.NewLine}{s.provinceHome}", horizotal: Element.ALIGN_LEFT, fontSize: 14));
                                        table2.AddCell(SetCellPDF(text: $"{s.father}{Environment.NewLine}{s.mother}", horizotal: Element.ALIGN_LEFT, fontSize: 14));
                                        table2.AddCell(SetCellPDF(text: $"{s.fatherJob}{Environment.NewLine}{s.motherJob}", horizotal: Element.ALIGN_LEFT, fontSize: 14));
                                        table2.AddCell(SetCellPDF(text: $"{s.oldSchool}", horizotal: Element.ALIGN_LEFT, fontSize: 14));
                                        table2.AddCell(SetCellPDF(text: $"{s.reason}", horizotal: Element.ALIGN_LEFT, fontSize: 14));
                                        table2.AddCell(SetCellPDF(text: $"{s.firstDate}", horizotal: Element.ALIGN_LEFT, fontSize: 14));
                                        table2.AddCell(SetCellPDF(text: $"{s.number} {s.soy} {s.muu} {s.road} {s.tumbon}{Environment.NewLine}{s.aumpher} {s.province}{Environment.NewLine}{s.post} {s.phone}", horizotal: Element.ALIGN_LEFT, fontSize: 14));
                                        table2.AddCell(SetCellPDF(text: $"{s.oldDegree}", horizotal: Element.ALIGN_LEFT, fontSize: 14));

                                        for (int i = 0; i < cols.Count; i++)
                                        {
                                            table2.AddCell(SetCellPDF(text: " "));
                                        }
                                    }

                                    doc1.Add(table2);

                                    doc1.Close();
                                    //  writer1.Close();
                                }
                                //

                                ms1.Position = 0;
                                var x = new PdfReader(ms1);
                                copy.AddDocument(x);
                                ms1.Dispose();

                            }

                        }

                    }
                    else
                    {
                        //string worksheetName = $"ไม่มีข้อมูล";
                        //excel.Workbook.Worksheets.Add(worksheetName);
                        //var worksheet = excel.Workbook.Worksheets[worksheetName];
                    }

                    //writer.Close();
                    copy.Close();
                    doc.Close();

                    return finalStream.ToArray();
                }


                // return doc;
            };
        }


        private void SetPdfHeader1(PdfWriter writer1, Document doc1, List<string> addedCol
    , string summary1
    , string summary2
    , int tableCol
    , string reportName)
        {
            writer1.PageEvent = new ITextEvents(school, summary1, summary2, reportName);
        }

        private void SetPdfHeader2(PdfWriter writer1, Document doc1, List<string> addedCol
            , string summary1
            , string summary2
            , int tableCol
            , string reportName)
        {
            writer1.PageEvent = new ITextEvents2(school, summary1, summary2, reportName);
        }

        public static PdfPCell SetCellPDF(
          string text = ""
         , int fontSize = 16
         , int fontStyle = iTextSharp.text.Font.NORMAL
         , int horizotal = Element.ALIGN_CENTER
         , int vetical = Element.ALIGN_MIDDLE
         , int border = iTextSharp.text.Rectangle.BOX
         , int colspan = 1
         , int rowspan = 1

         )
        {
            var font = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, fontSize, fontStyle);
            var p = new Phrase(text, font);
            var cell = new PdfPCell(p);
            //cell.UseAscender = true;
            cell.VerticalAlignment = vetical;
            cell.HorizontalAlignment = horizotal;
            cell.Border = border;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 1f;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;

            return cell;
        }


        #endregion

        #region Excel Report

        private void SetCell(ExcelRange xrange
          , bool isMerge = false
          , string text = ""
          , int fontSize = 11
          , bool isBold = false
          , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
          , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
          )
        {
            using (xrange)
            {
                xrange.Merge = isMerge;
                xrange.Value = text;
                xrange.Style.Font.Bold = isBold;
                xrange.Style.HorizontalAlignment = horizotal;
                xrange.Style.VerticalAlignment = vetical;
                xrange.Style.Font.Size = fontSize;

                xrange.AutoFitColumns();

            }
        }


        private ExcelPackage Report1ALL(int? year, string term, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report1VM> studentList = GetDataReport1(year, term, null, "", "", dbschool, status);

                ExcelPackage excel = new ExcelPackage();
                var studentAll = studentList
                    .OrderBy(o => o.sortValue)
                    .ThenBy(o => o.level1)
                    .ThenBy(o => o.level2)
                    .SelectMany(o => o.students).ToList();

                //foreach (var ป in studentList)
                {
                    string worksheetName = $@"ข้อมูลนักเรียนทั้งหมด";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet
                        , new List<string>() { }
                        , ""
                        , 14
                        , "ข้อมูลนักเรียน");

                    int _col = 1;
                    SetCell(worksheet.Cells[6, _col++], text: "ลำดับ");
                    SetCell(worksheet.Cells[6, _col++], text: "ชั้น/ห้อง");
                    SetCell(worksheet.Cells[6, _col++], text: "เลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานะ");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสบัตรประชาชน");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสนักศึกษา");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสลายนิ้วมือ");
                    SetCell(worksheet.Cells[6, _col++], text: "วันที่เข้าเรียน");

                    SetCell(worksheet.Cells[6, _col++], text: "เพศ");
                    SetCell(worksheet.Cells[6, _col++], text: "คำนำหน้า");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อ");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อเล่น");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อ(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อเล่น(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "วัน/เดือน/ปีเกิด");
                    SetCell(worksheet.Cells[6, _col++], text: "ศาสนา");
                    SetCell(worksheet.Cells[6, _col++], text: "สัญชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "เชื้อชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "มีพี่น้องทั้งหมด");
                    SetCell(worksheet.Cells[6, _col++], text: "เป็นบุตรคนที่");
                    SetCell(worksheet.Cells[6, _col++], text: "พี่/น้องเรียนในโรงเรียนนี้");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์โทรศัพท์");
                    SetCell(worksheet.Cells[6, _col++], text: "อีเมล์");
                    SetCell(worksheet.Cells[6, _col++], text: "เงินคงเหลือ");

                    //ที่อยู่ตามทะเบียนบ้าน
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสประจำบ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "แขวง/ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "เขต/อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์โทรศัพท์บ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่เกิดระบุที่เกิด(รพ.)");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่เกิดแขวง/ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่เกิดเขต/อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่เกิดจังหวัด");
                    //ที่อยู่ปัจจุบัน
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเลขที่ปัจจุบัน");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์โทรศัพท์บ้าน");

                    //อาศัยอยู่กับ
                    SetCell(worksheet.Cells[6, _col++], text: "อาศัยอยู่กับ");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "ลักษณะบ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "อีเมล์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์ติดต่อฉุกเฉิน");

                    //เพื่อนใกล้บ้าน
                    SetCell(worksheet.Cells[6, _col++], text: "เพื่อนใกล้บ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์โทรศัพท์เพื่อน");

                    //โรงเรียนเดิม
                    SetCell(worksheet.Cells[6, _col++], text: "สถานศึกษาเดิม");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "วุฒิการศึกษา");
                    SetCell(worksheet.Cells[6, _col++], text: "GPA");
                    SetCell(worksheet.Cells[6, _col++], text: "เหตุที่ย้าย");

                    //ภูมิลำเนา
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเกิดเลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์โทรศัพท์");

                    //ผู้ปกครอง
                    SetCell(worksheet.Cells[6, _col++], text: "ความสัมพันธ์");
                    SetCell(worksheet.Cells[6, _col++], text: "คำนำหน้า");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อผู้ปกครอง");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อผู้ปกครอง(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "วัน/เดือน/ปีเกิด");
                    SetCell(worksheet.Cells[6, _col++], text: "ศาสนา");
                    SetCell(worksheet.Cells[6, _col++], text: "สัญชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "เชื้อชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์บ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์มือถือ");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์ที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานะครอบครัว");
                    SetCell(worksheet.Cells[6, _col++], text: "วุฒิการศึกษา");
                    SetCell(worksheet.Cells[6, _col++], text: "อาชีพผู้ปกครอง");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อเดือน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อปี");
                    SetCell(worksheet.Cells[6, _col++], text: "เบิกค่าเล่าเรียน");

                    //บิดา
                    SetCell(worksheet.Cells[6, _col++], text: "คำนำหน้า");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อบิดา");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อบิดา(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "วัน/เดือน/ปีเกิด");
                    SetCell(worksheet.Cells[6, _col++], text: "ศาสนา");
                    SetCell(worksheet.Cells[6, _col++], text: "สัญชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "เชื้อชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์บ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์มือถือ");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์ที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "วุฒิการศึกษา");
                    SetCell(worksheet.Cells[6, _col++], text: "อาชีพบิดา");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อเดือน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อปี");

                    //มารดา
                    SetCell(worksheet.Cells[6, _col++], text: "คำนำหน้า");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อมารดา");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อมารดา(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "วัน/เดือน/ปีเกิด");
                    SetCell(worksheet.Cells[6, _col++], text: "ศาสนา");
                    SetCell(worksheet.Cells[6, _col++], text: "สัญชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "เชื้อชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์บ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์มือถือ");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์ที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "วุฒิการศึกษา");
                    SetCell(worksheet.Cells[6, _col++], text: "อาชีพมารดา");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อเดือน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อปี");

                    //ข้อมูลสุขภาพ
                    SetCell(worksheet.Cells[6, _col++], text: "น้ำหนัก");
                    SetCell(worksheet.Cells[6, _col++], text: "ส่วนสูง");
                    SetCell(worksheet.Cells[6, _col++], text: "กรุ๊ปเลือด");
                    SetCell(worksheet.Cells[6, _col++], text: "แพ้อาหาร");
                    SetCell(worksheet.Cells[6, _col++], text: "แพ้ยา");
                    SetCell(worksheet.Cells[6, _col++], text: "แพ้อื่นๆ");
                    SetCell(worksheet.Cells[6, _col++], text: "โรคประจำตัว");
                    SetCell(worksheet.Cells[6, _col++], text: "โรคร้ายแรง");
                    SetCell(worksheet.Cells[6, _col++], text: "ประเภทการเดินทาง");

                    SetCell(worksheet.Cells[6, _col++], text: "หมายเลขบัตร");
                    //foreach (var c in cols)
                    //{
                    //    SetCell(worksheet.Cells[6, _col++], text: c);
                    //}

                    for (int row = 0; row < studentAll.Count; row++)
                    {
                        _col = 1;
                        var s = studentAll[row];
                        //ข้อมูลนักเรียน
                        SetCell(worksheet.Cells[7 + row, _col++], text: (row + 1) + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.level2name);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.student_number + "");

                        var studentstatus = s.studentStatus;
                        var studentstatustxt = "";
                        switch (studentstatus)
                        {
                            case 0:
                                studentstatustxt = "กำลังศึกษา";
                                break;
                            case 1:
                                studentstatustxt = "จำหน่าย";
                                break;
                            case 2:
                                studentstatustxt = "ลาออก";
                                break;
                            case 3:
                                studentstatustxt = "พักการเรียน";
                                break;
                            case 5:
                                studentstatustxt = "ขาดการติดต่อ";
                                break;
                            case 6:
                                studentstatustxt = "พ้นสภาพ";
                                break;
                            case 7:
                                studentstatustxt = "นักเรียนไปโครงการ";
                                break;
                            default:
                                studentstatustxt = "กำลังศึกษา";
                                break;
                        }

                        SetCell(worksheet.Cells[7 + row, _col++], text: studentstatustxt);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.sidentification);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentId);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentPassWord);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stMoveIn);

                        var studentsex = s.studentsex;
                        var studentsextxt = "";
                        if (studentsex == "0")
                        {
                            studentsextxt = "ชาย";
                        }
                        else if (studentsex == "1")
                        {
                            studentsextxt = "หญิง";
                        }
                        else
                        {
                            studentsextxt = "";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: studentsextxt);


                        SetCell(worksheet.Cells[7 + row, _col++], text: s.titleDes);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentname);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentlastname);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stNickName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentnameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentlastnameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stNickNameEN);

                        SetCell(worksheet.Cells[7 + row, _col++], text: s.birth);

                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stReligion);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stNation);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stRace);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stSonTotal + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stSonNumber + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stRelativeHere + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.phone + "-");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stEmail);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.money);

                        //ที่อยู่ตามทะเบียนบ้าน
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stHomeRegistCode);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistNumber);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistSoy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistMuu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistRoad);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistTumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistAumpher);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistProvince);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistPost);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistPhone);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.bornFrom);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.bornFromTumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.bornFromAumpher);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.bornFromProvince);
                        //ที่อยู่ปัจจุบัน
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeNumber);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.muu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.soy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.road);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.tumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.aumpher);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.provin);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.post);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stHousePhone);

                        //อาศัยอยู่กับ
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.ststayWithName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.ststayWithLast);

                        var sststayHomeType = s.ststayHomeType;
                        var sststayHomeTypetxt = "";
                        if (sststayHomeType == 1)
                        {
                            sststayHomeTypetxt = "บ้านของตัวเอง";
                        }
                        else if (sststayHomeType == 2)
                        {
                            sststayHomeTypetxt = "บ้านญาติ";
                        }
                        else if (sststayHomeType == 3)
                        {
                            sststayHomeTypetxt = "บ้านเช่า";
                        }
                        else if (sststayHomeType == 4)
                        {
                            sststayHomeTypetxt = "บ้านพักราชการ";
                        }
                        else if (sststayHomeType == 5)
                        {
                            sststayHomeTypetxt = "หอพักโรงเรียน";
                        }
                        else
                        {
                            sststayHomeTypetxt = "";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: sststayHomeTypetxt);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.ststayWithEmail);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.ststayWithEmergency);

                        //เพื่อนใกล้บ้าน
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.friNearHomename);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.friNearHomelast);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.friNearHomephone);

                        //โรงเรียนเดิม
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldSchoolName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldSchoolTumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldSchoolAumpher);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldSchoolProvince);

                        var OldSchoolGrad = s.stOldSchoolGraduated;
                        var OldSchoolGradtxt = "";

                        switch (OldSchoolGrad + "")
                        {
                            case "1": OldSchoolGradtxt = "ประถมศึกษาปีที่ 1"; break;
                            case "2": OldSchoolGradtxt = "ประถมศึกษาปีที่ 2"; break;
                            case "3": OldSchoolGradtxt = "ประถมศึกษาปีที่ 3"; break;
                            case "4": OldSchoolGradtxt = "ประถมศึกษาปีที่ 4"; break;
                            case "5": OldSchoolGradtxt = "ประถมศึกษาปีที่ 5"; break;
                            case "6": OldSchoolGradtxt = "ประถมศึกษาปีที่ 6"; break;
                            case "7": OldSchoolGradtxt = "มัธยมศึกษาตอนต้น"; break;
                            case "8": OldSchoolGradtxt = "มัธยมศึกษาตอนปลาย"; break;
                            case "9": OldSchoolGradtxt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 1"; break;
                            case "10": OldSchoolGradtxt = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 1"; break;
                            case "11": OldSchoolGradtxt = "เตรียมอนุบาลศึกษา"; break;
                            case "12": OldSchoolGradtxt = "อนุบาลศึกษา 1"; break;
                            case "13": OldSchoolGradtxt = "อนุบาลศึกษา 2"; break;
                            case "14": OldSchoolGradtxt = "อนุบาลศึกษา 3"; break;
                            case "15": OldSchoolGradtxt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 2"; break;
                            case "16": OldSchoolGradtxt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 3"; break;
                            case "17": OldSchoolGradtxt = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 2"; break;
                            default: OldSchoolGradtxt = OldSchoolGrad; break;
                        }

                        SetCell(worksheet.Cells[7 + row, _col++], text: OldSchoolGradtxt);

                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldSchoolGPA + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stmoveOutReason);

                        //ภูมิลำเนา
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldhome);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldmuu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldsoy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldroad);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldtumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldaumper);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldprovince);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldpostcode);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldphone);

                        //ผู้ปกครอง
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famRelate);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famTitle);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famlastname);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famNameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famlastnameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famBirday);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famReligion);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famRace);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famNation);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famhome);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fammuu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famsoy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famroad);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famtumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famaumper);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famprovince);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fampostcode);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famphone1);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famphone2);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famphone3);

                        var Ffamstatus = s.famstatus;
                        var famstatustxt = "";
                        if (Ffamstatus == 1)
                        {
                            famstatustxt = "บิดามารดาอยู่ด้วยกัน";
                        }
                        else if (Ffamstatus == 2)
                        {
                            famstatustxt = "บิดามารดาแยกกันอยู่";
                        }
                        else if (Ffamstatus == 3)
                        {
                            famstatustxt = "บิดามารดาหย่าร้าง";
                        }
                        else if (Ffamstatus == 4)
                        {
                            famstatustxt = "บิดาถึงแก่กรรม";
                        }
                        else if (Ffamstatus == 5)
                        {
                            famstatustxt = "มารดาถึงแก่กรรม";
                        }
                        else if (Ffamstatus == 6)
                        {
                            famstatustxt = "บิดามารดาถึงแก่กรรม";
                        }
                        else if (Ffamstatus == 7)
                        {
                            famstatustxt = "บิดามารดาแต่งงานใหม่";
                        }
                        else
                        {
                            famstatustxt = "";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: famstatustxt);

                        var sfameducation = s.fameducation;
                        var sfameducationtxt = "";
                        if (sfameducation == 1)
                        {
                            sfameducationtxt = "ต่ำกว่าประถมศึกษา";
                        }
                        else if (sfameducation == 2)
                        {
                            sfameducationtxt = "ประถมศึกษา";
                        }
                        else if (sfameducation == 3)
                        {
                            sfameducationtxt = "มัธยมศึกษาหรือเทียบเท่า";
                        }
                        else if (sfameducation == 4)
                        {
                            sfameducationtxt = "ปริญญาตรี หรือเทียบเท่า ";
                        }
                        else if (sfameducation == 5)
                        {
                            sfameducationtxt = "ปริญญาโท";
                        }
                        else if (sfameducation == 6)
                        {
                            sfameducationtxt = "สูงกว่าปริญญาโท";
                        }
                        else
                        {
                            sfameducationtxt = "";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: sfameducationtxt);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famJob);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famJobTower);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famJobSalaryMonth);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famJobSalary);

                        var fWithMoney = s.famWithdrawMoney;
                        var fWithMoneytxt = "";
                        if (fWithMoney == 0)
                        {
                            fWithMoneytxt = "เบิกไม่ได้";
                        }
                        else if (fWithMoney == 1)
                        {
                            fWithMoneytxt = "เบิกได้";
                        }
                        else
                        {
                            fWithMoneytxt = "ไม่ระบุ";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: fWithMoneytxt);

                        //บิดา
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterTitle);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterLastname);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterNameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterLastnameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterBirday);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterReligion);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterRace);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterNation);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterhome);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fatermuu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fatersoy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterroad);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fatertumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fateraumper);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterprovince);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterpostcode);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterphone1);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterphone2);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterphone3);

                        var sfatereducation = s.fatereducation;
                        var sfatereducationtxt = "";
                        if (sfatereducation == 1)
                        {
                            sfatereducationtxt = "ต่ำกว่าประถมศึกษา";
                        }
                        else if (sfatereducation == 2)
                        {
                            sfatereducationtxt = "ประถมศึกษา";
                        }
                        else if (sfatereducation == 3)
                        {
                            sfatereducationtxt = "มัธยมศึกษาหรือเทียบเท่า";
                        }
                        else if (sfatereducation == 4)
                        {
                            sfatereducationtxt = "ปริญญาตรี หรือเทียบเท่า ";
                        }
                        else if (sfatereducation == 5)
                        {
                            sfatereducationtxt = "ปริญญาโท";
                        }
                        else if (sfatereducation == 6)
                        {
                            sfatereducationtxt = "สูงกว่าปริญญาโท";
                        }
                        else
                        {
                            sfatereducationtxt = "ไม่ระบุ";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: sfatereducationtxt);

                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterJob);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterJobTower);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterJobSalaryMonth);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterJobSalary);

                        //มารดา
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterTitle);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterLastname);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterNameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterLastnameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterBirday);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterReligion);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterRace);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterNation);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterhome);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.motermuu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.motersoy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterroad);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.motertumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moteraumper);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterprovince);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterpostcode);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterphone1);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterphone2);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterphone3);

                        var smotereducation = s.motereducation;
                        var smotereducationtxt = "";
                        if (smotereducation == 1)
                        {
                            smotereducationtxt = "ต่ำกว่าประถมศึกษา";
                        }
                        else if (smotereducation == 2)
                        {
                            smotereducationtxt = "ประถมศึกษา";
                        }
                        else if (smotereducation == 3)
                        {
                            smotereducationtxt = "มัธยมศึกษาหรือเทียบเท่า";
                        }
                        else if (smotereducation == 4)
                        {
                            smotereducationtxt = "ปริญญาตรี หรือเทียบเท่า ";
                        }
                        else if (smotereducation == 5)
                        {
                            smotereducationtxt = "ปริญญาโท";
                        }
                        else if (smotereducation == 6)
                        {
                            smotereducationtxt = "สูงกว่าปริญญาโท";
                        }
                        else
                        {
                            smotereducationtxt = "ไม่ระบุ";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: smotereducationtxt);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterJob);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterJobTower);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterJobSalaryMonth);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterJobSalary);

                        //ข้อมูลสุขภาพ
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdWeight + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdHeight + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdBlood);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdSickFood);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdSickDruq);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdSickOther);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdSickNormal);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdSickDanger);

                        SetCell(worksheet.Cells[7 + row, _col++], text: s.JourneyType);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.CardNFC);
                        //i = 0;
                        //foreach (var c in cols)
                        //{
                        //    SetCell(worksheet.Cells[7 + row, 7 + i], text: "");
                        //    i++;
                        //}
                    }

                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentAll.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }


        private ExcelPackage Report1(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report1VM> studentList = GetDataReport1(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet
                        , cols
                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 14
                        , "ข้อมูลนักเรียน");

                    int _col = 1;
                    //SetCell(worksheet.Cells[6, _col++], text: "ลำดับ");
                    SetCell(worksheet.Cells[6, _col++], text: "เลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานะ");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสบัตรประชาชน");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสนักศึกษา");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสลายนิ้วมือ");
                    SetCell(worksheet.Cells[6, _col++], text: "วันที่เข้าเรียน");

                    SetCell(worksheet.Cells[6, _col++], text: "เพศ");
                    SetCell(worksheet.Cells[6, _col++], text: "คำนำหน้า");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อ");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อเล่น");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อ(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อเล่น(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "วัน/เดือน/ปีเกิด");
                    SetCell(worksheet.Cells[6, _col++], text: "ศาสนา");
                    SetCell(worksheet.Cells[6, _col++], text: "สัญชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "เชื้อชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "มีพี่น้องทั้งหมด");
                    SetCell(worksheet.Cells[6, _col++], text: "เป็นบุตรคนที่");
                    SetCell(worksheet.Cells[6, _col++], text: "พี่/น้องเรียนในโรงเรียนนี้");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์โทรศัพท์");
                    SetCell(worksheet.Cells[6, _col++], text: "อีเมล์");
                    SetCell(worksheet.Cells[6, _col++], text: "เงินคงเหลือ");

                    //ที่อยู่ตามทะเบียนบ้าน
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสประจำบ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "แขวง/ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "เขต/อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์โทรศัพท์บ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่เกิดระบุที่เกิด(รพ.)");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่เกิดแขวง/ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่เกิดเขต/อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่เกิดจังหวัด");
                    //ที่อยู่ปัจจุบัน
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเลขที่ปัจจุบัน");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์โทรศัพท์บ้าน");

                    //อาศัยอยู่กับ
                    SetCell(worksheet.Cells[6, _col++], text: "อาศัยอยู่กับ");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "ลักษณะบ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "อีเมล์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์ติดต่อฉุกเฉิน");

                    //เพื่อนใกล้บ้าน
                    SetCell(worksheet.Cells[6, _col++], text: "เพื่อนใกล้บ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์โทรศัพท์เพื่อน");

                    //โรงเรียนเดิม
                    SetCell(worksheet.Cells[6, _col++], text: "สถานศึกษาเดิม");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "วุฒิการศึกษา");
                    SetCell(worksheet.Cells[6, _col++], text: "GPA");
                    SetCell(worksheet.Cells[6, _col++], text: "เหตุที่ย้าย");

                    //ภูมิลำเนา
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเกิดเลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์โทรศัพท์");

                    //ผู้ปกครอง
                    SetCell(worksheet.Cells[6, _col++], text: "ความสัมพันธ์");
                    SetCell(worksheet.Cells[6, _col++], text: "คำนำหน้า");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อผู้ปกครอง");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อผู้ปกครอง(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "วัน/เดือน/ปีเกิด");
                    SetCell(worksheet.Cells[6, _col++], text: "ศาสนา");
                    SetCell(worksheet.Cells[6, _col++], text: "สัญชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "เชื้อชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์บ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์มือถือ");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์ที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานะครอบครัว");
                    SetCell(worksheet.Cells[6, _col++], text: "วุฒิการศึกษา");
                    SetCell(worksheet.Cells[6, _col++], text: "อาชีพผู้ปกครอง");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อเดือน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อปี");
                    SetCell(worksheet.Cells[6, _col++], text: "เบิกค่าเล่าเรียน");


                    //บิดา
                    SetCell(worksheet.Cells[6, _col++], text: "คำนำหน้า");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อบิดา");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อบิดา(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "วัน/เดือน/ปีเกิด");
                    SetCell(worksheet.Cells[6, _col++], text: "ศาสนา");
                    SetCell(worksheet.Cells[6, _col++], text: "สัญชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "เชื้อชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์บ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์มือถือ");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์ที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "วุฒิการศึกษา");
                    SetCell(worksheet.Cells[6, _col++], text: "อาชีพบิดา");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อเดือน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อปี");

                    //มารดา
                    SetCell(worksheet.Cells[6, _col++], text: "คำนำหน้า");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อมารดา");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล");
                    SetCell(worksheet.Cells[6, _col++], text: "ชื่อมารดา(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "นามสกุล(อังกฤษ)");
                    SetCell(worksheet.Cells[6, _col++], text: "วัน/เดือน/ปีเกิด");
                    SetCell(worksheet.Cells[6, _col++], text: "ศาสนา");
                    SetCell(worksheet.Cells[6, _col++], text: "สัญชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "เชื้อชาติ");
                    SetCell(worksheet.Cells[6, _col++], text: "บ้านเลขที่");
                    SetCell(worksheet.Cells[6, _col++], text: "หมู่");
                    SetCell(worksheet.Cells[6, _col++], text: "ซอย");
                    SetCell(worksheet.Cells[6, _col++], text: "ถนน");
                    SetCell(worksheet.Cells[6, _col++], text: "ตำบล");
                    SetCell(worksheet.Cells[6, _col++], text: "อำเภอ");
                    SetCell(worksheet.Cells[6, _col++], text: "จังหวัด");
                    SetCell(worksheet.Cells[6, _col++], text: "รหัสไปรษณีย์");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์บ้าน");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์มือถือ");
                    SetCell(worksheet.Cells[6, _col++], text: "เบอร์ที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "วุฒิการศึกษา");
                    SetCell(worksheet.Cells[6, _col++], text: "อาชีพมารดา");
                    SetCell(worksheet.Cells[6, _col++], text: "สถานที่ทำงาน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อเดือน");
                    SetCell(worksheet.Cells[6, _col++], text: "รายได้ต่อปี");

                    //ข้อมูลสุขภาพ
                    SetCell(worksheet.Cells[6, _col++], text: "น้ำหนัก");
                    SetCell(worksheet.Cells[6, _col++], text: "ส่วนสูง");
                    SetCell(worksheet.Cells[6, _col++], text: "กรุ๊ปเลือด");
                    SetCell(worksheet.Cells[6, _col++], text: "แพ้อาหาร");
                    SetCell(worksheet.Cells[6, _col++], text: "แพ้ยา");
                    SetCell(worksheet.Cells[6, _col++], text: "แพ้อื่นๆ");
                    SetCell(worksheet.Cells[6, _col++], text: "โรคประจำตัว");
                    SetCell(worksheet.Cells[6, _col++], text: "โรคร้ายแรง");
                    SetCell(worksheet.Cells[6, _col++], text: "ประเภทการเดินทาง");

                    SetCell(worksheet.Cells[6, _col++], text: "หมายเลขบัตร");

                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[6, _col++], text: c);
                    }

                    for (int row = 0; row < r.students.Count; row++)
                    {
                        _col = 1;
                        var s = r.students[row];
                        //ข้อมูลนักเรียน
                        //SetCell(worksheet.Cells[7 + row, _col++], text: (row + 1) + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.student_number + "");

                        var studentstatus = s.studentStatus;
                        var studentstatustxt = "";
                        switch (studentstatus)
                        {
                            case 0:
                                studentstatustxt = "กำลังศึกษา";
                                break;
                            case 1:
                                studentstatustxt = "จำหน่าย";
                                break;
                            case 2:
                                studentstatustxt = "ลาออก";
                                break;
                            case 3:
                                studentstatustxt = "พักการเรียน";
                                break;
                            case 5:
                                studentstatustxt = "ขาดการติดต่อ";
                                break;
                            case 6:
                                studentstatustxt = "พ้นสภาพ";
                                break;
                            case 7:
                                studentstatustxt = "นักเรียนไปโครงการ";
                                break;
                            default:
                                studentstatustxt = "กำลังศึกษา";
                                break;
                        }

                        SetCell(worksheet.Cells[7 + row, _col++], text: studentstatustxt);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.sidentification);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentId);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentPassWord);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stMoveIn);

                        var studentsex = s.studentsex;
                        var studentsextxt = "";
                        if (studentsex == "0")
                        {
                            studentsextxt = "ชาย";
                        }
                        else if (studentsex == "1")
                        {
                            studentsextxt = "หญิง";
                        }
                        else
                        {
                            studentsextxt = "";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: studentsextxt);


                        SetCell(worksheet.Cells[7 + row, _col++], text: s.titleDes);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentname);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentlastname);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stNickName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentnameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.studentlastnameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stNickNameEN);

                        SetCell(worksheet.Cells[7 + row, _col++], text: s.birth);

                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stReligion);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stNation);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stRace);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stSonTotal + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stSonNumber + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stRelativeHere + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.phone);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stEmail);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.money);

                        //ที่อยู่ตามทะเบียนบ้าน
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stHomeRegistCode);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistNumber);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistSoy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistMuu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistRoad);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistTumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistAumpher);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistProvince);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistPost);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeRegistPhone);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.bornFrom);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.bornFromTumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.bornFromAumpher);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.bornFromProvince);
                        //ที่อยู่ปัจจุบัน
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.homeNumber);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.muu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.soy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.road);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.tumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.aumpher);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.provin);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.post);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stHousePhone);

                        //อาศัยอยู่กับ
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.ststayWithName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.ststayWithLast);

                        var sststayHomeType = s.ststayHomeType;
                        var sststayHomeTypetxt = "";
                        if (sststayHomeType == 1)
                        {
                            sststayHomeTypetxt = "บ้านของตัวเอง";
                        }
                        else if (sststayHomeType == 2)
                        {
                            sststayHomeTypetxt = "บ้านญาติ";
                        }
                        else if (sststayHomeType == 3)
                        {
                            sststayHomeTypetxt = "บ้านเช่า";
                        }
                        else if (sststayHomeType == 4)
                        {
                            sststayHomeTypetxt = "บ้านพักราชการ";
                        }
                        else if (sststayHomeType == 5)
                        {
                            sststayHomeTypetxt = "หอพักโรงเรียน";
                        }
                        else
                        {
                            sststayHomeTypetxt = "";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: sststayHomeTypetxt);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.ststayWithEmail);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.ststayWithEmergency);

                        //เพื่อนใกล้บ้าน
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.friNearHomename);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.friNearHomelast);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.friNearHomephone);

                        //โรงเรียนเดิม
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldSchoolName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldSchoolTumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldSchoolAumpher);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldSchoolProvince);

                        var OldSchoolGrad = s.stOldSchoolGraduated;
                        var OldSchoolGradtxt = "";

                        switch (OldSchoolGrad + "")
                        {
                            case "1": OldSchoolGradtxt = "ประถมศึกษาปีที่ 1"; break;
                            case "2": OldSchoolGradtxt = "ประถมศึกษาปีที่ 2"; break;
                            case "3": OldSchoolGradtxt = "ประถมศึกษาปีที่ 3"; break;
                            case "4": OldSchoolGradtxt = "ประถมศึกษาปีที่ 4"; break;
                            case "5": OldSchoolGradtxt = "ประถมศึกษาปีที่ 5"; break;
                            case "6": OldSchoolGradtxt = "ประถมศึกษาปีที่ 6"; break;
                            case "7": OldSchoolGradtxt = "มัธยมศึกษาตอนต้น"; break;
                            case "8": OldSchoolGradtxt = "มัธยมศึกษาตอนปลาย"; break;
                            case "9": OldSchoolGradtxt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 1"; break;
                            case "10": OldSchoolGradtxt = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 1"; break;
                            case "11": OldSchoolGradtxt = "เตรียมอนุบาลศึกษา"; break;
                            case "12": OldSchoolGradtxt = "อนุบาลศึกษา 1"; break;
                            case "13": OldSchoolGradtxt = "อนุบาลศึกษา 2"; break;
                            case "14": OldSchoolGradtxt = "อนุบาลศึกษา 3"; break;
                            case "15": OldSchoolGradtxt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 2"; break;
                            case "16": OldSchoolGradtxt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 3"; break;
                            case "17": OldSchoolGradtxt = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 2"; break;
                            default: OldSchoolGradtxt = OldSchoolGrad; break;
                        }

                        SetCell(worksheet.Cells[7 + row, _col++], text: OldSchoolGradtxt);

                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldSchoolGPA + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stmoveOutReason);

                        //ภูมิลำเนา
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldhome);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldmuu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldsoy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldroad);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldtumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldaumper);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldprovince);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldpostcode);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stOldphone);

                        //ผู้ปกครอง
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famRelate);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famTitle);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famlastname);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famNameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famlastnameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famBirday);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famReligion);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famRace);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famNation);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famhome);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fammuu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famsoy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famroad);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famtumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famaumper);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famprovince);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fampostcode);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famphone1);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famphone2);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famphone3);

                        var Ffamstatus = s.famstatus;
                        var famstatustxt = "";
                        if (Ffamstatus == 1)
                        {
                            famstatustxt = "บิดามารดาอยู่ด้วยกัน";
                        }
                        else if (Ffamstatus == 2)
                        {
                            famstatustxt = "บิดามารดาแยกกันอยู่";
                        }
                        else if (Ffamstatus == 3)
                        {
                            famstatustxt = "บิดามารดาหย่าร้าง";
                        }
                        else if (Ffamstatus == 4)
                        {
                            famstatustxt = "บิดาถึงแก่กรรม";
                        }
                        else if (Ffamstatus == 5)
                        {
                            famstatustxt = "มารดาถึงแก่กรรม";
                        }
                        else if (Ffamstatus == 6)
                        {
                            famstatustxt = "บิดามารดาถึงแก่กรรม";
                        }
                        else if (Ffamstatus == 7)
                        {
                            famstatustxt = "บิดามารดาแต่งงานใหม่";
                        }
                        else
                        {
                            famstatustxt = "";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: famstatustxt);

                        var sfameducation = s.fameducation;
                        var sfameducationtxt = "";
                        if (sfameducation == 1)
                        {
                            sfameducationtxt = "ต่ำกว่าประถมศึกษา";
                        }
                        else if (sfameducation == 2)
                        {
                            sfameducationtxt = "ประถมศึกษา";
                        }
                        else if (sfameducation == 3)
                        {
                            sfameducationtxt = "มัธยมศึกษาหรือเทียบเท่า";
                        }
                        else if (sfameducation == 4)
                        {
                            sfameducationtxt = "ปริญญาตรี หรือเทียบเท่า ";
                        }
                        else if (sfameducation == 5)
                        {
                            sfameducationtxt = "ปริญญาโท";
                        }
                        else if (sfameducation == 6)
                        {
                            sfameducationtxt = "สูงกว่าปริญญาโท";
                        }
                        else
                        {
                            sfameducationtxt = "";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: sfameducationtxt);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famJob);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famJobTower);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famJobSalaryMonth);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.famJobSalary);

                        var fWithMoney = s.famWithdrawMoney;
                        var fWithMoneytxt = "";
                        if (fWithMoney == 0)
                        {
                            fWithMoneytxt = "เบิกไม่ได้";
                        }
                        else if (fWithMoney == 1)
                        {
                            fWithMoneytxt = "เบิกได้";
                        }
                        else
                        {
                            fWithMoneytxt = "ไม่ระบุ";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: fWithMoneytxt);

                        //บิดา
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterTitle);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterLastname);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterNameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterLastnameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterBirday);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterReligion);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterRace);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterNation);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterhome);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fatermuu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fatersoy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterroad);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fatertumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.fateraumper);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterprovince);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterpostcode);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterphone1);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterphone2);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterphone3);

                        var sfatereducation = s.fatereducation;
                        var sfatereducationtxt = "";
                        if (sfatereducation == 1)
                        {
                            sfatereducationtxt = "ต่ำกว่าประถมศึกษา";
                        }
                        else if (sfatereducation == 2)
                        {
                            sfatereducationtxt = "ประถมศึกษา";
                        }
                        else if (sfatereducation == 3)
                        {
                            sfatereducationtxt = "มัธยมศึกษาหรือเทียบเท่า";
                        }
                        else if (sfatereducation == 4)
                        {
                            sfatereducationtxt = "ปริญญาตรี หรือเทียบเท่า ";
                        }
                        else if (sfatereducation == 5)
                        {
                            sfatereducationtxt = "ปริญญาโท";
                        }
                        else if (sfatereducation == 6)
                        {
                            sfatereducationtxt = "สูงกว่าปริญญาโท";
                        }
                        else
                        {
                            sfatereducationtxt = "ไม่ระบุ";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: sfatereducationtxt);

                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterJob);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterJobTower);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterJobSalaryMonth);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.faterJobSalary);

                        //มารดา
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterTitle);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterName);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterLastname);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterNameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterLastnameEN);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterBirday);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterReligion);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterRace);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterNation);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterhome);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.motermuu);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.motersoy);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterroad);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.motertumbon);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moteraumper);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterprovince);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterpostcode);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterphone1);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterphone2);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterphone3);

                        var smotereducation = s.motereducation;
                        var smotereducationtxt = "";
                        if (smotereducation == 1)
                        {
                            smotereducationtxt = "ต่ำกว่าประถมศึกษา";
                        }
                        else if (smotereducation == 2)
                        {
                            smotereducationtxt = "ประถมศึกษา";
                        }
                        else if (smotereducation == 3)
                        {
                            smotereducationtxt = "มัธยมศึกษาหรือเทียบเท่า";
                        }
                        else if (smotereducation == 4)
                        {
                            smotereducationtxt = "ปริญญาตรี หรือเทียบเท่า ";
                        }
                        else if (smotereducation == 5)
                        {
                            smotereducationtxt = "ปริญญาโท";
                        }
                        else if (smotereducation == 6)
                        {
                            smotereducationtxt = "สูงกว่าปริญญาโท";
                        }
                        else
                        {
                            smotereducationtxt = "ไม่ระบุ";
                        }
                        SetCell(worksheet.Cells[7 + row, _col++], text: smotereducationtxt);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterJob);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterJobTower);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterJobSalaryMonth);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.moterJobSalary);

                        //ข้อมูลสุขภาพ
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdWeight + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdHeight + "");
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdBlood);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdSickFood);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdSickDruq);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdSickOther);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdSickNormal);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.stdSickDanger);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.JourneyType);
                        SetCell(worksheet.Cells[7 + row, _col++], text: s.CardNFC);

                        //i = 0;
                        //foreach (var c in cols)
                        //{
                        //    SetCell(worksheet.Cells[7 + row, 7 + i], text: "");
                        //    i++;
                        //}
                    }

                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report2(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport2(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet
                        , cols
                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 13
                        , "บัญชีรายชื่อนักเรียน");

                    SetCell(worksheet.Cells[6, 1, 6, 13], isMerge: true, text: "บัญชีรายชื่อนักเรียน/นักศึกษา");

                    SetCell(worksheet.Cells[7, 1, 7, 2], isMerge: true, text: "ระดับชั้น/ห้อง");
                    SetCell(worksheet.Cells[7, 3, 7, 3], isMerge: true, text: $"{r.level1}/{r.level2}");
                    SetCell(worksheet.Cells[7, 4, 7, 5], isMerge: true, text: $"รอบ");
                    SetCell(worksheet.Cells[7, 6, 7, 7], isMerge: true, text: $"");
                    SetCell(worksheet.Cells[7, 8, 7, 9], isMerge: true, text: $"อาจารย์ที่ปรึกษา");
                    SetCell(worksheet.Cells[7, 10, 7, 13 + cols.Count], isMerge: true, text: $"{r.teacher} ");

                    //SetCell(worksheet.Cells[8, 1, 9, 1], isMerge: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[8, 1, 9, 1], isMerge: true, text: "เลขที่");
                    SetCell(worksheet.Cells[8, 2, 9, 2], isMerge: true, text: "รหัส");
                    SetCell(worksheet.Cells[8, 3, 9, 3], isMerge: true, text: "ชื่อ");
                    SetCell(worksheet.Cells[8, 4, 9, 4], isMerge: true, text: "สกุล");
                    SetCell(worksheet.Cells[8, 5, 8, 13], isMerge: true, text: "");
                    //SetCell(worksheet.Cells[9, 4], text: "");
                    //SetCell(worksheet.Cells[9, 5], text: "");
                    //SetCell(worksheet.Cells[9, 6], text: "");
                    //SetCell(worksheet.Cells[9, 7], text: "");
                    //SetCell(worksheet.Cells[9, 8], text: "");
                    //SetCell(worksheet.Cells[9, 9], text: "");
                    //SetCell(worksheet.Cells[9, 10], text: "");
                    //SetCell(worksheet.Cells[9, 11], text: "");
                    //SetCell(worksheet.Cells[9, 12], text: "");

                    SetCell(worksheet.Cells[8, 14, 9, 14], isMerge: true, text: "หมายเหตุ");


                    int i = 0;
                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[8, 15 + i, 9, 15 + i], isMerge: true, text: c);
                        i++;
                    }

                    for (int row = 0; row < r.students.Count; row++)
                    {
                        var s = r.students[row];
                        //SetCell(worksheet.Cells[10 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[10 + row, 1], text: s.no + "");
                        SetCell(worksheet.Cells[10 + row, 2], text: s.code);
                        SetCell(worksheet.Cells[10 + row, 3], text: s.title + " " + s.fname, horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[10 + row, 4], text: s.lname, horizotal: ExcelHorizontalAlignment.Left);

                        //i = 0;
                        //foreach (var c in cols)
                        //{
                        //    SetCell(worksheet.Cells[7 + row, 7 + i], text: "");
                        //    i++;
                        //}
                    }
                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report3(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet
                        , cols
                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 7
                        , "ใบรายชื่อ");
                    int col = 1;
                    //SetCell(worksheet.Cells[6, 1], text: "ลำดับ");
                    SetCell(worksheet.Cells[6, col++], text: "เลขที่");
                    SetCell(worksheet.Cells[6, col++], text: "เลขประจำตัวนักเรียน");
                    //worksheet.Column(col).Width = 100;
                    SetCell(worksheet.Cells[6, col++], text: "ชื่อ");
                    //worksheet.Column(col).Width = 100;
                    SetCell(worksheet.Cells[6, col++], text: "นามสกุล");
                    //worksheet.Column(col).Width = 100;
                    SetCell(worksheet.Cells[6, col++], text: "ชื่อเล่น");
                    SetCell(worksheet.Cells[6, col++], text: "วันเกิด");
                    SetCell(worksheet.Cells[6, col++], text: "เลขบัตรประชาชน");

                    //worksheet.Column(6).Width = 100;

                    int i = 0;
                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[6, col++], text: c);
                        i++;
                    }

                    for (int row = 0; row < r.students.Count; row++)
                    {
                        col = 1;
                        var s = r.students[row];
                        //SetCell(worksheet.Cells[7 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[7 + row, col++], text: s.no + "");
                        SetCell(worksheet.Cells[7 + row, col++], text: s.code);
                        SetCell(worksheet.Cells[7 + row, col++], text: s.title + " " + s.fname, horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, col++], text: s.lname, horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, col++], text: s.nick);
                        SetCell(worksheet.Cells[7 + row, col++], text: s.date);
                        SetCell(worksheet.Cells[7 + row, col++], text: s.id);

                        i = 0;
                        foreach (var c in cols)
                        {
                            SetCell(worksheet.Cells[7 + row, col++], text: "");
                            i++;
                        }
                    }
                    worksheet.Cells.AutoFitColumns(15, 50);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report4(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet, cols, $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 6, "ใบรายชื่อ");

                    //SetCell(worksheet.Cells[6, 1], text: "ลำดับ");
                    SetCell(worksheet.Cells[6, 1], text: "เลขที่");
                    SetCell(worksheet.Cells[6, 2], text: "เลขประจำตัวนักเรียน");
                    worksheet.Column(2).Width = 100;
                    SetCell(worksheet.Cells[6, 3], text: "ชื่อ - นามสกุล");
                    worksheet.Column(3).Width = 100;
                    SetCell(worksheet.Cells[6, 4], text: "ชื่อเล่น");
                    SetCell(worksheet.Cells[6, 5], text: "วันเกิด");

                    int i = 0;
                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[6, 6 + i], text: c);
                        i++;
                    }


                    for (int row = 0; row < r.students.Count; row++)
                    {
                        var s = r.students[row];
                        //SetCell(worksheet.Cells[7 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[7 + row, 1], text: s.no + "");
                        SetCell(worksheet.Cells[7 + row, 2], text: s.code);
                        SetCell(worksheet.Cells[7 + row, 3]
                            , text: s.name
                            , horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, 4], text: s.nick);
                        SetCell(worksheet.Cells[7 + row, 5], text: s.date);

                        i = 0;
                        foreach (var c in cols)
                        {
                            SetCell(worksheet.Cells[7 + row, 6 + i], text: "");
                            i++;
                        }
                    }
                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report13(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet, cols, $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 8, "ใบรายชื่อ");

                    //SetCell(worksheet.Cells[6, 1], text: "ลำดับ");
                    SetCell(worksheet.Cells[6, 1], text: "เลขที่");
                    SetCell(worksheet.Cells[6, 2], text: "เลขประจำตัวนักเรียน");
                    worksheet.Column(2).Width = 100;
                    SetCell(worksheet.Cells[6, 3], text: "ชื่อ - นามสกุล");
                    worksheet.Column(3).Width = 100;
                    SetCell(worksheet.Cells[6, 4], text: "ชื่อเล่น");
                    SetCell(worksheet.Cells[6, 5], text: "วันเกิด");
                    SetCell(worksheet.Cells[6, 6], text: "อายุ(ปี)");
                    SetCell(worksheet.Cells[6, 7], text: "อายุ(เดือน)");

                    int i = 0;
                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[6, 8 + i], text: c);
                        i++;
                    }


                    for (int row = 0; row < r.students.Count; row++)
                    {
                        var s = r.students[row];
                        //SetCell(worksheet.Cells[7 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[7 + row, 1], text: s.no + "");
                        SetCell(worksheet.Cells[7 + row, 2], text: s.code);
                        SetCell(worksheet.Cells[7 + row, 3], text: s.name, horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, 4], text: s.nick);
                        SetCell(worksheet.Cells[7 + row, 5], text: s.date);
                        SetCell(worksheet.Cells[7 + row, 6], text: s.ageYear + "");
                        SetCell(worksheet.Cells[7 + row, 7], text: s.ageMonth + "");
                        i = 0;
                        foreach (var c in cols)
                        {
                            SetCell(worksheet.Cells[7 + row, 8 + i], text: "");
                            i++;
                        }
                    }
                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report14(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport2(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet
                        , cols
                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 13
                        , "บัญชีรายชื่อนักเรียน");

                    SetCell(worksheet.Cells[6, 1, 6, 13], isMerge: true, text: "บัญชีรายชื่อนักเรียน/นักศึกษา");

                    SetCell(worksheet.Cells[7, 1, 7, 2], isMerge: true, text: "ระดับชั้น/ห้อง");
                    SetCell(worksheet.Cells[7, 3, 7, 3], isMerge: true, text: $"{r.level1}/{r.level2}");
                    SetCell(worksheet.Cells[7, 4, 7, 5], isMerge: true, text: $"รอบ");
                    SetCell(worksheet.Cells[7, 6, 7, 7], isMerge: true, text: $"");
                    SetCell(worksheet.Cells[7, 8, 7, 9], isMerge: true, text: $"อาจารย์ที่ปรึกษา");
                    SetCell(worksheet.Cells[7, 10, 7, 12 + cols.Count], isMerge: true, text: $"{r.teacher} ");

                    //SetCell(worksheet.Cells[8, 1, 9, 1], isMerge: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[8, 1, 9, 1], isMerge: true, text: "เลขที่");
                    SetCell(worksheet.Cells[8, 2, 9, 2], isMerge: true, text: "รหัส");
                    SetCell(worksheet.Cells[8, 3, 9, 3], isMerge: true, text: "ชื่อภาษาอังกฤษ");
                    SetCell(worksheet.Cells[8, 4, 8, 12], isMerge: true, text: "");
                    //SetCell(worksheet.Cells[9, 4], text: "");
                    //SetCell(worksheet.Cells[9, 5], text: "");
                    //SetCell(worksheet.Cells[9, 6], text: "");
                    //SetCell(worksheet.Cells[9, 7], text: "");
                    //SetCell(worksheet.Cells[9, 8], text: "");
                    //SetCell(worksheet.Cells[9, 9], text: "");
                    //SetCell(worksheet.Cells[9, 10], text: "");
                    //SetCell(worksheet.Cells[9, 11], text: "");
                    //SetCell(worksheet.Cells[9, 12], text: "");

                    SetCell(worksheet.Cells[8, 13, 9, 13], isMerge: true, text: "หมายเหตุ");


                    int i = 0;
                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[8, 14 + i, 9, 14 + i], isMerge: true, text: c);
                        i++;
                    }

                    for (int row = 0; row < r.students.Count; row++)
                    {
                        var s = r.students[row];
                        //SetCell(worksheet.Cells[10 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[10 + row, 1], text: s.no + "");
                        SetCell(worksheet.Cells[10 + row, 2], text: s.code);
                        SetCell(worksheet.Cells[10 + row, 3]
                            , text: s.nameEn
                            , horizotal: ExcelHorizontalAlignment.Left);

                    }
                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report15(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var studentList = GetDataReport15(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet
                        , cols
                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 10
                        , "บัญชีรายชื่อนักเรียน");

                    var row = 6;
                    SetCell(worksheet.Cells[row, 1, row, 10], isMerge: true, text: "ใบรายชื่อ (ที่อยู่)");
                    row++;
                    SetCell(worksheet.Cells[row, 1, row, 2], isMerge: true, text: "ระดับชั้น/ห้อง");
                    SetCell(worksheet.Cells[row, 3, row, 3], isMerge: true, text: $"{r.level1}/{r.level2}");
                    //SetCell(worksheet.Cells[row, 4, row, 5], isMerge: true, text: $"รอบ");
                    //SetCell(worksheet.Cells[row, 6, row, 7], isMerge: true, text: $"");
                    //SetCell(worksheet.Cells[row, 8, row, 9], isMerge: true, text: $"อาจารย์ที่ปรึกษา");
                    //SetCell(worksheet.Cells[row, 10, row, 12 + cols.Count], isMerge: true, text: $"{r.teacher} ");
                    row++;
                    //SetCell(worksheet.Cells[8, 1, 9, 1], isMerge: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[row, 1, row, 1], isMerge: true, text: "เลขที่");
                    SetCell(worksheet.Cells[row, 2, row, 2], isMerge: true, text: "รหัส");
                    SetCell(worksheet.Cells[row, 3, row, 3], isMerge: true, text: "ชื่อ");
                    SetCell(worksheet.Cells[row, 4, row, 4], isMerge: true, text: "นามสกุล");
                    SetCell(worksheet.Cells[row, 5, row, 8], isMerge: true, text: "ที่อยู่");

                    int i = 0;
                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[row, 9 + i, row, 9 + i], isMerge: true, text: c);
                        i++;
                    }
                    row++;
                    for (int j = 0; j < r.students.Count; j++)
                    {
                        var s = r.students[j];
                        //SetCell(worksheet.Cells[10 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[row + j, 1], text: s.no + "");
                        SetCell(worksheet.Cells[row + j, 2], text: s.code);
                        SetCell(worksheet.Cells[row + j, 3], text: s.title + " " + s.fname, horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[row + j, 4], text: s.lname, horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[row + j, 5, row + j, 8], isMerge: true,
                            text: $"{s.number} {s.soy} {s.muu} {s.road} {s.tumbon} {s.aumpher} {s.province} {s.post} {s.phone}",
                            horizotal: ExcelHorizontalAlignment.Left);

                    }
                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report16(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var studentList = GetDataReport16(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet
                        , cols
                        , $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 10
                        , "บัญชีรายชื่อนักเรียน");

                    var row = 6;
                    SetCell(worksheet.Cells[row, 1, row, 10], isMerge: true, text: "บัญชีรายชื่อนักเรียนเเบบย่อ");
                    row++;
                    SetCell(worksheet.Cells[row, 1, row, 2], isMerge: true, text: "ระดับชั้น/ห้อง");
                    SetCell(worksheet.Cells[row, 3, row, 3], isMerge: true, text: $"{r.level1}/{r.level2}");
                    //SetCell(worksheet.Cells[row, 4, row, 5], isMerge: true, text: $"รอบ");
                    //SetCell(worksheet.Cells[row, 6, row, 7], isMerge: true, text: $"");
                    //SetCell(worksheet.Cells[row, 8, row, 9], isMerge: true, text: $"อาจารย์ที่ปรึกษา");
                    //SetCell(worksheet.Cells[row, 10, row, 12 + cols.Count], isMerge: true, text: $"{r.teacher} ");
                    row++;
                    //SetCell(worksheet.Cells[8, 1, 9, 1], isMerge: true, text: "ลำดับ");
                    int col = 1;
                    SetCell(worksheet.Cells[row, col++], text: "รหัสนักเรียน");
                    SetCell(worksheet.Cells[row, col++], text: "ชื่อ-นามสกุล");
                    SetCell(worksheet.Cells[row, col++], text: "วัน/เดือน/ปีเกิด");
                    SetCell(worksheet.Cells[row, col++], text: "บ้านเกิด");
                    SetCell(worksheet.Cells[row, col++], text: "ชื่อบิดาและมารดา");
                    SetCell(worksheet.Cells[row, col++], text: "อาชีพของบิดาและมารดา");
                    SetCell(worksheet.Cells[row, col++], text: "โรงเรียนเดิม");
                    SetCell(worksheet.Cells[row, col++], text: "เหตุที่ย้าย");
                    SetCell(worksheet.Cells[row, col++], text: "วันที่เข้าเรียน");
                    SetCell(worksheet.Cells[row, col++], text: "ที่อยู่ปัจุบัน");
                    SetCell(worksheet.Cells[row, col++], text: "วุฒิการศึกษา");

                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[row, col++], isMerge: true, text: c);
                    }
                    row++;

                    for (int j = 0; j < r.students.Count; j++)
                    {
                        col = 1;
                        var s = r.students[j];
                        //SetCell(worksheet.Cells[10 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[row + j, col++], text: s.code + "");
                        SetCell(worksheet.Cells[row + j, col++], text: s.name + "", horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[row + j, col++], text: s.birthDay + "", horizotal: ExcelHorizontalAlignment.Left);

                        var cell = worksheet.Cells[row + j, col++];
                        cell.IsRichText = true;
                        cell.Style.WrapText = true;
                        var rt = cell.RichText;
                        var ert = rt.Add(s.tumbonHome + Environment.NewLine);
                        ert = rt.Add(s.aumpherHome + Environment.NewLine);
                        ert = rt.Add(s.provinceHome);

                        cell = worksheet.Cells[row + j, col++];
                        cell.IsRichText = true;
                        cell.Style.WrapText = true;
                        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rt = cell.RichText;
                        ert = rt.Add(s.father + Environment.NewLine);
                        ert = rt.Add(s.mother);

                        cell = worksheet.Cells[row + j, col++];
                        cell.IsRichText = true;
                        cell.Style.WrapText = true;
                        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rt = cell.RichText;
                        ert = rt.Add(s.fatherJob + Environment.NewLine);
                        ert = rt.Add(s.motherJob);

                        SetCell(worksheet.Cells[row + j, col++], text: s.oldSchool + "", horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[row + j, col++], text: s.reason + "", horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[row + j, col++], text: s.firstDate + "", horizotal: ExcelHorizontalAlignment.Left);

                        cell = worksheet.Cells[row + j, col++];
                        cell.IsRichText = true;
                        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        cell.Style.WrapText = true;
                        rt = cell.RichText;
                        ert = rt.Add($"{s.number} {s.soy} {s.muu} {s.road} {s.tumbon}" + Environment.NewLine);
                        ert = rt.Add($"{s.aumpher} {s.province} " + Environment.NewLine);
                        ert = rt.Add($"{s.post} {s.phone}");

                        SetCell(worksheet.Cells[row + j, col++], text: s.oldDegree, horizotal: ExcelHorizontalAlignment.Left);

                    }
                    worksheet.Cells.AutoFitColumns(30, 50);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report5(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                List<Report3VM> studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet, cols, $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 5, "ใบรายชื่อ");

                    //SetCell(worksheet.Cells[6, 1], text: "ลำดับ");
                    SetCell(worksheet.Cells[6, 1], text: "เลขที่");
                    SetCell(worksheet.Cells[6, 2], text: "เลขประจำตัวนักเรียน");
                    worksheet.Column(2).Width = 100;
                    SetCell(worksheet.Cells[6, 3], text: "ชื่อ");
                    worksheet.Column(3).Width = 100;
                    SetCell(worksheet.Cells[6, 4], text: "นามสกุล");
                    worksheet.Column(4).Width = 100;
                    SetCell(worksheet.Cells[6, 5], text: "ชื่อเล่น");

                    int i = 0;
                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[6, 6 + i], text: c);
                        i++;
                    }


                    for (int row = 0; row < r.students.Count; row++)
                    {
                        var s = r.students[row];
                        //SetCell(worksheet.Cells[7 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[7 + row, 1], text: s.no + "");
                        SetCell(worksheet.Cells[7 + row, 2], text: s.code);
                        SetCell(worksheet.Cells[7 + row, 3], text: s.title + " " + s.fname, horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, 4], text: s.lname, horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, 5], text: s.nick);

                        i = 0;
                        foreach (var c in cols)
                        {
                            SetCell(worksheet.Cells[7 + row, 6 + i], text: "");
                            i++;
                        }
                    }
                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report6(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var studentList = GetDataReport6(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet, cols, $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 11, "ใบรายชื่อ");

                    //SetCell(worksheet.Cells[6, 1], text: "ลำดับ");
                    SetCell(worksheet.Cells[6, 1], text: "เลขที่");
                    SetCell(worksheet.Cells[6, 2], text: "เลขประจำตัวนักเรียน");
                    worksheet.Column(2).Width = 100;
                    SetCell(worksheet.Cells[6, 3], text: "ชื่อ-สกุล");
                    worksheet.Column(3).Width = 100;
                    SetCell(worksheet.Cells[6, 4], text: "เลขบัตรประชาชน");
                    worksheet.Column(4).Width = 100;
                    SetCell(worksheet.Cells[6, 5], text: "ชื่อ-สกุล(มารดา)");
                    worksheet.Column(5).Width = 100;
                    SetCell(worksheet.Cells[6, 6], text: "เลขบัตรประชาชน(มารดา)");
                    worksheet.Column(6).Width = 100;
                    SetCell(worksheet.Cells[6, 7], text: "ชื่อ-สกุล(บิดา)");
                    worksheet.Column(7).Width = 100;
                    SetCell(worksheet.Cells[6, 8], text: "เลขบัตรประชาชน(บิดา)");
                    worksheet.Column(8).Width = 100;
                    SetCell(worksheet.Cells[6, 9], text: "ชื่อ-สกุล(ผู้ปกครอง)");
                    worksheet.Column(9).Width = 100;
                    SetCell(worksheet.Cells[6, 10], text: "เลขบัตรประชาชน(ผู้ปกครอง)");
                    worksheet.Column(10).Width = 100;

                    int i = 0;
                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[6, 11 + i], text: c);
                        i++;
                    }


                    for (int row = 0; row < r.students.Count; row++)
                    {
                        var s = r.students[row];
                        //SetCell(worksheet.Cells[7 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[7 + row, 1], text: s.no + "");
                        SetCell(worksheet.Cells[7 + row, 2], text: s.code);
                        SetCell(worksheet.Cells[7 + row, 3]
                            , text: s.name
                            , horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, 4], text: s.id);

                        SetCell(worksheet.Cells[7 + row, 5], text: s.mother
                             , horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, 6], text: s.motherid);

                        SetCell(worksheet.Cells[7 + row, 7], text: s.father
                        , horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, 8], text: s.fatherid);

                        SetCell(worksheet.Cells[7 + row, 9], text: s.family
                        , horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, 10], text: s.familyid);
                        i = 0;
                        //foreach (var c in cols)
                        //{
                        //    SetCell(worksheet.Cells[7 + row, 12 + i], text: "");
                        //    i++;
                        //}
                    }
                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report7(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet, cols, $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 5, "ใบรายชื่อ");

                    //SetCell(worksheet.Cells[6, 1], text: "ลำดับ");
                    SetCell(worksheet.Cells[6, 1], text: "เลขที่");
                    SetCell(worksheet.Cells[6, 2], text: "เลขประจำตัวนักเรียน");
                    worksheet.Column(2).Width = 100;
                    SetCell(worksheet.Cells[6, 3], text: "ชื่อ - นามสกุล");
                    worksheet.Column(3).Width = 100;
                    SetCell(worksheet.Cells[6, 4], text: "เลขบัตรประชาชน");
                    worksheet.Column(4).Width = 100;

                    int i = 0;
                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[6, 5 + i], text: c);
                        i++;
                    }


                    for (int row = 0; row < r.students.Count; row++)
                    {
                        var s = r.students[row];
                        //SetCell(worksheet.Cells[7 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[7 + row, 1], text: s.no + "");
                        SetCell(worksheet.Cells[7 + row, 2], text: s.code);
                        SetCell(worksheet.Cells[7 + row, 3]
                            , text: s.name
                            , horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[7 + row, 4], text: s.id);

                        //i = 0;
                        //foreach (var c in cols)
                        //{
                        //    SetCell(worksheet.Cells[7 + row, 6 + i], text: "");
                        //    i++;
                        //}
                    }
                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report8(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var studentList = GetDataReport3(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet, cols, $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 5, "ใบรายชื่อ");

                    // SetCell(worksheet.Cells[6, 1], text: "ลำดับ");
                    SetCell(worksheet.Cells[6, 1], text: "เลขที่");
                    SetCell(worksheet.Cells[6, 2], text: "เลขประจำตัวนักเรียน");
                    worksheet.Column(2).Width = 100;
                    SetCell(worksheet.Cells[6, 3], text: "ชื่อ - นามสกุล");
                    worksheet.Column(3).Width = 100;

                    int i = 0;
                    foreach (var c in cols)
                    {
                        SetCell(worksheet.Cells[6, 4 + i], text: c);
                        i++;
                    }


                    for (int row = 0; row < r.students.Count; row++)
                    {
                        var s = r.students[row];
                        //SetCell(worksheet.Cells[7 + row, 1], text: (row + 1) + "");
                        SetCell(worksheet.Cells[7 + row, 1], text: s.no + "");
                        SetCell(worksheet.Cells[7 + row, 2], text: s.code);
                        SetCell(worksheet.Cells[7 + row, 3]
                            , text: s.name
                            , horizotal: ExcelHorizontalAlignment.Left);

                        //i = 0;
                        //foreach (var c in cols)
                        //{
                        //    SetCell(worksheet.Cells[7 + row, 5 + i], text: "");
                        //    i++;
                        //}
                    }

                    worksheet.Cells.AutoFitColumns(15, 30);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }

        private ExcelPackage Report9(int? year, string term, int? lvl1, string lvl2, string name, List<string> cols, List<int> status)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var studentList = GetDataReport9(year, term, lvl1, lvl2, name, dbschool, status);

                ExcelPackage excel = new ExcelPackage();

                foreach (var r in studentList)
                {
                    string worksheetName = $@"{r.level1}_{r.level2}";

                    var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                    // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                    //worksheet.DefaultColWidth = 50;
                    SetSheetHeaderT3(worksheet, cols, $"{r.level1}/{r.level2} ปีการศึกษา {r.term}/{r.year} ครูประจำชั้น : {r.teacher} ชาย {r.male} คน หญิง {r.female} คน รวม {(r.male + r.female)} คน"
                        , 8, "ใบรายชื่อ");

                    var COL = 4d;
                    var rowMax = Math.Ceiling(r.students.Count / COL);

                    {
                        for (int i = 0; i < rowMax; i++)
                        {
                            var startRow = (i) * 5;

                            worksheet.Row(7 + startRow).Height = 180;
                            for (int col = 0; col < COL; col++)
                            {
                                var startCol = col * 2;

                                var s = r.students.ElementAtOrDefault((startRow - i) + col);

                                if (s == null) continue;

                                if (!string.IsNullOrEmpty(s.picture))
                                {
                                    var request = WebRequest.Create(s.picture);

                                    System.Drawing.Image img;

                                    using (var response = request.GetResponse())
                                    using (var stream = response.GetResponseStream())
                                    {
                                        img = System.Drawing.Image.FromStream(stream);
                                    }

                                    using (img)
                                    {
                                        var excelImage = worksheet.Drawings.AddPicture("student" + s.code, img);

                                        excelImage.SetPosition(7 + startRow - 1, 20, startCol, 25);

                                        excelImage.SetSize(90);
                                    }
                                }

                                SetCell(worksheet.Cells[7 + startRow + 0, startCol + 1, 7 + startRow + 0, startCol + 2], text: $"", isMerge: true);
                                SetCell(worksheet.Cells[7 + startRow + 1, startCol + 1, 7 + startRow + 1, startCol + 2], text: $"รหัส: {s.code}", isMerge: true);
                                SetCell(worksheet.Cells[7 + startRow + 2, startCol + 1, 7 + startRow + 2, startCol + 2], text: $"ชื่อ: {s.name}", isMerge: true);
                                SetCell(worksheet.Cells[7 + startRow + 3, startCol + 1, 7 + startRow + 3, startCol + 2], text: $"เลขบัตรประชาชน: {s.id}", isMerge: true);
                                SetCell(worksheet.Cells[7 + startRow + 4, startCol + 1, 7 + startRow + 4, startCol + 2], text: $"วันเกิด: {s.date}", isMerge: true);
                            }
                        }

                    }

                    worksheet.Cells.AutoFitColumns(15, 20);
                }

                if (studentList.Count == 0)
                {
                    string worksheetName = $"ไม่มีข้อมูล";
                    excel.Workbook.Worksheets.Add(worksheetName);
                    var worksheet = excel.Workbook.Worksheets[worksheetName];
                }

                return excel;
            };
        }
        #endregion

        private List<Report1VM> GetDataReport1(int? year, string term, int? lvl1, string lvl2, string name, JabJaiEntities dbschool, List<int> status)
        {

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                // var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var MasterTuser = dbmaster.TUsers.Where(w => w.nCompany == userData.CompanyID && w.cType == "0").ToList();



                //var q1 = dbschool.VGetAllStudentInfoByRooms.Where(o => o.SchoolID == userData.CompanyID && o.cDel == null);

                //if (!string.IsNullOrEmpty(name))
                //{
                //    q1 = q1.Where(w => (w.sName + " " + w.sLastname).Contains(name));
                //}

                //var q2 = dbschool.TB_StudentViews.Where(o => o.SchoolID == userData.CompanyID
                //&& (o.cDel == null || o.cDel == "G")
                //&& (o.nStudentStatus == null || o.nStudentStatus == 0)); // && o.numberYear == data.year && o.nTerm == data.term_id 

                var q2 = dbschool.TB_StudentViews.Where(o => o.SchoolID == userData.CompanyID
                && (o.cDel ?? "0") != "1" && status.Contains(o.nStudentStatus ?? 0));


                if (!string.IsNullOrEmpty(name))
                {
                    q2 = q2.Where(w => (w.sName + " " + w.sLastname).Contains(name));
                }

                if (!string.IsNullOrEmpty(lvl2))
                {
                    q2 = q2.Where(w => w.nTermSubLevel2 + "" == lvl2);
                }

                if (lvl1.HasValue)
                {
                    q2 = q2.Where(w => w.nTSubLevel == lvl1);
                }

                if (!string.IsNullOrEmpty(term))
                {
                    q2 = q2.Where(w => w.nTerm == term);
                }

                var qry = (from a in q2

                           from b in dbschool.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).DefaultIfEmpty()

                           let shi = dbschool.TStudentHealthInfoes.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID && o.sDeleted == "false").FirstOrDefault()

                           from jf in dbschool.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID && w.sID == a.sID).DefaultIfEmpty()

                           from c in dbschool.TClassMembers.Where(o => o.SchoolID == userData.CompanyID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2).DefaultIfEmpty()

                           from d in dbschool.TEmployees.Where(o => o.sEmp == c.nTeacherHeadid).DefaultIfEmpty()

                           from h in dbschool.TLevels.Where(w => w.SchoolID == userData.CompanyID && w.LevelID == a.nTLevel).DefaultIfEmpty()

                           select new
                           {
                               a = b,
                               b = a,
                               jf,
                               sortValue = h.sortValue,
                               a.SubLevel,
                               a.nTSubLevel2,
                               a.numberYear,
                               a.sTerm,
                               teacher = d.sName + " " + d.sLastname,
                               shi,
                           })
                           .ToList();

                var d_provinces = dbmaster.provinces.Select(s => new ModelItem1 { Id = s.PROVINCE_ID + "", Name = s.PROVINCE_NAME }).ToList();
                var d_districts = dbmaster.districts.Select(s => new ModelItem1 { Id = s.DISTRICT_ID + "", Name = s.DISTRICT_NAME }).ToList();
                var d_amphurs = dbmaster.amphurs.Select(s => new ModelItem1 { Id = s.AMPHUR_ID + "", Name = s.AMPHUR_NAME }).ToList();
                var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();
                var nation = dbschool.TMasterDatas.Where(w => w.MasterType == "3").ToList();
                var religion = dbschool.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                var race = dbschool.TMasterDatas.Where(w => w.MasterType == "9").ToList();

                string query = string.Format(@"
SELECT shg1.*
FROM TStudentHealthGrowth shg1 INNER JOIN
(
    SELECT ROW_NUMBER() OVER(PARTITION BY nHealthID ORDER BY nTSubLevel DESC, NewnMonth DESC) 'SEQ', shg0.*
    FROM
    (
        SELECT nHealthID, nTSubLevel, nMonth, (CASE nMonth WHEN 5 THEN 1 WHEN 8 THEN 2 WHEN 11 THEN 3 WHEN 2 THEN 4 END) 'NewnMonth'
        FROM TStudentHealthGrowth
        WHERE SchoolID = {0} {1}
    ) shg0
) shg2
ON shg1.nHealthID = shg2.nHealthID AND shg1.nTSubLevel = shg2.nTSubLevel AND shg1.nMonth = shg2.nMonth
WHERE shg2.SEQ = 1 AND SchoolID = {0}", userData.CompanyID, lvl1.HasValue ? " AND nTSubLevel = " + lvl1 : "");

                var studentHealthGrowth = dbschool.Database.SqlQuery<TStudentHealthGrowth>(query).ToList();

                var query2 = string.Format($@"

SELECT u.sID , ISNULL(C.NFC1, '')+','+ISNULL(C.NFC2, '')+','+ISNULL(C.NFC3, '') 'Card'

FROM TUser u LEFT JOIN
(
	SELECT c.sID
	, MAX(CASE WHEN c.[No] = 1 THEN c.NFC END) 'NFC1', MAX(CASE WHEN c.[No] = 2 THEN c.NFC END) 'NFC2', MAX(CASE WHEN c.[No] = 3 THEN c.NFC END) 'NFC3'
    , MAX(CASE WHEN c.[No] = 1 THEN c.NFCReverse END) 'NFCRe1', MAX(CASE WHEN c.[No] = 2 THEN c.NFCReverse END) 'NFCRe2', MAX(CASE WHEN c.[No] = 3 THEN c.NFCReverse END) 'NFCRe3'
	, MAX(CASE WHEN c.[No] = 1 THEN c.NFCEncrypt END) 'ENNFC1', MAX(CASE WHEN c.[No] = 2 THEN c.NFCEncrypt END) 'ENNFC2', MAX(CASE WHEN c.[No] = 3 THEN c.NFCEncrypt END) 'ENNFC3'
	, MAX(CASE WHEN c.[No] = 1 THEN c.NFCEncryptReverse END) 'ENNFCRe1', MAX(CASE WHEN c.[No] = 2 THEN c.NFCEncryptReverse END) 'ENNFCRe2', MAX(CASE WHEN c.[No] = 3 THEN c.NFCEncryptReverse END) 'ENNFCRe3'
	, MAX(CASE WHEN c.[No] = 1 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive1', MAX(CASE WHEN c.[No] = 2 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive2', MAX(CASE WHEN c.[No] = 3 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive3'
	, MAX(CASE WHEN c.[No] = 1 THEN a.sName + ' ' + a.sLastname END) 'UpdateBy1', MAX(CASE WHEN c.[No] = 2 THEN a.sName + ' ' + a.sLastname END) 'UpdateBy2', MAX(CASE WHEN c.[No] = 3 THEN a.sName + ' ' + a.sLastname END) 'UpdateBy3'
		, MAX(CASE WHEN c.[No] = 1 THEN c.Modified END) 'UpdateDate1', MAX(CASE WHEN c.[No] = 2 THEN c.Modified END) 'UpdateDate2', MAX(CASE WHEN c.[No] = 3 THEN c.Modified END) 'UpdateDate3'
	FROM TUser_Card c 
    LEFT JOIN TUser a ON C.SchoolID = a.nCompany AND c.ModifyBy = a.sID
	WHERE c.SchoolID={userData.CompanyID} AND c.IsDel=0  AND ISNULL(c.NFC, '') <> '' 

	GROUP BY c.sID
) C ON u.sID = C.sID
--/studentcardregister/studentcardregister.aspx/returnlist()[1]
WHERE u.nCompany={userData.CompanyID} AND u.cType='0'");

                var studentCard = dbmaster.Database.SqlQuery<StudentCard>(query2).ToList();

                var d1 = qry

                    .GroupBy(o => new
                    {
                        o.sTerm,
                        o.numberYear,
                        o.teacher,
                        o.SubLevel,
                        o.nTSubLevel2,
                        o.sortValue,
                    })
                    .Select(o => new Report1VM
                    {
                        term = o.Key.sTerm,
                        year = o.Key.numberYear,
                        teacher = o.Key.teacher,
                        level1 = o.Key.SubLevel,
                        level2 = o.Key.nTSubLevel2,
                        sortValue = o.Key.sortValue,
                        male = o.Count(i => i.a.cSex == "0"),
                        female = o.Count(i => i.a.cSex == "1"),

                        students = (from a in o

                                    from e in MasterTuser.Where(i => i.sID == a.a.sID).DefaultIfEmpty()

                                    from c in studentHealthGrowth.Where(i => i.nHealthID == a?.shi?.nHealthID).DefaultIfEmpty()

                                    from d in studentCard.Where(i => i.sID == a.a.sID).DefaultIfEmpty()

                                        //orderby new { a.b.nTSubLevel, a.b.nTSubLevel2 }
                                    select new Report1VM.Report1StudentVM
                                    {
                                        levelId = a.b.nTSubLevel,
                                        level2Id = a.b.nTermSubLevel2,
                                        tterm = a.b.nTerm.Trim(),
                                        levelname = a.b.SubLevel,
                                        level2name = a.b.SubLevel + "/" + a.b.nTSubLevel2,

                                        //ข้อมูลนักเรียน
                                        student_number = a.b.nStudentNumber,
                                        studentStatus = (a.b?.nStudentStatus ?? 0),
                                        sidentification = a.a == null ? "" : a.a.sIdentification,
                                        studentId = a.b.sStudentID,
                                        studentPassWord = e == null ? "" : (e.sPassword == null ? "0" : e.sPassword),

                                        stMoveIn = a.b.moveInDate.HasValue ? a.b.moveInDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : string.Empty,
                                        studentsex = a.a.cSex,
                                        titleDes = getTitlte(q_titles, a.a.sStudentTitle),
                                        studentname = a.a.sName == null ? "" : a.a.sName,
                                        studentlastname = a.a.sLastname == null ? "" : a.a.sLastname,
                                        stNickName = a.a.sNickName == null ? "" : a.a.sNickName,
                                        studentnameEN = a.a.sStudentNameEN == null ? "" : a.a.sStudentNameEN,
                                        studentlastnameEN = a.a.sStudentLastEN == null ? "" : a.a.sStudentLastEN,
                                        stNickNameEN = a.a.sNickNameEN == null ? "" : a.a.sNickNameEN,

                                        birth = a.a.dBirth.HasValue ? a.a.dBirth.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : string.Empty,

                                        stReligion = Common.geTReligion(religion, a.a.sStudentReligion),
                                        stNation = Common.geTNation(nation, a.a.sStudentNation),
                                        stRace = Common.geTRace(race, a.a.sStudentRace),

                                        stSonTotal = a.jf == null ? null : a.jf.nSonTotal,
                                        stSonNumber = a.a.nSonNumber == null ? null : a.a.nSonNumber,
                                        stRelativeHere = a.jf == null ? null : a.jf.nRelativeStudyHere,
                                        phone = a.a.sPhone == null ? "" : a.a.sPhone,
                                        stEmail = a.a.sEmail == null ? "" : a.a.sEmail,
                                        money = string.Format("{0:#,#0}", a.a.nMoney),

                                        //ที่อยู่ตามทะเบียนบ้าน
                                        stHomeRegistCode = a.a.sStudentHomeRegisterCode == null ? "" : a.a.sStudentHomeRegisterCode,
                                        homeRegistNumber = a.jf == null ? "" : a.jf.houseRegistrationNumber,
                                        homeRegistSoy = a.jf == null ? "" : a.jf.houseRegistrationSoy,
                                        homeRegistMuu = a.jf == null ? "" : a.jf.houseRegistrationMuu,
                                        homeRegistRoad = a.jf == null ? "" : a.jf.houseRegistrationRoad,
                                        homeRegistTumbon = GetAddress(d_districts, a.jf.houseRegistrationTumbon?.ToString()),
                                        homeRegistAumpher = GetAddress(d_amphurs, a.jf.houseRegistrationAumpher?.ToString()),
                                        homeRegistProvince = GetAddress(d_provinces, a.jf.houseRegistrationProvince?.ToString()),
                                        homeRegistPost = a.jf == null ? "" : a.jf.houseRegistrationPost,
                                        homeRegistPhone = a.jf == null ? "" : a.jf.houseRegistrationPhone,
                                        bornFrom = a.jf == null ? "" : a.jf.bornFrom,
                                        bornFromTumbon = GetAddress(d_districts, a.jf.bornFromTumbon?.ToString()),
                                        bornFromAumpher = GetAddress(d_amphurs, a.jf.bornFromAumpher?.ToString()),
                                        bornFromProvince = GetAddress(d_provinces, a.jf.bornFromProvince?.ToString()),
                                        //ที่อยู่ปัจจุบัน
                                        homeNumber = a.a.sStudentHomeNumber == null ? "" : a.a.sStudentHomeNumber,
                                        muu = a.a.sStudentMuu == null ? "" : a.a.sStudentMuu,
                                        soy = a.a.sStudentSoy == null ? "" : a.a.sStudentSoy,
                                        road = a.a.sStudentRoad == null ? "" : a.a.sStudentRoad,
                                        tumbon = GetAddress(d_districts, a.a.sStudentTumbon ?? ""),
                                        aumpher = GetAddress(d_amphurs, a.a.sStudentAumpher ?? ""),
                                        provin = GetAddress(d_provinces, a.a.sStudentProvince ?? ""),
                                        post = a.a.sStudentPost == null ? "" : a.a.sStudentPost,
                                        stHousePhone = a.a.sStudentHousePhone == null ? "" : a.a.sStudentHousePhone,

                                        //อาศัยอยู่กับ
                                        ststayWithName = a.jf == null ? "" : a.jf.stayWithName,
                                        ststayWithLast = a.jf == null ? "" : a.jf.stayWithLast,
                                        ststayHomeType = a.jf == null ? null : a.jf.HomeType,
                                        ststayWithEmail = a.jf == null ? "" : a.jf.stayWithEmail,
                                        ststayWithEmergency = a.jf == null ? "" : a.jf.stayWithEmergencyCall,

                                        //เพื่อนใกล้บ้าน
                                        friNearHomename = a.jf == null ? "" : a.jf.friendName,
                                        friNearHomelast = a.jf == null ? "" : a.jf.friendLastName,
                                        friNearHomephone = a.jf == null ? "" : a.jf.friendPhone,

                                        //โรงเรียนเดิม
                                        stOldSchoolName = a.a.oldSchoolName == null ? "" : a.a.oldSchoolName,
                                        stOldSchoolTumbon = GetAddress(d_districts, a.a.oldSchoolTumbon == null ? "" : a.a.oldSchoolTumbon),
                                        stOldSchoolAumpher = GetAddress(d_amphurs, a.a.oldSchoolAumpher == null ? "" : a.a.oldSchoolAumpher),
                                        stOldSchoolProvince = GetAddress(d_provinces, a.a.oldSchoolProvince == null ? "" : a.a.oldSchoolProvince),
                                        stOldSchoolGraduated = a.a.oldSchoolGraduated == null ? "" : a.a.oldSchoolGraduated,
                                        stOldSchoolGPA = a.a.oldSchoolGPA == null ? null : a.a.oldSchoolGPA,
                                        stmoveOutReason = a.a.moveOutReason == null ? "" : a.a.moveOutReason,

                                        //ข้อมูลภูมิลำเนา
                                        stOldhome = a.jf == null ? "" : a.jf.houseRegistrationNumber,
                                        stOldmuu = a.jf == null ? "" : a.jf.houseRegistrationMuu,
                                        stOldsoy = a.jf == null ? "" : a.jf.houseRegistrationSoy,
                                        stOldroad = a.jf == null ? "" : a.jf.houseRegistrationRoad,
                                        stOldtumbon = GetAddress(d_districts, a.jf == null ? "" : a.jf.houseRegistrationTumbon.ToString()),
                                        stOldaumper = GetAddress(d_amphurs, a.jf == null ? "" : a.jf.houseRegistrationAumpher.ToString()),
                                        stOldprovince = GetAddress(d_provinces, a.jf == null ? "" : a.jf.houseRegistrationProvince.ToString()),
                                        stOldpostcode = a.jf == null ? "" : a.jf.houseRegistrationPost,
                                        stOldphone = a.jf == null ? "" : a.jf.houseRegistrationPhone,

                                        //ข้อมูลผู้ปกครอง
                                        famRelate = a.jf == null ? "" : a.jf.sFamilyRelate,
                                        famTitle = a.jf == null ? "" : getTitlte(q_titles, a.jf.sFamilyTitle),
                                        famName = a.jf == null ? "" : a.jf.sFamilyName,
                                        famlastname = a.jf == null ? "" : a.jf.sFamilyLast,
                                        famNameEN = a.jf == null ? "" : a.jf.sFamilyNameEN,
                                        famlastnameEN = a.jf == null ? "" : a.jf.sFamilyLastEN,
                                        famBirday = a.jf == null ? "" : a.jf.dFamilyBirthDay.HasValue ? a.jf.dFamilyBirthDay.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : string.Empty,
                                        famReligion = a.jf == null ? "" : Common.geTReligion(religion, a.jf.sFamilyReligion),
                                        famRace = a.jf == null ? "" : Common.geTRace(race, a.jf.sFamilyRace),
                                        famNation = a.jf == null ? "" : Common.geTNation(nation, a.jf.sFamilyNation),
                                        famhome = a.jf == null ? "" : a.jf.sFamilyHomeNumber,
                                        fammuu = a.jf == null ? "" : a.jf.sFamilyMuu,
                                        famsoy = a.jf == null ? "" : a.jf.sFamilySoy,
                                        famroad = a.jf == null ? "" : a.jf.sFamilyRoad,
                                        famtumbon = GetAddress(d_districts, a.jf == null ? "" : a.jf.sFamilyTumbon),
                                        famaumper = GetAddress(d_amphurs, a.jf == null ? "" : a.jf.sFamilyAumpher),
                                        famprovince = GetAddress(d_provinces, a.jf == null ? "" : a.jf.sFamilyProvince),
                                        fampostcode = a.jf == null ? "" : a.jf.sFamilyPost,
                                        famphone1 = a.jf == null ? "" : a.jf.sPhoneOne,
                                        famphone2 = a.jf == null ? "" : a.jf.sPhoneTwo,
                                        famphone3 = a.jf == null ? "" : a.jf.sPhoneThree,
                                        famstatus = a.jf == null ? null : a.jf.familyStatus,
                                        fameducation = a.jf == null ? null : a.jf.sFamilyGraduated,
                                        famJob = a.jf == null ? "" : a.jf.sFamilyJob,
                                        famJobTower = a.jf == null ? "" : a.jf.sFamilyWorkPlace,
                                        famJobSalaryMonth = a.jf == null ? "-" : a.jf.nFamilyIncome?.ToString("#,0.#"),
                                        famJobSalary = a.jf == null ? "-" : CalcIncomSalary(a.jf.nFamilyIncome),
                                        famWithdrawMoney = a.jf == null ? null : a.jf.nFamilyRequestStudyMoney,

                                        //ข้อมูลบิดา
                                        faterTitle = a.jf == null ? "" : getTitlte(q_titles, a.jf.sFatherTitle),
                                        faterName = a.jf == null ? "" : a.jf.sFatherFirstName,
                                        faterLastname = a.jf == null ? "" : a.jf.sFatherLastName,
                                        faterNameEN = a.jf == null ? "" : a.jf.sFatherNameEN,
                                        faterLastnameEN = a.jf == null ? "" : a.jf.sFatherLastEN,
                                        faterBirday = a.jf == null ? null : a.jf.dFatherBirthDay.HasValue ? a.jf.dFatherBirthDay.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : string.Empty,
                                        faterReligion = a.jf == null ? "" : Common.geTReligion(religion, a.jf.sFatherReligion),
                                        faterRace = a.jf == null ? "" : Common.geTRace(race, a.jf.sFatherRace),
                                        faterNation = a.jf == null ? "" : Common.geTNation(nation, a.jf.sFatherNation),
                                        faterhome = a.jf == null ? "" : a.jf.sFatherHomeNumber,
                                        fatermuu = a.jf == null ? "" : a.jf.sFatherMuu,
                                        fatersoy = a.jf == null ? "" : a.jf.sFatherSoy,
                                        faterroad = a.jf == null ? "" : a.jf.sFatherRoad,
                                        fatertumbon = GetAddress(d_districts, a.jf == null ? "" : a.jf.sFatherTumbon),
                                        fateraumper = GetAddress(d_amphurs, a.jf == null ? "" : a.jf.sFatherAumpher),
                                        faterprovince = GetAddress(d_provinces, a.jf == null ? "" : a.jf.sFatherProvince),
                                        faterpostcode = a.jf == null ? "" : a.jf.sFatherPost,
                                        faterphone1 = a.jf == null ? "" : a.jf.sFatherPhone,
                                        faterphone2 = a.jf == null ? "" : a.jf.sFatherPhone2,
                                        faterphone3 = a.jf == null ? "" : a.jf.sFatherPhone3,
                                        fatereducation = a.jf == null ? null : a.jf.sFatherGraduated,
                                        faterJob = a.jf == null ? "" : a.jf.sFatherJob,
                                        faterJobTower = a.jf == null ? "" : a.jf.sFatherWorkPlace,
                                        faterJobSalaryMonth = a.jf == null ? "-" : a.jf.nFatherIncome?.ToString("#,0.#"),
                                        faterJobSalary = a.jf == null ? "-" : CalcIncomSalary(a.jf.nFatherIncome),//null : string.Format("{0:#,#0}", a.jf.nFatherIncome),

                                        //ข้อมูลมารดา
                                        moterTitle = a.jf == null ? "" : getTitlte(q_titles, a.jf.sMotherTitle),
                                        moterName = a.jf == null ? "" : a.jf.sMotherFirstName,
                                        moterLastname = a.jf == null ? "" : a.jf.sMotherLastName,
                                        moterNameEN = a.jf == null ? "" : a.jf.sMotherNameEN,
                                        moterLastnameEN = a.jf == null ? "" : a.jf.sMotherLastEN,
                                        moterBirday = a.jf == null ? null : a.jf.dMotherBirthDay.HasValue ? a.jf.dMotherBirthDay.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : string.Empty,
                                        moterReligion = a.jf == null ? "" : Common.geTReligion(religion, a.jf.sMotherReligion),
                                        moterRace = a.jf == null ? "" : Common.geTRace(race, a.jf.sMotherRace),
                                        moterNation = a.jf == null ? "" : Common.geTNation(nation, a.jf.sMotherNation),
                                        moterhome = a.jf == null ? "" : a.jf.sMotherHomeNumber,
                                        motermuu = a.jf == null ? "" : a.jf.sMotherMuu,
                                        motersoy = a.jf == null ? "" : a.jf.sMotherSoy,
                                        moterroad = a.jf == null ? "" : a.jf.sMotherRoad,
                                        motertumbon = GetAddress(d_districts, a.jf == null ? "" : a.jf.sMotherTumbon),
                                        moteraumper = GetAddress(d_amphurs, a.jf == null ? "" : a.jf.sMotherAumpher),
                                        moterprovince = GetAddress(d_provinces, a.jf == null ? "" : a.jf.sMotherProvince),
                                        moterpostcode = a.jf == null ? "" : a.jf.sMotherPost,
                                        moterphone1 = a.jf == null ? "" : a.jf.sMotherPhone,
                                        moterphone2 = a.jf == null ? "" : a.jf.sMotherPhone2,
                                        moterphone3 = a.jf == null ? "" : a.jf.sMotherPhone3,
                                        motereducation = a.jf == null ? null : a.jf.sMotherGraduated,
                                        moterJob = a.jf == null ? "" : a.jf.sMotherJob,
                                        moterJobTower = a.jf == null ? "" : a.jf.sMotherWorkPlace,
                                        moterJobSalaryMonth = a.jf == null ? "-" : a.jf.nMotherIncome?.ToString("#,0.#"),
                                        moterJobSalary = a.jf == null ? "-" : CalcIncomSalary(a.jf.nMotherIncome),

                                        //ข้อมูลสุขภาพ
                                        stdWeight = c?.Weight,
                                        stdHeight = c?.Height,
                                        stdBlood = a.shi?.sBlood,
                                        stdSickFood = a.shi?.sSickFood,
                                        stdSickDruq = a.shi?.sSickDrug,
                                        stdSickOther = a.shi?.sSickOther,
                                        stdSickNormal = a.shi?.sSickNormal,
                                        stdSickDanger = a.shi?.sSickDanger,

                                        JourneyType = a.a.JourneyType.HasValue
                                        ? (a.a.JourneyType == 1 ? "ไป-กลับ" : a.a.DormitoryName)
                                        : "",

                                        CardNFC = string.Join(",", d.Card.Split(',').Where(i => !string.IsNullOrEmpty(i)))
                                    })
                                    .OrderBy(i => i.student_number).ThenBy(i => i.studentId)
                                    .ToList()
                    })
                    .OrderBy(o => o.level1).ThenBy(o => o.level2)
                    .ToList();

                return d1;
            }
        }

        private List<Report3VM> GetDataReport2(int? year, string term, int? lvl1, string lvl2, string name, JabJaiEntities dbschool, List<int> status)
        {
            //var arrDisplay = new List<int?>() { 0, 4, null };
            var q1 = from a in dbschool.TB_StudentViews
                        .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == year && o.nTerm == term && (o.cDel ?? "0") != "1" && status.Contains(o.nStudentStatus ?? 0))
                     from b in dbschool.TClassMembers
                      .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                      .DefaultIfEmpty()
                     from c in dbschool.TEmployees.Where(o => o.sEmp == b.nTeacherHeadid).DefaultIfEmpty()
                     from d in dbschool.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).DefaultIfEmpty()

                     select new
                     {
                         a.titleDescription,
                         a.sName,
                         a.sLastname,
                         student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                         studentEn = a.sStudentNameEN + " " + a.sStudentLastEN,
                         a.nTerm,
                         a.SubLevel,
                         a.nTSubLevel,
                         a.nTSubLevel2,
                         a.nTermSubLevel2,
                         a.sStudentID,
                         a.nStudentNumber,
                         a.numberYear,
                         a.sTerm,
                         d.sNickName,
                         d.dBirth,
                         d.cSex,
                         d.sIdentification,
                         teacher = c.sName + " " + c.sLastname,
                     };

            if (!string.IsNullOrEmpty(name))
            {
                q1 = q1.Where(o => o.student.Contains(name));
            }

            if (lvl1.HasValue)
                q1 = q1.Where(o => o.nTSubLevel == lvl1);

            if (!string.IsNullOrEmpty(lvl2))
                q1 = q1.Where(o => o.nTermSubLevel2 + "" == lvl2);

            var studentList = q1
                 .ToList()
                 .GroupBy(o => new
                 {
                     o.teacher,
                     o.sTerm,
                     o.numberYear,
                     o.SubLevel,
                     o.nTSubLevel2,
                 })
                .Select(o => new Report3VM
                {
                    teacher = o.Key.teacher,
                    year = o.Key.numberYear,
                    term = o.Key.sTerm,
                    level1 = o.Key.SubLevel,
                    level2 = o.Key.nTSubLevel2,
                    male = o.Count(i => i.cSex == "0"),
                    female = o.Count(i => i.cSex == "1"),

                    students = o.Select(i => new Report3VM.Report3StudentVM
                    {
                        no = i.nStudentNumber,
                        code = i.sStudentID,
                        title = i.titleDescription,
                        fname = i.sName,
                        lname = i.sLastname,
                        name = i.student,
                        nameEn = i.studentEn,
                        nick = i.sNickName,
                        id = i.sIdentification,
                        date = i.dBirth?.ToString(@"dd MMM yyyy", new CultureInfo("th-TH"))
                    })
                    .OrderBy(i => i.no)
                    .ToList()

                })
                .OrderBy(o => o.level1).ThenBy(o => o.level2)
                .ToList();

            return studentList;
        }

        private List<Report3VM> GetDataReport3(int? year, string term, int? lvl1, string lvl2, string name, JabJaiEntities dbschool, List<int> status)
        {
            //var arrDisplay = new List<int?>() { 0, 4, null };
            var q1 = from a in dbschool.TB_StudentViews
                        .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == year && o.nTerm == term
                        && (o.cDel ?? "0") != "1" && status.Contains(o.nStudentStatus ?? 0))
                     from b in dbschool.TClassMembers
                      .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                      .DefaultIfEmpty()
                     from c in dbschool.TEmployees.Where(o => o.sEmp == b.nTeacherHeadid).DefaultIfEmpty()
                     from d in dbschool.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).DefaultIfEmpty()

                     select new ModelReportType3
                     {
                         title = a.titleDescription,
                         fname = a.sName,
                         lname = a.sLastname,
                         student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                         nTerm = a.nTerm,
                         SubLevel = a.SubLevel,
                         nTSubLevel = a.nTSubLevel,
                         nTSubLevel2 = a.nTSubLevel2,
                         nTermSubLevel2 = a.nTermSubLevel2,
                         sStudentID = a.sStudentID,
                         nStudentNumber = a.nStudentNumber,
                         numberYear = a.numberYear,
                         sTerm = a.sTerm,
                         sNickName = d.sNickName,
                         dBirth = d.dBirth,
                         cSex = d.cSex,
                         sIdentification = d.sIdentification,
                         teacher = c.sName + " " + c.sLastname,
                     };

            if (!string.IsNullOrEmpty(name))
            {
                q1 = q1.Where(o => o.student.Contains(name));
            }

            if (lvl1.HasValue)
                q1 = q1.Where(o => o.nTSubLevel == lvl1);

            if (!string.IsNullOrEmpty(lvl2))
                q1 = q1.Where(o => o.nTermSubLevel2 + "" == lvl2);

            var d1 = q1.ToList();

            d1.ForEach(o =>
            {
                o.age = CalcAge(o.dBirth);
            });

            var studentList = d1
                 .ToList()
                 .GroupBy(o => new
                 {
                     o.teacher,
                     o.sTerm,
                     o.numberYear,
                     o.SubLevel,
                     o.nTSubLevel2,
                 })
                .Select(o => new Report3VM
                {
                    teacher = o.Key.teacher,
                    year = o.Key.numberYear,
                    term = o.Key.sTerm,
                    level1 = o.Key.SubLevel,
                    level2 = o.Key.nTSubLevel2,
                    male = o.Count(i => i.cSex == "0"),
                    female = o.Count(i => i.cSex == "1"),

                    students = o.Select(i => new Report3VM.Report3StudentVM
                    {
                        no = i.nStudentNumber,
                        code = i.sStudentID,
                        title = i.title,
                        fname = i.fname,
                        lname = i.lname,
                        name = i.student,
                        nick = i.sNickName,
                        id = i.sIdentification,
                        date = i.dBirth?.ToString(@"dd MMMM yyyy", new CultureInfo("th-TH")),
                        ageYear = i.age?.Year,
                        ageMonth = i.age?.Month,
                    })
                    .OrderBy(i => i.no)
                    .ToList()

                })
                .OrderBy(o => o.level1).ThenBy(o => o.level2)
                .ToList();

            return studentList;
        }

        private List<Report3VM> GetDataReport9(int? year, string term, int? lvl1, string lvl2, string name, JabJaiEntities dbschool, List<int> status)
        {
            var q1 = from a in dbschool.TB_StudentViews
                        .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == year && o.nTerm == term && (o.cDel ?? "0") != "1" && status.Contains(o.nStudentStatus ?? 0))
                     from b in dbschool.TClassMembers
                      .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                      .DefaultIfEmpty()
                     from c in dbschool.TEmployees
                      .Where(o => o.sEmp == b.nTeacherHeadid)
                      .DefaultIfEmpty()

                     from d in dbschool.TUser
                      .Where(o => o.SchoolID == a.SchoolID && o.sID == a.sID)
                      .DefaultIfEmpty()

                     select new
                     {
                         student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                         a.nTerm,
                         a.SubLevel,
                         a.nTSubLevel,
                         a.nTSubLevel2,
                         a.nTermSubLevel2,
                         a.sStudentID,
                         a.nStudentNumber,
                         a.numberYear,
                         a.sTerm,
                         d.sNickName,
                         d.dBirth,
                         d.cSex,
                         d.sIdentification,
                         teacher = c.sName + " " + c.sLastname,
                         picture = d.sStudentPicture,
                     };

            if (!string.IsNullOrEmpty(name))
            {
                q1 = q1.Where(o => o.student.Contains(name));
            }

            if (lvl1.HasValue)
                q1 = q1.Where(o => o.nTSubLevel == lvl1);

            if (!string.IsNullOrEmpty(lvl2))
                q1 = q1.Where(o => o.nTermSubLevel2 + "" == lvl2);

            var studentList = q1
                 .ToList()
                 .GroupBy(o => new
                 {
                     o.teacher,
                     o.sTerm,
                     o.numberYear,
                     o.SubLevel,
                     o.nTSubLevel2,
                 })
                .Select(o => new Report3VM
                {
                    teacher = o.Key.teacher,
                    year = o.Key.numberYear,
                    term = o.Key.sTerm,
                    level1 = o.Key.SubLevel,
                    level2 = o.Key.nTSubLevel2,
                    male = o.Count(i => i.cSex == "0"),
                    female = o.Count(i => i.cSex == "1"),

                    students = o.Select(i => new Report3VM.Report3StudentVM
                    {
                        no = i.nStudentNumber,
                        code = i.sStudentID,
                        name = i.student,
                        nick = i.sNickName,
                        id = i.sIdentification,
                        date = i.dBirth?.ToString(@"dd MMM yyyy", new CultureInfo("th-TH")),
                        picture = i.picture ?? "",
                    })
                    .OrderBy(i => i.no)
                    .ToList()

                })
                .OrderBy(o => o.level1).ThenBy(o => o.level2)
                .ToList();

            return studentList;
        }

        private List<Report6VM> GetDataReport6(int? year, string term, int? lvl1, string lvl2, string name, JabJaiEntities dbschool, List<int> status)
        {
            var q1 = from a in dbschool.TB_StudentViews
                        .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == year && o.nTerm == term && (o.cDel ?? "0") != "1" && status.Contains(o.nStudentStatus ?? 0))
                     from b in dbschool.TClassMembers
                      .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                      .DefaultIfEmpty()
                     from c in dbschool.TEmployees
                      .Where(o => o.sEmp == b.nTeacherHeadid)
                      .DefaultIfEmpty()

                     from d in dbschool.TFamilyProfiles.Where(o => o.SchoolID == userData.CompanyID && o.sID == a.sID).DefaultIfEmpty()
                     from e in dbschool.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).DefaultIfEmpty()

                     select new
                     {
                         student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                         a.nTerm,
                         a.SubLevel,
                         a.nTSubLevel,
                         a.nTSubLevel2,
                         a.nTermSubLevel2,
                         a.sStudentID,
                         a.nStudentNumber,
                         a.numberYear,
                         a.sTerm,
                         e.sNickName,
                         e.dBirth,
                         e.cSex,
                         e.sIdentification,
                         teacher = c.sName + " " + c.sLastname,

                         d.sMotherIdCardNumber,
                         d.sMotherTitle,
                         mother = d.sMotherFirstName + " " + d.sMotherLastName,

                         d.sFatherIdCardNumber,
                         d.sFatherTitle,
                         father = d.sFatherFirstName + " " + d.sFatherLastName,

                         d.sFamilyIdCardNumber,
                         d.sFamilyTitle,
                         family = d.sFamilyName + " " + d.sFamilyLast,
                     };

            if (!string.IsNullOrEmpty(name))
            {
                q1 = q1.Where(o => o.student.Contains(name));
            }

            if (lvl1.HasValue)
                q1 = q1.Where(o => o.nTSubLevel == lvl1);

            if (!string.IsNullOrEmpty(lvl2))
                q1 = q1.Where(o => o.nTermSubLevel2 + "" == lvl2);

            var titleList = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

            var studentList = q1
                 .ToList()
                 .GroupBy(o => new
                 {
                     o.teacher,
                     o.sTerm,
                     o.numberYear,
                     o.SubLevel,
                     o.nTSubLevel2,
                 })
                .Select(o => new Report6VM
                {
                    teacher = o.Key.teacher,
                    year = o.Key.numberYear,
                    term = o.Key.sTerm,
                    level1 = o.Key.SubLevel,
                    level2 = o.Key.nTSubLevel2,
                    male = o.Count(i => i.cSex == "0"),
                    female = o.Count(i => i.cSex == "1"),

                    students = o.Select(i => new Report6VM.Report6StudentVM
                    {
                        no = i.nStudentNumber,
                        code = i.sStudentID,
                        name = i.student,
                        id = i.sIdentification,
                        familyid = i.sFamilyIdCardNumber ?? "-",
                        family = string.IsNullOrWhiteSpace(i.family) ? "-" : getTitlte(titleList, i.sFamilyTitle) + " " + i.family,
                        motherid = i.sMotherIdCardNumber ?? "-",
                        mother = string.IsNullOrWhiteSpace(i.mother) ? "-" : getTitlte(titleList, i.sMotherTitle) + " " + i.mother,
                        fatherid = i.sFatherIdCardNumber ?? "-",
                        father = string.IsNullOrWhiteSpace(i.father) ? "-" : getTitlte(titleList, i.sFatherTitle) + " " + i.father,
                    })
                    .OrderBy(i => i.no)
                    .ToList()

                })
                .OrderBy(o => o.level1).ThenBy(o => o.level2)
                .ToList();

            return studentList;
        }

        private List<Report15VM> GetDataReport15(int? year, string term, int? lvl1, string lvl2, string name, JabJaiEntities dbschool, List<int> status)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {

                var d_provinces = dbmaster.provinces
                    .Select(o => new ModelItem1 { Id = o.PROVINCE_ID + "", Name = o.PROVINCE_NAME }).ToList();
                var d_districts = dbmaster.districts
                    .Select(o => new ModelItem1 { Id = o.DISTRICT_ID + "", Name = o.DISTRICT_NAME }).ToList();
                var d_amphurs = dbmaster.amphurs
                    .Select(o => new ModelItem1 { Id = o.AMPHUR_ID + "", Name = o.AMPHUR_NAME }).ToList();
                var s = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
                var arrDisplay = new List<int?>() { 0, 4, null };

                var qry1 = from a in dbschool.TB_StudentViews
                            .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == year && o.nTerm == term
                              && (o.cDel ?? "0") != "1" && status.Contains(o.nStudentStatus ?? 0))

                           from b in dbschool.TClassMembers
                            .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                            .DefaultIfEmpty()

                           from c in dbschool.TEmployees
                            .Where(o => o.sEmp == b.nTeacherHeadid)
                            .DefaultIfEmpty()

                           from family in dbschool.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID && w.sID == a.sID).DefaultIfEmpty()

                           from d in dbschool.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).DefaultIfEmpty()

                           select new
                           {
                               a.titleDescription,
                               a.sName,
                               a.sLastname,
                               student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                               //studentEn = a.sStudentNameEN + " " + a.sStudentLastEN,
                               a.nTerm,
                               a.SubLevel,
                               a.nTSubLevel,
                               a.nTSubLevel2,
                               a.nTermSubLevel2,
                               a.sStudentID,
                               a.nStudentNumber,
                               a.sTerm,
                               a.numberYear,
                               d.cSex,
                               teacher = c.sName + " " + c.sLastname,

                               number = family.houseRegistrationNumber,
                               soy = family.houseRegistrationSoy,
                               muu = family.houseRegistrationMuu,
                               road = family.houseRegistrationRoad,
                               tumbon = family.houseRegistrationTumbon,
                               aumpher = family.houseRegistrationAumpher,
                               province = family.houseRegistrationProvince,
                               family.houseRegistrationPost,
                               family.houseRegistrationPhone,
                               //homeRegistTumbon = GetAddress(d_districts, family.houseRegistrationTumbon),
                               //homeRegistAumpher = GetAddress(d_amphurs, family.houseRegistrationAumpher),
                               //homeRegistProvince = GetAddress(d_provinces, family.houseRegistrationProvince),
                           };

                if (!string.IsNullOrEmpty(name))
                    qry1 = qry1.Where(o => o.student.Contains(name));

                if (lvl1.HasValue)
                    qry1 = qry1.Where(o => o.nTSubLevel == lvl1);

                if (!string.IsNullOrEmpty(lvl2))
                    qry1 = qry1.Where(o => o.nTermSubLevel2 + "" == lvl2);

                var studentList = qry1
                    .AsEnumerable()
                    .GroupBy(o => new
                    {
                        o.teacher,
                        o.sTerm,
                        o.numberYear,
                        o.SubLevel,
                        o.nTSubLevel2,
                    })
                    .Select(o => new Report15VM
                    {
                        teacher = o.Key.teacher,
                        year = o.Key.numberYear,
                        term = o.Key.sTerm,
                        level1 = o.Key.SubLevel,
                        level2 = o.Key.nTSubLevel2,
                        male = o.Count(i => i.cSex == "0"),
                        female = o.Count(i => i.cSex == "1"),

                        students = o.Select(i => new Report15VM.Report15StudentVM
                        {
                            no = i.nStudentNumber,
                            code = i.sStudentID,
                            name = i.student,
                            title = i.titleDescription,
                            fname = i.sName,
                            lname = i.sLastname,

                            number = i.number,
                            soy = string.IsNullOrEmpty(i.soy) ? "" : "ซอย" + i.soy,
                            muu = string.IsNullOrEmpty(i.muu) ? "" : "หมู่" + i.muu,
                            road = string.IsNullOrEmpty(i.road) ? "" : "ถนน" + i.road,
                            tumbon = i.tumbon.HasValue ? (i.province == 1 ? "เเขวง" : "ตำบล") + GetAddress(d_districts, i.tumbon + "") : "",
                            aumpher = i.aumpher.HasValue ? (i.province == 1 ? "เขต" : "อำเภอ") + GetAddress(d_amphurs, i.aumpher + "") : "",
                            province = i.province.HasValue ? (i.province == 1 ? "" : "จังหวัด") + GetAddress(d_provinces, i.province + "") : "",
                            post = i.houseRegistrationPost,
                            phone = string.IsNullOrEmpty(i.houseRegistrationPhone) ? "" : "โทร" + i.houseRegistrationPhone,
                        })
                        .OrderBy(i => i.no)
                        .ToList()

                    })
                    .OrderBy(o => o.level1).ThenBy(o => o.level2)
                    .ToList();

                return studentList;
            }
        }

        private List<Report16VM> GetDataReport16(int? year, string term, int? lvl1, string lvl2, string name, JabJaiEntities dbschool, List<int> status)
        {
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var d_provinces = dbmaster.provinces
                .Select(o => new ModelItem1 { Id = o.PROVINCE_ID + "", Name = o.PROVINCE_NAME }).ToList();
                var d_districts = dbmaster.districts
                    .Select(o => new ModelItem1 { Id = o.DISTRICT_ID + "", Name = o.DISTRICT_NAME }).ToList();
                var d_amphurs = dbmaster.amphurs
                    .Select(o => new ModelItem1 { Id = o.AMPHUR_ID + "", Name = o.AMPHUR_NAME }).ToList();
                var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                var s = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
                var arrDisplay = new List<int?>() { 0, 4, null };

                var qry1 = from a in dbschool.TB_StudentViews
                               .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == year && o.nTerm == term
                                 && (o.cDel ?? "0") != "1" && status.Contains(o.nStudentStatus ?? 0))

                           from a1 in dbschool.TUser.Where(o => o.SchoolID == a.SchoolID && o.sID == a.sID)

                           from b in dbschool.TClassMembers
                            .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                            .DefaultIfEmpty()

                           from c in dbschool.TEmployees
                            .Where(o => o.sEmp == b.nTeacherHeadid)
                            .DefaultIfEmpty()

                           from family in dbschool.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID && w.sID == a.sID).DefaultIfEmpty()

                           select new
                           {
                               //a.titleDescription,
                               //a.sName,
                               //a.sLastname,
                               student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                               //studentEn = a.sStudentNameEN + " " + a.sStudentLastEN,
                               a.nTerm,
                               a.SubLevel,
                               a.nTSubLevel,
                               a.nTSubLevel2,
                               a.nTermSubLevel2,
                               a.sStudentID,
                               a.nStudentNumber,
                               a.sTerm,
                               a.numberYear,
                               a1.cSex,

                               teacher = c.sName + " " + c.sLastname,

                               a1.dBirth,
                               a1.oldSchoolName,
                               a1.oldSchoolGraduated,
                               a1.moveOutReason,
                               a1.moveInDate,

                               number = a1.sStudentHomeNumber,
                               soy = a1.sStudentSoy,
                               muu = a1.sStudentMuu,
                               road = a1.sStudentRoad,
                               province = a1.sStudentProvince,
                               aumpher = a1.sStudentAumpher,
                               tumbon = a1.sStudentTumbon,
                               a1.sStudentPost,
                               a1.sPhone,

                               family.sFatherTitle,
                               family.sFatherFirstName,
                               family.sFatherLastName,
                               family.sFatherJob,

                               family.sMotherTitle,
                               family.sMotherFirstName,
                               family.sMotherLastName,
                               family.sMotherJob,

                               //number = family.houseRegistrationNumber,
                               //soy = family.houseRegistrationSoy,
                               //muu = family.houseRegistrationMuu,
                               //road = family.houseRegistrationRoad,
                               tumbonHome = family.houseRegistrationTumbon,
                               aumpherHome = family.houseRegistrationAumpher,
                               provinceHome = family.houseRegistrationProvince,
                               //family.houseRegistrationPost,
                               //family.houseRegistrationPhone,
                               //homeRegistTumbon = GetAddress(d_districts, family.houseRegistrationTumbon),
                               //homeRegistAumpher = GetAddress(d_amphurs, family.houseRegistrationAumpher),
                               //homeRegistProvince = GetAddress(d_provinces, family.houseRegistrationProvince),
                           };


                if (!string.IsNullOrEmpty(name))
                    qry1 = qry1.Where(o => o.student.Contains(name));

                if (lvl1.HasValue)
                    qry1 = qry1.Where(o => o.nTSubLevel == lvl1);

                if (!string.IsNullOrEmpty(lvl2))
                    qry1 = qry1.Where(o => o.nTermSubLevel2 + "" == lvl2);

                var studentList = qry1
                    .AsEnumerable()
                    .GroupBy(o => new
                    {
                        o.teacher,
                        o.sTerm,
                        o.numberYear,
                        o.SubLevel,
                        o.nTSubLevel2,
                    })
                    .Select(o => new Report16VM
                    {
                        teacher = o.Key.teacher,
                        year = o.Key.numberYear,
                        term = o.Key.sTerm,
                        level1 = o.Key.SubLevel,
                        level2 = o.Key.nTSubLevel2,
                        male = o.Count(i => i.cSex == "0"),
                        female = o.Count(i => i.cSex == "1"),

                        students = o.Select(i => new Report16VM.Report16StudentVM
                        {
                            no = i.nStudentNumber,
                            code = i.sStudentID,
                            name = i.student,
                            firstDate = i.moveInDate?.ToString("วันที่ dd เดือน MMMM พ.ศ. yyyy", new CultureInfo("th-TH")),
                            birthDay = i.dBirth?.ToString("วันที่ dd เดือน MMMM พ.ศ. yyyy", new CultureInfo("th-TH")),
                            oldSchool = i.oldSchoolName,
                            oldDegree = oldgraduate(i.oldSchoolGraduated),
                            reason = i.moveOutReason,
                            number = i.number,
                            soy = string.IsNullOrEmpty(i.soy) ? "" : "ซอย" + i.soy,
                            muu = string.IsNullOrEmpty(i.muu) ? "" : "หมู่" + i.muu,
                            road = string.IsNullOrEmpty(i.road) ? "" : "ถนน" + i.road,
                            //ตำบล/เเขวง อำเภอ/เขต จังหวัด  โทร                               
                            tumbon = !string.IsNullOrEmpty(i.tumbon) ? (i.province == "1" ? "เเขวง" : "ตำบล") + GetAddress(d_districts, i.tumbon + "") : "",
                            aumpher = !string.IsNullOrEmpty(i.aumpher) ? (i.province == "1" ? "เขต" : "อำเภอ") + GetAddress(d_amphurs, i.aumpher + "") : "",
                            province = !string.IsNullOrEmpty(i.province) ? (i.province == "1" ? "" : "จังหวัด") + GetAddress(d_provinces, i.province + "") : "",
                            post = i.sStudentPost,
                            phone = string.IsNullOrEmpty(i.sPhone) ? "" : "โทร" + i.sPhone,

                            tumbonHome = (i.tumbonHome.HasValue) ? (i.provinceHome == 1 ? "เเขวง" : "ตำบล") + GetAddress(d_districts, i.tumbonHome + "") : "",
                            aumpherHome = (i.aumpherHome.HasValue) ? (i.provinceHome == 1 ? "เขต" : "อำเภอ") + GetAddress(d_amphurs, i.aumpherHome + "") : "",
                            provinceHome = (i.provinceHome.HasValue) ? (i.provinceHome == 1 ? "" : "จังหวัด") + GetAddress(d_provinces, i.provinceHome + "") : "",

                            father = getTitlte(q_titles, i.sFatherTitle) + " " + i.sFatherFirstName + " " + i.sFatherLastName,
                            mother = getTitlte(q_titles, i.sMotherTitle) + " " + i.sMotherFirstName + " " + i.sMotherLastName,
                            fatherJob = i.sFatherJob,
                            motherJob = i.sMotherJob,
                        })
                        .OrderBy(i => i.no)
                        .ToList()

                    })
                    .OrderBy(o => o.level1).ThenBy(o => o.level2)
                    .ToList();

                return studentList;
            }
        }

        private void SetSheetHeaderT3(ExcelWorksheet worksheet
            , List<string> addedCol
            , string summary
            , int tableCol
            , string reportName)
        {
            if (!string.IsNullOrEmpty(school.sImage))
            {
                var request = WebRequest.Create(school.sImage);

                System.Drawing.Image logo;

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    logo = System.Drawing.Image.FromStream(stream);
                }

                using (logo)
                {
                    var excelImage = worksheet.Drawings.AddPicture("School Logo", logo);

                    excelImage.SetPosition(0, 5, 0, 5);
                    excelImage.SetSize(100, 100);
                }
            }

            tableCol = tableCol - 1;
            var addColLength = addedCol.Count;

            SetCell(worksheet.Cells[1, 1, 5, 1], true);

            SetCell(worksheet.Cells[1, 2, 1, tableCol + addColLength]
                , isMerge: true
                , text: school.sCompany
                , horizotal: ExcelHorizontalAlignment.Left
                , fontSize: 14
                , isBold: true);

            SetCell(worksheet.Cells[2, 2, 2, tableCol + addColLength]
                , isMerge: true
                , text: school.sAddress
                , horizotal: ExcelHorizontalAlignment.Left
                , fontSize: 12
                , isBold: true);

            SetCell(worksheet.Cells[3, 2, 3, tableCol + addColLength]
                , isMerge: true
                , text: $"โทรศัพท์ : {school.sPhoneOne + (string.IsNullOrEmpty(school.sPhoneTwo) ? "" : " ," + school.sPhoneTwo)}  โทรสาร : {school.sFax}"
                , horizotal: ExcelHorizontalAlignment.Left
                , fontSize: 12
                , isBold: true);


            SetCell(worksheet.Cells[4, 2, 4, tableCol + addColLength]
               , isMerge: true
               , text: $"{school.sWebsite}  อีเมล์ : {school.sEmailOne + (string.IsNullOrEmpty(school.sEmailTwo) ? "" : " ," + school.sEmailTwo)}"
               , horizotal: ExcelHorizontalAlignment.Left
               , fontSize: 12
               , isBold: true);


            SetCell(worksheet.Cells[5, 2, 5, tableCol + addColLength]
              , isMerge: true
              , text: summary
              , horizotal: ExcelHorizontalAlignment.Left
              , fontSize: 12
              , isBold: true);

            SetCell(worksheet.Cells[1, tableCol + 1 + addColLength, 5, tableCol + 1 + addColLength]
              , isMerge: true
              , text: reportName
              , horizotal: ExcelHorizontalAlignment.Center
              , fontSize: 12
              , isBold: true);
        }


        //private void SetCell(ExcelWorksheet excelWorksheet
        //    , string Cells
        //    , bool isMerge = false
        //    , string text = ""
        //    , int fontSize = 11
        //    , bool isBold = false
        //    , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
        //    , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
        //    )
        //{
        //    using (ExcelRange rng = excelWorksheet.Cells[Cells])
        //    {
        //        rng.Merge = isMerge;
        //        rng.Value = text;
        //        rng.Style.Font.Bold = isBold;
        //        rng.Style.HorizontalAlignment = horizotal;
        //        rng.Style.VerticalAlignment = vetical;
        //        rng.Style.Font.Size = fontSize;
        //    }
        //}

        private static ModelAge CalcAge(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                return null;
            //DateTime birthDate = new DateTime(1956, 8, 27);

            var dateTimeToday = DateTime.UtcNow;
            if (birthDate > dateTimeToday)
                return null;

            var difference = dateTimeToday.Subtract(birthDate.Value);
            var firstDay = new DateTime(1, 1, 1);

            int totalYears = (firstDay + difference).Year - 1;
            int totalMonths = (totalYears * 12) + (firstDay + difference).Month - 1;
            int runningMonths = totalMonths - (totalYears * 12);
            int runningDays = (dateTimeToday - birthDate.Value.AddMonths((totalYears * 12) + runningMonths)).Days;

            return new ModelAge() { Year = totalYears, Month = runningMonths, Days = runningDays };
        }

        private string getTitlte(List<TTitleList> titles, string titlesId)
        {
            int nTitleid = 0;
            int.TryParse((titlesId ?? "0"), out nTitleid);
            var f_titles = titles.FirstOrDefault(f => f.nTitleid == nTitleid);
            if (f_titles == null) return titlesId;
            else return f_titles.titleDescription;
        }

        private string GetAddress(List<ModelItem1> lst, string id)
        {
            return lst.FirstOrDefault(o => o.Id == id)?.Name + "";
        }

        private static string oldgraduate(string index)
        {
            string txt = "";
            if (index == null)
                txt = "";
            else if (index == "11")
                txt = "เตรียมอนุบาลศึกษา 1";
            else if (index == "12")
                txt = "อนุบาลศึกษา 1";
            else if (index == "13")
                txt = "อนุบาลศึกษา 2";
            else if (index == "14")
                txt = "อนุบาลศึกษา 3";
            else if (index == "1")
                txt = "ประถมศึกษาปีที่ 1";
            else if (index == "2")
                txt = "ประถมศึกษาปีที่ 2";
            else if (index == "3")
                txt = "ประถมศึกษาปีที่ 3";
            else if (index == "4")
                txt = "ประถมศึกษาปีที่ 4";
            else if (index == "5")
                txt = "ประถมศึกษาปีที่ 5";
            else if (index == "6")
                txt = "ประถมศึกษาปีที่ 6";
            else if (index == "7")
                txt = "มัธยมศึกษาตอนต้น";
            else if (index == "8")
                txt = "มัธยมศึกษาตอนปลาย";
            else if (index == "9")
                txt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 1";
            else if (index == "15")
                txt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 2";
            else if (index == "16")
                txt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 3";
            else if (index == "10")
                txt = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 1";
            else if (index == "17")
                txt = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 2";
            return txt;
        }

        private string CalcIncomSalary(double? income)
        {
            //ต่ำกว่า 50,000 / 50,001 - 100,000 / 200,001- 300,000/ มากกว่า300,000 )
            if (income.HasValue)
            {
                return (income.Value * 12).ToString("#,0.#");
                //if (income < 50000)
                //{
                //    return "ต่ำกว่า 50,000";
                //}
                //else if (income < 100000)
                //{
                //    return "50,001 - 100,000";
                //}
                //else if (income < 300000)
                //{
                //    return "100,001- 300,000";
                //}
                //else
                //{
                //    return "มากกว่า 300,000";
                //}
            }
            else
            {
                return "-";
            }

        }

        private int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region Model



        internal class Report1VM
        {
            internal int male;
            internal int female;

            public int? year { get; set; }
            public string term { get; set; }

            public string teacher { get; set; }
            public string level1 { get; set; }
            public string level2 { get; set; }
            public List<Report1StudentVM> students { get; set; }
            public int? sortValue { get; internal set; }

            internal class Report1StudentVM
            {
                public int levelId { get; set; }
                public int level2Id { get; set; }
                public string tterm { get; set; }
                public string levelname { get; set; }
                public string level2name { get; set; }
                public int? student_number { get; set; }
                public int? studentStatus { get; set; }
                public string sidentification { get; set; }
                public string studentId { get; set; }
                public string studentPassWord { get; set; }
                public string stMoveIn { get; set; }
                public string studentsex { get; set; }
                public string titleDes { get; set; }
                public string studentname { get; set; }
                public string studentlastname { get; set; }
                public string stNickName { get; set; }
                public string studentnameEN { get; set; }
                public string studentlastnameEN { get; set; }
                public string stNickNameEN { get; set; }
                public string birth { get; set; }
                public string stReligion { get; set; }
                public string stNation { get; set; }
                public string stRace { get; set; }
                public int? stSonTotal { get; set; }
                public int? stSonNumber { get; set; }
                public int? stRelativeHere { get; set; }
                public string phone { get; set; }
                public string stEmail { get; set; }
                public string money { get; set; }
                public string stHomeRegistCode { get; set; }
                public string homeRegistNumber { get; set; }
                public string homeRegistSoy { get; set; }
                public string homeRegistMuu { get; set; }
                public string homeRegistRoad { get; set; }
                public string homeRegistTumbon { get; set; }
                public string homeRegistAumpher { get; set; }
                public string homeRegistProvince { get; set; }
                public string homeRegistPost { get; set; }
                public string homeRegistPhone { get; set; }
                public string bornFrom { get; set; }
                public string bornFromTumbon { get; set; }
                public string bornFromAumpher { get; set; }
                public string bornFromProvince { get; set; }
                public string homeNumber { get; set; }
                public string muu { get; set; }
                public string soy { get; set; }
                public string road { get; set; }
                public string tumbon { get; set; }
                public string aumpher { get; set; }
                public string provin { get; set; }
                public string post { get; set; }
                public string stHousePhone { get; set; }
                public string ststayWithName { get; set; }
                public string ststayWithLast { get; set; }
                public int? ststayHomeType { get; set; }
                public string ststayWithEmail { get; set; }
                public string ststayWithEmergency { get; set; }
                public string friNearHomename { get; set; }
                public string friNearHomelast { get; set; }
                public string friNearHomephone { get; set; }
                public string stOldSchoolName { get; set; }
                public string stOldSchoolTumbon { get; set; }
                public string stOldSchoolAumpher { get; set; }
                public string stOldSchoolProvince { get; set; }
                public string stOldSchoolGraduated { get; set; }
                public decimal? stOldSchoolGPA { get; set; }
                public string stmoveOutReason { get; set; }
                public string stOldhome { get; set; }
                public string stOldmuu { get; set; }
                public string stOldsoy { get; set; }
                public string stOldroad { get; set; }
                public string stOldtumbon { get; set; }
                public string stOldaumper { get; set; }
                public string stOldprovince { get; set; }
                public string stOldpostcode { get; set; }
                public string stOldphone { get; set; }
                public string famRelate { get; set; }
                public string famTitle { get; set; }
                public string famName { get; set; }
                public string famlastname { get; set; }
                public string famNameEN { get; set; }
                public string famlastnameEN { get; set; }
                public string famBirday { get; set; }
                public string famReligion { get; set; }
                public string famRace { get; set; }
                public string famNation { get; set; }
                public string famhome { get; set; }
                public string fammuu { get; set; }
                public string famsoy { get; set; }
                public string famroad { get; set; }
                public string famtumbon { get; set; }
                public string famaumper { get; set; }
                public string famprovince { get; set; }
                public string fampostcode { get; set; }
                public string famphone1 { get; set; }
                public string famphone2 { get; set; }
                public string famphone3 { get; set; }
                public int? famstatus { get; set; }
                public int? fameducation { get; set; }
                public string famJob { get; set; }
                public string famJobTower { get; set; }
                public string famJobSalary { get; set; }
                public int? famWithdrawMoney { get; set; }
                public string faterTitle { get; set; }
                public string faterName { get; set; }
                public string faterLastname { get; set; }
                public string faterNameEN { get; set; }
                public string faterLastnameEN { get; set; }
                public string faterBirday { get; set; }
                public string faterReligion { get; set; }
                public string faterRace { get; set; }
                public string faterNation { get; set; }
                public string faterhome { get; set; }
                public string fatermuu { get; set; }
                public string fatersoy { get; set; }
                public string faterroad { get; set; }
                public string fatertumbon { get; set; }
                public string fateraumper { get; set; }
                public string faterprovince { get; set; }
                public string faterpostcode { get; set; }
                public string faterphone1 { get; set; }
                public string faterphone2 { get; set; }
                public string faterphone3 { get; set; }
                public int? fatereducation { get; set; }
                public string faterJob { get; set; }
                public string faterJobTower { get; set; }
                public string faterJobSalary { get; set; }
                public string moterTitle { get; set; }
                public string moterName { get; set; }
                public string moterLastname { get; set; }
                public string moterNameEN { get; set; }
                public string moterLastnameEN { get; set; }
                public string moterBirday { get; set; }
                public string moterReligion { get; set; }
                public string moterRace { get; set; }
                public string moterNation { get; set; }
                public string moterhome { get; set; }
                public string motermuu { get; set; }
                public string motersoy { get; set; }
                public string moterroad { get; set; }
                public string motertumbon { get; set; }
                public string moteraumper { get; set; }
                public string moterprovince { get; set; }
                public string moterpostcode { get; set; }
                public string moterphone1 { get; set; }
                public string moterphone2 { get; set; }
                public string moterphone3 { get; set; }
                public int? motereducation { get; set; }
                public string moterJob { get; set; }
                public string moterJobTower { get; set; }
                public string moterJobSalary { get; set; }
                public decimal? stdWeight { get; set; }
                public decimal? stdHeight { get; set; }
                public string stdBlood { get; set; }
                public string stdSickFood { get; set; }
                public string stdSickDruq { get; set; }
                public string stdSickOther { get; set; }
                public string stdSickNormal { get; set; }
                public string stdSickDanger { get; set; }
                public string JourneyType { get; internal set; }
                public string CardNFC { get; internal set; }
                public string famJobSalaryMonth { get; internal set; }
                public string faterJobSalaryMonth { get; internal set; }
                public string moterJobSalaryMonth { get; internal set; }
            }
        }

        internal class Report6VM
        {
            public string teacher { get; set; }
            public int? year { get; set; }
            public string term { get; set; }
            public string level1 { get; set; }
            public string level2 { get; set; }
            public int male { get; set; }
            public int female { get; set; }
            public List<Report6StudentVM> students { get; set; }

            internal class Report6StudentVM
            {
                public int? no { get; set; }
                public string code { get; set; }
                public string name { get; set; }
                public string id { get; set; }
                public string familyid { get; set; }
                public string family { get; set; }
                public string motherid { get; set; }
                public string mother { get; set; }
                public string fatherid { get; set; }
                public string father { get; set; }
            }
        }

        internal class Report3VM
        {
            public string teacher { get; set; }
            public int? year { get; set; }
            public string term { get; set; }
            public string level1 { get; set; }
            public string level2 { get; set; }
            public int male { get; set; }
            public int female { get; set; }
            public List<Report3StudentVM> students { get; set; }

            internal class Report3StudentVM
            {
                public int? no { get; set; }
                public string code { get; set; }
                public string name { get; set; }
                public string nick { get; set; }
                public string id { get; set; }
                public string date { get; set; }
                public string picture { get; set; }
                public string nameEn { get; internal set; }
                public int? ageYear { get; internal set; }
                public int? ageMonth { get; internal set; }
                public string title { get; internal set; }
                public string fname { get; internal set; }
                public string lname { get; internal set; }
            }
        }

        private class Report15VM
        {
            public string teacher { get; set; }
            public int? year { get; set; }
            public string term { get; set; }
            public string level1 { get; set; }
            public string level2 { get; set; }
            public int male { get; set; }
            public int female { get; set; }

            public List<Report15StudentVM> students { get; set; }

            public class Report15StudentVM
            {
                public int? no { get; internal set; }
                public string code { get; internal set; }
                public string name { get; internal set; }
                public string title { get; internal set; }
                public string fname { get; internal set; }
                public string lname { get; internal set; }
                public string number { get; internal set; }
                public string soy { get; internal set; }
                public string muu { get; internal set; }
                public string road { get; internal set; }
                public string tumbon { get; internal set; }
                public string aumpher { get; internal set; }
                public string province { get; internal set; }
                public string post { get; internal set; }
                public string phone { get; internal set; }
            }
        }

        private class Report16VM
        {
            public object teacher { get; set; }
            public object year { get; set; }
            public object term { get; set; }
            public object level1 { get; set; }
            public object level2 { get; set; }
            public int male { get; set; }
            public int female { get; set; }
            public List<Report16StudentVM> students { get; set; }

            internal class Report16StudentVM
            {
                public object no { get; set; }
                public string code { get; set; }
                public string name { get; set; }
                public string firstDate { get; set; }
                public string birthDay { get; set; }
                public string oldSchool { get; set; }
                public string oldDegree { get; set; }
                public string reason { get; set; }
                public string soy { get; set; }
                public string muu { get; set; }
                public string road { get; set; }
                public string tumbon { get; set; }
                public string aumpher { get; set; }
                public string province { get; set; }
                public string post { get; set; }
                public string phone { get; set; }
                public string tumbonHome { get; set; }
                public string aumpherHome { get; set; }
                public string provinceHome { get; set; }
                public string father { get; set; }
                public string mother { get; set; }
                public string fatherJob { get; set; }
                public string motherJob { get; set; }
                public string number { get; internal set; }
            }
        }
        private class StudentCard
        {
            public int sID { get; set; }
            public string Card { get; set; }
        }
        #endregion

        public class ITextEvents : PdfPageEventHelper
        {
            string summary1, summary2, reportName;
            //bool IsHeaderFirstPage = false;
            TCompany school;
            public ITextEvents(TCompany school, string sum1, string sum2, string report)//, bool isHeaderFirstPage = false)
            {
                this.school = school;
                summary1 = sum1;
                summary2 = sum2;
                reportName = report;
                //IsHeaderFirstPage = isHeaderFirstPage;
            }

            // This is the contentbyte object of the writer  
            PdfContentByte cb;

            // we will put the final number of pages in a template  
            PdfTemplate headerTemplate, footerTemplate;

            // this is the BaseFont we are going to use for the header / footer  
            BaseFont bf = null;

            // This keeps track of the creation time  
            DateTime PrintTime = DateTime.Now;
            string dateNow = "";
            #region Fields  
            private string _header;
            #endregion

            #region Properties  
            public string Header
            {
                get { return _header; }
                set { _header = value; }
            }
            #endregion

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    dateNow = DateTime.Now.ToString("d MMMM yyyy เวลา HH:mm", new CultureInfo("th-TH"));
                    //bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font f = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 14, iTextSharp.text.Font.NORMAL);
                    //bf = BaseFont.CreateFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    bf = f.BaseFont;
                    cb = writer.DirectContent;
                    headerTemplate = cb.CreateTemplate(document.PageSize.Width - 100, 100);
                    footerTemplate = cb.CreateTemplate(document.PageSize.Width - 100, 50);
                }
                catch (DocumentException de)
                {
                }
                catch (System.IO.IOException ioe)
                {
                }
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);

                //Add paging to footer  
                {
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10);

                    cb.SetTextMatrix(document.PageSize.GetLeft(30), document.PageSize.GetBottom(5));
                    var text1 = $"รายงานข้อมูล ณ วันที่ {dateNow}";
                    cb.ShowText(text1);

                    cb.SetTextMatrix(document.PageSize.GetRight(30), document.PageSize.GetBottom(5));
                    var text2 = $"{writer.PageNumber}/";
                    cb.ShowText(text2);
                    cb.EndText();
                    float len = bf.GetWidthPoint(text2, 10);
                    // cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));document.PageSize.GetRight(20)
                    cb.AddTemplate(footerTemplate, document.PageSize.GetRight(30) + len, document.PageSize.GetBottom(5));// document.PageSize.GetBottom(20)
                }

                //if (IsHeaderFirstPage && writer.PageNumber > 1)
                //{
                //    return;
                //}

                {//set header
                    PdfPTable mainTable = new PdfPTable(1);
                    mainTable.TotalWidth = document.PageSize.Width - 30f;

                    mainTable.WidthPercentage = 90;
                    mainTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    PdfPTable table = new PdfPTable(3);
                    //table.TotalWidth = document.PageSize.Width - 20f;
                    //table.WidthPercentage = 99f;                
                    table.WidthPercentage = 100;
                    table.PaddingTop = 30;
                    table.SetTotalWidth(new float[] { 20, 60, 20 });
                    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    if (!string.IsNullOrEmpty(school.sImage))
                    {
                        var jpg = iTextSharp.text.Image.GetInstance(school.sImage);
                        jpg.Alignment = Element.ALIGN_CENTER;
                        jpg.PaddingTop = 10;
                        jpg.ScaleAbsolute(70, 70);
                        PdfPCell cell = new PdfPCell(jpg);
                        cell.Rowspan = 7;
                        cell.Border = 0;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        table.AddCell(cell);
                    }
                    else
                    {
                        table.AddCell(SetCellPDF(
                         text: " ",
                         border: iTextSharp.text.Rectangle.NO_BORDER,
                         rowspan: 7
                         ));
                    }

                    //tableCol = tableCol - 1;
                    //var addColLength = addedCol.Count;

                    table.AddCell(SetCellPDF(
                        text: school.sCompany,
                        horizotal: Element.ALIGN_LEFT,
                        fontSize: 16,
                        border: iTextSharp.text.Rectangle.NO_BORDER
                        ));

                    table.AddCell(SetCellPDF(
                      text: reportName,
                      fontSize: 16,
                      horizotal: Element.ALIGN_CENTER,
                      fontStyle: iTextSharp.text.Font.BOLD,
                       border: iTextSharp.text.Rectangle.NO_BORDER,
                      rowspan: 7
                      ));

                    table.AddCell(SetCellPDF(
                       text: school.sAddress,
                       horizotal: Element.ALIGN_LEFT,
                        border: iTextSharp.text.Rectangle.NO_BORDER
                       ));

                    table.AddCell(SetCellPDF(
                      text: $"โทรศัพท์ : {school.sPhoneOne + (string.IsNullOrEmpty(school.sPhoneTwo) ? "" : " ," + school.sPhoneTwo)}  โทรสาร : {school.sFax}",
                      horizotal: Element.ALIGN_LEFT,
                       border: iTextSharp.text.Rectangle.NO_BORDER
                      ));

                    table.AddCell(SetCellPDF(
                       text: $"{(string.IsNullOrEmpty(school.sWebsite) ? "" : school.sWebsite + " ")}",
                       horizotal: Element.ALIGN_LEFT,
                       border: iTextSharp.text.Rectangle.NO_BORDER
                      ));

                    table.AddCell(SetCellPDF(
                      text: $"อีเมล์ : {school.sEmailOne + (string.IsNullOrEmpty(school.sEmailTwo) ? "" : " ," + school.sEmailTwo)}",
                      horizotal: Element.ALIGN_LEFT,
                      border: iTextSharp.text.Rectangle.NO_BORDER
                     ));

                    table.AddCell(SetCellPDF(
                       text: summary1,
                       horizotal: Element.ALIGN_LEFT,
                       border: iTextSharp.text.Rectangle.NO_BORDER
                      ));

                    table.AddCell(SetCellPDF(
                      text: summary2,
                      horizotal: Element.ALIGN_LEFT,
                      border: iTextSharp.text.Rectangle.NO_BORDER
                     ));

                    table.AddCell(SetCellPDF(
                        text: " ",
                        colspan: 3,
                        horizotal: Element.ALIGN_LEFT,
                        border: iTextSharp.text.Rectangle.NO_BORDER
                    ));

                    mainTable.AddCell(table);

                    mainTable.WriteSelectedRows(0, -1, 10, document.PageSize.Height + 5, writer.DirectContent);
                }
            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                //headerTemplate.BeginText();
                //headerTemplate.SetFontAndSize(bf, 12);
                //headerTemplate.SetTextMatrix(0, 0);
                //headerTemplate.ShowText((writer.PageNumber - 1).ToString());
                //headerTemplate.EndText();

                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 10);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber) + "");
                footerTemplate.EndText();
            }
        }

        public class ITextEvents2 : PdfPageEventHelper
        {
            string summary1, summary2, reportName;
            TCompany school;
            public ITextEvents2(TCompany school, string sum1, string sum2, string report)
            {
                this.school = school;
                summary1 = sum1;
                summary2 = sum2;
                reportName = report;
            }
            // This is the contentbyte object of the writer  
            PdfContentByte cb;

            // we will put the final number of pages in a template  
            PdfTemplate headerTemplate, footerTemplate;

            // this is the BaseFont we are going to use for the header / footer  
            BaseFont bf = null;

            // This keeps track of the creation time  
            DateTime PrintTime = DateTime.Now;
            string dateNow = "";
            #region Fields  
            private string _header;
            #endregion

            #region Properties  
            public string Header
            {
                get { return _header; }
                set { _header = value; }
            }
            #endregion

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    dateNow = DateTime.Now.ToString("d MMMM yyyy เวลา HH:mm", new CultureInfo("th-TH"));
                    //bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font f = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 14, iTextSharp.text.Font.NORMAL);
                    //bf = BaseFont.CreateFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    bf = f.BaseFont;
                    cb = writer.DirectContent;
                    headerTemplate = cb.CreateTemplate(document.PageSize.Width - 100, 50);
                    footerTemplate = cb.CreateTemplate(document.PageSize.Width - 100, 50);
                }
                catch (DocumentException de)
                {
                }
                catch (System.IO.IOException ioe)
                {
                }
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);

                //Add paging to footer  
                {
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10);

                    cb.SetTextMatrix(document.PageSize.GetLeft(30), document.PageSize.GetBottom(5));
                    var text1 = $"รายงานข้อมูล ณ วันที่ {dateNow}";
                    cb.ShowText(text1);

                    cb.SetTextMatrix(document.PageSize.GetRight(30), document.PageSize.GetBottom(5));
                    var text2 = $"{writer.PageNumber}/";
                    cb.ShowText(text2);
                    cb.EndText();
                    float len = bf.GetWidthPoint(text2, 10);
                    // cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));document.PageSize.GetRight(20)
                    cb.AddTemplate(footerTemplate, document.PageSize.GetRight(30) + len, document.PageSize.GetBottom(5));// document.PageSize.GetBottom(20)
                }

                {
                    PdfPTable mainTable = new PdfPTable(1);
                    mainTable.TotalWidth = document.PageSize.Width - 30f;

                    mainTable.WidthPercentage = 90;
                    mainTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    PdfPTable table = new PdfPTable(3);
                    //table.TotalWidth = document.PageSize.Width - 20f;
                    //table.WidthPercentage = 99f;                
                    table.WidthPercentage = 100;
                    table.PaddingTop = 30;
                    table.SetTotalWidth(new float[] { 10, 70, 20 });
                    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    if (!string.IsNullOrEmpty(school.sImage))
                    {
                        var jpg = iTextSharp.text.Image.GetInstance(school.sImage);
                        jpg.Alignment = Element.ALIGN_CENTER;
                        jpg.PaddingTop = 10;
                        jpg.ScaleAbsolute(40, 40);
                        PdfPCell cell = new PdfPCell(jpg);
                        cell.Rowspan = 3;
                        cell.Border = 0;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        table.AddCell(cell);
                    }
                    else
                    {
                        table.AddCell(SetCellPDF(
                         text: " ",
                         border: iTextSharp.text.Rectangle.NO_BORDER,
                         rowspan: 3
                         ));
                    }

                    //tableCol = tableCol - 1;
                    //var addColLength = addedCol.Count;

                    table.AddCell(SetCellPDF(
                        text: school.sCompany,
                        horizotal: Element.ALIGN_LEFT,
                        fontSize: 12,
                        border: iTextSharp.text.Rectangle.NO_BORDER
                        ));

                    table.AddCell(SetCellPDF(
                      text: reportName,
                      fontSize: 14,
                      horizotal: Element.ALIGN_CENTER,
                      fontStyle: iTextSharp.text.Font.BOLD,
                       border: iTextSharp.text.Rectangle.NO_BORDER,
                      rowspan: 3
                      ));

                    //table.AddCell(SetCellPDF(
                    //   text: school.sAddress,
                    //   horizotal: Element.ALIGN_LEFT,
                    //    border: iTextSharp.text.Rectangle.NO_BORDER
                    //   ));

                    //table.AddCell(SetCellPDF(
                    //  text: $"โทรศัพท์ : {school.sPhoneOne + (string.IsNullOrEmpty(school.sPhoneTwo) ? "" : " ," + school.sPhoneTwo)}  โทรสาร : {school.sFax}",
                    //  horizotal: Element.ALIGN_LEFT,
                    //   border: iTextSharp.text.Rectangle.NO_BORDER
                    //  ));

                    //table.AddCell(SetCellPDF(
                    //   text: $"{(string.IsNullOrEmpty(school.sWebsite) ? "" : school.sWebsite + " ")}",
                    //   horizotal: Element.ALIGN_LEFT,
                    //   border: iTextSharp.text.Rectangle.NO_BORDER
                    //  ));

                    //table.AddCell(SetCellPDF(
                    //  text: $"อีเมล์ : {school.sEmailOne + (string.IsNullOrEmpty(school.sEmailTwo) ? "" : " ," + school.sEmailTwo)}",
                    //  horizotal: Element.ALIGN_LEFT,
                    //  border: iTextSharp.text.Rectangle.NO_BORDER
                    // ));

                    table.AddCell(SetCellPDF(
                       text: summary1 + " " + summary2,
                       fontSize: 12,
                       horizotal: Element.ALIGN_LEFT,
                       border: iTextSharp.text.Rectangle.NO_BORDER
                      ));

                    table.AddCell(SetCellPDF(
                        text: " ",
                        fontSize: 12,
                        colspan: 3,
                        horizotal: Element.ALIGN_LEFT,
                        border: iTextSharp.text.Rectangle.NO_BORDER
                    ));

                    mainTable.AddCell(table);

                    mainTable.WriteSelectedRows(0, -1, 10, document.PageSize.Height + 5, writer.DirectContent);
                }
            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                //headerTemplate.BeginText();
                //headerTemplate.SetFontAndSize(bf, 12);
                //headerTemplate.SetTextMatrix(0, 0);
                //headerTemplate.ShowText((writer.PageNumber - 1).ToString());
                //headerTemplate.EndText();

                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 10);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber) + "");
                footerTemplate.EndText();
            }
        }


    }
}