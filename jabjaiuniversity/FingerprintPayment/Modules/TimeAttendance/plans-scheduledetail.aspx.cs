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
    public partial class plans_scheduledetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SiteMap.Provider.CurrentNode.ParentNode.ReadOnly = false;
            //SiteMap.CurrentNode.ParentNode.Url = "/Modules/TimeAttendance/plans-room.aspx?idterm=" + Server.UrlEncode(Request.QueryString["idterm"]);

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            if (!IsPostBack)
            {
                OpenData();
            }
        }
        private void OpenData()
        {
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
                int nTSubLevel2 = int.Parse(Request.QueryString["id"]);
                string idterm = Request.QueryString["idterm"];

                var _listterm = _db.TTerms.Where(w => w.SchoolID == schoolid && w.nTerm == idterm);
                var _teacher = from a in _db.TClassMembers
                               join b in _db.TEmployees.Where(w => w.cDel == null)
                               on a.nTeacherHeadid equals b.sEmp
                               where a.SchoolID == schoolid && b.SchoolID == schoolid && a.nTermSubLevel2 == nTSubLevel2 && a.nTerm.Trim() == idterm
                               select new { a, b };

                foreach (var _data in _teacher)
                {
                    ltrTeacher.Text = _data.b.sName + " " + _data.b.sLastname;
                }
                foreach (var _data in _listterm)
                {
                    ltrTerm.Text = _data.sTerm;
                    foreach (var _datayear in _db.TYears.Where(w => w.nYear == _data.nYear.Value && w.cDel == false))
                    {
                        ltrYear.Text = _datayear.numberYear.Value.ToString();
                    }
                }

                var _listroom = from a in _db.TSubLevels
                                join b in _db.TTermSubLevel2 on a.nTSubLevel equals b.nTSubLevel
                                where a.SchoolID == schoolid && b.SchoolID == schoolid && b.nTermSubLevel2 == nTSubLevel2
                                select new { a.SubLevel, b.nTSubLevel2 };
                foreach (var _data in _listroom)
                {
                    ltrLevel.Text = _data.SubLevel.Trim();
                    ltrSubLv.Text = _data.nTSubLevel2.ToString();
                }
            }
        }
    }
}