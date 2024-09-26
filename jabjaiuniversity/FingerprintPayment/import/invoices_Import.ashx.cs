using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FingerprintPayment.import
{
    /// <summary>
    /// Summary description for invoices_Import
    /// </summary>
    public class invoices_Import : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            string nTerm = context.Request["nTerm"];
            string sTermSubLevel2 = context.Request["nTermSubLevel2"];
            string sTSubLevel = context.Request["nTSubLevel"];
            HttpFileCollection files = context.Request.Files;
            bool isSuccess = true;
            int resultCode = 200;
            string message = "Success", errorMessage = "";

            if (context.Request.Files.Count > 0)
            {
                string filename = files[0].FileName.Replace(".xlsx", DateTime.Now.ToString("ddMMyyyyHHmmssffff") + ".xlsx");

                string targetpath = HttpContext.Current.Server.MapPath("~/images/");
                files[0].SaveAs(targetpath + filename);
                string pathtoexcelfile = targetpath + filename;
                FileInfo newFile = new FileInfo(pathtoexcelfile);
                ExcelPackage xlPackage = new ExcelPackage(newFile);
                var ds_package = xlPackage.ToDataSet(1);

                System.IO.File.Delete(targetpath + filename);

                List<string> HeaderNameArray = new List<string> { "Date", "Invoice", "Account code", "Student number", "Invoice amount", "Discount", "Due date" };

                try
                {
                    using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                    {
                        using (PeakengineEntities entities = Connection.PeakengineEntities(ConnectionDB.Read))
                        {
                            var q1 = ds_package.Tables[0].AsEnumerable()
                                .Where(w => !string.IsNullOrEmpty(w["Student number"].ToString().Trim()))
                                .GroupBy(gb => new
                                {
                                    Student_number = gb["Student number"],
                                    //InvoicesCode = gb["Account code"],
                                    Duedate = gb["Due date"],
                                    Invoice_Date = gb["Date"],
                                    invoices_Code = gb["Invoice"],
                                });

                            var dataGroup = q1
                                .Select(s => new
                                {
                                    Student_number = (string)s.Key.Student_number,
                                    //InvoicesCode = (string)s.Key.InvoicesCode,
                                    invoices_Code = (string)s.Key.invoices_Code,
                                    Duedate = (string)s.Key.Duedate,
                                    Invoice_Date = (string)s.Key.Invoice_Date,
                                });

                            var f_Term = (from a in jabJaiEntities.TTerms.Where(w => w.SchoolID == userData.CompanyID)
                                          join b in jabJaiEntities.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                                          on a.nYear equals b.nYear
                                          where a.nTerm == nTerm
                                          select new
                                          {
                                              a.nTerm,
                                              a.sTerm,
                                              b.numberYear,
                                              a.nYear
                                          }).FirstOrDefault();

                            StudentLogic studentLogic = new StudentLogic(jabJaiEntities);
                            string SubLevel = "", nTSubLevel2 = "", LevelName = "";
                            int nTermSubLevel2 = 0, nTSubLevel = 0, LevelID = 0;

                            if (string.IsNullOrEmpty(sTermSubLevel2))
                            {
                                nTSubLevel = int.Parse(sTSubLevel);
                                var f1 = (from a in jabJaiEntities.TSubLevels.Where(w => w.SchoolID == userData.CompanyID)
                                          join b in jabJaiEntities.TLevels.Where(w => w.SchoolID == userData.CompanyID)
                                          on a.nTLevel equals b.LevelID
                                          where a.nTSubLevel == nTSubLevel
                                          select new
                                          {
                                              a.nTSubLevel,
                                              b.LevelName,
                                              a.SubLevel,
                                              b.LevelID
                                          }).FirstOrDefault();

                                if (f1 != null)
                                {
                                    nTSubLevel = f1.nTSubLevel;
                                    LevelName = f1.LevelName;
                                    LevelID = f1.LevelID;
                                    SubLevel = f1.SubLevel;
                                }
                            }
                            else
                            {
                                nTermSubLevel2 = int.Parse(sTermSubLevel2);
                                var f1 = (from a in jabJaiEntities.TSubLevels.Where(w => w.SchoolID == userData.CompanyID)
                                          join b in jabJaiEntities.TLevels.Where(w => w.SchoolID == userData.CompanyID)
                                          on a.nTLevel equals b.LevelID
                                          join c in jabJaiEntities.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID)
                                          on a.nTSubLevel equals c.nTSubLevel
                                          where c.nTermSubLevel2 == nTermSubLevel2
                                          select new
                                          {
                                              a.nTSubLevel,
                                              b.LevelName,
                                              a.SubLevel,
                                              b.LevelID,
                                              c.nTermSubLevel2,
                                              c.nTSubLevel2
                                          }).FirstOrDefault();

                                if (f1 != null)
                                {
                                    nTSubLevel = f1.nTSubLevel;
                                    LevelName = f1.LevelName;
                                    LevelID = f1.LevelID;
                                    SubLevel = f1.SubLevel;
                                    nTermSubLevel2 = f1.nTermSubLevel2;
                                    nTSubLevel2 = f1.nTSubLevel2;
                                }
                            }

                            var studentData = (from a in jabJaiEntities.TUser.Where(w => w.SchoolID == userData.CompanyID)

                                               join b in jabJaiEntities.TTitleLists.Where(w => w.SchoolID == userData.CompanyID)
                                               on a.sStudentTitle equals b.nTitleid.ToString() into jab
                                               from jb in jab.DefaultIfEmpty()

                                               where (a.cDel ?? "0") == "0"
                                               select new TM_StudentViews
                                               {
                                                   sID = a.sID,
                                                   sStudentTitle = a.sStudentTitle,
                                                   sName = a.sName,
                                                   sLastname = a.sLastname,
                                                   nTerm = f_Term.nTerm,
                                                   sTerm = f_Term.sTerm,
                                                   numberYear = f_Term.numberYear,
                                                   //dStart = a.sStudentTitle,
                                                   //dEnd = a.sStudentTitle,
                                                   SubLevel = SubLevel,
                                                   nTSubLevel2 = nTSubLevel2,
                                                   nTermSubLevel2 = nTermSubLevel2,
                                                   nTSubLevel = nTSubLevel,
                                                   //nTLevel = nTLevel,
                                                   sStudentID = a.sStudentID,
                                                   nStudentNumber = a.nStudentNumber,
                                                   nStudentStatus = a.nStudentStatus,
                                                   cDel = a.cDel,
                                                   //titleDescription = a.sStudentTitle,
                                                   moveInDate = a.moveInDate,
                                               }).ToList();

                            var product = entities.Products.Where(w => w.school_id == userData.CompanyID && (w.Del ?? false) == false).ToList();

                            if (studentData.Count() > 0)
                            {
                                foreach (var _dataGroup in dataGroup)
                                {
                                    DateTime dueDate, issuedDate;
                                    dueDate = ConvertStringToDate(_dataGroup.Duedate);
                                    issuedDate = ConvertStringToDate(_dataGroup.Invoice_Date);

                                    var f_studentData = studentData.FirstOrDefault(f => f.sStudentID == _dataGroup.Student_number);
                                    if (f_studentData == null) continue;

                                    TInvoice _invoices = new TInvoice
                                    {
                                        UpdatedDate = null,
                                        UpdatedBy = null,
                                        peakUpdate = null,
                                        peakStatusUpdate = null,
                                        CreatedDate = DateTime.Now,
                                        CreatedBy = userData.UserID,
                                        code = _dataGroup.invoices_Code,
                                        dueDate = dueDate,
                                        issuedDate = issuedDate,
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
                                        invoices_status = "Approve",
                                        nTSubLevel = f_studentData.nTSubLevel,
                                        SubLevel2 = f_studentData.nTSubLevel2,
                                        invoices_Code = Guid.NewGuid().ToString()
                                    };

                                    long dateNum;
                                    List<TInvoices_Detail> invoices_Detail = new List<TInvoices_Detail>();
                                    DataRow[] dataRow = ds_package.Tables["Import Data"].Select(" [Student number] = '" + _dataGroup.Student_number + "' AND Invoice = '" + _dataGroup.invoices_Code + "' ");
                                    decimal paymentAmount = 0, TotalDiscount = 0, GrandTotal = 0;

                                    foreach (var data in dataRow)
                                    {
                                        decimal Invoice_amount = 0, Discount = 0;
                                        decimal.TryParse(data["Invoice amount"].ToString(), out Invoice_amount);
                                        decimal.TryParse(data["Discount"].ToString(), out Discount);

                                        string productId = (string)data["Account code"];
                                        int nPaymentID = 0;
                                        //int.TryParse((string)data["Account code"], out nPaymentID);
                                        var f_product = product.FirstOrDefault(f => f.productId == productId);
                                        invoices_Detail.Add(new TInvoices_Detail
                                        {
                                            AccountingId = 0,
                                            Discount = Discount,
                                            quantity = 1,
                                            IsDelete = false,
                                            nPaymentID = f_product.nPaymentID,
                                            product_id = f_product.productId,
                                            price = Invoice_amount,
                                            ProductAmount = Invoice_amount,
                                            GrandTotal = Invoice_amount - Discount,
                                            OutstandingAmount = Invoice_amount - Discount,
                                        });

                                        paymentAmount += Invoice_amount;
                                        TotalDiscount += Discount;
                                        GrandTotal += Invoice_amount - Discount;
                                    }

                                    _invoices.GrandTotal = GrandTotal;
                                    _invoices.TotalDiscount = TotalDiscount;
                                    _invoices.ManualDiscount = 0;
                                    _invoices.paymentAmount = paymentAmount;
                                    _invoices.OutstandingAmount = GrandTotal;

                                    if (GrandTotal <= 0) continue;

                                    entities.TInvoices.Add(_invoices);
                                    entities.SaveChanges();

                                    invoices_Detail.ForEach(f => f.invoices_Id = _invoices.invoices_Id);
                                    entities.TInvoices_Detail.AddRange(invoices_Detail);
                                    entities.SaveChanges();
                                }
                            }
                            else
                            {
                                isSuccess = false;
                                resultCode = 502;
                                message = "กรุณาทำการบันทึกข้อมูลนักเรียนในปีการศึกษานี้";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                    resultCode = 503;
                    message = "กรุณาตรวจสอบข้อมูล";
                    errorMessage = fcommon.ExceptionMessage(ex);
                }
            }
            else
            {
                isSuccess = false;
                resultCode = 501;
                message = "กรุณาเลือกไฟล์ที่อัพโหลด";
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            var result = new { success = isSuccess, message, resultCode, errorMessage };
            context.Response.Expires = -1;
            context.Response.ContentType = "application/json";
            context.Response.Write(serializer.Serialize(result));

            context.Response.End();
        }

        public DateTime ConvertStringToDate(string strdate)
        {
            DateTime dDate = DateTime.Today;
            double d = 0;
            double.TryParse(strdate, out d);
            if (d > 0)
            {
                dDate = DateTime.FromOADate(d);
            }
            else
            {
                if (DateTime.TryParseExact(strdate, "dd/MM/yy", new CultureInfo("en-us"), DateTimeStyles.None, out dDate)) { }
                else if (DateTime.TryParseExact(strdate, "dd/MM/yyyy", new CultureInfo("en-us"), DateTimeStyles.None, out dDate)) { }
                else if (DateTime.TryParseExact(strdate, "d/MM/yyyy", new CultureInfo("en-us"), DateTimeStyles.None, out dDate)) { }
                else if (DateTime.TryParseExact(strdate, "dd/M/yyyy", new CultureInfo("en-us"), DateTimeStyles.None, out dDate)) { }
                else if (DateTime.TryParseExact(strdate, "d/M/yyyy", new CultureInfo("en-us"), DateTimeStyles.None, out dDate)) { }
                //else if (DateTime.TryParseExact(strdate, "MM/dd/yyyy", new CultureInfo("en-us"), DateTimeStyles.None, out dDate)) { }
                //else if (DateTime.TryParseExact(strdate, "M/dd/yyyy", new CultureInfo("en-us"), DateTimeStyles.None, out dDate)) { }
                //else if (DateTime.TryParseExact(strdate, "M/d/yyyy", new CultureInfo("en-us"), DateTimeStyles.None, out dDate)) { }
                //else if (DateTime.TryParseExact(strdate, "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-us"), DateTimeStyles.None, out dDate)) { }
                //else if (DateTime.TryParseExact(strdate, "dd/MM/yy HH:mm:ss", new CultureInfo("en-us"), DateTimeStyles.None, out dDate)) { }

                if (dDate.Year > DateTime.Today.AddYears(2).Year)
                {
                    if (DateTime.TryParseExact(strdate, "dd/MM/yyyy", new CultureInfo("th-th"), DateTimeStyles.None, out dDate)) { }
                    else if (DateTime.TryParseExact(strdate, "dd/MM/yy", new CultureInfo("th-th"), DateTimeStyles.None, out dDate)) { }
                    else if (DateTime.TryParseExact(strdate, "d/MM/yyyy", new CultureInfo("th-th"), DateTimeStyles.None, out dDate)) { }
                    else if (DateTime.TryParseExact(strdate, "d/M/yyyy", new CultureInfo("th-th"), DateTimeStyles.None, out dDate)) { }
                    //else if (DateTime.TryParseExact(strdate, "MM/dd/yy", new CultureInfo("th-th"), DateTimeStyles.None, out dDate)) { }
                    //else if (DateTime.TryParseExact(strdate, "M/dd/yyyy", new CultureInfo("th-th"), DateTimeStyles.None, out dDate)) { }
                    //else if (DateTime.TryParseExact(strdate, "M/d/yyyy", new CultureInfo("th-th"), DateTimeStyles.None, out dDate)) { }
                    //else if (DateTime.TryParseExact(strdate, "dd/MM/yyyy HH:mm:ss", new CultureInfo("th-th"), DateTimeStyles.None, out dDate)) { }
                    //else if (DateTime.TryParseExact(strdate, "dd/MM/yy HH:mm:ss", new CultureInfo("th-th"), DateTimeStyles.None, out dDate)) { }
                }

                if (dDate == new DateTime())
                {
                    DateTime.TryParse(strdate, out dDate);
                }

            }

            return dDate;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        public class TM_StudentViews
        {
            public int sID { get; set; }
            public string sStudentTitle { get; set; }
            public string sName { get; set; }
            public string sLastname { get; set; }
            public string nTerm { get; set; }
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public int nTSubLevel { get; set; }
            public int nTermSubLevel2 { get; set; }
            public string sStudentID { get; set; }
            public Nullable<int> nStudentNumber { get; set; }
            public Nullable<int> nStudentStatus { get; set; }
            public string cDel { get; set; }
            public string sTerm { get; set; }
            public Nullable<int> numberYear { get; set; }
            public string titleDescription { get; set; }
            public int SchoolID { get; set; }
            public Nullable<System.DateTime> dStart { get; set; }
            public Nullable<System.DateTime> dEnd { get; set; }
            public Nullable<System.DateTime> moveInDate { get; set; }
            public Nullable<int> nTimeType { get; set; }
            public string ContactPeak { get; set; }
            public Nullable<int> nTLevel { get; set; }
        }
    }
}