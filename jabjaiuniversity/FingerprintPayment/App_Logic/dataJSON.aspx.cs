using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.App_Logic
{
    public partial class dataJSON : System.Web.UI.Page
    {
        int yearData;
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                switch (Request.QueryString["mode"])
                {
                    #region stdlv
                    case "stdlv":
                        System.Web.Script.Serialization.JavaScriptSerializer serializer2 = new System.Web.Script.Serialization.JavaScriptSerializer();
                        DataTable dt2 = fcommon.LinqToDataTable(_db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList());
                        List<Dictionary<string, object>> rows2 = new List<Dictionary<string, object>>();
                        Dictionary<string, object> row2;

                        string nAll = Request.QueryString["all"];
                        if (string.IsNullOrEmpty(nAll))
                        {
                            var query = from Levels in _db.TLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        join SubLevels in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        on Levels.LevelID equals SubLevels.nTLevel
                                        select new
                                        {
                                            Level = Levels.LevelName,
                                            nTSubLevel = SubLevels.nTSubLevel,
                                            SubLevel = SubLevels.SubLevel,
                                            nAll = "1"
                                        };
                            dt2 = fcommon.LinqToDataTable(query);
                        }
                        else
                        {
                            //List<THoliday> _hol2 = _db.THolidays.Where(w => w.nHoliday == nHol && w.sHolidayAll == "1").ToList();

                            //var queryfirst = from Holiday in _db.THoliday
                            //                 from HolidaySome in _db.THolidaySome
                            //                .Where(w => w.nHoliday == Holiday.nHoliday && Holiday.nHoliday == nHol).DefaultIfEmpty()
                            //                 select new
                            //                 {
                            //                     nHoliday = Holiday.nHoliday,
                            //                     nAll = Holiday.sHolidayAll,
                            //                     SubLevel = HolidaySome.nTSubLevel,
                            //                 };

                            //var query = from SubLevel in _db.TSubLevel
                            //            join result in queryfirst on SubLevel.nTSubLevel equals result.SubLevel into temp
                            //            let data = temp.DefaultIfEmpty()
                            //            from allData in data

                            //            select new
                            //              {
                            //                  nHoliday = allData.nHoliday,
                            //                  nTSubLevel = SubLevel.nTSubLevel,
                            //                  SubLevel = SubLevel.SubLevel,
                            //                  nAll = allData.nAll
                            //              } into selectf
                            //            group selectf by new { selectf.nTSubLevel, selectf.SubLevel, selectf.nHoliday, selectf.nAll } into g
                            //            select new
                            //             {
                            //                 nHoliday = g.Key.nHoliday,
                            //                 nTSubLevel = g.Key.nTSubLevel,
                            //                 SubLevel = g.Key.SubLevel,
                            //                 nAll = g.Key.nAll
                            //             };
                            //if (query.ToList().Count > 0)
                            //{
                            //    dt2 = fcommon.LinqToDataTable(query);
                            //}
                        }
                        //
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            row2 = new Dictionary<string, object>();
                            foreach (DataColumn col in dt2.Columns)
                            {
                                row2.Add(col.ColumnName, dr2[col]);
                            }
                            rows2.Add(row2);
                        }
                        // return ;
                        Response.Write(serializer2.Serialize(rows2).ToString());
                        Response.End();
                        break;
                    #endregion

                    #region getReport

                    #region stdScanReport
                    case "reportuser":
                        string userType = Request.QueryString["type"];
                        yearData = Int32.Parse(Request.QueryString["years"]);

                        DataTable dtDataReport = fcommon.LinqToDataTable(_db.TLogUserTimeScans.ToList()
                                                .Where(w => w.nYear == yearData)
                                                .Join(_db.TUser.ToList(), logs => logs.sID, user => user.sID,
                                                (logs, user) => new { Logs = logs, User = user }
                                                )
                                                .Where(w => w.User.cType == userType)
                                                .GroupBy(g => new
                                                {
                                                    g.Logs.LogType,
                                                    g.Logs.LogScanStatus,
                                                    g.Logs.LogDate.Value.Month,
                                                    g.Logs.LogDate.Value.Year
                                                })
                                                 .Select(g => new
                                                 {
                                                     g.Key.LogType,
                                                     g.Key.LogScanStatus,
                                                     currentMonth = g.Key.Month,
                                                     currentYear = g.Key.Year,
                                                     countData = g.Count()
                                                 })
                                                .OrderBy(o => new
                                                {
                                                    o.LogType,
                                                    o.LogScanStatus,
                                                    o.currentMonth
                                                })
                                                            );
                        string allJSON = "";
                        string StrJSON = "";
                        string StrCount1 = "";
                        string StrCount2 = "";
                        string StrCount3 = "";
                        Dictionary<string, int> logType0 = new Dictionary<string, int>();
                        List<int> Month0 = new List<int>();
                        Dictionary<string, int> logType1 = new Dictionary<string, int>();
                        List<int> Month1 = new List<int>();
                        Dictionary<string, int> logType5 = new Dictionary<string, int>();
                        List<int> Month5 = new List<int>();

                        List<int> MergedMonthIn = new List<int>();
                        string statusTemp = "0";
                        string statusCurrent = "0";
                        int monthTemp = 0;
                        int monthCurrent = 0;
                        int countRows = 0;

                        #region LogTYpe0
                        if (dtDataReport != null)
                        {
                            foreach (DataRow dataRows in dtDataReport.Select("LogType = 0"))
                            {
                                string Status = dataRows.ItemArray[1].ToString().Trim();
                                statusCurrent = Status;
                                monthCurrent = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());

                                if (countRows == 0)
                                {
                                    statusTemp = Status;
                                    monthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                }
                                else
                                {
                                    if (statusTemp != statusCurrent)
                                    {
                                        statusTemp = Status;
                                        monthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                    }
                                    else
                                    {
                                        if (monthCurrent < monthTemp)
                                        {
                                            monthCurrent = monthCurrent + 20;
                                        }
                                    }
                                }

                                switch (Status)
                                {
                                    case "0":
                                        Month0.Add(monthCurrent);
                                        logType0.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;
                                    case "1":
                                        Month1.Add(monthCurrent);
                                        logType1.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;
                                    case "5":
                                        Month5.Add(monthCurrent);
                                        logType5.Add(dataRows.ItemArray[2].ToString().Trim(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;

                                }
                                countRows++;
                                monthTemp = monthCurrent;
                                statusTemp = statusCurrent;
                            }
                        }
                        MergedMonthIn = Month5.Union(Month0.Union(Month1).ToList()).ToList();
                        MergedMonthIn.Sort();
                        StrJSON += "[{\"Month\":[";
                        StrCount1 += ",\"Count\":[";
                        StrCount2 += ",\"Count2\":[";
                        StrCount3 += ",\"Count3\":[";
                        int countRowsMonth = 1;
                        foreach (int month in MergedMonthIn)
                        {
                            if (countRowsMonth != 1)
                            {
                                StrJSON += ",";
                                StrCount1 += ",";
                                StrCount2 += ",";
                                StrCount3 += ",";
                            }
                            if (month < 20)
                            {
                                StrJSON += "\"" + monthConv(month.ToString()) + "\"";
                            }
                            else
                            {
                                string monthDelc = (month - 20) + "";
                                StrJSON += "\"" + monthConv(monthDelc) + "\"";
                            }

                            if (logType0.ContainsKey(month.ToString()))
                            {
                                StrCount1 += logType0[month.ToString()];
                            }
                            else
                            {
                                StrCount1 += 0;
                            }

                            if (logType1.ContainsKey(month.ToString()))
                            {
                                StrCount2 += logType5[month.ToString()];
                            }
                            else
                            {
                                StrCount2 += 0;
                            }

                            if (logType5.ContainsKey(month.ToString()))
                            {
                                StrCount3 += logType5[month.ToString()];
                            }
                            else
                            {
                                StrCount3 += 0;
                            }
                            countRowsMonth++;
                        }
                        #endregion
                        allJSON = StrJSON + "]" + StrCount1 + "]" + StrCount2 + "]" + StrCount3 + "]}]";

                        logType0 = new Dictionary<string, int>();
                        Month0 = new List<int>();
                        logType1 = new Dictionary<string, int>();
                        Month1 = new List<int>();
                        logType5 = new Dictionary<string, int>();
                        Month5 = new List<int>();
                        StrJSON = "";
                        StrCount1 = "";
                        StrCount2 = "";
                        StrCount3 = "";
                        #region LogTYpe1
                        if (dtDataReport != null)
                        {
                            foreach (DataRow dataRows in dtDataReport.Select("LogType = 1"))
                            {
                                string Status = dataRows.ItemArray[1].ToString().Trim();
                                statusCurrent = Status;
                                monthCurrent = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());

                                if (countRows == 0)
                                {
                                    statusTemp = Status;
                                    monthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                }
                                else
                                {
                                    if (statusTemp != statusCurrent)
                                    {
                                        statusTemp = Status;
                                        monthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                    }
                                    else
                                    {
                                        if (monthCurrent < monthTemp)
                                        {
                                            monthCurrent = monthCurrent + 20;
                                        }
                                    }
                                }

                                switch (Status)
                                {
                                    case "0":
                                        Month0.Add(monthCurrent);
                                        logType0.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;
                                    case "1":
                                        Month1.Add(monthCurrent);
                                        logType1.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;
                                    case "5":
                                        Month5.Add(monthCurrent);
                                        logType5.Add(dataRows.ItemArray[2].ToString().Trim(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;

                                }
                                countRows++;
                                monthTemp = monthCurrent;
                                statusTemp = statusCurrent;
                            }
                        }
                        MergedMonthIn = Month5.Union(Month0.Union(Month1).ToList()).ToList();
                        MergedMonthIn.Sort();
                        StrJSON += "split[{\"Month\":[";
                        StrCount1 += ",\"Count\":[";
                        StrCount2 += ",\"Count2\":[";
                        StrCount3 += ",\"Count3\":[";
                        countRowsMonth = 1;
                        foreach (int month in MergedMonthIn)
                        {
                            if (countRowsMonth != 1)
                            {
                                StrJSON += ",";
                                StrCount1 += ",";
                                StrCount2 += ",";
                                StrCount3 += ",";
                            }
                            if (month < 20)
                            {
                                StrJSON += "\"" + monthConv(month.ToString()) + "\"";
                            }
                            else
                            {
                                string monthDelc = (month - 20) + "";
                                StrJSON += "\"" + monthConv(monthDelc) + "\"";
                            }

                            if (logType0.ContainsKey(month.ToString()))
                            {
                                StrCount1 += logType0[month.ToString()];
                            }
                            else
                            {
                                StrCount1 += 0;
                            }

                            if (logType1.ContainsKey(month.ToString()))
                            {
                                StrCount2 += logType5[month.ToString()];
                            }
                            else
                            {
                                StrCount2 += 0;
                            }

                            if (logType5.ContainsKey(month.ToString()))
                            {
                                StrCount3 += logType5[month.ToString()];
                            }
                            else
                            {
                                StrCount3 += 0;
                            }
                            countRowsMonth++;
                        }
                        #endregion

                        allJSON += StrJSON + "]" + StrCount1 + "]" + StrCount2 + "]" + StrCount3 + "]}]";


                        Response.Write(allJSON);
                        Response.End();
                        break;
                    #endregion

                    #region userreportByID
                    case "userreportbyid":
                        string userNameData = " ";
                        yearData = Int32.Parse(Request.QueryString["years"]);
                        if (!String.IsNullOrEmpty(Request.QueryString["userid"]))
                        {
                            string userId = Request.QueryString["userid"];
                            int userdataID;

                            bool tryParse = Int32.TryParse(Request.QueryString["userid"], out userdataID);
                            if (!tryParse)
                            {
                                userdataID = 0;
                            }

                            DataTable dtDataReportUser = fcommon.LinqToDataTable(_db.TUser.Where(w => w.SchoolID == userData.CompanyID).AsQueryable().ToList()
                                                .Where(w => w.sID == userdataID || ((w.sName + " " + w.sLastname).Contains(userId)))
                                                .Join(_db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).AsQueryable().ToList(), user => user.sID, logs => logs.sID,
                                                (user, logs) => new { Logs = logs, User = user }
                                                )
                                                .Where(w => w.Logs.nYear == yearData)
                                                .GroupBy(g => new
                                                {
                                                    g.User.sLastname,
                                                    g.User.sName,
                                                    g.Logs.LogType,
                                                    g.Logs.LogScanStatus,
                                                    g.Logs.LogDate.Value.Month,
                                                    g.Logs.LogDate.Value.Year
                                                })
                                                 .Select(g => new
                                                 {
                                                     g.Key.LogType,
                                                     g.Key.LogScanStatus,
                                                     currentMonth = g.Key.Month,
                                                     currentYear = g.Key.Year,
                                                     countData = g.Count(),
                                                     Name = g.Key.sName + " " + g.Key.sLastname
                                                 })
                                                .OrderBy(o => new
                                                {
                                                    o.LogType,
                                                    o.LogScanStatus,
                                                    o.currentMonth
                                                })
                                                            );
                            string userallJSON = "";
                            string userStrJSON = "";
                            string userStrCount1 = "";
                            string userStrCount2 = "";
                            string userStrCount3 = "";
                            Dictionary<string, int> userlogType0 = new Dictionary<string, int>();
                            List<int> userMonth0 = new List<int>();
                            Dictionary<string, int> userlogType1 = new Dictionary<string, int>();
                            List<int> userMonth1 = new List<int>();
                            Dictionary<string, int> userlogType5 = new Dictionary<string, int>();
                            List<int> userMonth5 = new List<int>();

                            List<int> userMergedMonthIn = new List<int>();
                            string userstatusTemp = "0";
                            string userstatusCurrent = "0";
                            int usermonthTemp = 0;
                            int usermonthCurrent = 0;
                            int usercountRows = 0;
                            #region LogTYpe0
                            if (dtDataReportUser != null)
                            {
                                foreach (DataRow dataRows in dtDataReportUser.Select("LogType = 0"))
                                {
                                    string Status = dataRows.ItemArray[1].ToString().Trim();
                                    userstatusCurrent = Status;
                                    usermonthCurrent = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                    userNameData = dataRows.ItemArray[5].ToString();

                                    if (usercountRows == 0)
                                    {
                                        userstatusTemp = Status;
                                        usermonthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                    }
                                    else
                                    {
                                        if (userstatusTemp != userstatusCurrent)
                                        {
                                            userstatusTemp = Status;
                                            usermonthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                        }
                                        else
                                        {
                                            if (usermonthCurrent < usermonthTemp)
                                            {
                                                usermonthCurrent = usermonthCurrent + 20;
                                            }
                                        }
                                    }

                                    switch (Status)
                                    {
                                        case "0":
                                            userMonth0.Add(usermonthCurrent);
                                            userlogType0.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;
                                        case "1":
                                            userMonth1.Add(usermonthCurrent);
                                            userlogType1.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;
                                        case "5":
                                            userMonth5.Add(usermonthCurrent);
                                            userlogType5.Add(dataRows.ItemArray[2].ToString().Trim(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;

                                    }
                                    usercountRows++;
                                    usermonthTemp = usermonthCurrent;
                                    userstatusTemp = userstatusCurrent;
                                }
                            }
                            userMergedMonthIn = userMonth5.Union(userMonth0.Union(userMonth1).ToList()).ToList();
                            userMergedMonthIn.Sort();
                            userStrJSON += "[{\"Name\":\"" + userNameData + "\",\"Month\":[";
                            userStrCount1 += ",\"Count\":[";
                            userStrCount2 += ",\"Count2\":[";
                            userStrCount3 += ",\"Count3\":[";
                            int usercountRowsMonth = 1;
                            foreach (int month in userMergedMonthIn)
                            {
                                if (usercountRowsMonth != 1)
                                {
                                    userStrJSON += ",";
                                    userStrCount1 += ",";
                                    userStrCount2 += ",";
                                    userStrCount3 += ",";
                                }
                                if (month < 20)
                                {
                                    userStrJSON += "\"" + monthConv(month.ToString()) + "\"";
                                }
                                else
                                {
                                    string monthDelc = (month - 20) + "";
                                    userStrJSON += "\"" + monthConv(monthDelc) + "\"";
                                }

                                if (userlogType0.ContainsKey(month.ToString()))
                                {
                                    userStrCount1 += userlogType0[month.ToString()];
                                }
                                else
                                {
                                    userStrCount1 += 0;
                                }

                                if (userlogType1.ContainsKey(month.ToString()))
                                {
                                    userStrCount2 += userlogType5[month.ToString()];
                                }
                                else
                                {
                                    userStrCount2 += 0;
                                }

                                if (userlogType5.ContainsKey(month.ToString()))
                                {
                                    userStrCount3 += userlogType5[month.ToString()];
                                }
                                else
                                {
                                    userStrCount3 += 0;
                                }
                                usercountRowsMonth++;
                            }
                            #endregion
                            userallJSON = userStrJSON + "]" + userStrCount1 + "]" + userStrCount2 + "]" + userStrCount3 + "]}]";

                            userlogType0 = new Dictionary<string, int>();
                            userMonth0 = new List<int>();
                            userlogType1 = new Dictionary<string, int>();
                            userMonth1 = new List<int>();
                            userlogType5 = new Dictionary<string, int>();
                            userMonth5 = new List<int>();
                            userStrJSON = "";
                            userStrCount1 = "";
                            userStrCount2 = "";
                            userStrCount3 = "";
                            #region LogTYpe1
                            if (dtDataReportUser != null)
                            {
                                foreach (DataRow dataRows in dtDataReportUser.Select("LogType = 1"))
                                {
                                    string Status = dataRows.ItemArray[1].ToString().Trim();
                                    userstatusCurrent = Status;
                                    usermonthCurrent = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());

                                    if (usercountRows == 0)
                                    {
                                        userstatusTemp = Status;
                                        usermonthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                    }
                                    else
                                    {
                                        if (userstatusTemp != userstatusCurrent)
                                        {
                                            userstatusTemp = Status;
                                            usermonthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                        }
                                        else
                                        {
                                            if (usermonthCurrent < usermonthTemp)
                                            {
                                                usermonthCurrent = usermonthCurrent + 20;
                                            }
                                        }
                                    }

                                    switch (Status)
                                    {
                                        case "0":
                                            userMonth0.Add(usermonthCurrent);
                                            userlogType0.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;
                                        case "1":
                                            userMonth1.Add(usermonthCurrent);
                                            userlogType1.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;
                                        case "5":
                                            userMonth5.Add(usermonthCurrent);
                                            userlogType5.Add(dataRows.ItemArray[2].ToString().Trim(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;

                                    }
                                    usercountRows++;
                                    usermonthTemp = usermonthCurrent;
                                    userstatusTemp = userstatusCurrent;
                                }
                            }
                            userMergedMonthIn = userMonth5.Union(userMonth0.Union(userMonth1).ToList()).ToList();
                            userMergedMonthIn.Sort();
                            userStrJSON += "split[{\"Name\":\"" + userNameData + "\",\"Month\":[";
                            userStrCount1 += ",\"Count\":[";
                            userStrCount2 += ",\"Count2\":[";
                            userStrCount3 += ",\"Count3\":[";
                            usercountRowsMonth = 1;
                            foreach (int month in userMergedMonthIn)
                            {
                                if (usercountRowsMonth != 1)
                                {
                                    userStrJSON += ",";
                                    userStrCount1 += ",";
                                    userStrCount2 += ",";
                                    userStrCount3 += ",";
                                }
                                if (month < 20)
                                {
                                    userStrJSON += "\"" + monthConv(month.ToString()) + "\"";
                                }
                                else
                                {
                                    string monthDelc = (month - 20) + "";
                                    userStrJSON += "\"" + monthConv(monthDelc) + "\"";
                                }

                                if (userlogType0.ContainsKey(month.ToString()))
                                {
                                    userStrCount1 += userlogType0[month.ToString()];
                                }
                                else
                                {
                                    userStrCount1 += 0;
                                }

                                if (userlogType1.ContainsKey(month.ToString()))
                                {
                                    userStrCount2 += userlogType5[month.ToString()];
                                }
                                else
                                {
                                    userStrCount2 += 0;
                                }

                                if (userlogType5.ContainsKey(month.ToString()))
                                {
                                    userStrCount3 += userlogType5[month.ToString()];
                                }
                                else
                                {
                                    userStrCount3 += 0;
                                }
                                usercountRowsMonth++;
                            }
                            #endregion
                            userallJSON += userStrJSON + "]" + userStrCount1 + "]" + userStrCount2 + "]" + userStrCount3 + "]}]";


                            Response.Write(userallJSON);
                            Response.End();
                        }
                        break;

                    #endregion

                    #region employeeScanReport
                    case "reportemp":
                        yearData = Int32.Parse(Request.QueryString["years"]);

                        DataTable dtempDataReport = fcommon.LinqToDataTable(_db.TLogEmpTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.nYear == yearData)
                                                .GroupBy(g => new
                                                {
                                                    g.LogEmpType,
                                                    g.LogEmpScanStatus,
                                                    g.LogEmpDate.Value.Month,
                                                    g.LogEmpDate.Value.Year
                                                })
                                                 .Select(g => new
                                                 {
                                                     g.Key.LogEmpType,
                                                     g.Key.LogEmpScanStatus,
                                                     currentMonth = g.Key.Month,
                                                     currentYear = g.Key.Year,
                                                     countData = g.Count()
                                                 })
                                                .OrderBy(o => new
                                                {
                                                    o.LogEmpType,
                                                    o.LogEmpScanStatus,
                                                    o.currentMonth
                                                })
                                                            );
                        string allEmpJSON = "";
                        string StrEmpJSON = "";
                        string StrEmpCount1 = "";
                        string StrEmpCount2 = "";
                        string StrEmpCount3 = "";
                        Dictionary<string, int> logEmpType0 = new Dictionary<string, int>();
                        List<int> EmpMonth0 = new List<int>();
                        Dictionary<string, int> logEmpType1 = new Dictionary<string, int>();
                        List<int> EmpMonth1 = new List<int>();
                        Dictionary<string, int> logEmpType5 = new Dictionary<string, int>();
                        List<int> EmpMonth5 = new List<int>();

                        List<int> EmpMergedMonthIn = new List<int>();
                        string statusEmpTemp = "0";
                        string statusEmpCurrent = "0";
                        int monthEmpTemp = 0;
                        int monthEmpCurrent = 0;
                        int countEmpRows = 0;

                        #region LogTYpe0
                        if (dtempDataReport != null)
                        {
                            foreach (DataRow dataRows in dtempDataReport.Select("LogType = 0"))
                            {
                                string Status = dataRows.ItemArray[1].ToString().Trim();
                                statusEmpCurrent = Status;
                                monthEmpCurrent = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());

                                if (countEmpRows == 0)
                                {
                                    statusEmpTemp = Status;
                                    monthEmpTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                }
                                else
                                {
                                    if (statusEmpTemp != statusEmpCurrent)
                                    {
                                        statusEmpTemp = Status;
                                        monthEmpTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                    }
                                    else
                                    {
                                        if (monthEmpCurrent < monthEmpTemp)
                                        {
                                            monthEmpCurrent = monthEmpCurrent + 20;
                                        }
                                    }
                                }

                                switch (Status)
                                {
                                    case "0":
                                        EmpMonth0.Add(monthEmpCurrent);
                                        logEmpType0.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;
                                    case "1":
                                        EmpMonth1.Add(monthEmpCurrent);
                                        logEmpType1.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;
                                    case "5":
                                        EmpMonth5.Add(monthEmpCurrent);
                                        logEmpType5.Add(dataRows.ItemArray[2].ToString().Trim(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;

                                }
                                countEmpRows++;
                                monthEmpTemp = monthEmpCurrent;
                                statusEmpTemp = statusEmpCurrent;
                            }
                        }
                        EmpMergedMonthIn = EmpMonth5.Union(EmpMonth0.Union(EmpMonth1).ToList()).ToList();
                        EmpMergedMonthIn.Sort();
                        StrEmpJSON += "[{\"Month\":[";
                        StrEmpCount1 += ",\"Count\":[";
                        StrEmpCount2 += ",\"Count2\":[";
                        StrEmpCount3 += ",\"Count3\":[";
                        int countEmpRowsMonth = 1;
                        foreach (int month in EmpMergedMonthIn)
                        {
                            if (countEmpRowsMonth != 1)
                            {
                                StrEmpJSON += ",";
                                StrEmpCount1 += ",";
                                StrEmpCount2 += ",";
                                StrEmpCount3 += ",";
                            }
                            if (month < 20)
                            {
                                StrEmpJSON += "\"" + monthConv(month.ToString()) + "\"";
                            }
                            else
                            {
                                string monthEmpDelc = (month - 20) + "";
                                StrEmpJSON += "\"" + monthConv(monthEmpDelc) + "\"";
                            }

                            if (logEmpType0.ContainsKey(month.ToString()))
                            {
                                StrEmpCount1 += logEmpType0[month.ToString()];
                            }
                            else
                            {
                                StrEmpCount1 += 0;
                            }

                            if (logEmpType1.ContainsKey(month.ToString()))
                            {
                                StrEmpCount2 += logEmpType5[month.ToString()];
                            }
                            else
                            {
                                StrEmpCount2 += 0;
                            }

                            if (logEmpType5.ContainsKey(month.ToString()))
                            {
                                StrEmpCount3 += logEmpType5[month.ToString()];
                            }
                            else
                            {
                                StrEmpCount3 += 0;
                            }
                            countEmpRowsMonth++;
                        }
                        #endregion
                        allEmpJSON = StrEmpJSON + "]" + StrEmpCount1 + "]" + StrEmpCount2 + "]" + StrEmpCount3 + "]}]";

                        logEmpType0 = new Dictionary<string, int>();
                        EmpMonth0 = new List<int>();
                        logEmpType1 = new Dictionary<string, int>();
                        EmpMonth1 = new List<int>();
                        logEmpType5 = new Dictionary<string, int>();
                        EmpMonth5 = new List<int>();
                        StrEmpJSON = "";
                        StrEmpCount1 = "";
                        StrEmpCount2 = "";
                        StrEmpCount3 = "";
                        #region LogTYpe1
                        if (dtempDataReport != null)
                        {
                            foreach (DataRow dataRows in dtempDataReport.Select("LogType = 1"))
                            {
                                string Status = dataRows.ItemArray[1].ToString().Trim();
                                statusEmpCurrent = Status;
                                monthEmpCurrent = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());

                                if (countEmpRows == 0)
                                {
                                    statusEmpTemp = Status;
                                    monthEmpTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                }
                                else
                                {
                                    if (statusEmpTemp != statusEmpCurrent)
                                    {
                                        statusEmpTemp = Status;
                                        monthEmpTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                    }
                                    else
                                    {
                                        if (monthEmpCurrent < monthEmpTemp)
                                        {
                                            monthEmpCurrent = monthEmpCurrent + 20;
                                        }
                                    }
                                }

                                switch (Status)
                                {
                                    case "0":
                                        EmpMonth0.Add(monthEmpCurrent);
                                        logEmpType0.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;
                                    case "1":
                                        EmpMonth1.Add(monthEmpCurrent);
                                        logEmpType1.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;
                                    case "5":
                                        EmpMonth5.Add(monthEmpCurrent);
                                        logEmpType5.Add(dataRows.ItemArray[2].ToString().Trim(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                        break;

                                }
                                countEmpRows++;
                                monthEmpTemp = monthEmpCurrent;
                                statusEmpTemp = statusEmpCurrent;
                            }
                        }
                        EmpMergedMonthIn = EmpMonth5.Union(EmpMonth0.Union(EmpMonth1).ToList()).ToList();
                        EmpMergedMonthIn.Sort();
                        StrEmpJSON += "split[{\"Month\":[";
                        StrEmpCount1 += ",\"Count\":[";
                        StrEmpCount2 += ",\"Count2\":[";
                        StrEmpCount3 += ",\"Count3\":[";
                        countEmpRowsMonth = 1;
                        foreach (int month in EmpMergedMonthIn)
                        {
                            if (countEmpRowsMonth != 1)
                            {
                                StrEmpJSON += ",";
                                StrEmpCount1 += ",";
                                StrEmpCount2 += ",";
                                StrEmpCount3 += ",";
                            }
                            if (month < 20)
                            {
                                StrEmpJSON += "\"" + monthConv(month.ToString()) + "\"";
                            }
                            else
                            {
                                string monthEmpDelc = (month - 20) + "";
                                StrEmpJSON += "\"" + monthConv(monthEmpDelc) + "\"";
                            }

                            if (logEmpType0.ContainsKey(month.ToString()))
                            {
                                StrEmpCount1 += logEmpType0[month.ToString()];
                            }
                            else
                            {
                                StrEmpCount1 += 0;
                            }

                            if (logEmpType1.ContainsKey(month.ToString()))
                            {
                                StrEmpCount2 += logEmpType5[month.ToString()];
                            }
                            else
                            {
                                StrEmpCount2 += 0;
                            }

                            if (logEmpType5.ContainsKey(month.ToString()))
                            {
                                StrEmpCount3 += logEmpType5[month.ToString()];
                            }
                            else
                            {
                                StrEmpCount3 += 0;
                            }
                            countEmpRowsMonth++;
                        }
                        #endregion

                        allEmpJSON += StrEmpJSON + "]" + StrEmpCount1 + "]" + StrEmpCount2 + "]" + StrEmpCount3 + "]}]";


                        Response.Write(allEmpJSON);
                        Response.End();
                        break;
                    #endregion

                    #region empReportByID
                    case "empreportbyid":
                        string userEmpNameData = " ";
                        yearData = Int32.Parse(Request.QueryString["years"]);
                        if (!String.IsNullOrEmpty(Request.QueryString["userid"]))
                        {
                            string userEmpId = Request.QueryString["userid"];
                            DataTable dtEmpDataReportUser = fcommon.LinqToDataTable(_db.TEmployees.Where(w => w.SchoolID == userData.CompanyID)
                                                .Where(w => w.sIdentification == userEmpId || ((w.sName + " " + w.sLastname).Contains(userEmpId)))
                                                .Join(_db.TLogEmpTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList(), user => user.sEmp, logs => logs.sEmp,
                                                (user, logs) => new { Logs = logs, User = user }
                                                )
                                                .Where(w => w.Logs.nYear == yearData)
                                                .GroupBy(g => new
                                                {
                                                    g.User.sLastname,
                                                    g.User.sName,
                                                    g.Logs.LogEmpType,
                                                    g.Logs.LogEmpScanStatus,
                                                    g.Logs.LogEmpDate.Value.Month,
                                                    g.Logs.LogEmpDate.Value.Year
                                                })
                                                 .Select(g => new
                                                 {
                                                     g.Key.LogEmpType,
                                                     g.Key.LogEmpScanStatus,
                                                     currentMonth = g.Key.Month,
                                                     currentYear = g.Key.Year,
                                                     countData = g.Count(),
                                                     Name = g.Key.sName + " " + g.Key.sLastname
                                                 })
                                                .OrderBy(o => new
                                                {
                                                    o.LogEmpType,
                                                    o.LogEmpScanStatus,
                                                    o.currentMonth
                                                })
                                                            );
                            string userEmpallJSON = "";
                            string userEmpStrJSON = "";
                            string userEmpStrCount1 = "";
                            string userEmpStrCount2 = "";
                            string userEmpStrCount3 = "";
                            Dictionary<string, int> userEmplogType0 = new Dictionary<string, int>();
                            List<int> userEmpMonth0 = new List<int>();
                            Dictionary<string, int> userEmplogType1 = new Dictionary<string, int>();
                            List<int> userEmpMonth1 = new List<int>();
                            Dictionary<string, int> userEmplogType5 = new Dictionary<string, int>();
                            List<int> userEmpMonth5 = new List<int>();

                            List<int> userEmpMergedMonthIn = new List<int>();
                            string userEmpstatusTemp = "0";
                            string userEmpstatusCurrent = "0";
                            int userEmpmonthTemp = 0;
                            int userEmpmonthCurrent = 0;
                            int userEmpcountRows = 0;
                            #region LogTYpe0
                            if (dtEmpDataReportUser != null)
                            {
                                foreach (DataRow dataRows in dtEmpDataReportUser.Select("LogType = 0"))
                                {
                                    string Status = dataRows.ItemArray[1].ToString().Trim();
                                    userEmpstatusCurrent = Status;
                                    userEmpmonthCurrent = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                    userEmpNameData = dataRows.ItemArray[5].ToString();

                                    if (userEmpcountRows == 0)
                                    {
                                        userEmpstatusTemp = Status;
                                        userEmpmonthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                    }
                                    else
                                    {
                                        if (userEmpstatusTemp != userEmpstatusCurrent)
                                        {
                                            userEmpstatusTemp = Status;
                                            userEmpmonthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                        }
                                        else
                                        {
                                            if (userEmpmonthCurrent < userEmpmonthTemp)
                                            {
                                                userEmpmonthCurrent = userEmpmonthCurrent + 20;
                                            }
                                        }
                                    }

                                    switch (Status)
                                    {
                                        case "0":
                                            userEmpMonth0.Add(userEmpmonthCurrent);
                                            userEmplogType0.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;
                                        case "1":
                                            userEmpMonth1.Add(userEmpmonthCurrent);
                                            userEmplogType1.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;
                                        case "5":
                                            userEmpMonth5.Add(userEmpmonthCurrent);
                                            userEmplogType5.Add(dataRows.ItemArray[2].ToString().Trim(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;

                                    }
                                    userEmpcountRows++;
                                    userEmpmonthTemp = userEmpmonthCurrent;
                                    userEmpstatusTemp = userEmpstatusCurrent;
                                }
                            }
                            userEmpMergedMonthIn = userEmpMonth5.Union(userEmpMonth0.Union(userEmpMonth1).ToList()).ToList();
                            userEmpMergedMonthIn.Sort();
                            userEmpStrJSON += "[{\"Name\":\"" + userEmpNameData + "\",\"Month\":[";
                            userEmpStrCount1 += ",\"Count\":[";
                            userEmpStrCount2 += ",\"Count2\":[";
                            userEmpStrCount3 += ",\"Count3\":[";
                            int userEmpcountRowsMonth = 1;
                            foreach (int month in userEmpMergedMonthIn)
                            {
                                if (userEmpcountRowsMonth != 1)
                                {
                                    userEmpStrJSON += ",";
                                    userEmpStrCount1 += ",";
                                    userEmpStrCount2 += ",";
                                    userEmpStrCount3 += ",";
                                }
                                if (month < 20)
                                {
                                    userEmpStrJSON += "\"" + monthConv(month.ToString()) + "\"";
                                }
                                else
                                {
                                    string monthEmpDelc = (month - 20) + "";
                                    userEmpStrJSON += "\"" + monthConv(monthEmpDelc) + "\"";
                                }

                                if (userEmplogType0.ContainsKey(month.ToString()))
                                {
                                    userEmpStrCount1 += userEmplogType0[month.ToString()];
                                }
                                else
                                {
                                    userEmpStrCount1 += 0;
                                }

                                if (userEmplogType1.ContainsKey(month.ToString()))
                                {
                                    userEmpStrCount2 += userEmplogType5[month.ToString()];
                                }
                                else
                                {
                                    userEmpStrCount2 += 0;
                                }

                                if (userEmplogType5.ContainsKey(month.ToString()))
                                {
                                    userEmpStrCount3 += userEmplogType5[month.ToString()];
                                }
                                else
                                {
                                    userEmpStrCount3 += 0;
                                }
                                userEmpcountRowsMonth++;
                            }
                            #endregion
                            userEmpallJSON = userEmpStrJSON + "]" + userEmpStrCount1 + "]" + userEmpStrCount2 + "]" + userEmpStrCount3 + "]}]";

                            userEmplogType0 = new Dictionary<string, int>();
                            userEmpMonth0 = new List<int>();
                            userEmplogType1 = new Dictionary<string, int>();
                            userEmpMonth1 = new List<int>();
                            userEmplogType5 = new Dictionary<string, int>();
                            userEmpMonth5 = new List<int>();
                            userEmpStrJSON = "";
                            userEmpStrCount1 = "";
                            userEmpStrCount2 = "";
                            userEmpStrCount3 = "";
                            #region LogTYpe1
                            if (dtEmpDataReportUser != null)
                            {
                                foreach (DataRow dataRows in dtEmpDataReportUser.Select("LogType = 1"))
                                {
                                    string Status = dataRows.ItemArray[1].ToString().Trim();
                                    userEmpstatusCurrent = Status;
                                    userEmpmonthCurrent = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());

                                    if (userEmpcountRows == 0)
                                    {
                                        userEmpstatusTemp = Status;
                                        userEmpmonthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                    }
                                    else
                                    {
                                        if (userEmpstatusTemp != userEmpstatusCurrent)
                                        {
                                            userEmpstatusTemp = Status;
                                            userEmpmonthTemp = Int32.Parse(dataRows.ItemArray[2].ToString().Trim());
                                        }
                                        else
                                        {
                                            if (userEmpmonthCurrent < userEmpmonthTemp)
                                            {
                                                userEmpmonthCurrent = userEmpmonthCurrent + 20;
                                            }
                                        }
                                    }

                                    switch (Status)
                                    {
                                        case "0":
                                            userEmpMonth0.Add(userEmpmonthCurrent);
                                            userEmplogType0.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;
                                        case "1":
                                            userEmpMonth1.Add(userEmpmonthCurrent);
                                            userEmplogType1.Add(dataRows.ItemArray[2].ToString(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;
                                        case "5":
                                            userEmpMonth5.Add(userEmpmonthCurrent);
                                            userEmplogType5.Add(dataRows.ItemArray[2].ToString().Trim(), Int32.Parse(dataRows.ItemArray[4].ToString()));
                                            break;

                                    }
                                    userEmpcountRows++;
                                    userEmpmonthTemp = userEmpmonthCurrent;
                                    userEmpstatusTemp = userEmpstatusCurrent;
                                }
                            }
                            userEmpMergedMonthIn = userEmpMonth5.Union(userEmpMonth0.Union(userEmpMonth1).ToList()).ToList();
                            userEmpMergedMonthIn.Sort();
                            userEmpStrJSON += "split[{\"Name\":\"" + userEmpNameData + "\",\"Month\":[";
                            userEmpStrCount1 += ",\"Count\":[";
                            userEmpStrCount2 += ",\"Count2\":[";
                            userEmpStrCount3 += ",\"Count3\":[";
                            userEmpcountRowsMonth = 1;
                            foreach (int month in userEmpMergedMonthIn)
                            {
                                if (userEmpcountRowsMonth != 1)
                                {
                                    userEmpStrJSON += ",";
                                    userEmpStrCount1 += ",";
                                    userEmpStrCount2 += ",";
                                    userEmpStrCount3 += ",";
                                }
                                if (month < 20)
                                {
                                    userEmpStrJSON += "\"" + monthConv(month.ToString()) + "\"";
                                }
                                else
                                {
                                    string monthEmpDelc = (month - 20) + "";
                                    userEmpStrJSON += "\"" + monthConv(monthEmpDelc) + "\"";
                                }

                                if (userEmplogType0.ContainsKey(month.ToString()))
                                {
                                    userEmpStrCount1 += userEmplogType0[month.ToString()];
                                }
                                else
                                {
                                    userEmpStrCount1 += 0;
                                }

                                if (userEmplogType1.ContainsKey(month.ToString()))
                                {
                                    userEmpStrCount2 += userEmplogType5[month.ToString()];
                                }
                                else
                                {
                                    userEmpStrCount2 += 0;
                                }

                                if (userEmplogType5.ContainsKey(month.ToString()))
                                {
                                    userEmpStrCount3 += userEmplogType5[month.ToString()];
                                }
                                else
                                {
                                    userEmpStrCount3 += 0;
                                }
                                userEmpcountRowsMonth++;
                            }
                            #endregion
                            userEmpallJSON += userEmpStrJSON + "]" + userEmpStrCount1 + "]" + userEmpStrCount2 + "]" + userEmpStrCount3 + "]}]";


                            Response.Write(userEmpallJSON);
                            Response.End();
                        }
                        break;
                        #endregion

                        #endregion
                }
            }
        }

        public string monthConv(string month)
        {
            string txtMonth = "";
            switch (month)
            {
                case "1": txtMonth = "มกราคม"; break;
                case "2": txtMonth = "กุมภาพันธ์"; break;
                case "3": txtMonth = "มีนาคม"; break;
                case "4": txtMonth = "เมษายน"; break;
                case "5": txtMonth = "พฤษภาคม"; break;
                case "6": txtMonth = "มิถุนายน"; break;
                case "7": txtMonth = "กรกฎาคม"; break;
                case "8": txtMonth = "สิงหาคม"; break;
                case "9": txtMonth = "กันยายน"; break;
                case "10": txtMonth = "ตุลาคม"; break;
                case "11": txtMonth = "พฤศจิกายน"; break;
                case "12": txtMonth = "ธันวาคม"; break;
            }
            return txtMonth;
        }
    }
}
