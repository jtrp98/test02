using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace FingerprintPayment.Diploma
{
    public partial class DiplomaMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("DiplomaPrint2.aspx?sid=" + stdId.Text + "&cid=" + classroomId.Text);
        }


        [WebMethod]
        public static object LoadDiplomaCode(int sid, int cid)
        {

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            object result = null;

            try
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
                {
                    result = dbSchool.TUser.Where(w=>w.SchoolID == userData.CompanyID).Where(w => w.sID == sid && w.cType == "0").Select(s => new { code = s.DiplomaCode }).FirstOrDefault();
                }
            }
            catch
            {
                result = "error";
            }

            return result;
        }

        [WebMethod]
        public static object SaveDiplomaCode(DiplomaCode diplomaCode)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            object result = "success";

            try
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
                {
                    var user = dbSchool.TUser.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.sID == diplomaCode.sid && f.cType == "0");
                    if (user != null)
                    {
                        user.DiplomaCode = diplomaCode.code;

                        var studentHistory = dbSchool.TStudentHIstories.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2_OLD == diplomaCode.cid && w.sID == diplomaCode.sid && w.cDel == false).FirstOrDefault();
                        if (studentHistory != null)
                        {
                            studentHistory.DiplomaCode = diplomaCode.code;
                        }

                        dbSchool.SaveChanges();
                    }
                    else
                    {
                        result = "error";
                    }
                }
            }
            catch
            {
                result = "error";
            }

            return result;
        }

        public class DiplomaCode
        {
            public int sid { get; set; }
            public int cid { get; set; }
            public string code { get; set; }
        }

    }
}