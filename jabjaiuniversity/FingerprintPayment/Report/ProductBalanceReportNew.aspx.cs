using FluentDateTime;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.XRay.Recorder.Core.Sampling;
using Newtonsoft.Json;
using System.Windows.Ink;
using System.Web.UI.WebControls.Expressions;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using FingerprintPayment.Class;
using System.Net;

namespace FingerprintPayment.Report
{
    public partial class ProductBalanceReportNew : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            var userData = GetUserData();

            if (!this.IsPostBack)
            {
                using (var db = Connection.MasterEntities(ConnectionDB.Read))
                using (var _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    var _listslv = _db.TShops.Where(w => w.SchoolID == userData.CompanyID).OrderBy(o => o.shop_name);
                    foreach (var DataLV in _listslv)
                    {
                        ddlShop.Items.Add(new ListItem(DataLV.shop_name + "", DataLV.shop_id + ""));
                    }
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object GetProductType(int? ShopId)
        {
            var userData = GetUserData();

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + ""))
                return "Session Time Out";

            if (ShopId.HasValue)
            {
                using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
                {
                    var data = (from a in db.TTypes
                                where a.shop_id == ShopId && a.SchoolID == userData.CompanyID && string.IsNullOrEmpty(a.cDel)
                                orderby a.sType
                                select new
                                {
                                    TypeId = a.nTypeID,
                                    TypeName = a.sType
                                }).ToList();
                    return data;
                }
            }
            else
            {
                return null;
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object GetData(SearchData search)
        {
            var userData = GetUserData();

            var data = FetchData(search, userData);

            return new
            {
                data = data.Select((o, i) =>
                new
                {
                    No = i + 1,
                    ShopName = o.ShopName,
                    BarCode = o.BarCode,
                    o.ProductName,
                    o.Type,
                    Quantity = o.Quantity?.ToString("#,0.##"),
                    Cost = o.Cost?.ToString("#,0.##"),
                    Price = o.Price?.ToString("#,0.##"),
                    SumValue = o.SumValue?.ToString("#,0.##"),
                    Status = o.Status == "N" ? "ปกติ" : "สินค้าใกล้หมด",
                }),
                summary = new
                {
                    Balance = data.Sum(o => o.Quantity)?.ToString("#,0.##"),
                    Cost = data.Sum(o => o.Cost)?.ToString("#,0.##"),
                    Price = data.Sum(o => o.Price)?.ToString("#,0.##"),
                    SumValue = data.Sum(o => o.SumValue)?.ToString("#,0.##"),
                }
            };
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void GetExcel(SearchData search)
        {
            var userData = GetUserData();

            var d = FetchData(search, userData);
            var data = d.Select((o, i) =>
                new
                {
                    No = i + 1,
                    o.ShopName,
                    o.BarCode,
                    o.ProductName,
                    o.Type,
                    Quantity = o.Quantity?.ToString("#,0.##"),
                    Cost = o.Cost?.ToString("#,0.##"),
                    Price = o.Price?.ToString("#,0.##"),
                    SumValue = o.SumValue?.ToString("#,0.##"),
                    Status = o.Status == "N" ? "ปกติ" : "สินค้าใกล้หมด",
                });

            var summary = new
            {
                Balance = d.Sum(o => o.Quantity)?.ToString("#,0.##"),
                Cost = d.Sum(o => o.Cost)?.ToString("#,0.##"),
                Price = d.Sum(o => o.Price)?.ToString("#,0.##"),
                SumValue = d.Sum(o => o.SumValue)?.ToString("#,0.##"),
            };

            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("รายงานยอดสินค้าคงเหลือ");

                var worksheet = excel.Workbook.Worksheets["รายงานยอดสินค้าคงเหลือ"];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var row = 1;

                using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                    worksheet.Cells[row, 1, row++, 10].SetCellRange(isMerge: true, text: school.sCompany, fontSize: 16, isBold: true);
                    worksheet.Cells[row, 1, row++, 10].SetCellRange(isMerge: true, text: $"รายงานยอดสินค้าคงเหลือ ณ วันที่ {DateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"))}", fontSize: 16, isBold: true);
                }

                worksheet.Cells[row, 1].SetCellRange(isHeader: true, text: "ลำดับ/No");
                worksheet.Cells[row, 2].SetCellRange(isHeader: true, text: "ชื่อร้านค้า/Store name");
                worksheet.Cells[row, 3].SetCellRange(isHeader: true, text: "รหัสบาร์โค้ด/Barcode");
                worksheet.Cells[row, 4].SetCellRange(isHeader: true, text: "ชื่อสินค้า/Product name");
                worksheet.Cells[row, 5].SetCellRange(isHeader: true, text: "ประเภทสินค้า/Product type");
                worksheet.Cells[row, 6].SetCellRange(isHeader: true, text: "ยอดคงเหลือ(ชิ้น)/Balance (pieces)");
                worksheet.Cells[row, 7].SetCellRange(isHeader: true, text: "ราคาต้นทุน(หน่วย/บาท)/Cost price (unit/baht)");
                worksheet.Cells[row, 8].SetCellRange(isHeader: true, text: "ราคาขาย(หน่วย/บาท)/Selling price (unit/baht)");
                worksheet.Cells[row, 9].SetCellRange(isHeader: true, text: "มูลค่าคงคลัง (บาท)/Inventory value (baht)");
                worksheet.Cells[row++, 10].SetCellRange(isHeader: true, text: "สถานะ/Status");

                var no = 1;
                foreach (var r in data)
                {
                    var col = 1;
                    worksheet.Cells[row, col++].SetCellRange(text: no++ + "");
                    worksheet.Cells[row, col++].SetCellRange(text: r.ShopName + "");
                    worksheet.Cells[row, col++].SetCellRange(text: r.BarCode + "");
                    worksheet.Cells[row, col++].SetCellRange(text: r.ProductName + "");
                    worksheet.Cells[row, col++].SetCellRange(text: r.Type + "");
                    worksheet.Cells[row, col++].SetCellRange(text: r.Quantity + "");
                    worksheet.Cells[row, col++].SetCellRange(text: r.Cost + "");
                    worksheet.Cells[row, col++].SetCellRange(text: r.Price + "");
                    worksheet.Cells[row, col++].SetCellRange(text: r.SumValue + "");
                    worksheet.Cells[row, col++].SetCellRange(text: r.Status);
                    row++;
                }

                worksheet.Cells[row, 1].SetCellRange(text: "");
                worksheet.Cells[row, 2, row, 5].SetCellRange(isMerge: true, text: "ยอดสรุป" , horizotal:ExcelHorizontalAlignment.Left);
                worksheet.Cells[row, 6].SetCellRange(text: summary.Balance);
                worksheet.Cells[row, 7].SetCellRange(text: summary.Cost);
                worksheet.Cells[row, 8].SetCellRange(text: summary.Price);
                worksheet.Cells[row, 9].SetCellRange(text: summary.SumValue);

                worksheet.Column(1).AutoFit(10);
                worksheet.Column(2).AutoFit(20);
                worksheet.Column(3).AutoFit(20);
                worksheet.Column(4).AutoFit(40);
                worksheet.Column(5).AutoFit(20);
                worksheet.Column(6).AutoFit(20);
                worksheet.Column(7).AutoFit(20);
                worksheet.Column(8).AutoFit(20);
                worksheet.Column(9).AutoFit(20);
                worksheet.Column(10).AutoFit(25);
                //worksheet.Column(11).AutoFit(30);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/text;";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        private static List<RespData> FetchData(SearchData search, JWTToken.userData userData)
        {
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                string sql = $@"
SELECT B.shop_name 'ShopName' , A.sBarCode 'BarCode' , A.sProduct 'ProductName' , C.sType 'Type' ,
A.Quantity 'Quantity' , A.nCost 'Cost' , A.nPrice 'Price' , ISNULL(A.Quantity * A.nCost, 0) 'SumValue' ,
IIF(A.Quantity <= A.nWarn, 'N', 'L') 'Status' 
--, ROW_NUMBER() OVER (ORDER BY B.shop_name , A.sProduct) AS 'No'
FROM JabjaiSchoolSingleDB.dbo.V_TProduct A
LEFT JOIN JabjaiSchoolSingleDB.dbo.TShop B ON A.SchoolID = B.SchoolID AND B.shop_id = A.ShopID
LEFT JOIN JabjaiSchoolSingleDB.dbo.TType C ON A.SchoolID = C.SchoolID AND A.nType = C.nTypeID

--ProductBalanceReportNew.GetData
WHERE A.SchoolID = {userData.CompanyID} 
AND A.cDel is null
{(search.ShopId.HasValue ? " AND A.ShopID = @ShopID" : "")}
{(search.TypeId.HasValue ? " AND A.nType = @TypeID" : "")}
{(!string.IsNullOrEmpty(search.Name) ? " AND (A.sBarCode LIKE '%'+@WORDING+'%' OR A.sProduct LIKE '%'+@WORDING+'%') " : "")}
ORDER BY B.shop_name , A.sProduct
 ";

                var paras = new List<SqlParameter>();
                if (search.ShopId.HasValue)
                {
                    paras.Add(new SqlParameter("@ShopID", search.ShopId));
                }
                if (search.TypeId.HasValue)
                {
                    paras.Add(new SqlParameter("@TypeID", search.TypeId));
                }
                if (!string.IsNullOrEmpty(search.Name))
                {
                    paras.Add(new SqlParameter("@WORDING", search.Name.Trim()));
                }

                var data = db.Database.SqlQuery<RespData>(sql, paras.ToArray()).ToList();

                return data;
            }
        }

        private class RespData
        {
            public string ShopName { get; set; }
            public string BarCode { get; set; }
            public string ProductName { get; set; }
            public string Type { get; set; }
            public int? Quantity { get; set; }
            public decimal? Cost { get; set; }
            public decimal? Price { get; set; }
            public decimal? SumValue { get; set; }
            public string Status { get; set; }
            public long No { get; set; }
        }

        public class SearchData
        {

            public int? ShopId { get; set; }
            public int? TypeId { get; set; }
            public string Name { get; set; }
        }
    }
}