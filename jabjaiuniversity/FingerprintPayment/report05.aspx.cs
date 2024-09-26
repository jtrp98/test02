using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Configuration;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class report05 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            btnSearch2.Click += new EventHandler(btnSearch2_Click);
            btnSearch0.Click += new EventHandler(btnSearch0_Click);
            if (!IsPostBack)
            {
                DataTable _dt = fcommon.LinqToDataTable(_db.TTypes.Where(w => string.IsNullOrEmpty(w.cDel)).OrderBy(Or => Or.sType).ToList());
                fcommon.ListDataTableToDropDownList(_dt, ddlcType, "ทั้งหมด", "nTypeID", "sType");
                int sEmp = int.Parse(Session["sEmpID"] + "");
                string sClaimReport = _db.TEmployees.Where(w => w.sEmp == sEmp).SingleOrDefault().sStatusReport;
                ViewState["sClaim"] = (sClaimReport.Length > 4 && sClaimReport.Substring(4, 1) == "1") ? "0" : "1";
            }
            else
            {
            }
            SetBodyEventOnLoad(@"");
        }
        void btnSearch0_Click(object sender, EventArgs e)
        {
            lodareport();
        }
        void btnSearch2_Click(object sender, EventArgs e)
        {
            Response.Redirect("report05.aspx");
        }
        void btnSearch1_Click(object sender, EventArgs e)
        {
            ViewState["_Mode"] = 2;
            lodareport();
        }
        private void lodareport()
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            string SQL = @"SELECT DSELL,SPRODUCT,TB3.NNUMBER
            ,TB3.NNUMBER * TB4.NPRICE AS NTOTAL,SBARCODE,TB4.NPRICE,'รายงานการซื้อสินค้าในระบบ' AS SREPORTNAME
            ,TB5.sName + ' ' + TB5.sLastname AS sNameEmp, '' as sReportHeader
			,(nPrice - nCost) * TB3.NNUMBER AS Lucre,CONVERT(VARCHAR(10),DSELL,101) AS GDSELL
            FROM TSELL TB1 INNER JOIN TSELL_DETAIL TB3 ON TB1.SSELLID = TB3.NSELL
            INNER JOIN TPRODUCT TB4 ON TB3.NPRODUCT = TB4.NPRODUCTID
            INNER JOIN TEMPLOYEES TB5 ON TB1.NEMP = TB5.SEMP 
            WHERE 1=1 AND TB3.cDel IS NULL AND SPRODUCT LIKE '%" + txtSearch.Text + "%' AND SID = 0";
            SQL += " AND TB5.sName + ' ' + TB5.sLastName like '%" + txtsunit.Text + "%' ";

            try
            {
                if (txtdStart.Text != "" && txtdEnd.Text != "")
                {
                    SQL += @" AND DSELL BETWEEN '" + DateTime.ParseExact(txtdStart.Text, "MM/dd/yyyy", new CultureInfo("en-us")).ToString("MM/dd/yyyy", new CultureInfo("en-us")) +
                        "' AND '" + DateTime.ParseExact(txtdEnd.Text, "MM/dd/yyyy", new CultureInfo("en-us")).ToString("MM/dd/yyyy", new CultureInfo("en-us")) + " 23:59:00' ";
                }
                else if (txtdStart.Text != "")
                {
                    SQL += @" AND DSELL <= '" + Convert.ToDateTime(txtdStart.Text).ToString("MM/dd/yyyy", new CultureInfo("en-us")) + "'";

                }
            }
            catch
            {
                SetBodyEventOnLoad(@"Mgsalert('รูปแบบวันที่ไม่ถูกต้อง');");
                return;
            }
            SQL += " ORDER BY DSELL DESC";


            DataTable _dt = fcommon.Get_Data(_conn, SQL);
            if (_dt.Rows.Count == 0)
            {
                ltrHtml.Text = "ไม่มีข้อมูลที่ต้องการค้นหา";
                return;
            }
            ltrHtml.Text = @"<table class='table table-condensed table-bordered' style='width: 100%; font-size:20px;'>";
            DataTable _dtGroup = _dt.AsEnumerable().GroupBy(r => new { DSELL = r["GDSELL"] })
                .Select(g => g.OrderBy(r => r["GDSELL"]).First()).OrderByDescending(o => o["GDSELL"])
                .CopyToDataTable();
            string _sHtml = "";
            foreach (DataRow _drGroup in _dtGroup.Rows)
            {
                _sHtml += @" <tr>
                                        <td colspan='7' style='text-align:right;'>" + (_sHtml == "" ? "" : "<br/>") + _drGroup["GDSELL"] + @"</td>
                                </tr>
                                <tr class='warning'>
                                        <td style='width: 20%;'>ชื่อ - นามสกุล ผู้ขาย</td>
                                        <td style='width: 16%;'>ชื่อสินค้า</td>
                                        <td style='width: 12%;'>จำนวน</td>
                                        <td style='width: 12%;'>ราคา/หน่วย</td>
                                        <td style='width: 12%;'>ราคารวม</td>
                                        <td style='width: 14%;'>เวลา</td>
                                </tr>";
                double _sum = 0, Lucre = 0;
                foreach (DataRow _dr in _dt.Select("GDSELL ='" + _drGroup["GDSELL"] + "'"))
                {
                    _sHtml += @" <tr class='active'>
                                        <td style='text-align:left;'>" + _dr["sNameEmp"] + @"</td>
                                        <td style='text-align:left;'>" + _dr["SPRODUCT"] + @"</td>
                                        <td style='text-align:center;'>" + _dr["NNUMBER"] + @"</td>
                                        <td style='text-align:center;'>" + _dr["NPRICE"] + @"</td>
                                        <td style='text-align:center;'>" + _dr["NTOTAL"] + @"</td>
                                        <td style='text-align:right;'>" + Convert.ToDateTime(_dr["DSELL"] + "").ToShortTimeString() + @"</td>
                                </tr>";
                    _sum += double.Parse(_dr["NTOTAL"] + "");
                    Lucre += double.Parse(_dr["Lucre"] + "");
                }

                _sHtml += @" <tr style='text-align:left;' class='active'>
                                        <td style='text-align:right;' colspan=4>ยอดรวม</td>
                                        <td style='text-align:right;'colspan=2>" + _sum + @" บาท</td>
                                </tr>";
                if (ViewState["sClaim"] + "" == "1")
                {
                    _sHtml += @" <tr style='text-align:left;' class='active'>
                                        <td style='text-align:right;' colspan=4>กำไร</td>
                                        <td style='text-align:right;'colspan=2>" + Lucre + @" บาท</td>
                                </tr>";
                }
            }

            ltrHtml.Text += _sHtml + "</table>";
        }

        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }
    }
}