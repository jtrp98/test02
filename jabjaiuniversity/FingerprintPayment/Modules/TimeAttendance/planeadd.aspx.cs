using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class planeadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);
            ScriptManager1.RegisterPostBackControl(this.btnSave);
            ScriptManager1.RegisterPostBackControl(this.btnCancle);
        }


        private int GenListID()
        {
            int ListID = 1;
            //if (_db.TPlanes.Count() > 0) ListID = _db.TPlanes.Max(m => m.sPlaneID) + 1;
            return ListID;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (string.IsNullOrEmpty(txtPlaneID.Value) || string.IsNullOrEmpty(txtPlaneName.Value))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "requiredData", "modal_plane('<span style=\"font-size:20px;\">กรุณากรอกข้อมูลให้ครบถ้วน</span>');", true);
            }
            else
            {

                TPlane _Plane = new TPlane();
                _Plane.sPlaneID = txtPlaneID.Value;
                _Plane.sPlaneName = txtPlaneName.Value;

                string sPlaneID = txtPlaneID.Value;
                List<TPlane> _oldData = _db.TPlanes.Where(w => w.sPlaneID == sPlaneID && w.cDel == null).ToList();
                if (_oldData.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "wrongPlaneID", "modal_plane('<span style=\"font-size:20px;\">รหัสรายวิชาเคยทำการบันทึกแล้ว</span>');", true);
                }
                else
                {
                    foreach (string _str in txtListtime.Text.Split(','))
                    {
                        if (!string.IsNullOrEmpty(_str))
                        {
                            TPlane_TSubLevel _TPlane_TSubLevel = new TPlane_TSubLevel();
                            _TPlane_TSubLevel.nTSubLevel = int.Parse(_str);
                            _TPlane_TSubLevel.sPlaneID = txtPlaneID.Value;
                            _db.TPlane_TSubLevel.Add(_TPlane_TSubLevel);
                        }
                    }
                    _db.TPlanes.Add(_Plane);
                    _db.SaveChanges();
                }
            }
            Response.Redirect("planelist.aspx");
        }


        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("planelist.aspx");
        }

    }
}