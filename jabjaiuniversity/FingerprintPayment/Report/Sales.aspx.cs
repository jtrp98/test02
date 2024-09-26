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
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class Sales : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
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
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {

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
                             join b in GetReport_GroupYear(search, userData.CompanyID, db, "01/MM/yyyy", JsonDATE) on a.values.Month equals b.dSell.Month into jab

                             from jb in jab.DefaultIfEmpty()

                                 //group jb by new
                                 //{
                                 //    values = a.values.ToString("dd/MM/yyyy", new CultureInfo("en-us")),
                                 //    lable = a.values.ToString("MMM-yy", new CultureInfo("th-th")),
                                 //}
                                 //into gb
                             select new views01
                             {
                                 dSell = a.values,
                                 lable = a.values.ToString("MMM-yy", new CultureInfo("th-th")),
                                 //values = gb.Key.values,
                                 cost = jb == null ? 0 : jb.cost,
                                 price = jb == null ? 0 : jb.price,
                                 deduct = jb == null ? 0 : jb.deduct,
                                 //lucre = gb.Sum(s => s == null ? 0 : s.price - s.cost),
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

                    var q1 = GetReport_GroupMonth(search, userData.CompanyID, db, "dd/MM/yyyy", JsonDATE);
                    var q = (from a in day_data
                             join b in q1 on a.lable.Trim() equals b.values.Trim() into jab

                             from jb in jab.DefaultIfEmpty()

                             select new views01
                             {
                                 dSell = a.values,
                                 lable = a.values.ToString(search.sort_type == 0 ? "dddd" : "dd MMM yy", new CultureInfo("th-th")),
                                 //values = gb.Key.values,
                                 cost = jb == null ? 0 : jb.cost,
                                 price = jb == null ? 0 : jb.price,
                                 deduct = jb == null ? 0 : jb.deduct,
                                 //lucre = gb.Sum(s => s == null ? 0 : s.price - s.cost),
                             }).ToList();


                    return new header_reports { header_text = header_text, data = q, report_type = search.sort_type };
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_detail(Search search)
        {
            var userData = GetUserData();

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {

                List<day_data> day_data = new List<day_data>();

                search.dEnd = search.dStart.Value.AddDays(1.0);

                var q = GetReport_Details(search, userData.CompanyID, db);

                var q1 = (from a in q
                          group a by new { a.dSell, a.dayCancal, a.UpdatedTime, a.sell_Id, a.emp_name, a.user_name, a.id, a.userType, a.shop_name, a.nTotal, a.DeviceID, a.ApplicationName, a.CLASSNAME } into gb
                          select new report_detail
                          {
                              dSell = gb.Key.dSell,
                              UpdatedTime = gb.Key.UpdatedTime,
                              emp_name = gb.Key.emp_name,
                              user_name = gb.Key.user_name,
                              sell_Id = gb.Key.sell_Id,
                              id = gb.Key.id,
                              userType = gb.Key.userType,
                              dayCancal = gb.Key.dayCancal,
                              cancle_status = gb.Max(i => i.cancle_status),
                              cancle_name = gb.Max(i => i.cancle_name),
                              CLASSNAME = gb.Key.CLASSNAME,
                              DeviceID = gb.Key.DeviceID == gb.Key.ApplicationName ? gb.Key.ApplicationName : gb.Key.ApplicationName + " " + gb.Key.DeviceID,
                              IsEqualCancleDay = gb.Key.dayCancal?.Date == search.dStart?.Date,
                              IsEqualSynDay = gb.Key.UpdatedTime?.Date == search.dStart?.Date,
                              IsEqualSearchDay = gb.Key.dSell?.Date == search.dStart?.Date,
                              IsOffline = (gb.Key.UpdatedTime - gb.Key.dSell)?.TotalSeconds > 3,
                              //IsSum = (gb.Key.dayCancal ?? gb.Key.UpdatedTime ?? gb.Key.dSell)?.Date == search.dStart?.Date  ? 1 : 0,
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
                                                          deduct = S.DeductPercent,
                                                      }).ToList(), gb.Key.nTotal),
                              shop_name = gb.Key.shop_name,
                              PaymentType = gb.Max(i => i.PaymentType),
                              RefPromptPay = gb.Max(i => i.RefPromptPay) + "",
                              RemarkPromptPay = gb.Max(i => i.RemarkPromptPay)?.Trim() + "",
                              BuyPromptPay = gb.Min(i => i.CreateDatePromptPay)?.ToString("dd/MM/yyyy HH:mm" , new CultureInfo("th-TH"))+"",
                              KTType = gb.Max(i => i.KTType) + "",
                          }).OrderBy(o => o.UpdatedTime ?? o.dSell).ToList();

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

                string header_text = "ยอดขาย วันที่ " + search.dStart.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));

                return new views02 { charts = q2, report_Details = q1, header_text = header_text };
            }
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
                    //lucre = 0,
                    product_name = "-",
                    product_type = "-",
                    deduct = _Total * details.Max(o => o.deduct) * (decimal)0.01,
                    //total = _Total
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
                                //lucre = (a2.price - (a2.cost ?? 0)) * a2.amount,
                                //total = a2.price * a2.amount,
                                price = a2.price,
                                product_name = a2.product_name,
                                product_type = a2.product_type,
                                deduct = (a2.price ?? 0) * (a2.amount ?? 0) * (a2.deduct ?? 0) * (decimal)0.01f,
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
                    excel.Workbook.Worksheets.Add("รายงานการซื้อ-ขายสินค้า");

                    var worksheet = excel.Workbook.Worksheets["รายงานการซื้อ-ขายสินค้า"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();

                    var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                    SetHeader(worksheet, "A1:L1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, "A2:L2", true, report.header_text, 14, ExcelHorizontalAlignment.Center);

                    SetHeader(worksheet, "A3:K3", true, "พิมพ์วันที่ :", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, "L3:L3", true, DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Left);

                    SetHeader(worksheet, "A4:K4", true, "เวลา :", null, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, "L4:L4", true, DateTime.Now.ToString("HH:mm:ss"), null, ExcelHorizontalAlignment.Left);

                    SetHeader(worksheet, "A5:L5", true, "", null, ExcelHorizontalAlignment.Center);

                    string[] strHeader = { "ลำดับ", "เวลา", "ร้านค้า", "ชั้นเรียน", "ชื่อผู้ซื้อ", "ประเภท", "รหัสบาร์โค้ด", "ชื่อสินค้า", "ราคาต้นทุน", "ราคาขาย", "จำนวน\n(หน่วย)", "ยอดรวม\n(บาท)", "หมายเหตุ" };
                    int Columuns = 1;
                    foreach (string str in strHeader)
                    {
                        if (search.shop_id.HasValue && str == "ร้านค้า")
                            SetTableHeader(worksheet.Cells[6, Columuns++], false, "ชื่อผู้ขาย", ExcelHorizontalAlignment.Center);
                        else
                            SetTableHeader(worksheet.Cells[6, Columuns++], false, str, ExcelHorizontalAlignment.Center);
                    }

                    int Rows = 7, Index = 1;
                    Decimal Cost = 0, Total = 0, Lucre = 0, Cancle = 0, Deduct = 0, Offline = 0, Cancle2 = 0;
                    foreach (var dataSell in report.report_Details)
                    {
                        if (dataSell.products.Count() == 0) continue;
                        int rowsEnd = Rows + (dataSell.products.Count() - 1);
                        SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], true, Index + "", ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 2, rowsEnd, 2], true, dataSell.time, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 3, rowsEnd, 3], true, search.shop_id.HasValue ? dataSell.emp_name : dataSell.shop_name, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 4, rowsEnd, 4], true, dataSell.CLASSNAME, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 5, rowsEnd, 5], true, dataSell.user_name, ExcelHorizontalAlignment.Center);
                        foreach (var productData in dataSell.products)
                        {
                            SetTableRows(worksheet.Cells[Rows, 6], false, productData.product_type, ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 7], false, productData.barcode, ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 8], false, productData.product_name, ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 9], false, ((productData.cost ?? 0) + ""), ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 10], false, (productData.price + ""), ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 11], false, productData.amount + "", ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 12], false, (productData.total + ""), ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 13], false, dataSell.DeviceID + "\r\n"
                                + (dataSell.remark.Replace("$", " "))
                                + (dataSell.PaymentType == 3 ? " ซื้อสินค้าผ่านพร้อมเพย์" : "")
                                + (productData.total == 0 ? " เบิก/ฟรี" : "")
                                , ExcelHorizontalAlignment.Center);

                            Rows += 1;
                        }
                        Index++;


                        if (dataSell.cancle_status == "Y")
                        {
                            if (dataSell.IsEqualSearchDay || dataSell.IsEqualSynDay)
                            {
                                Total += dataSell.products.Sum(s => s.total) ?? 0;
                                //Price += dataSell.products.Sum(s => s.sum_price) ?? 0;
                                Cost += dataSell.products.Sum(s => s.sum_cost) ?? 0;
                                Lucre += dataSell.products.Sum(s => s.sum_lucre) ?? 0;
                                Deduct += dataSell.products.Sum(s => s.deduct) ?? 0;
                            }

                            if (dataSell.IsEqualCancleDay)
                            {
                                Cancle += dataSell.products.Sum(s => s.total) ?? 0;
                                Deduct -= dataSell.products.Sum(s => s.deduct) ?? 0;
                            }
                            else
                            {
                                Cancle2 += dataSell.products.Sum(s => s.total) ?? 0;
                            }
                        }
                        else if (dataSell.IsOffline && !dataSell.IsEqualSynDay)
                        {
                            Offline += dataSell.products.Sum(s => s.total) ?? 0;
                        }
                        else if (dataSell.IsEqualSearchDay || dataSell.IsEqualSynDay)
                        {
                            Total += dataSell.products.Sum(s => s.total) ?? 0;
                            //Price += dataSell.products.Sum(s => s.sum_price) ?? 0;
                            Cost += dataSell.products.Sum(s => s.sum_cost) ?? 0;
                            Lucre += dataSell.products.Sum(s => s.sum_lucre) ?? 0;
                            Deduct += dataSell.products.Sum(s => s.deduct) ?? 0;
                        }
                    }


                    SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "ยอดขายรวม", ExcelHorizontalAlignment.Right);
                    SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 12], true, string.Format("{0:#,##0.00}", Total), ExcelHorizontalAlignment.Center);

                    SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "ยอดยกเลิกรวม", ExcelHorizontalAlignment.Right);
                    SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 12], true, string.Format("{0:#,##0.00}", Cancle), ExcelHorizontalAlignment.Center);

                    SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "ยอดสุทธิ", ExcelHorizontalAlignment.Right);
                    SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 12], true, string.Format("{0:#,##0.00}", Total - Cancle), ExcelHorizontalAlignment.Center);

                    SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "ยอดหัก%", ExcelHorizontalAlignment.Right);
                    SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 12], true, string.Format("{0:#,##0.00}", Deduct), ExcelHorizontalAlignment.Center);

                    SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "ต้นทุน", ExcelHorizontalAlignment.Right);
                    SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 12], true, string.Format("{0:#,##0.00}", Cost), ExcelHorizontalAlignment.Center);

                    SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "กำไร", ExcelHorizontalAlignment.Right);
                    SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 12], true, string.Format("{0:#,##0.00}", Lucre - Deduct - Cancle), ExcelHorizontalAlignment.Center);

                    if (Offline > 0)
                    {
                        SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "รายการออฟไลน์", ExcelHorizontalAlignment.Right);
                        SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 12], true, string.Format("{0:#,##0.00}", Offline) + Environment.NewLine + "หมายเหตุ : รายการออฟไลน์ นับยอดในวันที่เงินเข้าระบบออนไลน์", ExcelHorizontalAlignment.Center);
                    }

                    if (Cancle2 > 0)
                    {
                        SetTableFooter(worksheet.Cells[Rows, 1, Rows, 9], true, "รายการยกเลิก", ExcelHorizontalAlignment.Right);
                        SetTableFooter(worksheet.Cells[Rows, 10, Rows++, 12], true, string.Format("{0:#,##0.00}", Cancle2) + Environment.NewLine + "หมายเหตุ : รายการยกเลิก จะนับยอดในวันที่กระทำการยกเลิก", ExcelHorizontalAlignment.Center);
                    }

                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(1).Width = 7;
                    worksheet.Column(2).Width = 15;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 20;
                    worksheet.Column(6).Width = 24;
                    worksheet.Column(7).Width = 24;
                    worksheet.Column(8).Width = 24;
                    worksheet.Column(10).Width = 15;
                    worksheet.Column(11).Width = 15;
                    worksheet.Column(12).Width = 15;
                    worksheet.Column(13).Width = 20;


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
                    SetTableRows(worksheet.Cells[Rows, 5], false, string.Format("{0:#,##0.00} บาท", dataSell.lucre - dataSell.deduct), ExcelHorizontalAlignment.Center);
                    Rows++;
                }

                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 4], true, "ยอดรวม", ExcelHorizontalAlignment.Right);
                SetTableFooter(worksheet.Cells[Rows++, 5], false, string.Format("{0:#,##0.00} บาท", reportData.Sum(s => s.price)), ExcelHorizontalAlignment.Center);


                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 4], true, "ยอดหัก%", ExcelHorizontalAlignment.Right);
                SetTableFooter(worksheet.Cells[Rows++, 5], false, string.Format("{0:#,##0.00} บาท", reportData.Sum(s => s.deduct)), ExcelHorizontalAlignment.Center);

                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 4], true, "ต้นทุน", ExcelHorizontalAlignment.Right);
                SetTableFooter(worksheet.Cells[Rows++, 5], false, string.Format("{0:#,##0.00} บาท", reportData.Sum(s => s.cost)), ExcelHorizontalAlignment.Center);

                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 4], true, "กำไร", ExcelHorizontalAlignment.Right);
                SetTableFooter(worksheet.Cells[Rows++, 5], false, string.Format("{0:#,##0.00} บาท", reportData.Sum(s => s.lucre) - reportData.Sum(s => s.deduct)), ExcelHorizontalAlignment.Center);

                worksheet.Cells.AutoFitColumns();
                worksheet.Column(1).Width = 7;
                worksheet.Row(6).Height = 25;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 20;
                worksheet.Column(5).Width = 20;

                HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.Write("รายงานการซื้อ-ขายสินค้า" + report.header_text + " โรงเรียน" + tCompany.sCompany + "_" + DateTime.Now.ToString("ddMMyyyyHHmmssss") + ".xls");
                //HttpContext.Current.Response.AddHeader("filename", "รายงานการซื้อ-ขายสินค้า" + report.header_text + " โรงเรียน" + tCompany.sCompany + "_" + DateTime.Now.ToString("ddMMyyyyHHmmssss") + ".xls");
                //HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=รายงานการซื้อ-ขายสินค้า" + report.header_text + " โรงเรียน" + tCompany.sCompany + "_" + DateTime.Now.ToString("ddMMyyyyHHmmssss") + ".xls");
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

        private static void SetTableRows(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal, bool isWrap = false)
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
                rng.Style.WrapText = isWrap;
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
            StudentLogic logic = new StudentLogic(db);
            string TermID1 = logic.GetLastTermId(search.dStart ?? DateTime.Today, new JWTToken.userData { CompanyID = SchoolID });
            string TermID2 = logic.GetTermId(search.dStart ?? DateTime.Today, new JWTToken.userData { CompanyID = SchoolID });

            var isSameTerm = TermID1 == TermID2;

            string SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @nTerm1 NVARCHAR(20) = '{6}';
