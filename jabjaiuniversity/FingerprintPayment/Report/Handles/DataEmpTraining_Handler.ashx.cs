using FingerprintPayment.Report.Models;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using FingerprintPayment.Report.Functions.Reports_06;
using OfficeOpenXml.Style;
using System.Globalization;
using JabjaiMainClass;
using FingerprintPayment.grade;
using System.Drawing;
using System.Net;
using FingerprintPayment.Helper;

namespace FingerprintPayment.Report.Handles
{
    /// <summary>
    /// Summary description for Reports03_exportHandler
    /// </summary>
    public class DataEmpTraining_Handler : IHttpHandler, IRequiresSessionState
    {
        JWTToken.userData userData;
        TCompany school;
        public void ProcessRequest(HttpContext context)
        {
            var c = context.Request["c"] + "";
            var sEmp = ToNullableInt(context.Request["emp"] + "");
            var type = context.Request["type"] + "";
            var start = context.Request["dstart"] + "";
            var end = context.Request["dend"] + "";
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }

            var d = new List<Model1>();
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var q = db.TEmpTrainings.Where(o => o.SchoolID == userData.CompanyID && o.cDel == false && o.sEmp == sEmp)
                    .Select(o => new
                    {
                        start = o.StartDate,
                        end = o.EndDate,
                        type = o.TrainingType,
                        project = o.ProjectName,
                        training = o.TrainingName,
                        place = o.Place,
                        hour = o.TrainingHours,
                    })
                    ;

                if (!string.IsNullOrEmpty(type))
                {
                    q = q.Where(o => o.type == type);
                }

                if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                {
                    var _start = DateTime.ParseExact(start, "ddMMyyyy", new CultureInfo("th-TH"));
                    var _end = DateTime.ParseExact(end, "ddMMyyyy", new CultureInfo("th-TH"));
                    q = q.Where(o => (_start >= o.start && _start <= o.end) ||
                        (_end >= o.start && _end <= o.end) ||
                        (_start >= o.start && _end <= o.end) ||
                        (o.start >= _start && o.end <= _end)
                    );
                }

