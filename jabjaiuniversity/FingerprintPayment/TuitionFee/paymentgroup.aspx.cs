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
using System.Transactions;

namespace FingerprintPayment.TuitionFee
{
    public partial class paymentgroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            dgd.DataBound += (s, Event) =>
            {
                pagefunction.GridView_Bound(dgd, "PageDropDownList", "PageDropDownList2", "CurrentPageLabel");
            };
            if (!this.IsPostBack)
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                    using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                    {
                        var qpayment = peakengine.Products.Where(w => w.school_id == qcompany.nCompany).ToList();
                        Opendata();
                    }
                }
            }
        }

        #region page data
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Opendata();
        }

        private void Opendata()
        {
            dgd.DataSource = returnlist("");
            dgd.PageSize = 20;
            dgd.DataBind();
        }

        private List<Paymentgroup_data> returnlist(string name)
        {
            string Search = string.IsNullOrEmpty(ViewState["Search"] + "") ? "" : ViewState["Search"] + "";
            string entities = Session["sEntities"].ToString();


            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == entities).FirstOrDefault();
                PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read);

                var q = (from a in peakengine.Product_Group.ToList()
                         join b in peakengine.Product_Group_List.ToList() on a.PaymentGroupID equals b.PaymentGroupID
                         where a.school_id == tCompany.nCompany
                         group b by new { a.GroupName, a.PaymentGroupID, a.Del } into ag
                         where ag.Key.GroupName.Contains(Search) && ag.Key.Del == null
                         select new Paymentgroup_data
                         {
                             paymentgroup_name = ag.Key.GroupName,
                             paymentgroup_id = ag.Key.PaymentGroupID,
                             totalprice = ag.Sum(sum => sum.Price)
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
            pagefunction.Nextpage(dgd, returnlist(""));
        }

        public void backbutton_Click(Object sender, EventArgs e)
        {
            pagefunction.Backpage(dgd, returnlist(""));
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {
            pagefunction.changepage(dgd, returnlist(""), "PageDropDownList");
        }

        protected void PageDropDownList2_SelectedIndexChanged(Object sender, EventArgs e)
        {
            pagefunction.changesizepage(dgd, returnlist(""), "PageDropDownList2");
        }
        #endregion
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getpaymentdata(int id)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                {
                    var qpayment = peakengine.Products.Find(id);
                    dynamic rss = new JObject();
                    return new JObject(new JObject {
                        new JProperty("payment_id",qpayment.nPaymentID),
                        new JProperty("payment_name",qpayment.sPayment),
                        new JProperty("price",qpayment.Pirce),
                    }).ToString();
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string paymentlist()
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                {
                    dynamic rss = new JObject();
                    rss.data = new JArray(from a in peakengine.Products.Where(w => w.school_id == qcompany.nCompany && w.Del == null).ToList()
                                          select new JObject {
                                          new JProperty("payment_id",a.nPaymentID),
                                          new JProperty("payment_name",a.sPayment),
                                      });

                    return rss.ToString();
                }
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string updatepaymentgroup(Paymentgroup_data paymentgroup_Data)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                {
                    if (paymentgroup_Data.paymentgroup_id == 0)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            paymentgroup_Data.paymentgroup_id = peakengine.Product_Group.Count() == 0 ? 1 : peakengine.Product_Group.Max(max => max.PaymentGroupID + 1);
                            peakengine.Product_Group.Add(new Product_Group
                            {
                                PaymentGroupID = paymentgroup_Data.paymentgroup_id,
                                GroupName = paymentgroup_Data.paymentgroup_name,
                                DayAdd = DateTime.Now,
                                school_id = qcompany.nCompany
                            });

                            paymentgroup_Data.paymentgroup_list
                                .ForEach(f => peakengine.Product_Group_List.Add(new Product_Group_List
                                {
                                    PaymentGroupID = paymentgroup_Data.paymentgroup_id,
                                    nPaymentID = f.payment_id,
                                    Price = f.price
                                }));

                            peakengine.SaveChanges();
                            transactionScope.Complete();
                        }
                    }
                    else
                    {
                        var qgroup = peakengine.Product_Group.Find(paymentgroup_Data.paymentgroup_id);
                        qgroup.GroupName = paymentgroup_Data.paymentgroup_name;
                        qgroup.DayUpdate = DateTime.Now;
                        var qgroup_list = peakengine.Product_Group_List.Where(w => w.PaymentGroupID == paymentgroup_Data.paymentgroup_id).ToList();
                        foreach (var data in qgroup_list)
                        {
                            var listupdate = paymentgroup_Data.paymentgroup_list.FirstOrDefault(w => w.payment_id == data.nPaymentID);
                            if (listupdate != null) data.Price = listupdate.price;
                            else peakengine.Product_Group_List.Remove(data);
                        }

                        var qupdate = (from a in paymentgroup_Data.paymentgroup_list
                                       where !qgroup_list.Select(s => s.nPaymentID).Contains(a.payment_id)
                                       select new Product_Group_List
                                       {
                                           nPaymentID = a.payment_id,
                                           PaymentGroupID = paymentgroup_Data.paymentgroup_id,
                                           Price = a.price
                                       });

                        peakengine.Product_Group_List.AddRange(qupdate);
                        peakengine.SaveChanges();
                    }
                }
            }
            return "Success";
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getdata(int paymentgroup_id)
        {
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
            {
                var qgroup = peakengine.Product_Group.Find(paymentgroup_id);
                dynamic rss = new JObject(
                    new JProperty("group_id", qgroup.PaymentGroupID),
                    new JProperty("group_name", qgroup.GroupName),
                    new JProperty("group_list", new JArray(from b in peakengine.Product_Group_List.ToList()
                                                           join c in peakengine.Products.ToList() on b.nPaymentID equals c.nPaymentID
                                                           where b.PaymentGroupID == paymentgroup_id
                                                           select new JObject
                                                           {
                                                               new JProperty("payment_id",b.nPaymentID),
                                                               new JProperty("payment_name",c.sPayment),
                                                               new JProperty("price",b.Price)
                                                           })));

                return rss.ToString();
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string deletedata(int paymentgroup_id)
        {
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
            {
                var q = peakengine.Product_Group.Find(paymentgroup_id);
                q.Del = true;
                peakengine.SaveChanges();
                return "Success";
            }
        }

        public class Paymentgroup_data
        {
            public int paymentgroup_id { get; set; }
            public string paymentgroup_name { get; set; }
            public List<paymentgroup_list> paymentgroup_list { get; set; }
            public decimal? totalprice { get; set; }
        }

        public class paymentgroup_list
        {
            public int payment_id { get; set; }
            public string payment_name { get; set; }
            public decimal? price { get; set; }
        }

    }
}