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
    public partial class SdqQuestion : System.Web.UI.Page
    {
        public static List<SDQ_DATA_QuestionA> SDQ_DATA_QuestionA = new List<SDQ_DATA_QuestionA>();

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string strEntities = Session["sEntities"].ToString();
            JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(strEntities, ConnectionDB.Read));

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
                }/*ฟิล nStudentNumber เป็นค่า int ต้องกำหนดค่าให้เป็นString คือการ to.String แต่ String เป็นค่า ว่างหรือNULL ไม่ได้ต้องใส่ค่าให้มันเสอมคือ .HasValue */

                SDQ_DATA_QuestionA = (from a in jabJaiEntities.T_FSDQ_Question
                                      join b in jabJaiEntities.T_FSDQ_Point.Where(w => w.SchoolID == userData.CompanyID) on a.T_FSDQ_Question_Id equals b.T_FSDQ_Question_Id

                                      group b by new
                                      {
                                          a.T_FSDQ_Question_Id,
                                          a.T_FSDQ_Question_Des,
                                          a.T_FSDQ_Question_Group
                                      }
                                      into gb
                                      select new SDQ_DATA_QuestionA
                                      {
                                          T_FSDQ_Question_Id = gb.Key.T_FSDQ_Question_Id,
                                          T_FSDQ_Question_Group = gb.Key.T_FSDQ_Question_Group,
                                          T_FSDQ_Question_Des = gb.Key.T_FSDQ_Question_Des,
                                          T_FSDQ_Points = gb.Where(w => w.T_FSDQ_Question_Id == gb.Key.T_FSDQ_Question_Id)

                                      }).ToList();

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static int updateData(int sID, List<T_FSDQ_Answer> t_FSDQ_Answers)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string strEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(strEntities, ConnectionDB.Read)))
            {
                //int T_FSDQ_DATA = entities.T_FSDQ_Data.Select(s => s.T_FSDQ_Data_Id).DefaultIfEmpty(0).Max() + 1;
                T_FSDQ_Data data = new T_FSDQ_Data
                {
                    //T_FSDQ_Data_Id = T_FSDQ_DATA,
                    T_FSDQ_Data_Datetime = DateTime.Now,
                    sID = sID,
                    SchoolID = userData.CompanyID
                };

                entities.T_FSDQ_Data.Add(data);

                foreach (var answerData in t_FSDQ_Answers)
                {
                    entities.T_FSDQ_Answer.Add(new T_FSDQ_Answer
                    {
                        T_FSDQ_Question_Id = answerData.T_FSDQ_Question_Id,
                        T_FSDQ_Point_Point = answerData.T_FSDQ_Point_Point,
                        sId = sID,
                        SchoolID = userData.CompanyID
                    });
                }

                entities.SaveChanges();
                return data.T_FSDQ_Data_Id;
            }
        }
    }

    public class SDQ_DATA_QuestionA
    {
        public int T_FSDQ_Question_Id { get; set; }
        public string T_FSDQ_Question_Des { get; set; }
        public int? T_FSDQ_Question_Group { get; set; }
        public IEnumerable<T_FSDQ_Point> T_FSDQ_Points { get; set; }
    }

}