using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;
using FingerprintPayment.Class;
using JabjaiMainClass;
using System.Globalization;
using System.Web.DynamicData;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using Microsoft.Ajax.Utilities;
using System.Security.Cryptography;
using System.Configuration;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using System.Web.Services;
using FingerprintPayment.PreRegister.CsCode;

namespace FingerprintPayment.PreRegister
{
    public partial class registerPaper : PreRegisterGateway
    {
        protected bool HaveProfileImage = false;
        protected string ProfileImageUrl = "";

        static class Rot13
        {
            /// <summary>
            /// Performs the ROT13 character rotation.
            /// </summary>
            public static string Transform(string value)
            {
                char[] array = value.ToCharArray();
                for (int i = 0; i < array.Length; i++)
                {
                    int number = (int)array[i];

                    if (number >= 'a' && number <= 'z')
                    {
                        if (number > 'm')
                        {
                            number -= 13;
                        }
                        else
                        {
                            number += 13;
                        }
                    }
                    else if (number >= 'A' && number <= 'Z')
                    {
                        if (number > 'M')
                        {
                            number -= 13;
                        }
                        else
                        {
                            number += 13;
                        }
                    }
                    array[i] = (char)number;
                }
                return new string(array);
            }
        }

        protected string mode = "1"; // 1 = ใบสมัครนักเรียน, 2 = ใบมอบตัวนักเรียน
        protected void Page_Load(object sender, EventArgs e)
        {
            var enSourceId = Request.QueryString["idSchool"];
            if (!string.IsNullOrEmpty(enSourceId))
            {
                string entities = ComFunction.Rot13Transform(enSourceId);
                int schoolID = Convert.ToInt32(entities.Replace(LettersShuffle, ""));

                //string sEntities = Rot13.Transform(Request.QueryString["idSchool"]);
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {

                   
                    string id = Request.QueryString["id"];
                    mode = Request.QueryString["mode"];
                    int? idn = Int32.Parse(id);

                    //var nCompany = _dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                    var user = _db.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == idn).FirstOrDefault();
                    var titledb = _db.TTitleLists.Where(w => w.SchoolID == schoolID).ToList();
                    var schooldata = _dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

                    var room = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == user.optionLevel).FirstOrDefault();

                    string schoolnum = schooldata.sHomeNumber;
                    string soy = " ซ." + schooldata.sSoy;
                    string road = " ถ." + schooldata.sRoad;
                    string aumper = " " + schooldata.sAumpher;
                    string province = " " + schooldata.sProvince;
                    string post = " " + schooldata.sPost;
                    string phone = "โทร. " + schooldata.sPhoneOne;

                    if (schooldata.sRoad == "")
                        road = "";
                    if (schooldata.sSoy == "")
                        soy = "";
                    if (schooldata.sPhoneTwo != "" && schooldata.sPhoneTwo != null)
                        phone = "โทร. " + schooldata.sPhoneOne + ", " + schooldata.sPhoneTwo;

                    var utitle = titledb.Where(w => w.nTitleid == user.StudentTitle).FirstOrDefault();
                    string usertitle = "";
                    if (utitle != null)
                        usertitle = utitle.titleDescription;


                    p1header1.Text = schooldata.sCompany;
                    p1header2.Text = schoolnum + soy + road + aumper + province + post;
                    p1header3.Text = phone;
                    p2header1.Text = schooldata.sCompany;
                    p2header2.Text = schoolnum + soy + road + aumper + province + post;
                    p2header3.Text = phone;

                    attachline1.Text = "หลักฐานแสดงผลการเรียน 2 ชุด (ม.3 = ปพ.1:3, ม.6 = ปพ.1:พ, ปวช.)";
                    attachline2.Text = "ใบรับรองการศึกษา 1 ฉบับ";
                    attachline3.Text = "รูปถ่าย 1 นิ้ว 2 รูป";
                    attachline4.Text = "สำเนาทะเบียนบ้านของผู้สมัครและผู้ปกครอง (อย่างละ 1 ฉบับ)";
                    attachline5.Text = "สำเนาบัตรประจำตัวประชาชน ของผู้สมัครและผู้ปกครอง (อย่างละ 1 ฉบับ)";
                    attachline6.Text = "หลักฐานการเปลี่ยนชื่อ - นามสกุล ถ้ามี (1 ฉบับ)";
                    attachline7.Text = "ผู้แนะนำ ........................................................................................";
                    bottomright1.Text = "ชำระแล้ว";
                    bottomright2.Text = "จำนวน....................................บาท";
                    bottomright3.Text = "ยังไม่ได้ชำระ";

                    userID.Text = "เลขที่บัตรประจำตัวประชาชน : ";
                    userID2.Text = user.sIdentification;
                    userBirth.Text = "วันเกิด : ";
                    if (user.dBirth != null)
                    {
                        userBirth2.Text = user.dBirth.Value.Day.ToString() + " " + monthtxt(user.dBirth.Value.Month) + " " + user.dBirth.Value.AddYears(543).Year.ToString();
                    }
                    userName.Text = "ชื่อ - นามสกุล : ";
                    userName2.Text = usertitle + user.sName + " " + user.sLastname;

                    userRace.Text = "เชื้อชาติ : ";
                    string studentRace = "";
                    if (!string.IsNullOrEmpty(user.sStudentRace))
                    {
                        studentRace = _db.TMasterDatas.Where(w => w.MasterType == "9" && w.MasterCode == user.sStudentRace).FirstOrDefault().MasterDes;
                    }
                    userRace2.Text = studentRace;
                    userNation.Text = "สัญชาติ : ";
                    string studentNation = "";
                    if (!string.IsNullOrEmpty(user.sStudentNation))
                    {
                        studentNation = _db.TMasterDatas.Where(w => w.MasterType == "3" && w.MasterCode == user.sStudentNation).FirstOrDefault().MasterDes;
                    }
                    userNation2.Text = studentNation;
                    userRelig.Text = "ศาสนา : ";
                    string studentReligion = "";
                    if (!string.IsNullOrEmpty(user.sStudentReligion))
                    {
                        studentReligion = _db.TMasterDatas.Where(w => w.MasterType == "6" && w.MasterCode == user.sStudentReligion).FirstOrDefault().MasterDes;
                    }
                    userRelig2.Text = studentReligion;

                    userEmail.Text = "อีเมล์ : ";
                    userEmail2.Text = user.sEmail;
                    userPhone.Text = "เบอร์มือถือ : ";
                    userPhone2.Text = user.sPhone;
                    userRok.Text = "โรคประจำตัว : ";
                    userRok2.Text = user.sSickNormal;
                    userDrug.Text = "ประวัติแพ้ยา : ";
                    userDrug2.Text = user.sSickDrug;
                    userWeight.Text = "น้ำหนัก : ";
                    userWeight2.Text = user.nWeight + " ก.ก.";
                    if (user.nWeight == null) userWeight2.Text = "";
                    userHeight.Text = "ส่วนสูง : ";
                    userHeight2.Text = user.nHeight + " ซ.ม.";
                    if (user.nHeight == null) userHeight2.Text = "";
                    userHomenum.Text = "เลขที่ : ";
                    userHomenum2.Text = user.sStudentHomeNumber;
                    userMuu.Text = "หมู่ : ";
                    userMuu2.Text = user.sStudentMuu;
                    userProvince.Text = "จังหวัด : ";
                    userPost.Text = "รหัสไปรษณีย์ : ";
                    userPost2.Text = user.sStudentPost;

                    p2line10_3.Text = "อาศัยอยู่ที่บ้านเลขที่ ";
                    p2line10_4.Text = user.sStudentHomeNumber;
                    p2line10_5.Text = "หมู่";
                    p2line10_6.Text = user.sStudentMuu;

                    p2line11_1.Text = "ตำบล";
                    p2line11_2.Text = " ";
                    p2line11_3.Text = "อำเภอ";
                    p2line11_4.Text = " ";
                    p2line11_5.Text = "จังหวัด";
                    p2line11_6.Text = "";


                    int n;
                    string stdProvince = "";
                    bool isNumeric = int.TryParse(user.sStudentProvince, out n);
                    if (isNumeric == true)
                    {
                        var userpro = _dbMaster.provinces.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        if (userpro != null)
                        {
                            stdProvince = userpro.PROVINCE_NAME;
                        }
                    }

                    userProvince2.Text = stdProvince;
                    p2line11_6.Text = stdProvince;


                    userRoad.Text = "ถนน : ";
                    userRoad2.Text = user.sStudentRoad;
                    userSoy.Text = "ซอย : ";
                    userSoy2.Text = user.sStudentSoy;
                    userTumbon.Text = "ตำบล : ";
                    string stdTumbon = "";
                    isNumeric = int.TryParse(user.sStudentTumbon, out n);
                    if (isNumeric == true)
                    {
                        var usertum = _dbMaster.districts.Where(w => w.DISTRICT_ID == n).FirstOrDefault();
                        if (usertum != null)
                        {
                            stdTumbon = usertum.DISTRICT_NAME;
                        }
                    }
                    userTumbon2.Text = stdTumbon;
                    p2line11_2.Text = stdTumbon;

                    userAumpher.Text = "อำเภอ : ";
                    string stdAumpher = "";
                    isNumeric = int.TryParse(user.sStudentAumpher, out n);
                    if (isNumeric == true)
                    {
                        var useraum = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                        if (useraum != null)
                        {
                            stdAumpher = useraum.AMPHUR_NAME;
                        }
                    }

                    userAumpher2.Text = stdAumpher;
                    p2line11_4.Text = stdAumpher;

                    userYear.Text = "ปีการศึกษา : ";
                    userYear2.Text = (user.registerYear + 543).ToString();

                    userNumber.Text = "เลขที่ใบสมัคร : ";
                    userNumber2.Text = user.sStudentID?.ToString();

                    userSood.Text = "หลักสูตร : ";
                    if (user.optionCourse == 1)
                        userSood2.Text = "ปกติ";
                    else if (user.optionCourse == 2)
                        userSood2.Text = "พิเศษ";
                    else if (user.optionCourse == 3)
                        userSood2.Text = "ทวิภาคี";

                    userTime.Text = "รอบ : ";
                    if (user.optionTime == 1)
                        userTime2.Text = "รอบเช้า";
                    else if (user.optionTime == 2)
                        userTime2.Text = "รอบบ่าย 1";
                    else if (user.optionTime == 3)
                        userTime2.Text = "รอบบ่าย 2";
                    else userTime2.Text = "";

                    userBranch.Text = "สาขา : ";
                    userLevel.Text = "ระดับ : ";
                    var b = _db.TBranchSpecs.Where(w => w.SchoolID == schoolID && w.BranchSpecId == user.optionBranch).FirstOrDefault();
                    if (b != null)
                        userBranch2.Text = b.BranchSpecName;
                    if (room != null)
                        userLevel2.Text = room.SubLevel;

                    fatherAumpher.Text = "อำเภอ : ";
                    isNumeric = int.TryParse(user.sFatherAumpher, out n);
                    if (isNumeric == true)
                    {
                        var aum2 = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                        if (aum2 != null)
                            fatherAumpher2.Text = aum2.AMPHUR_NAME;
                    }

                    var ftitle = titledb.Where(w => w.nTitleid == user.FatherTitle).FirstOrDefault();
                    string fathertitle = "";
                    if (ftitle != null && user.sFatherFirstName != "")
                        fathertitle = ftitle.titleDescription;

                    fatherHomenum.Text = "เลขที่ : ";
                    fatherHomenum2.Text = user.sFatherHomeNumber;
                    fatherJob.Text = "รายได้ : ";
                    //fatherJob2.Text = user.fatherIncome;
                    if (user.nFatherIncome != null)
                    {
                        int n2 = (int)user.nFatherIncome;

                        fatherJob2.Text = n2.ToString("#,0.#");
                    }
                    fatherMuu.Text = "หมู่ : ";
                    fatherMuu2.Text = user.sFatherMuu;
                    fatherName.Text = "ชื่อ - นามสกุล : ";
                    fatherName2.Text = fathertitle + user.sFatherFirstName + " " + user.sFatherLastName;
                    fatherPhone.Text = "โทรศัพท์ : ";
                    //string fatherPhoneNumber = string.IsNullOrEmpty(user.sFatherPhone) ? (string.IsNullOrEmpty(user.sFatherPhone2) ? (string.IsNullOrEmpty(user.sFatherPhone3) ? "" : user.sFatherPhone3) : user.sFatherPhone2) : user.sFatherPhone;
                    fatherPhone2.Text = user.sFatherPhone2;
                    fatherPost.Text = "รหัสไปรษณีย์ : ";
                    fatherPost2.Text = user.sFatherPost;

                    fatherProvince.Text = "จังหวัด : ";
                    isNumeric = int.TryParse(user.sFatherProvince, out n);
                    if (isNumeric == true)
                    {
                        var pro2 = _dbMaster.provinces.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        if (pro2 != null)
                            fatherProvince2.Text = pro2.PROVINCE_NAME;
                    }


                    fatherRoad.Text = "ถนน : ";
                    fatherRoad2.Text = user.sFamilyRoad;
                    fatherSoy.Text = "ซอย : ";
                    fatherSoy2.Text = user.sFatherSoy;
                    fatherTumbon.Text = "ตำบล : ";
                    isNumeric = int.TryParse(user.sFatherTumbon, out n);
                    if (isNumeric == true)
                    {
                        var tum2 = _dbMaster.districts.Where(w => w.DISTRICT_ID == n).FirstOrDefault();
                        if (tum2 != null)
                            fatherTumbon2.Text = tum2.DISTRICT_NAME;
                    }


                    motherAumpher.Text = "อำเภอ : ";
                    isNumeric = int.TryParse(user.sMotherAumpher, out n);
                    if (isNumeric == true)
                    {
                        var aum3 = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                        if (aum3 != null)
                            motherAumpher2.Text = aum3.AMPHUR_NAME;
                    }

                    var mtitle = titledb.Where(w => w.nTitleid == user.MotherTitle).FirstOrDefault();
                    string mothertitle = "";
                    if (mtitle != null && user.sMotherFirstName != "")
                        mothertitle = mtitle.titleDescription;

                    motherHomenum.Text = "เลขที่ : ";
                    motherHomenum2.Text = user.sMotherHomeNumber;
                    motherJob.Text = "รายได้ : ";
                    //motherJob2.Text = user.motherIncome;
                    if (user.nMotherIncome != null)
                    {
                        int n2 = (int)user.nMotherIncome;

                        motherJob2.Text = n2.ToString("#,0.#");
                    }
                    motherMuu.Text = "หมู่ : ";
                    motherMuu2.Text = user.sMotherMuu;
                    motherName.Text = "ชื่อ - นามสกุล : ";
                    motherName2.Text = mothertitle + user.sMotherFirstName + " " + user.sMotherLastName;
                    motherPhone.Text = "โทรศัพท์ : ";
                    //string motherPhoneNumber = string.IsNullOrEmpty(user.sMotherPhone) ? (string.IsNullOrEmpty(user.sMotherPhone2) ? (string.IsNullOrEmpty(user.sMotherPhone3) ? "" : user.sMotherPhone3) : user.sMotherPhone2) : user.sMotherPhone;
                    motherPhone2.Text = user.sMotherPhone2;
                    motherPost.Text = "รหัสไปรษณีย์ : ";
                    motherPost2.Text = user.sMotherPost;

                    motherProvince.Text = "จังหวัด : ";
                    isNumeric = int.TryParse(user.sMotherProvince, out n);
                    if (isNumeric == true)
                    {
                        var pro3 = _dbMaster.provinces.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        if (pro3 != null)
                            motherProvince2.Text = pro3.PROVINCE_NAME;
                    }

                    motherRoad.Text = "ถนน : ";
                    motherRoad2.Text = user.sMotherRoad;
                    motherSoy.Text = "ซอย : ";
                    motherSoy2.Text = user.sMotherSoy;

                    motherTumbon.Text = "ตำบล : ";
                    isNumeric = int.TryParse(user.sMotherTumbon, out n);
                    if (isNumeric == true)
                    {
                        var tum3 = _dbMaster.districts.Where(w => w.DISTRICT_ID == n).FirstOrDefault();
                        if (tum3 != null)
                            motherTumbon2.Text = tum3.DISTRICT_NAME;
                    }

                    p2line4_1.Text = "ณ บ้านเลขที่ ";
                    p2line4_2.Text = user.sFamilyHomeNumber;
                    p2line4_3.Text = " หมู่ ";
                    p2line4_4.Text = user.sFamilyMuu;
                    p2line4_5.Text = " ตำบล ";
                    p2line4_6.Text = " ";
                    p2line4_7.Text = " อำเภอ ";
                    p2line4_8.Text = " ";

                    p2line5_1.Text = " จังหวัด ";
                    p2line5_2.Text = " ";
                    p2line5_3.Text = " รหัสไปรษณีย์ ";
                    p2line5_4.Text = user.sFamilyPost;

                    famAumpher.Text = "อำเภอ : ";
                    isNumeric = int.TryParse(user.sFamilyAumpher, out n);
                    if (isNumeric == true)
                    {
                        var aum4 = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                        if (aum4 != null)
                        {
                            famAumpher2.Text = aum4.AMPHUR_NAME;
                            p2line4_8.Text = aum4.AMPHUR_NAME;
                        }
                    }

                    var famtitle = titledb.Where(w => w.nTitleid == user.nFamilyTitle).FirstOrDefault();
                    string familytitle = "";
                    if (famtitle != null && user.sFamilyName != "")
                        familytitle = famtitle.titleDescription;

                    famHomenum.Text = "เลขที่ : ";
                    famHomenum2.Text = user.sFamilyHomeNumber;
                    famMuu.Text = "หมู่ : ";
                    famMuu2.Text = user.sFamilyMuu;
                    famName.Text = "ชื่อ - นามสกุล : ";
                    famName2.Text = familytitle + user.sFamilyName + " " + user.sFamilyLast;
                    famPhone.Text = "โทรศัพท์ : ";
                    //string familyPhoneNumber = string.IsNullOrEmpty(user.sPhoneOne) ? (string.IsNullOrEmpty(user.sPhoneTwo) ? (string.IsNullOrEmpty(user.sPhoneThree) ? "" : user.sPhoneThree) : user.sPhoneTwo) : user.sPhoneOne;
                    famPhone2.Text = user.sPhoneTwo;
                    famPost.Text = "รหัสไปรษณีย์ : ";
                    famPost2.Text = user.sFamilyPost;

                    famProvince.Text = "จังหวัด : ";
                    isNumeric = int.TryParse(user.sFamilyProvince, out n);
                    if (isNumeric == true)
                    {
                        var pro4 = _dbMaster.provinces.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        if (pro4 != null)
                        {
                            famProvince2.Text = pro4.PROVINCE_NAME;
                            p2line5_2.Text = pro4.PROVINCE_NAME;
                        }
                    }

                    famRoad.Text = "ถนน : ";
                    famRoad2.Text = user.sFamilyRoad;
                    famSoy.Text = "ซอย : ";
                    famSoy2.Text = user.sFamilySoy;

                    famTumbon.Text = "ตำบล :";
                    isNumeric = int.TryParse(user.sFamilyTumbon, out n);
                    if (isNumeric == true)
                    {
                        var tum4 = _dbMaster.districts.Where(w => w.DISTRICT_ID == n).FirstOrDefault();
                        if (tum4 != null)
                        {
                            famTumbon2.Text = tum4.DISTRICT_NAME;
                            p2line4_6.Text = tum4.DISTRICT_NAME;
                        }
                    }

                    //knowFromTopic.Text = "ผู้สมัครรู้จักสถานศึกษา จาก : ";
                    //knowFrom1.Text = "บิดา - มารดา";
                    //knowFrom2.Text = "ญาติ";
                    //knowFrom3.Text = "พี่/น้อง กำลังศึกษา";
                    //knowFrom4.Text = "เอกสารแนะแนว";
                    //knowFrom5.Text = "ป้ายโฆษณา";
                    //knowFrom6.Text = "ป้ายรถเมล์";
                    //knowFrom7.Text = "งานแนะแนว";
                    //knowFrom8.Text = "มีผู้แนะนำ";
                    //knowFrom9.Text = "สื่อออนไลน์ (website)";
                    //knowFrom10.Text = "สื่อออนไลน์ (google)";
                    //knowFrom11.Text = "สื่อออนไลน์ (facebook)";

                    //know1txt.Text = user.knowFrom1.ToString();
                    //know2txt.Text = user.knowFrom2.ToString();
                    //know3txt.Text = user.knowFrom3.ToString();
                    //know4txt.Text = user.knowFrom4.ToString();
                    //know5txt.Text = user.knowFrom5.ToString();
                    //know6txt.Text = user.knowFrom6.ToString();
                    //know7txt.Text = user.knowFrom7.ToString();
                    //know8txt.Text = user.knowFrom8.ToString();
                    //know9txt.Text = user.knowFrom9.ToString();
                    //know10txt.Text = user.knowFrom10.ToString();
                    //know11txt.Text = user.knowFrom11.ToString();

                    oldName.Text = "ชื่อสถานศึกษา : ";
                    oldName2.Text = user.oldSchoolName;

                    oldAumpher.Text = "อำเภอ : ";

                    int old;
                    string oldaum = "";
                    isNumeric = int.TryParse(user.oldSchoolAumpher, out old);
                    if (isNumeric == true)
                    {
                        var aum5 = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == old).FirstOrDefault();
                        if (aum5 != null)
                            oldaum = aum5.AMPHUR_NAME;
                    }
                    oldAumpher2.Text = oldaum;

                    oldTumbon.Text = "ตำบล : ";
                    string oldtum = "";
                    bool isNumeric2 = int.TryParse(user.oldSchoolTumbon, out old);
                    if (isNumeric2 == true)
                    {
                        var tum5 = _dbMaster.districts.Where(w => w.DISTRICT_ID == old).FirstOrDefault();
                        if (tum5 != null)
                            oldtum = tum5.DISTRICT_NAME;
                    }
                    oldTumbon2.Text = oldtum;

                    oldProvince.Text = "จังหวัด : ";
                    string oldpro = "";
                    bool isNumeric3 = int.TryParse(user.oldSchoolProvince, out old);
                    if (isNumeric3 == true)
                    {
                        var pro5 = _dbMaster.provinces.Where(w => w.PROVINCE_ID == old).FirstOrDefault();
                        if (pro5 != null)
                            oldpro = pro5.PROVINCE_NAME;
                    }
                    oldProvince2.Text = oldpro;

                    oldGraduated.Text = "วุฒิการศึกษา : ";

                    string oldgradu = "";
                    bool isNumeric4 = int.TryParse(user.oldSchoolGraduated, out old);
                    if (isNumeric4 == true)
                    {

                        if (old == 11)
                            oldgradu = "เตรียมอนุบาลศึกษา";
                        else if (old == 12)
                            oldgradu = "อนุบาลศึกษา 1";
                        else if (old == 13)
                            oldgradu = "อนุบาลศึกษา 2";
                        else if (old == 14)
                            oldgradu = "อนุบาลศึกษา 3";
                        else if (old == 1)
                            oldgradu = "ประถมศึกษาปีที่ 1";
                        else if (old == 2)
                            oldgradu = "ประถมศึกษาปีที่ 2";
                        else if (old == 3)
                            oldgradu = "ประถมศึกษาปีที่ 3";
                        else if (old == 4)
                            oldgradu = "ประถมศึกษาปีที่ 4";
                        else if (old == 5)
                            oldgradu = "ประถมศึกษาปีที่ 5";
                        else if (old == 6)
                            oldgradu = "ประถมศึกษาปีที่ 6";
                        else if (old == 7)
                            oldgradu = "มัธยมศึกษาตอนต้น";
                        else if (old == 8)
                            oldgradu = "มัธยมศึกษาตอนปลาย";
                        else if (old == 9)
                            oldgradu = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 1";
                        else if (old == 15)
                            oldgradu = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 2";
                        else if (old == 16)
                            oldgradu = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 3";
                        else if (old == 10)
                            oldgradu = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 1";
                        else if (old == 17)
                            oldgradu = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 2";
                        else if (old == 18)
                            oldgradu = "มัธยมศึกษาปีที่ 1";
                        else if (old == 19)
                            oldgradu = "มัธยมศึกษาปีที่ 2";
                        else if (old == 20)
                            oldgradu = "มัธยมศึกษาปีที่ 3";
                        else if (old == 21)
                            oldgradu = "มัธยมศึกษาปีที่ 4";
                        else if (old == 22)
                            oldgradu = "มัธยมศึกษาปีที่ 5";
                        else if (old == 23)
                            oldgradu = "มัธยมศึกษาปีที่ 6";
                    }

                    oldGraduated2.Text = oldgradu;
                    oldGPA.Text = "คะแนนเฉลี่ยสะสม (GPA) : ";
                    oldGPA2.Text = user.oldSchoolGPA.ToString();


                    //lat2.Text = user.addressLat.ToString();
                    //lon2.Text = user.addressLng.ToString();

                    lastpage1.Text = "ข้าพเจ้าขอทำใบมอบตัวไว้กับ" + schooldata.sCompany + " ตั้งแต่วันที่ .............. เดือน ..................... ปี .............. ข้าพเจ้าได้ทราบระเบียบข้อบังคับของสถานศึกษานี้และจะดูแลนักเรียน/นักศึกษา ในความปกครองของข้าพเจ้าให้ปฎิบัติตนอยู่ในระเบียบของสถานศึกษาทุกประการ หากนักเรียน/นักศึกษาในความปกครองของข้าพเจ้า ไม่ปฏิบัติตนอยู่ในระเบียบของสถานศึกษา ข้าพเจ้ายินยอมให้ลงโทษตามกฏระเบียบของสถานศึกษาได้ทันที โดยข้าพเจ้าจะไม่เรียกสิทธิใดจากสถานศึกษาทั้งสิ้น";
                    lastpage2.Text = "หากปรากฏในภายหลังว่า ผู้สมัครขาดคุณสมบัติข้อใดข้อหนึ่ง หรือหลักฐานที่ใช้ประกอบการสมัครเป็นเท็จแม้ว่าจะผ่านการคัดเลือกให้เข้าศึกษาแล้วก็จะถูกถอนสถานภาพเป็นนักเรียน/นักศึกษา และไม่คืนค่าสมัครให้ไม่ว่ากรณีใดๆ";
                    name1.Text = "ลงชื่อ";
                    name2.Text = "..................................................";
                    name3.Text = "ผู้สมัคร";
                    name4.Text = usertitle + user.sName + " " + user.sLastname;
                    name5.Text = "วันที่ .................................";

                    fam1.Text = "ลงชื่อ";
                    fam2.Text = "..................................................";
                    fam3.Text = "ผู้ปกครอง/ผู้อุปการะ";
                    fam4.Text = "( ............................................... )";
                    fam5.Text = "วันที่ .................................";

                    officer1.Text = "ลงชื่อ";
                    officer2.Text = "..................................................";
                    officer3.Text = "เจ้าหน้าที่";
                    officer4.Text = "( ............................................... )";
                    officer5.Text = "วันที่ .................................";


                    sign1_1.Text = "ลงชื่อ..................................................ผู้สมัคร";
                    sign1_2.Text = "วันที่ .................../.................../...................";
                    sign2_1.Text = "ลงชื่อ..................................................ผู้ปกครอง";
                    sign2_2.Text = "วันที่................... เดือน................................. ปี...........................";

                    if (schooldata != null)
                    {
                        schoolpicture.Src = schooldata.sImage;
                        Img1.Src = schooldata.sImage;
                        Img2.Src = schooldata.sImage;
                        Img3.Src = schooldata.sImage;
                    }

                    p2top1.Text = "ชื่อนักศึกษา " + usertitle + user.sName + " " + user.sLastname;
                    p2top2.Text = "เป็นนักเรียน " + room.fullName;
                    p2top3.Text = "ณ " + schooldata.sCompany;
                    p2top4.Text = "รหัสประจำตัว " + user.sStudentID;
                    p2date.Text = "วันที่................... เดือน............................. ปี...................";
                    p2line1_1.Text = "ข้าพเจ้า ";
                    p2line1_2.Text = familytitle + user.sFamilyName + " " + user.sFamilyLast;
                    p2line1_3.Text = "หลักฐาน";
                    p2line1_4.Text = " ";
                    p2line1_5.Text = "ขอทำใบมอบตัวนักเรียน";
                    p2line2_1.Text = "ให้ไว้ต่อ ";
                    p2line2_2.Text = schooldata.SchoolHeadName + schooldata.SchoolHeadLastname;
                    p2line2_3.Text = "ผู้อำนวยการ ";
                    p2line2_4.Text = schooldata.sCompany;

                    p2line3_1.Text = "ด้วย ";
                    p2line3_2.Text = usertitle + user.sName + " " + user.sLastname;
                    p2line3_3.Text = " เกิดวันที่ ";
                    p2line3_5.Text = " เดือน ";
                    p2line3_7.Text = " ปี ";
                    if (user.dBirth != null)
                    {
                        p2line3_4.Text = user.dBirth.Value.Day.ToString();
                        p2line3_6.Text = monthtxt(user.dBirth.Value.Month);
                        p2line3_8.Text = user.dBirth.Value.AddYears(543).Year.ToString();
                    }

                    p2line6_1.Text = "ชื่อบิดา ";
                    p2line6_2.Text = fathertitle + user.sFatherFirstName + " " + user.sFatherLastName;
                    p2line6_3.Text = " ชื่อมารดา ";
                    p2line6_4.Text = mothertitle + user.sMotherFirstName + " " + user.sMotherLastName;

                    p2line7_1.Text = "หลักฐานบิดามารดา";
                    p2line7_2.Text = " ";
                    p2line7_3.Text = " เกี่ยวข้องเป็น ";
                    if (user.sFamilyRelate == "พุทธ")
                        p2line7_4.Text = "บิดา";
                    else p2line7_4.Text = user.sFamilyRelate;

                    p2line8_1.Text = "เคยเล่าเรียนใน";
                    p2line8_2.Text = user.oldSchoolName;
                    p2line8_3.Text = "และได้ออกเพราะสำเร็จการศึกษาระดับ";

                    p2line9_1.Text = oldgradu;
                    p2line9_2.Text = "มีรายงานประจำตัวแสดงความประพฤติและการเล่าเรียนมาด้วย";

                    p2line10_1.Text = "บัดนี้ ";
                    p2line10_2.Text = usertitle + user.sName + " " + user.sLastname;

                    p2line12_1.Text = "สมัครจะเข้าเป็นนักเรียนในโรงเรียนนี้ ข้าพเจ้าเห็นว่า ";
                    p2line12_2.Text = usertitle + user.sName + " " + user.sLastname;
                    p2line12_3.Text = " จะตั้งใจเล่าเรียนได้ดี";

                    p2line13_1.Text = "ข้าพเจ้าจึงรับเป็นผู้ปกครองและขอรับรองว่าข้าพเจ้าจะเป็นผู้คอยตักเตือน";
                    p2line13_2.Text = usertitle + user.sName + " " + user.sLastname;

                    p2line14_1.Text = "ให้หมั่นเล่าเรียนเสมอ และให้ประพฤติตนให้เรียบร้อยตามคำสั่งสอน ข้อบังคับและระเบียบวินัยของโรงเรียน ทั้งจะเป็น";
                    p2line15_1.Text = "ผู้อุปถัมภ์ด้วยค่าเล่าเรียน &nbspเครื่องแต่งกาย &nbspและเครื่องเล่าเรียนให้พอใช้สอยและถูกต้องตามระเบียบและข้อบังคับของ";

                    p2line16_1.Text = "โรงเรียนนี้ ข้าพเจ้าขอมอบ ";
                    p2line16_2.Text = usertitle + user.sName + " " + user.sLastname;
                    p2line16_3.Text = " ให้เข้าเป็นนักเรียน เล่าเรียนในโรงเรียนนี้ ตั้งแต่";

                    p2line17_1.Text = "วันนี้เป็นต้นไป";


                    p2foot1.Text = "(ลงชื่อ)";
                    p2foot2.Text = ".......................................................................................";
                    p2foot3.Text = "ผู้ปกครอง";
                    p2foot4.Text = "";
                    p2foot5.Text = "วันที่........................../........................../..........................";
                    p2foot6.Text = "";

                    Label1.Text = "ใบสมัครนักเรียน ชั้น " + room.SubLevel;
                    Label13.Text = "ใบสมัครเข้าเป็นนักเรียน";
                    ltrReport5Topic.Text = "ใบสมัครสอบคัดเลือกนักเรียน";
                    if (mode == "2")
                    {
                        Label1.Text = "ใบมอบตัวนักเรียน ชั้น " + room.SubLevel;
                        Label13.Text = "ใบมอบตัวนักเรียน";
                        ltrReport5Topic.Text = "ใบสมัคร/ใบมอบตัวนักเรียน"; // "ใบมอบตัวนักเรียน";
                    }
                    Label2.Text = "ปีการศึกษา " + (user.registerYear + 543).ToString();
                    Label23.Text = "ประจำปีการศึกษา " + (user.registerYear + 543);

                    string className = "";
                    string classLongName = "";
                    var subLevel = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == user.optionLevel).FirstOrDefault();
                    if (subLevel != null)
                    {
                        className = subLevel.SubLevel;
                        classLongName = subLevel.fullName;
                    }

