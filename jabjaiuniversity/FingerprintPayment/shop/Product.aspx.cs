using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;
using System.Web.Script.Services;
using Newtonsoft.Json.Linq;
using System.Data.Entity;
using PagedList;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Web.Services;
using System.IO;
using FingerprintPayment.Qusetion.CsCode;
using OfficeOpenXml;
using FingerprintPayment.Class;
using AccountingDB;
using System.Globalization;
using static FingerprintPayment.Card.PermissionCard.UCDialog;
using static JabjaiMainClass.StockModel;

namespace FingerprintPayment
{
    public partial class Product : StudentGateway
    {
        public class ProductCostModel
        {
            public int ProductID { get; set; }
            public decimal? CostAvg { get; set; }
        }

        public TShop shop = new TShop();
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(HttpContext.Current.Request.QueryString["shop_id"]));
            if (!this.IsPostBack)
            {
                using (var accountContext = new AccountingDBContext())
                using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
                {
                    fcommon.LinqToDropDownList(dbschool.TTypes.Where(w => w.shop_id == shop_id && w.cDel == null && w.SchoolID == userData.CompanyID).ToList(), ddlcType, "ทั้งหมด", "nTypeID", "sType");
                    fcommon.LinqToDropDownList(dbschool.TTypes.Where(w => w.shop_id == shop_id && w.cDel == null && w.SchoolID == userData.CompanyID).ToList(), ddlnType, "", "nTypeID", "sType");
                    fcommon.LinqToDropDownList(accountContext
                        .AccountMeasure.Where(w => (w.SchoolId == userData.CompanyID || w.SchoolId == null) && w.DeleteDate == null)
                        .OrderBy(o => o.MeasureCode)
                        .ThenByDescending(o => o.UpdateDate)
                        .ToList(), ddlUnit, "", "AccountMeasureId", "NameThai");
                    shop = dbschool.TShops.FirstOrDefault(s => s.shop_id == shop_id);
                }
            }
        }

        private static string CountNumberProduct(int product_id, List<TProduct> tProduct,
            List<TStockDetail> tStockDetail, List<TSell_Detail> tSell_Detail)
        {
            int? nCount = 0;
            var q_1 = tProduct.Where(w => w.nProductID == product_id && string.IsNullOrEmpty(w.cDel)).FirstOrDefault();
            int? _ProductNume = q_1 == null ? 0 : q_1.nNumber;
            int? nStock = tStockDetail.Where(w => w.nProductID == product_id).Sum(s => s.nNumber);

            int? nSellNumber = tSell_Detail.Where(w => w.nProduct == product_id && string.IsNullOrEmpty(w.cDel)).Sum(s => s.nNumber);
            nCount = (_ProductNume + (nStock == null ? 0 : nStock)) - ((nSellNumber == null ? 0 : nSellNumber));
            return nCount.ToString();
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string returnlist_v1(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(search.shop_id));
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
                {
                    dynamic rss = new JObject();
                    int i = 1;
                    var q = (from a in dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList()
                             join b in dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.nType equals b.nTypeID
                             where (a.sBarCode.Contains(search.wording) || a.sProduct.Contains(search.wording))
                             && a.cDel == null && b.shop_id == shop_id
                             select a).ToList();

                    //var qstock_detail = dbschool.TStockDetails.Where(w => w.SchoolID == userData.CompanyID).ToList();
                    //var qsell_detail = dbschool.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID).ToList();

                    if (search.product_type != null) q = q.Where(w => w.nType == search.product_type).ToList();

                    rss.FOOT = new JObject()
                    {
                        new JProperty("pageSize",(q.Count() / search.pageSize) + (q.Count() % search.pageSize == 0?0:1))
                    };

                    search.pageSize = search.pageSize.HasValue ? search.pageSize : 20;
                    search.pageNumber = search.pageNumber.HasValue ? search.pageNumber : 1;

                    q = q.ToPagedList(search.pageNumber.Value, search.pageSize.Value).ToList();

                    var q_productId = q.Select(s => s.nProductID).ToList();
                    List<TM_PRODUCT> _PRODUCTs = new List<TM_PRODUCT>();

                    int INDEX_START = (search.pageNumber ?? 1) == 1 ? 1 : (((search.pageNumber ?? 1) - 1) * (search.pageSize ?? 20)) + 1;
                    int INDEX_END = (search.pageSize ?? 20) * (search.pageNumber ?? 1);

                    //                    string SQL = string.Format(@"DECLARE @SCHOOLID INT = {0};
                    //DECLARE @nType INT = {1};
                    //DECLARE @SHOPID INT = {2};
                    //DECLARE @Page INT = {3};
                    //DECLARE @RowsOfPage INT = {4};
                    //DECLARE @WORDING VARCHAR(250) = '{5}';

                    //SELECT CONVERT(int,ROW_NUMBER() OVER(ORDER BY AA.sBarCode,Len(AA.sBarCode))) AS ROWS_INDEX,AA.nProductID AS PRODUCT_ID,AA.sProduct AS PRODUCT_NAME,
                    //AA.sBarCode AS BARCODE,ISNULL(AA.nNumber,0)  AS nNumber,
                    //(ISNULL(AA.nNumber,0) + ISNULL(SUM(TB.COUNT_STOCK_NUMBER),0)) - (ISNULL(SUM(T.COUNT_SELL_NUMBER),0) + ISNULL(SUM(TH.COUNT_SELL_NUMBER),0)) AS QUANTITY,
                    //(ISNULL(SUM(T.COUNT_SELL_NUMBER),0) + ISNULL(SUM(TH.COUNT_SELL_NUMBER),0)) AS COUNT_SELL_NUMBER, ISNULL(SUM(TB.COUNT_STOCK_NUMBER),0) AS COUNT_STOCK_NUMBER,
                    //AA.nPrice AS PRICE,AA.nCost AS COST,
                    //TS.shop_id AS SHOP_ID,TS.shop_name AS SHOP_NAME,AA.cStock AS STOCK_STATUS,AA.nType
                    //FROM TProduct AS AA 
                    //LEFT OUTER JOIN ( 
                    //SELECT B.nProduct,SUM(B.nNumber) AS COUNT_SELL_NUMBER
                    //FROM JabjaiSchoolHistory.DBO.TSell AS A 
                    //LEFT OUTER JOIN (SELECT * FROM JabjaiSchoolHistory.DBO.TSell_Detail WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON A.sSellID = B.nSell AND A.SchoolID = @SCHOOLID
                    //WHERE A.dayCancal IS NULL AND A.SchoolID = @SCHOOLID
                    //GROUP BY B.nProduct ) TH ON TH.nProduct = AA.nProductID
                    //LEFT OUTER JOIN
                    //(SELECT B.nProduct,SUM(B.nNumber) AS COUNT_SELL_NUMBER
                    //FROM TSell AS A 
                    //INNER JOIN (SELECT * FROM TSell_Detail WHERE ISNULL(cDel,'0') = '0' AND SchoolID = @SCHOOLID ) AS B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID
                    //WHERE A.dayCancal IS NULL AND A.SchoolID = @SCHOOLID
                    //GROUP BY B.nProduct ) T on T.nProduct = AA.nProductID
                    //LEFT OUTER JOIN
                    //(SELECT B.nProductID,SUM(B.nNumber) AS COUNT_STOCK_NUMBER 
                    //FROM TStock AS A 
                    //INNER JOIN TStockDetail AS B ON A.nStock = B.nStock AND A.SchoolID = B.SchoolID
                    //WHERE A.SchoolID = @SCHOOLID
                    //GROUP BY B.nProductID ) TB ON TB.nProductID = AA.nProductID

                    //INNER JOIN TType AS TY ON AA.nType = TY.nTypeID AND AA.SchoolID = TY.SchoolID
                    //INNER JOIN TShop AS TS  ON TY.shop_id = TS.shop_id AND TY.SchoolID = TS.SchoolID
                    //WHERE (AA.sBarCode LIKE '%'+@WORDING+'%' OR AA.sProduct LIKE '%'+@WORDING+'%' ) AND AA.SchoolID = @SCHOOLID AND (AA.nType = @nType OR @nType = 0)
                    //AND TY.shop_id = @SHOPID AND AA.cDel IS NULL
                    //GROUP BY AA.nProductID,AA.sProduct,AA.sBarCode,AA.nNumber,AA.nPrice,AA.nCost,TS.shop_id,TS.shop_name,AA.cStock,AA.nType

                    //ORDER BY AA.sBarCode,Len(AA.sBarCode)

                    //OFFSET (@Page - 1) * @RowsOfPage ROWS
                    //FETCH NEXT @RowsOfPage ROWS ONLY

                    //", userData.CompanyID, search.product_type ?? 0, shop_id, search.pageNumber, search.pageSize, search.wording);

                    string SQL = string.Format(@"DECLARE @SCHOOLID INT = {0};
DECLARE @nType INT = {1};
DECLARE @SHOPID INT = {2};
DECLARE @Page INT = {3};
DECLARE @RowsOfPage INT = {4};
DECLARE @WORDING VARCHAR(250) = '{5}';

SELECT CONVERT(int,ROW_NUMBER() OVER(ORDER BY AA.sBarCode,Len(AA.sBarCode))) AS ROWS_INDEX,AA.nProductID AS PRODUCT_ID,AA.sProduct AS PRODUCT_NAME,
AA.sBarCode AS BARCODE,ISNULL(AA.nNumber,0)  AS nNumber,AA.Quantity AS QUANTITY,
AA.CountSell AS COUNT_SELL_NUMBER, 
AA.CountStock AS COUNT_STOCK_NUMBER,
AA.nPrice AS PRICE,AA.nCost AS COST,
TS.shop_id AS SHOP_ID,TS.shop_name AS SHOP_NAME,AA.cStock AS STOCK_STATUS,AA.nType
FROM V_TProduct AS AA 

INNER JOIN TType AS TY ON AA.nType = TY.nTypeID AND AA.SchoolID = TY.SchoolID
INNER JOIN TShop AS TS  ON TY.shop_id = TS.shop_id AND TY.SchoolID = TS.SchoolID
WHERE (AA.sBarCode LIKE '%'+@WORDING+'%' OR AA.sProduct LIKE '%'+@WORDING+'%' ) AND AA.SchoolID = @SCHOOLID AND (AA.nType = @nType OR @nType = 0)
AND TY.shop_id = @SHOPID AND AA.cDel IS NULL
GROUP BY AA.nProductID,AA.sProduct,AA.sBarCode,AA.nNumber,AA.Quantity,AA.CountSell,AA.CountStock,AA.nPrice,AA.nCost,TS.shop_id,TS.shop_name,AA.cStock,AA.nType

ORDER BY AA.sBarCode,Len(AA.sBarCode)

OFFSET (@Page - 1) * @RowsOfPage ROWS
FETCH NEXT @RowsOfPage ROWS ONLY

", userData.CompanyID, search.product_type ?? 0, shop_id, search.pageNumber, search.pageSize, search.wording);


                    var PRODUCT_DATA = dbschool.Database.SqlQuery<TM_PRODUCT>(SQL).ToList();

                    rss.DATA = new JArray(from a in PRODUCT_DATA
                                              //orderby a.BARCODE.Length, a.BARCODE
                                          select new JObject
                                          {
                                              new JProperty("index",a.ROWS_INDEX),
                                              new JProperty("price",a.PRICE),
                                              new JProperty("product_id",a.PRODUCT_ID),
                                              new JProperty("product_name",a.PRODUCT_NAME),
                                              new JProperty("cost",a.COST),
                                              new JProperty("barcode",a.BARCODE),
                                              new JProperty("quantity", (a.STOCK_STATUS?? "0") == "0" ?  a.QUANTITY :0)
                                          });

                    return rss.ToString();
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object returnlist(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(search.shop_id));
                using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
                {
                    //dynamic rss = new JObject();
                    //int i = 1;
                    //var q = (from a in dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList()
                    //         join b in dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.nType equals b.nTypeID
                    //         where (a.sBarCode.Contains(search.wording) || a.sProduct.Contains(search.wording))
                    //         && a.cDel == null && b.shop_id == shop_id
                    //         select a).ToList();

                    ////var qstock_detail = dbschool.TStockDetails.Where(w => w.SchoolID == userData.CompanyID).ToList();
                    ////var qsell_detail = dbschool.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID).ToList();

                    //if (search.product_type != null) 
                    //    q = q.Where(w => w.nType == search.product_type).ToList();

                    //rss.FOOT = new JObject()
                    //{
                    //    new JProperty("pageSize",(q.Count() / search.pageSize) + (q.Count() % search.pageSize == 0?0:1))
                    //};

                    search.pageSize = search.pageSize.HasValue ? search.pageSize : 20;
                    search.pageNumber = search.pageNumber.HasValue ? search.pageNumber : 1;

                    //q = q.ToPagedList(search.pageNumber.Value, search.pageSize.Value).ToList();

                    //var q_productId = q.Select(s => s.nProductID).ToList();
                    //List<TM_PRODUCT> _PRODUCTs = new List<TM_PRODUCT>();

                    //int INDEX_START = (search.pageNumber ?? 1) == 1 ? 1 : (((search.pageNumber ?? 1) - 1) * (search.pageSize ?? 20)) + 1;
                    //int INDEX_END = (search.pageSize ?? 20) * (search.pageNumber ?? 1);

                    string SQL = string.Format(@"DECLARE @SCHOOLID INT = {0};
DECLARE @nType INT = {1};
DECLARE @SHOPID INT = {2};
DECLARE @Page INT = {3};
DECLARE @RowsOfPage INT = {4};
DECLARE @WORDING VARCHAR(250) = '{5}';


select * into #V_ProductWithStock from V_ProductWithStock where SchoolID = @SCHOOLID
select * into #TProduct from TProduct where SchoolID = @SCHOOLID

SELECT CONVERT(int,ROW_NUMBER() OVER(ORDER BY AA.sBarCode,Len(AA.sBarCode))) AS ROWS_INDEX ,
AA.nProductID AS PRODUCT_ID,AA.sProduct AS PRODUCT_NAME,
AA.sBarCode AS BARCODE,ISNULL(AA.nNumber,0)  AS nNumber,AA.Quantity AS QUANTITY,
AA.CountSell AS COUNT_SELL_NUMBER, 
AA.CountStock AS COUNT_STOCK_NUMBER,
AA.nPrice AS PRICE,
AA.shop_id AS SHOP_ID,TS.shop_name AS SHOP_NAME,AA.cStock AS STOCK_STATUS,AA.nType, C.NameThai 'Unit'

FROM #V_ProductWithStock AS AA 
JOIN TShop AS TS  ON AA.shop_id = TS.shop_id AND AA.SchoolID = TS.SchoolID
JOIN #TProduct B ON AA.SchoolID = B.SchoolID and B.nProductID = AA.nProductID
LEFT JOIN AccountingDB.dbo.AccountMeasure C ON B.UnitID = C.AccountMeasureId

WHERE (AA.sBarCode LIKE '%'+@WORDING+'%' OR AA.sProduct LIKE '%'+@WORDING+'%' ) AND AA.SchoolID = @SCHOOLID AND (AA.nType = @nType OR @nType = 0)
AND AA.shop_id = @SHOPID AND AA.cDel IS NULL AND AA.SchoolID = @SCHOOLID 
GROUP BY AA.nProductID,AA.sProduct,AA.sBarCode,AA.nNumber,AA.Quantity,AA.CountSell,AA.CountStock,AA.nPrice,AA.nCost,AA.shop_id,TS.shop_name,AA.cStock,AA.nType , C.NameThai

drop table if exists #V_ProductWithStock
drop table if exists #TProduct
", userData.CompanyID, search.product_type ?? 0, shop_id, search.pageNumber, search.pageSize, search.wording);

                    var data = dbschool.Database.SqlQuery<TM_PRODUCT>(SQL).ToList();

                    var products = data.Select(o => (int?)o.PRODUCT_ID).ToList();

                    var logic = new StockRepository(dbschool);
                    var dataList = logic.GetStockModelByDate(userData.CompanyID, shop_id, products, DateTime.Now.Date, DateTime.Now.Date);

                    var allOpening = logic.CalcAvgCostPrevDay(userData.CompanyID, shop_id, products, DateTime.Now.Date, DateTime.Now.Date);

                    var costList = new List<ProductCostModel>();

                    foreach (var p in products)
                    {
                        var opening = allOpening.Where(o => o.ProductID == p).FirstOrDefault();
                        if(opening != null)
                        {
                            var last = logic.FindStockCost(dataList.Where(o => o.ProductID == p).ToList(), opening);

                            var cost = (last == null) ? 0 : last.CostAvg;
                            costList.Add(new ProductCostModel { ProductID = p.Value, CostAvg = cost ?? 0 });
                        }                       
                    }

                    return new
                    {
                        data = from a in data
                               from b in costList.Where(o => o.ProductID == a.PRODUCT_ID).DefaultIfEmpty()
                               select
                               new
                               {
                                   index = a.ROWS_INDEX,
                                   price = a.PRICE,
                                   product_id = a.PRODUCT_ID,
                                   product_name = a.PRODUCT_NAME,
                                   cost = b?.CostAvg?.ToString("#,0.00"),
                                   barcode = a.BARCODE,
                                   unit = a.Unit,
                                   quantity = ((a.STOCK_STATUS ?? "0") == "0" ? a.QUANTITY : 0)
                               }

                    };

                }
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string delete_data(int product_id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (var historydb = new JabjaiSchoolHistoryEntity.JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {
                var product = dbschool.TProducts
                    .FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.nProductID == product_id);

                var sellCount1 = dbschool.TSell_Detail
                    .Where(f => f.SchoolID == userData.CompanyID && f.nProduct == product_id)
                    .Sum(o => o.nNumber);

                var sellCount2 = historydb.TSell_Detail
                   .Where(f => f.SchoolID == userData.CompanyID && f.nProduct == product_id)
                   .Sum(o => o.nNumber);

                var sellCount = sellCount1 + sellCount2;

                if (sellCount > 0)
                {
                    return "ProductUsed";
                }

                if (product.cStock == "1")//ไม่นับสต๊อก
                { }
                else
                {
                    var stock = dbschool.TStockDetails
                        .Where(o => o.SchoolID == userData.CompanyID && o.nProductID == product_id)
                        .Sum(o => o.nNumber);

                    var balance = stock - sellCount;

                    if (balance > 0)
                    {
                        return "ProductInStock";
                    }
                }

                product.cDel = "1";
                product.UpdatedBy = userData.UserID;
                product.UpdatedDate = DateTime.Now;

                dbschool.SaveChanges();
                return "Success";
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetProductDetail(int product_id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                string SQL = string.Format(@"
DECLARE @SCHOOLID INT = {0};
DECLARE @ProductID INT = {1};
SELECT top 1 [nProductID] 'PRODUCT_ID' ,[sProduct] 'PRODUCT_NAME'
  ,CASE WHEN cStock = '0' THEN [Quantity]  ELSE 0 END 'QUANTITY',[CountSell],[CountStock]
FROM [JabjaiSchoolSingleDB].[dbo].[V_ProductWithStock]
where SchoolID = @SCHOOLID and nProductID = @ProductID ", userData.CompanyID, product_id);

                var data = dbschool.Database.SqlQuery<TM_PRODUCT>(SQL).FirstOrDefault();

                return new { data = data };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SaveRestock(TStockImprove model)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {               
                var today = DateTime.Today;
                var count = dbschool.TStockImprove
                    .Where(o => o.SchoolID == userData.CompanyID 
                    && DbFunctions.TruncateTime(o.Created) == today 
                    && o.ProductID == model.ProductID)
                    .Count() + 1;

                var adjust = model;
                adjust.DocRef = adjust.Remark + today.ToString("ddMMyy", new CultureInfo("th-TH")) + "/" + count.ToString().PadLeft(2, '0');
                adjust.SchoolID = userData.CompanyID;
                adjust.Created = DateTime.Now;
                adjust.CreateBy = userData.UserID;
                dbschool.TStockImprove.Add(adjust);
                dbschool.SaveChanges();
                return new { status = "success" };
            }
        }

        [WebMethod(EnableSession = true)]
        public static string UpdateMonitor()
        {
            bool isComplete = false;
            decimal percentage = 0;

            percentage = Convert.ToDecimal((HttpContext.Current.Session["ProductImportPercentage"] ?? "0").ToString());
            if (percentage >= 100)
            {
                isComplete = true;
            }

            var result = new { isComplete, percentage };

            return JsonConvert.SerializeObject(result);
        }

        public class ProcessData
        {
            [JsonProperty(PropertyName = "no")]
            public int? No { get; set; }

            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "lastname")]
            public string Lastname { get; set; }

            [JsonProperty(PropertyName = "idCardNumber")]
            public string IDCardNumber { get; set; }

            [JsonProperty(PropertyName = "method")]
            public string Method { get; set; }

            [JsonProperty(PropertyName = "status")]
            // ready(Prepare Data), success(Save Data), warning, error
            public string Status { get; set; }

            [JsonProperty(PropertyName = "errors")]
            public List<Error> Errors { get; set; }

            [JsonProperty(PropertyName = "inDB")]
            // true: update, false: new
            public bool? InDB { get; set; }
        }

        public class Error
        {
            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }

            [JsonProperty(PropertyName = "log")]
            public string Log { get; set; }

            [JsonProperty(PropertyName = "message")]
            public string Message { get; set; }

            [JsonProperty(PropertyName = "status")]
            // warning, error 
            public string Status { get; set; }
        }

        public class Search
        {
            public string wording { get; set; }
            public Nullable<int> product_type { get; set; }
            public Nullable<int> pageSize { get; set; }
            public Nullable<int> pageNumber { get; set; }
            public string shop_id { get; set; }
        }

        public class TM_PRODUCT
        {
            public int ROWS_INDEX { get; set; }
            public int PRODUCT_ID { get; set; }
            public String PRODUCT_NAME { get; set; }
            public String BARCODE { get; set; }
            public decimal PRICE { get; set; }
            public decimal? COST { get; set; }
            public int QUANTITY { get; set; }
            public int CountSell { get; set; }
            public int CountStock { get; set; }
            public String STOCK_STATUS { get; set; }
            public string Unit { get; set; }
        }

        public partial class TM_Product
        {
            public int nProductID { get; set; }
            public string sProduct { get; set; }
            public string sBarCode { get; set; }
            public Nullable<int> nNumber { get; set; }
            public Nullable<decimal> nCost { get; set; }
            public Nullable<decimal> nPrice { get; set; }
            public Nullable<int> nBalance { get; set; }
            public Nullable<int> nType { get; set; }
            public string cDel { get; set; }
            public Nullable<int> nWarn { get; set; }
            public string cStock { get; set; }
            public int SchoolID { get; set; }
            public int ShopID { get; set; }
        }
    }
}