using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using iTextSharp.text;
using iTextSharp.text.log;
using iTextSharp.text.pdf;
using JabjaiEntity.DB;
using MasterEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using OfficeOpenXml.Style;
using ScottPlot.Drawing.Colormaps;
using ScottPlot.Styles;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    public partial class CheckingClass : BehaviorGateway
    {

        public class Search
        {
            public string term { get; set; }
            //public int teacherId { get; set; }
            //public string teacher { get; set; }
            public string subjectId { get; set; }
            //public string subject { get; set; }
            public int levelId { get; set; }
            public string levelNo { get; set; }
            public string classId { get; set; }
            public int reportType { get; set; }
            public string termNo { get; set; }
            public string yearNo { get; set; }
            public DateTime date1 { get; set; }
            public DateTime date2 { get; set; }
        }

        private class SubjectModel
        {
            public string nTerm { get; set; }
            public int SubjectID { get; set; }
            public string Subject { get; set; }

            public int LevelID { get; set; }
            public string LevelName { get; set; }
            public int ClassID { get; set; }
            public string ClassRoom { get; set; }
        }

        private class ResponeModel1
        {
            public int sScheduleID { get; set; }
            public int nPlaneDay { get; set; }
            public TimeSpan tStart { get; set; }
            public TimeSpan tEnd { get; set; }
            public string sPlaneName { get; set; }
            public string courseCode { get; set; }

            //public int TeacherID { get; set; }
            //public string Teacher { get; set; }
            public string ClassRoom { get; set; }
        }

        private class ResponeModel2
        {
            public int sScheduleID { get; set; }
            public DateTime LogLearnDate { get; set; }
            public long Count { get; set; }
            public string Teacher { get; set; }
        }

        private class ResultModel1
        {
            public string Code { get; set; }
            public string Time1 { get; set; }
            public string Time2 { get; set; }
            public int Col { get; set; }
            public int ColSpan { get; set; }
            public bool IsHoliday { get; set; }
            public bool IsChecked { get; set; }
            public string Teacher { get; internal set; }
        }

        private class ResultModel2
        {
            public string Date { get; set; }
            public List<ResultModel1> Schdule { get; set; }
            public bool IsHoliday { get; internal set; }
            public string Class { get; internal set; }
            public string Class2 { get; internal set; }
        }
        private class TimeModel
        {
            public int Index { get; set; }
            public string Time { get; set; }
            public TimeSpan? Time2 { get; internal set; }
        }


        //private class DataModel2
        //{
        //    public int Index { get; set; }
        //    public int sID { get; set; }
        //    public string FullName { get; set; }
        //    public int StudentNo { get; set; }
        //    public string Code { get; set; }
        //    public string Class { get; set; }
        //    public int ClassTime { get; set; }
        //    public int Come { get; set; }
        //    public int Absent { get; set; }
        //    public string Percent { get; set; }
        //    public string Status { get; set; }
        //    public string Teacher { get; set; }
        //    public string Note { get; set; }
        //}

        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadSubject(string term, int level)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var query = $@"

SELECT DISTINCT B.nTerm

	, H.nTSubLevel 'LevelID', H.SubLevel 'LevelName'  
	, G.nTermSubLevel2 'ClassID' , H.SubLevel + '/' + G.nTSubLevel2 'ClassRoom'  
    , P.sPlaneID 'SubjectID', ISNULL(P.courseCode + ' ' + P.sPlaneName,'') + ' ' + ISNULL(P.CourseCodeEn + ' ' + P.CourseNameEn ,'') 'Subject'
	
FROM TSchedule  A
JOIN TTermTimeTable  B ON A.nTermTable = B.nTermTable AND A.SchoolID = B.SchoolID
JOIN TPlanCourse D ON A.sPlaneID = D.sPlaneID AND A.SchoolID = D.SchoolID

JOIN TTermSubLevel2 G on  G.nTermSubLevel2 = B.nTermSubLevel2 and G.SchoolID = B.SchoolID
JOIN TSubLevel H ON G.nTSubLevel = H.nTSubLevel and G.SchoolID = H.SchoolID
JOIN TPlane P ON A.sPlaneID = P.sPlaneID and A.SchoolID = P.SchoolID

 WHERE A.cDel IS NULL 
AND D.CourseStatus = 1 and D.IsActive = 1     
AND P.cDel IS NULL 
AND B.nTerm = '{term}'
AND A.SchoolID = {userData.CompanyID} 
AND G.nTSubLevel = {level}
";

                var data = ctx.Database.SqlQuery<SubjectModel>(query).ToList();

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
                times = d.Item2,
                data = d.Item1,
            };
        }

        private static (List<ResultModel2>, List<TimeModel>) InitData(Search search)
        {
            var userData = GetUserData();
            using (var _context = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var qry1 = $@"
SELECT *
FROM (
    SELECT DISTINCT A.sScheduleID , A.nPlaneDay , A.tStart , A.tEnd , C.sPlaneID , C.sPlaneName , C.courseCode --,  A.*
    --, F.sEmp  'TeacherID', F.sName + ' ' + F.sLastname 'Teacher' 
   	, H.SubLevel , G.nTSubLevel2 , H.SubLevel + '/' + G.nTSubLevel2 'ClassRoom'  
    FROM JabjaiSchoolSingleDB.dbo.TSchedule A 
    JOIN JabjaiSchoolSingleDB.dbo.TTermTimeTable B ON A.SchoolID = B.SchoolID AND A.nTermTable = B.nTermTable
    JOIN JabjaiSchoolSingleDB.dbo.TPlane C ON A.SchoolID = C.SchoolID AND A.sPlaneID = C.sPlaneID

    --JOIN JabjaiSchoolSingleDB.dbo.TPlanCourse D ON A.sPlaneID = D.sPlaneID AND A.SchoolID = D.SchoolID
    --JOIN JabjaiSchoolSingleDB.dbo.TPlanCourseTeacher E on D.PlanCourseId = E.PlanCourseId and D.SchoolID = E.SchoolID
    --JOIN JabjaiSchoolSingleDB.dbo.TEmployees F ON E.SchoolID = F.SchoolID AND E.sEmp = F.sEmp

    JOIN JabjaiSchoolSingleDB.dbo.TTermSubLevel2 G on  G.nTermSubLevel2 = B.nTermSubLevel2 and G.SchoolID = B.SchoolID
    JOIN JabjaiSchoolSingleDB.dbo.TSubLevel H ON G.nTSubLevel = H.nTSubLevel and G.SchoolID = H.SchoolID

    WHERE A.cDel IS NULL 
    AND B.nTerm =  '{search.term}' 
    AND A.SchoolID = {userData.CompanyID}
    AND A.sPlaneID in ( {search.subjectId} )
    AND B.nTermSubLevel2 in ({search.classId})
    AND C.cDel IS NULL 
    --AND D.CourseStatus = 1 and D.IsActive = 1     
    --AND E.IsActive = 1  and ISNULL(E.cDel,0) = 0
)T
order by T.SubLevel 
, CAST( REPLACE(TRANSLATE(T.nTSubLevel2, 'abcdefghijklmnopqrstuvwxyz+()-. ,#+', '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@'), '@', '')  as int) 
";

                var res1 = _context.Database.SqlQuery<ResponeModel1>(qry1).ToList();
                var schListID = res1.Select(o => o.sScheduleID).Distinct();
                var qry2 = $@"


SELECT T1.* , B.sName + ' ' + B.sLastname 'Teacher' 
FROM(
	SELECT T.*
	FROM(
		SELECT  sScheduleID , LogLearnDate , ISNULL(A.nTeacherId,CreatedBy) 'nTeacherId' ,
		 ROW_NUMBER() OVER ( 
			  PARTITION BY sScheduleID ,LogLearnDate , A.nTeacherId
			  ORDER BY  A.CreatedDate DESC
		   ) 'Count'
  
		FROM JabjaiSchoolSingleDB.dbo.TLogLearnTimeScan  A
		WHERE  SchoolID = {userData.CompanyID}
		AND sScheduleID in ({string.Join(",", schListID)})
		AND LogLearnDate between '{search.date1.ToString("yyyyMMdd")} 00:00' and '{search.date2.ToString("yyyyMMdd")} 23:59'
	
	)T 
	WHERE T.Count = 1

	UNION ALL

	SELECT T.*
	FROM(
		SELECT  sScheduleID , LogLearnDate , ISNULL(A.nTeacherId,CreatedBy) 'nTeacherId' ,
		 ROW_NUMBER() OVER ( 
			  PARTITION BY sScheduleID ,LogLearnDate , A.nTeacherId
			  ORDER BY  A.CreatedDate DESC
		   ) 'Count'
  
		FROM JabjaiSchoolHistory.dbo.TLogLearnTimeScan  A
		WHERE  SchoolID = {userData.CompanyID}
		AND sScheduleID in ({string.Join(",", schListID)})
		AND LogLearnDate between '{search.date1.ToString("yyyyMMdd")} 00:00' and '{search.date2.ToString("yyyyMMdd")} 23:59'

	)T 
	WHERE T.Count = 1
)T1 LEFT JOIN JabjaiSchoolSingleDB.dbo.TEmployees B ON  T1.nTeacherId = B.sEmp
WHERE B.SchoolID = {userData.CompanyID} 


";

                var res2 = _context.Database.SqlQuery<ResponeModel2>(qry2).ToList()
                    .GroupBy(o => new { o.sScheduleID, o.LogLearnDate })
                    .Select(o => new ResponeModel2
                    {
                        sScheduleID = o.Key.sScheduleID,
                        LogLearnDate = o.Key.LogLearnDate,
                        Count = o.Min(i => i.Count),
                        Teacher = o.Min(i => i.Teacher)
                    });

                var arrClass = search.classId.Split(',').Select(o => Convert.ToInt32(o));
                var holidayList = _context.THolidays
                                  .Where(o => o.SchoolID == userData.CompanyID)
                                  .Where(w => ((search.date1 <= w.dHolidayStart && search.date1 <= w.dHolidayEnd)
                    || (search.date2 >= w.dHolidayStart && search.date2 <= w.dHolidayEnd)) && w.cDel == null && w.sHolidayType != "3")
                                  .ToList();

                var holidaySomeList = _context.THolidaySomes
                                  .Where(o => o.SchoolID == userData.CompanyID && arrClass.Contains(o.nTSubLevel.Value))
                                  .ToList();

                var term = _context.TTerms.Where(o => o.nTerm == search.term)
                    .Select(o => new { o.dStart, o.dEnd })
                    .FirstOrDefault();

                //var datesTerm = Enumerable.Range(0, (term.dEnd - term.dStart).Value.Days + 1)
                //   .Select(o => term.dStart?.AddDays(o));

                //var dow = scheduleList.Select(o => (DayOfWeek)o.nPlaneDay).Distinct();
                var scheduleList = from a in Enumerable.Range(0, (search.date2 - search.date1).Days + 1).Select(o => search.date1.AddDays(o))
                                   from b in res1.Where(o => a.DayOfWeek == (DayOfWeek)o.nPlaneDay)
                                   select new
                                   {
                                       b.sScheduleID,
                                       b.courseCode,
                                       b.tStart,
                                       b.tEnd,
                                       b.ClassRoom,
                                       Date = a,
                                       DoW = (DayOfWeek)a.DayOfWeek,
                                       IsHoliday = FilterHoliday(a, holidayList, holidaySomeList).HasValue,
                                       IsInTerm = (a >= term.dStart && a <= term.dEnd),
                                   };

                //var holidays = dates.Select(o => FilterHoliday(o, holidayList, holidaySomeList))
                //    .Where(o => o.HasValue)
                //    .Select(o => o);

                // var workDate = dates.Except(holidays);

                var data = (from a in scheduleList
                                //from e in dates.Where(o => o.DayOfWeek == (DayOfWeek)a.nPlaneDay)
                            from b in res2.Where(o => o.sScheduleID == a.sScheduleID && o.LogLearnDate == a.Date).DefaultIfEmpty()
                                //from c in holidays.Where(o => o?.Date == e.Date).DefaultIfEmpty()
                                //orderby e.Date
                            select new
                            {
                                isHoliday = a.IsHoliday,
                                date = a.Date,
                                code = a.courseCode,
                                time1 = a.tStart,
                                time2 = a.tEnd,
                                diff = a.tEnd - a.tStart,
                                isChecked = a.IsInTerm ? b?.Count > 0 : false,
                                teacher = b?.Teacher ?? "-",
                                classroom = a.ClassRoom
                            })
                            .ToList();

                var data1 = data
                    .GroupBy(o => new
                    {
                        //o.isHoliday,
                        o.date,
                        o.time1,
                        o.time2,
                        o.diff,
                    })
                    .Select(o => new
                    {
                        date = o.Key.date,
                        time1 = o.Key.time1,
                        time2 = o.Key.time2,
                        diff = o.Key.diff,
                        isHoliday = o.Min(i => i.isHoliday),
                        isChecked = o.Min(i => i.isChecked),
                        classroom = o.Min(i => i.classroom),
                        data = o.Select(i => new
                        {
                            i.teacher,
                            i.code,
                        }).ToList()
                        //teacher = o.Select(i => i.teacher),//string.Join(" ", o.Select(i => i.teacher)),
                        //code = o.Select(i => i.code),//string.Join(" ", o.Select(i => i.code)),
                    })
                    .OrderBy(o => o.date)
                    .ThenBy(o => o.time1)
                    .ToList();

                if (data1.Count() > 0)
                {
                    var minTime = data1.Min(o => o.time1);
                    var maxTime = data1.Max(o => o.time2);

                    var times = new List<TimeModel>();
                    var _index = 0;
                    //for (int i = 0; i < Math.Ceiling(((maxTime - minTime).TotalMinutes / 10)); i++)
                    //{
                    //    var t = minTime.Add(new TimeSpan(0, i * 10, 0));
                    //    times.Add(new TimeModel() { Index = _index++, Time = t.ToString("hh\\:mm") });
                    //}
                    for (int i = 0; i < (maxTime.Hours - minTime.Hours) + 1; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            var _t = new TimeSpan(minTime.Hours + i, j * 5, 0);
                            times.Add(new TimeModel() { Index = _index++, Time2 = _t });
                        }
                    }

                    //fix last to xx:00
                    var last = times.Last();
                    var t = last.Time2?.Add(new TimeSpan(0, 5, 0));
                    times.Add(new TimeModel()
                    {
                        Index = _index++,
                        // Time = t.ToString("hhm") ,
                        Time2 = t
                    });

                    foreach (var time in times)
                    {
                        var x = time.Time2?.ToString("hh\\:mm");
                        time.Time = x;
                    }

                    var timesDic = times.ToDictionary(o => o.Time, o => o.Index);

                    var result = data1.AsEnumerable()
                        .GroupBy(o => new { o.date, o.classroom })
                        .OrderBy(o => o.Key.date)
                        .ThenBy(o => Regex.Replace(o.Key.classroom, @"[^0-9]", "").ToNumber<double>())
                        .Select(g => new ResultModel2
                        {
                            Date = g.Key.date.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                            Class = g.Key.classroom,
                            //Class2 = Regex.Replace(g.Key.classroom, @"[^0-9]", ""),
                            IsHoliday = g.Any(o => o.isHoliday),
                            Schdule = g.Distinct().OrderBy(o => o.time1)
                                .Select(o => new ResultModel1
                                {
                                    Code = String.Join(" ", o.data.Select(i => i.code).ToArray()),
                                    Time1 = o.time1.ToString("hh\\:mm"),
                                    Time2 = o.time2.ToString("hh\\:mm"),
                                    Col = timesDic.ContainsKey(o.time1.ToString("hh\\:mm")) ? timesDic[o.time1.ToString("hh\\:mm")] : 0,
                                    ColSpan = (int)Math.Ceiling((o.diff.TotalMinutes / 5)),
                                    //IsHoliday = o.isHoliday,
                                    IsChecked = o.isChecked,
                                    Teacher = o.data.Select(i => i.teacher).FirstOrDefault(),
                                    //Teacher = String.Join(" ", o.data.Select(i => i.teacher).ToArray()),
                                }).ToList()
                        }).ToList();

                    return (result, times);
                }
                else
                {
                    return (new List<ResultModel2>(), new List<TimeModel>());
                }

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

            var times = result.Item2;
            var data = result.Item1;

            var userData = GetUserData();

            using (ExcelPackage excel = new ExcelPackage())
            {
                var sheetName = "รายงานการเช็กชื่อรายคาบ";
                excel.Workbook.Worksheets.Add(sheetName);

                var worksheet = excel.Workbook.Worksheets[sheetName];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                    worksheet.Cells[1, 1, 1, 2 + times.Count - 1].SetCellRange(isMerge: true
                      , text: school.sCompany, fontSize: 14, isBold: true);

                    worksheet.Cells[2, 1, 2, 2 + times.Count - 1].SetCellRange(isMerge: true
                     , text: $"รายงานการเช็กชื่อรายคาบ ระดับชั้น {search.levelNo} ภาคเรียนที่ {search.termNo} ปีการศึกษา {search.yearNo} "
                     , fontSize: 14, isBold: true);

                }

                var row = 3;

                worksheet.Row(row).Height = 30;
                worksheet.Cells[row, 1].SetCellRange(isHeader: true, text: $"วันที่{Environment.NewLine}Date");
                worksheet.Cells[row, 2].SetCellRange(isHeader: true, text: $"ระดับชั้น{Environment.NewLine}Class Level");

                var counter = 1;

                //for (int i = 0; i < times.Count; i++)
                for (int i = 0; i < times.Count - 6; i = i + 6)
                {
                    var range = worksheet.Cells[row, i + 3, row, i + 3 + 5];
                    range.SetCellRange(isHeader: true, isMerge: true);

                    //, text: $"คาบ {counter++}\r\n{times[i].Time}0 - {times[i + 6].Time}0"
                    range.IsRichText = true;
                    var rt = range.RichText;
                    ExcelRichText ert = rt.Add($"คาบ {counter++}{Environment.NewLine}");
                    ert.Color = Color.FromArgb(255, 255, 255);
                    ert = rt.Add($"{times[i].Time}0 - {times[i + 6].Time}0");
                    ert.Color = Color.FromArgb(255, 255, 255);
                }
                row++;

                foreach (var d in data)
                {
                    worksheet.Row(row).Height = 50;
                    worksheet.Cells[row, 1].SetCellRange(text: d.Date, border: ExcelBorderStyle.Thin);
                    worksheet.Cells[row, 2].SetCellRange(text: d.Class, border: ExcelBorderStyle.Thin);

                    if (d.IsHoliday == true)
                    {
                        worksheet.Cells[row, 3, row, times.Count + 1].SetCellRange(text: "วันหยุด", isMerge: true, border: ExcelBorderStyle.Thin);
                    }
                    else
                    {
                        counter = 1;

                        foreach (var s in d.Schdule)
                        {
                            while (counter < times.Count - 1)
                            {
                                if (s.Col == counter)
                                {
                                    var range = worksheet.Cells[row, 2 + s.Col, row, 2 + s.Col + s.ColSpan - 1];
                                    range.IsRichText = true;
                                    range.SetCellRange(border: ExcelBorderStyle.Thin, isMerge: true);
                                    var rt = range.RichText;
                                    ExcelRichText ert = rt.Add($"{s.Code}{Environment.NewLine}");
                                    ert = rt.Add($"{s.Teacher}{Environment.NewLine}");

                                    if (s.IsChecked == true)
                                    {
                                        ert = rt.Add("เช็กชื่อแล้ว");
                                        ert.Color = Color.FromArgb(0, 255, 0);
                                    }
                                    else
                                    {
                                        ert = rt.Add("ยังไม่ได้เช็กชื่อ");
                                        ert.Color = Color.FromArgb(255, 0, 0);
                                    }

                                    counter = counter + s.ColSpan - 1;
                                    break;
                                }
                                else
                                {
                                    //var range = worksheet.Cells[row, counter];
                                    //range.SetCellRange(border: ExcelBorderStyle.None);
                                    counter++;
                                }
                            }
                        }

                        var fix = worksheet.Cells[row, 2 + times.Count - 1];
                        fix.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }

                    row++;
                }

                for (int i = 0; i < times.Count; i++)
                {
                    var fix = worksheet.Cells[row, 2 + i];
                    fix.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                }
                worksheet.Cells.AutoFitColumns(5, 10);
                worksheet.Column(1).Width = 20;
                worksheet.Column(2).Width = 20;

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
            if (!FontFactory.IsRegistered("thsarabun1"))
            {
                var path = HttpContext.Current.Server.MapPath("~/Fonts/THSarabun.ttf");
                FontFactory.Register(path, "thsarabun1");
            }

            var userData = GetUserData();

            var result = InitData(search);

            var times = result.Item2;
            var data = result.Item1;

            string filename = $"รายงานการเช็กชื่อรายคาบ_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";

            byte[] docfinal = null;

            Document doc = new Document(PageSize.A4.Rotate(), 30, 30, 45, 25);

            using (MemoryStream finalStream = new MemoryStream())
            {
                var copy = new PdfCopy(doc, finalStream);
                doc.Open();

                using (MemoryStream ms1 = new MemoryStream())
                {
                    Document doc1 = new Document(PageSize.A4.Rotate(), 30, 30, 45, 25);

                    using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                    {
                        var font = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                        writer1.CloseStream = false;

                        using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                        {
                            var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                            writer1.PageEvent = new ITextEvents(school
                                , $"รายงานการเช็กชื่อรายคาบ ระดับชั้น {search.levelNo} ภาคเรียนที่ {search.termNo} ปีการศึกษา {search.yearNo} ");
                        }

                        doc1.Open();

                        PdfPTable table1 = new PdfPTable(2 + times.Count - 1);
                        table1.WidthPercentage = 99f;
                        var lstWidth = new List<float>(new float[] { 8, 8 });
                        for (int i = 0; i < times.Count - 1; i++)
                        {
                            lstWidth.Add(84 / (times.Count * 1f));
                        }
                        table1.SetTotalWidth(lstWidth.ToArray());

                        for (int i = 0; i < times.Count - 1 + 2; i++)
                        {
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: ""));
                        }

                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"วันที่{Environment.NewLine}Date"));
                        table1.AddCell(new PdfPCell().SetCellPDF(font, text: $"ระดับชั้น{Environment.NewLine}Class Level"));

                        var counter = 1;
                        for (int i = 0; i < times.Count - 6; i = i + 6)
                        {
                            table1.AddCell(new PdfPCell().SetCellPDF(font, colspan: 6
                                , text: $"คาบ {counter++}{Environment.NewLine}{times[i].Time}0 - {times[i + 6].Time}0"));
                        }

                        foreach (var d in data)
                        {
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: d.Date));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, text: d.Class));

                            if (d.IsHoliday == true)
                            {
                                table1.AddCell(new PdfPCell().SetCellPDF(font, text: "วันหยุด", colspan: times.Count));
                            }
                            else
                            {
                                counter = 0;

                                foreach (var s in d.Schdule)
                                {
                                    while (counter < times.Count - 1)
                                    {
                                        if (s.Col == counter)
                                        {
                                            PdfPTable table3 = new PdfPTable(1);
                                            table3.WidthPercentage = 99f;
                                            table3.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: s.Code
                                                , border: iTextSharp.text.Rectangle.NO_BORDER));
                                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: s.Teacher
                                                , border: iTextSharp.text.Rectangle.NO_BORDER));

                                            if (s.IsChecked == true)
                                            {
                                                var xfont = new iTextSharp.text.Font(font);
                                                xfont.SetColor(0, 255, 0);
                                                table3.AddCell(new PdfPCell().SetCellPDF(xfont, text: "เช็กชื่อแล้ว"
                                                    , border: iTextSharp.text.Rectangle.NO_BORDER));

                                            }
                                            else
                                            {
                                                var xfont = new iTextSharp.text.Font(font);
                                                xfont.SetColor(255, 0, 0);
                                                table3.AddCell(new PdfPCell().SetCellPDF(xfont, text: "ยังไม่ได้เช็กชื่อ"
                                                    , border: iTextSharp.text.Rectangle.NO_BORDER));

                                            }

                                            table1.AddCell(new PdfPCell(table3).SetCellPDF(font, colspan: s.ColSpan));
                                            counter = counter + s.ColSpan;

                                            break;
                                        }
                                        else
                                        {
                                            var cell = new PdfPCell().SetCellPDF(font, text: "");

                                            if (counter < times.Count - 2)
                                                cell.DisableBorderSide(iTextSharp.text.Rectangle.RIGHT_BORDER);
                                            cell.DisableBorderSide(iTextSharp.text.Rectangle.LEFT_BORDER);
                                            table1.AddCell(cell);
                                            counter++;
                                        }
                                    }
                                }

                                while (counter < times.Count - 1)
                                {
                                    var cell = new PdfPCell().SetCellPDF(font, text: "");

                                    if (counter < times.Count - 2)
                                        cell.DisableBorderSide(iTextSharp.text.Rectangle.RIGHT_BORDER);
                                    cell.DisableBorderSide(iTextSharp.text.Rectangle.LEFT_BORDER);
                                    table1.AddCell(cell);

                                    counter++;
                                }
                            }
                        }

                        doc1.Add(table1);

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

        public class ITextEvents : PdfPageEventHelper
        {
            iTextSharp.text.Font font;

            string summary1;
            TCompany school;
            public ITextEvents(TCompany school, string sum1)
            {
                this.school = school;
                summary1 = sum1;

                font = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            }
            // This is the contentbyte object of the writer  
            PdfContentByte cb;

            // we will put the final number of pages in a template  
            PdfTemplate headerTemplate, footerTemplate;



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
                    //iTextSharp.text.Font f = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 14, iTextSharp.text.Font.NORMAL);
                    //bf = BaseFont.CreateFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    //bf = font.BaseFont;
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
                    cb.SetFontAndSize(font.BaseFont, 10);

                    cb.SetTextMatrix(document.PageSize.GetLeft(30), document.PageSize.GetBottom(5));
                    var text1 = $"รายงานข้อมูล ณ วันที่ {dateNow}";
                    cb.ShowText(text1);

                    cb.SetTextMatrix(document.PageSize.GetRight(30), document.PageSize.GetBottom(5));
                    var text2 = $"{writer.PageNumber}/";
                    cb.ShowText(text2);
                    cb.EndText();
                    float len = font.BaseFont.GetWidthPoint(text2, 10);
                    // cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));document.PageSize.GetRight(20)
                    cb.AddTemplate(footerTemplate, document.PageSize.GetRight(30) + len, document.PageSize.GetBottom(5));// document.PageSize.GetBottom(20)
                }

                {
                    PdfPTable mainTable = new PdfPTable(1);
                    mainTable.TotalWidth = document.PageSize.Width - 30f;

                    mainTable.WidthPercentage = 90;
                    mainTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    PdfPTable table = new PdfPTable(1);
                    table.WidthPercentage = 100;
                    table.PaddingTop = 30;
                    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    table.AddCell(new PdfPCell().SetCellPDF(font,
                        text: school.sCompany,
                        horizotal: Element.ALIGN_CENTER,
                        fontSize: 16,
                        fontStyle: iTextSharp.text.Font.BOLD,
                        border: iTextSharp.text.Rectangle.NO_BORDER)
                    );

                    table.AddCell(new PdfPCell().SetCellPDF(font,
                        text: summary1,
                        horizotal: Element.ALIGN_CENTER,
                        fontSize: 14,
                        fontStyle: iTextSharp.text.Font.BOLD,
                        border: iTextSharp.text.Rectangle.NO_BORDER)
                    );

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
                footerTemplate.SetFontAndSize(font.BaseFont, 10);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber) + "");
                footerTemplate.EndText();
            }
        }
    }

}