using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;

using MasterEntity;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using JabjaiEntity.DB;
using OBS;
using OBS.Model;

namespace FingerprintPayment
{
    public partial class schoolprofile_edit : System.Web.UI.Page
    {
        protected string _lat;
        protected string _lng;

        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            btnSave.Click += new EventHandler(btnSave_Click);
            btnBack.Click += new EventHandler(btnBack_Click);


            if (!IsPostBack)
            {

                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();

                    var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var _dr = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).FirstOrDefault();

                    schNameTH.Text = _dr.sCompany;

                    if (nCompany.nSchoolHeadid != null)
                        schoolheadtxt.Text = nCompany.nSchoolHeadid.ToString();
                    if (nCompany.nRegistraDirectorid != null)
                        registxt.Text = nCompany.nRegistraDirectorid.ToString();
                    if (nCompany.nAcademicDirectorid != null)
                        academictxt.Text = nCompany.nAcademicDirectorid.ToString();
                    if (nCompany.nAcademicSubDirectorid != null)
                        subacademictxt.Text = nCompany.nAcademicSubDirectorid.ToString();
                    if (nCompany.nAccountingDirectorid != null)
                        acctxt.Text = nCompany.nAccountingDirectorid.ToString();
                    if (nCompany.nStudentDevelopmentDirectorid != null)
                        stdtxt.Text = nCompany.nStudentDevelopmentDirectorid.ToString();
                    if (nCompany.nWebAdminid != null)
                        webtxt.Text = nCompany.nWebAdminid.ToString();

                    if (nCompany.nPersonnel != null)
                        personnelTxt.Text = nCompany.nPersonnel.ToString();
                    if (nCompany.nGM != null)
                        gmTxT.Text = nCompany.nGM.ToString();


                    if (_dr.latitude == null)
                        _lat = 13.7563309.ToString();
                    else _lat = _dr.latitude.ToString();

                    if (_dr.longitude == null)
                        _lng = 100.50176510000006.ToString();
                    else _lng = _dr.longitude.ToString();

                    if (_dr.sImage == null || _dr.sImage == "")
                        schoolpicture.Src = "http://i.imgur.com/RbYLN3E.png";
                    else schoolpicture.Src = _dr.sImage;

                    if (_dr.schoolCoverPicture == null || _dr.schoolCoverPicture == "")
                        schoolcover.Src = "https://i.imgur.com/YsXkh0f.png";
                    else schoolcover.Src = _dr.schoolCoverPicture;



                    if (_dr.sCode == null)
                        schCode.Text = "";
                    else schCode.Text = _dr.sCode;

                    if (_dr.sNameEN == null)
                        schNameEN.Text = "";
                    else schNameEN.Text = _dr.sNameEN;

                    if (_dr.sPhoneOne == null)
                        schPhoneOne.Text = "";
                    else schPhoneOne.Text = _dr.sPhoneOne;

                    if (_dr.sPhoneTwo == null)
                        schPhoneTwo.Text = "";
                    else schPhoneTwo.Text = _dr.sPhoneTwo;

                    if (_dr.sMobilePhone == null)
                        schMobilePhone.Text = "";
                    else schMobilePhone.Text = _dr.sMobilePhone;

                    if (_dr.sFax == null)
                        schFax.Text = "";
                    else schFax.Text = _dr.sFax;

                    if (_dr.sEmailOne == null)
                        schEmailOne.Text = "";
                    else schEmailOne.Text = _dr.sEmailOne;

                    if (_dr.sEmailTwo == null)
                        schEmailTwo.Text = "";
                    else schEmailTwo.Text = _dr.sEmailTwo;

                    if (_dr.sWebsite == null)
                        schWebsite.Text = "";
                    else schWebsite.Text = _dr.sWebsite;

                    if (_dr.sHomeNumber == null)
                        schHomeNumber.Text = "";
                    else schHomeNumber.Text = _dr.sHomeNumber;

                    if (_dr.sSoy == null)
                        schSoy.Text = "";
                    else schSoy.Text = _dr.sSoy;

                    if (_dr.sMuu == null)
                        schMuu.Text = "";
                    else schMuu.Text = _dr.sMuu;

                    if (_dr.sRoad == null)
                        schRoad.Text = "";
                    else schRoad.Text = _dr.sRoad;

                    if (_dr.sTumbon == null)
                        schTumbon.Text = "";
                    else schTumbon.Text = _dr.sTumbon;

                    if (_dr.sAumpher == null)
                        schAumpher.Text = "";
                    else schAumpher.Text = _dr.sAumpher;

                    if (_dr.sPost == null)
                        schPost.Text = "";
                    else schPost.Text = _dr.sPost;

                    //############################################## new 5/05/19 ##############################################
                    if (_dr.sShortSchoolName == null)
                        shortSchoolName.Text = "";
                    else shortSchoolName.Text = _dr.sShortSchoolName;
                    if (_dr.sSchoolHistory == null)
                        txtSchoolHistory.Text = "";
                    else txtSchoolHistory.Text = _dr.sSchoolHistory;
                    if (_dr.sSchoolVision == null)
                        txtSchoolVision.Text = "";
                    else txtSchoolVision.Text = _dr.sSchoolVision;
                    if (_dr.sSchoolMission == null)
                        txtSchoolMission.Text = "";
                    else txtSchoolMission.Text = _dr.sSchoolMission;
                    //############################################## new 5/05/19 ##############################################



                    if (_dr.latitude == null)
                        schlat.Text = "";
                    else schlat.Text = _dr.latitude.ToString();

                    if (_dr.longitude == null)
                        schlon.Text = "";
                    else schlon.Text = _dr.longitude.ToString();

                    if (_dr.latitude != null)
                        lat2.Text = _dr.latitude.ToString();


                    if (_dr.longitude != null)
                        lon2.Text = _dr.longitude.ToString();

                    #region Set Provin
                    var provList = _dbMaster.provinces
                      .Select(o => new
                      {
                          Text = o.PROVINCE_NAME.Trim(),
                          Id = o.PROVINCE_ID,
                      }).OrderBy(o => o.Text).ToList();

