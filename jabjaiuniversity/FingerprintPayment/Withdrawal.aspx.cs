using Amazon.XRay.Recorder.Core;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using FingerprintPayment.Class;
using Newtonsoft.Json;

namespace FingerprintPayment
{
    public partial class Withdrawal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                //using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                //{
                //    //string sEntities = Session["sEntities"].ToString();
                //    //var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                //    //using (JabJaiEntities dbschool = new JabJaiEntities(fcommon.StringConnectionSchool(sEntities)))
                //    //{
                //    //    var q = QueryDataBases.SubLevel_Query.GetData(dbschool, userData);
                //    //    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                //    //}
                //}
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
                    var q = (from s in (from a in dbschool.TWithdrawals.Where(w => w.SchoolID == userData.CompanyID)
                                        join b in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID) on new { user_id = a.UserID.Value, user_type = a.userType }
                                        equals new { user_id = b.sID, user_type = "0" } into jab
                                        from jb in jab.DefaultIfEmpty()

                                        join c in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on new { user_id = a.UserID.Value, user_type = a.userType }
                                        equals new { user_id = c.sEmp, user_type = "1" } into jac
                                        from jc in jac.DefaultIfEmpty()

                                        orderby a.dWithdrawal descending
                                        where a.dCanCel == null && a.dWithdrawal >= DateTime.Today && a.userAdd == user_id

                                        select new
                                        {
                                            name = (jc.sName ?? jb.sName) + " " + (jc.sLastname ?? jb.sLastname),
                                            money = a.nMoney ?? 0,
                                            time = a.dWithdrawal.Value,
                                            user_id = a.UserID.Value,
                                            user_type = a.userType,
                                            a.Withdrawal_Id,
                                            a.nBalance,
                                        }).Take(20).ToList()

