using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;
using JabjaiMainClass;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for Handler2
    /// </summary>
    public class Handler2 : IHttpHandler , IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            string sHtml = "";

            string SQL = @"SELECT TOP 3 nEmp,dLog,sLog,nSession,sName + ' ' + sLastname AS sName,dsLog
            FROM (SELECT CONVERT(VARCHAR(10),dLog,101) AS dsLog,* FROM TSystemlog) AS TSystemlog 
            INNER JOIN TEmployees ON TSystemlogs.nEmp = TEmployees.sEmp
            WHERE sLog LIKE 'เติมเงินเข้าระบบให้คุณ%' ORDER BY dLog DESC";

            DataTable _dt = fcommon.Get_Data(_conn, SQL);
            if (_dt.Rows.Count == 0)
            {
                sHtml = "ไม่มีข้อมูลที่ต้องการค้นหา";
                return;
            }
            sHtml = @"<table class='table table-condensed table-add-money' style='width: 100%;'>";
            DataTable _dtGroup = _dt.AsEnumerable().GroupBy(r => new { DSELL = r["dsLog"] })
                .Select(g => g.OrderBy(r => r["dsLog"]).First()).OrderByDescending(o => o["dsLog"])
                .CopyToDataTable();
            string _sHtml = @"<tr style='background-color:#337AB7;color:#fff'>
                                        <td style='width: 35%;text-align:center;'>ชื่อ - นามสกุล</td>
                                        <td style='width: 40%;text-align:center'>เวลา</td>
                                        <td style='width: 25%;text-align:center'>ยอดเงิน</td>
                                </tr>";

            foreach (DataRow _dr in _dt.Rows)
            {
                string[] _str = (_dr["sLog"] + "").Split(new string[] { "จำนวน" }, StringSplitOptions.None);
                _sHtml += @" <tr class='active'>
                                        <td>" + (_str[0] + "").Replace("เติมเงินเข้าระบบให้คุณ ", "").Replace(" ", " ") + @"</td>
                                        <td>" + Convert.ToDateTime(_dr["dLog"] + "").ToString("dd/MM/yyyy HH:mm:ss") + @"</td>
                                        <td>" + _str[1] + @"</td>
                                </tr>";
            }

            sHtml += _sHtml + "</table>";

            context.Response.Expires = -1;
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(sHtml);
            context.Response.End();
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