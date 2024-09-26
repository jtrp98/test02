using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class subleveladd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            btnCancel.Click += new EventHandler(btnCancel_Click);
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                if (!this.IsPostBack)
                {
                    SiteMap.Provider.CurrentNode.ParentNode.ReadOnly = false;
                    SiteMap.CurrentNode.ParentNode.Url = "/Modules/TimeAttendance/sublevelsettings.aspx?id=" + Server.UrlEncode(Request.QueryString["id"]) + "&type=" + Server.UrlEncode(Request.QueryString["type"]);
                }
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("sublevelsettings.aspx?id=" + Server.UrlEncode(Request.QueryString["id"]) + "&type=" + Server.UrlEncode(Request.QueryString["type"]));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (requireData())
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    TSubLevel _TSLevel = new TSubLevel();
                    //string sID = STCrypt.DecryptURL(Request.QueryString["id"] + "")[0];
                    //string _type = STCrypt.DecryptURL(Request.QueryString["type"] + "")[0];
                    string sID = Request.QueryString["id"] + "";
                    string _type = Request.QueryString["type"] + "";
                    int dataID = Int32.Parse(sID);
                    //int nID = int.Parse(sID);
                    //if (_db.TSubLevels.ToList().Count == 0)
                    //    _TSLevel.nTSubLevel = 1;
                    //else
                    //    _TSLevel.nTSubLevel = _db.TSubLevels.Max(max => max.nTSubLevel) + 1;

                    _TSLevel.nTimeType = int.Parse(_type);
                    _TSLevel.nTLevel = dataID;
                    _TSLevel.SubLevel = txtSLevel.Text;
                    _TSLevel.SchoolID = userData.CompanyID;

                    _db.TSubLevels.Add(_TSLevel);
                    _db.SaveChanges();
                    int nTermSubLevel2 = 1;
                    if (_db.TTermSubLevel2.Count() > 0)
                    {
                        nTermSubLevel2 = _db.TTermSubLevel2.OrderByDescending(o => o.nTermSubLevel2).Take(1).SingleOrDefault().nTermSubLevel2 + 1;
                    }
                    //for (int i = 1; i <= int.Parse(txtnRooms.Text); i++)
                    //{
                    //    TTermSubLevel2 _TermSubLevel2 = new TTermSubLevel2();
                    //    _TermSubLevel2.nTermSubLevel2 = nTermSubLevel2++;
                    //    _TermSubLevel2.nTermSubLevel2Status = "1";
                    //    _TermSubLevel2.nTSubLevel = _TSLevel.nTSubLevel;
                    //    _TermSubLevel2.nTSubLevel2 = i;
                    //    _TermSubLevel2.nTimeType = int.Parse(_type);
                    //    _db.TTermSubLevel2.Add(_TermSubLevel2);
                    //}
                    _db.SaveChanges();
                    database.InsertLog(Session["sEmpID"] + "", "เพิ่มข้อมูลระดับชั้นเรียน " + _TSLevel.SubLevel,
                        HttpContext.Current.Session["sEntities"].ToString(), Request, 19, 2, 0);
                    Response.Redirect("sublevelsettings.aspx?id=" + Server.UrlEncode(Request.QueryString["id"]) + "&type=" + Server.UrlEncode(Request.QueryString["type"]));
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "<script>$(function(){j_infosell('<span>กรุณากรอกข้อมูลให้ครบถ้วน</span>');return false;});</script>", false);
            }
        }

        bool requireData()
        {
            bool boolData = true;

            if (String.IsNullOrEmpty(txtSLevel.Text))
            {
                boolData = false;
            }

            return boolData;
        }
    }
}