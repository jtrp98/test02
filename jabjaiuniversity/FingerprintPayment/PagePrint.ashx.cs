using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using JabjaiMainClass;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for PagePrint
    /// </summary>
    public class PagePrint : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            string _str = fcommon.ReplaceInjection(context.Request["ID"]);
            string _filter = HttpUtility.UrlDecode(context.Request["filter"]);
            string _sHtml = "";
            switch (_str)
            {
                case "report04":
                    _sHtml = Printreport04(_filter);
                    break;
                case "report07":
                    _sHtml = PrintReport07(_filter);
                    break;
            }

            context.Response.Expires = -1;
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(_sHtml);
            context.Response.End();
        }
        private string PrintReport07(string _filter)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            string SQL = "";

            SQL = @"select sName,sLastname,sIdentification,dBirth,TSubLevels.SubLevel,nMax,nMoney
             from TUser inner join TStudentLevel on TUsers.sID = TStudentLevels.sID
             inner join TSubLevel ON TSubLevels.nTSubLevel = TStudentLevels.nTSubLevel
            where cType = '0' ";
            string _comm = "";

            if (!string.IsNullOrEmpty(_filter.Split('|')[0]))
            {
                SQL += " AND TStudentLevels.nTSubLevel = " + fcommon.ReplaceInjection(_filter.Split('|')[0]);
            }
            if (!string.IsNullOrEmpty(_filter.Split('|')[1]))
            {
                SQL += " AND sName + ' ' + sLastname LIKE '%" + fcommon.ReplaceInjection(_filter.Split('|')[1]) + "%'";
            }

            if (_filter.Split('|')[2] == "1")
                SQL += " AND nMoney = 0";
            else if (_filter.Split('|')[2] == "2")
                SQL += " AND nMoney > 0";

            DataTable _dt = fcommon.Get_Data(_conn, SQL);
            if (_dt == null || _dt.Rows.Count == 0) return "ไม่พบรายการที่ต้องการค้นหา";
            DataTable _dtGroup = _dt.AsEnumerable().GroupBy(r => new { DSELL = r["SubLevel"] })
             .Select(g => g.OrderBy(r => r["SubLevel"]).First()).CopyToDataTable();

            string _sHtml = "";
            _sHtml = @"<table class='table table-condensed table-bordered' style='width: 100%; font-size:14px;'>";
            foreach (DataRow _drGroup in _dtGroup.Rows)
            {
                _sHtml += @"<tr>
                                    <td colspan='6' style='text-align: right;'>
                                        " + _drGroup["SubLevel"] + @"
                                    </td>
                            </tr>
                            <tr class=""warning"">
                                    <td style=""width: 15%;text-align: center;"">
                                        ชื่อ
                                    </td>
                                    <td style=""width: 15%;text-align: center;"">
                                        นามสกุล
                                    </td>
                                    <td style=""width: 25%;text-align: center;"">
                                        หมายเลขบัตรประชาชน
                                    </td>
                                    <td style=""width: 15%;text-align: center;"">
                                        ว/ด/ป เกิด
                                    </td>
                                    <td style=""text-align: center;"">
                                        ยอดเงินจำกัด
                                    </td>
                                    <td style=""width: 15%;text-align: center;"">
                                        ยอดเงินคงเหลือ
                                    </td>
                            </tr>";

                foreach (DataRow _dr in _dt.Select("SubLevel = '" + _drGroup["SubLevel"] + "'"))
                {
                    _sHtml += @" <tr class='active'>
                                    <td style='text-align: left;'>
                                        " + _dr["sName"] + @"
                                    </td>
                                    <td style='text-align: left;'>
                                        " + _dr["sLastname"] + @"
                                    </td>
                                    <td style='text-align: center;'>
                                        " + _dr["sIdentification"] + @"
                                    </td>
                                    <td style='text-align: right;'>
                                        " + DateTime.Parse(_dr["dBirth"].ToString()).ToShortDateString() + @" 
                                    </td>
                                    <td style='text-align: right;'>
                                        " + _dr["nMax"] + @"  
                                    </td>
                                    <td style='text-align: right;'>
                                      " + _dr["nMoney"] + @"  
                                    </td>
                                </tr>";
                }
            }
            _sHtml += "</table>";
            return _sHtml;
        }
        private string Printreport04(string _filter)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            string SQL = "";
            SQL = @"SELECT [nProductID],[sBarCode],[sProduct],[SellNumber],[ProNumber],[StockNumber],[balances],nType
  FROM [ViewInventory] WHERE (sBarCode LIKE '%" + fcommon.ReplaceInjection(_filter.Split('|')[1]) + "%' OR sProduct LIKE '%" + fcommon.ReplaceInjection(_filter.Split('|')[1]) + "%') ";

            if (_filter.Split('|')[0]!="")
            {
                SQL += "AND nType = " + fcommon.ReplaceInjection(_filter.Split('|')[0]);
            }

            DataTable _dt = fcommon.Get_Data(_conn, SQL);

            string _sHtml = "";
            _sHtml = @"<table class='table table-condensed table-bordered' id='table2excel' style='width: 100%; font-size:20px;'>";
            _sHtml += @"<tr class='noExl warning'>
                                                    <td style='width: 25%;'>BarCode</td>
                                                    <td style='width: 50%;'>ชื่อสินค้า</td>
                                                    <td style='width: 25%;'>คงเหลือ</td>
                                            </tr>";
            foreach (DataRow _dr in _dt.Rows)
            {
                _sHtml += @" <tr class='noExl active'>
                                                    <td style='text-align:left;'>" + _dr["sBarCode"] + @"</td>
                                                    <td style='text-align:left;'>" + _dr["sProduct"] + @"</td>
                                                    <td style='text-align:center;'>" + _dr["balances"] + @"</td>
                                            </tr>";
            }

            _sHtml += "</table>";
            return _sHtml;
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