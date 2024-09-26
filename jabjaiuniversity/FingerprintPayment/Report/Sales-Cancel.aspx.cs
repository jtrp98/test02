using Amazon.XRay.Recorder.Core;
using FingerprintPayment.Class;
using FingerprintPayment.Memory;
using FluentDateTime;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Utilities;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity.Injection;
using urbanairship;

namespace FingerprintPayment.Report
{
    public partial class Sales_Cancel : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                var userData = GetUserData();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)), userData);
                    hdfschoolname.Value = tCompany.sCompany;
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_data(Search search)
        {
            var userData = GetUserData();

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
            List<day_data> day_data = new List<day_data>();

            string header_text = "", JsonDATE = "";
            if (search.sort_type == 2)
            {

                header_text = "ยอดขาย ปี " + search.dStart.Value.ToString("yyyy ", new CultureInfo("th-th"));
                search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("01/01/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));
                search.dEnd = search.dStart.Value.NextYear().AddDays(-1.0);

                for (int i = 0; search.dStart.Value.AddMonths(i) <= search.dEnd.Value; i++)
                {
                    JsonDATE += (string.IsNullOrEmpty(JsonDATE) ? "" : ",") + "{  \"SchoolID\" : " + userData.CompanyID + ", \"TimeStart\": \"" + search.dStart.Value.AddMonths(i).ToString("MM/01/yyyy") + "\" , \"TimeEnd\": \"" + search.dStart.Value.AddMonths(i + 1).ToString("MM/01/yyyy") + "\" }" + Environment.NewLine;
                    day_data.Add(new day_data
                    {
                        lable = search.dStart.Value.AddMonths(i).ToString(),
                        values = search.dStart.Value.AddMonths(i),
                    });
                }

                var q = (from a in day_data
                         join b in GetReport_Group(search, userData.CompanyID, db, "01/MM/yyyy", JsonDATE) on a.values.Month equals b.dSell.Month into jab

                         from jb in jab.DefaultIfEmpty()
                         select new views01
                         {
                             dSell = a.values,
                             lable = a.values.ToString("MMM-yy", new CultureInfo("th-th")),
                             cost = jb == null ? 0 : jb.cost,
                             price = jb == null ? 0 : jb.price,
                         }).ToList();

                return new header_reports { header_text = header_text, data = q, report_type = search.sort_type };
            }
            else
            {
                if (search.sort_type == 0)
                {
                    search.dEnd = search.dStart.Value.Next(DayOfWeek.Sunday);
                    search.dStart = search.dEnd.Value.Previous(DayOfWeek.Monday);

                    header_text = "ยอดขาย วันที่ " + search.dStart.Value.ToString("dd MMM ", new CultureInfo("th-th")) + " - " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));
                }
                else if (search.sort_type == 1)
                {
                    search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("01/MM/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));
                    search.dEnd = search.dStart.Value.NextMonth().AddDays(-1.0);
                    header_text = "ยอดขาย เดือน " + search.dStart.Value.ToString("MMMM yyyy ", new CultureInfo("th-th"));
                }
                else
                {
                    header_text = "ยอดขาย วันที่ " + search.dStart.Value.ToString("dd MMM ", new CultureInfo("th-th")) + " - " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));
                    if (search.dStart == null && search.dEnd == null)
                    {
                        search.dStart = DateTime.Today;
                        search.dEnd = search.dStart.Value.NextDay();
                    }
                }

                for (double i = 0; search.dStart.Value.AddDays(i) <= search.dEnd.Value; i++)
                {
                    JsonDATE += (string.IsNullOrEmpty(JsonDATE) ? "" : ",") + "{  \"SchoolID\" : " + userData.CompanyID + ", \"TimeStart\": \"" + search.dStart.Value.AddDays(i).ToString("MM/dd/yyyy") + "\" , \"TimeEnd\": \"" + search.dStart.Value.AddDays(i + 1).ToString("MM/dd/yyyy") + "\" }" + Environment.NewLine;
                    day_data.Add(new day_data
                    {
                        lable = search.dStart.Value.AddDays(i).ToString("dd/MM/yyyy"),
                        values = search.dStart.Value.AddDays(i),
                    });
                }

                search.dEnd = search.dEnd.Value.AddDays(1.0);

                string json = fcommon.EntityToJson(day_data);

                var q1 = GetReport_Group(search, userData.CompanyID, db, "dd/MM/yyyy", JsonDATE);
                var q = (from a in day_data
                         join b in q1 on a.lable.Trim() equals b.values.Trim() into jab

                         from jb in jab.DefaultIfEmpty()

                         select new views01
                         {
                             dSell = a.values,
                             lable = a.values.ToString(search.sort_type == 0 ? "dddd" : "dd MMM yy", new CultureInfo("th-th")),
                             cost = jb == null ? 0 : jb.cost,
                             price = jb == null ? 0 : jb.price,
                         }).ToList();


                return new header_reports { header_text = header_text, data = q, report_type = search.sort_type };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_detail(Search search)
        {
            var userData = GetUserData();

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
            List<day_data> day_data = new List<day_data>();

            search.dEnd = search.dStart.Value.AddDays(1.0);

            var q = GetReport_Details(search, userData.CompanyID, db);

            var q1 = (from a in q
                      group a by new
                      {
                          a.dSell,
                          a.sell_Id,
                          a.emp_name,
                          a.user_name,
                          a.id,
                          a.userType,
                          a.shop_name,
                          a.nTotal,
                          a.nTSubLevel2,
                          a.SubLevel,
                          a.sStudentID,
                          a.UserCancel,
                          
                      } into gb
                      select new report_detail
                      {
                          dSell = gb.Key.dSell,
                          emp_name = gb.Key.UserCancel == 99999 ? "System Adjusted" : gb.Key.emp_name,                          
                          user_name = gb.Key.user_name,
                          sell_Id = gb.Key.sell_Id,
                          id = gb.Key.id,
                          userType = gb.Key.userType,
                          nTSubLevel2 = gb.Key.nTSubLevel2,
                          SubLevel = gb.Key.SubLevel,
                          sStudentID = gb.Key.sStudentID ?? "",
                          products = GetProducts((from S in gb
                                                  orderby S.product_name
                                                  select new sellDetail
                                                  {
                                                      amount = S.amount,
                                                      price = S.price,
                                                      cost = S.cost,
                                                      barcode = S.barcode,
                                                      product_name = S.product_name,
                                                      product_type = S.product_type,
                                                      dayCancal = S.dayCancal
                                                  }).ToList(), gb.Key.nTotal),
                          shop_name = gb.Key.shop_name
                      }).ToList();

            List<int> Hour = new List<int>();
            for (int i = 0; i < 24; i++) Hour.Add(i);


            var q2 = (from a in Hour
                      join b in (from a1 in q
                                 group a1 by a1.dSell.Value.Hour into gb
                                 select new
                                 {
                                     hour = gb.Key,
                                     values = gb.Sum(s => s.nTotal)
                                 }) on a equals b.hour into jab

                      from jb in jab.DefaultIfEmpty()

                      select new views02_chart
                      {
                          lable = a.ToString(),
                          values = jb == null ? 0 : jb.values
                      }).ToList();

            string header_text = "รายงานยกเลิกการขายประจำวันที่ " + search.dStart.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));
            return new views02 { charts = q2, report_Details = q1, header_text = header_text };
        }

        private static List<product> GetProducts(List<sellDetail> details, decimal? _Total)
        {
            List<product> products = new List<product>();
            if (details.Count(c => !string.IsNullOrEmpty(c.product_name)) == 0)
            {
                products.Add(new product
                {
                    amount = 1,
                    price = _Total,
                    barcode = "-",
                    cost = 0,
                    product_name = "-",
                    product_type = "-",
                });
            }
            else
            {
                products = (from a2 in details
                            select new product
                            {
                                amount = a2.amount,
                                barcode = a2.barcode,
                                cost = a2.cost,
                                price = a2.price,
                                product_name = a2.product_name,
                                product_type = a2.product_type,
                                dayCancal = a2.dayCancal
                            }).ToList();
            }
            return products;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void export_data(Search search)
        {

            var userData = GetUserData();
            try
            {
                views02 report = reports_detail(search) as views02;

                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานยกเลิกการขาย");

                    var worksheet = excel.Workbook.Worksheets["รายงานยกเลิกการขาย"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    
                    var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                    SetHeader(worksheet, "A1:K1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, "A2:K2", true, report.header_text, 14, ExcelHorizontalAlignment.Center);

                    SetHeader(worksheet, "A3:J3", true, "พิมพ์วันที่ :", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, "K3:K3", true, DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Left);

                    SetHeader(worksheet, "A4:J4", true, "เวลา :", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, "K4:K4", true, DateTime.Now.ToString("HH:mm:ss"), null, ExcelHorizontalAlignment.Left);

                    SetHeader(worksheet, "A5:K5", true, "", null, ExcelHorizontalAlignment.Center);

                    string[] strHeader = { "ลำดับ", "วันที่/เวลา", "ผู้ทำรายการ", "ชั้น", "รหัสนักเรียน", "ชื่อ-สกุล", "ชื่อร้าน	", "ชื่อสินค้า	", "จำนวน(หน่วย)", "ราคา(บาท)", "วันที่/เวลา ธุรกรรม" };
                    int Columuns = 1;
                    foreach (string str in strHeader)
                    {
                        if (search.shop_id.HasValue && str == "ร้านค้า")
                            SetTableHeader(worksheet.Cells[6, Columuns++], false, "ชื่อผู้ขาย", ExcelHorizontalAlignment.Center);
                        else
                            SetTableHeader(worksheet.Cells[6, Columuns++], false, str, ExcelHorizontalAlignment.Center);
                    }

                    int Rows = 7, Index = 1;
                    Decimal Cost = 0, Price = 0, Total = 0, Lucre = 0;
                    foreach (var dataSell in report.report_Details)
                    {
                        if (dataSell.products.Count() == 0) continue;
                        int rowsEnd = Rows + (dataSell.products.Count() - 1);
                        SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], true, Index + "", ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 2, rowsEnd, 2], true, dataSell.time, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 3, rowsEnd, 3], true, search.shop_id.HasValue ? dataSell.emp_name : dataSell.shop_name, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 4, rowsEnd, 4], true, dataSell.SubLevel + " / " + dataSell.nTSubLevel2, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 5, rowsEnd, 5], true, dataSell.sStudentID, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 6, rowsEnd, 6], true, dataSell.user_name, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 7, rowsEnd, 7], true, dataSell.shop_name, ExcelHorizontalAlignment.Center);
                        foreach (var productData in dataSell.products)
                        {
                            SetTableRows(worksheet.Cells[Rows, 8], false, productData.product_name, ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 9], false, productData.amount + "", ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 10], false, (productData.price + "") + " บาท", ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 11], false, productData.timeCancal, ExcelHorizontalAlignment.Center);
                            Rows += 1;
                        }
                        Index++;

                        Cost += dataSell.products.Sum(s => s.sum_cost) ?? 0;
                        Price += dataSell.products.Sum(s => s.sum_price) ?? 0;
                        Total += dataSell.products.Sum(s => s.total) ?? 0;
                        Lucre += dataSell.products.Sum(s => s.sum_lucre) ?? 0;
                    }

                    //SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "ยอดรวม", ExcelHorizontalAlignment.Right);
                    //SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 11], true, string.Format("{0:#,##0.00} บาท", Total), ExcelHorizontalAlignment.Center);

                    //SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "ต้นทุน", ExcelHorizontalAlignment.Right);
                    //SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 11], true, string.Format("{0:#,##0.00} บาท", Cost), ExcelHorizontalAlignment.Center);

                    //SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "กำไร", ExcelHorizontalAlignment.Right);
                    //SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 11], true, string.Format("{0:#,##0.00} บาท", Lucre), ExcelHorizontalAlignment.Center);

                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(1).Width = 7;
                    worksheet.Column(2).Width = 10;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 24;
                    worksheet.Column(6).Width = 24;
                    worksheet.Column(7).Width = 24;
                    worksheet.Column(9).Width = 15;
                    worksheet.Column(9).Width = 15;
                    worksheet.Column(11).Width = 15;
                    worksheet.Column(10).Width = 15;

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                    HttpContext.Current.Response.ContentType = "application/text";
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
                }
            }
            catch (Exception e)
            {
                AWSXRay xray = new AWSXRay();
                AWSXRayRecorder recorder = xray.Register();
                recorder.AddAnnotation("Method", "Sale Reports Export");
                recorder.AddException(e);
                recorder.EndSegment();
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void export02_data(Search search)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }
            var report = reports_data(search) as header_reports;

            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("รายงานการซื้อ-ขายสินค้า");

                var worksheet = excel.Workbook.Worksheets["รายงานการซื้อ-ขายสินค้า"];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
             
                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                SetHeader(worksheet, "A1:E1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A2:E2", true, report.header_text, 14, ExcelHorizontalAlignment.Center);

                SetHeader(worksheet, "A3:D3", true, "พิมพ์วันที่ :", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, "E3:E3", true, DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Left);

                SetHeader(worksheet, "A4:D4", true, "เวลา :", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, "E4:E4", true, DateTime.Now.ToString("HH:mm:ss"), null, ExcelHorizontalAlignment.Left);

                SetHeader(worksheet, "A5:E5", true, "", null, ExcelHorizontalAlignment.Center);

                string[] strHeader = { "ลำดับ", " วันที่", "ราคาต้นทุน", " ราคาขาย", "กำไร" };
                int Columuns = 1;
                foreach (string str in strHeader)
                {
                    SetTableHeader(worksheet.Cells[6, Columuns++], false, str, ExcelHorizontalAlignment.Center);
                }

                int Rows = 7, Index = 1;
                var reportData = report.data as List<views01>;
                foreach (var dataSell in reportData)
                {
                    SetTableRows(worksheet.Cells[Rows, 1], false, string.Format("{0}", Index++), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 2], false, dataSell.values, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 3], false, string.Format("{0:#,##0.00} บาท", dataSell.cost), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 4], false, string.Format("{0:#,##0.00} บาท", dataSell.price), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 5], false, string.Format("{0:#,##0.00} บาท", dataSell.lucre), ExcelHorizontalAlignment.Center);
                    Rows++;
                }

                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 4], true, "ยอดรวม", ExcelHorizontalAlignment.Right);
                SetTableFooter(worksheet.Cells[Rows++, 5], false, string.Format("{0:#,##0.00} บาท", reportData.Sum(s => s.price)), ExcelHorizontalAlignment.Center);

                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 4], true, "ต้นทุน", ExcelHorizontalAlignment.Right);
                SetTableFooter(worksheet.Cells[Rows++, 5], false, string.Format("{0:#,##0.00} บาท", reportData.Sum(s => s.cost)), ExcelHorizontalAlignment.Center);

                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 4], true, "กำไร", ExcelHorizontalAlignment.Right);
                SetTableFooter(worksheet.Cells[Rows++, 5], false, string.Format("{0:#,##0.00} บาท", reportData.Sum(s => s.lucre)), ExcelHorizontalAlignment.Center);

                worksheet.Cells.AutoFitColumns();
                worksheet.Column(1).Width = 7;
                worksheet.Row(6).Height = 25;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 20;
                worksheet.Column(5).Width = 20;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/text;";
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
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
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

        private static void SetTableFooter(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
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
                rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
            }
        }

        private static List<report_detail> GetReport_Details(Search search, int SchoolID, JabJaiEntities db)
        {
            List<report_detail> q = new List<report_detail>();
            string SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};

