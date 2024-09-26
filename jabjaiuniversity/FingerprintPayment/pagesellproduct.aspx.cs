using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO.Ports;
using System.Globalization;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class pagesellproduct : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            btnSavepage1.Click += new EventHandler(btnSavepage1_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel2.Click += new EventHandler(btnCancel2_Click);
            dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
            dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            timerSell.Tick += TimerSell_Tick;

            //ScriptManager1.RegisterPostBackControl(this.txtnNumber);
            //ScriptManager1.RegisterPostBackControl(this.txtsBarCode);
            //ScriptManager1.RegisterPostBackControl(this.txtCheckFinger);

            if (!IsPostBack)
            {
                fcommon.ExecuteNonQuery(fcommon.connMaster, @"UPDATE TConnect SET cStatus = '1' WHERE nConnectID = (SELECT TOP 1 nConnectID FROM TConnect 
                    WHERE cStatus != '4' AND cTypeConnect = 'SE' ORDER BY nConnectID DESC)");
                timerSell.Enabled = true;
                CreateConnection();
                AddTable();
            }
            else
            {
                if (mtvsell.ActiveViewIndex == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "postbackReady", "postbackReady();", true);
                }
                else
                {

                }
            }
        }
        private void CreateConnection()
        {
            int nConnectID = 1;
            DataTable _dt = fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TConnect ORDER BY nConnectID DESC");
            if (_dt.Rows.Count > 0) nConnectID = int.Parse(_dt.Rows[0]["nConnectID"] + "") + 1;
            string strInsert = @"INSERT INTO TConnect (nConnectID,sMacPC,sMacMoblie,cStatus,sValue,cTypeConnect,dList) VALUES({0},'{1}','{2}','1',NULL,'SE','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", new CultureInfo("en-us")) + "')";
            ViewState["nConnectID"] = nConnectID;
            fcommon.ExecuteNonQuery(fcommon.connMaster, string.Format(strInsert, nConnectID, "", Request.Cookies["myCookie"].Values["sMac"]));
        }
        private void TimerSell_Tick(object sender, EventArgs e)
        {
            DataTable _dt = fcommon.Get_Data(fcommon.connMaster, "SELECT TOP 1 * FROM TConnect ORDER BY nConnectID DESC");
            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr["cStatus"] + "" == "2")
                {

                    txtCheckFinger.Text = _dr["sValue"] + "";
                    txtUserFinger.Text = fcommon.Get_Value(fcommon.connMaster, "SELECT CONVERT(VARCHAR,nSystemID) + CASE WHEN CTYPE = '1' THEN 'E' ELSE 'S' END AS 'SID' FROM TUSER WHERE SID = " + _dr["sValue"]);
                    mtvsell.ActiveViewIndex = 0; ;
                    focusTxtBarCode();
                    dgd.DataSource = null;
                    timerSell.Enabled = false;
                }
            }
        }
        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
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

            switch (e.CommandName.Trim())
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
                    UpdateListItme(_dt);
                    dgd.DataSource = _dt;
                    dgd.DataBind();
                    txtsBarCode.Text = "";
                    productname.InnerHtml = "";
                    txtnNumber.Text = "1";
                    productprice.InnerHtml = "";
                    ViewState["Mode"] = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "txtsBarCode", "$('input[id*=txtsBarCode]').select();", true);
                    break;
                case "Edit":
                    txtsBarCode.Text = e.Item.Cells[0].Text;
                    productname.InnerHtml = e.Item.Cells[1].Text;
                    txtnNumber.Text = e.Item.Cells[2].Text;
                    productprice.InnerHtml = e.Item.Cells[4].Text;
                    ViewState["Mode"] = "Edit";
                    txtnNumber.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "txtnNumber", "$('input[id*=txtnNumber]').select();  $('.rowhidden').removeClass('hidden')", true);
                    lblAmount.Text = "";
                    txtnNumber.Visible = true;
                    break;
                default:
                    break;
            }
        }
        void btnCancel2_Click(object sender, EventArgs e)
        {
            Response.Redirect("pagesellproduct.aspx");
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            TSell _Sell = new TSell();
            _Sell.sEmp = int.Parse(Session["sEmpID"] + "");
            //if (_db.TSells.Where(w => _Sell.nEmp == w.nEmp && w.dSell == DateTime.Now).ToList().Count > 0)
            //{
            //    Response.Redirect("pagesellproduct.aspx");
            //}

            DataTable _dt = (DataTable)ViewState["dt"];
            //int nSell = 2, nOrder = 1;
            decimal nMoney = decimal.Parse(string.IsNullOrEmpty(txtnMoney.Text) ? "0" : txtnMoney.Text);
            decimal nMoneyUser = 0;
            int? sID = int.Parse(txtUserFinger.Text.Replace("E", "").Replace("S", ""));
            decimal? _nMax = 0;
            decimal _nMoneyDay = 0;

            if (txtUserFinger.Text.IndexOf("S") != -1)
            {
                _nMax = _db.TUsers.Where(w => w.sID == sID).FirstOrDefault().nMax;
                nMoneyUser = _db.TUsers.Where(w => w.sID == sID).FirstOrDefault().nMoney.Value;
                if (_db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID.Value == sID).Count() > 0)
                    _nMoneyDay = _db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID.Value == sID).Sum(s => s.nTotal).Value;
            }
            else
            {
                nMoneyUser = _db.TEmployees.Where(w => w.sEmp == sID).FirstOrDefault().nMoney.Value;
                if (_db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID2.Value == sID).Count() > 0)
                    _nMoneyDay = _db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID2.Value == sID).Sum(s => s.nTotal).Value;
            }

            if (dgd.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitStock", "j_infosell('กรุณาเลือกรายการที่ต้องการบันทึก'); $.unblockUI();", true);
                return;
            }

            if (decimal.Parse(txtnMoney.Text) > nMoneyUser)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "moneyLow", "j_infosell('<span style=\"font-size:20px;\">ยอดเงินเหลือไม่พอชำระสินค้า</span>');", true);
                return;
            }

            if (_nMax > 0 && _nMax < (_nMoneyDay + nMoney))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitDate", "j_infosell('<span style=\"font-size:20px;\">ท่านได้การซื้อสินค้าเกินยอดของวันนี้แล้ว</span>');", true);
                return;
            }

            //if (_db.TSells.Count() > 0)
            //{
            //    nSell = _db.TSells.OrderByDescending(o => o.sSellID).Take(1).FirstOrDefault().sSellID + 1;
            //}
            if (txtUserFinger.Text.IndexOf("S") != -1)
            {
                _Sell.sID = sID;
                _db.TUsers.Where(w => w.sID == sID).ToList().ForEach(f => f.nMoney = f.nMoney - nMoney);
            }
            else
            {
                _Sell.sID2 = sID;
                int nMoneyEmp = int.Parse(nMoney.ToString().Split('.')[0]);
                _db.TEmployees.Where(w => w.sEmp == sID).ToList().ForEach(f => f.nMoney = f.nMoney - nMoneyEmp);
            }

            _Sell.dSell = DateTime.Now;
            _Sell.nSession = _db.TSystemlogs.Where(w => DateTime.Compare(w.dLog, DateTime.Today) > 0
                && w.sEmp == _Sell.sEmp
                && w.nSession != null).OrderByDescending(o => o.nSession)
                .Take(1).FirstOrDefault().nSession;
            _Sell.nTotal = decimal.Parse(txtnMoney.Text);
            //_Sell.sSellID = nSell;
            _db.TSells.Add(_Sell);

            _db.SaveChanges();
            foreach (DataRow _dr in _dt.Rows)
            {
                TSell_Detail _Detail = new TSell_Detail();
                _Detail.nSell = _Sell.sSellID;
                _Detail.nProduct = int.Parse(_dr["nProductID"] + "");
                _Detail.nNumber = int.Parse(_dr["nNumber"] + "");
                //_Detail.nOrder = nOrder++;
                _db.TProducts.Where(w => w.nProductID == _Detail.nProduct).ToList().ForEach(f => f.nBalance = f.nBalance - (f.cStock == "1" ? 0 : _Detail.nNumber));
                _db.TSell_Detail.Add(_Detail);
            }
            _db.SaveChanges();

            database.InsertLog(Session["sEmpID"] + "", "ขายสินค้า",
                HttpContext.Current.Session["sEntities"].ToString(), Request, -1, 2, 0);
            //CreateConnection();
            Response.Redirect("pagesellproduct.aspx");
            //fcommon.ExecuteNonQuery(fcommon.connMaster, "UPDATE TConnect SET cStatus = '4' WHERE nConnectID = (SELECT TOP 1 nConnectID FROM TConnect ORDER BY nConnectID DESC) AND cStatus ");
        }
        void btnSavepage1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCheckFinger.Text) || txtCheckFinger.Text == "1")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<span>กรุณาสแกนลายนิ้วมือเพื่อทำรายการ</span>'); return false;});", true);
            }
            else
            {
                ViewState["sUserID"] = int.Parse(txtCheckFinger.Text);
                ViewState["sID"] = txtUserFinger.Text;
                mtvsell.ActiveViewIndex = 0;
                focusTxtBarCode();
                dgd.DataSource = null;
            }
        }
        void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "postbackReady", "postbackReady();", true);
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            DataTable _dt = (DataTable)ViewState["dt"];
            if (_db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text.Trim() && w.cDel == null).Count() == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "barcodeFound", "j_infosell('รหัส BarCode " + txtsBarCode.Text + " ไม่มีอยู่ในระบบ');", true);
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
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "wrongStock", "j_infosell('จำนวนสินค้าเกินจำนวนในสต๊อก');", true);
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "wrongStock", "j_infosell('จำนวนสินค้าเกินจำนวนในสต๊อก');", true);
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
            focusTxtBarCode();
            ViewState["dt"] = _dt;
        }
        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }
        protected void txtnNumber_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "postbackReady", "postbackReady();", true);
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            DataTable _dt = (DataTable)ViewState["dt"];
            int? sID = int.Parse(txtCheckFinger.Text);
            decimal _nProductID = 0, nMoney = decimal.Parse(string.IsNullOrEmpty(txtnMoney.Text) ? "0" : txtnMoney.Text);
            if (txtsBarCode.Text != "" && _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text.Trim() && w.cDel == null).Count() == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "barcodeFound", @"j_infosell('รหัส BarCode " + txtsBarCode.Text + @" ไม่มีอยู่ในระบบ'); ", true);
                ClearData();
            }
            else if (txtsBarCode.Text != "")
            {
                _nProductID = _db.TProducts.Where(w => w.sBarCode == txtsBarCode.Text && string.IsNullOrEmpty(w.cDel)).SingleOrDefault().nProductID;
                if (_db.TBlackLists.Where(w => w.sID == sID && w.nProductID == _nProductID).Count() > 0)
                {
                    ClearData();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "blackList", "j_infosell('รายการสินค้านี้ไม่สามารถซื้อได้เนื่องจากติด Black List ');", true);
                }
                else
                {
                    decimal? _nMax = 0;
                    if (txtUserFinger.Text.IndexOf("S") > -1) _nMax = _db.TUsers.Where(w => w.sID == sID).FirstOrDefault().nMax;
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
                            if (CheckMoney(_dt, txtsBarCode.Text, sID))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "limitDate", "j_infosell('ท่านได้การซื้อสินค้าเกินยอดของวันนี้แล้ว');", true);
                            }
                            else
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
                            }
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
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "wrongStock", "j_infosell('จำนวนสินค้าเกินจำนวนในสต๊อก');", true);
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
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "wrongStock", "j_infosell('จำนวนสินค้าเกินจำนวนในสต๊อก');", true);
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
                UpdateListItme(_dt);

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
            focusTxtBarCode();
        }
        private bool CheckMoney(DataTable _dt, string BarCode, int? UserID)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            decimal nTatol = 0;
            decimal? MaxMoney = 0;
            if (txtUserFinger.Text.IndexOf("S") > -1) MaxMoney = _db.TUsers.Where(w => w.sID == UserID).FirstOrDefault().nMax;
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
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (mtvsell.ActiveViewIndex == 1)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "fnCapture", "setTimeout(fnCapture, 500);", true);
            }
            else
            {
                if (!string.IsNullOrEmpty(txtCheckFinger.Text))
                {
                    int sID = 0;
                    decimal _nMoneyDay = 0;
                    decimal? _nMax = 0;
                    if (txtUserFinger.Text.IndexOf("S") != -1)
                    {
                        sID = int.Parse(txtUserFinger.Text.Split('S')[0]);
                        _nMoneyDay = 0;
                        _nMax = _db.TUsers.Where(w => w.sID == sID).FirstOrDefault().nMax;
                        if (_db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID.Value == sID).ToList().Count > 0)
                        {
                            _nMoneyDay = _db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID.Value == sID)
                                .Sum(s => s.nTotal).Value;
                        }
                        if (_nMax == 0) _nMoneyDay = _db.TUsers.Where(w => w.sID == sID).SingleOrDefault().nMoney.Value;
                        else _nMoneyDay = (_nMax - _nMoneyDay).Value;
                        lblsName.Text = _db.TUsers.Where(w => w.sID == sID).Select(s => new { sName = "ผู้ใช้บริการ " + s.sName + " " + s.sLastname }).Take(1).Single().sName + " ยอดเงินคงเหลือ " + _nMoneyDay + " บาท";

                    }
                    else
                    {
                        sID = int.Parse(txtUserFinger.Text.Split('E')[0]);
                        if (_db.TEmployees.Where(w => w.sEmp == sID).SingleOrDefault().nMoney.HasValue)
                            _nMoneyDay = _db.TEmployees.Where(w => w.sEmp == sID).SingleOrDefault().nMoney.Value;
                        //if (_db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID.Value == sID).ToList().Count > 0)
                        //{
                        //    _nMoneyDay = _db.TSells.Where(w => DateTime.Compare(w.dSell.Value, DateTime.Today) > 0 && w.sID.Value == sID)
                        //        .Sum(s => s.nTotal).Value;
                        //}
                        //_nMoneyDay = _db.TUsers.Where(w => w.sID == sID).SingleOrDefault().nMoney.Value;
                        lblsName.Text = _db.TEmployees.Where(w => w.sEmp == sID).Select(s => new { sName = "ผู้ใช้บริการ " + s.sName + " " + s.sLastname }).Take(1).Single().sName + " ยอดเงินคงเหลือ " + _nMoneyDay + " บาท";
                    }
                    ViewState["nMoney"] = _nMoneyDay;
                    fcommon.ExecuteNonQuery(fcommon.connMaster, "UPDATE TConnect SET sDisplay = '" + txtnMoney.Text + "," + ViewState["nMoney"] + "' WHERE nConnectID = " + ViewState["nConnectID"]);
                }
                ltrjavascript.Text = "";
                timerSell.Enabled = false;
            }
            focusTxtBarCode();
        }
        private static string CountNumberProduct(string sBarCode, JabJaiEntities _dbs)
        {
            int? nCount = 0;
            if (string.IsNullOrEmpty(sBarCode)) return "0";
            int? _ProductNume = _dbs.TProducts.Where(w => w.sBarCode == sBarCode && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().nNumber;
            string _cStock = _dbs.TProducts.Where(w => w.sBarCode == sBarCode && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().cStock;
            if (_cStock == "1") return "999999";
            int? nProductID = _dbs.TProducts.Where(w => w.sBarCode == sBarCode && string.IsNullOrEmpty(w.cDel)).FirstOrDefault().nProductID;
            int? nStock = _dbs.TStockDetails.Where(w => w.nProductID == nProductID).Sum(s => s.nNumber);
            int? nSellNumber = _dbs.TSell_Detail.Where(w => w.nProduct == nProductID && string.IsNullOrEmpty(w.cDel)).Sum(s => s.nNumber);
            nCount = (_ProductNume + (nStock == null ? 0 : nStock)) - ((nSellNumber == null ? 0 : nSellNumber));
            return nCount.ToString();
        }
        private void UpdateListItme(DataTable _dt)
        {
            string strListItme = "", SQL = "";
            foreach (DataRow _dr in _dt.Rows)
            {
                strListItme += (string.IsNullOrEmpty(strListItme) ? "" : ",") + _dr["nProductID"] + "=" + _dr["nNumber"];
            }


            SQL = @"UPDATE TConnect SET sDisplay = '" + txtnMoney.Text + "," + ViewState["nMoney"] + @"' 
                    , sList = '" + strListItme + "',dList = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", new CultureInfo("en-us")) + "' WHERE nConnectID = " + ViewState["nConnectID"];

            fcommon.ExecuteNonQuery(fcommon.connMaster, SQL);
        }

    }
}