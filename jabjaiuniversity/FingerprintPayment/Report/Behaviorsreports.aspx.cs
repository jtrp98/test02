using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JabjaiEntity.DB;
using JabjaiMainClass;
using System.Data.SqlClient;
using MasterEntity;
using System.Web.Script.Services;
using System.Web.Services;
using urbanairship;

namespace FingerprintPayment.Report
{
    public partial class Behaviorsreports : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                var userData = GetUserData();
                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
                DataTable dtYear = fcommon.LinqToDataTable(dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.numberYear).ToList());

                fcommon.ListDataTableToDropDownList(dtYear, ddlyear, "", "nYear", "numberYear");
                ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                    hdfschoolname.Value = tCompany.sCompany;
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object returnlist(Search search)
        {
            //JWTToken token = new JWTToken();
            //if (!token.CheckToken(HttpContext.Current)) { }
            var userData = GetUserData();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                string DBName = "";
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    DateTime dStart = DateTime.Today, dEnd = DateTime.Today;
                    if (search.sort_type == 0)
                    {
                        dStart = search.dStart ?? DateTime.Today;
                        dEnd = dStart.AddDays(1);
                    }
                    else if (search.sort_type == 1)
                    {
                        dStart = search.dStart.Value;
                        dEnd = search.dStart.Value.AddMonths(1);
                    }
                    else if (search.sort_type == 2)
                    {
                        //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                        dStart = f_term.dStart.Value;
                        dEnd = f_term.dEnd.Value.AddDays(1);

                        StudentLogic logic = new StudentLogic(dbschool);
                        var f_termCurrent = logic.GetTermDATA(DateTime.Today, userData);
                        if (f_termCurrent.nYear != f_term.nYear)
                        {
                            DBName = "JabjaiSchoolHistory";
                        }
                        else
                        {
                            DBName = "JabjaiSchoolSingleDB";
                        }
                    }
                    else
                    {
                        dStart = search.dStart.Value;
                        dEnd = search.dEnd.Value.AddDays(1);
                    }

                    string SQL = string.Format(@"SELECT A.sID,A.sName,A.sLastname,C.SubLevel,B.nTSubLevel2,B.nTermSubLevel2,C.nTSubLevel,
                    D.dAdd,D.Score,D.ResidualScore,D.Type,D.Note,D.BehaviorName,D.BehaviorHistoryId,D.dCanCel,
                    ISNULL(E.sName,'') AS teachername,ISNULL(E.sLastname,'') AS teacherlastname
                    FROM {0}.dbo.TBehaviorHistory AS D
                    LEFT JOIN TEmployees AS E 
                    ON D.UserAdd = E.sEmp
                    INNER JOIN TUser AS A
                    ON D.StudentId = A.sID
                    INNER JOIN TTermSubLevel2 AS B
                    ON A.nTermSubLevel2 = B.nTermSubLevel2
                    INNER JOIN TSubLevel AS C 
                    ON C.nTSubLevel = B.nTSubLevel
                    WHERE D.cDel IS NULL AND D.dAdd BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}' ", DBName, dStart, dEnd);

                    if (search.student_id.HasValue)
                    {
                        SQL += " AND A.sID = " + search.student_id;
                    }
                    else if (search.level2_id.HasValue)
                    {
                        SQL += " AND A.nTermSubLevel2 = " + search.level2_id;
                    }
                    else if (search.level_id.HasValue)
                    {
                        SQL += " AND B.nTSubLevel = " + search.level_id;
                    }

                    SQL += " ORDER BY A.dAdd DESC";
                    var lstudent = dbschool.Database.SqlQuery<TR_Behaviorsr>(SQL).ToList();

                    //var lstudent = (from d in dbschool.TBehaviorHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                    //                join e in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on d.UserAdd equals e.sEmp into jde
                    //                from je in jde.DefaultIfEmpty()

                    //                join a in dbschool.TUsers.Where(w => w.SchoolID == userData.CompanyID) on d.StudentId equals a.sID
                    //                join b in dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
                    //                join c in dbschool.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
                    //                orderby d.dAdd descending
                    //                where a.cDel == null &&
                    //                ((!search.level2_id.HasValue || search.level2_id == a.nTermSubLevel2)
                    //                && (!search.level_id.HasValue || search.level_id == b.nTSubLevel))
                    //                && dStart <= d.dAdd && dEnd >= d.dAdd &&
                    //                (!search.student_id.HasValue || search.student_id == a.sID)
                    //                select new
                    //                {
                    //                    a.sID,
                    //                    a.sName,
                    //                    a.sLastname,
                    //                    c.SubLevel,
                    //                    b.nTSubLevel2,
                    //                    b.nTermSubLevel2,
                    //                    c.nTSubLevel,
                    //                    teachername = je == null ? "" : je.sName,
                    //                    teacherlastname = je == null ? "" : je.sLastname,
                    //                    d.dAdd,
                    //                    d.Score,
                    //                    d.ResidualScore,
                    //                    d.Type,
                    //                    d.Note,
                    //                    d.BehaviorName,
                    //                    d.BehaviorHistoryId,
                    //                    d.dCanCel,
                    //                }).ToList();

                    return (from s in lstudent
                            select new Behaviorsreport
                            {
                                studentname = s.sName,
                                day = s.dAdd.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                studentlastname = s.sLastname,
                                behaviorname = s.BehaviorName,
                                BehaviorHistoryId = s.BehaviorHistoryId,
                                score = string.Format("{0:0.##}", s.Score ?? 0),
                                residualscore = string.Format("{0:0.##}", s.ResidualScore ?? 0),
                                teachername = s.teachername,
                                teacherlastname = s.teacherlastname,
                                note = s.Note,
                                behaviortype = (s.Type == "0" ? "เพิ่ม" : "ลด"),
                                status = s.dCanCel.HasValue ? "delete" : ""
                            }).ToList();
                }
            }

        }

        const string MessageSystem = "ยกเลิกรายการตัดคะแนนพฤติกรรม {0} คะแนน เนื่องจาก {1} คะแนนพฤติกรรมคงเหลือ {2} คะแนน";
        const string TitleSystem = "ยกเลิกคะแนนพฤติกรรม";

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static string CancelScore(int BehaviorsrId)
        {
            //JWTToken token = new JWTToken();
            //if (!token.CheckToken(HttpContext.Current)) { }
            var userData = GetUserData();
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    int UserId = int.Parse(HttpContext.Current.Session["sEmpID"].ToString());
                    //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                    {
                        var f_data = dbschool.TBehaviorHistories.FirstOrDefault(f => f.BehaviorHistoryId == BehaviorsrId);
                        if (f_data.dCanCel.HasValue)
                        {
                            return "Fail";
                        }
                        f_data.dCanCel = DateTime.Now;
                        f_data.UserCancel = UserId;

                        decimal? ResidualScore = 0;
                        var q_history = dbschool.TBehaviorHistories.Where(w => w.StudentId == f_data.StudentId
                        && w.BehaviorHistoryId > BehaviorsrId && w.dCanCel == null && w.cDel == false).ToList();

                        foreach (var data in q_history)
                        {
                            if (f_data.Type == "0") data.ResidualScore -= f_data.Score;
                            else data.ResidualScore += f_data.Score;
                            ResidualScore = data.ResidualScore;
                        }

                        string Message = string.Format(MessageSystem, f_data.Score, f_data.BehaviorName, ResidualScore);
                        int messageId = messagebox.insert_message(new messagebox.MessageBox
                        {
                            message_type = messagebox.Behaviors,
                            title = TitleSystem,
                            message = Message,
                            school_id = tCompany.nCompany,
                            send_time = DateTime.Now,
                            user_messagebox = new List<messagebox.user_messagebox>
                            {
                                new messagebox.user_messagebox
                                {
                                    message_receive = DateTime.Now,
                                    user_id = UserId,
                                    user_type  = 0
                                }
                            }
                        });

                        notification _notification = new notification();
                        _notification.title = TitleSystem;
                        _notification.message = Message;
                        _notification.user = UserId.ToString();
                        _notification.action = "vnd.jabjai.jabjaiapp://deeplink/come_to_school?message_id=" + messageId + "&school_id=" + tCompany.nCompany;
                        //await pushdata.push(_notification);

                        dbschool.SaveChanges();
                        return "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Fail";
            }
        }

        public class Search
        {
            public int sort_type { get; set; }
            public string term_id { get; set; }
            public int? level2_id { get; set; }
            public int? level_id { get; set; }
            public Nullable<int> student_id { get; set; }
            public DateTime? dStart { get; set; }
            public DateTime? dEnd { get; set; }
        }

    }

    public class TR_Behaviorsr
    {
        public int sID { get; set; }
        public string sName { get; set; }
        public string sLastname { get; set; }
        public int SubLevel { get; set; }
        public int nTSubLevel2 { get; set; }
        public int nTermSubLevel2 { get; set; }
        public int nTSubLevel { get; set; }
        public string teachername { get; set; }
        public string teacherlastname { get; set; }
        public DateTime? dAdd { get; set; }
        public decimal? Score { get; set; }
        public decimal? ResidualScore { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public string BehaviorName { get; set; }
        public int BehaviorHistoryId { get; set; }
        public DateTime? dCanCel { get; set; }
    }

    internal class Behaviorsreport
    {
        public string studentname { get; set; }
        public string day { get; set; }
        public string studentlastname { get; set; }
        public string behaviorname { get; set; }
        public string score { get; set; }
        public string residualscore { get; set; }
        public string teachername { get; set; }
        public string teacherlastname { get; set; }
        public string note { get; set; }
        public string behaviortype { get; set; }
        public int BehaviorHistoryId { get; set; }
        public string status { get; set; }
    }
}