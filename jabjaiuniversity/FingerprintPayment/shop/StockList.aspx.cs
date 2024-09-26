using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using PagedList;

namespace FingerprintPayment
{
    public partial class StockList : System.Web.UI.Page
    {
        public TShop shop = new TShop();
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(HttpContext.Current.Request.QueryString["shop_id"]));
            if (!this.IsPostBack)
            {
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
                {
                    shop = dbschool.TShops.Find(shop_id, userData.CompanyID);
                }
            }

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object returnlist(Search search)
        {
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(search.shop_id));
                using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
                {
                    var q = (from a in dbschool.TStocks.Where(w => w.shop_id == shop_id && w.SchoolID == userData.CompanyID 
                            /* && w.cDel != "1"*/ /*&& w.INVNo != null*/)
                             from b in dbschool.TEmployees.Where(o => o.SchoolID == a.SchoolID && o.sEmp == a.CreatedBy).DefaultIfEmpty()
                             select new
                             {
                                 a.nStock,
                                 a.INVNo,
                                 a.dStock,
                                 b.sName,
                                 b.sLastname,
                                 a.IsDone,
                                 a.CreatedDate,
                                 a.cDel,
                             })
                            .OrderByDescending(o => o.CreatedDate)
                            .ToList();

                    //rss.FOOT = new JObject()
                    //{
                    //    new JProperty("pageSize",(q.Count() / search.pageSize) + (q.Count() % search.pageSize == 0?0:1))
                    //};

                    search.pageSize = search.pageSize.HasValue ? search.pageSize : 20;
                    search.pageNumber = search.pageNumber.HasValue ? search.pageNumber : 1;

                    //rss.DATA = new JArray(from a in q.ToPagedList(search.pageNumber.Value, search.pageSize.Value).ToList()
                    //                      select new JObject
                    //                      {
                    //                          new JProperty("index", ( search.pageSize*(search.pageNumber -1)) +i++ ),
                    //                          new JProperty("stock_day",a.dStock.ToString("dd/MM/yyyy")),
                    //                          new JProperty("stock_id",HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes( EncryptMD5.Encrypt(string.Format("ST{0:000000}", a.nStock))))),
                    //                      });

                    //return rss.ToString();

                    return new
                    {
                        data = q.Select((a, i) => new
                        {
                            index = i + 1,
                            docNo = a.INVNo,
                            addDate = a.CreatedDate?.ToString("dd/MM/yyyy<br/>HH:mm น.", new CultureInfo("th-TH")),
                            //id = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(EncryptMD5.Encrypt(string.Format("ST{0:000000}", a.nStock)))),
                            id = EncryptMD5.UrlTokenEncode(a.nStock),
                            by = a.sName + " " + a.sLastname,
                            isDone = a.IsDone,
                            IsDel = a.cDel == "1" ? true: false,
                        }),
                    };
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object RemoveByID(string id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            int stockId = int.Parse(EncryptMD5.UrlTokenDecode(id));
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var data = db.TStocks.Where(o => o.SchoolID == userData.CompanyID && o.nStock == stockId).FirstOrDefault();
                data.cDel = "1";
                data.IsDone = true;

                var detail = db.TStockDetails.Where(o => o.SchoolID == userData.CompanyID && o.nStock == stockId).ToList();

                foreach (var i in detail)
                {
                    i.cDel = true;
                }

                db.SaveChanges();

                return new
                {
                    data = data,
                    text = "success"
                };
            }
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
        //            var qproduct = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList();

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
        //                                      join b in dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.nProductID equals b.nProductID
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

        public class Stock
        {
            public string shop_id { get; set; }
            public int stock_id { get; set; }
            public List<Table_Stock> table_Stocks { get; set; }

        }

        public class Table_Stock
        {
            public int amount { get; set; }
            public int product_id { get; set; }
        }


        public class Search
        {
            public string wording { get; set; }
            public Nullable<int> product_type { get; set; }
            public Nullable<int> pageSize { get; set; }
            public Nullable<int> pageNumber { get; set; }
            public string shop_id { get; set; }
        }

    }
}