                    string planName = "";
                    if (user.RegisterPlanSetupID != 0)
                    {
                        var planSetup = _db.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.RegisterPlanSetupID == user.RegisterPlanSetupID && w.nTSubLevel == user.optionLevel).FirstOrDefault();
                        if (planSetup != null)
                        {
                            planName = planSetup.PlanName;
                        }
                    }
                    else
                    {
                        planName = "ทั้งหมด";
                    }

                    lblLevel.Text = classLongName;
                    lblPlan.Text = planName;

                    Label3.Text = schooldata.sCompany + "ตำบล" + schooldata.sTumbon + " อำเภอ" + schooldata.sAumpher + " จังหวัด" + schooldata.sProvince;
                    Label33.Text = schooldata.sCompany;
                    Label4.Text = "หลักสูตรปกติ (อ.1-ม.6)";
                    Label5.Text = "โครงการ Intensive";
                    Label6.Text = "(อ.1-ม.3)";

                    page7line1_4txta1.Text = page6line1_4txta1.Text = "&nbsp;";
                    page7line1_4txta2.Text = page6line1_4txta2.Text = "&nbsp;";
                    page7line1_4txta3.Text = page6line1_4txta3.Text = "&nbsp;";
                    page7line1_4txta4.Text = page6line1_4txta4.Text = "&nbsp;";
                    page7line1_4txta5.Text = page6line1_4txta5.Text = "&nbsp;";
                    page7line1_4txta6.Text = page6line1_4txta6.Text = "&nbsp;";
                    page7line1_4txta7.Text = page6line1_4txta7.Text = "&nbsp;";
                    page7line1_4txta8.Text = page6line1_4txta8.Text = "&nbsp;";
                    page7line1_4txta9.Text = page6line1_4txta9.Text = "&nbsp;";
                    page7line1_4txta10.Text = page6line1_4txta10.Text = "&nbsp;";
                    page7line1_4txta11.Text = page6line1_4txta11.Text = "&nbsp;";
                    page7line1_4txta12.Text = page6line1_4txta12.Text = "&nbsp;";
                    page7line1_4txta13.Text = page6line1_4txta13.Text = "&nbsp;";