                    Ddlprovince.DataSource = provList;
                    Ddlprovince.DataTextField = "Text";
                    Ddlprovince.DataValueField = "Id";
                    Ddlprovince.DataBind();
                    if (_dr.ProvinceID.HasValue)
                    {
                        Ddlprovince.SelectedValue = _dr.ProvinceID + "";
                    }
                    else
                    {
                        var selectedProvin = provList.Where(o => o.Text == _dr.sProvince?.Trim()).FirstOrDefault();
                        Ddlprovince.SelectedValue = selectedProvin?.Id + "";
                    }

                    var aumpherList = _dbMaster.amphurs.Where(o => o.PROVINCE_ID + "" == Ddlprovince.SelectedValue)
                       .Select(o => new
                       {
                           Text = o.AMPHUR_NAME.Trim(),
                           Id = o.AMPHUR_ID,
                       }).OrderBy(o => o.Text).ToList();
                    aumpherList.Insert(0, new { Text = "-เลือก-", Id = 0 });
                    ddlAumpher.DataSource = aumpherList;
                    ddlAumpher.DataTextField = "Text";
                    ddlAumpher.DataValueField = "Id";
                    ddlAumpher.DataBind();

                    if (_dr.AumpherID.HasValue)
                    {
                        ddlAumpher.SelectedValue = _dr.AumpherID + "";
                    }
                    else
                    {
                        var selectedAumpher = aumpherList.Where(o => o.Text == _dr.sAumpher?.Trim()).FirstOrDefault();
                        ddlAumpher.SelectedValue = selectedAumpher?.Id + "";
                    }

                    var tumponList = _dbMaster.districts.Where(o => o.AMPHUR_ID + "" == ddlAumpher.SelectedValue)
                       .Select(o => new
                       {
                           Text = o.DISTRICT_NAME.Trim(),
                           Id = o.DISTRICT_ID,
                       }).OrderBy(o => o.Text).ToList();
                    tumponList.Insert(0, new { Text = "-เลือก-", Id = 0 });
                    ddlTumbon.DataSource = tumponList;
                    ddlTumbon.DataTextField = "Text";
                    ddlTumbon.DataValueField = "Id";
                    ddlTumbon.DataBind();

                    if (_dr.TumbonID.HasValue)
                    {
                        ddlTumbon.SelectedValue = _dr.TumbonID + "";
                    }
                    else
                    {
                        var selectedTumbon = tumponList.Where(o => o.Text == _dr.sTumbon?.Trim()).FirstOrDefault();
                        ddlTumbon.SelectedValue = selectedTumbon?.Id + "";
                    }
                    #endregion

                    //string[] provinceIndex;
                    //provinceIndex = new string[78];
                    //provinceIndex[0] = "กรุงเทพมหานคร";
                    //provinceIndex[1] = "กระบี่";
                    //provinceIndex[2] = "กาญจนบุรี";
                    //provinceIndex[3] = "กาฬสินธุ์";
                    //provinceIndex[4] = "กำแพงเพชร";
                    //provinceIndex[5] = "ขอนแก่น";
                    //provinceIndex[6] = "จันทบุรี";
                    //provinceIndex[7] = "ฉะเชิงเทรา";
                    //provinceIndex[8] = "ชัยนาท";
                    //provinceIndex[9] = "ชัยภูมิ";
                    //provinceIndex[10] = "ชุมพร";
                    //provinceIndex[11] = "ชลบุรี";
                    //provinceIndex[12] = "เชียงใหม่";
                    //provinceIndex[13] = "เชียงราย";
                    //provinceIndex[14] = "ตรัง";
                    //provinceIndex[15] = "ตราด";
                    //provinceIndex[16] = "ตาก";
                    //provinceIndex[17] = "นครนายก";
                    //provinceIndex[18] = "นครปฐม";
                    //provinceIndex[19] = "นครพนม";
                    //provinceIndex[20] = "นครราชสีมา";
                    //provinceIndex[21] = "นครศรีธรรมราช";
                    //provinceIndex[22] = "นครสวรรค์";
                    //provinceIndex[23] = "นราธิวาส";
                    //provinceIndex[24] = "น่าน";
                    //provinceIndex[25] = "นนทบุรี";
                    //provinceIndex[26] = "บึงกาฬ";
                    //provinceIndex[27] = "บุรีรัมย์";
                    //provinceIndex[28] = "ประจวบคีรีขันธ์";
                    //provinceIndex[29] = "ปทุมธานี";
                    //provinceIndex[30] = "ปราจีนบุรี";
                    //provinceIndex[31] = "ปัตตานี";
                    //provinceIndex[32] = "พะเยา";
                    //provinceIndex[33] = "พระนครศรีอยุธยา";
                    //provinceIndex[34] = "พังงา";
                    //provinceIndex[35] = "พิจิตร";
                    //provinceIndex[36] = "พิษณุโลก";
                    //provinceIndex[37] = "เพชรบุรี";
                    //provinceIndex[38] = "เพชรบูรณ์";
                    //provinceIndex[39] = "แพร่";
                    //provinceIndex[40] = "พัทลุง";
                    //provinceIndex[41] = "ภูเก็ต";
                    //provinceIndex[42] = "มหาสารคาม";
                    //provinceIndex[43] = "มุกดาหาร";
                    //provinceIndex[44] = "แม่ฮ่องสอน";
                    //provinceIndex[45] = "ยโสธร";
                    //provinceIndex[46] = "ยะลา";
                    //provinceIndex[47] = "ร้อยเอ็ด";
                    //provinceIndex[48] = "ระนอง";
                    //provinceIndex[49] = "ระยอง";
                    //provinceIndex[50] = "ราชบุรี";
                    //provinceIndex[51] = "ลพบุรี";
                    //provinceIndex[52] = "ลำปาง";
                    //provinceIndex[53] = "ลำพูน";
                    //provinceIndex[54] = "เลย";
                    //provinceIndex[55] = "ศรีสะเกษ";
                    //provinceIndex[56] = "สกลนคร";
                    //provinceIndex[57] = "สงขลา";
                    //provinceIndex[58] = "สมุทรสาคร";
                    //provinceIndex[59] = "สมุทรปราการ";
                    //provinceIndex[60] = "สมุทรสงคราม";
                    //provinceIndex[61] = "สระแก้ว";
                    //provinceIndex[62] = "สระบุรี";
                    //provinceIndex[63] = "สิงห์บุรี";
                    //provinceIndex[64] = "สุโขทัย";
                    //provinceIndex[65] = "สุพรรณบุรี";
                    //provinceIndex[66] = "สุราษฎร์ธานี";
                    //provinceIndex[67] = "สุรินทร์";
                    //provinceIndex[68] = "สตูล";
                    //provinceIndex[69] = "หนองคาย";
                    //provinceIndex[70] = "หนองบัวลำภู";
                    //provinceIndex[71] = "อำนาจเจริญ";
                    //provinceIndex[72] = "อุดรธานี";
                    //provinceIndex[73] = "อุตรดิตถ์";
                    //provinceIndex[74] = "อุทัยธานี";
                    //provinceIndex[75] = "อุบลราชธานี";
                    //provinceIndex[76] = "อ่างทอง";
                    //provinceIndex[77] = "อื่นๆ";

