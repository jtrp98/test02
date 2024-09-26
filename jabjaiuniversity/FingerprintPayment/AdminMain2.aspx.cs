using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections.Specialized;
using System.Web.Services;
using JabjaiEntity.DB;
using MasterEntity;
using System.Web.Script.Serialization;
using FingerprintPayment.App_Code;
using JabjaiMainClass;
using JabjaiMainClass.Models;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using Amazon.XRay.Recorder.Handlers.AspNet;
using FingerprintPayment.Class;
using System.Globalization;
using ExcelDataReader.Log;

namespace FingerprintPayment
{
    public partial class AdminMain2 : System.Web.UI.Page
    {

        [WebMethod]
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEmpID"];
        }

        public static TB_Server tb_Server = new TB_Server();

        [WebMethod(EnableSession = true)]
        public static string GetEvents()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                List<calendarEvent> eventList = new List<calendarEvent>();
                calendarEvent aa = new calendarEvent();

                foreach (var holiday in _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel != "1"))
                {
                    aa = new calendarEvent();
                    aa.Subject = holiday.sHoliday;
                    string sMonth = holiday.dHolidayStart.Value.Month.ToString();
                    string sDate = holiday.dHolidayStart.Value.Day.ToString();
                    string sYear = holiday.dHolidayStart.Value.Year.ToString();

                    string eMonth = holiday.dHolidayEnd.Value.Month.ToString();
                    string eDate = holiday.dHolidayEnd.Value.Day.ToString();
                    string eYear = holiday.dHolidayEnd.Value.Year.ToString();

                    aa.txtStart = sDate + " " + ChangeTxtMonth(sMonth) + " " + sYear;
                    aa.txtEnd = eDate + " " + ChangeTxtMonth(eMonth) + " " + eYear;
                    aa.start = holiday.dHolidayStart;
                    aa.end = holiday.dHolidayEnd.Value.AddDays(1);
                    if (holiday.sHolidayType == "0")
                    {
                        aa.Description = "วันหยุดประจำปี";
                    }
                    else if (holiday.sHolidayType == "1")
                    {
                        aa.Description = "วันหยุดพิเศษ";
                    }
                    else if (holiday.sHolidayType == "3")
                    {
                        aa.Description = "วันกิจกรรมประจำปี";
                    }
                    else
                    {
                        aa.Description = "วันกิจกรรมพิเศษ";
                    }
                    eventList.Add(aa);
                }

                JavaScriptSerializer jss = new JavaScriptSerializer();
                String json = jss.Serialize(eventList);
                return json;
            }
        }

        public List<studentStatus> StudentStatus = new List<studentStatus>();
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            //SetBeginSegment(recorder);

            tb_Server = ServeData.B_Server;
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!IsPostBack)
            {
                //SetBodyEventOnLoad(@"createCookie('sEntities','" + Session["sEntities"].ToString() + "', 10000);");
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string entities = Session["sEntities"].ToString();
                    //AWSXRay xray = new AWSXRay();
                    ////xray.Init();

                    //AWSXRayRecorder recorder = xray.Register();
                    //recorder.AddAnnotation("Method", "ADMIN PAGE");
                    //recorder.AddAnnotation("Entities", entities);

                    //HttpContext context = HttpContext.Current;
                    //if (context != null && context.Items.Contains(AWSXRayASPNET.XRayEntity))
                    //{
                    //    Segment requestSegment = (Segment)context.Items[AWSXRayASPNET.XRayEntity];
                    //    recorder.SetEntity(requestSegment);
                    //    //SetBeginSegment(recorder);
                    //    recorder.AddAnnotation("Method", "ADMIN PAGE");
                    //    recorder.AddAnnotation("Entities", entities);
                    //    Response.Write("AWSXRayASPNET");
                    //}
                    //else
                    //{
                    //    //SetBeginSegment(recorder);
                    //    //recorder.AddAnnotation("Method", "ADMIN PAGE");
                    //    //recorder.AddAnnotation("Entities", entities);
                    //    //Response.Redirect("~/logout.aspx");
                    //}

                    // List Message System
                    var list = dbmaster.TMessageSystems.OrderByDescending(o => o.AddDate).Take(10).ToList();
                    if (list.Count > 0)
                    {
                        foreach (var l in list)
                        {
                            string timeAgo = ComFunction.GetTimeSince(l.AddDate.Value);
                            this.ltrMessageSystem.Text += string.Format(@"
                        <div class=""dropdown-item d-flex align-items-center"" href=""#"" data-id=""{0}"">
                            <div class=""dropdown-list-image mr-3"">
                                <img class=""rounded-circle"" src=""images/School Bright logo only.png"" style=""width: 48px;"" alt="""">
                            </div>
                            <div class=""font-weight-bold"" style=""width: 100%; padding-left: 60px;"">
                                <div style=""float: right; font-size: 14px; margin: 0px -8px 0px 0px;"">{1}</div>
                                <div class=""text-truncate"">{2}</div>
                                <div class=""small text-gray-500 word-wrap"">{3}</div>
                            </div>
                        </div>
                        <hr style=""margin-top: 10px; margin-bottom: 10px;""/>", l.ID, l.AddDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), l.Title, l.Message);
                        }
                    }
                    else
                    {
                        this.ltrMessageSystem.Text = "<center>ไม่พบข่าวสารประกาศจากโรงเรียน</center>";
                    }

                    using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(entities,ConnectionDB.Read)))
                    {
                        var tSchool = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);
                        var f_usermaster = dbmaster.TUsers.Where(w => w.nCompany == tSchool.nCompany).Select(s => new
                        {
                            s.nSystemID,
                            s.cType
                        }).ToList();

                        var l_userType_1 = f_usermaster.Where(w1 => w1.cType == "1").Select(s => s.nSystemID).ToList();
                        var l_userType_0 = f_usermaster.Where(w1 => w1.cType == "0").Select(s => s.nSystemID).ToList();

                        status02.InnerHtml = _db.TEmployees.Where(w => w.SchoolID == tSchool.nCompany).Where(w => (w.cDel ?? "0") != "1" && l_userType_1.Contains(w.sEmp)).Count().ToString();

                        StudentLogic studentLogic = new StudentLogic(_db);
                        string TermId = studentLogic.GetTermId(userData);
                        StudentStatus = (from a in _db.TB_StudentViews.Where(w => w.SchoolID == tSchool.nCompany && (w.cDel ?? "0") == "0" && l_userType_0.Contains(w.sID) && TermId.Trim() == w.nTerm.Trim() && (w.moveInDate ?? DateTime.Today) <= DateTime.Today)
                                         group a by a.nStudentStatus into gb
                                         select new studentStatus
                                         {
                                             Count = gb.Count(),
                                             Status = gb.Key ?? 0
                                         }).AsQueryable().ToList();

                        status01.InnerHtml = StudentStatus.Where(w => w.Status == 0).Sum(s => s.Count) + "";

                        if (tSchool != null)
                        {
                            ltrSchoolName.Text = tSchool.sCompany;
                        }
                    }

                    SchoolOverView(userData);
                }
            }
        }

        private void SchoolOverView(JWTToken.userData userData)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                StudentLogic logic = new StudentLogic(_db);
                string TermId = logic.GetTermId(userData);

                var listQuery = (from y in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                                 join t in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on y.nYear equals t.nYear
                                 where t.nTerm == TermId
                                 select new
                                 {
                                     y.numberYear,
                                     t.sTerm,
                                     t.dStart,
                                     t.dEnd,
                                     y.YearStatus,
                                     y.nYear
                                 }).FirstOrDefault();

                var dayStart = listQuery.dStart.Value.Day.ToString();
                var monthStart = listQuery.dStart.Value.Month.ToString();
                var yearStart = Int32.Parse(listQuery.dStart.Value.Year.ToString()) + 543;

                var dayEnd = listQuery.dEnd.Value.Day.ToString();
                var monthEnd = listQuery.dEnd.Value.Month.ToString();
                var yearEnd = Int32.Parse(listQuery.dEnd.Value.Year.ToString()) + 543;

                schoolYear.Text = listQuery.numberYear.ToString();
                schoolTerm.Text = listQuery.sTerm;
                dateTermStart.Text = dayStart + " " + ChangeShortMonth(monthStart) + " " + yearStart.ToString();
                dateTermEnd.Text = dayEnd + " " + ChangeShortMonth(monthEnd) + " " + yearEnd.ToString();

                var behaviorQuery = (from b in _db.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID)
                                     select b.MaxScore).FirstOrDefault();

                var maxBehavior = behaviorQuery.ToString();
                txtMaxBehavior.Text = maxBehavior;

                var q_year = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.nYear == listQuery.nYear).ToList();

                var termStart = q_year.Select(s => s.dStart).Min();
                var termEnd = q_year.Select(s => s.dEnd).Max();

                var setHoliday = (from h in _db.THolidays.Where(w => w.SchoolID == userData.CompanyID)
                                  select h)
                              .Where(h => h.sHolidayType == "0" || h.sHolidayType == "1")
                              .Where(h => h.sWhoSeeThis == "0")
                              .Where(h => h.cDel == null)
                              .Where(h => h.dHolidayStart >= termStart & h.dHolidayEnd <= termEnd).ToList();

                var countHoliday = 0;
                var dayDiff = 0;


                for (var i = 0; i < setHoliday.Count(); i++)
                {
                    DateTime hStartDate = setHoliday[i].dHolidayStart.Value;
                    DateTime hEndDate = setHoliday[i].dHolidayEnd.Value;

                    dayDiff = (hEndDate - hStartDate).Days + 1;
                    countHoliday = countHoliday + dayDiff;
                }



                countAllHoliday.Text = countHoliday.ToString();
            }

        }

        private static void SetBeginSegment(AWSXRayRecorder recorder)
        {
            string url = HttpContext.Current.Request.Url.Host;

            var traceId = TraceId.NewId();
            recorder.BeginSegment(url, traceId);
        }

        #region 

        //1	ข้อมูลสินค้า
        //2	นำสินค้าเข้าสต๊อก
        //3	เติมเงินเข้าระบบ
        //4	ขายสินค้า
        //5	ขายสินค้าเงินสด
        //6	แก้ไขการขายที่ผิด
        //    รายงาน

        //    รายงานการซื้อ-ขาย


        //    รายงานขายรวบยอด

        //    รายงานการขายเงินสด
        //    รายงานการการเต็มเงิน

        //11	ข้อมูลเวลาเข้า-ออกโรงเรียน
        //12	ข้อมูลระดับชั้น
        //13	ข้อมูลวันหยุด
        //14	รายงาน
        //15	ข้อมูล SMS

        //8	รายงานการเข้าใช้ระบบ
        //9	ข้อมูลพนักงาน
        //10	ข้อมูลนักเรียน
        //    รายงานข้อมูลนักเรียน
        #endregion

        public string ChangeShortMonth(string txt)
        {
            string shortMonth;
            if (txt == "1")
                shortMonth = "ม.ค.";
            else if (txt == "2")
                shortMonth = "ก.พ.";
            else if (txt == "3")
                shortMonth = "มี.ค.";
            else if (txt == "4")
                shortMonth = "เม.ย.";
            else if (txt == "5")
                shortMonth = "พ.ค.";
            else if (txt == "6")
                shortMonth = "มิ.ย.";
            else if (txt == "7")
                shortMonth = "ก.ค.";
            else if (txt == "8")
                shortMonth = "ส.ค.";
            else if (txt == "9")
                shortMonth = "ก.ย.";
            else if (txt == "10")
                shortMonth = "ต.ค.";
            else if (txt == "11")
                shortMonth = "พ.ย.";
            else if (txt == "12")
                shortMonth = "ธ.ค.";
            else shortMonth = "##";
            return shortMonth;
        }

        protected static String ChangeTxtMonth(string txt)
        {
            if (txt == "1")
                return "มกราคม";
            if (txt == "2")
                return "กุมภาพันธ์";
            if (txt == "3")
                return "มีนาคม";
            if (txt == "4")
                return "เมษายน";
            if (txt == "5")
                return "พฤษภาคม";
            if (txt == "6")
                return "มิถุนายน";
            if (txt == "7")
                return "กรกฎาคม";
            if (txt == "8")
                return "สิงหาคม";
            if (txt == "9")
                return "กันยายน";
            if (txt == "10")
                return "ตุลาคม";
            if (txt == "11")
                return "พฤศจิกายน";
            return "ธันวาคม";
        }

        public List<BehaviorsTopScore> topScores()
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }

            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            {
                var f_school = masterEntities.TCompanies.FirstOrDefault(f => f.sEntities == token.entities);
                using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(f_school, ConnectionDB.Read)))
                {
                    StudentLogic logic = new StudentLogic(entities);
                    var TermId = logic.GetTermId(new JWTToken.userData { CompanyID = f_school.nCompany });
                    var f_BehaviorSettings = entities.TBehaviorSettings.Where(w => w.SchoolID == f_school.nCompany).FirstOrDefault();
                    DateTime? dateStart, dateEnd;
                    var f_term = entities.TTerms.FirstOrDefault(f => f.nTerm.Trim() == TermId.Trim());
                    if (f_BehaviorSettings.Type == 1)
                    {
                        dateStart = f_term.dStart;
                        dateEnd = f_term.dEnd;
                    }
                    else
                    {
                        var q_term = entities.TTerms.Where(w => w.SchoolID == f_school.nCompany && w.nYear == f_term.nYear && w.cDel == null).ToList();
                        dateStart = q_term.Min(min => min.dStart) ?? DateTime.Today;
                        dateEnd = q_term.Where(w => w.nYear == f_term.nYear && w.cDel == null && w.SchoolID == f_school.nCompany).Max(max => max.dEnd) ?? DateTime.Today;
                    }

                    //var q_sum = (from a in entities.TBehaviorHistories.Where(w => w.SchoolID == f_school.nCompany && w.cDel == false)
                    //             where dateStart <= a.dAdd && dateEnd >= a.dAdd
                    //             && a.dCanCel == null
                    //             group a by a.StudentId into gb
                    //             select new
                    //             {
                    //                 StudentId = gb.Key,
                    //                 Score = gb.Min(s => s.ResidualScore)
                    //             }).ToList();

                    var f_setting = entities.TBehaviorSettings.Where(w => w.SchoolID == f_school.nCompany).FirstOrDefault();
                    int ShowMinusSign = f_setting == null ? 1 : ((f_setting.ShowMinusSign ?? 0) == 1 ? 1 : -1);

                    string SQL = string.Format(@"DECLARE @DAYSTART DATETIME = '{0:yyyyMMdd} 00:00:00.000'
DECLARE @DAYEND DATETIME = '{1:yyyyMMdd} 23:59:59.000'
DECLARE @nTerm VARCHAR(25) = '{2}'
DECLARE @ShowMinusSign INT = '{3}'
DECLARE @SchoolID INT = '{4}'

SELECT TOP 10 B1.ResidualScore * @ShowMinusSign AS Score,U1.sName + ' ' + U1.sLastname AS _name,U1.SubLevel + ' / ' + U1.nTSubLevel2 AS _class ,U1.nTerm
FROM TBehaviorHistory AS B1
INNER JOIN 
(
	SELECT StudentId,MAX(BehaviorHistoryId) AS BehaviorHistoryId
	FROM TBehaviorHistory
	WHERE SchoolID = @SchoolID AND ISNULL(cDel,0) = 0
	AND dAdd BETWEEN @DAYSTART AND @DAYEND
	AND dCanCel IS NULL
	GROUP BY StudentId
) AS B2
ON B1.StudentId = B2.StudentId AND B1.BehaviorHistoryId = B2.BehaviorHistoryId
INNER JOIN TB_StudentViews AS U1
ON U1.sID = B1.StudentId AND U1.SchoolID = B1.SchoolID AND ISNULL(U1.nStudentStatus,0) NOT IN (2)
WHERE U1.nTerm = @nTerm
ORDER BY ResidualScore", dateStart, dateEnd, f_term.nTerm, ShowMinusSign, f_school.nCompany);

                    var q = entities.Database.SqlQuery<BehaviorsTopScore>(SQL).ToList();


                    return q;
                }
            }
        }

        protected class calendarEvent
        {
            public int EventId { get; set; }
            public String Subject { get; set; }
            public String Description { get; set; }
            public DateTime? start { get; set; }
            public DateTime? end { get; set; }
            public String ThemeColor { get; set; }
            public String IsFullDay { get; set; }
            public String txtStart { get; set; }
            public String txtEnd { get; set; }
        }

        public static List<Report02User> report02UsersView01()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {

                LogScanCome2SchoolLogic studentLogic = new LogScanCome2SchoolLogic(db, userData);
                List<LogScanCome2SchoolModel> studentModel = new List<LogScanCome2SchoolModel>();
                studentModel = studentLogic.GetAll(DateTime.Today, DateTime.Today);

                var q = (from a in studentModel.Where(w => (w.Student_Status ?? 0) == 0)
                         group a by new { a.DayScan } into gb

                         select new Report02User
                         {
                             day = gb.Key.DayScan.Value.ToString("dd/MM/yyyy"),
                             studentnumber = studentModel.Count(c => c.DayScan == gb.Key.DayScan),
                             female_status_0 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "0"),
                             female_status_1 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "12"),
                             female_status_2 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "1"),
                             female_status_3 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "11"),
                             female_status_4 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "10"),
                             female_status_5 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "3"),
                             female_status_6 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "99"),
                             male_status_0 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "0"),
                             male_status_1 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "12"),
                             male_status_2 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "1"),
                             male_status_3 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "11"),
                             male_status_4 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "10"),
                             male_status_5 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "3"),
                             male_status_6 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "99"),
                         }).ToList();

                return q;
            }
        }

        public class studentStatus
        {
            public int Status { get; set; }
            public int Count { get; set; }
        }

        public class BehaviorsTopScore
        {
            public string _name { get; set; }
            public string _class { get; set; }
            public decimal? Score { get; set; }
        }
    }
}