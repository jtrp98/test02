using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using System.Globalization;
using System.Web.DynamicData;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;

namespace FingerprintPayment.Setting
{
    public partial class settingTimePeriod : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                string comid = Request.QueryString["comid"];
                int sEmpID = int.Parse(Session["sEmpID"] + "");
                btnSave.Click += new EventHandler(btnSave_Click);


                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {

                    SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                    if (!IsPostBack)
                    {
                        OpenData();



                        List<TYear> tylist = new List<TYear>();
                        TYear ty = new TYear();
                        foreach (var a in _db.TYears.Where(x => x.SchoolID == schoolid && x.cDel == false).ToList())
                        {
                            ty = new TYear();
                            ty = a;
                            ty.SchoolID = schoolid;
                            tylist.Add(ty);
                        }
                        var newList = tylist.OrderByDescending(x => x.numberYear).ToList();

                        DropDownList1.DataSource = newList;
                        DropDownList1.DataTextField = "numberYear";
                        DropDownList1.DataValueField = "numberYear";
                        DropDownList1.DataBind();

                        string yearr = Request.QueryString["year"];
                        DropDownList1.SelectedValue = yearr;


                        var year = newList.Where(w => w.numberYear.ToString() == DropDownList1.SelectedValue).FirstOrDefault();
                        var tTerms = _db.TTerms.Where(x => x.SchoolID == schoolid && x.nYear == year.nYear).OrderBy(o => o.sTerm).ToList();

                        DropDownList2.DataSource = tTerms;
                        DropDownList2.DataTextField = "sTerm";
                        DropDownList2.DataValueField = "sTerm";
                        DropDownList2.DataBind();

                        string termm = Request.QueryString["term"];
                        DropDownList2.SelectedValue = termm;

                    }
                }
            }
        }


        private void OpenData()
        {

            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    string year = Request.QueryString["year"];
                    string term = Request.QueryString["term"];

                    if (year != null && term != null)
                    {
                        header.Text = "ระยะเวลาที่เปิดให้แก้ไขคะแนน" + " ปีการศึกษา " + year + " เทอม " + term;
                        DropDownList2.SelectedValue = term;
                        int? useryear = Int32.Parse(year);
                        int nyear = 0;
                        string nterm = "";

                        foreach (var ff in _db.TYears.Where(w => w.SchoolID == schoolid && w.numberYear == useryear && w.cDel == false))
                        {
                            nyear = ff.nYear;
                        }

                        foreach (var ee in _db.TTerms.Where(w => w.SchoolID == schoolid && w.sTerm == term && w.nYear == nyear))
                        {
                            nterm = ee.nTerm;
                        }

                        var data = _db.TGradeRegisterPeriods.Where(w => w.SchoolID == schoolid && w.nTerm == nterm).FirstOrDefault();
                        if (data != null)
                        {
                            if (data.beforeMidtermStart == null)
                                mid1.Text = "";
                            else
                            {
                                mid1.Text = monthtxt(data.beforeMidtermStart);
                                beforeMid1.Text = onedigit(data.beforeMidtermStart.Value.Day.ToString()) + "/" + onedigit(data.beforeMidtermStart.Value.Month.ToString()) + "/" + data.beforeMidtermStart.Value.Year;
                            }

                            if (data.beforeMidtermEnd == null)
                                mid2.Text = "";
                            else
                            {
                                mid2.Text = monthtxt(data.beforeMidtermEnd);
                                beforeMid2.Text = onedigit(data.beforeMidtermEnd.Value.Day.ToString()) + "/" + onedigit(data.beforeMidtermEnd.Value.Month.ToString()) + "/" + data.beforeMidtermEnd.Value.Year;
                            }

                            if (data.duringMidtermStart == null)
                                mid3.Text = "";
                            else
                            {
                                mid3.Text = monthtxt(data.duringMidtermStart);
                                duringMid1.Text = onedigit(data.duringMidtermStart.Value.Day.ToString()) + "/" + onedigit(data.duringMidtermStart.Value.Month.ToString()) + "/" + data.duringMidtermStart.Value.Year;
                            }

                            if (data.duringMidtermEnd == null)
                                mid4.Text = "";
                            else
                            {
                                mid4.Text = monthtxt(data.duringMidtermEnd);
                                duringMid2.Text = onedigit(data.duringMidtermEnd.Value.Day.ToString()) + "/" + onedigit(data.duringMidtermEnd.Value.Month.ToString()) + "/" + data.duringMidtermEnd.Value.Year;
                            }

                            if (data.afterMidtermStart == null)
                                mid5.Text = "";
                            else
                            {
                                mid5.Text = monthtxt(data.afterMidtermStart);
                                afterMid1.Text = onedigit(data.afterMidtermStart.Value.Day.ToString()) + "/" + onedigit(data.afterMidtermStart.Value.Month.ToString()) + "/" + data.afterMidtermStart.Value.Year;
                            }

                            if (data.afterMidtermEnd == null)
                                mid6.Text = "";
                            else
                            {
                                mid6.Text = monthtxt(data.afterMidtermEnd);
                                afterMid2.Text = onedigit(data.afterMidtermEnd.Value.Day.ToString()) + "/" + onedigit(data.afterMidtermEnd.Value.Month.ToString()) + "/" + data.afterMidtermEnd.Value.Year;
                            }

                            if (data.FinaltermStart == null)
                                fin1.Text = "";
                            else
                            {
                                fin1.Text = monthtxt(data.FinaltermStart);
                                final1.Text = onedigit(data.FinaltermStart.Value.Day.ToString()) + "/" + onedigit(data.FinaltermStart.Value.Month.ToString()) + "/" + data.FinaltermStart.Value.Year;
                            }

                            if (data.FinaltermEnd == null)
                                fin2.Text = "";
                            else
                            {
                                fin2.Text = monthtxt(data.FinaltermEnd);
                                final2.Text = onedigit(data.FinaltermEnd.Value.Day.ToString()) + "/" + onedigit(data.FinaltermEnd.Value.Month.ToString()) + "/" + data.FinaltermEnd.Value.Year;
                            }

                            if (data.ExtraStart == null)
                                ex1.Text = "";
                            else
                            {
                                ex1.Text = monthtxt(data.ExtraStart);
                                extra1.Text = onedigit(data.ExtraStart.Value.Day.ToString()) + "/" + onedigit(data.ExtraStart.Value.Month.ToString()) + "/" + data.ExtraStart.Value.Year;
                            }

                            if (data.ExtraEnd == null)
                                ex2.Text = "";
                            else
                            {
                                ex2.Text = monthtxt(data.ExtraEnd);
                                extra2.Text = onedigit(data.ExtraEnd.Value.Day.ToString()) + "/" + onedigit(data.ExtraEnd.Value.Month.ToString()) + "/" + data.ExtraEnd.Value.Year;
                            }

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
            }
        }

        string monthtxt(DateTime? original)
        {


            string txt1 = original.Value.Day.ToString();
            string txt2 = original.Value.Month.ToString();
            string txt3 = original.Value.Year.ToString();

            if (txt2 == "1")
                txt2 = "ม.ค.";
            else if (txt2 == "2")
                txt2 = "ก.พ.";
            else if (txt2 == "3")
                txt2 = "มี.ค.";
            else if (txt2 == "4")
                txt2 = "เม.ย.";
            else if (txt2 == "5")
                txt2 = "พ.ค.";
            else if (txt2 == "6")
                txt2 = "มิ.ย.";
            else if (txt2 == "7")
                txt2 = "ก.ค.";
            else if (txt2 == "8")
                txt2 = "ส.ค.";
            else if (txt2 == "9")
                txt2 = "ก.ย.";
            else if (txt2 == "10")
                txt2 = "ต.ค.";
            else if (txt2 == "11")
                txt2 = "พ.ย.";
            else if (txt2 == "12")
                txt2 = "ธ.ค.";

            int year = int.Parse(txt3);
            if (year < 2500)
                year = year + 543;


            return txt1 + " " + txt2 + " " + year.ToString();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    string year = Request.QueryString["year"];
                    string term = Request.QueryString["term"];
                    int sEmpID = int.Parse(Session["sEmpID"] + "");

                    int? useryear = Int32.Parse(year);
                    int nyear = 0;
                    string nterm = "";

                    foreach (var ff in _db.TYears.Where(w => w.SchoolID == schoolid && w.numberYear == useryear && w.cDel == false))
                    {
                        nyear = ff.nYear;
                    }

                    foreach (var ee in _db.TTerms.Where(w => w.SchoolID == schoolid && w.sTerm == term && w.nYear == nyear))
                    {
                        nterm = ee.nTerm;
                    }


                    var check = _db.TGradeRegisterPeriods.Where(w => w.SchoolID == schoolid && w.nTerm == nterm).FirstOrDefault();
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
                        else Period.beforeMidtermStart = null;


                        if (beforeMid2.Text != "")
                        {
                            DateTime data = DateTime.ParseExact(beforeMid2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Period.beforeMidtermEnd = data;
                        }
                        else Period.beforeMidtermEnd = null;


                        if (duringMid1.Text != "")
                        {
                            DateTime data = DateTime.ParseExact(duringMid1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Period.duringMidtermStart = data;
                        }
                        else Period.duringMidtermStart = null;

                        if (duringMid2.Text != "")
                        {
                            DateTime data = DateTime.ParseExact(duringMid2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Period.duringMidtermEnd = data;
                        }
                        else Period.duringMidtermEnd = null;

                        if (afterMid1.Text != "")
                        {
                            DateTime data = DateTime.ParseExact(afterMid1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Period.afterMidtermStart = data;
                        }
                        else Period.afterMidtermStart = null;

                        if (afterMid2.Text != "")
                        {
                            DateTime data = DateTime.ParseExact(afterMid2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Period.afterMidtermEnd = data;
                        }
                        else Period.afterMidtermEnd = null;

                        if (final1.Text != "")
                        {
                            DateTime data = DateTime.ParseExact(final1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Period.FinaltermStart = data;
                        }
                        else Period.FinaltermStart = null;

                        if (final2.Text != "")
                        {
                            DateTime data = DateTime.ParseExact(final2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Period.FinaltermEnd = data;
                        }
                        else Period.FinaltermEnd = null;

                        if (extra1.Text != "")
                        {
                            DateTime data = DateTime.ParseExact(extra1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Period.ExtraStart = data;
                        }
                        else Period.ExtraStart = null;

                        if (extra2.Text != "")
                        {
                            DateTime data = DateTime.ParseExact(extra2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Period.ExtraEnd = data;
                        }
                        else Period.ExtraEnd = null;

                        Period.SchoolID = schoolid;
                        Period.lastUpdate = DateTime.Now;
                        Period.nGradeRegisterPeriod = countID;
                        Period.nTerm = nterm;
                        Period.updateByEmp = sEmpID;
                        _db.TGradeRegisterPeriods.Add(Period);
                    }
                    else
                    {
                        check.SchoolID = schoolid;
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
                    Response.Redirect("settingTimePeriod.aspx?year=" + year + "&term=" + term);
                }
            }
        }

        protected string onedigit(string from)
        {
            string txt = from;
            if (from.Length == 1)
                txt = "0" + from;
            return txt;
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