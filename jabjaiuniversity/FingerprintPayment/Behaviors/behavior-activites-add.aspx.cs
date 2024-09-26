using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class behavior_activites_add : BehaviorGateway
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
                if (string.IsNullOrEmpty(BehaviorName.Value) || string.IsNullOrEmpty(Score.Value))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "requiredData", "modal_plane('<span style=\"font-size:20px;\">กรุณากรอกข้อมูลให้ครบถ้วน</span>');", true);
                }
                else
                {
                    int score = 0;
                    Int32.TryParse(Score.Value, out score);

                    //int BehaviorId = dbschool.TBehaviors.Count() == 0 ? 1 : dbschool.TBehaviors.Where(w => w.BehaviorId < 9000).Max(max => max.BehaviorId) + 1;
                    dbschool.TBehaviors.Add(new TBehavior
                    {
                        //BehaviorId = BehaviorId,
                        BehaviorName = BehaviorName.Value,
                        Type = int.Parse(Status.SelectedValue),
                        dAdd = DateTime.Now,
                        Status = true,
                        Score = score,
                        UserAdd = UserData.UserID,
                        SchoolID = schoolID
                    });

                    database.InsertLog(UserData.UserID.ToString(),
                            "ทำการเพิ่มข้อมูลความประพฤติ " + BehaviorName.Value,
                            UserData.Entities,
                            HttpContext.Current.Request, 4, 2, 0);
                    dbschool.SaveChanges();
                }

                Response.Redirect("behavior-activities.aspx");
            }
        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("behavior-activities.aspx");
        }
    }
}