DECLARE @nTerm2 NVARCHAR(20) = '{7}';

SELECT A.dSell 'dSell',A.UpdatedTime,A.sSellID AS 'sell_Id',A.id,A.nTotal,
CASE WHEN E1.sEmp IS NULL THEN  '' ELSE E1.sName + ' ' + E1.sLastname END AS 'emp_name',
CASE WHEN U2.CardHistoryID IS NOT NULL THEN U2.UserName + ' (บัตรสำรอง)'
WHEN U01.sID IS NOT NULL THEN U01.sStudentID + ' ' + U01.sName + ' ' + U01.sLastname 
WHEN U02.sID IS NOT NULL THEN U02.sStudentID + ' ' + U02.sName + ' ' + U02.sLastname 
WHEN U03.sID IS NOT NULL THEN U03.sStudentID + ' ' + U03.sName + ' ' + U03.sLastname 
WHEN U1.sEmp IS NOT NULL THEN U1.sName + ' ' + U1.sLastname ELSE '' END AS 'user_name',
ISNULL(B.nNumber,1) AS 'amount',ISNULL(C.sBarCode,'') AS 'barcode',ISNULL(S.shop_name,'') AS shop_name,
ISNULL(C.sProduct,'') AS 'product_name',ISNULL(D.sType,'') AS 'product_type',ISNULL(B.nCost,C.nCost) AS 'cost',B.nPrice AS 'price', 
ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1) AS 'sum_cost',B.nPrice * ISNULL(B.nNumber,1) AS 'sum_price' ,
 S.DeductPercent  ,
CASE WHEN A.dayCancal IS NULL THEN 'N' ELSE 'Y' END 'cancle_status', 
CASE WHEN A.UserCancel = 99999 THEN 'System Adjusted' ELSE ( CASE WHEN E2.sEmp IS NULL THEN  '' ELSE E2.sName + ' ' + E2.sLastname END ) END AS 'cancle_name',
A.dayCancal , A.PaymentType,ISNULL(A.DeviceID,'') AS DeviceID , ISNULL(A.ApplicationName,'') AS ApplicationName
,CASE WHEN U01.sID IS NOT NULL THEN ISNULL(U01.SubLevel + ' / ' + U01.nTSubLevel2,'') 
      WHEN U02.sID IS NOT NULL THEN ISNULL(U02.SubLevel + ' / ' + U02.nTSubLevel2,'') ELSE '' END AS 'CLASSNAME'
, KT.Type 'KTType' , A.RefPromptPay , KT.Remark 'RemarkPromptPay' , KT.CreateDate 'CreateDatePromptPay'

