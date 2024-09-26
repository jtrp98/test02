using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using Amazon.XRay.Recorder.Handlers.AspNet;
using FingerprintPayment.Class;
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
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI.WebControls;
using static FingerprintPayment.Leaveform.AllLeaveLogic;

namespace FingerprintPayment
{
    public partial class Default2 : System.Web.UI.Page
    {
        //[Inject]
        //public ILoginService LoginService { get; set; }

        [WebMethod(EnableSession = true)]
        public static object Login(string user_id, string password)
        {

            //return "ปิดปรับปรุงระบบตั้งแต่ วันที่ 16-20 ต.ค. 2561 ";
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
                              where a.username.Trim() == user_id && a.cType == "1" && a.cDel == null
                              // && (a.userpassword == password || a.userpassword == dBirth1 || a.userpassword == dBirth2)
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
                                  a.UseEncryptPassword
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
                        string encryptedConnectionString = StringCipher.Encrypt(Connection.StringConnectionSchoolForDynamic(sEntities,ConnectionDB.Read), "JabJai");
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

                                return resultModel;
                            }
                            else if (dbsmaster.TCompanies.Count(w => (w.isActive == false || w.cDel == true) && w.nCompany == q1.nCompany) > 0)
                            {
                                resultModel.Message = "SCHOOL IS NOT ACTIVE";
                                resultModel.StatusCode = "405";

                                return resultModel;
                            }
                            else // if (q_permission.Count() > 0)
                            {
                                try
                                {
                                    bool menudemo = ConfigurationManager.AppSettings["Menu_demo"].ToString().Trim() == "1";

                                    int nid = int.Parse(sID);
                                    HttpContext.Current.Session["sEmpID"] = sID;
                                    database.InsertLog(sID, "Login เข้าระบบ", sEntities, HttpContext.Current.Request, 999, 0, 0);
                                    var permission = menu_sidebar.permission_Menu(q1.nSystemID.Value, sEntities, menudemo);

                                    var menus = (from a in dbsmaster.TGroupPermissionUser.Where(o => o.SchoolID == q1.nCompany && o.UserID == nid && o.IsActive == true && o.IsDel == false)
                                                 from b in dbsmaster.TGroupPermissionMenu.Where(o => o.SchoolID == a.SchoolID && o.GroupID == a.GroupID && o.Type == "W")
                                                 from c in dbsmaster.TMenuPermission.Where(o => o.MenuID == b.MenuID)
                                                 from d in dbsmaster.TGroupPermission.Where(o => o.GroupID == a.GroupID && o.SchoolID == a.SchoolID)

                                                 select new //JabjaiMainClass.Models.TokenModel.MenuList
                                                 {
                                                     id = b.MenuID,
                                                     role = b.Role,
                                                     url = c.MenuUrl,
                                                     groupName = d.GroupName,
                                                     isEditable = d.IsEditable,
                                                 }).ToList();

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
                                    var payload = new //JabjaiMainClass.Models.TokenModel
                                    {
                                        user_id = q1.nSystemID,
                                        entities = sEntities,
                                        update = q1.dUpdate,
                                        user_name = q1.sName + " " + q1.sLastname,
                                        SchoolDBConnectionString = encryptedConnectionString,
                                        NCompany = q1.nCompany,
                                        IsAdmin = isAdmin,
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

                                    return resultModel;
                                }
                                catch (Exception ex)
                                {
                                    resultModel.Message = "Login Fail";
                                    resultModel.StatusCode = "501";
                                    resultModel.SystemErrorMessage = ex;

                                    //System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/Upload/Log.txt"),
                                    //     string.Format("Page Login Result : {0:dd/MM/yyyy HH:mm:ss.tttt} \r\nRESULT DATA : {1} \r\n", DateTime.Now, new JavaScriptSerializer().Serialize(resultModel)));

                                    return resultModel;
                                }
                            }
                            //else
                            //{
                            //    resultModel.Message = "No Permission";
                            //    resultModel.StatusCode = "404";

                            //    //System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/Upload/Log.txt"),
                            //    //     string.Format("Page Login Result : {0:dd/MM/yyyy HH:mm:ss.tttt} \r\nRESULT DATA : {1} \r\n", DateTime.Now, new JavaScriptSerializer().Serialize(resultModel)));

                            //    return resultModel;
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    //System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/Upload/Log.txt"),
                    //   string.Format("Page Login Result : {0:dd/MM/yyyy HH:mm:ss.zzzz} \r\n", DateTime.Now));

                    return ex.StackTrace + "\r\n DataSource : " + ConfigurationManager.AppSettings["DataSource"].ToString() + Environment.NewLine + " DB_Password : " + ConfigurationManager.AppSettings["DB_Password"].ToString();
                }

                resultModel.Message = "Login Fail";
                resultModel.StatusCode = "501";

                //System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/Upload/Log.txt"),
                //     string.Format("Page Login Result : {0:dd/MM/yyyy HH:mm:ss.zzzz} \r\nRESULT DATA : {1} \r\n", DateTime.Now, new JavaScriptSerializer().Serialize(resultModel)));

                return resultModel;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            AWSXRay xray = new AWSXRay();

            AWSXRayRecorder recorder = xray.Register();

            HttpContext context = HttpContext.Current;
            if (context != null && context.Items.Contains(AWSXRayASPNET.XRayEntity))
            {
                Segment requestSegment = (Segment)context.Items[AWSXRayASPNET.XRayEntity];
                recorder.SetEntity(requestSegment);
            }

            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) Response.Redirect("StudentCall/main.aspx");

            //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            //string str = "";
            //for (int j = 0; j <= nics.Length - 1; j++)
            //{
            //    PhysicalAddress address = nics[j].GetPhysicalAddress();
            //    byte[] bytes = address.GetAddressBytes();
            //    str += "<br>" + nics[j].Description + " : ";
            //    for (int i = 0; i < bytes.Length; i++)
            //    {
            //        str += bytes[i].ToString("X2");
            //        if (i != bytes.Length - 1)
            //        {
            //            str += ("-");
            //        }
            //    }
            //}
            //Response.Write(str);
        }

        protected void txtCheckFinger_TextChanged(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx?id=" + txtCheckFinger.Text);
        }
    }
}
