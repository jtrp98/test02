using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;
using System.Text;
using AccountingDB;
using FingerprintPayment.Models;
using FingerprintPayment.Report.Handles;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using FingerprintPayment.Class;

namespace FingerprintPayment
{
    public partial class StockExport : System.Web.UI.Page
    {
        public PageModel ModelData { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + ""))
                Response.Redirect("~/Default.aspx");

            var stockId = int.Parse(EncryptMD5.UrlTokenDecode(HttpContext.Current.Request.QueryString["id"]));

            ModelData = GetData(stockId, userData);
        }

        private static PageModel GetData(int stockId, JWTToken.userData userData)
        {

            using (var accountContext = new AccountingDBContext())
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
            {
                var stock = dbschool.TStocks.FirstOrDefault(o => o.nStock == stockId && o.SchoolID == userData.CompanyID);
                var shop = dbschool.TShops.FirstOrDefault(o => o.shop_id == stock.shop_id && o.SchoolID == userData.CompanyID);
                var unitList = accountContext.AccountMeasure
                      .Where(w => (w.SchoolId == userData.CompanyID || w.SchoolId == null) && w.DeleteDate == null)
                      .Select(o => new
                      {
                          o.AccountMeasureId,
                          o.NameThai
                      })
                     .ToList();

                var company = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID)
                    .Select(o => o.sCompany)
                    .FirstOrDefault();
                var contact = accountContext.AccountContact
                     .Where(o => o.SchoolId == userData.CompanyID && o.AccountGroup == "Expense" && o.AccountContactId == stock.ContactID)
                     .Select(o => o.ContactName)
                     .FirstOrDefault();

                var detailList1 = (from a in dbschool.TStockDetails.Where(o => o.nStock == stockId && o.SchoolID == userData.CompanyID)
                                   from b in dbschool.TProducts.Where(o => o.nProductID == a.nProductID && o.SchoolID == a.SchoolID).DefaultIfEmpty()
                                   select new
                                   {
                                       b.sBarCode,
                                       b.sProduct,
                                       b.UnitID,
                                       a.Cost,
                                       a.SumPrice,
                                       a.nOrder,
                                       a.nNumber,
                                   }).ToList();

                var detailList2 = (from a in dbschool.TStockDetailTemp.Where(o => o.nStock == stockId && o.SchoolID == userData.CompanyID)
                                   from b in dbschool.TProducts.Where(o => o.nProductID == a.nProductID && o.SchoolID == a.SchoolID).DefaultIfEmpty()
                                   select new
                                   {
                                       b.sBarCode,
                                       b.sProduct,
                                       b.UnitID,
                                       a.Cost,
                                       a.SumPrice,
                                       a.nOrder,
                                       a.nNumber,
                                   }).ToList();


                return new PageModel
                {
                    School = company,
                    Shop = shop,
                    Contact = contact,
                    Stock = stock,
                    DetailList = (from a in detailList1.Concat(detailList2)
                                  from b in unitList.Where(o => o.AccountMeasureId == a.UnitID).DefaultIfEmpty()
                                  select new DetailModel
                                  {
                                      Index = a.nOrder,
                                      Barcode = a.sBarCode,
                                      Product = a.sProduct,
                                      Unit = b?.NameThai,
                                      Cost = a.Cost,
                                      Total = a.SumPrice,
                                      Amount = a.nNumber,
                                  })
                                    .OrderBy(o => o.Index)
                           .ToList()
                };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportPdf(string id)
        {
            if (!FontFactory.IsRegistered("thsarabun1"))
            {
                var path = HttpContext.Current.Server.MapPath("~/Fonts/THSarabun.ttf");
                FontFactory.Register(path, "thsarabun1");
            }

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var stockId = int.Parse(EncryptMD5.UrlTokenDecode(id));

            var result = GetData(stockId, userData);

            string filename = $"รายงานนำเข้าสต็อก_{result.Stock.dStock.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))}.pdf";

            byte[] docfinal = null;

            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

            using (MemoryStream finalStream = new MemoryStream())
            {
                var copy = new PdfCopy(doc, finalStream);
                doc.Open();

                using (MemoryStream ms1 = new MemoryStream())
                {
                    Document doc1 = new Document(PageSize.A4, 20, 20, 20, 20);

                    using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                    {
                        var font = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                        writer1.CloseStream = false;

                        doc1.Open();

                        PdfPTable table1 = new PdfPTable(6);
                        table1.WidthPercentage = 99f;
                        table1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        var lstWidth = new List<float>(new float[] { 10, 20, 10, 20, 8, 15 });
                        table1.SetTotalWidth(lstWidth.ToArray());
                        table1.AddCell(new PdfPCell().SetCellPDF(font, colspan: 6, text: result.School, border: Rectangle.NO_BORDER, fontSize: 18));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, colspan: 6, text: $"รายงานนำเข้าสต็อก ร้าน {result.Shop.shop_name} วันที่ {result.Stock.dStock.ToString("dd MMMM yyyy", new CultureInfo("th-TH"))}", border: Rectangle.NO_BORDER, fontSize: 16));

                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $" ", border: Rectangle.NO_BORDER, colspan: 6));

                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"รหัสเอกสาร :", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"{result.Stock.DocRef}", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"เลขที่ INV :", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"{result.Stock.INVNo}", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"วันที่ :", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"{result.Stock.INVDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))}", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));

                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"ผู้ขาย :", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"{result.Contact}", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"เลขที่ PO :", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"{result.Stock.PONo}", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"วันที่ :", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"{result.Stock.PODate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))}", border: Rectangle.NO_BORDER, horizotal: Element.ALIGN_LEFT));

                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $" ", border: Rectangle.NO_BORDER, colspan: 6));

                        doc1.Add(table1);

                        PdfPTable table2 = new PdfPTable(7);
                        table2.HeaderRows = 1;
                        table2.WidthPercentage = 99f;
                        var lstWidth2 = new List<float>(new float[] { 8, 18, 23, 10, 10, 13, 13 });
                        table2.SetTotalWidth(lstWidth2.ToArray());
                        var c = new PdfPCell().SetCellPDF(font, text: "ลำดับ", paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);
                        c = new PdfPCell().SetCellPDF(font, text: "รหัสสินค้า", paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);
                        c = new PdfPCell().SetCellPDF(font, text: "ชื่อสินค้า", paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);
                        c = new PdfPCell().SetCellPDF(font, text: "หน่วยนับ", paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);
                        c = new PdfPCell().SetCellPDF(font, text: "ราคา/หน่วย", paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);
                        c = new PdfPCell().SetCellPDF(font, text: "จำนวน", paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);
                        c = new PdfPCell().SetCellPDF(font, text: "ราคารวมทุน", paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);

                        foreach (var d in result.DetailList)
                        {
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Index}", border: Rectangle.NO_BORDER));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Barcode}", border: Rectangle.NO_BORDER));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Product}", border: Rectangle.NO_BORDER));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Unit}", border: Rectangle.NO_BORDER));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Cost?.ToString("#,0.00")}", border: Rectangle.NO_BORDER));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Amount?.ToString("#,0")}", border: Rectangle.NO_BORDER));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: $"{d.Total?.ToString("#,0.00")}", border: Rectangle.NO_BORDER));
                        }

                        c = new PdfPCell().SetCellPDF(font, text: "");
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);

                        c = new PdfPCell().SetCellPDF(font, text: "รวม", paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);

                        c = new PdfPCell().SetCellPDF(font, text: "");
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);

                        c = new PdfPCell().SetCellPDF(font, text: "");
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);

                        c = new PdfPCell().SetCellPDF(font, text: result.DetailList.Sum(o => o.Cost)?.ToString("#,0.00"), paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);

                        c = new PdfPCell().SetCellPDF(font, text: result.DetailList.Sum(o => o.Amount)?.ToString("#,0"), paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);

                        c = new PdfPCell().SetCellPDF(font, text: result.DetailList.Sum(o => o.Total)?.ToString("#,0.00"), paddingBottom: 4);
                        c.BorderWidthTop = 1f;
                        c.BorderWidthBottom = 1f;
                        c.BorderWidthLeft = 0f;
                        c.BorderWidthRight = 0f;
                        table2.AddCell(c);

                        table2.AddCell(new PdfPCell().SetCellPDF(font, colspan: 7, text: $"พิมพ์วันที่ : {DateTime.Now.ToString("dd/MM/yyyy HH:mm น.", new CultureInfo("th-TH"))}", horizotal: Element.ALIGN_LEFT, border: Rectangle.NO_BORDER));

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

        public class ITextEvents : PdfPageEventHelper
        {
            iTextSharp.text.Font font;

            string summary1;
            TCompany school;
            public ITextEvents(TCompany school, string sum1)
            {
                this.school = school;
                summary1 = sum1;

                font = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            }
            // This is the contentbyte object of the writer  
            PdfContentByte cb;

            // we will put the final number of pages in a template  
            PdfTemplate headerTemplate, footerTemplate;



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
                    //iTextSharp.text.Font f = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 14, iTextSharp.text.Font.NORMAL);
                    //bf = BaseFont.CreateFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    //bf = font.BaseFont;
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
                    cb.SetFontAndSize(font.BaseFont, 10);

                    cb.SetTextMatrix(document.PageSize.GetLeft(30), document.PageSize.GetBottom(5));
                    var text1 = $"รายงานข้อมูล ณ วันที่ {dateNow}";
                    cb.ShowText(text1);

                    cb.SetTextMatrix(document.PageSize.GetRight(30), document.PageSize.GetBottom(5));
                    var text2 = $"{writer.PageNumber}/";
                    cb.ShowText(text2);
                    cb.EndText();
                    float len = font.BaseFont.GetWidthPoint(text2, 10);
                    // cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));document.PageSize.GetRight(20)
                    cb.AddTemplate(footerTemplate, document.PageSize.GetRight(30) + len, document.PageSize.GetBottom(5));// document.PageSize.GetBottom(20)
                }

                {
                    PdfPTable mainTable = new PdfPTable(1);
                    mainTable.TotalWidth = document.PageSize.Width - 30f;

                    mainTable.WidthPercentage = 90;
                    mainTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    PdfPTable table = new PdfPTable(1);
                    table.WidthPercentage = 100;
                    table.PaddingTop = 30;
                    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    table.AddCell(new PdfPCell().SetCellPDF(font,
                        text: school.sCompany,
                        horizotal: Element.ALIGN_CENTER,
                        fontSize: 16,
                        fontStyle: iTextSharp.text.Font.BOLD,
                        border: iTextSharp.text.Rectangle.NO_BORDER)
                    );

                    table.AddCell(new PdfPCell().SetCellPDF(font,
                        text: summary1,
                        horizotal: Element.ALIGN_CENTER,
                        fontSize: 14,
                        fontStyle: iTextSharp.text.Font.BOLD,
                        border: iTextSharp.text.Rectangle.NO_BORDER)
                    );

                    mainTable.AddCell(table);

                    mainTable.WriteSelectedRows(0, -1, 10, document.PageSize.Height, writer.DirectContent);
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
                footerTemplate.SetFontAndSize(font.BaseFont, 10);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber) + "");
                footerTemplate.EndText();
            }
        }

        public class PageModel
        {
            public string School { get; set; }
            public TShop Shop { get; set; }
            public TStock Stock { get; set; }
            public List<DetailModel> DetailList { get; set; }
            public string Contact { get; internal set; }
        }
        public class DetailModel
        {
            public int Index { get; set; }
            public string Barcode { get; set; }
            public string Product { get; set; }
            public string Unit { get; set; }
            public decimal? Cost { get; set; }
            public decimal? Total { get; set; }
            public int? Amount { get; set; }
        }
    }
}