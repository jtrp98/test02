using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;
using System.Net;
using System.Transactions;

namespace FingerprintPayment.Employees
{
    public partial class employees_add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();

            Button1.Click += new EventHandler(Button1_Click);
            Button2.Click += new EventHandler(Button2_Click);

            if (!IsPostBack)
            {
                fcommon.ListYears(ddlAge, "", 1900, "en-us", "th-TH");
                fcommon.ListDataTableToDropDownList(fcommon.LinqToDataTable(_db.TTimetypes.Where(w => w.cUserType == "2")), ddlcUserType, "", "nTimetype", "sTimetype");

                profileimage.Src = "https://jabjaistorage.blob.core.windows.net/userprofile/201782151010735704913.png";

                var dtprovinces = dbmaster.provinces.ToList();
                fcommon.LinqToDropDownList(dtprovinces, ddlprovince, "", "PROVINCE_ID", "PROVINCE_NAME");

                int PROVINCE_ID = int.Parse(ddlprovince.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), ddlaumper, "", "AMPHUR_ID", "AMPHUR_NAME");

                int AMPHUR_CODE = int.Parse(ddlaumper.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), ddltumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

                fcommon.LinqToDropDownList(_db.TTitleLists.Where(w => w.workStatus == "working" && w.deleted == "0").ToList(), empTitle, "", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(_db.TJobLists.Where(w => w.workStatus == "working" && w.deleted == "0").ToList(), ddlJob, "", "nJobid", "jobDescription");
            }
        }


        void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAge.SelectedValue == "-1")
                {
                    Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                    return;
                }

                JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));

                string sEntities = Session["sEntities"].ToString();
                JabJaiMasterEntities dbmaster = Connection.MasterEntities();
                //
                string soy = "";
                string muu = "";
                string road = "";
                string tumbon = "";
                string aumpher = "";
                string post = "";
                string province = ddlprovince.SelectedValue;
                string birthDate = ddlDate.SelectedValue;
                string birthMonth = ddlMonth.SelectedValue;
                string birthYear = ddlAge.SelectedValue;
                string combinedate = birthDate + "/" + birthMonth + "/" + birthYear;

                if (empSoy.Text != "")
                    soy = " ซอย" + empSoy.Text;
                if (empMuu.Text != "")
                    muu = " หมู่ " + empMuu.Text;
                if (empRoad.Text != "")
                    road = " ถนน" + empRoad.Text;
                if (ddltumbon.Text != "")
                    tumbon = " ตำบล" + ddltumbon.Text;
                if (ddlaumper.Text != "")
                    aumpher = " อำเภอ" + ddlaumper.Text;
                if (empPost.Text != "")
                    post = " รหัสไปรษณีย์ " + empPost.Text;
                int sID = 0;
                int sEmp = 0;
                //using (TransactionScope transactionscope = new TransactionScope())
                //{
                #region Add Data School
                string sPicture = "";

                int nPicversion = !string.IsNullOrEmpty(sPicture) ? 1 : 0;
                int nJob_id = int.Parse(ddlJob.SelectedValue);
                DateTime dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                try
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        sEmp = _db.TEmployees.Count() > 0 ? _db.TEmployees.Max(max => max.sEmp) + 1 : 1;
                        _db.TEmployees.Add(new TEmployee
                        {
                            sEmp = sEmp,
                            sHomeNumber = empHomenumber.Text,
                            sSoy = empSoy.Text,
                            sTumbon = ddltumbon.SelectedValue,
                            sProvince = ddlprovince.SelectedValue,
                            sMuu = empMuu.Text,
                            sRoad = empRoad.Text,
                            sAumpher = ddlaumper.SelectedValue,
                            sPost = empPost.Text,
                            sTitle = empTitle.SelectedValue.ToString(),
                            sName = empName.Text,
                            sLastname = empLastname.Text,
                            sIdentification = empIdCard.Text,
                            sPhone = empPhone.Text,
                            sEmail = empEmail.Text,
                            nTimeType = int.Parse(ddlcUserType.SelectedValue),
                            cType = ddluser_type.SelectedValue,
                            cSex = radiogender.SelectedValue == "ชาย" ? "0" : "1",
                            sAddress = empHomenumber.Text + soy + muu + road + tumbon + aumpher + province + post,
                            dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")),
                            dUpdate = DateTime.Now,
                            sPicture = sPicture,
                            dPicUpdate = DateTime.Now,
                            nPicversion = nPicversion,
                            nJobid = nJob_id,
                            nMoney = 0
                        });
                        _db.SaveChanges();
                        transactionScope.Complete();
                    }
                }
                finally
                {
                    try
                    {
                        #region Add Data Master
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                            int? AMPHUR_ID = !string.IsNullOrEmpty(ddlaumper.SelectedValue) ? int.Parse(ddlaumper.SelectedValue) : 0;
                            int? PROVINCE_ID = !string.IsNullOrEmpty(ddlprovince.SelectedValue) ? int.Parse(ddlprovince.SelectedValue) : 0;
                            int? DISTRICT_ID = !string.IsNullOrEmpty(ddltumbon.SelectedValue) ? int.Parse(ddltumbon.SelectedValue) : 0;

                            sID = dbmaster.TUsers.Count() > 0 ? dbmaster.TUsers.Max(max => max.sID) + 1 : 1;
                            dbmaster.TUsers.Add(new MasterEntity.TUser
                            {
                                sName = empName.Text,
                                nSystemID = sEmp,
                                sID = sID,
                                sLastname = empLastname.Text,
                                sIdentification = empIdCard.Text,
                                dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")),
                                sAddress = empHomenumber.Text + soy + muu + road + tumbon + aumpher + province + post,
                                sPassword = RandomNumber(),
                                sEmail = empEmail.Text,
                                nCompany = qcompany.nCompany,
                                cType = "1",
                                AMPHUR_ID = AMPHUR_ID,
                                PROVINCE_ID = PROVINCE_ID,
                                DISTRICT_ID = DISTRICT_ID,
                                username = empPhone.Text,
                                sPhone = empPhone.Text,
                                cSex = radiogender.SelectedValue == "ชาย" ? "0" : "1",
                                sPostalcode = empPost.Text,
                                dUpdate = DateTime.Now,
                                userpassword = dBirth.ToString("ddMMyyyy"),
                            });

                            dbmaster.SaveChanges();
                            transactionScope.Complete();
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        var f_emp = _db.TEmployees.FirstOrDefault(f => f.sEmp == sEmp);
                        if (f_emp != null)
                        {
                            _db.TEmployees.Remove(f_emp);
                            _db.SaveChanges();
                        }
                    }
                    finally
                    {
                        if (FileUpload1.HasFile)
                        {
                            sPicture = AzureStorage.UploadFile(FileUpload1, 150, sID);
                        }
                    }
                }

                database.InsertLog(Session["sEmpID"] + "", "เพิ่มข้อมูลพนักงาน " + empName.Text + " " + empLastname.Text
                    , Session["sEntities"].ToString(), Request, 13, 2, 0);
                #endregion
                //}

                Response.Redirect("/employees/employees-list.aspx");
            }
            catch (DbEntityValidationException dbe)
            {

            }
            catch (Exception ex)
            {
                //HttpWebRequest.(HttpStatusCode.OK, ex.ToString());
                //return;
            }
        }

        void Button2_Click(object sender, EventArgs e)
        {

            Response.Redirect("/employees/employees-list.aspx");
        }

        protected void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int PROVINCE_ID = int.Parse(ddlprovince.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), ddlaumper, "", "AMPHUR_ID", "AMPHUR_NAME");

            int AMPHUR_CODE = int.Parse(ddlaumper.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), ddltumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
        }

        protected void ddlaumper_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int AMPHUR_CODE = int.Parse(ddlaumper.SelectedValue);
            var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), ddltumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            empPost.Text = qAMPHUR.POSTCODE;
        }

        private string RandomNumber()
        {
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            string snumIterations = "";
            do
            {
                numIterations = rand.Next(100000, 999999);
                snumIterations = numIterations.ToString();

            } while (_dbMaster.TUsers.Where(w => w.sPassword == snumIterations && string.IsNullOrEmpty(w.sFinger)).ToList().Count > 0);

            return numIterations.ToString();
        }

    }
}