using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Configuration;
using System.Collections.Generic;
using PeakengineAPI;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Web;
using System.Threading;
using FingerprintPayment.Class;

namespace FingerprintPayment
{
    public static class MessageBox
    {
        public static void Show(this Page Page, String Message)
        {
            Page.ClientScript.RegisterStartupScript(
               Page.GetType(),
               "MessageBox",
               "<script language='javascript'>alert('" + Message + "');</script>"
            );
        }
    }
    public partial class userlist2_register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            Button1.Click += new EventHandler(Button1_Click);
            Button2.Click += new EventHandler(Button2_Click);
            ddlSubLV2.Enabled = false;
            ddlSubLV2.Items.Insert(0, new ListItem("เลือกห้อง", "0"));

            if (!IsPostBack)
            {
                fcommon.ListYears(ddlAge, "", 1900, "en-us", "th-TH");
                fcommon.ListYears(dFamilyBirthDayYY, "", 1900, "en-us", "th-TH");
                fcommon.ListYears(dFatherBirthDayYY, "", 1900, "en-us", "th-TH");
                fcommon.ListYears(dMotherBirthDayYY, "", 1900, "en-us", "th-TH");

                string sEntities = Session["sEntities"].ToString();
                JabJaiMasterEntities dbmaster = Connection.MasterEntities();
                var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(qcompany)));

                fcommon.LinqToDropDownList(q, ddlSubLV, "เลือกห้อง", "class_id", "class_name");

                var dtprovinces = dbmaster.provinces.ToList();
                fcommon.LinqToDropDownList(dtprovinces, ddlprovince, "- เลือกจังหวัด -", "PROVINCE_ID", "PROVINCE_NAME");
                fcommon.LinqToDropDownList(dtprovinces, oldSchoolProvince, "- เลือกจังหวัด -", "PROVINCE_ID", "PROVINCE_NAME");
                fcommon.LinqToDropDownList(dtprovinces, houseRegistrationProvince, "- เลือกจังหวัด -", "PROVINCE_ID", "PROVINCE_NAME");
                fcommon.LinqToDropDownList(dtprovinces, bornFromProvince, "- เลือกจังหวัด -", "PROVINCE_ID", "PROVINCE_NAME");

                //int PROVINCE_ID = int.Parse(ddlprovince.SelectedValue);
                //fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper, "", "AMPHUR_ID", "AMPHUR_NAME");
                //fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), oldSchoolProvince, "", "AMPHUR_ID", "AMPHUR_NAME");

                //int AMPHUR_CODE = int.Parse(txtAumper.SelectedValue);
                //fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

                fcommon.LinqToDropDownList(dtprovinces, famProvince, "- เลือกจังหวัด -", "PROVINCE_ID", "PROVINCE_NAME");
                //fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), famaumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                //fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), famTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

                fcommon.LinqToDropDownList(dtprovinces, fatherProvince, "- เลือกจังหวัด -", "PROVINCE_ID", "PROVINCE_NAME");
                fcommon.LinqToDropDownList(dtprovinces, motherProvince, "- เลือกจังหวัด -", "PROVINCE_ID", "PROVINCE_NAME");
                //var _listslv = _db.TSubLevels;
                //foreach (var DataLV in _listslv)
                //{
                //    ddlSubLV.Items.Add(new ListItem(DataLV.SubLevel.ToString(), DataLV.nTSubLevel.ToString()));
                //}

                var q_title = _db.TTitleLists.Where(w => w.workStatus == "working" && w.deleted == "0").ToList();
                fcommon.LinqToDropDownList(q_title, DropDownList4, "- เลือกคำนำหน้า -", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(q_title, famTitle, "- เลือกคำนำหน้า -", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(q_title, fatherTitle, "- เลือกคำนำหน้า -", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(q_title, motherTitle, "- เลือกคำนำหน้า -", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(q_title, motherTitle, "- เลือกคำนำหน้า -", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(q_title, stayWithTitle, "- กรุณาเลือก -", "nTitleid", "titleDescription");
                OpenData();
            }
            SetBodyEventOnLoad("");
        }

        protected void ddlSubLV_Change(object sender, EventArgs e)
        {
            ddlSubLV2.Enabled = false;
            ddlSubLV2.Items.Clear();
            int stateId = int.Parse(ddlSubLV.SelectedItem.Value);
            if (stateId > 0)
            {
                BindDropDownList(stateId);
                ddlSubLV2.Enabled = true;
            }
        }

        private void BindDropDownList(int value)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            var q2 = QueryDataBases.SubLevel2_Query.GetData(_db, value);
            fcommon.LinqToDropDownList(q2, ddlSubLV2, "ทั้งหมด", "classRoom_id", "classRoom_name");
        }

        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }
        void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("userlist2.aspx");
        }

        protected void Upload(object sender, EventArgs e)
        {

        }

        void Button1_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().userDdata;
            if (!token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));

            if (_db.TUsers.Where(w => w.sStudentID.Trim() == TextBox56.Text && string.IsNullOrEmpty(w.cDel)).Count() > 0)
            {
                Response.Write("<script>alert('รหัสนักเรียนนี้ได้ทำการใช้ไปแล้ว');</script>");
                return;
            }

            if (string.IsNullOrEmpty(TextBox56.Text))
            {
                Response.Write("<script>alert('กรุณากรอกข้อมูลรหัสนักเรียน');</script>");
                return;
            }

            if ((RadioButtonList8.SelectedValue != "ชาย") && (RadioButtonList8.SelectedValue != "หญิง"))
            {
                Response.Write("<script>alert('กรุณาเลือกเพศ');</script>");
                return;
            }
            if (string.IsNullOrEmpty(ddlSubLV.SelectedValue))
            {
                Response.Write("<script>alert('กรุณาเลือกชั้นเรียน');</script>");
                return;
            }
            if (string.IsNullOrEmpty(ddlSubLV2.SelectedValue))
            {
                Response.Write("<script>alert('กรุณาเลือกห้องเรียน');</script>");
                return;
            }
            if (DropDownList1.SelectedValue == "-1")
            {
                Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                return;
            }
            if (DropDownList2.SelectedValue == "-1")
            {
                Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                return;
            }
            if (ddlAge.SelectedValue == "-1")
            {
                Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                return;
            }

            if (dFamilyBirthDayDD.SelectedValue == "-1" || dFatherBirthDayDD.SelectedValue == "-1" || dMotherBirthDayDD.SelectedValue == "-1")
            {
                Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                return;
            }
            if (dFamilyBirthDayMM.SelectedValue == "-1" || dFatherBirthDayMM.SelectedValue == "-1" || dMotherBirthDayMM.SelectedValue == "-1")
            {
                Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                return;
            }
            if (dFamilyBirthDayYY.SelectedValue == "-1" || dFatherBirthDayYY.SelectedValue == "-1" || dMotherBirthDayYY.SelectedValue == "-1")
            {
                Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                return;
            }

            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            string link = "";

            int sID2 = 0;

            JabjaiEntity.DB.TUser _User = new JabjaiEntity.DB.TUser();
            int nID = 2;
            if (_db.TUsers.ToList().Count() > 0)
                nID = _db.TUsers.Max(o => o.sID) + 1;
            _User.sID = nID;
            sID2 = nID;
            _User.sName = TextBox1.Text;
            _User.sLastname = TextBox7.Text;
            _User.nMoney = 0;
            _User.nTermSubLevel2 = Int32.Parse(ddlSubLV2.SelectedValue);
            //Response.Write(txtdBirth.Text); return;
            string birthDate = DropDownList1.SelectedValue;
            string birthMonth = DropDownList2.SelectedValue;
            string birthYear = ddlAge.SelectedValue;
            string combinedate = birthDate + "/" + birthMonth + "/" + birthYear;
            string sex = "";
            if (RadioButtonList8.SelectedValue == "ชาย") sex = "0";
            else sex = "1";
            DateTime dt1 = DateTime.ParseExact(combinedate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string sEntities = Session["sEntities"] + "";
            int sID = 1;
            var f_Company = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            int nCompany = f_Company.nCompany;
            bool saveStatus = true;
            try
            {
                DateTime? _dBirth;
                try
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        if (DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                            _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                        else
                            _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                        _User.dBirth = _dBirth;
                        _User.sStudentID = TextBox56.Text.Length < 6 ? string.Format("{0:000000}", TextBox56.Text) : TextBox56.Text;
                        _User.sIdentification = TextBox6.Text;
                        _User.nMax = 0;
                        _User.sAddress = famHomenum.Text + " " + famMuu.Text + " " + famSoy.Text + " " + famRoad.Text + " " +
                                   famTumbon.Text + " " + famaumpher.Text + " " + famProvince.Text + " " + famPost.Text;

                        _User.cType = "0"; //ddlcType.SelectedValue.ToString();
                        _User.cSMS = "1";
                        //_User.cTelSMS = TextBox13.Text;
                        _User.cSex = sex;
                        _User.baseSalary = 0;
                        _User.sStudentPicture = link;
                        if (!string.IsNullOrEmpty(link)) _User.dPicUpdate = DateTime.Now;
                        _User.nPicversion = !string.IsNullOrEmpty(link) ? 1 : 0;

                        //_User.sPhone = TextBox13.Text;
                        //_User.sEmail = TextBox45.Text;
                        _User.sStudentTitle = DropDownList4.Text;
                        _User.sStudentNameEN = TextBox21.Text;
                        _User.sStudentLastEN = TextBox22.Text;
                        if (!string.IsNullOrEmpty(txtStudentNumber.Text))
                        {
                            _User.nStudentNumber = int.Parse(txtStudentNumber.Text);
                        }
                        bool raceThai = RadioButton1.Checked;
                        bool nationThai = RadioButton13.Checked;
                        bool Buddist = RadioButton15.Checked;
                        bool Christ = RadioButton17.Checked;
                        bool Islam = RadioButton18.Checked;
                        if (raceThai == true)
                            _User.sStudentRace = "ไทย";
                        else _User.sStudentRace = TextBox3.Text;
                        if (nationThai == true)
                            _User.sStudentNation = "ไทย";
                        else _User.sStudentNation = TextBox8.Text;
                        if (Buddist == true)
                            _User.sStudentReligion = "พุทธ";
                        else if (Christ == true)
                            _User.sStudentReligion = "คริสต์";
                        else if (Islam == true)
                            _User.sStudentReligion = "อิสลาม";
                        else _User.sStudentReligion = TextBox10.Text;
                        _User.sStudentIdCardNumber = TextBox6.Text;
                        _User.sStudentHomeNumber = famHomenum.Text;
                        _User.sStudentSoy = famSoy.Text;
                        _User.sStudentTumbon = famTumbon.SelectedValue;
                        _User.sStudentProvince = famProvince.SelectedValue;
                        _User.sStudentMuu = famMuu.Text;
                        _User.sStudentRoad = famRoad.Text;
                        _User.sStudentAumpher = famaumpher.SelectedValue;
                        _User.sStudentPost = famPost.Text;
                        _User.sNickName = TextBox52.Text;
                        if (!string.IsNullOrEmpty(DropDownList5.SelectedValue))
                        {
                            int sonddl = Int32.Parse(DropDownList5.SelectedValue);
                            _User.nSonNumber = sonddl;
                        }
                        _User.dUpdate = DateTime.Now;

                        _User.oldSchoolName = oldSchoolName.Text;
                        _User.oldSchoolProvince = oldSchoolProvince.SelectedValue;
                        _User.oldSchoolAumpher = oldSchoolAumpher.SelectedValue;
                        _User.oldSchoolTumbon = oldSchoolTumbon.SelectedValue;
                        decimal n;
                        if (decimal.TryParse(oldSchoolGPA.Text, out n) == true)
                        {
                            _User.oldSchoolGPA = n;
                        }

                        _User.oldSchoolGraduated = oldSchoolGraduated.SelectedValue;
                        _db.TUsers.Add(_User);
                        _db.SaveChanges();

                        transactionScope.Complete();
                    }

                }
                catch (Exception ex)
                {
                    SetBodyEventOnLoad(@"showModal('แจ้งผลการดำเนินการ','ไม่สามารถทำการบันทึกข้อมูลได้ <br/>กรุณาลองทำรายการใหม่อีกครั้ง');");
                    saveStatus = false;
                }
                finally
                {
                    #region Add Data Master
                    try
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            if (_dbMaster.TUsers.ToList().Count > 0) sID = _dbMaster.TUsers.Max(M => M.sID) + 1;
                            string sPassword = RandomNumber();

                            _dbMaster.TUsers.Add(new MasterEntity.TUser
                            {
                                sID = sID,
                                sName = _User.sName,
                                sLastname = _User.sLastname,
                                sIdentification = _User.sIdentification,
                                cSex = sex,
                                sPhone = _User.sPhone,
                                sEmail = _User.sEmail,
                                sPassword = sPassword,
                                dCreate = DateTime.Now,
                                cType = "0",
                                nCompany = nCompany,
                                nSystemID = _User.sID,
                                dBirth = _User.dBirth,
                                username = _User.sStudentID,
                                userpassword = _User.dBirth.Value.ToString("ddMMyyyy"),

                            });

                            _dbMaster.SaveChanges();
                            transactionScope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        var f_user = _db.TUsers.FirstOrDefault(f => f.sID == _User.sID);
                        if (f_user != null)
                        {
                            _db.TUsers.Remove(f_user);
                            _db.SaveChanges();
                        }
                        SetBodyEventOnLoad(@"showModal('แจ้งผลการดำเนินการ','ไม่สามารถทำการบันทึกข้อมูลได้ <br/>กรุณาลองทำรายการใหม่อีกครั้ง');");
                        saveStatus = false;
                    }
                    finally
                    {
                    }
                    #endregion
                }

                StudentLogic studentLogic = new StudentLogic(_db);
                _db.TStudentClassroomHistories.Add(new TStudentClassroomHistory
                {
                    nStudentId = _User.sID,
                    nTerm = studentLogic.GetTermId(userData.CompanyID),
                    nTermSubLevel2 = _User.nTermSubLevel2
                });

                _db.SaveChanges();
                if (!saveStatus) return;

                //var q_token = _dbMaster.TTokens.FirstOrDefault(f => f.School_Id == nCompany);
                //if (q_token != null)
                //{
                //    var f_usermaster = _dbMaster.TUsers.Find(sID);
                //    var f_user = _db.TUsers.Find(_User.sID);
                //    f_usermaster.ContactPeak = ContactsAPI.SendContactId(_User.sID, sEntities, ConfigurationManager.AppSettings["connectId"], ConfigurationManager.AppSettings["password"]);
                //    if (f_user.ContactPeak.Length <= 100)
                //    {
                //        f_user.ContactPeak = f_usermaster.ContactPeak;
                //        _db.SaveChanges();
                //        _dbMaster.SaveChanges();
                //    }
                //}
                if (FileUpload1.HasFile)
                {
                    var f_usermaster = _dbMaster.TUsers.Find(sID);
                    var f_user = _db.TUsers.Find(_User.sID);
                    f_user.sStudentPicture = AzureStorage.UploadFile(FileUpload1, 150, sID);
                    _db.SaveChanges();
                }

                int nSTDID = 1;
                if (_db.TStudentLevels.ToList().Count() > 0)
                    nSTDID = _db.TStudentLevels.Max(o => o.nStdLvID) + 1;
                TStudentLevel TStdLV = new TStudentLevel();
                TStdLV.nStdLvID = nSTDID;
                TStdLV.sID = nID;
                //TStdLV.nTSubLevel = Int32.Parse(ddlSubLV.SelectedValue);
                _db.TStudentLevels.Add(TStdLV);
                database.InsertLog(Session["sEmpID"] + "", "เพิ่มข้อมูลนักเรียน " + _User.sName + " " + _User.sLastname,
                    Session["sEntities"].ToString(), Request, 14, 2, 0);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                SetBodyEventOnLoad(@"showModal('แจ้งผลการดำเนินการ','ไม่สามารถทำการบันทึกข้อมูลได้ <br/>กรุณาตรวจข้อมูล ว/ด/ป เกิดใหม่อีกครั้ง ');");
            }

            //textbox

            TFamilyProfile famprofile = new TFamilyProfile();
            // tab 2
            int countfamily = _db.TFamilyProfiles.Count() == 0 ? 0 : _db.TFamilyProfiles.Max(max => max.nFamilyID);

            string FatherR = "";
            string FatherN = "";
            string FatherRLG = "";
            bool FatherRaceThai = fatherChurChadddl1.Checked;
            bool FatherNationThai = fatherSunChadddl1.Checked;
            bool FatherBuddist = fatherReligion1.Checked;
            bool FatherChrist = fatherReligion2.Checked;
            bool FatherIslam = fatherReligion3.Checked;
            if (FatherRaceThai == true)
                FatherR = "ไทย";
            else FatherR = fatherChurChad.Text;
            if (FatherNationThai == true)
                FatherN = "ไทย";
            else FatherN = fatherSunChad.Text;
            if (FatherBuddist == true)
                FatherRLG = "พุทธ";
            else if (FatherChrist == true)
                FatherRLG = "คริสต์";
            else if (FatherIslam == true)
                FatherRLG = "อิสลาม";
            else FatherRLG = fatherReligiontxt.Text;

            string MotherR = "";
            string MotherN = "";
            string MotherRLG = "";
            bool MotherRaceThai = motherChurChad1.Checked;
            bool MotherNationThai = motherSunChad1.Checked;
            bool MotherBuddist = motherReligion1.Checked;
            bool MotherChrist = motherReligion2.Checked;
            bool MotherIslam = motherReligion3.Checked;
            if (MotherRaceThai == true)
                MotherR = "ไทย";
            else MotherR = motherChurChadtxt.Text;
            if (MotherNationThai == true)
                MotherN = "ไทย";
            else MotherN = motherSunChadtxt.Text;
            if (MotherBuddist == true)
                MotherRLG = "พุทธ";
            else if (MotherChrist == true)
                MotherRLG = "คริสต์";
            else if (MotherIslam == true)
                MotherRLG = "อิสลาม";
            else MotherRLG = motherReligiontxt.Text;

            string GuardianR = "";
            string GuardianN = "";
            string GuardianRLG = "";
            bool GuardianRaceThai = GuardianChurChad1.Checked;
            bool GuardianNationThai = GuardianSunChad1.Checked;
            bool GuardianBuddist = GuardianReligion1.Checked;
            bool GuardianChrist = GuardianReligion2.Checked;
            bool GuardianIslam = GuardianReligion3.Checked;
            if (GuardianRaceThai == true)
                GuardianR = "ไทย";
            else GuardianR = GuardianChurChadtxt.Text;
            if (GuardianNationThai == true)
                GuardianN = "ไทย";
            else GuardianN = GuardianSunChadtxt.Text;
            if (GuardianBuddist == true)
                GuardianRLG = "พุทธ";
            else if (GuardianChrist == true)
                GuardianRLG = "คริสต์";
            else if (GuardianIslam == true)
                GuardianRLG = "อิสลาม";
            else GuardianRLG = GuardianReligiontxt.Text;


            string homenumFather = fatherHome.Text;
            string soyFather = fatherSoy.Text;
            string muuFather = fatherMuu.Text;
            string roadFather = fatherRoad.Text;
            string provinceFather = fatherProvince.SelectedValue;
            string aumpherFather = fatherAumpher.SelectedValue;
            string tumbonFather = fatherTumbon.SelectedValue;
            string postFather = fatherPost.Text;

            string homenumMother = fatherHome.Text;
            string soyMother = motherSoy.Text;
            string muuMother = motherMuu.Text;
            string roadMother = motherRoad.Text;
            string provinceMother = motherProvince.SelectedValue;
            string aumpherMother = motherAumpher.SelectedValue;
            string tumbonMother = motherTumbon.SelectedValue;
            string postMother = motherPost.Text;

            if (customCheck1.Checked == true)
            {
                homenumFather = famHomenum.Text;
                soyFather = famSoy.Text;
                muuFather = famMuu.Text;
                roadFather = famRoad.Text;
                provinceFather = famProvince.SelectedValue;
                aumpherFather = famaumpher.SelectedValue;
                tumbonFather = famTumbon.SelectedValue;
                postFather = famPost.Text;
            }

            if (customCheck2.Checked == true)
            {
                homenumMother = famHomenum.Text;
                soyMother = famSoy.Text;
                muuMother = famMuu.Text;
                roadMother = famRoad.Text;
                provinceMother = famProvince.SelectedValue;
                aumpherMother = famaumpher.SelectedValue;
                tumbonMother = famTumbon.SelectedValue;
                postMother = famPost.Text;
            }

            _db.TFamilyProfiles.Add(new TFamilyProfile
            {
                nFamilyID = countfamily + 1,
                sID = sID2,
                sDeleted = "false",
                sFamilyTitle = famTitle.Text,
                sFamilyName = famName.Text,
                sFamilyLast = famLast.Text,
                sFamilyRace = GuardianR,
                sFamilyNation = GuardianN,
                sFamilyReligion = GuardianRLG,
                sFamilyIdCardNumber = famIdCard.Text,
                sFamilyRelate = RadioButtonList1.SelectedValue,
                sFamilyHomeNumber = famHomenum.Text,
                sFamilySoy = famSoy.Text,
                sFamilyTumbon = famTumbon.SelectedValue,
                sFamilyProvince = famProvince.SelectedValue,
                sPhoneOne = famPhone1.Text,
                sPhoneTwo = famPhone2.Text,
                sPhoneThree = famPhone3.Text,
                //sPhoneMail = famEmail.Text,
                sFamilyMuu = famMuu.Text,
                sFamilyRoad = famRoad.Text,
                sFamilyAumpher = famaumpher.SelectedValue,
                sFamilyPost = famPost.Text,
                sFatherTitle = fatherTitle.Text,
                sFatherFirstName = fatherName.Text,
                sFatherLastName = fatherLast.Text,
                sFatherIdCardNumber = fatherIdCard.Text,
                sFatherNation = FatherN,
                sFatherRace = FatherR,
                sFatherReligion = FatherRLG,
                sMotherTitle = motherTitle.Text,
                sMotherFirstName = motherName.Text,
                sMotherIdCardNumber = motherIdCard.Text,
                sMotherLastName = motherLast.Text,
                sMotherNation = MotherN,
                sMotherRace = MotherR,
                sMotherReligion = MotherRLG,
                sFatherAumpher = aumpherFather,
                sFatherHomeNumber = homenumFather,
                sFatherMuu = muuFather,
                //sFatherPhone = fatherPhone.Text,
                sFatherPost = postFather,
                sFatherProvince = provinceFather,
                sFatherRoad = roadFather,
                sFatherSoy = soyFather,
                sFatherTumbon = tumbonFather,
                sMotherAumpher = aumpherMother,
                sMotherHomeNumber = homenumMother,
                sMotherMuu = muuMother,
                //sMotherPhone = motherPhone.Text,
                sMotherPost = postMother,
                sMotherProvince = provinceMother,
                sMotherRoad = roadMother,
                sMotherSoy = soyMother,
                sMotherTumbon = tumbonMother,
                sFamilyNameEN = sFamilyNameEN.Text,
                sFamilyLastEN = sFamilyLastEN.Text,
                //dFamilyBirthDay                      .Text =
                nFamilyRequestStudyMoney = ConvertInt(nFamilyRequestStudyMoney.Text),
                sFamilyGraduated = ConvertInt(sFamilyGraduated.Text),
                sFamilyWorkPlace = sFamilyWorkPlace.Text,
                nFamilyIncome = ConvertInt(nFamilyIncome.Text),
                sFatherNameEN = sFatherNameEN.Text,
                sFatherLastEN = sFatherLastEN.Text,
                sFatherGraduated = ConvertInt(sFatherGraduated.Text),
                sFatherJob = sFatherJob.Text,
                sFatherPhone2 = sFatherPhone2.Text,
                sFatherPhone3 = sFatherPhone3.Text,
                nFatherIncome = ConvertInt(nFatherIncome.Text),
                sMotherNameEN = sMotherNameEN.Text,
                sMotherLastEN = sMotherLastEN.Text,
                //dMotherBirthDay.Text = dMotherBirthDay,
                sMotherGraduated = ConvertInt(sMotherGraduated.SelectedValue),
                sMotherJob = sMotherJob.Text,
                sMotherWorkPlace = sMotherWorkPlace.Text,
                sMotherPhone2 = sMotherPhone2.Text,
                sMotherPhone3 = sMotherPhone3.Text,
                nMotherIncome = ConvertInt(nMotherIncome.Text),
                nSonTotal = ConvertInt(nSonTotal.SelectedValue),
                nRelativeStudyHere = ConvertInt(nRelativeStudyHere.SelectedValue),
                stayWithTitle = ConvertInt(stayWithTitle.SelectedValue),
                stayWithName = stayWithName.Text,
                stayWithLast = stayWithLast.Text,
                stayWithEmergencyCall = stayWithEmergencyCall.Text,
                stayWithEmail = stayWithEmail.Text,
                HomeType = ConvertInt(HomeType.SelectedValue),
                //friendSID = ConvertInt(friendSID.Text),
                houseRegistrationNumber = houseRegistrationNumber.Text,
                houseRegistrationMuu = houseRegistrationMuu.Text,
                houseRegistrationSoy = houseRegistrationSoy.Text,
                houseRegistrationRoad = houseRegistrationRoad.Text,
                houseRegistrationProvince = ConvertInt(houseRegistrationProvince.Text),
                houseRegistrationAumpher = ConvertInt(houseRegistrationAumpher.Text),
                houseRegistrationTumbon = ConvertInt(houseRegistrationTumbon.Text),
                //houseRegistrationPost = houseRegistrationPost,
                houseRegistrationPhone = houseRegistrationPhone.Text,
                bornFrom = bornFrom.Text,

            });

            try
            {
                THealtProfile health = new THealtProfile();
                //tab 3
                int counthealth = _db.THealtProfiles.Count() == 0 ? 0 : _db.THealtProfiles.Max(max => max.nHealthID);
                health.nHealthID = counthealth + 1;
                health.sID = sID2;
                health.sDeleted = "false";
                if ((TextBox42.Text == "") || (TextBox42.Text == null))
                    health.nWeight = 0;
                else health.nWeight = Int32.Parse(TextBox42.Text);
                if (TextBox43.Text == "")
                    health.nHeight = 0;
                else health.nHeight = Int32.Parse(TextBox43.Text);
                health.sBlood = ddlBlood.SelectedValue;
                bool sickFood1 = RadioButton3.Checked;
                if (sickFood1 == true)
                    health.sSickFood = "ไม่แพ้";
                else health.sSickFood = TextBox54.Text;
                bool sickDrug1 = RadioButton5.Checked;
                if (sickDrug1 == true)
                    health.sSickDrug = "ไม่แพ้";
                else health.sSickDrug = TextBox17.Text;
                bool sickOther1 = RadioButton7.Checked;
                if (sickOther1 == true)
                    health.sSickOther = "ไม่แพ้";
                else health.sSickOther = TextBox36.Text;
                bool sickNormal1 = RadioButton9.Checked;
                if (sickNormal1 == true)
                    health.sSickNormal = "ไม่มีโรค";
                else health.sSickNormal = TextBox41.Text;
                bool sickDanger1 = RadioButton11.Checked;
                if (sickDanger1 == true)
                    health.sSickDanger = "ไม่มีโรค";
                else health.sSickDanger = TextBox44.Text;
                _db.THealtProfiles.Add(health);
                _db.SaveChanges();

                //if (nCompany == 119)
                //{
                //    var f_usermaster = _dbMaster.TUsers.Find(sID);
                //    UpdateMemory memory = new UpdateMemory();
                //    memory.Student(_User, f_usermaster);
                //}
                if (!string.IsNullOrEmpty(f_Company.PaymentAPIUrl))
                {
                    var f_usermaster = _dbMaster.TUsers.Find(sID);
                    UpdateMemory memory = new UpdateMemory();
                    memory.Student(_User, f_usermaster, f_Company.PaymentAPIUrl);
                }
            }
            catch (DbEntityValidationException dbex)
            {
                string message_error = "";
                foreach (var eve in dbex.EntityValidationErrors)
                {
                    message_error += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message_error += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                SetBodyEventOnLoad(@"showModal('แจ้งผลการดำเนินการ','ไม่สามารถทำการบันทึกข้อมูลได้ <br/>กรุณาลองทำรายการใหม่อีกครั้ง');");
                return;
            }


            Response.Redirect("userlist2.aspx");
        }

        private int? ConvertInt(string DataValue)
        {
            if (string.IsNullOrEmpty(DataValue))
                return null;
            else
                return int.Parse(DataValue);
        }

        private void OpenData()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
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

        protected void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int PROVINCE_ID = int.Parse(ddlprovince.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper, "", "AMPHUR_ID", "AMPHUR_NAME");

            int AMPHUR_CODE = int.Parse(txtAumper.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

        }

        protected void txtAumper_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int AMPHUR_CODE = int.Parse(txtAumper.SelectedValue);
            var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            txtPost.Text = qAMPHUR.POSTCODE;
        }

        protected void famaumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int AMPHUR_CODE = int.Parse(famaumpher.SelectedValue);
            var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), famTumbon, "- เลือกแขวง/ตำบล -", "DISTRICT_ID", "DISTRICT_NAME");
            famPost.Text = qAMPHUR.POSTCODE;
        }

        protected void famProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            if (famProvince.SelectedIndex == 0)
            {
                famaumpher.Items.Clear();
                famaumpher.Items.Add(new ListItem("- เลือกเขต/อำเภอ -", ""));
            }
            else
            {
                int PROVINCE_ID = int.Parse(famProvince.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), famaumpher, "- เลือกเขต/อำเภอ -", "AMPHUR_ID", "AMPHUR_NAME");
            }

            famTumbon.Items.Clear();
            famTumbon.Items.Add(new ListItem("- เลือกแขวง/ตำบล -", ""));
        }

        protected void houseRegistrationProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int PROVINCE_ID = int.Parse(houseRegistrationProvince.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), houseRegistrationAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

            int AMPHUR_CODE = int.Parse(houseRegistrationAumpher.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), houseRegistrationTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
        }

        protected void houseRegistrationAumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int AMPHUR_CODE = int.Parse(houseRegistrationAumpher.SelectedValue);
            var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), houseRegistrationTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            //stdPost.Text = qAMPHUR.POSTCODE;
        }
        protected void bornFromProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int PROVINCE_ID = int.Parse(bornFromProvince.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), bornFromAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

            int AMPHUR_CODE = int.Parse(houseRegistrationAumpher.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), bornFromTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
        }

        protected void bornFromAumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int AMPHUR_CODE = int.Parse(bornFromAumpher.SelectedValue);
            var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), bornFromTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            //stdPost.Text = qAMPHUR.POSTCODE;
        }

        protected void fatheraumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int AMPHUR_CODE = int.Parse(fatherAumpher.SelectedValue);
            var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), fatherTumbon, "- เลือกแขวง/ตำบล -", "DISTRICT_ID", "DISTRICT_NAME");
            fatherPost.Text = qAMPHUR.POSTCODE;
        }

        protected void fatherProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int PROVINCE_ID = int.Parse(fatherProvince.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), fatherAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

            int AMPHUR_CODE = int.Parse(fatherAumpher.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), fatherTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
        }

        protected void motheraumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int AMPHUR_CODE = int.Parse(motherAumpher.SelectedValue);
            var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), motherTumbon, "- เลือกแขวง/ตำบล -", "DISTRICT_ID", "DISTRICT_NAME");
            motherPost.Text = qAMPHUR.POSTCODE;
        }

        protected void motherProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int PROVINCE_ID = int.Parse(motherProvince.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), motherAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

            int AMPHUR_CODE = int.Parse(motherAumpher.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), motherTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
        }

        protected void oldschoolaumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int AMPHUR_CODE = int.Parse(oldSchoolAumpher.SelectedValue);
            var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), oldSchoolTumbon, "- เลือกแขวง/ตำบล -", "DISTRICT_ID", "DISTRICT_NAME");
        }

        protected void oldschoolProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int PROVINCE_ID = int.Parse(oldSchoolProvince.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), oldSchoolAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

            int AMPHUR_CODE = int.Parse(oldSchoolAumpher.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), oldSchoolTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
        }

        private static void UpdataBalance(string json)
        {
            //var result = fcommon.send_req("http://paymentapi-prod.ap-southeast-1.elasticbeanstalk.com/api/shop/payment/topupmoney", fcommon.MethodPost, data, null);
            var result = fcommon.send_req("https://shopapi-test.schoolbright.co/api/shop/payment/addormodifystudent", fcommon.MethodPost, json, null);
        }

        internal class StudentData
        {
            public JabjaiEntity.DB.TUser SchoolTUser { get; set; }
            public MasterEntity.TUser MasterTUser { get; set; }
        }
    }
}