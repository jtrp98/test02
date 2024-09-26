using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using urbanairship;
using System.Threading.Tasks;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class smsedit : System.Web.UI.Page
    {
        public string setEditSMS;
        public string setLVSMS;
        public string setValLVSMS;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDelete.Click += BtnDelete_Click;
            if (string.IsNullOrEmpty(Session["sEmpID"] + ""))
            {
                Response.Redirect("/Default.aspx");
            }
            if (!IsPostBack)
            {
                ddlType.Items.Add(new ListItem("ส่งทันที", "0"));
                ddlType.Items.Add(new ListItem("ตั้งเวลาการส่ง", "1"));

                ddlDuration.Items.Add(new ListItem("แจ้งประกาศกิจกรรม", "0"));
                ddlDuration.Items.Add(new ListItem("แจ้งประกาศข่าวสาร", "1"));

                Opendata();

                if (Session["sEmpID"] != null)
                {
                    string checkpermission = HttpContext.Current.Session["sEmpID"].ToString();

                    if (!String.IsNullOrEmpty(checkpermission))
                    {
                        checkpermission = checkpermission.Substring(15, 1);
                        if (checkpermission != "2")
                        {
                        }
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
           
        }

        private void Opendata()
        {
            //JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
            //int nID = int.Parse(STCrypt.DecryptURL(Request.QueryString["id"])[0]);
            //JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read);
            //Double _span = (DateTime.Now - DateTime.UtcNow).TotalMinutes;
            //foreach (var _TSMS in _db.TSMS.Where(w => w.nSMS == nID))
            //{
            //    ddlDuration.Text = _TSMS.SMSTitle;
            //    //ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(_TSMS.SMSType.ToString()));//=ddlType.Items.FindByValue(_TSMS.SMSType).Selected = true;
            //    ddlDuration.SelectedIndex = ddlDuration.Items.IndexOf(ddlDuration.Items.FindByValue(_TSMS.SMSDuration.ToString()));//=ddlType.Items.FindByValue(_TSMS.SMSType).Selected = true;
            //    txtDesp.Text = _TSMS.SMSDesp;
            //    string dateEdit = "";
            //    monstop1.Value = _TSMS.dSend.Value.ToString("HH:mm");

            //    string[] txtDate = _TSMS.SMSDate.Value.Date.ToString().Split(' ');
            //    string[] words = txtDate[0].Split('/');

            //    string wordDay = "";
            //    string wordMonth = "";
            //    if (Int32.Parse(words[1].ToString()) < 10)
            //    {
            //        wordDay = "0" + words[1];
            //    }
            //    else
            //    {
            //        wordDay = words[1];
            //    }

            //    if (Int32.Parse(words[0].ToString()) < 10)
            //    {
            //        wordMonth = "0" + words[0];
            //    }
            //    else
            //    {
            //        wordMonth = words[0];
            //    }

            //    chkAll.Checked = _TSMS.SMSAll.Trim() == "1";
            //    chkEmp.Checked = _TSMS.SMSEMP == "1";

            //    dateEdit = wordDay + "/" + wordMonth + "/" + words[2];

            //    dateSMS.Value = dateEdit;

            //    if (_TSMS.SMSType == 0)
            //    {
            //        setEditSMS += "$('#subtype').show();";
            //        setEditSMS += "$('#datesection').hide();";
            //        setEditSMS += " $('#timesection').hide();";
            //        setEditSMS += " $('#lvSet').hide();";
            //        setEditSMS += " $('#setLV').hide();";
            //        //ddlStype.SelectedIndex = ddlStype.Items.IndexOf(ddlStype.Items.FindByValue(_TSMS.SMSSubType.ToString()));
            //    }
            //    else
            //    {
            //        setEditSMS += "$('#subtype').hide();";
            //        setEditSMS += "$('#datesection').show();";
            //        setEditSMS += " $('#timesection').show();";
            //        setEditSMS += " $('#lvSet').show();";
            //        setEditSMS += " $('#setLV').show();";
            //        List<TSMSSubLevel> _tempSMS = _db.TSMSSubLevels.Where(w => w.nSMS == nID).ToList();
            //        //setValLVSMS = "first";
            //        foreach (var tempdetail in _tempSMS.ToList())
            //        {
            //            setValLVSMS += tempdetail.nTSubLevel + ",";
            //            setLVSMS += "$('.termsub[sublv=" + tempdetail.nTSubLevel + "]').prop('checked', true);";
            //        }
            //        txtLv.Value = setValLVSMS;
            //    }
            //}
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("smssettings.aspx");
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            //int nSMS;
            //string sCompany = "", ListUser = "";
            //JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
            //JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read);
            //Double _span = (DateTime.Now - DateTime.UtcNow).TotalMinutes;
            //DateTime scheduled_time = DateTime.ParseExact(dateSMS.Value + " " + monstop1.Value, "dd/MM/yyyy HH:mm", new CultureInfo("en-us"));
            //string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            //int nCompany = 1;
            //foreach (var data in _dbMaster.TCompanies.Where(w => w.sEntities == sEntities))
            //{
            //    nCompany = data.nCompany;
            //    sCompany = data.sCompany;
            //}

            //if (requireData())
            //{

            //    int nID = int.Parse(STCrypt.DecryptURL(Request.QueryString["id"])[0]);
            //    foreach (var _TSMS in _db.TSMS.Where(w => w.nSMS == nID).ToList())
            //    {

            //        pushdata.delete(_TSMS.scheduled_id);
            //        _TSMS.SMSDate = DateTime.ParseExact(dateSMS.Value, "dd/MM/yyyy", new CultureInfo("en-us"));
            //        _TSMS.SMSDuration = Int32.Parse(ddlDuration.SelectedValue);
            //        _TSMS.SMSStatus = "-";
            //        _TSMS.SMSTitle = ddlDuration.SelectedItem.Text;
            //        _TSMS.SMSDesp = txtDesp.Text;

            //        if (chkAll.Checked)
            //        {
            //            _TSMS.SMSAll = "1";
            //        }
            //        else
            //        {
            //            _TSMS.SMSAll = "0";

            //        }

            //        _db.SaveChanges();
            //        _dbMaster.TMessages.Where(w => w.push_id == _TSMS.nSMS).ToList().ForEach(f => _dbMaster.TMessages.Remove(f));

            //        if (!chkAll.Checked)
            //        {
            //            List<TSMSSubLevel> _oldtempSMS = _db.TSMSSubLevels.Where(w => w.nSMS == nID).ToList();
            //            foreach (var otempdetail in _oldtempSMS.ToList())
            //            { _db.TSMSSubLevels.Remove(otempdetail); }

            //            string sublv = txtLv.Value;
            //            string[] arrSublv = sublv.Split(',');
            //            var sort = from s in arrSublv
            //                       where !string.IsNullOrEmpty(s)
            //                       orderby s
            //                       select s;

            //            foreach (string c in sort)
            //            {
            //                TSMSSubLevel _TSMSSubLV = new TSMSSubLevel();
            //                _TSMSSubLV.nSMS = nID;
            //                _TSMSSubLV.nTSubLevel = Int32.Parse(c);
            //                _db.TSMSSubLevels.Add(_TSMSSubLV);
            //                _db.SaveChanges();

            //                foreach (var data in (from a in _db.TUsers.ToList()
            //                                      join b in _db.TTermSubLevel2 on a.nTermSubLevel2 equals b.nTermSubLevel2
            //                                      where b.nTSubLevel == _TSMSSubLV.nTSubLevel
            //                                      select new { a, b }))
            //                {
            //                    int nMessageID = _dbMaster.TMessages.Max(m => m.nMessageID) + 1;
            //                    foreach (var dataMaster in _dbMaster.TUsers.Where(w => w.nCompany == nCompany && w.nSystemID == data.a.sID && w.cType == "0"))
            //                    {
            //                        ListUser += (string.IsNullOrEmpty(ListUser) ? "" : ",") + '"' + dataMaster.sID + '"';
            //                        _dbMaster.TMessages.Add(new TMessage
            //                        {
            //                            dSend = scheduled_time,
            //                            UserID = dataMaster.sID,
            //                            nType = 5,
            //                            nMessageID = nMessageID,
            //                            nStatus = 0,
            //                            sMessage = txtDesp.Text,
            //                            sTitle = ddlDuration.SelectedItem.Text + " : EN-TECH",//+ sCompany,
            //                                                                                  //scheduled_id = scheduled_id,
            //                            push_id = nID
            //                        });
            //                    }
            //                    _dbMaster.SaveChanges();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            foreach (var dataMaster in _dbMaster.TUsers.Where(w => w.nCompany == nCompany && w.cType == "0"))
            //            {
            //                ListUser += (string.IsNullOrEmpty(ListUser) ? "" : ",") + '"' + dataMaster.sID + '"';
            //                int nMessageID = _dbMaster.TMessages.Max(m => m.nMessageID) + 1;
            //                _dbMaster.TMessages.Add(new TMessage
            //                {
            //                    dSend = scheduled_time,
            //                    UserID = dataMaster.sID,
            //                    nType = 5,
            //                    nMessageID = nMessageID,
            //                    nStatus = 0,
            //                    sMessage = txtDesp.Text,
            //                    sTitle = ddlDuration.SelectedItem.Text + " : EN-TECH",//+ sCompany,
            //                    push_id = nID
            //                });
            //            }
            //            _dbMaster.SaveChanges();
            //        }

            //        if (chkEmp.Checked)
            //        {
            //            foreach (var dataMaster in _dbMaster.TUsers.Where(w => w.nCompany == nCompany && w.cType == "1"))
            //            {
            //                ListUser += (string.IsNullOrEmpty(ListUser) ? "" : ",") + '"' + dataMaster.sID + '"';
            //                int nMessageID = _dbMaster.TMessages.Max(m => m.nMessageID) + 1;
            //                _dbMaster.TMessages.Add(new TMessage
            //                {
            //                    dSend = scheduled_time,
            //                    UserID = dataMaster.sID,
            //                    nType = 5,
            //                    nMessageID = nMessageID,
            //                    nStatus = 0,
            //                    sMessage = txtDesp.Text,
            //                    sTitle = ddlDuration.SelectedItem.Text + " : EN-TECH",//+ sCompany,
            //                                                                          //scheduled_id = scheduled_id,
            //                    push_id = nID
            //                });
            //            }
            //            _dbMaster.SaveChanges();
            //        }
            //        string scheduled_id = "";
            //        if (ddlType.SelectedIndex == 0)
            //        {
            //            scheduled_id = await pushdata.scheduled("[" + ListUser + "]", txtDesp.Text, ddlDuration.SelectedItem.Text + " : " + sCompany, scheduled_time.AddMinutes(-_span), nID);
            //            if (!string.IsNullOrEmpty(scheduled_id))
            //            {
            //                scheduled_id = scheduled_id.Split('"')[3];
            //            }
            //        }
            //        else
            //        {
            //            scheduled_id = await pushdata.push("[" + ListUser + "]", txtDesp.Text, ddlDuration.SelectedItem.Text + " : " + sCompany, scheduled_time.AddMinutes(-_span), nID);
            //            if (!string.IsNullOrEmpty(scheduled_id))
            //            {
            //                scheduled_id = scheduled_id.Split('"')[3];
            //            }
            //        }

            //        _db.TSMS.Where(w => w.nSMS == nID).ToList().ForEach(f => f.scheduled_id = scheduled_id);
            //        _db.SaveChanges();
            //        fcommon.InsertLog(Session["sEmpID"] + "", "แก้ไขข้อมูล SMS " + _TSMS.SMSTitle, HttpContext.Current.Session["sEntities"].ToString());

            //    }

            //    Response.Redirect("smssettings.aspx");
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "<script>$(function(){j_infosell('<span>กรุณากรอกข้อมูลให้ครบถ้วน</span>');return false;});</script>", false);
            //}
        }

        bool requireData()
        {
            bool boolData = true;

            if (String.IsNullOrEmpty(dateSMS.Value))
            {
                boolData = false;
            }

            txtTitle.Text = "ส่งประกาศ SMS";

            /* if (String.IsNullOrEmpty(txtTitle.Text))
             {
                 boolData = false;
             }*/

            if (String.IsNullOrEmpty(txtDesp.Text))
            {
                boolData = false;
            }

            return boolData;
        }
    }
}