using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.Data.Entity;
using PagedList;
using System.Configuration;
using urbanairship;
using System.Threading;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using PeakengineAPI;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.ComponentModel;
using System.Web.Services;
using FingerprintPayment.Models;

namespace FingerprintPayment.TuitionFee
{
    public partial class Tuitionfeesetting_1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getyear()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {

                    dynamic rss = new JObject();
                    rss.data = new JArray(from a in dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.numberYear).ToList()
                                          select new JObject {
                                                 new JProperty("year_id",a.nYear),
                                                 new JProperty("year_name",a.numberYear),
                                             });
                    return rss.ToString();
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string gettrem(int year_id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    dynamic rss = new JObject();
                    rss.data = new JArray(from a in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                          where a.nYear == year_id && a.cDel == null
                                          select new JObject
                                          {
                                              new JProperty("term_id",a.nTerm),
                                              new JProperty("term_name",a.sTerm),
                                          });

                    return rss.ToString();
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getsublevel()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    dynamic rss = new JObject();
                    rss.data = new JArray(from a in QueryDataBases.SubLevel_Query.GetData(dbschool, userData)
                                          select new JObject
                                          {
                                              new JProperty("sublevel_id",a.class_id),
                                              new JProperty("sublevel_name",a.class_name),
                                          });
                    return rss.ToString();
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getclass()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    dynamic rss = new JObject();
                    rss.data = new JArray(from a in QueryDataBases.SubLevel2_Query.GetData(dbschool, userData)
                                          select new JObject
                                          {
                                              new JProperty("class_id",a.classRoom_id),
                                              new JProperty("level_id",a.class_id),
                                              new JProperty("class_name",a.classRoom_name),
                                          });
                    return rss.ToString();
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getgroup()
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                {
                    dynamic rss = new JObject();
                    var qlist = peakengine.Product_Group_List.ToList();
                    rss.data = new JArray(from a in peakengine.Product_Group.Where(w => w.school_id == qcompany.nCompany && w.Del == null).ToList()
                                          select new JObject
                                          {
                                              new JProperty("group_id",a.PaymentGroupID),
                                              new JProperty("group_name",a.GroupName),
                                              new JProperty("price",qlist.Where(w=>w.PaymentGroupID == a.PaymentGroupID).Sum(s=>s.Price)),
                                          });
                    return rss.ToString();
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string setpeakengine(Invoices invoices)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    var qusermaster = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.cType == "0")
                        .Select(s => new
                        {
                            s.nSystemID,
                            s.ContactPeak
                        }).ToList();

                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
                    {
                        PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read);
                        var qgroup = peakengine.Product_Group.ToList();
                        var qpayments = peakengine.Products.ToList();

                        var qinvoices_detail_data = peakengine.TInvoices_Detail.ToList();
                        var qlist = invoices.invoices_List.FirstOrDefault();

                        var qstudent = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).
                            Select(s => new
                            {
                                s.sID,
                                s.nTermSubLevel2,
                            }).ToList();

                        var contact = (from a in qusermaster
                                       join b in qstudent on a.nSystemID equals b.sID
                                       select new
                                       {
                                           a.ContactPeak,
                                           b.sID,
                                           b.nTermSubLevel2
                                       }).ToList();

