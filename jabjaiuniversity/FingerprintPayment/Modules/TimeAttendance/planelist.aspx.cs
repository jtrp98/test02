using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class planelist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            btnSearch.Click += BtnSearch_Click;

            if (!IsPostBack)
            {
                OpenData();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            dgd.CurrentPageIndex = 0;
            OpenData();
        }

        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            sbyte sbPlaneID = fcommon.FindIndexColumnOfDataFieldInGrid(dgd, "sPlaneID");
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
            {
                LinkButton _btnDel = e.Item.FindControl("btnDel") as LinkButton;
                string sPlaneID = e.Item.Cells[sbPlaneID].Text;
                if (_db.TSchedules.Where(W => W.sPlaneID == sPlaneID).Count() == 0)
                    _btnDel.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel.UniqueID + "'); return false;");
                else
                    _btnDel.Visible = false;
            }
        }

        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string sPlaneID = e.CommandArgument.ToString();
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));

            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect("planeedit.aspx?id=" + e.CommandArgument);
                    break;
                case "Del":
                    foreach (var datadel in _db.TPlanes.Where(w => w.sPlaneID == sPlaneID).ToList())
                    {
                        _db.TPlanes.Remove(datadel);
                    }
                    _db.SaveChanges();
                    Response.Redirect("planelist.aspx");
                    break;
                case "Page":
                    dgd.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;
                    OpenData();
                    break;
            }
        }
        private void OpenData()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            int index = 1;

            var _data = (from a in _db.TPlanes.ToList()
                         where a.cDel == null && (a.sPlaneID.Contains(txtSearch.Text) || a.sPlaneName.Contains(txtSearch.Text))
                         select new
                         {
                             rowindex = index++,
                             a.sPlaneName,
                             a.sPlaneID

                         }).ToList();

            dgd.DataSource = _data.ToList();
            dgd.DataBind();
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string updatedata(Plane_data tPlane)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                {
                    if (tPlane.mode == "Add")
                    {
                        if (dbschool.TPlanes.FirstOrDefault(w => w.sPlaneID == tPlane.plane_id) != null) return "";

                        dbschool.TPlanes.Add(new TPlane
                        {
                            sPlaneID = tPlane.plane_id,
                            sPlaneName = tPlane.plane_name
                        });
                    }
                    else
                    {
                        var q = dbschool.TPlanes.Find(tPlane.plane_id);
                        q.sPlaneName = tPlane.plane_name;
                    }
                    dbschool.SaveChanges();
                }
                return "Success";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Plane_data getdata(string plane_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                {
                     var q = (from a in _db.TPlanes.ToList()
                             where a.sPlaneID == plane_id
                             select new Plane_data
                             {
                                 plane_id = a.sPlaneID,
                                 plane_name = a.sPlaneName,
                                 mode = "Edit"
                             }).FirstOrDefault();

                    return q;
                }
            }
        }

        public class Plane_data
        {
            public string plane_id { get; set; }
            public string plane_name { get; set; }
            public string mode { get; set; }
        }
    }
}