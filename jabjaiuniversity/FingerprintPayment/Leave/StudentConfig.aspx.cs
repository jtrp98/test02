using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Leave
{
    public partial class StudentConfig : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static object SaveData1(byte? num)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TLeave_ConfigStudent.Where(o => o.SchoolID == userData.CompanyID).FirstOrDefault();

                var log = new TLeave_Log();
                log.Date = DateTime.Now;
                log.Type = "1";
                log.SchoolID = userData.CompanyID;
                log.ByUser = userData.Name;
                log.Text = $"แก้จำนวนผู้ตรวจสอบเป็น {num}";
                db.TLeave_Log.Add(log);

                if (data != null)
                {
                    data.ApproveNum = num;
                    data.ModifyBy = userData.UserID;
                    data.Modified = DateTime.Now;
                }
                else
                {
                    data = new TLeave_ConfigStudent();
                    data.ApproveNum = num;
                    data.IsDel = false;
                    data.SchoolID = userData.CompanyID;
                    data.CreateBy = userData.UserID;
                    data.Created = DateTime.Now;
                    data.ModifyBy = userData.UserID;
                    data.Modified = DateTime.Now;

                    db.TLeave_ConfigStudent.Add(data);
                }

                db.SaveChanges();

                return new
                {
                    text = "success"
                };
            }

        }

        [WebMethod(EnableSession = true)]
        public static object SaveData3(byte? num, string type)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TLeave_ConfigStudent.Where(o => o.SchoolID == userData.CompanyID).FirstOrDefault();

                var log = new TLeave_Log();
                log.Date = DateTime.Now;
                log.Type = "1";
                log.SchoolID = userData.CompanyID;
                log.ByUser = userData.Name;
                log.Text = $"แก้จำนวนการแจ้งลาเป็น {num} ครั้งภายใน 1 {(type == "Y" ? "ปีการศึกษา" : "เทอม")}";
                db.TLeave_Log.Add(log);

                if (data != null)
                {
                    data.LimitDay = num;
                    data.LimitType = type;
                    data.ModifyBy = userData.UserID;
                    data.Modified = DateTime.Now;
                }
                else
                {
                    data = new TLeave_ConfigStudent();
                    data.LimitDay = num;
                    data.LimitType = type;
                    data.IsDel = false;
                    data.SchoolID = userData.CompanyID;
                    data.CreateBy = userData.UserID;
                    data.Created = DateTime.Now;
                    data.ModifyBy = userData.UserID;
                    data.Modified = DateTime.Now;

                    db.TLeave_ConfigStudent.Add(data);
                }

                db.SaveChanges();

                return new
                {
                    data = data,
                    text = "success"
                };
            }

        }

        [WebMethod(EnableSession = true)]
        [HttpGet]
        public static object GetLog(string type)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logs = db.TLeave_Log.Where(o => o.SchoolID == userData.CompanyID && o.Type == type)
                 .OrderByDescending(o => o.Date)
                 .Take(20)
                 .ToList();

                var sbLogs = new StringBuilder();
                if (logs != null)
                {
                    foreach (var item in logs)
                    {
                        sbLogs.Append($"<strong>{item.Date?.ToString("dd/MM/yyyy HH:mm ", new CultureInfo("th-TH"))}</strong> {item.Text}, ผู้ทำรายการ : {item.ByUser}<br/>");
                    }
                }

                return new
                {                   
                    logs = sbLogs.ToString(),
                    text = "success"
                };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetData()
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TLeave_ConfigStudent.Where(o => o.SchoolID == userData.CompanyID && o.IsDel == false)
                     .Select(o => new
                     {
                         id = o.SetID,
                         approveNum = o.ApproveNum,
                         day = o.LimitDay,
                         type = o.LimitType,
                     })
                    .FirstOrDefault();

                //var logs = db.TLeave_Log.Where(o => o.SchoolID == userData.CompanyID && o.Type == "1")
                //   .OrderByDescending(o => o.Date)
                //   .Take(20)
                //   .ToList();

                //var sbLogs = new StringBuilder();
                //if (logs != null)
                //{
                //    foreach (var item in logs)
                //    {
                //        sbLogs.Append($"<strong>{item.Date?.ToString("dd/MM/yyyy HH:mm ", new CultureInfo("th-TH"))}</strong> {item.Text}, ผู้ทำรายการ : {item.ByUser}<br/>");
                //    }
                //}

                return new
                {
                    data = data,
                    //logs = sbLogs.ToString(),
                    text = "success"
                };
            }

        }

        public static JWTToken.userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }

            return userData;
        }

        public class Search
        {
            public string text { get; set; }
        }

    }
}