SELECT A.dSell,A.sSellID AS 'sell_Id',A.id,A.nTotal,A.dayCancal,ISNULL(U0.SubLevel,'') AS SubLevel,ISNULL(U0.nTSubLevel2,'') AS nTSubLevel2,
CASE WHEN E1.sEmp IS NULL THEN  '' ELSE E1.sName + ' ' + E1.sLastname END AS 'emp_name',
CASE WHEN U0.sID IS NOT NULL THEN U0.sName + ' ' + U0.sLastname 
WHEN U1.sEmp IS NOT NULL THEN U1.sName + ' ' + U1.sLastname 
WHEN U2.CardHistoryID IS NOT NULL THEN U2.UserName + ' (บัตรสำรอง)'
ELSE '' END AS 'user_name',ISNULL(B.nNumber,1) AS 'amount',ISNULL(C.sBarCode,'') AS 'barcode',ISNULL(S.shop_name,'') AS shop_name,
ISNULL(C.sProduct,'') AS 'product_name',ISNULL(D.sType,'') AS 'product_type',ISNULL(B.nCost,C.nCost) AS 'cost',B.nPrice AS 'price' ,
ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1) AS 'sum_cost',B.nPrice * ISNULL(B.nNumber,1) AS 'sum_price' ,A.UserCancel
FROM TSell AS A
LEFT OUTER JOIN (SELECT * FROM TSell_Detail WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID
LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
LEFT OUTER JOIN TShop AS S ON A.shop_id = S.shop_id AND A.SchoolID = S.SchoolID
LEFT OUTER JOIN TEmployees AS E1 ON A.UserCancel = E1.sEmp AND A.SchoolID = E1.SchoolID
LEFT OUTER JOIN TB_StudentViews AS U0 ON A.sID = U0.sID AND A.SchoolID = U0.SchoolID AND A.dSell BETWEEN U0.dStart AND U0.dEnd
LEFT OUTER JOIN TEmployees AS U1 ON A.sID2 = U1.sEmp AND A.SchoolID = U1.SchoolID
LEFT OUTER JOIN TBackupCardHistory AS U2 ON A.CardID = U2.CardHistoryID AND A.SchoolID = U2.SchoolID
WHERE A.dayCancal IS NOT NULL AND A.SchoolID = @SchoolID AND A.dayCancal BETWEEN @DAYSTART AND @DAYEND  
AND (U0.sID = @sID OR @sID = 0) AND (S.shop_id = @shop_id OR @shop_id = 0) AND (A.sEmp = @emp_id OR @emp_id = 0) 

ORDER BY A.dSell"
     , SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0);

            q.AddRange(db.Database.SqlQuery<report_detail>(SQL).ToList());

            if (q.Count() == 0)
            {
                SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};