                d = q
                   .AsEnumerable()
                   .Select((o, i) => new Model1
                   {
                       index = i + 1,
                       date = o.start?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) + "-" + o.end?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                       type = GetType(o.type),
                       project = o.project,
                       training = o.training,
                       place = o.place,
                       hour = o.hour,
                   }).ToList();
            }

            if (c == "data")
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.Write(JsonConvert.SerializeObject(new { data = d }, Formatting.None));
                //context.Response.End();
            }

            else if (c == "report")
            {
                ExcelPackage excel = new ExcelPackage();

                school = new TCompany();
                using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    school = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
                }

                string emp = "";
                using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                {
                    emp = (from a in db.TEmployees.Where(o => o.sEmp == sEmp && o.SchoolID == userData.CompanyID)
                           from b in db.TEmployeeInfoes.Where(o => o.sEmp == a.sEmp && o.SchoolID == a.SchoolID).DefaultIfEmpty()
                           select b.Code + " " + a.sName + " " + a.sLastname
                          )
                          .FirstOrDefault();

                }

                string worksheetName = $@"รายงานการฝึกอบรม";

                var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
                // var worksheet = excel.Workbook.Worksheets[sheet];//[worksheetName];
                //worksheet.DefaultColWidth = 50;

                string s = "-", e = "-";
                if (!string.IsNullOrEmpty(start))
                {
                    var _start = DateTime.ParseExact(start, "ddMMyyyy", new CultureInfo("en-US"));
                    s = _start.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                }

                if (!string.IsNullOrEmpty(end))
                {
                    var _end = DateTime.ParseExact(end, "ddMMyyyy", new CultureInfo("en-US"));
                    e = _end.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                }

                SetSheetHeader(worksheet, 7, $"วันที่เริ่มต้น:{s}   วันที่สิ้นสุด:{e}   ประเภทอบรม: {GetType(type)}   รหัสบุคลากร: {emp}");

                SetCell(worksheet.Cells[6, 1], text: "ลำดับ");
                SetCell(worksheet.Cells[6, 2], text: "วันที่");
                SetCell(worksheet.Cells[6, 3], text: "ประเภท");
                SetCell(worksheet.Cells[6, 4], text: "หลักสูตร");
                SetCell(worksheet.Cells[6, 5], text: "เรื่อง");
                SetCell(worksheet.Cells[6, 6], text: "สถานที่อบรม");
                SetCell(worksheet.Cells[6, 7], text: "จำนวนชั่วโมง");

                for (int row = 0; row < d.Count; row++)
                {
                    var data = d[row];
                    //SetCell(worksheet.Cells[7 + row, 1], text: (row + 1) + "");
                    SetCell(worksheet.Cells[7 + row, 1], text: data.index + "");
                    SetCell(worksheet.Cells[7 + row, 2], text: data.date);
                    SetCell(worksheet.Cells[7 + row, 3], text: data.type);
                    SetCell(worksheet.Cells[7 + row, 4], text: data.project);
                    SetCell(worksheet.Cells[7 + row, 5], text: data.training);
                    SetCell(worksheet.Cells[7 + row, 6], text: data.place);
                    SetCell(worksheet.Cells[7 + row, 7], text: data.hour + "");

                }

                //worksheet.Column(2).Width = 100;
                //worksheet.Column(3).Width = 100;

                worksheet.Cells.AutoFitColumns(15, 30);

                context.Response.Clear();
                context.Response.AddHeader("content-disposition", "attachment; filename=รายงานการฝึกอบรม_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                context.Response.ContentType = "application/text";
                context.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                context.Response.BinaryWrite(excel.GetAsByteArray());
                context.Response.Flush(); // Sends all currently buffered output to the client.
                context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
            }
        }

        private string GetType(string type)
        {
            switch (type)
            {
                case "1":
                    return "อบรม";
                case "2":
                    return "สัมมนา";
                case "3":
                    return "ประชุม";
                case "4":
                    return "ศึกษาดูงาน";
                default:
                    return "ทั้งหมด";
            }
        }

        private void SetSheetHeader(ExcelWorksheet worksheet, int tableCol, string title1)
        {
            if (!string.IsNullOrEmpty(school.sImage))
            {
                var request = WebRequest.Create(school.sImage);

                Image logo;

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    logo = Image.FromStream(stream);
                }

                using (logo)
                {
                    var excelImage = worksheet.Drawings.AddPicture("School Logo", logo);

                    excelImage.SetPosition(0, 5, 0, 5);
                    excelImage.SetSize(100, 100);
                }
            }

            SetCell(worksheet.Cells[1, 1, 5, 1], true);

            SetCell(worksheet.Cells[1, 2, 2, tableCol]
                , isMerge: true
                , text: school.sCompany
                , horizotal: ExcelHorizontalAlignment.Center
                , fontSize: 14
                , isBold: true);

            SetCell(worksheet.Cells[3, 2, 4, tableCol]
                , isMerge: true
                , text: "รายงานการฝึกอบรมแยกตามรหัสบุคลากร"
                , horizotal: ExcelHorizontalAlignment.Center
                , fontSize: 12
                , isBold: true);

            SetCell(worksheet.Cells[5, 2, 5, tableCol]
                , isMerge: true
                , text: title1
                , horizotal: ExcelHorizontalAlignment.Center
                , fontSize: 12
                , isBold: false);
        }


        private void SetCell(ExcelRange xrange
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

                xrange.AutoFitColumns();

            }
        }

        private int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private class Model1
        {
            public int index { get; set; }
            public string date { get; set; }
            public string type { get; set; }
            public string project { get; set; }
            public string training { get; set; }
            public string place { get; set; }
            public decimal? hour { get; set; }
        }
    }
}