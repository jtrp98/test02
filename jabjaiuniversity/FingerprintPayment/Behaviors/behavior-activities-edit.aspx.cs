using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiNoSQL;
using JabjaiNoSQL.Behavior;
using MasterEntity;
using JabjaiEntity.DB;
using JabjaiMainClass;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class behavior_activities_edit : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnEdit.Click += new EventHandler(btnEdit_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);
            ScriptManager1.RegisterPostBackControl(this.btnEdit);
            ScriptManager1.RegisterPostBackControl(this.btnCancle);
            if (!IsPostBack)
            {
                int schoolID = UserData.CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    if (string.IsNullOrEmpty(Request.QueryString["id"])) Response.Redirect("behavior-activities.aspx");
                    int BehaviorId = int.Parse(Request.QueryString["id"]);
                    var tBehavior = dbschool.TBehaviors.Where(w => w.SchoolID == schoolID && w.BehaviorId == BehaviorId).FirstOrDefault();
                    if (tBehavior == null) Response.Redirect("behavior-activities.aspx");
                    BehaviorName.Value = tBehavior.BehaviorName;
                    Score.Value = tBehavior.Score.Value.ToString();
                    Type.SelectedValue = tBehavior.Type.Value.ToString();
                    hfdsClassID.Value = Request.QueryString["id"];
                }
            }
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BehaviorName.Value) || string.IsNullOrEmpty(Score.Value))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "requiredData", "modal_plane('<span style=\"font-size:20px;\">กรุณากรอกข้อมูลให้ครบถ้วน</span>');", true);
            }
            else
            {
                int schoolID = UserData.CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    int BehaviorId = int.Parse(hfdsClassID.Value.ToString());
                    var tBehavior = dbschool.TBehaviors.Where(w => w.SchoolID == schoolID && w.BehaviorId == BehaviorId).FirstOrDefault();
                    int score = 0;
                    Int32.TryParse(Score.Value, out score);

                    tBehavior.BehaviorName = BehaviorName.Value;
                    tBehavior.Score = score;
                    tBehavior.Type = int.Parse(Type.SelectedValue);
                    dbschool.SaveChanges();
                    database.InsertLog(UserData.UserID.ToString(),
                           "ทำการแก้ไขข้อมูลความประพฤติ " + BehaviorName.Value,
                           UserData.Entities,
                           HttpContext.Current.Request, 4, 3, 0);
                }
            }

            Response.Redirect("behavior-activities.aspx");
        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("behavior-activities.aspx");
        }
    }
}