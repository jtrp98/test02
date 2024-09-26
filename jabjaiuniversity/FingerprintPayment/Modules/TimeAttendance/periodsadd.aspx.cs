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
    public partial class periodsadd : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            btnCancle.Click += new EventHandler(btnCancle_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            ScriptManager1.RegisterPostBackControl(this.btnSave);
            ScriptManager1.RegisterPostBackControl(this.btnCancle);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            TPeriod _TPeriod = new TPeriod();
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sPeriodID = "P00001";
            if (_db.TPeriods.Count() > 0)
            {
                sPeriodID = _db.TPeriods.Take(1).OrderByDescending(o => o.sPeriodID).SingleOrDefault().sPeriodID;
                sPeriodID = string.Format("P{0:00000}", int.Parse(sPeriodID.Replace("P", "")) + 1);
            }
            _TPeriod.sPeriodID = sPeriodID;
            _TPeriod.sPeriodName = txtPlaneID.Value;
            _TPeriod.dStart = DateTime.ParseExact(txtdStart.Value, "HH:mm", new CultureInfo("en-us"));
            _TPeriod.dEnd = DateTime.ParseExact(txtdEnd.Value, "HH:mm", new CultureInfo("en-us"));
            _TPeriod.dTimeStart_IN = DateTime.ParseExact(txtdTimeStart_IN.Value, "HH:mm", new CultureInfo("en-us"));
            _TPeriod.dTimeStart_OUT = DateTime.ParseExact(txtdTimeStart_OUT.Value, "HH:mm", new CultureInfo("en-us"));
            _TPeriod.dTimeEnd_IN = DateTime.ParseExact(txtdTimeEnd_IN.Value, "HH:mm", new CultureInfo("en-us"));
            _TPeriod.dTimeEnd_OUT = DateTime.ParseExact(txtdTimeEnd_OUT.Value, "HH:mm", new CultureInfo("en-us"));
            _db.TPeriods.Add(_TPeriod);
            foreach (string _str in txtListtime.Text.Split(','))
            {
                if (!string.IsNullOrEmpty(_str))
                {
                    TPeriod_TSubLevel _TPeriod_TSubLevel = new TPeriod_TSubLevel();
                    _TPeriod_TSubLevel.sPeriodID = sPeriodID;
                    _TPeriod_TSubLevel.nTSubLevel = int.Parse(_str);
                    _db.TPeriod_TSubLevel.Add(_TPeriod_TSubLevel);
                }
            }
            _db.SaveChanges();
            Response.Redirect("periodslist.aspx");
        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("periodslist.aspx");
        }
    }
}