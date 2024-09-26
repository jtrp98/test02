using JabjaiEntity.DB;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiMainClass;
using System.Globalization;

namespace FingerprintPayment.Employees
{
    public partial class employees_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();

            int id = 0;
            Int32.TryParse(Request.QueryString["id"], out id);
            TEmployee emp = new TEmployee();

            Image img = (Image)FindControl("profileimage");
            var empdata = _db.TEmployees.Where(w => w.sEmp == id).FirstOrDefault();

            emp = empdata;

            Button1.Click += new EventHandler(Button1_Click);
            Button2.Click += new EventHandler(Button2_Click);


            if (!IsPostBack)
            {
                string json = fcommon.EntityToJson(empdata);
                fcommon.ListYears(ddlAge, "", 1900, "en-us", "th-TH");
                empName.Text = empdata.sName;
                empLastname.Text = empdata.sLastname;
                empIdCard.Text = empdata.sIdentification;
                empPhone.Text = empdata.sPhone;
                empEmail.Text = empdata.sEmail;
                empPost.Text = empdata.sPost;
                empHomenumber.Text = empdata.sHomeNumber;
                empSoy.Text = empdata.sSoy;
                empMuu.Text = empdata.sMuu;
                empRoad.Text = empdata.sRoad;
                empEmail.Text = empdata.sEmail;

                fcommon.ListDataTableToDropDownList(fcommon.LinqToDataTable(_db.TTimetypes.Where(w => w.cUserType == "2")), ddlcUserType, "- กรุณาเลือกตารางเวลา - ", "nTimetype", "sTimetype");
                ddlcUserType.SelectedValue = empdata.nTimeType.HasValue ? empdata.nTimeType.Value.ToString() : "";
                ddluser_type.SelectedValue = empdata.cType;

                if ((empdata.sPicture == "") || (empdata.sPicture == null))
                    profileimage.Src = "https://jabjaistorage.blob.core.windows.net/userprofile/201782151010735704913.png";
                else profileimage.Src = empdata.sPicture + "?" + DateTime.Now.ToString("HHmmssss"); ;

                var dtprovinces = dbmaster.provinces.ToList();
                int PROVINCE_ID, AMPHUR_CODE;
                fcommon.LinqToDropDownList(dtprovinces, ddlprovince, "- กรุณาเลือก -", "PROVINCE_ID", "PROVINCE_NAME");
                if (empdata == null)
                    ddlprovince.SelectedIndex = 0;
                else ddlprovince.SelectedValue = empdata.sProvince;

                if (!string.IsNullOrEmpty(empdata.sProvince))
                {
                    int.TryParse(empdata.sProvince, out PROVINCE_ID);
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), ddlaumper, "- กรุณาเลือก -", "AMPHUR_ID", "AMPHUR_NAME");
                    if (empdata.sAumpher == null) ddlaumper.SelectedIndex = 0;
                    else ddlaumper.SelectedValue = empdata.sAumpher;
                }

                if (!string.IsNullOrEmpty(empdata.sAumpher))
                {
                    int.TryParse(ddlaumper.SelectedValue, out AMPHUR_CODE);
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), ddltumbon, "- กรุณาเลือก -", "DISTRICT_ID", "DISTRICT_NAME");
                    if (empdata.sTumbon == null) ddltumbon.SelectedIndex = 0;
                    else ddltumbon.SelectedValue = empdata.sTumbon;
                }

                fcommon.LinqToDropDownList(_db.TTitleLists.Where(w => w.workStatus == "working" && w.deleted == "0" || w.deleted == null).ToList(), empTitle, "- กรุณาเลือก -", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(_db.TJobLists.Where(w => w.workStatus == "working" && w.deleted == "0" || w.deleted == null).ToList(), ddlJob, "- กรุณาเลือก -", "nJobid", "jobDescription");
                fcommon.LinqToDropDownList(_db.TDepartments.Where(w => w.deleted == 0 || w.deleted == null).ToList(), ddlDepartment, "- กรุณาเลือก -", "departmentId", "departmentName");

                empTitle.SelectedValue = empdata.sTitle;
                ddlDate.SelectedValue = empdata.dBirth.Value.ToString("dd");
                ddlMonth.SelectedValue = empdata.dBirth.Value.ToString("MM");
                ddlAge.SelectedValue = empdata.dBirth.Value.Year.ToString();
                ddlJob.SelectedValue = emp.nJobid.ToString();
                ddlDepartment.SelectedValue = emp.nDepartmentId.ToString();

                if (empdata.cSex == "1")
                    radiogender.SelectedIndex = 1;
                else radiogender.SelectedIndex = 2;
                string sEntities = Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var tuser = dbmaster.TUsers.Where(w => w.nSystemID == id && w.nCompany == qcompany.nCompany && w.cType == "1").FirstOrDefault();
                if (tuser.sFinger == null && tuser.sFinger2 == null && tuser.sPassword == "000000")
                {
                    //ltrfinger.Text = "รหัสลายนิ้วมือ";
                    btnDelFinger.Visible = true;
                    //ltrpassword.Text = tuser.sPassword;
                }
                else if (tuser.sFinger == null && tuser.sFinger2 == null)
                {
                    ltrfinger.Text = "รหัสลายนิ้วมือ";
                    btnDelFinger.Visible = false;
                    ltrpassword.Text = tuser.sPassword;
                }
                else
                {
                    ltrfinger.Text = "ลายนิ้วมือ";
                }
            }
        }

        void Button1_Click(object sender, EventArgs e)
        {
            if (ddlAge.SelectedValue == "-1")
            {
                Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                return;
            }

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            int id = 0;
            Int32.TryParse(Request.QueryString["id"], out id);

            string sEntities = Session["sEntities"].ToString();
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            var qusermaster = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.nSystemID == id && w.cType != "0").FirstOrDefault();
            string link = "";
            if (FileUpload1.HasFile)
            {
                link = AzureStorage.UploadFile(FileUpload1, 150, qusermaster.sID);
            }
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
            string sex = "";

            foreach (var _data in _db.TEmployees.Where(w => w.sEmp == id))
            {
                _data.sHomeNumber = empHomenumber.Text;
                _data.sSoy = empSoy.Text;
                _data.sTumbon = ddltumbon.Text;
                _data.sProvince = ddlprovince.Text;
                _data.sMuu = empMuu.Text;
                _data.sRoad = empRoad.Text;
                _data.sAumpher = ddlaumper.Text;
                _data.sPost = empPost.Text;
                _data.sTitle = empTitle.SelectedValue.ToString();

                _data.sName = empName.Text;
                _data.sLastname = empLastname.Text;
                _data.sIdentification = empIdCard.Text;
                _data.sPhone = empPhone.Text;
                _data.sEmail = empEmail.Text;
                _data.nTimeType = int.Parse(ddlcUserType.SelectedValue);
                _data.cType = ddluser_type.SelectedValue;
                if (!string.IsNullOrEmpty(ddlJob.SelectedValue)) _data.nJobid = int.Parse(ddlJob.SelectedValue);
                if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue)) _data.nDepartmentId = int.Parse(ddlDepartment.SelectedValue.ToString());

                if (!string.IsNullOrEmpty(link)) _data.dPicUpdate = DateTime.Now;
                sex = "";
                if (radiogender.SelectedValue == "ชาย")
                    sex = "0";
                else sex = "1";
                _data.sAddress = empHomenumber.Text + soy + muu + road + tumbon + aumpher + province + post;

                _data.dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                _data.cSex = sex;
                _data.dUpdate = DateTime.Now;
                if (!string.IsNullOrEmpty(link)) _data.sPicture = link;
                if (!string.IsNullOrEmpty(link)) _data.nPicversion = (_data.nPicversion.HasValue ? _data.nPicversion.Value : 0) + 1;
                database.InsertLog(Session["sEmpID"] + "", "แก้ไขข้อมูลพนักงาน " + _data.sName + " " + _data.sLastname
                    , Session["sEntities"].ToString(), Request, 13, 3, 0);
            }
            _db.SaveChanges();

            #region Edit Data Master
            int? nCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault().nCompany;
            int? AMPHUR_ID = !string.IsNullOrEmpty(ddlaumper.SelectedValue) ? int.Parse(ddlaumper.SelectedValue) : 0;
            int? PROVINCE_ID = !string.IsNullOrEmpty(ddlprovince.SelectedValue) ? int.Parse(ddlprovince.SelectedValue) : 0;
            int? DISTRICT_ID = !string.IsNullOrEmpty(ddltumbon.SelectedValue) ? int.Parse(ddltumbon.SelectedValue) : 0;
            foreach (var _data in dbmaster.TUsers.Where(w => w.nSystemID == id && w.nCompany == nCompany && w.cType == "1"))
            {
                _data.sName = empName.Text;
                _data.sLastname = empLastname.Text;
                _data.sIdentification = empIdCard.Text;

                _data.AMPHUR_ID = AMPHUR_ID;
                _data.DISTRICT_ID = DISTRICT_ID;
                _data.PROVINCE_ID = PROVINCE_ID;
                _data.sPostalcode = empPost.Text;
                _data.dUpdate = DateTime.Now;
                birthDate = ddlDate.SelectedValue;
                birthMonth = ddlMonth.SelectedValue;
                birthYear = ddlAge.SelectedValue;
                combinedate = birthDate + "/" + birthMonth + "/" + birthYear;

                _data.dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));

                _data.sAddress = empHomenumber.Text + soy + muu + road + tumbon + aumpher + province + post;
                _data.sEmail = empEmail.Text;
            }

            dbmaster.SaveChanges();

            #endregion
            //

            Response.Redirect("/employees/employees-list.aspx");
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

        protected void btnDelFinger_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(Request.QueryString["id"]);
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                string sEntities = Session["sEntities"].ToString();
                var tcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var quser = dbmaster.TUsers.Where(w => w.nSystemID == ID && w.nCompany == tcompany.nCompany && w.cType != "0").ToList().FirstOrDefault();
                quser.cDel = "1";
                quser.dUpdate = DateTime.Now;

                dbmaster.SaveChanges();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                {
                    var _data = dbschool.TEmployees.Where(w => w.sEmp == ID).FirstOrDefault();
                    _data.cDel = "1";
                    _data.dUpdate = DateTime.Now;

                    database.InsertLog(Session["sEmpID"] + ""
                        , "ลบลายนิ้วมือพนักงาน " + _data.sName + " " + _data.sLastname
                        , HttpContext.Current.Session["sEntities"].ToString(),
                        Request, 13, 4, 0);
                    dbschool.SaveChanges();
                }
            }
        }
    }
}