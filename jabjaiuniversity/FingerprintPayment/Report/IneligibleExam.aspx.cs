using iTextSharp.text;
using iTextSharp.text.pdf;
using JabjaiEntity.DB;
using MasterEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ScottPlot.Control;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    public partial class IneligibleExam : BehaviorGateway
    {

        public class Search
        {
            public string term { get; set; }
            public int teacherId { get; set; }
            public string teacher { get; set; }
            public int subjectId { get; set; }
            public string subject { get; set; }
            public int levelId { get; set; }
            public string levelNo { get; set; }
            public string classId { get; set; }
            public int status { get; set; }
            public string termNo { get; set; }
            public string yearNo { get; set; }
        }
        private class DataModel
        {
            public string nTerm { get; set; }
            public int SubjectID { get; set; }
            public string Subject { get; set; }

            public int TeacherID { get; set; }
            public string Teacher { get; set; }
            public int LevelID { get; set; }
            public string LevelName { get; set; }
            public int ClassID { get; set; }
            public string ClassRoom { get; set; }
        }
        private class ScheduleModel
        {
            public int sScheduleID { get; set; }
            public int nPlaneDay { get; set; }
            public int nTermSubLevel2 { get; set; }
            public double? CourseHour { get; set; }
            public TimeSpan tStart { get; set; }
            public TimeSpan tEnd { get; set; }
            public int ClassPeriod { get; set; }
        }
        private class StudentModel
        {
            public int sScheduleID { get; set; }
            public int sID { get; set; }
            public DateTime LogLearnDate { get; set; }
            public int Come { get; set; }
            public int Absent { get; set; }
            //public string Code { get; set; }
            //public int? StudentNo { get; set; }
            //public string FullName { get; set; }
            //public int LevelID { get; set; }
            //public int ClassID { get; set; }
            //public string Class { get; set; }

        }
        private class DataModel2
        {
            public int Index { get; set; }
            public int sID { get; set; }
            public string FullName { get; set; }
            public int? StudentNo { get; set; }
            public string Code { get; set; }
            public string Class { get; set; }
            public int ClassTime { get; set; }
            public int Come { get; set; }
            public int Absent { get; set; }
            public string Percent { get; set; }
            public string Status { get; set; }
            public string Teacher { get; set; }
            public string Note { get; set; }
        }
        private class WorkDateModel
        {
            public DateTime Date { get; set; }
            public int sScheduleID { get; internal set; }
            public int ClassID { get; internal set; }
            public int ClassPeriod { get; internal set; }

            //public int WeekNum { get; set; }
            //public bool IsCalc { get; set; }
        }

        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadTeacherSubject(string term)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var query = $@"

SELECT  DISTINCT B.nTerm

, F.sEmp  'TeacherID', F.sName + ' ' + F.sLastname 'Teacher' 
	, H.nTSubLevel 'LevelID', H.SubLevel 'LevelName'  
	, G.nTermSubLevel2 'ClassID' , H.SubLevel + '/' + G.nTSubLevel2 'ClassRoom'  
    , P.sPlaneID 'SubjectID', ISNULL(P.courseCode + ' ' + P.sPlaneName,'') + ' ' + ISNULL(P.CourseCodeEn + ' ' + P.CourseNameEn ,'') 'Subject'

FROM TSchedule  A
JOIN TTermTimeTable  B ON A.nTermTable = B.nTermTable AND A.SchoolID = B.SchoolID
JOIN TPlanCourse D ON A.sPlaneID = D.sPlaneID AND A.SchoolID = D.SchoolID
JOIN TPlanCourseTeacher E on D.PlanCourseId = E.PlanCourseId and D.SchoolID = E.SchoolID
JOIN TEmployees F ON E.SchoolID = F.SchoolID AND E.sEmp = F.sEmp
JOIN TTermSubLevel2 G on  G.nTermSubLevel2 = B.nTermSubLevel2 and G.SchoolID = B.SchoolID
JOIN TSubLevel H ON G.nTSubLevel = H.nTSubLevel and G.SchoolID = H.SchoolID
JOIN TPlane P ON A.sPlaneID = P.sPlaneID and A.SchoolID = P.SchoolID

 WHERE A.cDel IS NULL 
AND D.CourseStatus = 1 and D.IsActive = 1     
AND P.cDel IS NULL 
AND E.IsActive = 1 
and ISNULL(E.cDel,0) = 0
AND B.nTerm = '{term}'
AND A.SchoolID = {userData.CompanyID} 

--order by P.courseGroup , P.courseType  asc 

";

                var data = ctx.Database.SqlQuery<DataModel>(query).ToList();

                return data;
            }

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadData(Search search)
        {
            var d = InitData(search);

            return new
            {
                data = d
            };
        }

        private static List<DataModel2> InitData(Search search)
        {
            var userData = GetUserData();
            using (var _context = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var term = _context.TTerms
                 .Where(f => f.nTerm == search.term)
                 .FirstOrDefault();
                var date1 = term.dStart.Value;
                var date2 = term.dEnd.Value;

                var qry1 = $@"
SELECT DISTINCT  A.sScheduleID , A.nPlaneDay , B.nTermSubLevel2 ,D.CourseHour ,  A.tStart , A.tEnd
FROM TSchedule AS A
 JOIN TTermTimeTable AS B ON A.nTermTable = B.nTermTable AND A.SchoolID = B.SchoolID
 JOIN TTerm AS C ON B.nTerm = C.nTerm AND B.SchoolID = C.SchoolID
 JOIN TPlanCourse D ON A.sPlaneID = D.sPlaneID AND A.SchoolID = D.SchoolID
WHERE A.cDel IS NULL 
AND A.sPlaneID = {search.subjectId} 
AND B.nTerm =  '{search.term}' 
AND A.SchoolID = {userData.CompanyID}
AND B.nTermSubLevel2 in ({search.classId})
";

                var scheduleList = _context.Database.SqlQuery<ScheduleModel>(qry1).ToList();

                foreach (var s in scheduleList)
                {
                    var min = (s.tEnd - s.tStart).TotalMinutes;
                    s.ClassPeriod = min < 50 ? 1 : (Convert.ToInt32(min) / 50);
                }

                var schListID = scheduleList.Select(o => o.sScheduleID);

                var arrClass = search.classId.Split(',').Select(o => Convert.ToInt32(o));
                var holidayList = _context.THolidays
                                  .Where(o => o.SchoolID == userData.CompanyID)
                                  .Where(w => ((date1 <= w.dHolidayStart && date1 <= w.dHolidayEnd)
                    || (date2 >= w.dHolidayStart && date2 <= w.dHolidayEnd)) && w.cDel == null && w.sHolidayType != "3")
                                  .ToList();

                var holidaySomeList = _context.THolidaySomes
                                  .Where(o => o.SchoolID == userData.CompanyID && arrClass.Contains(o.nTSubLevel.Value))
                                  .ToList();

                var dow = scheduleList.Select(o => (DayOfWeek)o.nPlaneDay).Distinct();
                var dates = Enumerable.Range(0, (date2 - date1).Days + 1)
                    .Select(o => date1.AddDays(o))
                    .Where(o => dow.Contains(o.DayOfWeek));
                //.Select(d => FilterHoliday(date1.AddDays(d), holidayList , holidaySomeList))

                var holidays = dates.Select(o => FilterHoliday(o, holidayList, holidaySomeList))
                    .Where(o => o.HasValue)
                    .Select(o => o.Value);

                //CultureInfo currentCulture = CultureInfo.CurrentCulture;

                var workDate = from a in dates.Except(holidays)
                               from b in scheduleList.Where(i => a.Date.DayOfWeek == (DayOfWeek)i.nPlaneDay)
                               select new WorkDateModel
                               {
                                   Date = a,
                                   sScheduleID = b.sScheduleID,
                                   ClassID = b.nTermSubLevel2,
                                   ClassPeriod = b.ClassPeriod
                                   //WeekNum = currentCulture.Calendar.GetWeekOfYear(a,
                                   //     CalendarWeekRule.FirstDay,
                                   //     DayOfWeek.Monday),
                                   //IsCalc = false,
                               };

                //var classDay = (from a in scheduleList
                //                from b in workDate.Where(i => i.Date.DayOfWeek == (DayOfWeek)a.nPlaneDay)
                //                select new
                //                {
                //                    a.nTermSubLevel2,
                //                    b.Date,
                //                    a.ClassPeriod,
                //                })
                //                .GroupBy(o => o.nTermSubLevel2)
                //                .Select(o => new
                //                {
                //                      ClassID = o.Key,
                //                      Day = o.Sum(i => i.ClassPeriod)
                //                 });
                //var weekList = new List<int>();
                //foreach (var d in workDate)
                //{
                //    if (!weekList.Contains(d.WeekNum))
                //    {
                //        weekList.Add(d.WeekNum);
                //        d.IsCalc = true;
                //    }
                //}
                /*
                var qry2 = $@"

SELECT A.sID , A.nStudentNumber 'StudentNo', A.sStudentID 'Code', A.sName + ' ' + A.sLastname 'FullName' ,A.nTSubLevel 'LevelID' , A.nTermSubLevel2 'ClassID' , A.SubLevel +'/'  + A.nTSubLevel2 'Class' 
, B.sScheduleID , B.LogLearnDate, ISNULL(B.Come,0) 'Come' , ISNULL(B.Absent,0) 'Absent'

FROM JabjaiSchoolSingleDB.dbo.TB_StudentViews A
LEFT JOIN (

	SELECT T.sScheduleID,T.LogLearnDate , T.sID , SUM(T.Come) 'Come' , SUM(T.Absent) 'Absent'
	FROM (
		SELECT sScheduleID,sID ,LogLearnDate, CASE WHEN LogLearnScanStatus in (0, 1) THEN 1 ELSE 0 END 'Come' ,  CASE WHEN LogLearnScanStatus not in (0, 1) THEN 1 ELSE 0 END 'Absent'
		FROM JabjaiSchoolSingleDB.dbo.TLogLearnTimeScan 
		WHERE  SchoolID = {userData.CompanyID}
		AND sScheduleID in ({string.Join(",", schListID)})
        {(holidays.Count() > 0 ? $" and LogLearnDate not in ( {string.Join(",", holidays.Select(o => o.ToString(@"\'yyyyMMdd\'")))} ) " : "")}

		UNION ALL

		SELECT sScheduleID,sID ,LogLearnDate, CASE WHEN LogLearnScanStatus in (0, 1) THEN 1 ELSE 0 END 'Come' ,  CASE WHEN LogLearnScanStatus not in (0, 1) THEN 1 ELSE 0 END 'Absent'
		FROM JabjaiSchoolHistory.dbo.TLogLearnTimeScan 
		WHERE  SchoolID = {userData.CompanyID}
		and sScheduleID in ({string.Join(",", schListID)})
        {(holidays.Count() > 0 ? $" and LogLearnDate not in ( {string.Join(",", holidays.Select(o => o.ToString(@"\'yyyyMMdd\'")))} ) " : "")}
	)T
	GROUP BY T.sScheduleID,T.LogLearnDate, T.sID

)B ON A.sID = B.sID
WHERE A.SchoolID = {userData.CompanyID}
AND A.nTerm = '{search.term}' 
AND A.nTSubLevel = {search.levelId}
AND A.nTermSubLevel2 in ({search.classId})
AND ISNULL(A.nStudentStatus,0) = 0
AND ISNULL(A.cDel,'0') = '0'

order by A.SubLevel 
, CAST( REPLACE(TRANSLATE(A.nTSubLevel2, 'abcdefghijklmnopqrstuvwxyz+()-. ,#+', '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@'), '@', '')  as int) 
, A.nStudentNumber ASC
";
                */

                var classArr = search.classId.Split(',').ToList();
                var studentList = _context.TB_StudentViews.Where(o => o.SchoolID == userData.CompanyID
                                    && o.nTerm == search.term
                                    && o.nTSubLevel == search.levelId
                                    && classArr.Contains(o.nTermSubLevel2 + "")
                                    && (o.cDel ?? "0") == "0"
                                    && (o.nStudentStatus ?? 0) == 0)
                                    .Select(o => new
                                    {
                                        o.sID,
                                        o.nStudentNumber,
                                        o.sStudentID,
                                        FullName = o.sName + " " + o.sLastname,
                                        LevelID = o.nTSubLevel,
                                        ClassID = o.nTermSubLevel2,
                                        Class = o.SubLevel + "/" + o.nTSubLevel2,
                                        Order = o.SubLevel + o.nTSubLevel2,
                                    })
                                    .OrderBy(o => o.Order)
                                    .ThenBy( o => o.nStudentNumber)
                                    .AsQueryable()
                                    .ToList();
                var qry2 = $@"
SELECT T.sScheduleID,T.LogLearnDate , T.sID , SUM(T.Come) 'Come' , SUM(T.Absent) 'Absent'
	FROM (
		SELECT sScheduleID,sID ,LogLearnDate, CASE WHEN LogLearnScanStatus in (0, 1) THEN 1 ELSE 0 END 'Come' ,  CASE WHEN LogLearnScanStatus not in (0, 1) THEN 1 ELSE 0 END 'Absent'
		FROM JabjaiSchoolSingleDB.dbo.TLogLearnTimeScan 
		WHERE  SchoolID = {userData.CompanyID}
		AND sScheduleID in ({string.Join(",", schListID)})
        {(holidays.Count() > 0 ? $" and LogLearnDate not in ( {string.Join(",", holidays.Select(o => o.ToString(@"\'yyyyMMdd\'")))} ) " : "")}

		UNION ALL

		SELECT sScheduleID,sID ,LogLearnDate, CASE WHEN LogLearnScanStatus in (0, 1) THEN 1 ELSE 0 END 'Come' ,  CASE WHEN LogLearnScanStatus not in (0, 1) THEN 1 ELSE 0 END 'Absent'
		FROM JabjaiSchoolHistory.dbo.TLogLearnTimeScan 
		WHERE  SchoolID = {userData.CompanyID}
		and sScheduleID in ({string.Join(",", schListID)})
        {(holidays.Count() > 0 ? $" and LogLearnDate not in ( {string.Join(",", holidays.Select(o => o.ToString(@"\'yyyyMMdd\'")))} ) " : "")}
	)T
	GROUP BY T.sScheduleID,T.LogLearnDate, T.sID
";
                var logLearnData = _context.Database.SqlQuery<StudentModel>(qry2).ToList();

                foreach (var student in studentList)
                {
                    foreach (var date in workDate.Where(i => i.ClassID == student.ClassID))
                    {
                        var _qry = logLearnData.Where(o => o.sID == student.sID && o.LogLearnDate == date.Date).AsQueryable();
                        var isComeToday = _qry.Where(o => o.Come == 1).Count() > 0;
                        var log = _qry.Where(o => o.sScheduleID == date.sScheduleID).FirstOrDefault();

                        if (log == null)
                        {
                            logLearnData.Add(new StudentModel()
                            {
                                sID = student.sID,
                                LogLearnDate = date.Date,
                                sScheduleID = date.sScheduleID,
                                Come = isComeToday ? 1 * date.ClassPeriod : 0,
                                Absent = isComeToday ? 0 : 1 * date.ClassPeriod,
                            });
                        }
                        else
                        {
                            log.Come = isComeToday ? 1 * date.ClassPeriod : 0;
                            log.Absent = isComeToday ? 0 : 1 * date.ClassPeriod;
                        }
                    }
                }

                var result = from a in studentList
                             from b in logLearnData.GroupBy(o => o.sID)
                              .Select(o => new
                              {
                                  sID = o.Key,
                                  Come = o.Sum(i => i.Come),
                                  Absent = o.Sum(i => i.Absent),
                                  ClassTime = o.Sum(i => i.Come) + o.Sum(i => i.Absent),
                              }).Where(o => o.sID == a.sID)

                             select new
                             {
                                 sID = a.sID,
                                 StudentNo = a.nStudentNumber,
                                 Code = a.sStudentID,
                                 FullName = a.FullName,
                                 LevelID = a.LevelID,
                                 ClassID = a.ClassID,
                                 Class = a.Class,
                                 Come = b.Come,
                                 Absent = b.Absent,
                                 ClassTime = b.ClassTime,
                                 Percent = b.ClassTime > 0 ? (b.Come * 100) / (b.ClassTime * 1f) : 0,
                             };
                //from b in scheduleList.Where(i => (DayOfWeek)i.nPlaneDay == a.Date.DayOfWeek)
                //var studentWithDate = from a in workDate
                //                      from b in rawLogLearn.Where(o => o.LogLearnDate == a.Date && o.sScheduleID == a.sScheduleID).DefaultIfEmpty()
                //                      select new
                //                      {
                //                          a.Date,
                //                          a.sScheduleID,
                //                          a.ClassPeriod,
                //                          a.ClassID,
                //                          b.sID,
                //                          b
                //                          Come = b?.Come ?? 0,
                //                          Absent = b?.Absent ?? 1,
                //                      };


                //foreach (var d in data1)
                //{
                //    var sch = scheduleList.FirstOrDefault(o => o.sScheduleID == d.sScheduleID);
                //    if (sch != null)
                //    {
                //        d.Come *= sch.ClassPeriod;
                //        d.Absent *= sch.ClassPeriod;
                //    }
                //}

                //data1 = data1.GroupBy(o => o.sID)
                //    .Select(o => new StudentModel
                //    {
                //        sID = o.Key,
                //        StudentNo = o.Max(i => i.StudentNo),
                //        Code = o.Max(i => i.Code),
                //        FullName = o.Max(i => i.FullName),
                //        LevelID = o.Max(i => i.LevelID),
                //        ClassID = o.Max(i => i.ClassID),
                //        Class = o.Max(i => i.Class),
                //        Come = o.Sum(i => i.Come),
                //        Absent = o.Sum(i => i.Absent),
                //    })
                //    .ToList();



                //var data2 = scheduleList
                //    .GroupBy(o => o.nTermSubLevel2)
                //    .Select(o => new
                //    {
                //        ClassID = o.Key,
                //        Day = (
                //            from a in o
                //            from b in workDate.Where(i => i.Date.DayOfWeek == (DayOfWeek)a.nPlaneDay)
                //            select a.ClassPeriod
                //        ).Sum()
                //    });

                //var result = from a in data1
                //             from b in classDay.Where(o => o.ClassID == a.ClassID).DefaultIfEmpty()

                //             select new
                //             {
                //                 a,
                //                 b,
                //                 Percent = b.Day > 0 ? (a.Come * 100) / (b.Day * 1f) : 0,
                //             };

                var teacher = _context.TEmployees
                     .Where(o => o.SchoolID == userData.CompanyID && o.sEmp == search.teacherId)
                     .Select(o => o.sName + " " + o.sLastname)
                     .FirstOrDefault();

                if (search.status == 1)
                {
                    result = result.Where(o => o.ClassTime < 80);
                }

                return result.Select((o, i) => new DataModel2
                {
                    Index = i + 1,
                    sID = o.sID,
                    StudentNo = o.StudentNo,
                    FullName = o.FullName,
                    Code = o.Code,
                    Class = o.Class,
                    ClassTime = o.ClassTime,
                    Come = o.Come,
                    Absent = o.Absent,
                    Percent = o.Percent.ToString("0.##"),
                    Status = o.Percent < 80 ? "ไม่มีสิทธิ์สอบ" : "มีสิทธิ์สอบ",
                    Teacher = teacher,
                    Note = ""
                }).ToList();
            }
        }

        private static DateTime? FilterHoliday(DateTime dateTime, List<THoliday> holidayList, List<THolidaySome> holidaySomeList)
        {
            var h = holidayList.FirstOrDefault(f => dateTime >= f.dHolidayStart && dateTime <= f.dHolidayEnd);
            if (h != null)
            {
                if (h.sWhoSeeThis == "0" || h.sWhoSeeThis == "2")
                {
                    return dateTime;
                }
                else if (h.sWhoSeeThis != "1")
                {
                    if (holidaySomeList.FirstOrDefault(f => f.nHoliday == h.nHoliday) != null)
                    {
                        return dateTime;
                    }
                }
            }

            return null;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(Search search)
        {
            var result = InitData(search);

            var userData = GetUserData();
            using (ExcelPackage excel = new ExcelPackage())
            {
                var sheetName = "รายงานไม่มีสิทธ์สอบ";
                excel.Workbook.Worksheets.Add(sheetName);

                var worksheet = excel.Workbook.Worksheets[sheetName];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                    SetCell(worksheet.Cells[1, 1, 1, 12]
                      , isMerge: true
                      , text: school.sCompany
                      , fontSize: 14
                      , isBold: true);

                    SetCell(worksheet.Cells[2, 1, 2, 12]
                     , isMerge: true
                     , text: $"รายงานนักเรียนไม่มีสิทธ์สอบ ระดับชั้น {search.levelNo} ภาคเรียนที่ {search.termNo} ปีการศึกษา {search.yearNo} "
                     , fontSize: 14
                     , isBold: true);

                    using (var _context = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                    {
                        //var subject = _context.TPlanCourses
                        //    .Where(o => o.SchoolID == userData.CompanyID && o.sPlaneID == search.subjectId)
                        //    .Select(o => new { o.CourseTotalHour })
                        //    .FirstOrDefault();

                        SetCell(worksheet.Cells[3, 1, 3, 12]
                        , isMerge: true
                        , text: $"วิชา {search.subject} ผู้สอน {search.teacher}"
                        , fontSize: 14
                        , isBold: true);
                    }

                }
                var row = 4;

                SetCell(worksheet.Cells[row, 1], isHeader: true, text: "ลำดับ");
                SetCell(worksheet.Cells[row, 2], isHeader: true, text: "เลขที่");
                SetCell(worksheet.Cells[row, 3], isHeader: true, text: "เลขประจำตัว");
                SetCell(worksheet.Cells[row, 4], isHeader: true, text: "ชื่อ-นามสกุล");
                SetCell(worksheet.Cells[row, 5], isHeader: true, text: "ระดับชั้น/ห้อง");
                SetCell(worksheet.Cells[row, 6], isHeader: true, text: "เวลาเรียน");
                SetCell(worksheet.Cells[row, 7], isHeader: true, text: "มาเรียน");
                SetCell(worksheet.Cells[row, 8], isHeader: true, text: "ขาดเรียน");
                SetCell(worksheet.Cells[row, 9], isHeader: true, text: "ร้อยละ");
                SetCell(worksheet.Cells[row, 10], isHeader: true, text: "สถานะ");
                SetCell(worksheet.Cells[row, 11], isHeader: true, text: "ครูผู้สอน");
                SetCell(worksheet.Cells[row++, 12], isHeader: true, text: "หมายเหตุ");

                foreach (var d in result)
                {
                    SetCell(worksheet.Cells[row, 1], text: d.Index + "");
                    SetCell(worksheet.Cells[row, 2], text: d.StudentNo + "");
                    SetCell(worksheet.Cells[row, 3], text: d.Code + "");
                    SetCell(worksheet.Cells[row, 4], text: d.FullName + "", horizotal: ExcelHorizontalAlignment.Left);
                    SetCell(worksheet.Cells[row, 5], text: d.Class);
                    SetCell(worksheet.Cells[row, 6], text: d.ClassTime + "");

                    SetCell(worksheet.Cells[row, 7], text: d.Come + "");
                    SetCell(worksheet.Cells[row, 8], text: d.Absent + "");
                    SetCell(worksheet.Cells[row, 9], text: d.Percent + "");
                    SetCell(worksheet.Cells[row, 10], text: d.Status + "");
                    SetCell(worksheet.Cells[row, 11], text: d.Teacher + "");
                    SetCell(worksheet.Cells[row++, 12], text: d.Note + "");

                }

                worksheet.Cells.AutoFitColumns(20, 40);


                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/text;";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

            }

        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportPdf(Search search)
        {
            var result = InitData(search);

            var userData = GetUserData();
            //TPlanCourse subject;
            TCompany school;
            //using (var _context = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            //{
            //    subject = _context.TPlanCourses
            //        .Where(o => o.SchoolID == userData.CompanyID && o.sPlaneID == search.subjectId)
            //        .AsEnumerable()
            //        .Select(o => new TPlanCourse { CourseTotalHour = o.CourseTotalHour })
            //        .FirstOrDefault();
            //}
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                school = dbmaster.TCompanies.Where(w => w.nCompany == userData.CompanyID)
                    .AsEnumerable()
                    .Select(o => new TCompany { sCompany = o.sCompany })
                    .FirstOrDefault();

            }
            if (!FontFactory.IsRegistered("thsarabun1"))
            {
                var path = HttpContext.Current.Server.MapPath("~/Fonts/THSarabun.ttf");
                FontFactory.Register(path, "thsarabun1");
            }

            string filename = $"รายงานไม่มีสิทธ์สอบ_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";

            byte[] docfinal = null;

            Document doc = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);

            using (MemoryStream finalStream = new MemoryStream())
            {
                var copy = new PdfCopy(doc, finalStream);
                doc.Open();

                using (MemoryStream ms1 = new MemoryStream())
                {
                    Document doc1 = new Document(PageSize.A4.Rotate(), 25, 25, 70, 30);

                    using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                    {
                        writer1.CloseStream = false;

                        writer1.PageEvent = new ITextEvents(school
                            , $"รายงานนักเรียนไม่มีสิทธ์สอบ ระดับชั้น {search.levelNo} ภาคเรียนที่ {search.termNo} ปีการศึกษา {search.yearNo} "
                            , $"วิชา {search.subject} ผู้สอน {search.teacher}");

                        doc1.Open();

                        PdfPTable table2 = new PdfPTable(12);
                        table2.WidthPercentage = 99f;
                        var lstWidth = new List<float>(new float[] { 5, 8, 10, 15, 10, 8, 8, 8, 10, 10, 15, 15 });

                        table2.SetTotalWidth(lstWidth.ToArray());
                        table2.AddCell(SetCellPDF(text: "ลำดับ", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "เลขที่", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "เลขประจำตัว", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "ชื่อ-สกุล", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "ระดับชั้น/ห้อง", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "เวลาเรียน", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "มาเรียน", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "ขาดเรียน", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "ร้อยละ", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "สถานะ", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "ครูผู้สอน", horizotal: Element.ALIGN_CENTER));
                        table2.AddCell(SetCellPDF(text: "หมายเหตุ", horizotal: Element.ALIGN_CENTER));
                        //row data
                        for (int row = 0; row < result.Count; row++)
                        {
                            var s = result[row];

                            table2.AddCell(SetCellPDF(text: s.Index + ""));
                            table2.AddCell(SetCellPDF(text: s.StudentNo + ""));
                            table2.AddCell(SetCellPDF(text: s.Code));
                            table2.AddCell(SetCellPDF(text: s.FullName, horizotal: Element.ALIGN_LEFT));
                            table2.AddCell(SetCellPDF(text: s.Class));
                            table2.AddCell(SetCellPDF(text: s.ClassTime + ""));
                            table2.AddCell(SetCellPDF(text: s.Come + ""));
                            table2.AddCell(SetCellPDF(text: s.Absent + ""));
                            table2.AddCell(SetCellPDF(text: s.Percent));
                            table2.AddCell(SetCellPDF(text: s.Status));
                            table2.AddCell(SetCellPDF(text: s.Teacher));
                            table2.AddCell(SetCellPDF(text: s.Note));
                        }

                        doc1.Add(table2);

                        doc1.Close();
                    }

                    ms1.Position = 0;
                    var x = new PdfReader(ms1);
                    copy.AddDocument(x);
                    ms1.Dispose();
                }

                copy.Close();
                doc.Close();

                docfinal = finalStream.ToArray();
            }



            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            HttpContext.Current.Response.BinaryWrite(docfinal);
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

        }


        public static PdfPCell SetCellPDF(
         string text = ""
        , int fontSize = 14
        , int fontStyle = iTextSharp.text.Font.NORMAL
        , int horizotal = Element.ALIGN_CENTER
        , int vetical = Element.ALIGN_MIDDLE
        , int border = iTextSharp.text.Rectangle.BOX
        , int colspan = 1
        , int rowspan = 1

        )
        {
            var font = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, fontSize, fontStyle);
            var p = new Phrase(text, font);
            var cell = new PdfPCell(p);
            //cell.UseAscender = true;
            cell.VerticalAlignment = vetical;
            cell.HorizontalAlignment = horizotal;
            cell.Border = border;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 1f;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;

            return cell;
        }

        private static void SetCell(ExcelRange xrange
          , bool isHeader = false
          , bool isMerge = false
          , string text = ""
          , int fontSize = 11
          , bool isBold = false
          , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
          , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
      )
        {
            using (xrange)
            {
                xrange.Merge = isMerge;
                xrange.Value = text;
                xrange.Style.Font.Bold = isBold;
                xrange.Style.HorizontalAlignment = horizotal;
                xrange.Style.VerticalAlignment = vetical;
                xrange.Style.Font.Size = fontSize;

                if (isHeader)
                {
                    xrange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    xrange.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    xrange.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
                }
                //    xrange.Style.Fill.BackgroundColor.SetColor(bgColor);

                xrange.AutoFitColumns();

            }
        }

        public class ITextEvents : PdfPageEventHelper
        {
            string summary1, summary2;
            TCompany school;
            public ITextEvents(TCompany school, string sum1, string sum2)
            {
                this.school = school;
                summary1 = sum1;
                summary2 = sum2;
            }
            // This is the contentbyte object of the writer  
            PdfContentByte cb;

            // we will put the final number of pages in a template  
            PdfTemplate headerTemplate, footerTemplate;

            // this is the BaseFont we are going to use for the header / footer  
            BaseFont bf = null;

            // This keeps track of the creation time  
            DateTime PrintTime = DateTime.Now;
            string dateNow = "";
            #region Fields  
            private string _header;
            #endregion

            #region Properties  
            public string Header
            {
                get { return _header; }
                set { _header = value; }
            }
            #endregion

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    dateNow = DateTime.Now.ToString("d MMMM yyyy เวลา HH:mm", new CultureInfo("th-TH"));
                    //bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font f = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 14, iTextSharp.text.Font.NORMAL);
                    //bf = BaseFont.CreateFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    bf = f.BaseFont;
                    cb = writer.DirectContent;
                    headerTemplate = cb.CreateTemplate(document.PageSize.Width - 100, 50);
                    footerTemplate = cb.CreateTemplate(document.PageSize.Width - 100, 50);
                }
                catch (DocumentException de)
                {
                }
                catch (System.IO.IOException ioe)
                {
                }
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);

                //Add paging to footer  
                {
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10);

                    cb.SetTextMatrix(document.PageSize.GetLeft(30), document.PageSize.GetBottom(5));
                    var text1 = $"รายงานข้อมูล ณ วันที่ {dateNow}";
                    cb.ShowText(text1);

                    cb.SetTextMatrix(document.PageSize.GetRight(30), document.PageSize.GetBottom(5));
                    var text2 = $"{writer.PageNumber}/";
                    cb.ShowText(text2);
                    cb.EndText();
                    float len = bf.GetWidthPoint(text2, 10);
                    // cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));document.PageSize.GetRight(20)
                    cb.AddTemplate(footerTemplate, document.PageSize.GetRight(30) + len, document.PageSize.GetBottom(5));// document.PageSize.GetBottom(20)
                }

                {
                    PdfPTable mainTable = new PdfPTable(1);
                    mainTable.TotalWidth = document.PageSize.Width - 30f;

                    mainTable.WidthPercentage = 90;
                    mainTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    PdfPTable table = new PdfPTable(1);
                    //table.TotalWidth = document.PageSize.Width - 20f;
                    //table.WidthPercentage = 99f;                
                    table.WidthPercentage = 100;
                    table.PaddingTop = 30;
                    //table.SetTotalWidth(new float[] { 10, 70, 20 });
                    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    table.AddCell(SetCellPDF(
                        text: school.sCompany,
                        horizotal: Element.ALIGN_CENTER,
                        fontSize: 16,
                         fontStyle: Font.BOLD,
                        border: Rectangle.NO_BORDER
                        ));

                    table.AddCell(SetCellPDF(
                      text: summary1,
                      fontSize: 16,
                      horizotal: Element.ALIGN_CENTER,
                      fontStyle: Font.BOLD,
                      border: Rectangle.NO_BORDER
                      ));

                    table.AddCell(SetCellPDF(
                        text: summary2,
                        fontSize: 16,
                        horizotal: Element.ALIGN_CENTER,
                        fontStyle: Font.BOLD,
                        border: Rectangle.NO_BORDER
                       ));

                    mainTable.AddCell(table);

                    mainTable.WriteSelectedRows(0, -1, 10, document.PageSize.Height, writer.DirectContent);
                }
            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                //headerTemplate.BeginText();
                //headerTemplate.SetFontAndSize(bf, 12);
                //headerTemplate.SetTextMatrix(0, 0);
                //headerTemplate.ShowText((writer.PageNumber - 1).ToString());
                //headerTemplate.EndText();

                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 10);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber) + "");
                footerTemplate.EndText();
            }
        }


    }

}