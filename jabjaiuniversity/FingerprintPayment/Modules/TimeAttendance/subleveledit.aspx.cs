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
    public partial class subleveledit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            btnCancel.Click += new EventHandler(btnCancel_Click);
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                if (!this.IsPostBack)
                {
                    SiteMap.Provider.CurrentNode.ParentNode.ReadOnly = false;
                    SiteMap.CurrentNode.ParentNode.Url = "/Modules/TimeAttendance/sublevelsettings.aspx?id=" + Server.UrlEncode(Request.QueryString["id"]) + "&type=" + Server.UrlEncode(Request.QueryString["type"]);
                    fcommon.ListDataTableToDropDownList(fcommon.LinqToDataTable(_db.TTimetypes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cUserType == "1")), ddlnTimeType, "", "nTimeType", "sTimeType");
                    Opendata();
                }
            }
        }

        private void Opendata()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                int _subid = int.Parse((Request.QueryString["subid"] + ""));
                int _sublv2 = string.IsNullOrEmpty(Request.QueryString["sublv2"]) ? 0 : int.Parse(Request.QueryString["sublv2"]);
                string _key = (Request.QueryString["key"] + "");
                ltrSLevel.Text = _key;
                if (_sublv2 == 0)
                {
                    foreach (var _TSLevel in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTSubLevel == _subid))
                    {
                        ddlnTimeType.SelectedValue = _TSLevel.nTimeType.Value.ToString();
                    }
                }
                else
                {
                    foreach (var _TSLevel in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2 == _sublv2))
                    {
                        ddlnTimeType.SelectedValue = _TSLevel.nTimeType.Value.ToString();
                    }
                }

            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("sublevelsettings.aspx?id=" + Server.UrlEncode(Request.QueryString["id"]) + "&type=" + Server.UrlEncode(Request.QueryString["type"]));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (requireData())
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    string sID = (Request.QueryString["id"] + "");
                    int _subid = int.Parse((Request.QueryString["subid"] + ""));
                    int nTermSubLevel2 = string.IsNullOrEmpty(Request.QueryString["sublv2"]) ? 0 : int.Parse(Request.QueryString["sublv2"]);
                    int dataID = Int32.Parse(sID);
                    //int nID = int.Parse(sID);

                    List<TTermSubLevel2> _lTermSubLevel2 = _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(W => W.nTSubLevel == _subid).ToList();
                    if (nTermSubLevel2 > 0)
                    {
                        _lTermSubLevel2 = _lTermSubLevel2.Where(wde => wde.nTermSubLevel2 == nTermSubLevel2).ToList();
                    }

                    foreach (var _TSLevel in _lTermSubLevel2)
                    {
                        _TSLevel.nTimeType = int.Parse(ddlnTimeType.SelectedValue);
                    }
                    _db.SaveChanges();
                    database.InsertLog(Session["sEmpID"] + "", "เพิ่มข้อมูลระดับชั้นเรียน ",
                        HttpContext.Current.Session["sEntities"].ToString(), Request, 19, 3, 0);
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

            if (String.IsNullOrEmpty(ltrSLevel.Text))
            {
                boolData = false;
            }

            return boolData;
        }
    }
}
