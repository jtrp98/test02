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
    public partial class ResignStudent : BehaviorGateway
    {
        public class VMData
        {
            public int sID { get; set; }
            public string title { get; set; }
            public string FullName { get; set; }
            public DateTime? moveInDate { get; set; }
            public string Room { get; set; }
           // public string oldSchoolName { get; set; }
            public string sStudentID { get; set; }
            public DateTime? MoveOutDate { get; set; }
            public string moveOutReason { get; set; }
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


        private static List<VMData> GetData(string term)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var sql = $@"

SELECT DISTINCT *
FROM (
	SELECT u.sID , sl.SubLevel +'/' + tsl.nTSubLevel2 'Room', B.sortValue , u.sStudentID , ISNULL(sch.MoveOutDate ,u.DayQuit) 'MoveOutDate' , ISNULL(u.Note,'') 'moveOutReason' , u.moveInDate , u.sName+ ' '+ u.sLastname 'FullName' , ISNULL(tt.titleDescription, u.sStudentTitle)  'title'
	FROM TUser u 
    LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
    LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
    LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
    LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
    LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
    LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
    LEFT JOIN TStudentHIstory sh ON sch.sID = sh.sID AND sch.nTermSubLevel2 = sh.nTermSubLevel2_OLD AND sch.nTerm = sh.nTerm AND u.SchoolID = sch.SchoolID
	LEFT JOIN[JabjaiSchoolSingleDB].dbo.TLevel B ON sl.SchoolID = B.SchoolID AND B.LevelID = sl.nTLevel
	WHERE u.SchoolID = {userData.CompanyID}  
	and sch.nTerm = '{term}'
	and sch.nStudentStatus = '2' 
	--and sch.cDel = 0
	and ISNULL(u.cDel, '0') <> '1'
)T
ORDER BY T.sortValue , T.Room  , T.sStudentID ASC
";

                var data = db.Database.SqlQuery<VMData>(sql).ToList();
                return data;

            }

        }

        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        [ScriptMethod()]
        public static object GetData1(string term)
        {
            var result = GetData(term);

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
                        moveOutDate = i.MoveOutDate.HasValue ? i.MoveOutDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : "-",
                        i.moveOutReason
                    })
                })
                .ToList();
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel1(string term, string year, string termno)
        {
            var userData = GetUserData();

            var result = GetData(term).GroupBy(o => o.Room)
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
                        moveOutDate = i.MoveOutDate.HasValue ? i.MoveOutDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : "-",
                        i.moveOutReason
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
                    SetCell(worksheet.Cells[row, 6], isHeader: true, text: "วันที่ลาออก");
                    SetCell(worksheet.Cells[row, 7], isHeader: true, text: "สาเหตุการลาออก");

                    row++;

                    int c = 1;
                    foreach (var r in room.Data)
                    {
                        SetCell(worksheet.Cells[row, 1], text: c + "");
                        SetCell(worksheet.Cells[row, 2], text: r.sStudentID);
                        SetCell(worksheet.Cells[row, 3], text: r.title + " " +r.FullName);
                        SetCell(worksheet.Cells[row, 4], text: room.Room);
                        SetCell(worksheet.Cells[row, 5], text: r.moveInDate);
                        SetCell(worksheet.Cells[row, 6], text: r.moveOutDate);
                        SetCell(worksheet.Cells[row, 7], text: r.moveOutReason);
                        c++;
                        row++;
                    }

                    SetCell(worksheet.Cells[row,1,row, 6] , isMerge:true, text: $"รวมนักเรียนลาออก {room.Count} คน", horizotal: ExcelHorizontalAlignment.Right);
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