using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using JabjaiNoSQL;
using JabjaiNoSQL.Behavior;
using MasterEntity;
using JabjaiEntity.DB;
using FingerprintPayment.Class;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class behavior_students_reduce : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);
            ScriptManager1.RegisterPostBackControl(this.btnSave);
            ScriptManager1.RegisterPostBackControl(this.btnCancle);

            if (!IsPostBack)
            {
                int schoolID = UserData.CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    int StudentId = 0;
                    Int32.TryParse(Request.QueryString["id"], out StudentId);

                    var Student = dbschool.TUser.Where(w => w.SchoolID == schoolID && w.sID == StudentId).FirstOrDefault();
                    studentName.Value = Student.sName + " " + Student.sLastname;
                    studentID.Value = Student.sIdentification;
                    DropDownList1.DataSource = dbschool.TBehaviors.Where(w => w.SchoolID == schoolID && w.Status == true && w.Type == 1).ToList();
                    DropDownList1.DataTextField = "BehaviorName";
                    DropDownList1.DataValueField = "BehaviorId";
                    DropDownList1.DataBind();
                }
            }
        }

        private int GenListID()
        {
            int ListID = 1;
            //if (_db.TPlane.Count() > 0) ListID = _db.TPlane.Max(m => m.sPlaneID) + 1;
            return ListID;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int StudentId = 0;
                Int32.TryParse(Request.QueryString["id"], out StudentId);
                if (string.IsNullOrEmpty(studentID.Value))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "requiredData", "modal_plane('<span style=\"font-size:20px;\">กรุณากรอกข้อมูลให้ครบถ้วน</span>');", true);
                }
                else
                {
                    int BehaviorsId = int.Parse(DropDownList1.SelectedValue.ToString());
                    var term = dbschool.TTerms.Where(w => w.SchoolID == schoolID && w.dStart <= DateTime.Today && w.dEnd >= DateTime.Today).FirstOrDefault();
                    var tBehaviorHistory = dbschool.TBehaviorHistories.Where(w => w.SchoolID == schoolID && w.cDel == false && w.dAdd >= term.dStart && w.dAdd <= term.dEnd && w.StudentId == StudentId)
                        .OrderByDescending(order => order.BehaviorHistoryId).FirstOrDefault();
                    var tBehaviors = dbschool.TBehaviors.Where(w => w.SchoolID == schoolID && w.BehaviorId == BehaviorsId).FirstOrDefault();
                    var teacherId = UserData.UserID;
                    //int BehaviorHistoryId = dbschool.TBehaviorHistories.Count() == 0 ? 1 : dbschool.TBehaviorHistories.Max(max => max.BehaviorHistoryId) + 1;
                    var tBehaviorSettings = dbschool.TBehaviorSettings.Where(w=>w.SchoolID == schoolID).FirstOrDefault();
                    decimal ResidualScore = (tBehaviorHistory == null ? tBehaviorSettings.MaxScore.Value : tBehaviorHistory.ResidualScore.Value) - tBehaviors.Score.Value;

                    dbschool.TBehaviorHistories.Add(new TBehaviorHistory
                    {
                        BehaviorId = tBehaviors.BehaviorId,
                        BehaviorName = tBehaviors.BehaviorName,
                        dAdd = DateTime.Now,
                        UserAdd = teacherId,
                        Score = tBehaviors.Score,
                        StudentId = StudentId,
                        //BehaviorHistoryId = BehaviorHistoryId,
                        ResidualScore = ResidualScore,
                        Note = Note.Value,
                        Type = tBehaviors.Type.ToString(),
                        SchoolID = UserData.CompanyID,
           
                    });

                    dbschool.SaveChanges();

                    database.InsertLog(UserData.UserID.ToString(),
                          "ทำการตัดคะแนนความประพฤติ ",
                          UserData.Entities,
                          HttpContext.Current.Request, 4, 2, 0);

                    behaviorclass.sendnotification(UserData, StudentId, tBehaviors, ResidualScore);
                }

                int? studentterm = 0;
                int room1 = 0;
                foreach (var data in dbschool.TUser.Where(w => w.SchoolID == schoolID && w.sID == StudentId))
                {
                    studentterm = data.nTermSubLevel2;
                }
                foreach (var data in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolID && w.nTermSubLevel2 == studentterm))
                {
                    room1 = data.nTSubLevel;
                }
                string link = "behavior-students.aspx?idlv=" + room1 + "&idlv2=" + studentterm + "&sname=";
                Response.Redirect(link);
            }
        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("behavior-students.aspx");
        }
    }
}