using Amazon.XRay.Recorder.Core;
using FingerprintPayment.Class;
using FingerprintPayment.Memory;
using FluentDateTime;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.ApplicationInsights.Channel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Utilities;
using OfficeOpenXml.Style;
using RestSharp;
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
    public partial class SystemHealthCheck : BehaviorGateway
    {
        //public class VMData
        //{
        //    public int sID { get; set; }
        //    public string title { get; set; }
        //    public string FullName { get; set; }
        //    public DateTime? moveInDate { get; set; }
        //    public string Room { get; set; }
        //    public string oldSchoolName { get; set; }
        //    public string sStudentID { get; set; }
        //}

        public class Search
        {
            public string method { get; set; }
            public DateTime? date1 { get; set; }
            public string time1 { get; set; }
            public string time2 { get; set; }
            public int page { get; set; }

        }
        private class ResponseModel
        {
            public DateTime? DeviceTime { get; set; }
            public double? ResponseTime { get; set; }
            public DateTime? APIProcessedTime { get; set; }
            public string Code { get; set; }
        }

        private class ResponseModel2
        {
            public DateTime? DeviceTime { get; set; }
            public double? ResponseTime { get; set; }
            public DateTime? APIProcessedTime { get; set; }
            public int? sID { get; set; }
            public int? sEmp { get; set; }
            public int? UserID { get; set; }
        }

        private class ApiResponseModel
        {
            public DateTime? Tstamp { get; set; }
            public int UserID { get; set; }
            public double? ResponseTime { get; set; }
            public DateTime? DeviceTime { get; set; }

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
        public static object LoadData(Search search)
        {
            var userData = GetUserData();
            using (var _context = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                //                var sql = $@"
                //SELECT B.DeviceTime , B.ResponseTime , B.APIProcessedTime ,  ISNULL(C.sStudentID , E.Code) 'Code'  --, A.UserID
                //  FROM [JabjaiSchoolSingleDB].[dbo].[UserAPIResponse] A
                //  JOIN [JabjaiSchoolSingleDB].[dbo].[UserAPIResponseDetails] B ON A.ID = B.ID 
                //  LEFT JOIN [JabjaiSchoolSingleDB].[dbo].TUser C ON A.UserID = C.sID AND A.SchoolID = C.SchoolID
                //  LEFT JOIN [JabjaiSchoolSingleDB].[dbo].TEmployeeInfo E ON A.UserID = E.sEmp AND A.SchoolID = E.SchoolID
                //WHERE A.SchoolID = {userData.CompanyID} 
                //AND B.MethodName = '{search.method}'
                //AND B.DeviceTime Between '{search.date1?.ToString("yyyyMMdd")} {search.time1}' AND '{search.date1?.ToString("yyyyMMdd")} {search.time2}'

                //";
                //var time1 = search.date1 + TimeSpan.Parse(search.time1);
                //var time2 = search.date1 + TimeSpan.Parse(search.time2);

                var sql = $@"EXEC [dbo].[GetAPIResponse]
	@SchoolID = {userData.CompanyID} ,
	@NoofRecord = {search.page},
	@MethodName = N'{search.method}',
	@ActionDate1 = '{search.date1?.ToString("yyyyMMdd")} {search.time1}',
	@ActionDate2 = '{search.date1?.ToString("yyyyMMdd")} {search.time2}'
";

                var result = _context.Database.SqlQuery<ResponseModel2>(sql).ToList();
                //var sidList = result.Select( o => new { o.UserID , o.sID}).ToList();
                //var empList = result.Select(o => o.sEmp).ToList();
                var userList = _context.TUser
                    .Where(o => o.SchoolID == userData.CompanyID && o.cDel == null)
                    .Select(o => new { o.sStudentID, o.sID })
                    .ToList();

                var empList = _context.TEmployeeInfoes
                  .Where(o => o.SchoolID == userData.CompanyID && o.cDel == false)
                  .Select(o => new { o.Code, o.sEmp })
                  .ToList();

                var data = (from a in result
                            from f in userList.Where(o => o.sID == a.UserID).DefaultIfEmpty()
                            from b in userList.Where(o => o.sID == a.sID).DefaultIfEmpty()
                            from c in empList.Where(o => o.sEmp == a.sEmp).DefaultIfEmpty()

                            select new
                            {
                                a.APIProcessedTime,
                                a.DeviceTime,
                                a.ResponseTime,
                                Code = (f?.sStudentID ?? b?.sStudentID ?? c?.Code) //+ " / " + a.UserID
                            })
                            .OrderBy(o => o.DeviceTime)
                            .ToList();
                return new
                {
                    data = data.Select((o, i) => new
                    {
                        Index = i + 1,
                        Transaction = o.DeviceTime?.ToString("HH:mm:ss"),
                        Transmission = o.APIProcessedTime?.ToString("HH:mm:ss"),
                        Processing = o.ResponseTime?.ToString("0.00"),
                        Code = o.Code,
                        Status1 = o.ResponseTime < 1000 ? "ปกติ" : "ไม่ปกติ",
                        Status2 = ((o.APIProcessedTime - o.DeviceTime)?.TotalSeconds <= 1 ? "ปกติ" : "ไม่ปกติ"),
                    }),

                };

                //var client = new RestClient($"https://paymentapi.schoolbright.co/api/device/api/showresponselog?SchoolID={userData.CompanyID}&UserID={userData.UserID}&NoofRecord={search.page}&MethodName={search.method}");
                //client.Timeout = -1;
                //var request = new RestRequest(Method.GET);
                //IRestResponse response = client.Execute(request);

                //if (!string.IsNullOrEmpty(response.Content))
                //{
                //    var content = response.Content;
                //    var result = JsonConvert.DeserializeObject<List<ApiResponseModel>>(content);

                //    var data = (from a in result
                //                from b in _context.TUsers.Where(o => o.sID == a.UserID && o.SchoolID == userData.CompanyID).DefaultIfEmpty()
                //                from c in _context.TEmployeeInfoes.Where(o => o.sEmp == a.UserID && o.SchoolID == userData.CompanyID).DefaultIfEmpty()

                //                select new
                //                {
                //                    a.Tstamp,
                //                    a.DeviceTime,
                //                    a.ResponseTime,
                //                    Code = (b?.sStudentID ?? c?.Code) //+ " / " + a.UserID
                //                }).ToList();
                //    return new
                //    {
                //        data = data.Select((o, i) => new
                //        {
                //            Index = i + 1,
                //            Transaction = o.Tstamp?.ToString("HH:mm:ss"),
                //            Transmission = o.DeviceTime?.ToString("HH:mm:ss"),
                //            Processing = o.ResponseTime?.ToString("0.00"),
                //            Code = o.Code,
                //            Status = o.ResponseTime > 1000 ? "ไม่ปกติ" : "ปกติ",
                //        }),

                //    };
                //}
                //else
                //{
                //    return new
                //    {
                //        data = new List<object> { }
                //    };
                //}


            }
        }

        //  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //  [WebMethod(EnableSession = true)]
        //  public static void ExportExcel1(string term, string year, string termno)
        //  {
        //      var userData = GetUserData();

        //      var result = GetData(term).GroupBy(o => o.Room)
        //          .Select(o => new
        //          {
        //              Room = o.Key,
        //              Count = o.Count(),
        //              Data = o.Select(i => new
        //              {
        //                  i.sID,
        //                  i.title,
        //                  i.FullName,
        //                  moveInDate = i.moveInDate.HasValue ? i.moveInDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : "-",
        //                  i.sStudentID,
        //                  oldSchoolName = string.IsNullOrWhiteSpace(i.oldSchoolName) ? "-" : i.oldSchoolName,
        //              })
        //          })
        //          .ToList();


        //      using (ExcelPackage excel = new ExcelPackage())
        //      {
        //          excel.Workbook.Worksheets.Add("รายงานนักเรียนใหม่");

        //          var worksheet = excel.Workbook.Worksheets["รายงานนักเรียนใหม่"];
        //          string entities = HttpContext.Current.Session["sEntities"].ToString();
        //          using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
        //          {
        //              var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

        //              SetCell(worksheet.Cells[1, 1, 1, 6]
        //                , isMerge: true
        //                , text: school.sCompany
        //                , fontSize: 14
        //                , isBold: true);

        //              SetCell(worksheet.Cells[2, 1, 2, 6]
        //               , isMerge: true
        //               , text: $"ปีการศึกษา {year} ภาคเรียนที่ {termno}"
        //               , fontSize: 14
        //               , isBold: true);
        //          }
        //          var row = 3;
        //          foreach (var room in result)
        //          {
        //              SetCell(worksheet.Cells[row, 1], isHeader: true, text: "ลำดับ");
        //              SetCell(worksheet.Cells[row, 2], isHeader: true, text: "เลขประจำตัว");
        //              SetCell(worksheet.Cells[row, 3], isHeader: true, text: "ชื่อ-นามสกุล");
        //              SetCell(worksheet.Cells[row, 4], isHeader: true, text: "ชั้น/ห้อง");
        //              SetCell(worksheet.Cells[row, 5], isHeader: true, text: "วันที่เข้าเรียน");
        //              SetCell(worksheet.Cells[row, 6], isHeader: true, text: "โรงเรียนเดิม");

        //              row++;

        //              int c = 1;
        //              foreach (var r in room.Data)
        //              {
        //                  SetCell(worksheet.Cells[row, 1], text: c + "");
        //                  SetCell(worksheet.Cells[row, 2], text: r.sStudentID);
        //                  SetCell(worksheet.Cells[row, 3], text: r.title + " " + r.FullName);
        //                  SetCell(worksheet.Cells[row, 4], text: room.Room);
        //                  SetCell(worksheet.Cells[row, 5], text: r.moveInDate);
        //                  SetCell(worksheet.Cells[row, 6], text: r.oldSchoolName);
        //                  c++;
        //                  row++;
        //              }

        //              SetCell(worksheet.Cells[row, 1, row, 6], isMerge: true, text: $"รวมนักเรียนเข้าใหม่ {room.Count} คน", horizotal: ExcelHorizontalAlignment.Right);
        //              row++;
        //          }

        //          worksheet.Cells.AutoFitColumns(20, 30);


        //          HttpContext.Current.Response.Clear();
        //          HttpContext.Current.Response.ContentType = "application/text;";
        //          HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
        //          HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
        //          HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        //          HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        //          HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

        //      }

        //  }

        //  private static void SetCell(ExcelRange xrange
        //    , bool isHeader = false
        //    , bool isMerge = false
        //    , string text = ""
        //    , int fontSize = 11
        //    , bool isBold = false
        //    , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
        //    , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
        //)
        //  {
        //      using (xrange)
        //      {
        //          xrange.Merge = isMerge;
        //          xrange.Value = text;
        //          xrange.Style.Font.Bold = isBold;
        //          xrange.Style.HorizontalAlignment = horizotal;
        //          xrange.Style.VerticalAlignment = vetical;
        //          xrange.Style.Font.Size = fontSize;

        //          if (isHeader)
        //          {
        //              xrange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //              xrange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //              xrange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //              xrange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //              xrange.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //              xrange.Style.Font.Color.SetColor(System.Drawing.Color.White);
        //              xrange.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
        //          }
        //          //    xrange.Style.Fill.BackgroundColor.SetColor(bgColor);

        //          xrange.AutoFitColumns();

        //      }
        //  }


    }

}