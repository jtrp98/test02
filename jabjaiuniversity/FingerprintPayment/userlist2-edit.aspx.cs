using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

namespace FingerprintPayment
{
    public partial class userlist2_edit : StudentInfo.CsCode.StudentGateway
    {
        public bool status = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            int schoolID = GetUserData().CompanyID;
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID));
            int id = 0;
            Int32.TryParse(Request.QueryString["id"], out id);
            var data3 = _db.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == id && w.sDeleted == "false").FirstOrDefault();
            var data4 = _db.THealtProfiles.Where(w => w.SchoolID == schoolID && w.sID == id && w.sDeleted == "false").FirstOrDefault();
            Image img = (Image)FindControl("profileimage");
            var data5 = _db.TUsers.Where(w => w.sID == id).FirstOrDefault();

            Button1.Click += new EventHandler(Button1_Click);

            if (!IsPostBack)
            {

                var nTerm = _db.TTerms.Where(w=>w.SchoolID == schoolID).FirstOrDefault(f => f.dStart <= DateTime.Today && f.dEnd >= DateTime.Today);
                if (nTerm == null) nTerm = _db.TTerms.Where(w => w.SchoolID == schoolID).OrderBy(o => o.dEnd).FirstOrDefault(f => f.dStart >= DateTime.Today);
                if (nTerm == null) nTerm = _db.TTerms.Where(w => w.SchoolID == schoolID).OrderByDescending(o => o.dEnd).FirstOrDefault(f => f.dEnd <= DateTime.Today);

                //string termId = Request.QueryString["termId"];
                //if (termId.Length < 5) termId = string.Format("TS{0:0000000}", int.Parse(termId));
                //if (termId.Trim() != nTerm.nTerm.Trim())
                //{
                //    ddlstatus.Enabled = false;
                //    ddlSubLV.Enabled = false;
                //    ddlSubLV2.Enabled = false;
                //    status = false;
                //}

                var q_title = _db.TTitleLists.Where(w => w.SchoolID == schoolID && w.workStatus == "working" && w.deleted == "0").ToList();
                fcommon.LinqToDropDownList(q_title, stdtitle, "- กรุณาเลือก -", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(q_title, famTitle, "- กรุณาเลือก -", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(q_title, fatherTitle, "- กรุณาเลือก -", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(q_title, motherTitle, "- กรุณาเลือก -", "nTitleid", "titleDescription");
                fcommon.LinqToDropDownList(q_title, stayWithTitle, "- กรุณาเลือก -", "nTitleid", "titleDescription");

                JabJaiMasterEntities dbmaster = Connection.MasterEntities();
                //string sEntities = Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                var tuser = dbmaster.TUsers.Where(w => w.nSystemID == id && w.nCompany == qcompany.nCompany && w.cType == "0").FirstOrDefault();
                if (tuser.sFinger == null && tuser.sFinger2 == null)
                {
                    ltrfinger.Text = "รหัสลายนิ้วมือ";
                    btnDelFinger.Visible = false;
                    ltrpassword.Text = tuser.sPassword;
                }
                else
                {
                    ltrfinger.Text = "ลายนิ้วมือ";
                }

                if (!string.IsNullOrEmpty(data5.oldSchoolGraduated))
                {
                    oldSchoolGraduated.SelectedValue = data5.oldSchoolGraduated;
                }

                fcommon.ListYears(ddlAge, "", 1900, "en-us", "th-TH");
                fcommon.ListYears(dFamilyBirthDayYY, "", 1900, "en-us", "th-TH");
                fcommon.ListYears(dFatherBirthDayYY, "", 1900, "en-us", "th-TH");
                fcommon.ListYears(dMotherBirthDayYY, "", 1900, "en-us", "th-TH");
                var roomdata = _db.TTermSubLevel2.Where(w => w.SchoolID == schoolID && w.nTermSubLevel2 == data5.nTermSubLevel2).FirstOrDefault();
                var roomdata2 = new TSubLevel();
                if (roomdata != null)
                {
                    roomdata2 = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == roomdata.nTSubLevel).FirstOrDefault();
                    ddlSubLV.SelectedValue = roomdata2.nTSubLevel.ToString();
                }

                var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(qcompany)));
                var q2 = QueryDataBases.SubLevel2_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(qcompany)), roomdata.nTSubLevel);
                fcommon.LinqToDropDownList(q, ddlSubLV, "ทั้งหมด", "class_id", "class_name");

                fcommon.LinqToDropDownList(q2, ddlSubLV2, "ทั้งหมด", "classRoom_id", "classRoom_name");
                fcommon.LinqToDropDownList(q2, ddlroomchange, "ทั้งหมด", "classRoom_id", "classRoom_name");
                //var q3 = _db.TSubLevels.Select(s => new { s.fullName, s.nTSubLevel }).ToList();
                //fcommon.LinqToDropDownList(q3, oldSchoolGraduated, "", "nTSubLevel", "fullName");

                ddlSubLV2.SelectedValue = roomdata.nTermSubLevel2.ToString();

                DropDownList2.SelectedValue = string.Format("{0:00}", data5.dBirth.Value.Month);
                DropDownList1.SelectedValue = string.Format("{0:00}", data5.dBirth.Value.Day);
                ddlAge.SelectedValue = data5.dBirth.Value.Year.ToString();

                txtday.Text = data5.DayQuit.HasValue ? data5.DayQuit.Value.ToString("dd/MM/yyyy") : "";
                txtNote.Text = data5.Note;

                profileimage.ImageUrl = data5.sStudentPicture + "?" + DateTime.Now.ToString("HHmmssss");
                if (string.IsNullOrEmpty(data5.sStudentPicture))
                {
                    profileimage.ImageUrl = "https://jabjaistorage.blob.core.windows.net/userprofile/201782151010735704913.png";
                    profileimage.Width = 180;
                    profileimage.Height = 180;
                }

                if (data5.sStudentID == null)
                    stdid.Text = "";
                else stdid.Text = data5.sStudentID;

                if (data5.sStudentTitle == null)
                    stdtitle.SelectedValue = "";
                else stdtitle.SelectedValue = data5.sStudentTitle;

                if (data5.sName == null)
                    stdnameTh.Text = "";
                else stdnameTh.Text = data5.sName;

                if (data5.sLastname == null)
                    stdlastTh.Text = "";
                else stdlastTh.Text = data5.sLastname;

                if (data5.sStudentNameEN == null)
                    stdnameEn.Text = "";
                else stdnameEn.Text = data5.sStudentNameEN;

                if (data5.sStudentLastOther == null)
                    stdlastEn.Text = "";
                else stdlastEn.Text = data5.sStudentLastEN;

                if (data5.sStudentNameOther == null)
                    stdnameOther.Text = "";
                else stdnameOther.Text = data5.sStudentNameOther;

                if (data5.sStudentLastOther == null)
                    stdlastOther.Text = "";
                else stdlastOther.Text = data5.sStudentLastOther;

                if (data5.sNickName == null)
                    stdnickname.Text = "";
                else stdnickname.Text = data5.sNickName;

                if (data5.sStudentRace == null)
                    stdRace.Text = "";
                else stdRace.Text = data5.sStudentRace;

                if (data5.sStudentNation == null)
                    stdNation.Text = "";
                else stdNation.Text = data5.sStudentNation;

                if (data5.sStudentReligion == null)
                    stdReligion.Text = "";
                else stdReligion.Text = data5.sStudentReligion;

                if (data5.sIdentification == null)
                    stdIdnumber.Text = "";
                else stdIdnumber.Text = data5.sIdentification;

                if (data5.nStudentStatus.HasValue) ddlstatus.SelectedValue = data5.nStudentStatus.Value.ToString();

                if (data5.nSonNumber.HasValue)
                {
                    stdSon.Text = data5.nSonNumber.ToString();
                }
                else stdSon.Text = "";

                oldSchoolName.Text = data5.oldSchoolName;

                //string gender = "";
                //if (data5.cSex == "0")
                //    gender = "ชาย";
                //else gender = "หญิง";     

                stdGender.SelectedValue = data5.cSex;

                if (data5.sStudentHomeNumber == null)
                    stdHome.Text = "";
                else stdHome.Text = data5.sStudentHomeNumber;

                if (data5.sStudentSoy == null)
                    stdSoy.Text = "";
                else stdSoy.Text = data5.sStudentSoy;

                if (data5.sStudentMuu == null)
                    stdMuu.Text = "";
                else stdMuu.Text = data5.sStudentMuu;

                if (data5.sStudentRoad == null)
                    stdRoad.Text = "";
                else stdRoad.Text = data5.sStudentRoad;

                txtStudentNumber.Text = data5.nStudentNumber.ToString();

                if (data5.moveInDate != null) moveInDate.Text = data5.moveInDate.Value.ToString("dd/MM/yyyy");

                var dtprovinces = dbmaster.provinces.ToList();
                var q_amphurs = dbmaster.amphurs.ToList();
                var q_districts = dbmaster.districts.ToList();

                getProvinceValue(oldSchoolProvince, dtprovinces, data5.oldSchoolProvince);
                getAumpherValue(oldSchoolAumpher, q_amphurs, data5.oldSchoolProvince, data5.oldSchoolAumpher);
                getDistrictsValue(oldSchoolTumbon, q_districts, data5.oldSchoolAumpher, data5.oldSchoolTumbon);

                getProvinceValue(ddlProvince, dtprovinces, data5.sStudentProvince);
                getAumpherValue(ddlAumper, q_amphurs, data5.sStudentProvince, data5.sStudentAumpher);
                getDistrictsValue(ddlTumbon, q_districts, data5.sStudentAumpher, data5.sStudentTumbon);

                oldSchoolGPA.Text = data5.oldSchoolGPA.ToString();

                if (data5.sStudentPost == null)
                    stdPost.Text = "";
                else stdPost.Text = data5.sStudentPost;

                if (data5.sPhone == null)
                    stdphone.Text = "";
                else stdphone.Text = data5.sPhone;

                if (data5.sEmail == null)
                    stdEmail.Text = "";
                else stdEmail.Text = data5.sEmail;

                moveOutReason.Text = data5.moveOutReason;
                sNickNameEN.Text = data5.sNickNameEN;
                sStudentHomeRegisterCode.Text = data5.sStudentHomeRegisterCode;
                sStudentHousePhone.Text = data5.sStudentHousePhone;


                getFamilyData(data3, dtprovinces, dbmaster);
                getHealthData(data4, data5, dbmaster, _db);
            }
        }

        protected void ddlSubLV_Change(object sender, EventArgs e)
        {
            ddlSubLV2.Enabled = false;
            ddlroomchange.Enabled = false;
            ddlSubLV2.Items.Clear();
            ddlroomchange.Items.Clear();
            int stateId = int.Parse(ddlSubLV.SelectedItem.Value);
            if (stateId > 0)
            {
                BindDropDownList(stateId);
                ddlSubLV2.Enabled = true;
                ddlroomchange.Enabled = true;
            }
        }

        private void BindDropDownList(int value)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            var q2 = QueryDataBases.SubLevel2_Query.GetData(_db, value);
            fcommon.LinqToDropDownList(q2, ddlSubLV2, "ทั้งหมด", "classRoom_id", "classRoom_name");
            fcommon.LinqToDropDownList(q2, ddlroomchange, "ทั้งหมด", "classRoom_id", "classRoom_name");

        }

        void Button1_Click(object sender, EventArgs e)
        {
            if (ddlSubLV.SelectedValue == "-1")
            {
                Response.Write("<script>alert('กรุณาเลือกชั้นเรียน');</script>");
                return;
            }
            if (ddlSubLV2.SelectedValue == "-1" || string.IsNullOrEmpty(ddlSubLV2.SelectedValue))
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

            string ff = stdSon.Text;
            string f2f = stdnameEn.Text;
            string f3f = stdlastEn.Text;

            if (stdSon.Text == "1" || stdSon.Text == "2" || stdSon.Text == "3" || stdSon.Text == "4" || stdSon.Text == "5" || stdSon.Text == "6" || stdSon.Text == "7" || stdSon.Text == "8" || stdSon.Text == "9" || stdSon.Text == "10" || stdSon.Text == "")
            {
            }
            else
            {
                Response.Write("<script>alert('กรุณาใส่หมายเลขในช่อง:บุตรคนที่');</script>");
                return;
            }

            try
            {
                int schoolID = UserData.CompanyID;
                JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID));

                int id = 0;
                Int32.TryParse(Request.QueryString["id"], out id);

                var data5 = _db.TUsers.Where(w => w.SchoolID == schoolID && w.sID == id).FirstOrDefault();

                //string sEntities = Session["sEntities"].ToString();
                JabJaiMasterEntities dbmaster = Connection.MasterEntities();
                var qcompany = dbmaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                var qusermaster = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.nSystemID == data5.sID && w.cType == "0").FirstOrDefault();
                string link = "";
                if (FileUpload1.HasFile)
                {
                    link = AzureStorage.UploadFile(FileUpload1, 150, qusermaster.sID);
                }

                JabjaiEntity.DB.TUser _User = new JabjaiEntity.DB.TUser();

                _User = data5;

                _User.sName = stdnameTh.Text;
                _User.sLastname = stdlastTh.Text;
                //Response.Write(txtdBirth.Text); return;
                string birthDate = DropDownList1.SelectedValue;
                string birthMonth = DropDownList2.SelectedValue;
                string birthYear = ddlAge.SelectedValue;
                string combinedate = birthDate + "/" + birthMonth + "/" + birthYear;
                string sex = stdGender.SelectedValue.ToString();
                DateTime dt1 = DateTime.ParseExact(combinedate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                int sonddl = Int32.Parse(string.IsNullOrEmpty(stdSon.Text) ? "1" : stdSon.Text);

                DateTime? _dBirth;
                if (DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("th-th"));

                _User.dBirth = _dBirth;
                _User.sAddress = stdHome.Text + " " + stdSoy.Text + " " + stdMuu.Text + " " + stdRoad.Text + " " +
                                 ddlTumbon.Text + " " + ddlAumper.Text + " " + ddlProvince.Text + " " + stdPost.Text;

                _User.cSex = sex;
                if (FileUpload1.HasFile)
                {
                    _User.sStudentPicture = link;
                    _User.dPicUpdate = DateTime.Now;
                    _User.nPicversion = (_User.nPicversion.HasValue ? _User.nPicversion : 0) + 1;
                }

                if (ddlstatus.SelectedValue != "0" && !string.IsNullOrEmpty(txtday.Text))
                {
                    DateTime DayQuit = DateTime.ParseExact(txtday.Text, "dd/MM/yyyy", new CultureInfo("en-us"));
                    _User.DayQuit = DayQuit;
                    _User.Note = txtNote.Text;
                }

                int? Level2New = null, Level2Old = null;

                if (ddlroomchange.SelectedValue.Trim() != "")
                {
                    DateTime DayChange = DateTime.ParseExact(txtdayroomchange.Text, "dd/MM/yyyy", new CultureInfo("en-us"));
                    Level2New = int.Parse(ddlroomchange.SelectedValue);
                    Level2Old = int.Parse(ddlSubLV2.SelectedValue);
                    var RoomChanges_Id = Guid.NewGuid();
                    _db.TRoomChanges.Add(new TRoomChange
                    {
                        DayChange = DayChange,
                        sID = _User.sID,
                        Level2New = Level2New,
                        Level2Old = Level2Old,
                        RoomChangeID = RoomChanges_Id.ToString(),
                        SchoolID = schoolID
                    });
                    _User.nTermSubLevel2 = Level2New;
                }
                else
                {
                    _User.nTermSubLevel2 = Int32.Parse(ddlSubLV2.SelectedValue);
                }

                _User.sPhone = stdphone.Text;
                _User.sEmail = stdEmail.Text;
                _User.sStudentTitle = stdtitle.SelectedValue;
                _User.sStudentNameEN = stdnameEn.Text;
                _User.sStudentLastEN = stdlastEn.Text;

                _User.sStudentNameOther = stdnameOther.Text;
                _User.sStudentLastOther = stdlastOther.Text;

                _User.sStudentRace = stdRace.Text;
                _User.sStudentNation = stdNation.Text;
                _User.sStudentReligion = stdReligion.Text;

                _User.sStudentID = stdid.Text;
                _User.sIdentification = stdIdnumber.Text.Trim();
                _User.sStudentHomeNumber = stdHome.Text;
                _User.sStudentSoy = stdSoy.Text;
                _User.sStudentTumbon = ddlTumbon.SelectedValue;
                _User.sStudentProvince = ddlProvince.SelectedValue;
                _User.sStudentMuu = stdMuu.Text;
                _User.sStudentRoad = stdRoad.Text;
                _User.sStudentAumpher = ddlAumper.SelectedValue;
                _User.sStudentPost = stdPost.Text;
                _User.sNickName = stdnickname.Text;
                _User.nSonNumber = sonddl;
                _User.nStudentStatus = int.Parse(ddlstatus.SelectedValue);
                _User.dUpdate = DateTime.Now;


                DateTime? _moveIn;

                var moveIn = moveInDate.Text;
                if (moveIn != "")
                {
                    if (DateTime.ParseExact(moveIn, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _moveIn = DateTime.ParseExact(moveIn, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _moveIn = DateTime.ParseExact(moveIn, "dd/MM/yyyy", new CultureInfo("th-th"));

                    _User.moveInDate = _moveIn;
                }

                var isNumeric = false;

                _User.oldSchoolName = oldSchoolName.Text;
                _User.oldSchoolProvince = oldSchoolProvince.SelectedValue;
                _User.oldSchoolAumpher = oldSchoolAumpher.SelectedValue;
                _User.oldSchoolTumbon = oldSchoolTumbon.SelectedValue;
                //_User.addressLng = addressLng.t
                //_User.addressLat = a
                _User.moveOutReason = moveOutReason.Text;
                _User.sNickNameEN = sNickNameEN.Text;
                _User.sStudentHomeRegisterCode = sStudentHomeRegisterCode.Text;
                _User.sStudentHousePhone = sStudentHousePhone.Text;

                decimal n3;
                bool isNumeric3 = decimal.TryParse(oldSchoolGPA.Text, out n3);
                if (isNumeric3 == true)
                {
                    _User.oldSchoolGPA = n3;
                }


                isNumeric = int.TryParse(txtStudentNumber.Text, out int stdnum);
                if (txtStudentNumber.Text != "" && isNumeric == false)
                {
                    Response.Write("<script>alert('กรุณาใส่หมายเลขในช่อง:เลขที่');</script>");
                    return;
                }
                else if (isNumeric == true) _User.nStudentNumber = int.Parse(txtStudentNumber.Text);
                else _User.nStudentNumber = null;


                if (ddlstatus.SelectedValue == "4") _User.cDel = "G";
                _User.oldSchoolGraduated = oldSchoolGraduated.SelectedValue;

                _db.SaveChanges();

                #region Add Data Master

                if (ddlstatus.SelectedValue == "4") qusermaster.cDel = "G";

                qusermaster.sName = stdnameTh.Text;
                qusermaster.sLastname = stdlastTh.Text;
                qusermaster.sPhone = stdphone.Text;
                qusermaster.cSex = sex;
                qusermaster.sEmail = stdEmail.Text;
                qusermaster.dBirth = _dBirth;

                if (!string.IsNullOrEmpty(ddlProvince.SelectedValue)) qusermaster.PROVINCE_ID = int.Parse(ddlProvince.SelectedValue);
                if (!string.IsNullOrEmpty(ddlAumper.SelectedValue)) qusermaster.AMPHUR_ID = int.Parse(ddlAumper.SelectedValue);
                if (!string.IsNullOrEmpty(ddlTumbon.SelectedValue)) qusermaster.DISTRICT_ID = int.Parse(ddlTumbon.SelectedValue);
                qusermaster.username = stdid.Text;

                //var q_token = dbmaster.TTokens.FirstOrDefault(f => f.School_Id == qcompany.nCompany);
                //if (q_token != null)
                //{
                //    string ContactPeak = "";
                //    ContactPeak = ContactsAPI.SendContactId(_User.sID, sEntities, ConfigurationManager.AppSettings["connectId"], ConfigurationManager.AppSettings["password"]);
                //    if (string.IsNullOrEmpty(qusermaster.ContactPeak) && ContactPeak.Length <= 100) qusermaster.ContactPeak = ContactPeak;
                //    //else
                //}

                dbmaster.SaveChanges();

                /// TODO ==>  Update History Class
                string termId = Request.QueryString["termId"];
                if (termId.Length < 5) termId = string.Format("TS{0:0000000}", int.Parse(termId));
                var f_history = _db.TStudentClassroomHistories.FirstOrDefault(f => f.nTerm.Trim() == termId && id == f.sID && f.SchoolID == schoolID);
                if (f_history == null)
                {
                    _db.TStudentClassroomHistories.Add(new TStudentClassroomHistory
                    {
                        sID = id,
                        nTermSubLevel2 = Level2New ?? _User.nTermSubLevel2,
                        nTerm = termId,
                        SchoolID = schoolID
                    });
                }
                else
                {
                    f_history.nTermSubLevel2 = Level2New ?? _User.nTermSubLevel2;
                }

                #endregion
                //textbox

                saveFamilyData(id, _db);
                saveHealthData(id, _db);

                _db.SaveChanges();

                if (qcompany.nCompany == 119)
                {
                    UpdateMemory memory = new UpdateMemory();
                    memory.Student(_User, qusermaster);
                }

                database.InsertLog(Session["sEmpID"] + "",
                    "แก้ไขข้อมูลนักเรียน " + qusermaster.sName + " " + qusermaster.sLastname
                    , Session["sEntities"].ToString(), Request, 14, 3, 0);
                Response.Redirect("userlist2.aspx");
            }
            catch (DbEntityValidationException dbex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = dbex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(dbex.Message, " The validation errors are: ", fullErrorMessage);

                Response.Write(exceptionMessage);
            }

        }

        private void getFamilyData(TFamilyProfile data3, List<province> dtprovinces, JabJaiMasterEntities dbmaster)
        {
            var q_amphurs = dbmaster.amphurs.ToList();
            var q_districts = dbmaster.districts.ToList();

            if (data3 != null)
            {
                int PROVINCE_ID, AMPHUR_CODE;
                if (data3.sFamilyTitle != null && data3.sFamilyTitle != "")
                {
                    famTitle.SelectedValue = data3.sFamilyTitle.ToString();
                }

                familyStatus.SelectedValue = (data3.familyStatus ?? null).ToString();
                RadioButtonList1.SelectedValue = data3.sFamilyRelate;
                hiddenfathertitle.Value = data3.sFatherTitle;
                hiddenmothertitle.Value = data3.sMotherTitle;
                hiddenfamtitle.Value = data3.sFamilyTitle;

                getProvinceValue(famProvince, dtprovinces, data3.sFamilyProvince);
                getAumpherValue(famaumpher, q_amphurs, data3.sFamilyProvince, data3.sFamilyAumpher);
                getDistrictsValue(famTumbon, q_districts, data3.sFamilyAumpher, data3.sFamilyTumbon);

                getProvinceValue(motherProvince, dtprovinces, data3.sMotherProvince);
                getAumpherValue(motherAumpher, q_amphurs, data3.sMotherProvince, data3.sMotherAumpher);
                getDistrictsValue(motherTumbon, q_districts, data3.sMotherAumpher, data3.sMotherTumbon);

                getProvinceValue(fatherProvince, dtprovinces, data3.sFatherProvince);
                getAumpherValue(fatherAumpher, q_amphurs, data3.sFatherProvince, data3.sFatherAumpher);
                getDistrictsValue(fatherTumbon, q_districts, data3.sFatherAumpher, data3.sFatherTumbon);

                getProvinceValue(houseRegistrationProvince, dtprovinces, (data3.houseRegistrationProvince ?? null).ToString());
                getAumpherValue(houseRegistrationAumpher, q_amphurs, (data3.houseRegistrationProvince ?? null).ToString(), (data3.houseRegistrationAumpher ?? null).ToString());
                getDistrictsValue(houseRegistrationTumbon, q_districts, (data3.houseRegistrationAumpher ?? null).ToString(), (data3.houseRegistrationTumbon ?? null).ToString());

                getProvinceValue(bornFromProvince, dtprovinces, (data3.bornFromProvince ?? null).ToString());
                getAumpherValue(bornFromAumpher, q_amphurs, (data3.bornFromProvince ?? null).ToString(), (data3.bornFromAumpher ?? null).ToString());
                getDistrictsValue(bornFromTumbon, q_districts, (data3.bornFromAumpher ?? null).ToString(), (data3.bornFromTumbon ?? null).ToString());


                txtfriendName.Text = data3.friendName;
                txtfriendLastname.Text = data3.friendLastName;
                txtfriendTel.Text = data3.friendPhone;



                fatherIdCard.Text = data3.sFatherIdCardNumber;
                fatherLast.Text = data3.sFatherLastName;
                fatherName.Text = data3.sFatherFirstName;
                fatherTitle.Text = data3.sFatherTitle;
                fatherCeerChad.Text = data3.sFatherRace;
                fatherSunChad.Text = data3.sFatherNation;
                fatherReligion.Text = data3.sFatherReligion;
                fatherHome.Text = data3.sFatherHomeNumber;
                sFatherWorkPlace.Text = data3.sFatherWorkPlace;
                fatherSoy.Text = data3.sFatherSoy;
                fatherMuu.Text = data3.sFatherMuu;
                fatherRoad.Text = data3.sFatherRoad;
                fatherPost.Text = data3.sFatherPost;
                sFatherPhone.Text = data3.sFatherPhone;

                motherIdCard.Text = data3.sMotherIdCardNumber;
                motherLast.Text = data3.sMotherLastName;
                motherName.Text = data3.sMotherFirstName;
                motherTitle.Text = data3.sMotherTitle;
                motherCeerChad.Text = data3.sMotherRace;
                motherSunChad.Text = data3.sMotherNation;
                motherReligion.Text = data3.sMotherReligion;
                motherHome.Text = data3.sMotherHomeNumber;
                motherSoy.Text = data3.sMotherSoy;
                motherMuu.Text = data3.sMotherMuu;
                motherRoad.Text = data3.sMotherRoad;
                motherPost.Text = data3.sMotherPost;
                sMotherPhone.Text = data3.sMotherPhone;

                famTitle.Text = data3.sFamilyTitle;
                famName.Text = data3.sFamilyName;
                famLast.Text = data3.sFamilyLast;
                sFamilyJob.Text = data3.sFamilyJob;
                famIdCard.Text = data3.sFamilyIdCardNumber;
                famHomenum.Text = data3.sFamilyHomeNumber;
                famSoy.Text = data3.sFamilySoy;
                famTumbon.Text = data3.sFamilyTumbon;
                famCeerChad.Text = data3.sFamilyRace;
                famSunChad.Text = data3.sFamilyNation;
                famReligion.Text = data3.sFamilyReligion;
                famMuu.Text = data3.sFamilyMuu;
                famRoad.Text = data3.sFamilyRoad;
                famPost.Text = data3.sFamilyPost;
                famPhone1.Text = data3.sPhoneOne;
                famPhone2.Text = data3.sPhoneTwo;
                famPhone3.Text = data3.sPhoneThree;
                //famEmail.Text = data3.sPhoneMail;

                sFamilyNameEN.Text = data3.sFamilyNameEN;
                sFamilyLastEN.Text = data3.sFamilyLastEN;
                nFamilyRequestStudyMoney.Text = (data3.nFamilyRequestStudyMoney ?? 0).ToString();
                sFamilyGraduated.Text = (data3.sFamilyGraduated ?? 0).ToString();
                sFamilyWorkPlace.Text = data3.sFamilyWorkPlace;
                nFamilyIncome.Text = (data3.nFamilyIncome ?? 0).ToString();
                sFatherNameEN.Text = data3.sFatherNameEN;
                sFatherLastEN.Text = data3.sFatherLastEN;
                sFatherGraduated.Text = (data3.sFatherGraduated ?? 0).ToString();
                sFatherJob.Text = data3.sFatherJob;
                sFatherPhone2.Text = data3.sFatherPhone2;
                sFatherPhone3.Text = data3.sFatherPhone3;
                nFatherIncome.Text = (data3.nFatherIncome ?? 0).ToString();
                sMotherNameEN.Text = data3.sMotherNameEN;
                sMotherLastEN.Text = data3.sMotherLastEN;
                sMotherGraduated.SelectedValue = (data3.sMotherGraduated ?? 0).ToString();
                sMotherJob.Text = data3.sMotherJob;
                sMotherWorkPlace.Text = data3.sMotherWorkPlace;
                sMotherPhone2.Text = data3.sMotherPhone2;
                sMotherPhone3.Text = data3.sMotherPhone3;
                nMotherIncome.Text = (data3.nMotherIncome ?? 0).ToString(); ;
                nSonTotal.SelectedValue = (data3.nSonTotal ?? 0).ToString(); ;
                nRelativeStudyHere.SelectedValue = (data3.nRelativeStudyHere ?? 0).ToString();
                stayWithTitle.SelectedValue = (data3.stayWithTitle ?? 0).ToString();
                stayWithName.Text = data3.stayWithName;
                stayWithLast.Text = data3.stayWithLast;
                stayWithEmergencyCall.Text = data3.stayWithEmergencyCall;
                stayWithEmail.Text = data3.stayWithEmail;
                HomeType.SelectedValue = (data3.HomeType ?? 0).ToString();
                //friendSID.Text = (data3.friendSID ?? 0).ToString();
                houseRegistrationNumber.Text = data3.houseRegistrationNumber;
                houseRegistrationMuu.Text = data3.houseRegistrationMuu;
                houseRegistrationSoy.Text = data3.houseRegistrationSoy;
                houseRegistrationRoad.Text = data3.houseRegistrationRoad;
                houseRegistrationPost.Text = data3.houseRegistrationPost;
                houseRegistrationPhone.Text = data3.houseRegistrationPhone;
                bornFrom.Text = data3.bornFrom;

                if (data3.dMotherBirthDay.HasValue)
                {

                    dMotherBirthDayDD.SelectedValue = string.Format("{0:00}", data3.dMotherBirthDay.Value.Day);
                    dMotherBirthDayMM.SelectedValue = string.Format("{0:00}", data3.dMotherBirthDay.Value.Month);
                    dMotherBirthDayYY.SelectedValue = string.Format("{0:00}", data3.dMotherBirthDay.Value.Year);
                }

                if (data3.dFamilyBirthDay.HasValue)
                {
                    dFamilyBirthDayDD.SelectedValue = string.Format("{0:00}", data3.dFamilyBirthDay.Value.Day);
                    dFamilyBirthDayMM.SelectedValue = string.Format("{0:00}", data3.dFamilyBirthDay.Value.Month);
                    dFamilyBirthDayYY.SelectedValue = string.Format("{0:00}", data3.dFamilyBirthDay.Value.Year);
                }

                if (data3.dFatherBirthDay.HasValue)
                {
                    dFatherBirthDayDD.SelectedValue = string.Format("{0:00}", data3.dFatherBirthDay.Value.Day);
                    dFatherBirthDayMM.SelectedValue = string.Format("{0:00}", data3.dFatherBirthDay.Value.Month);
                    dFatherBirthDayYY.SelectedValue = string.Format("{0:00}", data3.dFatherBirthDay.Value.Year);
                }
            }
            else
            {
                getProvinceValue(famProvince, dtprovinces, "");
                getAumpherValue(famaumpher, q_amphurs, "", "");
                getDistrictsValue(famTumbon, q_districts, "", "");

                getProvinceValue(motherProvince, dtprovinces, "");
                getAumpherValue(motherAumpher, q_amphurs, "", "");
                getDistrictsValue(motherTumbon, q_districts, "", "");

                getProvinceValue(fatherProvince, dtprovinces, "");
                getAumpherValue(fatherAumpher, q_amphurs, "", "");
                getDistrictsValue(fatherTumbon, q_districts, "", "");

            }
        }

        private void getHealthData(THealtProfile data4, JabjaiEntity.DB.TUser data5, JabJaiMasterEntities dbmaster, JabJaiEntities _db)
        {
            int schoolID = UserData.CompanyID;
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
            var tterm2 = _db.TTermSubLevel2.Where(w => w.SchoolID == schoolID && w.nTermSubLevel2 == data5.nTermSubLevel2).FirstOrDefault();
            var tlevel = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == tterm2.nTSubLevel).FirstOrDefault();

            DateTime start = DateTime.Today; //2562
            DateTime start2 = DateTime.Today.AddYears(1); //2563
            DateTime day1 = new DateTime(start.Year, 5, 1); //1/5/2562
            DateTime day2 = new DateTime(start.Year, 8, 1); //1/8/2562
            DateTime day3 = new DateTime(start.Year, 11, 1); //1/11/2562
            DateTime day4 = new DateTime(start2.Year, 2, 1); //1/2/2563

            int mon1 = day1.Month - data5.dBirth.Value.Month; // 1/5/2562 - [5/12/2558] = 41
            int year1 = day1.Year - data5.dBirth.Value.Year; // 1/5/2562 - [5/12/2558] = 3 5
            int mon2 = day2.Month - data5.dBirth.Value.Month; // 1/8/2562 - [5/12/2558] = 44
            int year2 = day2.Year - data5.dBirth.Value.Year; // 1/8/2562 - [5/12/2558] = 3 8
            int mon3 = day3.Month - data5.dBirth.Value.Month; // 1/11/2562 - [5/12/2558] = 47
            int year3 = day3.Year - data5.dBirth.Value.Year; // 1/11/2562 - [5/12/2558] = 3 11
            int mon4 = day4.Month - data5.dBirth.Value.Month; // 1/2/2563 - [5/12/2558] = 50
            int year4 = day4.Year - data5.dBirth.Value.Year; // 1/2/2563 - [5/12/2558] = 4 2

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
                txthidden.Text = "6";
                growMonth61.Text = "";
                growMonth62.Text = "";
                growMonth63.Text = "";
                growMonth64.Text = "";

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
                txthidden.Text = "4";
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
                }

            }

            if (data4 == null)
            {

                sickFood.Text = "";
                sickDrug.Text = "";
                sickOther.Text = "";
                sicknoemal.Text = "";
                sickDanger.Text = "";
            }
            else
            {

                blood.SelectedValue = data4.sBlood;
                sickFood.Text = data4.sSickFood;
                sickDrug.Text = data4.sSickDrug;
                sickOther.Text = data4.sSickOther;
                sicknoemal.Text = data4.sSickNormal;
                sickDanger.Text = data4.sSickDanger;
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

        private void saveFamilyData(int user_id, JabJaiEntities _db)
        {
            TFamilyProfile fampro = _db.TFamilyProfiles.FirstOrDefault(w => w.sID == user_id && w.sDeleted == "false");

            if (fampro == null) fampro = new TFamilyProfile();
            if (!string.IsNullOrEmpty(famTitle.SelectedValue)) fampro.sFamilyTitle = famTitle.SelectedValue.ToString();
            fampro.sFamilyName = famName.Text;
            fampro.sFamilyLast = famLast.Text;
            fampro.sFamilyRace = famCeerChad.Text;
            fampro.sFamilyNation = famSunChad.Text;
            fampro.sFamilyReligion = famReligion.Text;
            fampro.sFamilyIdCardNumber = famIdCard.Text;
            fampro.sFamilyRelate = RadioButtonList1.SelectedValue.ToString();
            fampro.sFamilyHomeNumber = famHomenum.Text;
            fampro.sFamilySoy = famSoy.Text;
            if (!string.IsNullOrEmpty(famTumbon.SelectedValue)) fampro.sFamilyTumbon = famTumbon.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(famProvince.SelectedValue)) fampro.sFamilyProvince = famProvince.SelectedValue.ToString();
            fampro.sPhoneOne = famPhone1.Text;
            fampro.sPhoneTwo = famPhone2.Text;
            fampro.sPhoneThree = famPhone3.Text;
            //fampro.sPhoneMail = famEmail.Text;
            fampro.sFamilyMuu = famMuu.Text;
            fampro.sFamilyRoad = famRoad.Text;
            if (!string.IsNullOrEmpty(famaumpher.SelectedValue)) fampro.sFamilyAumpher = famaumpher.SelectedValue.ToString();
            fampro.sFamilyPost = famPost.Text;

            if (!string.IsNullOrEmpty(fatherTitle.SelectedValue)) fampro.sFatherTitle = fatherTitle.SelectedValue.ToString();
            fampro.sFatherFirstName = fatherName.Text;
            fampro.sFatherLastName = fatherLast.Text;
            fampro.sFatherIdCardNumber = fatherIdCard.Text;
            fampro.sFatherNation = fatherSunChad.Text;
            fampro.sFatherRace = fatherCeerChad.Text;
            fampro.sFatherReligion = fatherReligion.Text;
            fampro.sFatherHomeNumber = fatherHome.Text;
            fampro.sFatherMuu = fatherMuu.Text;
            fampro.sFatherRoad = fatherRoad.Text;
            fampro.sFatherSoy = fatherSoy.Text;
            if (!string.IsNullOrEmpty(fatherProvince.SelectedValue)) fampro.sFatherProvince = fatherProvince.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(fatherAumpher.SelectedValue)) fampro.sFatherAumpher = fatherAumpher.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(fatherTumbon.SelectedValue)) fampro.sFatherTumbon = fatherTumbon.SelectedValue.ToString();
            //fampro.sFatherPhone = fatherPhone.Text;
            fampro.sFatherPost = fatherPost.Text;

            if (!string.IsNullOrEmpty(fatherTitle.SelectedValue)) fampro.sMotherTitle = motherTitle.SelectedValue.ToString();
            fampro.sMotherFirstName = motherName.Text;
            fampro.sMotherIdCardNumber = motherIdCard.Text;
            fampro.sMotherLastName = motherLast.Text;
            fampro.sMotherNation = motherSunChad.Text;
            fampro.sMotherRace = motherCeerChad.Text;
            fampro.sMotherReligion = motherReligion.Text;
            fampro.sMotherHomeNumber = motherHome.Text;
            fampro.sMotherMuu = motherMuu.Text;
            fampro.sMotherRoad = motherRoad.Text;
            fampro.sMotherSoy = motherSoy.Text;
            if (!string.IsNullOrEmpty(motherProvince.SelectedValue)) fampro.sMotherProvince = motherProvince.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(motherAumpher.SelectedValue)) fampro.sMotherAumpher = motherAumpher.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(motherTumbon.SelectedValue)) fampro.sMotherTumbon = motherTumbon.SelectedValue.ToString();
            //fampro.sMotherPhone = motherPhone.Text;
            fampro.sMotherPost = motherPost.Text;

            if (fampro.nFamilyID == 0)
            {
                int countfamily = _db.TFamilyProfiles.Select(s => s.nFamilyID).DefaultIfEmpty(0).Max() + 1;
                fampro.sID = user_id;
                fampro.nFamilyID = countfamily;
                fampro.sDeleted = "false";
                _db.TFamilyProfiles.Add(fampro);
            }

            fampro.sFamilyNameEN = sFamilyNameEN.Text;
            fampro.sFamilyLastEN = sFamilyLastEN.Text;
            fampro.nFamilyRequestStudyMoney = ConvertInt(nFamilyRequestStudyMoney.Text);
            fampro.sFamilyGraduated = ConvertInt(sFamilyGraduated.Text);
            fampro.sFamilyWorkPlace = sFamilyWorkPlace.Text;

            var isNumeric = int.TryParse(nFamilyIncome.Text, out int n);
            if (isNumeric == true || (nFamilyIncome.Text != "-" && nFamilyIncome.Text != "")) fampro.nFamilyIncome = ConvertIntSplit(nFamilyIncome.Text);
            else
            {
                fampro.nFamilyIncome = 0;
            }
            fampro.sFamilyJob = sFamilyJob.Text;
            fampro.sFatherNameEN = sFatherNameEN.Text;
            fampro.sFatherLastEN = sFatherLastEN.Text;
            fampro.sFatherLastEN = sFatherLastEN.Text;
            fampro.sFatherGraduated = ConvertInt(sFatherGraduated.Text);
            fampro.sFatherJob = sFatherJob.Text;
            fampro.sFatherJob = sFatherJob.Text;
            fampro.sFatherWorkPlace = sFatherWorkPlace.Text;
            fampro.sFatherPhone2 = sFatherPhone2.Text;
            fampro.sFatherPhone3 = sFatherPhone3.Text;

            isNumeric = int.TryParse(nFatherIncome.Text, out int fIncome);
            if (isNumeric == true || (nFatherIncome.Text != "-" && nFatherIncome.Text != "")) fampro.nFatherIncome = ConvertIntSplit(nFatherIncome.Text);
            else
            {
                fampro.nFatherIncome = 0;
            }

            fampro.sFatherPhone = sFatherPhone.Text;
            fampro.sMotherNameEN = sMotherNameEN.Text;
            fampro.sMotherLastEN = sMotherLastEN.Text;
            //dMotherBirthDay.Text = fampro.dMotherBirthDay;
            fampro.sMotherGraduated = ConvertInt(sMotherGraduated.SelectedValue);
            fampro.sMotherJob = sMotherJob.Text;
            fampro.sMotherWorkPlace = sMotherWorkPlace.Text;
            fampro.sMotherPhone2 = sMotherPhone2.Text;
            fampro.sMotherPhone3 = sMotherPhone3.Text;

            isNumeric = int.TryParse(nMotherIncome.Text, out int mIncome);
            if (isNumeric == true || (nMotherIncome.Text != "-" && nMotherIncome.Text != "")) fampro.nMotherIncome = ConvertIntSplit(nMotherIncome.Text);
            else
            {
                fampro.nMotherIncome = 0;
            }
            fampro.sMotherPhone = sMotherPhone.Text;
            fampro.nSonTotal = ConvertInt(nSonTotal.SelectedValue);
            fampro.nRelativeStudyHere = ConvertInt(nRelativeStudyHere.SelectedValue);
            fampro.stayWithTitle = ConvertInt(stayWithTitle.SelectedValue);
            fampro.stayWithName = stayWithName.Text;
            fampro.stayWithLast = stayWithLast.Text;
            fampro.stayWithEmergencyCall = stayWithEmergencyCall.Text;
            fampro.stayWithEmail = stayWithEmail.Text;
            fampro.HomeType = ConvertInt(HomeType.SelectedValue);
            //fampro.friendSID = ConvertInt(friendSID.Text);
            fampro.friendName = txtfriendName.Text;
            fampro.friendLastName = txtfriendLastname.Text;
            fampro.friendPhone = txtfriendTel.Text;
            fampro.houseRegistrationNumber = houseRegistrationNumber.Text;
            fampro.houseRegistrationMuu = houseRegistrationMuu.Text;
            fampro.houseRegistrationSoy = houseRegistrationSoy.Text;
            fampro.houseRegistrationRoad = houseRegistrationRoad.Text;
            fampro.houseRegistrationProvince = ConvertInt(houseRegistrationProvince.Text);
            fampro.houseRegistrationAumpher = ConvertInt(houseRegistrationAumpher.Text);
            fampro.houseRegistrationTumbon = ConvertInt(houseRegistrationTumbon.Text);
            fampro.houseRegistrationPost = houseRegistrationPost.Text;
            fampro.houseRegistrationPhone = houseRegistrationPhone.Text;
            fampro.bornFrom = bornFrom.Text;

            fampro.bornFromProvince = ConvertInt(bornFromProvince.SelectedValue);
            fampro.bornFromAumpher = ConvertInt(bornFromAumpher.SelectedValue);
            fampro.bornFromTumbon = ConvertInt(bornFromTumbon.SelectedValue);
            fampro.familyStatus = ConvertInt(familyStatus.SelectedValue);

            string birthDate = "";
            string birthMonth = "";
            string birthYear = "";
            string combinedate = "";
            DateTime? _dBirth;

            if (dFatherBirthDayDD.SelectedValue != "" && dFatherBirthDayMM.SelectedValue != "" && dFatherBirthDayYY.SelectedValue != "")
            {
                birthDate = dFatherBirthDayDD.SelectedValue;
                birthMonth = dFatherBirthDayMM.SelectedValue;
                birthYear = dFatherBirthDayYY.SelectedValue;
                combinedate = birthDate + "/" + birthMonth + "/" + birthYear;

                if (DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("th-th"));
                fampro.dFatherBirthDay = _dBirth;
            }
            if (dFamilyBirthDayDD.SelectedValue != "" && dFamilyBirthDayMM.SelectedValue != "" && dFamilyBirthDayYY.SelectedValue != "")
            {
                birthDate = dFamilyBirthDayDD.SelectedValue;
                birthMonth = dFamilyBirthDayMM.SelectedValue;
                birthYear = dFamilyBirthDayYY.SelectedValue;
                combinedate = birthDate + "/" + birthMonth + "/" + birthYear;

                if (DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("th-th"));
                fampro.dFamilyBirthDay = _dBirth;
            }
            if (dMotherBirthDayDD.SelectedValue != "" && dMotherBirthDayMM.SelectedValue != "" && dMotherBirthDayYY.SelectedValue != "")
            {
                birthDate = dMotherBirthDayDD.SelectedValue;
                birthMonth = dMotherBirthDayMM.SelectedValue;
                birthYear = dMotherBirthDayYY.SelectedValue;
                combinedate = birthDate + "/" + birthMonth + "/" + birthYear;

                if (DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("th-th"));

                fampro.dMotherBirthDay = _dBirth;
            }
        }

        private int? ConvertInt(string DataValue)
        {
            if (string.IsNullOrEmpty(DataValue))
                return null;
            else
                return int.Parse(DataValue);
        }

        private int? ConvertIntSplit(string DataValue)
        {
            if (string.IsNullOrEmpty(DataValue))
                return null;
            else
            {
                var money = DataValue.Split(',');
                var money1 = "";

                for (int i = 0; i < money.Length; i++)
                {
                    money1 = money1 + money[i];
                }

                var money2 = money1.Split(new string[] { ".-" }, StringSplitOptions.None);
                var money3 = "";

                for (int i = 0; i < money2.Length; i++)
                {
                    money3 = money3 + money2[i];
                }
                var money4 = money3.Split('.');

                return Int32.Parse(money4[0]);
            }
        }

        private void saveHealthData(int user_id, JabJaiEntities _db)
        {
            int schoolID = UserData.CompanyID;
            THealtProfile health = new THealtProfile();
            //tab 3

            var data4 = _db.THealtProfiles.Where(w => w.SchoolID == schoolID && w.sID == user_id && w.sDeleted == "false").FirstOrDefault();
            THealtProfile healthpro = new THealtProfile();

            if (data4 == null)
            {
                int counthealth = _db.THealtProfiles.Where(w=>w.SchoolID == schoolID).Select(s => s.nHealthID).DefaultIfEmpty(0).Max();

                healthpro.nHealthID = counthealth + 1;
                healthpro.sID = user_id;
                healthpro.sDeleted = "false";
            }
            else
            {
                healthpro = data4;
            }

            healthpro.sBlood = blood.SelectedValue;
            healthpro.sSickFood = sickFood.Text;
            healthpro.sSickDrug = sickDrug.Text;
            healthpro.sSickOther = sickOther.Text;
            healthpro.sSickNormal = sicknoemal.Text;
            healthpro.sSickDanger = sickDanger.Text;
            healthpro.Weight1_1 = weight11.Text;
            healthpro.Weight1_2 = weight12.Text;
            healthpro.Weight1_3 = weight13.Text;
            healthpro.Weight1_4 = weight14.Text;
            healthpro.Weight2_1 = weight21.Text;
            healthpro.Weight2_2 = weight22.Text;
            healthpro.Weight2_3 = weight23.Text;
            healthpro.Weight2_4 = weight24.Text;
            healthpro.Weight3_1 = weight31.Text;
            healthpro.Weight3_2 = weight32.Text;
            healthpro.Weight3_3 = weight33.Text;
            healthpro.Weight3_4 = weight34.Text;
            healthpro.Weight4_1 = weight41.Text;
            healthpro.Weight4_2 = weight42.Text;
            healthpro.Weight4_3 = weight43.Text;
            healthpro.Weight4_4 = weight44.Text;
            healthpro.Weight5_1 = weight51.Text;
            healthpro.Weight5_2 = weight52.Text;
            healthpro.Weight5_3 = weight53.Text;
            healthpro.Weight5_4 = weight54.Text;
            healthpro.Weight6_1 = weight61.Text;
            healthpro.Weight6_2 = weight62.Text;
            healthpro.Weight6_3 = weight63.Text;
            healthpro.Weight6_4 = weight64.Text;
            healthpro.Height1_1 = height11.Text;
            healthpro.Height1_2 = height12.Text;
            healthpro.Height1_3 = height13.Text;
            healthpro.Height1_4 = height14.Text;
            healthpro.Height2_1 = height21.Text;
            healthpro.Height2_2 = height22.Text;
            healthpro.Height2_3 = height23.Text;
            healthpro.Height2_4 = height24.Text;
            healthpro.Height3_1 = height31.Text;
            healthpro.Height3_2 = height32.Text;
            healthpro.Height3_3 = height33.Text;
            healthpro.Height3_4 = height34.Text;
            healthpro.Height4_1 = height41.Text;
            healthpro.Height4_2 = height42.Text;
            healthpro.Height4_3 = height43.Text;
            healthpro.Height4_4 = height44.Text;
            healthpro.Height5_1 = height51.Text;
            healthpro.Height5_2 = height52.Text;
            healthpro.Height5_3 = height53.Text;
            healthpro.Height5_4 = height54.Text;
            healthpro.Height6_1 = height61.Text;
            healthpro.Height6_2 = height62.Text;
            healthpro.Height6_3 = height63.Text;
            healthpro.Height6_4 = height64.Text;

            string weightNow = "";
            string heightNow = "";

            weightNow = isNumber(weight11.Text, weightNow);
            weightNow = isNumber(weight12.Text, weightNow);
            weightNow = isNumber(weight13.Text, weightNow);
            weightNow = isNumber(weight14.Text, weightNow);
            weightNow = isNumber(weight21.Text, weightNow);
            weightNow = isNumber(weight22.Text, weightNow);
            weightNow = isNumber(weight23.Text, weightNow);
            weightNow = isNumber(weight24.Text, weightNow);
            weightNow = isNumber(weight31.Text, weightNow);
            weightNow = isNumber(weight32.Text, weightNow);
            weightNow = isNumber(weight33.Text, weightNow);
            weightNow = isNumber(weight34.Text, weightNow);
            weightNow = isNumber(weight41.Text, weightNow);
            weightNow = isNumber(weight42.Text, weightNow);
            weightNow = isNumber(weight43.Text, weightNow);
            weightNow = isNumber(weight44.Text, weightNow);
            weightNow = isNumber(weight51.Text, weightNow);
            weightNow = isNumber(weight52.Text, weightNow);
            weightNow = isNumber(weight53.Text, weightNow);
            weightNow = isNumber(weight54.Text, weightNow);
            weightNow = isNumber(weight61.Text, weightNow);
            weightNow = isNumber(weight62.Text, weightNow);
            weightNow = isNumber(weight63.Text, weightNow);
            weightNow = isNumber(weight64.Text, weightNow);

            heightNow = isNumber(height11.Text, heightNow);
            heightNow = isNumber(height12.Text, heightNow);
            heightNow = isNumber(height13.Text, heightNow);
            heightNow = isNumber(height14.Text, heightNow);
            heightNow = isNumber(height21.Text, heightNow);
            heightNow = isNumber(height22.Text, heightNow);
            heightNow = isNumber(height23.Text, heightNow);
            heightNow = isNumber(height24.Text, heightNow);
            heightNow = isNumber(height31.Text, heightNow);
            heightNow = isNumber(height32.Text, heightNow);
            heightNow = isNumber(height33.Text, heightNow);
            heightNow = isNumber(height34.Text, heightNow);
            heightNow = isNumber(height41.Text, heightNow);
            heightNow = isNumber(height42.Text, heightNow);
            heightNow = isNumber(height43.Text, heightNow);
            heightNow = isNumber(height44.Text, heightNow);
            heightNow = isNumber(height51.Text, heightNow);
            heightNow = isNumber(height52.Text, heightNow);
            heightNow = isNumber(height53.Text, heightNow);
            heightNow = isNumber(height54.Text, heightNow);
            heightNow = isNumber(height61.Text, heightNow);
            heightNow = isNumber(height62.Text, heightNow);
            heightNow = isNumber(height63.Text, heightNow);
            heightNow = isNumber(height64.Text, heightNow);

            double n;
            bool isNumeric = double.TryParse(weightNow, out n);
            if (isNumeric == true)
            {
                healthpro.nWeight = n;
            }
            else healthpro.nWeight = 0;

            double n2;
            bool isNumeric2 = double.TryParse(heightNow, out n2);
            if (isNumeric2 == true)
            {
                healthpro.nHeight = n2;
            }
            else healthpro.nHeight = 0;


            if (data4 == null)
            {
                _db.THealtProfiles.Add(healthpro);
            }
        }

        private string isNumber(string text, string now)
        {
            double n;
            bool isNumeric = double.TryParse(text, out n);
            if (isNumeric == true) now = n.ToString();

            return now;
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

            int AMPHUR_CODE = int.Parse(bornFromAumpher.SelectedValue);
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

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int PROVINCE_ID = int.Parse(ddlProvince.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), ddlAumper, "", "AMPHUR_ID", "AMPHUR_NAME");

            int AMPHUR_CODE = int.Parse(ddlAumper.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), ddlTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
        }

        protected void ddlAumper_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            int AMPHUR_CODE = int.Parse(ddlAumper.SelectedValue);
            var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), ddlTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            stdPost.Text = qAMPHUR.POSTCODE;
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
            int PROVINCE_ID = int.Parse(famProvince.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), famaumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

            int AMPHUR_CODE = int.Parse(famaumpher.SelectedValue);
            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), famTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
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

        private void getProvinceValue(DropDownList dropDown, List<province> provinces, string provinces_id)
        {
            fcommon.LinqToDropDownList(provinces.ToList(), dropDown, "- กรุณาเลือก -", "PROVINCE_ID", "PROVINCE_NAME");
            if (string.IsNullOrEmpty(provinces_id)) dropDown.SelectedIndex = 0;
            else
            {
                int PROVINCE_ID;
                int.TryParse(provinces_id, out PROVINCE_ID);
                dropDown.SelectedValue = provinces_id;
            }
        }

        private void getAumpherValue(DropDownList dropDown, List<amphur> amphurs, string provinces_id, string amphurs_id)
        {

            if (!string.IsNullOrEmpty(provinces_id))
            {
                int PROVINCE_ID;
                int.TryParse(provinces_id, out PROVINCE_ID);
                var q_amphurs = amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList();
                fcommon.LinqToDropDownList(q_amphurs, dropDown, "- กรุณาเลือก -", "AMPHUR_ID", "AMPHUR_NAME");
                if (!string.IsNullOrEmpty(amphurs_id))
                {
                    dropDown.SelectedValue = amphurs_id;
                }
            }
            else
            {
                dropDown.Items.Insert(0, new ListItem("- กรุณาเลือก -", ""));
            }
        }

        private void getDistrictsValue(DropDownList dropDown, List<district> districts, string amphurs_id, string districts_id)
        {
            if (!string.IsNullOrEmpty(amphurs_id))
            {
                int AMPHUR_ID;
                int.TryParse(amphurs_id, out AMPHUR_ID);
                fcommon.LinqToDropDownList(districts.Where(w => w.AMPHUR_ID == AMPHUR_ID).ToList(), dropDown, "- กรุณาเลือก -", "DISTRICT_ID", "DISTRICT_NAME");
                if (!string.IsNullOrEmpty(districts_id))
                {
                    dropDown.SelectedValue = districts_id;
                }
            }
            else
            {
                dropDown.Items.Insert(0, new ListItem("- กรุณาเลือก -", ""));
            }
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