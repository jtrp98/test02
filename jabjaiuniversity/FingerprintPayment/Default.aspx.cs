﻿using FingerprintPayment.Models;
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
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Infrastructure.MappingViews;
using static FingerprintPayment.Leaveform.AllLeaveLogic;
using System.Web.Security;
using FingerprintPayment.Helper;

namespace FingerprintPayment
{
    public partial class _Default : System.Web.UI.Page
    {
        protected List<string> BANNER_LIST = new List<string>();
        public static HttpCookie languageCookie;
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) Response.Redirect("AdminMain.aspx");

            // Execute once
            InitialControl();

            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                lang = FingerprintPayment.Helper.LanguageHelper.GetDefaultLanguage();
                //var userLanguage = Request.UserLanguages;
                //    var userLang = userLanguage != null ? userLanguage[0] : "";
                //    if (userLang != "")
                //    {
                //        lang = userLang;
                //    }
                //    else
                //    {
                //        lang = FingerprintPayment.Helper.LanguageHelper.GetDefaultLanguage();
                //    }
            }

            try
            {
                new LanguageHelper().SetLanguage(lang);
            }
            catch //(Exception ex)
            {
            }

            languageCookie = Request.Cookies["culture"];
        }

        public static string HashPassword(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
        }

        [WebMethod(EnableSession = true)]
        public static string Login(int schoolId, string username, string password)
        {
            BaseResultModel resultModel = new BaseResultModel();
            using (JabJaiMasterEntities dbsmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                try
                {
                    DateTime.TryParseExact(password, "ddMMyyyy", new CultureInfo("en-us"), DateTimeStyles.None, out DateTime dateUS);
                    DateTime.TryParseExact(password, "ddMMyyyy", new CultureInfo("th-th"), DateTimeStyles.None, out DateTime dateTH);
                    string dateUSPassword = dateUS.ToString("ddMMyyyy");
                    string dateTHPassword = dateTH.ToString("ddMMyyyy");

                    //var userDTO = Helper.ServiceHelper.ValidateUser(user_id, password);
                    var q1 = (from a in dbsmaster.TUsers
                              join b in dbsmaster.TCompanies on a.nCompany equals b.nCompany
                              where a.nCompany == schoolId && a.username.Trim() == username && a.cType == "1" && a.cDel == null
                              //&& (a.userpassword == password || a.userpassword == dateUSPassword || a.userpassword == dateTHPassword)
                              select new
                              {
                                  a.nSystemID,
                                  a.nCompany,
                                  b.sEntities,
                                  a.dBirth,
                                  a.username,
                                  a.userpassword,
                                  a.sID,
                                  a.sName,
                                  a.sLastname,
                                  a.dUpdate,
                                  a.PasswordHash,
                                  a.UseEncryptPassword,
                                  a.sEmail
                              })
                              .OrderByDescending(o => o.sID)
                              .FirstOrDefault();

                    bool _cPassword = false;

                    if (q1 != null)
                    {
                        if (q1.UseEncryptPassword)
                        {
                            string PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                            if (q1.PasswordHash == PasswordHash)
                            {
                                _cPassword = true;
                            }
                        }
                        else
                        {
                            if (q1.userpassword == password || q1.userpassword == dateUSPassword || q1.userpassword == dateTHPassword)
                            {
                                _cPassword = true;
                            }
                        }
                    }

                    if (_cPassword)
                    {
                        string sEntities = q1.sEntities;
                        string sID = q1.nSystemID.ToString();
                        HttpContext.Current.Session["sEntities"] = q1.sEntities;
                        string encryptedConnectionString = StringCipher.Encrypt(Connection.StringConnectionSchoolForDynamic(sEntities, ConnectionDB.Read), "JabJai");
                        HttpContext.Current.Session["SchoolDBConnectionString"] = encryptedConnectionString;
                        HttpContext.Current.Session["nCompany"] = q1.nCompany;
                        HttpContext.Current.Session["Emp_Name"] = q1.sName + " " + q1.sLastname;
                        HttpContext.Current.Session["Emp_Name_Only"] = q1.sName;

                        using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                        {
                            var qemployees = dbschool.TEmployees.FirstOrDefault(f => f.sEmp == q1.nSystemID);
                            //var q_permission = dbsmaster.permissions.Where(w => w.user_id == q1.sID && w.actvice < 2).ToList();
                            var f_employee = dbschool.TEmployees.FirstOrDefault(f => f.sEmp == q1.sID);
                            var f_empSalary = dbschool.TEmpSalaries.FirstOrDefault(f => f.sEmp == f_employee.sEmp);
                            int? WorkStatus = f_empSalary == null ? 0 : f_empSalary.WorkStatus;

                            if (qemployees != null)
                            {
                                HttpContext.Current.Session["Emp_Image_Profile"] = string.IsNullOrEmpty(qemployees.sPicture) ? "/Content/Material/assets/img/logo-1.png" : qemployees.sPicture;
                            }

                            if (WorkStatus == 2 || WorkStatus == 3)
                            {
                                resultModel.Message = "No Permission";
                                resultModel.StatusCode = "401";

                                return JsonConvert.SerializeObject(resultModel);
                            }
                            else if (dbsmaster.TCompanies.Count(w => (w.isActive == false || w.cDel == true) && w.nCompany == q1.nCompany) > 0)
                            {
                                resultModel.Message = "SCHOOL IS NOT ACTIVE";
                                resultModel.StatusCode = "405";

                                return JsonConvert.SerializeObject(resultModel);
                            }
                            else //if (q_permission.Count() > 0)
                            {
                                try
                                {
                                    bool menudemo = ConfigurationManager.AppSettings["Menu_demo"].ToString().Trim() == "1";

                                    int nid = int.Parse(sID);
                                    HttpContext.Current.Session["sEmpID"] = sID;
                                    database.InsertLog(sID, "Login เข้าระบบ", sEntities, HttpContext.Current.Request, 999, 0, 0);
                                    var permission = menu_sidebar.permission_Menu(q1.nSystemID.Value, sEntities, menudemo);

                                    var menus = (from a in dbsmaster.TGroupPermissionUser.Where(o => o.SchoolID == q1.nCompany && o.UserID == nid && o.IsActive == true)
                                                 from b in dbsmaster.TGroupPermissionMenu.Where(o => o.SchoolID == a.SchoolID && o.GroupID == a.GroupID && o.Type == "W")
                                                 from c in dbsmaster.TMenuPermission.Where(o => o.MenuID == b.MenuID)
                                                 from d in dbsmaster.TGroupPermission.Where(o => o.GroupID == a.GroupID && o.SchoolID == a.SchoolID)

                                                 select new
                                                 {
                                                     id = b.MenuID,
                                                     role = b.Role,
                                                     url = c.MenuUrl,
                                                     groupName = d.GroupName,
                                                     isEditable = d.IsEditable,
                                                 })
                                                 .Distinct()
                                                 .ToList();

                                    var groupNames = menus.GroupBy(o => new { o.groupName, o.isEditable })
                                        .Select(o => new
                                        {
                                            o.Key.groupName,
                                            o.Key.isEditable,
                                        })
                                        .ToList();

                                    var isAdmin = false;

                                    if (groupNames.Any(o => o.groupName == "แอดมิน/ผู้บริหาร" && o.isEditable == false))
                                    {
                                        isAdmin = true;
                                    }


                                    JwtManagerFunction jwtManager = new JwtManagerFunction(Global.SecretAPIKey);
                                    string AuthValue = jwtManager.GenerateToken(username, password, schoolId.ToString(), sID.ToString(), q1.sEmail);
                                    string AuthKey = $"jabjaikey-{q1.nCompany}-{q1.sID}";

                                    var payload = new
                                    {
                                        user_id = q1.nSystemID,
                                        entities = sEntities,
                                        update = q1.dUpdate,
                                        user_name = q1.sName + " " + q1.sLastname,
                                        SchoolDBConnectionString = encryptedConnectionString,
                                        NCompany = q1.nCompany,
                                        IsAdmin = isAdmin,
                                        AuthValue,
                                        AuthKey,
                                        Menus = menus.Select(o => new
                                        {
                                            o.id,
                                            o.role,
                                            o.url,
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

                                    return JsonConvert.SerializeObject(resultModel);
                                }
                                catch (Exception ex)
                                {
                                    resultModel.Message = "Login Fail";
                                    resultModel.StatusCode = "501";
                                    resultModel.SystemErrorMessage = ex;

                                    return JsonConvert.SerializeObject(resultModel);
                                }
                            }
                            //else
                            //{
                            //    resultModel.Message = "No Permission";
                            //    resultModel.StatusCode = "404";

                            //    return JsonConvert.SerializeObject(resultModel);
                            //}

                        }
                    }
                }
                catch (Exception ex)
                {
                    resultModel.Message = ex.StackTrace;
                    resultModel.StatusCode = "502";

                    return JsonConvert.SerializeObject(resultModel);
                }

                resultModel.Message = "Login Fail";
                resultModel.StatusCode = "501";

                return JsonConvert.SerializeObject(resultModel);
            }
        }


        [WebMethod(EnableSession = true)]
        public static void SetLanguage()
        {

            string lang = null;

            if (languageCookie != null)
            {
                if (languageCookie.Value == "en")
                    lang = "th";
                else
                    lang = "en";
            }
            else
            {
                lang = LanguageHelper.GetDefaultLanguage();

            }

            try
            {
                new LanguageHelper().SetLanguage(lang);
            }
            catch //(Exception ex)
            {
            }
            //HttpContext.Current.Response.Redirect("~/Default.aspx");


        }

        private void InitialControl()
        {
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var schools = dbMaster.TCompanies.Where(w => w.isActive == true && w.cDel == false).OrderBy(o => o.sCompany).ToList();
                foreach (var s in schools)
                {
                    ltrSchoolList.Text += string.Format(@"<option value=""{0}"">{1}</option>", s.nCompany, s.sCompany);
                }

                var dateNow = DateTime.Today;
                BANNER_LIST = dbMaster.TNews2
                    .Where(o => o.IsSystemLogin == true && dateNow >= o.StartDate && dateNow <= o.EndDate)
                    .OrderBy(o => o.StartDate)
                    .Select(o => o.SystemLogin)
                    .ToList();
            }
        }
    }
}