FROM TSell AS A
LEFT OUTER JOIN (SELECT * FROM TSell_Detail WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID
LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
LEFT OUTER JOIN TShop AS S ON A.shop_id = S.shop_id AND A.SchoolID = S.SchoolID
LEFT OUTER JOIN TEmployees AS E1 ON A.sEmp = E1.sEmp AND A.SchoolID = E1.SchoolID
LEFT OUTER JOIN TB_StudentViews AS U01 ON A.sID = U01.sID AND A.SchoolID = U01.SchoolID AND U01.nTerm = @nTerm1
LEFT OUTER JOIN TB_StudentViews AS U02 ON A.sID = U02.sID AND A.SchoolID = U02.SchoolID AND U02.nTerm = @nTerm2
LEFT JOIN TUser AS U03 ON A.sID = U03.sID AND A.SchoolID = U03.SchoolID
LEFT OUTER JOIN TEmployees AS U1 ON A.sID2 = U1.sEmp AND A.SchoolID = U1.SchoolID
LEFT OUTER JOIN TBackupCardHistory AS U2 ON A.CardID = U2.CardHistoryID AND A.SchoolID = U2.SchoolID
LEFT JOIN TEmployees AS E2 ON A.UserCancel = E2.sEmp AND A.SchoolID = E2.SchoolID

LEFT JOIN (
	SELECT Type, ChargeID, Remark, CreateDate
	FROM
	(		
		SELECT 'KBank/' 'Type',ChargeID
		, (CASE WHEN DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 10 AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(k.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(k.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END)  'Remark' , CreateDate
		FROM JabjaiSchoolSingleDB.dbo.KTransaction k		
		WHERE SchoolID=@SchoolID AND CreateDate BETWEEN DATEADD(DAY,-5,@DAYSTART) AND @DAYEND  AND ChargeID IS NOT NULL
		UNION ALL
		SELECT 'KBank/' 'Type',ChargeID
		, (CASE WHEN DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 10 AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(k.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(k.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END)  'Remark' , CreateDate
		FROM JabjaiSchoolHistory.dbo.KTransaction k
		WHERE SchoolID=@SchoolID AND CreateDate BETWEEN DATEADD(DAY,-5,@DAYSTART) AND @DAYEND AND ChargeID IS NOT NULL
	    UNION ALL
		SELECT 'KTB/' 'Type', tx.Ref2 'ChargeID'
		, (CASE WHEN CAST(tx.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 THEN 'BOT3AM' ELSE '' END)  
		+' '+ (CASE WHEN CAST(tx.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) <= 2 AND DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) >= -2) THEN 'BANKDELAY' ELSE '' END)  
		+' '+ (CASE WHEN CAST(tx.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) > 2 OR DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) < -2) THEN 'BOTMANUAL' ELSE '' END) 'Remark' , tx.CreateDate
		FROM JabjaiSchoolSingleDB.dbo.KTBTransaction tx
		LEFT JOIN JabjaiSchoolSingleDB.dbo.KTBPaymentTransaction ptx ON tx.SchoolID = ptx.SchoolID AND tx.Ref2 = ptx.Ref2 AND ptx.RespCode = 0
		WHERE tx.SchoolID=@SchoolID AND tx.CreateDate BETWEEN DATEADD(DAY,-5,@DAYSTART) AND @DAYEND AND tx.Ref2 IS NOT NULL
	) A
	GROUP BY Type, ChargeID, Remark, CreateDate
) KT ON A.RefPromptPay = KT.ChargeID

WHERE A.SchoolID = @SchoolID AND ( A.dSell BETWEEN @DAYSTART AND @DAYEND  OR A.UpdatedTime BETWEEN @DAYSTART AND @DAYEND  OR A.dayCancal BETWEEN @DAYSTART AND @DAYEND)
AND (A.sID = @sID OR A.sID2 = @sID OR @sID = 0) AND (S.shop_id = @shop_id OR @shop_id = 0) AND (A.sEmp = @emp_id OR @emp_id = 0) 
ORDER BY A.dSell
"
     , SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, TermID1, TermID2);

            q.AddRange(db.Database.SqlQuery<report_detail>(SQL).ToList());

            SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @nTerm1 NVARCHAR(20) = '{6}';
DECLARE @nTerm2 NVARCHAR(20) = '{7}';

SELECT A.dSell 'dSell',A.UpdatedTime,A.sSellID AS 'sell_Id',A.id,A.nTotal,
CASE WHEN E1.sEmp IS NULL THEN  '' ELSE E1.sName + ' ' + E1.sLastname END AS 'emp_name',
CASE WHEN U2.CardHistoryID IS NOT NULL THEN U2.UserName + ' (บัตรสำรอง)'
WHEN U01.sID IS NOT NULL THEN U01.sStudentID + ' ' + U01.sName + ' ' + U01.sLastname 
WHEN U02.sID IS NOT NULL THEN U02.sStudentID + ' ' + U02.sName + ' ' + U02.sLastname 
WHEN U03.sID IS NOT NULL THEN U03.sStudentID + ' ' + U03.sName + ' ' + U03.sLastname 
WHEN U1.sEmp IS NOT NULL THEN U1.sName + ' ' + U1.sLastname ELSE '' END AS 'user_name',
ISNULL(B.nNumber,1) AS 'amount',ISNULL(C.sBarCode,'') AS 'barcode',ISNULL(S.shop_name,'') AS shop_name,
ISNULL(C.sProduct,'') AS 'product_name',ISNULL(D.sType,'') AS 'product_type',ISNULL(B.nCost,C.nCost) AS 'cost',B.nPrice AS 'price', 
ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1) AS 'sum_cost',B.nPrice * ISNULL(B.nNumber,1) AS 'sum_price' ,
 S.DeductPercent  ,
CASE WHEN A.dayCancal IS NULL THEN 'N' ELSE 'Y' END 'cancle_status', 
CASE WHEN A.UserCancel = 99999 THEN 'System Adjusted' ELSE ( CASE WHEN E2.sEmp IS NULL THEN  '' ELSE E2.sName + ' ' + E2.sLastname END ) END AS 'cancle_name',
A.dayCancal , A.PaymentType,ISNULL(A.DeviceID,'') AS DeviceID, ISNULL(A.ApplicationName,'') AS ApplicationName
,CASE WHEN U01.sID IS NOT NULL THEN ISNULL(U01.SubLevel + ' / ' + U01.nTSubLevel2,'') 
      WHEN U02.sID IS NOT NULL THEN ISNULL(U02.SubLevel + ' / ' + U02.nTSubLevel2,'') ELSE '' END AS 'CLASSNAME'
, KT.Type 'KTType' , A.RefPromptPay , KT.Remark 'RemarkPromptPay' , KT.CreateDate 'CreateDatePromptPay'

FROM [JabjaiSchoolHistory].[dbo].[TSell]  AS A 
LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolHistory].[dbo].[TSell_Detail] WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID
LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
LEFT OUTER JOIN TShop AS S ON A.shop_id = S.shop_id AND A.SchoolID = S.SchoolID
LEFT OUTER JOIN TEmployees AS E1 ON A.sEmp = E1.sEmp AND A.SchoolID = E1.SchoolID
LEFT OUTER JOIN TB_StudentViews AS U01 ON A.sID = U01.sID AND A.SchoolID = U01.SchoolID AND U01.nTerm = @nTerm1
LEFT OUTER JOIN TB_StudentViews AS U02 ON A.sID = U02.sID AND A.SchoolID = U02.SchoolID AND U02.nTerm = @nTerm2
LEFT JOIN       TUser AS U03 ON A.sID = U03.sID AND A.SchoolID = U03.SchoolID
LEFT OUTER JOIN TEmployees AS U1 ON A.sID2 = U1.sEmp AND A.SchoolID = U1.SchoolID
LEFT OUTER JOIN TBackupCardHistory AS U2 ON A.CardID = U2.CardHistoryID AND A.SchoolID = U2.SchoolID
LEFT JOIN TEmployees AS E2 ON A.UserCancel = E2.sEmp AND A.SchoolID = E2.SchoolID

LEFT JOIN (
	SELECT Type, ChargeID, Remark, CreateDate
	FROM
	(		
		SELECT 'KBank/' 'Type',ChargeID
		, (CASE WHEN DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 10 AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(k.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(k.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END)  'Remark' , CreateDate
		FROM JabjaiSchoolSingleDB.dbo.KTransaction k		
		WHERE SchoolID=@SchoolID AND CreateDate BETWEEN @DAYSTART AND @DAYEND  AND ChargeID IS NOT NULL
		UNION ALL
		SELECT 'KBank/' 'Type',ChargeID
		, (CASE WHEN DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 10 AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(k.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(k.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END)  'Remark' ,CreateDate
		FROM JabjaiSchoolHistory.dbo.KTransaction k
		WHERE SchoolID=@SchoolID AND CreateDate BETWEEN @DAYSTART AND @DAYEND AND ChargeID IS NOT NULL
	    UNION ALL
		SELECT 'KTB/' 'Type', tx.Ref2 'ChargeID'
		, (CASE WHEN CAST(tx.UpdatedDate AS TIME) BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 THEN 'BOT8AM' ELSE '' END)  
		+' '+ (CASE WHEN CAST(tx.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) <= 2 AND DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) >= -2) THEN 'BANKDELAY' ELSE '' END)  
		+' '+ (CASE WHEN CAST(tx.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) > 2 OR DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) < -2) THEN 'BOTMANUAL' ELSE '' END) 'Remark' , tx.CreateDate
		FROM JabjaiSchoolSingleDB.dbo.KTBTransaction tx
		LEFT JOIN JabjaiSchoolSingleDB.dbo.KTBPaymentTransaction ptx ON tx.SchoolID = ptx.SchoolID AND tx.Ref2 = ptx.Ref2 AND ptx.RespCode = 0
		WHERE tx.SchoolID=@SchoolID AND tx.CreateDate BETWEEN @DAYSTART AND @DAYEND AND tx.Ref2 IS NOT NULL
	) A
	GROUP BY Type, ChargeID, Remark, CreateDate
) KT ON A.RefPromptPay = KT.ChargeID

