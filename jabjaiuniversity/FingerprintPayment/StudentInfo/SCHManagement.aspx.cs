using FingerprintPayment.Helper;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class SCHManagement : StudentGateway
    {
        protected string LevelData = "";
        protected string RoomData = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();

            Response.Redirect("~/Default.aspx");
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
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
                RoomData = jsonClassRoom.Substring(1, jsonClassRoom.Length - 2);
            }
        }

        [WebMethod]
        public static string SearchStudent(string studentID)
        {
            bool success = true;
            string message = "Successfully";
            List<StudyInfo> data = new List<StudyInfo>();

            int sid = 0;

            try
            {
                int schoolID = GetUserData().CompanyID;
                JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

                var user = dbSchool.TUser.Where(w => w.SchoolID == schoolID && w.sStudentID == studentID && (w.cDel == null || w.cDel == "G")).FirstOrDefault();
                if (user != null)
                {
                    sid = user.sID;
                }

                string query = string.Format(@"
SELECT y.nYear 'yearID', y.numberYear 'year', t.nTerm 'termID', t.sTerm 'term', t.dStart 'start', t.dEnd 'end', sch.nHistoryId 'historyID', sch.nStudentNumber 'number', sch.nStudentStatus 'status'
, (CASE WHEN sch.nHistoryId IS NULL THEN 'new' ELSE 'old' END) 'flag'
, sl.nTSubLevel 'levelID', sch.nTermSubLevel2 'roomID', sl.SubLevel 'level', tsl.nTSubLevel2 'room', t.nTerm + CAST(ISNULL(sch.nHistoryId, '') AS VARCHAR(10)) 'uniqueID', sch.sID
FROM TYear y 
LEFT JOIN TTerm t ON y.SchoolID = t.SchoolID AND y.nYear = t.nYear
LEFT JOIN 
(
	SELECT * 
	FROM TStudentClassroomHistory 
	WHERE cDel=0 AND sID IN (SELECT sID FROM TUser WHERE (cDel IS NULL OR cDel='G') AND SchoolID={0} AND sStudentID='{1}')
) sch ON t.SchoolID = sch.SchoolID AND t.nTerm = sch.nTerm
LEFT JOIN TTermSubLevel2 tsl ON sch.SchoolID = tsl.SchoolID AND sch.nTermSubLevel2 = tsl.nTermSubLevel2
LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel 
WHERE y.SchoolID={0} AND y.cDel=0 AND y.numberYear>=YEAR(DATEADD(YEAR, 543-6, GETDATE())) AND t.cDel IS NULL AND t.nTerm IS NOT NULL
ORDER BY y.numberYear DESC, t.sTerm DESC", schoolID, studentID);
                data = dbSchool.Database.SqlQuery<StudyInfo>(query).ToList();
            }
            catch (Exception er)
            {
                success = false;
                message = er.Message;
            }

            var result = new { success, message, sid, data };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static object SaveStudyInfo(List<StudyInfo> studyInfos)
        {
            bool success = true;
            string message = "Save Successfully";

            try
            {
                JWTToken.userData userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TStudentClassroomHistory sch;

                    var studyData = studyInfos.Where(w => w.sID != null && w.sID != 0).ToList();
                    foreach (var si in studyData)
                    {
                        switch (si.flag)
                        {
                            case "insert":
                                if (si.roomID != null)
                                {
                                    sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == si.sID && w.nTerm == si.termID && w.nTermSubLevel2 == si.roomID && w.cDel == false).FirstOrDefault();
                                    if (sch != null)
                                    {
                                        sch.nTermSubLevel2 = si.roomID;
                                        sch.nStudentNumber = si.number;
                                        sch.nStudentStatus = si.status;
                                        sch.UpdatedBy = userData.UserID;
                                        sch.UpdatedDate = DateTime.Now.FixSecond(33).FixMillisecond(35);
                                    }
                                    else
                                    {
                                        sch = new TStudentClassroomHistory
                                        {
                                            sID = si.sID,
                                            nTerm = si.termID,
                                            nTermSubLevel2 = si.roomID,
                                            nStudentNumber = si.number,
                                            nStudentStatus = si.status,
                                            SchoolID = schoolID,
                                            CreatedBy = userData.UserID,
                                            CreatedDate = DateTime.Now.FixSecond(33).FixMillisecond(33),
                                            cDel = false
                                        };

                                        en.TStudentClassroomHistories.Add(sch);
                                    }

                                    en.SaveChanges();
                                }
                                break;
                            case "delete":
                                if (si.historyID != null)
                                {
                                    sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.nHistoryId == si.historyID).FirstOrDefault();
                                    if (sch != null)
                                    {
                                        //en.TStudentClassroomHistories.Remove(sch);
                                        sch.cDel = true;

                                        en.SaveChanges();
                                    }
                                }
                                break;
                            case "edit":
                                if (si.historyID != null && si.roomID != null)
                                {
                                    sch = en.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.nHistoryId == si.historyID).FirstOrDefault();
                                    if (sch != null)
                                    {
                                        sch.nTermSubLevel2 = si.roomID;
                                        sch.nStudentNumber = si.number;
                                        sch.nStudentStatus = si.status;
                                        sch.UpdatedBy = userData.UserID;
                                        sch.UpdatedDate = DateTime.Now.FixSecond(33).FixMillisecond(34);

                                        en.SaveChanges();
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception er)
            {
                success = false;
                message = er.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        public class StudyInfo
        {
            public string uniqueID { get; set; }
            public int? sID { get; set; }
            public int yearID { get; set; }
            public int year { get; set; }
            public string termID { get; set; }
            public string term { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public int? historyID { get; set; }
            public int? number { get; set; }
            public int? status { get; set; }
            public string flag { get; set; }
            public int? levelID { get; set; }
            public int? roomID { get; set; }
            public string level { get; set; }
            public string room { get; set; }
            public bool check { get; set; }

        }
    }
}