                    page7line1_4txtb1.Text = page6line1_4txtb1.Text = "&nbsp;";
                    page7line1_4txtb2.Text = page6line1_4txtb2.Text = "&nbsp;";
                    page7line1_4txtb3.Text = page6line1_4txtb3.Text = "&nbsp;";
                    page7line1_4txtb4.Text = page6line1_4txtb4.Text = "&nbsp;";
                    page7line1_4txtb5.Text = page6line1_4txtb5.Text = "&nbsp;";
                    page7line1_4txtb6.Text = page6line1_4txtb6.Text = "&nbsp;";
                    page7line1_4txtb7.Text = page6line1_4txtb7.Text = "&nbsp;";
                    page7line1_4txtb8.Text = page6line1_4txtb8.Text = "&nbsp;";
                    page7line1_4txtb9.Text = page6line1_4txtb9.Text = "&nbsp;";
                    page7line1_4txtb10.Text = page6line1_4txtb10.Text = "&nbsp;";
                    page7line1_4txtb11.Text = page6line1_4txtb11.Text = "&nbsp;";

                    page7line4_4_1.Text = page6line4_4_1.Text = "&nbsp;";
                    page7line4_4_2.Text = page6line4_4_2.Text = "&nbsp;";
                    page7line4_4_3.Text = page6line4_4_3.Text = "&nbsp;";
                    page7line4_4_4.Text = page6line4_4_4.Text = "&nbsp;";
                    page7line4_4_5.Text = page6line4_4_5.Text = "&nbsp;";
                    page7line4_4_6.Text = page6line4_4_6.Text = "&nbsp;";
                    page7line4_4_7.Text = page6line4_4_7.Text = "&nbsp;";
                    page7line4_4_8.Text = page6line4_4_8.Text = "&nbsp;";
                    page7line4_4_9.Text = page6line4_4_9.Text = "&nbsp;";
                    page7line4_4_10.Text = page6line4_4_10.Text = "&nbsp;";
                    page7line4_4_11.Text = page6line4_4_11.Text = "&nbsp;";
                    page7line4_4_12.Text = page6line4_4_12.Text = "&nbsp;";
                    page7line4_4_13.Text = page6line4_4_13.Text = "&nbsp;";

