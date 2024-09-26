using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    public partial class scoreRegisterSetting : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
       
            string comid = Request.QueryString["comid"];
            int sEmpID = int.Parse(Session["sEmpID"] + "");
            btnSave.Click += new EventHandler(btnSave_Click);

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                if (!IsPostBack)
                {
                    OpenData();

                    string yearr = Request.QueryString["year"];
                    DropDownList1.SelectedValue = yearr;
                    string termm = Request.QueryString["term"];
                    DropDownList2.SelectedValue = termm;




                    List<TYear> tylist = new List<TYear>();
                    TYear ty = new TYear();
                    foreach (var a in _db.TYears.ToList())
                    {
                        ty = new TYear();
                        ty = a;
                        tylist.Add(ty);
                    }
                    var newList = tylist.OrderByDescending(x => x.numberYear).ToList();

                    DropDownList1.DataSource = newList;
                    DropDownList1.DataTextField = "numberYear";
                    DropDownList1.DataValueField = "numberYear";
                    DropDownList1.DataBind();


                }
            }
        }

        private void OpenData()
        {



            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string year = Request.QueryString["year"];
                string term = Request.QueryString["term"];


                int? useryear = Int32.Parse(year);
                int nyear = 0;
                string nterm = "";

                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
                {
                    nyear = ff.nYear;
                }

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == term && w.nYear == nyear && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }

                var data = _db.TGradeRegisterPeriods.Where(w => w.nTerm == nterm).FirstOrDefault();
                if (data != null)
                {
                    if (data.beforeMidtermStart == null)
                        mid1.Text = "";
                    else mid1.Text = data.beforeMidtermStart.ToString();

                    if (data.beforeMidtermEnd == null)
                        mid2.Text = "";
                    else mid2.Text = data.beforeMidtermEnd.ToString();

                    if (data.duringMidtermStart == null)
                        mid3.Text = "";
                    else mid3.Text = data.duringMidtermStart.ToString();

                    if (data.duringMidtermEnd == null)
                        mid4.Text = "";
                    else mid4.Text = data.duringMidtermEnd.ToString();

                    if (data.afterMidtermStart == null)
                        mid5.Text = "";
                    else mid5.Text = data.afterMidtermStart.ToString();

                    if (data.afterMidtermEnd == null)
                        mid6.Text = "";
                    else mid6.Text = data.afterMidtermEnd.ToString();

                    if (data.FinaltermStart == null)
                        fin1.Text = "";
                    else fin1.Text = data.FinaltermStart.ToString();

                    if (data.FinaltermEnd == null)
                        fin2.Text = "";
                    else fin2.Text = data.FinaltermEnd.ToString();

                    if (data.ExtraStart == null)
                        ex1.Text = "";
                    else ex1.Text = data.ExtraStart.ToString();

                    if (data.ExtraEnd == null)
                        ex2.Text = "";
                    else ex2.Text = data.ExtraEnd.ToString();

                }
                else
                {
                    mid1.Text = "";
                    mid2.Text = "";
                    mid3.Text = "";
                    mid4.Text = "";
                    mid5.Text = "";
                    mid6.Text = "";
                    fin1.Text = "";
                    fin2.Text = "";
                    ex1.Text = "";
                    ex2.Text = "";
                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string year = Request.QueryString["year"];
                string term = Request.QueryString["term"];
                int sEmpID = int.Parse(Session["sEmpID"] + "");
                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int? useryear = Int32.Parse(year);
                int nyear = 0;
                string nterm = "";

                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
                {
                    nyear = ff.nYear;
                }

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == term && w.nYear == nyear && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }


                var check = _db.TGradeRegisterPeriods.Where(w => w.nTerm == nterm).FirstOrDefault();
                if (check == null)
                {
                    int countID = _db.TGradeRegisterPeriods.Count() == 0 ? 0 : _db.TGradeRegisterPeriods.Max(max => max.nGradeRegisterPeriod);
                    countID = countID + 1;

                    TGradeRegisterPeriod Period = new TGradeRegisterPeriod();
                    if (beforeMid1.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(beforeMid1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Period.beforeMidtermStart = data;
                    }
                    if (beforeMid2.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(beforeMid2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Period.beforeMidtermEnd = data;
                    }

                    if (duringMid1.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(duringMid1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Period.duringMidtermStart = data;
                    }

                    if (duringMid2.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(duringMid2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Period.duringMidtermEnd = data;
                    }

                    if (afterMid1.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(afterMid1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Period.afterMidtermStart = data;
                    }

                    if (afterMid2.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(afterMid2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Period.afterMidtermEnd = data;
                    }

                    if (final1.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(final1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Period.FinaltermStart = data;
                    }

                    if (final2.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(final2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Period.FinaltermEnd = data;
                    }

                    if (extra1.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(extra1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Period.ExtraStart = data;
                    }

                    if (extra2.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(extra2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        Period.ExtraEnd = data;
                    }

                    Period.lastUpdate = DateTime.Now;
                    Period.nGradeRegisterPeriod = countID;
                    Period.nTerm = nterm;
                    Period.updateByEmp = sEmpID;
                    _db.TGradeRegisterPeriods.Add(Period);
                }
                else
                {
                    check.updateByEmp = sEmpID;
                    check.lastUpdate = DateTime.Now;
                    TGradeRegisterPeriod Period = new TGradeRegisterPeriod();
                    if (beforeMid1.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(beforeMid1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        check.beforeMidtermStart = data;
                    }
                    else check.beforeMidtermStart = null;
                    if (beforeMid2.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(beforeMid2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        check.beforeMidtermEnd = data;
                    }
                    else check.beforeMidtermEnd = null;
                    if (duringMid1.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(duringMid1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        check.duringMidtermStart = data;
                    }
                    else check.duringMidtermStart = null;
                    if (duringMid2.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(duringMid2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        check.duringMidtermEnd = data;
                    }
                    else check.duringMidtermEnd = null;
                    if (afterMid1.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(afterMid1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        check.afterMidtermStart = data;
                    }
                    else check.afterMidtermStart = null;
                    if (afterMid2.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(afterMid2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        check.afterMidtermEnd = data;
                    }
                    else check.afterMidtermEnd = null;
                    if (final1.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(final1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        check.FinaltermStart = data;
                    }
                    else check.FinaltermStart = null;
                    if (final2.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(final2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        check.FinaltermEnd = data;
                    }
                    else check.FinaltermEnd = null;
                    if (extra1.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(extra1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        check.ExtraStart = data;
                    }
                    else check.ExtraStart = null;
                    if (extra2.Text != "")
                    {
                        DateTime data = DateTime.ParseExact(extra2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        check.ExtraEnd = data;
                    }
                    else check.ExtraEnd = null;
                }


                _db.SaveChanges();
                Response.Redirect("scoreRegisterSetting.aspx?year=" + year + "&term=" + term);
            }





            //protected class sortList
            //{
            //    public string planId { get; set; }
            //    public int? sortnumberType { get; set; }
            //    public int? sortnumberGroup { get; set; }
            //    public string planName { get; set; }
            //    public string planCode { get; set; }
            //}


        }
    }
}