SELECT A.dSell,A.sSellID AS 'sell_Id',A.id,A.nTotal,A.dayCancal,ISNULL(U0.SubLevel,'') AS SubLevel,ISNULL(U0.nTSubLevel2,'') AS nTSubLevel2,
CASE WHEN E1.sEmp IS NULL THEN  '' ELSE E1.sName + ' ' + E1.sLastname END AS 'emp_name',
CASE WHEN U0.sID IS NOT NULL THEN U0.sName + ' ' + U0.sLastname 
WHEN U1.sEmp IS NOT NULL THEN U1.sName + ' ' + U1.sLastname 
ELSE '' END AS 'user_name',ISNULL(B.nNumber,1) AS 'amount',ISNULL(C.sBarCode,'') AS 'barcode',ISNULL(S.shop_name,'') AS shop_name,
ISNULL(C.sProduct,'') AS 'product_name',ISNULL(D.sType,'') AS 'product_type',ISNULL(B.nCost,C.nCost) AS 'cost',B.nPrice AS 'price', 
ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1) AS 'sum_cost',B.nPrice * ISNULL(B.nNumber,1) AS 'sum_price' ,A.UserCancel
FROM [JabjaiSchoolHistory].[dbo].[TSell]  AS A 
LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolHistory].[dbo].[TSell_Detail] WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID
LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
LEFT OUTER JOIN TShop AS S ON A.shop_id = S.shop_id AND A.SchoolID = S.SchoolID
LEFT OUTER JOIN TEmployees AS E1 ON A.sEmp = E1.sEmp AND A.SchoolID = E1.SchoolID
LEFT OUTER JOIN TB_StudentViews AS U0 ON A.sID = U0.sID AND A.SchoolID = U0.SchoolID AND A.dSell BETWEEN U0.dStart AND U0.dEnd
LEFT OUTER JOIN TEmployees AS U1 ON A.sID2 = U1.sEmp AND A.SchoolID = U1.SchoolID
WHERE A.dayCancal IS NOT NULL AND A.SchoolID = @SchoolID AND A.dayCancal BETWEEN @DAYSTART AND @DAYEND
AND (U0.sID = @sID OR @sID = 0) AND (S.shop_id = @shop_id OR @shop_id = 0) AND (A.sEmp = @emp_id OR @emp_id = 0)  

