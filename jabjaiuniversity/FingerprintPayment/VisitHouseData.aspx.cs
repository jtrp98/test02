using FingerprintPayment.Class;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public class VisitHouseGateway : System.Web.UI.Page
    {
        private JWTToken.userData userData;
        protected JWTToken.userData UserData { get { return userData; } }

        protected override void OnLoad(EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        public static JWTToken.userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }

            return userData;
        }
    }

    public partial class VisitHouseData : VisitHouseGateway
    {
        private static string SessionPrimaryKeyYear = "VISITHOUSE_YEAR";
        private static string SessionPrimaryKeyID = "VISITHOUSE_ID";
        private static string SessionPrimaryKeySID = "VISITHOUSE_SID";
        private static string SessionPrimaryKeyTERM = "VISITHOUSE_TERM";

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
                    MvContent.ActiveViewIndex = 0;

                    int schoolID = UserData.CompanyID;
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        // Get current year
                        StudentLogic studentLogic = new StudentLogic(en);
                        string currentTerm = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });
                        int yearID = 0;
                        var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
                        if (term != null)
                        {
                            yearID = term.nYear.Value;
                        }

                        var listYear = en.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false).OrderByDescending(x => x.numberYear).ToList();
                        foreach (var l in listYear)
                        {
                            if (l.nYear == yearID)
                            {
                                this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                            }
                            else
                            {
                                this.ltrYear.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                            }

                            if (yearID == 0) yearID = l.nYear;
                        }

                        if (yearID != 0)
                        {
                            var listTerm = en.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yearID && w.cDel == null).OrderByDescending(o => o.nTerm).ToList();
                            foreach (var l in listTerm)
                            {
                                if (currentTerm.Trim() == l.nTerm)
                                {
                                    this.ltrTerm.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                                }
                                else
                                {
                                    this.ltrTerm.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nTerm, l.sTerm);
                                }
                            }
                        }

                        var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                        foreach (var l in listLevel)
                        {
                            this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                        }
                    }

                    break;
                case "form":
                    MvContent.ActiveViewIndex = 1; break;
                case "form-schedule":
                    MvContent.ActiveViewIndex = 3; break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        [WebMethod]
        public static string SaveItem(List<string> data)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    int Year = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKeyYear]);
                    int ID = 0;
                    int SID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKeySID]);
                    string Term = Convert.ToString(HttpContext.Current.Session[SessionPrimaryKeyTERM]);

                    short? TimeTogether = string.IsNullOrEmpty(data[1]) ? (short?)null : short.Parse(data[1]);
                    short? FatherRelationsLevel = string.IsNullOrEmpty(data[2]) ? (short?)null : short.Parse(data[2]);
                    short? MotherRelationsLevel = string.IsNullOrEmpty(data[3]) ? (short?)null : short.Parse(data[3]);
                    short? BrotherRelationsLevel = string.IsNullOrEmpty(data[4]) ? (short?)null : short.Parse(data[4]);
                    short? SistersRelationsLevel = string.IsNullOrEmpty(data[5]) ? (short?)null : short.Parse(data[5]);
                    short? RelativeRelationsLevel = string.IsNullOrEmpty(data[6]) ? (short?)null : short.Parse(data[6]);
                    short? TakeCareChildren = string.IsNullOrEmpty(data[7]) ? (short?)null : short.Parse(data[7]);
                    string TakeCareChildrenOther = string.IsNullOrEmpty(data[8]) ? null : data[8];
                    decimal? HouseholdIncome = string.IsNullOrEmpty(data[9]) ? (decimal?)null : decimal.Parse(data[9]);
                    string ExpensesFrom = string.IsNullOrEmpty(data[10]) ? null : data[10];
                    string ExtraWork = string.IsNullOrEmpty(data[11]) ? null : data[11];
                    short? ExtraWorkIncome = string.IsNullOrEmpty(data[12]) ? (short?)null : short.Parse(data[12]);
                    short? CarryMoneySchool = string.IsNullOrEmpty(data[13]) ? (short?)null : short.Parse(data[13]);
                    short? HelpFromSchool = string.IsNullOrEmpty(data[14]) ? (short?)null : short.Parse(data[14]);
                    string HelpFromSchoolOther = string.IsNullOrEmpty(data[15]) ? null : data[15];
                    short? HelpFamilyReceived = string.IsNullOrEmpty(data[16]) ? (short?)null : short.Parse(data[16]);
                    string HelpFamilyReceivedOther = string.IsNullOrEmpty(data[17]) ? null : data[17];
                    string ParentsConcerns = string.IsNullOrEmpty(data[18]) ? null : data[18];

                    short? Health = string.IsNullOrEmpty(data[19]) ? (short?)null : short.Parse(data[19]);
                    short? Welfare = string.IsNullOrEmpty(data[20]) ? (short?)null : short.Parse(data[20]);
                    decimal? DistanceSchool = string.IsNullOrEmpty(data[21]) ? (decimal?)null : decimal.Parse(data[21]);
                    short? TimeSchoolHour = string.IsNullOrEmpty(data[22]) ? (short?)null : short.Parse(data[22]);
                    short? TimeSchoolMinute = string.IsNullOrEmpty(data[23]) ? (short?)null : short.Parse(data[23]);
                    short? TravelMethod = string.IsNullOrEmpty(data[24]) ? (short?)null : short.Parse(data[24]);
                    string TravelMethodOther = string.IsNullOrEmpty(data[25]) ? null : data[25];
                    short? LivingConditions = string.IsNullOrEmpty(data[26]) ? (short?)null : short.Parse(data[26]);
                    short? StudentWorkFamily = string.IsNullOrEmpty(data[27]) ? (short?)null : short.Parse(data[27]);
                    string StudentWorkFamilyOther = string.IsNullOrEmpty(data[28]) ? null : data[28];
                    short? Hobby = string.IsNullOrEmpty(data[29]) ? (short?)null : short.Parse(data[29]);
                    string HobbyOther = string.IsNullOrEmpty(data[30]) ? null : data[30];
                    short? SubstanceAbuseBehavior = string.IsNullOrEmpty(data[31]) ? (short?)null : short.Parse(data[31]);
                    short? ViolenceBehavior = string.IsNullOrEmpty(data[32]) ? (short?)null : short.Parse(data[32]);
                    short? SexualBehavior = string.IsNullOrEmpty(data[33]) ? (short?)null : short.Parse(data[33]);
                    short? GameAddiction = string.IsNullOrEmpty(data[34]) ? (short?)null : short.Parse(data[34]);
                    string GameAddictionOther = string.IsNullOrEmpty(data[35]) ? null : data[35];
                    short? InternetAccess = string.IsNullOrEmpty(data[36]) ? (short?)null : short.Parse(data[36]);
                    short? UsingElectronicTools = string.IsNullOrEmpty(data[37]) ? (short?)null : short.Parse(data[37]);
                    short? Informant = string.IsNullOrEmpty(data[38]) ? (short?)null : short.Parse(data[38]);

                    string PhotosOutsideHome = string.IsNullOrEmpty(data[39]) ? null : data[39];
                    string PhotosInsideHome = string.IsNullOrEmpty(data[40]) ? null : data[40];
                    string Latitude = string.IsNullOrEmpty(data[41]) ? null : data[41];
                    string Longitude = string.IsNullOrEmpty(data[42]) ? null : data[42];
                    //DateTime? StampDate = string.IsNullOrEmpty(data[43]) ? (DateTime?)null : DateTime.Parse(data[43], new CultureInfo("th-TH"));
                    //string StampTime = string.IsNullOrEmpty(data[44]) ? null : data[44];

                    if (HttpContext.Current.Session[SessionPrimaryKeyID] == null)
                    {
                        // Insert Section
                        //ID = (int)(en.TVisitHouses.Where(w => w.Year == Year).Count() == 0 ? 1 : en.TVisitHouses.Where(w => w.Year == Year).Max(m => m.ID) + 1);

                        // Get Item
                        TVisitHouse p = new TVisitHouse
                        {
                            Year = Year,
                            //ID = ID,
                            sID = SID,
                            Term = Term,
                            TimeTogether = TimeTogether,
                            FatherRelationsLevel = FatherRelationsLevel,
                            MotherRelationsLevel = MotherRelationsLevel,
                            BrotherRelationsLevel = BrotherRelationsLevel,
                            SistersRelationsLevel = SistersRelationsLevel,
                            RelativeRelationsLevel = RelativeRelationsLevel,
                            TakeCareChildren = TakeCareChildren,
                            TakeCareChildrenOther = TakeCareChildrenOther,
                            HouseholdIncome = HouseholdIncome,
                            ExpensesFrom = ExpensesFrom,
                            ExtraWork = ExtraWork,
                            ExtraWorkIncome = ExtraWorkIncome,
                            CarryMoneySchool = CarryMoneySchool,
                            HelpFromSchool = HelpFromSchool,
                            HelpFromSchoolOther = HelpFromSchoolOther,
                            HelpFamilyReceived = HelpFamilyReceived,
                            HelpFamilyReceivedOther = HelpFamilyReceivedOther,
                            ParentsConcerns = ParentsConcerns,

                            Health = Health,
                            Welfare = Welfare,
                            DistanceSchool = DistanceSchool,
                            TimeSchoolHour = TimeSchoolHour,
                            TimeSchoolMinute = TimeSchoolMinute,
                            TravelMethod = TravelMethod,
                            TravelMethodOther = TravelMethodOther,
                            LivingConditions = LivingConditions,
                            StudentWorkFamily = StudentWorkFamily,
                            StudentWorkFamilyOther = StudentWorkFamilyOther,
                            Hobby = Hobby,
                            HobbyOther = HobbyOther,
                            SubstanceAbuseBehavior = SubstanceAbuseBehavior,
                            ViolenceBehavior = ViolenceBehavior,
                            SexualBehavior = SexualBehavior,
                            GameAddiction = GameAddiction,
                            GameAddictionOther = GameAddictionOther,
                            InternetAccess = InternetAccess,
                            UsingElectronicTools = UsingElectronicTools,
                            Informant = Informant,

                            PhotosOutsideHome = PhotosOutsideHome,
                            PhotosInsideHome = PhotosInsideHome,
                            Latitude = Latitude,
                            Longitude = Longitude,
                            //StampDate = StampDate,
                            //StampTime = StampTime,

                            UpdateDate = DateTime.Now,
                            SchoolID = schoolID
                        };

                        en.TVisitHouses.Add(p);

                        en.SaveChanges();

                        ID = p.VisitHouseID;

                        database.InsertLog(HttpContext.Current.Session["sEmpID"].ToString(), "เพิ่มข้อมูลเยี่ยมบ้านนักเรียน ปี:" + Year + ", รหัส:" + ID + ", รหัสนักเรียน:" + SID, HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 155, 2, 0);
                    }
                    else
                    {
                        // Modify Section
                        ID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKeyID]);

                        // Get Item
                        TVisitHouse pi = en.TVisitHouses.First(f => f.SchoolID == schoolID && f.Year == Year && f.VisitHouseID == ID);

                        pi.TimeTogether = TimeTogether;
                        pi.FatherRelationsLevel = FatherRelationsLevel;
                        pi.MotherRelationsLevel = MotherRelationsLevel;
                        pi.BrotherRelationsLevel = BrotherRelationsLevel;
                        pi.SistersRelationsLevel = SistersRelationsLevel;
                        pi.RelativeRelationsLevel = RelativeRelationsLevel;
                        pi.TakeCareChildren = TakeCareChildren;
                        pi.TakeCareChildrenOther = TakeCareChildrenOther;
                        pi.HouseholdIncome = HouseholdIncome;
                        pi.ExpensesFrom = ExpensesFrom;
                        pi.ExtraWork = ExtraWork;
                        pi.ExtraWorkIncome = ExtraWorkIncome;
                        pi.CarryMoneySchool = CarryMoneySchool;
                        pi.HelpFromSchool = HelpFromSchool;
                        pi.HelpFromSchoolOther = HelpFromSchoolOther;
                        pi.HelpFamilyReceived = HelpFamilyReceived;
                        pi.HelpFamilyReceivedOther = HelpFamilyReceivedOther;
                        pi.ParentsConcerns = ParentsConcerns;

                        pi.Health = Health;
                        pi.Welfare = Welfare;
                        pi.DistanceSchool = DistanceSchool;
                        pi.TimeSchoolHour = TimeSchoolHour;
                        pi.TimeSchoolMinute = TimeSchoolMinute;
                        pi.TravelMethod = TravelMethod;
                        pi.TravelMethodOther = TravelMethodOther;
                        pi.LivingConditions = LivingConditions;
                        pi.StudentWorkFamily = StudentWorkFamily;
                        pi.StudentWorkFamilyOther = StudentWorkFamilyOther;
                        pi.Hobby = Hobby;
                        pi.HobbyOther = HobbyOther;
                        pi.SubstanceAbuseBehavior = SubstanceAbuseBehavior;
                        pi.ViolenceBehavior = ViolenceBehavior;
                        pi.SexualBehavior = SexualBehavior;
                        pi.GameAddiction = GameAddiction;
                        pi.GameAddictionOther = GameAddictionOther;
                        pi.InternetAccess = InternetAccess;
                        pi.UsingElectronicTools = UsingElectronicTools;
                        pi.Informant = Informant;

                        pi.PhotosOutsideHome = PhotosOutsideHome;
                        pi.PhotosInsideHome = PhotosInsideHome;
                        pi.Latitude = Latitude;
                        pi.Longitude = Longitude;
                        //pi.StampDate = StampDate;
                        //pi.StampTime = StampTime;

                        pi.UpdateDate = DateTime.Now;

                        en.SaveChanges();

                        database.InsertLog(HttpContext.Current.Session["sEmpID"].ToString(), "อัปเดตข้อมูลเยี่ยมบ้านนักเรียน ปี:" + Year + ", รหัส:" + ID + ", รหัสนักเรียน:" + SID, HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 155, 3, 0);
                    }

                    // Save image : PhotosOutsideHome
                    if (!string.IsNullOrEmpty(PhotosOutsideHome))
                    {
                        // Check image in temp
                        string fullTempPathFile = HttpContext.Current.Server.MapPath(string.Format(GlobalVariable.VISITHOUSE_TMEP_OUT_FILE, HttpContext.Current.Session["sEmpID"], PhotosOutsideHome));
                        if (File.Exists(fullTempPathFile))
                        {
                            // Check Directory
                            string fullPathFolder = HttpContext.Current.Server.MapPath(string.Format(GlobalVariable.VISITHOUSE_FOLDER, Year, ID));
                            if (!Directory.Exists(fullPathFolder))
                            {
                                Directory.CreateDirectory(fullPathFolder);
                            }

                            // Remove old image
                            string fullPathFile = HttpContext.Current.Server.MapPath(string.Format(GlobalVariable.VISITHOUSE_OUT_FILE, Year, ID, PhotosOutsideHome));
                            if (File.Exists(fullPathFile))
                            {
                                File.Delete(fullPathFile);
                            }

                            // Move image
                            File.Move(fullTempPathFile, fullPathFile);
                        }
                    }

                    // Save image : PhotosInsideHome
                    if (!string.IsNullOrEmpty(PhotosInsideHome))
                    {
                        // Check image in temp
                        string fullTempPathFile = HttpContext.Current.Server.MapPath(string.Format(GlobalVariable.VISITHOUSE_TMEP_IN_FILE, HttpContext.Current.Session["sEmpID"], PhotosInsideHome));
                        if (File.Exists(fullTempPathFile))
                        {
                            // Check Directory
                            string fullPathFolder = HttpContext.Current.Server.MapPath(string.Format(GlobalVariable.VISITHOUSE_FOLDER, Year, ID));
                            if (!Directory.Exists(fullPathFolder))
                            {
                                Directory.CreateDirectory(fullPathFolder);
                            }

                            // Remove old image
                            string fullPathFile = HttpContext.Current.Server.MapPath(string.Format(GlobalVariable.VISITHOUSE_IN_FILE, Year, ID, PhotosInsideHome));
                            if (File.Exists(fullPathFile))
                            {
                                File.Delete(fullPathFile);
                            }

                            // Move image
                            File.Move(fullTempPathFile, fullPathFile);
                        }
                    }

                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod]
        public static string GetItem(string year, string vid, string sid, string term)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string infor = "new";

                try
                {
                    int iYear = 0;
                    if (!int.TryParse(year, out iYear)) { iYear = 0; }
                    int iID = 0;
                    if (!int.TryParse(vid, out iID)) { iID = 0; }
                    int iSID = 0;
                    if (!int.TryParse(sid, out iSID)) { iSID = 0; }

                    HttpContext.Current.Session[SessionPrimaryKeyYear] = iYear;
                    HttpContext.Current.Session[SessionPrimaryKeySID] = iSID;
                    HttpContext.Current.Session[SessionPrimaryKeyTERM] = term;

                    TVisitHouse p = en.TVisitHouses.Where(w => w.SchoolID == schoolID && w.Year == iYear && w.VisitHouseID == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 44; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        HttpContext.Current.Session[SessionPrimaryKeyID] = p.VisitHouseID;

                        dt.Rows[0]["F1"] = (p.TimeTogether == null ? null : p.TimeTogether.Value.ToString());
                        dt.Rows[0]["F2"] = (p.FatherRelationsLevel == null ? null : p.FatherRelationsLevel.Value.ToString());
                        dt.Rows[0]["F3"] = (p.MotherRelationsLevel == null ? null : p.MotherRelationsLevel.Value.ToString());
                        dt.Rows[0]["F4"] = (p.BrotherRelationsLevel == null ? null : p.BrotherRelationsLevel.Value.ToString());
                        dt.Rows[0]["F5"] = (p.SistersRelationsLevel == null ? null : p.SistersRelationsLevel.Value.ToString());
                        dt.Rows[0]["F6"] = (p.RelativeRelationsLevel == null ? null : p.RelativeRelationsLevel.Value.ToString());
                        dt.Rows[0]["F7"] = (p.TakeCareChildren == null ? null : p.TakeCareChildren.Value.ToString());
                        dt.Rows[0]["F8"] = p.TakeCareChildrenOther;
                        dt.Rows[0]["F9"] = (p.HouseholdIncome == null ? null : p.HouseholdIncome.Value.ToString());
                        dt.Rows[0]["F10"] = p.ExpensesFrom;
                        dt.Rows[0]["F11"] = p.ExtraWork;
                        dt.Rows[0]["F12"] = (p.ExtraWorkIncome == null ? null : p.ExtraWorkIncome.Value.ToString());
                        dt.Rows[0]["F13"] = (p.CarryMoneySchool == null ? null : p.CarryMoneySchool.Value.ToString());
                        dt.Rows[0]["F14"] = (p.HelpFromSchool == null ? null : p.HelpFromSchool.Value.ToString());
                        dt.Rows[0]["F15"] = p.HelpFromSchoolOther;
                        dt.Rows[0]["F16"] = (p.HelpFamilyReceived == null ? null : p.HelpFamilyReceived.Value.ToString());
                        dt.Rows[0]["F17"] = p.HelpFamilyReceivedOther;
                        dt.Rows[0]["F18"] = p.ParentsConcerns;

                        dt.Rows[0]["F19"] = (p.Health == null ? null : p.Health.Value.ToString());
                        dt.Rows[0]["F20"] = (p.Welfare == null ? null : p.Welfare.Value.ToString());
                        dt.Rows[0]["F21"] = (p.DistanceSchool == null ? null : p.DistanceSchool.Value.ToString());
                        dt.Rows[0]["F22"] = (p.TimeSchoolHour == null ? null : p.TimeSchoolHour.Value.ToString());
                        dt.Rows[0]["F23"] = (p.TimeSchoolMinute == null ? null : p.TimeSchoolMinute.Value.ToString());
                        dt.Rows[0]["F24"] = (p.TravelMethod == null ? null : p.TravelMethod.Value.ToString());
                        dt.Rows[0]["F25"] = p.TravelMethodOther;
                        dt.Rows[0]["F26"] = (p.LivingConditions == null ? null : p.LivingConditions.Value.ToString());
                        dt.Rows[0]["F27"] = (p.StudentWorkFamily == null ? null : p.StudentWorkFamily.Value.ToString());
                        dt.Rows[0]["F28"] = p.StudentWorkFamilyOther;
                        dt.Rows[0]["F29"] = (p.Hobby == null ? null : p.Hobby.Value.ToString());
                        dt.Rows[0]["F30"] = p.HobbyOther;
                        dt.Rows[0]["F31"] = (p.SubstanceAbuseBehavior == null ? null : p.SubstanceAbuseBehavior.Value.ToString());
                        dt.Rows[0]["F32"] = (p.ViolenceBehavior == null ? null : p.ViolenceBehavior.Value.ToString());
                        dt.Rows[0]["F33"] = (p.SexualBehavior == null ? null : p.SexualBehavior.Value.ToString());
                        dt.Rows[0]["F34"] = (p.GameAddiction == null ? null : p.GameAddiction.Value.ToString());
                        dt.Rows[0]["F35"] = p.GameAddictionOther;
                        dt.Rows[0]["F36"] = (p.InternetAccess == null ? null : p.InternetAccess.Value.ToString());
                        dt.Rows[0]["F37"] = (p.UsingElectronicTools == null ? null : p.UsingElectronicTools.Value.ToString());
                        dt.Rows[0]["F38"] = (p.Informant == null ? null : p.Informant.Value.ToString());

                        dt.Rows[0]["F39"] = p.PhotosOutsideHome;
                        dt.Rows[0]["F40"] = p.PhotosInsideHome;
                        dt.Rows[0]["F41"] = p.Latitude;
                        dt.Rows[0]["F42"] = p.Longitude;
                        dt.Rows[0]["F43"] = (p.StampDate == null ? null : p.StampDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                        dt.Rows[0]["F44"] = p.StampTime;

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

                if (infor == "new" || infor == "error") HttpContext.Current.Session[SessionPrimaryKeyID] = null;

                return infor;
            }
        }

        [WebMethod]
        public static string SaveItemSchedule(List<string> data)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string isComplete = "complete";

                try
                {
                    int Year = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKeyYear]);
                    int ID = 0;

                    DateTime? StampDate = string.IsNullOrEmpty(data[1]) ? (DateTime?)null : DateTime.Parse(data[1], new CultureInfo("th-TH"));
                    string StampTime = string.IsNullOrEmpty(data[2]) ? null : data[2];

                    if (HttpContext.Current.Session[SessionPrimaryKeyID] == null)
                    {
                        // Insert Section
                        //ID = (int)(en.TVisitHouses.Where(w => w.Year == Year).Count() == 0 ? 1 : en.TVisitHouses.Where(w => w.Year == Year).Max(m => m.ID) + 1);

                        // Get Item
                        TVisitHouse p = new TVisitHouse
                        {
                            Year = Year,
                            //ID = ID,
                            StampDate = StampDate,
                            StampTime = StampTime,

                            UpdateDate = DateTime.Now,
                            SchoolID = schoolID
                        };

                        en.TVisitHouses.Add(p);

                        en.SaveChanges();

                        ID = p.VisitHouseID;

                        database.InsertLog(HttpContext.Current.Session["sEmpID"].ToString(), "เพิ่มข้อมูลเยี่ยมบ้านนักเรียน(สร้างตารางเวลานัดหมาย) ปี:" + Year + ", รหัส:" + ID, HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 155, 2, 0);
                    }
                    else
                    {
                        // Modify Section
                        ID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKeyID]);

                        // Get Item
                        TVisitHouse pi = en.TVisitHouses.First(f => f.SchoolID == schoolID && f.Year == Year && f.VisitHouseID == ID);

                        pi.StampDate = StampDate;
                        pi.StampTime = StampTime;

                        pi.UpdateDate = DateTime.Now;

                        en.SaveChanges();

                        database.InsertLog(HttpContext.Current.Session["sEmpID"].ToString(), "อัปเดตข้อมูลเยี่ยมบ้านนักเรียน(สร้างตารางเวลานัดหมาย) ปี:" + Year + ", รหัส:" + ID, HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 155, 3, 0);
                    }

                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod]
        public static string GetItemSchedule(string year, string vid)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iYear = 0;
                    if (!int.TryParse(year, out iYear)) { iYear = 0; }
                    int iID = 0;
                    if (!int.TryParse(vid, out iID)) { iID = 0; }

                    HttpContext.Current.Session[SessionPrimaryKeyYear] = iYear;

                    TVisitHouse p = en.TVisitHouses.Where(w => w.SchoolID == schoolID && w.Year == iYear && w.VisitHouseID == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 2; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        HttpContext.Current.Session[SessionPrimaryKeyID] = p.VisitHouseID;

                        dt.Rows[0]["F1"] = (p.StampDate == null ? null : p.StampDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                        dt.Rows[0]["F2"] = p.StampTime;

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

                if (infor == "new" || infor == "error") HttpContext.Current.Session[SessionPrimaryKeyID] = null;

                return infor;
            }
        }

        [WebMethod]
        public static string ClearSessionID()
        {
            HttpContext.Current.Session[SessionPrimaryKeyYear] = null;
            HttpContext.Current.Session[SessionPrimaryKeyID] = null;
            HttpContext.Current.Session[SessionPrimaryKeySID] = null;
            HttpContext.Current.Session[SessionPrimaryKeyTERM] = null;

            return "";
        }

        [WebMethod]
        public static string[] GetStudentName(string keyword)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname
FROM TUser
WHERE cDel IS NULL AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%')
GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, keyword);
                List<string> result = dbschool.Database.SqlQuery<string>(sqlQuery).ToList();

                return result.ToArray();
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadTerm(string yearID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(yearID))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    int yID = Convert.ToInt32(yearID);

                    result = dbschool.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == yID && w.cDel == null).OrderByDescending(o => o.nTerm).Select(s => new EntityDropdown { id = s.nTerm, name = s.sTerm }).ToList();
                }
            }

            return result;
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadTermSubLevel2(string subLevelID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(subLevelID))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {

                    int slID = Convert.ToInt32(subLevelID);

                    string query = string.Format(@"
SELECT r.id, r.name
FROM
(
	SELECT CAST(t.nTermSubLevel2 AS VARCHAR(10)) 'id', s.SubLevel + ' / ' + t.nTSubLevel2 'name', s.SubLevel 'sort1', (CASE WHEN ISNUMERIC(t.nTSubLevel2) = 1 THEN RIGHT('0000' + t.nTSubLevel2, 5) ELSE t.nTSubLevel2 END) 'sort2'
	FROM TTermSubLevel2 t 
	LEFT JOIN TSubLevel s ON t.nTSubLevel = s.nTSubLevel 
	WHERE t.nTSubLevel = {0} AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1 AND t.SchoolID = {1} AND s.SchoolID = {1}
) r
ORDER BY r.sort1, r.sort2", slID, schoolID);
                    result = dbschool.Database.SqlQuery<EntityDropdown>(query).ToList();
                }
            }

            return result;
        }

        #endregion
    }
}