                    page7line4_9_1.Text = page6line4_9_1.Text = "&nbsp;";
                    page7line4_9_2.Text = page6line4_9_2.Text = "&nbsp;";
                    page7line4_9_3.Text = page6line4_9_3.Text = "&nbsp;";
                    page7line4_9_4.Text = page6line4_9_4.Text = "&nbsp;";
                    page7line4_9_5.Text = page6line4_9_5.Text = "&nbsp;";
                    page7line4_9_6.Text = page6line4_9_6.Text = "&nbsp;";
                    page7line4_9_7.Text = page6line4_9_7.Text = "&nbsp;";
                    page7line4_9_8.Text = page6line4_9_8.Text = "&nbsp;";
                    page7line4_9_9.Text = page6line4_9_9.Text = "&nbsp;";
                    page7line4_9_10.Text = page6line4_9_10.Text = "&nbsp;";
                    page7line4_9_11.Text = page6line4_9_11.Text = "&nbsp;";
                    page7line4_9_12.Text = page6line4_9_12.Text = "&nbsp;";
                    page7line4_9_13.Text = page6line4_9_13.Text = "&nbsp;";

                    page7line6_4_1.Text = page6line6_4_1.Text = "&nbsp;";
                    page7line6_4_2.Text = page6line6_4_2.Text = "&nbsp;";
                    page7line6_4_3.Text = page6line6_4_3.Text = "&nbsp;";
                    page7line6_4_4.Text = page6line6_4_4.Text = "&nbsp;";
                    page7line6_4_5.Text = page6line6_4_5.Text = "&nbsp;";
                    page7line6_4_6.Text = page6line6_4_6.Text = "&nbsp;";
                    page7line6_4_7.Text = page6line6_4_7.Text = "&nbsp;";
                    page7line6_4_8.Text = page6line6_4_8.Text = "&nbsp;";
                    page7line6_4_9.Text = page6line6_4_9.Text = "&nbsp;";
                    page7line6_4_10.Text = page6line6_4_10.Text = "&nbsp;";
                    page7line6_4_11.Text = page6line6_4_11.Text = "&nbsp;";
                    page7line6_4_12.Text = page6line6_4_12.Text = "&nbsp;";
                    page7line6_4_13.Text = page6line6_4_13.Text = "&nbsp;";

                    page6line1_1.Text = "1) ข้อมูลทั่วไป";
                    page7line1_1.Text = "<b>" + page6line1_1.Text + "</b>";

                    page7line1_2a.Text = page6line1_2a.Text = "ชื่อ-นามสกุล(ภาษาไทย)";
                    page7line1_2b.Text = page6line1_2b.Text = usertitle + user.sName + " " + user.sLastname;
                    page7line1_2c.Text = page6line1_2c.Text = "ชื่อเล่น";
                    page7line1_2d.Text = page6line1_2d.Text = user.sNickName;
                    page7line1_2e.Text = page6line1_2e.Text = "หมู่โลหิต";
                    page7line1_2f.Text = page6line1_2f.Text = user.sBlood;

                    page7line1_3a.Text = page6line1_3a.Text = "ชื่อ-นามสกุล(Eng)";
                    page7line1_3b.Text = page6line1_3b.Text = user.sStudentNameEN + " " + user.sStudentLastEN;
                    page7line1_3c.Text = page6line1_3c.Text = "ชื่อเล่น(Eng)";
                    page7line1_3d.Text = page6line1_3d.Text = user.sNickNameEN;
                    page7line1_3e.Text = page6line1_3e.Text = "ศาสนา";
                    page7line1_3f.Text = page6line1_3f.Text = studentReligion;

                    page7line1_4a.Text = page6line1_4a.Text = "เลขประจำตัวประชาชน";

                    if (user.sIdentification != "" && user.sIdentification != null)
                    {
                        string identification = user.sIdentification.Replace(" ", "").Replace("-", "");
                        if (identification.Length > 0) page7line1_4txta1.Text = page6line1_4txta1.Text = identification[0].ToString();
                        if (identification.Length > 1) page7line1_4txta2.Text = page6line1_4txta2.Text = identification[1].ToString();
                        if (identification.Length > 2) page7line1_4txta3.Text = page6line1_4txta3.Text = identification[2].ToString();
                        if (identification.Length > 3) page7line1_4txta4.Text = page6line1_4txta4.Text = identification[3].ToString();
                        if (identification.Length > 4) page7line1_4txta5.Text = page6line1_4txta5.Text = identification[4].ToString();
                        if (identification.Length > 5) page7line1_4txta6.Text = page6line1_4txta6.Text = identification[5].ToString();
                        if (identification.Length > 6) page7line1_4txta7.Text = page6line1_4txta7.Text = identification[6].ToString();
                        if (identification.Length > 7) page7line1_4txta8.Text = page6line1_4txta8.Text = identification[7].ToString();
                        if (identification.Length > 8) page7line1_4txta9.Text = page6line1_4txta9.Text = identification[8].ToString();
                        if (identification.Length > 9) page7line1_4txta10.Text = page6line1_4txta10.Text = identification[9].ToString();
                        if (identification.Length > 10) page7line1_4txta11.Text = page6line1_4txta11.Text = identification[10].ToString();
                        if (identification.Length > 11) page7line1_4txta12.Text = page6line1_4txta12.Text = identification[11].ToString();
                        if (identification.Length > 12) page7line1_4txta13.Text = page6line1_4txta13.Text = identification[12].ToString();
                    }

                    page7line1_4b.Text = page6line1_4b.Text = "เลขรหัสประจำบ้าน";
                    if (user.sStudentHomeRegisterCode != null && user.sStudentHomeRegisterCode != "")
                    {
                        string homeCode = user.sStudentHomeRegisterCode.Replace(" ", "").Replace("-", "");
                        if (homeCode.Length > 0) page7line1_4txtb1.Text = page6line1_4txtb1.Text = homeCode[0].ToString();
                        if (homeCode.Length > 1) page7line1_4txtb2.Text = page6line1_4txtb2.Text = homeCode[1].ToString();
                        if (homeCode.Length > 2) page7line1_4txtb3.Text = page6line1_4txtb3.Text = homeCode[2].ToString();
                        if (homeCode.Length > 3) page7line1_4txtb4.Text = page6line1_4txtb4.Text = homeCode[3].ToString();
                        if (homeCode.Length > 4) page7line1_4txtb5.Text = page6line1_4txtb5.Text = homeCode[4].ToString();
                        if (homeCode.Length > 5) page7line1_4txtb6.Text = page6line1_4txtb6.Text = homeCode[5].ToString();
                        if (homeCode.Length > 6) page7line1_4txtb7.Text = page6line1_4txtb7.Text = homeCode[6].ToString();
                        if (homeCode.Length > 7) page7line1_4txtb8.Text = page6line1_4txtb8.Text = homeCode[7].ToString();
                        if (homeCode.Length > 8) page7line1_4txtb9.Text = page6line1_4txtb9.Text = homeCode[8].ToString();
                        if (homeCode.Length > 9) page7line1_4txtb10.Text = page6line1_4txtb10.Text = homeCode[9].ToString();
                        if (homeCode.Length > 10) page7line1_4txtb11.Text = page6line1_4txtb11.Text = homeCode[10].ToString();
                    }

