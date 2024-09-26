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

namespace FingerprintPayment
{
    public partial class Stockadd : System.Web.UI.Page
    {
        public class FormSaveData
        {
            public string ShopID { get; set; }
            public TStock Stock { get; set; }
            public List<TStockDetail> Detail { get; set; }
        }

        public List<SelectListItem> ContactList { get; set; } = new List<SelectListItem>();

        public TShop shop = new TShop();
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(HttpContext.Current.Request.QueryString["shop_id"]));
            ProductAutocomplete.ShopID = HttpContext.Current.Request.QueryString["shop_id"] + "";

            using (var accountContext = new AccountingDBContext())
            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
            {
                shop = dbschool.TShops.Find(shop_id, userData.CompanyID);
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
                    int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(model.ShopID));
                    var today = DateTime.Today;
                    var count = dbschool.TStocks.Where(o => o.SchoolID == userData.CompanyID && o.dStock == today).Count() + 1;
                    var stock = new TStock()
                    {
                        nStock = 0,
                        DocRef = today.ToString("ddMMyy", new CultureInfo("th-TH")) + "/" + count.ToString().PadLeft(2, '0'),
                        ContactID = model.Stock.ContactID,
                        INVNo = model.Stock.INVNo,
                        INVDate = model.Stock.INVDate,
                        PONo = model.Stock.PONo,
                        PODate = model.Stock.PODate,
                        IsDone = model.Stock.IsDone,

                        dStock = today,
                        shop_id = shop_id,
                        SchoolID = userData.CompanyID,
                        CreatedBy = userData.UserID,
                        CreatedDate = DateTime.Now,
                    };

                    //var entry = dbschool.Entry(stock);
                    //entry.State = EntityState.Added;
                    dbschool.TStocks.Add(stock);
                    dbschool.SaveChanges();


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
                        else//for temp
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

        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static string json_pordoct(string queryString)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

        //    using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
        //    {
        //        int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(queryString));
        //        using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
        //        {
        //            dynamic rss = new JObject();
        //            var q = (from a in dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList()
        //                     join b in dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.nType equals b.nTypeID
        //                     //orderby new { a.sBarCode.Length, a.sBarCode }
        //                     where a.cDel == null && b.shop_id == shop_id
        //                     select a).ToList();

        //            rss.DATA = new JArray(from a in q
        //                                  select new JObject
        //                                  {
        //                                      new JProperty("value", a.nProductID ),
        //                                      new JProperty("label", a.sProduct),
        //                                      new JProperty("barcode",a.sBarCode),
        //                                  });

        //            return rss.ToString();
        //        }
        //    }
        //}

        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static string addstock(Stock stock)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

        //    using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
        //    {
        //        //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
        //        string entities = HttpContext.Current.Session["sEntities"].ToString();
        //        var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == entities).FirstOrDefault();
        //        int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(stock.shop_id));
        //        using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
        //        {
        //            int nOrder = 1;
        //            if (stock.stock_id == 0)
        //            {
        //                //stock.stock_id = dbschool.TStocks.Where(w => w.SchoolID == userData.CompanyID).Select(o => o.nStock).DefaultIfEmpty(0).Max() + 1;

        //                TStock _Stock = new TStock();
        //                //_Stock.nStock = stock.stock_id;
        //                _Stock.dStock = DateTime.Today;
        //                _Stock.shop_id = shop_id;
        //                _Stock.SchoolID = userData.CompanyID;
        //                _Stock.CreatedBy = userData.UserID;
        //                _Stock.CreatedDate = DateTime.Now;

        //                dbschool.TStocks.Add(_Stock);
        //                dbschool.SaveChanges();

        //                stock.stock_id = _Stock.nStock;
        //            }

        //            dbschool.TStockDetails.RemoveRange((from a in dbschool.TStockDetails.Where(w => w.SchoolID == userData.CompanyID).ToList()
        //                                                where a.nStock == stock.stock_id && !stock.table_Stocks.Select(s => s.product_id).Contains(a.nProductID.Value)
        //                                                select a));