ORDER BY A.dSell"
, SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0);
                q.AddRange(db.Database.SqlQuery<report_detail>(SQL).ToList());
            }

            return q;

        }

        private static List<views01> GetReport_Group(Search search, int SchoolID, JabJaiEntities db, string FORMAT, string JsonDATE)
        {
            string SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @sort_type INT = {6};
DECLARE @FORMAT VARCHAR(20) = '{7}';

SELECT A0.TimeStart AS 'values',CONVERT(DATETIME,FORMAT(A0.TimeStart, @FORMAT),103) AS dSell,
SUM(ISNULL(ISNULL(A0.nPrice, A0.nTotal), 0) * ISNULL(A0.nNumber, 1)) + SUM(ISNULL(ISNULL(A1.nPrice, A1.nTotal), 0) * ISNULL(A1.nNumber, 1))  AS 'price',
SUM(ISNULL(ISNULL(A0.nCost, A0.nCost), 0) * ISNULL(A0.nNumber, 1)) + SUM(ISNULL(ISNULL(A1.nCost, A1.nCost), 0) * ISNULL(A1.nNumber, 1)) AS 'cost',
@sort_type AS 'sort_type',A0.TimeStart
FROM(
    SELECT TT1.SchoolID, A.nTotal, A.dSell, B.nPrice, B.nNumber, B.nCost, TT1.TimeStart,A.dayCancal
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('[{8}]')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN[JabjaiSchoolSingleDB].[dbo].[TSell] AS A ON TT1.SchoolID = A.SchoolID AND A.dSell BETWEEN @DAYSTART AND @DAYEND AND  A.dSell BETWEEN TT1.TimeStart AND TT1.TimeEnd
	AND (A.sID = @sID OR @sID = 0) AND(A.shop_id = @shop_id OR @shop_id = 0) AND(A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NOT NULL
    LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID AND A.SchoolID = @SchoolID
    LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
    LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
    --WHERE (A.sID = @sID OR @sID = 0) AND (A.shop_id = @shop_id OR @shop_id = 0) AND (A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NOT NULL
) AS A0
INNER JOIN
(
    SELECT TT1.SchoolID, A.nTotal, A.dSell, B.nPrice, B.nNumber, B.nCost, TT1.TimeStart,A.dayCancal
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('[{8}]')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN [JabjaiSchoolHistory].[dbo].[TSell] AS A ON TT1.SchoolID = A.SchoolID AND A.dSell BETWEEN @DAYSTART AND @DAYEND AND  A.dSell BETWEEN TT1.TimeStart AND TT1.TimeEnd
	AND (A.sID = @sID OR @sID = 0) AND(A.shop_id = @shop_id OR @shop_id = 0) AND(A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NOT NULL
    LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolHistory].[dbo].[TSell_Detail] WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID AND A.SchoolID = @SchoolID
    LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
    LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
    --WHERE (A.sID = @sID OR @sID = 0) AND(A.shop_id = @shop_id OR @shop_id = 0) AND(A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NOT NULL
) AS A1  ON A0.TimeStart = A1.TimeStart AND A1.SchoolID = A0.SchoolID

GROUP BY A0.TimeStart
ORDER BY A0.TimeStart "
     , SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, search.sort_type, FORMAT, JsonDATE);

            return db.Database.SqlQuery<views01>(SQL).ToList();

        }

        internal class Balance
        {
            public int? SchoolID { get; set; }
            public int? sID { get; set; }
            public int? sEmp { get; set; }
            public decimal? nMoney { get; set; }
        }

        public class day_data
        {
            public string lable { get; set; }
            public DateTime values { get; set; }
        }

        public class header_reports
        {
            public string header_text { get; set; }
            public object data { get; set; }
            public int report_type { get; set; }
        }

        public class views01
        {
            public decimal? price { get; set; }
            public decimal? cost { get; set; }
            public decimal? lucre
            {
                get
                {
                    return price - cost;
                }
            }
            public int sort_type { get; set; }

            public string lable { get; set; }
            public string values
            {
                get
                {
                    return dSell.ToString("dd/MM/yyyy", new CultureInfo("en-us"));
                }
            }
            public DateTime dSell { get; set; }
        }

        public class views02
        {
            public List<report_detail> report_Details { get; set; }
            public List<views02_chart> charts { get; set; }
            public string header_text { get; set; }
        }

        public class report_detail
        {
            public string emp_name { get; set; }
            public string user_name { get; set; }
            public string time
            {
                get
                {
                    return string.Format("{0:dd/MM/yyyy HH:mm:ss}", dSell);
                }
            }
            public decimal? nTotal { get; set; }
            public string shop_name { get; set; }
            public int sell_Id { get; set; }
            public string id { get; set; }
            public int userType { get; set; }
            public DateTime? dSell { get; set; }
            public List<product> products { get; set; }

            public decimal? price { get; set; }
            public decimal? cost { get; set; }
            public string barcode { get; set; }
            public string product_name { get; set; }
            public string product_type { get; set; }
            public int? amount { get; set; }
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public string sStudentID { get; set; }
            public DateTime? dayCancal { get; set; }
            public string timeCancal
            {
                get
                {
                    return string.Format("{0:dd/MM/yyyy HH:mm:ss}", dayCancal);
                }
            }

            public int? UserCancel { get; set; }
        }

        public class product
        {
            public decimal? price { get; set; }
            public decimal? cost { get; set; }
            public decimal? lucre
            {
                get
                {
                    return (price ?? 0) - (cost ?? 0);
                }
            }

            public decimal? sum_cost
            {
                get
                {
                    return (cost ?? 0) * (amount ?? 0);
                }
            }
            public decimal? sum_price
            {
                get
                {
                    return (price ?? 0) * (amount ?? 0);
                }
            }
            public decimal? sum_lucre
            {
                get
                {
                    return ((price ?? 0) - (cost ?? 0)) * (amount ?? 0);
                }
            }

            public decimal? total
            {
                get
                {
                    return (price ?? 0) * (amount ?? 0);
                }
            }
            public string barcode { get; set; }
            public string product_name { get; set; }
            public string product_type { get; set; }
            public int? amount { get; set; }
            public DateTime? dayCancal { get; set; }
            public string timeCancal
            {
                get
                {
                    return string.Format("{0:dd/MM/yyyy HH:mm:ss}", dayCancal);
                }
            }
        }

        public class sellDetail
        {
            public decimal? price { get; set; }
            public decimal? cost { get; set; }
            public decimal? lucre { get; set; }
            public decimal? total { get; set; }
            public string barcode { get; set; }
            public string product_name { get; set; }
            public string product_type { get; set; }
            public int? amount { get; set; }
            public int sellId { get; set; }
            public DateTime? dayCancal { get; set; }
            public string timeCancal
            {
                get
                {
                    return string.Format("{0:dd/MM/yyyy HH:mm:ss}", dayCancal);
                }
            }
        }

        public class views02_chart
        {

            public string lable { get; set; }
            public decimal? values { get; set; }
        }

        public class Search
        {
            public int sort_type { get; set; }
            public string type { get; set; }
            public DateTime? dStart { get; set; }
            public DateTime? dEnd { get; set; }
            public int? shop_id { get; set; }
            public int? user_id { get; set; }
            public int? emp_id { get; set; }
        }
    }
}