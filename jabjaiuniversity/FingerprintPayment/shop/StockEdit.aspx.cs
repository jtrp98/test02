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
using System.Web.WebPages.Html;
using FingerprintPayment.UserControls;
using System.Data.Entity.Infrastructure;
using System.Web.Script.Services;
using Newtonsoft.Json;

namespace FingerprintPayment
{
    public partial class StockEdit : System.Web.UI.Page
    {
        public PageModel ModelData { get; set; }

        public List<SelectListItem> ContactList { get; set; } = new List<SelectListItem>();

        public class FormSaveData
        {
            public string ID { get; set; }
            public string ShopID { get; set; }
            public TStock Stock { get; set; }
            public List<TStockDetail> Detail { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            //int stockId = int.Parse(EncryptMD5.UrlTokenDecode(HttpContext.Current.Request.QueryString["id"]));

            using (var accountContext = new AccountingDBContext())
            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
            {
                // shop = dbschool.TShops.Find(shop_id, userData.CompanyID);
                ContactList = accountContext.AccountContact.Where(o => o.SchoolId == userData.CompanyID && o.AccountGroup == "Expense" && !o.DeleteDate.HasValue)
                    .Select(o => new { o.AccountContactId, o.ContactName, })
                    .AsEnumerable()
                    .Select(o => new SelectListItem
                    {
                        Text = o.ContactName,
                        Value = o.AccountContactId + "",
                    })
                    .ToList();
            }

            var stockId = int.Parse(EncryptMD5.UrlTokenDecode(HttpContext.Current.Request.QueryString["id"]));

            ModelData = GetData(stockId);

            ProductAutocomplete.ShopID = EncryptMD5.UrlTokenEncode(ModelData.Stock.shop_id + "");
        }

        private PageModel GetData(int stockId)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            //var _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            //DataTable _dt = (DataTable)ViewState["dt"];
            //string _str = Request.QueryString["id"];
            //byte[] decode = HttpServerUtility.UrlTokenDecode(_str);
            //_str = EncryptMD5.Decrypt(Encoding.UTF8.GetString(decode));

            // int nStock = int.Parse(_str.Replace("ST", ""));
            //var f_stock = _db.TStocks.FirstOrDefault(f => f.nStock == nStock);
            //f_stock.dStock = f_stock.dStock.AddDays(1);

