using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json.Linq;
using JabjaiMainClass;
using FingerprintPayment.Class;

namespace FingerprintPayment.Qusetion
{
    public partial class EqQuestion : System.Web.UI.Page
    {
        public static List<EQquestion> eQquestions = new List<EQquestion>();

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string strEntities = Session["sEntities"].ToString();
            JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(strEntities,ConnectionDB.Read));

            int id = 0;
            Int32.TryParse(Request.QueryString["id"], out id);

            if (!IsPostBack)
            {
                JoinDATAtable joinDATAtable = new JoinDATAtable();
                var data = joinDATAtable.student(jabJaiEntities, id, userData.CompanyID);

                txt_Font_Name.Value = data.studentName + " " + data.studentLastname;
                txt_Font_Class.Value = data.studentClass;

                if (data.studentSEX == "1")
                {
                    radioGender.SelectedIndex = 1;
                }
                else radioGender.SelectedIndex = 2;

                if (data.studentNumber.HasValue)
                {
                    txt_Font_Number.Value = data.studentNumber.Value.ToString();
                }

                eQquestions = (from a in jabJaiEntities.TB_EQ_Question.Where(w => w.SchoolID == userData.CompanyID)
                               join b in jabJaiEntities.TB_EQ_Point.Where(w => w.SchoolID == userData.CompanyID) on a.TB_EQ_Question_Id equals b.TB_EQ_Question_Id
                               group b by new
                               {
                                   a.TB_EQ_Question_Id,
                                   a.TB_EQ_Question_Des,
                                   a.TB_EQ_Question_Group
                               } into gb
                               select new EQquestion
                               {
                                   TB_EQ_Question_Id = gb.Key.TB_EQ_Question_Id,
                                   TB_EQ_Question_Group = gb.Key.TB_EQ_Question_Group,
                                   TB_EQ_Question_Des = gb.Key.TB_EQ_Question_Des,
                                   TB_EQ_Points = gb.Where(w => w.TB_EQ_Question_Id == gb.Key.TB_EQ_Question_Id)
                               }).ToList();
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static int upDateDataEQ(int sID, List<TB_EQ_Answer> tB_EQ_Answers)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string strEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(strEntities, ConnectionDB.Read)))
            {
                //int TB_EQ_Data = entities.TB_EQ_Data.Select(s => s.TB_EQ_Data_Id).DefaultIfEmpty(0).Max() + 1;
                TB_EQ_Data data = new TB_EQ_Data
                {
                    //TB_EQ_Data_Id = TB_EQ_Data,
                    TB_EQ_Data_Datetime = DateTime.Now,
                    sID = sID,
                    SchoolID = userData.CompanyID
                };

                entities.TB_EQ_Data.Add(data);

                foreach (var answerData in tB_EQ_Answers)
                {
                    entities.TB_EQ_Answer.Add(new TB_EQ_Answer
                    {
                        TB_EQ_Question_Id = answerData.TB_EQ_Question_Id,
                        TB_EQ_Point_Point = answerData.TB_EQ_Point_Point,
                        sID = sID,
                        SchoolID = userData.CompanyID
                    });
                }

                entities.SaveChanges();
                return data.TB_EQ_Data_Id;
            }
        }
    }

    public class EQquestion
    {
        public int TB_EQ_Question_Id { get; set; }
        public string TB_EQ_Question_Des { get; set; }
        public int? TB_EQ_Question_Group { get; set; }
        public IEnumerable<TB_EQ_Point> TB_EQ_Points { get; set; }
    }
}