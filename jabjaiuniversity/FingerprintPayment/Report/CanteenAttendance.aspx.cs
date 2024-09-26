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
    public partial class CanteenAttendance : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var userData = GetUserData();

                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)), userData);

                    fcommon.LinqToDropDownList(q, ddlLevel1, "เลือก", "class_id", "class_name");

                }
            }
        }

        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object GetData1(Search search)
        //(int? shop_id, DateTime? date1, DateTime? date2)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var result = GetQuery(userData, search, db);

                //var result = q.ToList();
                var data = result
                    //.GroupBy(o => new { o.shop, o.date.Date, o.buyer, o.level })
                    .OrderBy(o => o.date)
                    .ThenBy(o => o.shop)
                    .ThenBy(o => o.level)
                    .Select((o, i) => new
                    {
                        index = i + 1,
                        shop = o.shop,
                        date = o.date?.ToString("dd/MM/yyyy<br/>HH:mm:ss", new CultureInfo("th-TH")),
                        user = o.type == 0 ? o.student : o.employee,
                        level = o.level,
                        //student = o.student,
                        // count = o.Count(),
                    })
                    .ToList();

                return new { data = data };
            }
        }

        private static List<QueryModel> GetQuery(JWTToken.userData userData, Search search, JabJaiEntities db)
        {
            var sl = new StudentLogic(db);
            var term = sl.GetTermId(search.date1, userData);

            var q1 = db.TUserAttendance
                    .Where(o => o.SchoolID == userData.CompanyID);

            var q2 = db.TB_StudentViews
                    .Where(o => o.nTerm == term && o.SchoolID == userData.CompanyID);

            if (search.type.HasValue)
            {
                q1 = q1.Where(o => o.cType == search.type);
            }

            if (search.shop_id.HasValue)
                q1 = q1.Where(o => o.ShopID == search.shop_id);

            var d1 = search.date1;
            var d2 = search.date2;

            q1 = q1.Where(o => o.AttendanceDate >= d1);
            q1 = q1.Where(o => o.AttendanceDate <= d2);


            if (search.room.HasValue)
                q2 = q2.Where(o => o.nTermSubLevel2 == search.room);

            if (search.level.HasValue)
                q2 = q2.Where(o => o.nTSubLevel == search.level);

            var q = from a in q1
                    from d in db.TShops.Where(o => o.SchoolID == a.SchoolID && o.shop_id == a.ShopID)
                    from b in q2.Where(o => o.SchoolID == a.SchoolID && o.sID == a.sID).DefaultIfEmpty()
                    from e in db.TEmployees.Where(o => o.SchoolID == a.SchoolID && o.sEmp == a.sID).DefaultIfEmpty()
                        //from c in db.TEmployees
                        //.Where(o => o.SchoolID == a.SchoolID && o.sEmp == a.sID2)
                        //.DefaultIfEmpty()
                    where (b != null || e != null)
                    select new QueryModel
                    {
                        type = a.cType,
                        shop = d.shop_name,
                        student = b.sName + " " + b.sLastname,
                        employee = e.sName + " " + e.sLastname,
                        level = b != null ? b.SubLevel + "/" + b.nTSubLevel2 : "-",
                        date = a.TStamp,
                        // date = a.AttendanceDate.Date.ToString("dd/MM/yyyy<br/>HH:mm:ss", new CultureInfo("th-TH")),
                        //date = a.AttendanceDate.ToString("dd/MM/yyyy"),
                        //time = a.AttendanceDate.ToString("HH:mm"),
                        //buyer = b == null ? c.sName + " " + c.sLastname + " (ครู)"  : b.sName + " " + b.sLastname,
                        //sID = a.sID ?? 0,
                        //sID2 = a.sID2 ?? 0,
                    };

            return q.ToList();
        }

        //switch (search.type)
        //{
        //    case "1":
        //        q1 = q1.Where(o => o.cType == search.type);
        //        break;
        //    case "2":
        //        break;

        //    default:
        //        break;
        //}
        //if (d1.HasValue)
        //    q1 = q1.Where(o => o.AttendanceDate >= d1);
        //else
        //    q1 = q1.Where(o => o.AttendanceDate >= DateTime.Now.Date);

        //if (d2.HasValue)
        //    q1 = q1.Where(o => o.dSell <= d2);
        //else
        //    q1 = q1.Where(o => o.dSell <= DateTime.Now.AddDays(1).Date);

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel1(Search search)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var result = GetQuery(userData, search, db);

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานการเข้าโรงอาหาร");

                    var worksheet = excel.Workbook.Worksheets["รายงานการเข้าโรงอาหาร"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[1, 1, 1, 6]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);
                    }

                    SetCell(worksheet.Cells[2, 1], isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[2, 2], isHeader: true, text: "วันที่");
                    SetCell(worksheet.Cells[2, 3], isHeader: true, text: "ร้าน");
                    SetCell(worksheet.Cells[2, 4], isHeader: true, text: "ผู้ทำรายการ");
                    SetCell(worksheet.Cells[2, 5], isHeader: true, text: "ชั้น/ห้อง");

                    var data = result
                        //.GroupBy(o => new { o.shop, o.date.ฟะะ, o.buyer, o.level })
                        .OrderBy(o => o.date)
                        .ThenBy(o => o.shop)
                        .ThenBy(o => o.level)
                        .Select((o, i) => new
                        {
                            index = i + 1,
                            shop = o.shop,
                            date = o.date?.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")),
                            user = o.type == 0 ? o.student : o.employee,
                            level = o.level,
                        })
                        .ToList();

                    for (int row = 0; row < data.Count; row++)
                    {
                        var r = data[row];

                        SetCell(worksheet.Cells[3 + row, 1], text: r.index + "");
                        SetCell(worksheet.Cells[3 + row, 2], text: r.date);
                        SetCell(worksheet.Cells[3 + row, 3], text: r.shop + "");
                        SetCell(worksheet.Cells[3 + row, 4], text: r.user + "");
                        SetCell(worksheet.Cells[3 + row, 5], text: r.level + "");
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

        //private static void SetHeader(ExcelWorksheet excelWorksheet, string Cells, bool Merge, string strValues, int? fontSize, ExcelHorizontalAlignment excelHorizontal)
        //{
        //    using (ExcelRange rng = excelWorksheet.Cells[Cells])
        //    {
        //        rng.Merge = Merge;
        //        rng.Value = strValues;
        //        rng.Style.Font.Bold = true;
        //        rng.Style.HorizontalAlignment = excelHorizontal;
        //        rng.Style.Font.Size = fontSize ?? 10;
        //    }
        //}

        public class Search
        {
            public int? type { get; set; }
            public DateTime date1 { get; set; }
            public DateTime date2 { get; set; }
            public int? shop_id { get; set; }
            public int? level { get; set; }
            public int? room { get; set; }

            //public DateTime? date1
            //{
            //    get
            //    {
            //        if (!string.IsNullOrEmpty(sdate1))
            //        {
            //            DateTime _d;
            //            if (DateTime.TryParseExact(sdate1
            //                , "yyyyMMdd"
            //                , new CultureInfo("th-TH")
            //                , DateTimeStyles.None
            //                , out _d))
            //            {
            //                return _d;
            //            }
            //        }
            //        return null;
            //    }
            //}
            //public DateTime? date2
            //{
            //    get
            //    {
            //        if (!string.IsNullOrEmpty(sdate2))
            //        {
            //            DateTime _d;
            //            if (DateTime.TryParseExact(sdate2
            //                , "yyyyMMdd"
            //                , new CultureInfo("th-TH")
            //                , DateTimeStyles.None
            //                , out _d))
            //            {
            //                return _d;
            //            }
            //        }
            //        return null;
            //    }
            //}
        }
        public class QueryModel
        {
            public int? type { get; set; }
            public string shop { get; set; }
            public string buyer { get; set; }
            public string level { get; set; }
            public DateTime? date { get; set; }
            public string student { get; internal set; }
            public string employee { get; internal set; }
            // public string time { get; internal set; }
        }
    }

}