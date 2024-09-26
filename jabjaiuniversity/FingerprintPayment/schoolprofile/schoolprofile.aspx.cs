using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using MasterEntity;
using JabjaiEntity.DB;
using JabjaiMainClass;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace FingerprintPayment
{
    public partial class schoolprofile : BehaviorGateway
    {
        protected string _lat;
        protected string _lng;
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            _lat = "13.7563309";
            _lng = "100.50176510000006";

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                int sEmpID = int.Parse(HttpContext.Current.Session["sEmpID"].ToString());

                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                int schoolid = userData.CompanyID;
                // var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                TCompany nCompany = _dbMaster.TCompanies.Where(w => w.nCompany == schoolid).FirstOrDefault();

                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(nCompany, ConnectionDB.Read)))
                {


                    foreach (var _data in _dbMaster.TCompanies.Where(w => w.nCompany == schoolid))
                        btnEdit.Click += new EventHandler(btnEdit_Click);
                    foreach (var _dr in _dbMaster.TCompanies.Where(w => w.nCompany == schoolid))
                    {
                        schNameTH.Text = _dr.sCompany;


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

                        if (nCompany.nSchoolHeadid.HasValue)
                        {
                            var data1 = _db.TEmployees.Where(w => w.sEmp == nCompany.nSchoolHeadid).FirstOrDefault();

                            schoolHeadPicture.Src = ImageURL(data1);

                            if (data1 != null)
                            {
                                schoolHeadName.Text = data1.sName;
                                schoolHeadLastname.Text = data1.sLastname;
                                schoolHeadPhone.Text = data1.sPhone;
                            }
                            else
                            {
                                schoolHeadName.Text = _dr.SchoolHeadName;
                                schoolHeadLastname.Text = _dr.SchoolHeadLastname;
                            }
                        }
                        else
                        {
                            schoolHeadPicture.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                            schoolHeadName.Text = string.IsNullOrEmpty(_dr.SchoolHeadName) ? string.Empty : _dr.SchoolHeadName;
                            schoolHeadLastname.Text = string.IsNullOrEmpty(_dr.SchoolHeadLastname) ? string.Empty : _dr.SchoolHeadLastname;
                        }

                        if (nCompany.nRegistraDirectorid != null)
                        {
                            var data1 = _db.TEmployees.Where(w => w.SchoolID == schoolid && w.sEmp == nCompany.nRegistraDirectorid).FirstOrDefault();
                            Img1.Src = ImageURL(data1);
                 
                            //if (data1.sPicture != null && data1.sPicture != "")
                            //    Img1.Src = data1.sPicture + "?d=" + DateTime.Now.ToString("MMddyyyyHHmmss");
                            //else Img1.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                            regis1.Text = data1.sName;
                            regis2.Text = data1.sLastname;
                            regis3.Text = data1.sPhone;
                        }
                        else Img1.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";




                        if (nCompany.nAcademicDirectorid != null)
                        {
                            var data1 = _db.TEmployees.Where(w => w.sEmp == nCompany.nAcademicDirectorid).FirstOrDefault();
                            Img2.Src = ImageURL(data1);
                      
                            //if (data1.sPicture != null && data1.sPicture != "")
                            //    Img2.Src = data1.sPicture;
                            //else Img2.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                            aca1.Text = data1.sName;
                            aca2.Text = data1.sLastname;
                            aca3.Text = data1.sPhone;
                        }
                        else Img2.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";





                        if (nCompany.nAcademicSubDirectorid != null)
                        {
                            var data1 = _db.TEmployees.Where(w => w.SchoolID == schoolid && w.sEmp == nCompany.nAcademicSubDirectorid).FirstOrDefault();
                            Img3.Src = ImageURL(data1);
                       
                            //if (data1.sPicture != null && data1.sPicture != "")
                            //    Img3.Src = data1.sPicture + "?d=" + DateTime.Now.ToString("MMddyyyyHHmmss");
                            //else Img3.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                            sub1.Text = data1.sName;
                            sub2.Text = data1.sLastname;
                            sub3.Text = data1.sPhone;
                        }
                        else Img3.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                        if (nCompany.nAccountingDirectorid != null)
                        {
                            var data1 = _db.TEmployees.Where(w => w.SchoolID == schoolid && w.sEmp == nCompany.nAccountingDirectorid).FirstOrDefault();
                            Img4.Src = ImageURL(data1);
                      
                            //if (data1.sPicture != null && data1.sPicture != "")
                            //    Img4.Src = data1.sPicture + "?d=" + DateTime.Now.ToString("MMddyyyyHHmmss");
                            //else Img4.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                            acc1.Text = data1.sName;
                            acc2.Text = data1.sLastname;
                            acc3.Text = data1.sPhone;
                        }
                        else Img4.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
                        //############################################## new 2/05/19 ##############################################
                        if (nCompany.nPersonnel != null)
                        {
                            var data1 = _db.TEmployees.Where(w => w.SchoolID == schoolid && w.sEmp == nCompany.nPersonnel).FirstOrDefault();
                            imgPersonnel.Src = ImageURL(data1);

                            //if (data1.sPicture != null && data1.sPicture != "")
                            //    imgPersonnel.Src = data1.sPicture + "?d=" + DateTime.Now.ToString("MMddyyyyHHmmss");
                            //else imgPersonnel.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                            namePersonnel.Text = data1.sName;
                            lastnamePersonnel.Text = data1.sLastname;
                            telPersonnel.Text = data1.sPhone;
                        }
                        else imgPersonnel.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
                        //############################################## end new 2/05/19 ##############################################

                        if (nCompany.nStudentDevelopmentDirectorid != null)
                        {
                            var data1 = _db.TEmployees.Where(w => w.SchoolID == schoolid && w.sEmp == nCompany.nStudentDevelopmentDirectorid).FirstOrDefault();
                            Img5.Src = ImageURL(data1);

                            //if (data1.sPicture != null && data1.sPicture != "")
                            //    Img5.Src = data1.sPicture + "?d=" + DateTime.Now.ToString("MMddyyyyHHmmss");
                            //else Img5.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                            std1.Text = data1.sName;
                            std2.Text = data1.sLastname;
                            std3.Text = data1.sPhone;
                        }
                        else Img5.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                        //############################################## new 2/05/19 ##############################################
                        if (nCompany.nGM != null)
                        {
                            var data1 = _db.TEmployees.Where(w => w.SchoolID == schoolid && w.sEmp == nCompany.nGM).FirstOrDefault();
                            imgGM.Src = ImageURL(data1);
                            //if (data1.sPicture != null && data1.sPicture != "")
                            //    imgGM.Src = data1.sPicture + "?d=" + DateTime.Now.ToString("MMddyyyyHHmmss");
                            //else imgGM.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                            nameGM.Text = data1.sName;
                            lastnameGM.Text = data1.sLastname;
                            telGM.Text = data1.sPhone;
                        }
                        else imgGM.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
                        //############################################## end new 2/05/19 ##############################################

                        if (nCompany.nWebAdminid != null)
                        {
                            var data1 = _db.TEmployees.Where(w => w.SchoolID == schoolid && w.sEmp == nCompany.nWebAdminid).FirstOrDefault();
                            Img6.Src = ImageURL(data1);
                            //if (data1.sPicture != null && data1.sPicture != "")
                            //    Img6.Src = data1.sPicture + "?d=" + DateTime.Now.ToString("MMddyyyyHHmmss");
                            //else Img6.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                            admin1.Text = data1.sName;
                            admin2.Text = data1.sLastname;
                            admin3.Text = data1.sPhone;
                        }
                        else Img6.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";


                        //if (_dr.serverAdminPicture == null || _dr.serverAdminPicture == "")
                        //     serverAdminPicture.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
                        //else serverAdminPicture.Src = _dr.serverAdminPicture;

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



                        if (_dr.latitude == null)
                            schlat.Text = "";
                        else schlat.Text = _dr.latitude.ToString();

                        if (_dr.longitude == null)
                            schlon.Text = "";
                        else schlon.Text = _dr.longitude.ToString();

                        if (_dr.sProvince == null)
                            schProvince.Text = "";
                        else schProvince.Text = _dr.sProvince;

                        if (_dr.sOwner == null)
                            schOwner.Text = "";
                        else schOwner.Text = _dr.sOwner;

                        txtTaxId.Text = _dr.TaxId;

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
                        //##############################################end new 5/05/19 ##############################################

                        // Set School Area
                        var schoolAreaObj = _dbMaster.TSchoolAreas.Where(w => w.Code == _dr.SchoolAreaCode).FirstOrDefault();
                        if (schoolAreaObj != null)
                        {
                            schArea.Text = schoolAreaObj.Area;
                        }

                    }


                    var com = _dbMaster.TCompanies.Where(w => w.nCompany == schoolid).FirstOrDefault();
                    var getEmp = _dbMaster.TUsers.Where(w => w.nCompany == schoolid)
                                                     .Where(w => w.cType == "1")
                                                     .Where(w => w.cDel == null);


                    SignSchoolHeadImg.Src = "../images/emptySignature.jpg";
                    if (com.nSchoolHeadid != null)
                    {
                        var schoolHead = getEmp.Where(ee => ee.nSystemID == com.nSchoolHeadid).FirstOrDefault();
                        if (schoolHead != null && schoolHead.UserSignature != null)
                        {
                            string _d = "?d=";
                            if (schoolHead.UserSignature.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                            {
                                _d = "&d=";
                            }

                            SignSchoolHeadImg.Src = schoolHead.UserSignature + _d + DateTime.Now.ToString("MMddyyyyHHmmss");
                        }
                    }

                    SignRegistraDirectorImg.Src = "../images/emptySignature.jpg";
                    if (com.nRegistraDirectorid != null)
                    {
                        var RegistraDirector = getEmp.Where(ee => ee.nSystemID == com.nRegistraDirectorid).FirstOrDefault();
                        if (RegistraDirector != null && RegistraDirector.UserSignature != null)
                        {
                            string _d = "?d=";
                            if (RegistraDirector.UserSignature.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                            {
                                _d = "&d=";
                            }

                            SignRegistraDirectorImg.Src = RegistraDirector.UserSignature + _d + DateTime.Now.ToString("MMddyyyyHHmmss");
                        }
                    }

                    //---------------------------------------------------------------------------------------------------------------------
                    SignAcademicImg.Src = "../images/emptySignature.jpg";
                    if (com.nAcademicDirectorid != null)
                    {
                        var Academic = getEmp.Where(ee => ee.nSystemID == com.nAcademicDirectorid).FirstOrDefault();
                        if (Academic != null && Academic.UserSignature != null)
                        {
                            string _d = "?d=";
                            if (Academic.UserSignature.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                            {
                                _d = "&d=";
                            }

                            SignAcademicImg.Src = Academic.UserSignature + _d + DateTime.Now.ToString("MMddyyyyHHmmss");
                        }
                    }
                    //---------------------------------------------------------------------------------------------------------------------

                    SignSubAcademicImg.Src = "../images/emptySignature.jpg";
                    if (com.nAcademicSubDirectorid != null)
                    {
                        var SubAcademic = getEmp.Where(ee => ee.nSystemID == com.nAcademicSubDirectorid).FirstOrDefault();
                        if (SubAcademic != null && SubAcademic.UserSignature != null)
                        {
                            string _d = "?d=";
                            if (SubAcademic.UserSignature.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                            {
                                _d = "&d=";
                            }

                            SignSubAcademicImg.Src = SubAcademic.UserSignature + _d + DateTime.Now.ToString("MMddyyyyHHmmss");
                        }
                    }

                    SignAccountingDirectorImg.Src = "../images/emptySignature.jpg";
                    if (com.nAccountingDirectorid != null)
                    {
                        var AccountingDirector = getEmp.Where(ee => ee.nSystemID == com.nAccountingDirectorid).FirstOrDefault();
                        if (AccountingDirector != null && AccountingDirector.UserSignature != null)
                        {
                            string _d = "?d=";
                            if (AccountingDirector.UserSignature.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                            {
                                _d = "&d=";
                            }

                            SignAccountingDirectorImg.Src = AccountingDirector.UserSignature + _d + DateTime.Now.ToString("MMddyyyyHHmmss");
                        }
                    }

                    SignPersonnelImg.Src = "../images/emptySignature.jpg";
                    if (com.nPersonnel != null)
                    {
                        var Personnel = getEmp.Where(ee => ee.nSystemID == com.nPersonnel).FirstOrDefault();
                        if (Personnel != null && Personnel.UserSignature != null)
                        {
                            string _d = "?d=";
                            if (Personnel.UserSignature.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                            {
                                _d = "&d=";
                            }

                            SignPersonnelImg.Src = Personnel.UserSignature + _d + DateTime.Now.ToString("MMddyyyyHHmmss");
                        }
                    }

                    SignStudentDevelopmentDirectorImg.Src = "../images/emptySignature.jpg";
                    if (com.nStudentDevelopmentDirectorid != null)
                    {
                        var StudentDevelopmentDirector = getEmp.Where(ee => ee.nSystemID == com.nStudentDevelopmentDirectorid).FirstOrDefault();
                        if (StudentDevelopmentDirector != null && StudentDevelopmentDirector.UserSignature != null)
                        {
                            string _d = "?d=";
                            if (StudentDevelopmentDirector.UserSignature.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                            {
                                _d = "&d=";
                            }

                            SignStudentDevelopmentDirectorImg.Src = StudentDevelopmentDirector.UserSignature + _d + DateTime.Now.ToString("MMddyyyyHHmmss");
                        }
                    }

                    SignGMImg.Src = "../images/emptySignature.jpg";
                    if (com.nGM != null)
                    {
                        var gm = getEmp.Where(ee => ee.nSystemID == com.nGM).FirstOrDefault();
                        if (gm != null && gm.UserSignature != null)
                        {
                            string _d = "?d=";
                            if (gm.UserSignature.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                            {
                                _d = "&d=";
                            }

                            SignGMImg.Src = gm.UserSignature + _d + DateTime.Now.ToString("MMddyyyyHHmmss");
                        }
                    }

                    SignAdminImg.Src = "../images/emptySignature.jpg";
                    if (com.nWebAdminid != null)
                    {
                        var WebAdmin = getEmp.Where(ee => ee.nSystemID == com.nWebAdminid).FirstOrDefault();
                        if (WebAdmin != null && WebAdmin.UserSignature != null)
                        {
                            string _d = "?d=";
                            if (WebAdmin.UserSignature.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                            {
                                _d = "&d=";
                            }

                            SignAdminImg.Src = WebAdmin.UserSignature + _d + DateTime.Now.ToString("MMddyyyyHHmmss");
                        }
                    }

                }
            }
        }

        private string ImageURL(TEmployee employee)
        {
            if (!string.IsNullOrEmpty(employee.sPicture))
            {
                if (employee.sPicture.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                {
                    return employee.sPicture + "&d=" + DateTime.Now.ToString("MMddyyyyHHmmss");
                }
                else
                {
                    return employee.sPicture + "?d=" + DateTime.Now.ToString("MMddyyyyHHmmss");
                }
            }
            return "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("schoolprofile-edit.aspx");
        }

    }
}