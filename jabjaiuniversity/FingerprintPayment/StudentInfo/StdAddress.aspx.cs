using FingerprintPayment.Class;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Employees.CsCode;
using JabjaiMainClass;
using System.Diagnostics;
using FingerprintPayment.StudentInfo.CsCode;

namespace FingerprintPayment.StudentInfo
{
    public partial class StdAddress : StudentGateway
    {
        private static string SessionPrimaryKey = "STUDENTID";
        private static string SessionFamilyPrimaryKey = "FAMILYID";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        #region Method

        private void InitialPage()
        {
            string v = Request.QueryString["v"];
            switch (v)
            {
                case "list":
                    MvContent.ActiveViewIndex = 0; break;
                case "form":
                    MvContent.ActiveViewIndex = 1;

                    // Init data dropdown
                   

                    int schoolID = UserData.CompanyID;
                    using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {

                        // Title
                        var titleLists = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1").ToList();
                        foreach (var r in titleLists)
                        {
                            this.ltrHomeStayWithTitle.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTitleid, r.titleDescription);
                        }

                        // Province
                        var provinces = dbMaster.provinces.ToList();
                        foreach (var r in provinces)
                        {
                            this.ltrRegisterHomeProvince.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.PROVINCE_ID, r.PROVINCE_NAME);
                        }
                        this.ltrHomeProvince.Text = this.ltrBornFromProvince.Text = this.ltrRegisterHomeProvince.Text;
                    }
                    break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        //#ที่อยู่ตามทะเบียนบ้าน
        [WebMethod(EnableSession = true)]
        public static string GetAddressItem(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    JabjaiEntity.DB.TUser user = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == iStdID).FirstOrDefault();

                    TFamilyProfile p = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 14; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        //HttpContext.Current.Session[SessionFamilyPrimaryKey] = p.nFamilyID;

                        dt.Rows[0]["F0"] = p.nFamilyID.ToString();
                        dt.Rows[0]["F1"] = user?.sStudentHomeRegisterCode;
                        dt.Rows[0]["F2"] = p.houseRegistrationNumber;
                        dt.Rows[0]["F3"] = p.houseRegistrationSoy;
                        dt.Rows[0]["F4"] = p.houseRegistrationMuu;
                        dt.Rows[0]["F5"] = p.houseRegistrationRoad;
                        dt.Rows[0]["F6"] = p.houseRegistrationProvince?.ToString();
                        dt.Rows[0]["F7"] = p.houseRegistrationAumpher?.ToString();
                        dt.Rows[0]["F8"] = p.houseRegistrationTumbon?.ToString();
                        dt.Rows[0]["F9"] = p.houseRegistrationPost;
                        dt.Rows[0]["F10"] = p.houseRegistrationPhone;
                        dt.Rows[0]["F11"] = p.bornFrom;
                        dt.Rows[0]["F12"] = p.bornFromProvince?.ToString();
                        dt.Rows[0]["F13"] = p.bornFromAumpher?.ToString();
                        dt.Rows[0]["F14"] = p.bornFromTumbon?.ToString();

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                //if (infor == "new" || infor == "error") HttpContext.Current.Session[SessionFamilyPrimaryKey] = null;

