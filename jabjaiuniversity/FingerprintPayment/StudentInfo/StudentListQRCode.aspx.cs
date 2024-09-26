using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public partial class StudentListQRCode : StudentGateway
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

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                }
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadStudent()
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
                case "1": sortBy = "Code"; break;
                case "2": sortBy = "Title"; break;
                case "3": sortBy = "Name"; break;
                case "4": sortBy = "Lastname"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string userType = Convert.ToString(jsonObject["userType"]);
            string level = Convert.ToString(jsonObject["level"]);
            string className = Convert.ToString(jsonObject["className"]);
            string stdName = Convert.ToString(jsonObject["stdName"]);

            var json = QueryEngine.LoadStudentPrintQRCodeJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, userType, level, className, stdName);

            return json;
        }

        [WebMethod]
        public static string[] GetStudentName(string keyword, string userType)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string sqlQuery = "";

                switch (userType)
                {
                    case "0":
                        sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname
FROM TUser
WHERE cDel IS NULL AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%')
GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, keyword);
                        break;
                    case "1":
                        sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname
FROM TEmployees e 
LEFT JOIN TEmployeeInfo i ON e.sEmp = i.sEmp AND e.SchoolID = i.SchoolID
WHERE e.cDel IS NULL AND e.SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR i.Code LIKE N'%{1}%')
GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, keyword);
                        break;
                }

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
	WHERE t.nTSubLevel = {0} AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1 AND t.SchoolID = {1}
) r
ORDER BY r.sort1, r.sort2", slID, schoolID);
                    result = dbschool.Database.SqlQuery<EntityDropdown>(query).ToList();
                }
            }

            return result;
        }

        [WebMethod]
        public static object GenerateQrCode(int sid)
        {
            object result = null;

            string liffID = "1653630518-y03N0qao";// 1653527349-LRexneZx //1653630518-y03N0qao

            try
            {
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    int schoolID = GetUserData().CompanyID;
                    string code = string.Format(@"{0}-{1}", schoolID, sid);
                    //string url = string.Format(@"https://liff.line.me/{0}?ssid={1}", liffID, Class.EncryptionHelper.Encrypt(code));
                    string url = string.Format(@"line://app/{0}/?ssid={1}", liffID, Class.EncryptionHelper.Encrypt(code));
                    string imgBase64 = QRCodeFunction.Create(url, 250, 250);

                    result = new { qrcode = imgBase64 };
                }
            }
            catch (Exception error)
            {
                result = "error";
            }

            return result;
        }

    }
}