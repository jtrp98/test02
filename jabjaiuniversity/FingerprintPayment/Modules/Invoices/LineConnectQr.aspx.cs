using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class LineConnectQr : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var id = Request.QueryString["id"];

                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("not found state");                    
                }

                using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var sql = $@"select LineNotificationAccessToken
                                from  TNotificationSetting 
                                where NotificationSettingId={id}";

                    var staff = dbmaster.Database.SqlQuery<StaffDataModel>(sql).FirstOrDefault();

                    if (staff != null && !string.IsNullOrEmpty(staff.LineNotificationAccessToken))
                    {
                        lblMessage.Text = "คุณได้ทำการเชื่อมต่อ Line Notify แล้ว, หากข้อมูลไม่ถูกต้องกรุณาติดต่อผู้ดูแลระบ";
                        return;
                    }

                    var url = InvoicesList.GenLineConnect(int.Parse(id));

                    Response.Redirect(url,true);
                }
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }   
}