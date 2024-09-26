using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;
using System.Data;
using FingerprintPayment.Class;
using System.IO;
using JabjaiMainClass;
using FingerprintPayment.Employees.CsCode;
using Newtonsoft.Json;
using FingerprintPayment.Helper;
using RestSharp;
using System.Configuration;
using static FingerprintPayment.StudentInfo.StudentList;

namespace FingerprintPayment.Employees
{
    public partial class EmployeeList : EmployeeGateway
    {
        protected bool HaveNewResetPasswordPermission = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int schoolID = UserData.CompanyID;
                List<ListItem> listEmpType = Common.GetEmployeeTypeToDDL(schoolID);
                foreach (var l in listEmpType)
                {
                    this.ltrEmpType.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.Value, l.Text);
                }

                int userID = UserData.UserID;
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    // Check reset password permission
                    string query = $@"SELECT CAST(IIF(COUNT(*) = 1 AND MAX(A.Role) = 0, 1, 0) AS BIT) IsPermission
--SELECT A.MenuID, A.Role --Modify = 0, View = 1, NoPermission = 2
FROM [JabjaiMasterSingleDB].[dbo].[TGroupPermissionMenu] A
JOIN [JabjaiMasterSingleDB].[dbo].[TGroupPermissionUser] B ON A.SchoolID = B.SchoolID AND A.GroupID = B.GroupID
WHERE B.IsActive = 1 AND A.IsActive = 1 AND A.MenuID = 355 and B.UserID = {userID}";
                    HaveNewResetPasswordPermission = en.Database.SqlQuery<bool>(query).FirstOrDefault();
                    HaveNewResetPasswordPermission |= UserData.IsAdmin;
                }
            }
        }

        #region Method

        [WebMethod]
        public static object RemoveItem(int eid)
        {
            bool success = true;
            int status = 200;
            string message = "";

            var userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
               
                  
                    try
                    {
                        TEmployee pi = en.TEmployees.Where(w => w.SchoolID == schoolID && w.sEmp == eid).FirstOrDefault();

                        // Check data befour save
                        // Check data
                        if (pi == null)
                        {
                            success = false;
                            status = 201;
                            message += "[ไม่พบข้อมูลบุคลากร]<br/>";
                        }

                        // Check money
                        if (pi.nMoney > 0 && pi.nMoney != null)
                        {
                            success = false;
                            status = 202;
                            message += "[มียอดเงินคงเหลือในระบบ]<br/>";
                        }

                        // [P-1] Check exist sEmp in TNotificationSetting - ครูที่เป็นผู้รับใบแจ้งหนี้ ห้ามลบ/ปรับสถานะเป็นลาออก
                        int countNotificationSetting = dbMaster.TNotificationSettings.Where(w => w.SchoolId == schoolID && w.StaffID == eid).Count();
                        if (countNotificationSetting > 0)
                        {
                            success = false;
                            status = 203;
                            message += "[ไม่สามารถทำการลบหรือบันทึกลาออกได้ เนื่องจากผู้ใช้งานท่านนี้ถูกตั้งเป็นผู้รับใบแจ้งหนี้ กรุณาทำการเปลี่ยนแปลงข้อมูลผู้รับใบแจ้งหนี้แล้วลองใหม่อีกครั้ง]<br/>";
                        }

                        // School Data
                        if (success)
                        {
                            pi.cDel = "1";
                            pi.dUpdate = DateTime.Now;
                            pi.UpdatedBy = userData.UserID;
                            pi.UpdatedDate = DateTime.Now;

                            // Set flag delete other table
                            en.TEmpAddresses.Where(w => w.SchoolID == schoolID && w.sEmp == eid).ToList().ForEach(e => { e.cDel = true; e.UpdateBy = userData.UserID; e.UpdateDate = DateTime.Now; });
                            en.TEmpEducationInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == eid).ToList().ForEach(e => { e.cDel = true; e.UpdateBy = userData.UserID; e.UpdateDate = DateTime.Now; });
                            en.TEmpFamilies.Where(w => w.SchoolID == schoolID && w.sEmp == eid).ToList().ForEach(e => { e.cDel = true; e.UpdateBy = userData.UserID; e.UpdateDate = DateTime.Now; });
                            en.TEmpHonors.Where(w => w.SchoolID == schoolID && w.sEmp == eid).ToList().ForEach(e => { e.cDel = true; e.UpdateBy = userData.UserID; e.UpdateDate = DateTime.Now; });
                            en.TEmpInsignias.Where(w => w.SchoolID == schoolID && w.sEmp == eid).ToList().ForEach(e => { e.cDel = true; e.UpdateBy = userData.UserID; e.UpdateDate = DateTime.Now; });
                            en.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == eid).ToList().ForEach(e => { e.cDel = true; e.UpdateBy = userData.UserID; e.UpdateDate = DateTime.Now; });
                            en.TEmpProfessionalLicenses.Where(w => w.SchoolID == schoolID && w.sEmp == eid).ToList().ForEach(e => { e.cDel = true; e.UpdateBy = userData.UserID; e.UpdateDate = DateTime.Now; });
                            en.TEmpSalaries.Where(w => w.SchoolID == schoolID && w.sEmp == eid).ToList().ForEach(e => { e.cDel = true; e.UpdatedBy = userData.UserID; e.UpdatedDate = DateTime.Now; });
                            en.TEmpTeachings.Where(w => w.SchoolID == schoolID && w.sEmp == eid).ToList().ForEach(e => { e.cDel = true; e.UpdateBy = userData.UserID; e.UpdateDate = DateTime.Now; });
                            en.TEmpTrainings.Where(w => w.SchoolID == schoolID && w.sEmp == eid).ToList().ForEach(e => { e.cDel = true; e.UpdateBy = userData.UserID; e.UpdateDate = DateTime.Now; });

                            // User Card
                            dbMaster.TUser_Card.Where(w => w.SchoolID == schoolID && w.sID == eid).ToList().ForEach(e => { e.IsDel = true; e.IsActive = false; e.ModifyBy = userData.UserID; e.Modified = DateTime.Now; });

                            en.SaveChanges();

                            database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลบุคลากร " + pi.sName + " " + pi.sLastname, HttpContext.Current.Request, 156, 4, 0, schoolID);

                            // Master Data
                            var masterUser = dbMaster.TUsers.Where(w => w.sID == eid && w.nCompany == schoolID && w.cType == "1").FirstOrDefault();
                            if (masterUser != null)
                            {
                                masterUser.cDel = "1";
                                masterUser.dUpdate = DateTime.Now;

                                dbMaster.SaveChanges();

                                var p = en.TEmployees.FirstOrDefault(f => f.SchoolID == schoolID && f.sEmp == eid);
                                UpdateMemory memory = new UpdateMemory(userData.AuthKey, userData.AuthValue);
                                memory.Employee(p, masterUser);
                                JabjaiMainClass.Autocompletes.TopupMoney.Remove(eid.ToString());
                            }

                          

                            // Update memory
                            JabjaiMainClass.Autocompletes.TopupMoney.Remove(eid + "");
                        }
                    }
                    catch (Exception err)
                    {
                        success = false;
                        status = 500;
                        message += err.Message;

                       
                    }
                

                var result = new { success, status, message };

                return JsonConvert.SerializeObject(result);
            }
        }

        [WebMethod]
        public static object ShowPassword(int eid)
        {
            object result = null;

            try
            {
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    int schoolID = GetUserData().CompanyID;
                    result = dbMaster.TUsers.Where(w => w.sID == eid && w.cType == "1" && w.nCompany == schoolID).Select(s => new { fullName = s.sName + " " + s.sLastname, userName = s.username, password = s.UseEncryptPassword ? "" : s.userpassword }).FirstOrDefault();
                }
            }
            catch
            {
                result = "error";
            }

            return result;
        }

        [WebMethod]
        public static object ResetPIN(int eid)
        {
            object result = "success";

            try
            {
                JWTToken.userData userData = GetUserData();
                int schoolID = userData.CompanyID;
                int userID = userData.UserID;
                string URL = ConfigurationManager.AppSettings["PaymentApi"].ToString() + $"/api/shop/pos/resetpin?UserID={eid}&SchoolID={schoolID}";

                var client = new RestClient(URL);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader(userData.AuthKey, userData.AuthValue);

                string response = client.Execute(request).Content;
            }
            catch
            {
                result = "error";
            }

            return result;
        }

        [WebMethod]
        public static object ResetPassword(ResetPasswordInfo resetPasswordInfo)
        {
            string result = "";
            string message = "";

            try
            {
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var userData = GetUserData();
                    int schoolID = userData.CompanyID;
                    var userObj = dbMaster.TUsers.Where(w => w.sID == resetPasswordInfo.eid && w.cType == "1" && w.nCompany == schoolID).FirstOrDefault();
                    if (userObj != null)
                    {
                        userObj.userpassword = resetPasswordInfo.newPassword;
                        userObj.PasswordHash = ComFunction.HashSHA1(resetPasswordInfo.newPassword);
                        userObj.UseEncryptPassword = false;

                        dbMaster.SaveChanges();

                        result = "success";

                        database.InsertLog(userData.UserID.ToString(), "รีเซ็ตรหัสผ่านบุคลากร " + userObj.sName + " " + userObj.sLastname + " สำเร็จ", HttpContext.Current.Request, 2, 3, 0, schoolID);
                    }
                    else
                    {
                        result = "error";
                        message = "ไม่พบข้อมูลบุคลากรรายนี้";
                    }
                }
            }
            catch (Exception error)
            {
                result = "error";
                message = error.Message;
            }

            return JsonConvert.SerializeObject(new { result, message });
        }

        [WebMethod]
        public static string[] GetEmployeeName(string keyword)
        {
            int schoolID = GetUserData().CompanyID;
            JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

            string empIDMaster = "";
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sql = string.Format(@"SELECT TOP 100 sID FROM TUser WHERE nCompany = {0} AND cType = '1' AND username LIKE N'%{1}%'", schoolID, keyword);
                List<int> empIDs = dbMaster.Database.SqlQuery<int>(sql).ToList();
                empIDMaster = string.Join(",", empIDs);
            }

            string sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname
FROM TEmployees
WHERE cDel IS NULL AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') 
AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sPhone LIKE N'%{1}%' OR sEmp IN (SELECT TOP 100 sEmp FROM TEmployeeInfo WHERE cDel = 0 AND SchoolID = {0} AND Code LIKE N'%{1}%') {2}) 
GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, keyword, (string.IsNullOrEmpty(empIDMaster) ? "" : string.Format(@"OR sEmp IN ({0})", empIDMaster)));
            List<string> result = dbschool.Database.SqlQuery<string>(sqlQuery).ToList();

            return result.ToArray();
        }

        #endregion

        public class ResetPasswordInfo
        {
            public int eid { get; set; }
            public string newPassword { get; set; }

        }
    }
}