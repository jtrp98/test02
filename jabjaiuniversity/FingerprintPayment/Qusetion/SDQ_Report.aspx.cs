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
    public partial class SDQ_Report : System.Web.UI.Page
    {

        public static List<ShowDataStudentandFontQuestion> showDataStudentandFontQuestions = new List<ShowDataStudentandFontQuestion>();

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string strEntities = Session["sEntities"].ToString();
            JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(strEntities, ConnectionDB.Read));

            int id = 0;
            Int32.TryParse(Request.QueryString["id"], out id);     

            Image img = (Image)FindControl("profileimage");
            var imgDataShow = jabJaiEntities.TUser.Where(w=>w.SchoolID == userData.CompanyID).Where(w => w.sID == id && w.cDel == null).FirstOrDefault();
            
            if (!IsPostBack)
            {
                JoinDATAtable joinDATAtable = new JoinDATAtable();
                var data = joinDATAtable.student(jabJaiEntities, id,userData.CompanyID);

                sStudentID.Value = data.sStudentID;
                studentName.Value = data.studentName;
                studentClass.Value = data.studentClass;

                profileimage.ImageUrl = imgDataShow.sStudentPicture;

                if (profileimage.ImageUrl == "")
                {
                    profileimage.ImageUrl = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";
                    profileimage.Width = 180;
                    profileimage.Height = 180;
                }

                showDataStudentandFontQuestions = (from a in jabJaiEntities.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                                   join b in jabJaiEntities.T_FSDQ_Answer.Where(w => w.SchoolID == userData.CompanyID).ToList() on a.sID equals b.sId
                                                   join c in jabJaiEntities.T_FSDQ_Question.Where(w => w.SchoolID == userData.CompanyID).ToList() on b.T_FSDQ_Question_Id equals c.T_FSDQ_Question_Id

                                                   where a.sID == id

                                                   group b by new
                                                       {
                                                       c.T_FSDQ_Question_Group
                                                   } into gb 
                                                   select new ShowDataStudentandFontQuestion
                                                   {
                                                       Question_Group = gb.Key.T_FSDQ_Question_Group,
                                                       SumPoint = gb.Sum(s => s.T_FSDQ_Point_Point),     
                                                       SumString = SUM(gb.Key.T_FSDQ_Question_Group, gb.Sum(s => s.T_FSDQ_Point_Point))
                                                   }).ToList();
            }
        }



        private string SUM(int? Question_Group, int? SumPoint)
        {            
            var _ans = "";
            switch (Question_Group) {
                case 1:
                    if (SumPoint <= 5) _ans = "ปกติ";
                    else if (SumPoint == 6 && SumPoint < 7) _ans = "เสี่ยง";
                    else _ans = "มีปัญหา";
                    break;
                case 2:
                    if (SumPoint <= 4) _ans = "ปกติ";
                    else if (SumPoint == 5 && SumPoint < 6) _ans = "เสี่ยง";
                    else _ans = "มีปัญหา";
                    break;
                case 3:
                    if (SumPoint <= 5) _ans = "ปกติ";
                    else if (SumPoint == 6 && SumPoint < 7) _ans = "เสี่ยง";
                    else _ans = "มีปัญหา";
                    break;
                case 4:
                    if (SumPoint <= 3) _ans = "ปกติ";
                    else if (SumPoint == 4 && SumPoint < 5) _ans = "เสี่ยง";
                    else _ans = "มีปัญหา";
                    break;
                case 5:
                    if (SumPoint <= 2) return "มีปัญหา";
                    else if (SumPoint == 3 && SumPoint < 4) _ans = "เสี่ยง";
                    else _ans = "มีปัญหา";
                    break;
                case 6:
                    if (SumPoint == 0) return "ปกติ";
                    else if (SumPoint == 1 && SumPoint <= 2) _ans = "เสี่ยง";
                    else _ans = "มีปัญหา";
                    break;
            }
            return _ans;
        }
       
      

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static T_FSDQ_Answer T_FSDQ_Answer(int T_FSDQ_Answer_Id)
        {
            JabJaiEntities jabJaiEntities = new JabJaiEntities();
            return jabJaiEntities.T_FSDQ_Answer.FirstOrDefault(f => f.T_FSDQ_Answer_Id == T_FSDQ_Answer_Id);

        }

    }

    public class ShowDataStudentandFontQuestion
    {
        public int? Question_Group { get; set; }
        public int? SumPoint { get; set; }
        public string SumString { get; set; }
    }
}