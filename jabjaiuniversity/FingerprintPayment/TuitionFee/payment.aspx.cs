using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using PeakengineAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.TuitionFee
{
    public partial class payment : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            Button1.Click += new EventHandler(Button1_Click);   
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                dgd.DataBound += (s, Event) =>
                {
                    pagefunction.GridView_Bound(dgd, "PageDropDownList", "PageDropDownList2", "CurrentPageLabel");
                };

                if (!IsPostBack)
                {
                    Opendata();
                    int sEmpID = int.Parse(Session["sEmpID"] + "");
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Opendata();
        }

        private void Opendata()
        {
            dgd.DataSource = returnlist();
            dgd.PageSize = 999;
            dgd.DataBind();
        }

        protected List<payment_list> returnlist()
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                string sEntities = userData.Entities;
                var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                string Search = string.IsNullOrEmpty(ViewState["Search"] + "") ? "" : ViewState["Search"] + "";

                int counter = 1;
                var q = (from a in peakengine.Products.Where(w => w.school_id == qcompany.nCompany).ToList()
                         where a.Del == null && a.sPayment.Contains(Search)
                         select new payment_list
                         {
                             index = counter++,
                             payment_id = a.nPaymentID,
                             payment_name = a.sPayment,
                             price = a.Pirce,
                             productCode = a.productId
                         }).ToList();

                return q;
            }
        }

        public void Button1_Click(Object sender, EventArgs e)
        {
            ViewState["Search"] = txtSearch.Text;
            Opendata();
        }

        public void nextbutton_Click(Object sender, EventArgs e)
        {
            pagefunction.Nextpage(dgd, returnlist());
        }

        public void backbutton_Click(Object sender, EventArgs e)
        {
            pagefunction.Backpage(dgd, returnlist());
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {
            pagefunction.changepage(dgd, returnlist(), "PageDropDownList");
        }

        protected void PageDropDownList_SelectedIndexChanged2(Object sender, EventArgs e)
        {
            pagefunction.changesizepage(dgd, returnlist(), "PageDropDownList2");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Payment_data getdata(int payment_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == userData.Entities);
                using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                {
                    var q = (from a in peakengine.Products.ToList()
                             where a.nPaymentID == payment_id
                             select new Payment_data
                             {
                                 payment_id = a.nPaymentID,
                                 payment_name = a.sPayment,
                                 price = a.Pirce,
                                 productCode = a.productId,
                                 productType = a.ProductType
                             }).FirstOrDefault();

                    return q;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string updatedata(Payment_data payment_data)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                string entities = userData.Entities;
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                //string connectId = ConfigurationManager.AppSettings["connectId"];
                //string password = ConfigurationManager.AppSettings["password"];
                //var qXML = ConfigPeakenineAPI.GetToken(qcompany.nCompany, connectId, password);

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
                {
                    PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read);
                    if (payment_data.payment_id == 0)
                    {
                        payment_data.payment_id = peakengine.Products.Count() == 0 ? 1 : peakengine.Products.Max(max => max.nPaymentID + 1);
                        string product_code = string.Format("P{0:00000}", payment_data.payment_id);
                        //var result = Send_ProductData(qcompany, payment_data, connectId, password, qXML.Client_Token, qXML.User_Token);

                        //string productId = result.id;

                        peakengine.Products.Add(new MasterEntity.Product
                        {
                            nPaymentID = payment_data.payment_id,
                            sPayment = payment_data.payment_name,
                            Pirce = payment_data.price,
                            //productId = productId,
                            school_id = qcompany.nCompany,
                            productId = payment_data.productCode,
                            ProductType = payment_data.productType
                        });
                    }
                    else
                    {
                        var q = peakengine.Products.Find(payment_data.payment_id);
                        q.sPayment = payment_data.payment_name;
                        q.Pirce = payment_data.price;
                        q.productId = payment_data.productCode;
                        q.ProductType = payment_data.productType;

                        //if (string.IsNullOrEmpty(q.productId))
                        //{
                        //    var result = Send_ProductData(qcompany, payment_data, connectId, password, qXML.Client_Token, qXML.User_Token);
                        //    q.productId = result.id;
                        //}
                        //else
                        //{

                        //    var resultGet = ProductsAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, q.productId);
                        //    if (resultGet.peakProducts.products == null)
                        //    {
                        //        var result = Send_ProductData(qcompany, payment_data, connectId, password, qXML.Client_Token, qXML.User_Token);
                        //        q.productId = result.id;
                        //    }
                        //}
                    }
                    peakengine.SaveChanges();
                }
                return "Success";
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string delete(int payment_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                //string entities = "JabJaiEntities";
                string entities = userData.Entities;
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
                {
                    using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                    {
                        var _c = (from a in peakengine.TInvoices
                                  join b in peakengine.TInvoices_Detail on a.invoices_Id equals b.invoices_Id
                                  where (a.isDel ?? false) == false && b.IsDelete == false && b.nPaymentID == payment_id
                                  select b).Count();

                        if (_c == 0)
                        {
                            var f_product = peakengine.Products.FirstOrDefault(f => f.nPaymentID == payment_id);
                            if (f_product != null) f_product.Del = true;
                            peakengine.SaveChanges();
                            return "Success";
                        }
                        else
                        {
                            return "Faill";
                        }
                    }
                }
            }
        }


        private static ProductsAPI.products Send_ProductData(TCompany qcompany, Payment_data payment_data, string connectId, string password, string Client_Token, string User_Token)
        {
            return ProductsAPI.SendRequest(connectId, password, Client_Token, User_Token, new List<ProductsAPI.products>()
            {
                new ProductsAPI.products
                {
                    name = payment_data.payment_name,
                    sellValue = payment_data.price,
                    sellVatType = 1,
                    purchaseValue = 0,
                    purchaseVattype = 0,
                    description = "",
                    carryingBalanceValue = 0,
                    carryingBalanceAmount = 0
                }
            }).products.FirstOrDefault();
        }

        public class Payment_data
        {
            public int payment_id { get; set; }
            public string payment_name { get; set; }
            public decimal? price { get; set; }
            public string productCode { get; set; }
            public int? productType { get; set; }
        }

        protected class payment_list
        {
            public int index { get; set; }
            public int payment_id { get; set; }
            public string payment_name { get; set; }
            public decimal? price { get; set; }
            public string productCode { get; set; }
        }
    }
}