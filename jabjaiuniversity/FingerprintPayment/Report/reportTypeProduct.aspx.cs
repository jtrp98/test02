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

namespace FingerprintPayment.Report
{
    public partial class reportTypeProduct : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            var userData = GetUserData();
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            if (!this.IsPostBack)
            {
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)), userData);
                    hdfschoolname.Value = tCompany.sCompany;
                }

                var _listslv = _db.TShops.Where(w => w.SchoolID == userData.CompanyID);
                foreach (var DataLV in _listslv)
                {
                    ddshop_id.Items.Add(new ListItem(DataLV.shop_name.ToString(), DataLV.shop_id.ToString()));
                }

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object GetProductType(int? ShopId)
        {
            var userData = GetUserData();
            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {

                if (ShopId.HasValue)
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
                else
                {
                    return null;
                }
            }
        }



        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_data(Search search)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            var userData = GetUserData();
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {
                List<day_data> day_data = new List<day_data>();

                string header_text = "";

                if (search.sort_type == 2)
                {
                    header_text = "ยอดขายแบบรวมรายการสินค้า ปี" + search.dStart.Value.ToString("yyyy", new CultureInfo("th-th"));
                    search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("01/01/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));
                    search.dEnd = search.dStart.Value.NextYear().AddDays(-1.0);

                    for (int i = 0; search.dStart.Value.AddMonths(i) <= search.dEnd.Value; i++)
                    {
                        day_data.Add(new day_data
                        {
                            lable = search.dStart.Value.AddMonths(i).ToString(),
                            values = search.dStart.Value.AddMonths(i)
                        });
                    }

                    var qry1 = GetSellDetail_DatasYear(db, search, userData.CompanyID);

                    //var x = qry1.ToList();
                    var q1 = (
                              from a1 in qry1
                                  //from a1 in db.TSells.Where(w => w.SchoolID == userData.CompanyID)
                                  //join a2 in db.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID) on a1.sSellID equals a2.nSell
                                  //join a3 in db.TProducts.Where(w => w.SchoolID == userData.CompanyID) on a1.nProduct equals a3.nProductID
                                  //join a4 in db.TTypes.Where(w => w.SchoolID == userData.CompanyID) on a3.nType equals a4.nTypeID
                                  //join a5 in db.TShops.Where(w => w.SchoolID == userData.CompanyID) on a4.shop_id equals a5.shop_id
                                  //where //a1.dayCancal == null && a1.dSell >= search.dStart && a1.dSell <= search.dEnd.Value
                                  //(!search.sHopID.HasValue || search.sHopID == a5.shop_id)
                                  //&& (!search.nTypeID.HasValue || search.nTypeID == a4.nTypeID)

                          group a1 by new
                              {
                                  a1.dSell.Value.Month,
                                  a1.nPrice1,
                                  a1.nPrice2,
                                  a1.nCost,
                                  a1.nNumber,
                                  a1.sProduct,
                                  a1.sType
                              } into gb
                              select new
                              {
                                  dMoney = gb.Key.Month,
                                  price1 = gb.Sum(s => s.nPrice1),
                                  price2 = gb.Sum(s => s.nPrice2),
                                  cost = gb.Sum(s => s.nCost),
                                  amount = gb.Sum(s => s.nNumber),
                                  pname = gb.Key.sProduct,
                                  ptype = gb.Key.sType,
                                  profit = gb.Sum(s => s.nPrice1) + gb.Sum(s => s.nPrice2) - gb.Sum(s => s.nCost),
                              }).ToList();

                    var q = (from a in day_data

                             join b in q1 on a.values.Month equals b.dMoney into jab
                             from jb in jab.DefaultIfEmpty()

                             group jb by new
                             {
                                 values = a.values.ToString("dd/MM/yyyy", new CultureInfo("en-us")),
                                 lable = a.values.ToString("MMM-yy", new CultureInfo("th-th"))
                             }
                             into gb
                             select new views01
                             {
                                 lable = gb.Key.lable,
                                 values = gb.Key.values,
                                 pPrice1 = gb.Sum(s => s == null ? 0 : s.price1),
                                 pPrice2 = gb.Sum(s => s == null ? 0 : s.price2),
                                 nCost = gb.Sum(s => s == null ? 0 : s.cost),
                                 pAmount = gb.Sum(s => s == null ? 0 : s.amount),
                                 nProfit = gb.Sum(s => s == null ? 0 : s.profit),
                             }).ToList();

                    return new header_reports
                    {
                        header_text = header_text,
                        data = q,
                        report_type = search.sort_type
                    };
                }
                else
                {
                    if (search.sort_type == 0)
                    {
                        search.dEnd = search.dStart.Value.Next(DayOfWeek.Sunday);
                        search.dStart = search.dEnd.Value.Previous(DayOfWeek.Monday);

                        header_text = "ยอดขายแบบรวมรายการสินค้า วันที่" + search.dStart.Value.ToString("dd MMM ", new CultureInfo("th-th")) + " - " + search.dEnd.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));

                        for (double i = 0; search.dStart.Value.AddDays(i) <= search.dEnd.Value; i++)
                        {
                            day_data.Add(new day_data
                            {
                                lable = search.dStart.Value.AddDays(i).ToString(),
                                values = search.dStart.Value.AddDays(i)
                            });
                        }

                        search.dEnd = search.dEnd.Value.AddDays(1.0);

                        var qry1 = GetSellDetail_DatasDay(db, search, userData.CompanyID);

                        var q1 = (
                                from a1 in qry1
                                    //  from a1 in db.TSells.Where(w => w.SchoolID == userData.CompanyID)
                                    //join a2 in db.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID) on a1.sSellID equals a2.nSell
                                    //join a3 in db.TProducts.Where(w => w.SchoolID == userData.CompanyID) on a1.nProduct equals a3.nProductID
                                    //join a4 in db.TTypes.Where(w => w.SchoolID == userData.CompanyID) on a3.nType equals a4.nTypeID
                                    //join a5 in db.TShops.Where(w => w.SchoolID == userData.CompanyID) on a4.shop_id equals a5.shop_id

                                    //where //a1.dayCancal == null && a1.dSell >= search.dStart && a1.dSell <= search.dEnd.Value
                                    //(!search.sHopID.HasValue || search.sHopID == a5.shop_id)
                                    //&& (!search.nTypeID.HasValue || search.nTypeID == a4.nTypeID)

                            group a1 by new
                                {
                                    a1.dSell,
                                    a1.nPrice1,
                                    a1.nPrice2,
                                    a1.nNumber,
                                    a1.nCost,
                                } into gb
                                select new
                                {
                                    dMoney = gb.Key.dSell,
                                    price1 = gb.Sum(s => s.nPrice1),
                                    price2 = gb.Sum(s => s.nPrice2),
                                    cost = gb.Sum(s => s.nCost),
                                    amount = gb.Sum(s => s.nNumber),
                                    profit = gb.Sum(s => s.nPrice1) + gb.Sum(s => s.nPrice2) - gb.Sum(s => s.nCost),
                                }).ToList();

                        var q = (from a in day_data
                                 join b in q1 on a.values equals b.dMoney into jab
                                 from jb in jab.DefaultIfEmpty()

                                 group jb by new
                                 {
                                     values = a.values.ToString("dd/MM/yyyy", new CultureInfo("en-us")),
                                     lable = a.values.ToString(search.sort_type == 0 ? "dddd" : "dd MMM yy", new CultureInfo("th-th")),
                                 }
                                 into gb
                                 select new views01
                                 {
                                     lable = gb.Key.lable,
                                     values = gb.Key.values,
                                     pPrice1 = gb.Sum(s => s == null ? 0 : s.price1),
                                     pPrice2 = gb.Sum(s => s == null ? 0 : s.price2),
                                     nCost = gb.Sum(s => s == null ? 0 : s.cost),
                                     pAmount = gb.Sum(s => s == null ? 0 : s.amount),
                                     nProfit = gb.Sum(s => s == null ? 0 : s.profit),
                                 }).ToList();

                        return new header_reports
                        {
                            header_text = header_text,
                            data = q,
                            report_type = search.sort_type
                        };
                    }
                    else if (search.sort_type == 1)
                    {
                        search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("01/MM/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));
                        search.dEnd = search.dStart.Value.NextMonth().AddDays(-1.0);
                        header_text = "ยอดขายแบบรวมรายการสินค้า เดือน" + search.dStart.Value.ToString("MMMM yyyy ", new CultureInfo("th-th"));

                        for (double i = 0; search.dStart.Value.AddDays(i) <= search.dEnd.Value; i++)
                        {
                            day_data.Add(new day_data
                            {
                                lable = search.dStart.Value.AddDays(i).ToString(),
                                values = search.dStart.Value.AddDays(i)
                            });
                        }

                        search.dEnd = search.dEnd.Value.AddDays(1.0);

                        var qry1 = GetSellDetail_DatasMonth(db, search, userData.CompanyID);

                        var q1 = (
                                  //from a1 in db.TSells.Where(w => w.SchoolID == userData.CompanyID)
                                  //  join a2 in db.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID) on a1.sSellID equals a2.nSell
                                  from a1 in qry1
                                      //join a3 in db.TProducts.Where(w => w.SchoolID == userData.CompanyID) on a1.nProduct equals a3.nProductID
                                      //join a4 in db.TTypes.Where(w => w.SchoolID == userData.CompanyID) on a3.nType equals a4.nTypeID
                                      //join a5 in db.TShops.Where(w => w.SchoolID == userData.CompanyID) on a4.shop_id equals a5.shop_id

                                      //where //a1.dayCancal == null && a1.dSell >= search.dStart && a1.dSell <= search.dEnd.Value
                                      // (!search.sHopID.HasValue || search.sHopID == a1.shop_id)
                                      //&& (!search.nTypeID.HasValue || search.nTypeID == a4.nTypeID)

                              group a1 by new
                                  {
                                      a1.dSell,
                                      a1.nPrice1,
                                      a1.nPrice2,
                                      a1.nNumber,
                                      a1.nCost,
                                  } into gb
                                  select new
                                  {
                                      dMoney = gb.Key.dSell.Value,
                                      price1 = gb.Sum(s => s.nPrice1),
                                      price2 = gb.Sum(s => s.nPrice2),
                                      cost = gb.Sum(s => s.nCost),
                                      amount = gb.Sum(s => (s.nNumber ?? 0)),
                                      profit = gb.Sum(s => s.nPrice1) + gb.Sum(s => s.nPrice2) - gb.Sum(s => s.deduct1) - gb.Sum(s => s.deduct2) - gb.Sum(s => s.nCost),
                                  }).ToList();

                        var q = (from a in day_data
                                 join b in q1 on a.values equals b.dMoney into jab
                                 from jb in jab.DefaultIfEmpty()

                                 group jb by new
                                 {
                                     values = a.values.ToString("dd/MM/yyyy", new CultureInfo("en-us")),
                                     lable = a.values.ToString(search.sort_type == 0 ? "dddd" : "dd MMM yy", new CultureInfo("th-th")),
                                 }
                                 into gb
                                 select new views01
                                 {
                                     lable = gb.Key.lable,
                                     values = gb.Key.values,
                                     pPrice1 = gb.Sum(s => s == null ? 0 : s.price1),
                                     pPrice2 = gb.Sum(s => s == null ? 0 : s.price2),
                                     nCost = gb.Sum(s => s == null ? 0 : s.cost),
                                     pAmount = gb.Sum(s => s == null ? 0 : s.amount),
                                     nProfit = gb.Sum(s => s == null ? 0 : s.profit),
                                 }).ToList();

                        return new header_reports
                        {
                            header_text = header_text,
                            data = q,
                            report_type = search.sort_type
                        };

                    }
                    else
                    {

                        search.dEnd = search.dEnd.Value.AddDays(1.0);

                        var q_detail = GetSellDetail_DatasDay(db, search, userData.CompanyID);

                        //var q_detail = (
                        //                //from a in db.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID)
                        //                from a in qry1
                        //                join b in db.TProducts.Where(w => w.SchoolID == userData.CompanyID) on a.nProduct equals b.nProductID
                        //                join c in db.TTypes.Where(w => w.SchoolID == userData.CompanyID) on b.nType equals c.nTypeID
                        //                join d in db.TShops.Where(w => w.SchoolID == userData.CompanyID) on c.shop_id equals d.shop_id
                        //                join f in db.TSells.Where(w => w.SchoolID == userData.CompanyID) on a.nSell equals f.sSellID
                        //                where //f.dayCancal == null && f.dSell >= search.dStart && f.dSell <= search.dEnd.Value
                        //                (!search.sHopID.HasValue || search.sHopID == d.shop_id)
                        //                && (!search.nTypeID.HasValue || search.nTypeID == c.nTypeID)
                        //                select new
                        //                {
                        //                    a.nNumber,
                        //                    a.nPrice,
                        //                    b.sProduct,
                        //                    b.nProductID,
                        //                    b.sBarCode,
                        //                    c.sType,
                        //                    c.nTypeID,
                        //                    d.shop_id,
                        //                    d.shop_name
                        //                }).ToList();

                        var q1 = (from a in q_detail
                                  group a by new { a.nTypeID, a.sType, a.shop_id, a.shop_name } into gb1
                                  orderby gb1.Key.shop_id ascending
                                  select new report_detail
                                  {
                                      pTypeID = gb1.Key.nTypeID,
                                      pTypeName = gb1.Key.sType,
                                      sHopID = gb1.Key.shop_id,
                                      sHopName = gb1.Key.shop_name,

                                      products = (from a1 in gb1
                                                  group a1 by new { a1.sProduct, a1.nProductID, a1.sBarCode } into gb2
                                                  orderby gb2.Key.sBarCode
                                                  select new product
                                                  {
                                                      pRoductID = gb2.Key.nProductID,
                                                      pRoductName = gb2.Key.sProduct,
                                                      pRoductBarCode = gb2.Key.sBarCode,
                                                      pSumPrice1 = gb2.Sum(s => s.nPrice1),
                                                      pSumPrice2 = gb2.Sum(s => s.nPrice2),
                                                      pSumAmount = gb2.Sum(s => s.nNumber),
                                                      pSumCost = gb2.Sum(s => s.nCost),
                                                      pSumProfit = gb2.Sum(s => s.nPrice1) + gb2.Sum(s => s.nPrice2) - gb2.Sum(s => s.nCost),
                                                  }).ToList()
                                  }).ToList();

                        header_text = "รายงานยอดขายแบบรวมรายการสินค้าระหว่างวันที่ " + search.dStart.Value.ToString("dd MMM yyyy", new CultureInfo("th-th")) + " - " + search.dEnd.Value.AddDays(-1.0).ToString("dd MMM yyyy", new CultureInfo("th-th"));

                        return new header_reports
                        {
                            report_Details = q1,
                            header_text = header_text,
                            report_type = search.sort_type
                        };

                    }

                }
            }
        }


        //        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //        [WebMethod(EnableSession = true)]
        //        public static object reports_detail(Search search)
        //        {
        //            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
        //            var userData = GetUserData();
        //            string entities = HttpContext.Current.Session["sEntities"].ToString();
        //            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
        //            List<day_data> day_data = new List<day_data>();

        //            search.dEnd = search.dStart.Value.AddDays(1.0);

        //            //var qry1 = (
        //            //            from a in db.VTSellHistories.Where(o => o.SchoolID == userData.CompanyID)
        //            //            where a.dayCancal == null
        //            //              && a.dSell >= search.dStart
        //            //              && a.dSell <= search.dEnd.Value

        //            //            select new
        //            //            {
        //            //                a.dSell,
        //            //                a.nPrice,
        //            //                a.nNumber,
        //            //                a.nProduct,
        //            //                a.nSell,
        //            //            }
        //            //        )
        //            //        .Union
        //            //        (
        //            //            from a1 in db.TSells.Where(w => w.SchoolID == userData.CompanyID)
        //            //            from a2 in db.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID && a1.sSellID == w.nSell && (w.cDel ?? "0") == "0")

        //            //            where a1.dayCancal == null
        //            //              && a1.dSell >= search.dStart
        //            //              && a1.dSell <= search.dEnd.Value

        //            //            select new
        //            //            {
        //            //                a1.dSell,
        //            //                a2.nPrice,
        //            //                a2.nNumber,
        //            //                a2.nProduct,
        //            //                a2.nSell,
        //            //            }
        //            //        );

        //            var qry1 = new List<Q1>();
        //            qry1.AddRange(db.Database.SqlQuery<Q1>(string.Format(@"DECLARE @SchoolID INT = {1}

        //SELECT A.dSell,B.nPrice,ISNULL(B.nNumber,1) 'nNumber',B.nProduct,B.nSell, C.nType ,C.nProductID,ISNULL(C.sProduct,'-') 'sProduct',ISNULL(C.sBarCode,'-') 'sBarCode'
        //, CASE WHEN ISNULL(A.PaymentType,0) NOT IN (3) THEN  ISNULL((B.nPrice * b.nNumber) , A.nTotal) END 'nPrice1' 
        //, CASE WHEN A.PaymentType IN (3) THEN  ISNULL((B.nPrice * b.nNumber) , A.nTotal) END 'nPrice2' 
        //, ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1) AS 'nCost'

        //FROM JabjaiSchoolSingleDB.dbo.TSell A
        //LEFT JOIN JabjaiSchoolSingleDB.dbo.TSell_Detail B ON A.SchoolID = B.SchoolID AND A.sSellID = B.nSell
        //LEFT JOIN TProduct AS C ON C.SchoolID = B.SchoolID AND B.nProduct = C.nProductID

        //WHERE A.SchoolID = @SchoolID  AND A.dSell BETWEEN '{0:yyyyMMdd}' AND  DATEADD(dd,1,'{0:yyyyMMdd}') 
        //AND A.dayCancal IS NULL 
        //AND ISNULL(A.cDel,0) = 0
        //AND ISNULL(B.cDel,0) = 0
        //AND B.nProduct is not null ", search.dStart, userData.CompanyID)));

        //            qry1.AddRange(db.Database.SqlQuery<Q1>(string.Format(@"DECLARE @SchoolID INT = {1}

        //SELECT A.dSell,B.nPrice,ISNULL(B.nNumber,1) 'nNumber',B.nProduct,B.nSell, C.nType ,C.nProductID,ISNULL(C.sProduct,'-') 'sProduct',ISNULL(C.sBarCode,'-') 'sBarCode'
        //, CASE WHEN ISNULL(A.PaymentType,0) NOT IN (3) THEN  ISNULL((B.nPrice * b.nNumber) , A.nTotal) END 'nPrice1' 
        //, CASE WHEN A.PaymentType IN (3) THEN  ISNULL((B.nPrice * b.nNumber) , A.nTotal) END 'nPrice2' 
        //, ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1) AS 'nCost'
        //FROM JabjaiSchoolHistory.dbo.TSell A
        //LEFT JOIN JabjaiSchoolHistory.dbo.TSell_Detail B ON A.SchoolID = B.SchoolID AND A.sSellID = B.nSell
        //LEFT JOIN TProduct AS C ON C.SchoolID = B.SchoolID AND B.nProduct = C.nProductID

        //WHERE A.SchoolID = @SchoolID  AND A.dSell BETWEEN '{0:yyyyMMdd}' AND  DATEADD(dd,1,'{0:yyyyMMdd}') 
        //AND A.dayCancal IS NULL 
        //AND ISNULL(A.cDel,0) = 0
        //AND ISNULL(B.cDel,0) = 0
        //AND B.nProduct is not null  ", search.dStart, userData.CompanyID)));

        //            var q_detail = (
        //                            //from a in db.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID)
        //                            from a in qry1
        //                            // join b in db.TProducts.Where(w => w.SchoolID == userData.CompanyID) on a.nProduct equals b.nProductID
        //                            join c in db.TTypes.Where(w => w.SchoolID == userData.CompanyID) on a.nType equals c.nTypeID
        //                            join d in db.TShops.Where(w => w.SchoolID == userData.CompanyID) on c.shop_id equals d.shop_id
        //                            //join f in db.TSells.Where(w => w.SchoolID == userData.CompanyID) on a.nSell equals f.sSellID
        //                            where //f.dayCancal == null && f.dSell >= search.dStart && f.dSell <= search.dEnd.Value
        //                             (!search.sHopID.HasValue || search.sHopID == d.shop_id)
        //                            && (!search.nTypeID.HasValue || search.nTypeID == c.nTypeID)
        //                            select new
        //                            {
        //                                a.nNumber,
        //                                a.nPrice,
        //                                a.nPrice1,
        //                                a.nPrice2,
        //                                a.sProduct,
        //                                a.nProductID,
        //                                a.sBarCode,
        //                                c.sType,
        //                                c.nTypeID,
        //                                d.shop_id,
        //                                d.shop_name
        //                            }).ToList();

        //            var q1 = (from a in q_detail
        //                      group a by new { a.nTypeID, a.sType, a.shop_id, a.shop_name } into gb1
        //                      orderby gb1.Key.shop_name ascending
        //                      select new report_detail
        //                      {
        //                          pTypeID = gb1.Key.nTypeID,
        //                          pTypeName = gb1.Key.sType,
        //                          sHopID = gb1.Key.shop_id,
        //                          sHopName = gb1.Key.shop_name,

        //                          products = (from a1 in gb1
        //                                      group a1 by new { a1.sProduct, a1.nProductID, a1.sBarCode } into gb2
        //                                      orderby gb2.Key.sBarCode.Length, gb2.Key.sBarCode
        //                                      select new product
        //                                      {
        //                                          pRoductID = gb2.Key.nProductID,
        //                                          pRoductName = gb2.Key.sProduct,
        //                                          pRoductBarCode = gb2.Key.sBarCode,
        //                                          //pSumPrice1 = gb2.Sum(s => s.nPrice * s.nNumber),
        //                                          pSumPrice1 = gb2.Sum(s => s.nPrice1),
        //                                          pSumPrice2 = gb2.Sum(s => s.nPrice2),
        //                                          pSumAmount = gb2.Sum(s => s.nNumber)
        //                                      }).ToList()
        //                      }).ToList();

        //            string header_text = "รายงานยอดขายแบบรวมรายการสินค้า วันที่ " + search.dStart.Value.ToString("dd MMM yyyy", new CultureInfo("th-th"));

        //            return new views02
        //            {
        //                report_Details = q1,
        //                header_text = header_text
        //            };

        //        }

        private static List<SellDetail_Data> GetSellDetail_DatasYear(JabJaiEntities entities, Search search, int SchoolID)
        {
            string SQL = "";
            List<SellDetail_Data> q = new List<SellDetail_Data>();

            //var modelDate = new ModelDate() { SchoolID = 0, TimeStart = DateTime.Now, TimeEnd = DateTime.Now };
            var lstDate = new List<ModelDate>();
            //var shopId = search.sHopID ?? 0;
            for (int i = 0; search.dStart.Value.AddMonths(i) <= search.dEnd.Value; i++)
            {
                lstDate.Add(new ModelDate()
                {
                    SchoolID = SchoolID,
                    TimeStart = search.dStart.Value.AddMonths(i).ToString("MM/01/yyyy"),
                    TimeEnd = search.dStart.Value.AddMonths(i + 1).ToString("MM/01/yyyy")
                });
            }
            var jsonDate = JsonConvert.SerializeObject(lstDate);

            SQL = $@"
SELECT A0.TimeStart AS 'values',CONVERT(DATETIME,FORMAT(A0.TimeStart, 'dd/MM/yyyy'),103) AS dSell,
SUM(A0.nNumber) AS 'nNumber',
SUM(A0.price1) AS 'nPrice1',
SUM(A0.deduct1) 'deduct1',
SUM(A0.price2) AS 'nPrice2',
SUM(A0.deduct2) 'deduct2',
SUM(A0.cost) AS 'nCost'
FROM(
      SELECT TT1.SchoolID,  A.shop_id, SUM(ISNULL(A.nNumber,0)) 'nNumber',
       SUM(ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 )  AS 'price1'  ,
     SUM(ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 ) *0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct1',
	  SUM(ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 )  AS 'price2'  ,
     SUM(ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 ) *0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct2',
    SUM(ISNULL(ISNULL(A.nCost, C.nCost), 0) * ISNULL(A.nNumber, 1) - case WHEN ISNULL(A.UpdatedTime,A.dSell)  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nCost, C.nCost), 0) * ISNULL(A.nNumber, 1) END)  AS 'cost',
	
	TT1.TimeStart  
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('{jsonDate}')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN (
        select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,A.dayCancal,  B.nCost, ISNULL(B.nNumber,1) 'nNumber', B.nProduct 
		, CASE WHEN ISNULL(A.PaymentType,0) NOT IN (3) THEN  B.nPrice ELSE 0 END 'nPrice1' 
		, CASE WHEN A.PaymentType IN (3) THEN  B.nPrice ELSE 0 END 'nPrice2' 

        from 	[JabjaiSchoolSingleDB].[dbo].[TSell] A 
		LEFT JOIN [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
        WHERE A.SchoolID = {SchoolID} and (dayCancal BETWEEN '{search.dStart?.ToString("yyyyMMdd HH:mm")}' AND '{search.dEnd?.ToString("yyyyMMdd HH:mm")}' OR ISNULL(UpdatedTime,dSell) BETWEEN '{search.dStart?.ToString("yyyyMMdd HH:mm")}' AND '{search.dEnd?.ToString("yyyyMMdd HH:mm")}'  )	   
		{(search.sHopID.HasValue ? "  AND shop_id = " + search.sHopID : "")}
		UNION

	    select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,A.dayCancal, B.nCost, ISNULL(B.nNumber,1) 'nNumber', B.nProduct
		, CASE WHEN ISNULL(A.PaymentType,0) NOT IN (3) THEN  B.nPrice ELSE 0 END 'nPrice1' 
		, CASE WHEN A.PaymentType IN (3) THEN  B.nPrice ELSE 0 END 'nPrice2' 

        from JabjaiSchoolHistory.[dbo].[TSell] A 
		LEFT JOIN JabjaiSchoolHistory.[dbo].[TSell_Detail] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
        WHERE A.SchoolID = {SchoolID} and (dayCancal BETWEEN '{search.dStart?.ToString("yyyyMMdd HH:mm")}' AND '{search.dEnd?.ToString("yyyyMMdd HH:mm")}' OR ISNULL(UpdatedTime,dSell) BETWEEN '{search.dStart?.ToString("yyyyMMdd HH:mm")}' AND '{search.dEnd?.ToString("yyyyMMdd HH:mm")}' )
	    {(search.sHopID.HasValue ? "  AND shop_id = " + search.sHopID : "")}
    ) AS A ON A.SchoolID = TT1.SchoolID AND (dayCancal BETWEEN TT1.TimeStart AND TT1.TimeEnd OR ISNULL(UpdatedTime,dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  )
    --LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] WHERE SchoolID = @SCHOOLID AND ISNULL(cDel,'0') = '0'  ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID 
    LEFT OUTER JOIN TProduct AS C ON A.SchoolID = C.SchoolID AND  A.nProduct = C.nProductID 
    LEFT OUTER JOIN TType  AS D  ON C.SchoolID = D.SchoolID AND C.nType = D.nTypeID 
	LEFT OUTER JOIN TShop  AS E  ON A.SchoolID = E.SchoolID AND E.shop_id = A.shop_id 

WHERE 1=1 {(search.nTypeID.HasValue ? "AND D.nTypeID = " + search.nTypeID : "")}
	GROUP BY TT1.SchoolID,TT1.TimeStart,A.shop_id 

) AS A0

