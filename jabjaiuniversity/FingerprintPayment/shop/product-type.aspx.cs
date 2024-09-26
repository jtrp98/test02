using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;
using System.Web.Script.Services;
using Newtonsoft.Json.Linq;
using PagedList;
using System.Web.Configuration;

namespace FingerprintPayment
{
    public partial class product_type : System.Web.UI.Page
    {
        public TShop shop = new TShop();
        protected void Page_Load(object sender, EventArgs e)
        {
            int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(HttpContext.Current.Request.QueryString["shop_id"]));
            if (!this.IsPostBack)
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
                {
                    shop = dbschool.TShops.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.shop_id == shop_id);
                }
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string returnlist(Search search)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(search.shop_id));
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
                {
                    int index = 1;
                    dynamic rss = new JObject();
                    var q = (from a in dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID).ToList()
                             where a.sType.Contains(search.wording) && a.shop_id == shop_id
                             && a.cDel == null
                             select new
                             {
                                 a.sType,
                                 a.nTypeID,
                                 index = index++,
                             }).ToList();

                    search.pageSize = search.pageSize.HasValue ? search.pageSize : 20;
                    search.pageNumber = search.pageNumber.HasValue ? search.pageNumber : 1;

                    rss.DATA = new JArray(from a in q.ToPagedList(search.pageNumber.Value, search.pageSize.Value).ToList()
                                          select new JObject
                                          {
                                              new JProperty("index",a.index),
                                              new JProperty("type_id",a.nTypeID),
                                              new JProperty("type_name",a.sType),
                                          });

                    rss.FOOT = new JObject()
                    {
                        new JProperty("pageSize",(q.Count() / search.pageSize) + (q.Count() % search.pageSize == 0?0:1))
                    };

                    return rss.ToString();
                }
            }
        }

        public class Search
        {
            public string wording { get; set; }
            public string shop_id { get; set; }
            public Nullable<int> pageSize { get; set; }
            public Nullable<int> pageNumber { get; set; }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string update_data(ProductType productType)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    int shop_id = int.Parse(EncryptMD5.UrlTokenDecode(productType.shop_id));
                    if (productType.nTypeID == 0)
                    {
                        //if (dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID).Count() == 0) productType.nTypeID = 1;
                        //else productType.nTypeID = dbschool.TTypes.Max(max => max.nTypeID) + 1;

                        dbschool.TTypes.Add(new TType
                        {
                            //nTypeID = productType.nTypeID,
                            sType = productType.sType,
                            shop_id = shop_id,
                            SchoolID = userData.CompanyID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now
                        });
                    }
                    else
                    {
                        var q = dbschool.TTypes.Find(productType.nTypeID, userData.CompanyID);
                        q.sType = productType.sType;
                        q.UpdatedBy = userData.UserID;
                        q.UpdatedDate = DateTime.Now;
                    }

                    dbschool.SaveChanges();
                    return "Seccuss";
                }
            }
            catch
            {
                return "Fail";
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string delete_data(int productTyppe_id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.Entities, ConnectionDB.Read)))
            {
                var q = dbschool.TTypes.Find(productTyppe_id, userData.CompanyID);
                q.cDel = "1";
                q.UpdatedBy = userData.UserID;
                q.UpdatedDate = DateTime.Now;
                dbschool.SaveChanges();
                return "Success";
            }
        }

        public class ProductType
        {
            public string sType { get; set; }
            public int nTypeID { get; set; }
            public string shop_id { get; set; }
        }
    }
}