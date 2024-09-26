using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class levelsettings : System.Web.UI.Page
    {
        [WebMethod]
        public static string GetPermission()
        {
            return mp.Permission_Page.permission;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            rblTimeType.SelectedIndexChanged += new EventHandler(rblTimeType_SelectedIndexChanged);
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));

            if (!Page.IsPostBack)
            {
                var tTimetypes = _db.TTimetypes.Where(w => w.cUserType == "1" && w.cType == "1" && w.cDel == null).ToList();
                rblTimeType.DataSource = tTimetypes;
                rblTimeType.DataTextField = "sTimeType";
                rblTimeType.DataValueField = "nTimeType";
                rblTimeType.DataBind();
                rblTimeType.SelectedIndex = 0;
            }
            OpenData();
            ListHeader();
        }

        void rblTimeType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OpenData()
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString())))
            {
                int _nTimeType = int.Parse(rblTimeType.SelectedValue.ToString());

                var tLevels = _db.TLevels.Where(w => w.nTimeType == _nTimeType).ToList();
                var tSubLevels = _db.TSubLevels.ToList().ToList();

                var _level = (from a in tLevels
                              join b in tSubLevels on a.LevelID equals b.nTLevel into jleft
                              from b in jleft.DefaultIfEmpty()
                              group a by new { a.LevelID, a.LevelName }
                              into grp
                              select new
                              {
                                  nTLevel = grp.Key.LevelID,
                                  LevelName = grp.Key.LevelName,
                                  nCout = tSubLevels.Where(w => w.nTLevel == grp.Key.LevelID).Select(s => s.nTLevel).Count()
                              }).ToList();

                dgd.DataSource = _level;
                dgd.DataBind();
            }
        }

        private void ListHeader()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            ltrTabHeader.Text = "<ul class='nav nav-tabs' style='width: 90%; font-size: 30px; '>";
            bool _status = true;
            int _index = 0;
            foreach (var _data in _db.TTimetypes.Where(w => w.cType == "1" && w.cUserType == "1"))
            {
                _status = int.Parse(rblTimeType.SelectedValue.ToString()) == _data.nTimeType;
                ltrTabHeader.Text += "<li " + (_status ? "class='active'" : "") + @"><a data-toggle='tab' style='color:#000;' href='#' onclick='GetData(" + "" + _index++ + "" + ")'>" + _data.sTimeType + "</a></li>";
                _status = false;
            }
            ltrTabHeader.Text += @"</ul>";

        }

        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string _type = Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", rblTimeType.SelectedValue.ToString())));
            switch (e.CommandName.ToString())
            {
                case "Data":
                    string _id = Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", int.Parse(e.Item.Cells[0].Text))));
                    Response.Redirect("sublevelsettings.aspx?id=" + _id + "&type=" + _type);
                    break;

                case "Add":
                    Response.Redirect("leveladd.aspx?type=" + _type);
                    break;

                case "Del":
                    int nTLevel = int.Parse(e.Item.Cells[0].Text);
                    JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
                    _db.TLevels.Where(w => w.LevelID == nTLevel).ToList().ForEach(f => _db.TLevels.Remove(f));
                    _db.SaveChanges();
                    OpenData();
                    break;
            }
        }
    }
}