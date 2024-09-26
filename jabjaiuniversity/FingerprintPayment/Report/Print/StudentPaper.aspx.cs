using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report.Print
{
    public partial class StudentPaper : BasePrinter
    {
        public PageModel PageModel = new PageModel();

        public string mode = "1"; // 1 = ใบมอบตัวนักเรียน , 2 = ใบประวัต

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



        protected void Page_Load(object sender, EventArgs e)
        {
            //var enSourceId = Request.QueryString["idSchool"];

            int schoolID = UserData.CompanyID;
            //if (!string.IsNullOrEmpty(enSourceId))
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (var _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))

            {
// //                                
                
           
                mode = Request.QueryString["mode"];
                var type = Request.QueryString["type"];// 1 = ทั้งห้อง 2 = รายคน
                                                       //int? idn = Int32.Parse(id);
                var term = Request.QueryString["term"];
                var tbStudentView = _db.TB_StudentViews.Where(o => o.SchoolID == schoolID && o.cDel == null && o.nTerm == term);

             
                var lvl1 = Request.QueryString["lvl1"];
                if (type == "1")
                {
                    var lvl2 = Request.QueryString["lvl2"];
                    tbStudentView = tbStudentView.Where(o => o.nTermSubLevel2 + "" == lvl2 && o.nTSubLevel + "" == lvl1);

                }
                else
                {
                    var sid = Request.QueryString["sid"];
                    tbStudentView = tbStudentView.Where(o => o.sID + "" == sid);
                }


                var studentHealthGrowth = _db.Database.SqlQuery<TStudentHealthGrowth>(string.Format(@"
SELECT shg1.*
FROM TStudentHealthGrowth shg1 INNER JOIN
(
    SELECT ROW_NUMBER() OVER(PARTITION BY nHealthID ORDER BY nTSubLevel DESC, NewnMonth DESC) 'SEQ', shg0.*
    FROM
    (
        SELECT nHealthID, nTSubLevel, nMonth, (CASE nMonth WHEN 5 THEN 1 WHEN 8 THEN 2 WHEN 11 THEN 3 WHEN 2 THEN 4 END) 'NewnMonth'
        FROM TStudentHealthGrowth
        WHERE nHealthID = {0} AND SchoolID = {0}
    ) shg0
) shg2
ON shg1.nHealthID = shg2.nHealthID AND shg1.nTSubLevel = shg2.nTSubLevel AND shg1.nMonth = shg2.nMonth
WHERE shg2.SEQ = 1 AND SchoolID = {0}", UserData.CompanyID)).ToList();

                var users = (
                           from a0 in tbStudentView.AsEnumerable()

                           from a in _db.TUser.Where(o => o.SchoolID == schoolID && o.cDel == null && o.sID == a0.sID).AsEnumerable().DefaultIfEmpty()

                           from b in _db.TStudentHealthInfoes.Where(o => o.SchoolID == schoolID && o.sID == a.sID).AsEnumerable().DefaultIfEmpty()

                           from c in _db.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == a.sID).AsEnumerable().DefaultIfEmpty()

                           from d in studentHealthGrowth.Where(i => i.nHealthID == b?.nHealthID).DefaultIfEmpty()

                           select new
                           {
                               sview = a0,
                               user = a,
                               h = b,
                               f = c,
                               g = d,
                           })
                           .ToList();


                                                var titledb = _db.TTitleLists.Where(w => w.SchoolID == schoolID).ToList();
                var schooldata = _dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                var rooms = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel + "" == lvl1).ToList();
                var room = rooms.Where(w => w.nTSubLevel + "" == lvl1).FirstOrDefault();
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

                PageModel.p1header1 = schooldata.sCompany;
                PageModel.p1header2 = schoolnum + soy + road + aumper + province + post;
                PageModel.p1header3 = phone;
                PageModel.p2header1 = schooldata.sCompany;
                PageModel.p2header2 = schoolnum + soy + road + aumper + province + post;
                PageModel.p2header3 = phone;

                PageModel.attachline1 = "หลักฐานแสดงผลการเรียน 2 ชุด (ม.3 = ปพ.1:3, ม.6 = ปพ.1:พ, ปวช.)";
                PageModel.attachline2 = "ใบรับรองการศึกษา 1 ฉบับ";
                PageModel.attachline3 = "รูปถ่าย 1 นิ้ว 2 รูป";
                PageModel.attachline4 = "สำเนาทะเบียนบ้านของผู้สมัครและผู้ปกครอง (อย่างละ 1 ฉบับ)";
                PageModel.attachline5 = "สำเนาบัตรประจำตัวประชาชน ของผู้สมัครและผู้ปกครอง (อย่างละ 1 ฉบับ)";
                PageModel.attachline6 = "หลักฐานการเปลี่ยนชื่อ - นามสกุล ถ้ามี (1 ฉบับ)";
                PageModel.attachline7 = "ผู้แนะนำ ........................................................................................";
                PageModel.bottomright1 = "ชำระแล้ว";
                PageModel.bottomright2 = "จำนวน....................................บาท";
                PageModel.bottomright3 = "ยังไม่ได้ชำระ";

                var masterDataList = _db.TMasterDatas.AsNoTracking().ToList();
                var districList = _dbMaster.districts.AsNoTracking().ToList();
                var amphursList = _dbMaster.amphurs.AsNoTracking().ToList();
                var provincList = _dbMaster.provinces.AsNoTracking().ToList();

                PageModel.Label33 = schooldata.sCompany;
                PageModel.Img3 = schooldata.sImage;
                if (mode == "1")
                {
                    PageModel.Label13 = "ใบมอบตัวนักเรียน";
                }
                else
                {
                    PageModel.Label13 = "รายงานประวัติข้อมูลนักเรียน";
                }
                PageModel.Label23 = "ประจำปีการศึกษา";

                string className = "";
                string classLongName = "";
                var subLevel = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel + "" == lvl1).FirstOrDefault();
                if (subLevel != null)
                {
                    className = subLevel.SubLevel;
                    classLongName = subLevel.fullName;
                }
                PageModel.lblLevel = classLongName;

 
                                                                                                                                                  
                PageModel.Users = new List<UserModel>();
                foreach (var user in users)
                {
                    var userModel = new UserModel();
                    var utitle = titledb.Where(w => w.nTitleid + "" == user.user.sStudentTitle).FirstOrDefault();
                    string usertitle = "";
                    if (utitle != null)
                        usertitle = utitle.titleDescription;

                    userModel.Image = user.user.sStudentPicture;
                    userModel.userID = "เลขที่บัตรประจำตัวประชาชน : ";
                    userModel.userID2 = user.user.sIdentification;
                    userModel.userBirth = "วันเกิด : ";
                    userModel.userBirth2 = user.user.dBirth?.Day + " " + monthtxt(user.user.dBirth?.Month) + " " + user.user.dBirth?.AddYears(543).Year;
                    userModel.userName = "ชื่อ - นามสกุล : ";
                    userModel.userName2 = usertitle + user.user.sName + " " + user.user.sLastname;
                    userModel.studentID = user.user.sStudentID;

                    userModel.userRace = "เชื้อชาติ : ";
                    string studentRace = "";
                    if (!string.IsNullOrEmpty(user.user.sStudentRace))
                    {
                        studentRace = masterDataList.Where(w => w.MasterType == "9" && w.MasterCode == user.user.sStudentRace).FirstOrDefault().MasterDes;
                    }
                    userModel.userRace2 = studentRace;
                    userModel.userNation = "สัญชาติ : ";
                    string studentNation = "";
                    if (!string.IsNullOrEmpty(user.user.sStudentNation))
                    {
                        studentNation = masterDataList.Where(w => w.MasterType == "3" && w.MasterCode == user.user.sStudentNation).FirstOrDefault().MasterDes;
                    }
                    userModel.userNation2 = studentNation;
                    userModel.userReligext = "ศาสนา : ";
                    string studentReligion = "";
                    if (!string.IsNullOrEmpty(user.user.sStudentReligion))
                    {
                        studentReligion = masterDataList.Where(w => w.MasterType == "6" && w.MasterCode == user.user.sStudentReligion).FirstOrDefault().MasterDes;
                    }
                    userModel.userRelig2 = studentReligion;

                    userModel.userEmail = "อีเมล์ : ";
                    userModel.userEmail2 = user.user.sEmail;
                    userModel.userPhone = "เบอร์มือถือ : ";
                    userModel.userPhone2 = user.user.sPhone;
                    userModel.userRok = "โรคประจำตัว : ";
                    userModel.userRok2 = user.h?.sSickNormal;
                    userModel.userDrug = "ประวัติแพ้ยา : ";
                    userModel.userDrug2 = user.h?.sSickDrug;
                    userModel.userWeight = "น้ำหนัก : ";
                    userModel.userWeight2 = user.g?.Weight + " ก.ก.";
                    if (user.g?.Weight == null)
                        userModel.userWeight2 = "";
                    userModel.userHeight = "ส่วนสูง : ";
                    userModel.userHeight2 = user.g?.Height + " ซ.ม.";
                    if (user.g?.Height == null)
                        userModel.userHeight2 = "";
                    userModel.userHomenum = "เลขที่ : ";
                    userModel.userHomenum2 = user.user.sStudentHomeNumber;
                    userModel.userMuu = "หมู่ : ";
                    userModel.userMuu2 = user.user.sStudentMuu;
                    userModel.userProvince = "จังหวัด : ";
                    userModel.userPost = "รหัสไปรษณีย์ : ";
                    userModel.userPost2 = user.user.sStudentPost;

                 


                    int n;
                    string stdProvince = "";
                    bool isNumeric = int.TryParse(user.user.sStudentProvince, out n);
                    if (isNumeric == true)
                    {
                        var userpro = provincList.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        if (userpro != null)
                        {
                            stdProvince = userpro.PROVINCE_NAME;
                        }
                    }

                 
                    string stdTumbon = "";
                    isNumeric = int.TryParse(user.user.sStudentTumbon, out n);
                    if (isNumeric == true)
                    {
                        var usertum = districList.Where(w => w.DISTRICT_ID == n).FirstOrDefault();
                        if (usertum != null)
                        {
                            stdTumbon = usertum.DISTRICT_NAME;
                        }
                    }
                  
                    string stdAumpher = "";
                    isNumeric = int.TryParse(user.user.sStudentAumpher, out n);
                    if (isNumeric == true)
                    {
                        var useraum = amphursList.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                        if (useraum != null)
                        {
                            stdAumpher = useraum.AMPHUR_NAME;
                        }
                    }

                 

                    var ftitle = titledb.Where(w => w.nTitleid + "" == user.f?.sFatherTitle).FirstOrDefault();
                    string fathertitle = "";
                    if (ftitle != null && user.f?.sFatherFirstName != "")
                        fathertitle = ftitle.titleDescription;

                    var mtitle = titledb.Where(o => o.nTitleid + "" == user.f?.sMotherTitle).FirstOrDefault();
                    string mothertitle = "";
                    if (mtitle != null && user.f?.sMotherFirstName != "")
                        mothertitle = mtitle.titleDescription;


                    var famtitle = titledb.Where(w => w.nTitleid + "" == user.f?.sFamilyTitle).FirstOrDefault();
                    string familytitle = "";
                    if (famtitle != null && user.f?.sFamilyName != "")
                        familytitle = famtitle.titleDescription;

                
                    int old;
                    string oldaum = "";
                    isNumeric = int.TryParse(user.user.oldSchoolAumpher, out old);
                    if (isNumeric == true)
                    {
                        var aum5 = amphursList.Where(w => w.AMPHUR_ID == old).FirstOrDefault();
                        if (aum5 != null)
                            oldaum = aum5.AMPHUR_NAME;
                    }
               
                    string oldtum = "";
                    bool isNumeric2 = int.TryParse(user.user.oldSchoolTumbon, out old);
                    if (isNumeric2 == true)
                    {
                        var tum5 = districList.Where(w => w.DISTRICT_ID == old).FirstOrDefault();
                        if (tum5 != null)
                            oldtum = tum5.DISTRICT_NAME;
                    }
                   
                    string oldpro = "";
                    bool isNumeric3 = int.TryParse(user.user.oldSchoolProvince, out old);
                    if (isNumeric3 == true)
                    {
                        var pro5 = provincList.Where(w => w.PROVINCE_ID == old).FirstOrDefault();
                        if (pro5 != null)
                            oldpro = pro5.PROVINCE_NAME;
                    }
                   

                    string oldgradu = StudentInfo.StdEducation.GetNameOldSchoolGraduated(user.user.oldSchoolGraduated);
                  
                    userModel.page7line1_4txta1 = "&nbsp;";
                    userModel.page7line1_4txta2 = "&nbsp;";
                    userModel.page7line1_4txta3 = "&nbsp;";
                    userModel.page7line1_4txta4 = "&nbsp;";
                    userModel.page7line1_4txta5 = "&nbsp;";
                    userModel.page7line1_4txta6 = "&nbsp;";
                    userModel.page7line1_4txta7 = "&nbsp;";
                    userModel.page7line1_4txta8 = "&nbsp;";
                    userModel.page7line1_4txta9 = "&nbsp;";
                    userModel.page7line1_4txta10 = "&nbsp;";
                    userModel.page7line1_4txta11 = "&nbsp;";
                    userModel.page7line1_4txta12 = "&nbsp;";
                    userModel.page7line1_4txta13 = "&nbsp;";

                    userModel.page7line1_4txtb1 = "&nbsp;";
                    userModel.page7line1_4txtb2 = "&nbsp;";
                    userModel.page7line1_4txtb3 = "&nbsp;";
                    userModel.page7line1_4txtb4 = "&nbsp;";
                    userModel.page7line1_4txtb5 = "&nbsp;";
                    userModel.page7line1_4txtb6 = "&nbsp;";
                    userModel.page7line1_4txtb7 = "&nbsp;";
                    userModel.page7line1_4txtb8 = "&nbsp;";
                    userModel.page7line1_4txtb9 = "&nbsp;";
                    userModel.page7line1_4txtb10 = "&nbsp;";
                    userModel.page7line1_4txtb11 = "&nbsp;";

                    userModel.page7line4_4_1 = "&nbsp;";
                    userModel.page7line4_4_2 = "&nbsp;";
                    userModel.page7line4_4_3 = "&nbsp;";
                    userModel.page7line4_4_4 = "&nbsp;";
                    userModel.page7line4_4_5 = "&nbsp;";
                    userModel.page7line4_4_6 = "&nbsp;";
                    userModel.page7line4_4_7 = "&nbsp;";
                    userModel.page7line4_4_8 = "&nbsp;";
                    userModel.page7line4_4_9 = "&nbsp;";
                    userModel.page7line4_4_10 = "&nbsp;";
                    userModel.page7line4_4_11 = "&nbsp;";
                    userModel.page7line4_4_12 = "&nbsp;";
                    userModel.page7line4_4_13 = "&nbsp;";

                    userModel.page7line4_9_1 = "&nbsp;";
                    userModel.page7line4_9_2 = "&nbsp;";
                    userModel.page7line4_9_3 = "&nbsp;";
                    userModel.page7line4_9_4 = "&nbsp;";
                    userModel.page7line4_9_5 = "&nbsp;";
                    userModel.page7line4_9_6 = "&nbsp;";
                    userModel.page7line4_9_7 = "&nbsp;";
                    userModel.page7line4_9_8 = "&nbsp;";
                    userModel.page7line4_9_9 = "&nbsp;";
                    userModel.page7line4_9_10 = "&nbsp;";
                    userModel.page7line4_9_11 = "&nbsp;";
                    userModel.page7line4_9_12 = "&nbsp;";
                    userModel.page7line4_9_13 = "&nbsp;";

                    userModel.page7line6_4_1 = "&nbsp;";
                    userModel.page7line6_4_2 = "&nbsp;";
                    userModel.page7line6_4_3 = "&nbsp;";
                    userModel.page7line6_4_4 = "&nbsp;";
                    userModel.page7line6_4_5 = "&nbsp;";
                    userModel.page7line6_4_6 = "&nbsp;";
                    userModel.page7line6_4_7 = "&nbsp;";
                    userModel.page7line6_4_8 = "&nbsp;";
                    userModel.page7line6_4_9 = "&nbsp;";
                    userModel.page7line6_4_10 = "&nbsp;";
                    userModel.page7line6_4_11 = "&nbsp;";
                    userModel.page7line6_4_12 = "&nbsp;";
                    userModel.page7line6_4_13 = "&nbsp;";

                    userModel.page7line1_1 = "<b>1) ข้อมูลทั่วไป</b>";

                    userModel.page7line1_2a = "ชื่อ-นามสกุล(ภาษาไทย)";
                    userModel.page7line1_2b = usertitle + user.user.sName + " " + user.user.sLastname;
                    userModel.page7line1_2c = "ชื่อเล่น";
                    userModel.page7line1_2d = user.user.sNickName;
                    userModel.page7line1_2g = "ชั้น";
                    userModel.page7line1_2h = user.sview.SubLevel+"/"+user.sview.nTSubLevel2;
                    userModel.page7line1_2e = "หมู่โลหิต";
                    userModel.page7line1_2f = user.h?.sBlood;

                    userModel.page7line1_3a = "ชื่อ-นามสกุล(Eng)";
                    userModel.page7line1_3b = user.user.sStudentNameEN + " " + user.user.sStudentLastEN;
                    userModel.page7line1_3c = "ชื่อเล่น(Eng)";
                    userModel.page7line1_3d = user.user.sNickNameEN;
                    userModel.page7line1_3e = "ศาสนา";
                    userModel.page7line1_3f = studentReligion;

                    userModel.page7line1_4a = "เลขประจำตัวประชาชน";

                    if (user.user.sIdentification != "" && user.user.sIdentification != null)
                    {
                        if (user.user.sIdentification.Length == 13)
                        {
                            var iden = user.user.sIdentification;
                            userModel.page7line1_4txta1 = iden[0].ToString();
                            userModel.page7line1_4txta2 = iden[1].ToString();
                            userModel.page7line1_4txta3 = iden[2].ToString();
                            userModel.page7line1_4txta4 = iden[3].ToString();
                            userModel.page7line1_4txta5 = iden[4].ToString();
                            userModel.page7line1_4txta6 = iden[5].ToString();
                            userModel.page7line1_4txta7 = iden[6].ToString();
                            userModel.page7line1_4txta8 = iden[7].ToString();
                            userModel.page7line1_4txta9 = iden[8].ToString();
                            userModel.page7line1_4txta10 = iden[9].ToString();
                            userModel.page7line1_4txta11 = iden[10].ToString();
                            userModel.page7line1_4txta12 = iden[11].ToString();
                            userModel.page7line1_4txta13 = iden[12].ToString();
                        }
                    }

                    userModel.page7line1_4b = "เลขรหัสประจำบ้าน";
                    if (user.user.sStudentHomeRegisterCode != null && user.user.sStudentHomeRegisterCode != "")
                    {
                        if (user.user.sStudentHomeRegisterCode.Length == 11)
                        {
                            var code = user.user.sStudentHomeRegisterCode;
                            userModel.page7line1_4txtb1 = code[0].ToString();
                            userModel.page7line1_4txtb2 = code[1].ToString();
                            userModel.page7line1_4txtb3 = code[2].ToString();
                            userModel.page7line1_4txtb4 = code[3].ToString();
                            userModel.page7line1_4txtb5 = code[4].ToString();
                            userModel.page7line1_4txtb6 = code[5].ToString();
                            userModel.page7line1_4txtb7 = code[6].ToString();
                            userModel.page7line1_4txtb8 = code[7].ToString();
                            userModel.page7line1_4txtb9 = code[8].ToString();
                            userModel.page7line1_4txtb10 = code[9].ToString();
                            userModel.page7line1_4txtb11 = code[10].ToString();
                        }
                    }

                    userModel.page7line1_5a = "เกิดวันที่";
                    userModel.page7line1_5b = user.user.dBirth.Value.Day + "";
                    userModel.page7line1_5c = "เดือน";
                    userModel.page7line1_5d = monthtxt(user.user.dBirth.Value.Month);
                    userModel.page7line1_5e = "พ.ศ.";
                    userModel.page7line1_5f = user.user.dBirth.Value.AddYears(543).Year + "";
                    userModel.page7line1_5g = "เชื้อชาติ";
                    userModel.page7line1_5h = studentRace;
                    userModel.page7line1_5i = "สัญชาติ";
                    userModel.page7line1_5j = studentNation;
                    userModel.page7line1_5k = "ส่วนสูง";
                    userModel.page7line1_5l = user.g?.Height + "";
                    userModel.page7line1_5m = "ซม.";
                    userModel.page7line1_5n = "น้ำหนัก";
                    userModel.page7line1_5o = user.g?.Weight + "";
                    userModel.page7line1_5p = "กก.";

                    userModel.page7line1_6a = "จำนวนพี่น้องทั้งหมด(รวมนักเรียน)";
                    userModel.page7line1_6b = user.f?.nSonTotal.ToString();
                    userModel.page7line1_6c = "คน";
                    userModel.page7line1_6d = "มีพี่/น้องเรียนที่โรงเรียนแห่งนี้";
                    userModel.page7line1_6e = user.f?.nRelativeStudyHere.ToString();
                    userModel.page7line1_6f = "คน";
                    userModel.page7line1_6g = "นักเรียนเป็นบุตรคนที่";
                    userModel.page7line1_6h = user.user.nSonNumber.ToString();

                    userModel.page7line1_7a = "ประเภท";
                    userModel.page7line1_7b = "ไป-กลับ";

                    userModel.page7line1_7c = "ประจำหอพัก";
                    userModel.page7line1_7d = "หอพักนอก(ระบุ)";
                    userModel.page7line1_7e = "";

                    userModel.studentCategory1 = " fa-circle ";
                    userModel.studentCategory2 = " fa-circle ";
                    switch (user.user.JourneyType + "")
                    {
                        case "1":
                            userModel.studentCategory1 = " fa-check-circle ";
                            break;
                        case "2":
                            userModel.studentCategory2 = " fa-check-circle ";
                            break;
                    }

                    userModel.page7line1_7f = "โรคประจำตัว";
                    userModel.page7line1_7g = user.h?.sSickNormal;

                    userModel.page7line2_1a = "<b>2) ที่อยู่ปัจจุบัน</b>";
                    userModel.page7line2_1b = "บ้านเลขที่";
                    userModel.page7line2_1c = user.user.sStudentHomeNumber;
                    userModel.page7line2_1d = "หมู่";
                    userModel.page7line2_1e = user.user.sStudentMuu;
                    userModel.page7line2_1f = "ซอย";
                    userModel.page7line2_1g = user.user.sStudentSoy;
                    userModel.page7line2_1h = "ถนน";
                    userModel.page7line2_1i = user.user.sStudentRoad;
                    userModel.page7line2_1j = "ตำบล";
                    userModel.page7line2_1k = stdTumbon;

                    userModel.page7line2_2a = "อำเภอ";
                    userModel.page7line2_2b = stdAumpher;
                    userModel.page7line2_2c = "จังหวัด";
                    userModel.page7line2_2d = stdProvince;
                    userModel.page7line2_2e = "รหัสไปรษณีย์";
                    userModel.page7line2_2f = user.user.sStudentPost;
                    userModel.page7line2_2g = "โทรศัพท์";
                    userModel.page7line2_2h = user.user.sPhone;

                    var staytitle = titledb.Where(w => w.nTitleid == user.f?.stayWithTitle).FirstOrDefault();
                    string staywithtitle = "";
                    if (staytitle != null && user.f?.stayWithName != "")
                        staywithtitle = staytitle.titleDescription;

                    userModel.page7line2_3a = "พักอาศัยอยู่กับ";
                    userModel.page7line2_3b = staywithtitle + user.f?.stayWithName + " " + user.f?.stayWithLast;
                    userModel.page7line2_3c = "ที่ติดต่อฉุกเฉิน/โทรศัพท์";
                    userModel.page7line2_3d = user.f?.stayWithEmergencyCall;
                    userModel.page7line2_3e = "E-Mail";
                    userModel.page7line2_3f = user.f?.stayWithEmail;

                    userModel.page7line2_4a = "ลักษณะบ้านที่อยู่ปัจจุบัน";
                    userModel.page7line2_4b = "บ้านของตัวเอง";
                    userModel.page7line2_4c = "บ้านญาติ";
                    userModel.page7line2_4d = "บ้านเช่า";
                    userModel.page7line2_4e = "บ้านพักราชการ";
                    userModel.page7line2_4f = "หอพักโรงแรม";

                    userModel.page7line2_5a = "เพื่อนใกล้บ้าน";
                    userModel.page7line2_5b = user.f?.friendName + " " + user.f?.friendLastName;
                    userModel.page7line2_5c = "ชั้น";
                    if (user.f?.friendSubLevel != null && user.f?.friendSubLevel != 0 && user.f?.friendSubLevel != -1)
                    {
                        var sublv = rooms.Where(w => w.nTSubLevel == user.f?.friendSubLevel).FirstOrDefault();

                        if (sublv != null)
                        {
                            userModel.page7line2_5d = sublv.SubLevel;
                        }
                    }
                    else if (user.f?.friendSubLevel == -1)
                        userModel.page7line2_5d = "อื่นๆ";

                    userModel.page7line2_5e = "โทรศัพท์/มือถือ";
                    userModel.page7line2_5f = user.f?.friendPhone;

                    userModel.page7line3_1a = "<b>3) ภูมิลำเนา/ทะเบียนบ้าน</b>";

                    userModel.page7line3_1c = "บ้านเลขที่";
                    userModel.page7line3_1d = user.f?.houseRegistrationNumber;
                    userModel.page7line3_1e = "หมู่";
                    userModel.page7line3_1f = user.f?.houseRegistrationMuu;
                    userModel.page7line3_1g = "ซอย";
                    userModel.page7line3_1h = user.f?.houseRegistrationSoy;
                    userModel.page7line3_1i = "ถนน";
                    userModel.page7line3_1j = user.f?.houseRegistrationRoad;

                    if (user.f?.houseRegistrationTumbon != null)
                    {
                        var usertum = districList.Where(w => w.DISTRICT_ID == user.f?.houseRegistrationTumbon).FirstOrDefault();
                        if (usertum != null)
                        {
                            userModel.page7line3_2b = usertum.DISTRICT_NAME;
                        }
                    }

                    if (user.f?.houseRegistrationProvince != null)
                    {
                        var userpro = provincList.Where(w => w.PROVINCE_ID == user.f?.houseRegistrationProvince).FirstOrDefault();
                        if (userpro != null)
                        {
                            userModel.page7line3_2f = userpro.PROVINCE_NAME;
                        }
                    }

                    if (user.f?.houseRegistrationTumbon != null)
                    {
                        var useraum = amphursList.Where(w => w.AMPHUR_ID == user.f?.houseRegistrationAumpher).FirstOrDefault();
                        if (useraum != null)
                        {
                            userModel.page7line3_2d = useraum.AMPHUR_NAME;
                        }
                    }

                    userModel.page7line3_2a = "ตำบล";
                    userModel.page7line3_2c = "อำเภอ";
                    userModel.page7line3_2e = "จังหวัด";

                    userModel.page7line3_2g = "รหัสไปรษณีย์";
                    userModel.page7line3_2h = user.f?.houseRegistrationPost;
                    userModel.page7line3_2i = "โทรศัพท์";
                    userModel.page7line3_2j = user.f?.houseRegistrationPhone;

                    if (user.f?.bornFromTumbon != null)
                    {
                        var usertum = districList.Where(w => w.DISTRICT_ID == user.f?.bornFromTumbon).FirstOrDefault();
                        if (usertum != null)
                        {
                            userModel.page7line3_3d = usertum.DISTRICT_NAME;
                        }
                    }

                    if (user.f?.bornFromProvince != null)
                    {
                        var userpro = provincList.Where(w => w.PROVINCE_ID == user.f?.bornFromProvince).FirstOrDefault();
                        if (userpro != null)
                        {
                            userModel.page7line3_3h = userpro.PROVINCE_NAME;
                        }
                    }

                    if (user.f?.bornFromAumpher != null)
                    {
                        var useraum = amphursList.Where(w => w.AMPHUR_ID == user.f?.bornFromAumpher).FirstOrDefault();
                        if (useraum != null)
                        {
                            userModel.page7line3_3f = useraum.AMPHUR_NAME;
                        }
                    }

                    string age = "";
                    string age2 = "";
                    string age3 = "";

                    var today = DateTime.Today;
                    if (user.f?.dFatherBirthDay != null)
                    {
                        var cal1 = today.Year - user.f?.dFatherBirthDay.Value.Year;
                        if (cal1.HasValue)
                            if (user.f?.dFatherBirthDay.Value.Date > today.AddYears(-cal1.Value)) cal1--;
                        age = cal1.ToString();
                    }

                    if (user.f?.dMotherBirthDay != null)
                    {
                        var cal2 = today.Year - user.f?.dMotherBirthDay.Value.Year;
                        if (cal2.HasValue)
                            if (user.f?.dMotherBirthDay.Value.Date > today.AddYears(-cal2.Value)) cal2--;
                        age2 = cal2.ToString();
                    }

                    if (user.f?.dFamilyBirthDay != null)
                    {
                        var cal3 = today.Year - user.f?.dFamilyBirthDay.Value.Year;
                        if (cal3.HasValue)
                            if (user.f?.dFamilyBirthDay.Value.Date > today.AddYears(-cal3.Value)) cal3--;
                        age3 = cal3.ToString();
                    }

                    userModel.page7line3_3a = "สถานที่เกิด(ระบุที่เกิด)";
                    userModel.page7line3_3b = user.f?.bornFrom;
                    userModel.page7line3_3c = "ตำบล";
                    userModel.page7line3_3e = "อำเภอ";
                    userModel.page7line3_3g = "จังหวัด";

                    userModel.page7line4_1a = "<b>4) ประวัติครอบครัว</b>";

                    userModel.page7line4_2a = "ชื่อ-นามสกุลบิดา";
                    userModel.page7line4_2b = fathertitle + user.f?.sFatherFirstName + " " + user.f?.sFatherLastName;
                    userModel.page7line4_2c = "ว/ด/ปี-เกิด";
                    if (user.f?.dFatherBirthDay != null)
                        userModel.page7line4_2d = user.f?.dFatherBirthDay.Value.Day.ToString() + " " + monthtxt(user.f?.dFatherBirthDay.Value.Month) + " " + user.f?.dFatherBirthDay.Value.AddYears(543).Year.ToString();
                    userModel.page7line4_2e = "อายุ";
                    userModel.page7line4_2f = age.ToString();
                    userModel.page7line4_2g = "ปี";

                    userModel.page7line4_3a = "ชื่อ-นามสกุลบิดา(Eng)";
                    userModel.page7line4_3b = user.f?.sFatherNameEN + " " + user.f?.sFatherLastEN;
                    userModel.page7line4_3c = "เชื้อชาติ";

                    string fatherRace = "";
                    if (!string.IsNullOrEmpty(user.f?.sFatherRace))
                    {
                        fatherRace = masterDataList.Where(w => w.MasterType == "9" && w.MasterCode == user.f?.sFatherRace).FirstOrDefault().MasterDes;
                    }
                    userModel.page7line4_3d = fatherRace;
                    userModel.page7line4_3e = "สัญชาติ";
                    string fatherNation = "";
                    if (!string.IsNullOrEmpty(user.f?.sFatherNation))
                    {
                        fatherNation = masterDataList.Where(w => w.MasterType == "3" && w.MasterCode == user.f?.sFatherNation).FirstOrDefault().MasterDes;
                    }
                    userModel.page7line4_3f = fatherNation;
                    userModel.page7line4_3g = "ศาสนา";
                    string fatherReligion = "";
                    if (!string.IsNullOrEmpty(user.f?.sFatherReligion))
                    {
                        fatherReligion = masterDataList.Where(w => w.MasterType == "6" && w.MasterCode == user.f?.sFatherReligion).FirstOrDefault().MasterDes;
                    }
                    userModel.page7line4_3h = fatherReligion;

                    userModel.page7line4_4a = "เลขประจำตัวประชาชน";
                    userModel.page7line4_4b = "อาชีพ";
                    userModel.page7line4_4c = user.f?.sFatherJob;
                    userModel.page7line4_4d = "รายได้ต่อเดือน";

                    userModel.page7line4_5a = "ช่วงรายได้";
                    userModel.page7line4_5b = "ต่ำกว่า 150,000 บาท";
                    userModel.page7line4_5c = "150,000 - 300,000 บาท";
                    userModel.page7line4_5d = "มากกว่า 300,000 บาท";
                    userModel.page7line4_5e = "ไม่มีรายได้";

                    userModel.page7line4_6a = "ระดับการศึกษา";
                    userModel.page7line4_6b = educate(user.f?.sFatherGraduated);
                    if (user.f?.sFatherGraduated == 3 || user.f?.sFatherGraduated == 4)
                        userModel.educate13 = " f60 ";
                    else
                        userModel.educate13 = " f80 ";

                    userModel.page7line4_6c = "สถานที่ทำงาน";
                    userModel.page7line4_6d = user.f?.sFatherWorkPlace;
                    userModel.page7line4_6e = "โทรศัพท์ที่ทำงาน";
                    userModel.page7line4_6f = user.f?.sFatherPhone3;
                    userModel.page7line4_6g = "โทรศัพท์มือถือ";
                    userModel.page7line4_6h = user.f?.sFatherPhone2;

                    userModel.page7line4_7a = "ชื่อ-นามสกุลมารดา";
                    userModel.page7line4_7b = mothertitle + user.f?.sMotherFirstName + " " + user.f?.sMotherLastName;
                    userModel.page7line4_7c = "ว/ด/ป-เกิด";
                    if (user.f?.dMotherBirthDay != null)
                        userModel.page7line4_7d = user.f?.dMotherBirthDay.Value.Day.ToString() + " " + monthtxt(user.f?.dMotherBirthDay.Value.Month) + " " + user.f?.dMotherBirthDay.Value.AddYears(543).Year.ToString();
                    userModel.page7line4_7e = "อายุ";
                    userModel.page7line4_7f = age2.ToString();
                    userModel.page7line4_7g = "ปี";

                    userModel.page7line4_8a = "ชื่อ-นามสกุลมารดา(Eng)";
                    userModel.page7line4_8b = user.f?.sMotherNameEN + " " + user.f?.sMotherLastEN;
                    userModel.page7line4_8c = "เชื้อชาติ";
                    string motherRace = "";
                    if (!string.IsNullOrEmpty(user.f?.sMotherRace))
                    {
                        motherRace = masterDataList.Where(w => w.MasterType == "9" && w.MasterCode == user.f?.sMotherRace).FirstOrDefault().MasterDes;
                    }
                    userModel.page7line4_8d = motherRace;
                    userModel.page7line4_8e = "สัญชาติ";
                    string motherNation = "";
                    if (!string.IsNullOrEmpty(user.f?.sMotherNation))
                    {
                        motherNation = masterDataList.Where(w => w.MasterType == "3" && w.MasterCode == user.f?.sMotherNation).FirstOrDefault().MasterDes;
                    }
                    userModel.page7line4_8f = motherNation;
                    userModel.page7line4_8g = "ศาสนา";
                    string motherReligion = "";
                    if (!string.IsNullOrEmpty(user.f?.sMotherReligion))
                    {
                        motherReligion = masterDataList.Where(w => w.MasterType == "6" && w.MasterCode == user.f?.sMotherReligion).FirstOrDefault().MasterDes;
                    }
                    userModel.page7line4_8h = motherReligion;

                    userModel.page7line4_9a = "เลขประจำตัวประชาชน";
                    userModel.page7line4_9b = "อาชีพ";
                    userModel.page7line4_9c = user.f?.sMotherJob;
                    userModel.page7line4_9d = "รายได้ต่อเดือน";

                    userModel.page7line4_10a = "ช่วงรายได้";
                    userModel.page7line4_10b = "ต่ำกว่า 150,000 บาท";
                    userModel.page7line4_10c = "150,000 - 300,000 บาท";
                    userModel.page7line4_10d = "มากกว่า 300,000 บาท";
                    userModel.page7line4_10e = "ไม่มีรายได้";

                    userModel.page7line4_11a = "ระดับการศึกษา";
                    userModel.page7line4_11b = educate(user.f?.sMotherGraduated);
                    if (user.f?.sMotherGraduated == 3 || user.f?.sMotherGraduated == 4)
                        userModel.educate23 = " f60 ";
                    else
                        userModel.educate23 = " f80 ";
                    userModel.page7line4_11c = "สถานที่ทำงาน";
                    userModel.page7line4_11d = user.f?.sMotherWorkPlace;
                    userModel.page7line4_11e = "โทรศัพท์ที่ทำงาน";
                    userModel.page7line4_11f = user.f?.sMotherPhone3;
                    userModel.page7line4_11g = "โทรศัพท์มือถือ";
                    userModel.page7line4_11h = user.f?.sMotherPhone2;

                    userModel.page7line5_1a = "<b>5) สถานะครอบครัว</b>";
                    userModel.page7line5_1b = "บิดามารดาอยู่ด้วยกัน";
                    userModel.page7line5_1c = "บิดามารดาแยกกันอยู่";
                    userModel.page7line5_1d = "บิดามารดาหย่าร้าง";
                    userModel.page7line5_1e = "บิดาถึงแก่กรรม";

                    userModel.page7line5_2a = "มารดาถึงแก่กรรม";
                    userModel.page7line5_2b = "บิดามารดาถึงแก่กรรม";
                    userModel.page7line5_2c = "บิดามารดาแต่งงานใหม่";

                    userModel.page7line6_1a = "<b>6) ชื่อ-นามสกุลผู้ปกครอง</b>";

                    userModel.page7line6_1c = "บิดา";
                    userModel.page7line6_1d = "มารดา";
                    userModel.page7line6_1e = "ญาติ";
                    userModel.page7line6_1f = "";
                    userModel.page7line6_1g = "ขอเบิกค่าเล่าเรียน";
                    userModel.page7line6_1h = "เบิกได้";
                    userModel.page7line6_1i = "เบิกไม่ได้";

                    userModel.page7line6_2a = "ชื่อ-นามสกุลผู้ปกครอง";
                    userModel.page7line6_2b = familytitle + user.f?.sFamilyName + " " + user.f?.sFamilyLast;
                    userModel.page7line6_2c = "ว/ด/ปี-เกิด";
                    if (user.f?.dFamilyBirthDay != null)
                        userModel.page7line6_2d = user.f?.dFamilyBirthDay.Value.Day.ToString() + " " + monthtxt(user.f?.dFamilyBirthDay.Value.Month) + " " + user.f?.dFamilyBirthDay.Value.AddYears(543).Year.ToString();
                    userModel.page7line6_2e = "อายุ";
                    userModel.page7line6_2f = age3.ToString();
                    userModel.page7line6_2g = "ปี";

                    userModel.page7line6_3a = "ชื่อ-นามสกุลผู้ปกครอง(Eng)";
                    userModel.page7line6_3b = user.f?.sFamilyNameEN + " " + user.f?.sFamilyLastEN;
                    userModel.page7line6_3c = "เชื้อชาติ";
                    string familyRace = "";
                    if (!string.IsNullOrEmpty(user.f?.sFamilyRace))
                    {
                        familyRace = masterDataList.Where(w => w.MasterType == "9" && w.MasterCode == user.f?.sFamilyRace).FirstOrDefault().MasterDes;
                    }
                    userModel.page7line6_3d = familyRace;
                    userModel.page7line6_3e = "สัญชาติ";
                    string familyNation = "";
                    if (!string.IsNullOrEmpty(user.f?.sFamilyNation))
                    {
                        familyNation = masterDataList.Where(w => w.MasterType == "3" && w.MasterCode == user.f?.sFamilyNation).FirstOrDefault().MasterDes;
                    }
                    userModel.page7line6_3f = familyNation;
                    userModel.page7line6_3g = "ศาสนา";
                    string familyReligion = "";
                    if (!string.IsNullOrEmpty(user.f?.sFamilyReligion))
                    {
                        familyReligion = masterDataList.Where(w => w.MasterType == "6" && w.MasterCode == user.f?.sFamilyReligion).FirstOrDefault().MasterDes;
                    }
                    userModel.page7line6_3h = familyReligion;

                    userModel.page7line6_4a = "เลขประจำตัวประชาชน";
                    userModel.page7line6_4b = "อาชีพ";
                    userModel.page7line6_4c = user.f?.sFamilyJob;
                    userModel.page7line6_4d = "รายได้ต่อเดือน";
                    userModel.page7line6_4e = user.f?.nFamilyIncome?.ToString("#,0.#");

                    userModel.page7line6_5a = "ระดับการศึกษา";
                    userModel.page7line6_5b = educate(user.f?.sFamilyGraduated);
                    if (user.f?.sFamilyGraduated == 3 || user.f?.sFamilyGraduated == 4)
                        userModel.educate33 = " f60 ";
                    else
                        userModel.educate33 = " f80 ";

                    userModel.page7line6_5c = "สถานที่ทำงาน";
                    userModel.page7line6_5d = user.f?.sFamilyWorkPlace;
                    userModel.page7line6_5e = "โทรศัพท์ที่ทำงาน";
                    userModel.page7line6_5f = user.f?.sPhoneThree;
                    userModel.page7line6_5g = "โทรศัพท์มือถือ";
                    userModel.page7line6_5h = user.f?.sPhoneTwo;

                    userModel.page7line7_1a = "<b>7) ชื่อสถานศึกษาเดิมของนักเรียน</b>";
                    userModel.page7line7_1b = user.user.oldSchoolName;
                    userModel.page7line7_1c = "ตำบล";
                    userModel.page7line7_1d = oldtum;
                    userModel.page7line7_1e = "อำเภอ";
                    userModel.page7line7_1f = oldaum;

                    userModel.page7line7_2a = "จังหวัด";
                    userModel.page7line7_2b = oldpro;
                    userModel.page7line7_2c = "กำลังเรียน/จบชั้น";
                    userModel.page7line7_2d = oldgradu;
                    userModel.page7line7_2e = "ได้เกรดเฉลี่ย";
                    userModel.page7line7_2f = user.user.oldSchoolGPA2 + "";
                    userModel.page7line7_2g = "เหตุที่ย้าย";
                    userModel.page7line7_2h = user.user.moveOutReason;

                    userModel.page7foot1_1a = "ลงชื่อ";
                    userModel.page7foot1_1b = "";
                    userModel.page7foot1_1c = "ผู้ปกครอง";

                    userModel.page7foot1s_2a = "วันที่";
                    userModel.page7foot1s_2b = "";
                    userModel.page7foot1s_2c = "เดือน";
                    userModel.page7foot1s_2d = "";
                    userModel.page7foot1s_2e = "พ.ศ.";
                    userModel.page7foot1s_2f = "";

                    userModel.page7foot1s_2a = userModel.page7foot1_2a = "วันที่";
                    userModel.page7foot1s_2b = userModel.page7foot1_2b = "";
                    userModel.page7foot1s_2c = userModel.page7foot1_2c = "เดือน";
                    userModel.page7foot1s_2d = userModel.page7foot1_2d = "";
                    userModel.page7foot1s_2e = userModel.page7foot1_2e = "พ.ศ.";
                    userModel.page7foot1s_2f = userModel.page7foot1_2f = "";

                    if (user.f?.HomeType != null)
                    {
                        if (user.f?.HomeType == 1)
                            userModel.hometype13 = "fa-check-circle";
                        else if (user.f?.HomeType == 2)
                            userModel.hometype23 = "fa-check-circle";
                        else if (user.f?.HomeType == 3)
                            userModel.hometype33 = "fa-check-circle";
                        else if (user.f?.HomeType == 4)
                            userModel.hometype43 = "fa-check-circle";
                        else if (user.f?.HomeType == 5)
                            userModel.hometype53 = "fa-check-circle";
                    }

                    if (user.f?.nFatherIncome != null)
                    {
                        int n2 = (int)user.f?.nFatherIncome;
                        if (n2 == 0)
                            userModel.fatherincome43 = "fa-check-circle";
                        else if (n2 < 12500)
                            userModel.fatherincome13 = "fa-check-circle";
                        else if (n2 <= 25000)
                            userModel.fatherincome23 = "fa-check-circle";
                        else if (n2 > 25000)
                            userModel.fatherincome33 = "fa-check-circle";

                        userModel.page7line4_4e = n2.ToString("#,0.#");
                    }

                    if (user.f?.nMotherIncome != null)
                    {
                        int n2 = (int)user.f?.nMotherIncome;
                        if (n2 == 0)
                            userModel.motherincome43 = "fa-check-circle";
                        else if (n2 < 12500)
                            userModel.motherincome13 = "fa-check-circle";
                        else if (n2 <= 25000)
                            userModel.motherincome23 = "fa-check-circle";
                        else if (n2 > 25000)
                            userModel.motherincome33 = "fa-check-circle";

                        userModel.page7line4_9e = n2.ToString("#,0.#");
                    }

                    if (user.f?.familyStatus != null)
                    {
                        if (user.f?.familyStatus == 1)
                            userModel.famstatus13 = "fa-check-circle";
                        else if (user.f?.familyStatus == 2)
                            userModel.famstatus23 = "fa-check-circle";
                        else if (user.f?.familyStatus == 3)
                            userModel.famstatus33 = "fa-check-circle";
                        else if (user.f?.familyStatus == 4)
                            userModel.famstatus43 = "fa-check-circle";
                        else if (user.f?.familyStatus == 5)
                            userModel.famstatus53 = "fa-check-circle";
                        else if (user.f?.familyStatus == 6)
                            userModel.famstatus63 = "fa-check-circle";
                        else if (user.f?.familyStatus == 7)
                            userModel.famstatus73 = "fa-check-circle";
                    }

                    if (user.f?.sFatherIdCardNumber != "" && user.f?.sFatherIdCardNumber != null)
                    {
                        if (user.f?.sFatherIdCardNumber.Length == 13)
                        {
                            var num = user.f?.sFatherIdCardNumber;
                            userModel.page7line4_4_1 = num[0].ToString();
                            userModel.page7line4_4_2 = num[1].ToString();
                            userModel.page7line4_4_3 = num[2].ToString();
                            userModel.page7line4_4_4 = num[3].ToString();
                            userModel.page7line4_4_5 = num[4].ToString();
                            userModel.page7line4_4_6 = num[5].ToString();
                            userModel.page7line4_4_7 = num[6].ToString();
                            userModel.page7line4_4_8 = num[7].ToString();
                            userModel.page7line4_4_9 = num[8].ToString();
                            userModel.page7line4_4_10 = num[9].ToString();
                            userModel.page7line4_4_11 = num[10].ToString();
                            userModel.page7line4_4_12 = num[11].ToString();
                            userModel.page7line4_4_13 = num[12].ToString();
                        }
                    }

                    if (user.f?.sMotherIdCardNumber != "" && user.f?.sMotherIdCardNumber != null)
                    {
                        if (user.f?.sMotherIdCardNumber.Length == 13)
                        {
                            var num = user.f?.sMotherIdCardNumber;
                            userModel.page7line4_9_1 = num[0].ToString();
                            userModel.page7line4_9_2 = num[1].ToString();
                            userModel.page7line4_9_3 = num[2].ToString();
                            userModel.page7line4_9_4 = num[3].ToString();
                            userModel.page7line4_9_5 = num[4].ToString();
                            userModel.page7line4_9_6 = num[5].ToString();
                            userModel.page7line4_9_7 = num[6].ToString();
                            userModel.page7line4_9_8 = num[7].ToString();
                            userModel.page7line4_9_9 = num[8].ToString();
                            userModel.page7line4_9_10 = num[9].ToString();
                            userModel.page7line4_9_11 = num[10].ToString();
                            userModel.page7line4_9_12 = num[11].ToString();
                            userModel.page7line4_9_13 = num[12].ToString();
                        }
                    }

                    if (user.f?.sFamilyIdCardNumber != "" && user.f?.sFamilyIdCardNumber != null)
                    {
                        if (user.f?.sFamilyIdCardNumber.Length == 13)
                        {
                            var num = user.f?.sFamilyIdCardNumber;
                            userModel.page7line6_4_1 = num[0].ToString();
                            userModel.page7line6_4_2 = num[1].ToString();
                            userModel.page7line6_4_3 = num[2].ToString();
                            userModel.page7line6_4_4 = num[3].ToString();
                            userModel.page7line6_4_5 = num[4].ToString();
                            userModel.page7line6_4_6 = num[5].ToString();
                            userModel.page7line6_4_7 = num[6].ToString();
                            userModel.page7line6_4_8 = num[7].ToString();
                            userModel.page7line6_4_9 = num[8].ToString();
                            userModel.page7line6_4_10 = num[9].ToString();
                            userModel.page7line6_4_11 = num[10].ToString();
                            userModel.page7line6_4_12 = num[11].ToString();
                            userModel.page7line6_4_13 = num[12].ToString();
                        }
                    }

                    if (user.f?.sFamilyRelate != null)
                    {
                        switch (user.f?.sFamilyRelate.Trim())
                        {
                            case "บิดา":
                            case "พ่อ": userModel.famrelate13 = "fa-check-circle"; break;
                            case "มารดา":
                            case "แม่": userModel.famrelate23 = "fa-check-circle"; break;
                            case "ญาติ": userModel.famrelate33 = "fa-check-circle"; break;
                            default: userModel.famrelate33 = "fa-check-circle"; break;
                        }
                    }

                    if (user.f?.nFamilyRequestStudyMoney != null)
                    {
                        if (user.f?.nFamilyRequestStudyMoney == 1)
                            userModel.requestmoney13 = "fa-check-circle";
                        else if (user.f?.nFamilyRequestStudyMoney == 0)
                            userModel.requestmoney23 = "fa-check-circle";
                    }

                    PageModel.Users.Add(userModel);
                }
            }
        }

        string educate(int? index)
        {
            var familyGraduated = "";
            switch (index)
            {
                case 1: familyGraduated = "ต่ำกว่าประถม"; break;
                case 2: familyGraduated = "ประถมศึกษา"; break;
                case 7: familyGraduated = "มัธยมต้น"; break;
                case 3: familyGraduated = "มัธยมศึกษาหรือเทียบเท่า"; break;
                case 9: familyGraduated = "ประกาศนียบัตรวิชาชีพ"; break;
                case 10: familyGraduated = "ประกาศนียบัตรวิชาชีพชั้นสูง"; break;
                case 8: familyGraduated = "อนุปริญญา"; break;
                case 4: familyGraduated = "ปริญญาตรี"; break;
                case 5: familyGraduated = "ปริญญาโท"; break;
                case 6: familyGraduated = "ปริญญาเอก"; break;
                default: familyGraduated = ""; break;
            }
            return familyGraduated;
         
        }

        string monthtxt(int? month)
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

    public class PageModel
    {
        //public List<JabjaiEntity.DB.TUser> User { get; set; }
        public string p1header1 { get; internal set; }
        public string p1header2 { get; internal set; }
        public string p1header3 { get; internal set; }
        public string p2header1 { get; internal set; }
        public string p2header2 { get; internal set; }
        public string p2header3 { get; internal set; }
        public string attachline1 { get; internal set; }
        public string attachline2 { get; internal set; }
        public string attachline3 { get; internal set; }
        public string attachline4 { get; internal set; }
        public string attachline5 { get; internal set; }
        public string attachline6 { get; internal set; }
        public string attachline7 { get; internal set; }
        public string bottomright1 { get; internal set; }
        public string bottomright2 { get; internal set; }
        public string bottomright3 { get; internal set; }

        public List<UserModel> Users { get; set; }
        public string Label33 { get; internal set; }
        public string Label13 { get; internal set; }
        public string Label23 { get; internal set; }
        public string lblLevel { get; internal set; }
        public string lblPlan { get; internal set; }
        public string Img3 { get; internal set; }
    }


    public class UserModel
    {
        public string userID { get; internal set; }
        public string userID2 { get; internal set; }
        public string userBirth { get; internal set; }
        public string userBirth2 { get; internal set; }
        public string userName { get; internal set; }
        public string userName2 { get; internal set; }
        public string userRace { get; internal set; }
        public string userRace2 { get; internal set; }
        public string userNation { get; internal set; }
        public string userNation2 { get; internal set; }
        public string userReligext { get; internal set; }
        public string userRelig2 { get; internal set; }
        public string userEmail2 { get; internal set; }
        public string userEmail { get; internal set; }
        public string userPhone { get; internal set; }
        public string userPhone2 { get; internal set; }
        public string userRok { get; internal set; }
        public string userRok2 { get; internal set; }
        public string userDrug { get; internal set; }
        public string userDrug2 { get; internal set; }
        public string userWeight { get; internal set; }
        public string userWeight2 { get; internal set; }
        public string userHeight { get; internal set; }
        public string userHeight2 { get; internal set; }
        public string userHomenum { get; internal set; }
        public string userHomenum2 { get; internal set; }
        public string userMuu { get; internal set; }
        public string userMuu2 { get; internal set; }
        public string userProvince { get; internal set; }
        public string userPost { get; internal set; }
        public string userPost2 { get; internal set; }
        public string page7line1_4txta1 { get; internal set; }
        public string page7line1_4txta2 { get; internal set; }
        public string page7line1_4txta3 { get; internal set; }
        public string page7line1_4txta4 { get; internal set; }
        public string page7line1_4txta5 { get; internal set; }
        public string page7line1_4txta6 { get; internal set; }
        public string page7line1_4txta7 { get; internal set; }
        public string page7line1_4txta8 { get; internal set; }
        public string page7line1_4txta9 { get; internal set; }
        public string page7line1_4txta10 { get; internal set; }
        public string page7line1_4txta11 { get; internal set; }
        public string page7line1_4txta12 { get; internal set; }
        public string page7line1_4txta13 { get; internal set; }
        public string page7line1_4txtb1 { get; internal set; }
        public string page7line1_4txtb2 { get; internal set; }
        public string page7line1_4txtb3 { get; internal set; }
        public string page7line1_4txtb4 { get; internal set; }
        public string page7line1_4txtb5 { get; internal set; }
        public string page7line1_4txtb6 { get; internal set; }
        public string page7line1_4txtb7 { get; internal set; }
        public string page7line1_4txtb8 { get; internal set; }
        public string page7line1_4txtb9 { get; internal set; }
        public string page7line1_4txtb10 { get; internal set; }
        public string page7line1_4txtb11 { get; internal set; }
        public string page7line4_4_1 { get; internal set; }
        public string page7line4_4_2 { get; internal set; }
        public string page7line4_4_3 { get; internal set; }
        public string page7line4_4_4 { get; internal set; }
        public string page7line4_4_5 { get; internal set; }
        public string page7line4_4_6 { get; internal set; }
        public string page7line4_4_7 { get; internal set; }
        public string page7line4_4_8 { get; internal set; }
        public string page7line4_4_9 { get; internal set; }
        public string page7line4_4_10 { get; internal set; }
        public string page7line4_4_11 { get; internal set; }
        public string page7line4_4_12 { get; internal set; }
        public string page7line4_4_13 { get; internal set; }
        public string page7line4_9_1 { get; internal set; }
        public string page7line4_9_2 { get; internal set; }
        public string page7line4_9_3 { get; internal set; }
        public string page7line4_9_4 { get; internal set; }
        public string page7line4_9_5 { get; internal set; }
        public string page7line4_9_6 { get; internal set; }
        public string page7line4_9_7 { get; internal set; }
        public string page7line4_9_8 { get; internal set; }
        public string page7line4_9_9 { get; internal set; }
        public string page7line4_9_10 { get; internal set; }
        public string page7line4_9_11 { get; internal set; }
        public string page7line4_9_12 { get; internal set; }
        public string page7line4_9_13 { get; internal set; }
        public string page7line6_4_1 { get; internal set; }
        public string page7line6_4_2 { get; internal set; }
        public string page7line6_4_3 { get; internal set; }
        public string page7line6_4_4 { get; internal set; }
        public string page7line6_4_5 { get; internal set; }
        public string page7line6_4_6 { get; internal set; }
        public string page7line6_4_7 { get; internal set; }
        public string page7line6_4_8 { get; internal set; }
        public string page7line6_4_9 { get; internal set; }
        public string page7line6_4_10 { get; internal set; }
        public string page7line6_4_11 { get; internal set; }
        public string page7line6_4_12 { get; internal set; }
        public string page7line6_4_13 { get; internal set; }
        public string page7line1_1 { get; internal set; }
        public string page7line1_2a { get; internal set; }
        public string page7line1_2b { get; internal set; }
        public string page7line1_2c { get; internal set; }
        public string page7line1_2d { get; internal set; }
        public string page7line1_2e { get; internal set; }
        public string page7line1_2f { get; internal set; }
        public string page7line1_3a { get; internal set; }
        public string page7line1_3b { get; internal set; }
        public string page7line1_3c { get; internal set; }
        public string page7line1_3d { get; internal set; }
        public string page7line1_3e { get; internal set; }
        public string page7line1_3f { get; internal set; }
        public string page7line1_4a { get; internal set; }
        public string page7line1_4b { get; internal set; }
        public string page7line1_5a { get; internal set; }
        public string page7line1_5b { get; internal set; }
        public string page7line1_5c { get; internal set; }
        public string page7line1_5d { get; internal set; }
        public string page7line1_5e { get; internal set; }
        public string page7line1_5f { get; internal set; }
        public string page7line1_5g { get; internal set; }
        public string page7line1_5h { get; internal set; }
        public string page7line1_5i { get; internal set; }
        public string page7line1_5j { get; internal set; }
        public string page7line1_5k { get; internal set; }
        public string page7line1_5l { get; internal set; }
        public string page7line1_5m { get; internal set; }
        public string page7line1_5n { get; internal set; }
        public string page7line1_5o { get; internal set; }
        public string page7line1_5p { get; internal set; }
        public string page7line1_6a { get; internal set; }
        public string page7line1_6b { get; internal set; }
        public string page7line1_6c { get; internal set; }
        public string page7line1_6d { get; internal set; }
        public string page7line1_6e { get; internal set; }
        public string page7line1_6f { get; internal set; }
        public string page7line1_6g { get; internal set; }
        public string page7line1_6h { get; internal set; }
        public string page7line1_7a { get; internal set; }
        public string page7line1_7b { get; internal set; }
        public string page7line1_7c { get; internal set; }
        public string page7line1_7d { get; internal set; }
        public string page7line1_7e { get; internal set; }
        public string studentCategory1 { get; internal set; }
        public string studentCategory2 { get; internal set; }
        public string page7line1_7f { get; internal set; }
        public string page7line1_7g { get; internal set; }
        public string page7line2_1a { get; internal set; }
        public string page7line2_1b { get; internal set; }
        public string page7line2_1c { get; internal set; }
        public string page7line2_1d { get; internal set; }
        public string page7line2_1e { get; internal set; }
        public string page7line2_1f { get; internal set; }
        public string page7line2_1g { get; internal set; }
        public string page7line2_1h { get; internal set; }
        public string page7line2_1i { get; internal set; }
        public string page7line2_1j { get; internal set; }
        public string page7line2_1k { get; internal set; }
        public string page7line2_2a { get; internal set; }
        public string page7line2_2b { get; internal set; }
        public string page7line2_2c { get; internal set; }
        public string page7line2_2d { get; internal set; }
        public string page7line2_2e { get; internal set; }
        public string page7line2_2f { get; internal set; }
        public string page7line2_2g { get; internal set; }
        public string page7line2_2h { get; internal set; }
        public string page7line2_3a { get; internal set; }
        public string page7line2_3b { get; internal set; }
        public string page7line2_3c { get; internal set; }
        public string page7line2_3d { get; internal set; }
        public string page7line2_3e { get; internal set; }
        public string page7line2_3f { get; internal set; }
        public string page7line2_4a { get; internal set; }
        public string page7line2_4b { get; internal set; }
        public string page7line2_4c { get; internal set; }
        public string page7line2_4d { get; internal set; }
        public string page7line2_4e { get; internal set; }
        public string page7line2_4f { get; internal set; }
        public string page7line2_5a { get; internal set; }
        public string page7line2_5b { get; internal set; }
        public string page7line2_5c { get; internal set; }
        public string page7line2_5d { get; internal set; }
        public string page7line2_5e { get; internal set; }
        public string page7line2_5f { get; internal set; }
        public string page7line3_1a { get; internal set; }
        public string page7line3_1c { get; internal set; }
        public string page7line3_1d { get; internal set; }
        public string page7line3_1e { get; internal set; }
        public string page7line3_1f { get; internal set; }
        public string page7line3_1g { get; internal set; }
        public string page7line3_1h { get; internal set; }
        public string page7line3_1i { get; internal set; }
        public string page7line3_1j { get; internal set; }
        public string page7line3_2b { get; internal set; }
        public string page7line3_2f { get; internal set; }
        public string page7line3_2d { get; internal set; }
        public string page7line3_2a { get; internal set; }
        public string page7line3_2c { get; internal set; }
        public string page7line3_2e { get; internal set; }
        public string page7line3_2g { get; internal set; }
        public string page7line3_2h { get; internal set; }
        public string page7line3_2i { get; internal set; }
        public string page7line3_2j { get; internal set; }
        public string page7line3_3d { get; internal set; }
        public string page7line3_3h { get; internal set; }
        public string page7line3_3f { get; internal set; }
        public string page7line3_3a { get; internal set; }
        public string page7line3_3b { get; internal set; }
        public string page7line3_3c { get; internal set; }
        public string page7line3_3e { get; internal set; }
        public string page7line3_3g { get; internal set; }
        public string page7line4_1a { get; internal set; }
        public string page7line4_2a { get; internal set; }
        public string page7line4_2b { get; internal set; }
        public string page7line4_2c { get; internal set; }
        public string page7line4_2d { get; internal set; }
        public string page7line4_2e { get; internal set; }
        public string page7line4_2f { get; internal set; }
        public string page7line4_2g { get; internal set; }
        public string page7line4_3a { get; internal set; }
        public string page7line4_3b { get; internal set; }
        public string page7line4_3c { get; internal set; }
        public string page7line4_3d { get; internal set; }
        public string page7line4_3e { get; internal set; }
        public string page7line4_3f { get; internal set; }
        public string page7line4_3g { get; internal set; }
        public string page7line4_3h { get; internal set; }
        public string page7line4_4a { get; internal set; }
        public string page7line4_4b { get; internal set; }
        public string page7line4_4c { get; internal set; }
        public string page7line4_4d { get; internal set; }
        public string page7line4_5a { get; internal set; }
        public string page7line4_5b { get; internal set; }
        public string page7line4_5c { get; internal set; }
        public string page7line4_5d { get; internal set; }
        public string page7line4_5e { get; internal set; }
        public string page7line4_6a { get; internal set; }
        public string page7line4_6b { get; internal set; }
        public string educate13 { get; internal set; }
        public string page7line4_6c { get; internal set; }
        public string page7line4_6d { get; internal set; }
        public string page7line4_6e { get; internal set; }
        public string page7line4_6f { get; internal set; }
        public string page7line4_6g { get; internal set; }
        public string page7line4_6h { get; internal set; }
        public string page7line4_7a { get; internal set; }
        public string page7line4_7b { get; internal set; }
        public string page7line4_7c { get; internal set; }
        public string page7line4_7d { get; internal set; }
        public string page7line4_7e { get; internal set; }
        public string page7line4_7f { get; internal set; }
        public string page7line4_7g { get; internal set; }
        public string page7line4_8a { get; internal set; }
        public string page7line4_8b { get; internal set; }
        public string page7line4_8c { get; internal set; }
        public string page7line4_8d { get; internal set; }
        public string page7line4_8e { get; internal set; }
        public string page7line4_8f { get; internal set; }
        public string page7line4_8g { get; internal set; }
        public string page7line4_8h { get; internal set; }
        public string page7line4_9a { get; internal set; }
        public string page7line4_9b { get; internal set; }
        public string page7line4_9c { get; internal set; }
        public string page7line4_9d { get; internal set; }
        public string page7line4_10a { get; internal set; }
        public string page7line4_10b { get; internal set; }
        public string page7line4_10c { get; internal set; }
        public string page7line4_10d { get; internal set; }
        public string page7line4_10e { get; internal set; }
        public string page7line4_11a { get; internal set; }
        public string page7line4_11b { get; internal set; }
        public string educate23 { get; internal set; }
        public string page7line4_11c { get; internal set; }
        public string page7line4_11d { get; internal set; }
        public string page7line4_11e { get; internal set; }
        public string page7line4_11f { get; internal set; }
        public string page7line4_11g { get; internal set; }
        public string page7line4_11h { get; internal set; }
        public string page7line5_1a { get; internal set; }
        public string page7line5_1b { get; internal set; }
        public string page7line5_1c { get; internal set; }
        public string page7line5_1d { get; internal set; }
        public string page7line5_1e { get; internal set; }
        public string page7line5_2a { get; internal set; }
        public string page7line5_2b { get; internal set; }
        public string page7line5_2c { get; internal set; }
        public string page7line6_1a { get; internal set; }
        public string page7line6_1c { get; internal set; }
        public string page7line6_1d { get; internal set; }
        public string page7line6_1e { get; internal set; }
        public string page7line6_1f { get; internal set; }
        public string page7line6_1g { get; internal set; }
        public string page7line6_1h { get; internal set; }
        public string page7line6_1i { get; internal set; }
        public string page7line6_2a { get; internal set; }
        public string page7line6_2b { get; internal set; }
        public string page7line6_2c { get; internal set; }
        public string page7line6_2d { get; internal set; }
        public string page7line6_2e { get; internal set; }
        public string page7line6_2f { get; internal set; }
        public string page7line6_2g { get; internal set; }
        public string page7line6_3a { get; internal set; }
        public string page7line6_3b { get; internal set; }
        public string page7line6_3c { get; internal set; }
        public string page7line6_3d { get; internal set; }
        public string page7line6_3e { get; internal set; }
        public string page7line6_3f { get; internal set; }
        public string page7line6_3g { get; internal set; }
        public string page7line6_3h { get; internal set; }
        public string page7line6_4a { get; internal set; }
        public string page7line6_4b { get; internal set; }
        public string page7line6_4c { get; internal set; }
        public string page7line6_4d { get; internal set; }
        public string page7line6_4e { get; internal set; }
        public string page7line6_5a { get; internal set; }
        public string page7line6_5b { get; internal set; }
        public string educate33 { get; internal set; }
        public string page7line6_5c { get; internal set; }
        public string page7line6_5d { get; internal set; }
        public string page7line6_5e { get; internal set; }
        public string page7line6_5f { get; internal set; }
        public string page7line6_5g { get; internal set; }
        public string page7line6_5h { get; internal set; }
        public string page7line7_1a { get; internal set; }
        public string page7line7_1b { get; internal set; }
        public string page7line7_1c { get; internal set; }
        public string page7line7_1d { get; internal set; }
        public string page7line7_1e { get; internal set; }
        public string page7line7_1f { get; internal set; }
        public string page7line7_2a { get; internal set; }
        public string page7line7_2b { get; internal set; }
        public string page7line7_2c { get; internal set; }
        public string page7line7_2d { get; internal set; }
        public string page7line7_2e { get; internal set; }
        public string page7line7_2f { get; internal set; }
        public string page7line7_2g { get; internal set; }
        public string page7line7_2h { get; internal set; }
        public string page7foot1_1a { get; internal set; }
        public string page7foot1_1b { get; internal set; }
        public string page7foot1_1c { get; internal set; }
        public string page7foot1s_2a { get; internal set; }
        public string page7foot1s_2b { get; internal set; }
        public string page7foot1s_2c { get; internal set; }
        public string page7foot1s_2d { get; internal set; }
        public string page7foot1s_2e { get; internal set; }
        public string page7foot1s_2f { get; internal set; }
        public string hometype13 { get; internal set; }
        public string hometype23 { get; internal set; }
        public string hometype33 { get; internal set; }
        public string hometype43 { get; internal set; }
        public string hometype53 { get; internal set; }
        public string fatherincome43 { get; internal set; }
        public string fatherincome13 { get; internal set; }
        public string fatherincome23 { get; internal set; }
        public string fatherincome33 { get; internal set; }
        public string page7line4_4e { get; internal set; }
        public string page7line4_9e { get; internal set; }
        public string motherincome43 { get; internal set; }
        public string motherincome13 { get; internal set; }
        public string motherincome23 { get; internal set; }
        public string motherincome33 { get; internal set; }
        public string famstatus13 { get; internal set; }
        public string famstatus23 { get; internal set; }
        public string famstatus33 { get; internal set; }
        public string famstatus43 { get; internal set; }
        public string famstatus53 { get; internal set; }
        public string famstatus63 { get; internal set; }
        public string famstatus73 { get; internal set; }
        public string famrelate13 { get; internal set; }
        public string famrelate23 { get; internal set; }
        public string famrelate33 { get; internal set; }
        public string requestmoney13 { get; internal set; }
        public string requestmoney23 { get; internal set; }
        public string Label33 { get; internal set; }
        public string Label13 { get; internal set; }
        public string Image { get; internal set; }
        public string page7foot1_2a { get; internal set; }
        public string page7foot1_2b { get; internal set; }
        public string page7foot1_2c { get; internal set; }
        public string page7foot1_2d { get; internal set; }
        public string page7foot1_2e { get; internal set; }
        public string page7foot1_2f { get; internal set; }
        public string studentID { get; internal set; }
        public string page7line1_2g { get; internal set; }
        public string page7line1_2h { get; internal set; }
    }
}