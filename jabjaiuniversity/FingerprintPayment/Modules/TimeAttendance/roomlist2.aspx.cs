using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class roomlist2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            if (!IsPostBack)
            {
            }
        }

        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                //LinkButton _btnDel = e.Item.FindControl("btnDel") as LinkButton;
                //_btnDel.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel.UniqueID + "'); return false;");
            }
        }
        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string sClassID = e.CommandArgument.ToString();
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            switch (e.CommandName)
            {
                case "Del":
                    _db.TClasses.Where(w => w.sClassID == sClassID).ToList().ForEach(f => f.cDel = "1");
                    _db.SaveChanges();
                    Opendata();
                    break;
                case "Edit":
                    foreach (var _data in _db.TClasses.Where(w => w.sClassID == sClassID))
                    {
                        txtsClass.Text = _data.sClass;
                        txtsClassIP.Text = _data.sClassIP;
                        hfdsClassID.Value = _data.sClassID;
                    }
                    break;
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {

            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            TClass _Class = new TClass();
            _Class.sClass = txtsClass.Text;
            _Class.sClassIP = txtsClassIP.Text;
            if (string.IsNullOrEmpty(hfdsClassID.Value))
            {
                _Class.sClassID = GenID();
                _db.TClasses.Add(_Class);
            }
            else
            {
                string sClassID = hfdsClassID.Value.ToString();
                _Class.sClassID = sClassID;
                List<TClass> _oldData = _db.TClasses.Where(w => w.sClassID == sClassID).ToList();
                //_db.TClass.ApplyCurrentValues(_Class);
            }

            _db.SaveChanges();
            Response.Redirect("roomlist.aspx");
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/");
        }
        private void Opendata()
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            var data = _db.TClasses.Where(w => string.IsNullOrEmpty(w.cDel));
            //if (data.Count() > 0)
            //{
                dgd.DataSource = data.ToList();
                dgd.DataBind();
            //}
            //else {
            //    dgd.DataSource = null;
            //    dgd.DataBind();
            //}
        }
        private string GenID()
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
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