using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Web.SessionState;
using JabjaiEntity.DB;
using System.Text;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for dataReportGeneric
    /// </summary>
    public class dataReportGeneric : IHttpHandler, IRequiresSessionState
    {
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiEntities _db = new JabJaiEntities();
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + ""))
            {
                _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
                SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            }
            string _mode = fcommon.ReplaceInjection(context.Request["Mode"]);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            string sEntities = HttpContext.Current.Session["sEntities"] + "";
            string userid = fcommon.ReplaceInjection(context.Request["userid"]);
            string dstart = fcommon.ReplaceInjection(context.Request["dstart"]);
            string dend = fcommon.ReplaceInjection(context.Request["dend"]);
            string status = fcommon.ReplaceInjection(context.Request["status"]);
            string page = fcommon.ReplaceInjection(context.Request["page"]);
            string tremid = fcommon.ReplaceInjection(context.Request["tremid"]);
            string sublv = fcommon.ReplaceInjection(context.Request["sublv"]);
            string sublv2 = fcommon.ReplaceInjection(context.Request["sublv2"]);
            switch (_mode)
            {
                case "GetReportmobileall":
                    rows = GetReportmobileall();
                    break;
                case "GetReportmobile01":
                    rows = GetReportmobile01(userid, dstart, dend, status, page);
                    break;
                case "GetHeaderReportmobile01":
                    rows = GetHeaderReportmobile01(userid, dstart, dend, status);
                    break;
                case "GetReportmobile02":
                    rows = GetReportmobile02(userid, tremid);
                    break;
                case "GetReportmobile03":
                    rows = GetReportmobile03(userid, tremid, page, sublv, sublv2, status, dstart, dend);
                    break;
                case "GetHeaderReportmobile03":
                    rows = GetHeaderReportmobile03(userid, tremid, sublv, sublv2, status, dstart, dend);
                    break;
                case "ListScan":
                    rows = ListScan(sEntities);
                    break;
                case "NotificationLsit":
                    rows = NotificationLsit(sEntities, status);
                    break;
                case "NotificationLsit_count":
                    rows = NotificationLsit(sEntities, status);
                    context.Response.Expires = -1;
                    context.Response.ContentType = "text/plain";
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.Write(rows.ToList().Count());
                    context.Response.End();
                    return;
                case "GetReportChart01":
                    rows = GetReportChart01();
                    break;
                case "reportsbuyitem":
                    break;
            }
            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(serializer.Serialize(rows).ToString());
            context.Response.End();
        }

        private List<Dictionary<string, object>> NotificationLsit(string sEntities, string status)
        {
            DateTime _dstart = DateTime.Today.AddDays(-30);
            DateTime _dend = DateTime.Today;

            string SQL = @"select " + (status == "1" ? " top 3 " : "") + @"* from(
            SELECT sName + ' ' + sLastname AS sName
            ,LeaveStatus,dLeaveEnd,dLeaveStart,nLeave
            FROM TLeave
            inner join TEmployees on  TEmployees.sEmp = TLeaves.sID AND cTypeID = 'E'

            union

            SELECT sName + ' ' + sLastname AS sName
            ,LeaveStatus,dLeaveEnd,dLeaveStart,nLeave
            FROM TLeave
            inner join TUser on  TUsers.sID = TLeaves.sID AND cTypeID = 'U') AS tb
            where TUser.schoolID = " + userData.CompanyID + @" AND ( '" + DateTime.Today.ToString("MM/dd/yyyy", new CultureInfo("en-us")) + @"' between dLeaveStart and dLeaveStart  
            AND nLeave in (select nMessageID from TNotification where nUserID = '" + HttpContext.Current.Session["sEmpID"] + @"' AND cStatus is null)) or  
            nLeave not in (select nMessageID from TNotification where nUserID = '" + HttpContext.Current.Session["sEmpID"] + "')";

            SqlConnection _conn = fcommon.ConfigSqlConnection(sEntities);
            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dtUser == null) return rows;
            string Comm = "";
            string nNotificationID = fcommon.Get_Value(_conn, "select top 1 nNotificationID from TNotification order by nNotificationID desc");
            if (string.IsNullOrEmpty(nNotificationID)) nNotificationID = "0";
            foreach (DataRow dr in dtUser.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtUser.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                if (status == "1")
                {
                    DateTime dLeaveEnd = DateTime.Parse(dr["dLeaveEnd"] + "");
                    DateTime dLeaveStart = DateTime.Parse(dr["dLeaveStart"] + "");
                    if (DateTime.Today >= dLeaveStart && DateTime.Today <= dLeaveEnd)
                    {
                        DataTable _dt = fcommon.Get_Data(_conn, "select * from TNotification where nUserID = '" + HttpContext.Current.Session["sEmpID"] + "' AND nMessageID = '" + dr["nLeave"] + "' and cTypeMessage = '1'");
                        if (_dt.Rows.Count == 0)
                        {
                            nNotificationID = (int.Parse(nNotificationID) + 1).ToString();
                            Comm += string.Format(@"INSERT INTO TNotification 
                            (nNotificationID, nUserID, cType, nMessageID, cTypeMessage)
                            VALUES ({0}, {1}, '1', {2}, '1')", nNotificationID, HttpContext.Current.Session["sEmpID"], dr["nLeave"] + "");
                        }
                        else
                        {
                            Comm += string.Format(@"UPDATE TNotification SET cStatus = '1' WHERE nUserID = '{1}' AND nMessageID = '{2}'"
                            , nNotificationID, HttpContext.Current.Session["sEmpID"], dr["nLeave"] + "");
                        }
                        Comm += "";
                    }
                    else
                    {
                        nNotificationID = (int.Parse(nNotificationID) + 1).ToString();
                        Comm += string.Format(@"INSERT INTO TNotification
                        (nNotificationID, nUserID, cType, nMessageID, cTypeMessage)
                        VALUES ({0}, {1}, '1', {2}, '1')", nNotificationID, HttpContext.Current.Session["sEmpID"], dr["nLeave"] + "");
                    }

                }

                rows.Add(row);
            }
            if (!string.IsNullOrEmpty(Comm)) fcommon.ExecuteNonQuery(_conn, Comm);
            return rows;
        }

        private List<Dictionary<string, object>> ListScan(string sEntities)
        {
            string _comm = "";
            DateTime _dstart = DateTime.Today.AddDays(-30);
            DateTime _dend = DateTime.Today;

            int _index = 1;

            string SQL = @"SELECT * FROM (
            SELECT TOP 1 sName + ' ' + sLastname AS sName ,LogEmpTime ,LogEmpScanStatus,LogEmpType
            FROM TEmployees INNER JOIN TLogEmpTimeScan ON TLogEmpTimeScans.sEmp = TEmployees.sEmp
            WHERE LogEmpType = '0' AND LogEmpDate =  '" + _dend.ToString("MM/dd/yyyy") + @"'
            ORDER BY nLogEmpScanID DESC
            UNION
            SELECT TOP 1 sName + ' ' + sLastname AS sName ,LogTime ,LogScanStatus,LogType
            FROM TUser INNER JOIN TLogUserTimeScan ON TLogUserTimeScans.sID = TUsers.sID
            WHERE LogType = '0' AND LogDate = '" + _dend.ToString("MM/dd/yyyy") + @"'
            ORDER BY nLogScanID DESC
            UNION

            SELECT TOP 1 sName + ' ' + sLastname AS sName ,LogEmpTime ,LogEmpScanStatus,LogEmpType
            FROM TEmployees INNER JOIN TLogEmpTimeScan ON TLogEmpTimeScans.sEmp = TEmployees.sEmp
            WHERE LogEmpType = '1' AND LogEmpDate =  '" + _dend.ToString("MM/dd/yyyy") + @"'
            ORDER BY nLogEmpScanID DESC
            UNION
            SELECT TOP 1 sName + ' ' + sLastname AS sName ,LogTime ,LogScanStatus,LogType
            FROM TUser INNER JOIN TLogUserTimeScan ON TLogUserTimeScans.sID = TUsers.sID
            WHERE LogType = '1' AND LogDate =  '" + _dend.ToString("MM/dd/yyyy") + @"'
            ORDER BY nLogScanID DESC
            ) AS TB ORDER BY LogEmpTime";

            SqlConnection _conn = fcommon.ConfigSqlConnection(sEntities);
            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dtUser == null) return rows;
            foreach (DataRow dr in dtUser.Select().Skip(((_index * 20) - 20)).Take(20))
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

        private List<Dictionary<string, object>> GetReportmobileall()
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            DateTime _dstart = DateTime.Today.AddDays(-30);
            DateTime _dend = DateTime.Today;

            int _index = 1;

            string SQL = @"SELECT * FROM TScore WHERE 1=1 ";

            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dtUser == null) return rows;
            foreach (DataRow dr in dtUser.Select().Skip(((_index * 20) - 20)).Take(20))
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

        private List<Dictionary<string, object>> GetHeaderReportmobile03(string id, string tremid, string sublv, string sublv2, string status, string dstart, string dend)
        {
            string _comm = "", _comm2 = "";
            DateTime _dstart = DateTime.Today.AddDays(-30);
            DateTime _dend = DateTime.Today;

            if (!string.IsNullOrEmpty(id))
            {
                _comm += " AND sEmp = " + id;
            }

            if (!string.IsNullOrEmpty(sublv2)) _comm2 += " AND t.nTermSubLevel2  = " + sublv2;
            else if (!string.IsNullOrEmpty(sublv)) _comm2 += " AND t.nTSubLevel  = " + sublv;

            if (string.IsNullOrEmpty(dstart))
            {
                foreach (DataRow _dr in fcommon.Get_Data(fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + ""), " select * from TTerm where nTerm = '" + tremid + "'").Rows)
                {
                    _dstart = DateTime.Parse(_dr["dstart"] + "");
                    _dend = DateTime.Parse(_dr["dend"] + "");
                    if (DateTime.Today < _dend)
                    {
                        _dend = DateTime.Today;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(dstart))
                {
                    try
                    {
                        _dstart = DateTime.ParseExact(dstart, "dd/MM/yyyy", new CultureInfo("en-us"));
                    }
                    catch { }
                }
                if (!string.IsNullOrEmpty(dend))
                {
                    try
                    {
                        _dend = DateTime.ParseExact(dend, "dd/MM/yyyy", new CultureInfo("en-us"));
                    }
                    catch { }
                }
            }

            if (status != "")
            {
                if (status != "3")
                {
                    status = "AND StatusIN = '" + status + "'";
                }
                else
                {
                    status = "AND StatusIN = '3' OR StatusIN IS NULL";
                }

            }

            string SQL = @"SELECT StatusIN,CASE WHEN ISNULL(StatusIN,'3') = '0' THEN COUNT(*) END AS 'Status_0'
            ,CASE WHEN ISNULL(StatusIN,'3') = '1' THEN COUNT(*) END AS 'Status_1'
            ,CASE WHEN ISNULL(StatusIN,'3') = '3' OR StatusIN IS NULL THEN COUNT(*) END AS 'Status_2'
            FROM
            (SELECT t.sEmp,t.sName,t.sLastname,t.dScan,ROW_NUMBER() OVER (ORDER BY t.dScan DESC) as MyRowNumber
            ,t.nMoney,t.nMax
            FROM StudentList('" + _dstart.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"',
            '" + _dend.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"') AS t where 1=1 " + _comm2 + @" ) AS TB2
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
            WHERE 1 = 1 " + status + _comm + @" GROUP BY StatusIN ";
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
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
        private List<Dictionary<string, object>> GetReportmobile03(string id, string tremid, string page, string sublv, string sublv2, string status, string dstart, string dend)
        {
            string _comm = "", _comm2 = "";
            int _index = 1;
            if (!string.IsNullOrEmpty(page)) _index = int.Parse(page) == 0 ? 1 : int.Parse(page);
            //_comm += " and MyRowNumber between " + ((_index * 20) - 19) + " and " + (_index * 20) + "";
            DateTime _dstart = DateTime.Today.AddDays(-30);
            DateTime _dend = DateTime.Today;
            if (!string.IsNullOrEmpty(sublv)) _comm2 += " AND t.nTSubLevel  = " + sublv;
            if (!string.IsNullOrEmpty(sublv2)) _comm2 += " AND t.nTermSubLevel2  = " + sublv2;

            if (!string.IsNullOrEmpty(id))
            {
                _comm2 += " AND t.sEmp = " + id;
            }

            if (string.IsNullOrEmpty(dstart))
            {
                foreach (DataRow _dr in fcommon.Get_Data(fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + ""), " select * from TTerm where nTerm = '" + tremid + "'").Rows)
                {
                    _dstart = DateTime.Parse(_dr["dstart"] + "");
                    _dend = DateTime.Parse(_dr["dend"] + "");
                    if (DateTime.Today < _dend)
                    {
                        _dend = DateTime.Today;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(dstart))
                {
                    try
                    {
                        _dstart = DateTime.ParseExact(dstart, "dd/MM/yyyy", new CultureInfo("en-us"));
                    }
                    catch { }
                }
                if (!string.IsNullOrEmpty(dend))
                {
                    try
                    {
                        _dend = DateTime.ParseExact(dend, "dd/MM/yyyy", new CultureInfo("en-us"));
                    }
                    catch { }
                }
            }

            if (status != "")
            {
                if (status != "3")
                {
                    status = "AND StatusIN = '" + status + "'";
                }
                else
                {
                    status = "AND StatusIN = '3' OR StatusIN IS NULL";
                }

            }

            string SQL = @"SELECT sEmp,sName + ' ' + sLastname AS 'sName',dScan,_TimeIn AS 'dIn',_TimeOut AS 'dOut'
             ,ISNULL(StatusIN,'3') AS SIN,StatusOUT AS SOUT,TB2.SubLevel , TB2.nTSubLevel2
            FROM
            (SELECT t.sEmp,t.sName,t.sLastname,t.dScan,ROW_NUMBER() OVER (ORDER BY t.dScan DESC) as MyRowNumber
            ,t.nMoney,t.nMax,CONVERT(varchar,t.nTSubLevel2) AS nTSubLevel2,t.SubLevel
            FROM StudentList('" + _dstart.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"'
            ,'" + _dend.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"') AS t where 1=1 " + _comm2 + @") AS TB2
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
            WHERE 1 = 1 " + status + _comm + @"
            ORDER BY dScan DESC,TB2.SubLevel,TB2.nTSubLevel2,sName";
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dtUser == null) return rows;
            foreach (DataRow dr in dtUser.Select().Skip(((_index * 20) - 20)).Take(20))
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
        private List<Dictionary<string, object>> GetReportmobile02(string id, string tremid)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
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
            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
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
        private List<Dictionary<string, object>> GetHeaderReportmobile01(string id, string dstart, string dend, string status)
        {
            string _comm = "";
            DateTime _dstart = DateTime.Today.AddDays(-30);
            DateTime _dend = DateTime.Today;

            if (!string.IsNullOrEmpty(id))
            {
                _comm += " AND sEmp = " + id;
            }
            if (!string.IsNullOrEmpty(dstart))
            {
                try
                {
                    _dstart = DateTime.ParseExact(dstart, "dd/MM/yyyy", new CultureInfo("en-us"));
                }
                catch { }
            }
            if (!string.IsNullOrEmpty(dend))
            {
                try
                {
                    _dend = DateTime.ParseExact(dend, "dd/MM/yyyy", new CultureInfo("en-us"));
                }
                catch { }
            }
            if (status != "")
            {
                if (status != "3")
                {
                    status = "AND SIN = '" + status + "'";
                }
                else
                {
                    status = "AND SIN = '3' OR SIN IS NULL";
                }

            }
            string SQL = @"SELECT SIN AS 'StatusIN',CASE WHEN ISNULL(SIN,'3') = '0' THEN COUNT(*) END AS 'Status_0'
            ,CASE WHEN ISNULL(SIN,'1') = '1' THEN COUNT(*) END AS 'Status_1'
            ,CASE WHEN ISNULL(SIN,'3') = '3' OR SIN IS NULL THEN COUNT(*) END AS 'Status_2'
			FROM(SELECT TB2.dScan,TB2.sEmp,sName + ' ' + sLastname AS 'sName'
            ,MAX(_TimeIn) AS dIn,MAX(_TimeOut) AS 'dOut'
            ,ISNULL(MAX(StatusIN),'3') AS SIN,ISNULL(MAX(StatusOUT),'3') AS SOUT
            ,MyRowNumber
            FROM  (SELECT t.sEmp,t.sName,t.sLastname,t.dScan,ROW_NUMBER() OVER (ORDER BY t.dScan DESC) as MyRowNumber
            FROM sEmpList ('" + _dstart.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"','" + _dend.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"') AS t) AS TB2
            LEFT JOIN
            (
            (SELECT LogEmpDate,sEmp,LogEmpTime AS _TimeIn,LogEmpScanStatus AS StatusIN,'' AS '_TimeOut','' AS 'StatusOUT'
            FROM TLogEmpTimeScan   AS TBIN
            WHERE LogEmpType = '0')
            UNION ALL
            (SELECT LogEmpDate,sEmp,'','',LogEmpTime AS _TimeOut,LogEmpScanStatus AS StatusOUT
            FROM TLogEmpTimeScan  AS TBOUT
            WHERE LogEmpType = '1') ) AS TB1
            ON TB1.sEmp = TB2.sEmp AND TB2.dScan = LogEmpDate
            GROUP BY TB2.dScan,LogEmpDate,TB2.sEmp,sName,sLastname,MyRowNumber
            ) TB01
		    WHERE 1=1 " + status + _comm + @"
            GROUP BY SIN";
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
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
        private List<Dictionary<string, object>> GetReportmobile01(string id, string dstart, string dend, string status, string page)
        {
            string _comm = "";
            DateTime _dstart = DateTime.Today.AddDays(-30);
            DateTime _dend = DateTime.Today;

            int _index = 1;
            if (!string.IsNullOrEmpty(page)) _index = int.Parse(page) == 0 ? 1 : int.Parse(page);
            //_comm += " and MyRowNumber between " + ((_index * 20) - 19) + " and " + (_index * 20);
            if (!string.IsNullOrEmpty(id))
            {
                _comm += " AND sEmp = " + id;
            }
            if (!string.IsNullOrEmpty(dstart))
            {
                try
                {
                    _dstart = DateTime.ParseExact(dstart, "dd/MM/yyyy", new CultureInfo("en-us"));
                }
                catch { }
            }
            if (!string.IsNullOrEmpty(dend))
            {
                try
                {
                    _dend = DateTime.ParseExact(dend, "dd/MM/yyyy", new CultureInfo("en-us"));
                }
                catch { }
            }

            if (status != "")
            {
                if (status != "3")
                {
                    status = "AND SIN = '" + status + "'";
                }
                else
                {
                    status = "AND SIN = '3' OR SIN IS NULL";
                }

            }

            string SQL = @"SELECT * FROM(SELECT TB2.dScan,TB2.sEmp,sName + ' ' + sLastname AS 'sName'
            ,MAX(_TimeIn) AS dIn,MAX(_TimeOut) AS 'dOut'
            ,ISNULL(MAX(StatusIN),'3') AS SIN,ISNULL(MAX(StatusOUT),'3') AS SOUT
            ,MyRowNumber
            FROM  (SELECT t.sEmp,t.sName,t.sLastname,t.dScan,ROW_NUMBER() OVER (ORDER BY t.dScan DESC) as MyRowNumber
            FROM sEmpList ('" + _dstart.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"','" + _dend.ToString("yyyyMMdd", new CultureInfo("en-us")) + @"') AS t) AS TB2
            LEFT JOIN
            (
            (SELECT LogEmpDate,sEmp,LogEmpTime AS _TimeIn,LogEmpScanStatus AS StatusIN,'' AS '_TimeOut','' AS 'StatusOUT'
            FROM TLogEmpTimeScan   AS TBIN
            WHERE LogEmpType = '0')
            UNION ALL
            (SELECT LogEmpDate,sEmp,'','',LogEmpTime AS _TimeOut,LogEmpScanStatus AS StatusOUT
            FROM TLogEmpTimeScan  AS TBOUT
            WHERE LogEmpType = '1') ) AS TB1
            ON TB1.sEmp = TB2.sEmp AND TB2.dScan = LogEmpDate
            GROUP BY TB2.dScan,LogEmpDate,TB2.sEmp,sName,sLastname,MyRowNumber
           ) TB01
		   WHERE 1=1 " + status + _comm + @"
		    ORDER BY dScan DESC,sEmp,sName";
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dtUser == null) return rows;
            foreach (DataRow dr in dtUser.Select().Skip(((_index * 20) - 20)).Take(20))
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

        private List<Dictionary<string, object>> GetReportChart01()
        {
            DateTime _dstart = DateTime.Parse(DateTime.Today.ToString("MM/01/yyyy"));
            DateTime _dend = _dstart.AddMonths(1).AddDays(-1);

            string SQL = @"SELECT CONVERT(VARCHAR(10),thedate,120) AS thedate,ISNULL(nSumSell,0) AS nSumSell,ISNULL(nSumAdd,0) AS nSumAdd FROM 
            (SELECT * FROM ExplodeDates ('" + _dstart.ToString("yyyyMMdd") + "','" + _dend.ToString("yyyyMMdd") + @"')) AS TB0 LEFT JOIN
            (
            SELECT DATEADD(DAY,0, DATEDIFF(DAY,0, dSell)) AS 'dDate',SUM(nTotal) AS 'nSumSell' FROM TSell 
            WHERE dayCancal IS NULL AND SchoolID = " + userData.CompanyID + @"
            GROUP BY DATEADD(DAY,0, DATEDIFF(DAY,0, dSell))
            ) AS TB1 ON TB0.thedate = TB1.dDate
            LEFT JOIN
            (
            SELECT DATEADD(DAY,0, DATEDIFF(DAY,0, dMoney)) AS 'dDate',SUM(nMoney) AS 'nSumAdd' FROM TMoney 
            WHERE dayCancal IS NULL AND SchoolID = " + userData.CompanyID + @"
            GROUP BY DATEADD(DAY,0, DATEDIFF(DAY,0, dMoney))) AS TB2 ON TB0.thedate = TB2.dDate
            ORDER BY thedate";

            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            DataTable dtUser = fcommon.Get_Data(_conn, SQL);
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