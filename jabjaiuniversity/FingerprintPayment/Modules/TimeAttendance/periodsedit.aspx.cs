using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class periodsedit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);
            ScriptManager1.RegisterPostBackControl(this.btnSave);
            ScriptManager1.RegisterPostBackControl(this.btnCancle);
            if (!Page.IsPostBack)
            {
                Opendata();
            }
        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("periodslist.aspx");
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string id = Request.QueryString["id"];
            foreach (var data in _db.TPeriods.Where(w => w.sPeriodID == id))
            {
                data.sPeriodName = txtPlaneID.Value;
                data.dStart = DateTime.ParseExact(txtdStart.Value, "HH:mm", new CultureInfo("en-us"));
                data.dEnd = DateTime.ParseExact(txtdEnd.Value, "HH:mm", new CultureInfo("en-us"));
                data.dTimeStart_IN = DateTime.ParseExact(txtdTimeStart_IN.Value, "HH:mm", new CultureInfo("en-us"));
                data.dTimeStart_OUT = DateTime.ParseExact(txtdTimeStart_OUT.Value, "HH:mm", new CultureInfo("en-us"));
                data.dTimeEnd_IN = DateTime.ParseExact(txtdTimeEnd_IN.Value, "HH:mm", new CultureInfo("en-us"));
                data.dTimeEnd_OUT = DateTime.ParseExact(txtdTimeEnd_OUT.Value, "HH:mm", new CultureInfo("en-us"));
            }
            _db.TPeriod_TSubLevel.Where(w => w.sPeriodID == id).ToList().ForEach(f => _db.TPeriod_TSubLevel.Remove(f));
            _db.SaveChanges();
            foreach (string _str in txtListtime.Text.Split(','))
            {
                if (!string.IsNullOrEmpty(_str))
                {
                    TPeriod_TSubLevel _TPeriod_TSubLevel = new TPeriod_TSubLevel();
                    _TPeriod_TSubLevel.sPeriodID = id;
                    _TPeriod_TSubLevel.nTSubLevel = int.Parse(_str);
                    _db.TPeriod_TSubLevel.Add(_TPeriod_TSubLevel);
                }
            }
            _db.SaveChanges();
            Response.Redirect("periodslist.aspx");
        }

        private void Opendata()
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string id = Request.QueryString["id"];
            hdfid.Value = id;
            foreach (var data in _db.TPeriods.Where(w => w.sPeriodID == id))
            {
                txtdEnd.Value = data.dEnd.Value.ToString("HH:mm");
                txtdStart.Value = data.dStart.Value.ToString("HH:mm");
                txtdTimeStart_IN.Value = data.dTimeStart_IN.HasValue ? data.dTimeStart_IN.Value.ToString("HH:mm") : "";
                txtdTimeStart_OUT.Value = data.dTimeStart_OUT.HasValue ? data.dTimeStart_OUT.Value.ToString("HH:mm") : "";
                txtdTimeEnd_IN.Value = data.dTimeEnd_IN.HasValue ? data.dTimeEnd_IN.Value.ToString("HH:mm") : "";
                txtdTimeEnd_OUT.Value = data.dTimeEnd_OUT.HasValue ? data.dTimeEnd_OUT.Value.ToString("HH:mm") : "";
                txtPlaneID.Value = data.sPeriodName;
            }
        }
    }
}