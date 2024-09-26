using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.UI;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class LineCallback : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                lblMessage2.Text = "";
                var code = Request.QueryString["code"];
                var state = Request.QueryString["state"];

                if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(state))
                {
                    var id = int.Parse(state);

                    WebClient wc = new WebClient();
                    string targetAddress = "https://notify-bot.line.me/oauth/token";
                    wc.Encoding = Encoding.UTF8;
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    NameValueCollection nc = new NameValueCollection();
                    nc["grant_type"] = "authorization_code";
                    nc["code"] = code; //the value from Step 2
                    nc["redirect_uri"] = ConfigurationManager.AppSettings["LineCallback"];
                    nc["client_id"] = ConfigurationManager.AppSettings["LineClientID"];
                    nc["client_secret"] = ConfigurationManager.AppSettings["LineClientSecret"];
                    byte[] bResult = wc.UploadValues(targetAddress, nc);
                    string result = Encoding.UTF8.GetString(bResult);

                    LineAuthModel setting = JsonConvert.DeserializeObject<LineAuthModel>(result);

                    if (!string.IsNullOrEmpty(result))
                    {
                        using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                        {
                            var sql = $@"update TNotificationSetting set 
                                    LineNotificationAccessToken='{setting.access_token}',
                                    UpdateDate=getdate()
                                    where NotificationSettingId={id}";
                            dbmaster.Database.ExecuteSqlCommand(sql);
                        }
                      
                        lblMessage.Text = "เย้ ยินดีด้วย ท่านเชื่อมต่อ School Bright Billing สำเร็จ";
                        lblMessage2.Text = "ต่อจากนี้ท่านจะได้รับการแจ้งเตือนใบแจ้งหนี้ค่าระบบ School Bright ผ่าน Line อัติโนมัติ";
                        //Response.Redirect("InvoicesList.aspx");
                    }
                }
            }
            catch(Exception ex)
            {
                lblMessage.Text = "เกิดข้อผิดพลาดกรุณาลองใหม่";
                lblMessage2.Text = "";
            }
        }

        public class LineAuthModel
        {
            public int status { get; set; }
            public string message { get; set; }
            public string access_token { get; set; }
        }
    }   
}