using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.IO;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class useredit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnImport.Click += new EventHandler(btnImport_Click);
            if (!IsPostBack)
            {
                Opendata();
                //btnRegister1.Attributes.Add("onclick", );
                DataTable _dt = fcommon.LinqToDataTable(_db.TTypes.Where(w => string.IsNullOrEmpty(w.cDel)).ToList());
                if (_dt != null)
                    fcommon.ListDataTableToDropDownList(_dt, ddlTypeProduct, "ทั้งหมด", "nTypeID", "sType");
                foreach (var _Type in _db.TTypes.Where(w => string.IsNullOrEmpty(w.cDel)).ToList())
                {
                    ltrListProduct.Text += string.Format(@"<div ID='Type_{0}'>{1}", _Type.nTypeID, _Type.sType);
                    foreach (var _data in _db.TProducts.Where(w => w.nType == _Type.nTypeID && string.IsNullOrEmpty(w.cDel)).ToList())
                    {
                        ltrListProduct.Text += string.Format(@"<div ID='TProduct_{0}' class='TProduct' style=""margin-left:20px; cursor:pointer;cursor:pointer;width: 100%; clear: right;"">{1}</div>", _data.nProductID, _data.sProduct);
                    }
                    ltrListProduct.Text += "</div>";
                }
                SetBodyEventOnLoad("");
            }
        }
        void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Server.MapPath("~/images/IDCARD.txt")))
                    File.Delete(Server.MapPath("~/images/IDCARD.txt"));
                File.Copy("C:\\Profile\\IDCARD.txt", Server.MapPath("~/images/IDCARD.txt"));
                string _str = File.ReadAllText(Server.MapPath("~/images/IDCARD.txt")).Replace(@"""", "");
                _str = _str.Replace("{", "").Replace("}", "");
                string[] _values = _str.Split(',');
                _values.Where(w => w.IndexOf("Address") >= 0).ToList().ForEach(f => txtAddress.Text = f.Replace("Address:", ""));
                _values.Where(w => w.IndexOf("ThaiName") >= 0).ToList().ForEach(f => txtsName.Text = f.Replace(" ", ":").Split(':')[1]);
                _values.Where(w => w.IndexOf("ThaiName") >= 0).ToList().ForEach(f => txtsLastName.Text = f.Replace(" ", ":").Split(':')[2]);
                _values.Where(w => w.IndexOf("NationalID") >= 0).ToList().ForEach(f => txtsIdentification.Text = f.Split(':')[1]);
                _values.Where(w => w.IndexOf("Birthday") >= 0).ToList().ForEach(f => txtdBirth.Text = f.Split(':')[1]);  //Response.Write(_str);
            }
            catch
            {
                SetBodyEventOnLoad(@"Mgsalert('ไม่สามารถทำการดึงข้อมูลบัตรได้');");
            }
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Userlist.aspx");
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            int sID = int.Parse(STCrypt.DecryptURL(Request.QueryString["id"])[0]);
            _db.TBlackLists.Where(w => w.nUserID == sID).ToList().ForEach(f => _db.TBlackLists.Remove(f));
            string _sName = "";
            foreach (var _data in _db.TUsers.Where(w => w.sID == sID))
            {
                _data.sName = txtsName.Text;
                _data.sLastname = txtsLastName.Text;
                _data.sIdentification = txtsIdentification.Text;//txtsIdentification.Text.Length < 6 ? string.Format("{0:000000}", txtsIdentification.Text) : txtsIdentification.Text;
                _data.sStudentID = txtsIdentification.Text;
                _data.nTermSubLevel2 = int.Parse(txtSubLV2ID.Text);
                if (DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    _data.dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    _data.dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                _data.nMax = txtnMax.Text == "" ? 0 : decimal.Parse(txtnMax.Text);
                _data.sAddress = txtAddress.Text;
                _data.cType = "0";//ddlcType.SelectedValue.ToString();
                _sName = _data.sName + " " + _data.sLastname;
                _data.cSex = rblSex.SelectedValue;
                if (stdCheck.Checked && !String.IsNullOrEmpty(stdTel.Text))
                {
                    _data.cSMS = "1";
                    _data.cTelSMS = stdTel.Text;
                }
                else
                {
                    _data.cSMS = "0";
                    _data.cTelSMS = "";
                }
                _data.baseSalary = 0;

                foreach (string _str in txtval.Text.Split(','))
                {
                    TBlackList _List = new TBlackList();
                    if (_str.Replace("TProduct_", "") != "")
                    {
                        _List.nUserID = _data.sID;
                        _List.nProductID = int.Parse(_str.Replace("TProduct_", ""));
                        _db.TBlackLists.Add(_List);
                    }
                }
            }

            _db.SaveChanges();

            #region Edit Data Master
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            string sEntities = Session["sEntities"] + "";
            int? nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault().nCompany;
            foreach (var _data in _dbMaster.TUsers.Where(w => w.nSystemID == sID && w.nCompany == nCompany && w.cType == "0"))
            {
                _data.sName = txtsName.Text;
                _data.sLastname = txtsLastName.Text;
                _data.cSex = rblSex.SelectedValue;
                _data.sIdentification = txtsIdentification.Text.Length < 6 ? string.Format("{0:000000}", txtsIdentification.Text) : txtsIdentification.Text;
                if (DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    _data.dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    _data.dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                _data.sAddress = txtAddress.Text;
                _data.sEmail = "";
            }

            _dbMaster.SaveChanges();

            #endregion

            DataTable checkSTDLV = fcommon.LinqToDataTable(_db.TStudentLevels.Where(w => w.sID == sID).ToList());
            if (checkSTDLV == null)
            {
                int nSTDID = 1;
                if (_db.TStudentLevels.ToList().Count() > 0)
                    nSTDID = _db.TStudentLevels.Max(o => o.nStdLvID) + 1;
                TStudentLevel TStdLV = new TStudentLevel();
                TStdLV.nStdLvID = nSTDID;
                TStdLV.sID = sID;
                //TStdLV.nTSubLevel = Int32.Parse(ddlSubLV.SelectedValue);
                _db.TStudentLevels.Add(TStdLV);
            }
            else
            {
                //foreach (var _data in _db.TStudentLevels.Where(w => w.sID == sID))
                //{
                //    _data.nTSubLevel = Int32.Parse(ddlSubLV.SelectedValue.ToString());
                //}
            }
            _db.SaveChanges();

            database.InsertLog(Session["sEmpID"] + "", "แก้ไขข้อมูลนักเรียน " + _sName,
                Session["sEntities"].ToString(), Request, 14, 3, 0);
            Response.Redirect("Userlist.aspx");
        }
        private void Opendata()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            var tTermSubLevel2 = _db.TTermSubLevel2.ToList();
            var tLevel = _db.TSubLevels.ToList();
            try
            {
                int sID = int.Parse(STCrypt.DecryptURL(Request.QueryString["id"])[0]);
                lblUserID.Text = sID + "";
                foreach (var _data in _db.TUsers.Where(w => w.sID == sID))
                {
                    txtsName.Text = _data.sName;
                    txtsLastName.Text = _data.sLastname;
                    txtsIdentification.Text = _data.sIdentification;
                    txtdBirth.Text = _data.dBirth.Value.ToString("dd/MM/yyyy", new CultureInfo("en-us"));
                    txtAddress.Text = _data.sAddress;
                    txtnMax.Text = _data.nMax.Value.ToString();
                    txtSubLV2ID.Text = _data.nTermSubLevel2.Value.ToString();
                    rblSex.SelectedValue = _data.cSex;
                    var dTermSubLevel2 = _db.TTermSubLevel2.Find(_data.nTermSubLevel2);
                    var dLevel = _db.TSubLevels.Find(dTermSubLevel2.nTSubLevel);
                    tTermSubLevel2 = tTermSubLevel2.Where(w => w.nTSubLevel == dTermSubLevel2.nTSubLevel).ToList();

                    fcommon.ListDataTableToDropDownList(fcommon.LinqToDataTable(tLevel), ddlSubLV, "", "nTSubLevel", "SubLevel");

                    int _SubLv = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == _data.nTermSubLevel2).FirstOrDefault().nTSubLevel;
                    ddlSubLV.SelectedValue = _SubLv.ToString();
                    //ddlcType.SelectedValue = _data.cType;
                    if (_data.cType == "0")
                    {
                        rowSalary.Attributes.Add("class", "row hide");
                        rowSMS.Attributes.Add("class", "row");
                        if (_data.cSMS != null)
                        {
                            if (_data.cSMS == "1")
                            {
                                stdCheck.Checked = true;
                                rowTel.Attributes.Add("class", "row");
                                stdTel.Text = _data.cTelSMS;
                            }
                        }
                    }
                    else
                    {
                        //rowLV.Attributes.Add("class", "row hide");
                        rowSalary.Attributes.Add("class", "row");
                        rowSMS.Attributes.Add("class", "row hide");
                        rowTel.Attributes.Add("class", "row hide");
                        stdCheck.Checked = false;
                        salary.CssClass = "input--mid";
                        if (_data.baseSalary != null)
                        {
                            salary.Text = _data.baseSalary.ToString();
                        }
                    }
                    foreach (var _Black in _db.TBlackLists.Where(w => w.nUserID == _data.sID))
                    {
                        txtval.Text += "TProduct_" + _Black.nProductID + ",";
                    }

                    //foreach (var _LV in _db.TStudentLevels.Where(w => w.sID == _data.sID).Take(1))
                    //{
                    //    ddlSubLV.Items.FindByValue(_LV.nTSubLevel.ToString()).Selected = true;
                    //}
                }
            }
            catch
            {
                SetBodyEventOnLoad(@"Mgsalert('ไม่สามารถทำการบันทึกข้อมูลได้ <br/>กรุณาตรวจข้อมูล ว/ด/ป เกิดใหม่อีกครั้ง');");

            }
        }
        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }

    }
}