using Amazon.XRay.Recorder.Core;
using FingerprintPayment.Class;
using FingerprintPayment.Memory;
using FluentDateTime;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Utilities;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity.Injection;
using urbanairship;

namespace FingerprintPayment.Report
{
    public partial class NewStudent : BehaviorGateway
    {
        public class VMData
        {
            public int sID { get; set; }
            public string title { get; set; }
            public string FullName { get; set; }
            public DateTime? moveInDate { get; set; }
            public string Room { get; set; }
            public string oldSchoolName { get; set; }
            public string sStudentID { get; set; }
        }

        public class VMData2
        {
            public int sID { get; set; }
            public string nTerm { get; set; }
        }

        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var userData = GetUserData();

                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)), userData);

                    //fcommon.LinqToDropDownList(q, ddlLevel1, "เลือก", "class_id", "class_name");

                }
            }
        }


        private static List<VMData> GetData(string term, DateTime date1, DateTime date2)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var _term = db.TTerms.Where(o => o.SchoolID == userData.CompanyID && o.nTerm == term).FirstOrDefault();
                if (_term != null)
                {

                    var sql1 = $@"
SELECT T1.sID , T1.SubLevel +'/' + T1.nTSubLevel2 'Room',  T1.sStudentID ,  T1.titleDescription 'title', T1.sName+ ' '+ T1.sLastname 'FullName', T1.moveInDate 
	,B.sortValue, C.oldSchoolName 
FROM [JabjaiSchoolSingleDB].dbo.TB_StudentViews T1
JOIN [JabjaiSchoolSingleDB].dbo.TLevel B ON T1.SchoolID = B.SchoolID AND B.LevelID = T1.nTLevel
JOIN [JabjaiSchoolSingleDB].dbo.TUser C ON T1.SchoolID = C.SchoolID AND T1.sID = C.sID
WHERE T1.SchoolID={userData.CompanyID} AND ISNULL(T1.cDel,'0') = '0' AND T1.nTerm='{term}' and ISNULL(T1.nStudentStatus,0)=0 
and ((T1.moveInDate is not null and T1.moveInDate between '{date1.ToString("yyyyMMdd")}' AND '{date2.ToString("yyyyMMdd")}') 
    --OR (T1.moveInDate is null)
)
ORDER BY  B.sortValue , T1.SubLevel +'/' + T1.nTSubLevel2 ASC";

                    var d1 = db.Database.SqlQuery<VMData>(sql1).AsQueryable().ToList();

                    var sql2 = $@"
SELECT sID
	FROM TStudentClassroomHistory
	WHERE SchoolID={userData.CompanyID}  AND ISNULL(cDel,'0') = '0' AND nTerm='{term}' 
	AND sID NOT IN (
		SELECT sch.sID
		FROM TStudentClassroomHistory sch 
		LEFT JOIN TTerm t ON sch.SchoolID = t.SchoolID AND sch.nTerm = t.nTerm
		WHERE sch.SchoolID={userData.CompanyID}  AND sch.cDel='0' AND '{_term.dStart?.ToString("yyyyMMdd")}' > t.dStart AND sch.sID NOT IN (select sID from TStudentClassroomHistory where SchoolID={userData.CompanyID} AND (nStudentStatus=4 OR nStudentStatus=2) and cDel='0' )
	)
	AND sID NOT IN (
		SELECT sch.sID--, sch.nTerm, sch2.nTerm, t2.dStart, t.dStart
		FROM TStudentClassroomHistory sch 
		LEFT JOIN TTerm t ON sch.SchoolID = t.SchoolID AND sch.nTerm = t.nTerm
		LEFT JOIN TStudentClassroomHistory sch2 ON sch.sID = sch2.sID
		LEFT JOIN TTerm t2 ON sch2.SchoolID = t2.SchoolID AND sch2.nTerm = t2.nTerm
		WHERE sch.SchoolID={userData.CompanyID}  AND sch.cDel='0' AND sch2.cDel='0' AND (sch.nStudentStatus=4 OR sch.nStudentStatus=2) AND ISNULL(sch2.nStudentStatus,0)=0 AND t2.dStart > t.dStart AND t2.dStart < '{_term.dStart?.ToString("yyyyMMdd")}'
	
	) ";
                    //var d2 = db.Database.SqlQuery<int>(sql2).ToList();

                    //var data = d1.Where(o => d2.Contains(o.sID)).ToList();
                    var data = d1.ToList();

                    return data;
                }
                else
                {
                    return new List<VMData>();
                }

            }

        }

        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        [ScriptMethod()]
        public static object GetData1(string term, DateTime date1, DateTime date2)
        {
            var result = GetData(term, date1, date2);

            return result.GroupBy(o => o.Room)
                .Select(o => new
                {
                    Room = o.Key,
                    Count = o.Count(),
                    Data = o.Select(i => new
                    {
                        i.sID,
                        i.title,
                        i.FullName,
                        moveInDate = i.moveInDate.HasValue ? i.moveInDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : "-",
                        i.sStudentID,
                        oldSchoolName = string.IsNullOrWhiteSpace(i.oldSchoolName) ? "-" : i.oldSchoolName,

                    })
                })
                .ToList();
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel1(string term, string year, string termno, DateTime date1, DateTime date2)
        {
            var userData = GetUserData();

            var result = GetData(term, date1, date2).GroupBy(o => o.Room)

                .Select(o => new
                {
                    Room = o.Key,
                    Count = o.Count(),
                    Data = o.Select(i => new
                    {
                        i.sID,
                        i.title,
                        i.FullName,
                        moveInDate = i.moveInDate.HasValue ? i.moveInDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : "-",
                        i.sStudentID,
                        oldSchoolName = string.IsNullOrWhiteSpace(i.oldSchoolName) ? "-" : i.oldSchoolName,
                    })
                })
                .ToList();


            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("รายงานนักเรียนใหม่");

                var worksheet = excel.Workbook.Worksheets["รายงานนักเรียนใหม่"];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                    SetCell(worksheet.Cells[1, 1, 1, 6]
                      , isMerge: true
                      , text: school.sCompany
                      , fontSize: 14
                      , isBold: true);

                    SetCell(worksheet.Cells[2, 1, 2, 6]
                     , isMerge: true
                     , text: $"ปีการศึกษา {year} ภาคเรียนที่ {termno}"
                     , fontSize: 14
                     , isBold: true);
                }
                var row = 3;
                foreach (var room in result)
                {
                    SetCell(worksheet.Cells[row, 1], isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[row, 2], isHeader: true, text: "เลขประจำตัว");
                    SetCell(worksheet.Cells[row, 3], isHeader: true, text: "ชื่อ-นามสกุล");
                    SetCell(worksheet.Cells[row, 4], isHeader: true, text: "ชั้น/ห้อง");
                    SetCell(worksheet.Cells[row, 5], isHeader: true, text: "วันที่เข้าเรียน");
                    SetCell(worksheet.Cells[row, 6], isHeader: true, text: "โรงเรียนเดิม");

                    row++;

                    int c = 1;
                    foreach (var r in room.Data)
                    {
                        SetCell(worksheet.Cells[row, 1], text: c + "");
                        SetCell(worksheet.Cells[row, 2], text: r.sStudentID);
                        SetCell(worksheet.Cells[row, 3], text: r.title + " " + r.FullName);
                        SetCell(worksheet.Cells[row, 4], text: room.Room);
                        SetCell(worksheet.Cells[row, 5], text: r.moveInDate);
                        SetCell(worksheet.Cells[row, 6], text: r.oldSchoolName);
                        c++;
                        row++;
                    }

                    SetCell(worksheet.Cells[row, 1, row, 6], isMerge: true, text: $"รวมนักเรียนเข้าใหม่ {room.Count} คน", horizotal: ExcelHorizontalAlignment.Right);
                    row++;
                }

                worksheet.Cells.AutoFitColumns(20, 30);


                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/text;";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

            }

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

    }

}