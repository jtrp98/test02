using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.schoolProfileBoard
{
    public partial class schoolProfileBoard : System.Web.UI.Page
    {

        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = HttpContext.Current.Session["sEntities"].ToString();

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                //foreach (var _dr in _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany))
                //{
                var ff = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).ToList();
                int i = 0;
                if (_dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).Count() != 0)
                {
                    //ผู้อำนวยการโรงเรียน
                    if (nCompany.nSchoolHeadid != null)
                    {
                        var data1 = _db.TEmployees.Where(w => w.sEmp == nCompany.nSchoolHeadid).FirstOrDefault();
                        if (data1.sPicture != null && data1.sPicture != "")
                            imgSchoolHead.Src = data1.sPicture;
                        else imgSchoolHead.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                        schoolHeadName.Text = data1.sName + " " + data1.sLastname;
                    }
                    else
                    {
                        imgSchoolHead.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
                        schoolHeadName.Text = "ยังไม่ระบุชื่อผู้รับตำแหน่ง";
                    }

                    //หัวหน้าฝ่ายบุคคล
                    if (nCompany.nPersonnel != null)
                    {
                        var data1 = _db.TEmployees.Where(w => w.sEmp == nCompany.nPersonnel).FirstOrDefault();
                        if (data1.sPicture != null && data1.sPicture != "")
                            imgPersonnel.Src = data1.sPicture;
                        else imgPersonnel.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                        schoolHeadName.Text = data1.sName + " " + data1.sLastname;

                    }
                    else
                    {
                        imgPersonnel.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
                        personnelName.Text = "ยังไม่ระบุชื่อผู้รับตำแหน่ง";
                    }
                    //หัวหน้าฝ่ายปกครอง
                    if (nCompany.nStudentDevelopmentDirectorid != null)
                    {
                        var data1 = _db.TEmployees.Where(w => w.sEmp == nCompany.nStudentDevelopmentDirectorid).FirstOrDefault();
                        if (data1.sPicture != null && data1.sPicture != "")
                            imgStudentDevelopmentDirector.Src = data1.sPicture;
                        else imgStudentDevelopmentDirector.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                        studentDevelopmentDirectorName.Text = data1.sName + " " + data1.sLastname;

                    }
                    else
                    {
                        imgStudentDevelopmentDirector.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
                        studentDevelopmentDirectorName.Text = "ยังไม่ระบุชื่อผู้รับตำแหน่ง";
                    }

                    //หัวหน้าฝ่ายวัดผลและประเมินผล
                    if (nCompany.nRegistraDirectorid != null)
                    {
                        var data1 = _db.TEmployees.Where(w => w.sEmp == nCompany.nRegistraDirectorid).FirstOrDefault();
                        if (data1.sPicture != null && data1.sPicture != "")
                            imgRegistraDirector.Src = data1.sPicture;
                        else imgRegistraDirector.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                        registraDirectorName.Text = data1.sName + " " + data1.sLastname;

                    }
                    else
                    {
                        imgRegistraDirector.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
                        registraDirectorName.Text = "ยังไม่ระบุชื่อผู้รับตำแหน่ง";
                    }
                    //หัวหน้าฝ่ายบริหารทั่วไป
                    if (nCompany.nGM != null)
                    {
                        var data1 = _db.TEmployees.Where(w => w.sEmp == nCompany.nGM).FirstOrDefault();
                        if (data1.sPicture != null && data1.sPicture != "")
                            imgGM.Src = data1.sPicture;
                        else imgGM.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                        gmName.Text = data1.sName + " " + data1.sLastname;

                    }
                    else
                    {
                        imgGM.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
                        gmName.Text = "ยังไม่ระบุชื่อผู้รับตำแหน่ง";
                    }
                    //ผู้ช่วยผู้บริหารฝ่ายวิชาการ
                    if (nCompany.nAcademicSubDirectorid != null)
                    {
                        var data1 = _db.TEmployees.Where(w => w.sEmp == nCompany.nAcademicSubDirectorid).FirstOrDefault();
                        if (data1.sPicture != null && data1.sPicture != "")
                            imgAcademicSubDirectorid.Src = data1.sPicture;
                        else imgAcademicSubDirectorid.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";

                        academicSubDirectoridName.Text = data1.sName + " " + data1.sLastname;

                    }
                    else
                    {
                        imgAcademicSubDirectorid.Src = "http://icons.iconarchive.com/icons/double-j-design/origami-colored-pencil/256/blue-user-icon.png";
                        academicSubDirectoridName.Text = "ยังไม่ระบุชื่อผู้รับตำแหน่ง";
                    }
                }

            }

        }
    }
}