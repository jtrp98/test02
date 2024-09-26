using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Web.Services;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class leveladd : System.Web.UI.Page
    {

        [WebMethod]
        public static string GetPermission()
        {
            return mp.Permission_Page.permission;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            btnCancel.Click += new EventHandler(btnCancel_Click);
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("levelsettings.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (requireData())
            {
                JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
                int _nTimeType = int.Parse(STCrypt.DecryptURL(Request.QueryString["type"])[0]);
                TLevel _TLevel = new TLevel();
                //int nID = int.Parse(sID);
                if (_db.TLevels.ToList().Count == 0)
                    _TLevel.LevelID = 1;
                else
                    _TLevel.LevelID = _db.TLevels.Max(m => m.LevelID) + 1;

                _TLevel.nTimeType = _nTimeType;
                _TLevel.LevelName = txtLevel.Text;
                _db.TLevels.Add(_TLevel);
                _db.SaveChanges();
                database.InsertLog(Session["sEmpID"] + "", "เพิ่มข้อมูลระดับชั้น " + _TLevel.LevelName, Session["sEntities"].ToString(),
                    Request, 19, 2, 0);
                Response.Redirect("levelsettings.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "<script>$(function(){j_infosell('<span>กรุณากรอกข้อมูลให้ครบถ้วน</span>');return false;});</script>", false);
            }
        }

        bool requireData()
        {
            bool boolData = true;

            if (String.IsNullOrEmpty(txtLevel.Text))
            {
                boolData = false;
            }

            return boolData;
        }
    }
}