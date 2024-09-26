using EllipticCurve.Utils;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.import
{
    public partial class invoices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }


            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!this.IsPostBack)
            {
                fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear where SchoolID = '" + userData.CompanyID + "' order by numberYear desc", "", "nYear", "numberYear");
                ddlyear.SelectedValue = DateTime.Now.Year.ToString();

                JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read));
                var listLevel = en.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                }
                //this.ltrSortLevel.Text = this.ltrLevel.Text;
            }
        }

        #region Export Excel

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void ExportExcl()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            ExcelPackage excel = new ExcelPackage();
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var worksheet = excel.Workbook.Worksheets.Add("Import Data");

                List<string> HeaderNameArray = new List<string> { "Date", "Invoice", "Account code", "Student number", "Invoice amount", "Discount", "Due date" };
                SetSheetHeaderT1(worksheet, HeaderNameArray);

                var worksheet2 = excel.Workbook.Worksheets.Add($"Account code - description");
                HeaderNameArray = new List<string> { "Account code", "Description" };
                SetSheetHeaderT1(worksheet2, HeaderNameArray);

                int rowIndex = 2;
                var accountPaymentItem = dbschool.Database.SqlQuery<AccountPaymentItem>("SELECT * FROM [AccountingDB].[dbo].[AccountPaymentItem] WHERE DeleteDate IS NULL AND [SchoolId] = " + userData.CompanyID).ToList();

                foreach (var data in accountPaymentItem)
                {
                    SetCell(worksheet2.Cells[rowIndex, 1], text: (data.ReferenceNo ?? "").ToString(), horizotal: ExcelHorizontalAlignment.Center, fontSize: 12, isBold: false);
                    SetCell(worksheet2.Cells[rowIndex, 2], text: data.Name, horizotal: ExcelHorizontalAlignment.Center, fontSize: 12, isBold: false);
                    rowIndex++;
                }

                //using (PeakengineEntities entities = Connection.PeakengineEntities(ConnectionDB.Read))
                //{
                //    foreach (var data in entities.Products.Where(w => w.school_id == userData.CompanyID && (w.Del ?? false) == false && !string.IsNullOrEmpty(w.productId)))
                //    {
                //        SetCell(worksheet2.Cells[rowIndex, 1], text: (data.productId ?? "").ToString(), horizotal: ExcelHorizontalAlignment.Center, fontSize: 12, isBold: false);
                //        SetCell(worksheet2.Cells[rowIndex, 2], text: data.sPayment, horizotal: ExcelHorizontalAlignment.Center, fontSize: 12, isBold: false);
                //        rowIndex++;
                //    }
                //}
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            HttpContext.Current.Response.ContentType = "application/text";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
        }

        public class AccountPaymentItem
        {
            public int AccountPaymentItemId { get; set; }
            public int? SchoolId { get; set; }
            public int? AccountChartId { get; set; }
            public int? PaymentTypeID { get; set; }
            public string Name { get; set; }
            public decimal? Price { get; set; }
            public string ReferenceNo { get; set; }
            public DateTime? CreateDate { get; set; }
            public string CreateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? DeleteDate { get; set; }
            public string DeleteBy { get; set; }
        }


        private static void SetSheetHeaderT1(ExcelWorksheet worksheet, List<string> HeaderNameArray)
        {

            int colIndewx = 1;
            foreach (var reportName in HeaderNameArray)
            {
                SetCell(worksheet.Cells[1, colIndewx++], text: reportName, horizotal: ExcelHorizontalAlignment.Center, fontSize: 12, isBold: true);
            }
        }

        private static void SetCell(ExcelRange xrange
              , bool isMerge = false
              , string text = ""
              , int fontSize = 11
              , bool isBold = false
              , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
              , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
              , Color? bgColor = null
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
                if (bgColor.HasValue)
                {
                    xrange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    xrange.Style.Fill.BackgroundColor.SetColor(bgColor.Value);
                }
                xrange.AutoFitColumns();

            }
        }

        #endregion

        #region Import Excel


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void ImportExcl(Invoice_Impoert _Impoert)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string filename = _Impoert.fileBase.FileName;

            string targetpath = HttpContext.Current.Server.MapPath("~/Files/uploads/");
            _Impoert.fileBase.SaveAs(targetpath + filename);
            string pathtoexcelfile = targetpath + filename;
            FileInfo newFile = new FileInfo(pathtoexcelfile);
            ExcelPackage xlPackage = new ExcelPackage(newFile);
            var ds_package = xlPackage.ToDataSet(1);

            return;

            List<string> HeaderNameArray = new List<string> { "Date", "Invoice", "Account code", "Student number", "Invoice amount", "Discount", "Due date" };

            foreach (DataRow dr in ds_package.Tables[0].Select("birth <> ''"))
            {
                long dateNum;
                DateTime result;
                switch (dr["birth"])
                {
                    case "Due date":
                        long.TryParse(dr["Due date"].ToString(), out dateNum);
                        result = DateTime.FromOADate(dateNum);
                        dr["Due date"] = result;
                        break;
                    case "Date":
                        long.TryParse(dr["Date"].ToString(), out dateNum);
                        result = DateTime.FromOADate(dateNum);
                        dr["Date"] = result;
                        break;
                    case "Invoice amount":
                        double Invoice_amount = 0;
                        double.TryParse(dr["Invoice amount"].ToString(), out Invoice_amount);
                        break;
                    default:
                        break;
                }
            }

            using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                using (PeakengineEntities entities = Connection.PeakengineEntities(ConnectionDB.Read))
                {
                    var dataGroup = ds_package.Tables[0].AsEnumerable().GroupBy(gb => new
                    {
                        Student_number = gb["Student number"],
                        InvoicesCode = gb["Account code"],
                        Duedate = gb["Due date"],
                        invoices_Code = gb["Invoice"],
                    })
                    .Select(s => new
                    {
                        Student_number = (string)s.Key.Student_number,
                        InvoicesCode = (string)s.Key.InvoicesCode,
                        invoices_Code = (string)s.Key.invoices_Code,
                        Duedate = (DateTime)s.Key.Duedate,

                    });

                    var studentData = (from a in jabJaiEntities.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID)
                                       where a.nTerm == _Impoert.nTerm && (a.cDel ?? "0") == "0"
                                       select a).AsQueryable().ToList();

                    var product = entities.Products.Where(w => w.school_id == userData.CompanyID && (w.Del ?? false) == false).ToList();

                    foreach (var _dataGroup in dataGroup)
                    {
                        var f_studentData = studentData.FirstOrDefault(f => f.sStudentID == _dataGroup.Student_number);
                        TInvoice _invoices = new TInvoice
                        {
                            UpdatedDate = null,
                            UpdatedBy = null,
                            peakUpdate = null,
                            peakStatusUpdate = null,
                            CreatedDate = DateTime.Now,
                            CreatedBy = userData.UserID,
                            code = _dataGroup.InvoicesCode,
                            dueDate = _dataGroup.Duedate,
                            issuedDate = _dataGroup.Duedate,
                            ManualDiscount = 0,
                            school_id = userData.CompanyID,
                            paid_status = false,
                            isDel = false,
                            jabjaiUpdate = DateTime.Now,
                            Term = f_studentData.sTerm,
                            nTermSubLevel2 = f_studentData.nTermSubLevel2,
                            nTerm = f_studentData.nTerm,
                            LabelSubLevel = f_studentData.SubLevel,
                            SubLevel = f_studentData.SubLevel,
                            StudentFullName = f_studentData.titleDescription + " " + f_studentData.sName + " " + f_studentData.sLastname,
                            StudentCode = f_studentData.sStudentID,
                            student_id = f_studentData.sID,
                            StudentName = f_studentData.titleDescription + " " + f_studentData.sName + " " + f_studentData.sLastname,
                            TermYear = (f_studentData.numberYear ?? 0).ToString(),
                            invoices_status = "",
                        };

                        entities.SaveChanges();
                        List<TInvoices_Detail> invoices_Detail = new List<TInvoices_Detail>();
                        foreach (var data in ds_package.Tables[0].Select(" ['Student number'] = '" + _dataGroup.Student_number + "' "))
                        {
                            string productId = (string)data["Account code"];
                            var f_product = product.FirstOrDefault(f => f.productId == productId);
                            invoices_Detail.Add(new TInvoices_Detail
                            {
                                AccountingId = 0,
                                Discount = 0,
                                GrandTotal = 0,
                                OutstandingAmount = 0,
                                quantity = 1,
                                IsDelete = false,
                                invoices_Id = _invoices.invoices_Id,
                                nPaymentID = f_product.nPaymentID,
                                price = f_product.Pirce,
                                product_id = f_product.productId,
                                ProductAmount = 1,
                            });
                        }

                        entities.SaveChanges();
                    }
                }

            }
        }
        #endregion
    }

    public class Invoice_Impoert
    {
        public HttpPostedFileBase fileBase { get; set; }
        public string nTerm { get; set; }
    }

}