GROUP BY A0.TimeStart
ORDER BY A0.TimeStart 
";

            q.AddRange(entities.Database.SqlQuery<SellDetail_Data>(SQL).ToList());

            return q;
        }

        private static List<SellDetail_Data> GetSellDetail_DatasMonth(JabJaiEntities entities, Search search, int SchoolID)
        {
            string SQL = "";
            List<SellDetail_Data> q = new List<SellDetail_Data>();

            //var modelDate = new ModelDate() { SchoolID = 0, TimeStart = DateTime.Now, TimeEnd = DateTime.Now };
            var lstDate = new List<ModelDate>();
            //var shopId = search.sHopID ?? 0;
            for (double i = 0; search.dStart.Value.AddDays(i) <= search.dEnd.Value; i++)
            {
                lstDate.Add(new ModelDate()
                {
                    SchoolID = SchoolID,
                    TimeStart = search.dStart.Value.AddDays(i).ToString("MM/dd/yyyy"),
                    TimeEnd = search.dStart.Value.AddDays(i + 1).ToString("MM/dd/yyyy")
                });
            }
            var jsonDate = JsonConvert.SerializeObject(lstDate);

            SQL = $@"
SELECT A0.TimeStart AS 'values',CONVERT(DATETIME,FORMAT(A0.TimeStart, 'dd/MM/yyyy'),103) AS dSell,
SUM(A0.nNumber) AS 'nNumber',
SUM(A0.price1) AS 'nPrice1',
SUM(A0.deduct1) 'deduct1',
SUM(A0.price2) AS 'nPrice2',
SUM(A0.deduct2) 'deduct2',
SUM(A0.cost) AS 'nCost'
FROM(
      SELECT TT1.SchoolID,  A.shop_id, SUM(ISNULL(A.nNumber,0)) 'nNumber',
       SUM(ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 )  AS 'price1'  ,
     SUM(ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice1, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 ) *0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct1',
	  SUM(ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 )  AS 'price2'  ,
     SUM(ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1)
	  -  case WHEN A.dayCancal  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1) ELSE 0 END 
	  - case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nPrice2, A.nTotal), 0) * ISNULL(A.nNumber, 1) END 
	 ) *0.01 * ISNULL(MAX(E.DeductPercent),0) AS 'deduct2',
    SUM(ISNULL(ISNULL(A.nCost, C.nCost), 0) * ISNULL(A.nNumber, 1) - case WHEN ISNULL(A.UpdatedTime,A.dSell)  BETWEEN TT1.TimeStart AND TT1.TimeEnd  THEN 0 ELSE ISNULL(ISNULL(A.nCost, C.nCost), 0) * ISNULL(A.nNumber, 1) END)  AS 'cost',
	
	TT1.TimeStart  
    FROM (
        SELECT SchoolID,TimeStart,TimeEnd FROM OPENJSON('{jsonDate}')
        WITH
        (
            SchoolID INT,
            TimeStart DATETIME,
            TimeEnd DATETIME
        )
    ) AS TT1
    LEFT OUTER JOIN (
        select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,A.dayCancal,  B.nCost, ISNULL(B.nNumber,1) 'nNumber', B.nProduct 
		, CASE WHEN ISNULL(A.PaymentType,0) NOT IN (3) THEN  B.nPrice ELSE 0 END 'nPrice1' 
		, CASE WHEN A.PaymentType IN (3) THEN  B.nPrice ELSE 0 END 'nPrice2' 

        from 	[JabjaiSchoolSingleDB].[dbo].[TSell] A 
		LEFT JOIN [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
        WHERE A.SchoolID = {SchoolID} and (dayCancal BETWEEN '{search.dStart?.ToString("yyyyMMdd HH:mm")}' AND '{search.dEnd?.ToString("yyyyMMdd HH:mm")}' OR ISNULL(UpdatedTime,dSell) BETWEEN '{search.dStart?.ToString("yyyyMMdd HH:mm")}' AND '{search.dEnd?.ToString("yyyyMMdd HH:mm")}'  )	   
		{(search.sHopID.HasValue ? "  AND shop_id = " + search.sHopID : "")}
		UNION

	    select A.shop_id , A.sSellID , A.SchoolID ,A.nTotal , A.UpdatedTime , A.dSell ,A.dayCancal, B.nCost, ISNULL(B.nNumber,1) 'nNumber', B.nProduct
		, CASE WHEN ISNULL(A.PaymentType,0) NOT IN (3) THEN  B.nPrice ELSE 0 END 'nPrice1' 
		, CASE WHEN A.PaymentType IN (3) THEN  B.nPrice ELSE 0 END 'nPrice2' 

        from JabjaiSchoolHistory.[dbo].[TSell] A 
		LEFT JOIN JabjaiSchoolHistory.[dbo].[TSell_Detail] B ON A.sSellID = B.nSell AND A.SchoolID = B.SchoolID AND ISNULL(B.cDel,'0') = '0'
        WHERE A.SchoolID = {SchoolID} and (dayCancal BETWEEN '{search.dStart?.ToString("yyyyMMdd HH:mm")}' AND '{search.dEnd?.ToString("yyyyMMdd HH:mm")}' OR ISNULL(UpdatedTime,dSell) BETWEEN '{search.dStart?.ToString("yyyyMMdd HH:mm")}' AND '{search.dEnd?.ToString("yyyyMMdd HH:mm")}' )
	    {(search.sHopID.HasValue ? "  AND shop_id = " + search.sHopID : "")}
    ) AS A ON A.SchoolID = TT1.SchoolID AND (dayCancal BETWEEN TT1.TimeStart AND TT1.TimeEnd OR ISNULL(UpdatedTime,dSell) BETWEEN TT1.TimeStart AND TT1.TimeEnd  )
    --LEFT OUTER JOIN (SELECT * FROM [JabjaiSchoolSingleDB].[dbo].[TSell_Detail] WHERE SchoolID = @SCHOOLID AND ISNULL(cDel,'0') = '0'  ) AS B ON B.nSell = A.sSellID AND A.SchoolID = B.SchoolID 
    LEFT OUTER JOIN TProduct AS C ON A.SchoolID = C.SchoolID AND  A.nProduct = C.nProductID 
    LEFT OUTER JOIN TType  AS D  ON C.SchoolID = D.SchoolID AND C.nType = D.nTypeID 
	LEFT OUTER JOIN TShop  AS E  ON A.SchoolID = E.SchoolID AND E.shop_id = A.shop_id 

WHERE 1=1 {(search.nTypeID.HasValue ? "AND D.nTypeID = " + search.nTypeID : "")}
	GROUP BY TT1.SchoolID,TT1.TimeStart,A.shop_id 

) AS A0