        //            var qdetail = dbschool.TStockDetails.Where(w => w.nStock == stock.stock_id && w.SchoolID == userData.CompanyID).ToList();

        //            nOrder = dbschool.TStockDetails.Where(w => w.nStock == stock.stock_id && w.SchoolID == userData.CompanyID).Select(s => s.nOrder).DefaultIfEmpty(0).Max() + 1;
        //            var qproduct = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList();

        //            foreach (var data in stock.table_Stocks)
        //            {
        //                var f = qdetail.FirstOrDefault(fi => fi.nProductID == data.product_id);
        //                if (f == null)
        //                {
        //                    dbschool.TStockDetails.Add(new TStockDetail
        //                    {
        //                        nNumber = data.amount,
        //                        nProductID = data.product_id,
        //                        nStock = stock.stock_id,
        //                        nOrder = nOrder++,
        //                        SchoolID = userData.CompanyID,
        //                        CreatedBy = userData.UserID,
        //                        CreatedDate = DateTime.Now,
        //                        Tstamp = DateTime.Now
        //                    });
        //                }
        //                else
        //                {
        //                    f.nNumber = data.amount;
        //                }

        //                //qproduct.Where(w => w.nProductID == data.product_id && w.SchoolID == userData.CompanyID).ToList().ForEach(fi => fi.nBalance = (fi.nBalance ?? 0) + data.amount);
        //            }
        //            try
        //            {
        //                dbschool.SaveChanges();
        //            }
        //            catch (DbEntityValidationException exdb)
        //            {
        //                string message_error = "";
        //                foreach (var eve in exdb.EntityValidationErrors)
        //                {
        //                    message_error += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //                    foreach (var ve in eve.ValidationErrors)
        //                    {
        //                        message_error += string.Format("- Property: \"{0}\", Error: \"{1}\"",
        //                            ve.PropertyName, ve.ErrorMessage);
        //                    }
        //                }

        //                return message_error;
        //            }
        //            database.InsertLog(HttpContext.Current.Session["sEmpID"] + "", "นำสินค้าเข้าสต๊อก",
        //                            HttpContext.Current.Session["sEntities"] + "", HttpContext.Current.Request, 8, 2, 0);
        //            return "Success";
        //        }
        //    }
        //}

        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static string getstock(string queryString)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

        //    using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
        //    {
        //        int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(queryString));
        //        using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
        //        {
        //            dynamic rss = new JObject();
        //            var qstock = dbschool.TStocks.Where(w => w.dStock == DateTime.Today && w.shop_id == shop_id && w.SchoolID == userData.CompanyID).FirstOrDefault();
        //            if (qstock == null)
        //            {
        //                rss.DATA = new JArray();
        //                rss.stock_id = 0;
        //            }
        //            else
        //            {
        //                rss.DATA = new JArray(from a in dbschool.TStockDetails.Where(w => w.SchoolID == userData.CompanyID).ToList()
        //                                      join b in dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList() on a.nProductID equals b.nProductID
        //                                      where a.nStock == qstock.nStock
        //                                      select new JObject
        //                                      {
        //                                          new JProperty("product_id",a.nProductID),
        //                                          new JProperty("product_name",b.sProduct),
        //                                          new JProperty("barcode",b.sBarCode),
        //                                          new JProperty("amount",a.nNumber),
        //                                      });

        //                rss.stock_id = qstock.nStock;
        //            }

        //            database.InsertLog(HttpContext.Current.Session["sEmpID"] + "", "นำสินค้าเข้าสต๊อก",
        //                HttpContext.Current.Session["sEntities"] + "", HttpContext.Current.Request, 8, 2, 0);
        //            return rss.ToString();
        //        }
        //    }

        //}

        //public class Stock
        //{
        //    public string shop_id { get; set; }
        //    public int stock_id { get; set; }
        //    public List<Table_Stock> table_Stocks { get; set; }

        //}

        //public class Table_Stock
        //{
        //    public int amount { get; set; }
        //    public int product_id { get; set; }
        //}
    }
}