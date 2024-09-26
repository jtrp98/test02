using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class plans_scheduleold : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //SiteMap.Provider.CurrentNode.ParentNode.ReadOnly = false;
            //SiteMap.CurrentNode.ParentNode.Url = "/Modules/TimeAttendance/plans-room.aspx?idterm=" + Server.UrlEncode(Request.QueryString["idterm"]);

            if (Session["sEmpID"] == null) Response.Redirect("/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (!IsPostBack)
            {
                OpenData();
            }
        }
        private void OpenData()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            int nTSubLevel2 = int.Parse(Request.QueryString["id"]);
            string idterm = Request.QueryString["idterm"];
            var _listterm = _db.TTerms.Where(w => w.nTerm == idterm).ToList();
            foreach (var _data in _listterm)
            {
                ltrTerm.Text = _data.sTerm;
                foreach (var _datayear in _db.TYears.Where(w => w.nYear == _data.nYear.Value))
                {
                    ltrYear.Text = _datayear.numberYear.Value.ToString();
                }
            }

            var _listroom = from a in _db.TSubLevels.ToList()
                            join b in _db.TTermSubLevel2.ToList() on a.nTSubLevel equals b.nTSubLevel
                            where b.nTermSubLevel2 == nTSubLevel2
                            select new { a.SubLevel, b.nTSubLevel2 };
            foreach (var _data in _listroom)
            {
                ltrSubLv.Text = _data.SubLevel.Trim() + " / " + _data.nTSubLevel2;
            }
            foreach (var _data in _db.TTermTimeTables.Where(w => w.nTerm == idterm && w.nTermSubLevel2 == nTSubLevel2))
            {
                txtaddteacher.Value = _db.TEmployees.Where(w => w.sEmp == _data.nTeacher)
                    .Take(1).Select(s => new { sname = s.sName + " " + s.sLastname }).SingleOrDefault().sname;
                txtaddteacherid.Value = _data.nTeacher.Value.ToString();
            }
        }
    }
}