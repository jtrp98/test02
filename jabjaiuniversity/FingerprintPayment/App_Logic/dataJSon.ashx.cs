using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JabjaiMainClass;
using JabjaiEntity.DB;
using Newtonsoft.Json.Linq;
using System.Data;
using MasterEntity;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for dataJSON1
    /// </summary>
    public class dataJSon : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiEntities db = new JabJaiEntities();
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));
            dynamic rss = new JObject();
            string sMode = fcommon.ReplaceInjection(context.Request["mode"]);
            string id = fcommon.ReplaceInjection(context.Request["id"]);
            string term = fcommon.ReplaceInjection(context.Request["term"]);
            int nTermSubLevel2 = 0;
            switch (sMode)
            {
                case "product":
                    #region
                    int ProductId = int.Parse(id);
                    rss = new JArray(from a in db.TProducts.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nProductID == ProductId).ToList()
                                     select new JObject(
                                         new JProperty("productid", a.nProductID),
                                         new JProperty("productname", a.sProduct),
                                         new JProperty("barCode", a.sBarCode),
                                         new JProperty("cost", a.nCost),
                                         new JProperty("price", a.nPrice),
                                         new JProperty("number", a.nNumber),
                                         new JProperty("type", a.nType),
                                         new JProperty("warn", a.nWarn),
                                         new JProperty("stock", a.cStock),
                                         new JProperty("unit", a.UnitID)
                                         ));
                    #endregion
                    break;
                case "classmember":
                    #region
                    int idlv2 = int.Parse(id);
                    var check = db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2 == idlv2 && w.nTerm == term).FirstOrDefault();
                    int count = 0;
                    if (check.nTeacherHeadid != null)
                        count = count + 1;
                    if (check.nTeacherAssistOne != null)
                        count = count + 1;
                    if (check.nTeacherAssistTwo != null)
                        count = count + 1;
                    if (count == 3)
                    {
                        rss = new JArray(from a in db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID)
                                .Where(w => w.nTermSubLevel2 == idlv2 && w.nTerm == term).ToList()
                                         join b in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherHeadid equals b.sEmp
                                         join c in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherAssistOne equals c.sEmp
                                         join d in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherAssistTwo equals d.sEmp
                                         select new JObject(
                                             new JProperty("head1", b.sName + " " + b.sLastname),
                                             new JProperty("headid", a.nTeacherHeadid),
                                             new JProperty("one", c.sName + " " + c.sLastname),
                                             new JProperty("oneid", a.nTeacherAssistOne),
                                             new JProperty("two", d.sName + " " + d.sLastname),
                                             new JProperty("twoid", a.nTeacherAssistTwo)
                                         ));
                    }
                    if (count == 2)
                    {
                        if (check.nTeacherAssistOne != null && check.nTeacherAssistTwo != null)
                        {
                            rss = new JArray(from a in db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID)
                                    .Where(w => w.nTermSubLevel2 == idlv2 && w.nTerm == term).ToList()
                                             join c in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherAssistOne equals c.sEmp
                                             join d in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherAssistTwo equals d.sEmp
                                             select new JObject(
                                                 new JProperty("one", c.sName + " " + c.sLastname),
                                                 new JProperty("oneid", a.nTeacherAssistOne),
                                                 new JProperty("two", d.sName + " " + d.sLastname),
                                                 new JProperty("twoid", a.nTeacherAssistTwo)
                                             ));
                        }
                        if (check.nTeacherHeadid != null && check.nTeacherAssistTwo != null)
                        {
                            rss = new JArray(from a in db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID)
                                    .Where(w => w.nTermSubLevel2 == idlv2 && w.nTerm == term).ToList()
                                             join b in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherHeadid equals b.sEmp
                                             join d in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherAssistTwo equals d.sEmp
                                             select new JObject(
                                                 new JProperty("head1", b.sName + " " + b.sLastname),
                                                 new JProperty("headid", a.nTeacherHeadid),
                                                 new JProperty("two", d.sName + " " + d.sLastname),
                                                 new JProperty("twoid", a.nTeacherAssistTwo)
                                             ));
                        }
                        if (check.nTeacherHeadid != null && check.nTeacherAssistOne != null)
                        {
                            rss = new JArray(from a in db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID)
                                    .Where(w => w.nTermSubLevel2 == idlv2 && w.nTerm == term).ToList()
                                             join b in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherHeadid equals b.sEmp
                                             join c in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherAssistOne equals c.sEmp
                                             select new JObject(
                                                 new JProperty("head1", b.sName + " " + b.sLastname),
                                                 new JProperty("headid", a.nTeacherHeadid),
                                                 new JProperty("one", c.sName + " " + c.sLastname),
                                                 new JProperty("oneid", a.nTeacherAssistOne)
                                             ));
                        }
                    }
                    if (count == 1)
                    {
                        if (check.nTeacherHeadid != null)
                        {
                            rss = new JArray(from a in db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID)
                                    .Where(w => w.nTermSubLevel2 == idlv2 && w.nTerm == term).ToList()
                                             join b in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherHeadid equals b.sEmp
                                             select new JObject(
                                                 new JProperty("head1", b.sName + " " + b.sLastname),
                                                 new JProperty("headid", a.nTeacherHeadid)
                                             ));
                        }
                        if (check.nTeacherAssistOne != null)
                        {
                            rss = new JArray(from a in db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID)
                                    .Where(w => w.nTermSubLevel2 == idlv2 && w.nTerm == term).ToList()
                                             join c in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherAssistOne equals c.sEmp
                                             select new JObject(
                                                 new JProperty("one", c.sName + " " + c.sLastname),
                                                 new JProperty("oneid", a.nTeacherAssistOne)
                                             ));
                        }
                        if (check.nTeacherAssistTwo != null)
                        {
                            rss = new JArray(from a in db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID)
                                    .Where(w => w.nTermSubLevel2 == idlv2 && w.nTerm == term).ToList()
                                             join d in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.nTeacherAssistTwo equals d.sEmp
                                             select new JObject(
                                                 new JProperty("two", d.sName + " " + d.sLastname),
                                                 new JProperty("twoid", a.nTeacherAssistTwo)
                                             ));
                        }
                    }

                    #endregion
                    break;
                case "productlist":
                    int ProductType = 0;
                    List<TProduct> productlist = db.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList();
                    if (!string.IsNullOrEmpty(id))
                    {
                        ProductType = int.Parse(id);
                        productlist = productlist.Where(w => w.nType == ProductType).ToList();
                    }
                    rss = new JArray(from a in productlist
                                     select new JObject(
                                         new JProperty("value", a.nProductID),
                                         new JProperty("label", a.sProduct)
                                         ));
                    break;
                case "getpassword":
                    using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        int userid = int.Parse(context.Request["userid"]);
                        string type = context.Request["type"];
                        var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                        var tUser = dbmaster.TUsers.Where(w => w.nSystemID == userid && w.cType == type && w.nCompany == tCompany.nCompany).ToList();
                        rss = new JArray(from a in tUser
                                         select new JObject(
                                             new JProperty("password", a.sPassword)
                                             ));
                    }
                    break;
                case "getlevel":
                    var q1 = QueryDataBases.SubLevel_Query.GetData(db, userData);
                    var q2 = db.TLevels.Where(w=>w.SchoolID == userData.CompanyID).ToList();
                    rss = new JArray(from aa in q2
                                     orderby aa.sortValue
                                     select new JObject
                                     {
                                         new JProperty("levelname",aa.LevelName),
                                         new JProperty("levelid",aa.LevelID),
                                         new JProperty("sublevel",(from a in  QueryDataBases.SubLevel_Query.GetData(db,aa.LevelID,userData)
                                                                   select new JObject
                                                                   {
                                                                       new JProperty("sublevelid",a.class_id),
                                                                       new JProperty("sublevelname",a.class_name),
                                                                       new JProperty("sublevel2",(from b in QueryDataBases.SubLevel2_Query.GetData(db,a.class_id,userData)
                                                                                                  select new JObject
                                                                                                  {
                                                                                                      new JProperty("sublevel2id", b.classRoom_id),
                                                                                                      new JProperty("sublevel2name", b.classRoom_name)
                                                                                                  }))
                                                                   }))
                                     });
                    break;
                case "getbalance":
                    int studentid = 0;
                    if (!string.IsNullOrEmpty(id))
                    {
                        studentid = int.Parse(id);
                        var q = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                 where a.sID == studentid
                                 select a).FirstOrDefault();
                        rss.money = q.nMoney.HasValue ? q.nMoney.Value : 0;
                    }
                    break;
            }

            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(rss);
            context.Response.End();
        }

        private string GetDayName(int nPlaneDay)
        {
            switch (nPlaneDay)
            {
                case 0: return "su";
                case 1: return "mo";
                case 2: return "tu";
                case 3: return "we";
                case 4: return "th";
                case 5: return "fr";
                case 6: return "sa";
                default: return "";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}