using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.UserVerify
{
    public partial class LineCallback : System.Web.UI.Page
    {
        protected int SCHOOLID = 0;
        protected int STUDENTID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var code = Request.QueryString["code"];
                var state = Request.QueryString["state"];

                if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(state))
                {
                    var client = new RestClient($@"https://notify-bot.line.me/oauth/token?grant_type=authorization_code&code={code}&redirect_uri={ConfigurationManager.AppSettings["UserVerifyLineCallback"]}&client_id={ConfigurationManager.AppSettings["UserVerifyLineClientID"]}&client_secret={ConfigurationManager.AppSettings["UserVerifyLineClientSecret"]}");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    IRestResponse response = client.Execute(request);

                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        LINETokenModel lineTokenModel = JsonConvert.DeserializeObject<LINETokenModel>(response.Content);

                        var connectID = int.Parse(state);

                        using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(307,ConnectionDB.Read)))
                        {
                            if (!string.IsNullOrEmpty(lineTokenModel.AccessToken))
                            {
                                var query = $@"UPDATE TUserVerifyLINEConnect SET LINEAuthorizeCode='{code}', LINEToken='{lineTokenModel.AccessToken}', ConnectDate=GETDATE(), Status=1 WHERE ConnectID={connectID}";
                                ctx.Database.ExecuteSqlCommand(query);
                            }
                            else
                            {
                                try
                                {
                                    //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
                                    //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
                                    //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

                                    //string connectionString = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);

                                    //SqlConnection _conn = new SqlConnection(connectionString);
                                    fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Json Data[Modules/UserVerify/LineCallback]:{0}')", response.Content));
                                }
                                catch { }
                            }

                            var userVerifyLINEConnect = ctx.TUserVerifyLINEConnects.Where(w => w.ConnectID == connectID).FirstOrDefault();
                            if (userVerifyLINEConnect != null)
                            {
                                SCHOOLID = userVerifyLINEConnect.SchoolID;
                                STUDENTID = userVerifyLINEConnect.UserID;
                            }

                            if (userVerifyLINEConnect != null && userVerifyLINEConnect.Status == 1)
                            {
                                ltrIcon.Text = @"<span class=""material-icons text-success display-1 pb-2"" style=""color: #21c276;"">check_circle</span>";
                                ltrMessage.Text = "เชื่อมต่อ Line สำเร็จ";
                                ltrScript.Text = @"<script>document.getElementById(""btnClose"").click();</script>";
                            }
                            else
                            {
                                ltrIcon.Text = @"<span class=""material-icons text-danger display-1 pb-2"" style=""color: #21c276;"">cancel</span>";
                                ltrMessage.Text = "เชื่อมต่อ Line ไม่สำเร็จ";
                                ltrErrorMessage.Text = @"<span class=""d-block h6"">[Error Code: 104, Message: Failed to connect LINE.]</span>";
                            }
                        }
                    }
                    else
                    {
                        ltrIcon.Text = @"<span class=""material-icons text-danger display-1 pb-2"" style=""color: #21c276;"">cancel</span>";
                        ltrMessage.Text = "เชื่อมต่อ Line ไม่สำเร็จ";
                        ltrErrorMessage.Text = @"<span class=""d-block h6"">[Error Code: 101, Message: API LINE Token has a problem.]</span>";
                    }
                }
                else
                {
                    ltrIcon.Text = @"<span class=""material-icons text-danger display-1 pb-2"" style=""color: #21c276;"">cancel</span>";
                    ltrMessage.Text = "เชื่อมต่อ Line ไม่สำเร็จ";
                    ltrErrorMessage.Text = @"<span class=""d-block h6"">[Error Code: 102, Message: The parameter is invalid.]</span>";
                }
            }
            catch (Exception ex)
            {
                ltrIcon.Text = @"<span class=""material-icons text-danger display-1 pb-2"" style=""color: #21c276;"">cancel</span>";
                ltrMessage.Text = "เชื่อมต่อ Line ไม่สำเร็จ";
                ltrErrorMessage.Text = $@"<span class=""d-block h6"">[Error Code: 103, Message: {ex.Message}]</span>";
            }
        }

        public class LINETokenModel
        {
            [JsonProperty(PropertyName = "status")]
            public int status { get; set; }

            [JsonProperty(PropertyName = "message")]
            public string Message { get; set; }

            [JsonProperty(PropertyName = "access_token")]
            public string AccessToken { get; set; }
        }
    }
}