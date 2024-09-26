using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Web.Script.Services;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Validation;
using System.Web.WebPages.Html;
using AccountingDB;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using Newtonsoft.Json;
using Amazon.Runtime.Internal.Util;
using Amazon.Runtime.Internal.Transform;
using static FingerprintPayment.Card.PermissionCard.UCDialog;
using FingerprintPayment.Class;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Web.Services;
using System.Text;
using static FingerprintPayment.StockList;
using System.Web.UI.DataVisualization.Charting;
using FingerprintPayment.Helper;
using System.Data.Linq.Mapping;
using Microsoft.Data.OData.Metadata;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Microsoft.Ajax.Utilities;
using static JabjaiMainClass.StockModel;

namespace FingerprintPayment
{
    public partial class StockCard : System.Web.UI.Page
    {
        public class FormSearchData
        {
            public string ShopID { get; set; }
            public int ProductID { get; set; }
            public DateTime Date1 { get; set; }
            public DateTime Date2 { get; set; }
        }

        public List<SelectListItem> ShopList { get; set; } = new List<SelectListItem>();

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            //int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(HttpContext.Current.Request.QueryString["shop_id"]));
            //ProductAutocomplete.ShopID = HttpContext.Current.Request.QueryString["shop_id"] + "";

            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
            {
                ShopList = dbschool.TShops.Where(o => o.SchoolID == userData.CompanyID && o.cDel != true)
                     .Select(o => new { o.shop_id, o.shop_name, })
                    .AsEnumerable()
                    .Select(o => new SelectListItem
                    {
                        Text = o.shop_name,
                        Value = EncryptMD5.UrlTokenEncode(o.shop_id),
                    })
                    .ToList();
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object OnSearchReport(FormSearchData model)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var shopid = EncryptMD5.UrlTokenDecode(model.ShopID).ToNumber<int>();

                var result = GetStockCardData(model, userData, dbschool, shopid);

                return new
                {
                    data = result.Where( o => o.Index.HasValue).Select(o => new
                    {
                        index = o.Index,
                        date = o.Date?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                        doc = o.Doc,
                        type = o.Type,
                        cost = o.Cost?.ToString("#,0.00"),
                        amountIn = o.AmountIn?.ToString("#,0"),
                        totalIn = o.TotalIn?.ToString("#,0.00"),

                        amountOut = o.AmountOut?.ToString("#,0"),
                        totalOut = o.TotalOut?.ToString("#,0.00"),

                        costAvg = o.CostAvg?.ToString("#,0.00"),
                        // costAvg = TruncateDecimal((double)(o.CostAvg ?? 0), 2).ToString("#,0.00"),
                        amountNet = o.AmountNet?.ToString("#,0"),
                        totalNet = o.TotalNet?.ToString("#,0.00"),
                    }),

                    summary = new
                    {
                        amountIn = result.Sum(o => o.AmountIn)?.ToString("#,0"),
                        totalIn = result.Sum(o => o.TotalIn)?.ToString("#,0.00"),

                        amountOut = result.Sum(o => o.AmountOut)?.ToString("#,0"),
                        totalOut = result.Sum(o => o.TotalOut)?.ToString("#,0.00"),

                        costNet = result.LastOrDefault()?.CostAvg?.ToString("#,0.00"),
                        amountNet = result.LastOrDefault()?.AmountNet?.ToString("#,0"),
                        totalNet = result.LastOrDefault()?.TotalNet?.ToString("#,0.00"),
                    }
                };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportPdf(FormSearchData model)
        {
            if (!FontFactory.IsRegistered("thsarabun1"))
            {
                var path = HttpContext.Current.Server.MapPath("~/Fonts/THSarabun.ttf");
                FontFactory.Register(path, "thsarabun1");
            }

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var shopid = EncryptMD5.UrlTokenDecode(model.ShopID).ToNumber<int>();
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {

                var company = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID)
                    .Select(o => o.sCompany)
                    .FirstOrDefault();
                var shop = dbschool.TShops.FirstOrDefault(o => o.shop_id == shopid && o.SchoolID == userData.CompanyID);
                var product = dbschool.TProducts.FirstOrDefault(o => o.nProductID == model.ProductID && o.SchoolID == userData.CompanyID);

                var result = GetStockCardData(model, userData, dbschool, shopid);

                string filename = $"รายงานสต็อกการ์ด_{DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))}.pdf";

                byte[] docfinal = null;

                Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();

                    using (MemoryStream ms1 = new MemoryStream())
                    {
                        Document doc1 = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                        using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                        {
                            var font = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                            writer1.CloseStream = false;

                            doc1.Open();

                            PdfPTable table1 = new PdfPTable(10);
                            table1.WidthPercentage = 99f;
                            table1.DefaultCell.Border = Rectangle.NO_BORDER;
                            var lstWidth = new List<float>(new float[] { 6, 10, 6, 10, 6, 10, 6, 10, 7, 20 });
                            table1.SetTotalWidth(lstWidth.ToArray());
                            table1.AddCell(new PdfPCell().SetCellPDF(font, colspan: 10, text: company, border: Rectangle.NO_BORDER, fontSize: 18, fontStyle: Font.BOLD));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, colspan: 10, text: $"รายงานสต็อกการ์ด", border: Rectangle.NO_BORDER, fontSize: 16, fontStyle: Font.BOLD));

                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: $" ", border: Rectangle.NO_BORDER, colspan: 10));

                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"ร้านค้า : ", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT, fontStyle: Font.BOLD));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: shop.shop_name, border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"ชื่อสินค้า :", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT, fontStyle: Font.BOLD));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: product.sProduct, border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"วันที่เริ่ม :", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT, fontStyle: Font.BOLD));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: model.Date1.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"วันที่สิ้นสุด :", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT, fontStyle: Font.BOLD));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: model.Date2.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));

                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"พิมพ์เมือวันที่ :", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT, fontStyle: Font.BOLD));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: DateTime.Now.ToString("dd/MM/yyyy เวลา HH:mm", new CultureInfo("th-TH")), border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));

                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: $" ", border: Rectangle.NO_BORDER, colspan: 10));

                            doc1.Add(table1);

                            PdfPTable table2 = new PdfPTable(12);
                            table2.HeaderRows = 2;
                            table2.WidthPercentage = 99f;
                            var lstWidth2 = new List<float>(new float[] { 5, 10, 10, 10, 10, 8, 8, 8, 8, 8, 8, 8 });
                            table2.SetTotalWidth(lstWidth2.ToArray());
                            var c = new PdfPCell().SetCellPDF(font, text: "ลำดับ", paddingBottom: 4, rowspan: 2);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);
                            c = new PdfPCell().SetCellPDF(font, text: "วันที่", paddingBottom: 4, rowspan: 2);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);
                            c = new PdfPCell().SetCellPDF(font, text: "รหัสเอกสาร", paddingBottom: 4, rowspan: 2);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);
                            c = new PdfPCell().SetCellPDF(font, text: "ประเภทรายการ", paddingBottom: 4, rowspan: 2);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);
                            c = new PdfPCell().SetCellPDF(font, text: "ต้นทุน", paddingBottom: 4, rowspan: 2);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: "เข้า", colspan: 2);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 0f;
                            c.BorderWidthLeft = 1f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);
                            c = new PdfPCell().SetCellPDF(font, text: "ออก", colspan: 2);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 0f;
                            c.BorderWidthLeft = 1f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);
                            c = new PdfPCell().SetCellPDF(font, text: "คงเหลือ", colspan: 3);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 0f;
                            c.BorderWidthLeft = 1f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: "จำนวน", paddingBottom: 4);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 1f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: "ต้นทุนรวม", paddingBottom: 4);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: "จำนวน", paddingBottom: 4);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 1f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: "ต้นทุนรวม", paddingBottom: 4);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: "ต้นทุนเฉลี่ย", paddingBottom: 4);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 1f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: "จำนวน", paddingBottom: 4);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: "ต้นทุนรวม", paddingBottom: 4);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            // table2.AddCell(new PdfPCell().SetCellPDF(font, text: $" ", colspan: 12, border: Rectangle.NO_BORDER));

                            foreach (var d in result.Where(o => o.Index.HasValue))
                            {
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Index}", border: Rectangle.NO_BORDER));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Date?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))}", border: Rectangle.NO_BORDER));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Doc}", border: Rectangle.NO_BORDER));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Type}", border: Rectangle.NO_BORDER));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Cost?.ToString("#,0.00")}", border: Rectangle.NO_BORDER));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.AmountIn?.ToString("#,0")}", border: Rectangle.NO_BORDER));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.TotalIn?.ToString("#,0.00")}", border: Rectangle.NO_BORDER));

                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.AmountOut?.ToString("#,0")}", border: Rectangle.NO_BORDER));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.TotalOut?.ToString("#,0.00")}", border: Rectangle.NO_BORDER));

                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.CostAvg?.ToString("#,0.00")}", border: Rectangle.NO_BORDER));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.AmountNet?.ToString("#,0")}", border: Rectangle.NO_BORDER));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.TotalNet?.ToString("#,0.00")}", border: Rectangle.NO_BORDER));
                            }

                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: $" ", colspan: 12, border: Rectangle.NO_BORDER));
                            //c = new PdfPCell().SetCellPDF(font, text: "");
                            //c.BorderWidthTop = 1f;
                            //c.BorderWidthBottom = 1f;
                            //c.BorderWidthLeft = 0f;
                            //c.BorderWidthRight = 0f;
                            //table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: "รวม", fontStyle: Font.BOLD, paddingBottom: 5, colspan: 5, horizotal: Element.ALIGN_RIGHT);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 0f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: result.Sum(o => o.AmountIn)?.ToString("#,0"), paddingBottom: 5);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 0f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: result.Sum(o => o.TotalIn)?.ToString("#,0.00"), paddingBottom: 5);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 0f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: result.Sum(o => o.AmountOut)?.ToString("#,0"), paddingBottom: 5);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 0f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: result.Sum(o => o.TotalOut)?.ToString("#,0.00"), paddingBottom: 5);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 0f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: " ", paddingBottom: 5, colspan: 3);
                            c.BorderWidthTop = 1f;
                            c.BorderWidthBottom = 0f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: "มูลค่าสินค้าคงเหลือ", fontStyle: Font.BOLD, paddingBottom: 5, colspan: 9, horizotal: Element.ALIGN_RIGHT);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: result.LastOrDefault()?.CostAvg?.ToString("#,0.00"), paddingBottom: 5);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: result.LastOrDefault()?.AmountNet?.ToString("#,0"), paddingBottom: 5);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            c = new PdfPCell().SetCellPDF(font, text: result.LastOrDefault()?.TotalNet?.ToString("#,0.00"), paddingBottom: 5);
                            c.BorderWidthTop = 0f;
                            c.BorderWidthBottom = 1f;
                            c.BorderWidthLeft = 0f;
                            c.BorderWidthRight = 0f;
                            table2.AddCell(c);

                            doc1.Add(table2);

                            doc1.Close();
                        }

                        ms1.Position = 0;
                        var x = new PdfReader(ms1);
                        copy.AddDocument(x);
                        ms1.Dispose();
                    }

                    copy.Close();
                    doc.Close();

                    docfinal = finalStream.ToArray();
                }

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.BinaryWrite(docfinal);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
            }
        }

        private static List<StockCardResultModel> GetStockCardData(FormSearchData model, JWTToken.userData userData, JabJaiEntities dbschool, int? shopid)
        {
            var logic = new StockRepository(dbschool);
            //   var shopid = EncryptMD5.UrlTokenDecode(model.ShopID).ToNumber<int>();
            var dataList = logic.GetStockModelByDate(userData.CompanyID, shopid, model.ProductID, model.Date1, model.Date2);
            var result = new List<StockCardResultModel>();

            var c = 1;
            decimal? avgCost = null;

            var opening = logic.CalcAvgCostPrevDay(userData.CompanyID, shopid.Value, model.ProductID, model.Date1, model.Date2);

            result.Add(new StockCardResultModel
            {
                AmountIn = opening?.TotalQty,
                AmountNet = opening?.TotalQty,
                TotalIn = opening?.TotalCost,
                TotalNet = opening?.TotalCost,
                CostAvg = ((opening?.TotalQty ?? 0) == 0 ? 0 : Math.Round((opening.TotalCost ?? 0) / (decimal)opening.TotalQty, 2)),
            });

            foreach (var _d in dataList)
            {
                //int? qty = 0;
                //decimal? total = 0;

                var inData = new StockCardResultModel();

                inData.Index = c++;
                inData.Date = _d.Date;
                //r.Cost = avgCost ?? i.Cost;

                switch (_d.Ref)
                {
                    case 1:
                        inData.Type = "รับ";
                        inData.Doc = "R" + _d.Doc;
                        inData.Cost = _d.Cost;
                        inData.AmountIn = _d.Qty;
                        inData.TotalIn = _d.Total;

                        inData.AmountNet = result.Sum(o => o.AmountIn) - result.Sum(o => o.AmountOut) + inData.AmountIn;
                        inData.TotalNet = result.Sum(o => o.TotalIn) - result.Sum(o => o.TotalOut) + inData.TotalIn;
                        inData.CostAvg = (inData.AmountNet == 0 ? 0 : Math.Round((inData.TotalNet ?? 0) / (decimal)inData.AmountNet, 2));
                        avgCost = inData.CostAvg;

                        //qty = r.AmountIn;
                        //total = r.TotalIn;

                        break;

                    case 2:
                        if (!avgCost.HasValue)
                        {
                            avgCost = (opening.TotalQty == 0 ? 0 : Math.Round((opening.TotalCost ?? 0) / (decimal)opening.TotalQty, 2));
                        }

                        inData.Type = "ขาย";
                        inData.Doc = _d.Doc;
                        inData.Cost = avgCost;
                        inData.AmountOut = _d.Qty;
                        inData.TotalOut = inData.AmountOut * avgCost;

                        inData.AmountNet = result.Sum(o => o.AmountIn) - result.Sum(o => o.AmountOut) - inData.AmountOut;
                        inData.TotalNet = result.Sum(o => o.TotalIn) - result.Sum(o => o.TotalOut) - inData.TotalOut;
                        inData.CostAvg = (inData.AmountNet == 0 ? 0 : Math.Round((inData.TotalNet ?? 0) / (decimal)inData.AmountNet, 2));

                        break;

                    case 3:
                        switch (_d.Remark)
                        {
                            case "AL":
                                inData.Type = "ปรับปรุง/สูญหาย";
                                break;
                            case "AD":
                                inData.Type = "ปรับปรุง/ชำรุด";
                                break;
                            case "AU":
                                inData.Type = "ปรับปรุง/ส่งคืน";
                                break;
                            case "AO":
                                inData.Type = "ปรับปรุง/อื่นๆ";
                                break;
                            default:
                                break;
                        }
                        if (!avgCost.HasValue)
                        {
                            avgCost = (opening.TotalQty == 0 ? 0 : Math.Round((opening.TotalCost ?? 0) / (decimal)opening.TotalQty, 2));
                        }
                        inData.Doc = _d.Doc;
                        inData.Cost = avgCost;
                        inData.AmountOut = Math.Abs(_d.Qty.Value);
                        inData.TotalOut = inData.AmountOut * avgCost;

                        inData.AmountNet = result.Sum(o => o.AmountIn) - result.Sum(o => o.AmountOut) - inData.AmountOut;
                        inData.TotalNet = result.Sum(o => o.TotalIn) - result.Sum(o => o.TotalOut) - inData.TotalOut;
                        inData.CostAvg = (inData.AmountNet == 0 ? 0 : Math.Round((inData.TotalNet ?? 0) / (decimal)inData.AmountNet, 2));

                        break;

                    default:
                        break;
                }

                result.Add(inData);
            }

            return result;
        }

        //private static List<StockModel> GetStockModelByDate(JWTToken.userData userData, JabJaiEntities dbschool, int? shopid
        //    , int productId, DateTime date1, DateTime date2)
        //{
        //    var stockList = (from a in dbschool.TStockDetails
        //                    .Where(o => o.SchoolID == userData.CompanyID
        //                        && o.cDel != true
        //                        && o.nProductID == productId
        //                        && DbFunctions.TruncateTime(o.CreatedDate) >= date1 && DbFunctions.TruncateTime(o.CreatedDate) <= date2)
        //                     from b in dbschool.TStocks.Where(o => o.SchoolID == userData.CompanyID
        //                         && o.nStock == a.nStock
        //                         && o.shop_id == shopid)
        //                     select new
        //                     {
        //                         a.CreatedDate,
        //                         a.nNumber,
        //                         a.SumPrice,
        //                         //a.Cost,
        //                         b.DocRef,
        //                     })
        //                    .AsEnumerable()
        //                    .Select(o => new StockModel
        //                    {
        //                        Ref = 1,
        //                        Doc = o.DocRef,
        //                        Date = o.CreatedDate,
        //                        Qty = o.nNumber,
        //                        Total = o.SumPrice,
        //                        Cost = o.SumPrice / o.nNumber,
        //                    })
        //                   .ToList();

        //    var adjList = dbschool.TStockImprove
        //                  .Where(o => o.SchoolID == userData.CompanyID
        //                      && o.ProductID == productId
        //                      && DbFunctions.TruncateTime(o.Created) >= date1 && DbFunctions.TruncateTime(o.Created) <= date2)
        //                  .Select(o => new
        //                  {
        //                      o.DocRef,
        //                      o.Created,
        //                      o.Diff,
        //                      o.Remark,
        //                      o.RemarkText,
        //                  })
        //                  .AsEnumerable()
        //                  .Select(o => new StockModel
        //                  {
        //                      Ref = 3,
        //                      Doc = o.DocRef,
        //                      Date = o.Created,
        //                      Qty = o.Diff,
        //                      Remark = o.Remark,
        //                      RemarkText = o.RemarkText,
        //                  })
        //                  .ToList();

        //    var sellList = (from a in dbschool.TSells.Where(o => o.SchoolID == userData.CompanyID
        //                    && DbFunctions.TruncateTime(o.dSell) >= date1 && DbFunctions.TruncateTime(o.dSell) <= date2
        //                    && o.dayCancal == null)

        //                    from b in dbschool.TSell_Detail.Where(o => o.SchoolID == userData.CompanyID
        //                     && o.nSell == a.sSellID && o.nProduct == productId)

        //                    select new
        //                    {
        //                        a.dSell,
        //                        a.sSellID,
        //                        b.nNumber,
        //                    })
        //                  .AsEnumerable()
        //                  .Select(o => new StockModel
        //                  {
        //                      Ref = 2,
        //                      Doc = o.sSellID + "",
        //                      Date = o.dSell,
        //                      Qty = o.nNumber,
        //                  })
        //                  .ToList();

        //    var dataList = stockList.Concat(adjList).Concat(sellList)
        //                    .OrderBy(o => o.Date)
        //                    .ThenBy(o => o.Ref)
        //                    .ToList();
        //    return dataList;
        //}

        static double TruncateDecimal(double value, int decimalPlaces)
        {
            double power = Math.Pow(10, decimalPlaces);
            return Math.Truncate(value * power) / power;
        }






    }
}