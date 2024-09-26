using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Utilities;
using OfficeOpenXml.Style;
using System.Text;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using static FingerprintPayment.Report.Report_QuantityStudent;

namespace FingerprintPayment.VisitHousePage.Web.Report
{
    public partial class Summary : BehaviorGateway
    {

        public class Search
        {
            public string yearNo { get; set; }
            public string term { get; set; }
            public string termNo { get; set; }
            public int level1 { get; set; }
            public int? level2 { get; set; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadData(Search search)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new VisitHomeRepository(ctx);

                var result = logic.LoadReportSummary(userData.CompanyID, search.term, search.level1, search.level2);

                var d = result//.GroupBy( o => new { o.Room})
                    .Select(o => new
                    {
                        o.Room,
                        o.CountAll,
                        o.Status1,
                        o.Status2,
                        o.Status3,
                    });

                return new
                {
                    data = d,
                    percent = new
                    {
                        Status1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Status1) * 100) / d.Sum(o => o.CountAll) : 0,
                        Status2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Status2) * 100) / d.Sum(o => o.CountAll) : 0,
                        Status3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Status3) * 100) / d.Sum(o => o.CountAll) : 0,
                    },
                    summary = new
                    {
                        CountAll = d.Sum(o => o.CountAll),
                        Status1 = d.Sum(o => o.Status1),
                        Status2 = d.Sum(o => o.Status2),
                        Status3 = d.Sum(o => o.Status3),
                    },
                };
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(string yearNo, string term, string termNo, int level1, int? level2)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new VisitHomeRepository(ctx);

                var result = logic.LoadReportSummary(userData.CompanyID, term, level1, level2);

                var d = result//.GroupBy( o => new { o.Room})
                    .Select(o => new
                    {
                        o.Room,
                        o.CountAll,
                        o.Status1,
                        o.Status2,
                        o.Status3,
                    });

                var data = new
                {
                    data = d,
                    percent = new
                    {
                        Status1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Status1) * 100) / d.Sum(o => o.CountAll) : 0,
                        Status2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Status2) * 100) / d.Sum(o => o.CountAll) : 0,
                        Status3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Status3) * 100) / d.Sum(o => o.CountAll) : 0,
                    },
                    summary = new
                    {
                        CountAll = d.Sum(o => o.CountAll),
                        Status1 = d.Sum(o => o.Status1),
                        Status2 = d.Sum(o => o.Status2),
                        Status3 = d.Sum(o => o.Status3),
                    },
                };

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานสรุปสถานะการเยี่ยมบ้าน");

                    var worksheet = excel.Workbook.Worksheets["รายงานสรุปสถานะการเยี่ยมบ้าน"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var row = 1;

                    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[row, 1, row++, 5]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);

                        SetCell(worksheet.Cells[row, 1, row++, 5]
                        , isMerge: true
                        , text: $"รายงานสรุปสถานะการเยี่ยมบ้าน"
                        );

                        SetCell(worksheet.Cells[row, 1, row++, 5]
                         , isMerge: true
                         , text: $"ปีการศึกษา {yearNo} ภาคเรียนที่ {termNo}"
                         );
                    }

                    SetCell(worksheet.Cells[row, 1], isHeader: true, text: "ระดับชั้น");
                    SetCell(worksheet.Cells[row, 2], isHeader: true, text: "จำนวนนักเรียน");
                    SetCell(worksheet.Cells[row, 3], isHeader: true, text: "เยี่ยมแล้ว");
                    SetCell(worksheet.Cells[row, 4], isHeader: true, text: "เยี่ยมแต่ไม่พบ");
                    SetCell(worksheet.Cells[row++, 5], isHeader: true, text: "ยังไม่เยี่ยม");

                    foreach (var r in data.data)
                    {
                        int c = 1;
                        SetCell(worksheet.Cells[row, 1], text: r.Room);
                        SetCell(worksheet.Cells[row, 2], text: r.CountAll + "");
                        SetCell(worksheet.Cells[row, 3], text: r.Status2 + "");
                        SetCell(worksheet.Cells[row, 4], text: r.Status3 + "");
                        SetCell(worksheet.Cells[row, 5], text: r.Status1 + "");
                        c++;
                        row++;
                    }

                    SetCell(worksheet.Cells[row, 1], text: "รวมทั้งสิ้น");//\u2705
                    SetCell(worksheet.Cells[row, 2], text: data.summary.CountAll + "");
                    SetCell(worksheet.Cells[row, 3], text: data.summary.Status2 + "");
                    SetCell(worksheet.Cells[row, 4], text: data.summary.Status3 + "");
                    SetCell(worksheet.Cells[row++, 5], text: data.summary.Status1 + "");

                    //SetCell(worksheet.Cells[row, 1, row, 6], isMerge: true, text: $"รวมนักเรียนเข้าใหม่ {room.Count} คน", horizotal: ExcelHorizontalAlignment.Right);

                    SetCell(worksheet.Cells[row, 1, row++, 5]
                       , isMerge: true
                       , horizotal: ExcelHorizontalAlignment.Left
                       , text: $"เยี่ยมแล้ว คิดเป็นร้อยละ {data.percent.Status2?.ToString("0.00")}%"
                       );

                    SetCell(worksheet.Cells[row, 1, row++, 5]
                     , isMerge: true
                      , horizotal: ExcelHorizontalAlignment.Left
                     , text: $"เยี่ยมแต่ไม่พบ คิดเป็นร้อยละ {data.percent.Status3?.ToString("0.00")}%"
                     );

                    SetCell(worksheet.Cells[row, 1, row++, 5]
                    , isMerge: true
                     , horizotal: ExcelHorizontalAlignment.Left
                    , text: $"ยังไม่เยี่ยม คิดเป็นร้อยละ {data.percent.Status1?.ToString("0.00")}%"
                    );

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