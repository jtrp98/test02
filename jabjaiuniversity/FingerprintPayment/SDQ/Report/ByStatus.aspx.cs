using FingerprintPayment.PaymentGateway.KBank.KBankPromptPay;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.SDQ.Report
{
    public partial class ByStatus : BehaviorGateway
    {

        public class Search
        {
            public string term { get; set; }
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
                var logic = new SDQRepository(ctx);

                var result = logic.LoadReportSDQStatus(userData.CompanyID, search.term, search.level1, search.level2);

                var d = result.Select((o, i) => new
                {
                    Index = i + 1,
                    o.Room,
                    o.CountAll,
                    Done1 = o.Count1,
                    Not1 = o.CountAll - o.Count1,
                    Done2 = o.Count2,
                    Not2 = o.CountAll - o.Count2,
                    Done3 = o.Count3,
                    Not3 = o.CountAll - o.Count3,

                });

                return new
                {
                    data = d,
                    percent = new
                    {
                        Done1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Done1) * 100) / d.Sum(o => o.CountAll) : 0,
                        Not1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Not1) * 100) / d.Sum(o => o.CountAll) : 0,

                        Done2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Done2) * 100) / d.Sum(o => o.CountAll) : 0,
                        Not2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Not2) * 100) / d.Sum(o => o.CountAll) : 0,

                        Done3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Done3) * 100) / d.Sum(o => o.CountAll) : 0,
                        Not3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Not3) * 100) / d.Sum(o => o.CountAll) : 0,
                    },
                    summary = new
                    {
                        CountAll = d.Sum(o => o.CountAll),
                        Done1 = d.Sum(o => o.Done1),
                        Not1 = d.Sum(o => o.Not1),

                        Done2 = d.Sum(o => o.Done2),
                        Not2 = d.Sum(o => o.Not2),

                        Done3 = d.Sum(o => o.Done3),
                        Not3 = d.Sum(o => o.Not3),
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
                var logic = new SDQRepository(ctx);

                var result = logic.LoadReportSDQStatus(userData.CompanyID, term, level1, level2);

                var d = result.Select((o, i) => new
                {
                    Index = i + 1,
                    o.Room,
                    o.CountAll,
                    Done1 = o.Count1,
                    Not1 = o.CountAll - o.Count1,
                    Done2 = o.Count2,
                    Not2 = o.CountAll - o.Count2,
                    Done3 = o.Count3,
                    Not3 = o.CountAll - o.Count3,

                });

                var data = new
                {
                    data = d,
                    percent = new
                    {
                        Done1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Done1) * 100) / d.Sum(o => o.CountAll) : 0,
                        Not1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Not1) * 100) / d.Sum(o => o.CountAll) : 0,

                        Done2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Done2) * 100) / d.Sum(o => o.CountAll) : 0,
                        Not2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Not2) * 100) / d.Sum(o => o.CountAll) : 0,

                        Done3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Done3) * 100) / d.Sum(o => o.CountAll) : 0,
                        Not3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Not3) * 100) / d.Sum(o => o.CountAll) : 0,
                    },
                    summary = new
                    {
                        CountAll = d.Sum(o => o.CountAll),
                        Done1 = d.Sum(o => o.Done1),
                        Not1 = d.Sum(o => o.Not1),

                        Done2 = d.Sum(o => o.Done2),
                        Not2 = d.Sum(o => o.Not2),

                        Done3 = d.Sum(o => o.Done3),
                        Not3 = d.Sum(o => o.Not3),
                    },
                };

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานสรุปผลรายบุคคล");

                    var worksheet = excel.Workbook.Worksheets["รายงานสรุปผลรายบุคคล"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var row = 1;

                    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[row, 1, row++, 9]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);

                        SetCell(worksheet.Cells[row, 1, row++, 9]
                        , isMerge: true
                        , text: $"รายงานสรุปผลรายบุคคล"
                        );

                        SetCell(worksheet.Cells[row, 1, row++, 9]
                         , isMerge: true
                         , text: $"ปีการศึกษา {yearNo} ภาคเรียนที่ {termNo}"
                         );
                    }

                    //SetCell(worksheet.Cells[row, 1, row++, 7]
                    //    , isMerge: true
                    //    , text: $"รายชื่อนักเรียนทั้งหมด {data.count} คน"
                    //    );

                    //SetCell(worksheet.Cells[row, 1], isMerge: true, text: $"สถานะการประเมิน SDQ :");
                    //SetCell(worksheet.Cells[row, 2], text: $"ยังไม่ได้ประเมิน {SQUARE_UNICODE}", color: GRAY);
                    //SetCell(worksheet.Cells[row, 3], text: $"ประเมินแล้ว(ปกติ) {CHECK_UNICODE}", color: SUCCESS);
                    //SetCell(worksheet.Cells[row, 4], text: $"ประเมินแล้ว(เสี่ยง) {CHECK_UNICODE}", color: WARNING);
                    //SetCell(worksheet.Cells[row++, 5], text: $"ประเมินแล้ว(ปัญหา) {CHECK_UNICODE}", color: DANGER);

                    SetCell(worksheet.Cells[row, 1, row + 1, 1], isMerge: true, isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[row, 2, row + 1, 2], isMerge: true, isHeader: true, text: "ระดับชั้น");
                    SetCell(worksheet.Cells[row, 3, row + 1, 3], isMerge: true, isHeader: true, text: "จำนวนนักเรียน");
                    SetCell(worksheet.Cells[row, 4, row, 5], isMerge: true, isHeader: true, text: "นักเรียน");
                    SetCell(worksheet.Cells[row, 6, row, 7], isMerge: true, isHeader: true, text: "ครู");
                    SetCell(worksheet.Cells[row, 8, row, 9], isMerge: true, isHeader: true, text: "ผู้ปกครอง");
                    row++;

                    SetCell(worksheet.Cells[row, 4], isHeader: true, text: "ประเมินแล้ว");
                    SetCell(worksheet.Cells[row, 5], isHeader: true, text: "ยังไม่ประเมิน");
                    SetCell(worksheet.Cells[row, 6], isHeader: true, text: "ประเมินแล้ว");
                    SetCell(worksheet.Cells[row, 7], isHeader: true, text: "ยังไม่ประเมิน");
                    SetCell(worksheet.Cells[row, 8], isHeader: true, text: "ประเมินแล้ว");
                    SetCell(worksheet.Cells[row, 9], isHeader: true, text: "ยังไม่ประเมิน");
                    row++;

                    foreach (var r in data.data)
                    {

                        SetCell(worksheet.Cells[row, 1], text: r.Index + "");
                        SetCell(worksheet.Cells[row, 2], text: r.Room + "");
                        SetCell(worksheet.Cells[row, 3], text: r.CountAll + "");
                        SetCell(worksheet.Cells[row, 4], text: r.Done1 + "");
                        SetCell(worksheet.Cells[row, 5], text: r.Not1 + "");
                        SetCell(worksheet.Cells[row, 6], text: r.Done2 + "");
                        SetCell(worksheet.Cells[row, 7], text: r.Not2 + "");
                        SetCell(worksheet.Cells[row, 8], text: r.Done3 + "");
                        SetCell(worksheet.Cells[row, 9], text: r.Not3 + "");

                        row++;
                    }

                    SetCell(worksheet.Cells[row, 1, row, 2], isMerge: true, text: "รวมทั้งสิ้น");//\u2705
                    SetCell(worksheet.Cells[row, 3], text: data.summary.CountAll + "");
                    SetCell(worksheet.Cells[row, 4], text: data.summary.Done1 + "");
                    SetCell(worksheet.Cells[row, 5], text: data.summary.Not1 + "");
                    SetCell(worksheet.Cells[row, 6], text: data.summary.Done2 + "");
                    SetCell(worksheet.Cells[row, 7], text: data.summary.Not2 + "");
                    SetCell(worksheet.Cells[row, 8], text: data.summary.Done3 + "");
                    SetCell(worksheet.Cells[row, 9], text: data.summary.Not3 + "");
                    row++;

                    SetCell(worksheet.Cells[row, 1, row++, 9]
                     , isMerge: true
                     , horizotal: ExcelHorizontalAlignment.Left
                     , text: $"นักเรียนประเมินแล้ว คิดเป็นร้อยละ {data.percent.Done1?.ToString("0.00")}% / นักเรียนยังไม่ประเมิน คิดเป็นร้อยละ {data.percent.Not1?.ToString("0.00")}%"
                     );

                    SetCell(worksheet.Cells[row, 1, row++, 9]
                     , isMerge: true
                      , horizotal: ExcelHorizontalAlignment.Left
                     , text: $"ครูประเมินแล้ว คิดเป็นร้อยละ {data.percent.Done2?.ToString("0.00")}% / ครูยังไม่ประเมิน คิดเป็นร้อยละ {data.percent.Not2?.ToString("0.00")}% "
                     );

                    SetCell(worksheet.Cells[row, 1, row++, 9]
                    , isMerge: true
                     , horizotal: ExcelHorizontalAlignment.Left
                    , text: $"ผู้ปกครองประเมินแล้ว คิดเป็นร้อยละ {data.percent.Done3?.ToString("0.00")}% / ผู้ปกครองยังไม่ประเมิน คิดเป็นร้อยละ {data.percent.Not3?.ToString("0.00")}%"
                    );

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
        }

        private static void SetCell(ExcelRange xrange
          , bool isHeader = false
          , bool isMerge = false
          , string text = ""
          , int fontSize = 11
          , bool isBold = false
          , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
          , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
          , Color? color = null
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

                if (color.HasValue)
                {
                    xrange.Style.Font.Color.SetColor(color.Value);
                }

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