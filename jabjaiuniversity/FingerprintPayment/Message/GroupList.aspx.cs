using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Message.CsCode;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Message
{
    public partial class GroupList : MessageGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadGroup()
        {
            var jsonStream = "";
            HttpContext.Current.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonStream = inputStream.ReadToEnd();
            }
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int draw = Convert.ToInt32(jsonObject["draw"]);
            int pageIndex = Convert.ToInt32(jsonObject["page"]);
            int pageSize = Convert.ToInt32(jsonObject["length"]);
            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            string sortBy = "SMSGroupID";
            switch (sortIndex)
            {
                case "1": sortBy = "SMSGroupName"; break;
                case "2": sortBy = "SMSGroupNameEn"; break;
                case "3": sortBy = "Status"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string groupName = Convert.ToString(jsonObject["groupName"]);

            var json = QueryEngine.LoadGroupJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, groupName);

            return json;
        }

        [WebMethod]
        public static object RemoveItem(int gid)
        {
            bool success = true;
            string message = "success";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    var pi = en.TSMSGroup.First(f => f.SchoolID == schoolID && f.SMSGroupID == gid);
                    if (pi != null)
                    {
                        if (string.IsNullOrEmpty(pi.GroupDefault))
                        {
                            pi.cDel = true;
                            pi.UpdatedBy = userData.UserID;
                            pi.UpdatedDate = DateTime.Now;

                            en.SaveChanges();

                            //database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลนักเรียน " + pi.sName + " " + pi.sLastname, HttpContext.Current.Request, 2, 4, 0, schoolID);
                        }
                        else
                        {
                            success = false;
                            message += "ไม่สามารถลบกลุ่มผู้รับกลุ่มนี่ได้";
                        }
                    }
                    else
                    {
                        success = false;
                        message += "ไม่พบข้อมูลกลุ่มผู้รับ";
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message += err.Message;
                }
            }

            return JsonConvert.SerializeObject(new { success, message });
        }

        [WebMethod]
        public static object SwitchStatus(int gid)
        {
            bool success = true;
            string message = "success";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    var pi = en.TSMSGroup.First(f => f.SchoolID == schoolID && f.SMSGroupID == gid);
                    if (pi != null)
                    {
                        pi.Status = pi.Status == 1 ? 0 : 1;
                        pi.UpdatedBy = userData.UserID;
                        pi.UpdatedDate = DateTime.Now;

                        en.SaveChanges();

                        //database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลนักเรียน " + pi.sName + " " + pi.sLastname, HttpContext.Current.Request, 2, 4, 0, schoolID);
                    }
                    else
                    {
                        success = false;
                        message += "ไม่พบข้อมูลกลุ่มผู้รับ";
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message += err.Message;
                }
            }

            return JsonConvert.SerializeObject(new { success, message });
        }

    }
}