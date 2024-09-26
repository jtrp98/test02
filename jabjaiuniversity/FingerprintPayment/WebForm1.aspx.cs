using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;
using System.Xml.Linq;
using System.Data.SqlClient;
using QRCoder;
using System.Drawing;
using System.Configuration;
using PeakengineAPI;
using System.Diagnostics;
using System.Data.Linq;
using System.Data.Entity;

namespace FingerprintPayment
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectId = ConfigurationManager.AppSettings["connectId"];
        string password = ConfigurationManager.AppSettings["password"];
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<img src='" + QRCodeFunction.Create("system.jabjai.school", QRCodeGenerator.ECCLevel.Q) + "'>");
            Response.Write("<img src='" + BarCodeFunction.Create(DateTime.Now.ToString("138000356215"), BarCodeFunction.BarcodeTYPE.CODE128, true
                , BarCodeFunction.Alignment.CENTER, 400, 120) + "'>");

            string Key = EncryptMD5.Encrypt("3");
            Response.Write("Encrypt MD5 : " + HttpUtility.UrlEncode("3"));
            Response.Write("~<br/>Decrypt MD5 : " + EncryptMD5.Decrypt(Key) + "<br/>");

            string Time_Stamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string mkey = "jabjai_peak_uat";
            byte[] array = Encoding.ASCII.GetBytes(mkey);

            var q = ConfigPeakenineAPI.ReadXML(Server.MapPath("~/xml"));
            if (q != null) Response.Write(" " + q.Client_Token);

            string UrlTokenEncode = EncryptMD5.UrlTokenEncode("{ \"School_id\" : 37 }");
            Response.Write("<br/>UrlTokenEncode : " + UrlTokenEncode);
            Response.Write("<br/>UrlTokenDecode : " + EncryptMD5.UrlTokenDecode(UrlTokenEncode) + "<br/>" + Guid.NewGuid().ToString());
            Response.Write(DateTime.Now.ToString());

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                var stopWatch = Stopwatch.StartNew();
                //var q1 = (from a in dbmaster.TUsers
                //          where a.cDel == null
                //          select a).ToList();
                //stopWatch.Stop();
                //Response.Write(string.Format("<br/>Number of cores: {0} Time: {1:FFFFFFF}", 1, stopWatch.Elapsed));

                //stopWatch = Stopwatch.StartNew();
                var qq2 = dbmaster.TUsers.AsParallel();
                var qq1 = dbmaster.TCompanies.AsParallel();

                var q2 = (from a in qq2
                          join b in qq1 on a.nCompany equals b.nCompany
                          where a.cDel == null

                          select new { a, b });

                stopWatch.Stop();
                Response.Write(string.Format("<br/>Number of cores: {0} Time: {1:FFFFFFF}", 2, stopWatch.Elapsed));

                //stopWatch = Stopwatch.StartNew();
                //var q3 = (from a in dbmaster.TUsers.AsParallel()
                //          where a.cDel == null
                //          select a).AsParallel().ToList();
                //stopWatch.Stop();
                //Response.Write(string.Format("<br/>Number of cores: {0} Time: {1:FFFFFFF}", 3, stopWatch.Elapsed));

                //stopWatch = Stopwatch.StartNew();
                //var q4 = (from a in dbmaster.TUsers.AsParallel()
                //          where a.cDel == null
                //          select a).ToList();
                //stopWatch.Stop();
                //Response.Write(string.Format("<br/>Number of cores: {0} Time: {1:FFFFFFF}", 4, stopWatch.Elapsed));

                //stopWatch = Stopwatch.StartNew();
                //var q5 = (from a in dbmaster.TUsers
                //          where a.cDel == null
                //          select a).AsParallel().ToList();
                //stopWatch.Stop();
                //Response.Write(string.Format("<br/>Number of cores: {0} Time: {1:FFFFFFF}", 5, stopWatch.Elapsed));

                //stopWatch = Stopwatch.StartNew();
                //var q6 = (from a in dbmaster.TUsers.AsParallel()
                //          where a.cDel == null
                //          select a);

                //var q7 = q4.AsParallel().ToList();

                //stopWatch.Stop();
                //Response.Write(string.Format("<br/>Number of cores: {0} Time: {1:FFFFFFF}", 6, stopWatch.Elapsed));

                //stopWatch = Stopwatch.StartNew();
                //var q8 = (from a in dbmaster.TUsers
                //          where a.cDel == null
                //          select a).ToListAsync();

                //var q9 = await q8;

                //stopWatch.Stop();
                //Response.Write(string.Format("<br/>Number of cores: {0} Time: {1:FFFFFFF}", 7, stopWatch.Elapsed));
            }

            //using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            //{
            //    var f_company = dbmaster.TCompanies.Find(13);
            //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(f_company)))
            //    {
            //        var q_usermaster = dbmaster.TUsers.Where(w => w.nCompany == 13 && w.cType == "0" && string.IsNullOrEmpty(w.username)).ToList();
            //        var q_studennt = dbschool.TUsers.ToList();
            //        foreach (var data in q_usermaster)
            //        {
            //            //data.sStudentID = data.sIdentification;
            //            var f_user = q_studennt.FirstOrDefault(f => f.sID == data.nSystemID);
            //            data.username = f_user.sStudentID;
            //        }

            //        dbmaster.SaveChanges();
            //    }
            //}

        }

        static string ShaHash(string value, string key)
        {
            using (var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key)))
            {
                return ByteToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(value)));
            }
        }

        static string ByteToString(IEnumerable<byte> data)
        {
            return string.Concat(data.Select(b => b.ToString("x2")));
        }

        private async Task send_req()
        {
            //string time_stamp = "20180219094801";

            string connectId = ConfigurationManager.AppSettings["connectId"];
            string password = ConfigurationManager.AppSettings["password"];
            string time_stamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string time_signature = GetMACHash(time_stamp, connectId);
            HttpWebRequest tRequest;
            tRequest = (HttpWebRequest)WebRequest.Create("http://peakengineapidev.azurewebsites.net/api/v1/clienttoken");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers["Time-Stamp"] = time_stamp;
            tRequest.Headers["Time-Signature"] = time_signature;

            string postData = "{\"PeakClientToken\":{\"connectId\":\"" + connectId + "\",\"password\":\"" + password + "\"}} ";

            Response.Write("time_stamp : " + time_stamp + "<br/> time_signature : " + time_signature);
            Response.Write("<br/>time_signature : " + "c486f8eefb8166908cf612bd78824a9080fad343");
            Response.Write("<br/>" + postData);
            //Console.WriteLine(postData);

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;
            Stream dataStream = await tRequest.GetRequestStreamAsync();
            await dataStream.WriteAsync(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse tResponse = await tRequest.GetResponseAsync();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = await tReader.ReadToEndAsync();  //Get response from GCM server  
            tReader.Close();
            dataStream.Close();
            tResponse.Close();

            Response.Write("<br/>" + sResponseFromServer);
        }

        protected void btn_Click(object sender, EventArgs e)
        {

            var qXML = ConfigPeakenineAPI.ReadXML(Server.MapPath("~/xml"));
            if (qXML != null && qXML.Client_Time_Stamp.HasValue && qXML.Client_Time_Stamp.Value.AddHours(22) <= DateTime.UtcNow)
            {
                Response.Write(qXML.Client_Token);
            }
            else
            {
                Dictionary<string, string> HttpWebRequestHeader = new Dictionary<string, string>()
                {
                    { "Time-Stamp", connectId},
                    { "Time-Signature", connectId},
                };

                var qToken = ClientToken.SendRequest(connectId, password);

                ConfigPeakenineAPI.WriteXML(new ConfigPeakenineAPI.TokenAPI
                {
                    Client_Time_Stamp = DateTime.UtcNow,
                    Client_Token = qToken.token,
                    User_Token = qXML == null ? "" : qXML.User_Token
                }, Server.MapPath("~/xml"));

                Response.Write(qToken.token);
            }
        }

        public string GetMACHash(string textToHash, string key)
        {
            //  secret key shared by sender and receiver.
            byte[] secretkey = new Byte[64];
            string result = null;

            //get secret key
            secretkey = Encoding.UTF8.GetBytes(key);

            // Initialize the keyed hash object.
            HMACSHA1 myhmacsha1 = new HMACSHA1(secretkey);

            // Compute the hash of the text.
            byte[] bytedText = Encoding.UTF8.GetBytes(textToHash);
            byte[] hashValue = myhmacsha1.ComputeHash(bytedText);

            //Base-64 Encode the results and strip off ending '==', if it exists
            result = string.Concat(hashValue.Select(b => b.ToString("x2")));

            //set response
            return result;

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            var qXML = ConfigPeakenineAPI.ReadXML(Server.MapPath("~/xml"));
            var result = await UserToken.SendRequest(connectId, password, qXML.Client_Token, "tana074@hotmail.com", "ทดสอบระบบ");

            ConfigPeakenineAPI.WriteXML(new ConfigPeakenineAPI.TokenAPI
            {
                Client_Time_Stamp = qXML.Client_Time_Stamp,
                Client_Token = qXML.Client_Token,
                User_Token = result.authorizedId
            }, Server.MapPath("~/xml"));

            Response.Write("<br/> UserToken : " + result.authorizedId);
            Response.Write("<br/> logInUrl : " + result.logInUrl);
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            var qXML = ConfigPeakenineAPI.GetToken(3, connectId, password);
            var q = await ProductsAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token);
            Response.Write("<br/> result : " + q);
            foreach (var product_data in q.products)
            {
                Response.Write("<br/> Product Name : " + product_data.name);
                Response.Write("<br/> sell Value : " + product_data.sellValue);
                Response.Write("<br/> Balance Amount : " + product_data.carryingBalanceAmount);
            }

            Response.Write("<br/> User-Token : " + qXML.User_Token);
            Response.Write("<br/> Client-Token : " + qXML.Client_Token);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var qXML = ConfigPeakenineAPI.GetToken(EncryptMD5.Encrypt("3"));
            var q = ProductsAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, new List<ProductsAPI.products>() {
                new ProductsAPI.products
                {
                    name  ="ค่าหนังสือ",
                    purchaseValue = 100,
                    purchaseVattype = 3,
                    sellValue =100,
                    sellVatType = 3,
                    description = "Design by Peak",
                    carryingBalanceValue= 15,
                    carryingBalanceAmount = 20
                }
            });

            foreach (var product_data in q.products)
            {
                Response.Write("<br/> Product Name : " + product_data.name);
                Response.Write("<br/> Product id : " + product_data.id);
                Response.Write("<br/> sell Value : " + product_data.sellValue);
                Response.Write("<br/> Balance Amount : " + product_data.carryingBalanceAmount);
            }

            Response.Write("<br/> result : " + q);
            Response.Write("<br/> User-Token : " + qXML.User_Token);
            Response.Write("<br/> Client-Token : " + qXML.Client_Token);
        }

        protected async void Button4_Click(object sender, EventArgs e)
        {
            var qXML = ConfigPeakenineAPI.GetToken(EncryptMD5.Encrypt("3"));
            var q = await ServicesAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token);
            Response.Write("<br/> result : " + q);
            //foreach (var services_data in q.services)
            //{
            //    Response.Write("<br/> Services Name : " + services_data.name);
            //    Response.Write("<br/> sell Value : " + services_data.sellValue);
            //    Response.Write("<br/> ID : " + services_data.id);
            //}

            Response.Write("<br/> User-Token : " + qXML.User_Token);
            Response.Write("<br/> Client-Token : " + qXML.Client_Token);
        }

        protected async void Button5_Click(object sender, EventArgs e)
        {
            var qXML = ConfigPeakenineAPI.ReadXML(Server.MapPath("~/xml"));
            var q = await ServicesAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, new List<ServicesAPI.Services>() {
                new ServicesAPI.Services
                {
                    name  ="API service Test 01",
                    purchaseValue = 100,
                    purchaseVattype = 3,
                    sellValue =100,
                    sellVatType = 3,
                    description = "Fast and Clean",
                }
            });
            Response.Write("<br/> result : " + q);
            Response.Write("<br/> User-Token : " + qXML.User_Token);
            Response.Write("<br/> Client-Token : " + qXML.Client_Token);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                var qcompany = dbmaster.TCompanies.Where(w => w.nType == "1").ToList();
                int i = 0;
                foreach (var data in qcompany)
                {
                    try
                    {
                        SqlConnection conn = new SqlConnection("Server=" + data.sServer + ";Initial Catalog=" + data.sDatabases
                            + ";Persist Security Info=False;User ID=" + data.sUser + ";Password=" + data.sPassword + ";Connection Timeout=30;");
                        if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                        conn.Open();
                        string SQLQuery = @"" + txtQuery.Text;
                        SqlCommand comm = new SqlCommand(SQLQuery, conn);
                        comm.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        i += 1;
                    }
                }

                Response.Write(i);
            }
        }

        protected async void Button7_Click(object sender, EventArgs e)
        {
            //string Key = EncryptMD5.Encrypt("3");
            try
            {
                string connectId = ConfigurationManager.AppSettings["connectId"];
                string password = ConfigurationManager.AppSettings["password"];
                var qXML = ConfigPeakenineAPI.GetToken(3, connectId, password);
                if (qXML == null)
                {
                    //string Key = EncryptMD5.Encrypt("3");
                    //Response.Write("Encrypt MD5 : " + Key);
                    //Response.Write("Decrypt MD5 : " + EncryptMD5.Decrypt(Key));
                }
                else
                {
                    var q = ContactsAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token);
                    Response.Write("<br/> result : " + q);

                    //foreach (var data in q)
                    //{
                    //    Response.Write("<br/> First Name : " + data.name);
                    //    Response.Write("<br/> Last Name : " + data.contactLastName);
                    //    Response.Write("<br/> Code : " + data.id);
                    //}

                    Response.Write("<br/> User-Token : " + qXML.User_Token);
                    Response.Write("<br/> Client-Token : " + qXML.Client_Token);
                }
            }
            catch (Exception ex)
            {
                //Response.Write("Encrypt MD5 : " + Key);
                //Response.Write("Decrypt MD5 : " + EncryptMD5.Decrypt(Key));
            }
        }

        protected async void Button8_Click(object sender, EventArgs e)
        {
            var qXML = ConfigPeakenineAPI.ReadXML(Server.MapPath("~/xml"));
            var q = ContactsAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, new List<ContactsAPI.Contacts>() {
                new ContactsAPI.Contacts
                {
                    name = "TestAPI Contacts 02",
                    type = 1,
                    taxNumber = 1234567890,
                    branchCode = "00000",
                    address = "145/161 Soi Khu Bon 27/7, Khu Bon Rd. ",
                    subDistrict = "Tarang",
                    district = "Bangkhen",
                    province = "Bangkok",
                    country = "Thailand",
                    postCode = "10220",
                    callCenterNumber = "0863621919",
                    faxNumber = "0863621920",
                    email = "peak@peakengine.com",
                    website = " peakengine.com ",
                    contactFirstName = "Peak",
                    contactLastName = "Engine",
                    contactNickName = "Peak",
                    contactPosition = "Developer",
                    contactPhoneNumber = "0955559999",
                    contactEmail = "sutatpan@peakengine.com"
                }
            }, "");
            Response.Write("<br/> result : " + q);
            Response.Write("<br/> User-Token : " + qXML.User_Token);
            Response.Write("<br/> Client-Token : " + qXML.Client_Token);
        }

        protected async void Button9_Click(object sender, EventArgs e)
        {
            var qXML = ConfigPeakenineAPI.ReadXML(Server.MapPath("~/xml"));
            var q = await InvoiceAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token);
            Response.Write("<br/> result : " + q);

            //foreach (var data in q.invoices)
            //{
            //    Response.Write("<br/> First Name : " + data.name);
            //    Response.Write("<br/> Last Name : " + data.contactLastName);
            //    Response.Write("<br/> Code : " + data.id);
            //}

            Response.Write("<br/> User-Token : " + qXML.User_Token);
            Response.Write("<br/> Client-Token : " + qXML.Client_Token);
        }

        protected void Button10_Click(object sender, EventArgs e)
        {

        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            //using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            //{
            //    //var qCompany = dbmaster.TCompanies.Where(w => w.nCompany == 3).FirstOrDefault();
            //    var qCompany = dbmaster.TCompanies.Where(w => w.nType == "1").ToList();
            //    var q_menu = dbmaster.TMenus.Where(w => w.showmenu == true).ToList();
            //    var q_permission = dbmaster.permissions.ToList();
            //    var q_userMaster = dbmaster.TUsers.Where(w => w.cType == "1" && w.cDel == null).ToList();
            //    foreach (var q_company in qCompany)
            //    {
            //        using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(q_company)))
            //        {
            //            var l_add = new List<permission>();
            //            var q_user = dbschool.TEmployees.Where(w => w.cDel == null).ToList();
            //            foreach (var q_data in q_user)
            //            {
            //                var f_master = q_userMaster.FirstOrDefault(f => f.nSystemID == q_data.sEmp && f.nCompany == q_company.nCompany);
            //                if (f_master == null) continue;
            //                if (!string.IsNullOrEmpty(q_data.sClaim))
            //                {
            //                    for (int i = 0; i < q_data.sClaim.Length; i++)
            //                    {
            //                        var f_menu = q_menu.FirstOrDefault(f => f.MenuIndex == i);
            //                        if (f_menu == null) continue;
            //                        var f_permission = q_permission.FirstOrDefault(f => f.user_id == f_master.sID);
            //                        int actvice = int.Parse(q_data.sClaim.Substring(i, 1));
            //                        if (f_permission == null)
            //                        {
            //                            l_add.Add(new permission
            //                            {
            //                                actvice = actvice,
            //                                menu_id = f_menu.MenuId,
            //                                user_id = f_master.sID
            //                            });
            //                        }
            //                        else
            //                        {
            //                            f_permission.actvice = actvice;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    var l_permission = authentication.openMenu(dbmaster, q_menu, false);
            //                    authentication.PermissioUpdate(dbmaster, q_menu, f_master.sID, l_permission);
            //                }
            //            }
            //            dbmaster.permissions.AddRange(l_add);
            //            dbmaster.SaveChanges();
            //        }
            //    }
            //}
        }
    }
}