using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace FingerprintPayment
{
    [System.Web.Script.Services.ScriptService]
    [WebService(Namespace = "http://xmlforasp.net")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class mp : BaseMasterPage
    {
        public int sEmpID = 0;
        public string sEntities = "";
        public int schoolID = 0;
        public static RoleStatus Permission_Page = new RoleStatus();
        public static bool menudemo = ConfigurationManager.AppSettings["Menu_demo"].ToString().Trim() == "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/Files/uploads/Log.txt"),
                //string.Format("MasterPage Start : {0:dd/MM/yyyy HH:mm:ss.tttt} \r\n", DateTime.Now));

                //Response.Redirect("Logout.aspx");


                if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

                sEmpID = int.Parse(Session["sEmpID"] + "");
                sEntities = Session["sEntities"].ToString();
                schoolID = Convert.ToInt32(Session["nCompany"]);
                //Permission_Page = authentication.WebSite.RoleStatus(Request.Url, sEmpID, sEntities);
                Permission_Page.permission = "0";

                if (Permission_Page.permission == "2") Response.Redirect("~/Default.aspx");

                ViewState["Permission_Page"] = Permission_Page.permission;

                using (JabJaiMasterEntities entities = Connection.MasterEntities(ConnectionDB.Read))
                {
                    int SchoolId = (int)Session["nCompany"];

                    if (entities.TCompanies.Count(C => C.nCompany == SchoolId && (C.isActive == false || C.cDel == true)) > 0)
                    {
                        Response.Redirect("~/Logout.aspx");
                    }
                }

                //System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/Files/uploads/Log.txt"),
                //string.Format("MasterPage End : {0:dd/MM/yyyy HH:mm:ss.tttt} \r\n", DateTime.Now));
            }
            catch (Exception SystemErrorMessage)
            {
                //         System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/Files/uploads/Log.txt"),
                //             string.Format("MasterPage : {0:dd/MM/yyyy HH:mm:ss.tttt} \r\n", DateTime.Now) +
                //             "ERROR DATA : [{" + string.Format("\"Message\": \"{0}\" ,\"StackTrace\":\"{1}\",\"Source\":\"{2}\" ",
                //SystemErrorMessage.Message, SystemErrorMessage.StackTrace, SystemErrorMessage.Source) + "}] \r\n\r\n");
            }
        }

        public string listmenu(int user_id, int schoolID)
        {
            dynamic rss = new JObject();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.nCompany == schoolID);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    var qemployees_listmenu = dbschool.TEmployees.FirstOrDefault(f => f.sEmp == user_id);
                    var qmenu = dbmaster.TMenus.Where(w => w.showmenu == true).ToList();
                    var q_user = dbmaster.TUsers.FirstOrDefault(f => f.nCompany == qcompany.nCompany && f.cType == "1" && f.nSystemID == user_id);
                    var qgroupmenu = dbmaster.TGroupMenus.ToList();
                    qgroupmenu = qgroupmenu.Where(w => w.active == true).ToList();
                    var q_permission = dbmaster.permissions.Where(w => w.user_id == q_user.sID).ToList();
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
            if (ConfigurationManager.AppSettings["XRayWebApp"].ToString().ToLower().IndexOf("dev") != -1)
            {
                List<int> MenuId = new List<int> { 98, 131 };
                qmenu = qmenu.Where(r => !MenuId.Contains(r.MenuId)).ToList();
            }

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
                if (ConfigurationManager.AppSettings["XRayWebApp"].ToString().ToLower().IndexOf("dev") != -1)
                {
                    List<int> MenuId = new List<int> { 98, 131 };
                    qmenu = qmenu.Where(r => !MenuId.Contains(r.MenuId)).ToList();
                }

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

        public HtmlGenericControl SetBody
        {
            get { return this.from2; }
        }
    }
}