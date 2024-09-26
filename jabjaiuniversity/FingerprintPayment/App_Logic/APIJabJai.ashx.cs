using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Globalization;
using System.Data;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for APIJabJai
    /// </summary>
    public class APIJabJai : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string _str = fcommon.ReplaceInjection(context.Request["ID"]);
                string _mode = fcommon.ReplaceInjection(context.Request["Mode"]);
                string userid = fcommon.ReplaceInjection(context.Request["userid"]);
                string tremid = fcommon.ReplaceInjection(context.Request["tremid"]);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string _tmp = "";
                string SQL = "";
                Dictionary<string, object> row;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

                switch (_mode)
                {
                    case "jabjailogin":
                        try
                        {
                            string sUser = fcommon.ReplaceInjection(context.Request["user"]);
                            string sPass = fcommon.ReplaceInjection(context.Request["pass"]);
                            DateTime _date = DateTime.ParseExact(sPass, "ddMMyyyy", new CultureInfo("en-us"));
                            foreach (var _data in _db.TUser.Where(w => w.sIdentification.Equals(sUser) && w.dBirth == _date && w.SchoolID == userData.CompanyID))
                            {
                                _tmp = _data.sID.ToString();
                            }
                            //_tmp = context.Session["sid"] + "";
                        }
                        catch { }
                        context.Response.ContentType = "text/plain";
                        context.Response.AddHeader("Access-Control-Allow-Origin", "http://jabjai.school");
                        context.Response.Write(string.IsNullOrEmpty(_tmp) ? "0" : _tmp);
                        break;
                    case "getstatus":
                        try
                        {
                            SQL = "SELECT TOP 1 * FROM TConnect ORDER BY nConnectID DESC";
                            foreach (DataRow _dr in fcommon.Get_Data(fcommon.connMaster, SQL).Rows)
                            {
                                _tmp = _dr["cStatus"] + "" == "4" ? "1" : (_dr["cStatus"] + "" == "2" ? "-1" : "0");
                            }
                        }
                        catch { }
                        context.Response.ContentType = "text/plain";
                        context.Response.AddHeader("Access-Control-Allow-Origin", "http://jabjai.school");
                        context.Response.Write(string.IsNullOrEmpty(_tmp) ? "0" : _tmp);
                        break;
                    case "msgconfirm":
                        try
                        {
                            SQL = "UPDATE TConnect SET cStatus = '3' WHERE nConnectID = (SELECT TOP 1 nConnectID FROM TConnect ORDER BY nConnectID DESC)";
                            fcommon.ExecuteNonQuery(fcommon.connMaster, SQL);
                        }
                        catch { }
                        context.Response.ContentType = "text/plain";
                        context.Response.AddHeader("Access-Control-Allow-Origin", "http://jabjai.school");
                        context.Response.Write(string.IsNullOrEmpty(_tmp) ? "0" : _tmp);
                        break;
                    case "GetReportmobile02":
                        rows = GetReportmobile02(userid, tremid);
                        context.Response.Expires = -1;
                        context.Response.ContentType = "application/json";
                        context.Response.AddHeader("Access-Control-Allow-Origin", "http://jabjai.school");
                        context.Response.AddHeader("Origin", "*");
                        //context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.Write(serializer.Serialize(rows).ToString());
                        break;
                }
            }
        }
        private List<Dictionary<string, object>> GetReportmobile02(string id, string tremid)
        {
            string _comm = "";
            DateTime _dstart = DateTime.Today.AddDays(-30);
            DateTime _dend = DateTime.Today;

            if (!string.IsNullOrEmpty(id))
            {
                _comm += " AND sEmp = " + id;
            }

            foreach (DataRow _dr in fcommon.Get_Data(fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + ""), " select * from TTerm where nTerm = '" + tremid + "'").Rows)
            {
                _dstart = DateTime.Parse(_dr["dstart"] + "");
                _dend = DateTime.Parse(_dr["dend"] + "");
                if (DateTime.Today < _dend)
                {
                    _dend = DateTime.Today;
                }
            }

            string SQL = @"SELECT sEmp,sName,sLastname,dScan,MyRowNumber,_TimeIn,_TimeOut
            ,StatusIN,StatusOUT,nMoney,nMax,'ตั้งแต่วันที่ " + _dstart.ToString("dd/MM/yyyy") + " - " + _dend.ToString("dd/MM/yyyy") + @"' AS sHeaderReports
            FROM
            (SELECT t.sEmp,t.sName,t.sLastname,t.dScan,ROW_NUMBER() OVER (ORDER BY t.dScan DESC) as MyRowNumber
            ,t.nMoney,t.nMax
            FROM StudentList('" + _dstart.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"','" + _dend.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"') AS t) AS TB2
            LEFT JOIN
            (
            SELECT LogDate,sID,MAX(_TimeIn) AS _TimeIn,MAX(_TimeOut) AS _TimeOut
            ,ISNULL(MAX(StatusIN),'3') AS StatusIN,ISNULL(MAX(StatusOUT),'3') AS StatusOUT
            FROM
            (SELECT  LogDate,sID,LogTime AS _TimeIn,LogScanStatus AS StatusIN,NULL AS '_TimeOut',NULL AS 'StatusOUT'
            FROM TLogUserTimeScan 
            WHERE LogType = '0'
            UNION
            SELECT LogDate,sID,NULL,NULL,LogTime AS _TimeOut,LogScanStatus AS StatusOUT
            FROM TLogUserTimeScan  AS TBOUT
            WHERE LogType = '1') AS TB1
            GROUP BY LogDate,sID) AS TB3 ON TB2.sEmp = TB3.sID AND TB2.dScan = LogDate
            WHERE sEmp = " + id + @"
            ORDER BY dScan DESC,sEmp,sName";
            DataTable dtUser = fcommon.Get_Data(fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + ""), SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dtUser == null) return rows;
            foreach (DataRow dr in dtUser.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtUser.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
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