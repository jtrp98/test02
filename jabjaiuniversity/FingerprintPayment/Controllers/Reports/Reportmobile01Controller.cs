using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using JabjaiMainClass;

namespace FingerprintPayment.Controllers.Reports
{
    public class Reportmobile01Controller : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string id, string dstart, string dend, string status, string page)
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
            ,MAX(_TimeIn) AS dTimeIn,MAX(_TimeOut) AS 'dTimeOut'
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
            List<Reportmobile01> _Reportmobile01 = new List<Reportmobile01>();

            foreach (DataRow dr in dtUser.Select().Skip(((_index * 20) - 20)).Take(20))
            {
                _Reportmobile01.Add(new Reportmobile01
                {
                    sEmp = dr["sEmp"] + "",
                    sName = dr["sName"] + "",
                    dScan = DateTime.Parse(dr["dScan"] + ""),
                    MyRowNumber = dr["MyRowNumber"] + "",
                    dTimeIn = dr["dTimeIn"] + "",
                    dTimeOut = dr["dTimeOut"] + "",
                    SIN = dr["SIN"] + "",
                    SOUT = dr["SOUT"] + "",
                });
            }
            return Ok(_Reportmobile01.OrderByDescending(o => o.dScan));
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class Reportmobile01
    {
        public string sEmp { get; set; }
        public string sName { get; set; }
        public DateTime dScan { get; set; }
        public string MyRowNumber { get; set; }
        public string dTimeIn { get; set; }
        public string dTimeOut { get; set; }
        public string SIN { get; set; }
        public string SOUT { get; set; }

    }
}