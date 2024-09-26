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
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class Behavior_Setting_add : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);
            ScriptManager1.RegisterPostBackControl(this.btnSave);
            ScriptManager1.RegisterPostBackControl(this.btnCancle);
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
                if (string.IsNullOrEmpty(Score.Value))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "requiredData", "modal_plane('<span style=\"font-size:20px;\">กรุณากรอกข้อมูลให้ครบถ้วน</span>');", true);
                }
                else
                {
                    var tBehaviorsetting = dbschool.TBehaviorSettings.Where(w => w.SchoolID == schoolID && w.BehaviorSettingID == 1).FirstOrDefault();
                    tBehaviorsetting.MaxScore = int.Parse(Score.Value);
                    tBehaviorsetting.Type = int.Parse(Type.SelectedValue);

                    dbschool.SaveChanges();

                    database.InsertLog(UserData.UserID.ToString(),
                           "ทำการตั้งค่าคะแนนความประพฤติ ",
                           UserData.Entities,
                           HttpContext.Current.Request, 4, 3, 0);
                }
                Response.Redirect("behavior-setting.aspx");
            }
        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("behavior-setting.aspx");
        }
    }
}