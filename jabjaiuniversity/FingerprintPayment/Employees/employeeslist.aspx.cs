using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class employesslist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            //btnSearch.Click += new EventHandler(btnSearch_Click);
            txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            if (!IsPostBack)
            {
                Opendata("");
                int sEmpID = int.Parse(Session["sEmpID"] + "");
                dgd.Columns[5].Visible = fcommon.SettingPermission(sEmpID, 10, HttpContext.Current.Session["sEntities"].ToString());
                dgd.Columns[4].Visible = fcommon.SettingPermission(sEmpID, 10, HttpContext.Current.Session["sEntities"].ToString());
            }
        }
        void txtSearch_TextChanged(object sender, EventArgs e)
        {

            dgd.CurrentPageIndex = 0;
            Opendata(txtSearch.Text);
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            dgd.CurrentPageIndex = 0;
            Opendata(txtSearch.Text);
        }
        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            sbyte sEmp = fcommon.FindIndexColumnOfDataFieldInGrid(dgd, "sEmp");
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer && e.Item.ItemType != ListItemType.Pager)
            {
                LinkButton btnEdit = e.Item.FindControl("btnEdit") as LinkButton;
                Button _btnDel = e.Item.FindControl("btnDel") as Button;
                if (e.Item.Cells[sEmp].Text == Session["sEmpID"].ToString())
                {
                    btnEdit.Visible = false;
                    _btnDel.Visible = false;
                }

                sbyte cfinger = fcommon.FindIndexColumnOfDataFieldInGrid(dgd, "finger");
                if (e.Item.Cells[cfinger].Text.ToLower() == "false")
                {
                    e.Item.BackColor = System.Drawing.Color.Red;
                    e.Item.ForeColor = System.Drawing.Color.White;
                }
                //_btnDel.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel.UniqueID + "');return false;");
            }
        }
        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            sbyte sEmp = fcommon.FindIndexColumnOfDataFieldInGrid(dgd, "sEmp");
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect("employeesedit.aspx?id=" + Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", int.Parse(e.Item.Cells[sEmp].Text)))));
                    break;
                case "Page":
                    dgd.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;
                    Opendata(txtSearch.Text);
                    break;
                case "Del":
                    int ID = int.Parse(e.Item.Cells[sEmp].Text);
                    using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                    {
                        string sEntities = Session["sEntities"].ToString();
                        var tcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                        dbmaster.TUsers.Where(w => w.nSystemID == ID && w.nCompany == tcompany.nCompany && w.cType != "0").ToList().ForEach(f => f.cDel = "1");
                        dbmaster.SaveChanges();
                        using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                        {
                            foreach (var _data in dbschool.TEmployees.Where(w => w.sEmp == ID).ToList())
                            {
                                _data.cDel = "1";
                                database.InsertLog(Session["sEmpID"] + "",
                                    "ลบข้อมูลพนักงาน " + _data.sName + " " + _data.sLastname,
                                    HttpContext.Current.Session["sEntities"].ToString(),
                                    Request, 13, 4, 0);
                            }
                            dbschool.SaveChanges();
                        }
                    }
                    Opendata("");
                    break;
            }
        }
        private void Opendata(string sName)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString())))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tEmployees = _db.TEmployees.Where(w => w.cDel == null).ToList();
                    var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var tMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType != "0").ToList();
                    if (!string.IsNullOrEmpty(sName)) tEmployees = tEmployees.Where(w => (w.sName + " " + w.sLastname).Contains(sName)).ToList();
                    var data = (from a in tEmployees
                                join master in tMaster on a.sEmp equals master.nSystemID.Value
                                select new
                                {
                                    a.sName,
                                    a.sLastname,
                                    a.sIdentification,
                                    a.sEmp,
                                    a.dBirth,
                                    finger = master.sFinger != null && master.sFinger2 != null,
                                    a.cType
                                }).ToList();

                    string sname = Request.QueryString["name"] == null ? "" : Request.QueryString["name"];
                    string type = Request.QueryString["type"] == null ? "" : Request.QueryString["type"];
                    int index = 1;
                    dgd.DataSource = data.Where(w => (w.sName + " " + w.sLastname).Contains(sname) && w.cType.Contains(type))
                        .Select(a => new
                        {
                            rowindex = index++,
                            a.sName,
                            a.sLastname,
                            a.sIdentification,
                            a.sEmp,
                            a.dBirth,
                            a.finger,
                            a.cType
                        })
                        .ToList();
                    dgd.DataBind();
                }
            }
        }
    }
}