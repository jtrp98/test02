using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;
using System.Globalization;
using System.Web.DynamicData;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using FingerprintPayment.App_Logic;
using System.Web.Script.Serialization;
using System.Web.WebPages.Html;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using FingerprintPayment.Helper;
using System.Data.Entity;
using System.Web.Hosting;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class holidaysettings : System.Web.UI.Page
    {
        public class Search
        {
            public string start { get; set; }
            public string end { get; set; }
            public string type { get; set; }
            public string yearStr { get; set; }
        }
        public class LogModel
        {
            public DateTime? Date { get; set; }
            public string Detail { get; set; }
            public string ByName { get; set; }
        }

        protected List<LogModel> LOGS { get; set; }
        protected string CURRENT_YEAR { get; set; }
        protected JWTToken.userData userData = new JWTToken.userData();

        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }

        public static List<TEmployeeType> employeeTypes = new List<TEmployeeType>();
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            editSave.Click += new EventHandler(editSave_Click);
            deleteBtn.Click += new EventHandler(delete);
            Button1.Click += new EventHandler(register);

            //Button1.Click += new EventHandler(Button1_Click);

            if (!IsPostBack)
            {
                InitData();
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    employeeTypes = _db.TEmployeeTypes.Where(w => w.SchoolID == userData.CompanyID && w.IsDel == false && w.IsActive == true).ToList();
                    fcommon.LinqToDropDownList(employeeTypes, ddlEmpType_edit, "", "nTypeId", "Title");
                    fcommon.LinqToDropDownList(employeeTypes, ddlEmpType, "", "nTypeId", "Title");

                    int sEmpID = int.Parse(Session["sEmpID"] + "");
                    string year = Request.QueryString["year"];
                    string type = Request.QueryString["type"];
                    string start = Request.QueryString["start"];
                    string end = Request.QueryString["end"];

                    ddlType.SelectedValue = type;
                    startDay.Text = start;
                    endDay.Text = end;

                    using (var en = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                    {
                        //var listYear = en.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                        //    .AsEnumerable()
                        //    .Select(o => new SelectListItem
                        //    {
                        //        Text = o.numberYear + "",
                        //        Value = (Convert.ToInt32(o.numberYear) - 543) + "",
                        //    })
                        //    .OrderByDescending(x => x.Text)
                        //    .ToList();

                        var lstYear = en.THolidays.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)
                            .Select(o => new
                            {
                                y1 = o.dHolidayStart.Value.Year,
                                y2 = o.dHolidayEnd.Value.Year
                            })
                            .Distinct()
                            .ToList();

                        var y1 = lstYear.Select(o => o.y1).ToList();
                        var y2 = lstYear.Select(o => o.y2).ToList();
                        var listYear = y1.Concat(y2).Distinct().OrderBy(o => o)
                                .Select(o => new SelectListItem
                                {
                                    Text = (o + 543) + "",
                                    Value = o + "",
                                })
                            .ToList();

                        fcommon.LinqToDropDownList(listYear, ddlYear, "", "Value", "Text");
                        fcommon.LinqToDropDownList(listYear, ddlYearCopy, "", "Value", "Text");
                        ddlYear.SelectedValue = string.IsNullOrEmpty(year) ? DateTime.Now.Year + "" : year;
                        ddlYearCopy.SelectedValue = (DateTime.Now.Year + 1) + "";

                        var sl = new StudentLogic(en);
                        var currentTerm = sl.GetTermId(DateTime.Now.Date, userData);

                        CURRENT_YEAR = (from a in en.TTerms.Where(o => o.nTerm == currentTerm && o.SchoolID == userData.CompanyID)
                                        from b in en.TYears.Where(o => o.SchoolID == a.SchoolID && o.nYear == a.nYear)
                                        select b.numberYear + ""
                                       ).FirstOrDefault();

                        LOGS = (from a in en.THolidayLog.Where(o => o.SchoolID == userData.CompanyID)
                                from b in en.TEmployees.Where(o => o.SchoolID == a.SchoolID && o.sEmp == a.Creator).DefaultIfEmpty()
                                select new LogModel
                                {
                                    Date = a.Created,
                                    ByName = b.sName + " " + b.sLastname,
                                    Detail = a.Detail
                                }).OrderByDescending(o => o.Date)
                                      .Take(20)
                                      .ToList();


                    }
                }
            }
        }

        private void InitData()
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                classValue classValue = new classValue();
                foreach (var a in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nWorkingStatus == 1))
                {
                    var item2 = new ListItem
                    {
                        Text = a.SubLevel,
                        Value = a.nTSubLevel.ToString()
                    };
                    modalPlanType.Items.Add(item2);
                    modalPlanType2.Items.Add(item2);

                    foreach (var b in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == a.nTSubLevel && w.nWorkingStatus == 1))
                    {
                        var item = new ListItem
                        {
                            Text = a.SubLevel + "/" + b.nTSubLevel2,
                            Value = b.nTermSubLevel2.ToString()
                        };

                        modalClass.Items.Add(item);
                        modalClass2.Items.Add(item);
                    }
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadData(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }


            //string start = Request.QueryString["start"];
            //string end = Request.QueryString["end"];
            //string type = Request.QueryString["type"];
            //string yearStr = Request.QueryString["year"];
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                DateTime? startDate = null, endDate = null;
                int? year = null;

                if (!string.IsNullOrEmpty(search.yearStr))
                    year = Convert.ToInt32(search.yearStr);
                else
                    year = DateTime.Now.Year;

                //var yearId = _db.TYears.Where(o => o.SchoolID == userData.CompanyID && o.numberYear == (year + 543) && o.cDel == false)
                //    .Select(o => o.nYear)
                //    .FirstOrDefault();

                //var term = _db.TTerms.Where(o => o.SchoolID == userData.CompanyID && o.nYear == yearId && o.cDel != "1")
                //    .Select(o => new
                //    {
                //        o.dStart,
                //        o.dEnd
                //    }).ToList();

                if (!string.IsNullOrEmpty(search.start))
                    startDate = DateTime.ParseExact(search.start, "dd/MM/yyyy", new CultureInfo("th"));
                //else
                //    startDate = term.Min(o => o.dStart);

                if (!string.IsNullOrEmpty(search.end))
                    endDate = DateTime.ParseExact(search.end, "dd/MM/yyyy", new CultureInfo("th"));
                //else
                //    endDate = term.Max(o => o.dEnd);

                var data = returnlist(startDate, endDate, search.type, year);
                return new
                {
                    data = data
                };
            }
            //dgd.DataSource = 
            //dgd.PageSize = 999;
            //dgd.DataBind();

        }

        void editSave_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var listStudentLevel1 = new List<int>();
            var listStudentLevel2 = new List<int>();
            var listEmpType = new List<string>();

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var serializer = new JavaScriptSerializer();

                serializer.RegisterConverters(new[] { new DateTimeConverter() });
                dynamic rss = null;

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                var a = _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nHoliday == editid.Text).FirstOrDefault();
                rss = new { Holidays = a, HolidaySomes = _db.THolidaySomes.Where(w => w.nHoliday == a.nHoliday).ToList() };

                string start = editStart.Text;
                string end = editEnd.Text;

                DateTime endDate = DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("th"));
                DateTime startDate = DateTime.ParseExact(start, "dd/MM/yyyy", new CultureInfo("th"));

                var newType = "";
                if (editType.Text == "0")
                    newType = "0";
                else newType = "3";

                var logs = $@"แก้ไขปฏิทิน ชื่อวัน {editName.Text} 
{(a.sHolidayType == newType ? "" : "จากประเภท " + (a.sHolidayType == "0" ? "วันหยุด" : "กิจกรรม") + "เป็น" + (newType == "0" ? "วันหยุด" : "กิจกรรม"))}
{(newType != "0" ? (cStatusActive1.SelectedValue == "1" ? "เปิด" : "ปิด") + "การปรับสถานะกิจกรรมอัติโนมัติ" : "")} 
จากวันที่ {start} ถึง {end}";
                //log.Detail = $"เพิ่มปฏิทิน ชื่อวัน {a.sHoliday} ประเภท {(type == "0" ? "วันหยุด" : "กิจกรรม")} จากวันที่ {a.dHolidayStart?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))} ถึง {a.dHolidayEnd?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))}";
                var oldDate = Enumerable.Range(0, (a.dHolidayEnd - a.dHolidayStart).Value.Days + 1)
                    .Select(o => a.dHolidayStart.Value.AddDays(o))
                    .ToList();

                var newDate = Enumerable.Range(0, (endDate - startDate).Days + 1)
                   .Select(o => startDate.AddDays(o))
                   .ToList();

                //a.TimeType = Convert.ToByte(ddlTimeType2.SelectedValue);
                a.dHolidayEnd = endDate;
                if (editType.Text == "0")
                    a.sHolidayType = "0";
                else a.sHolidayType = "3";
                a.sHoliday = editName.Text;
                a.dHolidayStart = startDate;
                a.sColor = int.Parse(editColor.Text);
                a.cStatusActive = cStatusActive1.SelectedValue == "1";

                string who = editWho.SelectedValue;
                if (who == "3" || who == "4")
                {
                    a.sHolidayAll = "0";
                }
                else
                {
                    a.sHolidayAll = "1";
                }

                bool changeWhoSeeThis = (a.sWhoSeeThis != who);

                a.sWhoSeeThis = who;
                a.UpdatedBy = userData.UserID;
                a.UpdatedDate = DateTime.Now;

                string editmulti = editmulticlass_0.Text;
                string editmulti2 = editmulticlass_1.Text;
                string[] words = editmulti.Split('/');
                string[] words2 = editmulti2.Split('/');
                string[] words3 = editmulticlass_2.Text.Split('/');

                if (who == "3") //เฉพาะห้อง
                {
                    List<int?> oldLevelIDs = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && w.Deleted == null).Select(s => s.nTSubLevel).ToList();

                    int runID = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday).Count() + 1;
                    List<int?> level2IDs = new List<int?>();
                    foreach (string word in words)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            int level2ID = int.Parse(word);

                            level2IDs.Add(level2ID);
                            listStudentLevel2.Add(runID);

                            var check = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && w.nTSubLevel == level2ID).FirstOrDefault();
                            if (check == null)
                            {
                                string holidaySomeID = string.Format(@"{0}{1:000}", a.nHoliday, runID++);

                                THolidaySome some = new THolidaySome();
                                some.nHolidaySomeID = holidaySomeID;
                                some.nHoliday = a.nHoliday;
                                some.nTSubLevel = level2ID;
                                some.SchoolID = userData.CompanyID;
                                some.CreatedBy = userData.UserID;
                                some.CreatedDate = DateTime.Now;

                                _db.THolidaySomes.Add(some);
                            }
                            else
                            {
                                check.Deleted = null;
                            }
                        }
                    }

                    if (changeWhoSeeThis)
                    {
                        // remove old level id (who = 4)
                        if (oldLevelIDs.Count > 0)
                        {
                            var holidaySomes = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && oldLevelIDs.Contains(w.nTSubLevel));
                            foreach (var h in holidaySomes)
                            {
                                h.Deleted = 1;
                            }
                        }
                    }
                    else
                    {
                        // remove class id out list
                        if (level2IDs.Count > 0)
                        {
                            var holidaySomes = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && !level2IDs.Contains(w.nTSubLevel));
                            foreach (var h in holidaySomes)
                            {
                                h.Deleted = 1;
                            }
                        }
                    }
                }
                else if (who == "4") //เฉพาะระดับชั้น
                {
                    List<int?> oldLevel2IDs = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && w.Deleted == null).Select(s => s.nTSubLevel).ToList();

                    int runID = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday).Count() + 1;
                    List<int?> levelIDs = new List<int?>();
                    foreach (string word in words2)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            int levelID = int.Parse(word);

                            levelIDs.Add(levelID);
                            listStudentLevel1.Add(levelID);

                            var check = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && w.nTSubLevel == levelID).FirstOrDefault();
                            if (check == null)
                            {
                                string holidaySomeID = string.Format(@"{0}{1:000}", a.nHoliday, runID++);

                                THolidaySome some = new THolidaySome();
                                some.nHolidaySomeID = holidaySomeID;
                                some.nHoliday = a.nHoliday;
                                some.nTSubLevel = levelID;
                                some.SchoolID = userData.CompanyID;
                                some.CreatedBy = userData.UserID;
                                some.CreatedDate = DateTime.Now;

                                _db.THolidaySomes.Add(some);
                            }
                            else
                            {
                                check.Deleted = null;
                            }
                        }
                    }

                    if (changeWhoSeeThis)
                    {
                        // remove old class id (who = 3)
                        if (oldLevel2IDs.Count > 0)
                        {
                            var holidaySomes = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && oldLevel2IDs.Contains(w.nTSubLevel));
                            foreach (var h in holidaySomes)
                            {
                                h.Deleted = 1;
                            }
                        }
                    }
                    else
                    {
                        // remove level id out list
                        if (levelIDs.Count > 0)
                        {
                            var holidaySomes = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && !levelIDs.Contains(w.nTSubLevel));
                            foreach (var h in holidaySomes)
                            {
                                h.Deleted = 1;
                            }
                        }
                    }
                }
                else if (who == "5")
                {
                    List<int?> oldLevelIDs = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && w.Deleted == null).Select(s => s.nTSubLevel).ToList();

                    int runID = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday).Count() + 1;
                    List<int?> level2IDs = new List<int?>();
                    foreach (var data in _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday))
                    {
                        data.Deleted = 1;
                    }

                    foreach (string word in words3)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            int level2ID = int.Parse(word);
                            listEmpType.Add(word);
                            level2IDs.Add(level2ID);

                            var check = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && w.nTSubLevel == level2ID).FirstOrDefault();
                            if (check == null)
                            {
                                string holidaySomeID = string.Format(@"{0}{1:000}", a.nHoliday, runID++);

                                THolidaySome some = new THolidaySome();
                                some.nHolidaySomeID = holidaySomeID;
                                some.nHoliday = a.nHoliday;
                                some.nTSubLevel = level2ID;
                                some.SchoolID = userData.CompanyID;
                                some.CreatedBy = userData.UserID;
                                some.CreatedDate = DateTime.Now;

                                _db.THolidaySomes.Add(some);
                            }
                            else
                            {
                                check.Deleted = null;
                            }
                        }
                    }
                }
                else
                {
                    var holidaySomes = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday);
                    foreach (var h in holidaySomes)
                    {
                        h.Deleted = 1;
                    }
                }

                string json = serializer.Serialize(rss);

                TB_HistorySetting _HistorySetting = new TB_HistorySetting()
                {
                    Fd_SettingData = json,
                    Fd_UpdatedDate = DateTime.Now,
                    Fd_FunctionName = "Holiday Settings",
                    Fd_HistoryID = Guid.NewGuid(),
                    Fd_SchoolID = userData.CompanyID,
                    Fd_UpdatedBy = userData.UserID
                };

                _db.TB_HistorySetting.Add(_HistorySetting);

                var log = new THolidayLog();
                log.HolidayID = a.nHoliday;
                log.SchoolID = userData.CompanyID;
                log.Created = DateTime.Now;
                log.Creator = userData.UserID;
                log.Detail = logs;
                //log.Detail = $"เพิ่มปฏิทิน ชื่อวัน {a.sHoliday} ประเภท {(type == "0" ? "วันหยุด" : "กิจกรรม")} จากวันที่ {a.dHolidayStart?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))} ถึง {a.dHolidayEnd?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))}";
                _db.THolidayLog.Add(log);

                _db.SaveChanges();

                HostingEnvironment.QueueBackgroundWorkItem(ct =>
                {
                    CancleScore(a, userData, startDate, endDate, who, listStudentLevel1, listStudentLevel2, listEmpType);

                    var dates = oldDate.Except(newDate).ToList();
                    if (dates.Count > 0)
                    {
                        Set2UnknowScore(a, userData, startDate, dates, who, listStudentLevel1, listStudentLevel2, listEmpType);
                    }
                });
                Response.Redirect("holidaysettings.aspx");
            }
        }

        void register(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            var listStudentLevel1 = new List<int>();
            var listStudentLevel2 = new List<int>();
            var listEmpType = new List<string>();

            using (var _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (var _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                string start = modalStart.Text;
                string end = modalEnd.Text;
                string type = modalType.SelectedValue;
                string color = modalColor.SelectedValue;
                string who = modalWho.SelectedValue;
                string name = modalPlanName.Text;
                string classDetail = multiclassTxt1.Text;
                string classDetail2 = multiclassTxt2.Text;
                string[] words = classDetail.Split('/');
                string[] words2 = classDetail2.Split('/');
                string[] words3 = multiclassTxt3.Text.Split('/');

                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                if (end == "" || start == "")
                {
                    Response.Write("<script>alert('กรุณาตรวจสอบวันที่เริ่มและวันสิ้นสุดให้ถูกต้อง');</script>");
                    return;
                }

                DateTime startDate = DateTime.ParseExact(start, "dd/MM/yyyy", new CultureInfo("th"));
                DateTime endDate = DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("th"));

                int countRecord = _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Count();
                string holidayID = string.Format(@"{0:#000}{1:000000}", userData.CompanyID, countRecord + 1); // {4:SchoolID}{6:RunNumber}

                string type2 = (type == "0" ? "0" : "3");

                THoliday holiday = new THoliday();
                //holiday.TimeType = Convert.ToByte(ddlTimeType.SelectedValue);
                holiday.dHolidayEnd = endDate;
                holiday.dHolidayStart = startDate;
                holiday.nHoliday = holidayID;
                holiday.sHoliday = name;
                holiday.sHolidayType = type2;
                holiday.cStatusActive = cStatusActive0.SelectedValue == "1";

                if (who == "3" || who == "4")
                {
                    holiday.sHolidayAll = "0";
                }
                else
                {
                    holiday.sHolidayAll = "1";
                }

                holiday.sWhoSeeThis = who;
                holiday.sColor = Int32.Parse(color);
                holiday.SchoolID = userData.CompanyID;
                holiday.CreatedBy = userData.UserID;
                holiday.CreatedDate = DateTime.Now;

                _db.THolidays.Add(holiday);

                if (who == "3") //เฉพาะห้อง
                {
                    int runID = 1;
                    foreach (string word in words)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            int level2ID = int.Parse(word);
                            listStudentLevel2.Add(level2ID);
                            string holidaySomeID = string.Format(@"{0}{1:000}", holidayID, runID++);

                            THolidaySome some = new THolidaySome();
                            some.nHolidaySomeID = holidaySomeID;
                            some.nHoliday = holidayID;
                            some.nTSubLevel = level2ID;
                            some.SchoolID = userData.CompanyID;
                            some.CreatedBy = userData.UserID;
                            some.CreatedDate = DateTime.Now;

                            _db.THolidaySomes.Add(some);
                        }
                    }
                }
                else if (who == "4") //เฉพาะระดับชั้น
                {
                    int runID = 1;
                    foreach (string word in words2)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            int levelID = int.Parse(word);
                            listStudentLevel1.Add(levelID);
                            string holidaySomeID = string.Format(@"{0}{1:000}", holidayID, runID++);

                            THolidaySome some = new THolidaySome();
                            some.nHolidaySomeID = holidaySomeID;
                            some.nHoliday = holidayID;
                            some.nTSubLevel = levelID;
                            some.SchoolID = userData.CompanyID;
                            some.CreatedBy = userData.UserID;
                            some.CreatedDate = DateTime.Now;

                            _db.THolidaySomes.Add(some);
                        }
                    }
                }
                else if (who == "5")
                {
                    List<int?> oldLevelIDs = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == holidayID && w.Deleted == null).Select(s => s.nTSubLevel).ToList();

                    int runID = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == holidayID).Count() + 1;
                    List<int?> level2IDs = new List<int?>();
                    foreach (string word in words3)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            int level2ID = int.Parse(word);

                            level2IDs.Add(level2ID);
                            listEmpType.Add(word);

                            var check = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == holidayID && w.nTSubLevel == level2ID).FirstOrDefault();
                            if (check == null)
                            {
                                string holidaySomeID = string.Format(@"{0}{1:000}", holidayID, runID++);

                                THolidaySome some = new THolidaySome();
                                some.nHolidaySomeID = holidaySomeID;
                                some.nHoliday = holidayID;
                                some.nTSubLevel = level2ID;
                                some.SchoolID = userData.CompanyID;
                                some.CreatedBy = userData.UserID;
                                some.CreatedDate = DateTime.Now;

                                _db.THolidaySomes.Add(some);
                            }
                        }
                    }
                }

                var log = new THolidayLog();
                log.HolidayID = holiday.nHoliday;
                log.SchoolID = userData.CompanyID;
                log.Created = DateTime.Now;
                log.Creator = userData.UserID;
                log.Detail = $@"เพิ่มปฏิทิน ชื่อวัน {holiday.sHoliday} ประเภท {(type == "0" ? "วันหยุด" : "กิจกรรม")} 
 {(type != "0" ? (holiday.cStatusActive == true ? "เปิด" : "ปิด") + "การปรับสถานะกิจกรรมอัติโนมัติ" : "")} 