                return infor;
            }
        }

        //#ที่อยู่ตามทะเบียนบ้าน
        [WebMethod(EnableSession = true)]
        public static string SaveAddressItem(List<string> data)
        {
            string isComplete = "";
            bool flagComplete = true;

            string logMessage = "";
            int logAction = 0;

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
               
                    try
                    {
                        //int stdID = (int)HttpContext.Current.Session[SessionPrimaryKey];
                        string[] key = data[0].Split('-');
                        int stdID = int.Parse(key[0]); // sid
                        int fmlID = int.Parse(key[1]); // fmlid

                        string RegisterHomeCode = data[1];
                        string RegisterHomeNo = data[2];
                        string RegisterHomeSoi = data[3];
                        string RegisterHomeMoo = data[4];
                        string RegisterHomeRoad = data[5];
                        int? RegisterHomeProvince = string.IsNullOrEmpty(data[6]) ? (int?)null : int.Parse(data[6]);
                        int? RegisterHomeAmphoe = string.IsNullOrEmpty(data[7]) ? (int?)null : int.Parse(data[7]);
                        int? RegisterHomeTombon = string.IsNullOrEmpty(data[8]) ? (int?)null : int.Parse(data[8]);
                        string RegisterHomePostalCode = data[9];
                        string RegisterHomePhone = data[10];
                        string BornFrom = data[11];
                        int? BornFromProvince = string.IsNullOrEmpty(data[12]) ? (int?)null : int.Parse(data[12]);
                        int? BornFromAmphoe = string.IsNullOrEmpty(data[13]) ? (int?)null : int.Parse(data[13]);
                        int? BornFromTombon = string.IsNullOrEmpty(data[14]) ? (int?)null : int.Parse(data[14]);

                        //if (HttpContext.Current.Session[SessionFamilyPrimaryKey] == null)
                        if (fmlID == 0)
                        {
                            // Insert Section
                            //fmlID = (int)(en.TFamilyProfiles.Count() == 0 ? 1 : en.TFamilyProfiles.Max(m => m.nFamilyID) + 1);

                            // Get Item
                            TFamilyProfile p = new TFamilyProfile
                            {
                                sID = stdID,
                                //nFamilyID = fmlID,
                                houseRegistrationNumber = RegisterHomeNo,
                                houseRegistrationSoy = RegisterHomeSoi,
                                houseRegistrationMuu = RegisterHomeMoo,
                                houseRegistrationRoad = RegisterHomeRoad,
                                houseRegistrationProvince = RegisterHomeProvince,
                                houseRegistrationAumpher = RegisterHomeAmphoe,
                                houseRegistrationTumbon = RegisterHomeTombon,
                                houseRegistrationPost = RegisterHomePostalCode,
                                houseRegistrationPhone = RegisterHomePhone,
                                bornFrom = BornFrom,
                                bornFromProvince = BornFromProvince,
                                bornFromAumpher = BornFromAmphoe,
                                bornFromTumbon = BornFromTombon,

                                sDeleted = "false",
                                SchoolID = schoolID,
                                CreatedBy = userData.UserID,
                                CreatedDate = DateTime.Now
                            };

                            en.TFamilyProfiles.Add(p);

                            // Update some field TUser
                            JabjaiEntity.DB.TUser pi = en.TUser.First(f => f.SchoolID == schoolID && f.sID == stdID);

                            pi.sStudentHomeRegisterCode = RegisterHomeCode;
                            //pi.sStudentHomeNumber = RegisterHomeNo;
                            //pi.sStudentSoy = RegisterHomeSoi;
                            //pi.sStudentMuu = RegisterHomeMoo;
                            //pi.sStudentRoad = RegisterHomeRoad;
                            //pi.sStudentProvince = RegisterHomeProvince?.ToString();
                            //pi.sStudentAumpher = RegisterHomeAmphoe?.ToString();
                            //pi.sStudentTumbon = RegisterHomeTombon?.ToString();
                            //pi.sStudentPost = RegisterHomePostalCode;
                            //pi.sStudentHousePhone = RegisterHomePhone;

                            en.SaveChanges();

                            fmlID = p.nFamilyID;

                            isComplete = "complete-" + fmlID;

                            logMessage = "เพิ่มข้อมูลนักเรียน(ที่อยู่ตามทะเบียนบ้าน) รหัส: " + fmlID + ", ชื่อ: " + pi.sName + " " + pi.sLastname;
                            logAction = 2;
                        }
                        else
                        {
                            // Modify Section
                            //fmlID = Convert.ToInt32(HttpContext.Current.Session[SessionFamilyPrimaryKey]);

                            // Get Item
                            TFamilyProfile p = en.TFamilyProfiles.First(f => f.SchoolID == schoolID && f.nFamilyID == fmlID);

                            p.houseRegistrationNumber = RegisterHomeNo;
                            p.houseRegistrationSoy = RegisterHomeSoi;
                            p.houseRegistrationMuu = RegisterHomeMoo;
                            p.houseRegistrationRoad = RegisterHomeRoad;
                            p.houseRegistrationProvince = RegisterHomeProvince;
                            p.houseRegistrationAumpher = RegisterHomeAmphoe;
                            p.houseRegistrationTumbon = RegisterHomeTombon;
                            p.houseRegistrationPost = RegisterHomePostalCode;
                            p.houseRegistrationPhone = RegisterHomePhone;
                            p.bornFrom = BornFrom;
                            p.bornFromProvince = BornFromProvince;
                            p.bornFromAumpher = BornFromAmphoe;
                            p.bornFromTumbon = BornFromTombon;
                            p.UpdatedBy = userData.UserID;
                            p.UpdatedDate = DateTime.Now;

                            // Update some field TUser
                            JabjaiEntity.DB.TUser pi = en.TUser.First(f => f.SchoolID == schoolID && f.sID == stdID);

                            pi.sStudentHomeRegisterCode = RegisterHomeCode;
                            //pi.sStudentHomeNumber = RegisterHomeNo;
                            //pi.sStudentSoy = RegisterHomeSoi;
                            //pi.sStudentMuu = RegisterHomeMoo;
                            //pi.sStudentRoad = RegisterHomeRoad;
                            //pi.sStudentProvince = RegisterHomeProvince?.ToString();
                            //pi.sStudentAumpher = RegisterHomeAmphoe?.ToString();
                            //pi.sStudentTumbon = RegisterHomeTombon?.ToString();
                            //pi.sStudentPost = RegisterHomePostalCode;
                            //pi.sStudentHousePhone = RegisterHomePhone;

                            en.SaveChanges();

                            isComplete = "complete-" + fmlID;

                            logMessage = "อัปเดตข้อมูลนักเรียน(ที่อยู่ตามทะเบียนบ้าน) รหัส: " + fmlID + ", ชื่อ: " + pi.sName + " " + pi.sLastname;
                            logAction = 3;
                        }

                       
                    }
                    catch (Exception err)
                    {
                        isComplete = "error-" + err.Message + " :line " + ComFunction.GetLineNumberError(err);

                        flagComplete = false;

                        
                    }
                
            }

            if (flagComplete)
            {
                database.InsertLog(userData.UserID.ToString(), logMessage, HttpContext.Current.Request, 14, logAction, 0, schoolID);
            }

            return isComplete;
        }

        //#ที่อยู่ปัจจุบัน
        [WebMethod(EnableSession = true)]
        public static string GetContactAddressItem(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    JabjaiEntity.DB.TUser user = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == iStdID).FirstOrDefault();

                    TFamilyProfile p = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 18; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        //HttpContext.Current.Session[SessionFamilyPrimaryKey] = p.nFamilyID;

                        dt.Rows[0]["F0"] = p.nFamilyID.ToString();
                        dt.Rows[0]["F1"] = user?.sStudentHomeNumber;
                        dt.Rows[0]["F2"] = user?.sStudentSoy;
                        dt.Rows[0]["F3"] = user?.sStudentMuu;
                        dt.Rows[0]["F4"] = user?.sStudentRoad;
                        dt.Rows[0]["F5"] = user?.sStudentProvince;
                        dt.Rows[0]["F6"] = user?.sStudentAumpher;
                        dt.Rows[0]["F7"] = user?.sStudentTumbon;
                        dt.Rows[0]["F8"] = user?.sStudentPost;
                        dt.Rows[0]["F9"] = user?.sStudentHousePhone;

                        dt.Rows[0]["F10"] = p.stayWithTitle?.ToString();
                        dt.Rows[0]["F11"] = p.stayWithName;
                        dt.Rows[0]["F12"] = p.stayWithLast;
                        dt.Rows[0]["F13"] = p.stayWithEmergencyCall;
                        dt.Rows[0]["F14"] = p.stayWithEmail;
                        dt.Rows[0]["F15"] = p.friendName;
                        dt.Rows[0]["F16"] = p.friendLastName;
                        dt.Rows[0]["F17"] = p.friendPhone;
                        dt.Rows[0]["F18"] = p.HomeType == null ? "0" : p.HomeType.ToString();

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                //if (infor == "new" || infor == "error") HttpContext.Current.Session[SessionFamilyPrimaryKey] = null;

                return infor;
            }
        }

        //#ที่อยู่ปัจจุบัน
        [WebMethod(EnableSession = true)]
        public static string SaveContactAddressItem(List<string> data)
        {
            string isComplete = "";
            bool flagComplete = true;

            string logMessage = "";
            int logAction = 0;

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
               
                    

                    try
                    {
                        //int stdID = (int)HttpContext.Current.Session[SessionPrimaryKey];
                        string[] key = data[0].Split('-');
                        int stdID = int.Parse(key[0]); // sid
                        int fmlID = int.Parse(key[1]); // fmlid

                        string HomeNo = data[1];
                        string HomeSoi = data[2];
                        string HomeMoo = data[3];
                        string HomeRoad = data[4];
                        string HomeProvince = data[5];
                        string HomeAmphoe = data[6];
                        string HomeTombon = data[7];
                        string HomePostalCode = data[8];
                        string HomePhone = data[9];
                        int? HomeStayWithTitle = string.IsNullOrEmpty(data[10]) ? (int?)null : int.Parse(data[10]);
                        string HomeStayWithName = data[11];
                        string HomeStayWithLast = data[12];
                        string HomeStayWithEmergencyCall = data[13];
                        string HomeStayWithEmergencyEmail = data[14];
                        string HomeFriendName = data[15];
                        string HomeFriendLastName = data[16];
                        string HomeFriendPhone = data[17];
                        int? sltHomeHomeType = string.IsNullOrEmpty(data[18]) ? (int?)null : int.Parse(data[18]);

                        //if (HttpContext.Current.Session[SessionFamilyPrimaryKey] == null)
                        if (fmlID == 0)
                        {
                            // Insert Section
                            //fmlID = (int)(en.TFamilyProfiles.Count() == 0 ? 1 : en.TFamilyProfiles.Max(m => m.nFamilyID) + 1);

                            // Get Item
                            TFamilyProfile p = new TFamilyProfile
                            {
                                sID = stdID,
                                //nFamilyID = fmlID,
                                stayWithTitle = HomeStayWithTitle,
                                stayWithName = HomeStayWithName,
                                stayWithLast = HomeStayWithLast,
                                stayWithEmergencyCall = HomeStayWithEmergencyCall,
                                stayWithEmail = HomeStayWithEmergencyEmail,
                                friendName = HomeFriendName,
                                friendLastName = HomeFriendLastName,
                                friendPhone = HomeFriendPhone,
                                HomeType = sltHomeHomeType,

                                sDeleted = "false",
                                SchoolID = schoolID,
                                CreatedBy = userData.UserID,
                                CreatedDate = DateTime.Now
                            };

                            en.TFamilyProfiles.Add(p);

                            // Update some field TUser
                            JabjaiEntity.DB.TUser pi = en.TUser.First(f => f.SchoolID == schoolID && f.sID == stdID);

                            pi.sStudentHomeNumber = HomeNo;
                            pi.sStudentSoy = HomeSoi;
                            pi.sStudentMuu = HomeMoo;
                            pi.sStudentRoad = HomeRoad;
                            pi.sStudentProvince = HomeProvince;
                            pi.sStudentAumpher = HomeAmphoe;
                            pi.sStudentTumbon = HomeTombon;
                            pi.sStudentPost = HomePostalCode;
                            pi.sStudentHousePhone = HomePhone;
                            pi.UpdatedBy = userData.UserID;
                            pi.UpdatedDate = DateTime.Now;

                            en.SaveChanges();

                            fmlID = p.nFamilyID;

                            // Access to Master Database
                            MasterEntity.TUser userMasterData = dbMaster.TUsers.Where(w => w.nSystemID == stdID && w.nCompany == schoolID && w.cType == "0").FirstOrDefault();
                            if (userMasterData != null)
                            {
                                userMasterData.PROVINCE_ID = string.IsNullOrEmpty(HomeProvince) ? (int?)null : int.Parse(HomeProvince);
                                userMasterData.AMPHUR_ID = string.IsNullOrEmpty(HomeAmphoe) ? (int?)null : int.Parse(HomeAmphoe);
                                userMasterData.DISTRICT_ID = string.IsNullOrEmpty(HomeTombon) ? (int?)null : int.Parse(HomeTombon);

                                userMasterData.dUpdate = DateTime.Now;
                            }

                            dbMaster.SaveChanges();

                            isComplete = "complete-" + fmlID;

                            logMessage = "เพิ่มข้อมูลนักเรียน(ที่อยู่ปัจจุบัน) รหัส: " + fmlID + ", ชื่อ: " + pi.sName + " " + pi.sLastname;
                            logAction = 2;
                        }
                        else
                        {
                            // Modify Section
                            //fmlID = Convert.ToInt32(HttpContext.Current.Session[SessionFamilyPrimaryKey]);

                            // Get Item
                            TFamilyProfile p = en.TFamilyProfiles.First(f => f.SchoolID == schoolID && f.nFamilyID == fmlID);

                            p.stayWithTitle = HomeStayWithTitle;
                            p.stayWithName = HomeStayWithName;
                            p.stayWithLast = HomeStayWithLast;
                            p.stayWithEmergencyCall = HomeStayWithEmergencyCall;
                            p.stayWithEmail = HomeStayWithEmergencyEmail;
                            p.friendName = HomeFriendName;
                            p.friendLastName = HomeFriendLastName;
                            p.friendPhone = HomeFriendPhone;
                            p.HomeType = sltHomeHomeType;
                            p.UpdatedBy = userData.UserID;
                            p.UpdatedDate = DateTime.Now;

                            // Update some field TUser
                            JabjaiEntity.DB.TUser pi = en.TUser.First(f => f.SchoolID == schoolID && f.sID == stdID);

                            pi.sStudentHomeNumber = HomeNo;
                            pi.sStudentSoy = HomeSoi;
                            pi.sStudentMuu = HomeMoo;
                            pi.sStudentRoad = HomeRoad;
                            pi.sStudentProvince = HomeProvince;
                            pi.sStudentAumpher = HomeAmphoe;
                            pi.sStudentTumbon = HomeTombon;
                            pi.sStudentPost = HomePostalCode;
                            pi.sStudentHousePhone = HomePhone;
                            pi.UpdatedBy = userData.UserID;
                            pi.UpdatedDate = DateTime.Now;

                            en.SaveChanges();

                            // Access to Master Database
                            MasterEntity.TUser userMasterData = dbMaster.TUsers.Where(w => w.nSystemID == stdID && w.nCompany == schoolID && w.cType == "0").FirstOrDefault();
                            if (userMasterData != null)
                            {
                                userMasterData.PROVINCE_ID = string.IsNullOrEmpty(HomeProvince) ? (int?)null : int.Parse(HomeProvince);
                                userMasterData.AMPHUR_ID = string.IsNullOrEmpty(HomeAmphoe) ? (int?)null : int.Parse(HomeAmphoe);
                                userMasterData.DISTRICT_ID = string.IsNullOrEmpty(HomeTombon) ? (int?)null : int.Parse(HomeTombon);

                                userMasterData.dUpdate = DateTime.Now;
                            }

                            dbMaster.SaveChanges();

                            isComplete = "complete-" + fmlID;

                            logMessage = "อัปเดตข้อมูลนักเรียน(ที่อยู่ปัจจุบัน) รหัส: " + fmlID + ", ชื่อ: " + pi.sName + " " + pi.sLastname;
                            logAction = 3;
                        }

                       
                    }
                    catch (Exception err)
                    {
                        isComplete = "error-" + err.Message + " :line " + ComFunction.GetLineNumberError(err);

                        flagComplete = false;

                       
                    }
                
            }

            if (flagComplete)
            {
                database.InsertLog(userData.UserID.ToString(), logMessage, HttpContext.Current.Request, 14, logAction, 0, schoolID);
            }

            return isComplete;
        }

        [WebMethod]
        public static string ClearSessionID()
        {
            //HttpContext.Current.Session[SessionFamilyPrimaryKey] = null;

            return "";
        }

        //#ที่อยู่ตามทะเบียนบ้าน
        [WebMethod(EnableSession = true)]
        public static string GetAddressItemView(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

               

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    JabjaiEntity.DB.TUser user = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == iStdID).FirstOrDefault();

                    TFamilyProfile p = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 14; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F0"] = p.nFamilyID.ToString();
                        dt.Rows[0]["F1"] = user.sStudentHomeRegisterCode;
                        dt.Rows[0]["F2"] = p.houseRegistrationNumber;
                        dt.Rows[0]["F3"] = p.houseRegistrationSoy;
                        dt.Rows[0]["F4"] = p.houseRegistrationMuu;
                        dt.Rows[0]["F5"] = p.houseRegistrationRoad;

                        var province = "";
                        var provinceObj = dbMaster.provinces.Where(w => w.PROVINCE_ID == p.houseRegistrationProvince).FirstOrDefault();
                        if (provinceObj != null)
                        {
                            province = provinceObj.PROVINCE_NAME;
                        }
                        dt.Rows[0]["F6"] = province;

                        var aumpher = "";
                        var aumpherObj = dbMaster.amphurs.Where(w => w.PROVINCE_ID == p.houseRegistrationProvince && w.AMPHUR_ID == p.houseRegistrationAumpher).FirstOrDefault();
                        if (aumpherObj != null)
                        {
                            aumpher = aumpherObj.AMPHUR_NAME;
                        }
                        dt.Rows[0]["F7"] = aumpher;

                        var tumbon = "";
                        var tumbonObj = dbMaster.districts.Where(w => w.AMPHUR_ID == p.houseRegistrationAumpher && w.DISTRICT_ID == p.houseRegistrationTumbon).FirstOrDefault();
                        if (tumbonObj != null)
                        {
                            tumbon = tumbonObj.DISTRICT_NAME;
                        }
                        dt.Rows[0]["F8"] = tumbon;

                        dt.Rows[0]["F9"] = p.houseRegistrationPost;
                        dt.Rows[0]["F10"] = p.houseRegistrationPhone;
                        dt.Rows[0]["F11"] = p.bornFrom;

                        var province2 = "";
                        var provinceObj2 = dbMaster.provinces.Where(w => w.PROVINCE_ID == p.bornFromProvince).FirstOrDefault();
                        if (provinceObj2 != null)
                        {
                            province2 = provinceObj2.PROVINCE_NAME;
                        }
                        dt.Rows[0]["F12"] = province2;

                        var aumpher2 = "";
                        var aumpherObj2 = dbMaster.amphurs.Where(w => w.PROVINCE_ID == p.bornFromProvince && w.AMPHUR_ID == p.bornFromAumpher).FirstOrDefault();
                        if (aumpherObj2 != null)
                        {
                            aumpher2 = aumpherObj2.AMPHUR_NAME;
                        }
                        dt.Rows[0]["F13"] = aumpher2;

                        var tumbon2 = "";
                        var tumbonObj2 = dbMaster.districts.Where(w => w.AMPHUR_ID == p.bornFromAumpher && w.DISTRICT_ID == p.bornFromTumbon).FirstOrDefault();
                        if (tumbonObj2 != null)
                        {
                            tumbon2 = tumbonObj2.DISTRICT_NAME;
                        }
                        dt.Rows[0]["F14"] = tumbon2;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                return infor;
            }
        }

        //#ที่อยู่ปัจจุบัน
        [WebMethod(EnableSession = true)]
        public static string GetContactAddressItemView(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    JabjaiEntity.DB.TUser user = en.TUser.Where(w => w.SchoolID == schoolID && w.sID == iStdID).FirstOrDefault();

                    TFamilyProfile p = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 18; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F0"] = p.nFamilyID.ToString();
                        dt.Rows[0]["F1"] = user.sStudentHomeNumber;
                        dt.Rows[0]["F2"] = user.sStudentSoy;
                        dt.Rows[0]["F3"] = user.sStudentMuu;
                        dt.Rows[0]["F4"] = user.sStudentRoad;

                        var provinceID = string.IsNullOrEmpty(user.sStudentProvince) ? 0 : int.Parse(user.sStudentProvince);
                        var province = "";
                        var provinceObj = dbMaster.provinces.Where(w => w.PROVINCE_ID == provinceID).FirstOrDefault();
                        if (provinceObj != null)
                        {
                            province = provinceObj.PROVINCE_NAME;
                        }
                        dt.Rows[0]["F5"] = province;

                        var aumpherID = string.IsNullOrEmpty(user.sStudentAumpher) ? 0 : int.Parse(user.sStudentAumpher);
                        var aumpher = "";
                        var aumpherObj = dbMaster.amphurs.Where(w => w.PROVINCE_ID == provinceID && w.AMPHUR_ID == aumpherID).FirstOrDefault();
                        if (aumpherObj != null)
                        {
                            aumpher = aumpherObj.AMPHUR_NAME;
                        }
                        dt.Rows[0]["F6"] = aumpher;

                        var tumbonID = string.IsNullOrEmpty(user.sStudentTumbon) ? 0 : int.Parse(user.sStudentTumbon);
                        var tumbon = "";
                        var tumbonObj = dbMaster.districts.Where(w => w.AMPHUR_ID == aumpherID && w.DISTRICT_ID == tumbonID).FirstOrDefault();
                        if (tumbonObj != null)
                        {
                            tumbon = tumbonObj.DISTRICT_NAME;
                        }
                        dt.Rows[0]["F7"] = tumbon;

                        dt.Rows[0]["F8"] = user.sStudentPost;
                        dt.Rows[0]["F9"] = user.sStudentHousePhone;

                        var title = "";
                        var titleObj = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == p.stayWithTitle).FirstOrDefault();
                        if (titleObj != null)
                        {
                            title = titleObj.titleDescription;
                        }
                        dt.Rows[0]["F10"] = title;

                        dt.Rows[0]["F11"] = p.stayWithName;
                        dt.Rows[0]["F12"] = p.stayWithLast;
                        dt.Rows[0]["F13"] = p.stayWithEmergencyCall;
                        dt.Rows[0]["F14"] = p.stayWithEmail;
                        dt.Rows[0]["F15"] = p.friendName;
                        dt.Rows[0]["F16"] = p.friendLastName;
                        dt.Rows[0]["F17"] = p.friendPhone;

                        string homeType = "";
                        switch (p.HomeType)
                        {
                            case 0: homeType = ""; break;
                            case 1: homeType = "บ้านของตัวเอง"; break;
                            case 2: homeType = "บ้านญาติ"; break;
                            case 3: homeType = "บ้านเช่า"; break;
                            case 4: homeType = "บ้านพักราชการ"; break;
                            case 5: homeType = "หอพักโรงเรียน"; break;
                            default: homeType = ""; break;
                        }
                        dt.Rows[0]["F18"] = homeType;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                return infor;
            }
        }

        #endregion
    }
}
