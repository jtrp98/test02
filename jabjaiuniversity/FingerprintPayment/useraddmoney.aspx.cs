using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class useraddmoney : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            //btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnClear.Click += BtnClear_Click;
            SetBodyEventOnLoad("");
            Timer1.Tick += Timer1_Tick;
            //ListAddMoney();
            if (!IsPostBack)
            {
                CreateConnection();
                //SetBodyEventOnLoad("setTimeout(fnCapture, 500);");
            }
            else
            {
                SetBodyEventOnLoad("");
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            //if (fcommon.Get_Value(fcommon.connMaster, "SELECT cStatus FROM TConnect WHERE nConnectID = " + Session["nConnectID"]) != "4")
            //{
            CreateConnection();
            //}
            txtnBalance.Text = "";
            txtsID.Text = "";
            txtsName.Text = "";
            txtsLastName.Text = "";
            txtUserFinger.Text = "";
            txtnMoney.Text = "";
            txtnMoney.Focus();
            Timer1.Enabled = true;
        }

        private void CreateConnection()
        {
            int nConnectID = 1;
            DataTable _dt = fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TConnect ORDER BY nConnectID DESC");
            if (_dt.Rows.Count > 0) nConnectID = int.Parse(_dt.Rows[0]["nConnectID"] + "") + 1;
            string strInsert = @"INSERT INTO TConnect (nConnectID,sMacPC,sMacMoblie,cStatus,sValue,cTypeConnect,dList) VALUES({0},'{1}','{2}','1',NULL,'UM','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", new CultureInfo("en-us")) + "')";
            Session["nConnectID"] = nConnectID;
            fcommon.ExecuteNonQuery(fcommon.connMaster, string.Format(strInsert, nConnectID, "", ConfigurationSettings.AppSettings["smac"]));
        }

        private void ListAddMoney()
        {
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            string SQL = @"SELECT TOP 3 nEmp,dLog,sLog,nSession,sName + ' ' + sLastname AS sName,dsLog
            FROM (SELECT CONVERT(VARCHAR(10),dLog,101) AS dsLog,* FROM TSystemlog) AS TSystemlog 
            INNER JOIN TEmployees ON TSystemlogs.nEmp = TEmployees.sEmp
            WHERE sLog LIKE 'เติมเงินเข้าระบบให้คุณ%' ORDER BY dLog DESC";

            DataTable _dt = fcommon.Get_Data(_conn, SQL);
            if (_dt.Rows.Count == 0)
            {
                ltrHtml.Text = "ไม่มีข้อมูลที่ต้องการค้นหา";
                return;
            }
            ltrHtml.Text = @"<table class='table table-condensed' style='width: 100%;'>";
            DataTable _dtGroup = _dt.AsEnumerable().GroupBy(r => new { DSELL = r["dsLog"] })
                .Select(g => g.OrderBy(r => r["dsLog"]).First()).OrderByDescending(o => o["dsLog"])
                .CopyToDataTable();
            string _sHtml = @"<tr class='warning'>
                                        <td style='width: 45%;'>ชื่อ - นามสกุล</td>
                                        <td style='width: 30%;'>เวลา</td>
                                        <td style='width: 25%;'>ยอดเงิน</td>
                                </tr>";

            foreach (DataRow _dr in _dt.Rows)
            {
                string[] _str = (_dr["sLog"] + "").Split(new string[] { "จำนวน" }, StringSplitOptions.None);
                _sHtml += @" <tr class='active'>
                                        <td style='text-align:left;'>" + (_str[0] + "").Replace("เติมเงินเข้าระบบให้คุณ ", "") + @"</td>
                                        <td style='text-align:center;'>" + _dr["dLog"] + @"</td>
                                        <td style='text-align:right;'>" + _str[1] + @"</td>
                                </tr>";
            }

            ltrHtml.Text += _sHtml + "</table>";
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminMain.aspx");
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            int sID = int.Parse(txtUserFinger.Text.Replace("E", "").Replace("S", ""));
            decimal? nMoney = 0;
            string sName = "";
            if (!string.IsNullOrEmpty(txtnMoney.Text))
            {
                foreach (var _data in _db.TUsers.Where(w => w.sID == sID).ToList())
                {
                    _data.nMoney = _data.nMoney + decimal.Parse(txtnMoney.Text);
                    sName = _data.sName + " " + _data.sLastname;
                    nMoney = _data.nMoney;
                }

                TMoney _Money = new TMoney();
                _Money.dMoney = DateTime.Now;
                _Money.nMoney = decimal.Parse(txtnMoney.Text);
                _Money.sID = sID;
                _Money.cType = txtUserFinger.Text.IndexOf("S") != -1 ? "2" : "1";
                _db.TMoneys.Add(_Money);
                _db.SaveChanges();

                String SQL = @"UPDATE TConnect SET sDisplay = '" + txtnMoney.Text + "," + nMoney + @"',cStatus = '4'
                    ,dList = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", new CultureInfo("en-us")) +
                   "' WHERE nConnectID = " + ViewState["nConnectID"];

                fcommon.ExecuteNonQuery(fcommon.connMaster, SQL);

                database.InsertLog(Session["sEmpID"] + "", "เติมเงินเข้าระบบให้คุณ " + sName + " จำนวน " + txtnMoney.Text + " บาท",
                    Session["sEntities"].ToString(), Request, -1, 2, 0);
                //SetBodyEventOnLoad(@"j_infomainLed('แจ้งผลการดำเนินการ','ได้เติมเงินให้คุณ " + sName + " " + txtnMoney.Text + " บาท <br /> ยอดเงินคงเหลือในระบบคือ " + nMoney + " บาท ','" + txtnMoney.Text + "','" + nMoney.Value.ToString() + "'); ");
                SetBodyEventOnLoad(@"j_infosell('ได้เติมเงินให้คุณ " + sName + " " + txtnMoney.Text + " บาท <br /> ยอดเงินคงเหลือในระบบคือ " + nMoney + " บาท ','" + txtnMoney.Text + "','" + nMoney.Value.ToString() + "'); ");


                CreateConnection();
                txtnMoney.Text = "";
            }
            else
            {
                SetBodyEventOnLoad("j_infomainLed('แจ้งผลการดำเนินการ','กรุณากรอกข้อมูลจำนวนเงิน','0','0'); ");
            }
        }
        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            DataTable _dt = fcommon.Get_Data(fcommon.connMaster, "SELECT TOP 1 * FROM TConnect WHERE cTypeConnect = 'UM' ORDER BY nConnectID DESC  ");
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["cStatus"] + "" == "2")
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        txtUserFinger.Text = fcommon.Get_Value(fcommon.connMaster, "SELECT CONVERT(VARCHAR,nSystemID) + CASE WHEN CTYPE = '1' THEN 'E' ELSE 'S' END AS 'SID' FROM TUSER WHERE SID = " + _dr["sValue"]);

                        if (txtUserFinger.Text.IndexOf("S") != -1)
                        {
                            int sID = int.Parse(txtUserFinger.Text.Replace("S", ""));

                            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
                            foreach (var data in _db.TUsers.Where(w => w.sID == sID))
                            {
                                if (string.IsNullOrEmpty(_dt.Rows[0]["sDisplay"] + ""))
                                {
                                    fcommon.ExecuteNonQuery(fcommon.connMaster, "UPDATE TConnect SET sDisplay = '0," + data.nMoney.Value + "' WHERE nConnectID = " + Session["nConnectID"]);
                                }
                                txtnBalance.Text = data.nMoney.HasValue ? data.nMoney.Value.ToString() : "0.0";
                                txtsID.Text = data.sIdentification;
                                txtsName.Text = data.sName;
                                txtsLastName.Text = data.sLastname;
                                txtnMoney.Focus();
                                Timer1.Enabled = false;
                            }
                        }
                        else
                        {
                            int sEmp = int.Parse(txtUserFinger.Text.Replace("E", ""));

                            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
                            foreach (var data in _db.TEmployees.Where(w => w.sEmp == sEmp))
                            {
                                if (string.IsNullOrEmpty(_dt.Rows[0]["sDisplay"] + ""))
                                {
                                    fcommon.ExecuteNonQuery(fcommon.connMaster, "UPDATE TConnect SET sDisplay = '0," + data.nMoney.Value + "' WHERE nConnectID = " + Session["nConnectID"]);
                                }
                                txtnBalance.Text = data.nMoney.HasValue ? data.nMoney.Value.ToString() : "0.0";
                                txtsID.Text = data.sIdentification;
                                txtsName.Text = data.sName;
                                txtsLastName.Text = data.sLastname;
                                txtnMoney.Focus();
                                Timer1.Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    txtnBalance.Text = "";
                    txtsID.Text = "";
                    txtsName.Text = "";
                    txtsLastName.Text = "";
                    txtUserFinger.Text = "";
                    txtnMoney.Text = "";
                }

            }
            else
            {
                txtnBalance.Text = "";
                txtsID.Text = "";
                txtsName.Text = "";
                txtsLastName.Text = "";
                txtUserFinger.Text = "";
                txtnMoney.Text = "";
            }
        }
    }
}