                        //Query Invoices Data
                        var q = (from fee_data in peakengine.Tuitionfees.ToList()
                                 join fee_detail in peakengine.Tuitionfee_Detail.ToList() on fee_data.Tuitionfee_id equals fee_detail.Tuitionfee_id
                                 join invoices_data in peakengine.TInvoices.ToList() on fee_detail.TuitionfeeDetail_id equals invoices_data.tuitionfeeDetail_id
                                 join contact_data in contact on invoices_data.student_id equals contact_data.sID
                                 where string.IsNullOrEmpty(invoices_data.invoices_Code) && fee_data.trem_id == invoices.term_id
                                 && (invoices_data.peakUpdate == null || invoices_data.jabjaiUpdate > invoices_data.peakUpdate ||
                                 (string.IsNullOrEmpty(invoices_data.code) && !string.IsNullOrEmpty(invoices_data.contactId)))
                                 && fee_detail.class_id == qlist.class_id && fee_detail.level_id == qlist.sublevel_id//Insert By Rows Table
                                 && (invoices_data.isDel == false) == false
                                 select new InvoiceAPI.Invoices
                                 {
                                     dueDate = fee_detail.dueDate.Value.ToString("yyyyMMdd"),
                                     issuedDate = fee_detail.issuedDate.Value.ToString("yyyyMMdd"),
                                     name = "",
                                     id = invoices_data.invoices_Code,
                                     contactId = contact_data.ContactPeak,
                                     istaxInvoice = 1,
                                     invoices_jabjai = invoices_data.invoices_Id,
                                     status = invoices_data.invoices_status,
                                     products = (from invoices_detail in qinvoices_detail_data
                                                 join payments in qpayments on invoices_detail.nPaymentID equals payments.nPaymentID
                                                 where invoices_detail.invoices_Id == invoices_data.invoices_Id
                                                 select new InvoiceAPI.Product
                                                 {
                                                     productId = payments.productId,
                                                     quantity = 1,
                                                     price = invoices_detail.price,
                                                     vatType = 1,
                                                 }).ToList(),
                                     Paidpayments = new List<InvoiceAPI.Paidpayments>() {
                                           new InvoiceAPI.Paidpayments
                                           {
                                               paymentDate = DateTime.Now.ToString("yyyyMMdd"),
                                               payments = new List<InvoiceAPI.Payments>()
                                               {
                                                   new   InvoiceAPI.Payments
                                                   {
                                                       amount = qinvoices_detail_data.Where(w => w.invoices_Id == invoices_data.invoices_Id).Sum(s => s.price.Value)
                                                   }
                                               }
                                           }
                                       }
                                 }).ToList();

                        //Query Invoices New Item
                        var q_add = q.Where(w => string.IsNullOrEmpty(w.id)).ToList();
                        //Query Invoices Item Update
                        var q_edit = q.Where(w => !string.IsNullOrEmpty(w.id) && w.status == "Approve").ToList();
                        var q_void = q.Where(w => !string.IsNullOrEmpty(w.id) && w.status != "Approve").ToList();

                        //Send Requset Add Invoices               
                        int length = q_add.Count();
                        int page = (length / 5) + (length % 5 > 0 ? 1 : 0);
                        //string result = "";
                        #region 

