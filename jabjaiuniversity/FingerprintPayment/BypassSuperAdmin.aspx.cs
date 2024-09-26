using FingerprintPayment.Models;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using MasterEntity;
using Newtonsoft.Json;
using SchoolBright.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class BypassSuperAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var jsonContent = EncryptMD5.UrlTokenDecode(Request.QueryString["q"]);

            var root = JsonConvert.DeserializeObject<RootObject>(jsonContent);

            var resultModel = new BaseResultModel();
            using (var dbsmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                try
                {
                    var admin = dbsmaster.TAdmins.AsNoTracking()
                        .Where(o => o.id == root.userId)
                        .FirstOrDefault();

                    var company = dbsmaster.TCompanies.AsNoTracking()
                        .Where(o => o.nCompany == root.schoolId)
                        .FirstOrDefault();

                    if (admin != null && company != null)
                    {

                        HttpContext.Current.Session["sEntities"] = company.sEntities;
                        string encryptedConnectionString = StringCipher.Encrypt(Connection.StringConnectionSchoolForDynamic(company.sEntities,ConnectionDB.Read), "JabJai");
                        HttpContext.Current.Session["SchoolDBConnectionString"] = encryptedConnectionString;
                        HttpContext.Current.Session["nCompany"] = root.schoolId;
                        HttpContext.Current.Session["Emp_Name"] = admin.FirstName + " " + admin.LastName;
                        HttpContext.Current.Session["Emp_Name_Only"] = admin.FirstName;

                        using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(company.sEntities, ConnectionDB.Read)))
                        {

                            HttpContext.Current.Session["Emp_Image_Profile"] = "/Content/Material/assets/img/logo-1.png";

                            if (company.isActive == false || company.cDel == true)
                            {
                                resultModel.Message = "SCHOOL IS NOT ACTIVE";
                                resultModel.StatusCode = "405";

                                Response.Redirect("Default.aspx");
                            }
                            else
                            {
                                try
                                {
                                    HttpContext.Current.Session["sEmpID"] = admin.id2;
                                    HttpContext.Current.Session["AdminID"] = root.userId;
                                    database.InsertLog("0", $"Super Admin ({admin.FirstName + " " + admin.LastName}) Login เข้าระบบ", company.sEntities, HttpContext.Current.Request, 999, 0, 0);

                                
                                    var q = @"
select *
From(
	select D.MenuId , 0 'Role' , D.MenuUrl
	FROM TGroupMenu A 
	LEFT JOIN TSegmentMenu B ON A.groupmenuid = B.GroupMenuID
	LEFT JOIN TMenu C ON C.SegmentID = B.ID
	LEFT JOIN TMenuPermission D ON C.MenuId = D.MenuID
	WHERE  C.MenuId is not null AND B.Active = 1 AND C.active = 1 AND A.groupmenuid not in (27) 
	
	union all

	SELECT B.MenuId , 0 'Role', B.MenuUrl 
	FROM  TMenu A 
	LEFT JOIN TMenuPermission B ON A.MenuId = B.MenuID
	WHERE  A.MenuId in (209)  and A.IsExceptAuth = 0  AND A.active = 1
)T
WHERE T.MenuID IS NOT NULL
order by T.MenuID
";

                                    var menus = dbsmaster.Database.SqlQuery<MenuGroupModel>(q).ToList();

                                    var payload = new
                                    {
                                        user_id = 0,
                                        admin_id = root.userId,
                                        entities = company.sEntities,
                                        update = DateTime.Now,
                                        user_name = admin.FirstName + " " + admin.LastName,
                                        SchoolDBConnectionString = encryptedConnectionString,
                                        NCompany = root.schoolId,
                                        IsAdmin = true,
                                        Menus = menus.Select(o => new
                                        {
                                            id = o.MenuId,
                                            role = o.Role,
                                            url = o.MenuUrl,
                                        }),
                                    };

                                    HttpContext.Current.Session["token"] = JsonConvert.SerializeObject(payload);

                                    resultModel.Message = "Success";
                                    resultModel.StatusCode = "200";

                                    JWTToken jWTToken = new JWTToken();
                                    var userData = new JWTToken().UserData;
                                    if (jWTToken.CheckToken(HttpContext.Current))
                                    {
                                        userData = jWTToken.getTokenValues(HttpContext.Current);
                                    }

                                    Response.Redirect("AdminMain.aspx");
                                }
                                catch (Exception ex)
                                {
                                    resultModel.Message = "Login Fail";
                                    resultModel.StatusCode = "501";
                                    resultModel.SystemErrorMessage = ex;

                                    Response.Redirect("Default.aspx");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultModel.Message = ex.StackTrace;
                    resultModel.StatusCode = "502";

                    Response.Redirect("Default.aspx");

                }

                resultModel.Message = "Login Fail";
                resultModel.StatusCode = "501";

                Response.Redirect("AdminMain.aspx");
            }
        }

        public class RootObject
        {
            public string userId { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public int schoolId { get; set; }
        }

        public class MenuGroupModel
        {
            public int MenuId { get; set; }
            public int Role { get; set; }
            public string MenuUrl { get; set; }
        }
    }
}