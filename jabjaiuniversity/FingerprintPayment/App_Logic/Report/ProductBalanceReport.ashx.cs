using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.App_Logic.Report
{
    /// <summary>
    /// Summary description for ProductBalanceReport
    /// </summary>
    public class ProductBalanceReport : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string producttype = context.Request["producttype"];
            string productname = context.Request["productname"];
            string sEntities = HttpContext.Current.Session["sEntities"] + "";

            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                dynamic rss = new JObject();


                string SQL = @"

DECLARE @SCHOOLID INT = {0};
DECLARE @nType INT = {2};
DECLARE @WORDING VARCHAR(250) = '{1}';

SELECT AA.nProductID,AA.sProduct AS productname,AA.sBarCode AS barcode,AA.nNumber,TB.Quantity AS aumont,TB.nType
FROM TProduct AS AA 
LEFT JOIN V_TProduct TB ON AA.SchoolID = TB.SchoolID AND TB.nProductID = AA.nProductID 
WHERE AA.SchoolID = @SCHOOLID  AND ISNULL(AA.cDel,0) = 0

";
                if (!string.IsNullOrEmpty(productname))
                {
                    SQL += "AND (AA.sBarCode LIKE '%'+@WORDING+'%' OR AA.sProduct LIKE '%'+@WORDING+'%') ";
                }

                if (!string.IsNullOrEmpty(producttype))
                {
                    SQL += "AND AA.nType = @nType ";
                }

                var q = dbschool.Database.SqlQuery<TM_Product>(
                    string.Format(SQL, userData.CompanyID, productname, string.IsNullOrEmpty(producttype) ? "0" : producttype));

                rss = new JArray(from a in q
                                 orderby a.productname
                                 select new JObject {
                                     new JProperty("barcode",a.barcode),
                                     new JProperty("productname",a.productname),
                                     new JProperty("aumont", a.aumont),
                                     new JProperty("product_id",a.nProductID)
                                 });

                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                context.Response.Write(rss);
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class TM_Product
        {
            public string barcode { get; set; }
            public string productname { get; set; }
            public int? aumont { get; set; }
            public int nProductID { get; set; }
        }
    }
}