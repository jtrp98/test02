using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class pagcashsales1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            //btnAdd.Click += new EventHandler(btnAdd_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel2.Click += new EventHandler(btnCancel2_Click);
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            if (!IsPostBack)
            {
                AddTable();
                txtsBarCode.Focus();
                //ltrjavascript.Text = @"<script language=""javascript"">$(document).ready(function () {fnCapture(); });</script>";
                //fcommon.LED8("0", 2);
            }
            else
            {
                SetBodyEventOnLoad("");
            }
        }
        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                //TextBox txtdgdnNumber = (TextBox)e.Item.FindControl("txtdgdnNumber");
                //DataTable _dt = (DataTable)ViewState["dt"];
                //if (_dt.Select("sBarCode = '" + e.Item.Cells[0].Text + "'").Length > 0)
                //    txtdgdnNumber.Text = _dt.Select("sBarCode = '" + e.Item.Cells[0].Text + "'")[0]["nNumber"] + "";
            }
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminMain.aspx");
        }
        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataTable _dt = (DataTable)ViewState["dt"];
            decimal nTatol = 0;
            switch (e.CommandName)
            {
                case "Cancel":
                    foreach (DataRow _dr in _dt.Select("sBarCode = '" + e.Item.Cells[0].Text + "'"))
                        _dt.Rows.Remove(_dr);
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        _dr["nTotal"] = int.Parse(_dr["nNumber"] + "") * decimal.Parse(_dr["nPrice"] + "");
                        nTatol += decimal.Parse(_dr["nTotal"] + "");
                    }
                    txtnMoney.Text = nTatol + "";
                    ViewState["dt"] = _dt;
                    dgd.DataSource = _dt;
                    dgd.DataBind();
                    txtsBarCode.Text = "";
                    productname.InnerHtml = "";
                    txtnNumber.Text = "";
                    productprice.InnerHtml = "";
                    ViewState["Mode"] = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "txtsBarCode", "$('input[id*=txtsBarCode]').select();", true);
                    // SetBodyEventOnLoad(@"$(""input[id*=txtsBarCode]"").select();");
                    break;
                case "Edit":
                    txtsBarCode.Text = e.Item.Cells[0].Text;
                    productname.InnerHtml = e.Item.Cells[1].Text;
                    txtnNumber.Text = e.Item.Cells[2].Text;
                    productprice.InnerHtml = e.Item.Cells[4].Text;
                    ViewState["Mode"] = "Edit";
                    txtnNumber.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "txtnNumber", "$('input[id*=txtnNumber]').select();", true);
                    //SetBodyEventOnLoad(@"$(""input[id*=txtnNumber]"").select();");
                    //TextBox txtdgdnNumber = (TextBox)e.Item.FindControl("txtdgdnNumber");
                    //Response.Write(txtdgdnNumber.Text);
                    lblAmount.Text = "";
                    txtnNumber.Visible = true;
                    break;
                default:

                    break;
            }
        }
        void btnCancel2_Click(object sender, EventArgs e)
        {
            //fcommon.LED8("0", 2);
            Response.Redirect("pagesellproduct.aspx");
            //mtvsell.ActiveViewIndex = 1;
            //txtnMoney.Text = "0";
            //DataTable _dt = (DataTable)ViewState["dt"];
            //_dt = new DataTable();
            //dgd.DataSource = _dt;
            //ViewState["dt"] = _dt;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            TSell _Sell = new TSell();
            DataTable _dt = (DataTable)ViewState["dt"];
            int nSell = 2, nOrder = 1;
            decimal nMoney = decimal.Parse(string.IsNullOrEmpty(txtnMoney.Text) ? "0" : txtnMoney.Text);
            int? sID = 0;
            if (dgd.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitStock", "j_infosell('กรุณาเลือกรายการที่ต้องการบันทึก'); $.unblockUI();", true);
                return;
            }
            //Response.Write(txtnMoney.Text + " " + txtnBalance.Text + "111 "); return;
            //if (string.IsNullOrEmpty(txtnMoney.Text))
            //if (decimal.Parse(txtnMoney.Text) > _db.TUsers.Where(w => w.sID == sID).FirstOrDefault().nMoney)
            //{
            //    SetBodyEventOnLoad(@"Mgsalert('<span style=""font-size:20px;"">ยอดเงินเหลือไม่พอชำระสินค้า</span>');");
            //    return;
            //}
            decimal _nMoneyDay = 0;
            if (_db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID.Value == sID).Count() > 0)
                _nMoneyDay = _db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID.Value == sID).Sum(s => s.nTotal).Value;
            //if (_nMax > 0 && _nMax < (_nMoneyDay + nMoney))
            //{
            //    SetBodyEventOnLoad(@"Mgsalert('<span style=""font-size:20px;"">ท่านได้การซื้อสินค้าเกินยอดของวันนี้แล้ว</span>');");
            //    return;
            //}
            if (_db.TSells.Count() > 0)
            {
                nSell = _db.TSells.OrderByDescending(o => o.sSellID).Take(1).FirstOrDefault().sSellID + 1;
            }
            //_db.TUsers.Where(w => w.sID == sID).ToList().ForEach(f => f.nMoney = f.nMoney - nMoney);
            _Sell.dSell = DateTime.Now;
            _Sell.sID = sID;
            _Sell.sEmp = int.Parse(Session["sEmpID"] + "");
            int? nSession;
            nSession = 1;
            if (_db.TSystemlogs.Where(w => DateTime.Compare(w.dLog, DateTime.Today) > 0
               && w.sEmp == _Sell.sEmp
               && w.nSession != null).ToList().Count() > 0)
            {
                nSession = _db.TSystemlogs.Where(w => DateTime.Compare(w.dLog, DateTime.Today) > 0
                && w.sEmp == _Sell.sEmp
                && w.nSession != null).OrderByDescending(o => o.nSession)
                .Take(1).FirstOrDefault().nSession;
                _Sell.nSession = nSession == null ? 1 : nSession;
            }
            _Sell.nSession = nSession == null ? 1 : nSession;
            _Sell.nTotal = decimal.Parse(txtnMoney.Text);
            _Sell.sSellID = nSell;
            _db.TSells.Add(_Sell);

            _db.SaveChanges();
            foreach (DataRow _dr in _dt.Rows)
            {
                TSell_Detail _Detail = new TSell_Detail();
                _Detail.nSell = nSell;
                _Detail.nProduct = int.Parse(_dr["nProductID"] + "");
                _Detail.nNumber = int.Parse(_dr["nNumber"] + "");
                _Detail.nOrder = nOrder++;
                _db.TProducts.Where(w => w.nProductID == _Detail.nProduct).ToList().ForEach(f => f.nBalance = f.nBalance - _Detail.nNumber);
                _db.TSell_Detail.Add(_Detail);
            }
            _db.SaveChanges();
            database.InsertLog(Session["sEmpID"] + "", "ขายสินค้า",
                HttpContext.Current.Session["sEntities"].ToString(), Request, -1, 2, 0);
            //foreach (var _data in _db.TUsers.Where(w => w.sID == sID))
            //{
            //    fcommon.LED8(_data.nMoney.Value.ToString(), 4);
            //}
            Response.Redirect("pagcashsales.aspx");
        }
        //void btnSavepage1_Click(object sender, EventArgs e)
        //{
        //    //DataTable _dt = (DataTable)ViewState["dt"];
        //    //int nToTal = 0;
        //    //foreach (DataRow _dr in _dt.Rows)
        //    //{
        //    //    nToTal += int.Parse(_dr["nTotal"] + "");
        //    //}
        //    //txtnMoney.Text = nToTal + "";
        //    if (string.IsNullOrEmpty(txtCheckFinger.Text))
        //    {
        //        SetBodyEventOnLoad(@"Mgsalert('<span>กรุณาสแกนลายนิ้วมือเพื่อทำรายการ</span>');");
        //    }
        //    else
        //    {
        //        ViewState["sUserID"] = int.Parse(txtCheckFinger.Text);
        //        mtvsell.ActiveViewIndex = 0;
        //        txtsBarCode.Focus();
        //        dgd.DataSource = null;
        //    }
        //}
        void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "postbackReady", "postbackReady();", true);
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            DataTable _dt = (DataTable)ViewState["dt"];
            if (_db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text).Count() == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "barcodeFound", "j_infosell('รหัส BarCode ไม่มีอยู่ในระบบ');", true);
                //SetBodyEventOnLoad("Mgsalert('รหัส BarCode ไม่มีอยู่ในระบบ');");
            }
            else
            {
                int nNumber = 1;
                int.TryParse(txtnNumber.Text, out nNumber);
                if (_db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && w.cDel == null).FirstOrDefault().nNumber > nNumber)
                {
                    if (_dt == null || _dt.Select("sBarCode = '" + txtsBarCode.Text + "'").Count() == 0)
                    {
                        DataRow _dr = _dt.NewRow();
                        _dr["sBarCode"] = txtsBarCode.Text;
                        _dr["nNumber"] = nNumber;
                        _dr["sProduct"] = _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && w.cDel == null).FirstOrDefault().sProduct;
                        _dr["nProductID"] = _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && w.cDel == null).FirstOrDefault().nProductID;
                        _dr["nPrice"] = _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && w.cDel == null).FirstOrDefault().nPrice;
                        _dt.Rows.Add(_dr);

                    }
                    else
                    {
                        foreach (DataRow _dr in _dt.Select("sBarCode = '" + txtsBarCode.Text + "'"))
                        {
                            if (_db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && w.cDel == null).FirstOrDefault().nNumber > (int.Parse(_dr["nNumber"] + "") + nNumber))
                            {
                                _dr["nNumber"] = int.Parse(_dr["nNumber"] + "") + nNumber;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitStock", "j_infosell('จำนวนสินค้าเกินจำนวนในสต๊อก');", true);
                                //SetBodyEventOnLoad("Mgsalert('จำนวนสินค้าเกินจำนวนในสต๊อก');");
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitStock", "j_infosell('จำนวนสินค้าเกินจำนวนในสต๊อก');", true);
                    //     SetBodyEventOnLoad("j_infosell('จำนวนสินค้าเกินจำนวนในสต๊อก');");
                }
                int nTatol = 0;
                foreach (DataRow _dr in _dt.Rows)
                {
                    _dr["nTotal"] = int.Parse(_dr["nNumber"] + "") * int.Parse(_dr["nPrice"] + "");
                    nTatol += int.Parse(_dr["nTotal"] + "");
                }
                ViewState["dt"] = _dt;
                dgd.DataSource = _dt;
                dgd.DataBind();
                txtsBarCode.Text = "";
                txtnNumber.Text = "";
            }
            focusTxtBarCode();
        }

        public void focusTxtBarCode()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "postbackReady", "postbackReady();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "txtsBarCode", "$('input[id*=txtsBarCode]').select();", true);
        }

        private void AddTable()
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("sBarCode");
            _dt.Columns.Add("nNumber");
            _dt.Columns.Add("sProduct");
            _dt.Columns.Add("nPrice");
            _dt.Columns.Add("nTotal");
            _dt.Columns.Add("nProductID");
            dgd.DataSource = _dt;
            dgd.DataBind();
            ViewState["dt"] = _dt;
            focusTxtBarCode();
        }
        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }
        protected void txtnNumber_TextChanged(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "postbackReady", "postbackReady();", true);
            DataTable _dt = (DataTable)ViewState["dt"];
            int? sID = 0;
            decimal _nProductID = 0, nMoney = decimal.Parse(string.IsNullOrEmpty(txtnMoney.Text) ? "0" : txtnMoney.Text);
            if (txtsBarCode.Text != "" && _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && w.cDel == null).Count() == 0)
            {
                ClearData();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "barcodeFound", "j_infosell('รหัส BarCode ไม่มีอยู่ในระบบ');", true);
                //   SetBodyEventOnLoad(@"j_infosell('รหัส BarCode ไม่มีอยู่ในระบบ');");
            }
            else if (txtsBarCode.Text != "")
            {
                _nProductID = _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && string.IsNullOrEmpty(w.cDel)).SingleOrDefault().nProductID;
                if (_db.TBlackLists.Where(w => w.sID == sID && w.nProductID == _nProductID).Count() > 0)
                {
                    ClearData();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "blackList", "j_infosell('รายการสินค้านี้ไม่สามารถซื้อได้เนื่องจากติด Black List ');", true);
                    // SetBodyEventOnLoad(@"j_infosell('รายการสินค้านี้ไม่สามารถซื้อได้เนื่องจากติด Black List ');");
                }
                else
                {
                    decimal _nMoneyDay = 0;
                    int nNumber = 1;
                    int.TryParse(txtnNumber.Text, out nNumber);
                    if (nNumber == 0) nNumber = 1;
                    if (_db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID.Value == sID).Count() > 0)
                        _nMoneyDay = _db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID.Value == sID).Sum(s => s.nTotal).Value;

                    if (int.Parse(CountNumberProduct(txtsBarCode.Text, _db)) >= nNumber)
                    {
                        if (_dt == null || _dt.Select("sBarCode = '" + txtsBarCode.Text + "'").Count() == 0)
                        {
                            int? nWarn = 0;
                            if (_db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && string.IsNullOrEmpty(w.cDel)).Single().nWarn.HasValue)
                                nWarn = _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().nWarn.Value;
                            if (nWarn > 0 && int.Parse(CountNumberProduct(txtsBarCode.Text, _db)) <= nWarn)
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitBuy", "j_infosell('สินค้าถึงจุดสั่งซื้อแล้ว');", true);
                            DataRow _dr = _dt.NewRow();
                            _dr["sBarCode"] = txtsBarCode.Text;
                            _dr["nNumber"] = nNumber;
                            _dr["sProduct"] = _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().sProduct;
                            _dr["nProductID"] = _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().nProductID;
                            _dr["nPrice"] = _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().nPrice;
                            _dt.Rows.Add(_dr);
                            //}
                        }
                        else if (!CheckMoney(_dt, txtsBarCode.Text, sID))
                        {
                            foreach (DataRow _dr in _dt.Select("sBarCode = '" + txtsBarCode.Text + "'"))
                            {
                                if (int.Parse(CountNumberProduct(txtsBarCode.Text, _db)) >= (int.Parse(_dr["nNumber"] + "") + nNumber))
                                {
                                    int? nWarn = 0;
                                    if (_db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && string.IsNullOrEmpty(w.cDel)).Single().nWarn.HasValue)
                                        nWarn = _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().nWarn.Value;
                                    if (nWarn > 0 && int.Parse(CountNumberProduct(txtsBarCode.Text, _db)) <= nWarn)
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitBuy", "j_infosell('สินค้าถึงจุดสั่งซื้อแล้ว');", true);
                                    if (string.IsNullOrEmpty(ViewState["Mode"] + ""))
                                        _dr["nNumber"] = int.Parse(_dr["nNumber"] + "") + nNumber;
                                    else
                                        _dr["nNumber"] = nNumber;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitStock", "j_infosell('จำนวนสินค้าเกินจำนวนในสต๊อก');", true);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitDate", "j_infosell('ท่านได้การซื้อสินค้าเกินยอดของวันนี้แล้ว');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitStock", "j_infosell('จำนวนสินค้าเกินจำนวนในสต๊อก');", true);
                    }
                }
                decimal nTatol = 0;
                foreach (DataRow _dr in _dt.Rows)
                {
                    _dr["nTotal"] = int.Parse(_dr["nNumber"] + "") * decimal.Parse(_dr["nPrice"] + "");
                    nTatol += decimal.Parse(_dr["nTotal"] + "");
                }
                txtnMoney.Text = nTatol + "";
                //fcommon.LED8(txtnMoney.Text, 2);
                ViewState["dt"] = _dt;
                dgd.DataSource = _dt;
                dgd.DataBind();
                ClearData();
                txtnNumber.Visible = false;
            }
            focusTxtBarCode();
        }
        private void ClearData()
        {
            txtsBarCode.Text = "";
            txtnNumber.Text = "1";
            ViewState["Mode"] = "";
            productname.InnerHtml = "";
            productprice.InnerHtml = "";
            lblAmount.Text = "1";
            txtsBarCode.Focus();
        }
        private bool CheckMoney(DataTable _dt, string BarCode, int? UserID)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            decimal nTatol = 0;
            decimal? MaxMoney = 0;
            if (MaxMoney == 0) return false;
            decimal? nTotalMoney = _db.TProducts.Where(w => w.sBarCode == BarCode && w.cDel == null).FirstOrDefault().nPrice * int.Parse(txtnNumber.Text);
            if (_db.TSells.Where(w => w.sID == UserID && w.dSell.Value >= DateTime.Today).Count() > 0)
                nTotalMoney += _db.TSells.Where(w => w.sID == UserID && w.dSell.Value >= DateTime.Today).Sum(s => s.nTotal);
            foreach (DataRow _dr in _dt.Rows)
            {
                if (ViewState["Mode"] + "" != "Edit")
                {
                    _dr["nTotal"] = int.Parse(_dr["nNumber"] + "") * decimal.Parse(_dr["nPrice"] + "");
                    nTatol += decimal.Parse(_dr["nTotal"] + "");
                }
                else
                {
                    if (_dr["sBarCode"] + "" != BarCode)
                    {
                        _dr["nTotal"] = int.Parse(_dr["nNumber"] + "") * decimal.Parse(_dr["nPrice"] + "");
                        nTatol += decimal.Parse(_dr["nTotal"] + "");
                    }
                }
            }
            return MaxMoney < (nTatol + nTotalMoney);
        }
        protected void mtvsell_ActiveViewChanged(object sender, EventArgs e)
        {
            //if (mtvsell.ActiveViewIndex == 1)
            //{
            //    SetBodyEventOnLoad("setTimeout(fnCapture, 500);");
            //}
            //else
            //{
            //    ltrjavascript.Text = "";
            //}
        }
        private static string CountNumberProduct(string sBarCode, JabJaiEntities _dbs)
        {
            int? nCount = 0;
            if (string.IsNullOrEmpty(sBarCode)) return "0";
            int? _ProductNume = _dbs.TProducts.Where(w => w.sBarCode == sBarCode && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().nNumber;
            int? nProductID = _dbs.TProducts.Where(w => w.sBarCode == sBarCode && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().nProductID;
            int? nStock = _dbs.TStockDetails.Where(w => w.nProductID == nProductID).Sum(s => s.nNumber);
            int? nSellNumber = _dbs.TSell_Detail.Where(w => w.nProduct == nProductID && string.IsNullOrEmpty(w.cDel)).Sum(s => s.nNumber);
            nCount = (_ProductNume + (nStock == null ? 0 : nStock)) - ((nSellNumber == null ? 0 : nSellNumber));
            return nCount.ToString();
        }
    }
}