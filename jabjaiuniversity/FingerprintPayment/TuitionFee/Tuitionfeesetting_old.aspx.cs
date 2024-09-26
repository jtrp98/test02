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
    public partial class Tuitionfeesetting_old : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getyear()
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                {
                    dynamic rss = new JObject();
                    rss.data = new JArray(from a in dbschool.TYears.ToList()
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
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                {
                    dynamic rss = new JObject();
                    rss.data = new JArray(from a in dbschool.TTerms.ToList()
                                          where a.nYear == year_id && a.cDel == null
                                          select new JObject
                                          {
                                              new JProperty("trem_id",a.nTerm),
                                              new JProperty("trem_name",a.sTerm),
                                          });

                    return rss.ToString();
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getsublevel()
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                {
                    dynamic rss = new JObject();
                    rss.data = new JArray(from a in QueryDataBases.SubLevel_Query.GetData(dbschool)
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
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                {
                    dynamic rss = new JObject();
                    rss.data = new JArray(from a in QueryDataBases.SubLevel2_Query.GetData(dbschool)
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
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                using (PeakengineEntities peakengine = Connection.PeakengineEntities())
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
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
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

                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities)))
                    {
                        PeakengineEntities peakengine = Connection.PeakengineEntities();
                        var qgroup = peakengine.Product_Group.ToList();
                        var qpayments = peakengine.Products.ToList();
                        string connectId = ConfigurationManager.AppSettings["connectId"];
                        string password = ConfigurationManager.AppSettings["password"];
                        var qXML = ConfigPeakenineAPI.GetToken(qcompany.nCompany, connectId, password);

                        if (qXML.Client_Time_Stamp.Value.AddDays(1) <= DateTime.Now.AddMinutes(-1).ToUniversalTime())
                        {
                            var clientToken = ClientToken.SendRequest(connectId, password);
                            qXML.Client_Token = clientToken.token;
                            qXML.Client_Time_Stamp = DateTime.Now.ToUniversalTime();
                        }

                        var qinvoices_detail_data = peakengine.TInvoices_Detail.ToList();
                        var qlist = invoices.invoices_List.FirstOrDefault();

                        var qstudent = dbschool.TUsers.
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
                                 where string.IsNullOrEmpty(invoices_data.invoices_Code) && fee_data.trem_id == invoices.trem_id
                                 && (invoices_data.peakUpdate == null || invoices_data.jabjaiUpdate > invoices_data.peakUpdate ||
                                 (string.IsNullOrEmpty(invoices_data.code) && !string.IsNullOrEmpty(invoices_data.contactId)))
                                 && fee_detail.class_id == qlist.class_id && fee_detail.level_id == qlist.sublevel_id//Insert By Rows Table
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
                        for (int i = 1; i <= page; i++)
                        {
                            var Invoices = q_add.ToPagedList(i, 5).ToList();

                            var result = InvoiceAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, Invoices);
                            //Update Peakngine Invoices id
                            foreach (var jabjai_invoices_data in (from api_data in Invoices
                                                                  join jabjai_data in peakengine.TInvoices.ToList() on api_data.invoices_jabjai equals jabjai_data.invoices_Id
                                                                  select jabjai_data))
                            {
                                var qpeakinvoices_data = result.FirstOrDefault(w => w.contactId == jabjai_invoices_data.contactId);
                                if (qpeakinvoices_data != null)
                                {
                                    jabjai_invoices_data.invoices_Code = qpeakinvoices_data.id;
                                    jabjai_invoices_data.code = qpeakinvoices_data.code;
                                    jabjai_invoices_data.peakUpdate = DateTime.Now;
                                }
                            }
                        }

                        //return "{ \"postData\" : \"" + result + "\"}";
                        //Send Requset Edit Invoices
                        foreach (var data_edit in q_edit)
                        {
                            var api_result = InvoiceAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, data_edit, data_edit.id);
                            //Update Peakngine Invoices id 
                            var invoices_data = peakengine.TInvoices.Find(api_result);
                            if (invoices_data != null)
                            {
                                invoices_data.peakUpdate = DateTime.Now;
                            }
                        }

                        //Send Requset Void
                        foreach (var data_void in q_void)
                        {
                            var api_result = InvoiceAPI.SendRequest_void(connectId, password, qXML.Client_Token, qXML.User_Token, data_void.id);
                            //Update Peakngine Invoices id 
                            var invoices_data = peakengine.TInvoices.FirstOrDefault(f => f.invoices_Code == api_result);
                            if (invoices_data != null)
                            {
                                invoices_data.peakUpdate = DateTime.Now;
                            }
                        }
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
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
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

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities)))
                {
                    PeakengineEntities peakengine = Connection.PeakengineEntities();

                    var qgroup = peakengine.Product_Group.ToList();
                    var qpayments = peakengine.Products.ToList();

                    var qinvoices_detail_data = peakengine.TInvoices_Detail.ToList();
                    var qlist = invoices.invoices_List.FirstOrDefault();

                    if (qlist == null) return null;

                    var qstudent = dbschool.TUsers.
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
                             where fee_data.trem_id == invoices.trem_id
                             && !string.IsNullOrEmpty(contact_data.ContactPeak)
                             && (invoices_data.peakUpdate == null || invoices_data.jabjaiUpdate > invoices_data.peakUpdate ||
                             (string.IsNullOrEmpty(invoices_data.code) && !string.IsNullOrEmpty(invoices_data.contactId)))
                             && fee_detail.class_id == qlist.class_id && fee_detail.level_id == qlist.sublevel_id//Insert By Rows Table
                             && fee_data.school_id == qcompany.nCompany
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
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
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

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities)))
                {
                    PeakengineEntities peakengine = Connection.PeakengineEntities();
                    string connectId = ConfigurationManager.AppSettings["connectId"];
                    string password = ConfigurationManager.AppSettings["password"];
                    var qXML = ConfigPeakenineAPI.GetToken(qcompany.nCompany, connectId, password);

                    var invoices = new List<InvoiceAPI.Invoices>();
                    invoices.Add(invoices_data);

                    //Query Invoices New Item
                    if (string.IsNullOrEmpty(invoices_data.id))
                    {
                        var result = InvoiceAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, invoices).FirstOrDefault();
                        var invoicesJabjai_data = peakengine.TInvoices.FirstOrDefault(f => f.invoices_Id == invoices_data.invoices_jabjai);
                        var q1 = peakengine.Tuitionfee_Detail.FirstOrDefault(f => f.TuitionfeeDetail_id == invoicesJabjai_data.tuitionfeeDetail_id);
                        var q2 = peakengine.Tuitionfees.FirstOrDefault(f => f.Tuitionfee_id == q1.Tuitionfee_id);
                        var q3 = dbschool.TTerms.FirstOrDefault(f => f.nTerm == q2.trem_id);
                        var q4 = dbschool.TYears.FirstOrDefault(f => f.nYear == q2.year_id);

                        notification _notification = new notification();
                        if (invoicesJabjai_data != null)
                        {
                            invoicesJabjai_data.invoices_Code = result.id;
                            invoicesJabjai_data.code = result.code;
                            invoicesJabjai_data.peakUpdate = DateTime.Now;
                        }

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
                                await pushdata.push(_notification);
                            })
                            {
                                IsBackground = true
                            };
                            notification.Start();
                        }
                    }

                    ////Send Requset Edit Invoices
                    else if (!string.IsNullOrEmpty(invoices_data.id) && invoices_data.status == "Approve")
                    {
                        var api_result = InvoiceAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, invoices_data, invoices_data.id);
                        //Update Peakngine Invoices id 
                        var invoicesJabjai_data = peakengine.TInvoices.Find(api_result);
                        if (invoicesJabjai_data != null)
                        {
                            invoicesJabjai_data.peakUpdate = DateTime.Now;
                        }
                    }

                    ////Send Requset Void
                    else if (!string.IsNullOrEmpty(invoices_data.id) && invoices_data.status != "Approve")
                    {
                        var api_result = InvoiceAPI.SendRequest_void(connectId, password, qXML.Client_Token, qXML.User_Token, invoices_data.id);
                        //Update Peakngine Invoices id 
                        var invoicesJabjai_data = peakengine.TInvoices.FirstOrDefault(f => f.invoices_Code == api_result);
                        if (invoicesJabjai_data != null)
                        {
                            invoicesJabjai_data.peakUpdate = DateTime.Now;
                        }
                    }

                    peakengine.SaveChanges();
                    return "Succsess";
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string saveData(Accounting.Tuitionfee.Setting.Invoices invoices)
        {
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {
                    //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    var response = Accounting.Tuitionfee.Setting.SaveInvoiceData(qcompany, invoices);
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

        private static ConfigPeakenineAPI.TokenAPI GetToken(string MapPath)
        {
            var qXML = ConfigPeakenineAPI.ReadXML(MapPath);
            if (qXML == null && qXML.Client_Time_Stamp.HasValue && qXML.Client_Time_Stamp.Value.AddHours(22) <= DateTime.UtcNow)
            {
                string connectId = ConfigurationManager.AppSettings["connectId"];
                string password = ConfigurationManager.AppSettings["password"];
                Dictionary<string, string> HttpWebRequestHeader = new Dictionary<string, string>()
                {
                    { "Time-Stamp", connectId},
                    { "Time-Signature", connectId},
                };

                var qToken = ClientToken.SendRequest(connectId, password);
                ConfigPeakenineAPI.TokenAPI token = new ConfigPeakenineAPI.TokenAPI
                {
                    Client_Time_Stamp = DateTime.UtcNow,
                    Client_Token = qToken.token,
                    User_Token = qXML.User_Token
                };
                ConfigPeakenineAPI.WriteXML(token, MapPath);
                return token;
            }
            else
            {
                return qXML;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getdata(int year_id, string trem_id)
        {
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {
                    //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities)))
                    {
                        PeakengineEntities peakengine = Connection.PeakengineEntities();

                        dynamic rss = new JObject();
                        var q = (from a in peakengine.Tuitionfees.ToList()
                                 join b in peakengine.Tuitionfee_Detail.ToList() on a.Tuitionfee_id equals b.Tuitionfee_id
                                 join c in peakengine.Product_Group_List.ToList() on b.paygroup_id equals c.PaymentGroupID
                                 join d in dbschool.TSubLevels.ToList() on b.level_id equals d.nTSubLevel
                                 join e in dbschool.TTermSubLevel2.ToList() on b.class_id equals e.nTermSubLevel2 into jbe
                                 from _jbe in jbe.DefaultIfEmpty()
                                 where a.trem_id == trem_id && a.year_id == year_id && a.school_id == qcompany.nCompany && b.del != true
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

        #region Send Peak Engine
        //Setp 1
        //[System.Web.Script.Services.ScriptMethod()]
        [ScriptMethod]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getInvoices(int year_id, string trem_id, List<int> InvoicesGroup_Id)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {
                    //string entities = "JabJaiEntities00025"; 
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    //return SendPeakEngine.GetInvoices("TS0000003", qcompany);
                    var q = Accounting.Tuitionfee.SendPeakEngine.GetInvoices(trem_id, qcompany, InvoicesGroup_Id).ToList();
                    string str = js.Serialize(q);
                    return str;
                }
            }
            catch (Exception ex)
            {
                return js.Serialize(ex);
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getInvoicesVoid(int year_id, string trem_id)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {
                    //string entities = "JabJaiEntities00025"; 
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    var q = Accounting.Tuitionfee.SendPeakEngine.GetVoid(trem_id, qcompany).ToList();
                    string str = js.Serialize(q);
                    return str;
                }
            }
            catch (Exception ex)
            {
                return js.Serialize(ex);
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SendVoid(int Invoice_id)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {
                    PeakengineEntities peakengine = Connection.PeakengineEntities();
                    string connectId = ConfigurationManager.AppSettings["connectId"];
                    string password = ConfigurationManager.AppSettings["password"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    var qXML = ConfigPeakenineAPI.GetToken(qcompany.nCompany, connectId, password);
                    var f_invoices = peakengine.TInvoices.FirstOrDefault(f => f.invoices_Id == Invoice_id);
                    if (!string.IsNullOrEmpty(f_invoices.invoices_Code))
                    {
                        InvoiceAPI.SendRequest_void(connectId, password, qXML.Client_Token, qXML.User_Token, f_invoices.invoices_Code);
                    }
                    f_invoices.invoices_status = "Void";
                    peakengine.SaveChanges();
                    return string.Format("Invoices Id : {0:00000} Status : Send Void", f_invoices.invoices_Id);
                }
            }
            catch (Exception ex)
            {
                return js.Serialize(ex);
            }
        }

        //Setp 2
        //Update Invoices Data
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SetupInvoicess(Accounting.Tuitionfee.SendPeakEngine.Invoices invoices)
        {
            try
            {
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //string str = js.Serialize(invoices);
                //return str;

                var data1 = invoices.invoicesStudents.FirstOrDefault();
                using (PeakengineEntities peakengineEntities = Connection.PeakengineEntities())
                {

                    var item = new List<Accounting.Tuitionfee.Setting.Invoices_Detail>();
                    foreach (var data_item in invoices.items)
                    {
                        item.Add(new Accounting.Tuitionfee.Setting.Invoices_Detail
                        {
                            price = data_item.Price,
                            nPaymentID = data_item.Id,
                            product_id = data_item.Product_Id,
                            quantity = 1,
                        });
                    }

                    if (data1.Id == 0)
                    {
                        data1.Id = Accounting.Tuitionfee.Setting.InsertInvoice(peakengineEntities, new TInvoice
                        {
                            school_id = data1.School_id,
                            issuedDate = data1.IssuedDate,
                            dueDate = data1.DueDate,
                            tuitionfeeDetail_id = invoices.Id,
                            contactId = data1.ContactId,
                            student_id = data1.Student_Id
                        }, item);
                        return SendInvoices(data1.Id);
                    }
                    else
                    {
                        var f_invoices = peakengineEntities.TInvoices.FirstOrDefault(f => f.invoices_Id == data1.Id);
                        var f1 = peakengineEntities.Tuitionfee_Detail.FirstOrDefault(f => f.TuitionfeeDetail_id == f_invoices.tuitionfeeDetail_id);
                        if (f_invoices.peakStatusUpdate != true)
                        {
                            if ((f1.paygroup_id != invoices.GroupId)
                                    || (f_invoices.issuedDate != f1.issuedDate
                                    || f_invoices.dueDate != f1.dueDate)
                                    || invoices.Update > f_invoices.jabjaiUpdate)
                            {
                                f_invoices.issuedDate = f1.issuedDate;
                                f_invoices.dueDate = f1.dueDate;
                                Accounting.Tuitionfee.Setting.EditInvoice(peakengineEntities, f_invoices, item, true);
                                return SendInvoices(f_invoices.invoices_Id);

                            }
                            else if (string.IsNullOrEmpty(f_invoices.code) || !f_invoices.peakUpdate.HasValue)
                            {
                                return SendInvoices(f_invoices.invoices_Id);
                            }
                            else
                            {
                                return string.Format("Invoices Id : {0:00000} Status : Not Send Invoices", f_invoices.invoices_Id);
                            }
                        }
                        else
                        {
                            return string.Format("Invoices Id : {0:00000} Status : Not Send Invoices", f_invoices.invoices_Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "{" + string.Format("\"Message\": \"{0}\" ,\"StackTrace\":\"{1}\",\"Source\":\"{2}\" ",
                    ex.Message, ex.StackTrace, ex.Source) + "}";
            }
        }

        //Setp 3
        //Send Invoices Data
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SendInvoices(int invoices_id)
        {
            try
            {
                PeakengineEntities peakengineEntities = Connection.PeakengineEntities();
                JabJaiMasterEntities masterEntities = Connection.MasterEntities();

                var f_invoices = peakengineEntities.TInvoices.FirstOrDefault(f => f.invoices_Id == invoices_id);
                var f_company = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == f_invoices.school_id);
                sendinvoice_student(Accounting.Tuitionfee.SendPeakEngine.SendInvoices(invoices_id, f_company.sEntities));

                return string.Format("Invoices Id : {0:00000} Status : Send Invoices Success", invoices_id);
            }
            catch (Exception ex)
            {
                return string.Format("Invoices Id : {0:00000} Status : Send Invoices Fail message : {1}", invoices_id, ex.GetBaseException());
            }

        }
        #endregion


        //public delegate void Run();
        //private const string sessionKey = "progress";
        //private static string message_data = "";
        //public Progress Progress
        //{
        //    get
        //    {
        //        if (Session[sessionKey] == null)
        //            Session.Add(sessionKey, new Progress());
        //        return Session[sessionKey] as Progress;
        //    }
        //    set
        //    {
        //        Session[sessionKey] = value;
        //    }
        //}

        //public void StartDownloadExecute()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        //if(i==7)  
        //        //    Progress.Add(new ProgressStep(string.Format("Step # {0}", i), ProgressStatus.Error));  
        //        //else  
        //        Progress.Add(new ProgressStep(string.Format("Step # {0}", i), ProgressStatus.InProgress));
        //        Thread.Sleep(2000);
        //    }
        //    // ------Write further code to mark the process completed  
        //}

        //public void StartDownload()
        //{
        //    Progress = null;
        //    Progress.Add(new ProgressStep("Download Started", ProgressStatus.InProgress));

        //    Run run = new Run(StartDownloadExecute);

        //    IAsyncResult res = run.BeginInvoke((IAsyncResult ar) =>
        //    {
        //        Progress.Add(new ProgressStep("Download Completed", ProgressStatus.Completed, "000ABORT_CHECK000"));
        //    }, null);
        //}

        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static void StartProgress(int year_id, string trem_id, List<int> InvoicesGroup_Id)
        //{
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    try
        //    {
        //        using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
        //        {
        //            //string entities = "JabJaiEntities00025"; 
        //            string entities = HttpContext.Current.Session["sEntities"].ToString();
        //            var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
        //            //return SendPeakEngine.GetInvoices("TS0000003", qcompany);
        //            var q = SendPeakEngine.GetInvoices(trem_id, qcompany, InvoicesGroup_Id).ToList();
        //            Run run = new Run(() => ProgressSendInvoice(q));

        //            IAsyncResult res = run.BeginInvoke((IAsyncResult ar) =>
        //            {
        //                //Progress.Add(new ProgressStep("Download Completed", ProgressStatus.Completed, "000ABORT_CHECK000"));
        //            }, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //public static void ProgressSendInvoice(List<SendPeakEngine.Invoices> q)
        //{
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    message_data = "{ \"Message\" : \"Start Progress\" ,\"Status\" : \"Start Progress\" , \"Data\" : \"\" }";
        //    foreach (var f_data in q)
        //    {
        //        try
        //        {
        //            //SetupInvoicess(f_data);
        //            //message_data = "{ \"Message\" : \"Start Progress\",\"Status\" : \"Run Progress\"  , \"Data\" : { " + SendInvoices(f_data.Id) + " } }";
        //            foreach (var f_data_1 in f_data.invoicesStudents)
        //            {
        //                try
        //                {
        //                    message_data = ",{ \"Message\" : \"Start Progress\",\"Status\" : \"Run Progress\"  , \"Data\" : " + SendInvoices(f_data_1.Id) + " }";
        //                }
        //                catch (Exception ex)
        //                {
        //                    message_data = ",{ \"Message\" : \"Start Progress\" ,\"Status\" : \"Error Progress\" , \"Data\" : " + js.Serialize(ex) + " }";
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            message_data = "{ \"Message\" : \"Start Progress\" ,\"Status\" : \"Error Progress\" , \"Data\" : " + js.Serialize(ex) + " }";
        //        }
        //    }
        //    message_data = "{ \"Message\" : \"Start Progress\" ,\"Status\" : \"Completed Progress\" , \"Data\" : \"\" }";
        //}

        //[WebMethod(EnableSession = true)]
        //public static string CheckDownload()
        //{
        //    return message_data;
        //    //return HttpContext.Current.Session[sessionKey] == null ? string.Empty
        //    //    : ((Progress)HttpContext.Current.Session[sessionKey]).ToString();
        //}

        public class Invoices
        {
            public string trem_id { get; set; }
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