using System;
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
    public partial class employessedit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnImport.Click += new EventHandler(btnImport_Click);
            if (!this.IsPostBack)
            {
                fcommon.ListDataTableToDropDownList(fcommon.LinqToDataTable(_db.TTimetypes.Where(w => w.cUserType == "2")), ddlcUserType, "", "nTimetype", "sTimetype");
                SetDataGrid();
                OpenData(Request.QueryString["id"] + "");
            }
            SetBodyEventOnLoad("");

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

                _values.Where(w => w.IndexOf("Address") >= 0).ToList().ForEach(f => txtsAddress.Text = f.Replace("Address:", ""));
                _values.Where(w => w.IndexOf("ThaiName") >= 0).ToList().ForEach(f => txtsName.Text = f.Replace(" ", ":").Split(':')[1]);
                _values.Where(w => w.IndexOf("ThaiName") >= 0).ToList().ForEach(f => txtLastName.Text = f.Replace(" ", ":").Split(':')[2]);
                _values.Where(w => w.IndexOf("NationalID") >= 0).ToList().ForEach(f => txtsIdentification.Text = f.Split(':')[1]);
                _values.Where(w => w.IndexOf("Birthday") >= 0).ToList().ForEach(f => txtdBirth.Text = f.Split(':')[1]);
            }
            catch
            {
                SetBodyEventOnLoad(@"Mgsalert('ไม่สามารถทำการดึงข้อมูลบัตรได้');");
            }
        }

        private void SetDataGrid()
        {

            string ListTimesSys = @"ข้อมูลปีการศึกษา,ข้อมูลวันหยุด,ข้อมูลเวลาเข้า - ออกโรงเรียน,ข้อมูลระดับชั้น,ข้อมูลห้องเรียน,ข้อมูลการเรียนการสอน,รายงาน";
            ///  string ListTimesSys = @"ข้อมูลเวลาเข้า-ออกโรงเรียน,ข้อมูลวันหยุด,ข้อมูลรายวิชา,ข้อมูลห้องเรียน,ข้อมูลปีการศึกษา,ข้อมูลการลา,รายงาน";
            DataTable _dt2 = new DataTable();
            _dt2.Columns.Add("sMenu");
            foreach (string _str in ListTimesSys.Split(','))
            {
                DataRow _dr = _dt2.NewRow();
                _dr["sMenu"] = _str;
                _dt2.Rows.Add(_dr);
            }
            dgdTime.DataSource = _dt2;
            dgdTime.DataBind();

        }
        private void OpenData(string sID)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            sID = STCrypt.DecryptURL(sID)[0];
            int nID = int.Parse(sID);
            lblUserID.Text = sID;
            try
            {
                foreach (var _data in _db.TEmployees.Where(w => w.sEmp == nID))
                {
                    txtsName.Text = _data.sName;
                    txtLastName.Text = _data.sLastname;
                    txtsIdentification.Text = _data.sIdentification;
                    txtsAddress.Text = _data.sAddress;
                    txtsPhone.Text = _data.sPhone;
                    txtsEmail.Text = _data.sEmail;
                    txtdBirth.Text = _data.dBirth.Value.ToString("dd/MM/yyyy", new CultureInfo("en-us"));
                    rbnSex0.Checked = _data.cSex == "0" ? true : false;
                    rbnSex1.Checked = _data.cSex == "1" ? true : false;
                    ddlcUserType.SelectedValue = _data.nTimeType.HasValue ? _data.nTimeType.Value.ToString() : "";
                    ddlUserType.SelectedValue = _data.cType;

                    //ContentPlaceHolder MainContent = Page.Master.FindControl("MainContent") as ContentPlaceHolder;
                    //for (int _itemIndex = 0; _itemIndex < 8; _itemIndex++)
                    //{
                    //    RadioButton rebcStatus01 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex + 1) + "_rebcStatus01");
                    //    RadioButton rebcStatus02 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex + 1) + "_rebcStatus02");
                    //    //RadioButton rebcStatus03 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex + 1) + "_rebcStatus03");
                    //    switch (_data.sClaim.Substring(_itemIndex, 1))
                    //    {
                    //        case "0":
                    //            rebcStatus01.Checked = true;
                    //            break;
                    //        case "1":
                    //            rebcStatus02.Checked = true;
                    //            break;
                    //        case "2":
                    //            //rebcStatus03.Checked = true;
                    //            break;
                    //        default:
                    //            rebcStatus01.Checked = true;
                    //            break;
                    //    }
                    //}


                    //for (int _itemIndex = 8; _itemIndex <= 12; _itemIndex++)
                    //{
                    //    if ((_itemIndex - 7) == 4) continue;
                    //    RadioButton rebcStatus01 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex - 7) + "_rebcMember01");
                    //    RadioButton rebcStatus02 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex - 7) + "_rebcMember02");
                    //    RadioButton rebcStatus03 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex - 7) + "_rebcMember03");
                    //    switch (_data.sClaim.Substring(_itemIndex, 1))
                    //    {
                    //        case "0":
                    //            rebcStatus01.Checked = true;
                    //            break;
                    //        case "1":
                    //            rebcStatus02.Checked = true;
                    //            break;
                    //        case "2":
                    //            rebcStatus03.Checked = true;
                    //            break;
                    //        default:
                    //            rebcStatus01.Checked = true;
                    //            break;
                    //    }
                    //}
                    //sbyte sMenuTime = fcommon.FindIndexColumnOfDataFieldInGrid(dgdTime, "sMenu");
                    //foreach (DataGridItem dgi in dgdTime.Items)
                    //{
                    //    RadioButton rebcTime01 = (RadioButton)dgi.FindControl("rebcTime01");
                    //    RadioButton rebcTime02 = (RadioButton)dgi.FindControl("rebcTime02");
                    //    RadioButton rebcTime03 = (RadioButton)dgi.FindControl("rebcTime03");

                    //    switch (_data.sStatusReport.Substring(dgi.ItemIndex, 1))
                    //    {
                    //        case "0":
                    //            rebcTime01.Checked = true;
                    //            break;
                    //        case "1":
                    //            rebcTime02.Checked = true;
                    //            break;
                    //        case "2":
                    //            rebcTime03.Checked = true;
                    //            break;
                    //        default:
                    //            rebcTime01.Checked = true;
                    //            break;
                    //    }
                    //}

                    string sStatusReport = _data.sStatusReport;
                    //if (!string.IsNullOrEmpty(sStatusReport))
                    //{
                    //    for (int _itemIndex = 9; _itemIndex <= 12; _itemIndex++)
                    //    {
                    //        RadioButton rebcStatus01 = (RadioButton)MainContent.FindControl("ctl" + string.Format("{0:00}", (_itemIndex)) + "_rebsubcStatus01");
                    //        RadioButton rebcStatus02 = (RadioButton)MainContent.FindControl("ctl" + string.Format("{0:00}", (_itemIndex)) + "_rebsubcStatus02");
                    //        switch (sStatusReport.Substring(_itemIndex - 9, 1))
                    //        {
                    //            case "0":
                    //                rebcStatus01.Checked = true;
                    //                break;
                    //            case "1":
                    //                rebcStatus02.Checked = true;
                    //                break;
                    //            default:
                    //                rebcStatus01.Checked = true;
                    //                break;
                    //        }
                    //    }

                    //    if (sStatusReport.Substring(4, 1) == "0") ctl09_rebsubcStatus021.Checked = true;
                    //    else if (sStatusReport.Substring(4, 1) == "1") ctl09_rebsubcStatus022.Checked = true;

                    //    if (sStatusReport.Substring(5, 1) == "0") ctl04_rebcMember01.Checked = true;
                    //    else if (sStatusReport.Substring(5, 1) == "1") ctl04_rebcMember02.Checked = true;

                    //}

                }
            }
            catch (Exception ex) { Response.Write("Error : " + ex.StackTrace.ToString()); }
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("employeeslist.aspx");
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            string sEntities = Session["sEntities"].ToString();
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            try
            {
                string sID = STCrypt.DecryptURL(Request.QueryString["id"] + "")[0];
                int nID = int.Parse(sID);
                foreach (var _data in _db.TEmployees.Where(w => w.sEmp == nID))
                {
                    #region SettingsClaim
                    //    string sClaim = "", sClaimReport = "";

                    //    ContentPlaceHolder MainContent = Page.Master.FindControl("MainContent") as ContentPlaceHolder;
                    //    for (int _itemIndex = 0; _itemIndex < 8; _itemIndex++)
                    //    {
                    //        RadioButton rebcStatus01 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex + 1) + "_rebcStatus01");
                    //        RadioButton rebcStatus02 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex + 1) + "_rebcStatus02");
                    //        RadioButton rebcStatus03 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex + 1) + "_rebcStatus03");
                    //        if (rebcStatus01.Checked) sClaim += "0";
                    //        else if (rebcStatus02.Checked) sClaim += "1";
                    //        else if (rebcStatus03.Checked) sClaim += "2";
                    //        else sClaim += "0";
                    //    }

                    //    for (int _itemIndex = 1; _itemIndex <= 5; _itemIndex++)
                    //    {
                    //        RadioButton rebcStatus01 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex) + "_rebcMember01");
                    //        RadioButton rebcStatus02 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex) + "_rebcMember02");
                    //        RadioButton rebcStatus03 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex) + "_rebcMember03");
                    //        if (rebcStatus01.Checked) sClaim += "0";
                    //        else if (rebcStatus02.Checked) sClaim += "1";
                    //        else if (_itemIndex == 4) continue;
                    //        else if (rebcStatus03.Checked) sClaim += "2";
                    //        else sClaim += "0";
                    //    }

                    //    foreach (DataGridItem dgi in dgdTime.Items)
                    //    {
                    //        RadioButton rebcTime01 = (RadioButton)dgi.FindControl("rebcTime01");
                    //        RadioButton rebcTime02 = (RadioButton)dgi.FindControl("rebcTime02");
                    //        RadioButton rebcTime03 = (RadioButton)dgi.FindControl("rebcTime03");
                    //        if (rebcTime01.Checked) sClaim += "0";
                    //        if (rebcTime02.Checked) sClaim += "1";
                    //        if (rebcTime03.Checked) sClaim += "2";
                    //    }

                    //    if (sClaim.Substring(7, 1) == "0")
                    //    {
                    //        sClaimReport += "0000";
                    //    }
                    //    else
                    //    {
                    //        for (int _itemIndex = 9; _itemIndex <= 12; _itemIndex++)
                    //        {
                    //            RadioButton rebcStatus01 = (RadioButton)MainContent.FindControl("ctl" + string.Format("{0:00}", (_itemIndex)) + "_rebsubcStatus01");
                    //            RadioButton rebcStatus02 = (RadioButton)MainContent.FindControl("ctl" + string.Format("{0:00}", (_itemIndex)) + "_rebsubcStatus02");
                    //            if (rebcStatus01.Checked) sClaimReport += "0";
                    //            else if (rebcStatus02.Checked) sClaimReport += "1";
                    //        }
                    //    }

                    //    if (ctl09_rebsubcStatus021.Checked) sClaimReport += "0";
                    //    else if (ctl09_rebsubcStatus022.Checked) sClaimReport += "1";

                    //    if (ctl04_rebcMember01.Checked) sClaimReport += "0";
                    //    else if (ctl04_rebcMember02.Checked) sClaimReport += "1";

                    #endregion
                    JabJaiMasterEntities dbmaster = Connection.MasterEntities();
                    var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var qusermaster = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.nSystemID == nID && w.cType != "0").FirstOrDefault();

                    string link = "";
                    if (FileUpload1.HasFile)
                    {
                        link = AzureStorage.UploadFile(FileUpload1, 150, qusermaster.sID);
                    }

                    txtsIdentification.Text = txtsIdentification.Text.Replace("-", "");
                    //_data.sStatusReport = sClaimReport;
                    //_data.sClaim = sClaim;
                    _data.sName = txtsName.Text;
                    _data.sLastname = txtLastName.Text;
                    _data.sIdentification = txtsIdentification.Text.Length < 6 ? string.Format("{0:000000}", txtsIdentification.Text) : txtsIdentification.Text;
                    _data.sAddress = txtsAddress.Text;
                    _data.sPhone = txtsPhone.Text;
                    _data.sEmail = txtsEmail.Text;
                    _data.nTimeType = int.Parse(ddlcUserType.SelectedValue);
                    _data.cType = ddlUserType.SelectedValue;
                    _data.sPicture = link;

                    if (DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _data.dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _data.dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                    _data.cSex = rbnSex0.Checked ? "0" : "1";

                    if (!string.IsNullOrEmpty(txtUserFinger1.Text))
                    {
                        _data.sFinger = txtUserFinger1.Text;
                        _data.sFinger2 = txtUserFinger2.Text;
                    }

                    _data.dUpdate = DateTime.Now;
                    database.InsertLog(Session["sEmpID"] + "", "แก้ไขข้อมูลพนักงาน " + _data.sName + " " + _data.sLastname,
                        HttpContext.Current.Session["sEntities"].ToString(), Request, 13, 3, 0);
                }
                _db.SaveChanges();

                #region Edit Data Master
                JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
                int? nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault().nCompany;
                foreach (var _data in _dbMaster.TUsers.Where(w => w.nSystemID == nID && w.nCompany == nCompany && w.cType == "1"))
                {
                    _data.sName = txtsName.Text;
                    _data.sLastname = txtLastName.Text;
                    _data.sIdentification = txtsIdentification.Text.Length < 6 ? string.Format("{0:000000}", txtsIdentification.Text) : txtsIdentification.Text;

                    if (DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _data.dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _data.dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                    _data.sAddress = txtsAddress.Text;
                    _data.sEmail = "";
                }

                _dbMaster.SaveChanges();

                #endregion

                Response.Redirect("employeeslist.aspx");
            }
            catch (Exception err)
            {
                Response.Write(err);
                //SetBodyEventOnLoad(@"Mgsalert('ไม่สามารถทำการบันทึกข้อมูลได้ <br/>กรุณาตรวจข้อมูล ว/ด/ป เกิดใหม่อีกครั้ง');");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){ j_info('System Error','" + err.Message.ToString() + "'); return false;});", true);
            }

        }
        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }
    }
}