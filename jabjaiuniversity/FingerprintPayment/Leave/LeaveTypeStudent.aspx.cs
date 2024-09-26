using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Leave
{
    public partial class LeaveTypeStudent : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod(EnableSession = true)]
        public static object SaveData(FormSave data)
        {
            var userData = GetUserData();

            try
            {
                using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                {
                    var leave = new TLeave_Type();
                    var logText = "";
                    if (data.ID.HasValue)
                    {
                        logText = $"แก้ไขประเภทการลา : {data.NameTH} ,ประเภทการลา(ภาษาอังกฤษ) : {data.NameEN} ,ตัวย่อ : {data.AbbrTH},ตัวย่อ EN : {data.AbbrEN}";
                        leave = db.TLeave_Type.Where(o => o.SchoolID == userData.CompanyID && o.TypeID == data.ID).FirstOrDefault();
                        leave.TypeName = data.NameTH;
                        leave.TypeNameEN = data.NameEN;
                        leave.AbbrTH = data.AbbrTH;
                        leave.Abbr = data.AbbrEN;
                        leave.ModifyBy = userData.UserID;
                        leave.Modified = DateTime.Now;
                    }
                    else
                    {
                        logText = $"เพิ่มประเภทการลา : {data.NameTH} ,ประเภทการลา(ภาษาอังกฤษ) : {data.NameEN} ,ตัวย่อ : {data.AbbrTH},ตัวย่อ EN : {data.AbbrEN}";
                        leave.SchoolID = userData.CompanyID;
                        leave.TypeName = data.NameTH;
                        leave.TypeNameEN = data.NameEN;
                        leave.AbbrTH = data.AbbrTH;
                        leave.Abbr = data.AbbrEN;
                        leave.CreateBy = userData.UserID;
                        leave.Created = DateTime.Now;
                        leave.ModifyBy = userData.UserID;
                        leave.Modified = DateTime.Now;
                        leave.IsDefault = false;
                        leave.Type = "S";
                        leave.IsActive = true;
                        leave.IsDel = false;
                        db.TLeave_Type.Add(leave);
                    }

                    var log = new TLeave_Log();
                    log.Date = DateTime.Now;
                    log.Type = "4";
                    log.SchoolID = userData.CompanyID;
                    log.ByUser = userData.Name;
                    log.Text = logText;
                    db.TLeave_Log.Add(log);
                    db.SaveChanges();

                    return new
                    {
                        status = true,
                        text = "success"
                    };
                }
            }
            catch
            {
                return new
                {
                    status = false,
                    text = "error"
                };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetByID(int id)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TLeave_Type.Where(o => o.SchoolID == userData.CompanyID && o.TypeID == id)
                    .Select(o => new
                    {
                        id = o.TypeID,
                        name = o.TypeName + "",
                        abbrTH = o.AbbrTH + "",
                        nameEn = o.TypeNameEN + "",
                        abbrEN = o.Abbr + "",
                        active = o.IsActive,
                    }).FirstOrDefault();

                return new
                {
                    data = data,
                    text = "success",
                };
            }

        }


        [WebMethod(EnableSession = true)]
        public static object RemoveByID(int id)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TLeave_Type.Where(o => o.SchoolID == userData.CompanyID && o.TypeID == id).FirstOrDefault();
                data.IsDel = true;

                var log = new TLeave_Log();
                log.Date = DateTime.Now;
                log.Type = "4";
                log.SchoolID = userData.CompanyID;
                log.ByUser = userData.Name;
                log.Text = $"ลบ ประเภทการลา : {data.TypeName} ,ประเภทการลา(ภาษาอังกฤษ) : {data.TypeNameEN} ,ตัวย่อ : {data.AbbrTH} ,ตัวย่อ EN : {data.Abbr}";
                db.TLeave_Log.Add(log);

                db.SaveChanges();

                return new
                {
                    data = data,
                    text = "success"
                };
            }

        }

        [WebMethod(EnableSession = true)]
        public static object SwitchByID(int id, bool status)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TLeave_Type.Where(o => o.SchoolID == userData.CompanyID && o.TypeID == id).FirstOrDefault();
                data.IsActive = status;

                var log = new TLeave_Log();
                log.Date = DateTime.Now;
                log.Type = "4";
                log.SchoolID = userData.CompanyID;
                log.ByUser = userData.Name;
                log.Text = $"{(status ? "เปิด" : "ปิด")}การใช้งาน ประเภทการลา : {data.TypeName} ,ประเภทการลา(ภาษาอังกฤษ) : {data.TypeNameEN} ,ตัวย่อ : {data.Abbr}";
                db.TLeave_Log.Add(log);
                db.SaveChanges();

                return new
                {
                    data = data,
                    text = "success"
                };
            }

        }

        [WebMethod(EnableSession = true)]
        public static object GetData(Search search)
        {
            var userData = GetUserData();

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var qry = db.TLeave_Type.Where(o => o.SchoolID == userData.CompanyID && o.IsDel == false && o.Type == "S")
                    .Select(o => new
                    {
                        o.TypeID,
                        o.TypeName,
                        o.TypeNameEN,
                        o.Abbr,
                        o.AbbrTH,
                        o.IsActive,
                        o.IsDefault,
                    });

                if (!string.IsNullOrEmpty(search.text))
                {
                    qry = qry.Where(o => o.TypeName.Contains(search.text) || o.TypeNameEN.Contains(search.text));
                }

                var data = qry.AsEnumerable()
                    .Select((o, i) => new
                    {
                        index = i + 1,
                        id = o.TypeID,
                        name = o.TypeName + "",
                        nameEn = o.TypeNameEN + "",                
                        abbrTH = o.AbbrTH + "",
                        abbrEN = o.Abbr + "",
                        active = o.IsActive,
                        isDefault = o.IsDefault,
                    })
                    .ToList();

                //var logs = db.TLeave_Log.Where(o => o.SchoolID == userData.CompanyID && o.Type == "4")
                //     .OrderByDescending(o => o.Date)
                //     .Take(20)
                //     .ToList();


                //var sbLogs = new StringBuilder();
                //if (logs != null)
                //{
                //    foreach (var item in logs)
                //    {
                //        sbLogs.Append($"<strong>{item.Date?.ToString("dd/MM/yyyy HH:mm ", new CultureInfo("th-TH"))}</strong> {item.Text}, ผู้ทำรายการ : {item.ByUser}<br/>");
                //    }
                //}


                return new { data = data, /*logs = sbLogs.ToString()*/ };
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
        public class FormSave
        {
            public int? ID { get; set; }
            public string NameTH { get; set; }
            public string NameEN { get; set; }
            public string AbbrEN { get; set; }
            public string AbbrTH { get; set; }
        }
    }
}