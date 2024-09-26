using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using QRCoder;
using Sentry;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace FingerprintPayment.PaymentGateway.KTB
{
    /// <summary>
    /// Summary description for GenerateQRCode
    /// </summary>
    public class GenerateQRCode : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            bool isSuccess = true;
            string message = "Generate QRCode/Barcode Successfully!";
            string resDescTH = "", resDescEN = "";

            GenPara genPara = new GenPara();
            QRCodeModel data = new QRCodeModel();

            try
            {
                string jsonString = string.Empty;
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    jsonString = reader.ReadToEnd();
                }
                genPara = JsonConvert.DeserializeObject<GenPara>(jsonString);


                // Log request generate qr code / barcode
                //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
                //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
                //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

                //string strconn = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);
                //SqlConnection _conn = new SqlConnection(strconn);
                fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Data[PaymentGateway/KTB/GenerateQRCode.ashx]{0}')", JsonConvert.SerializeObject(genPara)));


                // Get school data 
                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(genPara.schoolID,ConnectionDB.Read)))
                {
                    if (!fcommon.PaymentSetting(mctx, "Canteen QRCode", "KTB"))
                    {
                        isSuccess = false;
                        message = "Server Maintenance";
                        data = null;
                        resDescTH = "ปิดระบบพร้อมเพย์ชั่วคราว เนื่องจากระบบขัดข้อง\r\nเจ้าหน้าที่กำลังดำเนินการแก้ไขอย่างเร่งด่วน";
                        resDescEN = "PromptPay system is temporarily closed due to a system failure.\r\nThe officials is working to fix it urgently.";
                    }
                    else
                    {
                        string ref1Type = "01";
                        string suffix = "31"; // 30: ค่าเทอม, 31: เติมเงิน, 32: ร้านค้า
                        if (genPara.shopID != null)
                        {
                            ref1Type = "02";
                            suffix = "32";
                        }
                        else if (genPara.invoiceID != null)
                        {
                            ref1Type = "03";
                            suffix = "30";
                        }

                        // Ref1: 011093670214032330 - [01][1093][670214][032330] - [trans type][school id][yymmdd][run no of day] - 01: topup, 02: shop, 03: invoice
                        DateTime currentDate = DateTime.Now.Date;
                        int runNumber = ctx.Database.SqlQuery<int>(string.Format(@"SELECT COUNT(*)+1 FROM KTBTransaction WHERE SchoolID = {0} AND CreateDate BETWEEN '{1:yyyy-MM-dd} 00:00:00' AND '{1:yyyy-MM-dd} 23:59:59'", genPara.schoolID, currentDate)).FirstOrDefault();
                        string ref1 = string.Format(@"{0}{1:D4}{2}{3:D6}", ref1Type
                            , genPara.schoolID
                            , DateTime.Now.ToString("yyMMdd", new CultureInfo("th-TH"))
                            , runNumber);

                        int yearID = int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));
                        string newGuid = Guid.NewGuid().ToString();

                        // Save & Get Transaction
                        KTBTransaction ktbTransaction = new KTBTransaction
                        {
                            SchoolID = genPara.schoolID,
                            Year = yearID,
                            GUID = newGuid,
                            Ref1 = ref1,
                            IMEI = genPara.imei,
                            UserID = genPara.userID,
                            Amount = genPara.amount,
                            InvoiceID = genPara.invoiceID,
                            ShopID = genPara.shopID,
                            AppName = genPara.appName,
                            SellDetail = genPara.sellDetail,
                            CreateDate = DateTime.Now,
                            cDel = false
                        };
                        ctx.KTBTransaction.Add(ktbTransaction);
                        ctx.SaveChanges();

                        int transID = ktbTransaction.TransactionID;

                        string ref2Type = "00"; // 01: student, 02: teacher, 03: shop, 04: invoice
                        if (genPara.shopID != null)
                        {
                            ref2Type = "03";
                        }
                        else if (genPara.invoiceID != null)
                        {
                            ref2Type = "04";
                        }
                        else
                        {
                            MasterEntity.TUser userObj = mctx.TUsers.Where(w => w.sID == genPara.userID).FirstOrDefault();
                            if (userObj != null)
                            {
                                switch (userObj.cType)
                                {
                                    case "0": ref2Type = "01"; break;
                                    case "1": ref2Type = "02"; break;
                                }
                            }
                        }

                        // Ref2: 010070012400000001 - [01][00700124][00000001] - [user type][user id or shop id or invoice id][trans id]
                        string ref2 = string.Format(@"{0}{1:D8}{2:D8}", ref2Type
                            , (ref2Type == "01" || ref2Type == "02" ? genPara.userID : (ref2Type == "03" ? genPara.shopID : (ref2Type == "04" ? genPara.invoiceID : 0)))
                            , transID);

                        ktbTransaction.Ref2 = ref2;
                        ctx.SaveChanges();

                        string amount = genPara.amount.ToString("#0.00").Replace(".", ""); ;

                        string taxID = "0105559067813" + suffix;
                        TCompany companyObj = mctx.TCompanies.Where(w => w.nCompany == genPara.schoolID).FirstOrDefault();
                        if (companyObj != null && !string.IsNullOrEmpty(companyObj.TaxId))
                        {
                            if (companyObj.TaxId == "0105559067813")
                            {
                                taxID = companyObj.TaxId + "00"; // Tax of Jabjai Corporation Co., Ltd.
                            }
                            else
                            {
                                taxID = companyObj.TaxId + suffix;
                            }
                        }
                        else
                        {
                            throw new Exception("Tax id not found.");
                        }

                        string code = "|" + taxID + "\r\n" + ref1 + "\r\n" + ref2 + "\r\n" + amount;

                        data.qrCode = code;
                        data.qrCodeBase64 = QRCodeFunction.Create(data.qrCode, QRCodeGenerator.ECCLevel.H, false, false).Replace("data:image/png;base64,", "");

                        data.barCode = code;
                        data.barCodeBase64 = BarCodeFunction.Create(data.barCode, BarCodeFunction.BarcodeTYPE.CODE128, false, BarCodeFunction.Alignment.CENTER, 550, 100);
                    }
                }
            }
            catch (Exception err)
            {
                SentrySdk.CaptureException(err);

                //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
                //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
                //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

                //string strconn = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);

                //SqlConnection _conn = new SqlConnection(strconn);

                fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Error Message:[PaymentGateway/KTB/GenerateQRCode]:{0}')", err.Message.Replace("'", "")));

                string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

                LINENotify notify = new LINENotify();
                notify.LineSBErrorSend(new LINENotifyDATA
                {
                    Parameter = new { genPara.schoolID, genPara.shopID, genPara.amount },
                    Date_Time = DateTime.Now,
                    URL = $@"{domainName}/PaymentGateway/KTB/GenerateQRCode.ashx",
                    Error_Method = "KTB - Generate QRCode/Barcode"
                }, err);

                isSuccess = false;
                message = err.Message;
            }

            var result = new { success = isSuccess, message, data, resDescTH, resDescEN };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }

    public class GenPara
    {
        public int schoolID { get; set; }
        public int userID { get; set; }
        public int? shopID { get; set; }
        public int? invoiceID { get; set; }
        public decimal amount { get; set; }
        public string imei { get; set; }
        public string type { get; set; }
        public string appName { get; set; }
        public string sellDetail { get; set; }
    }

    public class QRCodeModel
    {
        public string qrCode { get; set; }
        public string qrCodeBase64 { get; set; }
        public string barCode { get; set; }
        public string barCodeBase64 { get; set; }
    }

}