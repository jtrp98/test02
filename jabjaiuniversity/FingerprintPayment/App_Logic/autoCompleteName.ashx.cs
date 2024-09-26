using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using FingerprintPayment.Class;
using static FingerprintPayment.Global;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for autoCompleteName
    /// </summary>
    public class autoCompleteName : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string _mode = fcommon.ReplaceInjection(context.Request["Mode"]);
            string wording = fcommon.ReplaceInjection(context.Request["wording"]);
            string termID = fcommon.ReplaceInjection(context.Request["termID"]);
            dynamic rss;

            TB_Server B_Server = new TB_Server();
            string StatusDesc = "Online", Message = "";
            int StatusCode = 200;

            if (_mode.ToLower() == "serverupdate".ToLower())
            {
                using (JabJaiMasterEntities entitiesy = Connection.MasterEntities(ConnectionDB.Read))
                {
                    Guid guid = new Guid(ServeData.ServerID);
                    ServeData.B_Server = entitiesy.TB_Server.FirstOrDefault(f => f.ID == guid);

                    B_Server = ServeData.B_Server;
                    StatusDesc = B_Server.Status ?? true ? "Online" : "Offline";
                    Message = B_Server.Message;
                    StatusCode = B_Server.Status ?? true ? 200 : 400;
                }

                rss = new { Status = StatusDesc, StatusCode = StatusCode, Message = Message };

                context.Response.Expires = -1;
                context.Response.ContentType = "application/json";

                context.Response.Write(rss.ToString());
                context.Response.End();
                return;
            }
            else if (_mode.ToLower() == "Status_Autocompletes".ToLower())
            {
                try
                {
                    var c = JabjaiMainClass.Autocompletes.TopupMoney.topupMoney.Count();
                    if (c > 0)
                    {
                        rss = new { StatusCode = 200 };
                    }
                    else
                    {
                        rss = new { StatusCode = 500 };
                    }
                }
                catch
                {
                    rss = new { StatusCode = 500 };
                }

                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET");

                context.Response.Expires = -1;
                context.Response.ContentType = "application/json";

                context.Response.Write(rss.ToString());
                context.Response.End();

            }
            else if (_mode.ToLower() == "RestartMemory".ToLower())
            {
                JabjaiMainClass.Autocompletes.TopupMoney.CreateData();

                LINENotify notify = new LINENotify();
                notify.LineSBErrorSend(new LINENotifyDATA
                {
                    //Parameter = topup,
                    Date_Time = DateTime.Now,
                    //URL = HttpContext.Current.Request.RawUrl,
                    Error_Method = "RESTART WEB MEMORY"
                });

                rss = new { ResultDesc = "Success", StatusCode = 200 };

                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET");

                context.Response.Expires = -1;
                context.Response.ContentType = "application/json";

                context.Response.Write(rss.ToString());
                context.Response.End();
            }
            else if (_mode.ToLower() == "serverstatus".ToLower())
            {
                B_Server = ServeData.B_Server;
                StatusDesc = B_Server.Status ?? true ? "Online" : "Offline";
                Message = B_Server.Message;
                StatusCode = B_Server.Status ?? true ? 200 : 400;

                rss = new { Status = StatusDesc, StatusCode = StatusCode, Message = Message };

                context.Response.Expires = -1;
                context.Response.ContentType = "application/json";

                context.Response.Write(rss.ToString());
                context.Response.End();
                return;
            }

            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                rss = new { Status = StatusDesc, StatusCode = StatusCode, Message = Message };

                context.Response.Expires = -1;
                context.Response.ContentType = "application/json";

                context.Response.Write(rss.ToString());
                context.Response.End();
                return;
            }

            int schoolID = userData.CompanyID;

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> row;

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            string User_Type = string.Empty;

            switch (true)
            {
                #region GetStdList
                case bool b when "GetStdList".ToUpper() == _mode.ToUpper():
                    List<Std> stdList = new List<Std>();
                    stdList = GetStudentList(schoolID);
                    foreach (var l in stdList)
                    {
                        row = new Dictionary<string, object>();
                        row.Add("StdId", l.StdId);
                        row.Add("Name", l.Name);
                        row.Add("Code", l.Code);
                        rows.Add(row);
                    }
                    break;
                #endregion
                #region GetEmpList
                case bool b when "GetEmpList".ToUpper() == _mode.ToUpper():
                    List<Emp> empList = new List<Emp>();
                    empList = GetTeacherList(schoolID);
                    foreach (var l in empList)
                    {
                        row = new Dictionary<string, object>();
                        row.Add("EmpId", l.EmpId);
                        row.Add("Name", l.Name);
                        row.Add("Phone", l.Phone);
                        rows.Add(row);
                    }
                    break;
                case bool b when "ListStudent".ToUpper() == _mode.ToUpper():
                    try
                    {
                        if (JabjaiMainClass.Autocompletes.TopupMoney.workingStatus)
                        {
                            var q = (from a in JabjaiMainClass.Autocompletes.TopupMoney.topupMoney
                                     where !string.IsNullOrEmpty(a.KEYWORD) && a.SchoolID == userData.CompanyID && "0" == a.User_Type.Trim() //&& a.KEYWORD_ORDER == 0
                                     select a).ToList();

                            if (q != null)
                            {
                                var q1 = q.Where(w => w.KEYWORD.ToLower().Contains(wording.ToLower()) || w.User_Name.ToLower().Contains(wording.ToLower()))
                                    .OrderBy(o => o.KEYWORD.Length).ThenBy(t => t.User_Name)
                                    .Select(s => new
                                    {
                                        s.SchoolID,
                                        s.User_Id,
                                        s.User_Type,
                                        s.User_Name
                                    }).Distinct().Take(10).ToList();

                                foreach (var l in q1)
                                {
                                    row = new Dictionary<string, object>();
                                    row.Add("SchoolID", l.SchoolID);
                                    row.Add("User_Id", l.User_Id);
                                    row.Add("User_Type", l.User_Type);
                                    row.Add("User_Name", l.User_Name);
                                    rows.Add(row);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {


                        string logMessagePattern = @"[Function:ProcessRequest:ListStudent][SCHOOLID:{0}], [ErrorLine:{2}], [ErrorMessage:{3}]";
                        string errorMessage = e.Message;
                        string innerExceptionMessage = "";
                        while (e.InnerException != null) { innerExceptionMessage += ", " + e.InnerException.Message; e = e.InnerException; }
                        string logMessageDebug = string.Format(logMessagePattern, 0, "", Global.GetLineNumberError(e), errorMessage + ", " + innerExceptionMessage);
                        Global.InsertLogDebug(null, null, null, logMessageDebug);
                        //AWSXRay xray = new AWSXRay();
                        //AWSXRayRecorder recorder = xray.Register();
                        //recorder.AddAnnotation("Method", "Auto Complete Topup Money");
                        //recorder.AddMetadata("KEYWORD", wording);
                        //recorder.AddMetadata("URL", context.Request.RawUrl);
                        //recorder.AddException(e);
                        //recorder.EndSegment();
                    }
                    break;
                case bool b when "ListStudentTrem".ToUpper() == _mode.ToUpper():
                    try
                    {
                        using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
                        {

                            string sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname AS User_Name, sID AS User_Id
FROM TB_StudentViews
WHERE (cDel IS NULL OR cDel = 'G') AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%') AND nTerm='{2}'
--GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, wording, termID);
                            List<StudentList> result = dbschool.Database.SqlQuery<StudentList>(sqlQuery).ToList();

                            foreach (var l in result)
                            {
                                row = new Dictionary<string, object>();
                                row.Add("User_Id", l.User_Id);
                                row.Add("User_Name", l.User_Name);
                                rows.Add(row);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string logMessagePattern = @"[Function:ProcessRequest:ListStudentTrem][SCHOOLID:{0}], [ErrorLine:{2}], [ErrorMessage:{3}]";
                        string errorMessage = e.Message;
                        string innerExceptionMessage = "";
                        while (e.InnerException != null) { innerExceptionMessage += ", " + e.InnerException.Message; e = e.InnerException; }
                        string logMessageDebug = string.Format(logMessagePattern, 0, "", Global.GetLineNumberError(e), errorMessage + ", " + innerExceptionMessage);
                        Global.InsertLogDebug(null, null, null, logMessageDebug);

                        //AWSXRay xray = new AWSXRay();
                        //AWSXRayRecorder recorder = xray.Register();
                        //recorder.AddAnnotation("Method", "Auto Complete Topup Money");
                        //recorder.AddMetadata("KEYWORD", wording);
                        //recorder.AddMetadata("URL", context.Request.RawUrl);
                        //recorder.AddException(e);
                        //recorder.EndSegment();
                    }
                    break;
                case bool b when "TopUp".ToUpper() == _mode.ToUpper():
                    try
                    {



                        if (JabjaiMainClass.Autocompletes.TopupMoney.workingStatus)
                        {
                            bool bNeedRefresh = false;

                            try
                            {
                                if (JabjaiMainClass.Autocompletes.TopupMoney.topupMoney == null)
                                {
                                    bNeedRefresh = true;

                                }

                                if (JabjaiMainClass.Autocompletes.TopupMoney.topupMoney.Count == 0)
                                {
                                    bNeedRefresh = true;
                                }
                            }
                            catch
                            {
                                // bNeedRefresh = true;
                            }

                            if (bNeedRefresh)
                            {
                                JabjaiMainClass.Autocompletes.TopupMoney.CreateData();
                                try
                                {


                                    Global.InsertLogDebug(null, null, null, "Auto Refresh Data due null or empty.");
                                }
                                catch
                                {

                                }
                            }
                        }
                        if (JabjaiMainClass.Autocompletes.TopupMoney.workingStatus)
                        {
                            if (JabjaiMainClass.Autocompletes.TopupMoney.topupMoney != null)
                            {
                                var q = (from a in JabjaiMainClass.Autocompletes.TopupMoney.topupMoney
                                         where !string.IsNullOrEmpty(a.KEYWORD) && a.SchoolID == userData.CompanyID
                                         select a).OrderBy(x => x.User_Name).ToList();


                                if (q != null)
                                {

                                    var q1 = q.Where(w => w.KEYWORD.ToLower() == wording.ToLower())
                                        .OrderBy(o => o.KEYWORD.Length).ThenBy(t => t.User_Name)
                                       .Select(s => new
                                       {
                                           s.SchoolID,
                                           s.User_Id,
                                           s.User_Type,
                                           s.User_Name
                                       }).Distinct().Take(10).ToList();

                                    if (q1 != null)
                                    {
                                        if (q1.Count == 0)
                                        {
                                            q1 = q.Where(w => w.KEYWORD.ToLower().Contains(wording.ToLower()) || w.User_Name.ToLower().Contains(wording.ToLower()))
                                           .OrderBy(o => o.KEYWORD.Length).ThenBy(t => t.User_Name)
                                          .Select(s => new
                                          {
                                              s.SchoolID,
                                              s.User_Id,
                                              s.User_Type,
                                              s.User_Name
                                          }).Distinct().Take(10).ToList();
                                        }
                                    }

                                    bool bCheckUerData = false;

                                    if (q1 != null)
                                    {
                                        if (q1.Count == 0)
                                        {
                                            bCheckUerData = true;
                                        }
                                    }
                                    else
                                    {
                                        bCheckUerData = true;
                                    }

                                    if (bCheckUerData)
                                    {
                                        JabjaiMainClass.Autocompletes.TopupMoney.CheckAndAddUser(userData.CompanyID, wording);


                                        q = (from a in JabjaiMainClass.Autocompletes.TopupMoney.topupMoney
                                             where !string.IsNullOrEmpty(a.KEYWORD) && a.SchoolID == userData.CompanyID
                                             select a).OrderBy(x => x.User_Name).ToList();


                                        if (q != null)
                                        {
                                            q1 = q.Where(w => w.KEYWORD.ToLower() == wording.ToLower())
                                            .OrderBy(o => o.KEYWORD.Length).ThenBy(t => t.User_Name)
                                            .Select(s => new
                                            {
                                                s.SchoolID,
                                                s.User_Id,
                                                s.User_Type,
                                                s.User_Name
                                            }).Distinct().Take(10).ToList();

                                            if (q1 != null)
                                            {
                                                if (q1.Count == 0)
                                                {
                                                    q1 = q.Where(w => w.KEYWORD.ToLower().Contains(wording.ToLower()) || w.User_Name.ToLower().Contains(wording.ToLower()))
                                                 .OrderBy(o => o.KEYWORD.Length).ThenBy(t => t.User_Name)
                                                  .Select(s => new
                                                  {
                                                      s.SchoolID,
                                                      s.User_Id,
                                                      s.User_Type,
                                                      s.User_Name
                                                  }).Distinct().Take(10).ToList();
                                                }
                                            }
                                        }
                                    }

                                    foreach (var l in q1)
                                    {
                                        row = new Dictionary<string, object>();
                                        row.Add("SchoolID", l.SchoolID);
                                        row.Add("User_Id", l.User_Id);
                                        row.Add("User_Type", l.User_Type);
                                        row.Add("User_Name", l.User_Name);
                                        rows.Add(row);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {

                        string logMessagePattern = @"[Function:ProcessRequest:Topup][SCHOOLID:{0}], [ErrorLine:{2}], [ErrorMessage:{3}]";
                        string errorMessage = e.Message;
                        string innerExceptionMessage = "";
                        while (e.InnerException != null) { innerExceptionMessage += ", " + e.InnerException.Message; e = e.InnerException; }
                        string logMessageDebug = string.Format(logMessagePattern, 0, "", Global.GetLineNumberError(e), errorMessage + ", " + innerExceptionMessage);
                        Global.InsertLogDebug(null, null, null, logMessageDebug);
                    }
                    break;
                #endregion
                case bool b when "RestartMemory".ToUpper() == _mode.ToUpper():
                    JabjaiMainClass.Autocompletes.TopupMoney.CreateData();

                    LINENotify notify = new LINENotify();
                    notify.LineSBErrorSend(new LINENotifyDATA
                    {
                        //Parameter = topup,
                        Date_Time = DateTime.Now,
                        //URL = HttpContext.Current.Request.RawUrl,
                        Error_Method = "RESTART WEB MEMORY"
                    });

                    context.Response.Write("Success !!");
                    context.Response.End();
                    return;
            }

            context.Response.Expires = -1;
            context.Response.ContentType = "application/json";

            context.Response.Write(serializer.Serialize(rows));

            context.Response.End();

        }


        public static void CheckAndAddUserOld(int SchoolID, string keyword)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(SchoolID, ConnectionDB.Read)))
            {
                var tBackupCard = (from b in dbschool.TBackupCards
                                   join bh in dbschool.TBackupCardHistories on b.CardID equals bh.CardID
                                   where b.SchoolID == SchoolID && bh.ReturnDate == null && b.cDel == false
                                   && (keyword.ToLower().Contains(b.BarCode.ToLower()) || keyword.ToLower().Contains(b.NFC.ToLower()) || keyword.ToLower().Contains(b.NFCEncrypt.ToLower())
                                   || keyword.ToLower().Contains(b.NFCReverse.ToLower()) || keyword.ToLower().Contains(b.NFCEncryptReverse.ToLower()))
                                   select new
                                   {
                                       User_Id = b.CardID.ToString(),
                                       User_Name = bh.UserName == null ? "" : bh.UserName,
                                       User_Type = bh.UserType.ToString(),
                                       SchoolID = b.SchoolID,
                                       KEYWORD = keyword,
                                       KEYWORD_ORDER = 3,
                                       Active = true,

                                   }).AsEnumerable().Select(x => new JabjaiMainClass.Models.AC_TopupMoney
                                   {
                                       User_Id = x.User_Id,
                                       User_Name = x.User_Name,
                                       User_Type = x.User_Type,
                                       SchoolID = x.SchoolID,
                                       KEYWORD = x.KEYWORD,
                                       KEYWORD_ORDER = x.KEYWORD_ORDER,
                                       Active = x.Active,
                                   }).ToList();

            }

            using (JabJaiMasterEntities entitiesy = Connection.MasterEntities(ConnectionDB.Read))
            {

            }
        }
        public class StudentList
        {
            public string User_Name { get; set; }
            public int User_Id { get; set; }
        }

        private class Std
        {
            public string Name { get; set; }
            public int StdId { get; set; }
            public string Code { get; set; }

        }

        private List<Std> GetStudentList(int schoolID)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                var queryStd = (from std in _db.TUser
                                where std.SchoolID == schoolID && std.cDel != "1"
                                select new Std
                                {
                                    Name = std.sName + " " + std.sLastname,
                                    StdId = std.sID,
                                    Code = std.sStudentID

                                }).ToList();

                return queryStd;
            }
        }

        private class Emp
        {
            public string Name { get; set; }
            public int EmpId { get; set; }
            public string Phone { get; set; }
        }

        private List<Emp> GetTeacherList(int schoolID)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                var queryEmp = (from emp in _db.TEmployees
                                where emp.SchoolID == schoolID && emp.cDel != "1"
                                select new Emp
                                {
                                    Name = emp.sName + " " + emp.sLastname,
                                    EmpId = emp.sEmp,
                                    Phone = emp.sPhone
                                }).ToList();

                return queryEmp;
            }
        }

        private class EmpData
        {
            public int EmpId { get; set; }
            public string Name { get; set; }
            public string cType { get; set; }
            public string Job { get; set; }
            public string TimeType { get; set; }
            public string Depa { get; set; }
            public string Birthday { get; set; }
            public string Pic { get; set; }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}