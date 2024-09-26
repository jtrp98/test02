using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class pageditsellproduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            btnSavepage1.Click += new EventHandler(btnSavepage1_Click);
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            dgdEdit.ItemCommand += new DataGridCommandEventHandler(dgdEdit_ItemCommand);
            btnSaveEdit.Click += new EventHandler(btnSaveEdit_Click);
            btnCancelEidt.Click += new EventHandler(btnCancelEidt_Click);
            btnCancel2.Click += new EventHandler(btnCancel2_Click);
            btnBackEdit.Click += new EventHandler(btnBackEdit_Click);

            if (!IsPostBack)
            {
                int sEmpID = int.Parse(Session["sEmpID"] + "");
                dgd.Columns[3].Visible = fcommon.SettingPermission(sEmpID, 6, HttpContext.Current.Session["sEntities"].ToString());
                SetBodyEventOnLoad("setTimeout(fnCapture, 1000);");
            }
            else
            {
                SetBodyEventOnLoad("");
            }
        }
        void btnCancel2_Click(object sender, EventArgs e)
        {
            mtvsell.ActiveViewIndex = 1;
        }
        void btnCancelEidt_Click(object sender, EventArgs e)
        {
            mtvsell.ActiveViewIndex = 0;
        }
        void btnSaveEdit_Click(object sender, EventArgs e)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (string.IsNullOrEmpty(ViewState["ListDetail"] + ""))
            {
                mtvsell.ActiveViewIndex = 0;
            }
            else
            {
                DataTable _dt = (DataTable)ViewState["dt"];
                int nSellID = int.Parse(ViewState["nSellID"] + "");
                int sID = int.Parse(ViewState["sID"] + "");
                decimal nTatol = decimal.Parse(ViewState["nTatol"] + "");
                if (txtUserFinger.Text.IndexOf("S") != -1)
                {
                    _db.TUsers.Where(w => w.sID == sID).ToList().ForEach(f => f.nMoney = f.nMoney + nTatol);
                }
                else
                {
                    _db.TEmployees.Where(w => w.sEmp == sID).ToList().ForEach(f => f.nMoney = f.nMoney + nTatol);
                }
                if (!string.IsNullOrEmpty(ViewState["ListDetail"] + ""))
                {
                    string ListData = ViewState["ListDetail"] + "";
                    foreach (string _str in ListData.Split(','))
                    {
                        int nProduct = int.Parse(_str);
                        _db.TSell_Detail.Where(w => w.nSell == nSellID && w.nProduct == nProduct).ToList().ForEach(f => f.cDel = "1");
                    }
                    nTatol = 0;
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        _dr["nTotal"] = int.Parse(_dr["nNumber"] + "") * decimal.Parse(_dr["nPrice"] + "");
                        nTatol += decimal.Parse(_dr["nTotal"] + "");
                    }
                    _db.TSells.Where(w => w.sSellID == nSellID).ToList().ForEach(f => f.nTotal = nTatol);
                    _db.SaveChanges();
                }
                mtvsell.ActiveViewIndex = 0;
                ViewState["nTatol"] = 0;
                DateTime _dSelect = DateTime.Today.AddDays(-7);
                if (txtUserFinger.Text.IndexOf("S") != -1)
                    dgd.DataSource = _db.TSells.Where(w => w.dSell >= _dSelect && w.sID == sID && w.nTotal > 0).ToList();
                else
                    dgd.DataSource = _db.TSells.Where(w => w.dSell >= _dSelect && w.sID2 == sID && w.nTotal > 0).ToList();
                dgd.DataBind();
            }

        }
        void dgdEdit_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataTable _dt = (DataTable)ViewState["dt"];
            decimal nTatol = 0;
            TextBox txtNumber = (TextBox)e.Item.FindControl("txtNumber");
            Label lblNumber = (Label)e.Item.FindControl("lblNumber");
            LinkButton lnkCancel = (LinkButton)e.Item.FindControl("lnkCancel");
            LinkButton lbkEdit = (LinkButton)e.Item.FindControl("lbkEdit");
            LinkButton lbkConform = (LinkButton)e.Item.FindControl("lbkConform");
            switch (e.CommandName)
            {
                case "Cancel":
                    foreach (DataRow _dr in _dt.Select("sBarCode = '" + e.Item.Cells[0].Text + "'"))
                    {
                        if (string.IsNullOrEmpty(ViewState["nTatol"] + ""))
                            ViewState["nTatol"] = decimal.Parse(_dr["nTotal"] + "");
                        else
                            ViewState["nTatol"] = decimal.Parse(ViewState["nTatol"] + "") + decimal.Parse(_dr["nTotal"] + "");
                        _dt.Rows.Remove(_dr);
                    }
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        _dr["nTotal"] = int.Parse(_dr["nNumber"] + "") * decimal.Parse(_dr["nPrice"] + "");
                        nTatol += decimal.Parse(_dr["nTotal"] + "");
                    }
                    ViewState["dt"] = _dt;
                    dgdEdit.DataSource = _dt;
                    dgdEdit.DataBind();
                    ViewState["Mode"] = "";
                    if (string.IsNullOrEmpty(ViewState["ListDetail"] + ""))
                        ViewState["ListDetail"] = e.Item.Cells[3].Text;
                    else
                        ViewState["ListDetail"] += "," + e.Item.Cells[3].Text;
                    break;
                case "Edit":
                    txtNumber.Text = lblNumber.Text;
                    txtNumber.Visible = true;
                    lblNumber.Visible = false;
                    lnkCancel.Visible = false;
                    lbkEdit.Visible = false;
                    lbkConform.Visible = true;
                    break;
                case "Conform":
                    txtNumber.Visible = false;
                    lblNumber.Visible = true;
                    lnkCancel.Visible = true;
                    lbkEdit.Visible = true;
                    lbkConform.Visible = false;
                    break;
                default:
                    break;
            }
        }
        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    sbyte nSellID = fcommon.FindIndexColumnOfDataFieldInGrid(dgd, "sSellID");
                    mtvsell.ActiveViewIndex = 2;
                    OpenSellDetail(int.Parse(e.Item.Cells[nSellID].Text));
                    break;
            }
        }
        private void OpenSellDetail(int nSellID)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            var _list = from a in _db.TSell_Detail.Where(w => string.IsNullOrEmpty(w.cDel))
                        join b in _db.TProducts.ToList() on a.nProduct equals b.nProductID
                        join c in _db.TSells.Where(w => w.nTotal > 0) on a.nSell equals c.sSellID
                        where a.nSell == nSellID
                        select new { a.nNumber, b.sBarCode, b.sProduct, b.nPrice, nTotal = b.nPrice * a.nNumber, b.nProductID };
            ViewState["nSellID"] = nSellID;
            ViewState["dt"] = fcommon.LinqToDataTable(_list.ToList());
            dgdEdit.DataSource = fcommon.LinqToDataTable(_list.ToList());
            dgdEdit.DataBind();
        }
        void btnSavepage1_Click(object sender, EventArgs e)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (!string.IsNullOrEmpty(txtCheckFinger.Text))
            {
                int sID = int.Parse(txtCheckFinger.Text);
                if (txtUserFinger.Text.IndexOf("S") != -1)
                {
                    lblsName.Text = _db.TUsers.Where(w => w.sID == sID).Select(s => new { sName = "ผู้ใช้บริการ " + s.sName + " " + s.sLastname }).Take(1).Single().sName;
                }
                else
                {
                    lblsName.Text = _db.TEmployees.Where(w => w.sEmp == sID).Select(s => new { sName = "ผู้ใช้บริการ " + s.sName + " " + s.sLastname }).Take(1).Single().sName;
                }
                SelectSellData();
            }
        }
        protected void mtvsell_ActiveViewChanged(object sender, EventArgs e)
        {

        }
        private void SelectSellData()
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (string.IsNullOrEmpty(txtCheckFinger.Text))
            {
                SetBodyEventOnLoad(@"j_infomainLed('แจ้งผลการดำเนินการ','<span>กรุณาแสกนนิ้วลายมือเพื่อทำรายการ</span>','0','0');");
            }
            else
            {
                mtvsell.ActiveViewIndex = 0;
                int? sID = int.Parse(txtCheckFinger.Text);
                ViewState["sID"] = sID;
                DateTime _dSelect = DateTime.Today.AddDays(-7);
                if (txtUserFinger.Text.IndexOf("S") != -1)
                    dgd.DataSource = _db.TSells.Where(w => w.dSell >= _dSelect && w.sID == sID && w.nTotal > 0).OrderByDescending(O => O.dSell).ToList();
                else
                    dgd.DataSource = _db.TSells.Where(w => w.dSell >= _dSelect && w.sID2 == sID && w.nTotal > 0).OrderByDescending(O => O.dSell).ToList();
                dgd.DataBind();
            }
        }
        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }
        protected void btnBackEdit_Click(object sender, EventArgs e)
        {
            mtvsell.ActiveViewIndex = 0;

        }
    }
}