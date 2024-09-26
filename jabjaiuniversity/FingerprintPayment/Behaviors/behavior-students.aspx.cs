using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JabjaiNoSQL;
using JabjaiNoSQL.Behavior;
using WebGrease.Css.Extensions;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;
using System.Data.Entity;
using System.Web.Script.Services;
using Newtonsoft.Json.Linq;
using PagedList;
using System.Threading.Tasks;
using System.Web.Services;
using FingerprintPayment.Class;
using urbanairship;
using System.Web.Hosting;
using SchoolBright.Helper;

namespace FingerprintPayment
{
    public partial class behavior_students : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                SqlConnection _conn = fcommon.ConfigSqlConnection(schoolID);
                //dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
                //dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
                //btnSearch.Click += new EventHandler(btnSearch_Click);
                if (!IsPostBack)
                {
                    //OpenData();

                    var q = QueryDataBases.SubLevel_Query.GetData(_db, userData);
                    fcommon.LinqToDropDownList(q, ddlsublevel, "- ทั้งหมด -", "class_id", "class_name");

                    //int sEmpID = int.Parse(Session["sEmpID"] + "");
                    int sEmp = UserData.UserID;
                    //string sClaimReport = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == sEmp).SingleOrDefault().sStatusReport;
                }
            }
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            OpenData(UserData);
        }
        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                //LinkButton _btnDel = e.Item.FindControl("btnDel") as LinkButton;
                //_btnDel.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel.UniqueID + "');return false;");
            }
        }
        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            //int schoolID = UserData.CompanyID;
            //JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));
        }
        private async void OpenData(JWTToken.userData userData)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int idlv = 0;
                int.TryParse(Request.QueryString["idlv"], out idlv);
                int idlv2 = 0;
                int.TryParse(Request.QueryString["idlv2"], out idlv2);
                string sname = Request.QueryString["sname"];

                int schoolID = userData.CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    var tTerm = dbschool.TTerms.Where(w => w.SchoolID == schoolID && w.dStart <= DateTime.Today && w.dEnd >= DateTime.Today).FirstOrDefault();
                    var tBehaviorSettings = dbschool.TBehaviorSettings.Where(w => w.SchoolID == schoolID).FirstOrDefault();
                    DateTime _dstart = DateTime.Today;
                    DateTime _dEnd = DateTime.Today.AddDays(1);
                    int index = 1;

                    if (tTerm == null) return;
                    if (tBehaviorSettings.Type == 1)
                    {
                        _dstart = tTerm.dStart.Value;
                        _dEnd = DateTime.Today >= tTerm.dEnd.Value ? tTerm.dEnd.Value : _dEnd;
                    }
                    else
                    {
                        var tYear = dbschool.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == tTerm.nYear).ToList();
                        _dstart = tYear.Min(min => min.dStart).Value;
                        _dEnd = tYear.Max(max => max.dEnd).Value;
                    }

                    var gp = (from a in dbschool.TBehaviorHistories.Where(w => w.SchoolID == schoolID && w.cDel == false).ToList()
                              where _dstart <= a.dAdd && _dEnd >= a.dAdd
                              group a by a.StudentId into ag
                              select new
                              {
                                  Score = ag.LastOrDefault().ResidualScore,
                                  StudentId = ag.Key.Value
                              });

                    var qstudent = await dbschool.TUser.Where(w => w.SchoolID == schoolID && w.cDel == null).ToListAsync();
                    var qTermSubLevel2 = await dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolID).ToListAsync();
                    var qSubLevels = await dbschool.TSubLevels.Where(w => w.SchoolID == schoolID).ToListAsync();
                    var quser = await dbmaster.TUsers.Where(w => w.cType == "0" && w.nCompany == schoolID && w.cDel == null).ToListAsync();

                    var q1 = (from a in qstudent
                              join u in quser on a.sID equals u.nSystemID
                              join b in qTermSubLevel2 on a.nTermSubLevel2 equals b.nTermSubLevel2
                              join c in qSubLevels on b.nTSubLevel equals c.nTSubLevel
                              join d in gp on a.sID equals d.StudentId into ad
                              from dd in ad.DefaultIfEmpty()
                              orderby c.SubLevel, b.nTSubLevel2
                              where a.cDel == null
                              select new StudentScore
                              {
                                  sName = a.sName,
                                  nTSubLevel2 = b.nTSubLevel2,
                                  sIdentification = a.sIdentification,
                                  sLastName = a.sLastname,
                                  SubLevel = c.SubLevel,
                                  endScore = dd == null ? tBehaviorSettings.MaxScore.Value : dd.Score.Value,
                                  id = a.sID,
                                  nTermSubLevel2 = a.nTermSubLevel2,
                                  nTSubLevel = b.nTSubLevel
                              }).ToList();

                    if (!string.IsNullOrEmpty(sname)) q1 = q1.Where(w => w.sName + ' ' + w.sLastName == sname).ToList();
                    else if (idlv2 > 0) q1 = q1.Where(w => w.nTermSubLevel2 == idlv2).ToList();
                    else if (idlv > 0) q1 = q1.Where(w => w.nTSubLevel == idlv).ToList();
                    q1.ToList().ForEach(f => f.row = index++);

                    //dgd.DataSource = q1;
                    //dgd.DataBind();
                }
            }
        }
        protected void ddlcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                txtSearch.Text = "";
            }
            //dgd.DataSource = _db.TUsers.Where(w => string.IsNullOrEmpty(w.cDel)).ToList();
            //dgd.CurrentPageIndex = 0;
            //dgd.DataBind();
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read))) { }
            //dgd.DataSource = _db.TUsers.Where(w => (w.sName + " " + w.sLastname).Contains(txtSearch.Text) && string.IsNullOrEmpty(w.cDel)).ToList();
            //dgd.CurrentPageIndex = 0;
            //dgd.DataBind();
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static string BehaviorList()
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    dynamic rss = new JArray(from a in dbschool.TBehaviors.Where(w => w.SchoolID == schoolID && w.Status != false).ToList()
                                             select new JObject
                                             {
                                                new JProperty("behavior_name",a.BehaviorName + (a.Type == 0 ? " เพิ่ม " : "ลด ") + (a.Score??0) +" คะแนน"),
                                                new JProperty("behavior_id",a.BehaviorId),
                                                new JProperty("behavior_type",a.Type == 0 ? "add" : "reduce" ),
                                             });

                    return rss.ToString();
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static string UpdateScore(BehaviorHistoryUpdate update)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                {
                    var term = dbschool.TTerms.Where(w => w.SchoolID == schoolID && w.dStart <= DateTime.Today && w.dEnd >= DateTime.Today).FirstOrDefault();

                    var tBehaviors = dbschool.TBehaviors.Where(w => w.SchoolID == schoolID && w.BehaviorId == update.behavior_id).FirstOrDefault();
                    var teacherId = userData.UserID;
                    //int BehaviorHistoryId = dbschool.TBehaviorHistories.Count() == 0 ? 1 : dbschool.TBehaviorHistories.Max(max => max.BehaviorHistoryId) + 1;
                    var tBehaviorSettings = dbschool.TBehaviorSettings.Where(w => w.SchoolID == schoolID).FirstOrDefault();

                    decimal ResidualScore = tBehaviorSettings.MaxScore ?? 100;
                    TBehaviorHistory tBehaviorHistory = new TBehaviorHistory();
                    if (tBehaviorSettings.Type == 1)
                    {
                        tBehaviorHistory = dbschool.TBehaviorHistories.Where(w => w.SchoolID == schoolID && w.cDel == false && w.dAdd >= term.dStart && w.dAdd <= term.dEnd && w.StudentId == update.student_id && w.dCanCel == null)
                    .OrderByDescending(order => order.BehaviorHistoryId).FirstOrDefault();
                    }
                    else
                    {
                        var f1 = dbschool.TTerms.FirstOrDefault(f => f.nTerm == term.nTerm);
                        var q2 = dbschool.TTerms.Where(w => w.nYear == f1.nYear && (w.cDel ?? "0") == "0").ToList();
                        DateTime? dStart = q2.Select(s => s.dStart).Min();
                        DateTime? dEnd = term.dEnd.Value.AddHours(23).AddMinutes(59);

                        tBehaviorHistory = dbschool.TBehaviorHistories.Where(w => w.SchoolID == schoolID && w.cDel == false && w.dAdd >= dStart && w.dAdd <= dEnd && w.StudentId == update.student_id && w.dCanCel == null)
                    .OrderByDescending(order => order.BehaviorHistoryId).FirstOrDefault();
                    }

                    if (tBehaviorHistory == null && tBehaviorSettings.Type == 3)
                    {
                        BehaviorHistoryClass behaviorHistoryClass = new BehaviorHistoryClass();
                        behaviorHistoryClass.getResidualScore(dbschool, update.student_id, term.nTerm, tBehaviorSettings.MaxScore ?? 100);
                    }
                    else if (tBehaviorHistory != null)
                    {
                        ResidualScore = tBehaviorHistory.ResidualScore ?? ResidualScore;
                    }

                    if (tBehaviors.Type == 0) ResidualScore = ResidualScore + tBehaviors.Score.Value;
                    else ResidualScore = ResidualScore - tBehaviors.Score.Value;

                    dbschool.TBehaviorHistories.Add(new TBehaviorHistory
                    {
                        BehaviorId = tBehaviors.BehaviorId,
                        BehaviorName = tBehaviors.BehaviorName,
                        dAdd = DateTime.Now,
                        UserAdd = teacherId,
                        Score = tBehaviors.Score,
                        StudentId = update.student_id,
                        //BehaviorHistoryId = BehaviorHistoryId,
                        ResidualScore = ResidualScore,
                        Note = update.note,
                        Type = tBehaviors.Type.ToString(),
                        SchoolID = userData.CompanyID,
                    });

                    dbschool.SaveChanges();

                    database.InsertLog(userData.UserID.ToString(),
                         tBehaviors.Type == 0 ? "ทำการเพิ่มคะแนนความประพฤติ " : "ทำการตัดคะแนนความประพฤติ ",
                          userData.Entities,
                          HttpContext.Current.Request, 4, 2, 0);

                    notification _notification = new notification();
                    _notification.title = "แจ้งคะแนนพฤติกรรม";
                    if (tBehaviors.Type == 0) _notification.message = string.Format("ยินดีด้วยค่ะ\nมีรายงานการเพิ่มคะแนนพฤติกรรม {0:0.##} คะแนน\nเนื่องจาก {1}\nคะแนนพฤติกรรมคงเหลือ {2:0.##} คะแนน", tBehaviors.Score, tBehaviors.BehaviorName, ResidualScore);
                    else _notification.message = string.Format("เสียใจด้วยค่ะ\nมีรายงานการตัดคะแนนพฤติกรรม {0:0.##} คะแนน\nเนื่องจาก {1}\nคะแนนพฤติกรรมคงเหลือ {2:0.##} คะแนน", tBehaviors.Score, tBehaviors.BehaviorName, ResidualScore);

                    var student_data = dbmaster.TUsers.FirstOrDefault(w => w.sID == update.student_id && w.nCompany == schoolID && w.cType == "0");
                    int user_id = student_data.sID;
                    int school_id = student_data.nCompany;
                    int message_id = messagebox.insert_message(
                        new messagebox.MessageBox
                        {
                            message_type = 9,
                            send_time = DateTime.Now,
                            message = _notification.message,
                            title = _notification.title,
                            school_id = school_id,
                            user_messagebox = new List<messagebox.user_messagebox>() {
                                    new messagebox.user_messagebox
                                    {
                                        user_id = user_id,
                                        user_type = 0
                                    }},
                        });

                    _notification.message = _notification.message.Replace("\n", " ");
                    _notification.action = "vnd.jabjai.jabjaiapp://deeplink/behavior?message_id=" + message_id + "&school_id=" + school_id;
                    _notification.user = update.student_id.ToString();

                    FMCServicesLogic fMCServices = new FMCServicesLogic(dbmaster);
                    var q_Token = fMCServices.getTokenFormUserID(update.student_id, userData.CompanyID);

                    if (q_Token.Count(c => c.UserID == update.student_id) > 0)
                    {
                        var user_Token = q_Token.FirstOrDefault(f => f.UserID == update.student_id);
                        FMCServicesLogic.FirebaseCloudMessaging firebaseCloudMessagings = new FMCServicesLogic.FirebaseCloudMessaging
                        {
                            registration_ids = user_Token.Token,

                            push_mid = message_id,
                            push_mtype = messagebox.Behaviors,
                            push_userid = update.student_id,
                            push_schoolid = school_id,

                            notification = new FMCServicesLogic.FirebaseCloudMessaging.Notification
                            {

                                body = user_Token.UserName + " " + _notification.message,
                                title = user_Token.UserName + " " + _notification.title,
                            }
                        };

                        HostingEnvironment.QueueBackgroundWorkItem(ct => fMCServices.SendFirebaseCloudMessaging(firebaseCloudMessagings));
                    }

                    return "Success";
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Search_data returnlist(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
                {
                    var tTerm = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.dStart <= DateTime.Today && w.dEnd >= DateTime.Today).FirstOrDefault();
                    var tBehaviorSettings = dbschool.TBehaviorSettings.FirstOrDefault(w => w.SchoolID == userData.CompanyID);
                    DateTime _dstart = DateTime.Today;
                    DateTime _dEnd = DateTime.Today.AddDays(1);
                    int index = 1;

                    if (tTerm == null) return new Search_data
                    {
                        DATA = new List<StudentScore>(),
                        FOOT = new FOOT
                        {
                            pageSize = 1
                        }
                    };
                    if (tBehaviorSettings.Type == 1)
                    {
                        _dstart = tTerm.dStart.Value;
                        _dEnd = DateTime.Today >= tTerm.dEnd.Value ? tTerm.dEnd.Value : _dEnd;
                    }
                    else
                    {
                        var tYear = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.nYear == tTerm.nYear).ToList();
                        _dstart = tYear.Min(min => min.dStart).Value;
                        _dEnd = tYear.Max(max => max.dEnd).Value;
                    }

                    StudentLogic logic = new StudentLogic(dbschool);
                    string TermId = logic.GetTermId(userData);

                    decimal maxScore = tBehaviorSettings.MaxScore ?? 100;

                    BehaviorHistoryClass behaviorHistoryClass = new BehaviorHistoryClass();

                    string SQL = string.Format($@"

DECLARE @wording NVARCHAR(200) =  {(string.IsNullOrEmpty(search.wording) ? " null " : $" '{search.wording}' ")};
DECLARE @lv_id INT = {(search.lv_id ?? 0).ToString()};
DECLARE @lv2_id INT = {(search.lv2_id ?? 0).ToString()};
DECLARE @school_id INT = {userData.CompanyID};
DECLARE @TermId NVARCHAR(50) = '{TermId}';
DECLARE @dstart DATE = '{_dstart.ToString("yyyy-MM-dd")}';
DECLARE @dEnd DATE = '{_dEnd.ToString("yyyy-MM-dd")}';

WITH RankedBehaviorHistory AS (
    SELECT 
        a1.ResidualScore,
        a1.StudentId,
        a1.SchoolID,
        a1.dAdd,
        ROW_NUMBER() OVER (PARTITION BY a1.StudentId ORDER BY a1.dAdd DESC) AS RowNum
    FROM 
        JabjaiSchoolSingleDB.dbo.TBehaviorHistory AS a1
    WHERE 
        a1.SchoolID = @school_id
        AND a1.cDel = 0
        AND a1.dAdd BETWEEN @dstart AND @dEnd 
        AND a1.dCanCel IS NULL
)

SELECT 
    a.sName,
    CAST(b.nTSubLevel2 AS VARCHAR) AS nTSubLevel2,
    a.sStudentID AS sIdentification,
    a.sLastname AS sLastName,
    c.SubLevel,
    dd.ResidualScore AS endScore,
    a.sID AS id,
    a.nTermSubLevel2,
    b.nTSubLevel,
    ROW_NUMBER() OVER (ORDER BY c.SubLevel, b.nTSubLevel2) AS row
FROM 
    (SELECT 
        a1.sID,
        a1.nTermSubLevel2,
        a1.cDel,
        a1.sName,
        a1.sLastname,
        a1.sStudentID
     FROM JabjaiSchoolSingleDB.dbo.TB_StudentViews a1
     WHERE a1.SchoolID = @school_id 
       AND TRIM(a1.nTerm) = @TermId
       AND a1.cDel IS NULL
       AND (@wording IS NULL OR a1.sName + ' ' + a1.sLastname = @wording)
    ) a
JOIN 
    (SELECT 
        a1.nSystemID,
        a1.nCompany
     FROM JabjaiMasterSingleDB.dbo.TUser a1
     WHERE a1.cType = '0' 
       AND a1.nCompany = @school_id
       AND a1.cDel IS NULL
    ) u ON a.sID = u.nSystemID
JOIN 
    (SELECT 
        a1.nTermSubLevel2,
        a1.nTSubLevel2,
        a1.cDel,
        a1.nTSubLevel
     FROM JabjaiSchoolSingleDB.dbo.TTermSubLevel2 a1
     WHERE a1.SchoolID = @school_id
       AND (@lv2_id = 0 OR a1.nTermSubLevel2 = @lv2_id)
    ) b ON a.nTermSubLevel2 = b.nTermSubLevel2
JOIN 
    (SELECT 
        a1.nTSubLevel,
        a1.SubLevel
     FROM JabjaiSchoolSingleDB.dbo.TSubLevel a1
     WHERE a1.SchoolID = @school_id
       AND (@lv_id = 0 OR a1.nTSubLevel = @lv_id)
    ) c ON b.nTSubLevel = c.nTSubLevel
LEFT JOIN (SELECT * FROM RankedBehaviorHistory WHERE RowNum = 1) dd ON a.sID = dd.StudentId
WHERE a.cDel IS NULL
ORDER BY c.SubLevel, b.nTSubLevel2;

");
                    var studentScores = dbschool.Database.SqlQuery<StudentScore>(SQL).ToList();

                    var q2 = new Search_data();
                    int pageSize = 0;
                    if (studentScores.Count() > 0) pageSize = (studentScores.Count() / search.pageSize) + (studentScores.Count() % search.pageSize > 0 ? 1 : 0);
                    q2.FOOT = new FOOT
                    {
                        pageSize = pageSize
                    };

                    q2.DATA = studentScores.ToPagedList(search.pageNumber, search.pageSize).ToList();

                    foreach (var _data in q2.DATA)
                    {
                        if (_data.endScore == null)
                        {
                            if (tBehaviorSettings.Type == 3)
                            {
                                _data.endScore = behaviorHistoryClass.getResidualScore(dbschool, _data.id, tTerm.nTerm, maxScore);
                            }
                            else
                            {
                                _data.endScore = tBehaviorSettings.MaxScore ?? 100;
                            }
                        }
                    }

                    return q2;
                }
            }
        }

        public class Search
        {
            public int pageSize { get; set; }
            public int pageNumber { get; set; }
            public string wording { get; set; }
            public int? lv_id { get; set; }
            public int? lv2_id { get; set; }
        }

        public class BehaviorHistoryUpdate
        {
            public int student_id { get; set; }
            public int behavior_id { get; set; }
            public string note { get; set; }
        }

        public class Search_data
        {
            public FOOT FOOT { get; set; }
            public List<StudentScore> DATA { get; set; }
        }

        public class FOOT
        {
            public int? pageSize { get; set; }
        }

        public class StudentScore
        {
            public string sName { get; set; }
            public string sLastName { get; set; }
            public string sIdentification { get; set; }
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public decimal? endScore { get; set; }
            public int id { get; set; }
            public Int64 row { get; set; }
            public int nTSubLevel { get; internal set; }
            public int? nTermSubLevel2 { get; internal set; }
        }
    }
}