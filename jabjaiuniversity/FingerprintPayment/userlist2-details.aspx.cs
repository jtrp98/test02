using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using JabjaiEntity.DB;
using MasterEntity;
using System.Globalization;

namespace FingerprintPayment
{
    public partial class userlist2_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();


            int id = 0;
            Int32.TryParse(Request.QueryString["id"], out id);
            var data3 = _db.TFamilyProfiles.Where(w => w.sID == id && w.sDeleted == "false").FirstOrDefault();
            var data4 = _db.THealtProfiles.Where(w => w.sID == id && w.sDeleted == "false").FirstOrDefault();
            Image img = (Image)FindControl("profileimage");
            var data5 = _db.TUsers.Where(w => w.sID == id && w.cDel == null).FirstOrDefault();

            profileimage.ImageUrl = data5.sStudentPicture;
            if (profileimage.ImageUrl == "")
            {
                profileimage.ImageUrl = "https://jabjaistorage.blob.core.windows.net/userprofile/201782151010735704913.png";
                profileimage.Width = 180;
                profileimage.Height = 180;
            }

            if (data5.sIdentification == null)
                TextBox65.Text = "";
            else TextBox65.Text = data5.sStudentID;


            //if (data5.oldSchoolGPA != null)
            //{
            //    oldSchoolGPA.Text = data5.oldSchoolGPA.ToString();
            //}

            oldSchoolGPA.Text = data5.oldSchoolGPA2;

            if (data5.oldSchoolGraduated != null)
            {
                if (data5.oldSchoolGraduated == "1")
                    oldSchoolGraduated.Text = "ประถมศึกษาปีที่ 1";
                else if (data5.oldSchoolGraduated == "2")
                    oldSchoolGraduated.Text = "ประถมศึกษาปีที่ 2";
                else if (data5.oldSchoolGraduated == "3")
                    oldSchoolGraduated.Text = "ประถมศึกษาปีที่ 3";
                else if (data5.oldSchoolGraduated == "4")
                    oldSchoolGraduated.Text = "ประถมศึกษาปีที่ 4";
                else if (data5.oldSchoolGraduated == "5")
                    oldSchoolGraduated.Text = "ประถมศึกษาปีที่ 5";
                else if (data5.oldSchoolGraduated == "6")
                    oldSchoolGraduated.Text = "ประถมศึกษาปีที่ 6";
                else if (data5.oldSchoolGraduated == "7")
                    oldSchoolGraduated.Text = "มัธยมศึกษาปีที่ 3";
                else if (data5.oldSchoolGraduated == "8")
                    oldSchoolGraduated.Text = "มัธยมศึกษาปีที่ 6";
                else if (data5.oldSchoolGraduated == "9")
                    oldSchoolGraduated.Text = "ปวช.";
                else if (data5.oldSchoolGraduated == "10")
                    oldSchoolGraduated.Text = "ปวส.";
            }
            var qaumhurs = dbmaster.amphurs.ToList();
            var qdistricts = dbmaster.districts.ToList();
            var qprovinces = dbmaster.provinces.ToList();

            if (data5.oldSchoolName != null)
            {
                oldSchoolName.Text = data5.oldSchoolName;
            }

            if (!string.IsNullOrEmpty(data5.sStudentAumpher)) StudentAumpher.Text = getAMPHUR_NAME(data5.sStudentAumpher, qaumhurs);
            if (!string.IsNullOrEmpty(data5.sStudentTumbon)) StudentTumbon.Text = getDISTRICT_NAME(data5.sStudentTumbon, qdistricts);
            if (!string.IsNullOrEmpty(data5.sStudentProvince)) StudentProvince.Text = getPROVINCE_NAME(data5.sStudentProvince, qprovinces);

            if (!string.IsNullOrEmpty(data5.oldSchoolAumpher)) oldSchoolAumpher.Text = getAMPHUR_NAME(data5.oldSchoolAumpher, qaumhurs);
            if (!string.IsNullOrEmpty(data5.oldSchoolTumbon)) oldSchoolTumbon.Text = getDISTRICT_NAME(data5.oldSchoolTumbon, qdistricts);
            if (!string.IsNullOrEmpty(data5.oldSchoolProvince)) oldSchoolProvince.Text = getPROVINCE_NAME(data5.oldSchoolProvince, qprovinces);

            int nTitleid;
            int.TryParse(data5.sStudentTitle, out nTitleid);
            var f_Title = _db.TTitleLists.FirstOrDefault(f => f.nTitleid == nTitleid || f.titleDescription == data5.sStudentTitle);

            var q_titlle = _db.TTitleLists.ToList();

            TextBox12.Text = f_Title == null ? "" : f_Title.titleDescription;
            TextBox1.Text = data5.sName;
            TextBox7.Text = data5.sLastname;
            TextBox21.Text = data5.sStudentNameEN;
            TextBox22.Text = data5.sStudentLastEN;
            TextBox64.Text = data5.sNickName;
            TextBox2.Text = data5.sStudentRace;
            TextBox5.Text = data5.sStudentNation;
            TextBox4.Text = data5.sStudentReligion;
            TextBox6.Text = data5.sIdentification;
            if (data5.nSonNumber.HasValue)
            {
                TextBox63.Text = data5.nSonNumber.ToString();
            }
            string gender = "";
            if (data5.cSex == "0")
                gender = "ชาย";
            else gender = "หญิง";

            TextBox60.Text = gender;
            TextBox15.Text = data5.dBirth.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th"));
            TextBox8.Text = data5.sStudentHomeNumber;
            TextBox14.Text = data5.sStudentSoy;
            TextBox3.Text = data5.sStudentMuu;
            TextBox10.Text = data5.sStudentRoad;
            TextBox20.Text = data5.sStudentPost;
            TextBox13.Text = data5.sPhone;
            TextBox45.Text = data5.sEmail;
            txtStudentNumber.Text = data5.nStudentNumber.ToString();

