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
    public partial class planeedit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnEdit.Click += new EventHandler(btnEdit_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            ScriptManager1.RegisterPostBackControl(this.btnEdit);
            ScriptManager1.RegisterPostBackControl(this.btnCancle);
            if (!IsPostBack)
            {
                string planeID = Request.QueryString["id"];
                foreach (var _data in _db.TPlanes.Where(w => w.sPlaneID == planeID))
                {
                    txtPlaneID.Value = _data.sPlaneID;
                    txtPlaneName.Value = _data.sPlaneName;
                    hfdsClassID.Value = Request.QueryString["id"];
                }
            }
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (string.IsNullOrEmpty(txtPlaneID.Value) || string.IsNullOrEmpty(txtPlaneName.Value))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "requiredData", "modal_plane('<span style=\"font-size:20px;\">กรุณากรอกข้อมูลให้ครบถ้วน</span>');", true);
            }
            else
            {
                if (txtPlaneID.Value != hfdsClassID.Value)
                {
                    string sPlaneID = txtPlaneID.Value;
                    int oldData = _db.TPlanes.Where(w => w.sPlaneID == sPlaneID).Count();
                    if (oldData > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "wrongPlaneID", "modal_plane('<span style=\"font-size:20px;\">รหัสรายวิชาเคยทำการบันทึกแล้ว</span>');", true);
                    }
                    else
                    {
                        sPlaneID = hfdsClassID.Value.ToString();
                        TPlane tPlane = _db.TPlanes.Find(sPlaneID);
                        tPlane.sPlaneID = txtPlaneID.Value;
                        tPlane.sPlaneName = txtPlaneName.Value;
                        //_db.TPlanes.ApplyCurrentValues(_Plane);
                        _db.SaveChanges();
                        database.InsertLog(Session["sEmpID"] + "", "แก้ไขข้อมูลวิชา", Session["sEntities"].ToString(),
                            Request, 0, 3, 0);
                        Response.Redirect("planelist.aspx");
                    }
                }
                else
                {
                    string sPlaneID = hfdsClassID.Value.ToString();
                    TPlane tPlane = _db.TPlanes.Find(sPlaneID);
                    tPlane.sPlaneID = txtPlaneID.Value;
                    tPlane.sPlaneName = txtPlaneName.Value;
                    _db.SaveChanges();
                    database.InsertLog(Session["sEmpID"] + "", "แก้ไขข้อมูลวิชา", Session["sEntities"].ToString(),
                        Request, 0, 3, 0);
                    Response.Redirect("planelist.aspx");
                }
            }
        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("planelist.aspx");
        }
    }
}