GROUP BY A0.TimeStart
ORDER BY A0.TimeStart 
";

            q.AddRange(entities.Database.SqlQuery<SellDetail_Data>(SQL).ToList());

            return q;
        }

        private static List<SellDetail_Data> GetSellDetail_DatasDay(JabJaiEntities entities, Search search, int SchoolID)
        {
            string SQL = "";
            List<SellDetail_Data> q = new List<SellDetail_Data>();

            SQL = string.Format(@"
SELECT CONVERT(datetime,FORMAT(ISNULL(A.UpdatedTime, A.dSell),'yyyyMMdd')) AS dSell,B.nPrice,A.sSellID,B.nProduct,C.nProductID,ISNULL(C.sProduct,'-') 'sProduct',ISNULL(C.sBarCode,'-') 'sBarCode',
ISNULL(T1.sType,'-') 'sType' ,T1.nTypeID,A.shop_id,S1.shop_name 

, ISNULL(B.nNumber,1) 
- case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN  ISNULL(B.nNumber,1)  ELSE 0 END
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE  ISNULL(B.nNumber,1)  END 'nNumber' 

, CASE WHEN ISNULL(A.PaymentType,0) NOT IN (3) THEN  ISNULL((B.nPrice * b.nNumber) , A.nTotal) 
- (case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) ELSE 0 END) 
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) END
END 'nPrice1' 
, CASE WHEN A.PaymentType IN (3) THEN  ISNULL((B.nPrice * b.nNumber) , A.nTotal) 
- (case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) ELSE 0 END) 
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) END
END 'nPrice2' 
, ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1)
- case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN  ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1)  ELSE 0 END
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE  ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1)  END 'nCost' 

