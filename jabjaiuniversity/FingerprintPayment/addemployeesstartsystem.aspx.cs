using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using JabjaiMainClass;
using System.Configuration;
using System.Net.Mail;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class addemployeesstartsystem1 : System.Web.UI.Page
    {
        //JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
        protected void Page_Load(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            if (!this.IsPostBack)
            {
                if (_db.TEmployees == null) Response.Redirect("Default.aspx");
                fcommon.ListDataTableToDropDownList(fcommon.LinqToDataTable(_db.TTimetypes.Where(w => w.cUserType == "2")), ddlcUserType, "", "nTimetype", "sTimetype");
            }

        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            //FingerPaymentEntities _db = new FingerPaymentEntities(Session["sEntities"].ToString());
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

                _TEmp.nTimeType = int.Parse(ddlcUserType.SelectedValue);
                _TEmp.sClaim = "222222222222222222";
                _TEmp.sName = txtsName.Text;
                _TEmp.sLastname = txtLastName.Text;
                _TEmp.sIdentification = txtsIdentification.Text.Length < 6 ? string.Format("{0:000000}", txtsIdentification.Text) : txtsIdentification.Text;
                _TEmp.sAddress = txtsAddress.Text;
                _TEmp.sPhone = txtsPhone.Text;
                _TEmp.sEmail = txtsEmail.Text;
                _TEmp.cType = rblcUserType.SelectedValue;

                try
                {
                    if (DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _TEmp.dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _TEmp.dBirth = DateTime.ParseExact(txtdBirth.Text, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                }
                catch { }

                _TEmp.cSex = rbnSex0.Checked ? "0" : "1";
                _TEmp.dUpdate = DateTime.Now;
                //_TEmp.sFinger = txtUserFinger1.Text;
                //_TEmp.sFinger2 = txtUserFinger2.Text;
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
                //SendNetMail(ConfigurationSettings.AppSettings["e-Mail"], txtsEmail.Text, "PassWord", sPassword);
                Session["sEmpID"] = _TEmp.sEmp;
                Response.Redirect("AdminMain.aspx");

            }
            catch (Exception ex)
            {
                Response.Write(ex.StackTrace.ToString());
            }
        }

        private void SendNetMail(string from, string to, string subject, string sPassword)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.Send(ConfigurationSettings.AppSettings["e-Mail"], to, subject, sPassword);
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
            do
            {
                numIterations = rand.Next(100000, 999999);

            } while (_dbMaster.TUsers.Where(w => w.sPassword == numIterations.ToString() && string.IsNullOrEmpty(w.sFinger)).ToList().Count > 0);

            return numIterations.ToString();
        }
    }
}