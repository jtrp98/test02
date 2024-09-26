using System;
using System.Linq;
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
    public partial class employessadd : System.Web.UI.Page
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
        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("employeeslist.aspx");
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
        void btnSave_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            try
            {
                TEmployee _TEmp = new TEmployee();
                txtsIdentification.Text = txtsIdentification.Text.Replace("-", "");
                if (_db.TEmployees.Count() == 0)
                {
                    _TEmp.sEmp = 2;
                }
                else
                {
                    if (_db.TUsers.Where(w => w.sIdentification == txtsIdentification.Text && string.IsNullOrEmpty(w.cDel)).Count() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", @"<script type=""text/javascript"">  $(document).ready(function () { Mgsalert('หมายเลขบัตรประชาชนนี้ได้ทำการลงทะเบียนแล้ว'); });</script>", false);
                        return;
                    }
                    _TEmp.sEmp = _db.TEmployees.OrderByDescending(o => o.sEmp).Take(1).SingleOrDefault().sEmp + 1;
                }
                #region SettingsClaim
                string sClaim = "", sClaimReport = "";

                ContentPlaceHolder MainContent = Page.Master.FindControl("MainContent") as ContentPlaceHolder;
                for (int _itemIndex = 0; _itemIndex < 8; _itemIndex++)
                {
                    RadioButton rebcStatus01 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex + 1) + "_rebcStatus01");
                    RadioButton rebcStatus02 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex + 1) + "_rebcStatus02");
                    RadioButton rebcStatus03 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex + 1) + "_rebcStatus03");
                    if (rebcStatus01.Checked) sClaim += "0";
                    else if (rebcStatus02.Checked) sClaim += "1";
                    else if (rebcStatus03.Checked) sClaim += "2";
                    else sClaim += "0";
                }

                for (int _itemIndex = 1; _itemIndex <= 5; _itemIndex++)
                {
                    RadioButton rebcStatus01 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex) + "_rebcMember01");
                    RadioButton rebcStatus02 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex) + "_rebcMember02");
                    RadioButton rebcStatus03 = (RadioButton)MainContent.FindControl("ctl0" + (_itemIndex) + "_rebcMember03");
                    if (rebcStatus01.Checked) sClaim += "0";
                    else if (rebcStatus02.Checked) sClaim += "1";
                    else if (_itemIndex == 4) continue;
                    else if (rebcStatus03.Checked) sClaim += "2";
                    else sClaim += "0";
                }

                foreach (DataGridItem dgi in dgdTime.Items)
                {
                    RadioButton rebcTime01 = (RadioButton)dgi.FindControl("rebcTime01");
                    RadioButton rebcTime02 = (RadioButton)dgi.FindControl("rebcTime02");
                    RadioButton rebcTime03 = (RadioButton)dgi.FindControl("rebcTime03");
                    if (rebcTime01.Checked) sClaim += "0";
                    if (rebcTime02.Checked) sClaim += "1";
                    if (rebcTime03.Checked) sClaim += "2";
                }

                if (sClaim.Substring(7, 1) == "0")
                {
                    sClaimReport += "0000";
                }
                else
                {
                    for (int _itemIndex = 9; _itemIndex <= 12; _itemIndex++)
                    {
                        RadioButton rebcStatus01 = (RadioButton)MainContent.FindControl("ctl" + string.Format("{0:00}", (_itemIndex)) + "_rebsubcStatus01");
                        RadioButton rebcStatus02 = (RadioButton)MainContent.FindControl("ctl" + string.Format("{0:00}", (_itemIndex)) + "_rebsubcStatus02");
                        if (rebcStatus01.Checked) sClaimReport += "0";
                        else if (rebcStatus02.Checked) sClaimReport += "1";
                    }
                }

                if (ctl09_rebsubcStatus021.Checked) sClaimReport += "0";
                else if (ctl09_rebsubcStatus022.Checked) sClaimReport += "1";

                if (ctl04_rebcMember01.Checked) sClaimReport += "0";
                else if (ctl04_rebcMember02.Checked) sClaimReport += "1";

                #endregion

                string link = "";
                if (FileUpload1.HasFile)
                {
                    link = AzureStorage.UploadFile(FileUpload1);
                }

                _TEmp.nTimeType = int.Parse(ddlcUserType.SelectedValue);
                _TEmp.sStatusReport = sClaimReport;
                _TEmp.sClaim = sClaim;
                _TEmp.sName = txtsName.Text;
                _TEmp.sLastname = txtLastName.Text;
                _TEmp.sIdentification = txtsIdentification.Text.Length < 6 ? string.Format("{0:000000}", txtsIdentification.Text) : txtsIdentification.Text;
                _TEmp.sAddress = txtsAddress.Text;
                _TEmp.sPhone = txtsPhone.Text;
                _TEmp.sEmail = txtsEmail.Text;
                _TEmp.cType = rblcUserType.SelectedValue;
                _TEmp.sPicture = link;

                DateTime _dBirth;
                try
                {
                    if (DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                    _TEmp.dBirth = _dBirth;
                }
                catch { }

                _TEmp.cSex = rbnSex0.Checked ? "0" : "1";
                _TEmp.dUpdate = DateTime.Now;

                _db.TEmployees.Add(_TEmp);
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
                    sName = _TEmp.sName,
                    sLastname = _TEmp.sLastname,
                    sIdentification = _TEmp.sIdentification,
                    cSex = _TEmp.cSex,
                    sPhone = _TEmp.sPhone,
                    sEmail = _TEmp.sEmail,
                    sPassword = sPassword,
                    dUpdate = DateTime.Now,
                    cType = "1",
                    nCompany = nCompany,
                    nSystemID = _TEmp.sEmp,
                    dBirth = _TEmp.dBirth
                });

                _dbMaster.SaveChanges();

                #endregion

                database.InsertLog(Session["sEmpID"] + "", "เพิ่มข้อมูลพนักงาน " + _TEmp.sName + " " + _TEmp.sLastname,
                    Session["sEntities"].ToString(), Request, 13, 2, 0);
                Response.Redirect("employeeslist.aspx");
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