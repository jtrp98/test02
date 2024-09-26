using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class User_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            dgd.ItemCreated += Dgd_ItemCreated;
            //btnSearch.Click += new EventHandler(btnSearch_Click);
            if (!IsPostBack)
            {
                OpenData();
                fcommon.ListDBToDropDownList(_conn, ddlsublevel, "select * from TSubLevel", "- ทั้งหมด -", "nTSubLevel", "SubLevel");
                int sEmpID = int.Parse(Session["sEmpID"] + "");
                dgd.Columns[5].Visible = fcommon.SettingPermission(sEmpID, 11, HttpContext.Current.Session["sEntities"].ToString());
                dgd.Columns[4].Visible = fcommon.SettingPermission(sEmpID, 11, HttpContext.Current.Session["sEntities"].ToString());
                int sEmp = int.Parse(Session["sEmpID"] + "");
                string sClaimReport = _db.TEmployees.Where(w => w.sEmp == sEmp).SingleOrDefault().sStatusReport;
                //if (sClaimReport.Length > 5 && sClaimReport.Substring(5, 1) == "1") ltrMenu.Text = "<a href='report07.aspx' class='btn btn-primary'>รายงานรายชื่อนักเรียน</a>";
            }
        }

        private void Dgd_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer && e.Item.ItemType != ListItemType.Pager)
            {
                //sbyte cfinger = fcommon.FindIndexColumnOfDataFieldInGrid(dgd, "finger");
                //if (dgd.Items[e.Item.ItemIndex].Cells[cfinger].Text.ToLower() == "false") e.Item.BackColor = System.Drawing.Color.Red;
                //LinkButton _btnDel = e.Item.FindControl("btnDel") as LinkButton;
                //_btnDel.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel.UniqueID + "');return false;");
            }
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            OpenData();
        }
        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer && e.Item.ItemType != ListItemType.Pager)
            {
                sbyte cfinger = fcommon.FindIndexColumnOfDataFieldInGrid(dgd, "finger");
                if (e.Item.Cells[cfinger].Text.ToLower() == "false")
                {
                    e.Item.BackColor = System.Drawing.Color.Red;
                    e.Item.ForeColor = System.Drawing.Color.White;
                }
                //LinkButton _btnDel = e.Item.FindControl("btnDel") as LinkButton;
                //_btnDel.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel.UniqueID + "');return false;");
            }
        }
        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            sbyte sID = fcommon.FindIndexColumnOfDataFieldInGrid(dgd, "sID");
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect("useredit.aspx?id=" + Server.UrlEncode(STCrypt.Encrypt(string.Format("{0:000000}", int.Parse(e.Item.Cells[sID].Text)))));
                    break;
                case "Page":
                    dgd.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;
                    OpenData();
                    break;
                case "Del":
                    int ID = int.Parse(e.Item.Cells[sID].Text);
                    JabJaiMasterEntities dbmaster = Connection.MasterEntities();
                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                    var tcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    dbmaster.TUsers.Where(w => w.nSystemID == ID && w.nCompany == tcompany.nCompany && w.cType == "0").ToList().ForEach(f => f.cDel = "1");
                    dbmaster.SaveChanges();
                    foreach (var _data in _db.TUsers.Where(w => w.sID == ID).ToList())
                    {
                        _data.cDel = "1";
                        database.InsertLog(Session["sEmpID"] + "", "ลบข้อมูลนักเรียน " + _data.sName + " " + _data.sLastname,
                            HttpContext.Current.Session["sEntities"].ToString(), Request, 14, 4, 0);
                    }
                    _db.SaveChanges();
                    OpenData();
                    break;
            }
        }
        private void OpenData()
        {
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var TUserMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();
                    using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString())))
                    {
                        var tUser = db.TUsers.Where(w => w.cDel == null).ToList();
                        var tLevel2 = db.TTermSubLevel2.ToList();
                        int rowindex = 1;
                        var tLevel = db.TSubLevels.ToList();
                        var data = (from a in tUser
                                    join b in tLevel2 on a.nTermSubLevel2 equals b.nTermSubLevel2
                                    join c in tLevel on b.nTSubLevel equals c.nTSubLevel
                                    join master in TUserMaster on a.sID equals master.nSystemID.Value
                                    select new
                                    {
                                        a.nTermSubLevel2,
                                        a.sName,
                                        a.sLastname,
                                        a.sIdentification,
                                        a.sID,
                                        a.dBirth,
                                        finger = master.sFinger != null && master.sFinger2 != null,
                                        c.nTSubLevel
                                    }).ToList();

                        int idlv = int.Parse(string.IsNullOrEmpty(Request.QueryString["idlv"]) ? "0" : Request.QueryString["idlv"]);
                        int idlv2 = int.Parse(string.IsNullOrEmpty(Request.QueryString["idlv2"]) ? "0" : Request.QueryString["idlv2"]);
                        string sname = Request.QueryString["sname"];

                        if (idlv2 > 0) data = data.Where(w => w.nTermSubLevel2 == idlv2).ToList();
                        if (idlv > 0) data = data.Where(w => w.nTSubLevel == idlv).ToList();
                        if (!string.IsNullOrEmpty(sname)) data = data.Where(w => (w.sName + " " + w.sLastname).Contains(sname) || w.sIdentification.Contains(sname)).ToList();

                        dgd.DataSource = (from a in data
                                          orderby a.sIdentification
                                          select new
                                          {
                                              rowindex = rowindex++,
                                              a.nTermSubLevel2,
                                              a.sName,
                                              a.sLastname,
                                              a.sIdentification,
                                              a.sID,
                                              a.dBirth,
                                              a.finger,
                                              a.nTSubLevel
                                          }).ToList();
                        dgd.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                //Response.Write(fcommon.ConfigSqlConnection(Session["sEntities"].ToString()).ConnectionString.ToString());
                //Response.Write("<br>" + ConfigurationManager.ConnectionStrings[Session["sEntities"].ToString()].ConnectionString);
            }
        }
        protected void ddlcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            txtSearch.Text = "";
            dgd.DataSource = _db.TUsers.Where(w => string.IsNullOrEmpty(w.cDel)).ToList();
            dgd.CurrentPageIndex = 0;
            dgd.DataBind();
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            dgd.DataSource = _db.TUsers.Where(w => (w.sName + " " + w.sLastname).Contains(txtSearch.Text) && string.IsNullOrEmpty(w.cDel)).ToList();
            dgd.CurrentPageIndex = 0;
            dgd.DataBind();
        }
    }
}