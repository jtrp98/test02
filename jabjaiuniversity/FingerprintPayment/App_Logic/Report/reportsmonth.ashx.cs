using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Web.SessionState;
using MasterEntity;
using FingerprintPayment.Models.Report;
using JabjaiEntity.DB;
using JabjaiMainClass;

namespace FingerprintPayment.App_Logic.Report
{
    /// <summary>
    /// Summary description for reportsmonth
    /// </summary>
    public class reportsmonth : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string sEntities = "sbactestEntities";// HttpContext.Current.Session["sEntities"].ToString();//"sbactestEntities";//
            dynamic rss = new JObject();
            string sterm = context.Request["term"];
            DateTime dt = DateTime.Parse(context.Request["dayreport"]);
            var term = int.Parse(sterm);
            List<_30DayUserTimeScan> _30DayUserTimeScanList = new List<_30DayUserTimeScan>();
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                rss.daylist = new JArray();
                rss.userdata = new JArray();
                //DateTime dt = new DateTime(2017, 7, 1);///กำหนดขอบเขตค้นหา(เดือน)
                for (int i = 0; dt.AddDays(i) <= dt.AddMonths(1).AddDays(-1); i++)
                {
                    dynamic coldata = new JObject();
                    coldata.LogDate = dt.AddDays(i).ToString("dd/MM/yyyy");
                    coldata.LogDayName = ChaDayOfWeek(dt.AddDays(i));

                    rss.daylist.Add(coldata);
                }

                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany.sEntities,ConnectionDB.Read)))
                {
                    var student = dbschool.TUser.Where(w => w.nTermSubLevel2 == term && w.cDel == null).AsQueryable().ToList();
                    var inschool = dbschool.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == term ).AsQueryable().ToList();
                    foreach (var item in student)
                    {
                        List<TimeScan> TimeScanList = new List<TimeScan>();
                        _30DayUserTimeScan _30DayUserTimeScan = new _30DayUserTimeScan();
                        var ss = inschool.Where(w => w.sID == item.sID).ToList();
                        _30DayUserTimeScan.sIdentification = item.sIdentification.ToString();
                        _30DayUserTimeScan.name = item.sName + " " + item.sLastname;
                        var dayofmonth = 0;
                        if (dt.Month != 4 && dt.Month != 6 && dt.Month != 9 && dt.Month != 11) { dayofmonth = 31; }
                        else if (dt.Month == 2) { dayofmonth = 28; }
                        else { dayofmonth = 30; }
                        for (int i = 0; dt.AddDays(i) <= dt.AddMonths(1).AddDays(-1); i++)
                        {
                            TimeScan TimeScan = new TimeScan();
                            var day = ss.Where(w => w.LogDate.Value == dt.AddDays(i)).FirstOrDefault();
                            if (day != null)
                            {
                                if (day.LogTime != null)
                                {
                                    TimeSpan time = new TimeSpan(day.LogTime.Value.Hours, day.LogTime.Value.Minutes, day.LogTime.Value.Seconds);
                                    TimeScan.LogTime = time.ToString();
                                }
                                else
                                {
                                    TimeScan.LogTime = "-";
                                }

                                if (day.LogScanStatus.Trim().Equals("0"))
                                {
                                    _30DayUserTimeScan.LogScanStatus0 += 1;
                                }
                                else if (day.LogScanStatus.Trim().Equals("1"))
                                {
                                    _30DayUserTimeScan.LogScanStatus1 += 1;
                                }
                                else if (day.LogScanStatus.Trim().Equals("3"))
                                {
                                    _30DayUserTimeScan.LogScanStatus3 += 1;
                                }
                                else if (day.LogScanStatus.Trim().Equals("9"))
                                {
                                    _30DayUserTimeScan.LogScanStatus9 += 1;
                                }
                                TimeScan.scanstatus = day.LogScanStatus;
                                TimeScan.LogDay = i;
                                TimeScan.LogDate = day.LogDate.Value.ToString("dd/MM/yyyy");
                                //TimeScan.LogDayName = ChaDayOfWeek(day.LogDate.Value);

                            }
                            else
                            {
                                TimeScan.LogDay = i;
                                TimeScan.LogTime = "-";
                                _30DayUserTimeScan.LogScanStatus3 += 1;
                                TimeScan.scanstatus = "3";
                                //DateTime dtn = new DateTime(dt.Year, dt.Month, i);
                                TimeScan.LogDate = dt.AddDays(i).ToString("dd/MM/yyyy");
                                //TimeScan.LogDayName = ChaDayOfWeek(dtn);
                            }

                            TimeScanList.Add(TimeScan);
                            //ViewBag.Month = TimeScanList;
                        }
                        _30DayUserTimeScan.LogScanPercent0 = (_30DayUserTimeScan.LogScanStatus0 * 100) / (dayofmonth - _30DayUserTimeScan.LogScanStatus9);
                        _30DayUserTimeScan.LogScanPercent1 = (_30DayUserTimeScan.LogScanStatus1 * 100) / (dayofmonth - _30DayUserTimeScan.LogScanStatus9);
                        _30DayUserTimeScan.LogScanPercent3 = (_30DayUserTimeScan.LogScanStatus3 * 100) / (dayofmonth - _30DayUserTimeScan.LogScanStatus9);
                        //_30DayUserTimeScan.LogScanPercent9 = (_30DayUserTimeScan.LogScanStatus9 * 100) / (dayofmonth- _30DayUserTimeScan.LogScanStatus9);
                        _30DayUserTimeScan.time = TimeScanList;
                        _30DayUserTimeScanList.Add(_30DayUserTimeScan);
                        //ViewBag._30Day = _30DayUserTimeScanList;

                        //foreach (var data in _30DayUserTimeScanList)
                        //{
                        dynamic dataJSON = new JObject();
                        dataJSON.name = _30DayUserTimeScan.name;
                        dataJSON.sIdentification = _30DayUserTimeScan.sIdentification;
                        dataJSON.time = new JArray(from a in _30DayUserTimeScan.time
                                                   select new JObject(
                                                       new JProperty("LogTime", a.LogTime),
                                                       new JProperty("scanstatus", a.scanstatus),
                                                       new JProperty("LogDate", a.LogDate)
                                                       ));
                        dataJSON.LogScanStatus9 = _30DayUserTimeScan.LogScanStatus9;
                        dataJSON.LogScanStatus0 = _30DayUserTimeScan.LogScanStatus0;
                        dataJSON.LogScanStatus1 = _30DayUserTimeScan.LogScanStatus1;
                        dataJSON.LogScanStatus3 = _30DayUserTimeScan.LogScanStatus3;
                        dataJSON.LogScanStatus9 = _30DayUserTimeScan.LogScanStatus9;
                        dataJSON.LogScanPercent0 = _30DayUserTimeScan.LogScanPercent0;
                        dataJSON.LogScanPercent1 = _30DayUserTimeScan.LogScanPercent1;
                        dataJSON.LogScanPercent3 = _30DayUserTimeScan.LogScanPercent3;
                        dataJSON.LogScanPercent9 = _30DayUserTimeScan.LogScanPercent9;
                        rss.userdata.Add(dataJSON);
                        //}
                    }
                }

                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(rss);
                context.Response.End();
            }
        }

        private string ChaDayOfWeek(DateTime dtn)
        {
            if (dtn.DayOfWeek == DayOfWeek.Sunday)
            {
                return "อา";
            }
            else if (dtn.DayOfWeek == DayOfWeek.Monday)
            {
                return "จ";
            }
            else if (dtn.DayOfWeek == DayOfWeek.Tuesday)
            {
                return "อ";
            }
            else if (dtn.DayOfWeek == DayOfWeek.Wednesday)
            {
                return "พ";
            }
            else if (dtn.DayOfWeek == DayOfWeek.Thursday)
            {
                return "พฤ";
            }
            else if (dtn.DayOfWeek == DayOfWeek.Friday)
            {
                return "ศ";
            }
            else
            {
                return "ส";
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
}