                    string[] ownerIndex;
                    ownerIndex = new string[22];
                    ownerIndex[0] = "โรงเรียนการกุศลของวัดในพระพุทธศาสนา";
                    ownerIndex[1] = "สำนักงานคณะกรรมการส่งเสริมการศึกษาเอกชน";
                    ownerIndex[2] = "สำนักงานปลัดกระทรวงศึกษาธิการ";
                    ownerIndex[3] = "สำนักงานคณะกรรมการศึกษาขั้นพื้นฐาน";
                    ownerIndex[4] = "สำนักงานคณะกรรมการการอาชีวศึกษา";
                    ownerIndex[5] = "สพฐ.";
                    ownerIndex[6] = "สทศ.";
                    ownerIndex[7] = "สำนักงานคณะกรรมการอุดมศึกษา";
                    ownerIndex[8] = "โรงเรียนมหิดลวิทยานุสรณ์";
                    ownerIndex[9] = "กระทรวงมหาดไทย";
                    ownerIndex[10] = "กระทรวงการพัฒนาสังคมและความมั่นคงของมนุษย์";
                    ownerIndex[11] = "กรุงเทพมหานคร";
                    ownerIndex[12] = "กระทรวงสาธารณสุข";
                    ownerIndex[13] = "กระทรวงคมนาคม";
                    ownerIndex[14] = "กระทรวงกลาโหม";
                    ownerIndex[15] = "กระทรวงวัฒนธรรม";
                    ownerIndex[16] = "กระทรวงการท่องเที่ยวและกีฬา";
                    ownerIndex[17] = "สำนักงานพระพุทธศาสนาแห่งชาติ";
                    ownerIndex[18] = "องค์กรในกำกับขึ้นตรงนายกรัฐมนตรี";
                    ownerIndex[19] = "กระทรวงวิทยาศาสตร์เทคโนโลยีและสิ่งแวดล้อม";
                    ownerIndex[20] = "เอกชน";
                    ownerIndex[21] = "สำนักงานเขตพื้นที่การศึกษาประถมศึกษาสมุทรปราการ";

                    //int provinceSelect = 0;

                    //for (int xx = 0; xx < 78; xx++)
                    //{
                    //    if (_dr.sProvince == provinceIndex[xx])
                    //        provinceSelect = xx;
                    //}

                    //Ddlprovince.SelectedIndex = provinceSelect;

                    int ownerselect = 0;


                    for (int xz = 0; xz < 22; xz++)
                    {
                        if (_dr.sOwner == ownerIndex[xz])
                            ownerselect = xz;
                    }

                    schOwner.SelectedIndex = ownerselect;

                    txtTaxId.Text = _dr.TaxId;


