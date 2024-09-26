using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;

namespace FingerprintPayment.Employees
{
    public partial class employees_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int id = 0;
            Int32.TryParse(Request.QueryString["id"], out id);
            TEmployee emp = new TEmployee();

            Image img = (Image)FindControl("profileimage");
            var empdata = _db.TEmployees.Where(w => w.sEmp == id).FirstOrDefault();

            emp = empdata;

            if (!IsPostBack)
            {
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

                fcommon.ListDataTableToDropDownList(fcommon.LinqToDataTable(_db.TTimetypes.Where(w => w.cUserType == "2")), ddlcUserType, "ไม่ระบุ", "nTimetype", "sTimetype");
                fcommon.LinqToDropDownList(_db.TDepartments.Where(w => w.deleted == 0 || w.deleted == null).ToList(), ddlDepartment, "ไม่ระบุ", "departmentId", "departmentName");
                fcommon.LinqToDropDownList(_db.TTitleLists.Where(w => (w.deleted == "0" || w.deleted == null) && w.workStatus == "working").ToList(), empTitle, "ไม่ระบุ", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(_db.TJobLists.Where(w => w.workStatus == "working" && (w.deleted == "0" || w.deleted == null)).ToList(), ddlJob, "ไม่ระบุ", "nJobid", "jobDescription");
                ddlcUserType.SelectedValue = empdata.nTimeType.HasValue ? empdata.nTimeType.Value.ToString() : "ไม่ระบุ";
                ddluser_type.SelectedValue = empdata.cType;

                ddlJob.SelectedValue = emp.nJobid.ToString();

                if ((empdata.sPicture == "") || (empdata.sPicture == null))
                    profileimage.Src = "https://jabjaistorage.blob.core.windows.net/userprofile/201782151010735704913.png";
                else profileimage.Src = empdata.sPicture + "?" + DateTime.Now.ToString("HHmmssss"); ;

                var dtprovinces = dbmaster.provinces.ToList();
                fcommon.LinqToDropDownList(dtprovinces, ddlprovince, "ไม่ระบุ", "PROVINCE_ID", "PROVINCE_NAME");
                if (empdata == null)
                    ddlprovince.SelectedIndex = 0;
                else ddlprovince.SelectedValue = empdata.sProvince;
                int AMPHUR_CODE;
                int PROVINCE_ID;
                if (!string.IsNullOrEmpty(empdata.sProvince))
                {
                    int.TryParse(ddlprovince.SelectedValue, out PROVINCE_ID);
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), ddlaumper, "ไม่ระบุ", "AMPHUR_ID", "AMPHUR_NAME");
                    if (empdata.sAumpher == null) ddlaumper.SelectedIndex = 0;
                    else ddlaumper.SelectedValue = empdata.sAumpher;
                }


                if (!string.IsNullOrEmpty(empdata.sAumpher))
                {
                    int.TryParse(ddlaumper.SelectedValue, out AMPHUR_CODE);
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), ddltumbon, "ไม่ระบุ", "DISTRICT_ID", "DISTRICT_NAME");
                    if (empdata.sTumbon == null) ddltumbon.SelectedIndex = 0;
                    else ddltumbon.SelectedValue = empdata.sTumbon;
                }

                ddlDepartment.SelectedValue = emp.nDepartmentId.ToString();
                empTitle.SelectedValue = empdata.sTitle;
                ddlDate.SelectedValue = empdata.dBirth.Value.ToString("dd");
                ddlMonth.SelectedValue = empdata.dBirth.Value.ToString("MM");
                ddlAge.SelectedValue = empdata.dBirth.Value.Year.ToString();

                if (empdata.cSex == "1")
                    radiogender.SelectedIndex = 1;
                else radiogender.SelectedIndex = 2;
            }
        }

    }
}