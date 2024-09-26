using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using FingerprintPayment.Helper;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    public partial class RPstudent_Growth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    DataTable dtYear = fcommon.LinqToDataTable(_db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.numberYear).ToList());

                    fcommon.ListDataTableToDropDownList(dtYear, ddlyear, "เลือกปีการศึกษา", "nYear", "numberYear");
                    ddlyear.SelectedValue = DateTime.Now.Year.ToString();

                    using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        string sEntities = Session["sEntities"].ToString();
                        var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                        hdfschoolname.Value = tCompany.sCompany;
                        var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)), userData);

                        fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                    }

                    var qq = (from a in _db.TLevels.Where(w => w.SchoolID == userData.CompanyID)
                              join b in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on a.LevelID equals b.nTLevel
                              group a by new { a.LevelID, a.LevelName, a.sortValue, b.nWorkingStatus } into gb1
                              where gb1.Key.nWorkingStatus == 1
                              orderby new { gb1.Key.sortValue }
                              select new
                              {
                                  sMainClass = gb1.Key.LevelID,
                                  sMainClassName = gb1.Key.LevelName
                              }).ToList();

                    foreach (var DataMainClass in qq)
                    {
                        tlevel.Items.Add(new ListItem(DataMainClass.sMainClassName.ToString(), DataMainClass.sMainClass.ToString()));
                    }

                }

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object reports_Growth(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));

            List<studentlist> studentlists = new List<studentlist>();

            //search.tErm_Id = string.Format("TS{0:0000000}", int.Parse(search.tErm_Id));

            var tUser = db.Database.SqlQuery<JabjaiEntity.DB.TUser>($"SELECT * FROM TUser WHERE SchoolID = {userData.CompanyID}  AND ISNULL(cDel,'0') = '0' AND ISNULL(nStudentStatus, 0) = 0 ").ToList();

            var studentModel = (from a in tUser
                                join b in db.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                                join c in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on b.nTermSubLevel2 equals c.nTermSubLevel2
                                join d in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on c.nTSubLevel equals d.nTSubLevel
                                join e in db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on b.nTerm equals e.nTerm
                                join f in db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on e.nYear equals f.nYear
                                join h in db.TLevels.Where(w => w.SchoolID == userData.CompanyID) on d.nTLevel equals h.LevelID

                                join j in db.THealtProfiles on a.sID equals j.sID into jaj
                                from jj in jaj.DefaultIfEmpty()

                                where (!search.tLeveL_Id.HasValue || search.tLeveL_Id == h.LevelID)
                                && b.nTerm.Trim() == search.tErm_Id
                                //&& (!search.sUbLV_Id.HasValue || search.sUbLV_Id == d.nTSubLevel)
                                //&& (!search.sUbLV2_Id.HasValue || search.sUbLV2_Id == c.nTermSubLevel2)
                                //&& c.nWorkingStatus == 1 && d.nWorkingStatus == 1
                                orderby a.sStudentID

                                select new
                                {
                                    studentID = a.sStudentID,
                                    studentSID = a.sID,
                                    studentName = a.sName,
                                    studentlastName = a.sLastname,
                                    studentSex = a.cSex,
                                    studentBirthDay = a.dBirth,

                                    studentnHistoryId = b.nHistoryId,
                                    studentTterm = b.nTerm,

                                    facultyID = h.LevelID,
                                    facultyName = h.LevelName,
                                    facultySortValue = h.sortValue,
                                    classID = d.nTSubLevel,
                                    className = d.SubLevel,
                                    roomID = c.nTermSubLevel2,
                                    roomName = c.nTSubLevel2,

                                    stHeight = jj == null ? 0 : jj.nHeight,
                                    stWeight = jj == null ? 0 : jj.nWeight
                                }).ToList();

            foreach (var data2 in studentModel)
            {
                var now = DateTime.Today;
                var BirthDay = data2.studentBirthDay;
                var sex = data2.studentSex;
                int Age = now.Year - data2.studentBirthDay.Value.Year;

                int MonthOfBD = DateTime.Today.Month - data2.studentBirthDay.Value.Month;

                var height = data2.stHeight;
                var weight = data2.stWeight;
                var ResultAccepted = 0;

                if (height == 0 && weight == 0)
                    ResultAccepted = 0;
                else
                    ResultAccepted = 1;


                //CalHeight
                var maleGrowthReferenceWidth = GrowthReference.GetGrowthByWidths().Where(w => w.Height == height && w.LookUp.Sex == "0").Select(s => new
                {
                    CountThin = (s.LookUp.Thin.Min <= weight && s.LookUp.Thin.Max >= weight) ? 1 : 0,
                    CountSkinny = (s.LookUp.Skinny.Min <= weight && s.LookUp.Skinny.Max >= weight) ? 1 : 0,
                    CountShapely = (s.LookUp.Shapely.Min <= weight && s.LookUp.Shapely.Max >= weight) ? 1 : 0,
                    CountPlump = (s.LookUp.Plump.Min <= weight && s.LookUp.Plump.Max >= weight) ? 1 : 0,
                    CountChubby = (s.LookUp.Chubby.Min <= weight && s.LookUp.Chubby.Max >= weight) ? 1 : 0,
                    CountFat = (s.LookUp.Fat.Min <= weight && s.LookUp.Fat.Max >= weight) ? 1 : 0
                });
                var femaleGrowthReferenceWidth = GrowthReference.GetGrowthByWidths().Where(w => w.Height == height && w.LookUp.Sex == "1").Select(s => new
                {
                    CountThin = (s.LookUp.Thin.Min <= weight && s.LookUp.Thin.Max >= weight) ? 1 : 0,
                    CountSkinny = (s.LookUp.Skinny.Min <= weight && s.LookUp.Skinny.Max >= weight) ? 1 : 0,
                    CountShapely = (s.LookUp.Shapely.Min <= weight && s.LookUp.Shapely.Max >= weight) ? 1 : 0,
                    CountPlump = (s.LookUp.Plump.Min <= weight && s.LookUp.Plump.Max >= weight) ? 1 : 0,
                    CountChubby = (s.LookUp.Chubby.Min <= weight && s.LookUp.Chubby.Max >= weight) ? 1 : 0,
                    CountFat = (s.LookUp.Fat.Min <= weight && s.LookUp.Fat.Max >= weight) ? 1 : 0
                });

                var maleGrowthReferenceHeight = GrowthReference.GetgrowthByHeight().Where(w => w.Age == Age && w.Month == MonthOfBD && w.lookUpHeight.Sex == "0").Select(s => new
                {
                    CountShort = (s.lookUpHeight.Short.Min <= height && s.lookUpHeight.Short.Max >= height) ? 1 : 0,
                    CountMediumShort = (s.lookUpHeight.MediumShort.Min <= height && s.lookUpHeight.MediumShort.Max >= height) ? 1 : 0,
                    CountNormal = (s.lookUpHeight.Normal.Min <= height && s.lookUpHeight.Normal.Max >= height) ? 1 : 0,
                    CountMediumTall = (s.lookUpHeight.MediumTall.Min <= height && s.lookUpHeight.MediumTall.Max >= height) ? 1 : 0,
                    CountTall = (s.lookUpHeight.Tall.Min <= height && s.lookUpHeight.Tall.Max >= height) ? 1 : 0
                });
                var femaleGrowthReferenceHeight = GrowthReference.GetgrowthByHeight().Where(w => w.Age == Age && w.Month == MonthOfBD && w.lookUpHeight.Sex == "1").Select(s => new
                {
                    CountShort = (s.lookUpHeight.Short.Min <= height && s.lookUpHeight.Short.Max >= height) ? 1 : 0,
                    CountMediumShort = (s.lookUpHeight.MediumShort.Min <= height && s.lookUpHeight.MediumShort.Max >= height) ? 1 : 0,
                    CountNormal = (s.lookUpHeight.Normal.Min <= height && s.lookUpHeight.Normal.Max >= height) ? 1 : 0,
                    CountMediumTall = (s.lookUpHeight.MediumTall.Min <= height && s.lookUpHeight.MediumTall.Max >= height) ? 1 : 0,
                    CountTall = (s.lookUpHeight.Tall.Min <= height && s.lookUpHeight.Tall.Max >= height) ? 1 : 0
                });

                studentlists.Add(new studentlist
                {
                    //width
                    MaleThin = maleGrowthReferenceWidth.Where(w => w.CountThin == 1).Count(c => c.CountThin == 1),
                    MaleSkinny = maleGrowthReferenceWidth.Where(w => w.CountSkinny == 1).Count(c => c.CountSkinny == 1),
                    MaleShapely = maleGrowthReferenceWidth.Where(w => w.CountShapely == 1).Count(c => c.CountShapely == 1),
                    MalePlump = maleGrowthReferenceWidth.Where(w => w.CountPlump == 1).Count(c => c.CountPlump == 1),
                    MaleChubby = maleGrowthReferenceWidth.Where(w => w.CountChubby == 1).Count(c => c.CountChubby == 1),
                    MaleFat = maleGrowthReferenceWidth.Where(w => w.CountFat == 1).Count(c => c.CountFat == 1),

                    FemaleThin = femaleGrowthReferenceWidth.Where(w => w.CountThin == 1).Count(c => c.CountThin == 1),
                    FemaleSkinny = femaleGrowthReferenceWidth.Where(w => w.CountSkinny == 1).Count(c => c.CountSkinny == 1),
                    FemaleShapely = femaleGrowthReferenceWidth.Where(w => w.CountShapely == 1).Count(c => c.CountShapely == 1),
                    FemalePlump = femaleGrowthReferenceWidth.Where(w => w.CountPlump == 1).Count(c => c.CountPlump == 1),
                    FemaleChubby = femaleGrowthReferenceWidth.Where(w => w.CountChubby == 1).Count(c => c.CountChubby == 1),
                    FemaleFat = femaleGrowthReferenceWidth.Where(w => w.CountFat == 1).Count(c => c.CountFat == 1),

                    //height
                    MaleShort = maleGrowthReferenceHeight.Where(w => w.CountShort == 1).Count(c => c.CountShort == 1),
                    MaleMediumShort = maleGrowthReferenceHeight.Where(w => w.CountMediumShort == 1).Count(c => c.CountMediumShort == 1),
                    MaleNormal = maleGrowthReferenceHeight.Where(w => w.CountNormal == 1).Count(c => c.CountNormal == 1),
                    MaleMediumTall = maleGrowthReferenceHeight.Where(w => w.CountMediumTall == 1).Count(c => c.CountMediumTall == 1),
                    MaleTall = maleGrowthReferenceHeight.Where(w => w.CountTall == 1).Count(c => c.CountTall == 1),

                    FemaleShort = femaleGrowthReferenceHeight.Where(w => w.CountShort == 1).Count(c => c.CountShort == 1),
                    FemaleMediumShort = femaleGrowthReferenceHeight.Where(w => w.CountMediumShort == 1).Count(c => c.CountMediumShort == 1),
                    FemaleNormal = femaleGrowthReferenceHeight.Where(w => w.CountNormal == 1).Count(c => c.CountNormal == 1),
                    FemaleMediumTall = femaleGrowthReferenceHeight.Where(w => w.CountMediumTall == 1).Count(c => c.CountMediumTall == 1),
                    FemaleTall = femaleGrowthReferenceHeight.Where(w => w.CountTall == 1).Count(c => c.CountTall == 1),

                    ACCEPTE = ResultAccepted,
                    studentName = data2.studentName,
                    studentlastName = data2.studentlastName,
                    studentSex = data2.studentSex,

                    FacultyID = data2.facultyID,
                    FacultyName = data2.facultyName,
                    FacultySortValue = data2.facultySortValue,
                    classID = data2.classID,
                    className = data2.className,
                    roomID = data2.roomID,
                    roomName = data2.roomName
                });
            }
            var Layer0s = (from a in studentlists
                           group a by new { a.FacultyID, a.FacultyName, a.FacultySortValue } into gb0
                           orderby gb0.Key.FacultySortValue
                           select new Layer0
                           {
                               FacultyID = gb0.Key.FacultyID,
                               FacultyName = gb0.Key.FacultyName,
                               Layer1s = (from a1 in gb0
                                          where a1.FacultyID == gb0.Key.FacultyID
                                          group a1 by new { a1.classID, a1.className, a1.roomID, a1.roomName } into gb1
                                          orderby gb1.Key.className, gb1.Key.roomName.Length, gb1.Key.roomName
                                          select new Layer1
                                          {
                                              ClassFullName = gb1.Key.className + "/" + gb1.Key.roomName,
                                              Male = gb1.Where(w => w.classID == gb1.Key.classID).Count(c => c.studentSex == "0"),
                                              Female = gb1.Where(w => w.classID == gb1.Key.classID).Count(c => c.studentSex == "1"),
                                              SumStudent = gb1.Where(w => w.classID == gb1.Key.classID).Count(),

                                              Male_Acep = gb1.Where(w => w.ACCEPTE == 1).Count(c => c.studentSex == "0"),
                                              Female_Acep = gb1.Where(w => w.ACCEPTE == 1).Count(c => c.studentSex == "1"),

                                              CountMaleThin = gb1.Where(w => w.MaleThin == 1).Count(c => c.studentSex == "0"),
                                              CountFemaleThin = gb1.Where(w => w.FemaleThin == 1).Count(c => c.studentSex == "1"),
                                              CountMaleSkinny = gb1.Where(w => w.MaleSkinny == 1).Count(c => c.studentSex == "0"),
                                              CountFemaleSkinny = gb1.Where(w => w.FemaleSkinny == 1).Count(c => c.studentSex == "1"),
                                              CountMaleShapely = gb1.Where(w => w.MaleShapely == 1).Count(c => c.studentSex == "0"),
                                              CountFemaleShapely = gb1.Where(w => w.FemaleShapely == 1).Count(c => c.studentSex == "1"),
                                              CountMalePlump = gb1.Where(w => w.MalePlump == 1).Count(c => c.studentSex == "0"),
                                              CountFemalePlump = gb1.Where(w => w.FemalePlump == 1).Count(c => c.studentSex == "1"),
                                              CountMaleChubby = gb1.Where(w => w.MaleChubby == 1).Count(c => c.studentSex == "0"),
                                              CountFemaleChubby = gb1.Where(w => w.FemaleChubby == 1).Count(c => c.studentSex == "1"),
                                              CountMaleFat = gb1.Where(w => w.MaleFat == 1).Count(c => c.studentSex == "0"),
                                              CountFemaleFat = gb1.Where(w => w.FemaleFat == 1).Count(c => c.studentSex == "1"),

                                              //เกณฑ์น้ำหนักตามเกณฑ์ส่วนสูง
                                              CountMaleShort = gb1.Where(w => w.MaleShort == 1).Count(c => c.studentSex == "0"),
                                              CountFemaleShort = gb1.Where(w => w.FemaleShort == 1).Count(c => c.studentSex == "1"),
                                              CountMaleMediumShort = gb1.Where(w => w.MaleMediumShort == 1).Count(c => c.studentSex == "0"),
                                              CountFemaleMediumShort = gb1.Where(w => w.FemaleMediumShort == 1).Count(c => c.studentSex == "1"),
                                              CountMaleNormal = gb1.Where(w => w.MaleNormal == 1).Count(c => c.studentSex == "0"),
                                              CountFemaleNormal = gb1.Where(w => w.FemaleNormal == 1).Count(c => c.studentSex == "1"),
                                              CountMaleMediumTall = gb1.Where(w => w.MaleMediumTall == 1).Count(c => c.studentSex == "0"),
                                              CountFemaleMediumTall = gb1.Where(w => w.FemaleMediumTall == 1).Count(c => c.studentSex == "1"),
                                              CountMaleTall = gb1.Where(w => w.MaleTall == 1).Count(c => c.studentSex == "0"),
                                              CountFemaleTall = gb1.Where(w => w.FemaleTall == 1).Count(c => c.studentSex == "1"),

                                              //สมส่วนและสูงตามเกณฑ์
                                              MaleShapAnNormal = gb1.Where(w => w.MaleShapely == 1 && w.MaleNormal == 1).Count(c => c.studentSex == "0"),
                                              FemaleShapAnNormal = gb1.Where(w => w.FemaleShapely == 1 && w.FemaleNormal == 1).Count(c => c.studentSex == "1"),
                                              MaleShapAnMediumTall = gb1.Where(w => w.MaleShapely == 1 && w.MaleMediumTall == 1).Count(c => c.studentSex == "0"),
                                              FemaleShapAnMediumTall = gb1.Where(w => w.FemaleShapely == 1 && w.FemaleMediumTall == 1).Count(c => c.studentSex == "1"),
                                              MaleShapAnTall = gb1.Where(w => w.MaleShapely == 1 && w.MaleTall == 1).Count(c => c.studentSex == "0"),
                                              FemaleShapAnTall = gb1.Where(w => w.FemaleShapely == 1 && w.FemaleTall == 1).Count(c => c.studentSex == "1")
                                          }).ToList()
                           }).ToList();

            return new ListView
            {
                Layer0s = Layer0s
            };
        }



        public class studentlist
        {
            public int ACCEPTE { get; set; }
            public string studentName { get; set; }
            public string studentlastName { get; set; }
            public string studentSex { get; set; }

            public int FacultyID { get; set; }
            public string FacultyName { get; set; }
            public int? FacultySortValue { get; set; }
            public int classID { get; set; }
            public string className { get; set; }
            public int roomID { get; set; }
            public string roomName { get; set; }

            //width
            public int MaleThin { get; set; }
            public int MaleSkinny { get; set; }
            public int MaleShapely { get; set; }
            public int MalePlump { get; set; }
            public int MaleChubby { get; set; }
            public int MaleFat { get; set; }
            public int FemaleThin { get; set; }
            public int FemaleSkinny { get; set; }
            public int FemaleShapely { get; set; }
            public int FemalePlump { get; set; }
            public int FemaleChubby { get; set; }
            public int FemaleFat { get; set; }

            //height
            public int MaleShort { get; set; }
            public int MaleMediumShort { get; set; }
            public int MaleNormal { get; set; }
            public int MaleMediumTall { get; set; }
            public int MaleTall { get; set; }
            public int FemaleShort { get; set; }
            public int FemaleMediumShort { get; set; }
            public int FemaleNormal { get; set; }
            public int FemaleMediumTall { get; set; }
            public int FemaleTall { get; set; }
        }
        private class ListView
        {
            public List<Layer0> Layer0s { get; set; }
        }
        private class Layer0
        {
            public List<Layer1> Layer1s { get; set; }
            public int FacultyID { get; set; }
            public string FacultyName { get; set; }
            public int ClassID { get; set; }
            public string ClassName { get; set; }
        }
        private class Layer1
        {
            public string ClassFullName { get; set; }
            public int Male { get; set; }
            public int Female { get; set; }
            public int SumStudent { get; set; }

            public int Male_Acep { get; set; }
            public int Female_Acep { get; set; }

            public int CountMaleShort { get; set; }
            public int CountMaleMediumShort { get; set; }
            public int CountMaleNormal { get; set; }
            public int CountMaleMediumTall { get; set; }
            public int CountMaleTall { get; set; }

            public int CountFemaleShort { get; set; }
            public int CountFemaleMediumShort { get; set; }
            public int CountFemaleNormal { get; set; }
            public int CountFemaleMediumTall { get; set; }
            public int CountFemaleTall { get; set; }

            public int CountMaleThin { get; set; }
            public int CountMaleSkinny { get; set; }
            public int CountMaleShapely { get; set; }
            public int CountMalePlump { get; set; }
            public int CountMaleChubby { get; set; }
            public int CountMaleFat { get; set; }

            public int CountFemaleThin { get; set; }
            public int CountFemaleSkinny { get; set; }
            public int CountFemaleShapely { get; set; }
            public int CountFemalePlump { get; set; }
            public int CountFemaleChubby { get; set; }
            public int CountFemaleFat { get; set; }

            public int MaleShapAnNormal { get; set; }
            public int FemaleShapAnNormal { get; set; }
            public int MaleShapAnMediumTall { get; set; }
            public int FemaleShapAnMediumTall { get; set; }
            public int MaleShapAnTall { get; set; }
            public int FemaleShapAnTall { get; set; }

        }

        public class Search
        {
            public string yEar_Id { get; set; }
            public string tErm_Id { get; set; }
            public int? sUbLV_Id { get; set; }
            public int? sUbLV2_Id { get; set; }
            public int? tLeveL_Id { get; set; }
        }

        public class SemiNumericComparer : IComparer<string>
        {
            public int Compare(string s1, string s2)
            {
                if (IsNumeric(s1) && IsNumeric(s2))
                {
                    if (Convert.ToInt32(s1) > Convert.ToInt32(s2)) return 1;
                    if (Convert.ToInt32(s1) < Convert.ToInt32(s2)) return -1;
                    if (Convert.ToInt32(s1) == Convert.ToInt32(s2)) return 0;
                }
                if (IsNumeric(s1) && !IsNumeric(s2))
                    return -1;
                if (!IsNumeric(s1) && IsNumeric(s2))
                    return 1;
                return string.Compare(s1, s2, true);
            }
            public static bool IsNumeric(object value)
            {
                try
                {
                    int i = Convert.ToInt32(value.ToString());
                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }
            }
        }


    }


}

