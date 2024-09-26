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
    public partial class EQ_Report : System.Web.UI.Page
    {

        public static List<ShowDataStudentEQ> showDataStudentEQs = new List<ShowDataStudentEQ>();

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string strEntities = Session["sEntities"].ToString();
            using (JabJaiMasterEntities jabJaiMasterEntities = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(strEntities, ConnectionDB.Read)))
            {

                int id = 0;
                Int32.TryParse(Request.QueryString["id"], out id);

                Image img = (Image)FindControl("profileimage");
                var imgDataShow = jabJaiEntities.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sID == id && w.cDel == null).FirstOrDefault();

                if (!IsPostBack)
                {
                    JoinDATAtable joinDATAtable = new JoinDATAtable();
                    var data = joinDATAtable.student(jabJaiEntities, id, userData.CompanyID);

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


                    showDataStudentEQs = (from a in jabJaiEntities.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                          join b in jabJaiEntities.TB_EQ_Answer.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                                          join c in jabJaiEntities.TB_EQ_Question.Where(w => w.SchoolID == userData.CompanyID) on b.TB_EQ_Question_Id equals c.TB_EQ_Question_Id
                                          join d in jabJaiEntities.TB_EQ_Group.Where(w => w.SchoolID == userData.CompanyID) on c.TB_EQ_Question_Group equals d.TB_EQ_Question_Group

                                          where a.sID == id

                                          group (b) by new
                                          {
                                              c.TB_EQ_Question_Group,
                                              d.TB_EQ_Group_Des
                                          }
                                          into gb
                                          select new ShowDataStudentEQ
                                          {
                                              Question_GroupEQ = gb.Key.TB_EQ_Question_Group,
                                              SumPoint_EQ = gb.Sum(sum => sum.TB_EQ_Point_Point),
                                              Group_EQ = gb.Key.TB_EQ_Group_Des
                                          }).ToList();

                }
            }
    }



        //private string SumString_EQ(int? Question_GroupEQ, int? SumPoint_EQ)
        //{
        //    var _ansString = "";
        //    switch (Question_GroupEQ)
        //    {
        //        case 1:
        //            if (SumPoint_EQ <=47) _ansString = "ต่ำกว่าปกติ";
        //            else if (SumPoint_EQ >=48 && SumPoint_EQ <=58 ) _ansString = "เกณฑ์ปกติ";
        //            else _ansString = "สูงกว่าปกติ";
        //            break;
        //        case 2:
        //            if (SumPoint_EQ <=44) _ansString = "ต่ำกว่าปกติ";
        //            else if (SumPoint_EQ >= 45 && SumPoint_EQ <=57) _ansString = "เกณฑ์ปกติ";
        //            else _ansString = "สูงกว่าปกติ";
        //            break;
        //        case 3:
        //            if (SumPoint_EQ <=39) _ansString = "ต่ำกว่าปกติ";
        //            else if (SumPoint_EQ >= 40 && SumPoint_EQ <=55) _ansString = "เกณฑ์ปกติ";
        //            else _ansString = "สูงกว่าปกติ";
        //            break;
        //    }
        //    return _ansString;
        //}
    }

    public class ShowDataStudentEQ
    {
        public int? Question_GroupEQ { get; set; }
        public int? Question_GroupEaQ { get; set; }
        public string Group_EQ { get; set; } /*กรุ๊ปย่อย*/
        public int? SumPoint_EQ { get; set; }
        public int? SumInt_EQ { get; set; }
        public string SumString_EQ { get; set; }
    }
}