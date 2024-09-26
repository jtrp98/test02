using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiMainClass.Models;
using MasterEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{

    [ScriptService]
    [WebService(Namespace = "http://xmlforasp.net")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class mp_notFrom : BaseMasterPage
    {
        public int sEmpID = 0;
        public string sEntities = "";
        public static RoleStatus Permission_Page = new RoleStatus();
        public static bool menudemo = ConfigurationManager.AppSettings["Menu_demo"].ToString().Trim() == "1";
        public permissionMenuModel PermissionMenu = new permissionMenuModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) Response.Redirect("~/Default.aspx");

            sEmpID = int.Parse(Session["sEmpID"] + "");
            sEntities = Session["sEntities"].ToString();
            Permission_Page = authentication.WebSite.RoleStatus(Request.Url, sEmpID, sEntities);

            //if (Permission_Page.permission == "2") Response.Redirect("~/Default.aspx");

            ViewState["Permission_Page"] = Permission_Page.permission;
            PermissionMenu = JsonConvert.DeserializeObject<permissionMenuModel>(Session["permissionMenu"] + "");
        }

        public string listmenu(int user_id, string sEntities)
        {
            dynamic rss = new JObject();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    var qemployees_listmenu = dbschool.TEmployees.FirstOrDefault(f => f.sEmp == user_id);
                    List<permission> q_permission = new List<permission>();
                    var qmenu = dbmaster.TMenus.Where(w => w.showmenu == true).ToList();
                    if (qemployees_listmenu == null)
                    {
                        foreach (var d in qmenu)
                        {
                            q_permission.Add(new permission { menu_id = d.MenuId, type = "w", actvice = 1 });
                        }
                    }
                    else
                    {
                        var q_user = dbmaster.TUsers.FirstOrDefault(f => f.nCompany == qcompany.nCompany && f.cType == "1" && f.nSystemID == user_id);
                        q_permission = dbmaster.permissions.Where(w => w.user_id == q_user.sID).ToList();
                    }

                    var qgroupmenu = dbmaster.TGroupMenus.ToList();
                    qgroupmenu = qgroupmenu.Where(w => w.active == true).ToList();
                    rss.employessname = qemployees_listmenu.sName + " " + qemployees_listmenu.sLastname;
                    rss.groupmenu = new JArray(from a in qgroupmenu
                                               select new JObject(
                                                   new JProperty("groupmenuid", a.groupmenuid),
                                                   new JProperty("groupmenu_name", a.groupmenu),
                                                   new JProperty("groupmenu_class", a.@class),
                                                   new JProperty("groupmenu_title", a.title),
                                                   new JProperty("groupmenu_permissions", getpermissions(a.groupmenuid, qmenu, q_permission)),
                                                   new JProperty("submenu", qmenu.Where(w => w.groupmenuid == a.groupmenuid).Count() == 1 ? false : true),
                                                   new JProperty("groupmenu_url", groupmenu_url(qmenu, a.groupmenuid)),
                                                   new JProperty("menu", menu(qmenu, a.groupmenuid, q_permission, qcompany))
                                                   ));
                }
            }
            return rss.ToString();
        }

        private string groupmenu_url(List<TMenu> qmenu, int groupmenuid)
        {
            if (qmenu.Where(w => w.groupmenuid == groupmenuid).Count() == 1)
            {
                if (qmenu.Where(w => w.groupmenuid == groupmenuid).FirstOrDefault().demo == false)
                {
                    return qmenu.Where(w => w.groupmenuid == groupmenuid).FirstOrDefault().url;
                }
                else
                {
                    if (menudemo) return qmenu.Where(w => w.groupmenuid == groupmenuid).FirstOrDefault().url;
                    else return "#";
                }
            }
            else
            {
                return "#";
            }
        }

        private bool getpermissions(int groupmenu_id, List<TMenu> qmenu, string user_permissions)
        {
            bool permissions = false;
            foreach (var menu_data in qmenu.Where(w => w.groupmenuid == groupmenu_id))
            {
                if (permissions == false)
                {
                    if (user_permissions.Length > menu_data.MenuIndex.Value)
                        permissions = user_permissions.Substring(menu_data.MenuIndex.Value, 1) != "2";
                    else
                        permissions = menudemo == true;
                }
            }
            return permissions;
        }

        private JArray menu(List<TMenu> qmenu, int groupmenuid, string sClaim, TCompany qcompany)
        {
            dynamic rss = new JArray();
            if (qmenu.Where(w => w.groupmenuid == groupmenuid).Count() > 1)
            {
                if (menudemo == false)
                {
                    rss = new JArray(from b in qmenu
                                     where b.groupmenuid == groupmenuid
                                     && getpermission(sClaim, b.MenuIndex.Value) != "2" && b.demo == false
                                     orderby b.nMenuOrder
                                     select new JObject {
                                     new JProperty("menuid", b.MenuId),
                                     new JProperty("menu_name", (qcompany.sotfware == true && b.MenuName == "รายงานการมาโรงเรียน") ?"รายงานกิจกรรมหน้าเสาธง":b.MenuName),
                                     new JProperty("menu_class", b.@class),
                                     new JProperty("menu_url", b.demo == false ?  b.url:(menudemo == true ? b.url : "#")),
                                     new JProperty("menu_title", b.title)
                                 });
                }
                else
                {
                    rss = new JArray(from b in qmenu
                                     where b.groupmenuid == groupmenuid
                                     && getpermission(sClaim, b.MenuIndex.Value) != "2"
                                     orderby b.nMenuOrder
                                     select new JObject {
                                     new JProperty("menuid", b.MenuId),
                                     new JProperty("menu_name", (qcompany.sotfware == true && b.MenuName == "รายงานการมาโรงเรียน") ?"รายงานกิจกรรมหน้าเสาธง":b.MenuName),
                                     new JProperty("menu_class", b.@class),
                                     new JProperty("menu_url", b.url),
                                     new JProperty("menu_title", b.title)
                                 });
                }
            }
            return rss;
        }

        private bool getpermissions(int groupmenu_id, List<TMenu> qmenu, List<permission> user_permissions)
        {
            var q = (from a in qmenu
                     join b in user_permissions on a.MenuId equals b.menu_id
                     where b.actvice != 2 && a.groupmenuid == groupmenu_id
                     select new { b.menu_id }).ToList();

            return q.Count > 0;
        }

        private JArray menu(List<TMenu> qmenu, int groupmenuid, List<permission> user_permissions, TCompany qcompany)
        {
            dynamic rss = new JArray();
            if (qmenu.Where(w => w.groupmenuid == groupmenuid).Count() > 1)
            {
                if (menudemo == false)
                {
                    rss = new JArray(from b in qmenu
                                     join a in user_permissions on b.MenuId equals a.menu_id into jab
                                     from ja in jab.DefaultIfEmpty()
                                     where b.groupmenuid == groupmenuid && b.demo == false
                                     orderby b.nMenuOrder
                                     select new JObject {
                                     new JProperty("menuid", b.MenuId),
                                     new JProperty("menu_name", (qcompany.sotfware == true && b.MenuName == "รายงานการมาโรงเรียน") ?"รายงานกิจกรรมหน้าเสาธง":b.MenuName),
                                     new JProperty("menu_class", b.@class),
                                     new JProperty("menu_url", b.demo == false ?  b.url:(ja != null && ja.actvice != 2 ? b.url : "#")),
                                     new JProperty("menu_title", b.title),
                                     new JProperty("target",target(b.target))
                                 });
                }
                else
                {
                    rss = new JArray(from b in qmenu
                                     join a in user_permissions on b.MenuId equals a.menu_id into jab
                                     from ja in jab.DefaultIfEmpty()
                                     where b.groupmenuid == groupmenuid
                                     orderby b.nMenuOrder
                                     select new JObject {
                                     new JProperty("menuid", b.MenuId),
                                     new JProperty("menu_name", (qcompany.sotfware == true && b.MenuName == "รายงานการมาโรงเรียน") ?"รายงานกิจกรรมหน้าเสาธง":b.MenuName),
                                     new JProperty("menu_class", b.@class),
                                     new JProperty("menu_url", ja != null && ja.actvice != 2 ? b.url : "#"),
                                     new JProperty("menu_title", b.title),
                                     new JProperty("target",target(b.target))
                                 });
                }
            }
            return rss;
        }

        private string target(int? target_id)
        {
            switch (target_id)
            {
                case 0: return "_self";
                case 1: return "_blank";
                case 2: return "_top";
                case 3: return "_parent";
                default: return "";
            }
        }

        private string getpermission(string permission, int index)
        {
            if (string.IsNullOrEmpty(permission)) return "2";
            else if (string.IsNullOrEmpty(permission)) return "2";
            else if (permission.Length - 1 >= index) return permission.Substring(index, 1);
            else return "2";
        }
    }
}