FROM {0}.dbo.TSell AS A
LEFT JOIN {0}.dbo.TSell_Detail AS B ON A.SchoolID = B.SchoolID AND A.sSellID = B.nSell AND  ISNULL(B.cDel,0) = 0 
LEFT JOIN TProduct AS C ON C.SchoolID = B.SchoolID AND B.nProduct = C.nProductID
LEFT JOIN TType AS T1  ON T1.nTypeID = C.nType AND T1.SchoolID = C.SchoolID
LEFT JOIN TShop AS S1 ON A.shop_id = S1.shop_id AND A.SchoolID = S1.SchoolID

WHERE A.SchoolID = {3} 
AND ( ISNULL( A.UpdatedTime ,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}' OR A.dayCancal BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}' )

", "JabjaiSchoolHistory", search.dStart, search.dEnd, SchoolID);

            if (search.sHopID.HasValue) SQL += " AND A.shop_id = " + search.sHopID;
            if (search.nTypeID.HasValue) SQL += " AND T1.nTypeID = " + search.nTypeID;

            q.AddRange(entities.Database.SqlQuery<SellDetail_Data>(SQL).ToList());

            SQL = string.Format(@"
SELECT CONVERT(datetime,FORMAT(ISNULL(A.UpdatedTime, A.dSell),'yyyyMMdd')) AS dSell,B.nPrice,A.sSellID,B.nProduct,C.nProductID,ISNULL(C.sProduct,'-') 'sProduct',ISNULL(C.sBarCode,'-') 'sBarCode',
ISNULL(T1.sType,'-') 'sType' ,T1.nTypeID,A.shop_id,S1.shop_name 

, ISNULL(B.nNumber,1) 
- case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN  ISNULL(B.nNumber,1)  ELSE 0 END
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE  ISNULL(B.nNumber,1)  END 'nNumber' 

, CASE WHEN ISNULL(A.PaymentType,0) NOT IN (3) THEN  ISNULL((B.nPrice * b.nNumber) , A.nTotal) 
- (case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) ELSE 0 END) 
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) END
END 'nPrice1' 

