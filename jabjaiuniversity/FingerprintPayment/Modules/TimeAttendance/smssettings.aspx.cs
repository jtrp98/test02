using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;
using urbanairship;
using System.Text;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public class SMSGateway : System.Web.UI.Page
    {
        private JWTToken.userData userData;
        protected JWTToken.userData UserData { get { return userData; } }

        protected override void OnLoad(EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        public static JWTToken.userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }

            return userData;
        }
    }

    public partial class smssettings : SMSGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                dgd.ItemCommand += new DataGridCommandEventHandler(dgd_ItemCommand);
                dgd.ItemDataBound += new DataGridItemEventHandler(dgd_ItemDataBound);
                txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);

                if (!IsPostBack)
                {
                    DataTable _dt = new DataTable();
                    _dt.Columns.Add("nTypeID", typeof(Int16));
                    _dt.Columns.Add("sType", typeof(string));
                    _dt.Rows.Add(new Object[] { 0, "แจ้งประกาศกิจกรรม" });
                    _dt.Rows.Add(new Object[] { 1, "แจ้งประกาศข่าวสาร" });
                    fcommon.ListDataTableToDropDownList(_dt, ddlcType, "ทั้งหมด", "nTypeID", "sType");
                    OpenData();
                }
            }
        }

        void txtSearch_TextChanged(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                var _listProduct = _db.TSMS.Where(w => w.SchoolID == schoolID && (w.cDel) == false && w.SMSStatus == "-" && w.SMSDesp.Contains(txtSearch.Text)).OrderByDescending(o => o.nSMS).ToList();

                dgd.DataSource = _listProduct;
                dgd.DataBind();
            }
        }

        void dgd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                sbyte sSMS = fcommon.FindIndexColumnOfDataFieldInGrid(dgd, "nSMS");
                if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
                {
                    string[] txtDate = e.Item.Cells[2].Text.Split(' ');
                    string[] words = txtDate[0].Split('/');
                    //e.Item.Cells[2].Text = words[1] + "-" + words[0] + "-" + words[2];
                    LinkButton _btnDel = e.Item.FindControl("btnDel") as LinkButton;
                    _btnDel.Attributes.Add("onclick", "j_confirm('ยืนยันการลบข้อมูล','คุณต้องการที่จะลบข้อมูลนี้หรือไม่ ?','" + _btnDel.UniqueID + "');return false;");
                }
            }
        }

        void dgd_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int schoolID = UserData.CompanyID;

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                
                switch (e.CommandName.ToString())
                {
                    case "Edit":
                        Response.Redirect("smsedit.aspx?id=" + Server.UrlEncode((string.Format("{0:000000}", int.Parse(e.Item.Cells[0].Text)))));
                        break;
                    case "Page":
                        dgd.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;
                        OpenData();
                        break;
                    case "Del":
                        int ID = int.Parse(e.Item.Cells[0].Text);
                        foreach (var _data in _db.TSMS.Where(w => w.SchoolID == schoolID && w.nSMS == ID && (w.cDel) == false))
                        {
                            _data.isDel = true;
                            database.InsertLog(UserData.UserID.ToString(), "ลบข้อมูลSMS " + _data.SMSTitle,
                                UserData.Entities, Request, 2, 4, 0);
                            //foreach (var _dataessage in _dbMaster.TMessages.Where(w => w.push_id == _data.nSMS))
                            //{

                            //    _dataessage.cDel = "1";
                            //    if (_dataessage.scheduled_id != null) pushdata.delete(_dataessage.scheduled_id);
                            //}
                            //_dbMaster.SaveChanges();
                        }
                        _db.SaveChanges();
                        OpenData();
                        break;
                    case "AddMain":
                        Response.Redirect("smsadd.aspx?type=0");
                        break;
                    case "Add":
                        Response.Redirect("smsadd.aspx?type=1");
                        break;
                }
            }
        }

        private void OpenData()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int nTypeID = ddlcType.SelectedIndex == 0 ? -1 : int.Parse(ddlcType.SelectedValue);
                var _listProduct = _db.TSMS.Where(w => w.SchoolID == schoolID && w.SMSStatus == "-" && w.SMSDesp.Contains(txtSearch.Text) && (w.isDel ?? false) == false).OrderByDescending(o => o.dSend).ToList();

                if (nTypeID >= 0)
                    _listProduct = _listProduct.Where(w => w.SMSDuration == nTypeID).ToList();
                int index = 1;

                dgd.DataSource = (from a in _listProduct
                                  select new
                                  {
                                      index = index++,
                                      a.nSMS,
                                      a.SMSTitle,
                                      a.SMSDuration,
                                      a.dSend,
                                      SMSDesp = Utf8Encoder.GetString(Utf8Encoder.GetBytes((a.SMSDesp ?? "").Length > 100 ? a.SMSDesp.Substring(0, 100) : a.SMSDesp))
                                  }).ToList();

                dgd.DataBind();

            }
        }

        protected void ddlcType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            OpenData();
        }

        private static readonly Encoding Utf8Encoder = Encoding.GetEncoding("UTF-8", new EncoderReplacementFallback(string.Empty),
            new DecoderExceptionFallback());
    }
}