WHERE A.SchoolID = @SchoolID AND ( A.dSell BETWEEN @DAYSTART AND @DAYEND  OR A.UpdatedTime BETWEEN @DAYSTART AND @DAYEND OR A.dayCancal BETWEEN @DAYSTART AND @DAYEND)
AND (A.sID = @sID OR A.sID2 = @sID OR @sID = 0) AND (S.shop_id = @shop_id OR @shop_id = 0) AND (A.sEmp = @emp_id OR @emp_id = 0)  
ORDER BY A.dSell "
, SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, TermID1, TermID2);
            q.AddRange(db.Database.SqlQuery<report_detail>(SQL).ToList());

            var oldDate = new DateTime(DateTime.Now.Year, 05, 01);
            if (search.dStart <= oldDate)
            {
                SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @nTerm1 NVARCHAR(20) = '{6}';
DECLARE @nTerm2 NVARCHAR(20) = '{7}';

SELECT A.dSell 'dSell',A.UpdatedTime,A.sSellID AS 'sell_Id',A.id,A.nTotal,
CASE WHEN E1.sEmp IS NULL THEN  '' ELSE E1.sName + ' ' + E1.sLastname END AS 'emp_name',
CASE WHEN U2.CardHistoryID IS NOT NULL THEN U2.UserName + ' (บัตรสำรอง)'
WHEN U01.sID IS NOT NULL THEN U01.sStudentID + ' ' + U01.sName + ' ' + U01.sLastname 
WHEN U02.sID IS NOT NULL THEN U02.sStudentID + ' ' + U02.sName + ' ' + U02.sLastname 
WHEN U03.sID IS NOT NULL THEN U03.sStudentID + ' ' + U03.sName + ' ' + U03.sLastname 
WHEN U1.sEmp IS NOT NULL THEN U1.sName + ' ' + U1.sLastname ELSE '' END AS 'user_name',
ISNULL(B.nNumber,1) AS 'amount',ISNULL(C.sBarCode,'') AS 'barcode',ISNULL(S.shop_name,'') AS shop_name,
ISNULL(C.sProduct,'') AS 'product_name',ISNULL(D.sType,'') AS 'product_type',ISNULL(B.nCost,C.nCost) AS 'cost',B.nPrice AS 'price', 
ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1) AS 'sum_cost',B.nPrice * ISNULL(B.nNumber,1) AS 'sum_price' ,
 S.DeductPercent  ,
CASE WHEN A.dayCancal IS NULL THEN 'N' ELSE 'Y' END 'cancle_status', 
CASE WHEN A.UserCancel = 99999 THEN 'System Adjusted' ELSE ( CASE WHEN E2.sEmp IS NULL THEN  '' ELSE E2.sName + ' ' + E2.sLastname END ) END AS 'cancle_name',
A.dayCancal , A.PaymentType,ISNULL(A.DeviceID,'') AS DeviceID, ISNULL(A.ApplicationName,'') AS ApplicationName
,CASE WHEN U01.sID IS NOT NULL THEN ISNULL(U01.SubLevel + ' / ' + U01.nTSubLevel2,'') 
      WHEN U02.sID IS NOT NULL THEN ISNULL(U02.SubLevel + ' / ' + U02.nTSubLevel2,'') ELSE '' END AS 'CLASSNAME'
, KT.Type 'KTType' , A.RefPromptPay , KT.Remark 'RemarkPromptPay' , KT.CreateDate 'CreateDatePromptPay'

FROM [JabjaiSchoolHistory].[dbo].[TSell_Backup]  AS A 
LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolHistory].[dbo].[TSell_Detail_Backup] WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID
LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
LEFT OUTER JOIN TShop AS S ON A.shop_id = S.shop_id AND A.SchoolID = S.SchoolID
LEFT OUTER JOIN TEmployees AS E1 ON A.sEmp = E1.sEmp AND A.SchoolID = E1.SchoolID
LEFT OUTER JOIN TB_StudentViews AS U01 ON A.sID = U01.sID AND A.SchoolID = U01.SchoolID AND U01.nTerm = @nTerm1
LEFT OUTER JOIN TB_StudentViews AS U02 ON A.sID = U02.sID AND A.SchoolID = U02.SchoolID AND U02.nTerm = @nTerm2
LEFT JOIN       TUser AS U03 ON A.sID = U03.sID AND A.SchoolID = U03.SchoolID
LEFT OUTER JOIN TEmployees AS U1 ON A.sID2 = U1.sEmp AND A.SchoolID = U1.SchoolID
LEFT OUTER JOIN TBackupCardHistory AS U2 ON A.CardID = U2.CardHistoryID AND A.SchoolID = U2.SchoolID
LEFT JOIN TEmployees AS E2 ON A.UserCancel = E2.sEmp AND A.SchoolID = E2.SchoolID

