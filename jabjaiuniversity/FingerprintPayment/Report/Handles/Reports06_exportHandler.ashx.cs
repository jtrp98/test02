using FingerprintPayment.Report.Models;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using FingerprintPayment.Report.Functions.Reports_06;
using OfficeOpenXml.Style;
using System.Globalization;
using JabjaiMainClass;

namespace FingerprintPayment.Report.Handles
{
    /// <summary>
    /// Summary description for Reports03_exportHandler
    /// </summary>
    public class Reports06_exportHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }

            List<ReportsData_06Models> models = new List<ReportsData_06Models>();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";// context.Session["sEntities"].ToString();
                string entities = context.Session["sEntities"].ToString();
                var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(f_company,ConnectionDB.Read)))
                {
                    var jsonString = new StreamReader(context.Request.InputStream).ReadToEnd();
                    Search search = JsonConvert.DeserializeObject<Search>(jsonString);
                    DateTime dStart = DateTime.Today, dEnd = DateTime.Today;
                    if (search.sort_type == 0)
                    {
                        search.dEnd = search.dStart.Value;
                    }
                    else if (search.sort_type == 1)
                    {
                        search.dEnd = search.dStart.Value.AddMonths(1).AddDays(-1);
                        if (search.dEnd >= DateTime.Now) search.dEnd = DateTime.Now;
                    }
                    else if (search.sort_type == 2)
                    {
                        //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                        search.dStart = f_term.dStart.Value;
                        search.dEnd = f_term.dEnd.Value;
                        if (search.dEnd >= DateTime.Now) search.dEnd = DateTime.Now;
                    }


                    Export_excel(ReportsType_01.GetReports(dbschool, search, userData), search);
                }
            }
        }

        private void Export_excel(List<ReportsData_06Models> models, Search search)
        {
            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("รายงานการไม่เช็คชื่อ");

                var worksheet = excel.Workbook.Worksheets["รายงานการไม่เช็คชื่อ"];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                
                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                string ColspanHeader = search.report_type == 0 ? "A{0}:E{0}" : "A{0}:F{0}";

                string textHeader_reports = "";
                if (search.report_type == 0) textHeader_reports = "รายงานการไม่เช็คชื่อประจำวันที่ " + search.dStart.Value.ToString("dd MMMM yyyy", new CultureInfo("th-th"));
                else textHeader_reports = "รายงานการไม่เช็คชื่อประจำวันที่ " + search.dStart.Value.ToString("dd MMMM yyyy", new CultureInfo("th-th")) + " ถึง " + search.dEnd.Value.ToString("dd MMMM yyyy", new CultureInfo("th-th"));

                SetHeader(worksheet, string.Format(ColspanHeader, 1), true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, string.Format(ColspanHeader, 2), true, textHeader_reports, 14, ExcelHorizontalAlignment.Center);

                SetHeader(worksheet, string.Format(ColspanHeader, 3), true, "พิมพ์วันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")) + " เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Right);

                string[] strHeader = { "ลำดับ", "ชั้นเรียน", "ครูประจำชั้น", "ครูที่ปรึกษา 1", "ครูที่ปรึกษา 2", "จำนวนครั้งที่ไม่ได้เช็คชื่อ" };
                int Columuns = 1;
                foreach (string str in strHeader)
                {
                    if (search.sort_type == 0 && str == "จำนวนครั้งที่ไม่ได้เช็คชื่อ") continue;
                    SetTableHeader(worksheet.Cells[4, Columuns++], false, str, ExcelHorizontalAlignment.Center);
                }

                int rowsStart = 5;
                //int Status_0 = 0, Status_1 = 0, Status_2 = 0, Status_3 = 0;
                //int DayLength = report.headerReports.Sum(s => s.weeks.Sum(c1 => c1.days.Count()));
                List<string> arraryString = new List<string>() { "3", "1", "11", "10" };
                foreach (var reportsData in models)
                {
                    SetTableRows(worksheet.Cells[rowsStart, 1], true, (rowsStart - 4) + "", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsStart, 2], true, reportsData.roomName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsStart, 3], true, reportsData.teacherHead, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsStart, 4], true, reportsData.teacherAssistOne, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsStart, 5], true, reportsData.teacherAssistTwo, ExcelHorizontalAlignment.Center);
                    if (search.sort_type != 0) SetTableRows(worksheet.Cells[rowsStart, 6], true, reportsData.dayCount.ToString(), ExcelHorizontalAlignment.Center);
                    rowsStart++;
                }

                worksheet.Cells.AutoFitColumns();
                worksheet.Column(1).Width = 10;
                worksheet.Column(2).Width = 15;
                worksheet.Column(3).Width = 28;
                worksheet.Column(4).Width = 28;
                worksheet.Column(5).Width = 28;
                worksheet.Column(6).Width = 28;
                worksheet.Column(7).Width = 10;
                worksheet.Column(8).Width = 10;

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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}