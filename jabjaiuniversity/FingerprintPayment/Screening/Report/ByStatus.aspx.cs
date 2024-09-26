using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace FingerprintPayment.Screening.Report
{
    public partial class ByStatus : BehaviorGateway
    {
        private static Color SUCCESS = Color.FromArgb(76, 175, 80);
        private static Color INFO = Color.FromArgb(188, 212, 1);
        private static Color DANGER = Color.FromArgb(244, 67, 54);
        private static Color GRAY = Color.FromArgb(153, 153, 153);
        private static Color BLACK = Color.FromArgb(0, 0, 0);

        private static string CHECK_UNICODE = "\u2705";
        private static string SQUARE_UNICODE = "\u25FE";

        private static string STATUS1 = "ยังไม่ได้ประเมิน";
        private static string STATUS2 = "ประเมินแล้ว";

        public class Search
        {
            public string term { get; set; }
            public int level1 { get; set; }
            public int? level2 { get; set; }
            public string name { get; set; }
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
                var logic = new ScreeningRepository(ctx);

                var result = logic.LoadReportStatus(userData.CompanyID, search.term, search.level1, search.level2, search.name);

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
                        o.Status
                    }),
                    countAll = result.Count,
                };

            }

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(string yearNo, string term, string termNo, int level1, int? level2, string name)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new ScreeningRepository(ctx);

                var result = logic.LoadReportStatus(userData.CompanyID, term, level1, level2, name);

                var data = result.Select((o, i) => new
                {
                    Index = i + 1,
                    o.sId,
                    o.Number,
                    o.Code,
                    o.FullName,
                    o.Room,
                    o.Status
                });

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานสถานะการประเมินรายคน");

                    var worksheet = excel.Workbook.Worksheets["รายงานสถานะการประเมินรายคน"];
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
                        , text: $"รายงานสถานะการประเมินรายคน"
                        );

                        SetCell(worksheet.Cells[row, 1, row++, 5]
                         , isMerge: true
                         , text: $"ปีการศึกษา {yearNo} ภาคเรียนที่ {termNo}"
                         );
                    }
                    SetCell(worksheet.Cells[row, 1, row++, 5], isMerge: true, horizotal: ExcelHorizontalAlignment.Left, text: $"รายชื่อนักเรียนทั้งหมด " + data.Count() + " คน");


                    worksheet.Cells[row, 1, row, 5].IsRichText = true;
                    worksheet.Cells[row, 1, row, 5].Merge = true;
                    var rt = worksheet.Cells[row, 1, row, 5].RichText;
                    
                    ExcelRichText ert = rt.Add("สถานะการประเมิน : ");
                    //ert.Bold = true;

                    ert = rt.Add(" ยังไม่ได้ประเมิน ");
                    ert.Color = BLACK;
                    ert = rt.Add(SQUARE_UNICODE);
                    ert.Color = GRAY;
                                       
                    ert = rt.Add(" ประเมินแล้ว ");
                    ert.Color = BLACK;
                    ert = rt.Add(CHECK_UNICODE);
                    ert.Color = SUCCESS;

                    row++;

                    SetCell(worksheet.Cells[row, 1], isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[row, 2], isHeader: true, text: "รหัสนักเรียน");
                    SetCell(worksheet.Cells[row, 3], isHeader: true, text: "ชั้นเรียน");
                    SetCell(worksheet.Cells[row, 4], isHeader: true, text: "ชื่อ-นามสกุล");
                    SetCell(worksheet.Cells[row++, 5], isHeader: true, text: "สถานะ");

                    foreach (var r in data)
                    {

                        SetCell(worksheet.Cells[row, 1], text: r.Index + "");
                        SetCell(worksheet.Cells[row, 2], text: r.Code + "");
                        SetCell(worksheet.Cells[row, 3], text: r.Room + "");
                        SetCell(worksheet.Cells[row, 4], text: r.FullName + "");
                                              
                        var c = CalcStatus(r.Status);

                        SetCell(worksheet.Cells[row, 5], text: c.Item2/*, color: c.Item1*/);

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


        private static (Color, string) CalcStatus(int? score)
        {
            if (score == 1)
            {
                return (SUCCESS, STATUS2);
            }
            else
            {
                return (GRAY, STATUS1);
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