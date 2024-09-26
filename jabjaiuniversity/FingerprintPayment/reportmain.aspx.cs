using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.App_Code
{
    public partial class reportsmain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            if (!Page.IsPostBack)
            {
                ListMenu();
            }
        }

        private void ListMenu()
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            int sEmp = int.Parse(Session["sEmpID"] + "");
            string sClaimReport = _db.TEmployees.Where(w => w.sEmp == sEmp).SingleOrDefault().sStatusReport;
            ltrReportMenu.Text = @"  <div class='detail-card box-content'>
        <div id='main' class='row center'>
            <a href='#' id='link' class='btn card box-content mini hover long-btn'>+ รายงานการซื้อ-ขายสินค้า</a>
        </div>";
            string _sFormat = @"<div class='row center' id='sub'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<a href='report0{1}.aspx' class='btn card box-content mini hover long-btn'>{0}</a></div>";
            if (!string.IsNullOrEmpty(sClaimReport))
            {
                for (int _index = 0; _index < sClaimReport.Length; _index++)
                {
                    string _str = sClaimReport.Substring(_index, 1);
                    if (_str == "1" && _index == 0)
                    {
                        ltrReportMenu.Text += string.Format(_sFormat, @"รายงานการซื้อ-ขายสินค้าอย่างละเอียด", 3);
                    }
                    if (_str == "1" && _index == 1)
                    {
                        ltrReportMenu.Text += string.Format(_sFormat, @"รายงานขายสินค้าแบบรวบยอด", 8);
                    }
                    if (_str == "1" && _index == 2)
                    {
                        ltrReportMenu.Text += string.Format(_sFormat, @"รายงานการขายสินค้าเงินสด", 5);
                    }
                    if (_str == "1" && _index == 3)
                    {
                        ltrReportMenu.Text += string.Format(_sFormat, @"รายงานการการเต็มเงิน", 9);
                    }
                }
            }
            ltrReportMenu.Text += @"     <div class='row center'>
            <a href='report04.aspx' class='btn card box-content mini hover long-btn'>รายงานยอดคงเหลือสินค้า</a>
        </div>
    </div>";
        }
    }
}