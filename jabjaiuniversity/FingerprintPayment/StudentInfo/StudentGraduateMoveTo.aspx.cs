using AjaxControlToolkit;
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
using FingerprintPayment.Helper;

namespace FingerprintPayment.StudentInfo
{
    public partial class StudentGraduateMoveTo : StudentGateway
    {
        protected string LevelData = "";
        protected string ClassRoomData = "";
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
                string currentTerm = studentLogic.GetTermId(UserData);
                int yearID = 0;
                var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
                if (term != null)
                {
                    yearID = term.nYear.Value;
                }

                var listYear = en.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false).OrderByDescending(x => x.numberYear).ToList();
                foreach (var l in listYear)
                {
                    if (l.nYear == yearID)
                    {
                        this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }
                    else
                    {
                        this.ltrYear.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }

                    if (yearID == 0) yearID = l.nYear;
                }

                if (yearID != 0)
                {
                    var listTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yearID && w.cDel == null).OrderByDescending(o => o.nTerm).ToList();
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

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);

                    LevelData += string.Format(@", {{'levelID':{0}, 'levelName':'{1}'}}", l.nTSubLevel, l.SubLevel);
                }
                if (!string.IsNullOrEmpty(LevelData)) LevelData = LevelData.Remove(0, 2);

                string query = string.Format(@"
SELECT r.id, r.name, r.lid
FROM
(
	SELECT t.nTermSubLevel2 'id', s.SubLevel + ' / ' + t.nTSubLevel2 'name', s.nTSubLevel 'lid'
	, s.SubLevel 'sort1', (CASE WHEN ISNUMERIC(t.nTSubLevel2) = 1 THEN RIGHT('0000' + t.nTSubLevel2, 5) ELSE t.nTSubLevel2 END) 'sort2'
	FROM TTermSubLevel2 t 
	LEFT JOIN TSubLevel s ON t.nTSubLevel = s.nTSubLevel 
	WHERE t.SchoolID = {0} AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1
) r
ORDER BY r.sort1, r.sort2", schoolID);
                List<EntityDropdown2> classRooms = en.Database.SqlQuery<EntityDropdown2>(query).ToList();
                var jsonClassRoom = new JavaScriptSerializer().Serialize(classRooms);
                ClassRoomData = jsonClassRoom.Substring(1, jsonClassRoom.Length - 2);

                // List Curr & Next year
                int currYear = 0;
                var termDataCurr = en.TTerms.FirstOrDefault(f => f.dStart <= DateTime.Today && f.dEnd >= DateTime.Today && f.SchoolID == schoolID && f.cDel == null); // Curr
                if (termDataCurr != null)
                {
                    var y = listYear.Where(w => w.nYear == termDataCurr.nYear).FirstOrDefault();
                    if (y != null)
                    {
                        this.ltrStudentGraduateMoveToYear.Text += string.Format(@"<option value=""{0}"" {2}>{1}</option>", y.nYear, y.numberYear, y.nYear == yearID ? @"selected=""selected""" : "");
                        currYear = y.nYear;
                    }
                }

                var termDataNext = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear != currYear).OrderBy(o => o.dEnd).FirstOrDefault(f => f.dStart >= DateTime.Today && f.cDel == null); // Next
                if (termDataNext != null)
                {
                    var y = listYear.Where(w => w.nYear == termDataNext.nYear).FirstOrDefault();
                    if (y != null)
                    {
                        this.ltrStudentGraduateMoveToYear.Text += string.Format(@"<option value=""{0}"" {2}>{1}</option>", y.nYear, y.numberYear, y.nYear == yearID ? @"selected=""selected""" : "");
                    }
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
                case "1": sortBy = "[No]"; break;
                case "2": sortBy = "Code"; break;
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

            var json = QueryEngine.LoadStudentGraduateMoveToJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, year, term, level, className, stdName);

            return json;
        }

        [WebMethod(EnableSession = true)]
        public static object StudentGraduateMoveToClassRoom(List<StudentData> studentDatas, int? yearID, string year)
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";

            int iSuccess = 0;
            int iUnsuccess = 0;
            string error = "";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                if (yearID != null)
                {
                   
                        

                        try
                        {
                            var checkedDatas = studentDatas.Where(w => w.chk == true && w.lid != 0 && w.crid != 0).ToList();
                            foreach (var d in checkedDatas)
                            {
                                var u = en.TUser.Where(w => w.sID == d.sid).FirstOrDefault();
                                var mu = dbMaster.TUsers.Where(w => w.sID == d.sid).FirstOrDefault();

                                var terms = en.TTerms.Where(w => w.nYear == yearID && w.cDel == null).ToList();

                                //int existIdentify = en.TUsers.Where(w => w.SchoolID == schoolID && w.cDel == null && (((w.sIdentification ?? "") != "" && (w.sIdentification != "-") && w.sIdentification == u.sIdentification) || ((w.sStudentID ?? "") != "" && (w.sStudentID != "-") && w.sStudentID == u.sStudentID))).Count();
                                string[] aTerms = terms.Select(s => s.nTerm).ToArray();
                                string commaTerms = String.Join("','", aTerms);
                                //string query = string.Format(@"SELECT COUNT(*) FROM TStudentClassroomHistory WHERE nTerm IN ('{0}') AND sID={1} AND cDel = 0 AND ISNULL(nStudentStatus, 0) = 0", commaTerms, u.sID);
                                string query = string.Format(@"
SELECT CAST(CASE WHEN SUM(CASE WHEN sch.cDel=0 AND ISNULL(sch.nStudentStatus, 0) = 0 THEN 1 ELSE 0 END) = COUNT(*) AND COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) 
FROM TStudentClassroomHistory sch LEFT JOIN TUser u ON sch.SchoolID = u.SchoolID AND sch.sID = u.sID 
WHERE sch.nTerm IN ('{0}') AND sch.sID={1} AND u.cDel IS NULL", commaTerms, u.sID);
                                bool existStudying = en.Database.SqlQuery<bool>(query).FirstOrDefault();

                                int backupCardCount = en.TBackupCards.Where(w => w.SchoolID == schoolID && w.BarCode == u.sStudentID && w.cDel == false).Count();

                                if (u != null && mu != null && !existStudying && backupCardCount == 0)
                                {
                                    u.cDel = null;
                                    u.nTermSubLevel2 = d.crid;
                                    u.nStudentStatus = 0;
                                    //u.DayQuit = null;
                                    //u.Note = null;

                                    en.SaveChanges();

                                    // Get last moveInDate
                                    DateTime? lastMoveInDate = null;
                                    query = string.Format($@"SELECT MAX(MoveInDate) FROM TStudentClassroomHistory WHERE sID={u.sID} AND cDel=0");
                                    lastMoveInDate = en.Database.SqlQuery<DateTime?>(query).FirstOrDefault();
                                    if (lastMoveInDate == null && u.moveInDate != null)
                                    {
                                        lastMoveInDate = u.moveInDate;
                                    }
                                    if (lastMoveInDate == null)
                                    {
                                        lastMoveInDate = DateTime.Now;
                                    }

                                    // Bind term
                                    //var terms = en.TTerms.Where(w => w.nYear == yearID && w.cDel == null).ToList();
                                    foreach (var t in terms)
                                    {
                                        TStudentClassroomHistory sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == d.sid && w.nTerm == t.nTerm && w.cDel == false).FirstOrDefault();
                                        if (sch == null)
                                        {
                                            sch = new TStudentClassroomHistory
                                            {
                                                sID = d.sid,
                                                nTerm = t.nTerm,
                                                nTermSubLevel2 = d.crid,
                                                SchoolID = schoolID,
                                                CreatedBy = userData.UserID,
                                                CreatedDate = DateTime.Now.FixSecond(14).FixMillisecond(14),
                                                nStudentStatus = 0,
                                                MoveInDate = lastMoveInDate
                                            };

                                            en.TStudentClassroomHistories.Add(sch);
                                        }
                                        else
                                        {
                                            sch.nTermSubLevel2 = d.crid;
                                            sch.nStudentStatus = 0;
                                            sch.MoveOutDate = null;
                                            sch.DropOutType = null;
                                            sch.Note = null;
                                            sch.UpdatedBy = userData.UserID;
                                            sch.UpdatedDate = DateTime.Now.FixSecond(14).FixMillisecond(14);
                                        }

                                        en.SaveChanges();
                                    }

                                    mu.cDel = null;

                                    dbMaster.SaveChanges();

                                    iSuccess++;

                                    database.InsertLog(userData.UserID.ToString(), "นำเข้าข้อมูลศิษย์เก่า: " + d.sid + ", ชั้น: " + d.lid + ", ห้อง: " + d.crid + ", yearID:" + yearID, HttpContext.Current.Request, 14, 2, 0, schoolID);

                                    try
                                    {
                                        // Update memory
                                        JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(schoolID, mu.sID + "", "0", "");
                                        Class.UpdateMemory memory = new Class.UpdateMemory(userData.AuthKey, userData.AuthValue);
                                        memory.Student(u, mu);
                                    }
                                    catch
                                    {
                                        string msg = "มีปัญหาในการ update ข้อมูลใน memory";
                                        error += string.Format($@"รหัส {d.studentId} - {msg}<br/>");
                                    }
                                }
                                else
                                {
                                    iUnsuccess++;

                                    string msg = "";
                                    if (mu == null) msg += ", ไม่พบรหัสนักเรียนนี้ในข้อมูลนักเรียน(Master)";
                                    if (u == null) msg += ", ไม่พบรหัสนักเรียนนี้ในข้อมูลนักเรียน";
                                    if (existStudying) msg += $", พบรหัสนักเรียนนี้กำลังศึกษาในปี {year}";
                                    if (backupCardCount > 0) msg += ", พบรหัสนักเรียนนี้ในข้อมูลบัตรสำรอง";

                                    if (!string.IsNullOrEmpty(msg))
                                    {
                                        msg = msg.Remove(0, 2);
                                    }
                                    else
                                    {
                                        msg = "???";
                                    }

                                    error += string.Format($@"รหัส {d.studentId} - {msg}<br/>");
                                }
                            }

                           
                        }
                        catch (Exception err)
                        {
                           

                            success = false;
                            message = err.Message;

                            Class.ComFunction.InsertLogDebug(schoolID, null, userData.UserID, err.Message);
                        }
                    
                }
                else
                {
                    success = false;
                    message = "ไม่พบข้อมูลปี/เทอม";
                }
            }

            var result = new { success, statusCode, message, log = new { success = iSuccess, unsuccess = iUnsuccess, error } };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static string[] GetStudentName(string keyword)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname
FROM TUser
WHERE cDel IS NULL AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%')
GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, keyword);
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
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
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

        public class StudentData
        {
            public int sid { get; set; }
            public int lid { get; set; }
            public int crid { get; set; }
            public bool chk { get; set; }
            public string studentId { get; set; }
        }

    }
}