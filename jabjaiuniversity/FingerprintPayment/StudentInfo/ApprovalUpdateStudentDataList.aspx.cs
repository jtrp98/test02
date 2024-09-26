using FingerprintPayment.StudentInfo.CsCode;
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

namespace FingerprintPayment.StudentInfo
{
    public partial class ApprovalUpdateStudentDataList : StudentGateway
    {
        protected string CurrentTermID = "";

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
                // Get current year
                StudentLogic studentLogic = new StudentLogic(en);
                CurrentTermID = studentLogic.GetTermId(UserData);

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                }
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadRequestUpdateStudent()
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

            string sortBy = "sID";
            switch (sortIndex)
            {
                case "1": sortBy = "StudentCode"; break;
                case "2": sortBy = "Classroom"; break;
                case "3": sortBy = "Name"; break;
                case "4": sortBy = "Lastname"; break;
                case "5": sortBy = "RequestDate"; break;
                case "6": sortBy = "ApproveDate"; break;
                case "7": sortBy = "ApproveStatus"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string level = Convert.ToString(jsonObject["level"]);
            string classId = Convert.ToString(jsonObject["classId"]);
            string studentName = Convert.ToString(jsonObject["studentName"]);

            var json = QueryEngine.LoadRequestUpdateStudentJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, level, classId, studentName);

            return json;
        }

        [WebMethod]
        public static string[] GetStudentName(string keyword, string termID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname
FROM TB_StudentViews
WHERE (cDel IS NULL OR cDel = 'G') AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%') AND nTerm='{2}'
GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, keyword, termID);
                List<string> result = dbschool.Database.SqlQuery<string>(sqlQuery).ToList();

                return result.ToArray();
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadTermSubLevel2(string subLevelID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(subLevelID))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {

                    int slID = Convert.ToInt32(subLevelID);

                    string query = string.Format(@"
SELECT r.id, r.name
FROM
(
	SELECT CAST(t.nTermSubLevel2 AS VARCHAR(10)) 'id', s.SubLevel + ' / ' + t.nTSubLevel2 'name', s.SubLevel 'sort1', (CASE WHEN ISNUMERIC(t.nTSubLevel2) = 1 THEN RIGHT('0000' + t.nTSubLevel2, 5) ELSE t.nTSubLevel2 END) 'sort2'
	FROM TTermSubLevel2 t 
	LEFT JOIN TSubLevel s ON t.nTSubLevel = s.nTSubLevel 
	WHERE t.SchoolID = {0} AND t.nTSubLevel = {1} AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1
) r
ORDER BY r.sort1, r.sort2", schoolID, slID);
                    result = dbschool.Database.SqlQuery<EntityDropdown>(query).ToList();
                }
            }
            return result;
        }

        [WebMethod]
        public static object GetApprovalConfig()
        {
            bool success = true;
            string code = "200";
            string message = "Get data success.";

            var data = new ConfigData();

            int schoolID = GetUserData().CompanyID;
            using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
            {
                try
                {
                    TSystemSetting systemSetting = mctx.TSystemSetting.Where(w => w.SchoolID == schoolID).FirstOrDefault();
                    if (systemSetting != null)
                    {
                        data.IsOpenApproveUserProfile = systemSetting.IsOpenApproveUserProfile;
                        data.ApproveOption = systemSetting.ApproveOption;
                        data.ApproveStartDate = systemSetting.ApproveStartDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        data.ApproveEndDate = systemSetting.ApproveEndDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    code = "500";
                    message = err.Message;
                }
            }

            return JsonConvert.SerializeObject(new { success, code, message, data });
        }

        [WebMethod]
        public static object SaveApprovalConfig(ConfigData configData)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                {
                    TSystemSetting systemSetting = mctx.TSystemSetting.Where(w => w.SchoolID == schoolID).FirstOrDefault();
                    if (systemSetting != null)
                    {
                        systemSetting.IsOpenApproveUserProfile = configData.IsOpenApproveUserProfile;
                        systemSetting.ApproveOption = configData.ApproveOption;
                        systemSetting.ApproveStartDate = string.IsNullOrEmpty(configData.ApproveStartDate) ? (DateTime?)null : DateTime.Parse(configData.ApproveStartDate, new CultureInfo("th-TH"));
                        systemSetting.ApproveEndDate = string.IsNullOrEmpty(configData.ApproveEndDate) ? (DateTime?)null : DateTime.Parse(configData.ApproveEndDate, new CultureInfo("th-TH"));
                    }

                    mctx.SaveChanges();
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        public class ConfigData
        {
            [JsonProperty(PropertyName = "isOpenApproveUserProfile")]
            public bool? IsOpenApproveUserProfile { get; set; }

            [JsonProperty(PropertyName = "approveOption")]
            public int? ApproveOption { get; set; }

            [JsonProperty(PropertyName = "approveStartDate")]
            public string ApproveStartDate { get; set; }

            [JsonProperty(PropertyName = "approveEndDate")]
            public string ApproveEndDate { get; set; }
        }
    }
}