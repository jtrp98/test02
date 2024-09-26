using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PagedList;
using System.Web.Script.Services;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Services;
using FingerprintPayment.Memory;
using RestSharp;
using Newtonsoft.Json;
using static JabjaiMainClass.authentication;

namespace FingerprintPayment.shop
{
    public partial class index : System.Web.UI.Page
    {
        public ClientScriptManager clientScriptManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            //JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
            //if (!this.IsPostBack)
            //{
            //}
        }

        [WebMethod(EnableSession = true)]
        public static object returnlist(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
                {
                    dynamic rss = new JObject();
                    var qshop = new List<TShop>();
                    var q1 = new List<TShop_Employees>();
                    var q2 = new List<TEmployee>();
                    var t1 = Task.Run(async () => qshop = await dbschool.TShops.Where(w => w.del == null && w.SchoolID == userData.CompanyID).ToListAsync()).Result;
                    var t2 = Task.Run(async () => q1 = await dbschool.TShop_Employees.Where(w => w.SchoolID == userData.CompanyID).ToListAsync()).Result;
                    var t3 = Task.Run(async () => q2 = await dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToListAsync()).Result;

                    //t2.Wait();
                    //t3.Wait();

                    var q_emp = (from b in q1
                                 join c in q2 on b.sEmp equals c.sEmp
                                 select new
                                 {
                                     employees_id = c.sEmp,
                                     employees_name = c.sName + " " + c.sLastname,
                                     b.ShopID
                                 }).ToList();

                    var q = (from a in qshop
                             where a.shop_name.Contains(search.wording)
                             select a).ToList();

                    search.pageSize = search.pageSize.HasValue ? search.pageSize : 20;
                    search.pageNumber = search.pageNumber.HasValue ? search.pageNumber : 1;

                    //int index = 1;
                    //rss.DATA = new JArray(from a in q//.ToPagedList(search.pageNumber.Value, search.pageSize.Value).ToList()
                    //                      select  new 
                    //                      {
                    //                          index = 
                    //                          //new JProperty("index",((search.pageNumber-1) * search.pageSize)+index++),
                    //                          //new JProperty("shop_id", EncryptMD5.UrlTokenEncode(a.shop_id)),
                    //                          //new JProperty("shop_name",a.shop_name),
                    //                          //new JProperty("deduct",a.DeductPercent.HasValue ? a.DeductPercent+"" : "-"),
                    //                          //new JProperty("mobile",a.mobile?? true),
                    //                          //new JProperty("image", QRCodeFunction.Create(string.Format("S{0:0000}", a.shop_id),QRCoder.QRCodeGenerator.ECCLevel.H)),
                    //                          //new JProperty("employess", (from emp in q_emp
                    //                          //                            where emp.ShopID == a.shop_id
                    //                          //                            select new JObject{
                    //                          //                                new JProperty("employees_name",emp.employees_name),
                    //                          //                                new JProperty("employees_id",emp.employees_id),
                    //                          //                            }))
                    //                      });

                    //rss.FOOT = new JObject()
                    //{
                    //    new JProperty("pageSize",(q.Count() / search.pageSize) + (q.Count() % search.pageSize == 0?0:1))
                    //};

                    //return rss.ToString();
                    return new
                    {
                        data = q.Select((a, i) => new
                        {
                            index = i + 1,
                            shop_id = EncryptMD5.UrlTokenEncode(a.shop_id),
                            shop_name = a.shop_name,
                            deduct = (a.DeductPercent.HasValue ? a.DeductPercent + "" : "-"),
                            mobile = a.mobile ?? true,
                            image = QRCodeFunction.Create(string.Format("S{0:0000}", a.shop_id), QRCoder.QRCodeGenerator.ECCLevel.H),
                            employess = q_emp.Where(o => o.ShopID == a.shop_id)
                                .Select(o => new
                                {
                                    employees_name = o.employees_name,
                                    employees_id = o.employees_id,
                                })
                        })
                    };
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string get_shop(string id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int ShopID = int.Parse(EncryptMD5.UrlTokenDecode(id));
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
                {
                    dynamic rss = new JObject();
                    var q_shop = dbschool.TShops.FirstOrDefault(w => w.shop_id == ShopID && w.SchoolID == userData.CompanyID);
                    var q_emp = (from b in dbschool.TShop_Employees.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                 join c in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList() on b.sEmp equals c.sEmp
                                 where ShopID == b.ShopID && b.cDel == false
                                 select new
                                 {
                                     employees_id = c.sEmp,
                                     employees_name = c.sName + " " + c.sLastname
                                 });

                    rss = new JObject()
                    {
                        new JProperty("shop_id", EncryptMD5.UrlTokenEncode(q_shop.shop_id)),
                        new JProperty("deduct", q_shop.DeductPercent),
                        new JProperty("shop_name", q_shop.shop_name),
                        new JProperty("mobile", q_shop.mobile?? true),
                        new JProperty("employees",(from a in q_emp
                                                    select new JObject
                                                    {
                                                        new JProperty("employees_id",a.employees_id),
                                                        new JProperty("employees_name",a.employees_name),
                                                    })),
                    };

                    return rss.ToString();
                }
            }
        }

        private static string HostURL = "";

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string update_data(Shop shop)
        {
            //try
            //{
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            //int ShopID = 0;
            if (!string.IsNullOrEmpty(shop.shop_id)) shop.shop_id = EncryptMD5.UrlTokenDecode(shop.shop_id);
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                int sEmp = int.Parse(HttpContext.Current.Session["sEmpID"].ToString());
                TShop shop1 = new TShop();
                if (string.IsNullOrEmpty(shop.shop_id))
                {
                    shop1 = new TShop
                    {
                        //shop_id = ShopID,
                        shop_name = shop.shop_name,
                        dayadd = DateTime.Now,
                        DeductPercent = shop.deduct,
                        useradd = sEmp,
                        mobile = shop.mobile.ToLower() == "true",
                        SchoolID = userData.CompanyID,
                        CreatedBy = userData.UserID,
                        CreatedDate = DateTime.Now
                    };
                    //ShopID = dbschool.TShops.Select(s => s.shop_id).DefaultIfEmpty(0).Max() + 1;
                    dbschool.TShops.Add(shop1);
                    dbschool.SaveChanges();
                }
                else
                {
                    int ShopID = int.Parse(shop.shop_id);
                    shop1 = dbschool.TShops.Find(ShopID, userData.CompanyID);
                    shop1.shop_name = shop.shop_name;
                    shop1.userupdate = sEmp;
                    shop1.dayupdate = DateTime.Now;
                    shop1.DeductPercent = shop.deduct;
                    shop1.mobile = shop.mobile.ToLower() == "true";
                }

                var q_emp = dbschool.TShop_Employees.Where(w => w.ShopID == shop1.shop_id && w.SchoolID == userData.CompanyID).ToList();

                var q_del = (from a in q_emp
                             where !shop.employees.Select(s => s.employees_id).Contains(a.sEmp)
                             select a
                             ).ToList();

                List<TShop_Employees> shop_Employees = new List<TShop_Employees>();
                ///Insert Delete Item
                shop_Employees.AddRange((from a in q_del
                                         select new TShop_Employees
                                         {
                                             sEmp = a.sEmp,
                                             SchoolID = a.SchoolID,
                                             cDel = true,
                                             CreatedBy = a.CreatedBy,
                                             CreatedDate = a.CreatedDate,
                                             ShopEmpID = a.ShopEmpID,
                                             ShopID = a.ShopID,
                                             UpdatedBy = userData.UserID,
                                             UpdatedDate = DateTime.Now
                                         }).ToList());

                dbschool.TShop_Employees.AddRange(from a in shop.employees
                                                  where !q_emp.Select(s => s.sEmp).Contains(a.employees_id)
                                                  select new TShop_Employees
                                                  {
                                                      sEmp = a.employees_id,
                                                      ShopID = shop1.shop_id,
                                                      SchoolID = userData.CompanyID,
                                                      CreatedBy = userData.UserID,
                                                      CreatedDate = DateTime.Now
                                                  }).ToList();

                dbschool.TShop_Employees.RemoveRange(q_del);

                #region Update Memory

                dbschool.SaveChanges();

                ///Insert AddOrModifiy Item
                shop_Employees.AddRange((from a in dbschool.TShop_Employees
                                         where a.SchoolID == userData.CompanyID && a.ShopID == shop1.shop_id
                                         select a).ToList());


                Request_API request = new Request_API(API_Type.Payment_API);

                string Json = JsonConvert.SerializeObject(shop1);
                request.POST(Json, $"/api/shop/payment/addormodifyshop", ("addormodify shop").ToUpper(), userData.CompanyID, userData.AuthKey, userData.AuthValue);

                Json = JsonConvert.SerializeObject(shop_Employees);
                request.POST(Json, $"/api/shop/payment/addormodifytshopemployee", ("AddOrModifyt ShopEmployee").ToUpper(), userData.CompanyID, userData.AuthKey, userData.AuthValue);

                #endregion

                return "Seccuss";
            }
            //}
            //catch
            //{
            //    return "Fail";
            //}
        }

        public class Shop
        {
            public byte? deduct { get; set; }
            public string shop_name { get; set; }
            public string shop_id { get; set; }
            public string mobile { get; set; }
            public List<Employees> employees { get; set; }
        }

        public class Employees
        {
            public string employees_name { get; set; }
            public Nullable<int> employees_id { get; set; }
        }

        public class Search
        {
            public string wording { get; set; }
            public Nullable<int> pageSize { get; set; }
            public Nullable<int> pageNumber { get; set; }
        }
    }
}