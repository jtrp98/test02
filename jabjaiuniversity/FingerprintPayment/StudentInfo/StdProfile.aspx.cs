using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class StdProfile : StudentGateway
    {
        private static string SessionPrimaryKey = "STUDENTID";
        private static string SessionPrimaryKeyTermID = "TERMID";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        #region Method

        private void InitialPage()
        {
            string v = Request.QueryString["v"];

            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {

                int sid = 0;
                string query = "";
                string currentTermID = "";
                string termID = "";

                List<TermData> termData = new List<TermData>();

                string paddingCheckbox = "";

                TTerm term = null;

                var userData = GetUserData();
                StudentLogic studentLogic = new StudentLogic(en);

                switch (v)
                {
                    case "list":
                        MvContent.ActiveViewIndex = 0; break;
                    case "form":
                        MvContent.ActiveViewIndex = 1;

                        // Init data dropdown

                        using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                        {
                            // Title
                            var titleLists = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && w.workStatus == "working").ToList();
                            foreach (var r in titleLists)
                            {
                                this.ltrStudentTitle.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTitleid, r.titleDescription);
                            }

                            // Level
                            var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                            foreach (var l in listLevel)
                            {
                                this.ltrStudentClass.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                            }

                            // Finger
                            sid = Convert.ToInt32(Request.QueryString["sid"]);
                            if (sid != 0)
                            {
                                var masterUser = dbMaster.TUsers.Where(w => w.sID == sid && w.nCompany == schoolID && w.cType == "0").FirstOrDefault();
                                if (masterUser != null)
                                {
                                    if (masterUser.sFinger == null && masterUser.sFinger2 == null && masterUser.sPassword == "000000")
                                    {
                                        ltrDelFinger.Visible = true;
                                    }
                                    else if (masterUser.sFinger == null && masterUser.sFinger2 == null)
                                    {
                                        ltrFinger.Text = "รหัสลายนิ้วมือ :";
                                        ltrDelFinger.Visible = false;
                                        ltrPassword.Text = @"<p style=""padding-top: 15px;"">" + masterUser.sPassword + "</p>";
                                    }
                                    else
                                    {
                                        ltrFinger.Text = "ลายนิ้วมือ :";
                                    }
                                }
                            }
                            else
                            {
                                ltrFinger.Text = "";
                                ltrDelFinger.Visible = false;
                            }

                            // Nation
                            var listNation = en.TMasterDatas.Where(w => w.MasterType == "3").OrderBy(o => o.MasterDes).ToList();
                            foreach (var l in listNation)
                            {
                                this.ltrStudentNation.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                            }

                            // Religion
                            var listReligion = en.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                            foreach (var l in listReligion)
                            {
                                this.ltrStudentReligion.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                            }

                            // Race
                            var listRace = en.TMasterDatas.Where(w => w.MasterType == "9").OrderBy(o => o.MasterDes).ToList();
                            foreach (var l in listRace)
                            {
                                this.ltrStudentRace.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                            }

                            // Disability - ความพิการ
                            var listDisability = en.TMasterDatas.Where(w => w.MasterType == "7").ToList();
                            foreach (var l in listDisability)
                            {
                                this.ltrDisabilityCode.Text += string.Format(@"<option {2} value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes, (l.MasterCode == "99" ? "selected=\"selected\"" : ""));
                            }

                            // Disadvantage - ความด้อยโอกาส
                            var listDisadvantage = en.TMasterDatas.Where(w => w.MasterType == "8").ToList();
                            foreach (var l in listDisadvantage)
                            {
                                this.ltrDisadvantageCode.Text += string.Format(@"<option {2} value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes, (l.MasterCode == "99" ? "selected=\"selected\"" : ""));
                            }

                            // Generate Term
                            // sid=0 is new user

                            currentTermID = studentLogic.GetTermId(userData);

                            termID = "";
                            if (sid == 0)
                            {
                                termID = currentTermID;
                            }
                            else
                            {
                                termID = Request.QueryString["tid"];
                            }

                            term = en.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();

                            query = string.Format(@"
SELECT y.nYear 'YearID', y.numberYear 'Year', t.nTerm 'TermID', t.sTerm 'Term', CAST((CASE WHEN sch.nHistoryId IS NOT NULL THEN 'true' ELSE 'false' END) AS BIT) 'TermCheck'
FROM TTerm t 
LEFT JOIN TYear y ON t.SchoolID = y.SchoolID AND t.nYear = y.nYear
LEFT JOIN TStudentClassroomHistory sch ON t.SchoolID = sch.SchoolID AND t.nTerm = sch.nTerm AND sch.cDel=0 AND sch.sID={2}
WHERE t.SchoolID={0} AND t.nYear IN (SELECT nYear FROM TTerm WHERE SchoolID={0} AND nTerm='{1}') AND t.cDel IS NULL
ORDER BY t.sTerm", schoolID, termID, sid);
                            termData = en.Database.SqlQuery<TermData>(query).ToList();

                            ltrTerm.Text = "";
                            paddingCheckbox = "";
                            foreach (var t in termData)
                            {
                                string currentTermStyle = string.Format(@"style=""{0}""", t.TermID == termID ? "font-weight: bold; color: black;" : "font-weight: normal;");
                                string isCurrentTerm = string.Format(@"{0}", t.TermID == termID ? "true" : "false");
                                string isCurrentYear = string.Format(@"{0}", term != null && t.YearID == term.nYear ? "true" : "false");

                                //ltrTerm.Text += string.Format(@"
                                //    <div class=""form-check"" {5}>
                                //        <label>
                                //            <input type=""checkbox"" id=""chk{0}"" name=""chkTerm"" data-id=""{0}"" data-flag=""{1}"" data-current=""{7}"" data-current-year=""{8}"" class=""choose-term"" {4} />
                                //            <span class=""label-text"" {6}>{2}/{3}</span>
                                //        </label>
                                //    </div>", t.TermID, t.TermCheck ? "edit" : "new", t.Year, t.Term, t.TermCheck || sid == 0 ? "checked" : "", paddingCheckbox, currentTermStyle, isCurrentTerm, isCurrentYear);

                                ltrTerm.Text += string.Format(@"
                            <div class=""form-check form-check-inline"">
                                <label class=""form-check-label"" {6}>
                                    <input id=""chk{0}"" name=""chkTerm"" class=""form-check-input choose-term"" type=""checkbox"" data-id=""{0}"" data-flag=""{1}"" data-current=""{7}"" data-current-year=""{8}"" class=""choose-term"" {4} />
                                    {2}/{3}
                                    <span class=""form-check-sign"">
                                        <span class=""check""></span>
                                    </span>
                                </label>
                            </div>", t.TermID, t.TermCheck ? "edit" : "new", t.Year, t.Term, t.TermCheck || sid == 0 ? "checked" : "", paddingCheckbox, currentTermStyle, isCurrentTerm, isCurrentYear);

                                paddingCheckbox = string.Format(@" style=""margin-left: 15px;""");
                            }

                            if (!string.IsNullOrEmpty(ltrTerm.Text))
                            {
                                ltrTerm.Text += @"
                            <a href=""#"" data-toggle=""tooltip"" title=""ตัวหนาคือ 'ปีการศึกษา/เทอม' ที่เลือกหรือปัจจุบัน"" style=""font-size: 20px; margin: 7px;"">
                                <i class=""fa fa-question-circle"" aria-hidden=""true""></i>
                            </a>";
                            }
                        }
                        break;
                    case "view":
                        MvContent.ActiveViewIndex = 2;

                        // Init data dropdown

                        using (JabJaiMasterEntities dbMasterView = Connection.MasterEntities(ConnectionDB.Read))
                        {
                            // Finger
                            sid = Convert.ToInt32(Request.QueryString["sid"]);
                            if (sid != 0)
                            {
                                var masterUser = dbMasterView.TUsers.Where(w => w.sID == sid && w.nCompany == schoolID && w.cType == "0").FirstOrDefault();
                                if (masterUser != null)
                                {
                                    if (masterUser.sFinger == null && masterUser.sFinger2 == null)
                                    {
                                        ltrFingerView.Text = "รหัสลายนิ้วมือ :";
                                        ltrPasswordView.Text = string.Format(@"<span class=""span-data"">{0}</span>", masterUser.sPassword);
                                    }
                                    else
                                    {
                                        ltrFingerView.Text = "ลายนิ้วมือ :";
                                    }
                                }
                            }
                            else
                            {
                                ltrFingerView.Text = "";
                            }

                            // Generate Term
                            // sid=0 is new user

                            currentTermID = studentLogic.GetTermId(userData);

                            termID = "";
                            if (sid == 0)
                            {
                                termID = currentTermID;
                            }
                            else
                            {
                                termID = Request.QueryString["tid"];
                            }

                            term = en.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();

                            query = string.Format(@"
SELECT y.nYear 'YearID', y.numberYear 'Year', t.nTerm 'TermID', t.sTerm 'Term', CAST((CASE WHEN sch.nHistoryId IS NOT NULL THEN 'true' ELSE 'false' END) AS BIT) 'TermCheck'
FROM TTerm t 
LEFT JOIN TYear y ON t.SchoolID = y.SchoolID AND t.nYear = y.nYear
LEFT JOIN TStudentClassroomHistory sch ON t.SchoolID = sch.SchoolID AND t.nTerm = sch.nTerm AND sch.cDel=0 AND sch.sID={2}
WHERE t.SchoolID={0} AND t.nYear IN (SELECT nYear FROM TTerm WHERE SchoolID={0} AND nTerm='{1}') AND t.cDel IS NULL
ORDER BY t.sTerm", schoolID, termID, sid);
                            termData = en.Database.SqlQuery<TermData>(query).ToList();

                            ltrTermView.Text = "";
                            paddingCheckbox = "";
                            foreach (var t in termData)
                            {
                                string currentTermStyle = string.Format(@"style=""font-weight: {0};""", t.TermID == termID ? "bold" : "normal");
                                string isCurrentTerm = string.Format(@"{0}", t.TermID == termID ? "true" : "false");
                                string isCurrentYear = string.Format(@"{0}", term != null && t.YearID == term.nYear ? "true" : "false");

                                //ltrTermView.Text += string.Format(@"
                                //    <div class=""form-check"" {5}>
                                //        <label>
                                //            <input type=""checkbox"" id=""chk{0}"" name=""chkTerm"" data-id=""{0}"" data-flag=""{1}"" data-current=""{7}"" data-current-year=""{8}"" class=""choose-term"" {4} />
                                //            <span class=""label-text"" {6}>{2}/{3}</span>
                                //        </label>
                                //    </div>", t.TermID, t.TermCheck ? "edit" : "new", t.Year, t.Term, t.TermCheck ? "checked" : "", paddingCheckbox, currentTermStyle, isCurrentTerm, isCurrentYear);

                                ltrTermView.Text += string.Format(@"
                            <div class=""form-check disabled form-check-inline"">
                                <label class=""form-check-label"" {6}>
                                    <input id=""chk{0}"" name=""chkTerm"" class=""form-check-input choose-term"" type=""checkbox"" data-id=""{0}"" data-flag=""{1}"" data-current=""{7}"" data-current-year=""{8}"" class=""choose-term"" {4} />
                                    {2}/{3}
                                    <span class=""form-check-sign"">
                                        <span class=""check""></span>
                                    </span>
                                </label>
                            </div>", t.TermID, t.TermCheck ? "edit" : "new", t.Year, t.Term, t.TermCheck || sid == 0 ? "checked" : "", paddingCheckbox, currentTermStyle, isCurrentTerm, isCurrentYear);

                                paddingCheckbox = string.Format(@" style=""margin-left: 15px;""");
                            }

                            if (!string.IsNullOrEmpty(ltrTermView.Text))
                            {
                                ltrTermView.Text += @"
                            <a href=""#"" data-toggle=""tooltip"" title=""ตัวหนาคือ 'ปีการศึกษา/เทอม' ที่เลือกหรือปัจจุบัน"" style=""font-size: 20px; margin: 7px;"">
                                <i class=""fa fa-question-circle"" aria-hidden=""true""></i>
                            </a>";
                            }
                        }
                        break;
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static string SaveItem(List<string> data)
        {
            string isComplete = "";
            bool flagComplete = true;

            string logMessage = "";
            int logAction = 0;

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                string[] key = data[0].Split('-');
                int stdID = int.Parse(key[0]); // sid
                string termID = key[1]; // tid

                string StudentID = data[1].Trim();
                string Identification = data[20];

                int? StudentStatus = string.IsNullOrEmpty(data[2]) ? (int?)null : int.Parse(data[2]);
                int? StudentNumber = string.IsNullOrEmpty(data[5]) ? (int?)null : int.Parse(data[5]);
                int? StudentClassRoom = string.IsNullOrEmpty(data[8]) ? (int?)null : int.Parse(data[8]);

                int[] sIDsSameIdentity = en.TUser.Where(w => w.SchoolID == schoolID && w.sID != stdID && w.cDel == null && (((w.sIdentification ?? "") != "" && (w.sIdentification != "-") && w.sIdentification == Identification) || ((w.sStudentID ?? "") != "" && (w.sStudentID != "-") && w.sStudentID == StudentID))).Select(s => s.sID).ToArray();
                bool schFoundStudy = false;
                int? schFoundStudyInYear = null;
                var schStudyList = en.TStudentClassroomHistories.Where(w => sIDsSameIdentity.Contains((int)w.sID) && w.cDel == false).ToList();
                foreach (var sid in sIDsSameIdentity)
                {
                    var studyStatus = schStudyList.Where(w => w.sID == sid).OrderByDescending(o => o.nHistoryId).FirstOrDefault();
                    if (studyStatus != null && (studyStatus.nStudentStatus ?? 0) == 0)
                    {
                        schFoundStudy = true;

                        var termObj = en.TTerms.Where(w => w.nTerm == studyStatus.nTerm).FirstOrDefault();
                        if (termObj != null)
                        {
                            var yearObj = en.TYears.Where(w => w.nYear == termObj.nYear).FirstOrDefault();
                            if (yearObj != null)
                            {
                                schFoundStudyInYear = yearObj.numberYear;
                            }
                        }

                        break;
                    }
                }

                // Check duplicate student number
                bool duplicateStudentNumber = false;
                string query = string.Format(@"SELECT COUNT(*) 
FROM TStudentClassroomHistory sch LEFT JOIN TUser u ON sch.SchoolID = u.SchoolID AND sch.sID = u.sID
WHERE sch.sID <> {0} AND sch.cDel = 0 AND sch.nTerm = '{1}' AND sch.nTermSubLevel2 = {2} AND sch.nStudentNumber = {3} AND u.cDel IS NULL
", stdID, termID, StudentClassRoom, StudentNumber ?? 0);
                int countDuplicateStudentNumber = en.Database.SqlQuery<int>(query).FirstOrDefault();
                duplicateStudentNumber = countDuplicateStudentNumber != 0 && StudentNumber != null;

                // Check exist StudentID in TBackupCard.BarCode
                int backupCardCount = 0;

                // [SB-8422] เปลี่ยนสถานะ นอกจากกำลังศึกษา สถานะเป็นอื่นๆ เช่น ลาออก จำหน่าย พ้นสภาพ อื่นๆ เป็นต้น ห้ามเปลี่ยนสถานะทุกกรณีหากมี เงินค้างในระบบ
                bool canChangeStudentStatus = true;

                int checkExistUsername = 0;

                if (stdID == 0)
                {
                    // Insert case
                    backupCardCount = en.TBackupCards.Where(w => w.SchoolID == schoolID && w.BarCode == StudentID && w.cDel == false).Count();

                    using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        checkExistUsername = dbMaster.TUsers.Where(w => w.nCompany == schoolID && w.cDel == null && ((w.username ?? "") != "" && (w.username != "-") && w.username == StudentID)).Count();
                    }
                }
                else
                {
                    // Edit case
                    JabjaiEntity.DB.TUser user = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == stdID).FirstOrDefault();
                    string oldStudentID = user.sStudentID;

                    backupCardCount = en.TBackupCards.Where(w => w.SchoolID == schoolID && w.BarCode != oldStudentID && w.BarCode == StudentID && w.cDel == false).Count();

                    // Change student status
                    canChangeStudentStatus = ((user.nMoney ?? 0) == 0 || StudentStatus == 0);

                    using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        checkExistUsername = dbMaster.TUsers.Where(w => w.nCompany == schoolID && w.sID != stdID && w.cDel == null && ((w.username ?? "") != "" && (w.username != "-") && w.username == StudentID)).Count();
                    }
                }

                // Check valid data
                string message = "";
                if (schFoundStudy)
                {
                    message += $", ไม่สามารถ{(stdID == 0 ? "เพิ่ม" : "แก้ไข")}รายการนี้ได้เพราะมีรายการซ้ำ [Duplicate Student ID: {StudentID} or Duplicate ID Card Number: {Identification} in this year: {(schFoundStudyInYear == null ? "null" : schFoundStudyInYear.ToString())}]";
                }
                if (backupCardCount > 0)
                {
                    message += $", ไม่สามารถ{(stdID == 0 ? "เพิ่ม" : "แก้ไข")}รายการนี้ได้เพราะมีรายการซ้ำ [Duplicate Barcode: {StudentID}]";
                }
                if (duplicateStudentNumber)
                {
                    message += $", ไม่สามารถ{(stdID == 0 ? "เพิ่ม" : "แก้ไข")}รายการนี้ได้เพราะมีรายการซ้ำ [Duplicate Student Number: {StudentNumber}]";
                }
                if (!canChangeStudentStatus)
                {
                    message += $", ปรับสถานะรายการนี้ไม่สำเร็จ [มียอดเงินคงเหลือในระบบ]";
                }

                if (!string.IsNullOrEmpty(message))
                {
                    isComplete = "warning-" + message.Remove(0, 2);
                    flagComplete = false;
                }

                if (flagComplete)
                {
                    using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    {

                        TCompany qCompany;
                        qCompany = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                        JabjaiEntity.DB.TUser pi = new JabjaiEntity.DB.TUser();
                        int stdIDMaster = 0;

                        try
                        {
                            //int? StudentStatus = string.IsNullOrEmpty(data[2]) ? (int?)null : int.Parse(data[2]);
                            DateTime? MoveInDate = string.IsNullOrEmpty(data[3]) ? (DateTime?)null : DateTime.Parse(data[3], new CultureInfo("th-TH"));
                            DateTime? DayQuit = string.IsNullOrEmpty(data[4]) ? (DateTime?)null : DateTime.Parse(data[4], new CultureInfo("th-TH"));
                            //int? StudentNumber = string.IsNullOrEmpty(data[5]) ? (int?)null : int.Parse(data[5]);
                            string Note = data[6];
                            int? StudentClass = string.IsNullOrEmpty(data[7]) ? (int?)null : int.Parse(data[7]);
                            //int? StudentClassRoom = string.IsNullOrEmpty(data[8]) ? (int?)null : int.Parse(data[8]);
                            string Sex = data[9];
                            string StudentTitle = data[10];
                            string Name = data[11];
                            Name = Name.Trim();
                            string LastName = data[12];
                            LastName = LastName.Trim();
                            string StudentNameEn = data[13];
                            string StudentLastEn = data[14];
                            string StudentNameOther = data[15];
                            string StudentLastOther = data[16];
                            string NickName = data[17];
                            string NickNameEn = data[18];
                            DateTime? Birthday = string.IsNullOrEmpty(data[19]) ? (DateTime?)null : DateTime.Parse(data[19], new CultureInfo("th-TH"));
                            //string Identification = data[20];
                            string StudentRace = data[21];
                            string StudentNation = data[22];
                            string StudentReligion = data[23];
                            string Phone = data[24];
                            string Email = data[25];
                            int? SonTotal = string.IsNullOrEmpty(data[26]) ? (int?)null : int.Parse(data[26]);
                            int? SonNumber = string.IsNullOrEmpty(data[27]) ? (int?)null : int.Parse(data[27]);
                            int? RelativeStudyHere = string.IsNullOrEmpty(data[28]) ? (int?)null : int.Parse(data[28]);

                            bool? RemoveOldPicture = string.IsNullOrEmpty(data[29]) ? (bool?)null : bool.Parse(data[29]); // Use edit mode only

                            bool UseBiometric = bool.Parse(data[30]);
                            string Note2 = data[31];
                            DateTime? ChangeRoomDate = string.IsNullOrEmpty(data[32]) ? (DateTime?)null : DateTime.Parse(data[32], new CultureInfo("th-TH"));

                            string DisabilityCode = data[33];
                            string DisadvantageCode = data[34];
                            decimal? nMax = string.IsNullOrEmpty(data[35]) ? (decimal?)null : decimal.Parse(data[35]);

                            int? JourneyType = string.IsNullOrEmpty(data[36]) ? (int?)null : int.Parse(data[36]);
                            string DormitoryName = null;
                            if (JourneyType == 2) DormitoryName = data[37];

                            int? DropOutType = string.IsNullOrEmpty(data[38]) ? (int?)null : int.Parse(data[38]);

                            string termDatas = data[39];
                            List<TermData> termObjects = JsonConvert.DeserializeObject<List<TermData>>(termDatas);

                            string PassportNumber = data[40];
                            string PassportCountry = data[41];
                            DateTime? PassportExpirationDate = string.IsNullOrEmpty(data[42]) ? (DateTime?)null : DateTime.Parse(data[42], new CultureInfo("th-TH"));

                            StudentLogic studentLogic = new StudentLogic(en);

                            string cDelete = null;

                            switch (StudentStatus)
                            {
                                case 0:
                                    DayQuit = null;
                                    Note = null;
                                    break;
                                case 4:
                                    cDelete = "G";
                                    break;
                            }
                            //if (HttpContext.Current.Session[SessionPrimaryKey] == null)
                            if (stdID == 0)
                            {
                                // Insert Section

                                // Access & Save data to Master Database
                                MasterEntity.TUser userMasterData = new MasterEntity.TUser
                                {
                                    //sID = stdIDMaster,
                                    //nSystemID = stdID,
                                    nCompany = schoolID,
                                    sName = Name,
                                    sLastname = LastName,
                                    sIdentification = Identification,
                                    cSex = Sex,
                                    sPhone = Phone,
                                    sEmail = Email,
                                    //sPassword = ComFunction.RandomNumber(),
                                    sPassword = "999999",
                                    dBirth = Birthday,
                                    dCreate = DateTime.Now.FixSecondAndMillisecond(2, 10),
                                    cType = "0",
                                    cDel = cDelete,
                                    username = StudentID,
                                    userpassword = (Birthday != null ? Birthday.Value.ToString("ddMMyyyy") : "00000000"),
                                    PasswordHash = ComFunction.HashSHA1((Birthday != null ? Birthday.Value.ToString("ddMMyyyy") : "00000000")),
                                    UseEncryptPassword = false,
                                    UseBiometric = UseBiometric
                                };
                                dbMaster.TUsers.Add(userMasterData);
                                dbMaster.SaveChanges();

                                stdIDMaster = userMasterData.sID;
                                userMasterData.nSystemID = stdIDMaster;
                                dbMaster.SaveChanges();


                                stdID = stdIDMaster;

                                // Get Item
                                pi = new JabjaiEntity.DB.TUser
                                {
                                    sID = stdID,
                                    sStudentID = StudentID,
                                    nStudentStatus = StudentStatus,
                                    moveInDate = MoveInDate,
                                    DayQuit = DayQuit,
                                    nStudentNumber = StudentNumber,
                                    Note = Note,
                                    nTermSubLevel2 = StudentClassRoom,
                                    cSex = Sex,
                                    sStudentTitle = StudentTitle,
                                    sName = Name,
                                    sLastname = LastName,
                                    sStudentNameEN = StudentNameEn,
                                    sStudentLastEN = StudentLastEn,
                                    sStudentNameOther = StudentNameOther,
                                    sStudentLastOther = StudentLastOther,
                                    sNickName = NickName,
                                    sNickNameEN = NickNameEn,
                                    dBirth = Birthday,
                                    sIdentification = Identification,
                                    sStudentRace = StudentRace,
                                    sStudentNation = StudentNation,
                                    sStudentReligion = StudentReligion,
                                    sPhone = Phone,
                                    sEmail = Email,
                                    nSonNumber = SonNumber,
                                    Note2 = Note2,
                                    cDel = cDelete,
                                    DisabilityCode = DisabilityCode,
                                    DisadvantageCode = DisadvantageCode,
                                    nMax = nMax,
                                    cType = "0",
                                    JourneyType = JourneyType,
                                    DormitoryName = DormitoryName,

                                    PassportNumber = PassportNumber,
                                    PassportCountry = PassportCountry,
                                    PassportExpirationDate = PassportExpirationDate,

                                    dUpdate = DateTime.Now.FixSecondAndMillisecond(2, 10),
                                    SchoolID = schoolID,

                                    CreatedBy = userData.UserID,
                                    CreatedDate = DateTime.Now.FixSecondAndMillisecond(2, 10)
                                };
                                en.TUser.Add(pi);
                                en.SaveChanges();


                                // Update some field TFamilyProfile
                                var fmlID = 0;
                                TFamilyProfile f = new TFamilyProfile
                                {
                                    sID = stdID,
                                    //nFamilyID = fmlID,
                                    nSonTotal = SonTotal,
                                    nRelativeStudyHere = RelativeStudyHere,

                                    sDeleted = "false",
                                    SchoolID = schoolID
                                };

                                en.TFamilyProfiles.Add(f);

                                en.SaveChanges();

                                fmlID = f.nFamilyID;

                                // Save data to Student Classroom History
                                //string currentTermID = studentLogic.GetTermId(userData);
                                //var termData = en.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                                //var termList = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == termData.nYear && w.cDel == null).ToList();
                                //foreach (var term in termList)
                                //{
                                //    var studentClassRoom = new TStudentClassroomHistory
                                //    {
                                //        sID = stdID,
                                //        nTerm = term.nTerm,
                                //        nTermSubLevel2 = StudentClassRoom,
                                //        SchoolID = schoolID,
                                //        CreatedBy = userData.UserID,
                                //        CreatedDate = DateTime.Now.FixSecond(2).FixMillisecond(11),
                                //        nStudentStatus = StudentStatus,
                                //        nStudentNumber = StudentNumber,
                                //        DropOutType = DropOutType,
                                //        cDel = false
                                //    };

                                //    en.TStudentClassroomHistories.Add(studentClassRoom);
                                //    en.SaveChanges();
                                //}

                                string currentTermID = "";
                                foreach (var term in termObjects)
                                {
                                    if (term.Current)
                                    {
                                        currentTermID = term.TermID;
                                    }

                                    if (term.TermCheck && term.Flag == "new")
                                    {
                                        var studentClassRoom = new TStudentClassroomHistory
                                        {
                                            sID = stdID,
                                            nTerm = term.TermID,
                                            nTermSubLevel2 = StudentClassRoom,
                                            SchoolID = schoolID,
                                            CreatedBy = userData.UserID,
                                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(2, 11),
                                            nStudentStatus = StudentStatus,
                                            nStudentNumber = StudentNumber,
                                            DropOutType = DropOutType,
                                            cDel = false
                                        };

                                        en.TStudentClassroomHistories.Add(studentClassRoom);
                                        en.SaveChanges();
                                    }
                                }

                                isComplete = "complete-" + stdID + "-" + fmlID + "-" + currentTermID;

                                logMessage = "เพิ่มข้อมูลนักเรียน(ประวัติส่วนตัว) [รหัส: " + stdID + ", ชื่อ-นามสกุล: " + Name + " " + LastName + "]";
                                logAction = 2;
                            }
                            else
                            {
                                // Modify Section

                                bool changeClassRoom = false;
                                bool changeStudentStatus = true;
                                bool changeStudentNumber = true;
                                bool changeDropOutType = true;
                                bool changeMoveInDate = false;

                                // Access to Master Database
                                MasterEntity.TUser userMasterData = dbMaster.TUsers.Where(w => w.sID == stdID && w.nCompany == qCompany.nCompany && w.cType == "0").FirstOrDefault();
                                if (userMasterData != null)
                                {
                                    userMasterData.sName = Name;
                                    userMasterData.sLastname = LastName;
                                    userMasterData.sIdentification = Identification;
                                    userMasterData.sPhone = Phone;
                                    userMasterData.cSex = Sex;
                                    userMasterData.sEmail = Email;
                                    userMasterData.dBirth = Birthday;
                                    userMasterData.cDel = cDelete;
                                    userMasterData.username = StudentID;
                                    userMasterData.UseBiometric = UseBiometric;
                                    if (RemoveOldPicture != null && RemoveOldPicture.Value)
                                    {
                                        userMasterData.sPicture = null;
                                    }
                                    userMasterData.dUpdate = DateTime.Now.FixSecondAndMillisecond(2, 12);
                                }
                                else
                                {
                                    userMasterData = new MasterEntity.TUser
                                    {
                                        //sID = stdIDMaster,
                                        //nSystemID = stdID,
                                        nCompany = schoolID,
                                        sName = Name,
                                        sLastname = LastName,
                                        sIdentification = Identification,
                                        cSex = Sex,
                                        sPhone = Phone,
                                        sEmail = Email,
                                        //sPassword = ComFunction.RandomNumber(),
                                        sPassword = "999999",
                                        dBirth = Birthday,
                                        dCreate = DateTime.Now.FixSecondAndMillisecond(2, 10),
                                        cType = "0",
                                        cDel = cDelete,
                                        username = StudentID,
                                        userpassword = (Birthday != null ? Birthday.Value.ToString("ddMMyyyy") : "00000000"),
                                        PasswordHash = ComFunction.HashSHA1((Birthday != null ? Birthday.Value.ToString("ddMMyyyy") : "00000000")),
                                        UseBiometric = UseBiometric
                                    };
                                    dbMaster.TUsers.Add(userMasterData);
                                }
                                dbMaster.SaveChanges();

                                stdIDMaster = userMasterData.sID;
                                userMasterData.nSystemID = stdIDMaster;
                                dbMaster.SaveChanges();

                                // Get Item
                                pi = en.TUser.First(f => f.SchoolID == schoolID && f.sID == stdID);

                                int? StudentOldClassRoom = pi.nTermSubLevel2;

                                // Get old class room in term
                                var schOldClassRoom = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == stdID && w.nTerm == termID && w.cDel == false).FirstOrDefault();
                                if (schOldClassRoom != null)
                                {
                                    StudentOldClassRoom = schOldClassRoom.nTermSubLevel2;
                                    changeStudentStatus = (schOldClassRoom.nStudentStatus != StudentStatus);
                                    changeDropOutType = (schOldClassRoom.DropOutType != DropOutType);
                                    changeStudentNumber = (schOldClassRoom.nStudentNumber != StudentNumber);
                                    changeMoveInDate = (schOldClassRoom.MoveInDate != MoveInDate);
                                }

                                changeClassRoom = (StudentOldClassRoom != StudentClassRoom);
                                //changeStudentStatus = (pi.nStudentStatus != StudentStatus);
                                //changeStudentNumber = (pi.nStudentNumber != StudentNumber);

                                pi.sStudentID = StudentID;
                                pi.nStudentStatus = StudentStatus;
                                pi.moveInDate = MoveInDate;
                                pi.DayQuit = DayQuit;
                                pi.nStudentNumber = StudentNumber;
                                pi.Note = Note;
                                pi.nTermSubLevel2 = StudentClassRoom;
                                pi.cSex = Sex;
                                pi.sStudentTitle = StudentTitle;
                                pi.sName = Name;
                                pi.sLastname = LastName;
                                pi.sStudentNameEN = StudentNameEn;
                                pi.sStudentLastEN = StudentLastEn;
                                pi.sStudentNameOther = StudentNameOther;
                                pi.sStudentLastOther = StudentLastOther;
                                pi.sNickName = NickName;
                                pi.sNickNameEN = NickNameEn;
                                pi.dBirth = Birthday;
                                pi.sIdentification = Identification;
                                pi.sStudentRace = StudentRace;
                                pi.sStudentNation = StudentNation;
                                pi.sStudentReligion = StudentReligion;
                                pi.sPhone = Phone;
                                pi.sEmail = Email;
                                pi.nSonNumber = SonNumber;
                                pi.Note2 = Note2;
                                if (RemoveOldPicture != null && RemoveOldPicture.Value)
                                {
                                    pi.sStudentPicture = null;
                                }
                                pi.cDel = cDelete;
                                pi.DisabilityCode = DisabilityCode;
                                pi.DisadvantageCode = DisadvantageCode;
                                pi.nMax = nMax;
                                pi.JourneyType = JourneyType;
                                pi.DormitoryName = DormitoryName;

                                pi.PassportNumber = PassportNumber;
                                pi.PassportCountry = PassportCountry;
                                pi.PassportExpirationDate = PassportExpirationDate;

                                pi.UpdatedBy = userData.UserID;
                                pi.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(2, 12);

                                // Update some field TFamilyProfile
                                var fmlID = 0;
                                int cf = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == stdID && w.sDeleted == "false").Count();
                                if (cf == 0)
                                {
                                    //int fmlID = (int)(en.TFamilyProfiles.Count() == 0 ? 1 : en.TFamilyProfiles.Max(m => m.nFamilyID) + 1);
                                    TFamilyProfile f = new TFamilyProfile
                                    {
                                        sID = stdID,
                                        //nFamilyID = fmlID,
                                        nSonTotal = SonTotal,
                                        nRelativeStudyHere = RelativeStudyHere,

                                        sDeleted = "false",
                                        SchoolID = schoolID,

                                        CreatedBy = userData.UserID,
                                        CreatedDate = DateTime.Now.FixSecondAndMillisecond(2, 10)
                                    };

                                    en.TFamilyProfiles.Add(f);

                                    en.SaveChanges();

                                    fmlID = f.nFamilyID;
                                }
                                else
                                {
                                    TFamilyProfile fi = en.TFamilyProfiles.First(f => f.SchoolID == schoolID && f.sID == stdID && f.sDeleted == "false");

                                    fi.nSonTotal = SonTotal;
                                    fi.nRelativeStudyHere = RelativeStudyHere;
                                    fi.UpdatedBy = userData.UserID;
                                    fi.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(2, 12);

                                    en.SaveChanges();

                                    fmlID = fi.nFamilyID;
                                }


                                // Save data to Student Classroom History
                                // New or Delete

                                string[] arTermCheck = termObjects.Where(w => w.TermCheck == true).Select(s => s.TermID).ToArray();

                                foreach (var term in termObjects)
                                {
                                    if (term.TermCheck && term.Flag == "new")
                                    {
                                        TStudentClassroomHistory sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == stdID && w.nTerm == term.TermID && w.cDel == false).FirstOrDefault();
                                        if (sch == null)
                                        {
                                            sch = new TStudentClassroomHistory
                                            {
                                                sID = stdID,
                                                nTerm = term.TermID,
                                                nTermSubLevel2 = StudentClassRoom,
                                                SchoolID = schoolID,
                                                CreatedBy = userData.UserID,
                                                CreatedDate = DateTime.Now.FixSecondAndMillisecond(2, 14),
                                                nStudentStatus = StudentStatus,
                                                nStudentNumber = StudentNumber,
                                                DropOutType = DropOutType,
                                                cDel = false
                                            };

                                            en.TStudentClassroomHistories.Add(sch);

                                            en.SaveChanges();
                                        }
                                    }
                                    else if (term.Flag == "edit") // && term.CurrentYear // MO - 27/10/2565
                                    {
                                        if (!term.TermCheck)
                                        {
                                            // Check Out
                                            TStudentClassroomHistory sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == stdID && w.nTerm == term.TermID && w.cDel == false).FirstOrDefault();
                                            if (sch != null)
                                            {
                                                sch.cDel = true;
                                                sch.UpdatedBy = userData.UserID;
                                                sch.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(2, 101);

                                                en.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            // Check In or original
                                            TStudentClassroomHistory sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == stdID && w.nTerm == term.TermID && w.cDel == false).FirstOrDefault();
                                            if (sch != null)
                                            {
                                                sch.MoveInDate = MoveInDate;
                                                sch.UpdatedBy = userData.UserID;
                                                sch.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(2, 105);

                                                if (term.TermID == termID && (StudentStatus != 0 && StudentStatus != 4))
                                                {
                                                    sch.MoveOutDate = DayQuit;
                                                    sch.Note = Note;
                                                }

                                                en.SaveChanges();
                                            }
                                        }
                                    }
                                }


                                if (changeClassRoom)
                                {
                                    string currentTermID = studentLogic.GetTermId(userData);
                                    var currentTermData = en.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                                    var termData = en.TTerms.Where(w => w.nTerm == termID).FirstOrDefault();
                                    //if (currentTermData.nYear == termData.nYear)
                                    //{
                                    // Can edit in academic year
                                    var termList = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == termData.nYear && w.dStart >= termData.dStart && w.cDel == null && arTermCheck.Contains(w.nTerm)).ToList();
                                    foreach (var term in termList)
                                    {
                                        TStudentClassroomHistory sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == stdID && w.nTerm == term.nTerm && w.cDel == false).FirstOrDefault();
                                        if (sch == null)
                                        {
                                            sch = new TStudentClassroomHistory
                                            {
                                                sID = stdID,
                                                nTerm = term.nTerm,
                                                nTermSubLevel2 = StudentClassRoom,
                                                SchoolID = schoolID,
                                                CreatedBy = userData.UserID,
                                                CreatedDate = DateTime.Now.FixSecondAndMillisecond(2, 12),
                                                nStudentNumber = StudentNumber,
                                                MoveInDate = MoveInDate,
                                                cDel = false
                                            };

                                            en.TStudentClassroomHistories.Add(sch);
                                        }
                                        else
                                        {
                                            sch.nTermSubLevel2 = StudentClassRoom;
                                            sch.UpdatedBy = userData.UserID;
                                            sch.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(2, 110);
                                            sch.nStudentNumber = StudentNumber;
                                            sch.MoveInDate = MoveInDate;
                                            sch.cDel = false;
                                        }

                                        en.SaveChanges();
                                    }

                                    TRoomChange roomChange = new TRoomChange
                                    {
                                        RoomChangeID = Guid.NewGuid().ToString(),
                                        sID = stdID,
                                        Level2Old = StudentOldClassRoom,
                                        Level2New = StudentClassRoom,
                                        DayChange = ChangeRoomDate,
                                        SchoolID = schoolID,
                                        CreatedBy = userData.UserID,
                                        CreatedDate = DateTime.Now.FixSecondAndMillisecond(2, 110)
                                    };
                                    en.TRoomChanges.Add(roomChange);
                                    en.SaveChanges();
                                    //}
                                }

                                // Change StudentStatus
                                if (changeStudentStatus)
                                {
                                    // List all term in year
                                    var schoolTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList();

                                    var termData = schoolTerm.Where(w => w.nTerm == termID).FirstOrDefault();
                                    if (termData != null)
                                    {
                                        int? yearID = termData.nYear;
                                        var terms = schoolTerm.Where(w => w.nYear == yearID).ToList();

                                        // Check curr term; is term 1 or term 2; check from dStart
                                        terms = schoolTerm.Where(w => w.nYear == yearID && w.dStart >= termData.dStart && arTermCheck.Contains(w.nTerm)).ToList();
                                        foreach (var t in terms)
                                        {
                                            TStudentClassroomHistory sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == stdID && w.nTerm == t.nTerm && w.cDel == false).FirstOrDefault();
                                            if (sch == null)
                                            {
                                                sch = new TStudentClassroomHistory
                                                {
                                                    sID = stdID,
                                                    nTerm = t.nTerm,
                                                    nTermSubLevel2 = StudentClassRoom,
                                                    SchoolID = schoolID,
                                                    CreatedBy = userData.UserID,
                                                    CreatedDate = DateTime.Now.FixSecondAndMillisecond(2, 13),
                                                    nStudentStatus = StudentStatus,
                                                    nStudentNumber = StudentNumber,
                                                    MoveInDate = MoveInDate,
                                                    cDel = false
                                                };

                                                // 1: จำหน่าย, 2: ลาออก, 3: พักการเรียน, 5: ขาดการติดต่อ, 6: พ้นสภาพ, 7: นักเรียนไปโครงการ
                                                if (StudentStatus != 0 && StudentStatus != 4)
                                                {
                                                    if (sch.nTerm == termID)
                                                    {
                                                        sch.MoveOutDate = DayQuit;
                                                        sch.Note = Note;
                                                    }
                                                    else
                                                    {
                                                        sch.MoveOutDate = null;
                                                        sch.Note = null;
                                                    }
                                                    // 1: ลบออกจากปีปัจจุบัน(Set cDel=1), 2: คงไว้ในปีปัจจุบัน(Set cDel=0)
                                                    sch.DropOutType = DropOutType;
                                                    switch (DropOutType)
                                                    {
                                                        case 1: sch.cDel = true; break;
                                                        case 2: sch.cDel = false; break;
                                                    }
                                                }
                                                else
                                                {
                                                    // Clear DropOutType
                                                    sch.DropOutType = null;
                                                    sch.cDel = false;

                                                    // SB-8875, SB-9335
                                                    if (StudentStatus == 0)
                                                    {
                                                        sch.MoveOutDate = null;
                                                        sch.Note = null;
                                                    }
                                                }

                                                en.TStudentClassroomHistories.Add(sch);
                                            }
                                            else
                                            {
                                                sch.nTermSubLevel2 = StudentClassRoom;
                                                sch.UpdatedBy = userData.UserID;
                                                sch.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(2, 115);
                                                sch.nStudentStatus = StudentStatus;
                                                sch.nStudentNumber = StudentNumber;
                                                sch.MoveInDate = MoveInDate;
                                                sch.cDel = false;

                                                // 1: จำหน่าย, 2: ลาออก, 3: พักการเรียน, 5: ขาดการติดต่อ, 6: พ้นสภาพ, 7: นักเรียนไปโครงการ
                                                if (StudentStatus != 0 && StudentStatus != 4)
                                                {
                                                    if (sch.nTerm == termID)
                                                    {
                                                        sch.MoveOutDate = DayQuit;
                                                        sch.Note = Note;
                                                    }
                                                    else
                                                    {
                                                        sch.MoveOutDate = null;
                                                        sch.Note = null;
                                                    }
                                                    // 1: ลบออกจากปีปัจจุบัน(Set cDel=1), 2: คงไว้ในปีปัจจุบัน(Set cDel=0)
                                                    sch.DropOutType = DropOutType;
                                                    switch (DropOutType)
                                                    {
                                                        case 1: sch.cDel = true; break;
                                                        case 2: sch.cDel = false; break;
                                                    }
                                                }
                                                else
                                                {
                                                    // Clear DropOutType
                                                    sch.DropOutType = null;
                                                    sch.cDel = false;

                                                    // SB-8875, SB-9335
                                                    if (StudentStatus == 0)
                                                    {
                                                        sch.MoveOutDate = null;
                                                        sch.Note = null;
                                                    }
                                                }
                                            }

                                            en.SaveChanges();
                                        }
                                    }
                                }
                                else
                                {
                                    // Change DropOutType
                                    if (changeDropOutType)
                                    {
                                        // List all term in year
                                        var schoolTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList();

                                        var termData = schoolTerm.Where(w => w.nTerm == termID).FirstOrDefault();
                                        if (termData != null)
                                        {
                                            int? yearID = termData.nYear;
                                            var terms = schoolTerm.Where(w => w.nYear == yearID).ToList();

                                            // Check curr term; is term 1 or term 2; check from dStart
                                            terms = schoolTerm.Where(w => w.nYear == yearID && w.dStart >= termData.dStart && arTermCheck.Contains(w.nTerm)).ToList();
                                            foreach (var t in terms)
                                            {
                                                TStudentClassroomHistory sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == stdID && w.nTerm == t.nTerm && w.cDel == false).FirstOrDefault();
                                                if (sch != null)
                                                {
                                                    sch.UpdatedBy = userData.UserID;
                                                    sch.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(2, 120);

                                                    if (StudentStatus != 0 && StudentStatus != 4)
                                                    {
                                                        // 1: ลบออกจากปีปัจจุบัน(Set cDel=1), 2: คงไว้ในปีปัจจุบัน(Set cDel=0)
                                                        sch.DropOutType = DropOutType;
                                                        switch (DropOutType)
                                                        {
                                                            case 1: sch.cDel = true; break;
                                                            case 2: sch.cDel = false; break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // Clear DropOutType
                                                        sch.DropOutType = null;
                                                        sch.cDel = false;

                                                        // SB-8875, SB-9335
                                                        if (StudentStatus == 0)
                                                        {
                                                            sch.MoveOutDate = null;
                                                            sch.Note = null;
                                                        }
                                                    }
                                                }

                                                en.SaveChanges();
                                            }
                                        }
                                    }
                                }

                                // Change StudentNumber
                                if (changeStudentNumber)
                                {
                                    TStudentClassroomHistory sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == stdID && w.nTerm == termID && w.cDel == false).FirstOrDefault();
                                    if (sch != null)
                                    {
                                        sch.nStudentNumber = StudentNumber;
                                        sch.UpdatedBy = userData.UserID;
                                        sch.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(2, 125);

                                        en.SaveChanges();
                                    }
                                }

                                // Change MoveInDate (SB-9294)
                                int noOfRowUpdatedChangeMoveInDate = 0;
                                if (changeMoveInDate)
                                {
                                    string queryUpdateMoveInDate = string.Format(@"UPDATE TStudentClassroomHistory SET MoveInDate={0}, UpdatedDate='{1:yyyy-MM-dd HH:mm:02.200}' WHERE sID={2} AND cDel=0 AND nStudentStatus=0", (MoveInDate == null ? "NULL" : string.Format(@"'{0:yyyy-MM-dd} 00:00:00.000'", MoveInDate)), DateTime.Now, stdID);
                                    noOfRowUpdatedChangeMoveInDate = en.Database.ExecuteSqlCommand(queryUpdateMoveInDate);
                                }


                                // Update Invoice Data
                                using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                                {
                                    //var queryUpdate = @"UPDATE TInvoices SET StudentCode = @StudentCode, StudentName = @StudentName WHERE school_id = @school_id AND student_id = @student_id";
                                    //int noOfRowUpdated = peakengine.Database.ExecuteSqlCommand(
                                    //    queryUpdate,
                                    //    new SqlParameter("@StudentCode", StudentID),
                                    //    new SqlParameter("@StudentName", Name + ' ' + LastName),
                                    //    new SqlParameter("@school_id", schoolID),
                                    //    new SqlParameter("@student_id", stdID)
                                    //    );

                                    var sql_update_student = $@"update b set
                                                                b.StudentName='{Name} {LastName}',
                                                                b.StudentCode='{StudentID}'
                                                                from AccountingDB.dbo.AccountInvoice a inner join AccountingDB.dbo.AccountInvoiceStudent b on a.AccountInvoiceId = b.AccountInvoiceId 
                                                                where a.SchoolId={schoolID} and b.StudentId={stdID}";
                                    peakengine.Database.ExecuteSqlCommand(sql_update_student);
                                }

                                isComplete = "complete-" + stdID + "-" + fmlID + "-" + termID;

                                // ACT:00001(4) - ACT:[changeClassRoom][changeStudentStatus][changeStudentNumber][changeDropOutType][changeMoveInDate](noOfRowUpdatedChangeMoveInDate)
                                logMessage = string.Format(@"อัปเดตข้อมูลนักเรียน(ประวัติส่วนตัว) [รหัส: {0}, ชื่อ-นามสกุล: {1} {2}][ACT:{3}{4}{5}{6}{7}({8})]", stdID, Name, LastName, (changeClassRoom ? "1" : "0"), (changeStudentStatus ? "1" : "0"), (changeStudentNumber ? "1" : "0"), (changeDropOutType ? "1" : "0"), (changeMoveInDate ? "1" : "0"), noOfRowUpdatedChangeMoveInDate);
                                logAction = 3;
                            }



                            Accounting.Invoices invoices = new Accounting.Invoices(en);
                            invoices.UpdateAccountingInfo(pi);


                            JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(schoolID, stdIDMaster + "", "0", "");
                            //var f_usermaster = dbMaster.TUsers.Find(stdIDMaster);
                            var f_usermaster = dbMaster.TUsers.Where(w => w.sID == stdIDMaster).FirstOrDefault();
                            if (f_usermaster != null)
                            {
                                UpdateMemory memory = new UpdateMemory(userData.AuthKey, userData.AuthValue);
                                memory.Student(pi, f_usermaster);
                            }
                        }
                        catch (Exception err)
                        {
                            isComplete = "error-" + err.Message + " :line " + ComFunction.GetLineNumberError(err);

                            flagComplete = false;



                            string logMessagePattern = @"[SchoolEntities:{0}], [StudentData:{1}], [ErrorLine:{2}], [ErrorMessage:{3}]";
                            string errorMessage = err.Message;
                            string innerExceptionMessage = "";
                            while (err.InnerException != null) { innerExceptionMessage += ", " + err.InnerException.Message; err = err.InnerException; }
                            string logMessageDebug = string.Format(logMessagePattern, userData.Entities, new JavaScriptSerializer().Serialize(data), ComFunction.GetLineNumberError(err), errorMessage + ", " + innerExceptionMessage);

                            int? sEmpID = userData.UserID;

                            ComFunction.InsertLogDebug(null, null, sEmpID, logMessageDebug);
                        }

                    }
                }
            }

            if (flagComplete)
            {
                database.InsertLog(userData.UserID.ToString(), logMessage, HttpContext.Current.Request, 14, logAction, 0, schoolID);
            }

            return isComplete;
        }

        [WebMethod(EnableSession = true)]
        public static string GetItem(string stdID, string termID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    JabjaiEntity.DB.TUser p = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == iStdID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 43; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        //HttpContext.Current.Session[SessionPrimaryKey] = p.sID;
                        //HttpContext.Current.Session[SessionPrimaryKeyTermID] = termID;

                        int fmlID = 0;
                        var fmlProfile = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == p.sID && w.sDeleted == "false").FirstOrDefault();
                        if (fmlProfile != null)
                        {
                            fmlID = fmlProfile.nFamilyID;
                        }

                        var studentStatus = p.nStudentStatus == null ? "0" : p.nStudentStatus.ToString();
                        int? nTermSubLevel2 = p.nTermSubLevel2;
                        int? nStudentNumber = null;
                        int? DropOutType = null;
                        DateTime? MoveInDate = null;
                        DateTime? MoveOutDate = null;
                        string Note = "";

                        TStudentClassroomHistory studentClassroomHistory = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.nTerm == termID && w.cDel == false).FirstOrDefault();
                        if (studentClassroomHistory != null)
                        {
                            studentStatus = studentClassroomHistory.nStudentStatus == null ? "0" : studentClassroomHistory.nStudentStatus.ToString();
                            nTermSubLevel2 = studentClassroomHistory.nTermSubLevel2;
                            nStudentNumber = studentClassroomHistory.nStudentNumber;
                            DropOutType = studentClassroomHistory.DropOutType;
                            MoveInDate = studentClassroomHistory.MoveInDate;
                            MoveOutDate = studentClassroomHistory.MoveOutDate;
                            Note = studentClassroomHistory.Note;
                            if (studentStatus != "0" && studentStatus != "4" && (MoveOutDate == null || string.IsNullOrEmpty(Note)))
                            {
                                var termObj1 = en.TTerms.Where(w => w.nTerm == termID).FirstOrDefault();
                                int? yearID = 0;
                                if (termObj1 != null)
                                {
                                    yearID = termObj1.nYear;
                                }
                                var termObj2 = en.TTerms.Where(w => w.nTerm != termID && w.nYear == yearID).FirstOrDefault();
                                if (termObj2 != null)
                                {
                                    TStudentClassroomHistory studentClassroomHistory2 = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.nTerm == termObj2.nTerm && w.cDel == false).FirstOrDefault();
                                    if (studentClassroomHistory2 != null)
                                    {
                                        MoveOutDate = studentClassroomHistory2.MoveOutDate;
                                        Note = studentClassroomHistory2.Note;
                                    }
                                }
                            }
                        }

                        if (!MoveInDate.HasValue)
                        {
                            MoveInDate = p.moveInDate;
                        }

                        dt.Rows[0]["F0"] = fmlID;
                        dt.Rows[0]["F1"] = p.sStudentID;
                        dt.Rows[0]["F2"] = studentStatus;
                        dt.Rows[0]["F3"] = MoveInDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F4"] = MoveOutDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F5"] = nStudentNumber?.ToString();
                        dt.Rows[0]["F6"] = Note;
                        dt.Rows[0]["F7"] = null;

                        //dt.Rows[0]["F8"] = p.nTermSubLevel2?.ToString();
                        dt.Rows[0]["F8"] = nTermSubLevel2?.ToString();
                        var level = en.TTermSubLevel2.Where(w => w.SchoolID == schoolID && w.nTermSubLevel2 == nTermSubLevel2).FirstOrDefault();
                        if (level != null)
                        {
                            dt.Rows[0]["F7"] = level.nTSubLevel;
                        }
                        dt.Rows[0]["F9"] = p.cSex;
                        string studentTitle = null;
                        var titleList = en.TTitleLists.Where(w => w.SchoolID == schoolID && (w.nTitleid.ToString() == p.sStudentTitle || w.titleDescription == p.sStudentTitle) && w.deleted != "1" && w.workStatus == "working").FirstOrDefault();
                        if (titleList != null)
                        {
                            studentTitle = titleList.nTitleid.ToString();
                        }
                        dt.Rows[0]["F10"] = studentTitle;
                        dt.Rows[0]["F11"] = p.sName;
                        dt.Rows[0]["F12"] = p.sLastname;
                        dt.Rows[0]["F13"] = p.sStudentNameEN;
                        dt.Rows[0]["F14"] = p.sStudentLastEN;
                        dt.Rows[0]["F15"] = p.sStudentNameOther;
                        dt.Rows[0]["F16"] = p.sStudentLastOther;
                        dt.Rows[0]["F17"] = p.sNickName;
                        dt.Rows[0]["F18"] = p.sNickNameEN;
                        dt.Rows[0]["F19"] = p.dBirth?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F20"] = p.sIdentification;
                        dt.Rows[0]["F21"] = p.sStudentRace;
                        dt.Rows[0]["F22"] = p.sStudentNation;
                        dt.Rows[0]["F23"] = p.sStudentReligion;
                        dt.Rows[0]["F24"] = p.sPhone;
                        dt.Rows[0]["F25"] = p.sEmail;
                        dt.Rows[0]["F26"] = "0";
                        dt.Rows[0]["F27"] = p.nSonNumber?.ToString();
                        dt.Rows[0]["F28"] = null;
                        dt.Rows[0]["F29"] = p.sStudentPicture;

                        bool useBiometric = false;
                        string originalStudentPicture = "";
                        using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                        {

                            var userMaster = dbMaster.TUsers.Where(w => w.sID == p.sID && w.nCompany == schoolID && w.cType == "0").FirstOrDefault();
                            if (userMaster != null)
                            {
                                useBiometric = userMaster.UseBiometric;

                                originalStudentPicture = ComFunction.GetImageAsBase64Url(userMaster.sPicture);
                            }
                            dt.Rows[0]["F30"] = useBiometric;

                            TFamilyProfile familyProfile = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                            if (familyProfile != null)
                            {
                                dt.Rows[0]["F26"] = familyProfile.nSonTotal == null ? "0" : familyProfile.nSonTotal.ToString();
                                dt.Rows[0]["F28"] = familyProfile.nRelativeStudyHere?.ToString();
                            }
                            dt.Rows[0]["F31"] = p.Note2;
                            dt.Rows[0]["F32"] = ""; // ChangeRoomDate
                            dt.Rows[0]["F33"] = string.IsNullOrEmpty(p.DisabilityCode) ? "99" : p.DisabilityCode;
                            dt.Rows[0]["F34"] = string.IsNullOrEmpty(p.DisadvantageCode) ? "99" : p.DisadvantageCode;
                            dt.Rows[0]["F35"] = p.nMax == null ? "0.00" : p.nMax.Value.ToString("0.00");

                            dt.Rows[0]["F36"] = p.JourneyType == null ? "" : p.JourneyType.Value.ToString();
                            dt.Rows[0]["F37"] = p.DormitoryName;
                            dt.Rows[0]["F38"] = DropOutType == null ? "" : DropOutType.Value.ToString();
                            dt.Rows[0]["F39"] = ""; // term data
                            dt.Rows[0]["F40"] = originalStudentPicture;

                            dt.Rows[0]["F41"] = p.PassportNumber;
                            dt.Rows[0]["F42"] = p.PassportCountry;
                            dt.Rows[0]["F43"] = p.PassportExpirationDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

                            // Check verified email & phone number
                            List<TUserVerify> userVerifies = en.TUserVerifies.Where(w => w.SchoolID == schoolID && w.UserID == iStdID && w.Status == 1).ToList();
                            if (userVerifies.Count > 0)
                            {
                                dt.Rows.Add();

                                if (userVerifies.Count(c => c.Type == 1 && c.Email == p.sEmail?.Trim()) > 0)
                                {
                                    dt.Rows[1]["F25"] = true;
                                }
                                if (userVerifies.Count(c => c.Type == 2 && c.PhoneNumber == p.sPhone?.Trim()) > 0)
                                {
                                    dt.Rows[1]["F24"] = true;
                                }
                            }
                            //

                            ds.Tables.Add(dt);

                            infor = ds.GetXml();
                        }
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                //if (infor == "new" || infor == "error")
                //{
                //    HttpContext.Current.Session[SessionPrimaryKey] = null;
                //    HttpContext.Current.Session[SessionPrimaryKeyTermID] = null;
                //}

                return infor;
            }
        }

        [WebMethod(EnableSession = true)]
        public static object CheckScoreEntered(int stdID, string termID, int nTermSubLevelId, int newRoomnTermSubLevelId)
        {
            List<string> courseCode = new List<string>();
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);


                using (var _dbGrade = new PostgreSQL.PGGradeDBEntities("PGGradeDBEntities"))
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)))
                {
                    var oldClass = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == nTermSubLevelId).FirstOrDefault();
                    var newClass = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == newRoomnTermSubLevelId).FirstOrDefault();

                    if (oldClass.nTSubLevel == newClass.nTSubLevel)
                    {
                        // Define your parameters
                        var stdIDParam = new NpgsqlParameter("@stdID", stdID);
                        var companyIDParam = new NpgsqlParameter("@CompanyID", userData.CompanyID);
                        var termIDParam = new NpgsqlParameter("@termID", termID.Trim());
                        var nTermSubLevelIdParam = new NpgsqlParameter("@nTermSubLevelId", nTermSubLevelId);

                        // SQL query as a string
                        string sqlQuery = string.Format(@"
                                    SELECT DISTINCT g.{0}sPlaneID{0}
                                    FROM public.{0}PGTGrade{0} g
                                    JOIN public.{0}PGTGradeDetail{0} gd ON g.{0}nGradeId{0} = gd.{0}nGradeId{0}
                                    WHERE gd.{0}sID{0} = @stdID
                                    AND g.{0}SchoolID{0} = @CompanyID
                                    AND g.{0}nTerm{0} = @termID
                                    AND g.{0}nTermSubLevel2{0} = @nTermSubLevelId
                                    AND NOT (
                                        (gd.{0}getScore100{0} IS NULL OR gd.{0}getScore100{0} = '' OR gd.{0}getScore100{0} = '0')
                                        AND gd.{0}getSpecial{0} = '-1'
                                        AND (gd.{0}scoreFinalTerm{0} IS NULL OR gd.{0}scoreFinalTerm{0} = '')
                                        AND (gd.{0}scoreMidTerm{0} IS NULL OR gd.{0}scoreMidTerm{0} = '')
                                        AND (gd.{0}ScoreBeforeMidTerm{0} IS NULL OR gd.{0}ScoreBeforeMidTerm{0} = '')
                                    )", '"');

                        // Execute the query and get the results
                        var tGrade = _dbGrade.Database.SqlQuery<int>(sqlQuery, stdIDParam, companyIDParam, termIDParam, nTermSubLevelIdParam).ToList();

                        if (tGrade != null && tGrade.Count > 0)
                        {
                            courseCode = (from g in tGrade
                                          join p in dbschool.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on g equals p.sPlaneID
                                          select p.courseCode).Distinct().ToList();
                        }
                    }

                }
                return courseCode;
            }

        }

        [WebMethod]
        public static string ClearSessionID()
        {
            //HttpContext.Current.Session[SessionPrimaryKey] = null;
            //HttpContext.Current.Session[SessionPrimaryKeyTermID] = null;

            return "";
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
	WHERE t.nTSubLevel = {0} AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1 AND t.SchoolID = {1} AND s.SchoolID = {1}
) r
ORDER BY r.sort1, r.sort2", slID, schoolID);
                    result = dbschool.Database.SqlQuery<EntityDropdown>(query).ToList();
                }
            }
            return result;
        }

        [WebMethod(EnableSession = true)]
        public static string GetLastStudentNumberInClassroom(int? classroomID)
        {
            bool success = true;
            string message = "Save Successfully";

            int newStudentNumber = 1;

            try
            {
                if (classroomID != null)
                {
                    var userData = GetUserData();
                    int schoolID = userData.CompanyID;
                    using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                    {
                        StudentLogic studentLogic = new StudentLogic(dbSchool);
                        string termID = studentLogic.GetTermId(userData);

                        string query = string.Format(@"
SELECT TOP 1 ISNULL(nStudentNumber, 0) + 1 'NewStudentNumber' 
FROM TStudentClassroomHistory 
WHERE SchoolID={0} AND nTerm='{1}' AND nTermSubLevel2={2} AND cDel=0 AND ISNULL(nStudentStatus, 0)=0
ORDER BY nStudentNumber DESC", schoolID, termID, classroomID);
                        int? newNumber = dbSchool.Database.SqlQuery<int?>(query).FirstOrDefault();

                        newStudentNumber = newNumber == null ? 0 : newNumber.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            var result = new { success, message, newStudentNumber };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string GetItemView(string stdID, string termID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    JabjaiEntity.DB.TUser p = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == iStdID).FirstOrDefault();

                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 37; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        int fmlID = 0;
                        var fmlProfile = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == p.sID && w.sDeleted == "false").FirstOrDefault();
                        if (fmlProfile != null)
                        {
                            fmlID = fmlProfile.nFamilyID;
                        }

                        dt.Rows[0]["F0"] = fmlID;
                        dt.Rows[0]["F1"] = p.sStudentID;

                        string studentStatus = "";
                        int? studentStatusID = null;
                        int? nTermSubLevel2 = p.nTermSubLevel2;
                        int? nStudentNumber = null;
                        DateTime? MoveInDate = null;
                        DateTime? MoveOutDate = null;
                        string Note = "";

                        TStudentClassroomHistory studentClassroomHistory = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.nTerm == termID && (w.cDel == false || (w.cDel == true && (w.nStudentStatus == 1 || w.nStudentStatus == 2)))).FirstOrDefault();
                        if (studentClassroomHistory != null)
                        {
                            studentStatusID = studentClassroomHistory.nStudentStatus;
                            nTermSubLevel2 = studentClassroomHistory.nTermSubLevel2;
                            nStudentNumber = studentClassroomHistory.nStudentNumber;
                            MoveInDate = studentClassroomHistory.MoveInDate;
                            MoveOutDate = studentClassroomHistory.MoveOutDate;
                            Note = studentClassroomHistory.Note;
                        }

                        if (!MoveInDate.HasValue)
                        {
                            MoveInDate = p.moveInDate;
                        }

                        switch (studentStatusID)
                        {
                            case 0: studentStatus = "กำลังศึกษา"; break;
                            case 1: studentStatus = "จำหน่าย"; break;
                            case 2: studentStatus = "ลาออก"; break;
                            case 3: studentStatus = "พักการเรียน"; break;
                            case 4: studentStatus = "สำเร็จการศึกษา"; break;
                            case 5: studentStatus = "ขาดการติดต่อ"; break;
                            case 6: studentStatus = "พ้นสภาพ"; break;
                            case 7: studentStatus = "นักเรียนไปโครงการ"; break;
                            default: studentStatus = "กำลังศึกษา"; break;
                        }

                        dt.Rows[0]["F2"] = studentStatus;
                        dt.Rows[0]["F3"] = MoveInDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

                        DateTime? DayQuit = MoveOutDate;
                        var studentHistory = en.TStudentHIstories.Where(w => w.SchoolID == schoolID && w.sID == p.sID && w.nTerm == termID && w.nTermSubLevel2_OLD == nTermSubLevel2).FirstOrDefault();
                        if (studentHistory != null)
                        {
                            DayQuit = studentHistory.DayAdd;
                        }

                        dt.Rows[0]["F4"] = DayQuit?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F5"] = nStudentNumber?.ToString();
                        dt.Rows[0]["F6"] = Note;
                        dt.Rows[0]["F7"] = null;

                        string termSubLevel2 = "";
                        string query = string.Format(@"
SELECT r.id, r.name
FROM
(
	SELECT CAST(t.nTermSubLevel2 AS VARCHAR(10)) 'id', s.SubLevel + ' / ' + t.nTSubLevel2 'name', s.SubLevel 'sort1', (CASE WHEN ISNUMERIC(t.nTSubLevel2) = 1 THEN RIGHT('0000' + t.nTSubLevel2, 5) ELSE t.nTSubLevel2 END) 'sort2'
	FROM TTermSubLevel2 t 
	LEFT JOIN TSubLevel s ON t.nTSubLevel = s.nTSubLevel 
	WHERE t.nTermSubLevel2 = {0} AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1 AND t.SchoolID = {1} AND s.SchoolID = {1}
) r
ORDER BY r.sort1, r.sort2", (nTermSubLevel2 == null ? 0 : nTermSubLevel2), schoolID);
                        var termSubLevel2Obj = en.Database.SqlQuery<EntityDropdown>(query).FirstOrDefault();
                        if (termSubLevel2Obj != null)
                        {
                            termSubLevel2 = termSubLevel2Obj.name;
                        }
                        dt.Rows[0]["F8"] = termSubLevel2;

                        var subLevel = "";
                        var subLevelObj = en.TTermSubLevel2.Where(w => w.SchoolID == schoolID && w.nTermSubLevel2 == nTermSubLevel2).FirstOrDefault();
                        if (subLevelObj != null)
                        {
                            var levelObj = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1 && w.nTSubLevel == subLevelObj.nTSubLevel).FirstOrDefault();
                            if (levelObj != null)
                            {
                                subLevel = levelObj.SubLevel;
                            }
                        }
                        dt.Rows[0]["F7"] = subLevel;

                        dt.Rows[0]["F9"] = p.cSex == "0" ? "ชาย" : "หญิง";
                        string studentTitle = null;
                        var titleList = en.TTitleLists.Where(w => w.SchoolID == schoolID && (w.nTitleid.ToString() == p.sStudentTitle || w.titleDescription == p.sStudentTitle) && w.deleted != "1" && w.workStatus == "working").FirstOrDefault();
                        if (titleList != null)
                        {
                            studentTitle = titleList.titleDescription;
                        }
                        dt.Rows[0]["F10"] = studentTitle;
                        dt.Rows[0]["F11"] = p.sName;
                        dt.Rows[0]["F12"] = p.sLastname;
                        dt.Rows[0]["F13"] = p.sStudentNameEN;
                        dt.Rows[0]["F14"] = p.sStudentLastEN;
                        dt.Rows[0]["F15"] = p.sStudentNameOther;
                        dt.Rows[0]["F16"] = p.sStudentLastOther;
                        dt.Rows[0]["F17"] = p.sNickName;
                        dt.Rows[0]["F18"] = p.sNickNameEN;
                        dt.Rows[0]["F19"] = p.dBirth?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F20"] = p.sIdentification;

                        var studentRace = "";
                        var raceObj = en.TMasterDatas.Where(w => w.MasterType == "9" && w.MasterCode == p.sStudentRace).FirstOrDefault();
                        if (raceObj != null)
                        {
                            studentRace = raceObj.MasterDes;
                        }
                        dt.Rows[0]["F21"] = studentRace;

                        var studentNation = "";
                        var nationObj = en.TMasterDatas.Where(w => w.MasterType == "3" && w.MasterCode == p.sStudentNation).FirstOrDefault();
                        if (nationObj != null)
                        {
                            studentNation = nationObj.MasterDes;
                        }
                        dt.Rows[0]["F22"] = studentNation;

                        var studentReligion = "";
                        var religionObj = en.TMasterDatas.Where(w => w.MasterType == "6" && w.MasterCode == p.sStudentReligion).FirstOrDefault();
                        if (religionObj != null)
                        {
                            studentReligion = religionObj.MasterDes;
                        }
                        dt.Rows[0]["F23"] = studentReligion;

                        dt.Rows[0]["F24"] = p.sPhone;
                        dt.Rows[0]["F25"] = p.sEmail;
                        dt.Rows[0]["F26"] = "0";
                        dt.Rows[0]["F27"] = p.nSonNumber?.ToString();
                        dt.Rows[0]["F28"] = null;
                        dt.Rows[0]["F29"] = p.sStudentPicture;

                        bool useBiometric = false;
                        using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                        {

                            var userMaster = dbMaster.TUsers.Where(w => w.sID == p.sID && w.nCompany == schoolID && w.cType == "0").FirstOrDefault();
                            if (userMaster != null)
                            {
                                useBiometric = userMaster.UseBiometric;
                            }
                            dt.Rows[0]["F30"] = useBiometric ? "เปิด" : "ปิด";

                            TFamilyProfile familyProfile = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                            if (familyProfile != null)
                            {
                                dt.Rows[0]["F26"] = familyProfile.nSonTotal == null ? "0" : familyProfile.nSonTotal.ToString();
                                dt.Rows[0]["F28"] = familyProfile.nRelativeStudyHere?.ToString();
                            }
                            dt.Rows[0]["F31"] = p.Note2;
                            dt.Rows[0]["F32"] = ""; // ChangeRoomDate

                            var disability = "";
                            var disabilityObj = en.TMasterDatas.Where(w => w.MasterType == "7" && w.MasterCode == p.DisabilityCode).FirstOrDefault();
                            if (disabilityObj != null)
                            {
                                disability = disabilityObj.MasterDes;
                            }
                            dt.Rows[0]["F33"] = disability;

                            var disadvantage = "";
                            var disadvantageObj = en.TMasterDatas.Where(w => w.MasterType == "8" && w.MasterCode == p.DisadvantageCode).FirstOrDefault();
                            if (disadvantageObj != null)
                            {
                                disadvantage = disadvantageObj.MasterDes;
                            }
                            dt.Rows[0]["F34"] = disadvantage;

                            dt.Rows[0]["F35"] = p.nMax == null ? "0.00" : p.nMax.Value.ToString("0.00");

                            dt.Rows[0]["F36"] = p.JourneyType == null ? "" : (p.JourneyType.Value.ToString() == "1" ? "ไป - กลับ" : "ประจำ");
                            dt.Rows[0]["F37"] = p.DormitoryName;

                            ds.Tables.Add(dt);

                            infor = ds.GetXml();
                        }
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                return infor;
            }
        }

        #endregion

        public class TermData
        {
            public int Year { get; set; }

            [JsonProperty(PropertyName = "yearID")]
            public int YearID { get; set; }

            [JsonProperty(PropertyName = "termID")]
            public string TermID { get; set; }

            public string Term { get; set; }

            [JsonProperty(PropertyName = "termCheck")]
            public bool TermCheck { get; set; }

            [JsonProperty(PropertyName = "flag")]
            public string Flag { get; set; }

            [JsonProperty(PropertyName = "current")]
            public bool Current { get; set; }

            [JsonProperty(PropertyName = "currentYear")]
            public bool CurrentYear { get; set; }
        }

    }
}