using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Permission
{
    public partial class Main : BasePermissionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userData = GetUserData();

            if (!this.IsPostBack)
            {
                using (var db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var logs = db.TGroupPermission_Log.Where(o => o.SchoolID == userData.CompanyID )
                      .OrderBy(o => o.Date)
                      .Take(20)
                      .ToList();

                    var sbLogs = new StringBuilder();
                    if (logs?.Count > 0)
                    {
                        foreach (var item in logs)
                        {
                            sbLogs.Append($"{item.Date?.ToString("dd/MM/yyyyy HH:mm ")} {item.Text}, ผู้ทำรายการ : {item.ByUser}<br/>");
                        }

                        ltrLogHistory.Text = sbLogs.ToString();
                    }
                }
            }
        }


        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadData()
        {
            var userData = GetUserData();

            using (var db = Connection.MasterEntities(ConnectionDB.Read))
            {
                var d = (from a in db.TGroupPermission.Where(o => o.SchoolID == userData.CompanyID && o.IsDel == false && o.IsActive == true)
                         from b in db.TUsers.Where(o => o.nCompany == a.SchoolID && o.sID == a.ModifyBy).DefaultIfEmpty()
                         select new
                         {
                             a.GroupID,
                             a.GroupName,
                             a.Created,
                             a.IsEditable,
                             a.Modified,
                             editor = b.sName + " " + b.sLastname,
                         })
                        .AsEnumerable()
                        .OrderBy(o => o.IsEditable)
                        .ThenBy(o => o.Created)
                        .Select((a, i) => new
                        {
                            index = i + 1,
                            title = a.GroupName,
                            id = a.GroupID,
                            isEdit = a.IsEditable ?? true,
                            a.editor,
                            modify = a.Modified?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"))
                        })  
                        .ToList();

                return new { data = d };
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object RemoveGroup(int gid)
        {
            var userData = GetUserData();

            using (var db = Connection.MasterEntities(ConnectionDB.Read))
            {
                var d1 = db.TGroupPermission
                    .FirstOrDefault(o => o.SchoolID == userData.CompanyID && o.GroupID == gid);

                d1.IsActive = false;
                d1.IsDel = true;

                var d2 = db.TGroupPermissionUser
                    .Where(o => o.SchoolID == userData.CompanyID && o.GroupID == gid)
                    .ToList();

                foreach (var i in d2)
                {
                    i.IsActive = false;
                    i.IsDel = true;
                }

                db.SaveChanges();

                return new { status = "success" };
            }
        }
    }
}