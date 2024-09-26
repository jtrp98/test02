using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using Npgsql;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using QueryEngine = FingerprintPayment.StudentInfo.CsCode.QueryEngine;

namespace FingerprintPayment.StudentInfo
{
    public partial class StudentList : StudentGateway
    {
        protected string YearTermData = "";
        protected string FunctionDeleteAllStudentData = "";
        protected bool HaveNewResetPasswordPermission = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            int userID = UserData.UserID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                // Get current year
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTerm = studentLogic.GetTermId(UserData);
                int currentYearID = 0;
                var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
                if (term != null)
                {
                    currentYearID = term.nYear.Value;
                }

                var listYear = en.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false).OrderByDescending(x => x.numberYear).ToList();
                foreach (var l in listYear)
                {
                    if (l.nYear == currentYearID)
                    {
                        this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }
                    else
                    {
                        this.ltrYear.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }

                    if (currentYearID == 0) currentYearID = l.nYear;
                }

                if (currentYearID != 0)
                {
                    var listTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == currentYearID && w.cDel == null).OrderByDescending(o => o.nTerm).ToList();
                    foreach (var l in listTerm)
                    {
                        if (l.nTerm.Trim() == currentTerm)
                        {
                            this.ltrTerm.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                        }
                        else
                        {
                            this.ltrTerm.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                        }
                    }
                }

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).OrderBy(o => o.MasterCode).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                }
                this.ltrSortLevel.Text = this.ltrTitleLevel.Text = this.ltrLevel.Text;

                var numberYear = 0;
                var yearObj = listYear.Where(w => w.nYear == currentYearID).FirstOrDefault();
                if (yearObj != null)
                {
                    numberYear = (int)yearObj.numberYear;
                }

                string query = string.Format(@"
SELECT CAST(ROW_NUMBER() OVER(ORDER BY y.numberYear, t.sTerm) AS INT) 'No', y.nYear 'YearID', y.numberYear 'YearNumber', t.nTerm 'TermID', t.sTerm 'Term', CAST(IIF(y.nYear = {2}, 1, 0) AS BIT) 'CurrentYear', CAST(IIF(t.nTerm = '{3}', 1, 0) AS BIT) 'CurrentTerm'
FROM TTerm t 
LEFT JOIN TYear y ON t.SchoolID=y.SchoolID AND t.nYear=y.nYear 
WHERE t.SchoolID={0} AND t.cDel IS NULL AND y.cDel=0 AND y.numberYear >= {1}", schoolID, numberYear, currentYearID, currentTerm);
                List<YearTermModel> yearTermModels = en.Database.SqlQuery<YearTermModel>(query).ToList();

                YearTermData = JsonConvert.SerializeObject(yearTermModels);

                YearTermData = YearTermData.Remove(YearTermData.Length - 1, 1).Remove(0, 1);

                // Check reset password permission
                query = $@"SELECT CAST(IIF(COUNT(*) = 1 AND MAX(A.Role) = 0, 1, 0) AS BIT) IsPermission
--SELECT A.MenuID, A.Role --Modify = 0, View = 1, NoPermission = 2
FROM [JabjaiMasterSingleDB].[dbo].[TGroupPermissionMenu] A
JOIN [JabjaiMasterSingleDB].[dbo].[TGroupPermissionUser] B ON A.SchoolID = B.SchoolID AND A.GroupID = B.GroupID
WHERE B.IsActive = 1 AND A.IsActive = 1 AND A.MenuID = 355 and B.UserID = {userID}";
                HaveNewResetPasswordPermission = en.Database.SqlQuery<bool>(query).FirstOrDefault();
                HaveNewResetPasswordPermission |= UserData.IsAdmin;
            }

            if (schoolID == 936)
            {
                // Only SB Academy school
                FunctionDeleteAllStudentData = $@"<li><a href=""#"" onclick=""return ClearAllStudentData();"">ลบข้อมูลนักเรียนทั้งหมด</a></li>";
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
                case "1": sortBy = "[No]"; break;
                case "2": sortBy = "RIGHT('0000000000' + Code, 10)"; break;
                //case "2": sortBy = "Code"; break;
                case "3": sortBy = "Title"; break;
                case "4": sortBy = "Name"; break;
                case "5": sortBy = "Lastname"; break;
                case "6": sortBy = "ClassName"; break;
                case "7": sortBy = "Status"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string year = Convert.ToString(jsonObject["year"]);
            string term = Convert.ToString(jsonObject["term"]);
            string level = Convert.ToString(jsonObject["level"]);
            string className = Convert.ToString(jsonObject["className"]);
            string stdName = Convert.ToString(jsonObject["stdName"]);

            var json = QueryEngine.LoadStudentJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, year, term, level, className, stdName);

            return json;
        }

        [WebMethod]
        public static object RemoveItem(int sid)
        {
            bool success = true;
            string message = "";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {



                try
                {
                    // School Data
                    var pi = en.TUser.First(f => f.SchoolID == schoolID && f.sID == sid);
                    if (pi != null)
                    {
                        // Check invoice data - ใบแจ้งหนี้ค้างชำระ(รวมที่ยังไม่ได้จ่ายด้วย)
                        string query = $@"
SELECT COUNT(*) 'Count'
FROM
(
	SELECT b.AccountInvoiceStudentId, b.StudentId, COUNT(*) 'Count'
	FROM [AccountingDB].dbo.AccountInvoice a INNER JOIN [AccountingDB].dbo.AccountInvoiceStudent b ON a.AccountInvoiceId = b.AccountInvoiceId
	WHERE ISNULL(a.DeleteDate, '') = '' AND ISNULL(b.DeleteDate, '') = '' AND b.Status <> 'Success' AND b.Status <> 'Void' AND b.StudentId = {sid}
	GROUP BY b.AccountInvoiceStudentId, b.StudentId
) A";
                        var existInvoice = en.Database.SqlQuery<int>(query).FirstOrDefault();
                        if (existInvoice > 0)
                        {
                            throw new Exception("[เนื่องจากมีใบแจ้งหนี้ค้างชำระในระบบ]<br/>");
                        }


                        // Check grade data
                        using (var _dbGrade = new PostgreSQL.PGGradeDBEntities("PGGradeDBEntities"))
                        using (var _dbGradeHistory = new PostgreSQL.PGGradeDBEntities("PGGradeDBHistoryContainer"))
                        {

                            // Define your parameters
                            var schoolIDParam = new NpgsqlParameter("@schoolID", schoolID);
                            var sidParam = new NpgsqlParameter("@sid", sid);

                            // SQL query to count records in PGTGradeDetails
                            string gradeCountQuery = string.Format(@"
                                            SELECT COUNT(*)
                                            FROM public.{0}PGTGradeDetail{0}
                                            WHERE {0}SchoolID{0} = @schoolID AND {0}sID{0} = @sid AND {0}cDel{0} = FALSE", '"');

                            // Execute the query and get the result
                            var gradeCount = _dbGrade.Database.SqlQuery<int>(gradeCountQuery, schoolIDParam, sidParam).Single();

                            // SQL query to count records in PGTGradeHistory (assuming a separate table)
                            string gradeHistoryCountQuery = string.Format(@"
                                            SELECT COUNT(*)
                                            FROM public.{0}PGTGradeDetailHistory{0}
                                            WHERE {0}SchoolID{0} = @schoolID AND {0}sID{0} = @sid AND {0}cDel{0} = FALSE", '"');

                            var gradeHistoryCount = _dbGradeHistory.Database.SqlQuery<int>(
                                                                           gradeHistoryCountQuery,
                                                                           new NpgsqlParameter("@schoolID", schoolID),
                                                                           new NpgsqlParameter("@sid", sid)
                                                                       ).Single();



                            if (gradeCount > 0 || gradeHistoryCount > 0)
                            {
                                throw new Exception("[เนื่องจากมีคะแนนในระบบ]<br/>");
                            }
                        }

                        // Check balance
                        if (pi.nMoney == 0 || pi.nMoney == null)
                        {
                            try
                            {
                                pi.cDel = "1";
                                pi.dUpdate = DateTime.Now.FixSecondAndMillisecond(2, 140);
                                pi.UpdatedBy = userData.UserID;
                                pi.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(2, 140);

                                en.SaveChanges();

                                database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลนักเรียน " + pi.sName + " " + pi.sLastname, HttpContext.Current.Request, 2, 4, 0, schoolID);

                                // Master Data
                                var masterUser = dbMaster.TUsers.Where(w => w.sID == sid && w.nCompany == schoolID && w.cType == "0").FirstOrDefault();
                                if (masterUser != null)
                                {
                                    masterUser.cDel = "1";
                                    masterUser.dUpdate = DateTime.Now.FixSecondAndMillisecond(2, 140);
                                }

                                // User Card
                                dbMaster.TUser_Card.Where(w => w.SchoolID == schoolID && w.sID == sid).ToList().ForEach(e => { e.IsDel = true; e.IsActive = false; e.ModifyBy = userData.UserID; e.Modified = DateTime.Now; });

                                dbMaster.SaveChanges();


                            }
                            catch (Exception err)
                            {
                                success = false;
                                message = err.Message;


                            }
                        }
                        else
                        {
                            throw new Exception("[เนื่องจากมียอดเงินคงเหลือในระบบ]<br/>");
                        }
                    }
                    else
                    {
                        throw new Exception("[ไม่พบข้อมูลนักเรียน]<br/>");
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }


                //  if (success) JabjaiMainClass.Autocompletes.TopupMoney.Remove(sid + "");


                try
                {
                    string userid = sid.ToString();
                    JabjaiMainClass.Autocompletes.TopupMoney.topupMoney.RemoveAll(x => x.User_Id.ToUpper() == userid.ToUpper());
                }
                catch
                {

                }

                var result = new { success, message };
                return JsonConvert.SerializeObject(result);
            }
        }

        [WebMethod]
        public static string[] GetStudentName(string keyword, string termID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
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
        public static List<EntityDropdown> LoadTerm(string yearID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(yearID))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    int yID = Convert.ToInt32(yearID);

                    result = dbschool.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yID && w.cDel == null).OrderByDescending(o => o.nTerm).Select(s => new EntityDropdown { id = s.nTerm, name = s.sTerm }).ToList();
                }
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
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
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

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadRoom(string subLevelID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(subLevelID))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {

                    //int slID = Convert.ToInt32(subLevelID);
                    var arr = subLevelID.Split(',');

                    string query = string.Format(@"
SELECT r.id, r.name
FROM
(
	SELECT CAST(t.nTermSubLevel2 AS VARCHAR(10)) 'id', s.SubLevel + ' / ' + t.nTSubLevel2 'name', s.SubLevel 'sort1', (CASE WHEN ISNUMERIC(t.nTSubLevel2) = 1 THEN RIGHT('0000' + t.nTSubLevel2, 5) ELSE t.nTSubLevel2 END) 'sort2'
	FROM TTermSubLevel2 t 
	LEFT JOIN TSubLevel s ON t.nTSubLevel = s.nTSubLevel 
	WHERE t.SchoolID = {0} AND t.nTSubLevel in ({1}) AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1
) r
ORDER BY r.sort1, r.sort2", schoolID, string.Join(",", arr));
                    result = dbschool.Database.SqlQuery<EntityDropdown>(query).ToList();
                }
            }
            return result;
        }

        [WebMethod]
        public static object ShowPassword(int sid)
        {
            object result = null;

            try
            {
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    int schoolID = GetUserData().CompanyID;
                    result = dbMaster.TUsers.Where(w => w.sID == sid && w.cType == "0" && w.nCompany == schoolID).Select(s => new { fullName = s.sName + " " + s.sLastname, userName = s.username, password = s.UseEncryptPassword ? "" : s.userpassword }).FirstOrDefault();
                }
            }
            catch
            {
                result = "error";
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

        [WebMethod]
        public static object ResetPIN(int sid)
        {
            object result = "success";

            try
            {
                JWTToken.userData userData = GetUserData();
                int schoolID = userData.CompanyID;
                int userID = userData.UserID;
                string URL = ConfigurationManager.AppSettings["PaymentApi"].ToString() + $"/api/shop/pos/resetpin?UserID={sid}&SchoolID={schoolID}";

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
                    var userObj = dbMaster.TUsers.Where(w => w.sID == resetPasswordInfo.sid && w.cType == "0" && w.nCompany == schoolID).FirstOrDefault();
                    if (userObj != null)
                    {
                        userObj.userpassword = resetPasswordInfo.newPassword;
                        userObj.PasswordHash = ComFunction.HashSHA1(resetPasswordInfo.newPassword);
                        userObj.UseEncryptPassword = false;

                        dbMaster.SaveChanges();

                        result = "success";

                        database.InsertLog(userData.UserID.ToString(), "รีเซ็ตรหัสผ่านนักเรียน " + userObj.sName + " " + userObj.sLastname + " สำเร็จ", HttpContext.Current.Request, 2, 3, 0, schoolID);
                    }
                    else
                    {
                        result = "error";
                        message = "ไม่พบข้อมูลนักเรียนรายนี้";
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

        public class ResetPasswordInfo
        {
            public int sid { get; set; }
            public string newPassword { get; set; }

        }

        [WebMethod]
        public static object SaveSortStudentNo(SortStudentNo sortOption)
        {
            bool success = true;
            string message = "Success";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                try
                {
                    List<string> terms = sortOption.terms;

                    // Check null column moveInDate when checked
                    if (sortOption.moveIn)
                    {
                        string condition = "";
                        if (sortOption.level == "room")
                        {
                            if (sortOption.roomID == null)
                            {
                                condition += " AND tsl.nTSubLevel = " + sortOption.levelID;
                            }
                            else
                            {
                                condition += " AND sch.nTermSubLevel2 = " + sortOption.roomID;
                            }
                        }

                        string query = string.Format(@"
SELECT DISTINCT u.sStudentID--, sch.MoveInDate, u.moveInDate, ISNULL(sch.MoveInDate, u.moveInDate) 'MoveInDate'
FROM TStudentClassroomHistory sch 
LEFT JOIN TUser u ON sch.SchoolID = u.SchoolID AND sch.sID = u.sID
LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
WHERE sch.SchoolID = {0} AND sch.cDel = 0 AND ISNULL(sch.nStudentStatus, 0) = 0 AND ISNULL(sch.MoveInDate, u.moveInDate) IS NULL {1}
", schoolID, condition);
                        IList<string> sStudentIDs = en.Database.SqlQuery<string>(query).ToList();

                        throw new Exception(string.Format(@"ไม่พบข้อมูล <b>วันที่เข้าเรียน</b> ของรหัสนักเรียนนี้: {0}", string.Join(", ", sStudentIDs)));
                    }

                    switch (sortOption.level)
                    {
                        case "all":
                            // กรณีเลือกทั้งหมด
                            var roomList = (from t in en.TUser
                                            where t.SchoolID == schoolID
                                            group t by new
                                            {
                                                t.nTermSubLevel2
                                            } into g
                                            select new
                                            {
                                                g.Key.nTermSubLevel2
                                            }).ToList();

                            foreach (var r in roomList)
                            {
                                foreach (var term in terms)
                                {
                                    UpdateStudentNo(en, schoolID, term, r.nTermSubLevel2, sortOption);
                                }
                            }

                            break;
                        case "room":

                            if (sortOption.roomID == null)
                            {
                                // กรณีเลือกทั้งระดับชั้นเรียน
                                var levelRoomList = en.TTermSubLevel2.Where(w => w.SchoolID == schoolID && w.nTSubLevel == sortOption.levelID).Select(s => new { s.nTermSubLevel2 }).ToList();
                                foreach (var r in levelRoomList)
                                {
                                    foreach (var term in terms)
                                    {
                                        UpdateStudentNo(en, schoolID, term, r.nTermSubLevel2, sortOption);
                                    }
                                }
                            }
                            else
                            {
                                // กรณีเลือกชั้นเรียน
                                foreach (var term in terms)
                                {
                                    UpdateStudentNo(en, schoolID, term, null, sortOption);
                                }
                            }

                            break;
                    }

                    var json = new JavaScriptSerializer().Serialize(sortOption);

                    database.InsertLog(userData.UserID.ToString(), "จัดเรียงเลขที่นักเรียน [" + json + "]", HttpContext.Current.Request, 2, 3, 0, schoolID);
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }

                return JsonConvert.SerializeObject(new { success, message });
            }
        }

        [WebMethod]
        public static object SaveCopyStudentNo(CopyStudentNoModel copyStudentNo)
        {
            bool success = true;
            string message = "Success";

            int noRowUpdated = 0;

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                try
                {
                    string query = string.Format(@"
UPDATE dest 
SET dest.nStudentNumber=ori.nStudentNumber
FROM TStudentClassroomHistory dest
INNER JOIN
(SELECT * FROM TStudentClassroomHistory WHERE SchoolID={0} and nTerm='{1}') ori
ON dest.SchoolID=ori.SchoolID AND dest.sID = ori.sID
WHERE dest.SchoolID={0} AND dest.nTerm='{2}'
", schoolID, copyStudentNo.OriginTermId, copyStudentNo.DestinationTermId);
                    noRowUpdated = en.Database.ExecuteSqlCommand(query);

                    database.InsertLog(userData.UserID.ToString(), string.Format(@"คัดลอกเลขที่นักเรียน จากปีการศึกษา {0} เทอม {1} ไปยัง ปีการศึกษา {2} เทอม {3} [NOROWUPDATED: {4}]", copyStudentNo.OriginYear, copyStudentNo.OriginTerm, copyStudentNo.DestinationYear, copyStudentNo.DestinationTerm, noRowUpdated), HttpContext.Current.Request, 2, 3, 0, schoolID);
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }

                return JsonConvert.SerializeObject(new { success, message, noRowUpdated });
            }
        }

        static void UpdateStudentNo(JabJaiEntities en, int schoolID, string termID, int? roomID, SortStudentNo sortOption)
        {
            if (roomID == null)
            {
                roomID = sortOption.roomID;
            }

            string orderQuery = "";

            switch (sortOption.sex)
            {
                case "0": orderQuery += ", cSex ASC"; break;
                case "1": orderQuery += ", cSex DESC"; break;
            }
            if (sortOption.moveIn)
            {
                orderQuery += ", ISNULL(sch.moveInDate, u.moveInDate) ASC";
            }
            if (sortOption.studentID)
            {
                orderQuery += ", RIGHT('0000000000' + sStudentID, 10) ASC";
                //orderQuery += ", sStudentID ASC";
            }

            string sqlQuery = "";
            if (sortOption.moveInAfterMonth6)
            {
                sqlQuery = string.Format(@"
SELECT sID
FROM
(
	SELECT TOP 200 u.sID 
	FROM TUser u
	LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
	LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
	WHERE u.cDel IS NULL AND sch.cDel = 0 AND u.SchoolID = {0} AND sch.nTerm = '{1}' AND sch.nTermSubLevel2 = {2} AND CAST('1'+RIGHT('00' + CONVERT(VARCHAR(2), DATEPART(MONTH, ISNULL(sch.moveInDate, u.moveInDate))), 2)+RIGHT('00' + CONVERT(VARCHAR(2), DATEPART(DAY, ISNULL(sch.moveInDate, u.moveInDate))), 2) AS INT) <= 10625 --10810 --10610[1MMDD]
	{3}
) A
UNION ALL
SELECT sID
FROM
(
    SELECT TOP 200 u.sID 
    FROM TUser u
    LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
    LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
    WHERE u.cDel IS NULL AND sch.cDel = 0 AND u.SchoolID = {0} AND sch.nTerm = '{1}' AND sch.nTermSubLevel2 = {2} AND CAST('1'+RIGHT('00' + CONVERT(VARCHAR(2), DATEPART(MONTH, ISNULL(sch.moveInDate, u.moveInDate))), 2)+RIGHT('00' + CONVERT(VARCHAR(2), DATEPART(DAY, ISNULL(sch.moveInDate, u.moveInDate))), 2) AS INT) > 10625 --10810 --10610[1MMDD]
) B
UNION ALL
SELECT sID
FROM
(
    SELECT TOP 200 u.sID 
    FROM TUser u
    LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
    LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
    WHERE u.cDel IS NULL AND sch.cDel = 0 AND u.SchoolID = {0} AND sch.nTerm = '{1}' AND sch.nTermSubLevel2 = {2} AND ISNULL(sch.moveInDate, u.moveInDate) IS NULL 
) C", schoolID, termID, roomID, (string.IsNullOrEmpty(orderQuery) ? "" : "ORDER BY " + orderQuery.Remove(0, 2)));
            }
            else
            {
                sqlQuery = string.Format(@"
SELECT u.sID 
FROM TUser u
LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
WHERE u.cDel IS NULL AND sch.cDel = 0 AND u.SchoolID = {0} AND sch.nTerm = '{1}' AND sch.nTermSubLevel2 = {2} 
{3}", schoolID, termID, roomID, (string.IsNullOrEmpty(orderQuery) ? "" : "ORDER BY " + orderQuery.Remove(0, 2)));
            }
            List<int> sIDList = en.Database.SqlQuery<int>(sqlQuery).ToList();

            JWTToken.userData userData = GetUserData();

            int runNumber = 1;
            foreach (var sID in sIDList)
            {
                en.TUser.First(w => w.sID == sID).nStudentNumber = runNumber;

                var studentClassroomHistory = en.TStudentClassroomHistories.FirstOrDefault(w => w.SchoolID == schoolID && w.sID == sID && w.nTerm == termID && w.nTermSubLevel2 == roomID && w.cDel == false);
                if (studentClassroomHistory != null)
                {
                    studentClassroomHistory.nStudentNumber = runNumber;
                    studentClassroomHistory.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(2, 130);
                    studentClassroomHistory.UpdatedBy = userData.UserID;
                }

                runNumber++;
            }
            en.SaveChanges();
        }

        [WebMethod]
        public static object SaveManageStudentTitle(ManageStudentTitle titleOption)
        {
            object result = "success";
            string message = "";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                try
                {
                    var commandUpdateMale = "";
                    var commandUpdateFemale = "";
                    var studentTitleMale = "";
                    var studentTitleFemale = "";

                    TTitleList maleTitle;
                    TTitleList femaleTitle;

                    // 0 = เพศชาย, 1 = เพศหญิง
                    // Age Lower 15
                    if (titleOption.ageLower15 != null)
                    {
                        // 1 = ด.ช. ด.ญ., 2 = เด็กชาย เด็กหญิง
                        switch (titleOption.ageLower15)
                        {
                            case "1":
                                studentTitleMale = "ด.ช.";
                                studentTitleFemale = "ด.ญ.";
                                break;
                            case "2":
                                studentTitleMale = "เด็กชาย";
                                studentTitleFemale = "เด็กหญิง";
                                break;
                        }

                        maleTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted == "0" && w.workStatus == "working" && w.titleDescription == studentTitleMale).FirstOrDefault();
                        if (maleTitle != null)
                        {
                            studentTitleMale = maleTitle.nTitleid.ToString();
                        }

                        femaleTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted == "0" && w.workStatus == "working" && w.titleDescription == studentTitleFemale).FirstOrDefault();
                        if (femaleTitle != null)
                        {
                            studentTitleFemale = femaleTitle.nTitleid.ToString();
                        }

                        commandUpdateMale = string.Format(@"
UPDATE TUser
SET sStudentTitle = @StudentTitle
WHERE cDel IS NULL AND SchoolID = {0} AND cSex = '0' AND DATEDIFF(HOUR, dBirth, GETDATE())/ 8760.0 < 15;", schoolID);

                        commandUpdateFemale = string.Format(@"
UPDATE TUser
SET sStudentTitle = @StudentTitle
WHERE cDel IS NULL AND SchoolID = {0} AND cSex = '1' AND DATEDIFF(HOUR, dBirth, GETDATE())/ 8760.0 < 15;", schoolID);

                        var paraMale = new SqlParameter("@StudentTitle", studentTitleMale);
                        int rMale = en.Database.ExecuteSqlCommand(commandUpdateMale, paraMale);

                        var paraFemale = new SqlParameter("@StudentTitle", studentTitleFemale);
                        int rFemale = en.Database.ExecuteSqlCommand(commandUpdateFemale, paraFemale);

                        message += string.Format(@" [อายุต่ำกว่า 15 ปี อัปเดตนักเรียนชาย {0} รายการ, อัปเดตนักเรียนหญิง {1} รายการ]", rMale, rFemale);
                    }

                    // Age Over 15
                    if (titleOption.ageOver15)
                    {
                        // นาย นางสาว
                        studentTitleMale = "นาย";
                        studentTitleFemale = "นางสาว";

                        maleTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted == "0" && w.workStatus == "working" && w.titleDescription == studentTitleMale).FirstOrDefault();
                        if (maleTitle != null)
                        {
                            studentTitleMale = maleTitle.nTitleid.ToString();
                        }

                        femaleTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted == "0" && w.workStatus == "working" && w.titleDescription == studentTitleFemale).FirstOrDefault();
                        if (femaleTitle != null)
                        {
                            studentTitleFemale = femaleTitle.nTitleid.ToString();
                        }

                        commandUpdateMale = string.Format(@"
UPDATE TUser
SET sStudentTitle = @StudentTitle
WHERE cDel IS NULL AND SchoolID = {0} AND cSex = '0' AND DATEDIFF(HOUR, dBirth, GETDATE())/ 8760.0 >= 15;", schoolID);

                        commandUpdateFemale = string.Format(@"
UPDATE TUser
SET sStudentTitle = @StudentTitle
WHERE cDel IS NULL AND SchoolID = {0} AND cSex = '1' AND DATEDIFF(HOUR, dBirth, GETDATE())/ 8760.0 >= 15;", schoolID);

                        var paraMale = new SqlParameter("@StudentTitle", studentTitleMale);
                        int rMale = en.Database.ExecuteSqlCommand(commandUpdateMale, paraMale);

                        var paraFemale = new SqlParameter("@StudentTitle", studentTitleFemale);
                        int rFemale = en.Database.ExecuteSqlCommand(commandUpdateFemale, paraFemale);

                        message += string.Format(@" [ตั้งแต่ 15 ปีขึ้นไป อัปเดตนักเรียนชาย {0} รายการ, อัปเดตนักเรียนหญิง {1} รายการ]", rMale, rFemale);
                    }

                    // Change title as specified date
                    if (!string.IsNullOrEmpty(titleOption.changeTitleAsSpecifiedDate.SpecifiedDate))
                    {
                        // นาย นางสาว
                        studentTitleMale = "นาย";
                        studentTitleFemale = "นางสาว";

                        maleTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted == "0" && w.workStatus == "working" && w.titleDescription == studentTitleMale).FirstOrDefault();
                        if (maleTitle != null)
                        {
                            studentTitleMale = maleTitle.nTitleid.ToString();
                        }

                        femaleTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted == "0" && w.workStatus == "working" && w.titleDescription == studentTitleFemale).FirstOrDefault();
                        if (femaleTitle != null)
                        {
                            studentTitleFemale = femaleTitle.nTitleid.ToString();
                        }

                        DateTime dSpecifiedDate = DateTime.ParseExact(titleOption.changeTitleAsSpecifiedDate.SpecifiedDate, "dd/MM/yyyy", new CultureInfo("th-TH"));

                        commandUpdateMale = string.Format(@"
UPDATE TUser
SET sStudentTitle = @StudentTitle
WHERE cDel IS NULL AND SchoolID = {0} AND cSex = '0' AND DATEDIFF(HOUR, dBirth, '{1:yyyy-MM-dd}')/ 8760.0 >= 15;", schoolID, dSpecifiedDate);

                        commandUpdateFemale = string.Format(@"
UPDATE TUser
SET sStudentTitle = @StudentTitle
WHERE cDel IS NULL AND SchoolID = {0} AND cSex = '1' AND DATEDIFF(HOUR, dBirth, '{1:yyyy-MM-dd}')/ 8760.0 >= 15;", schoolID, dSpecifiedDate);

                        var paraMale = new SqlParameter("@StudentTitle", studentTitleMale);
                        int rMale = en.Database.ExecuteSqlCommand(commandUpdateMale, paraMale);

                        var paraFemale = new SqlParameter("@StudentTitle", studentTitleFemale);
                        int rFemale = en.Database.ExecuteSqlCommand(commandUpdateFemale, paraFemale);

                        message += string.Format(@" [ปรับคำนำหน้าอัตโนมัติวันที่ {2} ระดับชั้น {3}({4}) ชั้น {5}({6}) อัปเดตนักเรียนชาย {0} รายการ, อัปเดตนักเรียนหญิง {1} รายการ]", rMale, rFemale, titleOption.changeTitleAsSpecifiedDate.SpecifiedDate, titleOption.changeTitleAsSpecifiedDate.Level, titleOption.changeTitleAsSpecifiedDate.LevelID, titleOption.changeTitleAsSpecifiedDate.Classroom, titleOption.changeTitleAsSpecifiedDate.ClassroomID);
                    }

                    database.InsertLog(userData.UserID.ToString(), "จัดคำนำหน้านักเรียน:" + message, HttpContext.Current.Request, 2, 3, 0, schoolID);
                }
                catch
                {
                    result = "error";
                }

                return result;
            }
        }

        [WebMethod]
        public static object ClearAllStudentData()
        {
            bool success = true;
            string message = "";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            int userID = userData.UserID;
            using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                try
                {
                    if (schoolID != 936) throw new Exception("This school does not have permission to use this function.");

                    string updateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    // Generate Recovery Script
                    string script = "";
                    List<int> sIDs = new List<int>();
                    script += "--Master User" + Environment.NewLine;
                    var mus = mctx.TUsers.Where(w => w.nCompany == schoolID && w.cType == "0" && w.cDel == null).ToList();
                    foreach (var mu in mus)
                    {
                        script += $@"UPDATE JabjaiMasterSingleDB.dbo.TUser SET cDel=NULL WHERE nCompany={schoolID} AND sID={mu.sID} AND cDel='1' AND dUpdate='{updateDate}'{Environment.NewLine}";

                        sIDs.Add(mu.sID);
                    }

                    script += "--User" + Environment.NewLine;
                    var us = ctx.TUser.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList();
                    foreach (var u in us)
                    {
                        script += $@"UPDATE TUser SET cDel=NULL WHERE SchoolID={schoolID} AND sID={u.sID} AND cDel='1' AND UpdatedDate='{updateDate}'{Environment.NewLine}";
                    }

                    script += "--User Card" + Environment.NewLine;
                    var ucs = mctx.TUser_Card.Where(w => w.SchoolID == schoolID && w.IsDel == false && sIDs.Contains(w.sID)).ToList();
                    foreach (var uc in ucs)
                    {
                        script += $@"UPDATE JabjaiMasterSingleDB.dbo.TUser_Card SET IsDel=0, IsActive={((bool)uc.IsActive ? "1" : "0")} WHERE SchoolID={schoolID} AND sID={uc.sID} AND IsDel=1 AND Modified='{updateDate}'{Environment.NewLine}";
                    }


                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Student")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Student"));
                    }

                    string fileName = string.Format(@"RecoveryScript_{0}{1}_{2}.sql", schoolID.ToString("D4"), userID.ToString("D8"), DateTime.Now.ToString("yyMMddHHmmss"));
                    string filePath = HttpContext.Current.Server.MapPath("~/Upload/Student/" + fileName);

                    // Save file in File folder.
                    File.WriteAllText(filePath, script);


                    // Execute
                    string query = string.Format($@"UPDATE JabjaiMasterSingleDB.dbo.TUser SET cDel='1', dUpdate='{updateDate}' WHERE nCompany={schoolID} AND cType='0' AND cDel IS NULL");
                    int effectRowMU = ctx.Database.ExecuteSqlCommand(query);

                    query = string.Format($@"UPDATE TUser SET cDel='1', UpdatedDate='{updateDate}' WHERE SchoolID={schoolID} AND cDel IS NULL");
                    int effectRowU = ctx.Database.ExecuteSqlCommand(query);

                    query = string.Format($@"UPDATE JabjaiMasterSingleDB.dbo.TUser_Card SET IsDel=1, IsActive=0, Modified='{updateDate}' WHERE SchoolID={schoolID} AND IsDel=0 AND sID IN ({(sIDs.Count > 0 ? string.Join(", ", sIDs) : "0")})");
                    int effectRowUC = ctx.Database.ExecuteSqlCommand(query);

                    database.InsertLog(userData.UserID.ToString(), $@"ลบข้อมูลนักเรียนทั้งหมด: [Master User: {effectRowMU}, User: {effectRowU}, User Card: {effectRowUC}]", HttpContext.Current.Request, 2, 3, 0, schoolID);
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }

                var result = new { success, message };
                return JsonConvert.SerializeObject(result);
            }
        }

        public class SortStudentNo
        {
            public bool studentID { get; set; }
            public string sex { get; set; }
            public bool moveIn { get; set; }
            public bool moveInAfterMonth6 { get; set; }
            public string level { get; set; }
            public int levelID { get; set; }
            public int? roomID { get; set; }
            public List<string> terms { get; set; }
        }

        public class CopyStudentNoModel
        {
            [JsonProperty(PropertyName = "originYearId")]
            public int OriginYearId { get; set; }

            [JsonProperty(PropertyName = "originYear")]
            public int OriginYear { get; set; }

            [JsonProperty(PropertyName = "originTermId")]
            public string OriginTermId { get; set; }

            [JsonProperty(PropertyName = "originTerm")]
            public string OriginTerm { get; set; }

            [JsonProperty(PropertyName = "destinationYearId")]
            public int DestinationYearId { get; set; }

            [JsonProperty(PropertyName = "destinationYear")]
            public int DestinationYear { get; set; }

            [JsonProperty(PropertyName = "destinationTermId")]
            public string DestinationTermId { get; set; }

            [JsonProperty(PropertyName = "destinationTerm")]
            public string DestinationTerm { get; set; }
        }

        public class ManageStudentTitle
        {
            public string ageLower15 { get; set; }
            public bool ageOver15 { get; set; }
            public ChangeTitleAsSpecifiedDate changeTitleAsSpecifiedDate { get; set; }
        }

        public class ChangeTitleAsSpecifiedDate
        {
            [JsonProperty(PropertyName = "specifiedDate")]
            public string SpecifiedDate { get; set; }

            [JsonProperty(PropertyName = "levelId")]
            public int? LevelID { get; set; }

            [JsonProperty(PropertyName = "level")]
            public string Level { get; set; }

            [JsonProperty(PropertyName = "classroomId")]
            public int? ClassroomID { get; set; }

            [JsonProperty(PropertyName = "classroom")]
            public string Classroom { get; set; }
        }

        public class YearTermModel
        {
            [JsonProperty(PropertyName = "no")]
            public int No { get; set; }

            [JsonProperty(PropertyName = "yearId")]
            public int YearID { get; set; }

            [JsonProperty(PropertyName = "yearNumber")]
            public int YearNumber { get; set; }

            [JsonProperty(PropertyName = "termId")]
            public string TermID { get; set; }

            [JsonProperty(PropertyName = "term")]
            public string Term { get; set; }

            [JsonProperty(PropertyName = "currentYear")]
            public bool CurrentYear { get; set; }

            [JsonProperty(PropertyName = "currentTerm")]
            public bool CurrentTerm { get; set; }
        }

    }
}