using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    //this code was copy from studentcardregister.aspx
    public partial class ReportBalanceNew : System.Web.UI.Page
    {

        public class QueryModel
        {

            public int sID { get; set; }
            public string cType { get; set; }
            public string sStudentID { get; set; }
            public string FullName { get; set; }

            public decimal? OpeningBalance { get; set; }
            public decimal? TotalTopUp { get; set; }
            public decimal? TotalCancelTopUp { get; set; }
            public decimal? TotalWithDraw { get; set; }
            public decimal? TotalCancelWithDraw { get; set; }
            public decimal? TotalSales { get; set; }
            public decimal? TotalCancelSales { get; set; }
            public decimal? Balance { get; set; }
            public DateTime? BusinessDate { get; set; }

        }

        public class QueryModel3
        {

            public int sID { get; set; }
            public string cType { get; set; }
            public string Card { get; set; }
            public string UserName { get; set; }
            public decimal? OpeningBalance { get; set; }
            public decimal? TotalTopUp { get; set; }
            public decimal? TotalCancelTopUp { get; set; }
            public decimal? TotalWithDraw { get; set; }
            public decimal? TotalCancelWithDraw { get; set; }
            public decimal? TotalSales { get; set; }
            public decimal? TotalCancelSales { get; set; }
            public decimal? Balance { get; set; }
            public DateTime? BusinessDate { get; set; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                var userData = GetUserData();
                JabJaiEntities _db = new JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
                DataTable dtYear = fcommon.LinqToDataTable(_db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.numberYear).ToList());

                using (JabJaiMasterEntities db = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                    List<TYear> tylist = new List<TYear>();
                    TYear ty = new TYear();
                    foreach (var a in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).ToList())
                    {
                        ty = new TYear();
                        ty = a;
                        tylist.Add(ty);
                    }
                    var newList = tylist.OrderByDescending(x => x.numberYear).ToList();
                    ddlYear.DataSource = newList;
                    ddlYear.DataTextField = "numberYear";
                    ddlYear.DataValueField = "numberYear";
                    ddlYear.DataBind();
                }
            }
        }

        public class Search
        {
            //public string sdate1 { get; set; }
            public DateTime? date1 { get; set; }
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
            public int year { get; set; }
        }

        public static JWTToken.userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }

            return userData;
        }

        private static List<QueryModel> GetQuery1(JWTToken.userData userData, Search search, JabJaiEntities db)
        {
            var sl = new StudentLogic(db);
            var term = sl.GetTermId((search.date1 ?? DateTime.Now), userData);

            var q = $@"

SELECT  BusinessDate 
	, SUM([OpeningBalance]) 'OpeningBalance' 
	,SUM([TotalTopUp]) 'TotalTopUp' ,SUM([TotalCancelTopUp])'TotalCancelTopUp'
	,SUM([TotalWithDraw])'TotalWithDraw',SUM([TotalCancelWithDraw]) 'TotalCancelWithDraw'
	,SUM([TotalSales])'TotalSales',SUM([TotalCancelSales]) 'TotalCancelSales'
	,SUM(ISnull([Balance],0)) 'Balance'
FROM (
	SELECT A.BusinessDate  
	,SUM([OpeningBalance]) 'OpeningBalance' 
	,SUM([TotalTopUp]) 'TotalTopUp' ,SUM([TotalCancelTopUp])'TotalCancelTopUp'
	,SUM([TotalWithDraw])'TotalWithDraw',SUM([TotalCancelWithDraw]) 'TotalCancelWithDraw'
	,SUM([TotalSales])'TotalSales',SUM([TotalCancelSales]) 'TotalCancelSales'
	,SUM(ISnull([Balance],0)) 'Balance'

	FROM [JabjaiSchoolSingleDB].[dbo].[UserDailyBalance] A
	LEFT JOIN [JabjaiSchoolSingleDB].[dbo].TEmployeeInfo B ON A.SchoolID = B.SchoolID AND A.sID = B.sEmp

	WHERE A.SchoolID = {userData.CompanyID} 
    AND A.BusinessDate = '{search.date1?.ToString("yyyyMMdd")}' 
    AND A.cDel is null

	GROUP BY A.BusinessDate 

	UNION

	SELECT A.BusinessDate 
	, SUM([OpeningBalance]) 'OpeningBalance' 
	,SUM([TotalTopUp]) 'TotalTopUp' ,SUM([TotalCancelTopUp])'TotalCancelTopUp'
	,SUM([TotalWithDraw])'TotalWithDraw',SUM([TotalCancelWithDraw]) 'TotalCancelWithDraw'
	,SUM([TotalSales])'TotalSales',SUM([TotalCancelSales]) 'TotalCancelSales'
	,SUM(ISnull([Balance],0)) 'Balance'

	FROM [JabjaiSchoolSingleDB].[dbo].[UserDailyBalanceTempCard] A
	LEFT JOIN [JabjaiSchoolSingleDB].[dbo].[TBackupCardHistory] B ON A.SchoolID = B.SchoolID AND A.CardHistoryID = B.CardHistoryID

	WHERE A.SchoolID = {userData.CompanyID} 
    AND A.BusinessDate = '{search.date1?.ToString("yyyyMMdd")}' 
   
	GROUP BY A.BusinessDate
)T
GROUP BY T.BusinessDate
ORDER BY T.BusinessDate 

";
            var d = db.Database.SqlQuery<QueryModel>(q).ToList();

            return d;
        }

        private static List<QueryModel> GetQuery2(JWTToken.userData userData, Search search, JabJaiEntities db)
        {
            var sl = new StudentLogic(db);
            var term = sl.GetTermId((search.date1 ?? DateTime.Now), userData);

            var q = $@"
SELECT A.BusinessDate , A.cType  ,  CASE cType WHEN 0 THEN A.sStudentID ELSE B.Code END 'sStudentID'
, A.sName + ' ' + A.sLastname 'FullName' , 
SUM([OpeningBalance]) 'OpeningBalance' 
,SUM([TotalTopUp]) 'TotalTopUp' ,SUM([TotalCancelTopUp])'TotalCancelTopUp'
,SUM([TotalWithDraw])'TotalWithDraw',SUM([TotalCancelWithDraw]) 'TotalCancelWithDraw'
,SUM([TotalSales])'TotalSales',SUM([TotalCancelSales]) 'TotalCancelSales'
,SUM(ISnull([Balance],0)) 'Balance'

FROM [JabjaiSchoolSingleDB].[dbo].[UserDailyBalance] A
LEFT JOIN [JabjaiSchoolSingleDB].[dbo].TEmployeeInfo B ON A.SchoolID = B.SchoolID AND A.sID = B.sEmp

WHERE A.SchoolID = {userData.CompanyID} 
AND A.BusinessDate = '{search.date1?.ToString("yyyyMMdd")}' 
AND A.cDel is null

GROUP BY A.BusinessDate , A.cType , A.sStudentID,B.Code , A.sName , A.sLastname
ORDER BY A.sName , A.sLastname
";
            var d = db.Database.SqlQuery<QueryModel>(q).ToList();

            return d;
        }

        private static List<QueryModel3> GetQuery3(JWTToken.userData userData, Search search, JabJaiEntities db)
        {
            var sl = new StudentLogic(db);
            var term = sl.GetTermId((search.date1 ?? DateTime.Now), userData);


            var q = $@"

SELECT A.BusinessDate , A.cType  , A.sName  'Card' , B.UserName
,SUM([OpeningBalance]) 'OpeningBalance' 
,SUM([TotalTopUp]) 'TotalTopUp' ,SUM([TotalCancelTopUp])'TotalCancelTopUp'
,SUM([TotalWithDraw])'TotalWithDraw',SUM([TotalCancelWithDraw]) 'TotalCancelWithDraw'
,SUM([TotalSales])'TotalSales',SUM([TotalCancelSales]) 'TotalCancelSales'
,SUM(ISnull([Balance],0)) 'Balance'

FROM [JabjaiSchoolSingleDB].[dbo].[UserDailyBalanceTempCard] A
LEFT JOIN [JabjaiSchoolSingleDB].[dbo].[TBackupCardHistory] B ON A.SchoolID = B.SchoolID AND A.CardHistoryID = B.CardHistoryID

WHERE A.SchoolID = {userData.CompanyID} 
AND A.BusinessDate = '{search.date1?.ToString("yyyyMMdd")}' 


GROUP BY A.BusinessDate , A.cType , A.sName ,B.UserName
ORDER BY A.sName ,B.UserName
";
            var d = db.Database.SqlQuery<QueryModel3>(q).ToList();

            return d;
        }

        [WebMethod(EnableSession = true)]
        public static object GetData1(Search search)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var result = GetQuery1(userData, search, db);

                //var result = q.ToList();
                var data = result
                    //.GroupBy(o => o.BusinessDate)
                    //.OrderBy(o => o.Key)
                    .Select((o, i) => new
                    {
                        index = i + 1,
                      
                        date = o.BusinessDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),

                        topup = o.TotalTopUp?.ToString("#,0.##"),
                        topupCancle = o.TotalCancelTopUp?.ToString("#,0.##"),

                        use = o.TotalSales?.ToString("#,0.##"),
                        useCancle = o.TotalCancelSales?.ToString("#,0.##"),

                        withraw = o.TotalWithDraw?.ToString("#,0.##"),
                        withrawCancle = o.TotalCancelWithDraw?.ToString("#,0.##"),

                        net = o.Balance?.ToString("#,0.##"),

                    })
                    .ToList();

                return new { data = data };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetData2(Search search)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var result = GetQuery2(userData, search, db);

                //var result = q.ToList();
                var data = result
                    .OrderBy(o => o.FullName)
                    .Select((o, i) => new
                    {
                        index = i + 1,
                        date = o.BusinessDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                        code = o.sStudentID,
                        fullName = o.FullName,
                        type = o.cType == "0" ? "นักเรียน" : "ครู",

                        topup = o.TotalTopUp?.ToString("#,0.##"),
                        topupCancle = o.TotalCancelTopUp?.ToString("#,0.##"),

                        use = o.TotalSales?.ToString("#,0.##"),
                        useCancle = o.TotalCancelSales?.ToString("#,0.##"),

                        withraw = o.TotalWithDraw?.ToString("#,0.##"),
                        withrawCancle = o.TotalCancelWithDraw?.ToString("#,0.##"),

                        net = o.Balance?.ToString("#,0.##"),

                    })
                    .ToList();

                return new { data = data };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetData3(Search search)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var result = GetQuery3(userData, search, db);

                //var result = q.ToList();
                var data = result
                    .OrderBy(o => o.Card).ThenBy(o => o.UserName)
                    .Select((o, i) => new
                    {
                        index = i + 1,
                        date = o.BusinessDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                        card = o.Card,
                        fullName = o.UserName,
                        type = o.cType == "0" ? "นักเรียน" : (o.cType == "1" ? "ครู" : "บุคคลภายนออก"),

                        topup = o.TotalTopUp?.ToString("#,0.##"),
                        topupCancle = o.TotalCancelTopUp?.ToString("#,0.##"),

                        use = o.TotalSales?.ToString("#,0.##"),
                        useCancle = o.TotalCancelSales?.ToString("#,0.##"),

                        withraw = o.TotalWithDraw?.ToString("#,0.##"),
                        withrawCancle = o.TotalCancelWithDraw?.ToString("#,0.##"),

                        net = o.Balance?.ToString("#,0.##"),

                    })
                    .ToList();

                return new { data = data };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel1(Search search)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var result = GetQuery1(userData, search, db);

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานยอดเงินคงเหลือ");

                    var worksheet = excel.Workbook.Worksheets["รายงานยอดเงินคงเหลือ"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    using (var dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[1, 1, 1, 8]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);
                    }

                    SetCell(worksheet.Cells[2, 1], isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[2, 2], isHeader: true, text: "วันที่");
                    SetCell(worksheet.Cells[2, 3], isHeader: true, text: "ยอดเติม");
                    SetCell(worksheet.Cells[2, 4], isHeader: true, text: "ยอดยกเลิกเติม");
                    SetCell(worksheet.Cells[2, 5], isHeader: true, text: "ยอดใช้จ่าย");
                    SetCell(worksheet.Cells[2, 6], isHeader: true, text: "ยอดยกเลิกใช้จ่าย");
                    SetCell(worksheet.Cells[2, 7], isHeader: true, text: "ยอดถอน");
                    SetCell(worksheet.Cells[2, 8], isHeader: true, text: "ยอดคงเหลือ");

                    var data = result
                      //.GroupBy(o => o.BusinessDate)
                      //.OrderBy(o => o.Key)
                      .Select((o, i) => new
                      {
                          index = i + 1,
                          date = o.BusinessDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),

                          topup = o.TotalTopUp?.ToString("#,0.##"),
                          topupCancle = o.TotalCancelTopUp?.ToString("#,0.##"),

                          use = o.TotalSales?.ToString("#,0.##"),
                          useCancle = o.TotalCancelSales?.ToString("#,0.##"),

                          withraw = o.TotalWithDraw?.ToString("#,0.##"),
                          withrawCancle = o.TotalCancelWithDraw?.ToString("#,0.##"),

                          net = o.Balance?.ToString("#,0.##"),
                      })
                      .ToList();

                    for (int row = 0; row < data.Count; row++)
                    {
                        var r = data[row];

                        SetCell(worksheet.Cells[3 + row, 1], text: r.index + "");
                        SetCell(worksheet.Cells[3 + row, 2], text: r.date + "");
                        SetCell(worksheet.Cells[3 + row, 3], text: r.topup + "");
                        SetCell(worksheet.Cells[3 + row, 4], text: r.topupCancle + "");
                        SetCell(worksheet.Cells[3 + row, 5], text: r.use + "");
                        SetCell(worksheet.Cells[3 + row, 6], text: r.useCancle + "");
                        SetCell(worksheet.Cells[3 + row, 7], text: r.withraw + "");
                        SetCell(worksheet.Cells[3 + row, 8], text: r.net + "");
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

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel2(Search search)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var result = GetQuery2(userData, search, db);

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานยอดเงินคงเหลือ");

                    var worksheet = excel.Workbook.Worksheets["รายงานยอดเงินคงเหลือ"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    using (var dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[1, 1, 1, 11]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);
                    }

                    SetCell(worksheet.Cells[2, 1], isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[2, 2], isHeader: true, text: "วันที่");
                    SetCell(worksheet.Cells[2, 3], isHeader: true, text: "รหัสนักเรียน");
                    SetCell(worksheet.Cells[2, 4], isHeader: true, text: "ชื่อ-สกุล");
                    SetCell(worksheet.Cells[2, 5], isHeader: true, text: "นักเรียน / ครู");
                    SetCell(worksheet.Cells[2, 6], isHeader: true, text: "ยอดเติม");
                    SetCell(worksheet.Cells[2, 7], isHeader: true, text: "ยอดยกเลิกเติม");
                    SetCell(worksheet.Cells[2, 8], isHeader: true, text: "ยอดใช้จ่าย");
                    SetCell(worksheet.Cells[2, 9], isHeader: true, text: "ยอดยกเลิกใช้จ่าย");
                    SetCell(worksheet.Cells[2, 10], isHeader: true, text: "ยอดถอน");
                    SetCell(worksheet.Cells[2, 11], isHeader: true, text: "ยอดคงเหลือ");

                    var data = result
                     .OrderBy(o => o.FullName)
                     .Select((o, i) => new
                     {
                         index = i + 1,
                         date = o.BusinessDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                         code = o.sStudentID,
                         fullName = o.FullName,
                         type = o.cType == "0" ? "นักเรียน" : "ครู",

                         topup = o.TotalTopUp?.ToString("#,0.##"),
                         topupCancle = o.TotalCancelTopUp?.ToString("#,0.##"),

                         use = o.TotalSales?.ToString("#,0.##"),
                         useCancle = o.TotalCancelSales?.ToString("#,0.##"),

                         withraw = o.TotalWithDraw?.ToString("#,0.##"),
                         withrawCancle = o.TotalCancelWithDraw?.ToString("#,0.##"),

                         net = o.Balance?.ToString("#,0.##"),

                     })
                     .ToList();

                    for (int row = 0; row < data.Count; row++)
                    {
                        var r = data[row];

                        SetCell(worksheet.Cells[3 + row, 1], text: r.index + "");
                        SetCell(worksheet.Cells[3 + row, 2], text: r.date + "");
                        SetCell(worksheet.Cells[3 + row, 3], text: r.code + "");
                        SetCell(worksheet.Cells[3 + row, 4], text: r.fullName + "");
                        SetCell(worksheet.Cells[3 + row, 5], text: r.type + "");
                        SetCell(worksheet.Cells[3 + row, 6], text: r.topup + "");
                        SetCell(worksheet.Cells[3 + row, 7], text: r.topupCancle + "");
                        SetCell(worksheet.Cells[3 + row, 8], text: r.use + "");
                        SetCell(worksheet.Cells[3 + row, 9], text: r.useCancle + "");
                        SetCell(worksheet.Cells[3 + row, 10], text: r.withraw + "");
                        SetCell(worksheet.Cells[3 + row, 11], text: r.net + "");
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

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel3(Search search)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var result = GetQuery3(userData, search, db);

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานยอดเงินคงเหลือ");

                    var worksheet = excel.Workbook.Worksheets["รายงานยอดเงินคงเหลือ"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    using (var dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[1, 1, 1, 11]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);
                    }

                    SetCell(worksheet.Cells[2, 1], isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[2, 2], isHeader: true, text: "วันที่");
                    SetCell(worksheet.Cells[2, 3], isHeader: true, text: "ชื่อบัตร");
                    SetCell(worksheet.Cells[2, 4], isHeader: true, text: "ชื่อ");
                    SetCell(worksheet.Cells[2, 5], isHeader: true, text: "ประภท");
                    SetCell(worksheet.Cells[2, 6], isHeader: true, text: "ยอดเติม");
                    SetCell(worksheet.Cells[2, 7], isHeader: true, text: "ยอดยกเลิกเติม");
                    SetCell(worksheet.Cells[2, 8], isHeader: true, text: "ยอดใช้จ่าย");
                    SetCell(worksheet.Cells[2, 9], isHeader: true, text: "ยอดยกเลิกใช้จ่าย");
                    SetCell(worksheet.Cells[2, 10], isHeader: true, text: "ยอดถอน");
                    SetCell(worksheet.Cells[2, 11], isHeader: true, text: "ยอดคงเหลือ");

                    var data = result
                     .OrderBy(o => o.UserName)
                     .Select((o, i) => new
                     {
                         index = i + 1,
                         date = o.BusinessDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),               
                         card = o.Card,
                         fullName = o.UserName,
                         type = o.cType == "0" ? "นักเรียน" : (o.cType == "1" ? "ครู" : "บุคคลภายนออก"),

                         topup = o.TotalTopUp?.ToString("#,0.##"),
                         topupCancle = o.TotalCancelTopUp?.ToString("#,0.##"),

                         use = o.TotalSales?.ToString("#,0.##"),
                         useCancle = o.TotalCancelSales?.ToString("#,0.##"),

                         withraw = o.TotalWithDraw?.ToString("#,0.##"),
                         withrawCancle = o.TotalCancelWithDraw?.ToString("#,0.##"),

                         net = o.Balance?.ToString("#,0.##"),

                     })
                     .ToList();

                    for (int row = 0; row < data.Count; row++)
                    {
                        var r = data[row];

                        SetCell(worksheet.Cells[3 + row, 1], text: r.index + "");
                        SetCell(worksheet.Cells[3 + row, 2], text: r.date + "");
                        SetCell(worksheet.Cells[3 + row, 3], text: r.card + "");
                        SetCell(worksheet.Cells[3 + row, 4], text: r.fullName + "");
                        SetCell(worksheet.Cells[3 + row, 5], text: r.type + "");
                        SetCell(worksheet.Cells[3 + row, 6], text: r.topup + "");
                        SetCell(worksheet.Cells[3 + row, 7], text: r.topupCancle + "");
                        SetCell(worksheet.Cells[3 + row, 8], text: r.use + "");
                        SetCell(worksheet.Cells[3 + row, 9], text: r.useCancle + "");
                        SetCell(worksheet.Cells[3 + row, 10], text: r.withraw + "");
                        SetCell(worksheet.Cells[3 + row, 11], text: r.net + "");
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

    }
}