            using (var accountContext = new AccountingDBContext())
            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
            {
                var stock = dbschool.TStocks.FirstOrDefault(o => o.nStock == stockId && o.SchoolID == userData.CompanyID);

                if (stock.IsDone == true)
                {
                    Response.Redirect("/shop/StockDetail.aspx?id=" + EncryptMD5.UrlTokenEncode(stock.nStock));
                }
                var shop = dbschool.TShops.FirstOrDefault(o => o.shop_id == stock.shop_id && o.SchoolID == userData.CompanyID);
                var unitList = accountContext.AccountMeasure
                      .Where(w => (w.SchoolId == userData.CompanyID || w.SchoolId == null) && w.DeleteDate == null)
                      .Select(o => new
                      {
                          o.AccountMeasureId,
                          o.NameThai
                      })
                     .ToList();

                var contact = accountContext.AccountContact
                     .Where(o => o.SchoolId == userData.CompanyID && o.AccountGroup == "Expense" && o.AccountContactId == stock.ContactID)
                     .Select(o => o.ContactName)
                     .FirstOrDefault();

                var detailList1 = (from a in dbschool.TStockDetails.Where(o => o.nStock == stockId)
                                   from b in dbschool.TProducts.Where(o => o.nProductID == a.nProductID && o.SchoolID == a.SchoolID).DefaultIfEmpty()
                                   select new
                                   {
                                       b.nProductID,
                                       b.sBarCode,
                                       b.sProduct,
                                       b.UnitID,
                                       a.Cost,
                                       a.SumPrice,
                                       a.nOrder,
                                       a.nNumber,
                                   }).ToList();

                var detailList2 = (from a in dbschool.TStockDetailTemp.Where(o => o.nStock == stockId)
                                   from b in dbschool.TProducts.Where(o => o.nProductID == a.nProductID && o.SchoolID == a.SchoolID).DefaultIfEmpty()
                                   select new
                                   {
                                       b.nProductID,
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
                    Shop = shop,
                    Contact = contact,
                    Stock = stock,
                    DetailList = (from a in detailList1.Concat(detailList2)
                                  from b in unitList.Where(o => o.AccountMeasureId == a.UnitID).DefaultIfEmpty()
                                  select new DetailModel
                                  {
                                      ProductID = a.nProductID,
                                      Index = a.nOrder,
                                      Barcode = a.sBarCode,
                                      Product = a.sProduct,
                                      Unit = b?.NameThai,
                                      Cost = a.Cost,
                                      Total = a.SumPrice,
                                      Amount = a.nNumber,
                                  })
                       .ToList()
                };
            }

            //var _list = (from a in _db.TStocks.Where(w => w.SchoolID == userData.CompanyID)
            //             join b in _db.TStockDetails.Where(w => w.SchoolID == userData.CompanyID) on a.nStock equals b.nStock
            //             join c in _db.TProducts.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null) on b.nProductID equals c.nProductID

            //             join d in (from a1 in _db.TSells.Where(w => w.SchoolID == userData.CompanyID)
            //                        join b1 in _db.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID && (w.cDel ?? "0") == "0") on a1.sSellID equals b1.nSell
            //                        where a1.dayCancal == null && a1.dSell <= f_stock.dStock
            //                        group b1 by b1.nProduct into gbb1
            //                        select new { gbb1.Key, productAmout = gbb1.Sum(s => s.nNumber) }) on c.nProductID equals d.Key into jcd
            //             from jc in jcd.DefaultIfEmpty()

            //             join e in (from a2 in _db.TStocks.Where(w => w.SchoolID == userData.CompanyID)
            //                        join b2 in _db.TStockDetails.Where(w => w.SchoolID == userData.CompanyID) on a2.nStock equals b2.nStock
            //                        where a2.dStock < f_stock.dStock
            //                        group b2 by b2.nProductID into gbb2
            //                        select new { gbb2.Key, productStock = gbb2.Sum(s => s.nNumber) }) on c.nProductID equals e.Key into jce
            //             from je in jce.DefaultIfEmpty()

            //             where a.nStock == nStock
            //             select new
            //             {
            //                 c.sProduct,
            //                 c.sBarCode,
            //                 c.nProductID,
            //                 b.nNumber,
            //                 productNumber = c.nNumber ?? 0,
            //                 productAmout = jc == null ? 0 : jc.productAmout,
            //                 productStock = je == null ? 0 : je.productStock
            //             }).ToList();

            //dgd.DataSource = (from a in _list
            //                  select new
            //                  {
            //                      nNumber = a.nNumber,
            //                      nProductID = a.nProductID,
            //                      sBarCode = a.sBarCode,
            //                      sProduct = a.sProduct,
            //                      nCount = ((a.productStock + a.productNumber) - a.productAmout) + a.nNumber
            //                  }).ToList();

            //dgd.DataBind();
            //ViewState["dt"] = _dt;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SaveData(FormSaveData model)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                try
                {
                    int id = int.Parse(EncryptMD5.UrlTokenDecode(model.ID));
                    var today = DateTime.Today;
                    var stock = dbschool.TStocks.Where(o => o.SchoolID == userData.CompanyID && o.nStock == id).FirstOrDefault();

                    stock.ContactID = model.Stock.ContactID;
                    stock.INVNo = model.Stock.INVNo;
                    stock.INVDate = model.Stock.INVDate;
                    stock.PONo = model.Stock.PONo;
                    stock.PODate = model.Stock.PODate;
                    stock.IsDone = model.Stock.IsDone;
                    stock.CreatedBy = userData.UserID;
                    stock.CreatedDate = DateTime.Now;

                    var removeDetail = dbschool.TStockDetailTemp
                        .Where(o => o.SchoolID == userData.CompanyID && o.nStock == id)
                        .ToList();

                    if (removeDetail.Count > 0)
                    {
                        dbschool.TStockDetailTemp.RemoveRange(removeDetail);
                    }
                    // dbschool.SaveChanges();

                    foreach (var detail in model.Detail)
                    {
                        if (stock.IsDone == true)
                        {
                            var stockDetail = detail;
                            stockDetail.Cost = stockDetail.SumPrice / stockDetail.nNumber;
                            stockDetail.nStock = stock.nStock;
                            stockDetail.SchoolID = userData.CompanyID;
                            stockDetail.CreatedBy = userData.UserID;
                            stockDetail.CreatedDate = DateTime.Now;
                            stockDetail.Tstamp = DateTime.Now;

                            dbschool.TStockDetails.Add(stockDetail);
                        }
                        else
                        {
                            var temp = CloneObject<TStockDetailTemp, TStockDetail>(detail);
                            temp.Cost = temp.SumPrice / temp.nNumber;
                            temp.nStock = stock.nStock;
                            temp.SchoolID = userData.CompanyID;
                            temp.CreatedBy = userData.UserID;
                            temp.CreatedDate = DateTime.Now;
                            temp.Tstamp = DateTime.Now;

                            dbschool.TStockDetailTemp.Add(temp);
                        }
                    }

                    dbschool.SaveChanges();

                    return new
                    {
                        status = "success"
                    };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return new
                    {
                        status = "fail",
                        msg = ex.Message
                    };
                }
                catch (Exception ex)
                {
                    return new
                    {
                        status = "fail",
                        msg = ex.Message
                    };
                }
            }
        }

        private static T1 CloneObject<T1, T2>(T2 source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T1>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        public class PageModel
        {
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
            public int ProductID { get; internal set; }
        }
    }
}