จากวันที่ {holiday.dHolidayStart?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))} ถึง {holiday.dHolidayEnd?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))}";
                _db.THolidayLog.Add(log);

                _db.SaveChanges();

                HostingEnvironment.QueueBackgroundWorkItem(ct =>
                {
                    CancleScore(holiday, userData, startDate, endDate, who, listStudentLevel1, listStudentLevel2, listEmpType);
                });
                Response.Redirect("holidaysettings.aspx");
            }
        }

        private void CancleScore(THoliday holiday, JWTToken.userData userData
        , DateTime startDate, DateTime endDate, string who
        , List<int> listStudentLevel1, List<int> listStudentLevel2, List<string> listEmpType)
        {
            if (holiday.sHolidayType == "3" && holiday.cStatusActive == false)
                return;

            if (startDate.Date > DateTime.Now.Date)
                return;

            using (var context = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var sl = new StudentLogic(context);
                var currentTerm = sl.GetTermId(startDate, userData);
                var qryEmp = context.TEmployees.Where(o => o.SchoolID == userData.CompanyID && o.cDel == null);
                var qryStd = context.TB_StudentViews.AsQueryable().Where(o => o.SchoolID == userData.CompanyID && o.cDel == null && o.nTerm == currentTerm).ToList();

                var lstStd = new List<int>();
                var lstEmp = new List<int>();

                switch (who)
                {
                    case "0":
                        break;
                    case "1":
                        qryStd = null;
                        break;
                    case "2":
                        qryEmp = null;
                        break;
                    case "3":
                        qryEmp = null;
                        qryStd = qryStd.Where(o => listStudentLevel2.Contains(o.nTermSubLevel2)).ToList();
                        break;
                    case "4":
                        qryEmp = null;
                        qryStd = qryStd.Where(o => listStudentLevel1.Contains(o.nTSubLevel)).ToList();
                        break;
                    case "5":
                        qryStd = null;
                        qryEmp = qryEmp.Where(o => listEmpType.Contains(o.cType));
                        break;

                    default:
                        break;
                }

                if (qryStd != null)
                {
                    var d = qryStd.Select(o => o.sID).ToList();
                    lstStd.AddRange(d);
                }

                if (qryEmp != null)
                {
                    var d = qryEmp.Select(o => o.sEmp).ToList();
                    lstEmp.AddRange(d);
                }

                //update status on logUser // logEmp
                //cancle in tbehaviorHistory
                //re calc scoreP
                if (lstStd.Count > 0)
                {
                    var dates = Enumerable.Range(0, (endDate - startDate).Days + 1).Select(o => startDate.AddDays(o));
                    var now = DateTime.Now.ToString("yyyyMMdd HH:mm");

                    var sql = "";
                    foreach (var date in dates)
                    {
                        if (date > DateTime.Now.Date)
                            continue;

                        sql += $@"
                            UPDATE [dbo].[TLogUserTimeScan]
                               SET [LogScanStatus] = {(holiday.sHolidayType == "3" ? 12 : 8)}    
                                  ,[UpdatedBy] = {userData.UserID}
                                  ,[UpdatedDate] = '{now}'   
                             WHERE SchoolID = {userData.CompanyID}
                             AND sID in ({string.Join(",", lstStd)})
                             AND LogDate = '{date.ToString("yyyyMMdd")}'
                             AND LogScanStatus <>  {(holiday.sHolidayType == "3" ? 12 : 8)}  
    
                            UPDATE [JabjaiSchoolHistory].[dbo].[TLogUserTimeScan]
                               SET [LogScanStatus] = {(holiday.sHolidayType == "3" ? 12 : 8)}    
                                  ,[UpdatedBy] = {userData.UserID}
                                  ,[UpdatedDate] = '{now}'   
                             WHERE SchoolID = {userData.CompanyID}
                             AND sID in ({string.Join(",", lstStd)})
                             AND LogDate = '{date.ToString("yyyyMMdd")}'
                             AND LogScanStatus <>  {(holiday.sHolidayType == "3" ? 12 : 8)}  ";

                        sql += $@"
                            UPDATE [dbo].[TBehaviorHistory]
                               SET 
                                   [dCanCel] = '{now}'
                                  ,[UserCancel] = {userData.UserID}
                                  ,IsHolidayCancel = 1

                             WHERE SchoolID = {userData.CompanyID}
                             AND StudentID in ({string.Join(",", lstStd)})
                             AND LogDate =  '{date.ToString("yyyyMMdd")}'
                             AND dCanCel is null ";
                    }

                    var data = context.Database.ExecuteSqlCommand(sql);

                    foreach (var std in lstStd)
                    {
                        context.SP_CalculateScore(std, userData.CompanyID, currentTerm);
                    }
                }
            }
        }

        void delete(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

        

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                var a = _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nHoliday == deleteid.Text).FirstOrDefault();
                a.cDel = "1";
                foreach (var b in _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nHoliday == a.nHoliday))
                {
                    b.Deleted = 1;
                }

                _db.SaveChanges();

                var listStudentLevel1 = new List<int>();
                var listStudentLevel2 = new List<int>();
                var listEmpType = new List<string>();

                var who = a.sWhoSeeThis;

                if (who == "3") //เฉพาะห้อง
                {
                    listStudentLevel2 = _db.THolidaySomes
                        .Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && w.Deleted == null)
                        .Select(s => s.nTSubLevel.Value)
                        .ToList();
                }
                else if (who == "4") //เฉพาะระดับชั้น
                {
                    listStudentLevel1 = _db.THolidaySomes
                        .Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && w.Deleted == null)
                        .Select(s => s.nTSubLevel.Value)
                        .ToList();
                }
                else if (who == "5")
                {
                    listEmpType = _db.THolidaySomes
                        .Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == a.nHoliday && w.Deleted == null)
                        .Select(s => s.nTSubLevel + "")
                        .ToList();
                }


                HostingEnvironment.QueueBackgroundWorkItem(ct =>
                {
                    var dates = Enumerable.Range(0, (a.dHolidayEnd - a.dHolidayStart).Value.Days + 1)
                    .Select(o => a.dHolidayStart.Value.AddDays(o))
                    .ToList();

                    Set2UnknowScore(a, userData, a.dHolidayStart.Value, dates, a.sWhoSeeThis, listStudentLevel1, listStudentLevel2, listEmpType);
                });

                var log = new THolidayLog();
                log.HolidayID = a.nHoliday;
                log.SchoolID = userData.CompanyID;
                log.Created = DateTime.Now;
                log.Creator = userData.UserID;
                log.Detail = $"ลบปฏิทิน ชื่อวัน {a.sHoliday} จากวันที่ {a.dHolidayStart?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))} ถึง {a.dHolidayEnd?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))}";
                _db.THolidayLog.Add(log);

                _db.SaveChanges();

                Response.Redirect("holidaysettings.aspx");
            }
        }

        private void Set2UnknowScore(THoliday holiday, JWTToken.userData userData
          , DateTime startDate, List<DateTime> dates, string who
         , List<int> listStudentLevel1, List<int> listStudentLevel2, List<string> listEmpType)
        {
            if (holiday.sHolidayType == "3" && holiday.cStatusActive == false)
                return;

            if (startDate.Date > DateTime.Now.Date)
                return;

            using (var context = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var sl = new StudentLogic(context);
                var currentTerm = sl.GetTermId(startDate, userData);
                var qryEmp = context.TEmployees.Where(o => o.SchoolID == userData.CompanyID && o.cDel == null);
                List<TB_StudentViews> qryStd = context.TB_StudentViews.AsQueryable().Where(o => o.SchoolID == userData.CompanyID && o.cDel == null && o.nTerm == currentTerm).ToList();

                var lstStd = new List<int>();
                var lstEmp = new List<int>();

                switch (who)
                {
                    case "0":
                        break;
                    case "1":
                        qryStd = null;
                        break;
                    case "2":
                        qryEmp = null;
                        break;
                    case "3":
                        qryEmp = null;
                        qryStd = qryStd.Where(o => listStudentLevel2.Contains(o.nTermSubLevel2)).ToList();
                        break;
                    case "4":
                        qryEmp = null;
                        qryStd = qryStd.Where(o => listStudentLevel1.Contains(o.nTSubLevel)).ToList();
                        break;
                    case "5":
                        qryStd = null;
                        qryEmp = qryEmp.Where(o => listEmpType.Contains(o.cType));
                        break;

                    default:
                        break;
                }

                if (qryStd != null)
                {
                    var d = qryStd.Select(o => o.sID).ToList();
                    lstStd.AddRange(d);
                }

                if (qryEmp != null)
                {
                    var d = qryEmp.Select(o => o.sEmp).ToList();
                    lstEmp.AddRange(d);
                }

                //update status on logUser // logEmp
                //cancle in tbehaviorHistory
                //re calc score
                if (lstStd.Count > 0)
                {
                    //var dates = Enumerable.Range(0, (endDate - startDate).Days + 1).Select(o => startDate.AddDays(o));
                    var now = DateTime.Now.ToString("yyyyMMdd HH:mm");

                    var sql = "";
                    foreach (var date in dates)
                    {
                        if (date > DateTime.Now.Date)
                            continue;

                        sql += $@"
                            UPDATE [dbo].[TLogUserTimeScan]
                               SET [LogScanStatus] = 99   
                                  ,[UpdatedBy] = {userData.UserID}
                                  ,[UpdatedDate] = '{now}'   
                             WHERE SchoolID = {userData.CompanyID}
                             AND sID in ({string.Join(",", lstStd)})
                             AND LogDate = '{date.ToString("yyyyMMdd")}' ";
                    }

                    var data = context.Database.ExecuteSqlCommand(sql);

                }
            }
        }
        protected static List<holidaylist> returnlist(DateTime? startday, DateTime? endday, string type, int? year)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

           
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {

                var tCompany = dbmaster.TCompanies.Where(w => w.nCompany == userData.CompanyID).FirstOrDefault();

                //var dateCheck = DateTime.Now.AddYears(1000);
                //if (endday == null)
                //    endday = DateTime.Now.AddYears(1000);
                //if (startday == null)
                //    startday = DateTime.Now.AddYears(-1000);

                List<holidaylist> holidaylist = new List<holidaylist>();
                holidaylist holiday = new holidaylist();

                int count = 1;

                var qry1 = _db.THolidays.Where(w => w.SchoolID == userData.CompanyID
                        && (w.dHolidayEnd.Value.Year == year || w.dHolidayStart.Value.Year == year)
                        && w.cDel == null
                        );

                //var qry2 = _db.THolidays.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null && w.TimeType == 2);

                //var lst2 = qry2.ToList();
                //foreach (var item in lst2)
                //{
                //    var diff1 = year - item.dHolidayStart?.Year;
                //    var diff2 = year - item.dHolidayEnd?.Year;
                //    item.dHolidayStart = item.dHolidayStart.Value.AddYears(diff1 ?? 0);
                //    item.dHolidayEnd = item.dHolidayEnd.Value.AddYears(diff2 ?? 0);
                //}

                if (startday.HasValue)
                {
                    qry1 = qry1.Where(w => w.dHolidayEnd >= startday);
                    //lst2 = lst2.Where(w => w.dHolidayEnd >= startday).ToList();
                }

                if (endday.HasValue)
                {
                    qry1 = qry1.Where(w => w.dHolidayStart <= endday);
                    //lst2 = lst2.Where(w => w.dHolidayStart <= endday).ToList();
                }

                if (type == "0")
                {
                    var lst = qry1.ToList().OrderBy(o => o.dHolidayStart);

                    foreach (var data in lst)
                    {
                        DateTime? st = data.dHolidayStart;
                        DateTime? ed = data.dHolidayEnd;
                        holiday = new holidaylist();
                        holiday.sort = data.dHolidayStart;
                        holiday.color = data.sColor.ToString();

                        holiday.start = data.dHolidayStart?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        holiday.end = data.dHolidayEnd?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        holiday.name = data.sHoliday;
                        holiday.TimeType = (data.TimeType == 2 ? "ประจำปี" : "ประจำวัน");

                        count = count + 1;
                        if (data.sHolidayType == "0")
                            holiday.type = "วันหยุด";
                        else if (data.sHolidayType == "1")
                            holiday.type = "วันหยุด";
                        else if (data.sHolidayType == "3")
                            holiday.type = "กิจกรรม";
                        else if (data.sHolidayType == "4")
                            holiday.type = "กิจกรรม";

                        if (data.sColor == null && (data.sHolidayType == "0" || data.sHolidayType == "1"))
                            holiday.color = "0";
                        if (data.sColor == null && (data.sHolidayType == "3" || data.sHolidayType == "4"))
                            holiday.color = "1";
                        if (data.sWhoSeeThis == "0" || data.sWhoSeeThis == null)
                            holiday.whoSee = "ทุกคน";
                        else if (data.sWhoSeeThis == "1")
                            holiday.whoSee = "อาจารย์/พนักงาน";
                        else if (data.sWhoSeeThis == "2")
                            holiday.whoSee = "นักเรียน";
                        else if (data.sWhoSeeThis == "3")
                            holiday.whoSee = "บางห้องเรียน";
                        else if (data.sWhoSeeThis == "4")
                            holiday.whoSee = "บางระดับชั้น";
                        else if (data.sWhoSeeThis == "5")
                            holiday.whoSee = "ตามประเภทบุคลากร";
                        holiday.nHoliday = data.nHoliday;
                        holiday.SchoolID = userData.CompanyID;
                        holiday.created = data.CreatedDate?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        holiday.modify = data.UpdatedDate?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        holidaylist.Add(holiday);
                    }
                }
                else if (type == "1")
                {
                    var lst = qry1
                        .Where(w => (w.sHolidayType == "0" || w.sHolidayType == "1"))
                        .ToList()

                        .OrderBy(o => o.dHolidayStart);

                    foreach (var data in lst)
                    {
                        DateTime? st = data.dHolidayStart;
                        DateTime? ed = data.dHolidayEnd;
                        holiday = new holidaylist();
                        holiday.sort = data.dHolidayStart;
                        holiday.color = data.sColor.ToString();
                        holiday.start = data.dHolidayStart?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        holiday.end = data.dHolidayEnd?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        holiday.name = data.sHoliday;
                        holiday.TimeType = (data.TimeType == 2 ? "ประจำปี" : "ประจำวัน");

                        count = count + 1;
                        if (data.sHolidayType == "0")
                            holiday.type = "วันหยุด";
                        else if (data.sHolidayType == "1")
                            holiday.type = "วันหยุด";
                        else if (data.sHolidayType == "3")
                            holiday.type = "กิจกรรม";
                        else if (data.sHolidayType == "4")
                            holiday.type = "กิจกรรม";

                        if (data.sColor == null && (data.sHolidayType == "0" || data.sHolidayType == "1"))
                            holiday.color = "0";
                        if (data.sColor == null && (data.sHolidayType == "3" || data.sHolidayType == "4"))
                            holiday.color = "1";
                        if (data.sWhoSeeThis == "0" || data.sWhoSeeThis == null)
                            holiday.whoSee = "ทุกคน";
                        else if (data.sWhoSeeThis == "1")
                            holiday.whoSee = "อาจารย์/พนักงาน";
                        else if (data.sWhoSeeThis == "2")
                            holiday.whoSee = "นักเรียน";
                        else if (data.sWhoSeeThis == "3")
                            holiday.whoSee = "บางห้องเรียน";
                        else if (data.sWhoSeeThis == "4")
                            holiday.whoSee = "บางระดับชั้น";
                        else if (data.sWhoSeeThis == "5")
                            holiday.whoSee = "ตามประเภทบุคลากร";
                        holiday.nHoliday = data.nHoliday;
                        holiday.SchoolID = userData.CompanyID;
                        holiday.created = data.CreatedDate?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        holiday.modify = data.UpdatedDate?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        holidaylist.Add(holiday);
                    }
                }
                else if (type == "2")
                {
                    var lst = qry1
                        .Where(w => (w.sHolidayType == "3" || w.sHolidayType == "4"))
                        .ToList()
                        //.Concat(lst2)
                        .OrderBy(o => o.dHolidayStart);

                    foreach (var data in lst)
                    {
                        DateTime? st = data.dHolidayStart;
                        DateTime? ed = data.dHolidayEnd;
                        holiday = new holidaylist();
                        holiday.sort = data.dHolidayStart;
                        holiday.color = data.sColor.ToString();
                        holiday.start = data.dHolidayStart?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        holiday.end = data.dHolidayEnd?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        holiday.name = data.sHoliday;
                        holiday.TimeType = (data.TimeType == 2 ? "ประจำปี" : "ประจำวัน");

                        count = count + 1;
                        if (data.sHolidayType == "0")
                            holiday.type = "วันหยุด";
                        else if (data.sHolidayType == "1")
                            holiday.type = "วันหยุด";
                        else if (data.sHolidayType == "3")
                            holiday.type = "กิจกรรม";
                        else if (data.sHolidayType == "4")
                            holiday.type = "กิจกรรม";

                        if (data.sColor == null && (data.sHolidayType == "0" || data.sHolidayType == "1"))
                            holiday.color = "0";
                        if (data.sColor == null && (data.sHolidayType == "3" || data.sHolidayType == "4"))
                            holiday.color = "1";
                        if (data.sWhoSeeThis == "0" || data.sWhoSeeThis == null)
                            holiday.whoSee = "ทุกคน";
                        else if (data.sWhoSeeThis == "1")
                            holiday.whoSee = "อาจารย์/พนักงาน";
                        else if (data.sWhoSeeThis == "2")
                            holiday.whoSee = "นักเรียน";
                        else if (data.sWhoSeeThis == "3")
                            holiday.whoSee = "บางห้องเรียน";
                        else if (data.sWhoSeeThis == "4")
                            holiday.whoSee = "บางระดับชั้น";
                        else if (data.sWhoSeeThis == "5")
                            holiday.whoSee = "ตามประเภทบุคลากร";
                        holiday.nHoliday = data.nHoliday;
                        holiday.SchoolID = userData.CompanyID;
                        holiday.created = data.CreatedDate?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        holiday.modify = data.UpdatedDate?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        holidaylist.Add(holiday);
                    }
                }
                else
                {
                    var lst = qry1.ToList().OrderBy(o => o.dHolidayStart);

                    foreach (var data in lst)
                    {
                        DateTime? st = data.dHolidayStart;
                        DateTime? ed = data.dHolidayEnd;
                        holiday = new holidaylist();
                        holiday.sort = data.dHolidayStart;
                        holiday.color = data.sColor.ToString();
                        holiday.start = data.dHolidayStart?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        holiday.end = data.dHolidayEnd?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        holiday.name = data.sHoliday;
                        holiday.TimeType = (data.TimeType == 2 ? "ประจำปี" : "ประจำวัน");

                        count = count + 1;
                        if (data.sHolidayType == "0")
                            holiday.type = "วันหยุด";
                        else if (data.sHolidayType == "1")
                            holiday.type = "วันหยุด";
                        else if (data.sHolidayType == "3")
                            holiday.type = "กิจกรรม";
                        else if (data.sHolidayType == "4")
                            holiday.type = "กิจกรรม";

                        if (data.sColor == null && (data.sHolidayType == "0" || data.sHolidayType == "1"))
                            holiday.color = "0";
                        if (data.sColor == null && (data.sHolidayType == "3" || data.sHolidayType == "4"))
                            holiday.color = "1";

                        if (data.sWhoSeeThis == "0" || data.sWhoSeeThis == null)
                            holiday.whoSee = "ทุกคน";
                        else if (data.sWhoSeeThis == "1")
                            holiday.whoSee = "อาจารย์/พนักงาน";
                        else if (data.sWhoSeeThis == "2")
                            holiday.whoSee = "นักเรียน";
                        else if (data.sWhoSeeThis == "3")
                            holiday.whoSee = "บางห้องเรียน";
                        else if (data.sWhoSeeThis == "4")
                            holiday.whoSee = "บางระดับชั้น";
                        else if (data.sWhoSeeThis == "5")
                            holiday.whoSee = "ตามประเภทบุคลากร";
                        holiday.nHoliday = data.nHoliday;
                        holiday.SchoolID = userData.CompanyID;
                        holiday.created = data.CreatedDate?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        holiday.modify = data.UpdatedDate?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        holidaylist.Add(holiday);
                    }
                }

                List<holidaylist> SortedList = holidaylist.OrderBy(o => o.sort).ToList();

                int counter = 1;
                foreach (var b in SortedList)
                {
                    b.number = counter;
                    counter = counter + 1;
                }

                return SortedList;

            }
        }

        protected class classValue
        {
            public int nTSubLevel { get; set; }
            public int? value { get; set; }
        }

        protected class holidaylist
        {
            public DateTime? sort { get; set; }
            public int number { get; set; }
            public string TimeType { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string name { get; set; }
            public string whoSee { get; set; }
            public string color { get; set; }
            public string type { get; set; }
            public string nHoliday { get; set; }
            public int SchoolID { get; set; }

            public string modify { get; set; }
            public string created { get; internal set; }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var selectedYear = Convert.ToInt32(ddlYearCopy.SelectedValue);


            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                //var listDate = CalendarHelper.GetDefaultHoliday(selectedYear);

                var sl = new StudentLogic(db);
                var currentTerm = sl.GetTermId(DateTime.Now.Date, userData);

                var fromYaer = (from a in db.TTerms.Where(o => o.nTerm == currentTerm && o.SchoolID == userData.CompanyID)
                                from b in db.TYears.Where(o => o.SchoolID == a.SchoolID && o.nYear == a.nYear)
                                select b.numberYear - 543
                                ).FirstOrDefault();

                var d1 = new DateTime(fromYaer.Value, 1, 1);
                var d2 = new DateTime(fromYaer.Value, 12, 31);

                var listDate = db.THolidays
                    .Where(o => o.SchoolID == userData.CompanyID && o.cDel != "1"
                        && (o.dHolidayStart.Value.Year == fromYaer || o.dHolidayEnd.Value.Year == fromYaer)
                        && (d1 <= o.dHolidayEnd || o.dHolidayStart <= d2))
                    .ToList();

                var holidayList = db.THolidays
                    .Where(o => o.SchoolID == userData.CompanyID && o.cDel != "1"
                        && (o.dHolidayStart.Value.Year == selectedYear || o.dHolidayEnd.Value.Year == selectedYear))
                    .ToList();
                int countRecord = db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Count();

                foreach (var d in listDate)
                {
                    var isExist = holidayList.Where(o => o.dHolidayStart == d.dHolidayStart && o.dHolidayEnd == d.dHolidayEnd).Count() > 0;

                    if (!isExist)
                    {
                        var now = DateTime.Now;
                        now = now.AddMilliseconds(-now.Millisecond + 666);

                        string holidayID = string.Format(@"{0:#000}{1:000000}", userData.CompanyID, ++countRecord);

                        var diff1 = Math.Abs(selectedYear - d.dHolidayStart.Value.Year);
                        var diff2 = Math.Abs(selectedYear - d.dHolidayEnd.Value.Year);


                        if (d.dHolidayStart.Value.Year < d.dHolidayEnd.Value.Year)
                        {
                            var v = (d.dHolidayEnd.Value.Year - d.dHolidayStart.Value.Year); ;
                            if (fromYaer == d.dHolidayEnd.Value.Year)
                            {
                                diff1 = diff1 - v;
                            }
                            else
                            {
                                diff2 = diff2 + v;
                            }
                            //diff2 = diff2 - (d.dHolidayEnd.Value.Year - d.dHolidayStart.Value.Year);
                        }

                        var start = d.dHolidayStart.Value.AddYears(diff1);
                        var end = d.dHolidayEnd.Value.AddYears(diff2);
                        //var start = new DateTime(selectedYear, d.dHolidayStart.Value.Month, d.dHolidayStart.Value.Day);
                        //var end = new DateTime(selectedYear, d.dHolidayEnd.Value.Month, d.dHolidayEnd.Value.Day);

                        var h = new THoliday()
                        {
                            nHoliday = holidayID,
                            sHoliday = d.sHoliday,
                            sHolidayType = d.sHolidayType,
                            dHolidayStart = start,
                            dHolidayEnd = end,
                            sHolidayAll = d.sHolidayAll,
                            sWhoSeeThis = d.sWhoSeeThis,
                            sColor = d.sColor,
                            cStatusActive = d.cStatusActive,
                            SchoolID = userData.CompanyID,
                            CreatedBy = userData.UserID,
                            CreatedDate = now,
                            UpdatedBy = userData.UserID,
                            UpdatedDate = now,
                        };

                        db.THolidays.Add(h);

                        var holidaySome = db.THolidaySomes
                            .Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == d.nHoliday && w.cDel != true && w.Deleted != 1)
                            .ToList();

                        int runID = 1;

                        foreach (var some in holidaySome)
                        {
                            string holidaySomeID = string.Format(@"{0}{1:000}", holidayID, runID++);
                            var s = new THolidaySome()
                            {
                                nHolidaySomeID = holidaySomeID,
                                nHoliday = holidayID,
                                nTSubLevel = some.nTSubLevel,

                                SchoolID = userData.CompanyID,
                                CreatedBy = userData.UserID,
                                CreatedDate = now,
                                UpdatedBy = userData.UserID,
                                UpdatedDate = now,
                                cDel = some.cDel,
                                Deleted = some.Deleted,
                            };
                            db.THolidaySomes.Add(s);
                        }


                    }
                }

                var log = new THolidayLog();
                log.SchoolID = userData.CompanyID;
                log.Created = DateTime.Now;
                log.Creator = userData.UserID;
                log.Detail = $"คัดลอกปฏิทิน จากปี {fromYaer + 543} ไปยังปี {selectedYear + 543}";
                db.THolidayLog.Add(log);

                db.SaveChanges();

                Response.Redirect("holidaysettings.aspx");
            }
        }


    }
}