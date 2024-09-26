using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
//using CLSmartCard;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class Useradd : System.Web.UI.Page
    {
        public string sSubLVID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            //rc = new RequestCard("Alcor Micro USB Smart Card Reader");
            btnSave.Click += new EventHandler(btnSave_Click);
            btnImport.Click += new EventHandler(btnImport_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            if (!IsPostBack)
            {
                DataTable _dt = fcommon.LinqToDataTable(_db.TTypes.Where(w => string.IsNullOrEmpty(w.cDel)));
                if (_dt != null)
                    fcommon.ListDataTableToDropDownList(_dt, ddlTypeProduct, "ทั้งหมด", "nTypeID", "sType");
                foreach (var _Type in _db.TTypes.Where(w => string.IsNullOrEmpty(w.cDel)))
                {
                    ltrListProduct.Text += string.Format(@"<div ID='Type_{0}'>{1}", _Type.nTypeID, _Type.sType);
                    foreach (var _data in _db.TProducts.Where(w => w.nType == _Type.nTypeID && string.IsNullOrEmpty(w.cDel)))
                    {
                        ltrListProduct.Text += string.Format(@"<div ID='TProduct_{0}' class='TProduct' style=""margin-left:20px; cursor:pointer;cursor:pointer;width: 100%; clear: right;"">{1}</div>", _data.nProductID, _data.sProduct);
                    }
                    ltrListProduct.Text += "</div>";
                }
                rowSalary.Attributes.Add("class", "row hide");
                rowTel.Attributes.Add("class", "row hide");
                rowSMS.Attributes.Add("class", "row");

                var _listslv = _db.TSubLevels.ToList();
                foreach (var DataLV in _listslv)
                {
                    ddlSubLV.Items.Add(new ListItem(DataLV.SubLevel.ToString(), DataLV.nTSubLevel.ToString()));
                }

            }
            SetBodyEventOnLoad("");
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Userlist.aspx");
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
                SetBodyEventOnLoad(@"showModal('แจ้งผลการดำเนินการ','ไม่สามารถทำการดึงข้อมูลบัตรได้');");
            }
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            if (_db.TUsers.Where(w => w.sIdentification == txtsIdentification.Text && string.IsNullOrEmpty(w.cDel)).Count() > 0)
            {
                SetBodyEventOnLoad(@"showModal('แจ้งผลการดำเนินการ','หมายเลขบัตรนี้ได้ทำการใช้ไปแล้ว');");
                return;
            }
            JabjaiEntity.DB.TUser _User = new JabjaiEntity.DB.TUser();
            int nID = 2;
            if (_db.TUsers.ToList().Count() > 0)
                nID = _db.TUsers.Max(o => o.sID) + 1;
            _User.sID = nID;
            //_User.sFinger = txtUserFinger1.Text;
            //_User.sFinger2 = txtUserFinger2.Text;
            _User.sName = txtsName.Text;
            _User.sLastname = txtsLastName.Text;
            _User.nMoney = 0;
            _User.nTermSubLevel2 = int.Parse(txtSubLV2ID.Text);
            //Response.Write(txtdBirth.Text); return;
            try
            {
                DateTime? _dBirth;
                if (DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    _dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    _dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                _User.dBirth = _dBirth;
                _User.sIdentification = txtsIdentification.Text.Length < 6 ? string.Format("{0:000000}", txtsIdentification.Text) : txtsIdentification.Text;
                _User.nMax = txtnMax.Text == "" ? 0 : int.Parse(txtnMax.Text);
                _User.sAddress = txtAddress.Text;
                _User.cType = "0"; //ddlcType.SelectedValue.ToString();
                _User.cSex = rblSex.SelectedValue;
                //if (ddlcType.SelectedValue == "0")
                //{
                if (stdCheck.Checked && !String.IsNullOrEmpty(stdTel.Text))
                {
                    _User.cSMS = "1";
                    _User.cTelSMS = stdTel.Text;
                }
                else
                {
                    _User.cSMS = "0";
                    _User.cTelSMS = "";
                }
                _User.baseSalary = 0;

                _db.TUsers.Add(_User);
                foreach (string _str in txtval.Text.Split(','))
                {
                    TBlackList _List = new TBlackList();
                    if (_str.Replace("TProduct_", "") != "")
                    {
                        _List.sID = _User.sID;
                        _List.nProductID = int.Parse(_str.Replace("TProduct_", ""));
                        _db.TBlackLists.Add(_List);
                    }
                }
                _db.SaveChanges();

                #region Add Data Master
                JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
                string sPassword = RandomNumber();
                string sEntities = Session["sEntities"] + "";
                int nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault().nCompany;
                int sID = 1;
                if (_dbMaster.TUsers.ToList().Count > 0) sID = _dbMaster.TUsers.Max(M => M.sID) + 1;
                _dbMaster.TUsers.Add(new MasterEntity.TUser
                {
                    sID = sID,
                    sName = _User.sName,
                    sLastname = _User.sLastname,
                    sIdentification = _User.sIdentification,
                    cSex = _User.cSex,
                    sPhone = _User.sPhone,
                    sEmail = _User.sEmail,
                    sPassword = sPassword,
                    dUpdate = DateTime.Now,
                    cType = "0",
                    nCompany = nCompany,
                    nSystemID = _User.sID,
                    dBirth = _dBirth
                });

                _dbMaster.SaveChanges();

                #endregion

                //int nSTDID = 1;
                //if (_db.TStudentLevels.ToList().Count() > 0)
                //    nSTDID = _db.TStudentLevels.Max(o => o.nStdLvID) + 1;
                TStudentLevel TStdLV = new TStudentLevel();
                //TStdLV.nStdLvID = nSTDID;
                TStdLV.sID = nID;
                //TStdLV.nTSubLevel = Int32.Parse(ddlSubLV.SelectedValue);
                _db.TStudentLevels.Add(TStdLV);
                _db.SaveChanges();
                database.InsertLog(Session["sEmpID"] + "", "เพิ่มข้อมูลนักเรียน " + _User.sName + " " + _User.sLastname,
                    Session["sEntities"].ToString(), Request, 14, 2, 0);

                Response.Redirect("Userlist.aspx");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                SetBodyEventOnLoad(@"showModal('แจ้งผลการดำเนินการ','ไม่สามารถทำการบันทึกข้อมูลได้ <br/>กรุณาตรวจข้อมูล ว/ด/ป เกิดใหม่อีกครั้ง ');");

            }

        }
        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }

        private string RandomNumber()
        {
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            string snumIterations = "";
            do
            {
                numIterations = rand.Next(1000, 999999);
                snumIterations = numIterations.ToString();

            } while (_dbMaster.TUsers.Where(w => w.sPassword == snumIterations && string.IsNullOrEmpty(w.sFinger)).ToList().Count > 0);

            return numIterations.ToString();
        }
    }
}