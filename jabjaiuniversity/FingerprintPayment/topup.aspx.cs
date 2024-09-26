using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using JabjaiTopup;
using System.Threading;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using System.Globalization;
using Amazon.XRay.Recorder.Handlers.AspNet;
using FingerprintPayment.Memory;
using FingerprintPayment.Class;
using Ninject.Activation;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System.Web.Services;

namespace FingerprintPayment
{
    public partial class topup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static object GetConfig()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var d = dbmaster.TSystemSetting
                  .Where(o => o.SchoolID == userData.CompanyID)
                  .FirstOrDefault();

                return new
                {
                    maxTopup = d?.MaxTopup ?? 1000,
                };
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<HistoryTopup> Historytopups()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                int user_id = int.Parse(HttpContext.Current.Session["sEmpID"].ToString());
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    string SQL = string.Format(@"
DECLARE @SCHOOLID INT = {0}
DECLARE @DAY DATE = convert(date,getdate())
DECLARE @sEmp INT = {2}

SELECT TOP 20 * FROM(SELECT CASE WHEN A.cType = 0 THEN B.sName + ' ' + B.sLastname
WHEN A.cType = 1 THEN C.sName + ' ' + C.sLastname
WHEN A.cType = 2 THEN D.UserName + ' (บัตรสำรอง)'
END AS name,A.cType,A.nMoney AS money,A.cType AS user_type,A.nBalance,A.MoneyID AS id,A.dMoney,
D.ReturnDate
FROM TMoney AS A 
LEFT OUTER JOIN TBackupCardHistory AS D ON A.CardHistoryID = D.CardHistoryID AND A.cType = 2
LEFT OUTER JOIN TUser AS B ON A.sID = B.sID AND A.SchoolID = B.SchoolID AND A.cType = 0 
LEFT OUTER JOIN TEmployees AS C ON A.sID = C.sEmp AND A.SchoolID = C.SchoolID AND A.cType = 1
WHERE A.SchoolID = @SCHOOLID AND A.dMoney > @DAY AND A.dayCancal IS NULL AND A.sEmp = @sEmp) TB
WHERE ReturnDate  IS NULL

ORDER BY dMoney DESC
", userData.CompanyID, DateTime.Today.Date, userData.UserID);
                    var q = dbschool.Database.SqlQuery<HistoryTopup>(SQL).ToList();

                    return q;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<HistoryTopup> SearchHistoryTopups(string user_id, string user_type, DateTime? topup_date)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                DateTime maxDay = DateTime.Today.AddDays(-14);
                //DateTime? dMoney = null;
                //if (!string.IsNullOrEmpty(topup_date)) dMoney = DateTime.ParseExact(topup_date, "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-us"));
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                int emp_id = int.Parse(HttpContext.Current.Session["sEmpID"].ToString());
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    var q = dbschool.Database.SqlQuery<HistoryTopup>(string.Format(@"DECLARE @SCHOOLID INT = {0}
DECLARE @DAY DATE = '{1:d}'
DECLARE @USER_ID varchar(100) = '{2}'
DECLARE @sEmp INT = {3}

SELECT TOP 20 CASE WHEN A.cType = 0 THEN B.sName + ' ' + B.sLastname
WHEN A.cType = 1 THEN C.sName + ' ' + C.sLastname
WHEN A.cType = 2 THEN D.UserName + ' (บัตรสำรอง)'
END AS name,A.cType,A.nMoney AS money,A.cType AS user_type,A.nBalance,A.MoneyID AS id,A.dMoney,A.CardHistoryID,
E.CardID
FROM TMoney AS A 
LEFT OUTER JOIN TUser AS B ON A.sID = B.sID AND A.SchoolID = B.SchoolID AND A.cType = 0
LEFT OUTER JOIN TEmployees AS C ON A.sID = C.sEmp AND A.SchoolID = C.SchoolID AND A.cType = 1
LEFT OUTER JOIN TBackupCardHistory AS D ON A.CardHistoryID = D.CardHistoryID AND A.cType = 2 AND D.ReturnDate IS NULL AND A.SchoolID = D.SchoolID
LEFT OUTER JOIN TBackupCard AS E ON E.CardID = D.CardID AND A.SchoolID = E.SchoolID
WHERE A.SchoolID = @SCHOOLID AND A.dMoney > @DAY AND A.dayCancal IS NULL 
AND (CONVERT(varchar(10),A.sID) = @USER_ID OR CONVERT(varchar(250),E.CardID) = @USER_ID)

ORDER BY A.dMoney DESC", userData.CompanyID, maxDay, user_id, userData.UserID)).ToList();

                    return q;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object TopupMoney(TopupData topup)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            //AWSXRay xray = new AWSXRay();
            //AWSXRayRecorder recorder = xray.Register();

            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var result = new Topup_Result();

            if (string.IsNullOrEmpty(topup.User_id)) return result;
            if (topup.Money < 0) return result;
            //recorder.AddAnnotation("Method", "Topup Money");
            //recorder.AddAnnotation("Entities", entities);
            //recorder.AddAnnotation("UserId", topup.User_id);
            //recorder.AddMetadata("Data", "Topup", fcommon.EntityToJson(topup));
            Request_API request = new Request_API(API_Type.Payment_API);
            var checkServerStatus = Topup.GetChecktempTopResult(userData.CompanyID, userData.AuthKey, userData.AuthValue);

            if (checkServerStatus.statusCode != 200)
            {
                //return new Topup_Result
                //{
                //    data = new Topup_Result.Topup_Detail
                //    {

                //    },
                //    status = "Function Cannot be access at this time. System is down temporarily"
                //};

                return checkServerStatus;
            }
            else
            {
                try
                {
                    TB_Send_API_Log send_API_Log = new TB_Send_API_Log();
                    send_API_Log.SchoolID = userData.CompanyID;
                    send_API_Log.Info = fcommon.EntityToJson(topup);
                    send_API_Log.Api_Name = "TOPUP LOG";
                    send_API_Log.Tstamp = DateTime.Now;

                    int emp_id = int.Parse(HttpContext.Current.Session["sEmpID"] + "");
                    if (topup.User_type == "2")
                    {
                        using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                        {
                            var backupCardHistory = jabJaiEntities.TBackupCardHistories.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.CardID.ToString() == topup.User_id && f.ReturnDate == null);

                            string JSon = "{" +
                                $"\"SchoolID\" : {userData.CompanyID}," +
                                $"\"sEmp\" : \"{userData.UserID}\", " +
                                $"\"sID\" : 0," +
                                $"\"nTopupMoney\" : {topup.Money}," +
                                $"\"CardID\" : \"{backupCardHistory.CardID}\"," +
                                $"\"CardHistoryID\" : \"{backupCardHistory.CardHistoryID}\" ," +
                                $"\"TopupType\" : \"WB01\" " + "}";

                            string resultDes = request.POST(JSon, $"/Api/shop/pos/topuptempcard", "Topup Temp Card", userData.CompanyID, userData.AuthKey, userData.AuthValue).resultDes;

                            result = JsonConvert.DeserializeObject<Topup_Result>(resultDes);

                            //var backupCardHistory = jabJaiEntities.TBackupCardHistories.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.CardID.ToString() == topup.User_id && f.ReturnDate == null);
                            //result = BackupCardTopup.UpdateData(
                            //    new TMoney
                            //    {
                            //        cType = topup.User_type,
                            //        //sID = topup.User_id,
                            //        nMoney = topup.Money ?? 0,
                            //        topup_type = "WBS1",
                            //        sEmp = emp_id,
                            //        SchoolID = userData.CompanyID
                            //    }, backupCardHistory, userData.Entities);

                            //var backupCard = jabJaiEntities.TBackupCards.FirstOrDefault(f => f.CardID == backupCardHistory.CardID);

                            //var thread = new Thread(() =>
                            //{
                            //    TopupTempCard topupTempCard = new TopupTempCard();
                            //    topupTempCard.init(new TopupTempCard
                            //    {
                            //        CardID = backupCard.CardID,
                            //        Money = backupCard.Money ?? 0,
                            //        SchoolID = userData.CompanyID,
                            //        BarCode = backupCard.BarCode,
                            //        CardName = backupCard.CardName,
                            //        NFC = backupCard.NFC,
                            //        NFCEncrypt = backupCard.NFCEncrypt,
                            //        UserID = backupCardHistory.UserID ?? 0,
                            //        UserType = backupCardHistory.UserType ?? 0
                            //    });
                            //});
                            ////var thread = new Thread(() => UpdataBalance(_balance));
                            //thread.IsBackground = true;
                            //thread.SetApartmentState(ApartmentState.STA);
                            //thread.Start();
                        }
                    }
                    else
                    {
                        int User_Id = int.Parse(topup.User_id);
                        result = Topup.UpdateData(
                            new TMoney
                            {
                                cType = topup.User_type,
                                sID = User_Id,
                                nMoney = topup.Money ?? 0,
                                topup_type = "WBS1",
                                sEmp = emp_id,
                                SchoolID = userData.CompanyID
                            }, userData.Entities, userData.AuthKey, userData.AuthValue);
                    }

                    if (result != null)
                    {
                        if (result.status.ToLower() == "Please wait while collecting data.".ToLower())
                        {
                            return result;
                        }
                        else if (result.status.ToLower() == "topup updated sucessfully".ToLower())
                        {
                            database.InsertLog(HttpContext.Current.Session["sEmpID"] + "", "ทำการเติมเงิน : " + topup.Name, userData.Entities, HttpContext.Current.Request, -1, 2, 0);

                            //recorder.AddMetadata("Data", "Result", fcommon.EntityToJson(result));
                            //recorder.EndSegment();
                            send_API_Log.Result = fcommon.EntityToJson(result);
                            InsertLogAPI._Send_API_Logs.Add(send_API_Log);

                            return result;

                        }
                        else
                        {
                            return result;
                        }
                    }
                    else
                    {
                        //recorder.EndSegment();
                        send_API_Log.Result = "FAILL";
                        InsertLogAPI._Send_API_Logs.Add(send_API_Log);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    //recorder.AddException(ex);
                    //recorder.EndSegment();
                    LINENotify notify = new LINENotify();
                    notify.LineSBErrorSend(new LINENotifyDATA
                    {
                        Parameter = topup,
                        Date_Time = DateTime.Now,
                        URL = HttpContext.Current.Request.RawUrl,
                        Error_Method = "TOPUP WEBSITE"
                    }, ex);

                    return result;
                }
            }

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string TopupMoney_Cancel(int MoneyID, string name)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            AWSXRay xray = new AWSXRay();

            AWSXRayRecorder recorder = xray.Register();

            string entities = HttpContext.Current.Session["sEntities"].ToString();
            int emp_id = int.Parse(HttpContext.Current.Session["sEmpID"] + "");

            recorder.AddAnnotation("Method", "Cancal Topup Money");
            recorder.AddAnnotation("Entities", entities);
            recorder.AddAnnotation("UserId", emp_id);
            recorder.AddMetadata("Data", "Topup", "{ \"UserId\" : " + emp_id + " ,  \"MoneyID\" : \"" + MoneyID + "\"}");

            try
            {
                TMoney result = new TMoney();

                result = Topup.Cancel(MoneyID, emp_id, userData.CompanyID, userData.AuthKey, userData.AuthValue);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                recorder.AddException(ex);
                recorder.EndSegment();
                return "fail";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Balance Getbalance_v1(string user_id, string user_type)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            Balance rss = new Balance();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(f_company,ConnectionDB.Read)))
                {
                    if (user_type == "1")
                    {
                        int User_Id = int.Parse(user_id);
                        var f_employees = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.sEmp == User_Id);
                        rss.Money = f_employees.nMoney ?? 0;
                    }
                    else if (user_type == "0")
                    {
                        int User_Id = int.Parse(user_id);
                        var f_student = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.sID == User_Id);
                        rss.Money = f_student.nMoney ?? 0;
                    }
                    else
                    {
                        var f_BackupCard = dbschool.TBackupCards.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.CardID.ToString() == user_id);
                        rss.Money = f_BackupCard.Money ?? 0;
                    }
                }
            }
            return rss;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Balance Getbalance(string user_id, string user_type)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            //Balance rss = new Balance();
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            string sID = user_type == "0" ? user_id : "0";
            string sEmp = user_type == "1" ? user_id : "0";
            string TempCardID = user_type == "2" ? user_id : "00000000-0000-0000-0000-000000000000";

            try
            {
                Request_API request_API = new Request_API(API_Type.Payment_API);
                var result = request_API.GET($"/api/shop/user/getuserbalance?SchoolID={userData.CompanyID}&sID={sID}&sEmp={sEmp}&TempCardID={TempCardID}", "GET User Balance", userData.CompanyID, userData.AuthKey, userData.AuthValue);

                if (result.resultDes.IndexOf("not have number id") == -1)
                {
                    var f = JsonConvert.DeserializeObject<List<Balance_Result>>(result.resultDes).FirstOrDefault();
                    return new Balance { Money = f.nMoney, Picture = f.sPicture };
                }
                else
                {
                    return new Balance { Money = 0, Picture = "" };
                }
            }
            catch (Exception ex)
            {
                return new Balance { Money = 0, Picture = "" };
            }
            //return rss;
        }

        private static void SetBeginSegment(AWSXRayRecorder recorder)
        {
            string url = HttpContext.Current.Request.Url.Host;

            var traceId = TraceId.NewId();
            recorder.BeginSegment(url, traceId);
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetUser(string wording)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var q = JabjaiMainClass.Autocompletes.TopupMoney.topupMoney
                .Where(w => !string.IsNullOrEmpty(w.KEYWORD) && w.SchoolID == userData.CompanyID).ToList();
            //.Where(w => w.KEYWORD.Contains(wording) || w.User_Name.Contains(wording))
            //.Select(s => new
            //{
            //    s.SchoolID,
            //    s.User_Id,
            //    s.User_Type,
            //    s.User_Name
            //}).Distinct().Take(10).ToList();

            return q;

            //using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            //{
            //    string entities = HttpContext.Current.Session["sEntities"].ToString();
            //    var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
            //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(f_company,ConnectionDB.Read)))
            //    {
            //                    string SQL = @"DECLARE @S VARCHAR(MAX) = '" + wording + @"'
            //DECLARE @SCHOOLID INT = " + f_company.nCompany + @"

            //SELECT TOP 10 * FROM (
            //SELECT sName + ' ' + sLastname AS User_Name,CONVERT(VARCHAR(200),sID) AS User_Id,'0' AS User_Type 
            //FROM TUser
            //WHERE (sName + ' ' + sLastname LIKE '%'+@S+'%' OR sStudentID LIKE '%'+@S+'%') AND SchoolID = @SCHOOLID AND ISNULL(cDel,'0') = '0'

            //UNION 

            //SELECT sName + ' ' + sLastname AS User_Name,CONVERT(VARCHAR(200),sEmp) AS User_Id,'1' AS User_Type 
            //FROM TEmployees
            //WHERE (sName + ' ' + sLastname LIKE '%'+@S+'%' OR sPhone LIKE '%'+@S+'%') AND SchoolID = @SCHOOLID AND ISNULL(cDel,'0') = '0'

            //UNION 

            //SELECT sName + ' ' + sLastname AS User_Name,CONVERT(VARCHAR(200),A.sID) AS User_Id,cType AS User_Type 
            //FROM [JabjaiMasterSingleDB].[dbo].[TUser] AS A LEFT OUTER JOIN [JabjaiMasterSingleDB].[dbo].[TUser_Card] AS B ON A.sID = B.sID AND A.nCompany = B.SchoolID
            //WHERE  (UPPER(B.NFC) LIKE '%'+UPPER(@S)+'%' OR UPPER(B.NFCEncrypt) LIKE '%'+UPPER(@S)+'%' OR username LIKE '%'+@S+'%') 
            //AND nCompany = @SCHOOLID AND ISNULL(A.cDel,'0') = '0'

            //UNION 

            //SELECT UserName + ' (บัตรสำรอง)' AS User_Name,CONVERT(VARCHAR(200),A.CardID) AS User_Id,'2' AS User_Type 
            //FROM TBackupCardHistory AS A INNER JOIN TBackupCard AS B ON A.CardID = B.CardID AND A.ReturnDate IS NULL AND A.SchoolID = B.SchoolID
            //WHERE (UserName LIKE '%'+@S+'%' OR B.BarCode LIKE '%'+@S+'%' OR B.NFC LIKE '%'+@S+'%') AND A.SchoolID = @SCHOOLID AND ISNULL(A.cDel,'0') = '0'
            //) AS TB     ";

            //                    return dbschool.Database.SqlQuery<UserList>(SQL).ToList();

            //    }
            //}
        }

        public class Balance
        {
            public decimal? Money { get; set; }
            public string Picture { get; internal set; }
        }

        public class UserList
        {
            public string User_Name { get; set; }
            public string User_Id { get; set; }
            public string User_Type { get; set; }
        }

        public class TopupData
        {
            public decimal? Money { get; set; }
            public string User_type { get; set; }
            public string User_id { get; set; }
            public string Name { get; set; }
        }

        public class HistoryTopup
        {
            public string name { get; set; }
            public decimal? money { get; set; }
            internal decimal? nBalance { get; set; }
            internal DateTime? dMoney { get; set; }
            public string time
            {
                get
                {
                    return string.Format("{0:dd/MM/yyyy HH:mm:ss}", dMoney);
                }
            }

            public string balannce
            {
                get
                {
                    return string.Format("{0:#,#0}", (nBalance ?? 0) + money);
                }
            }
            public string user_type { get; set; }
            public int user_id { get; set; }
            public int id { get; set; }
            public string date { get; set; }

        }

        public class Balance_Result
        {
            public int sID { get; set; }
            public string UserName { get; set; }
            public string cType { get; set; }
            public string sName { get; set; }
            public string sLastname { get; set; }
            public int SchoolID { get; set; }
            public string sPicture { get; set; }
            public decimal? nMoney { get; set; }
            public string NFCEncrypt { get; set; }
            public string CardID { get; set; }
            public string CardHistoryID { get; set; }
            public string TopupType { get; set; }
        }
    }
}