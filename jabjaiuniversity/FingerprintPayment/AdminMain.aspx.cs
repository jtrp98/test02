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
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace FingerprintPayment
{
    public partial class AdminMain : System.Web.UI.Page
    {
        protected AdminMainModel AdminMainData = new AdminMainModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

                if (!IsPostBack)
                {
                    int schoolID = userData.CompanyID;

                    using (JabJaiMasterEntities em = Connection.MasterEntities(ConnectionDB.Read))
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                    {
                        var schoolObj = em.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                        if (schoolObj != null)
                        {
                            AdminMainData.SchoolName = schoolObj.sCompany;
                        }

                        StudentLogic studentLogic = new StudentLogic(en);
                        string currentTermID = studentLogic.GetTermId(userData);
                        TTerm termObj = en.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                        TYear yearObj = null;
                        if (termObj != null)
                        {
                            yearObj = en.TYears.Where(w => w.nYear == termObj.nYear).FirstOrDefault();
                        }

                        // จำนวนนักเรียน & สถิตินักเรียนทั้งหมด
                        string query = string.Format(@"
SELECT ISNULL(MAX(CASE WHEN A.StudentStatus=0 OR A.StudentStatus=1 OR A.StudentStatus=2 THEN A.StudentStatusCount ELSE 0 END), 0) 'StudentAmount'
, ISNULL(MAX(CASE WHEN A.StudentStatus=0 THEN A.StudentStatusCount ELSE 0 END), 0) 'StudentStatus0'
, ISNULL(MAX(CASE WHEN A.StudentStatus=1 THEN A.StudentStatusCount ELSE 0 END), 0) 'StudentStatus1'
, ISNULL(MAX(CASE WHEN A.StudentStatus=2 THEN A.StudentStatusCount ELSE 0 END), 0) 'StudentStatus2'
, ISNULL(MAX(CASE WHEN A.StudentStatus=3 THEN A.StudentStatusCount ELSE 0 END), 0) 'StudentStatus3'
, ISNULL(MAX(CASE WHEN A.StudentStatus=4 THEN A.StudentStatusCount ELSE 0 END), 0) 'StudentStatus4'
, ISNULL(MAX(CASE WHEN A.StudentStatus=5 THEN A.StudentStatusCount ELSE 0 END), 0) 'StudentStatus5'
, ISNULL(MAX(CASE WHEN A.StudentStatus=6 THEN A.StudentStatusCount ELSE 0 END), 0) 'StudentStatus6'
FROM
(
	SELECT ISNULL(nStudentStatus, 0) 'StudentStatus', COUNT(*) 'StudentStatusCount'
	FROM TB_StudentViews 
	WHERE SchoolID={0} AND ISNULL(cDel, '0')='0' AND nTerm='{1}' 
	AND (((ISNULL(nStudentStatus, 0)=0 OR nStudentStatus=4) AND ISNULL(moveInDate, CONVERT(DATE, GETDATE())) <= CONVERT(DATE, GETDATE())) 
	OR ((nStudentStatus=1 OR nStudentStatus=2) AND CONVERT(DATE, GETDATE()) >= ISNULL(MoveOutDate, CONVERT(DATE, GETDATE())))
	OR ISNULL(nStudentStatus, 0) IN (3, 5, 6))
	AND sID IN (SELECT sID FROM JabjaiMasterSingleDB.dbo.TUser WHERE nCompany={0} AND cType = '0')
	GROUP BY ISNULL(nStudentStatus, 0)
) A", schoolID, currentTermID);
                        AdminMainData.StudentStatusData = en.Database.SqlQuery<StudentStatusModel>(query).FirstOrDefault();

                        // จำนวนบุคลากร
                        query = string.Format(@"
SELECT COUNT(*) 
FROM TEmployees e 
LEFT JOIN TEmpSalary es ON e.SchoolID=es.SchoolID AND e.sEmp=es.sEmp
WHERE e.SchoolID={0} AND ISNULL(e.cDel, '0')='0' AND e.sEmp IN (SELECT sID FROM JabjaiMasterSingleDB.dbo.TUser WHERE nCompany={0} AND cType = '1')
AND ((ISNULL(es.WorkStatus, 1)=1 AND CONVERT(DATE, GETDATE()) >= ISNULL(es.WorkStartDate, CONVERT(DATE, GETDATE()))) 
OR ((es.WorkStatus=2 OR es.WorkStatus=3) AND CONVERT(DATE, GETDATE()) < ISNULL(es.DayQuit, CONVERT(DATE, GETDATE()))))", schoolID);
                        AdminMainData.EmployeeAmount = en.Database.SqlQuery<int>(query).FirstOrDefault();

                        // เครดิตบูโร
                        AdminMainData.SchoolCreditBureau = "A+";

                        // ภาพรวมระบบ
                        AdminMainData.SummaryTime = "9:30";
                        AdminMainData.ReportTime = "10:30";

                        if (yearObj != null)
                        {
                            AdminMainData.AcademicYear = yearObj.numberYear;
                        }

                        AdminMainData.HolidayAmount = 0;
                        if (termObj != null)
                        {
                            AdminMainData.Semester = termObj.sTerm;
                            AdminMainData.SemesterStartDate = termObj.dStart;
                            AdminMainData.SemesterEndDate = termObj.dEnd;

                            var holidays = en.THolidays.Where(w => w.SchoolID == schoolID && (w.sHolidayType == "0" || w.sHolidayType == "1") && w.sWhoSeeThis == "0" && w.cDel == null && w.cDel == null && (w.dHolidayStart >= termObj.dStart && w.dHolidayEnd <= termObj.dEnd)).ToList();
                            for (var i = 0; i < holidays.Count(); i++)
                            {
                                DateTime hStartDate = holidays[i].dHolidayStart.Value;
                                DateTime hEndDate = holidays[i].dHolidayEnd.Value;

                                var dayDiff = (hEndDate - hStartDate).Days + 1;
                                AdminMainData.HolidayAmount += dayDiff;
                            }

                            // ตารางการจัดอันดับคะแนนความประพฤติ
                            // โหลดเมื่อกดปุ่ม Refresh

                        }

                        AdminMainData.MaxScoreBehavior = en.TBehaviorSettings.Where(w => w.SchoolID == schoolID).Select(s => s.MaxScore ?? 0).FirstOrDefault();

                        // ปฏิทิน
                        // HolidayType = 0 : วันหยุดประจำปี [event-azure], 1 : วันหยุดพิเศษ [event-rose], 3 : วันกิจกรรมประจำปี [event-orange], default : วันกิจกรรมพิเศษ [event-default]
                        List<CalendarEventModel> calendarEvents = en.THolidays.Where(w => w.SchoolID == schoolID).Where(w => w.cDel != "1")
                            .Select(s => new CalendarEventModel { ID = s.nHoliday, Title = s.sHoliday, Start = s.dHolidayStart, End = s.dHolidayEnd, AllDay = false, ClassName = (s.sHolidayType == "0" ? "event-azure" : (s.sHolidayType == "1" ? "event-rose" : (s.sHolidayType == "3" ? "event-orange" : "event-default"))), HolidayType = (s.sHolidayType == "0" ? "วันหยุดประจำปี" : (s.sHolidayType == "1" ? "วันหยุดพิเศษ" : (s.sHolidayType == "3" ? "วันกิจกรรมประจำปี" : "วันกิจกรรมพิเศษ"))) })
                            .ToList();

                        // Prepare data
                        foreach (var c in calendarEvents)
                        {
                            c.End = c.End.Value.AddDays(1).AddMinutes(-1);

                            c.StartText = c.Start.Value.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                            c.EndText = c.End.Value.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                        }

                        if (calendarEvents.Count == 0)
                        {
                            AdminMainData.EventJsonData = "''";
                        }
                        else
                        {
                            AdminMainData.EventJsonData = JsonConvert.SerializeObject(calendarEvents);
                        }


                        // ข่าวสารจากระบบ
                        // AdminMainData.MessageSystems = em.TMessageSystems.OrderByDescending(o => o.AddDate).Take(10).ToList();

                        var qry = "";

                        if (userData.IsAdmin)
                        {
                            qry = $@" SELECT TOP 10 A.NewsID, A.Created, A.Title, A.NoteAppWeb 'Detail'
                                    FROM JabjaiMasterSingleDB.dbo.TNews2  A                          
                                    WHERE IsNoteAppWeb = 1                           
                                    AND (A.SchoolID IS NULL OR A.SchoolID = {schoolID} )
                                    --AND GETDATE() BETWEEN StartDate AND EndDate 
                                    AND A.IsDelete = 0 
                            ";
                        }
                        else
                        {
                            qry = $@" SELECT TOP 10 A.NewsID, A.Created, A.Title, A.NoteAppWeb 'Detail'
                                    FROM JabjaiMasterSingleDB.dbo.TNews2  A
                                    JOIN JabjaiMasterSingleDB.dbo.TNewsPushNotify  B ON A.SchoolID = B.SchoolID AND A.NewsID = B.NewsID
                                    WHERE IsNoteAppWeb = 1 
                                    AND B.UserID = {userData.UserID}
                                    AND (A.SchoolID IS NULL OR A.SchoolID = {schoolID} )
                                    --AND GETDATE() BETWEEN StartDate AND EndDate 
                                    AND A.IsDelete = 0 ";
                        }

                        var invliceSql = "";
                        var hasPermission = HasInvoicePermission(userData.CompanyID, userData.UserID);
                        if (hasPermission)//แสดงใบแจ้งหนี้และใบเสร็จให้กับ user ที่มีสิทธิ์
                        {
                            invliceSql = $@"
                            UNION
                            SELECT TOP 1 NewsID, Created, Title, Detail FROM TNews WHERE remark in('RemindInvoice') AND ISNULL(ISDone,0)=0 and SchoolId = {schoolID} AND GETDATE() BETWEEN StartDate AND EndDate
                            UNION
                            SELECT TOP 1 NewsID, Created, Title, Detail FROM TNews WHERE remark in('RemindReceipt') AND ISNULL(ISDone,0)=1 and SchoolId = {schoolID} AND GETDATE() BETWEEN StartDate AND EndDate";
                        }

                        query = $@"
                        SELECT * FROM 
                        (                            
                            {qry}

                            UNION 

                            SELECT TOP 10 NewsID, Created, Title, Detail FROM TNews WHERE IsBroadcast = 1 --ORDER BY Created DESC   

                            UNION

                            SELECT TOP 10 NewsID, Created, Title, Detail FROM TNews WHERE remark in ('RemindBillSetting','RemindPayment','RemindSchoolInActive') and ISNULL(ISDone,0)=0 and SchoolId = {schoolID} AND GETDATE() BETWEEN StartDate AND EndDate

                            {invliceSql}
                        ) as tmp ORDER BY tmp.Created DESC ";


                        AdminMainData.Broadcasts = em.Database.SqlQuery<BroadcastModel>(query).ToList();

                        foreach (var b in AdminMainData.Broadcasts)
                        {
                            // Replace hyperlink
                            b.Detail = Regex.Replace(b.Detail, @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)", "<a target='_blank' href='$1'>$1</a>");
                        }

                        // สถิติการสแกนเข้า-ออกโรงเรียน
                        // 0 : เข้าตรงเวลา, 12 : กิจกรรม, 1 : สาย, 11 : ลาป่วย, 10 : ลากิจ, 3 : ขาด, 99 : ไม่ได้เช็คชื่อ
                        // โหลดเมื่อกดปุ่ม Refresh
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod(EnableSession = true)]
        public static string LoadStudentDayScanData()
        {
            bool success = true;
            string message = "Save Successfully";
            string data = string.Format(@"{{""labels"":[""0%"", ""0%"", ""0%"", ""0%"", ""0%"", ""0%"", ""0%""], ""series"":[0, 0, 0, 0, 0, 0, 0]}}");

            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                int schoolID = userData.CompanyID;

                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    // Student scan
                    LogScanCome2SchoolLogic logScanCome2SchoolLogic = new LogScanCome2SchoolLogic(en, userData);
                    List<LogScanCome2SchoolModel> logScanCome2Schools = new List<LogScanCome2SchoolModel>();
                    logScanCome2Schools = logScanCome2SchoolLogic.GetAll(DateTime.Today, DateTime.Today);

                    var studentDayScanData = (from a in logScanCome2Schools.Where(w => (w.Student_Status ?? 0) == 0)
                                              group a by new { a.DayScan } into gb
                                              select new DayScanModel
                                              {
                                                  Day = gb.Key.DayScan.Value.ToString("dd/MM/yyyy"),
                                                  StudentAmount = logScanCome2Schools.Count(c => c.DayScan == gb.Key.DayScan),
                                                  ScanStatus0 = gb.Count(s => s.ScanStatus == "0"),
                                                  ScanStatus12 = gb.Count(s => s.ScanStatus == "12"),
                                                  ScanStatus1 = gb.Count(s => s.ScanStatus == "1"),
                                                  ScanStatus11 = gb.Count(s => s.ScanStatus == "11"),
                                                  ScanStatus10 = gb.Count(s => s.ScanStatus == "10"),
                                                  ScanStatus3 = gb.Count(s => s.ScanStatus == "3"),
                                                  ScanStatus99 = gb.Count(s => s.ScanStatus == "99"),
                                              }).FirstOrDefault();

                    if (studentDayScanData != null)
                    {
                        var StudentAmount = studentDayScanData.StudentAmount == 0 ? 1 : studentDayScanData.StudentAmount;
                        data = string.Format(@"{{""labels"":[""{0}"", ""{1}"", ""{2}"", ""{3}"", ""{4}"", ""{5}"", ""{6}""], ""series"":[{7}, {8}, {9}, {10}, {11}, {12}, {13}]}}"
        , ((studentDayScanData.ScanStatus0 * 100) / StudentAmount) < 1 ? " " : ((studentDayScanData.ScanStatus0 * 100) / StudentAmount) + "%"
        , ((studentDayScanData.ScanStatus12 * 100) / StudentAmount) < 1 ? " " : ((studentDayScanData.ScanStatus12 * 100) / StudentAmount) + "%"
        , ((studentDayScanData.ScanStatus1 * 100) / StudentAmount) < 1 ? " " : ((studentDayScanData.ScanStatus1 * 100) / StudentAmount) + "%"
        , ((studentDayScanData.ScanStatus11 * 100) / StudentAmount) < 1 ? " " : ((studentDayScanData.ScanStatus11 * 100) / StudentAmount) + "%"
        , ((studentDayScanData.ScanStatus10 * 100) / StudentAmount) < 1 ? " " : ((studentDayScanData.ScanStatus10 * 100) / StudentAmount) + "%"
        , ((studentDayScanData.ScanStatus3 * 100) / StudentAmount) < 1 ? " " : ((studentDayScanData.ScanStatus3 * 100) / StudentAmount) + "%"
        , ((studentDayScanData.ScanStatus99 * 100) / StudentAmount) < 1 ? " " : ((studentDayScanData.ScanStatus99 * 100) / StudentAmount) + "%"
        , ((studentDayScanData.ScanStatus0 * 100) / StudentAmount)
        , ((studentDayScanData.ScanStatus12 * 100) / StudentAmount)
        , ((studentDayScanData.ScanStatus1 * 100) / StudentAmount)
        , ((studentDayScanData.ScanStatus11 * 100) / StudentAmount)
        , ((studentDayScanData.ScanStatus10 * 100) / StudentAmount)
        , ((studentDayScanData.ScanStatus3 * 100) / StudentAmount)
        , ((studentDayScanData.ScanStatus99 * 100) / StudentAmount));
                    }
                    //
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message, data };

            return JsonConvert.SerializeObject(result);
        }


        [WebMethod(EnableSession = true)]
        public static string LoadStudentBehaviorScoreData()
        {
            bool success = true;
            string message = "Save Successfully";
            List<BehaviorScoreModel> data = new List<BehaviorScoreModel>();

            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                int schoolID = userData.CompanyID;

                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    StudentLogic studentLogic = new StudentLogic(en);
                    string currentTermID = studentLogic.GetTermId(userData);
                    TTerm termObj = en.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();

                    if (termObj != null)
                    {
                        // ตารางการจัดอันดับคะแนนความประพฤติ
                        var behaviorSettings = en.TBehaviorSettings.Where(w => w.SchoolID == schoolID).FirstOrDefault();
                        int showMinusSign = behaviorSettings == null ? 1 : ((behaviorSettings.ShowMinusSign ?? 0) == 1 ? 1 : -1);
                        DateTime? startDate, endDate;
                        if (behaviorSettings.Type == 1)
                        {
                            startDate = termObj.dStart;
                            endDate = termObj.dEnd;
                        }
                        else
                        {
                            var terms = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == termObj.nYear && w.cDel == null).ToList();
                            startDate = terms.Min(min => min.dStart) ?? DateTime.Today;
                            endDate = terms.Where(w => w.nYear == termObj.nYear && w.cDel == null && w.SchoolID == schoolID).Max(max => max.dEnd) ?? DateTime.Today;
                        }

                        string query = string.Format(@"
DECLARE @DAYSTART DATETIME = '{0:yyyyMMdd} 00:00:00.000'
DECLARE @DAYEND DATETIME = '{1:yyyyMMdd} 23:59:59.000'
DECLARE @nTerm VARCHAR(25) = '{2}'
DECLARE @ShowMinusSign INT = '{3}'
DECLARE @SchoolID INT = '{4}'

SELECT TOP 10 bh.ResidualScore * @ShowMinusSign 'Score', sv.sName + ' ' + sv.sLastname 'Name', sv.SubLevel + ' / ' + sv.nTSubLevel2 'Classroom', sv.nTerm
FROM TBehaviorHistory bh
INNER JOIN 
(
	SELECT StudentId, MAX(BehaviorHistoryId) 'BehaviorHistoryId'
	FROM TBehaviorHistory
	WHERE SchoolID = @SchoolID AND ISNULL(cDel, 0) = 0
	AND dAdd BETWEEN @DAYSTART AND @DAYEND
	AND dCanCel IS NULL
	GROUP BY StudentId
) B
ON bh.StudentId = B.StudentId AND bh.BehaviorHistoryId = B.BehaviorHistoryId
INNER JOIN TB_StudentViews sv
ON sv.sID = bh.StudentId AND sv.SchoolID = bh.SchoolID AND ISNULL(sv.nStudentStatus, 0) NOT IN (2)
WHERE sv.nTerm = @nTerm
ORDER BY ResidualScore", startDate, endDate, currentTermID, showMinusSign, schoolID);
                        data = en.Database.SqlQuery<BehaviorScoreModel>(query).ToList();
                        //
                    }
                }

            }
            catch (Exception err)
            {

            }

            var result = new { success, message, data };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string CheckInvoiceRemind()
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                int schoolID = userData.CompanyID;
                int userID = userData.UserID;

                using (JabJaiMasterEntities em = Connection.MasterEntities(ConnectionDB.Read))
                {
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                    {
                        StudentLogic studentLogic = new StudentLogic(en);
                        string currentTermID = studentLogic.GetTermId(userData);
                        TTerm termObj = en.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                        TYear yearObj = en.TYears.Where(w => w.nYear == termObj.nYear).FirstOrDefault();


                        var qryCountInvoice = $@"select count(1) as CountInvoice from TInvoice a inner join TInvoiceDetail b on a.InvoiceId = b.InvoiceId 
                                            inner join TCompany c on a.SchoolId = c.nCompany
                                            where  isnull(a.recordDelete,0)=0 and isnull(b.recordDelete,0)=0 and b.Year={yearObj.numberYear} and a.SchoolId={schoolID}";

                        var checkCountInvoice = em.Database.SqlQuery<int>(qryCountInvoice).FirstOrDefault();
                        if (checkCountInvoice > 0)
                        {
                            var permissionSql = "";
                            var hasPermission = HasInvoicePermission(schoolID, userID);
                            if (hasPermission)
                            {
                                permissionSql = $@"union
                                                    SELECT Detail,Created FROM TNews WHERE remark in('RemindInvoice') AND ISNULL(ISDone,0)=0 and EndDate >= getdate() and SchoolId = {schoolID}
                                                    UNION
                                                    SELECT Detail,Created FROM TNews WHERE remark in('RemindReceipt') AND ISNULL(ISDone,0)=1 and EndDate >= getdate() and SchoolId = {schoolID}";
                            }
                            var sql = $@"select top 1 Detail from (
                                                    SELECT Detail,Created FROM TNews WHERE remark in ('RemindBillSetting','RemindPayment','RemindSchoolInActive') AND ISNULL(ISDone,0)=0 and EndDate >= getdate() and SchoolId = {schoolID}
                                                    {permissionSql}
                                                ) as tmp order by Created desc";

                            var msgInvoice = em.Database.SqlQuery<string>(sql).FirstOrDefault();

                            if (!string.IsNullOrEmpty(msgInvoice))
                            {
                                return JsonConvert.SerializeObject(new { success = true, message = msgInvoice });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return JsonConvert.SerializeObject(new { success = false, message = "" });
        }

        public static bool HasInvoicePermission(int schoolId, int userId)
        {
            using (var dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var item = (from a in dbMaster.TGroupPermission
                            join b in dbMaster.TGroupPermissionUser on a.GroupID equals b.GroupID
                            where a.SchoolID == schoolId && a.GroupName == "แอดมิน/ผู้บริหาร" && b.UserID == userId && b.IsActive == true && b.IsDel == false
                            select a).ToList();
                if (item.Any())
                {
                    return true;
                }

                var sql = $"select NotificationSettingId from TNotificationSetting where schoolId={schoolId} and staffId={userId}";
                var contact = dbMaster.Database.SqlQuery<int?>(sql).FirstOrDefault();
                if (contact != null && contact.HasValue)
                {
                    return true;
                }


                return false;
            }
        }
    }

    public class AdminMainModel
    {
        public string SchoolName { get; set; }

        public int EmployeeAmount { get; set; }
        public string SchoolCreditBureau { get; set; }

        public string SummaryTime { get; set; }
        public string ReportTime { get; set; }
        public int? AcademicYear { get; set; }
        public string Semester { get; set; }
        public DateTime? SemesterStartDate { get; set; }
        public DateTime? SemesterEndDate { get; set; }
        public int HolidayAmount { get; set; }
        public decimal? MaxScoreBehavior { get; set; }

        public StudentStatusModel StudentStatusData { get; set; }

        public string EventJsonData { get; set; }

        public List<TMessageSystem> MessageSystems { get; set; }
        public List<BroadcastModel> Broadcasts { get; set; }

        public string StudentDayScanJsonData { get; set; }

        public List<BehaviorScoreModel> BehaviorScoreRankings { get; set; }
    }

    public class StudentStatusModel
    {
        public int? StudentAmount { get; set; } = 0;
        public int? StudentStatus0 { get; set; } = 0;
        public int? StudentStatus1 { get; set; } = 0;
        public int? StudentStatus2 { get; set; } = 0;
        public int? StudentStatus3 { get; set; } = 0;
        public int? StudentStatus4 { get; set; } = 0;
        public int? StudentStatus5 { get; set; } = 0;
        public int? StudentStatus6 { get; set; } = 0;
    }

    public class BehaviorScoreModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "classroom")]
        public string Classroom { get; set; }

        [JsonProperty(PropertyName = "score")]
        public decimal? Score { get; set; }
    }

    public class CalendarEventModel
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "start")]
        public DateTime? Start { get; set; }

        [JsonProperty(PropertyName = "allDay")]
        public bool AllDay { get; set; }

        [JsonProperty(PropertyName = "end")]
        public DateTime? End { get; set; }

        // color classes: [ event-default | event-rose | event-blue | event-azure | event-green | event-orange | event-red ]
        [JsonProperty(PropertyName = "className")]
        public string ClassName { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "startText")]
        public string StartText { get; set; }

        [JsonProperty(PropertyName = "endText")]
        public string EndText { get; set; }

        [JsonProperty(PropertyName = "holidayType")]
        public string HolidayType { get; set; }
    }

    public class DayScanModel
    {
        public string Day { get; set; }
        public int? StudentAmount { get; set; }
        public int? ScanStatus0 { get; set; }
        public int? ScanStatus12 { get; set; }
        public int? ScanStatus1 { get; set; }
        public int? ScanStatus11 { get; set; }
        public int? ScanStatus10 { get; set; }
        public int? ScanStatus3 { get; set; }
        public int? ScanStatus99 { get; set; }
    }

    public class BroadcastModel
    {
        public int NewsID { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
    }

    public class NotificationModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AddDate { get; set; }
        public string Type { get; set; }
    }
}
