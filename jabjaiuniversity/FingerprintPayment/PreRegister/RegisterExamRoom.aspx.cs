using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterExamRoom : PreRegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string query = string.Format(@"
SELECT r.RegisterPlanSetupID 'PlanSetupID', 'ชั้น: ' + s.SubLevel + ', รหัสแผน: ' + ISNULL(r.PlanCode, '-') + ', ชื่อแผน: ' + r.PlanName 'PlanSetupName'
FROM TRegisterPlanSetup r 
LEFT JOIN TSubLevel s ON r.nTSubLevel = s.nTSubLevel AND r.SchoolID = s.SchoolID
WHERE 1 = 1 AND r.cDel = 0 AND r.SchoolID = {0}
GROUP BY r.RegisterPlanSetupID, r.nTSubLevel, s.SubLevel, r.PlanName, r.PlanCode
ORDER BY s.SubLevel, r.PlanCode, r.RegisterPlanSetupID", schoolID);
                List<EntityPlan> listPlan = en.Database.SqlQuery<EntityPlan>(query).ToList();

                foreach (var l in listPlan)
                {
                    this.ltrPlanSearch.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.PlanSetupID, l.PlanSetupName);
                }

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                }
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadRegisterExamRoom()
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

            string sortBy = "RegisterExamRoomID";
            switch (sortIndex)
            {
                case "1": sortBy = "PlanName"; break;
                case "2": sortBy = "ExamRoomName"; break;
                case "3": sortBy = "Seats"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string plan = Convert.ToString(jsonObject["plan"]);
            string examRoomName = Convert.ToString(jsonObject["examRoomName"]);

            var json = QueryEngine.LoadRegisterExamRoomJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, plan, examRoomName);

            return json;
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadPlan(string subLevelID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(subLevelID))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    string query = string.Format(@"
SELECT CAST(r.RegisterPlanSetupID AS VARCHAR(10)) 'id', r.PlanName 'name'
FROM TRegisterPlanSetup r 
LEFT JOIN TSubLevel s ON r.nTSubLevel = s.nTSubLevel AND r.SchoolID = s.SchoolID
WHERE 1 = 1 AND r.cDel = 0 AND r.SchoolID = {0} AND r.nTSubLevel = {1}
GROUP BY r.RegisterPlanSetupID, r.nTSubLevel, s.SubLevel, r.PlanName, r.PlanCode
ORDER BY s.SubLevel, r.PlanCode, r.RegisterPlanSetupID", schoolID, subLevelID);
                    result = dbSchool.Database.SqlQuery<EntityDropdown>(query).ToList();
                }
            }

            return result;
        }

        [WebMethod]
        public static object GetRegisterExamRoomData(int erid)
        {
            bool success = true;
            string code = "200";
            string message = "Success to delete data.";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            TRegisterExamRoom examRoom = new TRegisterExamRoom();

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    examRoom = en.TRegisterExamRooms.Where(w => w.SchoolID == schoolID && w.RegisterExamRoomID == erid).FirstOrDefault();
                }
                catch (Exception err)
                {
                    success = false;
                    code = "500";
                    message = err.Message;
                }
            }

            var result = new { success, code, message, data = examRoom };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object RemoveItem(int erid)
        {
            bool success = true;
            string code = "200";
            string message = "Success to delete data.";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    // Check delete exam room, no delete if use exam room in generate seat no
                    bool existUsageExamRoom = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.cDel == null && w.RegisterExamRoomID == erid).Count() > 0;
                    if (!existUsageExamRoom)
                    {
                        TRegisterExamRoom examRoom = en.TRegisterExamRooms.Where(w => w.SchoolID == schoolID && w.RegisterExamRoomID == erid).FirstOrDefault();
                        if (examRoom != null)
                        {
                            examRoom.IsDel = true;
                            examRoom.UpdateDate = DateTime.Now;
                            examRoom.UpdateBy = userData.UserID;

                            en.SaveChanges();
                        }
                        else
                        {
                            success = false;
                            code = "501";
                            message = "Data not found";
                        }
                    }
                    else
                    {
                        success = false;
                        code = "501";
                        message = "ห้องสอบถูกนำไปใช้ในการสร้างเลขที่นั่งสอบแล้ว";
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    code = "500";
                    message = err.Message;
                }
            }

            var result = new { success, code, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static object SaveData(RegisterExamRoomData registerExamRoomData)
        {
            bool success = true;
            string code = "200";
            string message = "Success to save data.";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    if (registerExamRoomData.ExamRoomID == 0)
                    {
                        TRegisterExamRoom examRoom = new TRegisterExamRoom
                        {
                            nTSubLevel = registerExamRoomData.LevelID,
                            RegisterPlanSetupID = registerExamRoomData.PlanID,
                            ExamRoomName = registerExamRoomData.ExamRoomName,
                            Seats = registerExamRoomData.Seats,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now,
                            UpdateBy = userData.UserID,
                            IsDel = false
                        };
                        en.TRegisterExamRooms.Add(examRoom);

                        en.SaveChanges();
                    }
                    else
                    {
                        TRegisterExamRoom examRoom = en.TRegisterExamRooms.Where(w => w.SchoolID == schoolID && w.RegisterExamRoomID == registerExamRoomData.ExamRoomID).FirstOrDefault();
                        if (examRoom != null)
                        {
                            examRoom.ExamRoomName = registerExamRoomData.ExamRoomName;
                            examRoom.Seats = registerExamRoomData.Seats;
                            examRoom.UpdateDate = DateTime.Now;
                            examRoom.UpdateBy = userData.UserID;

                            en.SaveChanges();
                        }
                        else
                        {
                            success = false;
                            code = "501";
                            message = "Data not found";
                        }
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    code = "500";
                    message = err.Message;
                }
            }

            var result = new { success, code, message };

            return JsonConvert.SerializeObject(result);
        }
    }

    public class EntityPlan
    {
        public int PlanSetupID { get; set; }
        public string PlanSetupName { get; set; }
    }

    public class RegisterExamRoomData
    {
        [JsonProperty(PropertyName = "examRoomId")]
        public int ExamRoomID { get; set; }

        [JsonProperty(PropertyName = "levelId")]
        public int LevelID { get; set; }

        [JsonProperty(PropertyName = "planId")]
        public int PlanID { get; set; }

        [JsonProperty(PropertyName = "examRoomName")]
        public string ExamRoomName { get; set; }

        [JsonProperty(PropertyName = "seats")]
        public int Seats { get; set; }
    }

}