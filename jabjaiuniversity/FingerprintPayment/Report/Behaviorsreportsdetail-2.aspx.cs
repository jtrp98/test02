using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Report.Models;
using FingerprintPayment.Report.Functions.BehaviorsReports;
using System.Web.Script.Serialization;
using FingerprintPayment.Report.Models.BehaviorsReports;
using urbanairship;
using System.Web.Script.Services;
using System.Web.Services;
using System.Threading.Tasks;
using System.Threading;

namespace FingerprintPayment.Report
{
    public partial class Behaviorsreportsdetail_2 : BehaviorGateway
    {

        //public DetailModels models { get; set; }
        //public static string ModelJson { get; set; }
        //public static Search search;

        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            { 
                if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            String quser_Id = (String)Request.QueryString["userid"];
            String term_Id = (String)Request.QueryString["termid"];
            String qyear_Id = (String)Request.QueryString["yearid"];
            String qMode = (String)Request.QueryString["Mode"];

           
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);

            hdfschoolname.Value = tCompany.sCompany;

            if (!string.IsNullOrEmpty(qMode) && qMode.ToLower() == "update")
            {
                using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(tCompany.nCompany, ConnectionDB.Read)))
                {
                    int StudentID = int.Parse(quser_Id);
                    jabJaiEntities.SP_CalculateScore(StudentID, tCompany.nCompany, term_Id);
                }
            }
        }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static DetailModels GetData(string quser_Id, string term_Id, string qyear_Id)
        {
            //JWTToken token = new JWTToken();
            //if (!token.CheckToken(HttpContext.Current)) { }

            var userData = GetUserData();
            var models = new DetailModels();
            var search = new Search();
            var schoolID = userData.CompanyID;

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    int nYear;
                    int.TryParse(qyear_Id, out nYear);
                    int student_id;
                    int.TryParse(quser_Id, out student_id);

                    search.student_id = student_id;
                    search.year_Id = nYear;
                    search.term_id = term_Id;
                    var f_year = dbschool.TYears.FirstOrDefault(f => f.SchoolID == schoolID && f.nYear == nYear);
                    var setting = dbschool.TBehaviorSettings.Where(w => w.SchoolID == schoolID).FirstOrDefault();

                    if (setting.Type == 1)
                    {
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.SchoolID == schoolID && f.nTerm.Trim() == search.term_id.Trim());
                        search.dStart = f_term.dStart.Value;
                        search.dEnd = f_term.dEnd.Value;
                    }
                    else
                    {
                        search.dStart = dbschool.TTerms.Where(w => w.nYear == search.year_Id && w.cDel == null && userData.CompanyID == w.SchoolID).Min(min => min.dStart) ?? DateTime.Today;
                        search.dEnd = dbschool.TTerms.Where(w => w.nYear == search.year_Id && w.cDel == null && userData.CompanyID == w.SchoolID).Max(max => max.dEnd) ?? DateTime.Today;
                    }



                    models = reportsDetailType_01.GetReports(dbschool, search);
                    models.term = search.term_id;
                    models.year = f_year.numberYear.ToString();
                    //var ModelJson = new JavaScriptSerializer().Serialize(models);
                    //models.Details = models.Details.OrderByDescending(o => o.ID).ToList();

                    string oldResidual = null;
                    foreach (var data in models.Details.OrderBy(o => o.ID))
                    {
                        if (string.IsNullOrEmpty(data.Status))
                        {
                            oldResidual = data.residualScore;
                        }
                        else
                        {
                            data.residualScore = oldResidual ?? data.residualScore;
                        }
                    }
                    return models;
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
                        var f_data = dbschool.TBehaviorHistories.FirstOrDefault(f => f.BehaviorHistoryId == BehaviorsrId && f.SchoolID == userData.CompanyID && f.cDel == false);
                        var f_student = dbmaster.TUsers.FirstOrDefault(f => f.nSystemID == f_data.StudentId && f.cType == "0" && f.nCompany == tCompany.nCompany);
                        var f_setting = dbschool.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault();
                        int ShowMinusSign = f_setting == null ? 1 : ((f_setting.ShowMinusSign ?? 0) == 1 ? 1 : -1);

                        StudentLogic logic = new StudentLogic(dbschool);
                        string TermId = logic.GetTermId(f_data.dAdd ?? DateTime.Today, userData).Trim();
                        var f_Trem = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == TermId);

                        if (f_data.dCanCel.HasValue)
                        {
                            return "Fail";
                        }
                        f_data.dCanCel = DateTime.Now;
                        f_data.UserCancel = UserId;
                        dbschool.SaveChanges();

                        decimal? ResidualScore = 0;
                        var q_history = dbschool.TBehaviorHistories.Where(w => w.StudentId == f_data.StudentId && w.cDel == false
                        && w.BehaviorHistoryId > BehaviorsrId && w.dCanCel == null && w.SchoolID == userData.CompanyID).ToList();

                        dbschool.SP_CalculateScore(f_data.StudentId, f_data.SchoolID, TermId);

                        //if (q_history.Count() > 0)
                        //{
                        //    DateTime dEnd = DateTime.Today;
                        //    var f_term = dbschool.TTerms.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.nTerm.Trim() == TermId.Trim());
                        //    if (f_setting.Type == 1)
                        //    {
                        //        dEnd = f_term.dEnd.Value;
                        //    }
                        //    else
                        //    {
                        //        var f_year = dbschool.TYears.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.nYear == f_term.nYear);
                        //        dEnd = dbschool.TTerms.Where(w => w.nYear == f_year.nYear && w.cDel == null && userData.CompanyID == w.SchoolID).Max(max => max.dEnd) ?? DateTime.Today;
                        //    }

                        //    dEnd = dEnd.AddDays(1);

                        //    foreach (var data in q_history.Where(w => w.dAdd <= dEnd))
                        //    {
                        //        if (f_data.Type == "0") data.ResidualScore -= f_data.Score;
                        //        else data.ResidualScore += f_data.Score;
                        //        ResidualScore = data.ResidualScore;
                        //    }

                        //    dbschool.SaveChanges();
                        //}


                        //var f_history = dbschool.TBehaviorHistories.OrderByDescending(o => o.dAdd)
                        //    .FirstOrDefault(w => w.dAdd >= f_Trem.dStart && w.dCanCel == null && w.StudentId == f_data.StudentId && w.SchoolID == userData.CompanyID && w.cDel == false);
                        //string Message = string.Format(MessageSystem, f_data.Score, f_data.BehaviorName, f_history == null ? f_setting.MaxScore : (f_history.ResidualScore * ShowMinusSign));

                        //int messageId = messagebox.insert_message(new messagebox.MessageBox
                        //{
                        //    message_type = messagebox.Behaviors,
                        //    title = TitleSystem,
                        //    message = Message,
                        //    school_id = tCompany.nCompany,
                        //    send_time = DateTime.Now,
                        //    user_messagebox = new List<messagebox.user_messagebox>
                        //    {
                        //        new messagebox.user_messagebox
                        //        {
                        //            message_receive = DateTime.Now,
                        //            user_id = f_student.sID,
                        //            user_type  = 0
                        //        }
                        //    }
                        //});

                        //notification _notification = new notification();
                        //_notification.title = TitleSystem;
                        //_notification.message = Message;
                        //_notification.user = f_student.sID.ToString();
                        //_notification.action = "vnd.jabjai.jabjaiapp://deeplink/come_to_school?message_id=" + messageId + "&school_id=" + tCompany.nCompany;
                        //Thread notification = new Thread(async delegate ()
                        //{
                        //    await pushdata.push(_notification);
                        //})
                        //{
                        //    IsBackground = true
                        //};
                        //notification.Start();

                        return "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Fail";
            }
        }
    }
}