                    page7line1_5a.Text = page6line1_5a.Text = "เกิดวันที่";
                    page7line1_5c.Text = page6line1_5c.Text = "เดือน";
                    page7line1_5e.Text = page6line1_5e.Text = "พ.ศ.";
                    if (user.dBirth != null)
                    {
                        page7line1_5b.Text = page6line1_5b.Text = user.dBirth.Value.Day.ToString();
                        page7line1_5d.Text = page6line1_5d.Text = monthtxt(user.dBirth.Value.Month);
                        page7line1_5f.Text = page6line1_5f.Text = user.dBirth.Value.AddYears(543).Year.ToString();
                    }
                    page7line1_5g.Text = page6line1_5g.Text = "เชื้อชาติ";
                    page7line1_5h.Text = page6line1_5h.Text = studentRace;
                    page7line1_5i.Text = page6line1_5i.Text = "สัญชาติ";
                    page7line1_5j.Text = page6line1_5j.Text = studentNation;
                    page7line1_5k.Text = page6line1_5k.Text = "ส่วนสูง";
                    page7line1_5l.Text = page6line1_5l.Text = user.nHeight.ToString();
                    page7line1_5m.Text = page6line1_5m.Text = "ซม.";
                    page7line1_5n.Text = page6line1_5n.Text = "น้ำหนัก";
                    page7line1_5o.Text = page6line1_5o.Text = user.nWeight.ToString();
                    page7line1_5p.Text = page6line1_5p.Text = "กก.";

                    page7line1_6a.Text = page6line1_6a.Text = "จำนวนพี่น้องทั้งหมด(รวมนักเรียน)";
                    page7line1_6b.Text = page6line1_6b.Text = user.nSonTotal.ToString();
                    page7line1_6c.Text = page6line1_6c.Text = "คน";
                    page7line1_6d.Text = page6line1_6d.Text = "มีพี่/น้องเรียนที่โรงเรียนแห่งนี้";
                    page7line1_6e.Text = page6line1_6e.Text = user.nRelativeStudyHere.ToString();
                    page7line1_6f.Text = page6line1_6f.Text = "คน";
                    page7line1_6g.Text = page6line1_6g.Text = "นักเรียนเป็นบุตรคนที่";
                    page7line1_6h.Text = page6line1_6h.Text = user.nSonNumber.ToString();

                    page7line1_7a.Text = page6line1_7a.Text = "ประเภท";
                    page7line1_7b.Text = page6line1_7b.Text = "ไป-กลับ";

                    page7line1_7c.Text = page6line1_7c.Text = "ประจำหอพัก";
                    page7line1_7d.Text = page6line1_7d.Text = "หอพักนอก(ระบุ)";
                    page7line1_7e.Text = page6line1_7e.Text = "";

                    switch (user.StudentCategory)
                    {
                        case "1":
                            studentCategory1.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                            break;
                        case "2":
                            studentCategory2.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                            break;
                    }

                    page7line1_7f.Text = page6line1_7f.Text = "โรคประจำตัว";
                    page7line1_7g.Text = page6line1_7g.Text = user.sSickNormal;

                    page6line2_1a.Text = "2) ที่อยู่ปัจจุบัน";
                    page7line2_1a.Text = "<b>" + page6line2_1a.Text + "</b>";
                    page7line2_1b.Text = page6line2_1b.Text = "บ้านเลขที่";
                    page7line2_1c.Text = page6line2_1c.Text = user.sStudentHomeNumber;
                    page7line2_1d.Text = page6line2_1d.Text = "หมู่";
                    page7line2_1e.Text = page6line2_1e.Text = user.sStudentMuu;
                    page7line2_1f.Text = page6line2_1f.Text = "ซอย";
                    page7line2_1g.Text = page6line2_1g.Text = user.sStudentSoy;
                    page7line2_1h.Text = page6line2_1h.Text = "ถนน";
                    page7line2_1i.Text = page6line2_1i.Text = user.sStudentRoad;
                    page7line2_1j.Text = page6line2_1j.Text = "ตำบล";
                    page7line2_1k.Text = page6line2_1k.Text = stdTumbon;

                    page7line2_2a.Text = page6line2_2a.Text = "อำเภอ";
                    page7line2_2b.Text = page6line2_2b.Text = stdAumpher;
                    page7line2_2c.Text = page6line2_2c.Text = "จังหวัด";
                    page7line2_2d.Text = page6line2_2d.Text = stdProvince;
                    page7line2_2e.Text = page6line2_2e.Text = "รหัสไปรษณีย์";
                    page7line2_2f.Text = page6line2_2f.Text = user.sStudentPost;
                    page7line2_2g.Text = page6line2_2g.Text = "โทรศัพท์";
                    page7line2_2h.Text = page6line2_2h.Text = user.sPhone;

                    var staytitle = titledb.Where(w => w.nTitleid == user.stayWithTitle).FirstOrDefault();
                    string staywithtitle = "";
                    if (staytitle != null && user.stayWithName != "")
                        staywithtitle = staytitle.titleDescription;

                    page7line2_3a.Text = page6line2_3a.Text = "พักอาศัยอยู่กับ";
                    page7line2_3b.Text = page6line2_3b.Text = staywithtitle + user.stayWithName + " " + user.stayWithLast;
                    page7line2_3c.Text = page6line2_3c.Text = "ที่ติดต่อฉุกเฉิน/โทรศัพท์";
                    page7line2_3d.Text = page6line2_3d.Text = user.stayWithEmergencyCall;
                    page7line2_3e.Text = page6line2_3e.Text = "E-Mail";
                    page7line2_3f.Text = page6line2_3f.Text = user.stayWithEmail;

                    page7line2_4a.Text = page6line2_4a.Text = "ลักษณะบ้านที่อยู่ปัจจุบัน";
                    page7line2_4b.Text = page6line2_4b.Text = "บ้านของตัวเอง";
                    page7line2_4c.Text = page6line2_4c.Text = "บ้านญาติ";
                    page7line2_4d.Text = page6line2_4d.Text = "บ้านเช่า";
                    page7line2_4e.Text = page6line2_4e.Text = "บ้านพักราชการ";
                    page7line2_4f.Text = page6line2_4f.Text = "หอพักโรงแรม";

