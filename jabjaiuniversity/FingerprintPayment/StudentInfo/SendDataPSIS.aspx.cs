using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class SendDataPSIS : StudentGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();

            //HttpContext.Current.Session["SchoolCodePSIS"] = "1194100191"; // SchoolID : 208
            //HttpContext.Current.Session["SchoolCodePSIS"] = "1110100810"; // SchoolID : 875
            //HttpContext.Current.Session["SchoolCodePSIS"] = "1170100030"; // SchoolID : 921
            //HttpContext.Current.Session["SchoolCodePSIS"] = "1134100030"; // SchoolID : 870
            //HttpContext.Current.Session["SchoolCodePSIS"] = "1120100059"; // SchoolID : 84
            //HttpContext.Current.Session["SchoolCodePSIS"] = "1130100085"; // SchoolID : 909
            //HttpContext.Current.Session["SchoolCodePSIS"] = "1130100075"; // SchoolID : 1093
            //HttpContext.Current.Session["SchoolCodePSIS"] = "1183100024"; // SchoolID : 12
            //HttpContext.Current.Session["SchoolCodePSIS"] = "1120100048"; // SchoolID : 1077

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
                case "1": sortBy = "[No], Code"; break;
                case "2": sortBy = "Title"; break;
                case "3": sortBy = "Name"; break;
                case "4": sortBy = "Lastname"; break;
                case "5": sortBy = "ClassName"; break;
                case "6": sortBy = "Status"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string level = Convert.ToString(jsonObject["level"]);
            string className = Convert.ToString(jsonObject["className"]);
            string stdName = Convert.ToString(jsonObject["stdName"]);

            var json = QueryEngine.LoadStudentForAPIJsonData(draw, pageIndex, pageSize, sortBy, level, className, stdName, GetUserData());

            return json;
        }

        [WebMethod]
        public static string[] GetStudentName(string keyword)
        {
            int schoolID = GetUserData().CompanyID;
            JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

            string sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname
FROM TUser
WHERE cDel IS NULL AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%')
GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, keyword);
            List<string> result = dbschool.Database.SqlQuery<string>(sqlQuery).ToList();

            return result.ToArray();
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadTerm(string yearID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(yearID))
            {
                int schoolID = GetUserData().CompanyID;
                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

                int yID = Convert.ToInt32(yearID);

                result = dbschool.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yID && w.cDel == null).OrderByDescending(o => o.nTerm).Select(s => new EntityDropdown { id = s.nTerm, name = s.sTerm }).ToList();
            }

            return result;
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadTermSubLevel2(string subLevelID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(subLevelID))
            {
                int schoolID = GetUserData().CompanyID;
                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

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

            return result;
        }

        [WebMethod(EnableSession = true)]
        public static object SendDataToCentral(List<StudentData> studentDatas)
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";

            int iSuccess = 0;
            int iUnsuccess = 0;

            //int iSuccess2 = 0;
            //int iUnsuccess2 = 0;

            if (HttpContext.Current.Session["SchoolCodePSIS"] != null)
            {
                JWTToken.userData userData = GetUserData();
                int schoolID = userData.CompanyID;

                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                  
                    try
                    {
                        var titleLists = en.TTitleLists.Where(w => w.SchoolID == schoolID).ToList();
                        var districts = dbMaster.districts.ToList();
                        var termSubLevel2s = en.TTermSubLevel2.Where(w => w.SchoolID == schoolID).ToList();
                        var subLevels = en.TSubLevels.Where(w => w.SchoolID == schoolID).ToList();

                        foreach (var sd in studentDatas)
                        {
                            JabjaiEntity.DB.TUser user = en.TUser.Where(w => w.sID == sd.sid).FirstOrDefault();
                            TTitleList titleList = titleLists.Where(w => w.SchoolID == schoolID && w.nTitleid.ToString() == user.sStudentTitle).FirstOrDefault();
                            TFamilyProfile familyProfile = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == user.sID).FirstOrDefault();
                            
                            TTitleList fatherTitleObj = titleLists.Where(w => w.SchoolID == schoolID && w.nTitleid.ToString() == familyProfile.sFatherTitle).FirstOrDefault();
                            TTitleList motherTitleObj = titleLists.Where(w => w.SchoolID == schoolID && w.nTitleid.ToString() == familyProfile.sMotherTitle).FirstOrDefault();

                            district district = districts.Where(w => w.DISTRICT_ID == familyProfile.houseRegistrationTumbon).FirstOrDefault();
                            string districtCode = "";
                            if (district != null)
                            {
                                districtCode = district.DISTRICT_CODE;
                            }

                            TTermSubLevel2 termSubLevel2 = termSubLevel2s.Where(w => w.SchoolID == schoolID && w.nTermSubLevel2 == user.nTermSubLevel2).FirstOrDefault();
                            TSubLevel subLevel = subLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == termSubLevel2.nTSubLevel).FirstOrDefault();

                            // เลขประจำตัวประชาชน|เลขประจำตัวนักเรียน|รหัสคำนำหน้า|เพศ|ชื่อจริง|นามสกุล|วันเดือนปีที่เกิด|รหัสสัญชาติ|รหัสเชื้อชาติ|รหัสศาสนา|*10*รหัสประจำบ้าน|เลขที่บ้าน|หมู่ที่|ซอย|ถนน|รหัสตำบล|รหัสไปรษณีย์|หมายเลขโทรศัพท์|อีเมล์|รหัสความพิการ|*20*วันที่เข้าเรียน|รหัสระดับชั้น|รหัสความด้อยโอกาส|*23*เลขประจำตัวประชาชนบิดา|รหัสคำหน้านามบิดา|ชื่อบิดา|นามสกุลบิดา|FatherFirstname|FatherSurname|เลขประจำตัวประชาชนมารดา|รหัสคำหน้านามมารดา|ชื่อมารดา|*32*นามสกุลมารดา|MotherFirstname|MotherSurname
                            var bodyData = string.Format(@"{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}|{22}|{23}|{24}|{25}|{26}|{27}|{28}|{29}|{30}|{31}|{32}|{33}|{34}",
                                /*0*/ user.sIdentification.Replace("-", "").Trim(), user.sStudentID, titleList?.MasterCode, (user.cSex == "0" ? "1" : "2"), user.sName, user.sLastname, user.dBirth?.ToString("yyyyMMdd", new CultureInfo("th-TH")), user.sStudentNation, user.sStudentRace, user.sStudentReligion,
                                /*10*/ user.sStudentHomeRegisterCode, familyProfile.houseRegistrationNumber, (string.IsNullOrEmpty(familyProfile.houseRegistrationMuu) || familyProfile.houseRegistrationMuu.Length > 2 ? "" : familyProfile.houseRegistrationMuu), familyProfile.houseRegistrationSoy, familyProfile.houseRegistrationRoad, districtCode, familyProfile.houseRegistrationPost, user.sPhone, user.sEmail, string.IsNullOrEmpty(user.DisabilityCode) ? "99" : user.DisabilityCode,
                                /*20*/ user.moveInDate?.ToString("yyyyMMdd", new CultureInfo("th-TH")), subLevel?.MasterCode, string.IsNullOrEmpty(user.DisadvantageCode) ? "99" : user.DisadvantageCode,
                                /*23*/ familyProfile.sFatherIdCardNumber, fatherTitleObj?.MasterCode, familyProfile.sFatherFirstName, familyProfile.sFatherLastName, familyProfile.sFatherNameEN, familyProfile.sFatherLastEN, familyProfile.sMotherIdCardNumber, motherTitleObj?.MasterCode, familyProfile.sMotherFirstName,
                                /*32*/ familyProfile.sMotherLastName, familyProfile.sMotherNameEN, familyProfile.sMotherLastEN);

                            // Replace single quotes
                            bodyData = bodyData.Replace("'", "''");

                            string yearBE = "2563";
                            TYear year = en.TYears.Where(w => w.SchoolID == schoolID && w.nYear == sd.yid).FirstOrDefault();
                            yearBE = year.numberYear?.ToString();

                            var res = SaveStudentOnline(HttpContext.Current.Session["SchoolCodePSIS"].ToString(), yearBE, bodyData);

                            if (res.Status == "Success")
                            {
                                int c = en.TSendDataPSIS.Where(w => w.SchoolID == schoolID && w.nYear == sd.yid && w.sID == sd.sid).Count();
                                if (c == 0)
                                {
                                    TSendDataPSI s = new TSendDataPSI
                                    {
                                        SchoolID = schoolID,
                                        nYear = sd.yid,
                                        sID = sd.sid,
                                        SendDate = DateTime.Now,
                                        StatusCode = res.StatusCode,
                                        ResponseContent = res.ResponseContent
                                    };

                                    en.TSendDataPSIS.Add(s);
                                }
                                else
                                {
                                    TSendDataPSI si = en.TSendDataPSIS.First(f => f.SchoolID == schoolID && f.nYear == sd.yid && f.sID == sd.sid);

                                    si.SendDate = DateTime.Now;
                                    si.StatusCode = res.StatusCode;
                                    si.ResponseContent = res.ResponseContent;
                                }
                                en.SaveChanges();

                                database.InsertLog(userData.UserID.ToString(), "ส่งข้อมูลนักเรียนเข้าศูนย์กลาง: " + sd.sid + ", รหัสปีการศึกษา: " + sd.yid, HttpContext.Current.Request, 174, 2, 0, schoolID);
                            }
                            else
                            {
                                Class.ComFunction.InsertLogDebug(schoolID, null, userData.UserID, res.JsonError);
                            }

                            switch (res.StatusCode)
                            {
                                case 201: iSuccess++; break;
                                case 400:
                                case 401: iUnsuccess++; break;
                            }

                            //// Save Student Family
                            //TTitleList fatherTitleObj = titleLists.Where(w => w.SchoolID == schoolID && w.nTitleid.ToString() == familyProfile.sFatherTitle).FirstOrDefault();
                            //TTitleList motherTitleObj = titleLists.Where(w => w.SchoolID == schoolID && w.nTitleid.ToString() == familyProfile.sMotherTitle).FirstOrDefault();

                            //// เลขประจำตัวประชาชนนักเรียน|เลขประจำตัวประชาชนบิดา|รหัสคำหน้านามบิดา|ชื่อบิดา|นามสกุลบิดา|FatherFirstname|FatherSurname|เลขประจำตัวประชาชนมารดา|รหัสคำหน้านามมารดา|ชื่อมารดา|นามสกุลมารดา|MotherFirstname|MotherSurname
                            //bodyData = string.Format(@"{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}",
                            //    /*0*/ user.sIdentification, familyProfile.sFatherIdCardNumber, fatherTitleObj.MasterCode, familyProfile.sFatherFirstName, familyProfile.sFatherLastName, familyProfile.sFatherNameEN, familyProfile.sFatherLastEN, familyProfile.sMotherIdCardNumber, motherTitleObj.MasterCode, familyProfile.sMotherFirstName,
                            //    /*10*/ familyProfile.sMotherLastName, familyProfile.sMotherNameEN, familyProfile.sMotherLastEN);

                            //var res2 = SaveStudentFamilyOnline(HttpContext.Current.Session["SchoolCodePSIS"].ToString(), bodyData);

                            //if (res2.Status == "Success")
                            //{
                            //    int c = en.TSendDataPSIS.Where(w => w.SchoolID == schoolID && w.nYear == sd.yid && w.sID == sd.sid).Count();
                            //    if (c == 0)
                            //    {
                            //        TSendDataPSI s = new TSendDataPSI
                            //        {
                            //            SchoolID = schoolID,
                            //            nYear = sd.yid,
                            //            sID = sd.sid,
                            //            SendDate2 = DateTime.Now,
                            //            StatusCode2 = res2.StatusCode,
                            //            ResponseContent2 = res2.ResponseContent
                            //        };

                            //        en.TSendDataPSIS.Add(s);
                            //    }
                            //    else
                            //    {
                            //        TSendDataPSI si = en.TSendDataPSIS.First(f => f.SchoolID == schoolID && f.nYear == sd.yid && f.sID == sd.sid);

                            //        si.SendDate2 = DateTime.Now;
                            //        si.StatusCode2 = res2.StatusCode;
                            //        si.ResponseContent2 = res2.ResponseContent;
                            //    }
                            //    en.SaveChanges();

                            //    database.InsertLog(userData.UserID.ToString(), "ส่งข้อมูลนักเรียน(ผู้ปกครอง)เข้าศูนย์กลาง: " + sd.sid + ", รหัสปีการศึกษา: " + sd.yid, HttpContext.Current.Request, 174, 2, 0, schoolID);
                            //}
                            //else
                            //{
                            //    Class.ComFunction.InsertLogDebug(schoolID, null, userData.UserID, res2.JsonError);
                            //}

                            //switch (res2.StatusCode)
                            //{
                            //    case 201: iSuccess2++; break;
                            //    case 400:
                            //    case 401: iUnsuccess2++; break;
                            //}
                        }
                    }
                    catch (Exception error)
                    {
                        success = false;
                        statusCode = 500;
                        message = error.Message;
                    }
                }
            }
            else
            {
                success = false;
                statusCode = 401; // Unauthorized
            }

            //var result = new { success, statusCode, message, log = new { success = iSuccess, unsuccess = iUnsuccess }, log2 = new { success = iSuccess2, unsuccess = iUnsuccess2 } };
            var result = new { success, statusCode, message, log = new { success = iSuccess, unsuccess = iUnsuccess } };

            return JsonConvert.SerializeObject(result);
        }

        public class StudentData
        {
            public int sid { get; set; }
            public int yid { get; set; }
        }

        public static ResponseSaveStudentOnline SaveStudentOnline(string schoolCode, string year, string bodyData)
        {
            var responseSaveStudentOnline = new ResponseSaveStudentOnline();

            string responseFromServer = "";

            // เลขประจำตัวประชาชน|เลขประจำตัวนักเรียน|รหัสคำนำหน้า|เพศ|ชื่อจริง|นามสกุล|วันเดือนปีที่เกิด|รหัสสัญชาติ|รหัสเชื้อชาติ|รหัสศาสนา|*10*รหัสประจำบ้าน|เลขที่บ้าน|หมู่ที่|ซอย|ถนน|รหัสตำบล|รหัสไปรษณีย์|หมายเลขโทรศัพท์|อีเมล์|รหัสความพิการ|*20*วันที่เข้าเรียน|รหัสระดับชั้น|รหัสความด้อยโอกาส|*23*เลขประจำตัวประชาชนบิดา|รหัสคำหน้านามบิดา|ชื่อบิดา|นามสกุลบิดา|FatherFirstname|FatherSurname|เลขประจำตัวประชาชนมารดา|รหัสคำหน้านามมารดา|ชื่อมารดา|*32*นามสกุลมารดา|MotherFirstname|MotherSurname
            // bodyData = "1209000982967|63000005|001|1|ปวริศ|หนาซุย|25600126|099|099|101||10/39|5|7|เนินพลับหวาน|200401|20260|||99|25630507|111|99|";
            // bodyData = "1709800494164|30709|003|1|ปวีณ์กร|ศรีสุข|25490702|099|099|101|70050065611|37|7|-|-|700504|70110|0906039857|paweekorn2006@gmail.com|99|25650516|411|99|3700500712725|003|อนุศักดิ์|ศรีสุข|ANUSAK|Srisuk|3700500658313|004|ปิยะนารถ|ท้วมศิริ|Piyanart|Thuamsiri";

            try
            {
                var client = new RestClient("https://regis-api.opec.go.th/api/v1/SaveStudentOnline");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("X-Office-Request", "SCHOOLBRIGHT");
                request.AddHeader("X-Office-Key", "5EB064D9ABE79F301B17E508F8F3C201A5CF44BA");
                request.AddHeader("SchoolCode", schoolCode);
                request.AddHeader("AcademicYear", year);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("Students", bodyData);
                IRestResponse response = client.Execute(request);

                responseFromServer = response.Content;

                responseSaveStudentOnline = JsonConvert.DeserializeObject<ResponseSaveStudentOnline>(responseFromServer);

                responseSaveStudentOnline.ResponseContent = responseFromServer;
                HttpStatusCode statusCode = response.StatusCode;
                responseSaveStudentOnline.StatusCode = (int)statusCode;
                responseSaveStudentOnline.Status = "Success";
            }
            catch (Exception ex)
            {
                responseSaveStudentOnline.Status = "Error";
                responseSaveStudentOnline.JsonError = string.Format(@"{{""Message"" : ""{0}"", ""StackTrace"" : ""{1}"", ""Source"" : ""{2}"", ""BodyData"" : {3}, ""responseData"" : {4}}}", ex.Message, ex.StackTrace, ex.Source, bodyData, responseFromServer);
            }

            return responseSaveStudentOnline;
        }

        public static ResponseSaveStudentOnline SaveStudentFamilyOnline(string schoolCode, string bodyData) {
            var responseSaveStudentOnline = new ResponseSaveStudentOnline();

            string responseFromServer = "";

            // เลขประจำตัวประชาชนนักเรียน|เลขประจำตัวประชาชนบิดา|รหัสคำหน้านามบิดา|ชื่อบิดา|นามสกุลบิดา|FatherFirstname|FatherSurname|เลขประจำตัวประชาชนมารดา|รหัสคำหน้านามมารดา|ชื่อมารดา|นามสกุลมารดา|MotherFirstname|MotherSurname
            // bodyData = "1709800661486|3710500631377|003|สุมล|อุดมกิจกุล|sumon|udomkijkul|1571100004118|004|เกษมศรี|ไขปัญญา|kasemsri|khaipunya";

            try
            {
                var client = new RestClient("https://regis-api.opec.go.th/api/v1/SaveFamily");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("X-Office-Request", "SCHOOLBRIGHT");
                request.AddHeader("X-Office-Key", "5EB064D9ABE79F301B17E508F8F3C201A5CF44BA");
                request.AddHeader("SchoolCode", schoolCode);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("Students", bodyData);
                IRestResponse response = client.Execute(request);

                responseFromServer = response.Content;

                responseSaveStudentOnline = JsonConvert.DeserializeObject<ResponseSaveStudentOnline>(responseFromServer);

                responseSaveStudentOnline.ResponseContent = responseFromServer;
                HttpStatusCode statusCode = response.StatusCode;
                responseSaveStudentOnline.StatusCode = (int)statusCode;
                responseSaveStudentOnline.Status = "Success";
            }
            catch (Exception ex)
            {
                responseSaveStudentOnline.Status = "Error";
                responseSaveStudentOnline.JsonError = string.Format(@"{{""Message"" : ""{0}"", ""StackTrace"" : ""{1}"", ""Source"" : ""{2}"", ""BodyData"" : {3}, ""responseData"" : {4}}}", ex.Message, ex.StackTrace, ex.Source, bodyData, responseFromServer);
            }

            return responseSaveStudentOnline;

        }
        public class ResponseSaveStudentOnline
        {
            [JsonProperty(PropertyName = "message")]
            public string Message { get; set; }

            [JsonProperty(PropertyName = "errors")]
            public object Errors { get; set; }

            public string ResponseContent { get; set; }
            public int StatusCode { get; set; }
            public string Status { get; set; }
            public string JsonError { get; set; }
        }

        public class Errors
        {
            [JsonProperty(PropertyName = "message")]
            public string Message { get; set; }

            [JsonProperty(PropertyName = "recordOrder")]
            public string RecordOrder { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static object LoginPSIS(LoginData loginData)
        {
            string result = "";
            string message = "";

            try
            {
                var client = new RestClient(string.Format(@"https://regis-api.opec.go.th/api/v1/GetLogin/{0}/{1}", loginData.username, loginData.password));
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("X-Office-Request", "SCHOOLBRIGHT");
                request.AddHeader("X-Office-Key", "5EB064D9ABE79F301B17E508F8F3C201A5CF44BA");
                IRestResponse response = client.Execute(request);

                string responseFromServer = response.Content;

                if (!string.IsNullOrEmpty(responseFromServer))
                {
                    var responseGetLogin = JsonConvert.DeserializeObject<ResponseGetLogin>(responseFromServer);
                    if (!string.IsNullOrEmpty(responseGetLogin.SchoolCode))
                    {
                        result = "success";

                        // Set school code in session
                        HttpContext.Current.Session["SchoolCodePSIS"] = responseGetLogin.SchoolCode;
                    }
                    else
                    {
                        result = "error";
                        message = "ท่านระบุชื่อผู้ใช้/รหัสผ่านไม่ถูกต้อง กรุณาตรวจสอบอีกครั้ง";
                    }
                }
                else
                {
                    result = "error";
                    message = "ท่านระบุชื่อผู้ใช้/รหัสผ่านไม่ถูกต้อง กรุณาตรวจสอบอีกครั้ง";
                }
            }
            catch (Exception error)
            {
                result = "error";
                message = error.Message;
            }

            return JsonConvert.SerializeObject(new { result, message });
        }

        public class LoginData
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class ResponseGetLogin
        {
            [JsonProperty(PropertyName = "firstname")]
            public string Firstname { get; set; }

            [JsonProperty(PropertyName = "lastname")]
            public string Lastname { get; set; }

            [JsonProperty(PropertyName = "schoolCode")]
            public string SchoolCode { get; set; }

            [JsonProperty(PropertyName = "schoolName")]
            public string SchoolName { get; set; }

            [JsonProperty(PropertyName = "username")]
            public string Username { get; set; }

            [JsonProperty(PropertyName = "officeName")]
            public string OfficeName { get; set; }

        }

    }
}