                        dbschool.SaveChanges();
                        peakengine.SaveChanges();
                        #endregion
                        return "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                return "{" + string.Format("\"Message\": \"{0}\" ,\"StackTrace\":\"{1}\",\"Source\":\"{2}\" ",
                    ex.Message, ex.StackTrace, ex.Source) + "}";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<InvoiceAPI.Invoices> getinvoice_student(Invoices invoices)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                var qusermaster = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.cType == "0")
                    .Select(s => new
                    {
                        s.nSystemID,
                        s.ContactPeak,
                        s.sName,
                        s.sLastname,
                    }).ToList();

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
                {
                    PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read);

                    var qgroup = peakengine.Product_Group.ToList();
                    var qpayments = peakengine.Products.ToList();

                    var qinvoices_detail_data = peakengine.TInvoices_Detail.ToList();
                    var qlist = invoices.invoices_List.FirstOrDefault();

                    if (qlist == null) return null;

                    var qstudent = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).
                        Select(s => new
                        {
                            s.sID,
                            s.nTermSubLevel2,
                        }).ToList();

                    var contact = (from a in qusermaster
                                   join b in qstudent on a.nSystemID equals b.sID
                                   select new
                                   {
                                       a.ContactPeak,
                                       b.sID,
                                       b.nTermSubLevel2,
                                       a.sName,
                                       a.sLastname
                                   }).ToList();

                    var q = (from fee_data in peakengine.Tuitionfees.ToList()
                             join fee_detail in peakengine.Tuitionfee_Detail.ToList() on fee_data.Tuitionfee_id equals fee_detail.Tuitionfee_id
                             join invoices_data in peakengine.TInvoices.ToList() on fee_detail.TuitionfeeDetail_id equals invoices_data.tuitionfeeDetail_id
                             join contact_data in contact on invoices_data.student_id equals contact_data.sID
                             where fee_data.trem_id == invoices.term_id
                             && !string.IsNullOrEmpty(contact_data.ContactPeak)
                             && (invoices_data.peakUpdate == null || invoices_data.jabjaiUpdate > invoices_data.peakUpdate ||
                             (string.IsNullOrEmpty(invoices_data.code) && !string.IsNullOrEmpty(invoices_data.contactId)))
                             && fee_detail.class_id == qlist.class_id && fee_detail.level_id == qlist.sublevel_id//Insert By Rows Table
                             && fee_data.school_id == qcompany.nCompany && (invoices_data.isDel == false) == false
                             select new InvoiceAPI.Invoices
                             {
                                 dueDate = fee_detail.dueDate.Value.ToString("yyyyMMdd"),
                                 issuedDate = fee_detail.issuedDate.Value.ToString("yyyyMMdd"),
                                 name = contact_data.sName + " " + contact_data.sLastname,
                                 id = invoices_data.invoices_Code,
                                 contactId = contact_data.ContactPeak,
                                 istaxInvoice = 1,
                                 invoices_jabjai = invoices_data.invoices_Id,
                                 status = invoices_data.invoices_status,
                                 products = (from invoices_detail in qinvoices_detail_data
                                             join payments in qpayments on invoices_detail.nPaymentID equals payments.nPaymentID
                                             where invoices_detail.invoices_Id == invoices_data.invoices_Id
                                             select new InvoiceAPI.Product
                                             {
                                                 productId = payments.productId,
                                                 quantity = 1,
                                                 price = invoices_detail.price,
                                                 vatType = 1,
                                             }).ToList(),
                                 Paidpayments = new List<InvoiceAPI.Paidpayments>() {
                                           new InvoiceAPI.Paidpayments
                                           {
                                               paymentDate = DateTime.Now.ToString("yyyyMMdd"),
                                               payments = new List<InvoiceAPI.Payments>()
                                               {
                                                   new   InvoiceAPI.Payments
                                                   {
                                                       amount = qinvoices_detail_data.Where(w => w.invoices_Id == invoices_data.invoices_Id).Sum(s => s.price.Value)
                                                   }
                                               }
                                           }
                                       }
                             }).ToList();

                    return q;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        private static string sendinvoice_student(InvoiceAPI.Invoices invoices_data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                var qusermaster = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.cType == "0")
                    .Select(s => new
                    {
                        s.nSystemID,
                        s.ContactPeak,
                        s.sName,
                        s.sLastname,
                        s.sID
                    }).ToList();

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
                {
                    PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read);
                    //string connectId = ConfigurationManager.AppSettings["connectId"];
                    //string password = ConfigurationManager.AppSettings["password"];
                    //var qXML = ConfigPeakenineAPI.GetToken(qcompany.nCompany, connectId, password);

                    //var invoices = new List<InvoiceAPI.Invoices>();
                    //invoices.Add(invoices_data);

                    //Query Invoices New Item
                    if (string.IsNullOrEmpty(invoices_data.id))
                    {
                        //var result = InvoiceAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, invoices).FirstOrDefault();
                        var invoicesJabjai_data = peakengine.TInvoices.FirstOrDefault(f => f.invoices_Id == invoices_data.invoices_jabjai);
                        var q1 = peakengine.Tuitionfee_Detail.FirstOrDefault(f => f.TuitionfeeDetail_id == invoicesJabjai_data.tuitionfeeDetail_id);
                        var q2 = peakengine.Tuitionfees.FirstOrDefault(f => f.Tuitionfee_id == q1.Tuitionfee_id);
                        var q3 = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nTerm == q2.trem_id);
                        var q4 = dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).FirstOrDefault(f => f.nYear == q2.year_id);

                        notification _notification = new notification();
                        //if (invoicesJabjai_data != null)
                        //{
                        //    invoicesJabjai_data.invoices_Code = result.id;
                        //    invoicesJabjai_data.code = result.code;
                        //    invoicesJabjai_data.peakUpdate = DateTime.Now;
                        //}

                        var fuser = qusermaster.FirstOrDefault(f => f.ContactPeak == invoicesJabjai_data.contactId);
                        if (fuser != null)
                        {
                            var totalprice = peakengine.TInvoices_Detail.Where(w => w.invoices_Id == invoices_data.invoices_jabjai).Sum(s => s.price);

                            _notification.user = fuser.sID.ToString();
                            //_notification.user = "[ " + fuser.sID.ToString() + "]";
                            _notification.title = fuser.sName + " " + fuser.sLastname;
                            _notification.message = string.Format("แจ้งชำระค่าเล่าเรียน {2:#,##0.00} บาท ภาคเรียนที่ {0} ปีการศึกษา {1}", q3.sTerm, q4.numberYear, totalprice);
                            _notification.title = "แจ้งชำระค่าเล่าเรียน";

                            int message_id = messagebox.insert_message(
                                new messagebox.MessageBox
                                {
                                    message_type = messagebox.Invoices,
                                    send_time = DateTime.Now,
                                    school_id = qcompany.nCompany,
                                    message = _notification.message,
                                    title = _notification.title,
                                    user_messagebox = new List<messagebox.user_messagebox>()
                                    {
                                        new messagebox.user_messagebox {
                                            user_id = fuser.sID,
                                            user_type = 0,
                                            message_receive = DateTime.Now
                                        }
                                    }
                                });

                            _notification.action = "vnd.jabjai.jabjaiapp://deeplink/invoices?message_id=" + message_id + "&school_id=" + qcompany.nCompany;

                            Thread notification = new Thread(async delegate ()
                            {
                                await pushdata.pushAsync(_notification);
                            })
                            {
                                IsBackground = true
                            };
                            notification.Start();
                        }
                    }

                    ////Send Requset Edit Invoices
                    //else if (!string.IsNullOrEmpty(invoices_data.id) && invoices_data.status == "Approve")
                    //{
                    //    //var api_result = InvoiceAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, invoices_data, invoices_data.id);
                    //    //Update Peakngine Invoices id 
                    //    var invoicesJabjai_data = peakengine.TInvoices.FirstOrDefault(api_result);
                    //    if (invoicesJabjai_data != null)
                    //    {
                    //        invoicesJabjai_data.peakUpdate = DateTime.Now;
                    //    }
                    //}

                    //////Send Requset Void
                    //else if (!string.IsNullOrEmpty(invoices_data.id) && invoices_data.status != "Approve")
                    //{
                    //    var api_result = InvoiceAPI.SendRequest_void(connectId, password, qXML.Client_Token, qXML.User_Token, invoices_data.id);
                    //    //Update Peakngine Invoices id 
                    //    var invoicesJabjai_data = peakengine.TInvoices.FirstOrDefault(f => f.invoices_Code == api_result);
                    //    if (invoicesJabjai_data != null)
                    //    {
                    //        invoicesJabjai_data.peakUpdate = DateTime.Now;
                    //    }
                    //}

                    peakengine.SaveChanges();
                    return "Succsess";
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string saveData(Accounting.Tuitionfee.Setting.Invoices invoices, string term_id, bool? tremStatus)
        {
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    var response = Accounting.Tuitionfee.Setting.SaveInvoiceData(qcompany, invoices, term_id, tremStatus);
                    return response.ToString();
                }
            }
            catch (UpdateException e)
            {
                return e.Message.ToString();
            }
            catch (DbUpdateException ex) //DbContext
            {
                return ex.InnerException.ToString();
            }
            catch (DbEntityValidationException exdb)
            {
                string message_error = "";
                foreach (var eve in exdb.EntityValidationErrors)
                {
                    message_error += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message_error += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return message_error;
            }
            catch (Exception ex)
            {
                return "{" + string.Format("\"Message\": \"{0}\" ,\"StackTrace\":\"{1}\",\"Source\":\"{2}\" ",
              ex.Message, ex.StackTrace, ex.Source) + "}";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getdata(int year_id, string term_id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
                    {
                        PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read);

                        dynamic rss = new JObject();
                        var q = (from a in peakengine.Tuitionfees
                                 join b in peakengine.Tuitionfee_Detail on a.Tuitionfee_id equals b.Tuitionfee_id
                                 join c in peakengine.Product_Group_List on b.paygroup_id equals c.PaymentGroupID
                                 join d in dbschool.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.level_id equals d.nTSubLevel
                                 join e in dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on b.class_id equals e.nTermSubLevel2 into jbe
                                 from _jbe in jbe.DefaultIfEmpty()
                                 where a.trem_id == term_id && a.year_id == year_id && a.school_id == userData.CompanyID && b.del != true
                                 select new
                                 {
                                     b.class_id,
                                     b.level_id,
                                     b.dueDate,
                                     b.issuedDate,
                                     b.paygroup_id,
                                     level_name = d.SubLevel.Trim(),
                                     class_name = _jbe == null ? "เลือกห้องทั้งหมด" : d.SubLevel.Trim() + " / " + _jbe.nTSubLevel2,
                                     c.Price,
                                     b.TuitionfeeDetail_id,
                                 }).ToList();

                        rss.data = new JArray(from a in q.ToList()
                                              group a by new
                                              {
                                                  a.class_id,
                                                  a.level_id,
                                                  a.dueDate,
                                                  a.issuedDate,
                                                  a.paygroup_id,
                                                  a.class_name,
                                                  a.level_name,
                                                  a.TuitionfeeDetail_id
                                              } into data
                                              select new JObject
                                              {
                                                  new JProperty("class_id",data.Key.class_id),
                                                  new JProperty("class_name",data.Key.class_name),
                                                  new JProperty("level_id",data.Key.level_id),
                                                  new JProperty("level_name",data.Key.level_name),
                                                  new JProperty("paygroup_id",data.Key.paygroup_id),
                                                  new JProperty("dueDate",data.Key.dueDate.Value.ToString("dd/MM/yyyy")),
                                                  new JProperty("issuedDate",data.Key.issuedDate.Value.ToString("dd/MM/yyyy")),
                                                  new JProperty("price",data.Sum(s=>s.Price)),
                                                  new JProperty("id", data.Key.TuitionfeeDetail_id)
                                              });

                        var f1 = peakengine.Tuitionfees.FirstOrDefault(f => f.trem_id == term_id);
                        rss.tremStatus = f1 == null ? null : f1.Fd_NewTremStatus;

                        return rss.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "{" + string.Format("\"Message\": \"{0}\" ,\"StackTrace\":\"{1}\",\"Source\":\"{2}\" ",
            ex.Message, ex.StackTrace, ex.Source) + "}";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SaveInvoice(int year_id, string term_id, List<int> tuitionfeeDetail_Id)
        {
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
                    {
                        PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read);

                        var q_Tuitionfee = (from a in peakengine.Tuitionfees
                                            join b in peakengine.Tuitionfee_Detail on a.Tuitionfee_id equals b.Tuitionfee_id
                                            where a.trem_id.Trim() == term_id && a.year_id == year_id
                                            && a.school_id == qcompany.nCompany && (tuitionfeeDetail_Id.Count() == 0 || tuitionfeeDetail_Id.Contains(b.TuitionfeeDetail_id))
                                            select b).ToList();

                        var Invoices_List = new List<Accounting.Tuitionfee.Setting.Invoices_List>();
                        q_Tuitionfee.ToList().ForEach(f => Invoices_List.Add(new Accounting.Tuitionfee.Setting.Invoices_List
                        {
                            Class_Id = f.class_id,
                            DueDate = f.dueDate,
                            Group_Id = f.paygroup_id,
                            IssuedDate = f.issuedDate,
                            Sublevel_Id = f.level_id,
                            Id = f.TuitionfeeDetail_id
                        }));

                        Accounting.Tuitionfee.Setting.SaveInvoiceData_v1(qcompany, new Accounting.Tuitionfee.Setting.Invoices
                        {
                            Term_id = term_id,
                            Year_id = year_id,
                            Invoices_List = Invoices_List,
                        }, true);

                        return "Success !!";

                    }
                }
            }
            catch (UpdateException e)
            {
                return e.Message.ToString();
            }
            catch (DbUpdateException ex) //DbContext
            {
                return ex.InnerException.ToString();
            }
            catch (DbEntityValidationException exdb)
            {
                string message_error = "";
                foreach (var eve in exdb.EntityValidationErrors)
                {
                    message_error += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message_error += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return message_error;
            }
            catch (Exception ex)
            {
                return "{" + string.Format("\"Message\": \"{0}\" ,\"StackTrace\":\"{1}\",\"Source\":\"{2}\" ",
              ex.Message, ex.StackTrace, ex.Source) + "}";
            }
        }


        public class Invoices
        {
            public string term_id { get; set; }
            public int? year_id { get; set; }
            public List<Invoices_List> invoices_List { get; set; }
        }

        public class Invoices_List
        {
            public DateTime? issuedDate { get; set; }
            public DateTime? dueDate { get; set; }
            public int? sublevel_id { get; set; }
            public int? class_id { get; set; }
            public int? group_id { get; set; }
        }
    }
}