                    page7line2_5a.Text = page6line2_5a.Text = "เพื่อนใกล้บ้าน";
                    page7line2_5b.Text = page6line2_5b.Text = user.friendName + " " + user.friendLastName;
                    page7line2_5c.Text = page6line2_5c.Text = "ชั้น";
                    if (user.friendSubLevel != null && user.friendSubLevel != 0 && user.friendSubLevel != -1)
                    {
                        var sublv = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == user.friendSubLevel).FirstOrDefault();
                        if (sublv != null)
                        {
                            page6line2_5d.Text = sublv.SubLevel;
                        }
                    }
                    else if (user.friendSubLevel == -1)
                        page6line2_5d.Text = "อื่นๆ";
                    page7line2_5d.Text = page6line2_5d.Text;

                    page7line2_5e.Text = page6line2_5e.Text = "โทรศัพท์/มือถือ";
                    page7line2_5f.Text = page6line2_5f.Text = user.friendPhone;

                    page6line3_1a.Text = "3) ภูมิลำเนา/ทะเบียนบ้าน";
                    page7line3_1a.Text = "<b>" + page6line3_1a.Text + "</b>";

                    page7line3_1c.Text = page6line3_1c.Text = "บ้านเลขที่";
                    page7line3_1d.Text = page6line3_1d.Text = user.houseRegistrationNumber;
                    page7line3_1e.Text = page6line3_1e.Text = "หมู่";
                    page7line3_1f.Text = page6line3_1f.Text = user.houseRegistrationMuu;
                    page7line3_1g.Text = page6line3_1g.Text = "ซอย";
                    page7line3_1h.Text = page6line3_1h.Text = user.houseRegistrationSoy;
                    page7line3_1i.Text = page6line3_1i.Text = "ถนน";
                    page7line3_1j.Text = page6line3_1j.Text = user.houseRegistrationRoad;


                    if (user.houseRegistrationTumbon != null)
                    {
                        var usertum = _dbMaster.districts.Where(w => w.DISTRICT_ID == user.houseRegistrationTumbon).FirstOrDefault();
                        if (usertum != null)
                        {
                            page7line3_2b.Text = page6line3_2b.Text = usertum.DISTRICT_NAME;
                        }
                    }

                    if (user.houseRegistrationProvince != null)
                    {
                        var userpro = _dbMaster.provinces.Where(w => w.PROVINCE_ID == user.houseRegistrationProvince).FirstOrDefault();
                        if (userpro != null)
                        {
                            page7line3_2f.Text = page6line3_2f.Text = userpro.PROVINCE_NAME;
                        }
                    }

                    if (user.houseRegistrationTumbon != null)
                    {
                        var useraum = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == user.houseRegistrationAumpher).FirstOrDefault();
                        if (useraum != null)
                        {
                            page7line3_2d.Text = page6line3_2d.Text = useraum.AMPHUR_NAME;
                        }
                    }

                    page7line3_2a.Text = page6line3_2a.Text = "ตำบล";
                    page7line3_2c.Text = page6line3_2c.Text = "อำเภอ";
                    page7line3_2e.Text = page6line3_2e.Text = "จังหวัด";

                    page7line3_2g.Text = page6line3_2g.Text = "รหัสไปรษณีย์";
                    page7line3_2h.Text = page6line3_2h.Text = user.houseRegistrationPost;
                    page7line3_2i.Text = page6line3_2i.Text = "โทรศัพท์";
                    page7line3_2j.Text = page6line3_2j.Text = user.houseRegistrationPhone;

                    if (user.bornFromTumbon != null)
                    {
                        var usertum = _dbMaster.districts.Where(w => w.DISTRICT_ID == user.bornFromTumbon).FirstOrDefault();
                        if (usertum != null)
                        {
                            page7line3_3d.Text = page6line3_3d.Text = usertum.DISTRICT_NAME;
                        }
                    }

                    if (user.bornFromProvince != null)
                    {
                        var userpro = _dbMaster.provinces.Where(w => w.PROVINCE_ID == user.bornFromProvince).FirstOrDefault();
                        if (userpro != null)
                        {
                            page7line3_3h.Text = page6line3_3h.Text = userpro.PROVINCE_NAME;
                        }
                    }

                    if (user.bornFromAumpher != null)
                    {
                        var useraum = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == user.bornFromAumpher).FirstOrDefault();
                        if (useraum != null)
                        {
                            page7line3_3f.Text = page6line3_3f.Text = useraum.AMPHUR_NAME;
                        }
                    }

                    string age = "";
                    string age2 = "";
                    string age3 = "";

                    var today = DateTime.Today;
                    if (user.dFatherBirthDay != null)
                    {
                        var cal1 = today.Year - user.dFatherBirthDay.Value.Year;
                        if (user.dFatherBirthDay.Value.Date > today.AddYears(-cal1)) cal1--;
                        age = cal1.ToString();
                    }

                    if (user.dMotherBirthDay != null)
                    {
                        var cal2 = today.Year - user.dMotherBirthDay.Value.Year;
                        if (user.dMotherBirthDay.Value.Date > today.AddYears(-cal2)) cal2--;
                        age2 = cal2.ToString();
                    }

                    if (user.dFamilyBirthDay != null)
                    {
                        var cal3 = today.Year - user.dFamilyBirthDay.Value.Year;
                        if (user.dFamilyBirthDay.Value.Date > today.AddYears(-cal3)) cal3--;
                        age3 = cal3.ToString();
                    }


                    page7line3_3a.Text = page6line3_3a.Text = "สถานที่เกิด(ระบุที่เกิด)";
                    page7line3_3b.Text = page6line3_3b.Text = user.bornFrom;
                    page7line3_3c.Text = page6line3_3c.Text = "ตำบล";
                    page7line3_3e.Text = page6line3_3e.Text = "อำเภอ";
                    page7line3_3g.Text = page6line3_3g.Text = "จังหวัด";

                    page6line4_1a.Text = "4) ประวัติครอบครัว";
                    page7line4_1a.Text = "<b>" + page6line4_1a.Text + "</b>";

                    page7line4_2a.Text = page6line4_2a.Text = "ชื่อ-นามสกุลบิดา";
                    page7line4_2b.Text = page6line4_2b.Text = fathertitle + user.sFatherFirstName + " " + user.sFatherLastName;
                    page7line4_2c.Text = page6line4_2c.Text = "ว/ด/ปี-เกิด";
                    if (user.dFatherBirthDay != null)
                        page6line4_2d.Text = user.dFatherBirthDay.Value.Day.ToString() + " " + monthtxt(user.dFatherBirthDay.Value.Month) + " " + user.dFatherBirthDay.Value.AddYears(543).Year.ToString();
                    page7line4_2d.Text = page6line4_2d.Text;
                    page7line4_2e.Text = page6line4_2e.Text = "อายุ";
                    page7line4_2f.Text = page6line4_2f.Text = age.ToString();
                    page7line4_2g.Text = page6line4_2g.Text = "ปี";

                    page7line4_3a.Text = page6line4_3a.Text = "ชื่อ-นามสกุลบิดา(Eng)";
                    page7line4_3b.Text = page6line4_3b.Text = user.sFatherNameEN + " " + user.sFatherLastEN;
                    page7line4_3c.Text = page6line4_3c.Text = "เชื้อชาติ";
                    string fatherRace = "";
                    if (!string.IsNullOrEmpty(user.sFatherRace))
                    {
                        fatherRace = _db.TMasterDatas.Where(w => w.MasterType == "9" && w.MasterCode == user.sFatherRace).FirstOrDefault().MasterDes;
                    }
                    page7line4_3d.Text = page6line4_3d.Text = fatherRace;
                    page7line4_3e.Text = page6line4_3e.Text = "สัญชาติ";
                    string fatherNation = "";
                    if (!string.IsNullOrEmpty(user.sFatherNation))
                    {
                        fatherNation = _db.TMasterDatas.Where(w => w.MasterType == "3" && w.MasterCode == user.sFatherNation).FirstOrDefault().MasterDes;
                    }
                    page7line4_3f.Text = page6line4_3f.Text = fatherNation;
                    page7line4_3g.Text = page6line4_3g.Text = "ศาสนา";
                    string fatherReligion = "";
                    if (!string.IsNullOrEmpty(user.sFatherReligion))
                    {
                        fatherReligion = _db.TMasterDatas.Where(w => w.MasterType == "6" && w.MasterCode == user.sFatherReligion).FirstOrDefault().MasterDes;
                    }
                    page7line4_3h.Text = page6line4_3h.Text = fatherReligion;

                    page7line4_4a.Text = page6line4_4a.Text = "เลขประจำตัวประชาชน";
                    page7line4_4b.Text = page6line4_4b.Text = "อาชีพ";
                    page7line4_4c.Text = page6line4_4c.Text = user.sFatherJob;
                    page7line4_4d.Text = page6line4_4d.Text = "รายได้ต่อเดือน";


                    page7line4_5a.Text = page6line4_5a.Text = "ช่วงรายได้";
                    page7line4_5b.Text = page6line4_5b.Text = "ต่ำกว่า 150,000 บาท";
                    page7line4_5c.Text = page6line4_5c.Text = "150,000 - 300,000 บาท";
                    page7line4_5d.Text = page6line4_5d.Text = "มากกว่า 300,000 บาท";
                    page7line4_5e.Text = page6line4_5e.Text = "ไม่มีรายได้";

                    page7line4_6a.Text = page6line4_6a.Text = "ระดับการศึกษา";
                    page7line4_6b.Text = page6line4_6b.Text = educate(user.sFatherGraduated);
                    if (user.sFatherGraduated == 3 || user.sFatherGraduated == 4)
                        educate13.Attributes["class"] = educate1.Attributes["class"] = "col-xs-5 pad0 lh50 botdot f60";
                    else educate13.Attributes["class"] = educate1.Attributes["class"] = "col-xs-5 pad0 lh50 botdot f80";

                    page7line4_6c.Text = page6line4_6c.Text = "สถานที่ทำงาน";
                    page7line4_6d.Text = page6line4_6d.Text = user.sFatherWorkPlace;
                    page7line4_6e.Text = page6line4_6e.Text = "โทรศัพท์ที่ทำงาน";
                    page7line4_6f.Text = page6line4_6f.Text = user.sFatherPhone3;
                    page7line4_6g.Text = page6line4_6g.Text = "โทรศัพท์มือถือ";
                    page7line4_6h.Text = page6line4_6h.Text = user.sFatherPhone2;

                    page7line4_7a.Text = page6line4_7a.Text = "ชื่อ-นามสกุลมารดา";
                    page7line4_7b.Text = page6line4_7b.Text = mothertitle + user.sMotherFirstName + " " + user.sMotherLastName;
                    page7line4_7c.Text = page6line4_7c.Text = "ว/ด/ป-เกิด";
                    if (user.dMotherBirthDay != null)
                        page7line4_7d.Text = page6line4_7d.Text = user.dMotherBirthDay.Value.Day.ToString() + " " + monthtxt(user.dMotherBirthDay.Value.Month) + " " + user.dMotherBirthDay.Value.AddYears(543).Year.ToString();
                    page7line4_7e.Text = page6line4_7e.Text = "อายุ";
                    page7line4_7f.Text = page6line4_7f.Text = age2.ToString();
                    page7line4_7g.Text = page6line4_7g.Text = "ปี";

                    page7line4_8a.Text = page6line4_8a.Text = "ชื่อ-นามสกุลมารดา(Eng)";
                    page7line4_8b.Text = page6line4_8b.Text = user.sMotherNameEN + " " + user.sMotherLastEN;
                    page7line4_8c.Text = page6line4_8c.Text = "เชื้อชาติ";
                    string motherRace = "";
                    if (!string.IsNullOrEmpty(user.sMotherRace))
                    {
                        motherRace = _db.TMasterDatas.Where(w => w.MasterType == "9" && w.MasterCode == user.sMotherRace).FirstOrDefault().MasterDes;
                    }
                    page7line4_8d.Text = page6line4_8d.Text = motherRace;
                    page7line4_8e.Text = page6line4_8e.Text = "สัญชาติ";
                    string motherNation = "";
                    if (!string.IsNullOrEmpty(user.sMotherNation))
                    {
                        motherNation = _db.TMasterDatas.Where(w => w.MasterType == "3" && w.MasterCode == user.sMotherNation).FirstOrDefault().MasterDes;
                    }
                    page7line4_8f.Text = page6line4_8f.Text = motherNation;
                    page7line4_8g.Text = page6line4_8g.Text = "ศาสนา";
                    string motherReligion = "";
                    if (!string.IsNullOrEmpty(user.sMotherReligion))
                    {
                        motherReligion = _db.TMasterDatas.Where(w => w.MasterType == "6" && w.MasterCode == user.sMotherReligion).FirstOrDefault().MasterDes;
                    }
                    page7line4_8h.Text = page6line4_8h.Text = motherReligion;

                    page7line4_9a.Text = page6line4_9a.Text = "เลขประจำตัวประชาชน";
                    page7line4_9b.Text = page6line4_9b.Text = "อาชีพ";
                    page7line4_9c.Text = page6line4_9c.Text = user.sMotherJob;
                    page7line4_9d.Text = page6line4_9d.Text = "รายได้ต่อเดือน";


                    page7line4_10a.Text = page6line4_10a.Text = "ช่วงรายได้";
                    page7line4_10b.Text = page6line4_10b.Text = "ต่ำกว่า 150,000 บาท";
                    page7line4_10c.Text = page6line4_10c.Text = "150,000 - 300,000 บาท";
                    page7line4_10d.Text = page6line4_10d.Text = "มากกว่า 300,000 บาท";
                    page7line4_10e.Text = page6line4_10e.Text = "ไม่มีรายได้";

                    page7line4_11a.Text = page6line4_11a.Text = "ระดับการศึกษา";
                    page7line4_11b.Text = page6line4_11b.Text = educate(user.sMotherGraduated);
                    if (user.sMotherGraduated == 3 || user.sMotherGraduated == 4)
                        educate23.Attributes["class"] = educate2.Attributes["class"] = "col-xs-5 pad0 lh50 botdot f60";
                    else educate23.Attributes["class"] = educate2.Attributes["class"] = "col-xs-5 pad0 lh50 botdot f80";
                    page7line4_11c.Text = page6line4_11c.Text = "สถานที่ทำงาน";
                    page7line4_11d.Text = page6line4_11d.Text = user.sMotherWorkPlace;
                    page7line4_11e.Text = page6line4_11e.Text = "โทรศัพท์ที่ทำงาน";
                    page7line4_11f.Text = page6line4_11f.Text = user.sMotherPhone3;
                    page7line4_11g.Text = page6line4_11g.Text = "โทรศัพท์มือถือ";
                    page7line4_11h.Text = page6line4_11h.Text = user.sMotherPhone2;

                    page6line5_1a.Text = "5) สถานะครอบครัว";
                    page7line5_1a.Text = "<b>" + page6line5_1a.Text + "</b>";
                    page7line5_1b.Text = page6line5_1b.Text = "บิดามารดาอยู่ด้วยกัน";
                    page7line5_1c.Text = page6line5_1c.Text = "บิดามารดาแยกกันอยู่";
                    page7line5_1d.Text = page6line5_1d.Text = "บิดามารดาหย่าร้าง";
                    page7line5_1e.Text = page6line5_1e.Text = "บิดาถึงแก่กรรม";

                    page7line5_2a.Text = page6line5_2a.Text = "มารดาถึงแก่กรรม";
                    page7line5_2b.Text = page6line5_2b.Text = "บิดามารดาถึงแก่กรรม";
                    page7line5_2c.Text = page6line5_2c.Text = "บิดามารดาแต่งงานใหม่";

                    page6line6_1a.Text = "6) ชื่อ-นามสกุลผู้ปกครอง";
                    page7line6_1a.Text = "<b>" + page6line6_1a.Text + "</b>";

                    page7line6_1c.Text = page6line6_1c.Text = "บิดา";
                    page7line6_1d.Text = page6line6_1d.Text = "มารดา";
                    page7line6_1e.Text = page6line6_1e.Text = "ญาติ";
                    page7line6_1f.Text = page6line6_1f.Text = "";
                    page7line6_1g.Text = page6line6_1g.Text = "ขอเบิกค่าเล่าเรียน";
                    page7line6_1h.Text = page6line6_1h.Text = "เบิกได้";
                    page7line6_1i.Text = page6line6_1i.Text = "เบิกไม่ได้";

                    page7line6_2a.Text = page6line6_2a.Text = "ชื่อ-นามสกุลผู้ปกครอง";
                    page7line6_2b.Text = page6line6_2b.Text = familytitle + user.sFamilyName + " " + user.sFamilyLast;
                    page7line6_2c.Text = page6line6_2c.Text = "ว/ด/ปี-เกิด";
                    if (user.dFamilyBirthDay != null)
                        page7line6_2d.Text = page6line6_2d.Text = user.dFamilyBirthDay.Value.Day.ToString() + " " + monthtxt(user.dFamilyBirthDay.Value.Month) + " " + user.dFamilyBirthDay.Value.AddYears(543).Year.ToString();
                    page7line6_2e.Text = page6line6_2e.Text = "อายุ";
                    page7line6_2f.Text = page6line6_2f.Text = age3.ToString();
                    page7line6_2g.Text = page6line6_2g.Text = "ปี";

                    page7line6_3a.Text = page6line6_3a.Text = "ชื่อ-นามสกุลผู้ปกครอง(Eng)";
                    page7line6_3b.Text = page6line6_3b.Text = user.sFamilyNameEN + " " + user.sFamilyLastEN;
                    page7line6_3c.Text = page6line6_3c.Text = "เชื้อชาติ";
                    string familyRace = "";
                    if (!string.IsNullOrEmpty(user.sFamilyRace))
                    {
                        familyRace = _db.TMasterDatas.Where(w => w.MasterType == "9" && w.MasterCode == user.sFamilyRace).FirstOrDefault().MasterDes;
                    }
                    page7line6_3d.Text = page6line6_3d.Text = familyRace;
                    page7line6_3e.Text = page6line6_3e.Text = "สัญชาติ";
                    string familyNation = "";
                    if (!string.IsNullOrEmpty(user.sFamilyNation))
                    {
                        familyNation = _db.TMasterDatas.Where(w => w.MasterType == "3" && w.MasterCode == user.sFamilyNation).FirstOrDefault().MasterDes;
                    }
                    page7line6_3f.Text = page6line6_3f.Text = familyNation;
                    page7line6_3g.Text = page6line6_3g.Text = "ศาสนา";
                    string familyReligion = "";
                    if (!string.IsNullOrEmpty(user.sFamilyReligion))
                    {
                        familyReligion = _db.TMasterDatas.Where(w => w.MasterType == "6" && w.MasterCode == user.sFamilyReligion).FirstOrDefault().MasterDes;
                    }
                    page7line6_3h.Text = page6line6_3h.Text = familyReligion;

                    page7line6_4a.Text = page6line6_4a.Text = "เลขประจำตัวประชาชน";
                    page7line6_4b.Text = page6line6_4b.Text = "อาชีพ";
                    page7line6_4c.Text = page6line6_4c.Text = user.sFamilyJob;
                    page7line6_4d.Text = page6line6_4d.Text = "รายได้ต่อเดือน";
                    page7line6_4e.Text = page6line6_4e.Text = user.nFamilyIncome?.ToString("#,0.#");

                    page7line6_5a.Text = page6line6_5a.Text = "ระดับการศึกษา";
                    page7line6_5b.Text = page6line6_5b.Text = educate(user.sFamilyGraduated);
                    if (user.sFamilyGraduated == 3 || user.sFamilyGraduated == 4)
                        educate33.Attributes["class"] = educate3.Attributes["class"] = "col-xs-5 pad0 lh50 botdot f60";
                    else educate33.Attributes["class"] = educate3.Attributes["class"] = "col-xs-5 pad0 lh50 botdot f80";
                    page7line6_5c.Text = page6line6_5c.Text = "สถานที่ทำงาน";
                    page7line6_5d.Text = page6line6_5d.Text = user.sFamilyWorkPlace;
                    page7line6_5e.Text = page6line6_5e.Text = "โทรศัพท์ที่ทำงาน";
                    page7line6_5f.Text = page6line6_5f.Text = user.sPhoneThree;
                    page7line6_5g.Text = page6line6_5g.Text = "โทรศัพท์มือถือ";
                    page7line6_5h.Text = page6line6_5h.Text = user.sPhoneTwo;

                    page6line7_1a.Text = "7) ชื่อสถานศึกษาเดิมของนักเรียน";
                    page7line7_1a.Text = "<b>" + page6line7_1a.Text + "</b>";
                    page7line7_1b.Text = page6line7_1b.Text = user.oldSchoolName;
                    page7line7_1c.Text = page6line7_1c.Text = "ตำบล";
                    page7line7_1d.Text = page6line7_1d.Text = oldtum;
                    page7line7_1e.Text = page6line7_1e.Text = "อำเภอ";
                    page7line7_1f.Text = page6line7_1f.Text = oldaum;

                    page7line7_2a.Text = page6line7_2a.Text = "จังหวัด";
                    page7line7_2b.Text = page6line7_2b.Text = oldpro;
                    page7line7_2c.Text = page6line7_2c.Text = "กำลังเรียน/จบชั้น";
                    page7line7_2d.Text = page6line7_2d.Text = oldgradu;
                    page7line7_2e.Text = page6line7_2e.Text = "ได้เกรดเฉลี่ย";
                    page7line7_2f.Text = page6line7_2f.Text = user.oldSchoolGPA.ToString();
                    page7line7_2g.Text = page6line7_2g.Text = "เหตุที่ย้าย";
                    page7line7_2h.Text = page6line7_2h.Text = user.moveOutReason;

                    page7foot1_1a.Text = page6foot1_1a.Text = "ลงชื่อ";
                    page7foot1_1b.Text = page6foot1_1b.Text = "";
                    page7foot1_1c.Text = page6foot1_1c.Text = "ผู้ปกครอง";

                    page7foot1s_2a.Text = page7foot1_2a.Text = page6foot1_2a.Text = "วันที่";
                    page7foot1s_2b.Text = page7foot1_2b.Text = page6foot1_2b.Text = "";
                    page7foot1s_2c.Text = page7foot1_2c.Text = page6foot1_2c.Text = "เดือน";
                    page7foot1s_2d.Text = page7foot1_2d.Text = page6foot1_2d.Text = "";
                    page7foot1s_2e.Text = page7foot1_2e.Text = page6foot1_2e.Text = "พ.ศ.";
                    page7foot1s_2f.Text = page7foot1_2f.Text = page6foot1_2f.Text = "";


                    footerleft1a.Text = "วันนัด: ยื่นใบสมัคร";
                    footerleft2a.Text = "มอบตัวชำระเงิน";

                    footerright1a.Text = "เจ้าหน้าที่";
                    footerright1c.Text = "ผู้รับสมัคร";
                    footerright2a.Text = "วันที่สมัคร";
                    footerright3a.Text = "รับค่าสมัคร";
                    footerright3c.Text = "แล้ว";

                    if (user.HomeType != null)
                    {
                        if (user.HomeType == 1)
                            hometype13.Attributes["class"] = hometype1.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.HomeType == 2)
                            hometype23.Attributes["class"] = hometype2.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.HomeType == 3)
                            hometype33.Attributes["class"] = hometype3.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.HomeType == 4)
                            hometype43.Attributes["class"] = hometype4.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.HomeType == 5)
                            hometype53.Attributes["class"] = hometype5.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                    }

                    if (user.nFatherIncome != null)
                    {
                        int n2 = (int)user.nFatherIncome;
                        if (n2 == 0)
                            fatherincome43.Attributes["class"] = fatherincome4.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (n2 < 12500)
                            fatherincome13.Attributes["class"] = fatherincome1.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (n2 <= 25000)
                            fatherincome23.Attributes["class"] = fatherincome2.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (n2 > 25000)
                            fatherincome33.Attributes["class"] = fatherincome3.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";


                        page7line4_4e.Text = page6line4_4e.Text = n2.ToString("#,0.#");
                    }

                    if (user.nMotherIncome != null)
                    {
                        int n2 = (int)user.nMotherIncome;
                        if (n2 == 0)
                            motherincome43.Attributes["class"] = motherincome4.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (n2 < 12500)
                            motherincome13.Attributes["class"] = motherincome1.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (n2 <= 25000)
                            motherincome23.Attributes["class"] = motherincome2.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (n2 > 25000)
                            motherincome33.Attributes["class"] = motherincome3.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";

                        page7line4_9e.Text = page6line4_9e.Text = n2.ToString("#,0.#");
                    }

                    if (user.familyStatus != null)
                    {
                        if (user.familyStatus == 1)
                            famstatus13.Attributes["class"] = famstatus1.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.familyStatus == 2)
                            famstatus23.Attributes["class"] = famstatus2.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.familyStatus == 3)
                            famstatus33.Attributes["class"] = famstatus3.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.familyStatus == 4)
                            famstatus43.Attributes["class"] = famstatus4.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.familyStatus == 5)
                            famstatus53.Attributes["class"] = famstatus5.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.familyStatus == 6)
                            famstatus63.Attributes["class"] = famstatus6.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.familyStatus == 7)
                            famstatus73.Attributes["class"] = famstatus7.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                    }

                    page7line4_4_1.Text = page6line4_4_1.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 0);
                    page7line4_4_2.Text = page6line4_4_2.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 1);
                    page7line4_4_3.Text = page6line4_4_3.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 2);
                    page7line4_4_4.Text = page6line4_4_4.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 3);
                    page7line4_4_5.Text = page6line4_4_5.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 4);
                    page7line4_4_6.Text = page6line4_4_6.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 5);
                    page7line4_4_7.Text = page6line4_4_7.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 6);
                    page7line4_4_8.Text = page6line4_4_8.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 7);
                    page7line4_4_9.Text = page6line4_4_9.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 8);
                    page7line4_4_10.Text = page6line4_4_10.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 9);
                    page7line4_4_11.Text = page6line4_4_11.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 10);
                    page7line4_4_12.Text = page6line4_4_12.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 11);
                    page7line4_4_13.Text = page6line4_4_13.Text = GetIDCardNumberTooIndex(user.sFatherIdCardNumber, 12);

                    page7line4_9_1.Text = page6line4_9_1.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 0);
                    page7line4_9_2.Text = page6line4_9_2.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 1);
                    page7line4_9_3.Text = page6line4_9_3.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 2);
                    page7line4_9_4.Text = page6line4_9_4.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 3);
                    page7line4_9_5.Text = page6line4_9_5.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 4);
                    page7line4_9_6.Text = page6line4_9_6.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 5);
                    page7line4_9_7.Text = page6line4_9_7.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 6);
                    page7line4_9_8.Text = page6line4_9_8.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 7);
                    page7line4_9_9.Text = page6line4_9_9.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 8);
                    page7line4_9_10.Text = page6line4_9_10.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 9);
                    page7line4_9_11.Text = page6line4_9_11.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 10);
                    page7line4_9_12.Text = page6line4_9_12.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 11);
                    page7line4_9_13.Text = page6line4_9_13.Text = GetIDCardNumberTooIndex(user.sMotherIdCardNumber, 12);

                    page7line6_4_1.Text = page6line6_4_1.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 0);
                    page7line6_4_2.Text = page6line6_4_2.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 1);
                    page7line6_4_3.Text = page6line6_4_3.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 2);
                    page7line6_4_4.Text = page6line6_4_4.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 3);
                    page7line6_4_5.Text = page6line6_4_5.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 4);
                    page7line6_4_6.Text = page6line6_4_6.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 5);
                    page7line6_4_7.Text = page6line6_4_7.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 6);
                    page7line6_4_8.Text = page6line6_4_8.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 7);
                    page7line6_4_9.Text = page6line6_4_9.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 8);
                    page7line6_4_10.Text = page6line6_4_10.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 9);
                    page7line6_4_11.Text = page6line6_4_11.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 10);
                    page7line6_4_12.Text = page6line6_4_12.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 11);
                    page7line6_4_13.Text = page6line6_4_13.Text = GetIDCardNumberTooIndex(user.sFamilyIdCardNumber, 12);

                    if (user.sFamilyRelate != null)
                    {
                        switch (user.sFamilyRelate.Trim())
                        {
                            case "บิดา":
                            case "พ่อ": famrelate13.Attributes["class"] = famrelate1.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle"; break;
                            case "มารดา":
                            case "แม่": famrelate23.Attributes["class"] = famrelate2.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle"; break;
                            case "ญาติ": famrelate33.Attributes["class"] = famrelate3.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle"; break;
                            default: famrelate33.Attributes["class"] = famrelate3.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle"; break;
                        }
                    }

                    if (user.nFamilyRequestStudyMoney != null)
                    {
                        if (user.nFamilyRequestStudyMoney == 1)
                            requestmoney13.Attributes["class"] = requestmoney1.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                        else if (user.nFamilyRequestStudyMoney == 0)
                            requestmoney23.Attributes["class"] = requestmoney2.Attributes["class"] = "col-xs-1 far fa-check-circle lh50 circle";
                    }

                    // Page 8, Page 9
                    if (schooldata != null)
                    {
                        imgPage8Logo.Src = schooldata.sImage;
                    }
                    ltrSchoolName.Text = schooldata.sCompany;
                    ltrRegisterYear.Text = (user.registerYear + 543).ToString();
                    ltrStudentID.Text = user.sStudentID?.ToString();
                    ltrExamCode.Text = user.ExamCode;
                    ltrRegisterLevel.Text = classLongName;
                    ltrPlan.Text = planName;
                    ltrRegisterDate.Text = user.addDate?.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                    switch (user.sStudentReligion)
                    {
                        case "101": ltrReligion101.Text = "-check"; break;
                        case "102": ltrReligion102.Text = "-check"; break;
                        case "103": ltrReligion103.Text = "-check"; break;
                        default: ltrReligion999.Text = "-check"; break;
                    }
                    ltrStudentName.Text = usertitle + user.sName + " " + user.sLastname;
                    ltrStudentNickname.Text = user.sNickName;
                    ltrStudentBirthday.Text = user.dBirth?.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                    ltrStudentAgeYear.Text = user.dBirth != null ? ComFunction.CalculateAge(user.dBirth.Value, "year") : "";
                    ltrStudentAgeMonth.Text = user.dBirth != null ? ComFunction.CalculateAge(user.dBirth.Value, "month") : "";

                    string sickFood = user.sSickFood?.Replace("-", "").Replace("ไม่แพ้", "").Replace("ไม่มี", "").Trim();
                    string sickDrug = user.sSickDrug?.Replace("-", "").Replace("ไม่แพ้", "").Replace("ไม่มี", "").Trim();
                    string sickOther = user.sSickOther?.Replace("-", "").Replace("ไม่แพ้", "").Replace("ไม่มี", "").Trim();
                    string sickNormal = user.sSickNormal?.Replace("-", "").Replace("ไม่มีโรค", "").Replace("ไม่มี", "").Trim();
                    string sickDanger = user.sSickDanger?.Replace("-", "").Replace("ไม่มีโรค", "").Replace("ไม่มี", "").Trim();
                    string symptom = "";
                    symptom += !string.IsNullOrEmpty(sickFood) ? ", " + sickFood : "";
                    symptom += !string.IsNullOrEmpty(sickDrug) ? ", " + sickDrug : "";
                    symptom += !string.IsNullOrEmpty(sickOther) ? ", " + sickOther : "";
                    symptom += !string.IsNullOrEmpty(sickNormal) ? ", " + sickNormal : "";
                    symptom += !string.IsNullOrEmpty(sickDanger) ? ", " + sickDanger : "";

                    ltrSymptoms.Text = !string.IsNullOrEmpty(symptom) ? symptom.Remove(0, 2) : "-";
                    ltrStudentHomeNumber.Text = user.sStudentHomeNumber;
                    ltrStudentMuu.Text = user.sStudentMuu;
                    ltrStudentSoy.Text = user.sStudentSoy;
                    ltrStudentRoad.Text = user.sStudentRoad;
                    ltrStudentTumbon.Text = stdTumbon;
                    ltrStudentAumpher.Text = stdAumpher;
                    ltrStudentProvince.Text = stdProvince;
                    ltrStudentPost.Text = user.sStudentPost;
                    ltrPhone.Text = user.sPhone;

                    ltrHouseRegistrationNumber.Text = user.houseRegistrationNumber;
                    ltrHouseRegistrationMuu.Text = user.houseRegistrationMuu;
                    ltrHouseRegistrationSoy.Text = user.houseRegistrationSoy;
                    ltrHouseRegistrationRoad.Text = user.houseRegistrationRoad;
                    ltrHouseRegistrationTumbon.Text = page6line3_2b.Text;
                    ltrHouseRegistrationAumpher.Text = page6line3_2d.Text;
                    ltrHouseRegistrationProvince.Text = page6line3_2f.Text;
                    ltrIdentification.Text = user.sIdentification;

                    ltrFatherName.Text = fathertitle + user.sFatherFirstName + " " + user.sFatherLastName;
                    ltrFatherIdentification.Text = user.sFatherIdCardNumber;
                    ltrFatherAge.Text = age.ToString();
                    ltrFatherRace.Text = fatherRace;
                    ltrFatherNation.Text = fatherNation;
                    ltrFatherReligion.Text = fatherReligion;
                    ltrFatherGraduated.Text = educate(user.sFatherGraduated);
                    ltrFatherJob.Text = user.sFatherJob;
                    ltrFatherIncome.Text = user.nFatherIncome?.ToString("#,0");
                    ltrFatherWorkPlace.Text = user.sFatherWorkPlace;
                    ltrFatherPhone1.Text = user.sFatherPhone;
                    ltrFatherPhone2.Text = user.sFatherPhone2;

                    ltrMotherName.Text = mothertitle + user.sMotherFirstName + " " + user.sMotherLastName;
                    ltrMotherIdentification.Text = user.sMotherIdCardNumber;
                    ltrMotherAge.Text = age2.ToString();
                    ltrMotherRace.Text = motherRace;
                    ltrMotherNation.Text = motherNation;
                    ltrMotherReligion.Text = motherReligion;
                    ltrMotherGraduated.Text = educate(user.sMotherGraduated);
                    ltrMotherJob.Text = user.sMotherJob;
                    ltrMotherIncome.Text = user.nMotherIncome?.ToString("#,0");
                    ltrMotherWorkPlace.Text = user.sMotherWorkPlace;
                    ltrMotherPhone1.Text = user.sMotherPhone;
                    ltrMotherPhone2.Text = user.sMotherPhone2;

                    switch (user.familyStatus)
                    {
                        case 1: ltrFamilyStatus01.Text = "-check"; break;
                        case 2: break;
                        case 3: ltrFamilyStatus03.Text = "-check"; break;
                        case 4: ltrFamilyStatus04.Text = "-check"; break;
                        case 5: ltrFamilyStatus05.Text = "-check"; break;
                        case 6: ltrFamilyStatus06.Text = "-check"; break;
                        case 7: ltrFamilyStatus07.Text = "-check"; break;
                        default: ltrFamilyStatus99.Text = "-check"; break;
                    }

                    ltrParentName.Text = familytitle + user.sFamilyName + " " + user.sFamilyLast;
                    ltrParentIdentification.Text = user.sFamilyIdCardNumber;
                    ltrParentAge.Text = age3.ToString();
                    ltrParentRace.Text = familyRace;
                    ltrParentNation.Text = familyNation;
                    ltrParentReligion.Text = familyReligion;
                    ltrParentRelate.Text = user.sFamilyRelate;
                    ltrParentGraduated.Text = educate(user.sFamilyGraduated);
                    ltrParentJob.Text = user.sFamilyJob;
                    ltrParentIncome.Text = user.nFamilyIncome?.ToString("#,0");
                    ltrParentWorkPlace.Text = user.sFamilyWorkPlace;
                    ltrParentPhone1.Text = user.sPhoneOne;
                    ltrParentPhone2.Text = user.sPhoneTwo;

                    ltrOldSchoolName.Text = user.oldSchoolName;
                    ltrOldSchoolTumbon.Text = oldtum;
                    ltrOldSchoolAumpher.Text = oldaum;
                    ltrOldSchoolProvince.Text = oldpro;
                    ltrOldSchoolGraduated.Text = oldgradu;
                    ltrOldSchoolGPA.Text = user.oldSchoolGPA.ToString();

                    // Check profile image
                    if (!string.IsNullOrEmpty(user.sStudentPicture))
                    {
                        HaveProfileImage = true;
                        ProfileImageUrl = user.sStudentPicture + "?" + DateTime.Now.Ticks;
                    }

                    var registerSetupObj = _db.TRegisterSetups.Where(w => w.SchoolID == schoolID && w.Year == (user.registerYear + 543) && w.StudentType == user.StudentType && w.RegisterPlanSetupID == user.RegisterPlanSetupID && w.nTSubLevel == user.optionLevel).FirstOrDefault();
                    if (registerSetupObj != null)
                    {
                        ltrFee.Text = registerSetupObj.Fee?.ToString("#,0.#");
                    }
                    else
                    {
                        ltrFee.Text = "-";
                    }

                    if (!IsPostBack)
                    {

                    }
                }
            }

            string educate(int? index)
            {
                string graduatedName = "";
                switch (index)
                {
                    case 1: graduatedName = "ต่ำกว่าประถม"; break;
                    case 2: graduatedName = "ประถมศึกษา"; break;
                    case 3: graduatedName = "มัธยมศึกษาหรือเทียบเท่า"; break;
                    case 4: graduatedName = "ปริญญาตรี"; break;
                    case 5: graduatedName = "ปริญญาโท"; break;
                    case 6: graduatedName = "ปริญญาเอก"; break;
                    case 7: graduatedName = "มัธยมต้น"; break;
                    case 8: graduatedName = "อนุปริญญา"; break;
                    case 9: graduatedName = "ประกาศนียบัตรวิชาชีพ"; break;
                    case 10: graduatedName = "ประกาศนียบัตรวิชาชีพชั้นสูง"; break;
                }
                return graduatedName;
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

        public string GetIDCardNumberTooIndex(string idCardNumber, int index)
        {
            return !string.IsNullOrEmpty(idCardNumber) && idCardNumber.Length > index ? idCardNumber[index].ToString() : "&nbsp;";
        }
    }
}