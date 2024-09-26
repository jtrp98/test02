using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class report08 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            btnSearch.Click += new EventHandler(btnSearch_Click);
            if (!this.IsPostBack)
            {
                DataTable _dt = fcommon.LinqToDataTable(_db.TTypes.Where(w => string.IsNullOrEmpty(w.cDel)).ToList());
                fcommon.ListDataTableToDropDownList(_dt, ddlcType, "ทั้งหมด", "nTypeID", "sType");
            }
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            string _comm = "";
            try
            {
                if (txtdStart.Text != "" && txtdEnd.Text != "")
                {
                    _comm += @" AND DSELL BETWEEN '" + DateTime.ParseExact(txtdStart.Text, "MM/dd/yyyy", new CultureInfo("en-us")).ToString("MM/dd/yyyy", new CultureInfo("en-us")) +
                        "' AND '" + DateTime.ParseExact(txtdEnd.Text, "MM/dd/yyyy", new CultureInfo("en-us")).ToString("MM/dd/yyyy", new CultureInfo("en-us")) + " 23:59:00' ";
                }
                else if (txtdStart.Text != "")
                {
                    _comm += @" AND DSELL <= '" + Convert.ToDateTime(txtdStart.Text).ToString("MM/dd/yyyy", new CultureInfo("en-us")) + "'";

                }
            }
            catch
            {
                SetBodyEventOnLoad(@"Mgsalert('รูปแบบวันที่ไม่ถูกต้อง');");
                return;
            }
            string SQL = "";

            SQL += @"SELECT SPRODUCT,SUM(SELL1) AS SELL1,SUM(SELL0) AS SELL0,(SUM(SELL0)  + SUM(SELL1)) * nPrice AS Lucre
,DSELL,SUM(SELL0)  + SUM(SELL1) AS NNUMBER
FROM (SELECT SPRODUCT,sum(TB3.NNUMBER) AS NNUMBER,SUM(TB3.NNUMBER) * nPrice AS Lucre, DSELL
,CASE WHEN sID = 0 THEN SUM(TB3.nNumber) ELSE 0 END AS SELL1,CASE WHEN sID > 0 THEN SUM(TB3.nNumber) ELSE 0 END AS SELL0
,nPrice
FROM (SELECT CONVERT(VARCHAR(10),DSELL,101) AS DSELL,SSELLID,NEMP,SID  FROM TSELL WHERE 1=1 " + _comm + @") TB1 
INNER JOIN TSELL_DETAIL TB3 ON TB1.SSELLID = TB3.NSELL
INNER JOIN TPRODUCT TB4 ON TB3.NPRODUCT = TB4.NPRODUCTID
INNER JOIN TEMPLOYEES TB5 ON TB1.NEMP = TB5.SEMP 
WHERE 1=1 AND TB3.cDel IS NULL AND SPRODUCT LIKE '%" + txtSearch.Text + @"%' 
GROUP BY SPRODUCT,nPrice,DSELL,sID) AS TBGROUP
GROUP BY SPRODUCT,DSELL,nPrice ORDER BY SPRODUCT";

            //Response.Write(SQL);
            DataTable _dt = fcommon.Get_Data(_conn, SQL);

            if (_dt.Rows.Count == 0)
            {
                ltrHtml.Text = "ไม่มีข้อมูลที่ต้องการค้นหา";
                return;
            }
            ltrHtml.Text = @"<table class='table table-condensed table-bordered' style='width: 100%;'>";
            DataTable _dtGroup = _dt.AsEnumerable().GroupBy(r => new { DSELL = r["DSELL"] })
                .Select(g => g.OrderBy(r => r["DSELL"]).First()).OrderByDescending(o => o["DSELL"])
                .CopyToDataTable();
            string _sHtml = "";
            foreach (DataRow _drGroup in _dtGroup.Rows)
            {
                _sHtml += @" <tr>
                                        <td colspan='5' style='text-align:right;'>" + (_sHtml == "" ? "" : "<br/>") + _drGroup["DSELL"] + @"</td>
                                </tr>
                                <tr class='warning'>
                                        <td style='width: 40%;'>ชื่อสิ้นค้า</td>
                                        <td style='width: 15%;'>ลายนิ้วมือ</td>
                                        <td style='width: 15%;'>เงินสด</td>
                                        <td style='width: 15%;'>จำนวนรวม</td>
                                        <td style='width: 15%;'>ราคารวม</td>
                                </tr>";
                double _sum = 0;
                foreach (DataRow _dr in _dt.Select("DSELL ='" + _drGroup["DSELL"] + "'"))
                {
                    _sHtml += @" <tr class='active'>
                                        <td style='text-align:left;'>" + _dr["SPRODUCT"] + @"</td>
                                        <td style='text-align:center;'>" + _dr["SELL0"] + @"</td>
                                        <td style='text-align:center;'>" + _dr["SELL1"] + @"</td>
                                        <td style='text-align:center;'>" + _dr["NNUMBER"] + @"</td>
                                        <td style='text-align:right;'>" + double.Parse(_dr["Lucre"] + "") + @"</td>
                                </tr>";
                    _sum += double.Parse(_dr["Lucre"] + "");
                }

                _sHtml += @" <tr style='text-align:left;' class='active'>
                                        <td style='text-align:right;' colspan=4>ยอดรวม</td>
                                        <td style='text-align:right;'>" + _sum + @"</td>
                                </tr>";
            }

            ltrHtml.Text += _sHtml + "</table>";
        }

        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);

        }
    }
}