using EntityFramework.BulkInsert.Extensions;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
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
    public class product_Import : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            int ShopID = int.Parse(EncryptMD5.UrlTokenDecode(context.Request["ShopID"]));
            HttpFileCollection files = context.Request.Files;
            bool isSuccess = true;
            int resultCode = 200;
            string message = "Success", errorMessage = "";
            dynamic rss = null;
            int ItemRows = 0;
            List<IProduct> rssSuccess = new List<IProduct>();
            List<IProduct> rssError = new List<IProduct>();

            HttpContext.Current.Session["ProductImportPercentage"] = 0;
            if (context.Request.Files.Count > 0)
            {
                string filename = files[0].FileName.Replace(".xlsx", DateTime.Now.ToString("ddMMyyyyHHmmssffff") + ".xlsx");

                string targetpath = HttpContext.Current.Server.MapPath("~/images/");
                files[0].SaveAs(targetpath + filename);
                string pathtoexcelfile = targetpath + filename;
                FileInfo newFile = new FileInfo(pathtoexcelfile);
                ExcelPackage xlPackage = new ExcelPackage(newFile);
                var ds_package = xlPackage.ToDataSet(1);
                bool isUpdate = false;

                File.Delete(targetpath + filename);

                List<string> HeaderNameArray = new List<string> { "Date", "Invoice", "Account code", "Student number", "Invoice amount", "Discount", "Due date" };

                try
                {
                    using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                    {

                        var ProductType = jabJaiEntities.TTypes.Where(w => w.SchoolID == userData.CompanyID && w.shop_id == ShopID && w.cDel == null).ToList();
                        List<TProduct> Iproducts = new List<TProduct>();

                        double _r = ds_package.Tables[0].Rows.Count;
                        double _i = 0;

                        foreach (DataRow dataRow in ds_package.Tables[0].Rows)
                        {
                            HttpContext.Current.Session["ProductImportPercentage"] = (_i++ * 100.0) / _r;
                            string TypeName = dataRow["ประเภทสินค้า"].ToString();
                            string BarCode = dataRow["รหัสบาร์โค้ด"].ToString().Trim();

                            if (string.IsNullOrEmpty(BarCode)) continue;

                            decimal nCost = decimal.Parse((string.IsNullOrEmpty(dataRow["ราคาต้นทุน"].ToString()) ? "0" : dataRow["ราคาต้นทุน"]).ToString().Trim());
                            decimal nPrice = decimal.Parse((dataRow["ราคาขาย"] ?? "0").ToString().Trim());
                            var nWarn = ((dataRow["จำนวนแจ้งเตือน"] ?? "0").ToString().Trim()).ToNumber<int>();
                            string cStock = dataRow["ไม่นับสต๊อก"].ToString().Trim() == "นับ" ? "0" : "1";
                            string ProductName = dataRow["ชื่อสินค้า"].ToString();

                            if (string.IsNullOrEmpty(BarCode))
                            {
                                continue;
                            }

                            ItemRows += 1;
                            var type = ProductType.FirstOrDefault(f => f.sType == TypeName);
                            if (type == null)
                            {
                                rssError.Add(new IProduct
                                {
                                    Rows = ItemRows,
                                    BarCode = BarCode,
                                    nPrice = nPrice,
                                    cStock = cStock,
                                    nCost = nCost,
                                    nWarn = nWarn,
                                    TypeName = TypeName,
                                    ProductName = ProductName,
                                    Note = $"บรรทัดที่ {ItemRows}  : ไม่มีประเภทสินค้า "
                                }); ;

                                continue;
                            }
                            else
                            {
                                var product = jabJaiEntities.TProducts.FirstOrDefault(w => w.SchoolID == userData.CompanyID &&
           w.nType == type.nTypeID && w.sBarCode.Trim() == BarCode && w.cDel == null);

                                if (product == null)
                                {
                                    Iproducts.Add(new TProduct
                                    {
                                        CreatedBy = userData.UserID,
                                        CreatedDate = DateTime.Now,
                                        sBarCode = BarCode,
                                        nPrice = nPrice,
                                        cStock = cStock,
                                        nCost = nCost,
                                        nWarn = nWarn,
                                        sProduct = ProductName,
                                        nType = type.nTypeID,
                                        SchoolID = userData.CompanyID,
                                    });

                                    rssSuccess.Add(new IProduct
                                    {
                                        BarCode = BarCode,
                                        ProductName = ProductName,
                                        nPrice = nPrice,
                                        cStock = cStock,
                                        nCost = nCost,
                                        nWarn = nWarn,
                                        TypeName = TypeName
                                    });

                                    isUpdate = true;
                                }
                                else
                                {
                                    isUpdate = true;
                                    product.UpdatedBy = userData.UserID;
                                    product.UpdatedDate = DateTime.Now;
                                    product.sProduct = ProductName;
                                    product.nCost = nCost;
                                    product.nWarn = nWarn;
                                    product.cStock = cStock;
                                    product.nPrice = nPrice;
                                }
                            }
                        }

                        if (Iproducts.Count() > 0)
                        {
                            if (Iproducts.Count() < 500)
                            {
                                jabJaiEntities.TProducts.AddRange(Iproducts);
                            }
                            else
                            {
                                jabJaiEntities.BulkInsert<TProduct>(Iproducts);
                            }
                        }

                        if (isUpdate)
                        {
                            jabJaiEntities.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Session["ProductImportPercentage"] = 0;
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
            var d = new
            {
                Item = ItemRows,
                Success = serializer.Serialize(rssSuccess),
                Error = serializer.Serialize(rssError),
                errorMessage,
                isSuccess,
                resultCode
            };

            var result = new { success = isSuccess, message, resultCode, errorMessage };
            //context.Response.Expires = -1;
            context.Response.ContentType = "application/json";
            context.Response.Write(serializer.Serialize(d));

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

        public class IProduct
        {
            public int Rows { get; set; }
            public string TypeName { get; set; }
            public string ProductName { get; set; }
            public string BarCode { get; set; }
            public decimal nCost { get; set; }
            public decimal nPrice { get; set; }
            public int? nWarn { get; set; }
            public string cStock { get; set; }
            public string Note { get; set; }
        }
    }
}