LEFT JOIN (
	SELECT Type, ChargeID, Remark, CreateDate
	FROM
	(		
		SELECT 'KBank/' 'Type',ChargeID
		, (CASE WHEN DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 10 AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(k.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(k.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END)  'Remark' ,CreateDate
		FROM JabjaiSchoolSingleDB.dbo.KTransaction k		
		WHERE SchoolID=@SchoolID AND CreateDate BETWEEN @DAYSTART AND @DAYEND  AND ChargeID IS NOT NULL
		UNION ALL
		SELECT 'KBank/' 'Type',ChargeID
		, (CASE WHEN DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 10 AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) < 60 THEN 'BOT15MIN' ELSE '' END)  
		+' '+(CASE WHEN CAST(k.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOT3AM'  ELSE '' END) 
		+' '+ (CASE WHEN CAST(k.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 'BOTMANUAL' ELSE '' END)  'Remark', CreateDate
		FROM JabjaiSchoolHistory.dbo.KTransaction k
		WHERE SchoolID=@SchoolID AND CreateDate BETWEEN @DAYSTART AND @DAYEND AND ChargeID IS NOT NULL
	    UNION ALL
		SELECT 'KTB/' 'Type', tx.Ref2 'ChargeID'
		, (CASE WHEN CAST(tx.UpdatedDate AS TIME) BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 THEN 'BOT8AM' ELSE '' END)  
		+' '+ (CASE WHEN CAST(tx.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) <= 2 AND DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) >= -2) THEN 'BANKDELAY' ELSE '' END)  
		+' '+ (CASE WHEN CAST(tx.UpdatedDate AS TIME) NOT BETWEEN '08:00:00' AND '08:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) > 2 OR DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) < -2) THEN 'BOTMANUAL' ELSE '' END) 'Remark' ,tx.CreateDate
		FROM JabjaiSchoolSingleDB.dbo.KTBTransaction tx
		LEFT JOIN JabjaiSchoolSingleDB.dbo.KTBPaymentTransaction ptx ON tx.SchoolID = ptx.SchoolID AND tx.Ref2 = ptx.Ref2 AND ptx.RespCode = 0
		WHERE tx.SchoolID=@SchoolID AND tx.CreateDate BETWEEN @DAYSTART AND @DAYEND AND tx.Ref2 IS NOT NULL
	) A
	GROUP BY Type, ChargeID, Remark, CreateDate
) KT ON A.RefPromptPay = KT.ChargeID

WHERE A.SchoolID = @SchoolID AND ( A.dSell BETWEEN @DAYSTART AND @DAYEND  OR A.UpdatedTime BETWEEN @DAYSTART AND @DAYEND OR A.dayCancal BETWEEN @DAYSTART AND @DAYEND)
AND (U01.sID = @sID OR U02.sID = @sID OR U1.sEmp = @sID OR @sID = 0) AND (S.shop_id = @shop_id OR @shop_id = 0) AND (A.sEmp = @emp_id OR @emp_id = 0)  

ORDER BY A.dSell "
, SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, TermID1, TermID2);
                q.AddRange(db.Database.SqlQuery<report_detail>(SQL).ToList());
            }
            return q;

        }

        private static List<views01> GetReport_GroupYear_v1(Search search, int SchoolID, JabJaiEntities db, string FORMAT, string JsonDATE)
        {
            var q = new List<views01>();
            string SQL = "";
            var oldDate = new DateTime(DateTime.Now.Year, 05, 01);

            var subSQL = @"

 select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,dayCancal,
--  case when  CONVERT(date,dayCancal) <> CONVERT( date, ISNULL(UpdatedTime,dSell) ) THEN null else dayCancal END  'dayCancal',
	B.nPrice , B.nCost, B.nNumber, B.nProduct

from 	[JabjaiSchoolSingleDB].[dbo].[TSell] A
LEFT JOIN [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
WHERE A.SchoolID = @SchoolID and (dayCancal BETWEEN @DAYSTART AND @DAYEND OR ISNULL(UpdatedTime,dSell) BETWEEN @DAYSTART AND @DAYEND )
AND (sID = @sID OR @sID = 0 OR sID2 = @sID) AND(shop_id = @shop_id OR @shop_id = 0) AND(sEmp = @emp_id OR @emp_id = 0)

UNION 

select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,dayCancal,
--  case when  CONVERT(date,dayCancal) <> CONVERT( date, ISNULL(UpdatedTime,dSell) ) THEN null else dayCancal END  'dayCancal',
	B.nPrice , B.nCost, B.nNumber, B.nProduct

from 	JabjaiSchoolHistory.[dbo].[TSell] A
LEFT JOIN JabjaiSchoolHistory.[dbo].[TSell_Detail] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
WHERE A.SchoolID = @SchoolID and (dayCancal BETWEEN @DAYSTART AND @DAYEND OR ISNULL(UpdatedTime,dSell) BETWEEN @DAYSTART AND @DAYEND )
AND (sID = @sID OR @sID = 0 OR sID2 = @sID) AND(shop_id = @shop_id OR @shop_id = 0) AND(sEmp = @emp_id OR @emp_id = 0)

";

            if (search.dStart <= oldDate)
            {
                subSQL += @"
UNION 

select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,dayCancal,
--  case when  CONVERT(date,dayCancal) <> CONVERT( date, ISNULL(UpdatedTime,dSell) ) THEN null else dayCancal END  'dayCancal',
	B.nPrice , B.nCost, B.nNumber, B.nProduct

from 	JabjaiSchoolHistory.[dbo].[TSell_Backup] A
LEFT JOIN JabjaiSchoolHistory.[dbo].[TSell_Detail_Backup] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
WHERE A.SchoolID = @SchoolID and (dayCancal BETWEEN @DAYSTART AND @DAYEND OR ISNULL(UpdatedTime,dSell) BETWEEN @DAYSTART AND @DAYEND )
AND (sID = @sID OR @sID = 0 OR sID2 = @sID) AND(shop_id = @shop_id OR @shop_id = 0) AND(sEmp = @emp_id OR @emp_id = 0)
";
            }

            SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @sort_type INT = {6};
DECLARE @FORMAT VARCHAR(20) = '{7}';

SELECT A0.TimeStart AS 'values',CONVERT(DATETIME,FORMAT(A0.TimeStart, @FORMAT),103) AS dSell,
SUM(A0.price) AS 'price',
SUM(A0.cost) AS 'cost',
SUM(A0.deduct) 'deduct' ,
@sort_type AS 'sort_type',A0.TimeStart
FROM
(
    SELECT TT1.SchoolID,  A.shop_id,
     SUM(ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 )  AS 'price'  ,
     SUM(ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 ) *0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct',
    SUM(ISNULL(ISNULL(A.nCost, C.nCost), 0) * ISNULL(A.nNumber, 1)) AS 'cost', TT1.TimeStart  
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('[{8}]')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN (
       
        {9}

    ) AS A ON A.SchoolID = TT1.SchoolID AND (dayCancal BETWEEN TT1.TimeStart AND TT1.TimeEnd OR ISNULL(UpdatedTime,dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd )
    --LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] WHERE SchoolID = @SCHOOLID  AND ISNULL(cDel,'0') = '0') AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID 
    LEFT OUTER JOIN (SELECT * FROM TProduct wHERE SchoolID = @SchoolID) AS C ON A.nProduct = C.nProductID 
    LEFT OUTER JOIN (select * from TType wHERE SchoolID = @SchoolID) AS D ON C.nType = D.nTypeID 
	LEFT OUTER JOIN (select * from TShop wHERE SchoolID = @SchoolID) AS E ON E.shop_id = A.shop_id 
	GROUP BY TT1.SchoolID,TT1.TimeStart,A.shop_id
 
) AS A0

GROUP BY A0.TimeStart
ORDER BY A0.TimeStart "
, SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, search.sort_type, FORMAT, JsonDATE
, subSQL
);

            q.AddRange(db.Database.SqlQuery<views01>(SQL).ToList());

            return q;
        }

        private static List<views01> GetReport_GroupMonth_v1(Search search, int SchoolID, JabJaiEntities db, string FORMAT, string JsonDATE)
        {
            var q = new List<views01>();

            string SQL = "";

            var oldDate = new DateTime(DateTime.Now.Year, 05, 01);

            var subSQL = @"

 select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,A.dayCancal, B.nPrice , B.nCost, B.nNumber, B.nProduct
from 	[JabjaiSchoolSingleDB].[dbo].[TSell] A 
LEFT JOIN [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
WHERE A.SchoolID = @SchoolID and (dayCancal BETWEEN @DAYSTART AND @DAYEND OR ISNULL(UpdatedTime,dSell) BETWEEN @DAYSTART AND @DAYEND  )
AND (sID = @sID OR @sID = 0 OR sID2 = @sID) AND(shop_id = @shop_id OR @shop_id = 0) AND(sEmp = @emp_id OR @emp_id = 0) 
		
UNION

select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,A.dayCancal, B.nPrice , B.nCost, B.nNumber , B.nProduct
from JabjaiSchoolHistory.[dbo].[TSell] A 
LEFT JOIN JabjaiSchoolHistory.[dbo].[TSell_Detail] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
WHERE A.SchoolID = @SchoolID and (dayCancal BETWEEN @DAYSTART AND @DAYEND OR ISNULL(UpdatedTime,dSell) BETWEEN @DAYSTART AND @DAYEND  )
AND (sID = @sID OR @sID = 0 OR sID2 = @sID) AND(shop_id = @shop_id OR @shop_id = 0) AND(sEmp = @emp_id OR @emp_id = 0) 
";

            if (search.dStart <= oldDate)
            {
                subSQL += @"
UNION 

select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,A.dayCancal, B.nPrice , B.nCost, B.nNumber , B.nProduct
from JabjaiSchoolHistory.[dbo].[TSell_Backup] A 
LEFT JOIN JabjaiSchoolHistory.[dbo].[TSell_Detail_Backup] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
WHERE A.SchoolID = @SchoolID and (dayCancal BETWEEN @DAYSTART AND @DAYEND OR ISNULL(UpdatedTime,dSell) BETWEEN @DAYSTART AND @DAYEND  )
AND (sID = @sID OR @sID = 0 OR sID2 = @sID) AND(shop_id = @shop_id OR @shop_id = 0) AND(sEmp = @emp_id OR @emp_id = 0) 

";
            }

            SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @sort_type INT = {6};
DECLARE @FORMAT VARCHAR(20) = '{7}';

SELECT A0.TimeStart AS 'values',CONVERT(DATETIME,FORMAT(A0.TimeStart, @FORMAT),103) AS dSell,
SUM(A0.price) AS 'price',
SUM(A0.cost) AS 'cost',
SUM(A0.deduct) 'deduct' ,
@sort_type AS 'sort_type',A0.TimeStart
FROM(
      SELECT TT1.SchoolID,  A.shop_id,
     SUM(ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 )  AS 'price'  ,
     SUM(ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 ) *0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct',
    SUM(ISNULL(ISNULL(A.nCost, C.nCost), 0) * ISNULL(A.nNumber, 1) - case WHEN ISNULL(A.UpdatedTime,A.dSell)  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nCost, C.nCost), 0) * ISNULL(A.nNumber, 1) END)  AS 'cost',
	
	TT1.TimeStart  
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('[{8}]')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN (
       {9}
    ) AS A ON A.SchoolID = TT1.SchoolID AND (dayCancal BETWEEN TT1.TimeStart AND TT1.TimeEnd OR ISNULL(UpdatedTime,dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  )
    --LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] WHERE SchoolID = @SCHOOLID AND ISNULL(cDel,'0') = '0'  ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID 
    LEFT OUTER JOIN (SELECT * FROM TProduct wHERE SchoolID = @SchoolID) AS C ON A.nProduct = C.nProductID 
    LEFT OUTER JOIN (select * from TType wHERE SchoolID = @SchoolID) AS D ON C.nType = D.nTypeID 
	LEFT OUTER JOIN (select * from TShop wHERE SchoolID = @SchoolID) AS E ON E.shop_id = A.shop_id 
	GROUP BY TT1.SchoolID,TT1.TimeStart,A.shop_id

) AS A0

GROUP BY A0.TimeStart
ORDER BY A0.TimeStart "
, SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, search.sort_type, FORMAT, JsonDATE, subSQL);

            q.AddRange(db.Database.SqlQuery<views01>(SQL).ToList());

            return q;

        }

        private static List<views01> GetReport_GroupYear(Search search, int SchoolID, JabJaiEntities db, string FORMAT, string JsonDATE)
        {
            var q = new List<views01>();
            string subSQL = "";
            string SQL = @"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @sort_type INT = {6};
DECLARE @FORMAT VARCHAR(20) = '{7}';

SELECT A0.TimeStart AS 'values',CONVERT(DATETIME,FORMAT(A0.TimeStart, @FORMAT),103) AS dSell,
SUM(A0.price) AS 'price',
SUM(A0.cost) AS 'cost',
SUM(A0.deduct) 'deduct' ,
@sort_type AS 'sort_type',A0.TimeStart
FROM
(
    SELECT TT1.SchoolID,  A.shop_id,
     SUM(ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 )  AS 'price'  ,
     SUM(ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 ) *0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct',
    SUM(ISNULL(ISNULL(A.nCost, C.nCost), 0) * ISNULL(A.nNumber, 1)) AS 'cost', TT1.TimeStart  
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('[{8}]')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN (
       
        {9}

    ) AS A ON A.SchoolID = TT1.SchoolID AND (dayCancal BETWEEN TT1.TimeStart AND TT1.TimeEnd OR ISNULL(UpdatedTime,dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd )
    --LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] WHERE SchoolID = @SCHOOLID  AND ISNULL(cDel,'0') = '0') AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID 
    LEFT OUTER JOIN (SELECT * FROM TProduct wHERE SchoolID = @SchoolID) AS C ON A.nProduct = C.nProductID 
    LEFT OUTER JOIN (select * from TType wHERE SchoolID = @SchoolID) AS D ON C.nType = D.nTypeID 
	LEFT OUTER JOIN (select * from TShop wHERE SchoolID = @SchoolID) AS E ON E.shop_id = A.shop_id 
	GROUP BY TT1.SchoolID,TT1.TimeStart,A.shop_id
 
) AS A0

GROUP BY A0.TimeStart
ORDER BY A0.TimeStart ";
            var oldDate = new DateTime(DateTime.Now.Year, 05, 01);

            subSQL = @"

 select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,dayCancal,
--  case when  CONVERT(date,dayCancal) <> CONVERT( date, ISNULL(UpdatedTime,dSell) ) THEN null else dayCancal END  'dayCancal',
	B.nPrice , B.nCost, B.nNumber, B.nProduct

