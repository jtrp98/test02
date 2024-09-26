using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class app_permissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            //JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static string returnlist(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    dynamic rss = new JObject();

                    TEmployeeType employeeType = null;


                    string SQL = $@"SELECT ISNULL(A.sName,'') AS sName,ISNULL(A.sLastname,'') AS sLastname,A.sIdentification,A.sEmp,A.dBirth,
A.cType,A.sPhone,B.Title AS memberType
FROM TEmployees AS A
LEFT JOIN TEmployeeType AS B ON A.SchoolID = B.SchoolID AND(A.cType = B.nTypeId OR A.cType = B.nTypeId2)
LEFT JOIN TEmpSalary C ON A.sEmp = C.sEmp AND A.SchoolID = C.SchoolID
WHERE A.SchoolID = { userData.CompanyID} AND isnull(A.cDel , '0') = '0' 
AND   (
   ( ISNULL(C.WorkStatus,1) = 1 AND CAST( GETDATE() AS Date ) >= CAST( ISNULL(C.WorkStartDate,GETDATE()) AS Date ) )
   OR ( (C.WorkStatus = 2 OR C.WorkStatus = 3) AND CAST( GETDATE() AS Date ) <  CAST(C.DayQuit AS Date ) )
  )";

                    if (!string.IsNullOrEmpty(search.user_type))
                    {
                        int nTypeId = int.Parse(search.user_type ?? "0");
                        employeeType = dbschool.TEmployeeTypes.FirstOrDefault(f => f.nTypeId == nTypeId);
                        if (employeeType != null) SQL += $" AND ( cType = '{employeeType.nTypeId}' OR cType = '{employeeType.nTypeId2}' )";
                    }
                    //else
                    //{
                    //    SQL += $" AND ( cType IN ( SELECT nTypeId FROM TEmployeeType WHERE IsDel = 0 AND IsActive = 1 AND SchoolID = {userData.CompanyID} ) " +
                    //        $"OR cType  IN (SELECT nTypeId2 FROM TEmployeeType WHERE  IsDel = 0 AND IsActive = 1 AND nTypeId2 IS NOT NULL AND SchoolID = {userData.CompanyID} ) )";
                    //}

                    var tEmployees = dbschool.Database.SqlQuery<TM_Employees>(SQL).ToList();
                    //var tEmployees = (from a in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)
                    //                      //join b in dbschool.TEmployeeInfoes.Where(w => w.SchoolID == userData.CompanyID) on a.sEmp equals b.sEmp

                    //                  where (employeeType == null || (a.cType == employeeType.nTypeId || a.cType == employeeType.nTypeId2))
                    //                  select a).ToList();

                    var tMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "1" && w.cDel == null).ToList();
                    //if (!string.IsNullOrEmpty(sName)) tEmployees = tEmployees.Where(w => (w.sName + " " + w.sLastname).Contains(sName)).ToList();
                    var qtimetable = dbschool.TTimetypes.Where(w => w.SchoolID == userData.CompanyID).ToList();
                    int index = 1;

                    var q = (from a in tEmployees
                             join master in tMaster on a.sEmp equals master.nSystemID.Value
                             join timetable in qtimetable on a.nTimeType equals timetable.nTimeType into timetable_join
                             from timetable in timetable_join.DefaultIfEmpty()
                             where (a.sName.Contains(search.wording) || a.sLastName.Contains(search.wording) || (a.sName + " " + a.sLastName).Contains(search.wording)) //&& (a.cType ?? "").Contains(search.user_type)
                             select new empList
                             {
                                 number = 1,
                                 sName = a.sName,
                                 sLastName = a.sLastName,
                                 sIdentification = a.sIdentification,
                                 sEmp = a.sEmp,
                                 dBirth = a.dBirth.HasValue ? a.dBirth.Value.ToString("dd/MM/yyyy") : null,
                                 fingerstatus = master.sFinger != null && master.sFinger2 != null,
                                 cType = a.cType,
                                 phone = a.sPhone,
                                 timetype = (timetable != null ? timetable.sTimeType : null),
                                 memberType = a.memberType
                             }).ToList();

                    rss.DATA = new JArray(from a in q.ToPagedList(search.pageNumber.Value, search.pageSize.Value).ToList()
                                          select new JObject
                                          {
                                              new JProperty("index",((search.pageNumber-1) * search.pageSize)+index++),
                                              new JProperty("emp_name", a.sName),
                                              new JProperty("emp_lastname",a.sLastName),
                                              new JProperty("emp_type", a.memberType),
                                              new JProperty("emp_id", a.sEmp),
                                              new JProperty("phone", a.phone),
                                              new JProperty("birth",a.dBirth),
                                          });

                    rss.FOOT = new JObject()
                    {
                        new JProperty("pageSize",(q.Count() / search.pageSize) + (q.Count() % search.pageSize == 0?0:1))
                    };

                    return rss.ToString();

                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string get_permission(int user_id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                bool menudemo = ConfigurationManager.AppSettings["Menu_demo"].ToString().Trim() == "1";
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                string web = authentication.WebSite.permission_setting(user_id, entities, menudemo, dbmaster);
                string mobile = authentication.Mobile.permission_setting(user_id, entities, dbmaster);

                return "{ \"web\" : " + web + ",\"mobile\": " + mobile + " }";
            }
        }

        private static string get_permissionMobile(int user_id, JabJaiMasterEntities dbmaster)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var q = dbmaster.TMobileMenus.ToList();
            dynamic rss = new JArray();
            rss = new JArray(from a in q
                             where a.SubMenu_Id == null
                             select new JObject
                                      {
                                          new JProperty("menu_name",a.Menu_Name),
                                          new JProperty("menu_id",a.Menu_Id),
                                          new JProperty("submenu", addSubMenu(q,a.Menu_Id) )
                                      });
            return rss.ToString();
        }

        private static object addSubMenu(List<TMobileMenu> q, int menu_Id)
        {
            if (q.Count(c => c.SubMenu_Id == menu_Id) == 0)
            {
                return new JArray(from a in q
                                  where a.Menu_Id == menu_Id
                                  select new JObject
                                  {
                                      new JProperty("submenu_name",a.Menu_Name),
                                      new JProperty("submenu_id",a.Menu_Id),
                                  });
            }
            else
            {
                return new JArray(from a in q
                                  where a.SubMenu_Id == menu_Id
                                  select new JObject
                                  {
                                      new JProperty("submenu_name",a.Menu_Name),
                                      new JProperty("submenu_id",a.Menu_Id),
                                  });
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static string UpdatePermission(Permission_data permission_Data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                var f_user = dbmaster.TUsers.FirstOrDefault(f => f.nSystemID == permission_Data.user_id && f.cType == "1" && f.nCompany == f_company.nCompany);

                var l_Webpermission = permission_Data.Web_menu.ToList();
                var l_Mobilepermission = permission_Data.Mobile_menu.ToList();

                authentication.Mobile.PermissioUpdate(dbmaster, f_user.sID, l_Mobilepermission);
                authentication.WebSite.PermissioUpdate(dbmaster, f_user.sID, l_Webpermission);

                database.InsertLog(userData.UserID.ToString(), "ตั้งค่าสิทธิ์พนักงาน : " + f_user.sName + " " + f_user.sLastname, userData.Entities, HttpContext.Current.Request, 16, 2, 0);

                return "Success";
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object getEmployeeTypes()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var employeeTypes = entities.TEmployeeTypes.Where(w => w.SchoolID == userData.CompanyID && w.IsDel == false && w.IsActive == true).ToList();

                dynamic res;

                res = (from a in employeeTypes
                       select new
                       {
                           a.nTypeId,
                           a.Title
                       }).ToList();

                return res;

            }
        }

        public class Search
        {
            public string wording { get; set; }
            public Nullable<int> pageSize { get; set; }
            public Nullable<int> pageNumber { get; set; }
            public string user_type { get; set; }
        }

        public class emp_list
        {
            public string emp_name { get; set; }
            public int emp_id { get; set; }
            public string phone { get; set; }
        }

        public class Permission_data
        {
            public int user_id { get; set; }
            public List<authentication.Permission_data> Web_menu { get; set; }
            public List<authentication.Permission_data> Mobile_menu { get; set; }
        }

    }

    internal class empList
    {
        public int number { get; set; }
        public string sName { get; set; }
        public string sLastName { get; set; }
        public string sIdentification { get; set; }
        public int sEmp { get; set; }
        public string dBirth { get; set; }
        public bool fingerstatus { get; set; }
        public string cType { get; set; }
        public string phone { get; set; }
        public string timetype { get; set; }
        public string memberType { get; set; }
        public Nullable<int> nTimeType { get; set; }
    }

    internal class TM_Employees
    {
        public string sName { get; set; }
        public string sLastName { get; set; }
        public string sIdentification { get; set; }
        public int sEmp { get; set; }
        public DateTime? dBirth { get; set; }
        public string cType { get; set; }
        public string sPhone { get; set; }
        public string timetype { get; set; }
        public string memberType { get; set; }
        public Nullable<int> nTimeType { get; set; }
    }
}