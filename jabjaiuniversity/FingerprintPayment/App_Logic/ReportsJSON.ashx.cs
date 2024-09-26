using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.SessionState;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for ReportsJSON
    /// </summary>
    public class ReportsJSON : IHttpHandler, IRequiresSessionState
    {
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            dynamic rss = new JObject();
            string sMode = fcommon.ReplaceInjection(context.Request["mode"]);
            switch (sMode)
            {
                case "reportsbuyitem":
                    rss = ReportBuyItem(context);
                    break;
                case "reportsummoney":
                    rss = ReportSumMoney(context);
                    break;
                case "reportsummoney4topup":
                    rss = ReportSumMoney4Topup(context);
                    break;
                case "reportdetailmoney4topup":
                    rss = ReportDetailMoney4Topup(context);
                    break;
            }
            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(rss);
            context.Response.End();
        }

        private dynamic ReportSumMoney4Topup(HttpContext context)
        {
            dynamic rss = new JArray();
            string customer_type = fcommon.ReplaceInjection(context.Request["customer_type"]);
            int CustomerId = int.Parse(fcommon.ReplaceInjection(context.Request["customer_id"]));
            string user_type = fcommon.ReplaceInjection(context.Request["user_type"]);
            int EmployeesId = int.Parse(fcommon.ReplaceInjection(context.Request["employees_id"]));
            string DayStart = fcommon.ReplaceInjection(context.Request["daystart"]);
            string DayEnd = fcommon.ReplaceInjection(context.Request["dayend"]);
            string topup_type = fcommon.ReplaceInjection(context.Request["topup_type"]);
            int level_id = int.Parse(fcommon.ReplaceInjection(string.IsNullOrEmpty(context.Request["level_id"]) ? "0" : context.Request["level_id"]));
            int level2_id = int.Parse(fcommon.ReplaceInjection(string.IsNullOrEmpty(context.Request["level2_id"]) ? "0" : context.Request["level2_id"]));

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                List<TEmployee> lEmployess = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<TMoney> lTopup = dbschool.TMoneys.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.dayCancal == null).OrderByDescending(o => o.dMoney).ToList();

                if (!string.IsNullOrEmpty(DayStart) && !string.IsNullOrEmpty(DayEnd))
                {
                    DateTime _Start = DateTime.ParseExact(DayStart, "dd/MM/yyyy", new CultureInfo("EN-US"));
                    DateTime _End = DateTime.ParseExact(DayEnd, "dd/MM/yyyy", new CultureInfo("EN-US")).AddDays(1);
                    lTopup = lTopup.Where(w => w.dMoney >= _Start && w.dMoney <= _End).ToList();
                }
                else if (!string.IsNullOrEmpty(DayStart))
                {
                    DateTime _Start = DateTime.ParseExact(DayStart, "dd/MM/yyyy", new CultureInfo("EN-US"));
                    DateTime _End = _Start.AddDays(1);
                    lTopup = lTopup.Where(w => w.dMoney >= _Start && w.dMoney <= _End).ToList();
                }
                else
                {
                    DateTime _Start = DateTime.Today;
                    DateTime _End = _Start.AddDays(1);
                    lTopup = lTopup.Where(w => w.dMoney >= _Start && w.dMoney <= _End).ToList();
                }

                List<TMoney> lStudent4Topup = lTopup.Where(w => w.cType == "0").ToList();
                List<TMoney> lEmployees4Topup = lTopup.Where(w => w.cType != "0").ToList();
                List<TProduct> lProduct = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<TType> lType = dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<JabjaiEntity.DB.TUser> tUser = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList();
                List<TEmployee> tEmployess = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList();

                #region Topup Terminal
                if (topup_type == "0")
                {
                    lStudent4Topup = lStudent4Topup.Where(w => w.topup_type == "TM01" || string.IsNullOrEmpty(w.topup_type)).ToList();
                    lEmployees4Topup = lEmployees4Topup.Where(w => w.topup_type == "TM01" || string.IsNullOrEmpty(w.topup_type)).ToList();
                    //User Type
                    if (user_type == "1")
                    {
                        //Customer Type
                        if (CustomerId > 0)
                        {
                            if (customer_type == "0")
                            {
                                lStudent4Topup = lStudent4Topup.Where(w => w.sID == CustomerId).ToList();
                                lEmployees4Topup = new List<TMoney>();
                            }
                            else
                            {
                                lStudent4Topup = new List<TMoney>();
                                lEmployees4Topup = lEmployees4Topup.Where(w => w.sID == CustomerId).ToList();
                            }
                        }
                        else
                        {
                            if (customer_type == "0")
                            {
                                if (level2_id > 0)
                                {
                                    tUser = tUser.Where(w => w.nTermSubLevel2 == level2_id).ToList();
                                    lEmployees4Topup = new List<TMoney>();
                                }
                                else if (level_id > 0)
                                {
                                    tUser = tUser.Where(w => w.nTermSubLevel2 == level2_id).ToList();
                                    lEmployees4Topup = new List<TMoney>();
                                }
                            }
                        }
                    }
                }
                #endregion
                #region Topup Mobile
                else if (topup_type == "1")
                {
                    lStudent4Topup = lStudent4Topup.Where(w => w.topup_type == "MB01").ToList();
                    lEmployees4Topup = lEmployees4Topup.Where(w => w.topup_type == "MB01").ToList();
                    if (CustomerId > 0)
                    {
                        if (customer_type == "0")
                        {
                            lStudent4Topup = lStudent4Topup.Where(w => w.sID == CustomerId).ToList();
                            lEmployees4Topup = new List<TMoney>();
                        }
                        else
                        {
                            lStudent4Topup = new List<TMoney>();
                            lEmployees4Topup = lEmployees4Topup.Where(w => w.sID == CustomerId).ToList();
                        }
                    }
                    else
                    {
                        if (customer_type == "0")
                        {
                            if (level2_id > 0)
                            {
                                tUser = tUser.Where(w => w.nTermSubLevel2 == level2_id).ToList();
                                lEmployees4Topup = new List<TMoney>();
                            }
                            else if (level_id > 0)
                            {
                                tUser = tUser.Where(w => w.nTermSubLevel2 == level2_id).ToList();
                                lEmployees4Topup = new List<TMoney>();
                            }
                        }
                    }
                }
                #endregion
                #region All
                else
                {

                }
                #endregion

                var q1 = (from a in lStudent4Topup
                          join c in tUser on a.sID equals c.sID
                          join e in lEmployess on a.sEmp equals e.sEmp
                          select new
                          {
                              daytopup = a.dMoney.Date,
                              timetoup = a.dMoney.TimeOfDay,
                              customername = c.sName + " " + c.sLastname,
                              employeesnamm = e.sName + " " + e.sLastname,
                              balance = a.nBalance,
                              money = a.nMoney
                          }).ToList();

                var q2 = (from a in lEmployees4Topup
                          join c in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.sID equals c.sEmp
                          join e in lEmployess on a.sEmp equals e.sEmp into je
                          from ee in je.DefaultIfEmpty()
                          select new
                          {
                              daytopup = a.dMoney.Date,
                              timetoup = a.dMoney.TimeOfDay,
                              customername = c.sName + " " + c.sLastname,
                              employeesnamm = ee == null ? "" : ee.sName + " " + ee.sLastname,
                              balance = a.nBalance,
                              money = a.nMoney
                          }).ToList();

                var q3 = q1.Union(q2).Distinct().ToList();

                rss = new JArray(from groupdaysell in q3
                                 orderby groupdaysell.daytopup descending
                                 group groupdaysell by groupdaysell.daytopup into gb
                                 select new JObject(
                                     new JProperty("daytopup", gb.Key.ToString("dd/MM/yyyy")),
                                     new JProperty("money", gb.Sum(sum => sum.money))));
                return rss;
            }
        }

        private dynamic ReportDetailMoney4Topup(HttpContext context)
        {
            dynamic rss = new JArray();
            string customer_type = fcommon.ReplaceInjection(context.Request["customer_type"]);
            int CustomerId = int.Parse(fcommon.ReplaceInjection(context.Request["customer_id"]));
            string user_type = fcommon.ReplaceInjection(context.Request["user_type"]);
            int EmployeesId = int.Parse(fcommon.ReplaceInjection(context.Request["employees_id"]));
            string DayStart = fcommon.ReplaceInjection(context.Request["daystart"]);
            string DayEnd = fcommon.ReplaceInjection(context.Request["dayend"]);
            string topup_type = fcommon.ReplaceInjection(context.Request["topup_type"]);
            int level_id = int.Parse(fcommon.ReplaceInjection(string.IsNullOrEmpty(context.Request["level_id"]) ? "0" : context.Request["level_id"]));
            int level2_id = int.Parse(fcommon.ReplaceInjection(string.IsNullOrEmpty(context.Request["level2_id"]) ? "0" : context.Request["level2_id"]));


            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                List<TEmployee> lEmployess = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<JabjaiEntity.DB.TMoney> lTopup = dbschool.TMoneys.Where(w => w.SchoolID == userData.CompanyID && w.dayCancal == null).OrderByDescending(o => o.dMoney).ToList();

                if (!string.IsNullOrEmpty(DayStart) && !string.IsNullOrEmpty(DayEnd))
                {
                    DateTime _Start = DateTime.ParseExact(DayStart, "dd/MM/yyyy", new CultureInfo("EN-US"));
                    DateTime _End = DateTime.ParseExact(DayEnd, "dd/MM/yyyy", new CultureInfo("EN-US")).AddDays(1);
                    lTopup = lTopup.Where(w => w.dMoney >= _Start && w.dMoney <= _End).ToList();
                }
                else if (!string.IsNullOrEmpty(DayStart))
                {
                    DateTime _Start = DateTime.ParseExact(DayStart, "dd/MM/yyyy", new CultureInfo("EN-US"));
                    DateTime _End = _Start.AddDays(1);
                    lTopup = lTopup.Where(w => w.dMoney >= _Start && w.dMoney <= _End).ToList();
                }
                else
                {
                    DateTime _Start = DateTime.Today;
                    DateTime _End = _Start.AddDays(1);
                    lTopup = lTopup.Where(w => w.dMoney >= _Start && w.dMoney <= _End).ToList();
                }

                List<TMoney> lStudent4Topup = lTopup.Where(w => w.cType == "0").ToList();
                List<TMoney> lEmployees4Topup = lTopup.Where(w => w.cType != "0").ToList();
                List<TProduct> lProduct = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<TType> lType = dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<JabjaiEntity.DB.TUser> tUser = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList();
                List<TEmployee> tEmployess = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList();

                #region Topup Terminal
                if (topup_type == "0")
                {
                    lStudent4Topup = lStudent4Topup.Where(w => w.topup_type == "TM01" || string.IsNullOrEmpty(w.topup_type)).ToList();
                    lEmployees4Topup = lEmployees4Topup.Where(w => w.topup_type == "TM01" || string.IsNullOrEmpty(w.topup_type)).ToList();
                    //User Type
                    if (user_type == "1")
                    {
                        //Customer Type
                        if (CustomerId > 0)
                        {
                            if (customer_type == "0")
                            {
                                lStudent4Topup = lStudent4Topup.Where(w => w.sID == CustomerId).ToList();
                                lEmployees4Topup = new List<TMoney>();
                            }
                            else
                            {
                                lStudent4Topup = new List<TMoney>();
                                lEmployees4Topup = lEmployees4Topup.Where(w => w.sID == CustomerId).ToList();
                            }
                        }
                        else
                        {
                            if (customer_type == "0")
                            {
                                if (level2_id > 0)
                                {
                                    tUser = tUser.Where(w => w.nTermSubLevel2 == level2_id).ToList();
                                    lEmployees4Topup = new List<TMoney>();
                                }
                                else if (level_id > 0)
                                {
                                    tUser = tUser.Where(w => w.nTermSubLevel2 == level2_id).ToList();
                                    lEmployees4Topup = new List<TMoney>();
                                }
                            }
                        }
                    }
                }
                #endregion
                #region Topup Mobile
                else if (topup_type == "1")
                {
                    lStudent4Topup = lStudent4Topup.Where(w => w.topup_type == "MB01").ToList();
                    lEmployees4Topup = lEmployees4Topup.Where(w => w.topup_type == "MB01").ToList();
                    if (CustomerId > 0)
                    {
                        if (customer_type == "0")
                        {
                            lStudent4Topup = lStudent4Topup.Where(w => w.sID == CustomerId).ToList();
                            lEmployees4Topup = new List<TMoney>();
                        }
                        else
                        {
                            lStudent4Topup = new List<TMoney>();
                            lEmployees4Topup = lEmployees4Topup.Where(w => w.sID == CustomerId).ToList();
                        }
                    }
                    else
                    {
                        if (customer_type == "0")
                        {
                            if (level2_id > 0)
                            {
                                tUser = tUser.Where(w => w.nTermSubLevel2 == level2_id).ToList();
                                lEmployees4Topup = new List<TMoney>();
                            }
                            else if (level_id > 0)
                            {
                                tUser = tUser.Where(w => w.nTermSubLevel2 == level2_id).ToList();
                                lEmployees4Topup = new List<TMoney>();
                            }
                        }
                    }
                }
                #endregion
                #region All
                else
                {

                }
                #endregion

                var q1 = (from a in lStudent4Topup
                          join c in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.sID equals c.sID
                          join e in lEmployess on a.sEmp equals e.sEmp
                          select new
                          {
                              daytopup = a.dMoney.ToString("dd/MM/yyyy"),
                              timetoup = a.dMoney.ToString("HH:mm"),
                              customername = c.sName + " " + c.sLastname,
                              employeesnamm = e.sName + " " + e.sLastname,
                              balance = a.nBalance,
                              money = a.nMoney
                          }).ToList();

                var q2 = (from a in lEmployees4Topup
                          join c in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.sID equals c.sEmp
                          join e in lEmployess on a.sEmp equals e.sEmp into je
                          from ee in je.DefaultIfEmpty()
                          select new
                          {
                              daytopup = a.dMoney.ToString("dd/MM/yyyy"),
                              timetoup = a.dMoney.ToString("HH:mm"),
                              customername = c.sName + " " + c.sLastname,
                              employeesnamm = ee == null ? "" : ee.sName + " " + ee.sLastname,
                              balance = a.nBalance,
                              money = a.nMoney
                          }).ToList();

                var q3 = q1.Union(q2).Distinct().ToList();

                foreach (var groupsell in (from groupdaysell in q3
                                           group groupdaysell by groupdaysell.daytopup into gb
                                           select new { gb.Key }).ToList())
                {
                    dynamic rss2 = new JObject();
                    rss2.daytopup = groupsell.Key.ToString();
                    rss2.topupdate = new JArray(from a in q3
                                                where a.daytopup == groupsell.Key
                                                orderby a.timetoup
                                                select new JObject(
                                                new JProperty("customername", a.customername),
                                                new JProperty("employeesnamm", a.employeesnamm),
                                                new JProperty("money", a.money),
                                                new JProperty("timetoup", a.timetoup)));
                    rss.Add(rss2);
                }
                return rss;
            }
        }

        private dynamic ReportBuyItem(HttpContext context)
        {
            dynamic rss = new JArray();
            int ProductId = int.Parse(fcommon.ReplaceInjection(context.Request["productid"]));
            string customer_type = fcommon.ReplaceInjection(context.Request["customer_type"]);
            int CustomerId = int.Parse(fcommon.ReplaceInjection(context.Request["customerid"]));
            string user_type = fcommon.ReplaceInjection(context.Request["user_type"]);
            int EmployeesId = int.Parse(fcommon.ReplaceInjection(context.Request["employeesid"]));
            string DayStart = fcommon.ReplaceInjection(context.Request["daystart"]);
            string DayEnd = fcommon.ReplaceInjection(context.Request["dayend"]);
            string sell_type = fcommon.ReplaceInjection(context.Request["sell_type"]);
            int level_id = int.Parse(fcommon.ReplaceInjection(string.IsNullOrEmpty(context.Request["level_id"]) ? "0" : context.Request["level_id"]));
            int level2_id = int.Parse(fcommon.ReplaceInjection(string.IsNullOrEmpty(context.Request["level2_id"]) ? "0" : context.Request["level2_id"]));
            int product_type = int.Parse(fcommon.ReplaceInjection(string.IsNullOrEmpty(context.Request["product_type"]) ? "0" : context.Request["product_type"]));
            string product_name = fcommon.ReplaceInjection(context.Request["product_name"]);

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                List<TEmployee> lEmployess = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<TSell> lSell = dbschool.TSells.Where(w => w.SchoolID == userData.CompanyID && w.dayCancal == null).OrderByDescending(o => o.dSell).ToList();
                List<TSell_Detail> lSellDetai = dbschool.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID && (w.cDel ?? "0") == "0").ToList();
                if (!string.IsNullOrEmpty(DayStart) && !string.IsNullOrEmpty(DayEnd))
                {
                    DateTime _Start = DateTime.ParseExact(DayStart, "dd/MM/yyyy", new CultureInfo("EN-US"));
                    DateTime _End = DateTime.ParseExact(DayEnd, "dd/MM/yyyy", new CultureInfo("EN-US")).AddDays(1);
                    lSell = lSell.Where(w => w.dSell >= _Start && w.dSell <= _End).ToList();
                }
                else if (!string.IsNullOrEmpty(DayStart))
                {
                    DateTime _Start = DateTime.ParseExact(DayStart, "dd/MM/yyyy", new CultureInfo("EN-US"));
                    DateTime _End = _Start.AddDays(1);
                    lSell = lSell.Where(w => w.dSell >= _Start && w.dSell <= _End).ToList();
                }
                else
                {
                    DateTime _Start = DateTime.Today;
                    DateTime _End = _Start.AddDays(1);
                    lSell = lSell.Where(w => w.dSell >= _Start && w.dSell <= _End).ToList();
                }

                List<TSell> lSellStudent = lSell.Where(w => w.sID.HasValue).ToList();
                List<TSell> lSellEmployees = lSell.Where(w => w.sID2.HasValue).ToList();
                List<TProduct> lProduct = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<TType> lType = dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<JabjaiEntity.DB.TUser> tUser = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList();
                List<TEmployee> tEmployess = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList();

                #region 1 Display
                if (sell_type == "1")
                {
                    lSellStudent = lSellStudent.Where(w => w.sEmp == null).ToList();
                    lSellEmployees = lSellEmployees.Where(w => w.sEmp == null).ToList();
                    //Student
                    if (customer_type == "0")
                    {
                        if (CustomerId > 0)
                        {
                            lSellStudent = lSellStudent.Where(w => w.sID == CustomerId).ToList();
                        }
                        else
                        {
                            var quser = tUser.Where(w => w.nTermSubLevel2 == level2_id).Select(s => s.sID).ToList();
                            lSellStudent = lSellStudent.Where(w => quser.Contains(w.sID.Value) && w.sID.HasValue).ToList();
                        }
                        lSellEmployees = new List<TSell>();
                    }
                    //Employees
                    else if (customer_type == "1")
                    {
                        lSellStudent = new List<TSell>();
                        lSellEmployees = lSellEmployees.Where(w => w.sID2 == CustomerId).ToList();
                    }
                }
                #endregion
                #region  2 Display
                else if (sell_type == "0")
                {
                    lSellStudent = lSellStudent.Where(w => w.sEmp != null).ToList();
                    lSellEmployees = lSellEmployees.Where(w => w.sEmp != null).ToList();
                    if (user_type == "1")
                    {
                        //Student
                        if (customer_type == "0")
                        {
                            if (CustomerId > 0)
                            {
                                lSellStudent = lSellStudent.Where(w => w.sID == CustomerId).ToList();
                            }
                            else if (level2_id > 0)
                            {
                                var quser = tUser.Where(w => w.nTermSubLevel2 == level2_id).Select(s => s.sID).ToList();
                                lSellStudent = lSellStudent.Where(w => quser.Contains(w.sID.Value) && w.sID.HasValue).ToList();
                            }
                            else if (level_id > 0)
                            {
                                var qlevel2 = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTSubLevel == level_id).Select(s => s.nTermSubLevel2).ToList();
                                var quser = tUser.Where(w => qlevel2.Contains(w.nTermSubLevel2.Value)).Select(s => s.sID).ToList();
                                lSellStudent = lSellStudent.Where(w => quser.Contains(w.sID.Value) && w.sID.HasValue).ToList();
                            }
                            lSellEmployees = new List<TSell>();
                        }
                        //Employees
                        else if (customer_type == "1")
                        {
                            lSellStudent = new List<TSell>();
                            lSellEmployees = lSellEmployees.Where(w => w.sID2 == CustomerId).ToList();
                        }
                    }
                    else if (user_type == "2")
                    {
                        if (EmployeesId > 0) lSellStudent = lSellStudent.Where(w => w.sEmp == EmployeesId).ToList();
                    }
                }
                #endregion
                #region All Type
                else if (sell_type == "2")
                {

                }
                #endregion

                #region Select Product
                if (ProductId > 0)
                {
                    lSellDetai = lSellDetai.Where(w => w.nProduct == ProductId).ToList();
                    lProduct = lProduct.Where(w => w.nProductID == ProductId).ToList();
                }
                else if (product_type > 0)
                {
                    var qproduct_type = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nType == product_type).Select(s => s.nProductID).ToList();
                    lSellDetai = lSellDetai.Where(w => qproduct_type.Contains(w.nProduct.Value)).ToList();
                }
                else if (!string.IsNullOrEmpty(product_name))
                {
                    var qproduct_type = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sProduct == product_name).Select(s => s.nProductID).ToList();
                    lSellDetai = lSellDetai.Where(w => qproduct_type.Contains(w.nProduct.Value)).ToList();
                }
                #endregion

                var q1 = (from a in lSellStudent
                          join b in lSellDetai on a.sSellID equals b.nSell
                          join c in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.sID equals c.sID
                          join d in lProduct on b.nProduct equals d.nProductID
                          join e in lEmployess on a.sEmp equals e.sEmp into je
                          from ee in je.DefaultIfEmpty()
                          join type in lType on d.nType equals type.nTypeID
                          where b.nNumber > 0
                          select new
                          {
                              customername = c.sName + " " + c.sLastname,
                              tsell = a.dSell.Value.ToString("HH:mm:ss"),
                              dgsell = a.dSell.Value.ToString("dd/MM/yyyy"),
                              product = d.sProduct,
                              amount = b.nNumber,
                              total = b.nNumber * d.nPrice,
                              barcode = d.sBarCode,
                              price = d.nPrice,
                              lucre = (b.nNumber * d.nPrice) - (b.nNumber * d.nCost),
                              employeesnamm = ee == null ? "" : ee.sName + " " + ee.sLastname,
                              sbarcode = d.sBarCode,
                              cost = d.nCost,
                              producttype = type.sType
                          }).ToList();

                var q3 = (from a in lSellStudent
                          join b in tEmployess on a.sEmp equals b.sEmp
                          select new { a.sSellID, b.sName, b.sLastname }).ToList();

                var q2 = (from a in lSellEmployees
                          join b in lSellDetai on a.sSellID equals b.nSell
                          join c in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.sID2 equals c.sEmp
                          join d in lProduct on b.nProduct equals d.nProductID
                          join e in lEmployess on a.sEmp equals e.sEmp into je
                          from ee in je.DefaultIfEmpty()
                          join type in lType on d.nType equals type.nTypeID
                          where b.nNumber > 0
                          select new
                          {
                              customername = c.sName + " " + c.sLastname,
                              tsell = a.dSell.Value.ToString("HH:mm:ss"),
                              dgsell = a.dSell.Value.ToString("dd/MM/yyyy"),
                              product = d.sProduct,
                              amount = b.nNumber,
                              total = b.nNumber * d.nPrice,
                              barcode = d.sBarCode,
                              price = d.nPrice,
                              lucre = (b.nNumber * d.nPrice) - (b.nNumber * d.nCost),
                              employeesnamm = ee == null ? "" : ee.sName + " " + ee.sLastname,
                              sbarcode = d.sBarCode,
                              cost = d.nCost,
                              producttype = type.sType,
                          }).ToList();

                var CustomerBuyItem = q1.Union(q2).Distinct().ToList();

                foreach (var groupsell in (from groupdaysell in CustomerBuyItem
                                           group groupdaysell by groupdaysell.dgsell into gb
                                           select new { gb.Key }).ToList())
                {
                    dynamic rss2 = new JObject();
                    rss2.dsell = groupsell.Key.ToString();
                    rss2.selldata = new JArray(from a in CustomerBuyItem
                                               where a.dgsell == groupsell.Key
                                               orderby a.tsell
                                               select new JObject(
                                               new JProperty("customername", a.customername),
                                               new JProperty("dgsell", a.dgsell),
                                               new JProperty("sell_type", string.IsNullOrEmpty(a.employeesnamm)),
                                               new JProperty("product", a.product),
                                               new JProperty("amount", a.amount),
                                               new JProperty("total", a.total),
                                               new JProperty("barcode", a.barcode),
                                               new JProperty("price", a.price),
                                               new JProperty("lucre", a.lucre),
                                               new JProperty("employeesnamm", a.employeesnamm),
                                               new JProperty("cost", a.cost),
                                               new JProperty("sbarcode", a.sbarcode),
                                               new JProperty("producttype", a.producttype),
                                               new JProperty("tsell", a.tsell)));
                    rss.Add(rss2);
                }
                return rss;
            }
        }

        private dynamic ReportSumMoney(HttpContext context)
        {
            dynamic rss = new JArray();
            int ProductId = int.Parse(fcommon.ReplaceInjection(context.Request["productid"]));
            string customer_type = fcommon.ReplaceInjection(context.Request["customer_type"]);
            int CustomerId = int.Parse(fcommon.ReplaceInjection(context.Request["customerid"]));
            string user_type = fcommon.ReplaceInjection(context.Request["user_type"]);
            int EmployeesId = int.Parse(fcommon.ReplaceInjection(context.Request["employeesid"]));
            string DayStart = fcommon.ReplaceInjection(context.Request["daystart"]);
            string DayEnd = fcommon.ReplaceInjection(context.Request["dayend"]);
            string sell_type = fcommon.ReplaceInjection(context.Request["sell_type"]);
            int level_id = int.Parse(fcommon.ReplaceInjection(string.IsNullOrEmpty(context.Request["level_id"]) ? "0" : context.Request["level_id"]));
            int level2_id = int.Parse(fcommon.ReplaceInjection(string.IsNullOrEmpty(context.Request["level2_id"]) ? "0" : context.Request["level2_id"]));
            int product_type = int.Parse(fcommon.ReplaceInjection(string.IsNullOrEmpty(context.Request["product_type"]) ? "0" : context.Request["product_type"]));
            string product_name = fcommon.ReplaceInjection(context.Request["product_name"]);

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                List<TEmployee> lEmployess = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<TSell> lSell = dbschool.TSells.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.dayCancal == null).OrderByDescending(o => o.dSell).ToList();
                List<TSell_Detail> lSellDetai = dbschool.TSell_Detail.Where(w => w.SchoolID == userData.CompanyID && (w.cDel ?? "0") == "0").ToList();
                if (!string.IsNullOrEmpty(DayStart) && !string.IsNullOrEmpty(DayEnd))
                {
                    DateTime _Start = DateTime.ParseExact(DayStart, "dd/MM/yyyy", new CultureInfo("EN-US"));
                    DateTime _End = DateTime.ParseExact(DayEnd, "dd/MM/yyyy", new CultureInfo("EN-US")).AddDays(1);
                    lSell = lSell.Where(w => w.dSell >= _Start && w.dSell <= _End).ToList();
                }
                else if (!string.IsNullOrEmpty(DayStart))
                {
                    DateTime _Start = DateTime.ParseExact(DayStart, "dd/MM/yyyy", new CultureInfo("EN-US"));
                    DateTime _End = _Start.AddDays(1);
                    lSell = lSell.Where(w => w.dSell >= _Start && w.dSell <= _End).ToList();
                }
                else
                {
                    DateTime _Start = DateTime.Today;
                    DateTime _End = _Start.AddDays(1);
                    lSell = lSell.Where(w => w.dSell >= _Start && w.dSell <= _End).ToList();
                }

                List<TSell> lSellStudent = lSell.Where(w => w.sID.HasValue).ToList();
                List<TSell> lSellEmployees = lSell.Where(w => w.sID2.HasValue).ToList();
                List<TProduct> lProduct = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<TType> lType = dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID).ToList();
                List<JabjaiEntity.DB.TUser> tUser = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList();
                List<TEmployee> tEmployess = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList();

                #region 1 Display
                if (sell_type == "1")
                {
                    lSellStudent = lSellStudent.Where(w => w.sEmp == null).ToList();
                    lSellEmployees = lSellEmployees.Where(w => w.sEmp == null).ToList();
                    //Student
                    if (customer_type == "0")
                    {
                        if (CustomerId > 0)
                        {
                            lSellStudent = lSellStudent.Where(w => w.sID == CustomerId).ToList();
                        }
                        else
                        {
                            var quser = tUser.Where(w => w.nTermSubLevel2 == level2_id).Select(s => s.sID).ToList();
                            lSellStudent = lSellStudent.Where(w => quser.Contains(w.sID.Value) && w.sID.HasValue).ToList();
                        }
                        lSellEmployees = new List<TSell>();
                    }
                    //Employees
                    else if (customer_type == "1")
                    {
                        lSellStudent = new List<TSell>();
                        lSellEmployees = lSellEmployees.Where(w => w.sID2 == CustomerId).ToList();
                    }
                }
                #endregion
                #region  2 Display
                else if (sell_type == "0")
                {
                    lSellStudent = lSellStudent.Where(w => w.sEmp != null).ToList();
                    lSellEmployees = lSellEmployees.Where(w => w.sEmp != null).ToList();
                    if (user_type == "1")
                    {
                        //Student
                        if (customer_type == "0")
                        {
                            if (CustomerId > 0)
                            {
                                lSellStudent = lSellStudent.Where(w => w.sID == CustomerId).ToList();
                            }
                            else if (level2_id > 0)
                            {
                                var quser = tUser.Where(w => w.nTermSubLevel2 == level2_id).Select(s => s.sID).ToList();
                                lSellStudent = lSellStudent.Where(w => quser.Contains(w.sID.Value) && w.sID.HasValue).ToList();
                            }
                            else if (level_id > 0)
                            {
                                var qlevel2 = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTSubLevel == level_id).Select(s => s.nTermSubLevel2).ToList();
                                var quser = tUser.Where(w => qlevel2.Contains(w.nTermSubLevel2.Value)).Select(s => s.sID).ToList();
                                lSellStudent = lSellStudent.Where(w => quser.Contains(w.sID.Value) && w.sID.HasValue).ToList();
                            }
                            lSellEmployees = new List<TSell>();
                        }
                        //Employees
                        else if (customer_type == "1")
                        {
                            lSellStudent = new List<TSell>();
                            lSellEmployees = lSellEmployees.Where(w => w.sID2 == CustomerId).ToList();
                        }
                    }
                    else if (user_type == "2")
                    {
                        if (EmployeesId > 0) lSellStudent = lSellStudent.Where(w => w.sEmp == EmployeesId).ToList();
                    }
                }
                #endregion
                #region All Type
                else if (sell_type == "2")
                {

                }
                #endregion

                #region Select Product
                if (ProductId > 0)
                {
                    lSellDetai = lSellDetai.Where(w => w.nProduct == ProductId).ToList();
                    lProduct = lProduct.Where(w => w.nProductID == ProductId).ToList();
                }
                else if (product_type > 0)
                {
                    var qproduct_type = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nType == product_type).Select(s => s.nProductID).ToList();
                    lSellDetai = lSellDetai.Where(w => qproduct_type.Contains(w.nProduct.Value)).ToList();
                }
                else if (!string.IsNullOrEmpty(product_name))
                {
                    var qproduct_type = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sProduct == product_name).Select(s => s.nProductID).ToList();
                    lSellDetai = lSellDetai.Where(w => qproduct_type.Contains(w.nProduct.Value)).ToList();
                }
                #endregion

                var q1 = (from a in lSellStudent
                          join b in lSellDetai on a.sSellID equals b.nSell
                          join c in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.sID equals c.sID
                          join d in lProduct on b.nProduct equals d.nProductID
                          join e in lEmployess on a.sEmp equals e.sEmp into je
                          from ee in je.DefaultIfEmpty()
                          join type in lType on d.nType equals type.nTypeID
                          where b.nNumber > 0
                          select new
                          {
                              customername = c.sName + " " + c.sLastname,
                              tsell = a.dSell.Value.ToString("HH:mm:ss"),
                              dgsell = a.dSell.Value.Date,
                              product = d.sProduct,
                              amount = b.nNumber,
                              total = b.nNumber * d.nPrice,
                              barcode = d.sBarCode,
                              price = b.nNumber * d.nPrice,
                              lucre = (b.nNumber * d.nPrice) - (b.nNumber * d.nCost),
                              employeesnamm = ee == null ? "" : ee.sName + " " + ee.sLastname,
                              sbarcode = d.sBarCode,
                              cost = b.nNumber * d.nCost,
                              producttype = type.sType
                          }).ToList();

                var q2 = (from a in lSellEmployees
                          join b in lSellDetai on a.sSellID equals b.nSell
                          join c in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.sID2 equals c.sEmp
                          join d in lProduct on b.nProduct equals d.nProductID
                          join e in lEmployess on a.sEmp equals e.sEmp into je
                          from ee in je.DefaultIfEmpty()
                          join type in lType on d.nType equals type.nTypeID
                          where b.nNumber > 0
                          select new
                          {
                              customername = c.sName + " " + c.sLastname,
                              tsell = a.dSell.Value.ToString("HH:mm:ss"),
                              dgsell = a.dSell.Value.Date,
                              product = d.sProduct,
                              amount = b.nNumber,
                              total = b.nNumber * d.nPrice,
                              barcode = d.sBarCode,
                              price = d.nPrice,
                              lucre = (b.nNumber * d.nPrice) - (b.nNumber * d.nCost),
                              employeesnamm = ee == null ? "" : ee.sName + " " + ee.sLastname,
                              sbarcode = d.sBarCode,
                              cost = b.nNumber * d.nCost,
                              producttype = type.sType
                          }).ToList();

                var CustomerBuyItem = q1.Union(q2).Distinct().ToList();

                rss = new JArray(from groupdaysell in CustomerBuyItem
                                 orderby groupdaysell.dgsell descending
                                 group groupdaysell by groupdaysell.dgsell into gb
                                 select new JObject(
                                     new JProperty("dsell", gb.Key.ToString("dd/MM/yyyy")),
                                     new JProperty("cost", gb.Sum(sum => sum.cost)),
                                     new JProperty("total", gb.Sum(sum => sum.total)),
                                     new JProperty("lucre", gb.Sum(sum => sum.lucre))));
                return rss;
            }
        }

        private string GetString4Params(string Parameter)
        {
            return string.Format("newObject[{0}]", Parameter);
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