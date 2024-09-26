using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class report09 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            //btnSearch.Click += new EventHandler(btnSearch_Click);
            if (!Page.IsPostBack)
            {
                //DataTable _dt = fcommon.LinqToDataTable(_db.TSubLevels.ToList());
                //fcommon.ListDataTableToDropDownList(_dt, ddlcType, "ทั้งหมด", "nTSubLevel", "SubLevel");
            }
        }

        //void btnSearch_Click(object sender, EventArgs e)
        //{
        //    JabJaiMasterEntities dbmaster = Connection.MasterEntities();
        //    DateTime Daystart = string.IsNullOrEmpty(txtdEnd.Text) ? DateTime.Today : DateTime.ParseExact(txtdStart.Text, "MM/dd/yyyy", new CultureInfo("en-us"));
        //    DateTime DayEnd = string.IsNullOrEmpty(txtdEnd.Text) ? Daystart.AddDays(1) : DateTime.ParseExact(txtdEnd.Text, "MM/dd/yyyy", new CultureInfo("en-us")).AddDays(1);

        //    string sEntities = Session["sEntities"].ToString();
        //    TCompany tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
        //    var tConnect = dbmaster.TConnect.Where(w => w.dList.Value >= Daystart && w.dList.Value <= DayEnd && w.cTypeConnect == "TU" && w.cStatus == "3").ToList();

        //    var tCustomer = (from a in dbmaster.TUsers.ToList()
        //                     join b in tConnect on a.sID equals b.CustomerID
        //                     where a.nCompany == tCompany.nCompany
        //                     select new { sName = a.sName + " " + a.sLastname, b.nConnectID, b.dList, b.sDisplay }).ToList();

        //    var tEmployess = (from a in dbmaster.TUsers.ToList()
        //                      join b in tConnect on a.sID equals b.EmployessID
        //                      where a.nCompany == tCompany.nCompany
        //                      select new { sName = a.sName + " " + a.sLastname, b.nConnectID }).ToList();

        //    var ReportTopup = (from a in tCustomer
        //                       join b in tEmployess on a.nConnectID equals b.nConnectID
        //                       select new { CustomerName = a.sName, EmployessName = b.sName, a.dList, a.sDisplay });

        //    ltrHtml.Text = @"<table class='table table-condensed table-bordered' style='width: 100%; font-size:20px;'>";
        //    string _sHtml = "";
        //    for (int i = 0; DayEnd.AddDays(-i) >= Daystart; i++)
        //    {
        //        DateTime DayReports = DayEnd.AddDays(-i);
        //        _sHtml += @" <tr>
        //                                <td colspan='4' style='text-align:right;'>" + (_sHtml == "" ? "" : "<br/>") + DayReports.ToString("dd/MM/yyyy") + @"</td>
        //                        </tr>
        //                        <tr class='warning'>
        //                                <td style='width: 25%;'>ชื่อ - นามสกุล</td>
        //                                <td style='width: 35%;'>ชื่อ - นามสกุลซื่อผู้เติม</td>
        //                                <td style='width: 20%;'>เวลา</td>
        //                                <td style='width: 20%;'>ยอดการเติม</td>
        //                        </tr>";
        //        double _sum = 0;

        //        foreach (var dataReport in ReportTopup.Where(w => w.dList.Value <= DayReports.AddDays(1) && w.dList.Value >= DayReports).OrderBy(o => o.dList))
        //        {
        //            _sHtml += @" <tr class='active'>
        //                                <td style='text-align:left;'>" + dataReport.EmployessName + @"</td>
        //                                <td style='text-align:left;'>" + dataReport.CustomerName + @"</td>
        //                                <td style='text-align:center;'>" + dataReport.dList.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"</td>
        //                                <td style='text-align:right;'>" + dataReport.sDisplay + @"</td>
        //                        </tr>";
        //            _sum += double.Parse(dataReport.sDisplay);
        //        }

        //        _sHtml += @" <tr style='text-align:left;' class='active'>
        //                                <td style='text-align:right;' colspan=3>ยอดรวม</td>
        //                                <td style='text-align:right;'>" + _sum + @" บาท</td>
        //                        </tr>";
        //    }

        //    ltrHtml.Text += _sHtml + "</table>";
        //}
    }
}