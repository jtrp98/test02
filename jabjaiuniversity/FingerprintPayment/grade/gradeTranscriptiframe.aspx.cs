using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    public partial class gradeTranscriptiframe : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                

                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                Img4.Src = nCompany.sImage;

                if (!IsPostBack)
                {
                    string sid = Request.QueryString["id"];
                    int sidn = int.Parse(sid);

                    var user = _db.TUser.Where(w => w.sID == sidn).FirstOrDefault();
                    int? idlv2n = _db.TB_StudentViews.Where(w =>  w.SchoolID == nCompany.nCompany && w.sID == sidn).OrderByDescending(o => o.numberYear).Take(1).FirstOrDefault().nTermSubLevel2; 

                    var room = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                    var sub = _db.TSubLevels.Where(w => w.nTSubLevel == room.nTSubLevel).FirstOrDefault();

                    var provincelist = _dbMaster.provinces.ToList();

                    var titlelist = _db.TTitleLists.ToList();
                    string stdTitle = "";
                    string faTitle = "";
                    string moTitle = "";
                    string oldpro = "";

                    int n;
                    bool isNumeric = int.TryParse(user.sStudentTitle, out n);
                    if (isNumeric == true)
                    {
                        var title = titlelist.Where(w => w.nTitleid == n).FirstOrDefault();
                        if (title != null)
                            stdTitle = title.titleDescription;
                    }
                    else stdTitle = user.sStudentTitle;

                    string schoolprovince = "";
                    isNumeric = int.TryParse(nCompany.sProvince, out n);
                    if (isNumeric == true)
                    {
                        var provin = provincelist.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        schoolprovince = provin.PROVINCE_NAME;
                    }
                    else schoolprovince = nCompany.sProvince;

                    if (user.oldSchoolProvince != null)
                    {
                        isNumeric = int.TryParse(user.oldSchoolProvince, out n);
                        if (isNumeric == true)
                        {
                            var provin = provincelist.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                            oldpro = provin.PROVINCE_NAME;
                        }
                    }

                    var fam = _db.TFamilyProfiles.Where(w => w.sID == sidn).FirstOrDefault();

                    if (fam != null)
                    {
                        if (fam.sFatherTitle != null)
                        {
                            isNumeric = int.TryParse(fam.sFatherTitle, out n);
                            if (isNumeric == true)
                            {
                                var title = titlelist.Where(w => w.nTitleid == n).FirstOrDefault();
                                if (title != null)
                                    faTitle = title.titleDescription;
                            }
                            else faTitle = fam.sFatherTitle;
                        }

                        if (fam.sFatherFirstName == "")
                            faTitle = "";

                        if (fam.sMotherTitle != null)
                        {
                            isNumeric = int.TryParse(fam.sMotherTitle, out n);
                            if (isNumeric == true)
                            {
                                var title = titlelist.Where(w => w.nTitleid == n).FirstOrDefault();
                                if (title != null)
                                    moTitle = title.titleDescription;
                            }
                            else moTitle = fam.sMotherTitle;
                        }


                        if (fam.sMotherFirstName == "")
                            moTitle = "";

                        header261.Text = "ชื่อบิดา " + faTitle + fam.sFatherFirstName + " " + fam.sFatherLastName;
                        header262.Text = "ชื่อมารดา " + moTitle + fam.sMotherFirstName + " " + fam.sMotherLastName;

                    }
                    else
                    {
                        header261.Text = "ชื่อบิดา ";
                        header262.Text = "ชื่อมารดา ";
                    }

                    string stdB3 = "";
                    string stdB2 = "";
                    string stdB1 = "";
                    if (room != null)
                    {
                        if (room.nBranchSpecId != null)
                        {
                            var spec = _db.TBranchSpecs.Where(w => w.BranchSpecId == room.nBranchSpecId).FirstOrDefault();
                            var subject = _db.TBranchSubjects.Where(w => w.BranchSubjectId == spec.BranchSubjectId).FirstOrDefault();
                            var brach = _db.TBranches.Where(w => w.BranchId == subject.BranchId).FirstOrDefault();

                            stdB3 = spec.BranchSpecName;
                            stdB2 = subject.BranchSubjectName;
                            stdB1 = brach.BranchName;
                        }
                    }

                    string stdTerm = "";
                    string stdYear = "";
                    DateTime now = DateTime.Now;
                    var tterm = _db.TTerms.Where(w => w.dStart <= now && w.dEnd >= now && w.cDel == null).FirstOrDefault();
                    if (tterm != null)
                    {
                        var yyear = _db.TYears.Where(w => w.nYear == tterm.nYear).FirstOrDefault();
                        stdTerm = tterm.sTerm;
                        stdYear = yyear.numberYear.ToString();
                    }

                    header11.Text = "ใบสำรวจผลการเรียน/ใบแจ้งผลการเรียน";
                    header12.Text = nCompany.sCompany;
                    header13.Text = "ประเภทวิชา " + stdB1;
                    header14.Text = "สาขาวิชา " + stdB2;
                    header15.Text = "สาขางาน " + stdB3;
                    header16.Text = "จังหวัด " + schoolprovince;


                    header21.Text = "ระดับชั้น " + sub.SubLevel + " ภาคเรียนที่ " + stdTerm + " ปีการศึกษา " + stdYear;
                    header22.Text = "ชื่อ-สกุล " + stdTitle + user.sName + " " + user.sLastname;
                    header231.Text = "เลขประจำตัว " + user.sStudentID;
                    header232.Text = "วันเดือนปีเกิด " + user.dBirth.Value.Day + " " + monthtxt(user.dBirth.Value.Month) + " " + user.dBirth.Value.AddYears(543).Year;
                    header24.Text = "หมายเลขบัตรประชาชน " + user.sIdentification;





                    string studentRace = "";
                    if (!string.IsNullOrEmpty(user.sStudentRace))
                    {
                        studentRace = _db.TMasterDatas.Where(w => w.MasterType == "9" && w.MasterCode == user.sStudentRace).FirstOrDefault().MasterDes;
                    }

                    string studentNation = "";
                    if (!string.IsNullOrEmpty(user.sStudentNation))
                    {
                        studentNation = _db.TMasterDatas.Where(w => w.MasterType == "3" && w.MasterCode == user.sStudentNation).FirstOrDefault().MasterDes;
                    }

                    string studentReligion = "";
                    if (!string.IsNullOrEmpty(user.sStudentReligion))
                    {
                        studentReligion = _db.TMasterDatas.Where(w => w.MasterType == "6" && w.MasterCode == user.sStudentReligion).FirstOrDefault().MasterDes;
                    }

                    header251.Text = "เชื้อชาติ " + studentRace;
                    header252.Text = " สัญชาติ " + studentNation;
                    header253.Text = " ศาสนา " + studentReligion;


                    header31.Text = "สถานศึกษาเดิม " + user.oldSchoolName;
                    header32.Text = "เขตจังหวัด " + oldpro;
                    header33.Text = "ชั้นเรียนสุดท้าย " + oldgraduate(user.oldSchoolGraduated);
                    header34.Text = "ผลการเรียน " + user.oldSchoolGPA2;


                }
            }


            string oldgraduate(string index)
            {
                string txt = "";
                if (index == null)
                    txt = "";
                else if (index == "11")
                    txt = "เตรียมอนุบาลศึกษา 1";
                else if (index == "12")
                    txt = "อนุบาลศึกษา 1";
                else if (index == "13")
                    txt = "อนุบาลศึกษา 2";
                else if (index == "14")
                    txt = "อนุบาลศึกษา 3";
                else if (index == "1")
                    txt = "ประถมศึกษาปีที่ 1";
                else if (index == "2")
                    txt = "ประถมศึกษาปีที่ 2";
                else if (index == "3")
                    txt = "ประถมศึกษาปีที่ 3";
                else if (index == "4")
                    txt = "ประถมศึกษาปีที่ 4";
                else if (index == "5")
                    txt = "ประถมศึกษาปีที่ 5";
                else if (index == "6")
                    txt = "ประถมศึกษาปีที่ 6";
                else if (index == "7")
                    txt = "มัธยมศึกษาตอนต้น";
                else if (index == "8")
                    txt = "มัธยมศึกษาตอนปลาย";
                else if (index == "9")
                    txt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 1";
                else if (index == "15")
                    txt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 2";
                else if (index == "16")
                    txt = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 3";
                else if (index == "10")
                    txt = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 1";
                else if (index == "17")
                    txt = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 2";
                return txt;
            }

            string monthtxt(int month)
            {
                string txt = "";
                if (month == 1)
                    txt = "มกราคม";
                else if (month == 2)
                    txt = "กุมภาพันธ์";
                else if (month == 3)
                    txt = "มีนาคม";
                else if (month == 4)
                    txt = "เมษายน";
                else if (month == 5)
                    txt = "พฤษภาคม";
                else if (month == 6)
                    txt = "มิถุนายน";
                else if (month == 7)
                    txt = "กรกฎาคม";
                else if (month == 8)
                    txt = "สิงหาคม";
                else if (month == 9)
                    txt = "กันยายน";
                else if (month == 10)
                    txt = "ตุลาคม";
                else if (month == 11)
                    txt = "พฤศจิกายน";
                else if (month == 12)
                    txt = "ธันวาคม";
                return txt;
            }

        }

    }
}