                             select new HistoryTopup
                             {
                                 name = s.name,
                                 money = s.money,
                                 time = s.time.ToString("dd/MM/yyyy HH:mm:ss"),
                                 user_id = s.user_id,
                                 user_type = s.user_type,
                                 id = s.Withdrawal_Id,
                                 balannce = string.Format("{0:#,#0}", (s.nBalance ?? 0) - (s.money))
                             }).ToList();

                    return q;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<HistoryTopup> SearchHistoryTopups(int? user_id, string user_type, DateTime? topup_date)
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
                    var q = (from s in (from a in dbschool.TWithdrawals.Where(w => w.SchoolID == userData.CompanyID)
                                        join b in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID) on new { user_id = a.UserID.Value, user_type = a.userType }
                                        equals new { user_id = b.sID, user_type = "0" } into jab
                                        from jb in jab.DefaultIfEmpty()

                                        join c in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on new { user_id = a.UserID.Value, user_type = a.userType }
                                        equals new { user_id = c.sEmp, user_type = "1" } into jac
                                        from jc in jac.DefaultIfEmpty()

                                        orderby a.dWithdrawal descending
                                        where a.dCanCel == null && a.dWithdrawal >= maxDay
                                        && (!user_id.HasValue || ((user_type == "0" && user_id == jb.sID) || (user_type == "1" && user_id == jc.sEmp)))
                                        && a.userType == user_type
                                        && (!topup_date.HasValue || a.dWithdrawal < topup_date)

                                        select new
                                        {
                                            name = (jc.sName ?? jb.sName) + " " + (jc.sLastname ?? jb.sLastname),
                                            money = a.nMoney ?? 0,
                                            time = a.dWithdrawal.Value,
                                            user_id = a.UserID.Value,
                                            user_type = a.userType,
                                            a.Withdrawal_Id,
                                        }).Take(20).ToList()

                             select new HistoryTopup
                             {
                                 name = s.name,
                                 money = s.money,
                                 time = s.time.ToString("dd/MM/yyyy HH:mm:ss"),
                                 user_id = s.user_id,
                                 user_type = s.user_type,
                                 id = s.Withdrawal_Id,
                                 date = s.time.ToString("MM/dd/yyyy HH:mm:ss"),
                             }).ToList();

                    return q;
                }
            }
        }

        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static string TopupMoney(TopupData topup)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

        //    string entities = HttpContext.Current.Session["sEntities"].ToString();
        //    var result = new TWithdrawal();
        //    int emp_id = int.Parse(HttpContext.Current.Session["sEmpID"] + "");

        //    AWSXRay xray = new AWSXRay();

        //    AWSXRayRecorder recorder = xray.Register();

        //    recorder.AddAnnotation("Method", "Withdrawal Money");
        //    recorder.AddAnnotation("Entities", entities);
        //    recorder.AddAnnotation("UserID", topup.User_id);
        //    recorder.AddMetadata("Data", "Withdrawal", fcommon.EntityToJson(topup));

        //    try
        //    {
        //        WithdrawalLogic logic = new WithdrawalLogic(new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)));

        //        result = logic.updateData(new TWithdrawal
        //        {
        //            nMoney = topup.Money,
        //            userAdd = emp_id,
        //            dWithdrawal = DateTime.Now,
        //            userType = topup.User_type,
        //            UserID = topup.User_id,
        //            sWithdrawalType = "WBS01",
        //            SchoolID = userData.CompanyID
        //        });

        //        if (result != null)
        //        {
        //            database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
        //                "ทำการถอนเงิน : " + topup.Name, entities, HttpContext.Current.Request, -1, 2, 0);

        //            recorder.AddMetadata("Data", "Result", fcommon.EntityToJson(result));
        //            //recorder.EndSegment();
        //            return "success";
        //        }
        //        else
        //        {
        //            return "fail";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        recorder.AddException(ex);
        //        recorder.EndSegment();

        //        return ex.Message.ToString();
        //    }
        //}

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static API_Renponse WithdrawalMoney(TopupData topup)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var result = new TWithdrawal();
            int emp_id = int.Parse(HttpContext.Current.Session["sEmpID"] + "");

            AWSXRay xray = new AWSXRay();

            AWSXRayRecorder recorder = xray.Register();

            recorder.AddAnnotation("Method", "Withdrawal Money");
            recorder.AddAnnotation("Entities", entities);
            recorder.AddAnnotation("UserID", topup.User_id);
            recorder.AddMetadata("Data", "Withdrawal", fcommon.EntityToJson(topup));

            try
            {
                Request_API request = new Request_API(API_Type.Payment_API);

                string Json = "{" +
                    $"\"SchoolID\" : \"{userData.CompanyID}\" ," +
                    $"\"UserID\" : \"{topup.User_id}\" ," +
                    $"\"UserType\" : \"{topup.User_type}\" ," +
                    $"\"WithdrawalAmount\" : \"{topup.Money}\" ," +
                    $"\"CardHistoryID\" : \"{topup.User_id}\" ," +
                    $"\"UserAdd\" : \"{userData.UserID}\" " +
                    "}";

                string response = request.POST(Json, $"/api/shop/pos/withdrawal", ("payment withdrawal").ToUpper(), userData.CompanyID, userData.AuthKey, userData.AuthValue).resultDes;

                var rss = JsonConvert.DeserializeObject<API_Renponse>(response);

                return rss;

            }
            catch (Exception ex)
            {
                recorder.AddException(ex);
                recorder.EndSegment();

                return new API_Renponse
                {
                    success = false,
                    statusCode = 500,
                    message = ex.Message
                };
            }
        }

        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static string TopupMoney_Cancel(string topup_id, string name)
        //{
        //    string entities = HttpContext.Current.Session["sEntities"].ToString();
        //    int emp_id = int.Parse(HttpContext.Current.Session["sEmpID"] + "");

        //    AWSXRayRecorder recorder = AWSXRayRecorder.Instance;
        //    SetBeginSegment(recorder);

        //    recorder.AddAnnotation("Method", "Cancal Topup Money");
        //    recorder.AddAnnotation("Entities", entities);
        //    recorder.AddAnnotation("UserID", emp_id);
        //    recorder.AddMetadata("Data", "Topup", "{ \"UserID\" : " + emp_id + " ,  \"Topup Id\" : \"" + topup_id + "\"}");

        //    try
        //    {
        //        var result = Topup.Cancel(topup_id, emp_id, entities);
        //        if (result != null)
        //        {
        //            if (result.nMoney > 0)
        //            {
        //                database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
        //                    "ทำการยกเลิกการเติมเงิน : " + name + " รหัสการเติมเงิน : " + result.TopupId, entities, HttpContext.Current.Request, -1, 2, 0);
        //                recorder.AddMetadata("Data", "Result", fcommon.EntityToJson(result));
        //                recorder.EndSegment();
        //                return "success";
        //            }
        //            else
        //            {
        //                return "low money";
        //            }
        //        }
        //        else
        //        {
        //            recorder.EndSegment();
        //            return "fail";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        recorder.AddException(ex);
        //        recorder.EndSegment();
        //        return "fail";
        //    }
        //}

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Balance Getbalance(int user_id, string user_type)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            Balance rss = new Balance();
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            int sID = user_type == "0" ? user_id : 0;
            int sEmp = user_type == "1" ? user_id : 0;
            string TempCardID = "00000000-0000-0000-0000-000000000000";//user_type == "2" ? user_id : "00000000-0000-0000-0000-000000000000";

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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Balance Getbalance_v1(int user_id, string user_type)
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
                        var f_employees = dbschool.TEmployees.FirstOrDefault(f => f.sEmp == user_id);
                        rss.Money = f_employees.nMoney ?? 0;
                    }
                    else
                    {
                        var f_student = dbschool.TUser.FirstOrDefault(f => f.sID == user_id);
                        rss.Money = f_student.nMoney ?? 0;
                    }
                }
            }
            return rss;
        }


        private static void SetBeginSegment(AWSXRayRecorder recorder)
        {
            string url = HttpContext.Current.Request.Url.Host;

            var traceId = TraceId.NewId();
            recorder.BeginSegment(url, traceId);
        }




        public class Balance
        {
            public decimal? Money { get; set; }
            public string Picture { get; internal set; }
        }

        public class TopupData
        {
            public decimal? Money { get; set; }
            public string User_type { get; set; }
            public int User_id { get; set; }
            public string Name { get; set; }
        }

        public class API_Renponse
        {
            public bool success { get; set; }
            public int statusCode { get; set; }
            public string message { get; set; }
        }

        public class HistoryTopup
        {
            public string name { get; set; }
            public decimal? money { get; set; }
            public string time { get; set; }
            public string user_type { get; set; }
            public int user_id { get; set; }
            public string id { get; set; }
            public string date { get; set; }
            public string balannce { get; set; }
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