using FingerprintPayment.Message.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static JabjaiMainClass.JWTToken;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class InvoicesList : Page
    {
        public StaffDataModel StaffModel { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            StaffModel = new StaffDataModel();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadHistoryList()
        {
            var jsonStream = "";
            HttpContext.Current.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonStream = inputStream.ReadToEnd();
            }
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int draw = Convert.ToInt32(jsonObject["draw"]);
            int pageIndex = Convert.ToInt32(jsonObject["page"]);
            int pageSize = Convert.ToInt32(jsonObject["length"]);
            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var school_id = GetUserData().CompanyID;

                var model = new BillHistoryModel();

                var school = (from a in dbmaster.TCompanies where a.nCompany == school_id select a).FirstOrDefault();

                model.Description = $"School ID:{school.nCompany} {school.sCompany}";

                var sql = $@"select 
                                current_year
                                ,current_term
                                ,create_date
                                ,due_date
                                ,invoice_code
                                ,invoice_link_url
                                ,reciept_code
                                ,reciept_link_url
                                ,approve_name
                                from 
                                (
	                                select 
	                                    isnull(d.Year,0) as 'current_year'
	                                    ,case when d.ProductId in(1,4,5,10)  then N'รายปี' else cast( d.CurrentTerm as varchar(2)) end as 'current_term'
	                                    ,format(d.InvoiceCreateDate , 'dd/MM/yyyy HH:mm','th-TH') as 'create_date'
	                                    ,d.InvoiceUrl as 'invoice_link_url'
	                                    ,d.InvoiceCode as 'invoice_code'
	                                    ,d.RecieptUrl as 'reciept_link_url'
	                                    ,d.RecieptCode as 'reciept_code'
	                                    ,format(d.DueDate, 'dd/MM/yyyy HH:mm','th-TH')  as 'due_date'                     
	                                    ,o.Name as 'approve_name'
	                                from TInvoice i 
                                    left join TInvoiceDetail d on i.InvoiceId = d.InvoiceId
	                                left join TAdmin o on d.UpdateBy = o.id2
	                                where isnull(d.RecordDelete,0)=0 and d.InvoiceCreateDate is not null and i.SchoolId={school_id}	
                                )as tmp 
                                group by current_year, current_term, create_date , due_date, invoice_code,invoice_link_url,reciept_code,reciept_link_url,approve_name
                                order by current_year desc,current_term desc";

                model.Items = dbmaster.Database.SqlQuery<ViewHistoryData>(sql).ToList();


                CollectionData<ViewHistoryData> data = new CollectionData<ViewHistoryData>();
                data.draw = draw;
                data.pageIndex = pageIndex;
                data.pageSize = pageSize;
                data.pageCount = (model.Items.Count / pageSize) + ((model.Items.Count % pageSize) == 0 ? 0 : 1);
                data.recordsTotal = model.Items.Count;
                data.recordsFiltered = model.Items.Count;
                data.data = model.Items;


                //return Json(new { draw = search.draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = schools });


                var json = JsonConvert.SerializeObject(data);

                return json;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadNotificationSettingList()
        {
            var jsonStream = "";
            HttpContext.Current.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonStream = inputStream.ReadToEnd();
            }
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int draw = Convert.ToInt32(jsonObject["draw"]);
            int pageIndex = Convert.ToInt32(jsonObject["page"]);
            int pageSize = Convert.ToInt32(jsonObject["length"]);
            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var school_id = GetUserData().CompanyID;

                var sql = $@"select s.NotificationSettingId, e.sEmp as 'StaffId', t.jobDescription as 'Position', e.sName + ' ' + e.sLastname as 'FullName'  ,isnull( e.sEmail ,'N/A') as 'Email',s.LineNotificationAccessToken as 'LineNotificationAccessToken'
                            from  JabjaiSchoolSingleDB.dbo.TEmployees e left join 
                            JabjaiSchoolSingleDB.dbo.TJobList t on e.nJobid=t.nJobid and e.SchoolID=t.SchoolID
                            inner join JabjaiMasterSingleDB.dbo.TNotificationSetting s on e.sEmp=s.StaffID
                            where e.SchoolID={school_id} order by s.CreateDate, s.UpdateDate desc";

                var items = dbmaster.Database.SqlQuery<StaffDataModel>(sql).ToList();

                foreach(var i in items)
                {
                    i.LineUrlConnect = WrapLineConnect(i.NotificationSettingId);
                }

                CollectionData<StaffDataModel> data = new CollectionData<StaffDataModel>();
                data.draw = draw;
                data.pageIndex = pageIndex;
                data.pageSize = pageSize;
                data.pageCount = (items.Count / pageSize) + ((items.Count % pageSize) == 0 ? 0 : 1);
                data.recordsTotal = items.Count;
                data.recordsFiltered = items.Count;
                data.data = items;

                var json = JsonConvert.SerializeObject(data);

                return json;
            }
        }

        [WebMethod(EnableSession = true)]
        public static object AddStaffNotification(StaffListDataModel data)
        {
            var message = "";
            var userData = GetUserData();

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                try
                {
                    var sql = $@"select * from TNotificationSetting where SchoolId={userData.CompanyID} and StaffID='{data.StaffId}'";
                    var items = dbmaster.Database.SqlQuery<StaffDataModel>(sql).FirstOrDefault();

                    if (items != null)
                    {
                        throw new Exception($"{data.FullName} มีในระบบแล้ว");
                    }

                    var sql3 = $@"insert TNotificationSetting(schoolId,StaffID,CreateDate,CreateBy) 
                                    values({userData.CompanyID},'{data.StaffId}',getdate(),{userData.UserID})";
                    dbmaster.Database.ExecuteSqlCommand(sql3);
                }
                catch (Exception err)
                {
                    message = err.Message;
                }
            }

            return JsonConvert.SerializeObject(new { message = message });
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetStaffByID(StaffDataModel data)
        {
            bool success = true;
            string message = "Success";

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var school_id = GetUserData().CompanyID;

                var sql = $@"select * from TNotificationSetting where NotificationSettingId={data.NotificationSettingId}";

                var items = dbmaster.Database.SqlQuery<StaffDataModel>(sql).FirstOrDefault();

                return JsonConvert.SerializeObject(new { success, message, data=items });
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetStaffList()
        {
            var school_id = GetUserData().CompanyID;
            using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(school_id, ConnectionDB.Read)))
            {
                var sql = $@"select e.sEmp as 'StaffId', e.sName + ' ' + e.sLastname as 'FullName',t.jobDescription 
                            from TEmployees e left join TJobList t on e.nJobid=t.nJobid and e.SchoolID=t.SchoolID
                            where e.cDel is null and e.SchoolID={school_id}";

                var items = jabJaiEntities.Database.SqlQuery<StaffListDataModel>(sql).ToList();

                return JsonConvert.SerializeObject(new { data = items });
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object RemoveNotification(StaffDataModel data)
        {
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                try
                {
                    if (data.NotificationSettingId > 0)
                    {
                        var sql2 = $@"delete TNotificationSetting where NotificationSettingId={data.NotificationSettingId}";
                        dbmaster.Database.ExecuteSqlCommand(sql2);
                    }
                }
                catch (Exception err)
                {

                }
            }

            return JsonConvert.SerializeObject(new { success = "success" });
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object ConnectLine(StaffDataModel data)
        {
            var url = GenLineConnect(data.NotificationSettingId);
            return JsonConvert.SerializeObject(new { success = true, url = url });
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object OpenQr(StaffDataModel data)
        {
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var sql = $@"select s.NotificationSettingId, e.sEmp as 'StaffId', t.jobDescription as 'Position', e.sName + ' ' + e.sLastname as 'FullName' 
                                from  JabjaiSchoolSingleDB.dbo.TEmployees e left join 
                                JabjaiSchoolSingleDB.dbo.TJobList t on e.nJobid=t.nJobid and e.SchoolID=t.SchoolID
                                inner join JabjaiMasterSingleDB.dbo.TNotificationSetting s on e.sEmp=s.StaffID
                                where s.NotificationSettingId={data.NotificationSettingId}";
                var staff = dbmaster.Database.SqlQuery<StaffDataModel>(sql).FirstOrDefault();

                var url = WrapLineConnect(data.NotificationSettingId);
                var qr = QRCodeFunction.Create(url, QRCodeGenerator.ECCLevel.L).Replace("data:image/png;base64,", "");

                return JsonConvert.SerializeObject(new { qr = qr,url = url, staff = staff });
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object DisConnectLine(StaffDataModel data)
        {
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                try
                {                    
                    if (data.NotificationSettingId > 0)
                    {
                        var sql2 = $@"update TNotificationSetting set 
                                        LineNotificationAccessToken='',                                
                                        UpdateDate=getdate(),
                                        UpdateBy={GetUserData().UserID}
                                        where NotificationSettingId={data.NotificationSettingId}";
                        dbmaster.Database.ExecuteSqlCommand(sql2);
                    }                                     
                }
                catch (Exception err)
                {
                   
                }
            }

            return JsonConvert.SerializeObject(new { success="success" });
        }

        public static userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            return userData;
        }

        public static string GenLineConnect(int NotificationSettingId)
        {
            var url = $"https://notify-bot.line.me/oauth/authorize?response_type=code" +
                $"&client_id={ConfigurationManager.AppSettings["LineClientID"]}" +
                $"&redirect_uri={ConfigurationManager.AppSettings["LineCallback"]}" +
                $"&scope=notify" +
                $"&state={NotificationSettingId}";

            return url;
        }

        public static string WrapLineConnect(int NotificationSettingId)
        {
            var url = $"{HttpContext.Current.Request.Url.Host}/modules/invoices/LineConnectQr.aspx?id={NotificationSettingId}";
            return url;
        }
    }

    public class BillHistoryModel
    {
        public string Description { get; set; }
        public List<ViewHistoryData> Items { get; set; }
    }

    public class ViewHistoryData
    {
        public int current_year { get; set; }
        public string current_term { get; set; }
        public int student_invoice { get; set; }
        public string price { get; set; }
        public string total_price { get; set; }
        public string create_date { get; set; }
        public string invoice_link_url { get; set; }
        public string invoice_code { get; set; }

        public string reciept_link_url { get; set; }
        public string reciept_code { get; set; }
        public string due_date { get; set; }

        public string approve_name { get; set; }
    }

    public class StaffDataModel
    {
        public int NotificationSettingId { get; set; }
        public string Position { get; set; }
        public string FullName { get; set; }     
        public string Email { get; set; }       
        public string LineNotificationAccessToken { get; set; }
        public string LineUrlConnect { get; set; }
    }

    public class StaffListDataModel
    {
        public int StaffId { get; set; }
        public string FullName { get; set; }     
    }
}