, CASE WHEN A.PaymentType IN (3) THEN  ISNULL((B.nPrice * b.nNumber) , A.nTotal) 
- (case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) ELSE 0 END) 
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) END
END 'nPrice2' 

, ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1)
- case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN  ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1)  ELSE 0 END
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE  ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1)  END 'nCost' 

FROM {0}.dbo.TSell AS A
LEFT JOIN {0}.dbo.TSell_Detail AS B ON A.SchoolID = B.SchoolID AND A.sSellID = B.nSell AND  ISNULL(B.cDel,0) = 0 
LEFT JOIN TProduct AS C ON C.SchoolID = B.SchoolID AND B.nProduct = C.nProductID
LEFT JOIN TType AS T1 ON T1.nTypeID = C.nType AND T1.SchoolID = C.SchoolID
LEFT JOIN TShop AS S1 ON A.shop_id = S1.shop_id AND A.SchoolID = S1.SchoolID

WHERE A.SchoolID = {3} 
AND ( ISNULL( A.UpdatedTime ,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  OR A.dayCancal BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}' )

", "JabjaiSchoolSingleDB", search.dStart, search.dEnd, SchoolID);

            if (search.sHopID.HasValue) SQL += " AND A.shop_id = " + search.sHopID;
            if (search.nTypeID.HasValue) SQL += " AND T1.nTypeID = " + search.nTypeID;

            q.AddRange(entities.Database.SqlQuery<SellDetail_Data>(SQL).ToList());

            var oldDate = new DateTime(DateTime.Now.Year, 05, 01);
            if (search.dStart <= oldDate)
            {
                SQL = string.Format(@"
SELECT CONVERT(datetime,FORMAT(ISNULL(A.UpdatedTime, A.dSell),'yyyyMMdd')) AS dSell,B.nPrice,A.sSellID,B.nProduct,C.nProductID,ISNULL(C.sProduct,'-') 'sProduct',ISNULL(C.sBarCode,'-') 'sBarCode',
ISNULL(T1.sType,'-') 'sType' ,T1.nTypeID,A.shop_id,S1.shop_name 

, ISNULL(B.nNumber,1) 
- case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN  ISNULL(B.nNumber,1)  ELSE 0 END
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE  ISNULL(B.nNumber,1)  END 'nNumber' 

, CASE WHEN ISNULL(A.PaymentType,0) NOT IN (3) THEN  ISNULL((B.nPrice * b.nNumber) , A.nTotal) 
- (case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) ELSE 0 END) 
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) END
END 'nPrice1' 

, CASE WHEN A.PaymentType IN (3) THEN  ISNULL((B.nPrice * b.nNumber) , A.nTotal) 
- (case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) ELSE 0 END) 
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE ISNULL(ISNULL(B.nPrice, A.nTotal), 0) * ISNULL(B.nNumber, 1) END
END 'nPrice2' 

, ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1)
- case WHEN A.dayCancal  BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN  ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1)  ELSE 0 END
- case WHEN ISNULL(A.UpdatedTime,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  THEN 0 ELSE  ISNULL(B.nCost,C.nCost) * ISNULL(B.nNumber,1)  END 'nCost' 

FROM {0}.dbo.TSell_Backup AS A
LEFT JOIN {0}.dbo.TSell_Detail_Backup AS B ON A.SchoolID = B.SchoolID AND A.sSellID = B.nSell AND  ISNULL(B.cDel,0) = 0 
LEFT JOIN TProduct AS C ON C.SchoolID = B.SchoolID AND B.nProduct = C.nProductID
LEFT JOIN TType AS T1 ON T1.nTypeID = C.nType AND T1.SchoolID = C.SchoolID
LEFT JOIN TShop AS S1 ON A.shop_id = S1.shop_id AND A.SchoolID = S1.SchoolID

WHERE A.SchoolID = {3} 
AND ( ISNULL( A.UpdatedTime ,A.dSell) BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  OR A.dayCancal BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}' )

", "JabjaiSchoolHistory", search.dStart, search.dEnd, SchoolID);

                if (search.sHopID.HasValue) SQL += " AND A.shop_id = " + search.sHopID;
                if (search.nTypeID.HasValue) SQL += " AND T1.nTypeID = " + search.nTypeID;

                q.AddRange(entities.Database.SqlQuery<SellDetail_Data>(SQL).ToList());
            }

            return q;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void export_data(Search search) /*Excel_reports_detail*/
        {
            var report = reports_data(search) as header_reports;
            using(JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("รายงานแบบรวมรายการสินค้าสินค้า");
                var worksheet = excel.Workbook.Worksheets["รายงานแบบรวมรายการสินค้าสินค้า"];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                
                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                var tToday = DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th"));
                var tNow = DateTime.Now.ToString("HH:mm:ss");

                SetHeader(worksheet, "A1:J1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A2:J2", true, report.header_text, 12, ExcelHorizontalAlignment.Center);

                SetHeader(worksheet, "A3:I3", true, "พิมพ์วันที่ :", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, "H3:J3", true, tToday, null, ExcelHorizontalAlignment.Left);

                SetHeader(worksheet, "A4:I4", true, "เวลา :", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, "H4:J4", true, tNow, null, ExcelHorizontalAlignment.Left);

                SetHeader(worksheet, "A5:J5", true, "", null, ExcelHorizontalAlignment.Center);

                string[] strHeader = { "ลำดับ", "ชื่อร้าน", "ประเภท", "Barcode", "ชื่อสินค้า", "จำนวน/ชิ้น", "ต้นทุน" };
                int Columuns = 1;
                foreach (string str in strHeader)
                {
                    SetTableHeader(worksheet.Cells[6, Columuns, 7, Columuns], true, str, ExcelHorizontalAlignment.Center);
                    Columuns++;
                }

                SetTableHeader(worksheet.Cells[6, 8, 6, 9], true, "ยอดขาย", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[7, 8], false, "ระบบภายในโรงเรียน", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[7, 9], false, "QR Code พร้อมเพย์", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[6, 10, 7, 10], true, "กำไร", ExcelHorizontalAlignment.Center);

                int Rows = 8;
                int Index = 1;
                int sum_amount = 0;
                Decimal sum_price1 = 0;
                Decimal sum_price2 = 0;
                Decimal sum_cost = 0;
                Decimal sum_profit = 0;
                foreach (var dataSell in report.report_Details)
                {
                    if (dataSell.products.Count() == 0) continue;
                    int rowsEnd = Rows + (dataSell.products.Count() - 1);
                    SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], true, string.Format("{0:#,0}", Index), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 2, rowsEnd, 2], true, dataSell.sHopName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 3, rowsEnd, 3], true, dataSell.pTypeName, ExcelHorizontalAlignment.Center);

                    int amount = 0;
                    Decimal price1 = 0;
                    Decimal price2 = 0;
                    Decimal cost = 0;
                    Decimal profit = 0;

                    foreach (var productData in dataSell.products)
                    {
                        SetTableRows(worksheet.Cells[Rows, 4], false, productData.pRoductBarCode, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 5], false, productData.pRoductName, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 6], false, string.Format("{0:#,0}", productData.pSumAmount), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 7], false, string.Format("{0:#,##0.00}", productData.pSumCost), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 8], false, string.Format("{0:#,##0.00}", productData.pSumPrice1), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 9], false, string.Format("{0:#,##0.00}", productData.pSumPrice2), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 10], false, string.Format("{0:#,##0.00}", productData.pSumProfit), ExcelHorizontalAlignment.Center);
                        Rows += 1;

                        amount += productData.pSumAmount ?? 0;
                        price1 += productData.pSumPrice1 ?? 0;
                        price2 += productData.pSumPrice2 ?? 0;
                        cost += productData.pSumCost ?? 0;
                        profit += productData.pSumProfit ?? 0;
                    }

                    SetTableRows(worksheet.Cells[Rows, 1, Rows, 3], true, "รวม " + dataSell.pTypeName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 4], false, "", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 5], false, "", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 6], false, string.Format("{0:#,0}", amount), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 7], false, string.Format("{0:#,0}", cost), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 8], false, string.Format("{0:#,##0.00}", price1), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 9], false, string.Format("{0:#,##0.00}", price2), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 10], false, string.Format("{0:#,##0.00}", profit), ExcelHorizontalAlignment.Center);
                    Rows += 1;

                    Index++;

                    sum_amount += dataSell.products.Sum(s => s.pSumAmount) ?? 0;
                    sum_cost += dataSell.products.Sum(s => s.pSumCost) ?? 0;
                    sum_price1 += dataSell.products.Sum(s => s.pSumPrice1) ?? 0;
                    sum_price2 += dataSell.products.Sum(s => s.pSumPrice2) ?? 0;
                    sum_profit += dataSell.products.Sum(s => s.pSumProfit) ?? 0;
                }

                //SetTableFooter(worksheet.Cells[Rows, 1, Rows, 6], true, "จำนวนทั้งหมด", ExcelHorizontalAlignment.Right);
                //SetTableFooter(worksheet.Cells[Rows, 7, Rows++, 8], true, string.Format("{0:#,0}", sum_amount), ExcelHorizontalAlignment.Center);

                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 5], true, "ยอดขายทั้งหมด", ExcelHorizontalAlignment.Right);
                SetTableFooter(worksheet.Cells[Rows, 6], false, string.Format("{0:#,##0.00}", sum_amount), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 7], false, string.Format("{0:#,##0.00}", sum_cost), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 8], false, string.Format("{0:#,##0.00}", sum_price1), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 9], false, string.Format("{0:#,##0.00}", sum_price2), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 10], false, string.Format("{0:#,##0.00}", sum_profit), ExcelHorizontalAlignment.Center);
                Rows += 1;

                worksheet.Cells.AutoFitColumns();
                worksheet.Column(1).Width = 7;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 15;
                worksheet.Column(4).Width = 15;
                worksheet.Column(5).Width = 40;
                worksheet.Column(6).Width = 15;
                worksheet.Column(7).Width = 24;
                worksheet.Column(8).Width = 24;
                worksheet.Column(9).Width = 24;
                worksheet.Column(10).Width = 24;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                HttpContext.Current.Response.ContentType = "application/text";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void export02_data(Search search) /*Excel_reports_data*/
        {
            var report = reports_data(search) as header_reports;
            using(JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("รายงานแบบรวมรายการสินค้าสินค้า");

                var worksheet = excel.Workbook.Worksheets["รายงานแบบรวมรายการสินค้าสินค้า"];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                
                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                SetHeader(worksheet, "A1:G1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A2:G2", true, report.header_text, 12, ExcelHorizontalAlignment.Center);

                SetHeader(worksheet, "A3:F3", true, "พิมพ์วันที่ :", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, "G3:G3", true, DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")), null, ExcelHorizontalAlignment.Left);

                SetHeader(worksheet, "A4:F4", true, "เวลา :", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, "G4:G4", true, DateTime.Now.ToString("HH:mm:ss"), null, ExcelHorizontalAlignment.Left);

                SetHeader(worksheet, "A5:G5", true, "", null, ExcelHorizontalAlignment.Center);

                string[] strHeader = { "ลำดับ", "วันที่", "จำนวน/ชิ้น", "ต้นทุน" };
                int Columuns = 1;
                foreach (string str in strHeader)
                {
                    SetTableHeader(worksheet.Cells[6, Columuns, 7, Columuns], true, str, ExcelHorizontalAlignment.Center);
                    Columuns++;
                }

                SetTableHeader(worksheet.Cells[6, 5, 6, 6], true, "ยอดขาย", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[7, 5], false, "ระบบภายในโรงเรียน", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[7, 6], false, "QR Code พร้อมเพย์", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[6, 7, 7, 7], true, "กำไร", ExcelHorizontalAlignment.Center);

                int Rows = 8; /*ข้อมูลเริ่มแถวที่8*/
                int Index = 1;
                var reportData = report.data as List<views01>;
                foreach (var dataSell in reportData)
                {
                    SetTableRows(worksheet.Cells[Rows, 1], false, string.Format("{0:#,0}", Index++), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 2], false, dataSell.lable, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 3], false, string.Format("{0:#,0}", dataSell.pAmount), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 4], false, string.Format("{0:#,##0.00}", dataSell.nCost), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 5], false, string.Format("{0:#,##0.00}", dataSell.pPrice1), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 6], false, string.Format("{0:#,##0.00}", dataSell.pPrice2), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 7], false, string.Format("{0:#,##0.00}", dataSell.nProfit), ExcelHorizontalAlignment.Center);
                    Rows++;
                }

                //SetTableFooter(worksheet.Cells[Rows, 1, Rows, 3], true, "จำนวนทั้งหมด", ExcelHorizontalAlignment.Right);
                //SetTableFooter(worksheet.Cells[Rows, 4, Rows++, 5], true, string.Format("{0:#,0}", reportData.Sum(s => s.pAmount)), ExcelHorizontalAlignment.Center);

                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 2], true, "ยอดขายทั้งหมด", ExcelHorizontalAlignment.Right);
                SetTableFooter(worksheet.Cells[Rows, 3], false, string.Format("{0:#,0}", reportData.Sum(s => s.pAmount)), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 4], false, string.Format("{0:#,##0.00}", reportData.Sum(s => s.nCost)), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 5], false, string.Format("{0:#,##0.00}", reportData.Sum(s => s.pPrice1)), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 6], false, string.Format("{0:#,##0.00}", reportData.Sum(s => s.pPrice2)), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 7], false, string.Format("{0:#,##0.00}", reportData.Sum(s => s.nProfit)), ExcelHorizontalAlignment.Center);
                //SetTableFooter(worksheet.Cells[Rows, 4], false, string.Format("{0:#,##0.00}", reportData.Sum(s => s.pPrice1)), ExcelHorizontalAlignment.Center);
                //SetTableFooter(worksheet.Cells[Rows++, 5], false, string.Format("{0:#,##0.00}", reportData.Sum(s => s.pPrice2)), ExcelHorizontalAlignment.Center);

                worksheet.Cells.AutoFitColumns();
                worksheet.Row(6).Height = 25;
                worksheet.Column(1).Width = 7;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 20;
                worksheet.Column(5).Width = 20;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/text";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void export03_data(Search search)
        {
            var report = reports_data(search) as header_reports;
            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("รายงานแบบรวมรายการสินค้าสินค้า");
                var worksheet = excel.Workbook.Worksheets["รายงานแบบรวมรายการสินค้าสินค้า"];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
               
                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                var tToday = DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th"));
                var tNow = DateTime.Now.ToString("HH:mm:ss");

                SetHeader(worksheet, "A1:J1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A2:J2", true, report.header_text, 12, ExcelHorizontalAlignment.Center);

                SetHeader(worksheet, "A3:I3", true, "พิมพ์วันที่ :", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, "J3:J3", true, tToday, null, ExcelHorizontalAlignment.Left);

                SetHeader(worksheet, "A4:I4", true, "เวลา :", null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, "J4:J4", true, tNow, null, ExcelHorizontalAlignment.Left);
                SetHeader(worksheet, "A5:J5", true, "", null, ExcelHorizontalAlignment.Center);

                string[] strHeader = { "ลำดับ", "ชื่อร้าน", "ประเภท", "Barcode", "ชื่อสินค้า", "จำนวน/ชิ้น", "ต้นทุน" };
                int Columuns = 1;
                foreach (string str in strHeader)
                {
                    SetTableHeader(worksheet.Cells[6, Columuns, 7, Columuns], true, str, ExcelHorizontalAlignment.Center);
                    Columuns++;
                }

                SetTableHeader(worksheet.Cells[6, 8, 6, 9], true, "ยอดขาย/บาท", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[7, 8], false, "ระบบภายในโรงเรียน", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[7, 9], false, "QR Code พร้อมเพย์", ExcelHorizontalAlignment.Center);
                SetTableHeader(worksheet.Cells[6, 10, 7, 10], true, "กำไร", ExcelHorizontalAlignment.Center);

                int Rows = 8;
                int Index = 1;
                int sum_amount = 0;
                Decimal sum_price1 = 0;
                Decimal sum_price2 = 0;
                Decimal sum_cost = 0;
                Decimal sum_profit = 0;
                foreach (var dataSell in report.report_Details)
                {
                    if (dataSell.products.Count() == 0) continue;
                    int rowsEnd = Rows + (dataSell.products.Count() - 1);
                    SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], true, string.Format("{0:#,0}", Index), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 2, rowsEnd, 2], true, dataSell.sHopName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 3, rowsEnd, 3], true, dataSell.pTypeName, ExcelHorizontalAlignment.Center);

                    int amount = 0;
                    Decimal price1 = 0;
                    Decimal price2 = 0;
                    Decimal cost = 0;
                    Decimal profit = 0;

                    foreach (var productData in dataSell.products)
                    {
                        SetTableRows(worksheet.Cells[Rows, 4], false, productData.pRoductBarCode, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 5], false, productData.pRoductName, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 6], false, string.Format("{0:#,0}", productData.pSumAmount), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 7], false, string.Format("{0:#,##0.00}", productData.pSumCost), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 8], false, string.Format("{0:#,##0.00}", productData.pSumPrice1), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 9], false, string.Format("{0:#,##0.00}", productData.pSumPrice2), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 10], false, string.Format("{0:#,##0.00}", productData.pSumProfit), ExcelHorizontalAlignment.Center);
                        Rows += 1;

                        amount += productData.pSumAmount ?? 0;
                        price1 += productData.pSumPrice1 ?? 0;
                        price2 += productData.pSumPrice2 ?? 0;
                        cost += productData.pSumCost ?? 0;
                        profit += productData.pSumProfit ?? 0;
                    }
                    Index++;



                    SetTableRows(worksheet.Cells[Rows, 1, Rows, 3], true, "รวม " + dataSell.pTypeName, ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 4], false, "", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 5], false, "", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 6], false, string.Format("{0:#,0}", amount), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 7], false, string.Format("{0:#,##0.00}", cost), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 8], false, string.Format("{0:#,##0.00}", price1), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 9], false, string.Format("{0:#,##0.00}", price2), ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 10], false, string.Format("{0:#,##0.00}", profit), ExcelHorizontalAlignment.Center);

                    Rows += 1;

                    sum_amount += dataSell.products.Sum(s => s.pSumAmount) ?? 0;
                    sum_cost += dataSell.products.Sum(s => s.pSumCost) ?? 0;
                    sum_price1 += dataSell.products.Sum(s => s.pSumPrice1) ?? 0;
                    sum_price2 += dataSell.products.Sum(s => s.pSumPrice2) ?? 0;
                    sum_profit += dataSell.products.Sum(s => s.pSumProfit) ?? 0;

                }

                //SetTableFooter(worksheet.Cells[Rows, 1, Rows, 6], true, "จำนวนทั้งหมด", ExcelHorizontalAlignment.Right);
                //SetTableFooter(worksheet.Cells[Rows, 7, Rows++, 8], true, string.Format("{0:#,0}", sum_amount), ExcelHorizontalAlignment.Center);

                //SetTableFooter(worksheet.Cells[Rows, 1, Rows, 6], true, "ยอดขายทั้งหมด", ExcelHorizontalAlignment.Right);
                //SetTableFooter(worksheet.Cells[Rows, 7, Rows, 7], true, string.Format("{0:#,##0.00}", sum_price1), ExcelHorizontalAlignment.Center);
                //SetTableFooter(worksheet.Cells[Rows, 8, Rows++, 8], true, string.Format("{0:#,##0.00}", sum_price2), ExcelHorizontalAlignment.Center);

                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 5], true, "ยอดขายทั้งหมด", ExcelHorizontalAlignment.Right);
                SetTableFooter(worksheet.Cells[Rows, 6], false, string.Format("{0:#,##0.00}", sum_amount), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 7], false, string.Format("{0:#,##0.00}", sum_cost), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 8], false, string.Format("{0:#,##0.00}", sum_price1), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 9], false, string.Format("{0:#,##0.00}", sum_price2), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 10], false, string.Format("{0:#,##0.00}", sum_profit), ExcelHorizontalAlignment.Center);

                worksheet.Cells.AutoFitColumns();
                worksheet.Column(1).Width = 7;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 15;
                worksheet.Column(4).Width = 15;
                worksheet.Column(5).Width = 40;
                worksheet.Column(6).Width = 15;
                worksheet.Column(7).Width = 24;
                worksheet.Column(8).Width = 24;
                worksheet.Column(9).Width = 24;
                worksheet.Column(10).Width = 24;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                HttpContext.Current.Response.ContentType = "application/text";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
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


        protected void ddshop_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddntype_id.Items.Clear();
            //int stateId = int.Parse(ddshop_id.SelectedItem.Value);
            //if (stateId > 0)
            //{
            //    BindDropDownList(stateId);
            //    ddntype_id.Enabled = true;
            //}
        }


        private void BindDropDownList(int value)
        {
            //JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            //var _listslv2 = _db.TTypes;
            //ddntype_id.Items.Add(new ListItem("ทั้งหมด", "0"));
            //foreach (var DataLV2 in _listslv2.Where(w => w.shop_id == value))
            //{
            //    ddntype_id.Items.Add(new ListItem(DataLV2.sType.ToString(), DataLV2.nTypeID.ToString()));
            //}
        }


        public class Q1
        {
            public DateTime dSell { get; set; }
            public decimal? nPrice { get; set; }
            public decimal? nPrice1 { get; set; }
            public decimal? nPrice2 { get; set; }
            public int? nNumber { get; set; }
            public int? nProduct { get; set; }
            public int nSell { get; set; }
            public decimal? nCost { get; set; }
            public int? nType { get; set; }
            public object sProduct { get; internal set; }
            public object nProductID { get; internal set; }
            public object sBarCode { get; internal set; }
        }

        public class header_reports
        {
            public string header_text { get; set; }
            public object data { get; set; }
            public int report_type { get; set; }
            public List<report_detail> report_Details { get; set; }
        }
        public class views01
        {
            public string lable { get; set; }
            public string values { get; set; }
            public decimal? pPrice1 { get; set; }
            public decimal? pPrice2 { get; set; }
            public int? pAmount { get; set; }
            public decimal? nCost { get; internal set; }
            public decimal? nProfit { get; internal set; }
        }

        public class views02
        {
            public List<report_detail> report_Details { get; set; }
            public string header_text { get; set; }
        }
        public class report_detail
        {
            public int? pTypeID { get; set; }
            public string pTypeName { get; set; }
            public string pBarCode { get; set; }
            public int? sHopID { get; set; }
            public string sHopName { get; set; }
            public int? pAmount { get; set; }
            public List<product> products { get; set; }
        }
        public class product
        {
            //public decimal? pSumPrice { get; set; }

            public decimal? pSumPrice1 { get; set; }
            public decimal? pSumPrice2 { get; set; }

            public int? pSumAmount { get; set; }
            public string pRoductName { get; set; }
            public int? pRoductID { get; set; }
            public decimal? pPrice { get; set; }
            public int? pAmount { get; set; }
            public string pRoductBarCode { get; set; }
            public decimal? pSumCost { get; internal set; }
            public decimal? pSumProfit { get; internal set; }
        }

        private class SellDetail_Data
        {
            public DateTime? dSell { get; set; }
            //public decimal? nPrice { get; set; }
            public decimal? nPrice1 { get; set; }
            public decimal? nPrice2 { get; set; }
            public decimal? deduct1 { get; set; }
            public decimal? deduct2 { get; set; }
            public int? nNumber { get; set; }
            public int? nProductID { get; set; }
            public int? nSell { get; set; }
            public string sProduct { get; set; }
            public string sBarCode { get; set; }
            public string sType { get; set; }
            public int? nTypeID { get; set; }
            public int? shop_id { get; set; }
            public string shop_name { get; set; }

            public decimal? nCost { get; set; }
            //public decimal? profit
            //{
            //    get
            //    {
            //        return nNumber * ((nPrice1 ?? 0) + (nPrice2 ?? 0) - (nCost ?? 0));
            //    }
            //}
        }

        public class day_data
        {
            public string lable { get; set; }
            public DateTime values { get; set; }
        }

        public class Search
        {
            public int sort_type { get; set; }
            public string type { get; set; }
            public DateTime? dStart { get; set; }
            public DateTime? dEnd { get; set; }
            public int? nTypeID { get; set; }
            public int? sHopID { get; set; }
        }

        private class ModelDate
        {
            public ModelDate()
            {
            }

            public int SchoolID { get; set; }
            public string TimeStart { get; set; }
            public string TimeEnd { get; set; }
        }
    }
}