            var roomdata = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == data5.nTermSubLevel2).FirstOrDefault();
            var roomdata2 = _db.TSubLevels.Where(w => w.nTSubLevel == roomdata.nTSubLevel).FirstOrDefault();
            TextBox16.Text = roomdata2.SubLevel;
            TextBox61.Text = roomdata.nTSubLevel2.ToString();

            if (data3 != null)
            {

                if (!string.IsNullOrEmpty(data3.sFamilyAumpher)) famAumpher.Text = getAMPHUR_NAME(data3.sFamilyAumpher, qaumhurs);
                if (!string.IsNullOrEmpty(data3.sFamilyTumbon)) famTumbon.Text = getDISTRICT_NAME(data3.sFamilyTumbon, qdistricts);
                if (!string.IsNullOrEmpty(data3.sFamilyAumpher)) famProvince.Text = getPROVINCE_NAME(data3.sFamilyProvince, qprovinces);

                if (!string.IsNullOrEmpty(data3.sFatherAumpher)) fatherAumpher.Text = getAMPHUR_NAME(data3.sFatherAumpher, qaumhurs);
                if (!string.IsNullOrEmpty(data3.sFatherTumbon)) fatherTumbon.Text = getDISTRICT_NAME(data3.sFatherTumbon, qdistricts);
                if (!string.IsNullOrEmpty(data3.sFatherAumpher)) fatherProvince.Text = getPROVINCE_NAME(data3.sFatherProvince, qprovinces);

                if (!string.IsNullOrEmpty(data3.sMotherAumpher)) motherAumpher.Text = getAMPHUR_NAME(data3.sMotherAumpher, qaumhurs);
                if (!string.IsNullOrEmpty(data3.sMotherTumbon)) motherTumbon.Text = getDISTRICT_NAME(data3.sMotherTumbon, qdistricts);
                if (!string.IsNullOrEmpty(data3.sMotherAumpher)) motherProvince.Text = getPROVINCE_NAME(data3.sMotherProvince, qprovinces);

                if (!string.IsNullOrEmpty(data3.sFamilyTitle)) famTitle.Text = getTitle(data3.sFamilyTitle, q_titlle);
                if (!string.IsNullOrEmpty(data3.sFatherTitle)) fatherTitle.Text = getTitle(data3.sFatherTitle, q_titlle);
                if (!string.IsNullOrEmpty(data3.sMotherTitle)) motherTitle.Text = getTitle(data3.sMotherTitle, q_titlle);

                fatherIdCard.Text = data3.sFatherIdCardNumber;
                fatherLast.Text = data3.sFatherLastName;
                fatherName.Text = data3.sFatherFirstName;
                fatherReligion.Text = data3.sFatherReligion;
                fatherSunChad.Text = data3.sFatherRace;

                motherCeerChad.Text = data3.sMotherRace;
                motherIdCard.Text = data3.sMotherIdCardNumber;
                motherLast.Text = data3.sMotherLastName;
                motherName.Text = data3.sMotherFirstName;
                motherReligion.Text = data3.sMotherReligion;
                motherSunChad.Text = data3.sMotherNation;

                famCeerChad.Text = data3.sFamilyRace;
                famIdCard.Text = data3.sFamilyIdCardNumber;
                famLast.Text = data3.sFamilyLast;
                famName.Text = data3.sFamilyName;
                famReletive.Text = data3.sFamilyRelate;
                famReligion.Text = data3.sFamilyReligion;
                famSunChad.Text = data3.sFamilyNation;

                famEmail.Text = data3.sPhoneMail;
                famMuu.Text = data3.sFamilyMuu;
                famHomenum.Text = data3.sFamilyHomeNumber;
                famPhone1.Text = data3.sPhoneOne;
                famPhone2.Text = data3.sPhoneTwo;
                famPhone3.Text = data3.sPhoneThree;
                famPost.Text = data3.sFamilyPost;
                famRoad.Text = data3.sFamilyRoad;
                famSoy.Text = data3.sFamilySoy;

                fatherMuu.Text = data3.sFatherMuu;
                fatherHome.Text = data3.sFatherHomeNumber;
                fatherPhone.Text = data3.sFatherPhone;
                fatherPost.Text = data3.sFatherPost;
                fatherRoad.Text = data3.sFatherRoad;
                fatherSoy.Text = data3.sFatherSoy;

                motherMuu.Text = data3.sMotherMuu;
                motherHome.Text = data3.sMotherHomeNumber;
                motherPhone.Text = data3.sMotherPhone;
                motherPost.Text = data3.sMotherPost;
                motherRoad.Text = data3.sMotherRoad;
                motherSoy.Text = data3.sMotherSoy;
            }

            growMonth11.Text = "พ.ค.";
            growMonth12.Text = "ส.ค.";
            growMonth13.Text = "พ.ย.";
            growMonth14.Text = "ก.พ.";
            growMonth21.Text = "พ.ค.";
            growMonth22.Text = "ส.ค.";
            growMonth23.Text = "พ.ย.";
            growMonth24.Text = "ก.พ.";
            growMonth31.Text = "พ.ค.";
            growMonth32.Text = "ส.ค.";
            growMonth33.Text = "พ.ย.";
            growMonth34.Text = "ก.พ.";
            growMonth41.Text = "พ.ค.";
            growMonth42.Text = "ส.ค.";
            growMonth43.Text = "พ.ย.";
            growMonth44.Text = "ก.พ.";
            growMonth51.Text = "พ.ค.";
            growMonth52.Text = "ส.ค.";
            growMonth53.Text = "พ.ย.";
            growMonth54.Text = "ก.พ.";
            growMonth61.Text = "พ.ค.";
            growMonth62.Text = "ส.ค.";
            growMonth63.Text = "พ.ย.";
            growMonth64.Text = "ก.พ.";
            var tterm2 = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == data5.nTermSubLevel2).FirstOrDefault();
            var tlevel = _db.TSubLevels.Where(w => w.nTSubLevel == tterm2.nTSubLevel).FirstOrDefault();

            DateTime start = DateTime.Today;
            DateTime start2 = DateTime.Today.AddYears(1);
            DateTime day1 = new DateTime(start.Year, 5, 1);
            DateTime day2 = new DateTime(start.Year, 8, 1);
            DateTime day3 = new DateTime(start.Year, 11, 1);
            DateTime day4 = new DateTime(start2.Year, 2, 1);

            int mon1 = day1.Month - data5.dBirth.Value.Month;
            int year1 = day1.Year - data5.dBirth.Value.Year;
            int mon2 = day2.Month - data5.dBirth.Value.Month;
            int year2 = day2.Year - data5.dBirth.Value.Year;
            int mon3 = day3.Month - data5.dBirth.Value.Month;
            int year3 = day3.Year - data5.dBirth.Value.Year;
            int mon4 = day4.Month - data5.dBirth.Value.Month;
            int year4 = day4.Year - data5.dBirth.Value.Year;

            if (day1.Day < data5.dBirth.Value.Day) mon1--;
            if (day2.Day < data5.dBirth.Value.Day) mon2--;
            if (day3.Day < data5.dBirth.Value.Day) mon3--;
            if (day4.Day < data5.dBirth.Value.Day) mon4--;

            if (mon1 < 0)
            {
                year1--;
                mon1 += 12;
            }
            if (mon2 < 0)
            {
                year2--;
                mon2 += 12;
            }
            if (mon3 < 0)
            {
                year3--;
                mon3 += 12;
            }
            if (mon4 < 0)
            {
                year4--;
                mon4 += 12;
            }


            if (tlevel.nTLevel == 1 || tlevel.nTLevel == 2)
            {
                growClass1.Text = "ปวช. 1";
                growClass2.Text = "ปวช. 2";
                growClass3.Text = "ปวช. 3";
                growClass4.Text = "ปวส. 1";
                growClass5.Text = "ปวส. 2";
                growClass6.Text = "";

                growMonth61.Text = "";
                growMonth62.Text = "";
                growMonth63.Text = "";
                growMonth64.Text = "";

                txthidden.Text = "6";
                if (tterm2.nTSubLevel == 54)
                {
                    age11.Text = year1 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 3 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 3 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 3 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 3 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 4 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 4 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 4 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 4 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 + "/" + mon1;
                    graph12.Text = year2 + "/" + mon2;
                    graph13.Text = year3 + "/" + mon3;
                    graph14.Text = year4 + "/" + mon4;
                    graph21.Text = year1 + 1 + "/" + mon1;
                    graph22.Text = year2 + 1 + "/" + mon2;
                    graph23.Text = year3 + 1 + "/" + mon3;
                    graph24.Text = year4 + 1 + "/" + mon4;
                    graph31.Text = year1 + 2 + "/" + mon1;
                    graph32.Text = year2 + 2 + "/" + mon2;
                    graph33.Text = year3 + 2 + "/" + mon3;
                    graph34.Text = year4 + 2 + "/" + mon4;
                    graph41.Text = year1 + 3 + "/" + mon1;
                    graph42.Text = year2 + 3 + "/" + mon2;
                    graph43.Text = year3 + 3 + "/" + mon3;
                    graph44.Text = year4 + 3 + "/" + mon4;
                    graph51.Text = year1 + 4 + "/" + mon1;
                    graph52.Text = year2 + 4 + "/" + mon2;
                    graph53.Text = year3 + 4 + "/" + mon3;
                    graph54.Text = year4 + 4 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 55)
                {
                    age11.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 3 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 3 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 3 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 3 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 1 + "/" + mon1;
                    graph12.Text = year2 - 1 + "/" + mon2;
                    graph13.Text = year3 - 1 + "/" + mon3;
                    graph14.Text = year4 - 1 + "/" + mon4;
                    graph21.Text = year1 + 0 + "/" + mon1;
                    graph22.Text = year2 + 0 + "/" + mon2;
                    graph23.Text = year3 + 0 + "/" + mon3;
                    graph24.Text = year4 + 0 + "/" + mon4;
                    graph31.Text = year1 + 1 + "/" + mon1;
                    graph32.Text = year2 + 1 + "/" + mon2;
                    graph33.Text = year3 + 1 + "/" + mon3;
                    graph34.Text = year4 + 1 + "/" + mon4;
                    graph41.Text = year1 + 2 + "/" + mon1;
                    graph42.Text = year2 + 2 + "/" + mon2;
                    graph43.Text = year3 + 2 + "/" + mon3;
                    graph44.Text = year4 + 2 + "/" + mon4;
                    graph51.Text = year1 + 3 + "/" + mon1;
                    graph52.Text = year2 + 3 + "/" + mon2;
                    graph53.Text = year3 + 3 + "/" + mon3;
                    graph54.Text = year4 + 3 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 56)
                {
                    age11.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 2 + "/" + mon1;
                    graph12.Text = year2 - 2 + "/" + mon2;
                    graph13.Text = year3 - 2 + "/" + mon3;
                    graph14.Text = year4 - 2 + "/" + mon4;
                    graph21.Text = year1 - 1 + "/" + mon1;
                    graph22.Text = year2 - 1 + "/" + mon2;
                    graph23.Text = year3 - 1 + "/" + mon3;
                    graph24.Text = year4 - 1 + "/" + mon4;
                    graph31.Text = year1 + 0 + "/" + mon1;
                    graph32.Text = year2 + 0 + "/" + mon2;
                    graph33.Text = year3 + 0 + "/" + mon3;
                    graph34.Text = year4 + 0 + "/" + mon4;
                    graph41.Text = year1 + 1 + "/" + mon1;
                    graph42.Text = year2 + 1 + "/" + mon2;
                    graph43.Text = year3 + 1 + "/" + mon3;
                    graph44.Text = year4 + 1 + "/" + mon4;
                    graph51.Text = year1 + 2 + "/" + mon1;
                    graph52.Text = year2 + 2 + "/" + mon2;
                    graph53.Text = year3 + 2 + "/" + mon3;
                    graph54.Text = year4 + 2 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 57)
                {
                    age11.Text = year1 - 3 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 3 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 3 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 3 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 3 + "/" + mon1;
                    graph12.Text = year2 - 3 + "/" + mon2;
                    graph13.Text = year3 - 3 + "/" + mon3;
                    graph14.Text = year4 - 3 + "/" + mon4;
                    graph21.Text = year1 - 2 + "/" + mon1;
                    graph22.Text = year2 - 2 + "/" + mon2;
                    graph23.Text = year3 - 2 + "/" + mon3;
                    graph24.Text = year4 - 2 + "/" + mon4;
                    graph31.Text = year1 - 1 + "/" + mon1;
                    graph32.Text = year2 - 1 + "/" + mon2;
                    graph33.Text = year3 - 1 + "/" + mon3;
                    graph34.Text = year4 - 1 + "/" + mon4;
                    graph41.Text = year1 + 0 + "/" + mon1;
                    graph42.Text = year2 + 0 + "/" + mon2;
                    graph43.Text = year3 + 0 + "/" + mon3;
                    graph44.Text = year4 + 0 + "/" + mon4;
                    graph51.Text = year1 + 1 + "/" + mon1;
                    graph52.Text = year2 + 1 + "/" + mon2;
                    graph53.Text = year3 + 1 + "/" + mon3;
                    graph54.Text = year4 + 1 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 58)
                {
                    age11.Text = year1 - 4 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 4 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 4 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 4 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 3 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 3 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 3 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 3 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 - 0 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 - 0 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 - 0 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 - 0 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 4 + "/" + mon1;
                    graph12.Text = year2 - 4 + "/" + mon2;
                    graph13.Text = year3 - 4 + "/" + mon3;
                    graph14.Text = year4 - 4 + "/" + mon4;
                    graph21.Text = year1 - 3 + "/" + mon1;
                    graph22.Text = year2 - 3 + "/" + mon2;
                    graph23.Text = year3 - 3 + "/" + mon3;
                    graph24.Text = year4 - 3 + "/" + mon4;
                    graph31.Text = year1 - 2 + "/" + mon1;
                    graph32.Text = year2 - 2 + "/" + mon2;
                    graph33.Text = year3 - 2 + "/" + mon3;
                    graph34.Text = year4 - 2 + "/" + mon4;
                    graph41.Text = year1 - 1 + "/" + mon1;
                    graph42.Text = year2 - 1 + "/" + mon2;
                    graph43.Text = year3 - 1 + "/" + mon3;
                    graph44.Text = year4 - 1 + "/" + mon4;
                    graph51.Text = year1 - 0 + "/" + mon1;
                    graph52.Text = year2 - 0 + "/" + mon2;
                    graph53.Text = year3 - 0 + "/" + mon3;
                    graph54.Text = year4 - 0 + "/" + mon4;
                }
            }
            else if (tlevel.nTLevel == 3 || tlevel.nTLevel == 7)
            {
                growClass1.Text = "ประถมศึกษาปีที่ 1";
                growClass2.Text = "ประถมศึกษาปีที่ 2";
                growClass3.Text = "ประถมศึกษาปีที่ 3";
                growClass4.Text = "ประถมศึกษาปีที่ 4";
                growClass5.Text = "ประถมศึกษาปีที่ 5";
                growClass6.Text = "ประถมศึกษาปีที่ 6";
                txthidden.Text = "0";
                if (tterm2.nTSubLevel == 42)
                {
                    age11.Text = year1 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 3 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 3 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 3 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 3 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 4 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 4 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 4 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 4 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 5 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 5 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 5 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 5 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 + "/" + mon1;
                    graph12.Text = year2 + "/" + mon2;
                    graph13.Text = year3 + "/" + mon3;
                    graph14.Text = year4 + "/" + mon4;
                    graph21.Text = year1 + 1 + "/" + mon1;
                    graph22.Text = year2 + 1 + "/" + mon2;
                    graph23.Text = year3 + 1 + "/" + mon3;
                    graph24.Text = year4 + 1 + "/" + mon4;
                    graph31.Text = year1 + 2 + "/" + mon1;
                    graph32.Text = year2 + 2 + "/" + mon2;
                    graph33.Text = year3 + 2 + "/" + mon3;
                    graph34.Text = year4 + 2 + "/" + mon4;
                    graph41.Text = year1 + 3 + "/" + mon1;
                    graph42.Text = year2 + 3 + "/" + mon2;
                    graph43.Text = year3 + 3 + "/" + mon3;
                    graph44.Text = year4 + 3 + "/" + mon4;
                    graph51.Text = year1 + 4 + "/" + mon1;
                    graph52.Text = year2 + 4 + "/" + mon2;
                    graph53.Text = year3 + 4 + "/" + mon3;
                    graph54.Text = year4 + 4 + "/" + mon4;
                    graph61.Text = year1 + 5 + "/" + mon1;
                    graph62.Text = year2 + 5 + "/" + mon2;
                    graph63.Text = year3 + 5 + "/" + mon3;
                    graph64.Text = year4 + 5 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 43)
                {
                    age11.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 3 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 3 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 3 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 3 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 4 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 4 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 4 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 4 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 1 + "/" + mon1;
                    graph12.Text = year2 - 1 + "/" + mon2;
                    graph13.Text = year3 - 1 + "/" + mon3;
                    graph14.Text = year4 - 1 + "/" + mon4;
                    graph21.Text = year1 + 0 + "/" + mon1;
                    graph22.Text = year2 + 0 + "/" + mon2;
                    graph23.Text = year3 + 0 + "/" + mon3;
                    graph24.Text = year4 + 0 + "/" + mon4;
                    graph31.Text = year1 + 1 + "/" + mon1;
                    graph32.Text = year2 + 1 + "/" + mon2;
                    graph33.Text = year3 + 1 + "/" + mon3;
                    graph34.Text = year4 + 1 + "/" + mon4;
                    graph41.Text = year1 + 2 + "/" + mon1;
                    graph42.Text = year2 + 2 + "/" + mon2;
                    graph43.Text = year3 + 2 + "/" + mon3;
                    graph44.Text = year4 + 2 + "/" + mon4;
                    graph51.Text = year1 + 3 + "/" + mon1;
                    graph52.Text = year2 + 3 + "/" + mon2;
                    graph53.Text = year3 + 3 + "/" + mon3;
                    graph54.Text = year4 + 3 + "/" + mon4;
                    graph61.Text = year1 + 4 + "/" + mon1;
                    graph62.Text = year2 + 4 + "/" + mon2;
                    graph63.Text = year3 + 4 + "/" + mon3;
                    graph64.Text = year4 + 4 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 44)
                {
                    age11.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 3 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 3 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 3 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 3 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 2 + "/" + mon1;
                    graph12.Text = year2 - 2 + "/" + mon2;
                    graph13.Text = year3 - 2 + "/" + mon3;
                    graph14.Text = year4 - 2 + "/" + mon4;
                    graph21.Text = year1 - 1 + "/" + mon1;
                    graph22.Text = year2 - 1 + "/" + mon2;
                    graph23.Text = year3 - 1 + "/" + mon3;
                    graph24.Text = year4 - 1 + "/" + mon4;
                    graph31.Text = year1 + 0 + "/" + mon1;
                    graph32.Text = year2 + 0 + "/" + mon2;
                    graph33.Text = year3 + 0 + "/" + mon3;
                    graph34.Text = year4 + 0 + "/" + mon4;
                    graph41.Text = year1 + 1 + "/" + mon1;
                    graph42.Text = year2 + 1 + "/" + mon2;
                    graph43.Text = year3 + 1 + "/" + mon3;
                    graph44.Text = year4 + 1 + "/" + mon4;
                    graph51.Text = year1 + 2 + "/" + mon1;
                    graph52.Text = year2 + 2 + "/" + mon2;
                    graph53.Text = year3 + 2 + "/" + mon3;
                    graph54.Text = year4 + 2 + "/" + mon4;
                    graph61.Text = year1 + 3 + "/" + mon1;
                    graph62.Text = year2 + 3 + "/" + mon2;
                    graph63.Text = year3 + 3 + "/" + mon3;
                    graph64.Text = year4 + 3 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 45)
                {
                    age11.Text = year1 - 3 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 3 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 3 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 3 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 3 + "/" + mon1;
                    graph12.Text = year2 - 3 + "/" + mon2;
                    graph13.Text = year3 - 3 + "/" + mon3;
                    graph14.Text = year4 - 3 + "/" + mon4;
                    graph21.Text = year1 - 2 + "/" + mon1;
                    graph22.Text = year2 - 2 + "/" + mon2;
                    graph23.Text = year3 - 2 + "/" + mon3;
                    graph24.Text = year4 - 2 + "/" + mon4;
                    graph31.Text = year1 - 1 + "/" + mon1;
                    graph32.Text = year2 - 1 + "/" + mon2;
                    graph33.Text = year3 - 1 + "/" + mon3;
                    graph34.Text = year4 - 1 + "/" + mon4;
                    graph41.Text = year1 + 0 + "/" + mon1;
                    graph42.Text = year2 + 0 + "/" + mon2;
                    graph43.Text = year3 + 0 + "/" + mon3;
                    graph44.Text = year4 + 0 + "/" + mon4;
                    graph51.Text = year1 + 1 + "/" + mon1;
                    graph52.Text = year2 + 1 + "/" + mon2;
                    graph53.Text = year3 + 1 + "/" + mon3;
                    graph54.Text = year4 + 1 + "/" + mon4;
                    graph61.Text = year1 + 2 + "/" + mon1;
                    graph62.Text = year2 + 2 + "/" + mon2;
                    graph63.Text = year3 + 2 + "/" + mon3;
                    graph64.Text = year4 + 2 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 46)
                {
                    age11.Text = year1 - 4 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 4 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 4 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 4 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 3 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 3 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 3 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 3 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 - 0 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 - 0 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 - 0 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 - 0 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 4 + "/" + mon1;
                    graph12.Text = year2 - 4 + "/" + mon2;
                    graph13.Text = year3 - 4 + "/" + mon3;
                    graph14.Text = year4 - 4 + "/" + mon4;
                    graph21.Text = year1 - 3 + "/" + mon1;
                    graph22.Text = year2 - 3 + "/" + mon2;
                    graph23.Text = year3 - 3 + "/" + mon3;
                    graph24.Text = year4 - 3 + "/" + mon4;
                    graph31.Text = year1 - 2 + "/" + mon1;
                    graph32.Text = year2 - 2 + "/" + mon2;
                    graph33.Text = year3 - 2 + "/" + mon3;
                    graph34.Text = year4 - 2 + "/" + mon4;
                    graph41.Text = year1 - 1 + "/" + mon1;
                    graph42.Text = year2 - 1 + "/" + mon2;
                    graph43.Text = year3 - 1 + "/" + mon3;
                    graph44.Text = year4 - 1 + "/" + mon4;
                    graph51.Text = year1 - 0 + "/" + mon1;
                    graph52.Text = year2 - 0 + "/" + mon2;
                    graph53.Text = year3 - 0 + "/" + mon3;
                    graph54.Text = year4 - 0 + "/" + mon4;
                    graph61.Text = year1 + 1 + "/" + mon1;
                    graph62.Text = year2 + 1 + "/" + mon2;
                    graph63.Text = year3 + 1 + "/" + mon3;
                    graph64.Text = year4 + 1 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 47)
                {
                    age11.Text = year1 - 5 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 5 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 5 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 5 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 4 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 4 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 4 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 4 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 - 3 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 - 3 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 - 3 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 - 3 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 5 + "/" + mon1;
                    graph12.Text = year2 - 5 + "/" + mon2;
                    graph13.Text = year3 - 5 + "/" + mon3;
                    graph14.Text = year4 - 5 + "/" + mon4;
                    graph21.Text = year1 - 4 + "/" + mon1;
                    graph22.Text = year2 - 4 + "/" + mon2;
                    graph23.Text = year3 - 4 + "/" + mon3;
                    graph24.Text = year4 - 4 + "/" + mon4;
                    graph31.Text = year1 - 3 + "/" + mon1;
                    graph32.Text = year2 - 3 + "/" + mon2;
                    graph33.Text = year3 - 3 + "/" + mon3;
                    graph34.Text = year4 - 3 + "/" + mon4;
                    graph41.Text = year1 - 2 + "/" + mon1;
                    graph42.Text = year2 - 2 + "/" + mon2;
                    graph43.Text = year3 - 2 + "/" + mon3;
                    graph44.Text = year4 - 2 + "/" + mon4;
                    graph51.Text = year1 - 1 + "/" + mon1;
                    graph52.Text = year2 - 1 + "/" + mon2;
                    graph53.Text = year3 - 1 + "/" + mon3;
                    graph54.Text = year4 - 1 + "/" + mon4;
                    graph61.Text = year1 + 0 + "/" + mon1;
                    graph62.Text = year2 + 0 + "/" + mon2;
                    graph63.Text = year3 + 0 + "/" + mon3;
                    graph64.Text = year4 + 0 + "/" + mon4;
                }
            }
            else if (tlevel.nTLevel == 4 || tlevel.nTLevel == 9)
            {
                growClass1.Text = "มัธยมศึกษาปีที่ 1";
                growClass2.Text = "มัธยมศึกษาปีที่ 2";
                growClass3.Text = "มัธยมศึกษาปีที่ 3";
                growClass4.Text = "มัธยมศึกษาปีที่ 4";
                growClass5.Text = "มัธยมศึกษาปีที่ 5";
                growClass6.Text = "มัธยมศึกษาปีที่ 6";
                txthidden.Text = "0";
                if (tterm2.nTSubLevel == 48)
                {
                    age11.Text = year1 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 3 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 3 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 3 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 3 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 4 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 4 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 4 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 4 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 5 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 5 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 5 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 5 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 + "/" + mon1;
                    graph12.Text = year2 + "/" + mon2;
                    graph13.Text = year3 + "/" + mon3;
                    graph14.Text = year4 + "/" + mon4;
                    graph21.Text = year1 + 1 + "/" + mon1;
                    graph22.Text = year2 + 1 + "/" + mon2;
                    graph23.Text = year3 + 1 + "/" + mon3;
                    graph24.Text = year4 + 1 + "/" + mon4;
                    graph31.Text = year1 + 2 + "/" + mon1;
                    graph32.Text = year2 + 2 + "/" + mon2;
                    graph33.Text = year3 + 2 + "/" + mon3;
                    graph34.Text = year4 + 2 + "/" + mon4;
                    graph41.Text = year1 + 3 + "/" + mon1;
                    graph42.Text = year2 + 3 + "/" + mon2;
                    graph43.Text = year3 + 3 + "/" + mon3;
                    graph44.Text = year4 + 3 + "/" + mon4;
                    graph51.Text = year1 + 4 + "/" + mon1;
                    graph52.Text = year2 + 4 + "/" + mon2;
                    graph53.Text = year3 + 4 + "/" + mon3;
                    graph54.Text = year4 + 4 + "/" + mon4;
                    graph61.Text = year1 + 5 + "/" + mon1;
                    graph62.Text = year2 + 5 + "/" + mon2;
                    graph63.Text = year3 + 5 + "/" + mon3;
                    graph64.Text = year4 + 5 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 49)
                {
                    age11.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 3 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 3 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 3 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 3 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 4 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 4 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 4 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 4 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 1 + "/" + mon1;
                    graph12.Text = year2 - 1 + "/" + mon2;
                    graph13.Text = year3 - 1 + "/" + mon3;
                    graph14.Text = year4 - 1 + "/" + mon4;
                    graph21.Text = year1 + 0 + "/" + mon1;
                    graph22.Text = year2 + 0 + "/" + mon2;
                    graph23.Text = year3 + 0 + "/" + mon3;
                    graph24.Text = year4 + 0 + "/" + mon4;
                    graph31.Text = year1 + 1 + "/" + mon1;
                    graph32.Text = year2 + 1 + "/" + mon2;
                    graph33.Text = year3 + 1 + "/" + mon3;
                    graph34.Text = year4 + 1 + "/" + mon4;
                    graph41.Text = year1 + 2 + "/" + mon1;
                    graph42.Text = year2 + 2 + "/" + mon2;
                    graph43.Text = year3 + 2 + "/" + mon3;
                    graph44.Text = year4 + 2 + "/" + mon4;
                    graph51.Text = year1 + 3 + "/" + mon1;
                    graph52.Text = year2 + 3 + "/" + mon2;
                    graph53.Text = year3 + 3 + "/" + mon3;
                    graph54.Text = year4 + 3 + "/" + mon4;
                    graph61.Text = year1 + 4 + "/" + mon1;
                    graph62.Text = year2 + 4 + "/" + mon2;
                    graph63.Text = year3 + 4 + "/" + mon3;
                    graph64.Text = year4 + 4 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 50)
                {
                    age11.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 3 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 3 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 3 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 3 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 2 + "/" + mon1;
                    graph12.Text = year2 - 2 + "/" + mon2;
                    graph13.Text = year3 - 2 + "/" + mon3;
                    graph14.Text = year4 - 2 + "/" + mon4;
                    graph21.Text = year1 - 1 + "/" + mon1;
                    graph22.Text = year2 - 1 + "/" + mon2;
                    graph23.Text = year3 - 1 + "/" + mon3;
                    graph24.Text = year4 - 1 + "/" + mon4;
                    graph31.Text = year1 + 0 + "/" + mon1;
                    graph32.Text = year2 + 0 + "/" + mon2;
                    graph33.Text = year3 + 0 + "/" + mon3;
                    graph34.Text = year4 + 0 + "/" + mon4;
                    graph41.Text = year1 + 1 + "/" + mon1;
                    graph42.Text = year2 + 1 + "/" + mon2;
                    graph43.Text = year3 + 1 + "/" + mon3;
                    graph44.Text = year4 + 1 + "/" + mon4;
                    graph51.Text = year1 + 2 + "/" + mon1;
                    graph52.Text = year2 + 2 + "/" + mon2;
                    graph53.Text = year3 + 2 + "/" + mon3;
                    graph54.Text = year4 + 2 + "/" + mon4;
                    graph61.Text = year1 + 3 + "/" + mon1;
                    graph62.Text = year2 + 3 + "/" + mon2;
                    graph63.Text = year3 + 3 + "/" + mon3;
                    graph64.Text = year4 + 3 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 51)
                {
                    age11.Text = year1 - 3 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 3 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 3 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 3 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 3 + "/" + mon1;
                    graph12.Text = year2 - 3 + "/" + mon2;
                    graph13.Text = year3 - 3 + "/" + mon3;
                    graph14.Text = year4 - 3 + "/" + mon4;
                    graph21.Text = year1 - 2 + "/" + mon1;
                    graph22.Text = year2 - 2 + "/" + mon2;
                    graph23.Text = year3 - 2 + "/" + mon3;
                    graph24.Text = year4 - 2 + "/" + mon4;
                    graph31.Text = year1 - 1 + "/" + mon1;
                    graph32.Text = year2 - 1 + "/" + mon2;
                    graph33.Text = year3 - 1 + "/" + mon3;
                    graph34.Text = year4 - 1 + "/" + mon4;
                    graph41.Text = year1 + 0 + "/" + mon1;
                    graph42.Text = year2 + 0 + "/" + mon2;
                    graph43.Text = year3 + 0 + "/" + mon3;
                    graph44.Text = year4 + 0 + "/" + mon4;
                    graph51.Text = year1 + 1 + "/" + mon1;
                    graph52.Text = year2 + 1 + "/" + mon2;
                    graph53.Text = year3 + 1 + "/" + mon3;
                    graph54.Text = year4 + 1 + "/" + mon4;
                    graph61.Text = year1 + 2 + "/" + mon1;
                    graph62.Text = year2 + 2 + "/" + mon2;
                    graph63.Text = year3 + 2 + "/" + mon3;
                    graph64.Text = year4 + 2 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 52)
                {
                    age11.Text = year1 - 4 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 4 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 4 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 4 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 3 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 3 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 3 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 3 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 - 0 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 - 0 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 - 0 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 - 0 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 4 + "/" + mon1;
                    graph12.Text = year2 - 4 + "/" + mon2;
                    graph13.Text = year3 - 4 + "/" + mon3;
                    graph14.Text = year4 - 4 + "/" + mon4;
                    graph21.Text = year1 - 3 + "/" + mon1;
                    graph22.Text = year2 - 3 + "/" + mon2;
                    graph23.Text = year3 - 3 + "/" + mon3;
                    graph24.Text = year4 - 3 + "/" + mon4;
                    graph31.Text = year1 - 2 + "/" + mon1;
                    graph32.Text = year2 - 2 + "/" + mon2;
                    graph33.Text = year3 - 2 + "/" + mon3;
                    graph34.Text = year4 - 2 + "/" + mon4;
                    graph41.Text = year1 - 1 + "/" + mon1;
                    graph42.Text = year2 - 1 + "/" + mon2;
                    graph43.Text = year3 - 1 + "/" + mon3;
                    graph44.Text = year4 - 1 + "/" + mon4;
                    graph51.Text = year1 - 0 + "/" + mon1;
                    graph52.Text = year2 - 0 + "/" + mon2;
                    graph53.Text = year3 - 0 + "/" + mon3;
                    graph54.Text = year4 - 0 + "/" + mon4;
                    graph61.Text = year1 + 1 + "/" + mon1;
                    graph62.Text = year2 + 1 + "/" + mon2;
                    graph63.Text = year3 + 1 + "/" + mon3;
                    graph64.Text = year4 + 1 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 53)
                {
                    age11.Text = year1 - 5 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 5 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 5 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 5 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 4 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 4 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 4 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 4 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 - 3 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 - 3 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 - 3 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 - 3 + " ปี <br />" + mon4 + " ด.";
                    age41.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age42.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age43.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age44.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age51.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age52.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age53.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age54.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age61.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age62.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age63.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age64.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 5 + "/" + mon1;
                    graph12.Text = year2 - 5 + "/" + mon2;
                    graph13.Text = year3 - 5 + "/" + mon3;
                    graph14.Text = year4 - 5 + "/" + mon4;
                    graph21.Text = year1 - 4 + "/" + mon1;
                    graph22.Text = year2 - 4 + "/" + mon2;
                    graph23.Text = year3 - 4 + "/" + mon3;
                    graph24.Text = year4 - 4 + "/" + mon4;
                    graph31.Text = year1 - 3 + "/" + mon1;
                    graph32.Text = year2 - 3 + "/" + mon2;
                    graph33.Text = year3 - 3 + "/" + mon3;
                    graph34.Text = year4 - 3 + "/" + mon4;
                    graph41.Text = year1 - 2 + "/" + mon1;
                    graph42.Text = year2 - 2 + "/" + mon2;
                    graph43.Text = year3 - 2 + "/" + mon3;
                    graph44.Text = year4 - 2 + "/" + mon4;
                    graph51.Text = year1 - 1 + "/" + mon1;
                    graph52.Text = year2 - 1 + "/" + mon2;
                    graph53.Text = year3 - 1 + "/" + mon3;
                    graph54.Text = year4 - 1 + "/" + mon4;
                    graph61.Text = year1 + 0 + "/" + mon1;
                    graph62.Text = year2 + 0 + "/" + mon2;
                    graph63.Text = year3 + 0 + "/" + mon3;
                    graph64.Text = year4 + 0 + "/" + mon4;
                }
            }
            else if (tlevel.nTLevel == 10)
            {
                growClass1.Text = "อนุบาลศึกษาปีที่ 1";
                growClass2.Text = "อนุบาลศึกษาปีที่ 2";
                growClass3.Text = "อนุบาลศึกษาปีที่ 3";
                growClass4.Text = "";
                growClass5.Text = "";
                growClass6.Text = "";
                growMonth41.Text = "";
                growMonth42.Text = "";
                growMonth43.Text = "";
                growMonth44.Text = "";
                growMonth51.Text = "";
                growMonth52.Text = "";
                growMonth53.Text = "";
                growMonth54.Text = "";
                growMonth61.Text = "";
                growMonth62.Text = "";
                growMonth63.Text = "";
                growMonth64.Text = "";
                txthidden.Text = "4";
                if (tterm2.nTSubLevel == 39)
                {
                    age11.Text = year1 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 2 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 2 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 2 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 2 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 + "/" + mon1;
                    graph12.Text = year2 + "/" + mon2;
                    graph13.Text = year3 + "/" + mon3;
                    graph14.Text = year4 + "/" + mon4;
                    graph21.Text = year1 + 1 + "/" + mon1;
                    graph22.Text = year2 + 1 + "/" + mon2;
                    graph23.Text = year3 + 1 + "/" + mon3;
                    graph24.Text = year4 + 1 + "/" + mon4;
                    graph31.Text = year1 + 2 + "/" + mon1;
                    graph32.Text = year2 + 2 + "/" + mon2;
                    graph33.Text = year3 + 2 + "/" + mon3;
                    graph34.Text = year4 + 2 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 40)
                {
                    age11.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 1 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 1 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 1 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 1 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 1 + "/" + mon1;
                    graph12.Text = year2 - 1 + "/" + mon2;
                    graph13.Text = year3 - 1 + "/" + mon3;
                    graph14.Text = year4 - 1 + "/" + mon4;
                    graph21.Text = year1 + 0 + "/" + mon1;
                    graph22.Text = year2 + 0 + "/" + mon2;
                    graph23.Text = year3 + 0 + "/" + mon3;
                    graph24.Text = year4 + 0 + "/" + mon4;
                    graph31.Text = year1 + 1 + "/" + mon1;
                    graph32.Text = year2 + 1 + "/" + mon2;
                    graph33.Text = year3 + 1 + "/" + mon3;
                    graph34.Text = year4 + 1 + "/" + mon4;
                }
                else if (tterm2.nTSubLevel == 41)
                {
                    age11.Text = year1 - 2 + " ปี <br />" + mon1 + " ด.";
                    age12.Text = year2 - 2 + " ปี <br />" + mon2 + " ด.";
                    age13.Text = year3 - 2 + " ปี <br />" + mon3 + " ด.";
                    age14.Text = year4 - 2 + " ปี <br />" + mon4 + " ด.";
                    age21.Text = year1 - 1 + " ปี <br />" + mon1 + " ด.";
                    age22.Text = year2 - 1 + " ปี <br />" + mon2 + " ด.";
                    age23.Text = year3 - 1 + " ปี <br />" + mon3 + " ด.";
                    age24.Text = year4 - 1 + " ปี <br />" + mon4 + " ด.";
                    age31.Text = year1 + 0 + " ปี <br />" + mon1 + " ด.";
                    age32.Text = year2 + 0 + " ปี <br />" + mon2 + " ด.";
                    age33.Text = year3 + 0 + " ปี <br />" + mon3 + " ด.";
                    age34.Text = year4 + 0 + " ปี <br />" + mon4 + " ด.";

                    graph11.Text = year1 - 2 + "/" + mon1;
                    graph12.Text = year2 - 2 + "/" + mon2;
                    graph13.Text = year3 - 2 + "/" + mon3;
                    graph14.Text = year4 - 2 + "/" + mon4;
                    graph21.Text = year1 - 1 + "/" + mon1;
                    graph22.Text = year2 - 1 + "/" + mon2;
                    graph23.Text = year3 - 1 + "/" + mon3;
                    graph24.Text = year4 - 1 + "/" + mon4;
                    graph31.Text = year1 + 0 + "/" + mon1;
                    graph32.Text = year2 + 0 + "/" + mon2;
                    graph33.Text = year3 + 0 + "/" + mon3;
                    graph34.Text = year4 + 0 + "/" + mon4;
                }

            }

            if (data4 == null)
            {
                TextBox53.Text = "";
                TextBox54.Text = "";
                TextBox62.Text = "";
                TextBox55.Text = "";
                TextBox56.Text = "";
                TextBox57.Text = "";
                TextBox58.Text = "";
                TextBox59.Text = "";
            }
            else
            {
                if (data4.nWeight == 0)
                    TextBox53.Text = "";
                else TextBox53.Text = data4.nWeight.ToString();
                if (data4.nHeight == 0)
                    TextBox54.Text = "";
                else TextBox54.Text = data4.nHeight.ToString();
                TextBox62.Text = data4.sBlood;
                TextBox55.Text = data4.sSickFood;
                TextBox56.Text = data4.sSickDrug;
                TextBox57.Text = data4.sSickOther;
                TextBox58.Text = data4.sSickNormal;
                TextBox59.Text = data4.sSickDanger;
                if (data4.Weight1_1 != "" && data4.Weight1_1 != null)
                    weight11.Text = data4.Weight1_1.ToString();
                if (data4.Weight1_2 != "" && data4.Weight1_2 != null)
                    weight12.Text = data4.Weight1_2.ToString();
                if (data4.Weight1_3 != "" && data4.Weight1_3 != null)
                    weight13.Text = data4.Weight1_3.ToString();
                if (data4.Weight1_4 != "" && data4.Weight1_4 != null)
                    weight14.Text = data4.Weight1_4.ToString();
                if (data4.Weight2_1 != "" && data4.Weight2_1 != null)
                    weight21.Text = data4.Weight2_1.ToString();
                if (data4.Weight2_2 != "" && data4.Weight2_2 != null)
                    weight22.Text = data4.Weight2_2.ToString();
                if (data4.Weight2_3 != "" && data4.Weight2_3 != null)
                    weight23.Text = data4.Weight2_3.ToString();
                if (data4.Weight2_4 != "" && data4.Weight2_4 != null)
                    weight24.Text = data4.Weight2_4.ToString();
                if (data4.Weight3_1 != "" && data4.Weight3_1 != null)
                    weight31.Text = data4.Weight3_1.ToString();
                if (data4.Weight3_2 != "" && data4.Weight3_2 != null)
                    weight32.Text = data4.Weight3_2.ToString();
                if (data4.Weight3_3 != "" && data4.Weight3_3 != null)
                    weight33.Text = data4.Weight3_3.ToString();
                if (data4.Weight3_4 != "" && data4.Weight3_4 != null)
                    weight34.Text = data4.Weight3_4.ToString();
                if (data4.Weight4_1 != "" && data4.Weight4_1 != null)
                    weight41.Text = data4.Weight4_1.ToString();
                if (data4.Weight4_2 != "" && data4.Weight4_2 != null)
                    weight42.Text = data4.Weight4_2.ToString();
                if (data4.Weight4_3 != "" && data4.Weight4_3 != null)
                    weight43.Text = data4.Weight4_3.ToString();
                if (data4.Weight4_4 != "" && data4.Weight4_4 != null)
                    weight44.Text = data4.Weight4_4.ToString();
                if (data4.Weight5_1 != "" && data4.Weight5_1 != null)
                    weight51.Text = data4.Weight5_1.ToString();
                if (data4.Weight5_2 != "" && data4.Weight5_2 != null)
                    weight52.Text = data4.Weight5_2.ToString();
                if (data4.Weight5_3 != "" && data4.Weight5_3 != null)
                    weight53.Text = data4.Weight5_3.ToString();
                if (data4.Weight5_4 != "" && data4.Weight5_4 != null)
                    weight54.Text = data4.Weight5_4.ToString();
                if (data4.Weight6_1 != "" && data4.Weight6_1 != null)
                    weight61.Text = data4.Weight6_1.ToString();
                if (data4.Weight6_2 != "" && data4.Weight6_2 != null)
                    weight62.Text = data4.Weight6_2.ToString();
                if (data4.Weight6_3 != "" && data4.Weight6_3 != null)
                    weight63.Text = data4.Weight6_3.ToString();
                if (data4.Weight6_4 != "" && data4.Weight6_4 != null)
                    weight64.Text = data4.Weight6_4.ToString();
                if (data4.Height1_1 != "" && data4.Height1_1 != null)
                    height11.Text = data4.Height1_1.ToString();
                if (data4.Height1_2 != "" && data4.Height1_2 != null)
                    height12.Text = data4.Height1_2.ToString();
                if (data4.Height1_3 != "" && data4.Height1_3 != null)
                    height13.Text = data4.Height1_3.ToString();
                if (data4.Height1_4 != "" && data4.Height1_4 != null)
                    height14.Text = data4.Height1_4.ToString();
                if (data4.Height2_1 != "" && data4.Height2_1 != null)
                    height21.Text = data4.Height2_1.ToString();
                if (data4.Height2_2 != "" && data4.Height2_2 != null)
                    height22.Text = data4.Height2_2.ToString();
                if (data4.Height2_3 != "" && data4.Height2_3 != null)
                    height23.Text = data4.Height2_3.ToString();
                if (data4.Height2_4 != "" && data4.Height2_4 != null)
                    height24.Text = data4.Height2_4.ToString();
                if (data4.Height3_1 != "" && data4.Height3_1 != null)
                    height31.Text = data4.Height3_1.ToString();
                if (data4.Height3_2 != "" && data4.Height3_2 != null)
                    height32.Text = data4.Height3_2.ToString();
                if (data4.Height3_3 != "" && data4.Height3_3 != null)
                    height33.Text = data4.Height3_3.ToString();
                if (data4.Height3_4 != "" && data4.Height3_4 != null)
                    height34.Text = data4.Height3_4.ToString();
                if (data4.Height4_1 != "" && data4.Height4_1 != null)
                    height41.Text = data4.Height4_1.ToString();
                if (data4.Height4_2 != "" && data4.Height4_2 != null)
                    height42.Text = data4.Height4_2.ToString();
                if (data4.Height4_3 != "" && data4.Height4_3 != null)
                    height43.Text = data4.Height4_3.ToString();
                if (data4.Height4_4 != "" && data4.Height4_4 != null)
                    height44.Text = data4.Height4_4.ToString();
                if (data4.Height5_1 != "" && data4.Height5_1 != null)
                    height51.Text = data4.Height5_1.ToString();
                if (data4.Height5_2 != "" && data4.Height5_2 != null)
                    height52.Text = data4.Height5_2.ToString();
                if (data4.Height5_3 != "" && data4.Height5_3 != null)
                    height53.Text = data4.Height5_3.ToString();
                if (data4.Height5_4 != "" && data4.Height5_4 != null)
                    height54.Text = data4.Height5_4.ToString();
                if (data4.Height6_1 != "" && data4.Height6_1 != null)
                    height61.Text = data4.Height6_1.ToString();
                if (data4.Height6_2 != "" && data4.Height6_2 != null)
                    height62.Text = data4.Height6_2.ToString();
                if (data4.Height6_3 != "" && data4.Height6_3 != null)
                    height63.Text = data4.Height6_3.ToString();
                if (data4.Height6_4 != "" && data4.Height6_4 != null)
                    height64.Text = data4.Height6_4.ToString();
            }
        }

        private string getAMPHUR_NAME(string Amphur_Id, List<amphur> q_amphurs)
        {
            int AMPHUR_ID = 0;
            int.TryParse(Amphur_Id, out AMPHUR_ID);
            var f_data = q_amphurs.FirstOrDefault(f => f.AMPHUR_ID == AMPHUR_ID);
            if (f_data != null) return f_data.AMPHUR_NAME;
            else return "";
        }

        private string getDISTRICT_NAME(string District_Id, List<district> q_district)
        {
            int DISTRICT_ID = 0;
            int.TryParse(District_Id, out DISTRICT_ID);
            var f_data = q_district.FirstOrDefault(f => f.DISTRICT_ID == DISTRICT_ID);
            if (f_data != null) return f_data.DISTRICT_NAME;
            else return "";
        }

        private string getPROVINCE_NAME(string Province_Id, List<province> q_province)
        {
            int PROVINCE_ID = 0;
            int.TryParse(Province_Id, out PROVINCE_ID);
            var f_data = q_province.FirstOrDefault(f => f.PROVINCE_ID == PROVINCE_ID);
            if (f_data != null) return f_data.PROVINCE_NAME;
            else return "";
        }

        private string getTitle(string tilte_Id, List<TTitleList> titles)
        {
            int TitleId = 0;
            int.TryParse(tilte_Id, out TitleId);
            var f_Tiltle = titles.FirstOrDefault(f => f.nTitleid == TitleId);
            if (f_Tiltle != null) return f_Tiltle.titleDescription;
            else return "";
        }

    }
}