using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for dataGeneric
    /// </summary>
    public class dataGeneric : IHttpHandler, IRequiresSessionState
    {
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                throw new Exception();
            }

            int schoolID = userData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                SqlConnection _conn = new SqlConnection();
                if (!string.IsNullOrEmpty(userData.Entities))
                {
                    //_db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));
                    _conn = fcommon.ConfigSqlConnection(schoolID);
                }
                string _str = fcommon.ReplaceInjection(context.Request["ID"]);
                string _mode = fcommon.ReplaceInjection(context.Request["Mode"]);
                string _tmp = "";
                string SQL = "";
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, object> row;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                int nTimeType = 0;
                DataTable dt = new DataTable();

                switch (_mode)
                {
                    #region time
                    case "time":
                        nTimeType = int.Parse(_str == "" ? "0" : _str);
                        dt = fcommon.LinqToDataTable(_db.TTimes.Where(w => w.nTimeType == nTimeType));
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }
                        break;
                    #endregion
                    #region tabtimetype
                    case "tabtimetype":
                        DataTable dttabtimetype = fcommon.LinqToDataTable(_db.TTimetypes.Where(w => w.SchoolID == schoolID && w.cType == "1" && w.cUserType == "1"));
                        if (dttabtimetype.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dttabtimetype.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dttabtimetype.Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                        }
                        break;
                    #endregion
                    #region listlevel
                    case "listlevel":
                        nTimeType = int.Parse(_str == "" ? "0" : _str);
                        DataTable dtListlevel = fcommon.LinqToDataTable(_db.TLevels.Where(w => w.SchoolID == schoolID && w.nTimeType == nTimeType));
                        if (dtListlevel != null)
                        {
                            foreach (DataRow dr in dtListlevel.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dtListlevel.Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                        }
                        break;
                    #endregion
                    #region listsublevel
                    case "listsublevel":
                        nTimeType = int.Parse(_str == "" ? "0" : _str);
                        string nhol = fcommon.ReplaceInjection(context.Request["nhol"]);

                        var q1 = _db.THolidaySomes.Where(w => w.SchoolID == schoolID && w.nHoliday == nhol).ToList();
                        var q2 = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTLevel == nTimeType).ToList();
                        var q3 = _db.THolidays.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList();
                        var q4 = (from A in q2
                                  join B in q1 on A.nTSubLevel equals B.nTSubLevel into AB
                                  from subpet in AB.DefaultIfEmpty()
                                  select new
                                  {
                                      nHoliday = subpet == null ? "0" : subpet.nHoliday,
                                      nTSubLevel = A.nTSubLevel,
                                      SubLevel = A.SubLevel,
                                      A.nTLevel
                                  }).ToList();

                        var _list = from TB1 in q4
                                    join C in q3 on TB1.nHoliday equals C.nHoliday into AB
                                    from subpet in AB.DefaultIfEmpty()
                                    select new
                                    {
                                        nHoliday = TB1.nHoliday,
                                        nTSubLevel = TB1.nTSubLevel,
                                        SubLevel = TB1.SubLevel,
                                        nTLevel = TB1.nTLevel,
                                        nAll = TB1.nHoliday
                                    };
                        DataTable dtSubListlevel = fcommon.LinqToDataTable(_list);
                        if (dtSubListlevel != null)
                        {
                            foreach (DataRow dr in dtSubListlevel.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dtSubListlevel.Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                        }
                        break;
                    #endregion
                    #region listsublevel2
                    case "listsublevel2":
                        int nSublv = int.Parse(fcommon.ReplaceInjection((string.IsNullOrEmpty(context.Request["nhol"]) || (context.Request["nhol"] == "undefined")) ? "0" : context.Request["nhol"]));
                        if (_db != null)
                        {
                            var listsublevel2 = QueryDataBases.SubLevel2_Query.GetData(_db, nSublv, userData);

                            foreach (var lv2_data in listsublevel2)
                            {
                                row = new Dictionary<string, object>();
                                row.Add("nTermSubLevel2", lv2_data.classRoom_id);
                                row.Add("nTSubLevel2", lv2_data.classRoom_name);
                                rows.Add(row);
                            }
                        }
                        else
                        {
                            row = new Dictionary<string, object>();
                        }
                        break;
                    #endregion
                    #region listPlane
                    case "listPlane":
                        nTimeType = int.Parse(_str == "" ? "0" : _str);
                        string splane = fcommon.ReplaceInjection(context.Request["splane"]);
                        var listplane = from A in _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTLevel == nTimeType)
                                        join B in _db.TPlane_TSubLevel.Where(w => w.SchoolID == schoolID && w.sPlaneID.ToString() == splane) on A.nTSubLevel equals B.nTSubLevel into AB
                                        from subpet in AB.DefaultIfEmpty()
                                        select new
                                        {
                                            nHoliday = subpet.sPlaneID,
                                            nTSubLevel = A.nTSubLevel,
                                            SubLevel = A.SubLevel,
                                            A.nTLevel
                                        };
                        DataTable dtlistPlane = fcommon.LinqToDataTable(listplane);
                        if (dtlistPlane != null)
                        {
                            foreach (DataRow dr in dtlistPlane.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dtlistPlane.Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                        }
                        break;
                    #endregion
                    #region getterm
                    case "listterm":
                        rows = GetListTerm(fcommon.ReplaceInjection(context.Request["id"]));
                        break;
                    #endregion
                    #region listplane
                    case "listplane":
                        rows = GetListPlane(fcommon.ReplaceInjection(context.Request["nhol"]));
                        break;
                    #endregion
                    #region listvaluesperiod
                    case "listvaluesperiod":
                        rows = GetListValuesPeriod(fcommon.ReplaceInjection(context.Request["id"]));
                        break;
                    #endregion
                    #region listschedule
                    case "listschedule":
                        rows = GetListSchedule(schoolID, fcommon.ReplaceInjection(context.Request["idterm"]), fcommon.ReplaceInjection(context.Request["id"]));
                        break;
                    #endregion
                    #region listplansroom
                    case "listplansroom":
                        rows = GetListPlansRoom(schoolID, fcommon.ReplaceInjection(context.Request["id"]), fcommon.ReplaceInjection(context.Request["idterm"]));
                        break;
                    #endregion
                    #region listperiods
                    case "listperiods":
                        rows = GetListPeriods(fcommon.ReplaceInjection(context.Request["id"]), fcommon.ReplaceInjection(context.Request["nhol"]));
                        break;
                    #endregion
                    #region getperiod
                    case "getperiod":
                        rows = GetPeriods(fcommon.ReplaceInjection(context.Request["id"]));
                        break;
                    #endregion
                    #region insertschedule
                    case "insertschedule":
                        //using (WebClient wc = new WebClient())
                        //{
                        //    var json = wc.DownloadString("url");
                        //}
                        string json = context.Request["json"].ToString();
                        InsertSchedule(json);

                        context.Response.Expires = -1;
                        context.Response.ContentType = "text/plain";
                        context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.Write("Success");
                        context.Response.End();
                        return;
                    #endregion
                    #region insertTerm
                    case "insertterm":

                        InsertTerm(context.Request["json"].ToString(), context.Request["id"].ToString(), context.Request["year"].ToString());

                        context.Response.Expires = -1;
                        context.Response.ContentType = "text/plain";
                        context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.Write("Success");
                        context.Response.End();
                        return;
                    #endregion
                    #region insertmoney
                    case "insertmoney":

                        _tmp = InsertMoney(context.Request["id"].ToString(), context.Request["money"].ToString(), context.Session["sEmpID"] + "");

                        context.Response.Expires = -1;
                        context.Response.ContentType = "text/plain";
                        context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.Write(_tmp);
                        context.Response.End();
                        return;
                    #endregion
                    #region insertteacher
                    case "insertteacher":

                        _tmp = InsertTeacher(context.Request["teacherid"].ToString(), context.Request["id"].ToString(), context.Request["idterm"] + "");

                        context.Response.Expires = -1;
                        context.Response.ContentType = "text/plain";
                        context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.Write(_tmp);
                        context.Response.End();
                        return;
                    #endregion
                    #region EmpScan
                    case "EmpScan":
                        SQL = @"select * from 
                                    (select sName,sLastname,CONVERT(varchar, sID)+'S'+CONVERT(varchar,ISNULL(nTimeType,'')) AS sID,sFinger,sFinger2,sIdentification,nMoney,sID AS ID
                                    from TUser left join TTermSubLevel2 on TUsers.nTermSubLevel2 = TTermSubLevel2.nTermSubLevel2
                                    left join TSubLevel on TSubLevels.nTSubLevel = TTermSubLevel2.nTSubLevel
                                    where cDel IS NULL
                                    union
                                    select sName,sLastname,CONVERT(varchar,sEmp)+'E'+CONVERT(varchar,ISNULL(nTimeType,'')) as sID,sFinger,sFinger2,sIdentification,nMoney,sEmp AS ID
                                    from TEmployees where cDel IS NULL) as tb1
                                    where sIdentification like '%" + _str + @"' ";
                        DataTable dtEmp = fcommon.Get_Data(_conn, SQL);
                        if (dtEmp != null)
                        {
                            foreach (DataRow dr in dtEmp.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dtEmp.Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                        }
                        break;
                    #endregion
                    #region JobScan
                    case "JobScan":
                        SQL = @"select * from 
                                    (select sName,sLastname,CONVERT(varchar, sID)+'S'+CONVERT(varchar,ISNULL(TTermSubLevel2.nTimeType,'')) AS sID,sFinger,sFinger2,sIdentification,ISNULL(nMoney,0) AS nMoney,sID AS ID
                                    from TUser left join TTermSubLevel2 on TUsers.nTermSubLevel2 = TTermSubLevel2.nTermSubLevel2
                                    left join TSubLevel on TSubLevels.nTSubLevel = TTermSubLevel2.nTSubLevel
                                    where cDel IS NULL
                                    union
                                    select sName,sLastname,CONVERT(varchar,sEmp)+'E'+CONVERT(varchar,ISNULL(nTimeType,'')) as sID,sFinger,sFinger2,sIdentification,ISNULL(nMoney,0) AS nMoney,sEmp AS ID
                                    from TEmployees where cDel IS NULL) as tb1
                                    where sIdentification like '%" + _str + @"' ";

                        DataTable dtUser = fcommon.Get_Data(_conn, SQL);
                        foreach (DataRow dr in dtUser.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dtUser.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }
                        break;
                    #endregion
                    #region JobScan
                    case "LoginScan":
                        SQL = @"select sName,sLastname,sID,sFinger,sFinger2,sIdentification,sID AS ID
                            from TUser where cDel IS NULL
                            and sIdentification like '%" + _str + @"' ";

                        dt = fcommon.Get_Data(fcommon.connMaster, SQL);
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }
                        break;
                    #endregion
                    #region ListMacAddress
                    case "ListMacAddress":
                        SQL = @"select top 1 sMac,sEntities
                            from TComputer inner join TCompany on TComputer.nCompany = TCompanies.nCompany
                            and sMac IN (" + context.Request["ID"] + @") ";

                        dt = fcommon.Get_Data(fcommon.connMaster, SQL);
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }
                        break;
                    #endregion
                    #region get list Product
                    case "getlistproduct":
                        SQL = @"select sProduct,nProductID,sBarCode
                            from TProduct where cDel IS NULL
                            and sProduct like '%" + _str + @"%' OR sBarCode like '%" + _str + @"%   ' ";
                        if (context.Request["type"] + "" != "") SQL += " AND nType = '" + context.Request["type"] + "'";
                        SQL += " order by nType,sProduct ";
                        dt = fcommon.Get_Data(_conn, SQL);
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }
                        break;
                    #endregion
                    #region getuser
                    case "getuser":
                        SQL = @"SELECT sFinger, sFinger2 FROM TUser WHERE cDel IS NULL
                            AND ( sName = '" + _str + "' OR sLastname = '" + context.Request["param3var"] + "' OR sIdentification = '" + context.Request["param4var"] + "'";

                        _conn = fcommon.ConfigSqlConnection(fcommon.GetCookies());
                        dt = fcommon.Get_Data(_conn, SQL);
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }
                        break;
                    #endregion
                    #region getemp
                    case "getemp":
                        SQL = @"SELECT sFinger, sFinger2 FROM TEmployees WHERE cDel IS NULL
                            AND ( sName = '" + _str + "' OR sLastname = '" + context.Request["param3var"] + "' OR sIdentification = '" + context.Request["param4var"] + "'";

                        _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"].ToString());
                        dt = fcommon.Get_Data(_conn, SQL);
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }
                        break;
                        #endregion
                }
                context.Response.Expires = -1;
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(serializer.Serialize(rows).ToString());
                context.Response.End();
            }
        }

        private string InsertMoney(string id, string money, string sEmpID)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                int sID = 0;
                decimal? nMoney = 0;
                string sName = "";
                if (!string.IsNullOrEmpty(money))
                {
                    try
                    {
                        if (id.IndexOf("S") != -1)
                        {
                            sID = int.Parse(id.Split('S')[0]);
                            foreach (var _data in _db.TUser.Where(w => w.sID == sID))
                            {
                                _data.nMoney = (_data.nMoney.HasValue ? _data.nMoney.Value : 0) + decimal.Parse(money);
                                sName = _data.sName + " " + _data.sLastname;
                                nMoney = _data.nMoney;
                            }
                        }
                        else
                        {
                            sID = int.Parse(id.Split('E')[0]);
                            foreach (var _data in _db.TEmployees.Where(w => w.sEmp == sID))
                            {
                                _data.nMoney = (_data.nMoney.HasValue ? _data.nMoney.Value : 0) + decimal.Parse(money);
                                sName = _data.sName + " " + _data.sLastname;
                                nMoney = _data.nMoney;
                            }
                        }

                        TMoney _Money = new TMoney();
                        _Money.dMoney = DateTime.Now;
                        _Money.nMoney = decimal.Parse(money);
                        _Money.sID = sID;
                        _Money.cType = id.IndexOf("S") != -1 ? "2" : "1";
                        _Money.SchoolID = userData.CompanyID;
                        _db.TMoneys.Add(_Money);

                        _db.SaveChanges();
                        database.InsertLog(sEmpID, "เติมเงินเข้าระบบให้คุณ " + sName + " จำนวน " + money + " บาท",
                            HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, -1, 2, 0);
                        fcommon.ExecuteNonQuery(fcommon.connMaster, @"UPDATE TConnect SET cStatus = '3',sDisplay = '" + money + "," + nMoney + @"'
                        WHERE nConnectID ="
    + HttpContext.Current.Session["nConnectID"].ToString() + " ");

                        int nConnectID = 1;
                        DataTable _dt = fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TConnect ORDER BY nConnectID DESC");
                        if (_dt.Rows.Count > 0) nConnectID = int.Parse(_dt.Rows[0]["nConnectID"] + "") + 1;
                        string strInsert = @"INSERT INTO TConnect (nConnectID,sMacPC,sMacMoblie,cStatus,sValue,cTypeConnect) VALUES({0},'{1}',NULL,'1',NULL,'UM')";

                        //txtnMoney.Text = "";
                        return nMoney.Value.ToString();
                    }
                    catch
                    {
                        return "-1";
                    }
                }
                else
                {
                    return "0";
                    //SetBodyEventOnLoad("j_infomainLed('แจ้งผลการดำเนินการ','กรุณากรอกข้อมูลจำนวนเงิน','0','0'); ");
                }
            }
        }
        private string InsertTeacher(string teacherid, string id, string idterm)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                int nTermTable = 1;
                try
                {
                    int nTermSubLevel2 = int.Parse(id);
                    if (_db.TTermTimeTables.Where(w => w.nTerm == idterm && w.nTermSubLevel2 == nTermSubLevel2).Count() > 0)
                    {
                        foreach (var _data in _db.TTermTimeTables.Where(w => w.nTerm == idterm && w.nTermSubLevel2 == nTermSubLevel2))
                        {
                            _data.nTerm = idterm;
                            _data.nTeacher = int.Parse(teacherid);
                            nTermTable = _data.nTermTable;
                            //_db.TTermTimeTables.ApplyCurrentValues(_data);
                        }
                        database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                             "ทำการแก้ตารางเรียน รหัส : " + nTermTable,
                             HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 1, 3, 0);
                        _db.SaveChanges();
                    }
                    else
                    {
                        if (_db.TTermTimeTables.Count() > 0)
                        {
                            //nTermTable = _db.TTermTimeTables.Take(1).OrderByDescending(o => o.nTermTable).SingleOrDefault().nTermTable + 1;
                            TTermTimeTable _TimeTable = new TTermTimeTable();
                            //_TimeTable.nTermTable = nTermTable;
                            _TimeTable.nTermSubLevel2 = int.Parse(id);
                            _TimeTable.nTerm = idterm;
                            _TimeTable.nTeacher = int.Parse(teacherid);
                            _TimeTable.SchoolID = userData.CompanyID;

                            _db.TTermTimeTables.Add(_TimeTable);
                            _db.SaveChanges();
                            database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                          "ทำการสร้างตารางเรียน รหัส : " + nTermTable,
                          HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 1, 2, 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
                return nTermTable + "";
            }
        }

        private void InsertSchedule(string json)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                json = json.Replace("]", "").Replace("[", "").Replace("{", "");
                foreach (string _data in json.Replace("},", "}").Split('}'))
                {
                    string[] _values = _data.Replace(@"""", "").Split(',');
                    if (string.IsNullOrEmpty(Array.Find(_values, element => element.StartsWith("nday", StringComparison.Ordinal)).Split(':')[1].Replace(@"""", "")))
                    {
                        //int nschedule = int.Parse(Array.Find(_values, element => element.StartsWith("nschedule", StringComparison.Ordinal)).Split(':')[1]);
                        //foreach (var datadel in _db.TSchedules.Where(w => w.sScheduleID == nschedule))
                        //{
                        //    //_db.TSchedules.Remove(datadel);
                        //    datadel.cDel = true;
                        //}
                    }
                    else
                    {
                        if (_values.Length > 1)
                        {
                            int nPlaneDay = int.Parse(Array.Find(_values, element => element.StartsWith("nday", StringComparison.Ordinal)).Split(':')[1].Replace(@"""", ""));
                            string sPlaneID = Array.Find(_values, element => element.StartsWith("nplane", StringComparison.Ordinal)).Split(':')[1].Replace(@"""", "");
                            int sPeriodID = int.Parse(Array.Find(_values, element => element.StartsWith("nperiod", StringComparison.Ordinal)).Split(':')[1]);
                            string tableid = Array.Find(_values, element => element.StartsWith("tableid", StringComparison.Ordinal)).Split(':')[1];
                            int nschedule = int.Parse(Array.Find(_values, element => element.StartsWith("nschedule", StringComparison.Ordinal)).Split(':')[1]);
                            int teacherid = int.Parse(Array.Find(_values, element => element.StartsWith("teacherid", StringComparison.Ordinal)).Split(':')[1]);
                            string roomsid = Array.Find(_values, element => element.StartsWith("roomsid", StringComparison.Ordinal)).Split(':')[1];

                            if (nschedule > 0)
                            {
                                foreach (var _dataedit in _db.TSchedules.Where(w => w.sScheduleID == nschedule))
                                {
                                    int sPlaneId = 0;
                                    int.TryParse(sPlaneID, out sPlaneId);
                                    _dataedit.sPlaneID = sPlaneId;
                                    _dataedit.sEmp = teacherid;
                                    _dataedit.sClassID = roomsid;
                                }

                                database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                                    "ทำการแก้ตารางสอน - วิชาเรียน รหัส : " + nschedule,
                                    HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 1, 2, 0);
                            }
                            else if (nschedule == 0)
                            {
                                TSchedule _TSchedule = new TSchedule();
                                //int sScheduleID = 1;
                                //if (_db.TSchedules.Count() > 0) sScheduleID = _db.TSchedules.OrderByDescending(o => o.sScheduleID).Take(1).SingleOrDefault().sScheduleID + 1;
                                //_TSchedule.sScheduleID = sScheduleID;

                                int sPlaneIdSchedule = 0;
                                int.TryParse(sPlaneID, out sPlaneIdSchedule);

                                _TSchedule.sClassID = "1";
                                _TSchedule.sScheduleID = sPeriodID;
                                _TSchedule.sPlaneID = sPlaneIdSchedule;
                                _TSchedule.nPlaneDay = nPlaneDay;
                                _TSchedule.sEmp = teacherid;
                                _TSchedule.sClassID = roomsid;
                                _TSchedule.nTermTable = int.Parse(tableid);
                                _TSchedule.SchoolID = userData.CompanyID;
                                if (sPlaneIdSchedule != 0)
                                {
                                    _db.TSchedules.Add(_TSchedule);
                                }
                                database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                                    "ทำการเพิ่มตารางสอน - วิชาเรียน รหัส : " + nschedule,
                                    HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 1, 2, 0);
                            }
                        }
                    }
                    _db.SaveChanges();
                }
            }
        }
        private string InsertTerm(string json, string nYear, string sYear)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                TYear _Year = new TYear();
                var tYear = _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).ToList();
                _Year.numberYear = int.Parse(sYear);
                int nTerm = 1;
                if (_db.TTerms.Count() > 0)
                {
                    nTerm = int.Parse(_db.TTerms.Take(1).OrderByDescending(o => o.nTerm).SingleOrDefault().nTerm.Replace("TS", "")) + 1;
                }
                if (tYear.Where(w => w.numberYear == _Year.numberYear).Count() == 0)
                {
                    //nYear = "1";
                    //if (tYear.Count() > 0)
                    //{
                    //    nYear = (_db.TYears.Take(1).OrderByDescending(o => o.nYear).SingleOrDefault().nYear + 1) + "";
                    //}
                    //_Year.nYear = int.Parse(nYear);
                    _Year.YearStatus = "1";
                    _db.TYears.Add(_Year);
                }
                else
                {
                    _Year = tYear.Where(w => w.numberYear == _Year.numberYear).FirstOrDefault();
                }
                var q_trem = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList();
                List<termdata> _ITerm = JsonConvert.DeserializeObject<List<termdata>>(json).ToList();
                foreach (var _trem in _ITerm)
                {
                    var f_term = q_trem.FirstOrDefault(f => (_trem.dStart <= f.dStart && _trem.dEnd >= f.dStart) && (_trem.dStart <= f.dEnd && _trem.dEnd >= f.dEnd));
                    if (f_term != null) return "Fail";
                    if (_trem.nTerm < 0)
                    {
                        string _str = string.Format("TS{0:0000000}", _trem.nTerm * -1);
                        _db.TTerms.Where(w => w.nTerm == _str).ToList().ForEach(f => _db.TTerms.Remove(f));
                    }
                    else if (_trem.nTerm == 0)
                    {
                        TTerm _TTerm = new TTerm();
                        _TTerm.nYear = int.Parse(nYear);
                        _TTerm.dEnd = _trem.dEnd;
                        _TTerm.sTerm = _trem.sTerm;
                        _TTerm.dStart = _trem.dStart;
                        _TTerm.nTerm = string.Format("TS{0:0000000}", nTerm++);
                        _TTerm.SchoolID = userData.CompanyID;
                        _db.TTerms.Add(_TTerm);
                        database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                            "ทำการเพิ่มปีการศึกษา : " + nYear + " เทอม : " + _trem.sTerm + " รหัสเทอม : " + nTerm,
                            HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 1, 2, 0);
                    }
                    else
                    {
                        string _nTerm = string.Format("TS{0:0000000}", _trem.nTerm);
                        var q = _db.TTerms.Where(w => w.nTerm == _nTerm && w.cDel == null).FirstOrDefault();
                        q.sTerm = _trem.sTerm;
                        q.dEnd = _trem.dEnd;
                        q.dStart = _trem.dStart;
                        database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                            "ทำการแก้ปีการศึกษา : " + nYear + " เทอม : " + _trem.sTerm + " รหัสเทอม : " + nTerm,
                            HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 1, 2, 0);
                    }
                }
                _db.SaveChanges();
                return "Success";
            }
        }
        private List<Dictionary<string, object>> GetListPlansRoom(int schoolID, string sID, string idterm)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(schoolID);
            string SQL = @"select  B.nTermSubLevel2,SubLevel,nTSubLevel2,COUNT(C.nTermTable) AS NumberSchedule,A.nTSubLevel
            from  TSubLevel AS A left join TTermSubLevel2 AS B ON A.nTSubLevel = B.nTSubLevel
            left join TTermTimeTable AS C on B.nTermSubLevel2 = C.nTermSubLevel2 AND C.nTerm = '" + idterm + @"' 
            left join TSchedule AS D ON D.nTermTable = C.nTermTable
            --WHERE A.nTSubLevel = " + sID + @" AND A.SchoolID = " + userData.CompanyID + @"
            GROUP BY SubLevel,nTSubLevel2,B.nTermSubLevel2,A.nTSubLevel";

            DataTable dt = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dt == null) return rows;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col].ToString().Trim());
                }
                rows.Add(row);
            }
            return rows;
        }
        private List<Dictionary<string, object>> GetListPlansRoom(string sID, string idterm)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            string SQL = @"select  B.nTermSubLevel2,SubLevel,nTSubLevel2,COUNT(C.nTermTable) AS NumberSchedule,A.nTSubLevel
            from  TSubLevel AS A left join TTermSubLevel2 AS B ON A.nTSubLevel = B.nTSubLevel
            left join TTermTimeTable AS C on B.nTermSubLevel2 = C.nTermSubLevel2 AND C.nTerm = '" + idterm + @"' 
            left join TSchedule AS D ON D.nTermTable = C.nTermTable
            --WHERE A.nTSubLevel = " + sID + @" AND A.SchoolID = " + userData.CompanyID + @"
            GROUP BY SubLevel,nTSubLevel2,B.nTermSubLevel2,A.nTSubLevel";

            DataTable dt = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dt == null) return rows;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col].ToString().Trim());
                }
                rows.Add(row);
            }
            return rows;
        }
        private List<Dictionary<string, object>> GetListValuesPeriod(string sID)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                Dictionary<string, object> row;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                int sIDTrem = int.Parse(sID);
                int nTSubLevel = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == sIDTrem).SingleOrDefault().nTSubLevel;

                var listsublevel2 = from A in _db.TPeriods.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                    join B in _db.TPeriod_TSubLevel.Where(w => w.nTSubLevel == nTSubLevel).ToList() on A.sPeriodID equals B.sPeriodID
                                    select new
                                    {
                                        value = A.sPeriodID,
                                        label = A.sPeriodName,
                                        A.dEnd,
                                        A.dStart
                                    };
                DataTable dt = fcommon.LinqToDataTable(listsublevel2);
                if (dt == null) return rows;
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            switch (col.ColumnName)
                            {
                                case "dStart":
                                    row.Add("dStart", Convert.ToDateTime(dr[col] + "").ToString("HH:mm", new CultureInfo("en-us")));
                                    break;
                                case "dEnd":
                                    row.Add("dEnd", Convert.ToDateTime(dr[col] + "").ToString("HH:mm", new CultureInfo("en-us")));
                                    break;
                                default:
                                    row.Add(col.ColumnName, dr[col]);
                                    break;
                            }
                        }
                        rows.Add(row);
                    }
                }
                return rows;
            }
        }

        private List<Dictionary<string, object>> GetListSchedule(int schoolID, string nTerm, string nTermSubLevel2)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(schoolID);
            string SQL = @"select b.sPlaneID + ' ' + b.sPlaneName as plane,
            	a.sPlaneID as nplane,d.sEmp as teacherid,
            	d.sName + ' ' + d.sLastname as teacher,
            	e.sClassID as roomsid,e.sClass as rooms,
            	a.nPlaneDay as nday,'' as sday,c.sPeriodName as speriod,
            	a.sPeriodID as nperiod,a.sScheduleID as nschedule,
            	a.nTermTable as tableid 
                from TSchedule as a left join TPlane as b on a.sPlaneID = b.sPlaneID
                left join TPeriod as c on a.sPeriodID = c.sPeriodID
                left join TEmployees as d on a.sEmp = d.sEmp
                left join TClass as e on a.sRoomID = e.sClassID
                where nTermTable = 
                (select top 1 nTermTable from TTermTimeTable where nTerm = '" + nTerm + "' and nTermSubLevel2 = '" + nTermSubLevel2 + "')  AND A.SchoolID = " + userData.CompanyID + @"";
            DataTable dt = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dt == null) return rows;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
        }
        private List<Dictionary<string, object>> GetListSchedule(string nTerm, string nTermSubLevel2)
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(HttpContext.Current.Session["sEntities"] + "");
            string SQL = @"select b.sPlaneID + ' ' + b.sPlaneName as plane,
            	a.sPlaneID as nplane,d.sEmp as teacherid,
            	d.sName + ' ' + d.sLastname as teacher,
            	e.sClassID as roomsid,e.sClass as rooms,
            	a.nPlaneDay as nday,'' as sday,c.sPeriodName as speriod,
            	a.sPeriodID as nperiod,a.sScheduleID as nschedule,
            	a.nTermTable as tableid 
                from TSchedule as a left join TPlane as b on a.sPlaneID = b.sPlaneID
                left join TPeriod as c on a.sPeriodID = c.sPeriodID
                left join TEmployees as d on a.sEmp = d.sEmp
                left join TClass as e on a.sRoomID = e.sClassID
                where nTermTable = 
                (select top 1 nTermTable from TTermTimeTable where nTerm = '" + nTerm + "' and nTermSubLevel2 = '" + nTermSubLevel2 + "')  AND A.SchoolID = " + userData.CompanyID + @"";
            DataTable dt = fcommon.Get_Data(_conn, SQL);
            Dictionary<string, object> row;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dt == null) return rows;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
        }
        private List<Dictionary<string, object>> GetListTerm(string sID)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                int nYear = int.Parse(sID);
                DataTable dt = fcommon.LinqToDataTable(_db.TTerms.Where(w => w.nYear == nYear && w.cDel == null).Select(s => new
                {
                    dStart = s.dStart,
                    dEnd = s.dEnd,
                    sTerm = s.sTerm,
                    nTerm = s.nTerm,
                    TermStatus = s.TermStatus,
                    numberTerm = s.numberTerm,
                    nYear = s.nYear
                }).ToList());
                Dictionary<string, object> row;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                if (dt == null) return rows;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        switch (col.ColumnName)
                        {
                            case "dStart":
                                row.Add("dStart", Convert.ToDateTime(dr[col] + "").ToString("MM/dd/yyyy", new CultureInfo("en-us")));
                                break;
                            case "dEnd":
                                row.Add("dEnd", Convert.ToDateTime(dr[col] + "").ToString("MM/dd/yyyy", new CultureInfo("en-us")));
                                break;
                            case "sTerm":
                                row.Add("sTerm", dr[col]);
                                break;
                            case "nTerm":
                                //row.Add("nTerm", int.Parse(dr[col].ToString().Replace("TS", "")));
                                row.Add("nTerm", dr[col]);
                                break;
                            default:
                                row.Add(col.ColumnName, dr[col]);
                                break;
                        }
                    }
                    rows.Add(row);
                }
                return rows;
            }
        }

        private List<Dictionary<string, object>> GetPeriods(string Comm)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                Dictionary<string, object> row;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                var List = from A in _db.TPeriods.Where(w => w.SchoolID == userData.CompanyID).ToList()
                           where A.sPeriodID == Comm.Trim()
                           select new
                           {
                               sTime = string.Empty,
                               A.dStart,
                               A.dEnd,
                               sStartTime = "",
                               A.dTimeStart_OUT,
                               A.dTimeStart_IN,
                               sEndTime = "",
                               A.dTimeEnd_IN,
                               A.dTimeEnd_OUT,
                           };
                DataTable dt = fcommon.LinqToDataTable(List);
                foreach (DataRow _row in dt.Rows)
                {
                    string _dStart = DateTime.Parse(_row["dTimeStart_IN"] + "").ToString("HH:mm") + " - " + DateTime.Parse(_row["dTimeStart_OUT"] + "").ToString("HH:mm");
                    string _dEnd = DateTime.Parse(_row["dTimeEnd_IN"] + "").ToString("HH:mm") + " - " + DateTime.Parse(_row["dTimeEnd_OUT"] + "").ToString("HH:mm");
                    string _sTime = DateTime.Parse(_row["dStart"] + "").ToString("HH:mm") + " - " + DateTime.Parse(_row["dEnd"] + "").ToString("HH:mm");
                    _row["sTime"] = _sTime;
                    _row["sStartTime"] = _dStart;
                    _row["sEndTime"] = _dEnd;
                }
                if (dt == null) return rows;
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                }
                return rows;
            }
        }

        private List<Dictionary<string, object>> GetListPeriods(string Comm, string _sID)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                Dictionary<string, object> row;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                int nlv = int.Parse(Comm);
                var listsublevel2 = from A in _db.TSubLevels.Where(w => w.nTLevel == nlv && w.SchoolID == userData.CompanyID)
                                    join B in _db.TPeriod_TSubLevel.Where(w => w.sPeriodID == _sID) on A.nTSubLevel equals B.nTSubLevel into AB
                                    from subpet in AB.DefaultIfEmpty()
                                    select new
                                    {
                                        nTSubLevel = A.nTSubLevel,
                                        SubLevel = A.SubLevel,
                                        subpet.sPeriodID,
                                        A.nTLevel
                                    };
                DataTable dt = fcommon.LinqToDataTable(listsublevel2);
                if (dt == null) return rows;
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                }
                return rows;
            }
        }

        private List<Dictionary<string, object>> GetListPlane(string Comm)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                DataTable dtUser = fcommon.LinqToDataTable(_db.TPlanes.Where(w => w.SchoolID == userData.CompanyID && (w.sPlaneID + " " + w.sPlaneName).Contains(Comm)));
                Dictionary<string, object> row;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                if (dtUser == null) return rows;
                foreach (DataRow dr in dtUser.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dtUser.Columns)
                    {
                        switch (col.ColumnName)
                        {
                            case "sPlaneName":
                                row.Add("name", dr[col]);
                                break;
                            case "sPlaneID":
                                row.Add("value", dr[col]);
                                break;
                            default:
                                row.Add(col.ColumnName, dr[col]);
                                break;
                        }
                    }
                    rows.Add(row);
                }
                return rows;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    class termdata
    {
        public DateTime dStart { get; set; }
        public DateTime dEnd { get; set; }
        public string sTerm { get; set; }
        public int nTerm { get; set; }
    }
}