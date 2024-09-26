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
    public partial class roomadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            btnCancel.Click += BtnCancel_Click;
            btnSave.Click += BtnSave_Click;
            if (!Page.IsPostBack)
            {
                hfdsClassID.Value = Request.QueryString["id"] + "";
                if (!string.IsNullOrEmpty(hfdsClassID.Value)) Opendata();
            }
        }

        private void Opendata()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            foreach (var _data in _db.TClasses.Where(w=>w.SchoolID == userData.CompanyID).Where(w => w.sClassID == hfdsClassID.Value))
            {
                txtsClass.Text = _data.sClass;
                txtsClassIP.Text = _data.sClassIP;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            TClass _Class = new TClass();
            _Class.sClass = txtsClass.Text;
            _Class.sClassIP = txtsClassIP.Text;
            if (string.IsNullOrEmpty(hfdsClassID.Value))
            {
                _Class.sClassID = GenID();
                _Class.SchoolID = userData.CompanyID;
                _db.TClasses.Add(_Class);
            }
            else
            {
                //string sClassID = hfdsClassID.Value.ToString();
                //_Class.sClassID = sClassID;
                foreach (var _data in _db.TClasses.Where(w => w.sClassID == hfdsClassID.Value).ToList())
                {
                    _data.sClass = txtsClass.Text;
                }
                //_db.TClass.ApplyCurrentValues(_Class);
            }

            _db.SaveChanges();
            Response.Redirect("roomlist.aspx");
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("roomlist.aspx");
        }

        private string GenID()
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            string sClass = "CL0001";
            if (_db.TClasses.Count() > 0)
            {
                sClass = _db.TClasses.OrderByDescending(O => O.sClassID).FirstOrDefault().sClassID;
                sClass = "CL" + string.Format("{0:0000}", int.Parse(sClass.Replace("CL", "")) + 1);
            }
            return sClass;
        }
    }
}