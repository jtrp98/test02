using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using JabjaiMainClass;
using FingerprintPayment.Report.Models;
using FingerprintPayment.Report.Functions.BehaviorsReports;


namespace FingerprintPayment.Report.Handles
{
    /// <summary>
    /// Summary description for Behaviorsdetailreports2_exportHandler
    /// </summary>
    public class Behaviorsdetailreports2_exportHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken.userData();
                if (token.CheckToken(HttpContext.Current))
                {
                    userData = token.getTokenValues(HttpContext.Current);
                }

                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = context.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    var jsonString = new StreamReader(context.Request.InputStream).ReadToEnd();
                    Search search = JsonConvert.DeserializeObject<Search>(jsonString);
                    //DateTime dStart = DateTime.Today, dEnd = DateTime.Today;

                    var setting = dbschool.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault();

                    if (setting.Type == 1)
                    {
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.nTerm.Trim() == search.term_id.Trim());
                        search.dStart = f_term.dStart.Value;
                        search.dEnd = f_term.dEnd.Value;
                    }
                    else
                    {
                        search.dStart = dbschool.TTerms.Where(w => w.nYear == search.year_Id && w.cDel == null && userData.CompanyID == w.SchoolID).Min(min => min.dStart) ?? DateTime.Today;
                        search.dEnd = dbschool.TTerms.Where(w => w.nYear == search.year_Id && w.cDel == null && userData.CompanyID == w.SchoolID).Max(max => max.dEnd) ?? DateTime.Today;
                    }

                    //if (!string.IsNullOrEmpty(search.term_id))
                    //{
                    //    //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                    //    var f_term = dbschool.TTerms.FirstOrDefault(f => f.SchoolID == tCompany.nCompany && f.nTerm.Trim() == search.term_id.Trim());
                    //    search.dStart = f_term.dStart.Value;
                    //    search.dEnd = f_term.dEnd.Value;
                    //}
                    //else
                    //{
                    //    search.dStart = dbschool.TTerms.Where(w => w.nYear == search.year_Id && w.cDel == null && w.SchoolID == userData.CompanyID).Min(min => min.dStart) ?? DateTime.Today;
                    //    search.dEnd = dbschool.TTerms.Where(w => w.nYear == search.year_Id && w.cDel == null && w.SchoolID == userData.CompanyID).Max(max => max.dEnd) ?? DateTime.Today;
                    //}

                    Export_excel(reportsDetailType_01.GetReports(dbschool, search), search, dbschool);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void Export_excel(Models.BehaviorsReports.DetailModels models, Search search, JabJaiEntities jabJaiEntities)
        {
            using(JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (ExcelPackage excel = new ExcelPackage())
            {
                string Worksheets = "รายงานคะแนนพฤติกรรม";
                excel.Workbook.Worksheets.Add(Worksheets);

                var worksheet = excel.Workbook.Worksheets[Worksheets];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
      
                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                string ColspanHeader = "A{0}:I{0}";
                string textHeader_reports = "รายงานคะแนนพฤติกรรม";
                int rowsStart = 1;

                SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, textHeader_reports, 14, ExcelHorizontalAlignment.Center);

                var f_year = jabJaiEntities.TYears.FirstOrDefault(f => f.SchoolID == tCompany.nCompany && f.nYear == search.year_Id);
                var setting = jabJaiEntities.TBehaviorSettings.Where(w=>w.SchoolID == tCompany.nCompany).FirstOrDefault();
                SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, "ปีการศึกษา : " + f_year.numberYear, null, ExcelHorizontalAlignment.Right);
                if (setting.Type == 1)
                {
                    var f_term = jabJaiEntities.TTerms.FirstOrDefault(f => f.SchoolID == tCompany.nCompany && f.nTerm.Trim() == search.term_id);
                    SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, "เทอม : " + f_term.sTerm, null, ExcelHorizontalAlignment.Right);
                }

                SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, "รหัสนักเรียน  : " + models.student_Code, null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, "ชื่อ-นามสุกล : " + models.student_name, null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, "ชั้นเรียน  : " + models.roomName, null, ExcelHorizontalAlignment.Right);

                SetHeader(worksheet, string.Format(ColspanHeader, rowsStart++), true, "พิมพ์วันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")) + " เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Right);

                string[] strHeader = { "ลำดับ", "วันที่", "เวลา", "ชื่อคะแนนพฤติกรรม", "ประเภท", "จำนวนคะแนน", "คะแนนคงเหลือ", "ผู้บันทึก", "หมายเหตุ" };
                int Columuns = 1;
                foreach (string str in strHeader)
                {
                    SetTableHeader(worksheet.Cells[rowsStart, Columuns++], false, str, ExcelHorizontalAlignment.Center);
                }

                rowsStart++;
                //int Status_0 = 0, Status_1 = 0, Status_2 = 0, Status_3 = 0;
                //int DayLength = report.headerReports.Sum(s => s.weeks.Sum(c1 => c1.days.Count()));
                List<string> arraryString = new List<string>() { "3", "1", "11", "10" };
                int dataIndex = 1;
                models.Details = models.Details.OrderByDescending(o => o.ID).ToList();
                foreach (var reportsData in models.Details.Where(w => w.Status != "delete"))
                {
                    SetTableRows(worksheet.Cells[rowsStart, 1], true, (dataIndex++) + "", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsStart, 2], true, reportsData.dateTime.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsStart, 3], true, reportsData.dateTime.Value.ToString("HHH:mm:ss"), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsStart, 4], true, reportsData.Name, ExcelHorizontalAlignment.Left);
                    SetTableRows(worksheet.Cells[rowsStart, 5], true, reportsData.Type, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsStart, 6], true, reportsData.Score, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsStart, 7], true, reportsData.residualScore.ToString(), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[rowsStart, 8], true, reportsData.teacherName, ExcelHorizontalAlignment.Left);
                    SetTableRows(worksheet.Cells[rowsStart, 9], true, reportsData.Note, ExcelHorizontalAlignment.Center);
                    rowsStart++;
                }

                worksheet.Cells.AutoFitColumns();
                worksheet.Column(1).Width = 10;
                worksheet.Column(2).Width = 11;
                worksheet.Column(3).Width = 11;
                worksheet.Column(4).Width = 28;
                worksheet.Column(5).Width = 10;
                worksheet.Column(6).Width = 15;
                worksheet.Column(7).Width = 15;
                worksheet.Column(8).Width = 24;
                worksheet.Column(9).Width = 30;

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

    }
}