from 	[JabjaiSchoolSingleDB].[dbo].[TSell] A
LEFT JOIN [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
WHERE A.SchoolID = @SchoolID and (dayCancal BETWEEN @DAYSTART AND @DAYEND OR ISNULL(UpdatedTime,dSell) BETWEEN @DAYSTART AND @DAYEND )
AND (sID = @sID OR @sID = 0 OR sID2 = @sID) AND(shop_id = @shop_id OR @shop_id = 0) AND(sEmp = @emp_id OR @emp_id = 0)

";


            subSQL = String.Format(SQL
, SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, search.sort_type, FORMAT, JsonDATE
, subSQL
);
            q.AddRange(db.Database.SqlQuery<views01>(subSQL).ToList());


            subSQL = @"

select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,dayCancal,
--  case when  CONVERT(date,dayCancal) <> CONVERT( date, ISNULL(UpdatedTime,dSell) ) THEN null else dayCancal END  'dayCancal',
	B.nPrice , B.nCost, B.nNumber, B.nProduct

from 	JabjaiSchoolHistory.[dbo].[TSell] A
LEFT JOIN JabjaiSchoolHistory.[dbo].[TSell_Detail] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
WHERE A.SchoolID = @SchoolID and (dayCancal BETWEEN @DAYSTART AND @DAYEND OR ISNULL(UpdatedTime,dSell) BETWEEN @DAYSTART AND @DAYEND )
AND (sID = @sID OR @sID = 0 OR sID2 = @sID) AND(shop_id = @shop_id OR @shop_id = 0) AND(sEmp = @emp_id OR @emp_id = 0)

";


            subSQL = String.Format(SQL
, SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, search.sort_type, FORMAT, JsonDATE
, subSQL
);
            q.AddRange(db.Database.SqlQuery<views01>(subSQL).ToList());


            if (search.dStart <= oldDate)
            {
                subSQL = @" 

select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,dayCancal,
--  case when  CONVERT(date,dayCancal) <> CONVERT( date, ISNULL(UpdatedTime,dSell) ) THEN null else dayCancal END  'dayCancal',
	B.nPrice , B.nCost, B.nNumber, B.nProduct

from 	JabjaiSchoolHistory.[dbo].[TSell_Backup] A
LEFT JOIN JabjaiSchoolHistory.[dbo].[TSell_Detail_Backup] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
WHERE A.SchoolID = @SchoolID and (dayCancal BETWEEN @DAYSTART AND @DAYEND OR ISNULL(UpdatedTime,dSell) BETWEEN @DAYSTART AND @DAYEND )
AND (sID = @sID OR @sID = 0 OR sID2 = @sID) AND(shop_id = @shop_id OR @shop_id = 0) AND(sEmp = @emp_id OR @emp_id = 0)
";

                subSQL = String.Format(SQL
, SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, search.sort_type, FORMAT, JsonDATE
, subSQL
);
                q.AddRange(db.Database.SqlQuery<views01>(subSQL).ToList());

            }

            return q;
        }

        private static List<views01> GetReport_GroupMonth(Search search, int SchoolID, JabJaiEntities db, string FORMAT, string JsonDATE)
        {
            var q = new List<views01>();

            string SQL = @"
DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @sort_type INT = {6};
DECLARE @FORMAT VARCHAR(20) = '{7}';

-- ลบ Temp Table ถ้ามีอยู่
IF OBJECT_ID('tempdb..#TempSalesData') IS NOT NULL
BEGIN
    DROP TABLE #TempSalesData;
END

IF OBJECT_ID('tempdb..#DateRange') IS NOT NULL
BEGIN
    DROP TABLE #DateRange;
END

-- สร้าง Temp Table
CREATE TABLE #TempSalesData (
    shop_id INT,
    sSellID INT,
    SchoolID INT,
    nTotal DECIMAL(18, 2),
    UpdatedTime DATETIME,
    dSell DATETIME,
    dayCancal DATETIME,
    nPrice DECIMAL(18, 2),
    nCost DECIMAL(18, 2),
    nNumber INT,
    nProduct INT,
    CancelAmount DECIMAL(18, 2),
    SalesAmount DECIMAL(18, 2),
    CostAmount DECIMAL(18, 2),
    SaleDate DATE,
	DeductPercent TINYINT NULL
);

-- แคชข้อมูลลงใน Temp Table
INSERT INTO #TempSalesData
SELECT 
    A.shop_id,
    A.sSellID,
    A.SchoolID,
    A.nTotal,
    A.UpdatedTime,
    CONVERT(DATE ,A.dSell) AS 'dSell',
    CONVERT(DATE ,A.dayCancal) AS 'dayCancal',
    B.nPrice,
    B.nCost,
    B.nNumber,
    B.nProduct,
    CASE 
        WHEN A.dayCancal IS NOT NULL 
		THEN ISNULL(B.nPrice, A.nTotal) * ISNULL(B.nNumber, 1)
        ELSE 0
    END AS CancelAmount,
    CASE 
        WHEN B.nPrice IS NULL THEN A.nTotal
        ELSE ISNULL(B.nPrice, A.nTotal) * ISNULL(B.nNumber, 1)
    END AS SalesAmount,
    CASE 
        WHEN ISNULL(A.UpdatedTime, A.dSell) IS NULL THEN 0
        ELSE ISNULL(B.nCost, C.nCost) * ISNULL(B.nNumber, 1)
    END AS CostAmount,
    CAST(ISNULL(A.UpdatedTime, A.dSell) AS DATE) AS SaleDate,
	ISNULL(E.DeductPercent,0) AS DeductPercent
FROM [{8}].[dbo].[TSell{9}] A 
LEFT JOIN [{8}].[dbo].[TSell_Detail{9}] B 
    ON A.sSellID = B.nSell 
    AND A.SchoolID = B.SchoolID 
    AND ISNULL(B.cDel,'0') = '0'
LEFT OUTER JOIN (SELECT * FROM TProduct WHERE SchoolID = @SchoolID) AS C 
ON B.nProduct = C.nProductID 
LEFT OUTER JOIN (SELECT * FROM TType WHERE SchoolID = @SchoolID) AS D 
ON C.nType = D.nTypeID 
LEFT OUTER JOIN (SELECT * FROM TShop WHERE SchoolID = @SchoolID) AS E 
ON E.shop_id = A.shop_id 
WHERE 
    A.SchoolID = @SchoolID 
    AND (A.dayCancal BETWEEN @DAYSTART AND @DAYEND OR ISNULL(A.UpdatedTime, A.dSell) BETWEEN @DAYSTART AND @DAYEND)
    AND (A.sID = @sID OR @sID = 0 OR A.sID2 = @sID)
    AND (A.shop_id = @shop_id OR @shop_id = 0)
    AND (A.sEmp = @emp_id OR @emp_id = 0);

-- สร้าง Temp Table สำหรับเก็บวันที่
CREATE TABLE #DateRange (
    TimeStart DATE,
	TimeEnd DATE
);

-- เติมข้อมูลวันที่ลงใน Temp Table
DECLARE @CurrentDate DATETIME = @DAYSTART;

WHILE @CurrentDate <= @DAYEND
BEGIN
    INSERT INTO #DateRange (TimeStart,TimeEnd)
    VALUES (@CurrentDate,DATEADD(DAY, 1, @CurrentDate));

    SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
END;

-- สร้างรายงานรายวัน
SELECT DR.TimeStart AS 'values',DR.TimeStart AS 'dSell',
ISNULL(SUM(TSD.SalesAmount), 0) - ISNULL(SUM(TSD1.SalesAmount), 0) AS 'price',
ISNULL(SUM(TSD.CostAmount), 0) - ISNULL(SUM(TSD1.CostAmount), 0) AS 'cost',
ISNULL(SUM(TSD.DeductPercent), 0) - ISNULL(SUM(TSD1.DeductPercent), 0) AS 'deduct',
@sort_type AS 'sort_type',DR.TimeStart
FROM #DateRange DR
LEFT JOIN
(
	SELECT 
			SaleDate,SUM(SalesAmount) AS SalesAmount,
			SUM(CostAmount) AS CostAmount,SUM(DeductPercent) AS DeductPercent
	FROM 
	(
		SELECT
			SaleDate,SUM(SalesAmount) AS SalesAmount,
			SUM(CostAmount) AS CostAmount,
			SUM(SalesAmount) * 0.01 * MAX(DeductPercent) AS 'DeductPercent'
		FROM #TempSalesData 
		GROUP BY SaleDate,shop_id
	) TB1
	GROUP BY SaleDate
) AS TSD
ON DR.TimeStart = TSD.SaleDate
LEFT JOIN
(
	SELECT 
			SaleDate,SUM(SalesAmount) AS SalesAmount,
			SUM(CostAmount) AS CostAmount,SUM(DeductPercent) AS DeductPercent
	FROM 
	(
		SELECT
			dayCancal AS SaleDate,SUM(SalesAmount) AS SalesAmount,
			SUM(CostAmount) AS CostAmount,
			SUM(SalesAmount) * 0.01 * MAX(DeductPercent) AS 'DeductPercent',
			COUNT(*) AS ITEM
		FROM #TempSalesData 
		WHERE dayCancal IS NOT NULL
		GROUP BY dayCancal,shop_id
	) TB1
	GROUP BY SaleDate
) AS TSD1
ON DR.TimeStart = TSD1.SaleDate 

GROUP BY DR.TimeStart,DR.TimeEnd
ORDER BY DR.TimeStart;

-- ลบ Temp Table หลังการใช้งาน
DROP TABLE #TempSalesData;
DROP TABLE #DateRange;

