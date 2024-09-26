using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
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
    public partial class ByStudentSDQType : BehaviorGateway
    {
        private static Color SUCCESS = Color.FromArgb(76, 175, 80);
        private static Color WARNING = Color.FromArgb(255, 152, 0);
        private static Color DANGER = Color.FromArgb(244, 67, 54);
        private static Color GRAY = Color.FromArgb(153, 153, 153);

        private static string CHECK_UNICODE = "\u2705";
        private static string SQUARE_UNICODE = "\u25FE";

        public class Search
        {
            public string term { get; set; }
            public int level1 { get; set; }
            public int? level2 { get; set; }
            public string name { get; set; }
            public int type { get; set; }
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

                var result = logic.LoadReportStudentByType(userData.CompanyID, search.term, search.level1, search.level2, search.name);

                return new
                {
                    data = result.Select((o, i) => new
                    {
                        Index = i + 1,
                        o.sId,
                        o.Number,
                        o.Code,
                        o.FullName,
                        o.Room,
                        o.Score1,
                        o.Score2,
                        o.Score3,
                    }),
                    count = result.Count(),
                };

            }
            
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(string yearNo, string term, string termNo, int level1, int? level2 , string name)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new SDQRepository(ctx);

                var result = logic.LoadReportStudentByType(userData.CompanyID, term, level1, level2, name);

                var data = new
                {
                    data = result.Select((o, i) => new
                    {
                        Index = i + 1,
                        o.sId,
                        o.Number,
                        o.Code,
                        o.FullName,
                        o.Room,
                        o.Score1,
                        o.Score2,
                        o.Score3,
                    }),
                    count = result.Count(),
                };

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานสถานะการประเมินรายคน");

                    var worksheet = excel.Workbook.Worksheets["รายงานสถานะการประเมินรายคน"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var row = 1;

                    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[row, 1, row++, 7]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);

                        SetCell(worksheet.Cells[row, 1, row++, 7]
                        , isMerge: true
                        , text: $"รายงานสถานะการประเมินรายคน"
                        );

                        SetCell(worksheet.Cells[row, 1, row++, 7]
                         , isMerge: true
                         , text: $"ปีการศึกษา {yearNo} ภาคเรียนที่ {termNo}"
                         );
                    }

                    SetCell(worksheet.Cells[row, 1, row++, 7]
                        , isMerge: true
                        , text: $"รายชื่อนักเรียนทั้งหมด {data.count} คน"
                        );

                    SetCell(worksheet.Cells[row, 1], isMerge: true, text: $"สถานะการประเมิน SDQ :");
                    SetCell(worksheet.Cells[row, 2], text: $"ยังไม่ได้ประเมิน {SQUARE_UNICODE}", color:GRAY);
                    SetCell(worksheet.Cells[row, 3], text: $"ประเมินแล้ว(ปกติ) {CHECK_UNICODE}", color: SUCCESS);
                    SetCell(worksheet.Cells[row, 4], text: $"ประเมินแล้ว(เสี่ยง) {CHECK_UNICODE}", color: WARNING);
                    SetCell(worksheet.Cells[row++, 5], text: $"ประเมินแล้ว(ปัญหา) {CHECK_UNICODE}", color: DANGER);

                    SetCell(worksheet.Cells[row, 1], isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[row, 2], isHeader: true, text: "รหัสนักเรียน");
                    SetCell(worksheet.Cells[row, 3], isHeader: true, text: "ชั้นเรียน");
                    SetCell(worksheet.Cells[row, 4], isHeader: true, text: "ชื่อ-นามสกุล");
                    SetCell(worksheet.Cells[row, 5], isHeader: true, text: "นักเรียน");
                    SetCell(worksheet.Cells[row, 6], isHeader: true, text: "ครู");
                    SetCell(worksheet.Cells[row++, 7], isHeader: true, text: "ผู้ปกครอง");

                    foreach (var r in data.data)
                    {
                       
                        SetCell(worksheet.Cells[row, 1], text: r.Index + "");
                        SetCell(worksheet.Cells[row, 2], text: r.Code + "");
                        SetCell(worksheet.Cells[row, 3], text: r.Room + "");
                        SetCell(worksheet.Cells[row, 4], text: r.FullName + "");

                        var c1 = CalcStatus1(r.Score1);
                        SetCell(worksheet.Cells[row, 5], text:c1.Item2 , color:c1.Item1 , fontSize:(c1.Item2 == SQUARE_UNICODE ? 18 : 14) );
                        var c2 = CalcStatus2(r.Score2);
                        SetCell(worksheet.Cells[row, 6], text: c2.Item2, color: c2.Item1, fontSize: (c2.Item2 == SQUARE_UNICODE ? 18 : 14));
                        var c3 = CalcStatus2(r.Score3);
                        SetCell(worksheet.Cells[row, 7], text: c3.Item2, color: c3.Item1, fontSize: (c3.Item2 == SQUARE_UNICODE ? 18 : 14));
                        row++;
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
        }


        private static (Color, string) CalcStatus1(int? score)
        {
            if (score.HasValue)
            {
                if (score >= 0 && score <= 16)
                    return (SUCCESS,CHECK_UNICODE);
                else if (score >= 17 && score <= 18)
                    return (WARNING, CHECK_UNICODE);
                else
                    return (DANGER, CHECK_UNICODE);
            }
            else
            {
                return (GRAY, SQUARE_UNICODE);
            }
        }

        private static (Color, string) CalcStatus2(int? score)
        {
            if (score.HasValue)
            {
                if (score >= 0 && score <= 15)
                    return (SUCCESS, CHECK_UNICODE);
                else if (score >= 16 && score <= 17)
                    return (WARNING, CHECK_UNICODE);
                else
                    return (DANGER, CHECK_UNICODE);
            }
            else
            {
                return (GRAY, SQUARE_UNICODE);
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