                    using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                    {

                        List<empList> empList = new List<empList>();
                        empList emp = new empList();
                        foreach (var data1 in _db.TEmployees.Where(w => w.cDel == null && w.SchoolID == nCompany.nCompany))
                        {
                            emp = new empList();
                            emp.fullname = data1.sName + " " + data1.sLastname;
                            emp.semp = data1.sEmp;
                            empList.Add(emp);
                        }

                        var newList = empList.OrderBy(x => x.fullname).ToList();

                        ddlschoolHead.DataSource = newList;
                        ddlschoolHead.DataTextField = "fullname";
                        ddlschoolHead.DataValueField = "semp";
                        ddlschoolHead.DataBind();

                        ddlRegis.DataSource = newList;
                        ddlRegis.DataTextField = "fullname";
                        ddlRegis.DataValueField = "semp";
                        ddlRegis.DataBind();

                        ddlAcademic.DataSource = newList;
                        ddlAcademic.DataTextField = "fullname";
                        ddlAcademic.DataValueField = "semp";
                        ddlAcademic.DataBind();

                        ddlSubAcademic.DataSource = newList;
                        ddlSubAcademic.DataTextField = "fullname";
                        ddlSubAcademic.DataValueField = "semp";
                        ddlSubAcademic.DataBind();

                        ddlAcc.DataSource = newList;
                        ddlAcc.DataTextField = "fullname";
                        ddlAcc.DataValueField = "semp";
                        ddlAcc.DataBind();

                        ddlStd.DataSource = newList;
                        ddlStd.DataTextField = "fullname";
                        ddlStd.DataValueField = "semp";
                        ddlStd.DataBind();

                        ddladmin.DataSource = newList;
                        ddladmin.DataTextField = "fullname";
                        ddladmin.DataValueField = "semp";
                        ddladmin.DataBind();

                        dropdownPersonnel.DataSource = newList;
                        dropdownPersonnel.DataTextField = "fullname";
                        dropdownPersonnel.DataValueField = "semp";
                        dropdownPersonnel.DataBind();

                        dropdownGM.DataSource = newList;
                        dropdownGM.DataTextField = "fullname";
                        dropdownGM.DataValueField = "semp";
                        dropdownGM.DataBind();



                        var getEmp = _dbMaster.TUsers.Where(w => w.nCompany == nCompany.nCompany)
                                                     .Where(w => w.cType == "1")
                                                     .Where(w => w.cDel == null);

                        SignSchoolHeadImg.Src = "../images/select-img-bg.jpg";
                        if (_dr.nSchoolHeadid != null)
                        {
                            var schoolHead = getEmp.Where(ee => ee.nSystemID == _dr.nSchoolHeadid).FirstOrDefault();
                            if (schoolHead != null && schoolHead.UserSignature != null)
                            {
                                SignSchoolHeadImg.Src = schoolHead.UserSignature;
                                SignSchoolHeadBtn.Style.Add("display", "block");
                            }
                        }

                        SignRegistraDirectorImg.Src = "../images/select-img-bg.jpg";
                        if (_dr.nRegistraDirectorid != null)
                        {
                            var RegistraDirector = getEmp.Where(ee => ee.nSystemID == _dr.nRegistraDirectorid).FirstOrDefault();
                            if (RegistraDirector != null && RegistraDirector.UserSignature != null)
                            {
                                SignRegistraDirectorImg.Src = RegistraDirector.UserSignature;
                                SignRegistraDirectorBtn.Style.Add("display", "block");
                            }
                        }

                        //---------------------------------------------------------------------------------------------------------------------

                        SignAcademicImg.Src = "../images/select-img-bg.jpg";
                        if (_dr.nAcademicDirectorid != null)
                        {
                            var Academic = getEmp.Where(ee => ee.nSystemID == _dr.nAcademicDirectorid).FirstOrDefault();
                            if (Academic != null && Academic.UserSignature != null)
                            {
                                SignAcademicImg.Src = Academic.UserSignature;
                                SignAcademicBtn.Style.Add("display", "block");
                            }
                        }
                        //---------------------------------------------------------------------------------------------------------------------

                        SignSubAcademicImg.Src = "../images/select-img-bg.jpg";
                        if (_dr.nAcademicSubDirectorid != null)
                        {
                            var SubAcademic = getEmp.Where(ee => ee.nSystemID == _dr.nAcademicSubDirectorid).FirstOrDefault();
                            if (SubAcademic != null && SubAcademic.UserSignature != null)
                            {
                                SignSubAcademicImg.Src = SubAcademic.UserSignature;
                                SignSubAcademicBtn.Style.Add("display", "block");
                            }
                        }

                        SignAccountingDirectorImg.Src = "../images/select-img-bg.jpg";
                        if (_dr.nAccountingDirectorid != null)
                        {
                            var AccountingDirector = getEmp.Where(ee => ee.nSystemID == _dr.nAccountingDirectorid).FirstOrDefault();
                            if (AccountingDirector != null && AccountingDirector.UserSignature != null)
                            {
                                SignAccountingDirectorImg.Src = AccountingDirector.UserSignature;
                                SignAccountingDirectorBtn.Style.Add("display", "block");
                            }
                        }

                        SignPersonnelImg.Src = "../images/select-img-bg.jpg";
                        if (_dr.nPersonnel != null)
                        {
                            var Personnel = getEmp.Where(ee => ee.nSystemID == _dr.nPersonnel).FirstOrDefault();
                            if (Personnel != null && Personnel.UserSignature != null)
                            {
                                SignPersonnelImg.Src = Personnel.UserSignature;
                                SignPersonnelBtn.Style.Add("display", "block");
                            }
                        }

                        SignStudentDevelopmentDirectorImg.Src = "../images/select-img-bg.jpg";
                        if (_dr.nStudentDevelopmentDirectorid != null)
                        {
                            var StudentDevelopmentDirector = getEmp.Where(ee => ee.nSystemID == _dr.nStudentDevelopmentDirectorid).FirstOrDefault();
                            if (StudentDevelopmentDirector != null && StudentDevelopmentDirector.UserSignature != null)
                            {
                                SignStudentDevelopmentDirectorImg.Src = StudentDevelopmentDirector.UserSignature;
                                SignStudentDevelopmentDirectorBtn.Style.Add("display", "block");
                            }
                        }

                        SignGMImg.Src = "../images/select-img-bg.jpg";
                        if (_dr.nGM != null)
                        {
                            var gm = getEmp.Where(ee => ee.nSystemID == _dr.nGM).FirstOrDefault();
                            if (gm != null && gm.UserSignature != null)
                            {
                                SignGMImg.Src = gm.UserSignature;
                                SignGMBtn.Style.Add("display", "block");
                            }
                        }

                        SignAdminImg.Src = "../images/select-img-bg.jpg";
                        if (_dr.nWebAdminid != null)
                        {
                            var WebAdmin = getEmp.Where(ee => ee.nSystemID == _dr.nWebAdminid).FirstOrDefault();
                            if (WebAdmin != null && WebAdmin.UserSignature != null)
                            {
                                SignAdminImg.Src = WebAdmin.UserSignature;
                                SignAdminBtn.Style.Add("display", "block");
                            }
                        }

                        // Load School Area
                        List<ListItem> schoolAreas = _dbMaster.TSchoolAreas.Select(s => new ListItem { Value = s.Code, Text = s.Area }).ToList();
                        schArea.DataTextField = "Text";
                        schArea.DataValueField = "Value";
                        schArea.DataSource = schoolAreas;
                        schArea.DataBind();

                        if (!string.IsNullOrEmpty(_dr.SchoolAreaCode))
                        {
                            schArea.Items.FindByValue(_dr.SchoolAreaCode).Selected = true;
                        }

                    }

                }
            }
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string link = "";
                if (FileUpload1.HasFile)
                {
                    var newname = "";
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

                    var path = "";
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    HttpPostedFile postedFile = FileUpload1.PostedFile;
                    var name = postedFile.FileName.Split('.');
                    //เช็ดไอดีนักเรียนตามรูปภาพ
                    if (postedFile.ContentLength > 0)
                    {
                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        string fileName2 = FileUpload1.PostedFile.FileName;

                        string month = DateTime.Now.Month.ToString();
                        string year = DateTime.Now.Year.ToString();
                        string date = DateTime.Now.DayOfYear.ToString();
                        string hour = DateTime.Now.Hour.ToString();
                        string min = DateTime.Now.Minute.ToString();
                        string sec = DateTime.Now.Second.ToString();
                        Random rnd = new Random();
                        string rng = rnd.Next(10000000, 99999999).ToString();

                        newname = year + month + date + hour + min + sec + rng + ".png";
                        link = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/" + newname;
                        //resize
                        System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
                        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

                        var imageHeight = img.Height;
                        var imageWidth = img.Width;

                        path = Path.Combine(Server.MapPath("~/images/" + newname));
                        System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);

                        int newwidthimg = 0;
                        int newHeight = 0;
                        int newWidth = 0;
                        Bitmap resizeBitmap = null;

                        if (imageHeight < imageWidth)
                        {
                            newWidth = 400;
                            float AspectRatio = (float)image.Size.Height / (float)image.Size.Width;
                            newwidthimg = Convert.ToInt32(newWidth / AspectRatio);
                            resizeBitmap = new Bitmap(image, newwidthimg, newWidth);
                        }
                        else
                        {
                            newWidth = 400;
                            float AspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                            newwidthimg = Convert.ToInt32(newWidth / AspectRatio);
                            resizeBitmap = new Bitmap(image, newWidth, newwidthimg);
                        }

                        Graphics thumbnailGraph = Graphics.FromImage(resizeBitmap);
                        thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imageRectangle = new Rectangle(0, 0, newwidthimg, newHeight);
                        thumbnailGraph.DrawImage(image, imageRectangle);
                        resizeBitmap.Save(path, ImageFormat.Png);
                        thumbnailGraph.Dispose();
                        resizeBitmap.Dispose();
                        image.Dispose();

                        //image = ResizeImage(image, 1800, 900, 500);
                        //resizeBitmap = new Bitmap(image);
                        //resizeBitmap.Save(path, ImageFormat.Png);
                        //resizeBitmap.Dispose();
                        //image.Dispose();
                    }

                    try
                    {

                        ObsClient client;
                        string bucketName = "userstorage";
                        string endpoint = "obs.ap-southeast-2.myhuaweicloud.com";
                        string AK = "UVIKYLYS2BECK7RGLFVN";
                        string SK = "LCQcPRii1eSYVHInqMvJU0TklY5lH6VrUeikwhdK";

                        ObsConfig config = new ObsConfig();
                        config.Endpoint = endpoint;
                        client = new ObsClient(AK, SK, config);

                        var request = new PutObjectRequest()
                        {
                            BucketName = bucketName,
                            ObjectKey = "userprofile/" + newname,
                            FilePath = path,
                            //ContentType = contentType,
                            //CannedAcl = CannedAclEnum.PublicRead
                        };


                        PutObjectResponse response = client.PutObject(request);


                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/images/" + newname)))
                        {
                            System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/images/" + newname));
                        }

                        link = response.ObjectUrl;
                    }
                    catch (Exception ex)
                    {
                        link = "";
                    }



                }

                string link4 = "";
                if (FileUpload4.HasFile)
                {
                    var newname = "";
                    string fileName = Path.GetFileName(FileUpload4.PostedFile.FileName);

                    var path = "";
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    HttpPostedFile postedFile = FileUpload4.PostedFile;
                    var name = postedFile.FileName.Split('.');
                    //เช็ดไอดีนักเรียนตามรูปภาพ
                    if (postedFile.ContentLength > 0)
                    {
                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        string fileName2 = FileUpload4.PostedFile.FileName;

                        string month = DateTime.Now.Month.ToString();
                        string year = DateTime.Now.Year.ToString();
                        string date = DateTime.Now.DayOfYear.ToString();
                        string hour = DateTime.Now.Hour.ToString();
                        string min = DateTime.Now.Minute.ToString();
                        string sec = DateTime.Now.Second.ToString();
                        Random rnd = new Random();
                        string rng = rnd.Next(10000000, 99999999).ToString();

                        newname = year + month + date + hour + min + sec + rng + ".png";
                        link4 = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/" + newname;
                        //resize
                        path = Path.Combine(Server.MapPath("~/images/" + newname));
                        System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);
                        int newwidthimg = 600;
                        float AspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                        int newHeight = 300;
                        Bitmap resizeBitmap = new Bitmap(image, newwidthimg, newHeight);
                        Graphics thumbnailGraph = Graphics.FromImage(resizeBitmap);
                        thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imageRectangle = new Rectangle(0, 0, newwidthimg, newHeight);
                        thumbnailGraph.DrawImage(image, imageRectangle);
                        resizeBitmap.Save(path, ImageFormat.Png);
                        thumbnailGraph.Dispose();
                        resizeBitmap.Dispose();
                        image.Dispose();
                    }


                    try
                    {

                        ObsClient client;
                        string bucketName = "userstorage";
                        string endpoint = "obs.ap-southeast-2.myhuaweicloud.com";
                        string AK = "UVIKYLYS2BECK7RGLFVN";
                        string SK = "LCQcPRii1eSYVHInqMvJU0TklY5lH6VrUeikwhdK";

                        ObsConfig config = new ObsConfig();
                        config.Endpoint = endpoint;
                        client = new ObsClient(AK, SK, config);

                        var request = new PutObjectRequest()
                        {
                            BucketName = bucketName,
                            ObjectKey = "userprofile/" + newname,
                            FilePath = path,
                            //ContentType = contentType,
                            //CannedAcl = CannedAclEnum.PublicRead
                        };


                        PutObjectResponse response = client.PutObject(request);


                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/images/" + newname)))
                        {
                            System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/images/" + newname));
                        }

                        link4 = response.ObjectUrl;
                    }
                    catch (Exception ex)
                    {
                        link4 = "";
                    }
                }

                string lat = SendA.Value;
                string lon = SendB.Value;

                #region Edit Data Master
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {

                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();

                    var company = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities);
                    var _data = company.FirstOrDefault();
                    //var _data = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                    _data.sCompany = schNameTH.Text;
                    _data.sCode = schCode.Text;
                    _data.sNameEN = schNameEN.Text;
                    _data.sOwner = schOwner.SelectedValue;
                    _data.SchoolAreaCode = schArea.SelectedValue;
                    _data.sPhoneOne = schPhoneOne.Text;
                    _data.sPhoneTwo = schPhoneTwo.Text;
                    _data.sMobilePhone = schMobilePhone.Text;
                    _data.sFax = schFax.Text;
                    _data.sEmailOne = schEmailOne.Text;
                    _data.sEmailTwo = schEmailTwo.Text;
                    _data.sWebsite = schWebsite.Text;
                    _data.sHomeNumber = schHomeNumber.Text;
                    _data.sSoy = schSoy.Text;
                    _data.sMuu = schMuu.Text;
                    _data.sRoad = schRoad.Text;
                    _data.TumbonID = Request.Form[ddlTumbon.UniqueID].ToNumber<short>();
                    _data.sTumbon = schTumbon.Text;
                    _data.AumpherID = Request.Form[ddlAumpher.UniqueID].ToNumber<short>();
                    _data.sAumpher = schAumpher.Text;
                    _data.ProvinceID = Ddlprovince.SelectedValue.ToNumber<short>();
                    _data.sProvince = Ddlprovince.SelectedItem.Text;
                    _data.sPost = schPost.Text;
                    _data.TaxId = txtTaxId.Text;

                    if (editHead.Text == "")
                        _data.nSchoolHeadid = null;
                    else _data.nSchoolHeadid = int.Parse(editHead.Text);

                    if (editRegis.Text == "")
                        _data.nRegistraDirectorid = null;
                    else _data.nRegistraDirectorid = int.Parse(editRegis.Text);

                    if (editAca.Text == "")
                        _data.nAcademicDirectorid = null;
                    else _data.nAcademicDirectorid = int.Parse(editAca.Text);

                    if (editSub.Text == "")
                        _data.nAcademicSubDirectorid = null;
                    else _data.nAcademicSubDirectorid = int.Parse(editSub.Text);

                    if (editAcc.Text == "")
                        _data.nAccountingDirectorid = null;
                    else _data.nAccountingDirectorid = int.Parse(editAcc.Text);

                    if (editStd.Text == "")
                        _data.nStudentDevelopmentDirectorid = null;
                    else _data.nStudentDevelopmentDirectorid = int.Parse(editStd.Text);

                    if (editAdmin.Text == "")
                        _data.nWebAdminid = null;
                    else _data.nWebAdminid = int.Parse(editAdmin.Text);

                    //############################################## new 2/05/19 ##############################################
                    if (editpersonnel.Text == "")
                        _data.nPersonnel = null;
                    else _data.nPersonnel = int.Parse(editpersonnel.Text);

                    if (editGM.Text == "")
                        _data.nGM = null;
                    else _data.nGM = int.Parse(editGM.Text);

                    if (shortSchoolName.Text == "")
                        _data.sShortSchoolName = null;
                    else _data.sShortSchoolName = shortSchoolName.Text;

                    if (txtSchoolHistory.Text == "")
                        _data.sSchoolHistory = null;
                    else _data.sSchoolHistory = txtSchoolHistory.Text;

                    if (txtSchoolVision.Text == "")
                        _data.sSchoolVision = null;
                    else _data.sSchoolVision = txtSchoolVision.Text;

                    if (txtSchoolMission.Text == "")
                        _data.sSchoolMission = null;
                    else _data.sSchoolMission = txtSchoolMission.Text;
                    //############################################## end new 2/05/19 ##############################################


                    if (FileUpload1.HasFile)
                    {
                        _data.sImage = link;
                    }

                    if (FileUpload4.HasFile)
                    {
                        _data.schoolCoverPicture = link4;
                    }
                    if (lat != "")
                    {
                        float lat2 = (float)Convert.ToDouble(lat);
                        _data.latitude = lat2;
                    }
                    if (lon != "")
                    {
                        float lon2 = (float)Convert.ToDouble(lon);
                        _data.longitude = lon2;
                    }

                    var emp = from com in company
                              join u in _dbMaster.TUsers on com.nCompany equals u.nCompany
                              where u.cType == "1" & u.cDel == null
                              select u;

                    ///ผู้อำนวยการโรงเรียน
                    if (editHead.Text != "" && SignSchoolHead.HasFile)
                    {
                        var idInt = int.Parse(editHead.Text);
                        var schoolHead = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                        string signLink = UploadFileSignature(SignSchoolHead, _data.nCompany, idInt);

                        if (signLink == "ValidationFile")
                        {
                            Response.Write("<script>alert('กรุณาใส่ไฟล์รูปภาพ .jpg / .jpeg /.png ');</script>");
                            return;
                        }

                        schoolHead.UserSignature = signLink;
                    }
                    else
                    {
                        if (editHead.Text != "" && SignSchoolHeadStatus.Value == "DEL")
                        {
                            var idInt = int.Parse(editHead.Text);
                            var schoolHead = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                            schoolHead.UserSignature = null;
                        }
                    }

                    ///หัวหน้านายทะเบียน/หัวหน้าฝ่ายวัดผลและประเมินผล
                    if (editRegis.Text != "" && SignRegistraDirector.HasFile)
                    {
                        var idInt = int.Parse(editRegis.Text);
                        var RegistraDirector = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                        string signLink = UploadFileSignature(SignRegistraDirector, _data.nCompany, idInt);

                        if (signLink == "ValidationFile")
                        {
                            Response.Write("<script>alert('กรุณาใส่ไฟล์รูปภาพ .jpg / .jpeg /.png ');</script>");
                            return;
                        }

                        RegistraDirector.UserSignature = signLink;
                    }
                    else
                    {
                        if (editRegis.Text != "" && SignRegistraDirectorStatus.Value == "DEL")
                        {
                            var idInt = int.Parse(editRegis.Text);
                            var RegistraDirector = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                            RegistraDirector.UserSignature = null;
                        }
                    }

                    ///ผู้อำนวยการฝ่ายฝ่ายวิชาการ
                    if (editAca.Text != "" && SignAcademic.HasFile)
                    {
                        var idInt = int.Parse(editAca.Text);
                        var AcademicDirector = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                        string signLink = UploadFileSignature(SignAcademic, _data.nCompany, idInt);

                        if (signLink == "ValidationFile")
                        {
                            Response.Write("<script>alert('กรุณาใส่ไฟล์รูปภาพ .jpg / .jpeg /.png ');</script>");
                            return;
                        }

                        AcademicDirector.UserSignature = signLink;
                    }
                    else
                    {
                        if (editAca.Text != "" && SignAcademicStatus.Value == "DEL")
                        {
                            var idInt = int.Parse(editAca.Text);
                            var AcademicDirector = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                            AcademicDirector.UserSignature = null;
                        }
                    }

                    ///รองผู้อำนวยการฝ่ายวิชาการ / ผู้ช่วยผู้อำนวยการฝ่ายวิชาการ
                    if (editSub.Text != "" && SignSubAcademic.HasFile)
                    {
                        var idInt = int.Parse(editSub.Text);
                        var AcademicSubDirector = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                        string signLink = UploadFileSignature(SignSubAcademic, _data.nCompany, idInt);

                        if (signLink == "ValidationFile")
                        {
                            Response.Write("<script>alert('กรุณาใส่ไฟล์รูปภาพ .jpg / .jpeg /.png ');</script>");
                            return;
                        }

                        AcademicSubDirector.UserSignature = signLink;
                    }
                    else
                    {
                        if (editSub.Text != "" && SignSubAcademicStatus.Value == "DEL")
                        {
                            var idInt = int.Parse(editSub.Text);
                            var AcademicSubDirector = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                            AcademicSubDirector.UserSignature = null;
                        }
                    }

                    ///รองผู้อำนวยการฝ่ายการเงิน / หัวหน้าฝ่ายการเงิน
                    if (editAcc.Text != "" && SignAccountingDirector.HasFile)
                    {
                        var idInt = int.Parse(editAcc.Text);
                        var AccountingDirector = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                        string signLink = UploadFileSignature(SignAccountingDirector, _data.nCompany, idInt);

                        if (signLink == "ValidationFile")
                        {
                            Response.Write("<script>alert('กรุณาใส่ไฟล์รูปภาพ .jpg / .jpeg /.png ');</script>");
                            return;
                        }

                        AccountingDirector.UserSignature = signLink;
                    }
                    else
                    {
                        if (editAcc.Text != "" && SignAccountingDirectorStatus.Value == "DEL")
                        {
                            var idInt = int.Parse(editAcc.Text);
                            var obj = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                            obj.UserSignature = null;
                        }
                    }

                    ///รองผู้อำนวยการฝ่ายบุคคล / หัวหน้าฝ่ายบุคคล
                    if (editpersonnel.Text != "" && SignPersonnel.HasFile)
                    {
                        var idInt = int.Parse(editpersonnel.Text);
                        var Personnel = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                        string signLink = UploadFileSignature(SignPersonnel, _data.nCompany, idInt);

                        if (signLink == "ValidationFile")
                        {
                            Response.Write("<script>alert('กรุณาใส่ไฟล์รูปภาพ .jpg / .jpeg /.png ');</script>");
                            return;
                        }

                        Personnel.UserSignature = signLink;
                    }
                    else
                    {
                        if (editpersonnel.Text != "" && SignPersonnelStatus.Value == "DEL")
                        {
                            var idInt = int.Parse(editpersonnel.Text);
                            var obj = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                            obj.UserSignature = null;
                        }
                    }

                    ///รองผู้อำนวยการฝ่ายพัฒนานักเรียน / หัวหน้าฝ่ายปกครอง
                    if (editStd.Text != "" && SignStudentDevelopmentDirector.HasFile)
                    {
                        var idInt = int.Parse(editStd.Text);
                        var StudentDevelopmentDirector = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                        string signLink = UploadFileSignature(SignStudentDevelopmentDirector, _data.nCompany, idInt);

                        if (signLink == "ValidationFile")
                        {
                            Response.Write("<script>alert('กรุณาใส่ไฟล์รูปภาพ .jpg / .jpeg /.png ');</script>");
                            return;
                        }

                        StudentDevelopmentDirector.UserSignature = signLink;
                    }
                    else
                    {
                        if (editStd.Text != "" && SignStudentDevelopmentDirectorStatus.Value == "DEL")
                        {
                            var idInt = int.Parse(editStd.Text);
                            var obj = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                            obj.UserSignature = null;
                        }
                    }


                    ///รองผู้อำนวยการฝ่ายบริหารทั่วไป / หัวหน้าฝ่ายบริหารทั่วไป
                    if (editGM.Text != "" && SignGM.HasFile)
                    {
                        var idInt = int.Parse(editGM.Text);
                        var GM = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                        string signLink = UploadFileSignature(SignGM, _data.nCompany, idInt);

                        if (signLink == "ValidationFile")
                        {
                            Response.Write("<script>alert('กรุณาใส่ไฟล์รูปภาพ .jpg / .jpeg /.png ');</script>");
                            return;
                        }

                        GM.UserSignature = signLink;
                    }
                    else
                    {
                        if (editGM.Text != "" && SignGMStatus.Value == "DEL")
                        {
                            var idInt = int.Parse(editGM.Text);
                            var obj = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                            obj.UserSignature = null;
                        }
                    }

                    //ผู้ดูแลระบบ
                    if (editAdmin.Text != "" && SignAdmin.HasFile)
                    {
                        var idInt = int.Parse(editAdmin.Text);
                        var WebAdmin = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                        string signLink = UploadFileSignature(SignAdmin, _data.nCompany, idInt);

                        if (signLink == "ValidationFile")
                        {
                            Response.Write("<script>alert('กรุณาใส่ไฟล์รูปภาพ .jpg / .jpeg /.png ');</script>");
                            return;
                        }

                        WebAdmin.UserSignature = signLink;
                    }
                    else
                    {
                        if (editAdmin.Text != "" && SignAdminStatus.Value == "DEL")
                        {
                            var idInt = int.Parse(editAdmin.Text);
                            var obj = emp.Where(ee => ee.nSystemID == idInt).FirstOrDefault();
                            obj.UserSignature = null;
                        }
                    }

                    _dbMaster.SaveChanges();

                    #endregion

                    Response.Redirect("schoolprofile.aspx");
                }


            }
            catch (Exception ex)
            {
                int ii = 1;
            }
        }



        private string UploadFileSignature(FileUpload file, int schoolid, int userId)
        {
            var link = "";
            if (file.HasFile)
            {
                var newname = "";
                string fileName = Path.GetFileName(file.PostedFile.FileName);

                var path = "";
                Dictionary<string, object> dict = new Dictionary<string, object>();
                HttpPostedFile postedFile = file.PostedFile;
                var name = postedFile.FileName.Split('.');
                //เช็ดไอดีนักเรียนตามรูปภาพ
                if (postedFile.ContentLength > 0)
                {
                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
                    var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));

                    var validation = AllowedFileExtensions.ToList().Contains(ext);
                    if (validation == false) return "ValidationFile";

                    string fileName2 = file.PostedFile.FileName;

                    string month = DateTime.Now.Month.ToString();
                    string year = DateTime.Now.Year.ToString();
                    string date = DateTime.Now.DayOfYear.ToString();
                    string hour = DateTime.Now.Hour.ToString();
                    string min = DateTime.Now.Minute.ToString();
                    string sec = DateTime.Now.Second.ToString();
                    Random rnd = new Random();
                    string rng = rnd.Next(10000000, 99999999).ToString();

                    newname = userId + '_' + year + month + date + hour + min + sec + rng + ".png";
                    //signSchoolHead = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/" + newname;

                    link = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_schoolprofile/" + schoolid + "/" + newname;

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/images/sb_schoolprofile/" + schoolid)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/images/sb_schoolprofile/" + schoolid));
                    }

                    //resize
                    path = Path.Combine(Server.MapPath("~/images/sb_schoolprofile/" + schoolid + "/" + newname));
                    System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);
                    int newwidthimg = 300;
                    float AspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                    int newHeight = 100;
                    Bitmap resizeBitmap = new Bitmap(image, newwidthimg, newHeight);
                    Graphics thumbnailGraph = Graphics.FromImage(resizeBitmap);
                    thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    var imageRectangle = new Rectangle(0, 0, newwidthimg, newHeight);
                    thumbnailGraph.DrawImage(image, imageRectangle);
                    resizeBitmap.Save(path, ImageFormat.Png);
                    thumbnailGraph.Dispose();
                    resizeBitmap.Dispose();
                    image.Dispose();
                }

                try
                {

                    ObsClient client;
                    string bucketName = "userstorage";
                    string endpoint = "obs.ap-southeast-2.myhuaweicloud.com";
                    string AK = "UVIKYLYS2BECK7RGLFVN";
                    string SK = "LCQcPRii1eSYVHInqMvJU0TklY5lH6VrUeikwhdK";

                    ObsConfig config = new ObsConfig();
                    config.Endpoint = endpoint;
                    client = new ObsClient(AK, SK, config);

                    var request = new PutObjectRequest()
                    {
                        BucketName = bucketName,
                        ObjectKey = $"sb_schoolprofile/{schoolid}/" + newname,
                        FilePath = path,
                    };

                    PutObjectResponse response = client.PutObject(request);

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/images/sb_schoolprofile/" + schoolid + "/" + newname)))
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/images/sb_schoolprofile/" + schoolid + "/" + newname));
                    }

                    link = response.ObjectUrl;
                }
                catch (Exception ex)
                {
                    link = "";
                }

            }
            return link;
        }


        void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("schoolprofile.aspx");
        }
        // Add the following function to the schoolprofile_edit class

        //ฟังชั่นปรับภาพให้ได้ความยาว 1800 ความกว้าง 900 px ขนาดไม่เกิน 500 kb   
        private System.Drawing.Image ResizeImage(System.Drawing.Image image, int maxWidth, int maxHeight, int maxSize)
        {
            int width = image.Width;
            int height = image.Height;

            // Calculate the new dimensions while maintaining the aspect ratio
            if (width > maxWidth || height > maxHeight)
            {
                double aspectRatio = (double)width / height;
                if (aspectRatio > 1)
                {
                    width = maxWidth;
                    height = (int)(width / aspectRatio);
                }
                else
                {
                    height = maxHeight;
                    width = (int)(height * aspectRatio);
                }
            }

            // Resize the image
            System.Drawing.Image resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                graphics.DrawImage(image, 0, 0, width, height);
            }

            // Compress the image if necessary
            if (maxSize > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

                    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo jpegCodec = codecs.FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

                    resizedImage.Save(memoryStream, jpegCodec, encoderParameters);

                    while (memoryStream.Length > maxSize)
                    {
                        int quality = encoderParameters.Param[0].NumberOfValues - 10;
                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                        memoryStream.SetLength(0);
                        resizedImage.Save(memoryStream, jpegCodec, encoderParameters);
                    }

                    resizedImage.Dispose();
                    resizedImage = System.Drawing.Image.FromStream(memoryStream);
                }
            }

            return resizedImage;
        }

        class empList
        {
            public int semp { get; set; }
            public string fullname { get; set; }
        }
    }
}