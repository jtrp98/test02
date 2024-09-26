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

namespace FingerprintPayment
{
    public partial class StockDetail : System.Web.UI.Page
    {
        public PageModel ModelData { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + ""))
                Response.Redirect("~/Default.aspx");

            var stockId = int.Parse(EncryptMD5.UrlTokenDecode(HttpContext.Current.Request.QueryString["id"]));

            ModelData = GetData(stockId);
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
                                  .OrderBy( o => o.Index)
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
        }
    }
}