";

            var oldDate = new DateTime(DateTime.Now.Year, 05, 01);

            List<IDatabases> databases = new List<IDatabases>
            {
                new IDatabases { DBName = "JabjaiSchoolSingleDB", TableName = "" },
                new IDatabases { DBName = "JabjaiSchoolHistory", TableName = "" }
            };

            if (search.dStart <= oldDate)
            {
                databases.Add(new IDatabases { DBName = "JabjaiSchoolHistory", TableName = "_Backup" });
            }

            foreach (var keyValue in databases)
            {
                string subSQL = String.Format(SQL
  , SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, search.sort_type,
  FORMAT, keyValue.DBName, keyValue.TableName);

                q.AddRange(db.Database.SqlQuery<views01>(subSQL).ToList());
            }

            var g = q.GroupBy(o => new { o.dSell })
                .Select(o => new views01
                {
                    dSell = o.Key.dSell,
                    price = o.Sum(i => i.price),
                    cost = o.Sum(i => i.cost),
                    deduct = o.Sum(i => i.deduct),
                    sort_type = search.sort_type,
                }).ToList();

            return g;

        }

        public class IDatabases
        {
            public string DBName { get; set; }
            public string TableName { get; set; }
        }

        private static List<views01> GetReport_GroupOld(Search search, int SchoolID, JabJaiEntities db, string FORMAT, string JsonDATE)
        {
            string SQL = "";
            if (search.dStart <= DateTime.Today.AddDays(-31))
            {
                if (search.dEnd >= DateTime.Today.AddDays(-3))
                {
                    SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @sort_type INT = {6};
DECLARE @FORMAT VARCHAR(20) = '{7}';

SELECT A0.TimeStart AS 'values',CONVERT(DATETIME,FORMAT(A0.TimeStart, @FORMAT),103) AS dSell,
SUM(A0.price) + SUM(A1.price) AS 'price',
SUM(A0.cost) + SUM(A1.cost)  AS 'cost',
SUM(A0.deduct) + SUM(A1.deduct)  AS 'deduct',
@sort_type AS 'sort_type',A0.TimeStart
FROM(
    SELECT TT1.SchoolID, A.shop_id ,
    SUM(ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1))  AS 'price',
    SUM(ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1)) * 0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct',
    SUM(ISNULL(ISNULL(B.nCost, C.nCost), 0) * ISNULL(B.nNumber, 1)) AS 'cost', TT1.TimeStart  
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('[{8}]')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN[JabjaiSchoolSingleDB].[dbo].[TSell] AS A ON TT1.SchoolID = A.SchoolID AND  ISNULL(A.UpdatedTime, A.dSell) BETWEEN @DAYSTART AND @DAYEND AND  ISNULL(A.UpdatedTime, A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd
	AND (A.sID = @sID OR @sID = 0 OR A.sID2 = @sID) AND(A.shop_id = @shop_id OR @shop_id = 0) AND(A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NULL
    LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID AND A.SchoolID = @SchoolID
    LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
    LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
    LEFT JOIN TShop E ON E.shop_id = A.shop_id AND E.SchoolID = A.SchoolID
    --WHERE (A.sID = @sID OR @sID = 0) AND (A.shop_id = @shop_id OR @shop_id = 0) AND (A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NULL
	GROUP BY TT1.SchoolID,TT1.TimeStart,A.shop_id

) AS A0
INNER JOIN
(
    SELECT TT1.SchoolID, A.shop_id ,
    SUM(ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1))  AS 'price',
    SUM(ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1)) * 0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct',
    SUM(ISNULL(ISNULL(B.nCost, C.nCost), 0) * ISNULL(B.nNumber, 1)) AS 'cost', TT1.TimeStart
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('[{8}]')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN [JabjaiSchoolHistory].[dbo].[TSell] AS A ON TT1.SchoolID = A.SchoolID AND ISNULL(A.UpdatedTime, A.dSell) BETWEEN @DAYSTART AND @DAYEND AND  ISNULL(A.UpdatedTime, A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd
	AND (A.sID = @sID OR @sID = 0 OR A.sID2 = @sID) AND(A.shop_id = @shop_id OR @shop_id = 0) AND(A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NULL
    LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolHistory].[dbo].[TSell_Detail] WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID AND A.SchoolID = @SchoolID
    LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
    LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
    LEFT JOIN TShop E ON E.shop_id = A.shop_id AND E.SchoolID = A.SchoolID
    --WHERE (A.sID = @sID OR @sID = 0) AND(A.shop_id = @shop_id OR @shop_id = 0) AND(A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NULL
	GROUP BY TT1.SchoolID,TT1.TimeStart,A.shop_id

) AS A1  ON A0.TimeStart = A1.TimeStart AND A1.SchoolID = A0.SchoolID

GROUP BY A0.TimeStart
ORDER BY A0.TimeStart "
 , SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, search.sort_type, FORMAT, JsonDATE);
                }
                else
                {
                    SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @sort_type INT = {6};
DECLARE @FORMAT VARCHAR(20) = '{7}';

SELECT A0.TimeStart AS 'values',CONVERT(DATETIME,FORMAT(A0.TimeStart, @FORMAT),103) AS dSell,
SUM(A0.price) AS 'price',
SUM(A0.cost) AS 'cost',
SUM(A0.deduct) 'deduct' ,
@sort_type AS 'sort_type',A0.TimeStart
FROM
(
    SELECT TT1.SchoolID, 
    SUM(ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1))  AS 'price',
	SUM(ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1)) * 0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct',
    SUM(ISNULL(ISNULL(B.nCost, C.nCost), 0) * ISNULL(B.nNumber, 1)) AS 'cost', TT1.TimeStart
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('[{8}]')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN [JabjaiSchoolHistory].[dbo].[TSell] AS A ON TT1.SchoolID = A.SchoolID AND ISNULL(A.UpdatedTime, A.dSell) BETWEEN @DAYSTART AND @DAYEND AND  ISNULL(A.UpdatedTime, A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd
	AND (A.sID = @sID OR @sID = 0 OR A.sID2 = @sID) AND(A.shop_id = @shop_id OR @shop_id = 0) AND(A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NULL
    LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolHistory].[dbo].[TSell_Detail] WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID AND A.SchoolID = @SchoolID
    LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
    LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
	LEFT JOIN TShop E ON E.shop_id = A.shop_id AND E.SchoolID = A.SchoolID
    --WHERE (A.sID = @sID OR @sID = 0) AND(A.shop_id = @shop_id OR @shop_id = 0) AND(A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NULL
	GROUP BY TT1.SchoolID,TT1.TimeStart,A.shop_id

) AS A0

GROUP BY A0.TimeStart
ORDER BY A0.TimeStart "
, SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, search.sort_type, FORMAT, JsonDATE);
                }

            }
            else
            {
                SQL = String.Format(@"DECLARE @SchoolID INT = {0};
DECLARE @DAYSTART DATETIME = '{1:d} 00:00';
DECLARE @DAYEND DATETIME = '{2:d}';
DECLARE @sID INT = {3};
DECLARE @shop_id INT = {4};
DECLARE @emp_id INT = {5};
DECLARE @sort_type INT = {6};
DECLARE @FORMAT VARCHAR(20) = '{7}';

SELECT A0.TimeStart AS 'values',CONVERT(DATETIME,FORMAT(A0.TimeStart, @FORMAT),103) AS dSell,
SUM(A0.price) AS 'price',
SUM(A0.cost) AS 'cost',
SUM(A0.deduct) 'deduct' ,
@sort_type AS 'sort_type',A0.TimeStart
FROM(
    SELECT TT1.SchoolID,  A.shop_id,
    SUM(ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1))  AS 'price',
    SUM(ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1)) * 0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct',
    SUM(ISNULL(ISNULL(B.nCost, C.nCost), 0) * ISNULL(B.nNumber, 1)) AS 'cost', TT1.TimeStart  
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('[{8}]')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN[JabjaiSchoolSingleDB].[dbo].[TSell] AS A ON TT1.SchoolID = A.SchoolID AND ISNULL(A.UpdatedTime, A.dSell) BETWEEN @DAYSTART AND @DAYEND AND  ISNULL(A.UpdatedTime, A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd
	AND (A.sID = @sID OR @sID = 0 OR A.sID2 = @sID) AND(A.shop_id = @shop_id OR @shop_id = 0) AND(A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NULL
    LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID AND A.SchoolID = @SchoolID
    LEFT OUTER JOIN TProduct AS C ON B.nProduct = C.nProductID AND A.SchoolID = C.SchoolID
    LEFT OUTER JOIN TType AS D ON C.nType = D.nTypeID AND A.SchoolID = D.SchoolID
    LEFT JOIN TShop E ON E.shop_id = A.shop_id AND E.SchoolID = A.SchoolID
    --WHERE (A.sID = @sID OR @sID = 0) AND (A.shop_id = @shop_id OR @shop_id = 0) AND (A.sEmp = @emp_id OR @emp_id = 0) AND A.dayCancal IS NULL
	GROUP BY TT1.SchoolID,TT1.TimeStart,A.shop_id

) AS A0

GROUP BY A0.TimeStart
ORDER BY A0.TimeStart "
  , SchoolID, search.dStart, search.dEnd, search.user_id ?? 0, search.shop_id ?? 0, search.emp_id ?? 0, search.sort_type, FORMAT, JsonDATE);
            }

            return db.Database.SqlQuery<views01>(SQL).ToList();

        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object CancelBuyProduct(int sell_id)
        {
            Balance _balance = new Balance();
            var userData = GetUserData();
            int user_id = userData.UserID;
            //AWSXRayRecorder recorder = AWSXRayRecorder.Instance;
            //recorder.AddAnnotation("Method", "Payment Cancel");
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var f_masterUser = dbmaster.TUsers.FirstOrDefault(f => f.sID == user_id);
                var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.nCompany == f_masterUser.nCompany);
                _balance.SchoolID = f_company.nCompany;
                int schoolid = (int)f_company.nCompany;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(f_company, ConnectionDB.Read)))
                {
                    var f_sell = dbschool.TSells.FirstOrDefault(f => f.SchoolID == schoolid && f.sSellID == sell_id && f.dayCancal == null);
                    dynamic rss = new JObject();
                    if (f_sell == null)
                    {
                        return "Not Found";
                    }
                    else
                    {
                        decimal? TotalMoney = 0;
                        var f_buyUser = new MasterEntity.TUser();

                        f_sell.dayCancal = DateTime.Now;
                        f_sell.UserCancel = f_masterUser.nSystemID;

                        if (f_sell.CardID.HasValue)
                        {
                            var backupCardHistory = dbschool.TBackupCardHistories.FirstOrDefault(f => f.CardHistoryID == f_sell.CardID && !f.ReturnDate.HasValue);

                            if (backupCardHistory != null)
                            {
                                var backupCard = dbschool.TBackupCards.FirstOrDefault(f => f.CardID == backupCardHistory.CardID);
                                backupCard.Money += f_sell.nTotal;
                                dbschool.SaveChanges();

                                var thread = new Thread(() =>
                                {
                                    TopupTempCard topupTempCard = new TopupTempCard();
                                    topupTempCard.init(new TopupTempCard
                                    {
                                        CardID = backupCard.CardID,
                                        Money = backupCard.Money ?? 0,
                                        SchoolID = userData.CompanyID,
                                        BarCode = backupCard.BarCode,
                                        CardName = backupCard.CardName,
                                        NFC = backupCard.NFC,
                                        NFCEncrypt = backupCard.NFCEncrypt,
                                        UserID = backupCardHistory.UserID ?? 0,
                                        UserType = backupCardHistory.UserType ?? 0
                                    }, userData.AuthKey, userData.AuthValue);
                                });
                                //var thread = new Thread(() => UpdataBalance(_balance));
                                thread.IsBackground = true;
                                thread.SetApartmentState(ApartmentState.STA);
                                thread.Start();
                                return "success";
                            }
                            else
                            {
                                return "fail";
                            }
                        }
                        else if (!f_sell.sID.HasValue && !f_sell.sID2.HasValue && !f_sell.CardID.HasValue)
                        {
                            dbschool.SaveChanges();
                            return "success";
                        }
                        else
                        {
                            if (f_sell.sID.HasValue)
                            {
                                var f_student = dbschool.TUser.FirstOrDefault(w => w.SchoolID == schoolid && w.sID == f_sell.sID);
                                f_student.nMoney += f_sell.nTotal;
                                TotalMoney = f_student.nMoney;
                                f_buyUser = dbmaster.TUsers.FirstOrDefault(f => f.nSystemID == f_sell.sID && f.cType == "0" && f.nCompany == f_company.nCompany);
                                _balance.sID = f_student.sID;
                                _balance.sEmp = null;
                                _balance.nMoney = f_student.nMoney;
                            }
                            else
                            {
                                var f_employess = dbschool.TEmployees.FirstOrDefault(w => w.SchoolID == schoolid && w.sEmp == f_sell.sID2);
                                f_employess.nMoney += f_sell.nTotal;
                                TotalMoney = f_employess.nMoney;
                                f_buyUser = dbmaster.TUsers.FirstOrDefault(f => f.nSystemID == f_sell.sID2 && f.cType == "1" && f.nCompany == f_company.nCompany);
                                _balance.sID = null;
                                _balance.sEmp = f_employess.sEmp;
                                _balance.nMoney = f_employess.nMoney;
                            }

                            dbschool.SaveChanges();

                            int nMessageID = messagebox.insert_message(
                                new messagebox.MessageBox
                                {
                                    message_type = messagebox.BuyProduct,
                                    send_time = DateTime.Now,
                                    message_type_id = f_sell.sSellID,
                                    message = f_sell.nTotal + "," + TotalMoney + ",status=cancal",
                                    school_id = f_company.nCompany,
                                    user_messagebox = new List<messagebox.user_messagebox>()
                                    {
                                    new messagebox.user_messagebox
                                    {
                                        user_id = f_buyUser.sID
                                    }
                                    },
                                });

                            HostingEnvironment.QueueBackgroundWorkItem(ct =>
                            {
                                var _notification = new urbanairship.notification();
                                _notification.message = string.Format(messagebox.m_BuyItemCancel, f_sell.nTotal, DateTime.Today.ToString("dd/MM/yyyy", new CultureInfo("en-us")), TotalMoney, f_company.sPhoneOne);
                                _notification.user = f_buyUser.sID.ToString();
                                _notification.title = f_buyUser.sName + " " + f_buyUser.sLastname;

                                _notification.action = "vnd.jabjai.jabjaiapp://deeplink/buy_product?message_id=" + nMessageID + "&school_id=" + f_company.nCompany;

                                bool SendStatus = false;
                                //do
                                //{
                                //    SendStatus = pushdata.pushShop(_notification);
                                //    //if (SendCount < 5) SendCount += 1;
                                //    //else SendStatus = true;
                                //} while (SendStatus);
                            });

                            if (!string.IsNullOrEmpty(f_company.PaymentAPIUrl))
                            {
                                HostingEnvironment.QueueBackgroundWorkItem(ct => UpdataBalance(_balance, userData.AuthKey, userData.AuthValue));
                            }
                            return "success";
                        }
                    }
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object CancelBuyProduct_v2(int sell_id)
        {
            Balance _balance = new Balance();
            var userData = GetUserData();
            int user_id = userData.UserID;
            var aPI_Reuslt = new API_Reuslt();

            try
            {
                Request_API request = new Request_API(API_Type.Payment_API);
                aPI_Reuslt = request.POST(null, $"/api/shop/pos/cancelpossales?SchoolID={userData.CompanyID}&sSellID={sell_id}&EmployeeID={userData.UserID}", "Cancel Pos Sales", userData.CompanyID, userData.AuthKey, userData.AuthValue);
            }
            catch (Exception ex)
            {
                aPI_Reuslt.StatusCode = 500;
                aPI_Reuslt.resultDes = ex.Message;
            }

            return aPI_Reuslt;
        }

        private static void UpdataBalance(Balance balance, string AuthKey, string AuthValue)
        {
            string data = fcommon.EntityToJson(balance);
            var result = fcommon.send_req(ConfigurationManager.AppSettings["PaymentApi"].ToString() + "/api/shop/payment/topupmoney", fcommon.MethodPost, data, null, AuthKey, AuthValue);
            //var result = fcommon.send_req("https://shopapi-test.schoolbright.co/api/shop/payment/topupmoney", fcommon.MethodPost, data, null);
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
            public decimal? deduct { get; set; }
            public decimal? lucre
            {
                get
                {
                    return price - cost;
                }
            }
            public int sort_type { get; set; }

            public string lable { get; set; }
            //{
            //    get
            //    {
            //        return dSell.Value.ToString(sort_type == 0 ? "dddd" : "dd MMM yy", new CultureInfo("th-th"));
            //    }
            //}
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
                    return (UpdatedTime ?? dSell)?.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH"));
                }
            }
            public decimal? nTotal { get; set; }
            public string shop_name { get; set; }
            public int sell_Id { get; set; }
            public string id { get; set; }
            public int userType { get; set; }
            public DateTime? dSell { get; set; }
            public DateTime? UpdatedTime { get; set; }
            public List<product> products { get; set; }

            public decimal? price { get; set; }
            public decimal? cost { get; set; }
            public string barcode { get; set; }
            public string product_name { get; set; }
            public string product_type { get; set; }
            public string cancle_status { get; set; }
            public int? amount { get; set; }
            public string cancle_name { get; set; }
            public DateTime? dayCancal { get; set; }
            public int? PaymentType { get; set; }
            public byte? DeductPercent { get; set; }

            public string remark
            {
                get
                {
                    var txt = "";

                    if ((UpdatedTime - dSell)?.TotalSeconds > 3)
                    {
                        txt += "ขายโหมดออฟไลน์ " + dSell?.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")) + "$รายการออฟไลน์จะนับรวมยอดในวันที่ระบบออนไลน์$";
                    }

                    if (cancle_status == "Y")
                    {
                        txt += "ยกเลิก " + dayCancal?.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")) + "$" + cancle_name + "$รายการยกเลิกจะนับยอดในวันที่กระทำการยกเลิก$";
                    }

                    return txt;
                }
            }
            public string DeviceID { get; set; }
            public string CLASSNAME { get; set; }
            public int IsSum { get; internal set; }
            public bool IsEqualCancleDay { get; internal set; }
            public bool IsEqualSynDay { get; internal set; }
            public bool IsOffline { get; internal set; }
            public bool IsEqualSearchDay { get; internal set; }
            public string ApplicationName { get; internal set; }

            public string KTType { get; internal set; }
            public string RefPromptPay { get; internal set; }
            public string RemarkPromptPay { get; internal set; }
            public DateTime? CreateDatePromptPay { get; internal set; }
            public string BuyPromptPay { get; internal set; }
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
            public decimal? deduct { get; internal set; }
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
            public int? payment_type { get; internal set; }
            public decimal? deduct { get; internal set; }
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