using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    public class CommonGateway : System.Web.UI.Page
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

    public partial class employessreports : CommonGateway
    {

        public class searchreports
        {
            public string etype { get; set; }
            public string name { get; set; }
            public string rtype { get; set; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ddlsublevel.DataSource = Common.GetEmployeeTypeToDDL(UserData.CompanyID);
                ddlsublevel.DataBind();
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReportsType1(searchreports data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var qcompany = new TCompany();
            var tuser = new List<MasterEntity.TUser>();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                qcompany = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
                tuser = dbmaster.TUsers.Where(w => w.cType == "1" && w.nCompany == qcompany.nCompany && w.cDel == null).ToList();
            }

            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {

                var qry1 = dbschool.TEmployees.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID);

                if (!string.IsNullOrEmpty(data.etype))
                {
                    qry1 = qry1.Where(o => o.cType == data.etype);
                }

                var q1 = (from a in qry1
                          from c in dbschool.TTimetypes.Where(w => w.SchoolID == a.SchoolID && a.nTimeType == w.nTimeType).DefaultIfEmpty()
                          from d in dbschool.TEmployeeTypes.Where(o => o.SchoolID == a.SchoolID && (o.nTypeId2 ?? o.nTypeId) + "" == a.cType).DefaultIfEmpty()
                          from m in dbschool.TEmpSalaries.Where(o => o.SchoolID == a.SchoolID && o.cDel == false && o.sEmp == a.sEmp).DefaultIfEmpty()
                          from f in dbschool.TTitleLists.Where(o => o.nTitleid + "" == a.sTitle && o.SchoolID == a.SchoolID).DefaultIfEmpty()

                          where (a.sName + " " + a.sLastname).Contains(data.name) && m.WorkStatus != 2

                          select new
                          {
                              a.sEmp,
                              a.nTimeType,
                              cType = a.cType ?? "-",
                              title = f == null ? a.sTitle : f.titleDescription ,
                              a.sName,
                              a.sLastname,
                              a.sPhone,
                              type = d.Title,//a.cType.Trim() == "2" ? "อาจารย์" : "พนักงาน",
                              c.sTimeType
                          })
                          .ToList();

                var d1 = (from a in tuser
                          from b in q1.Where(w => a.sID == w.sEmp)

                          orderby a.cType, a.sName

                          select new
                          {
                              employessname = b.title + " " + a.sName + " " + a.sLastname,
                              phone = a.sPhone,
                              type = b?.type,
                              password = string.IsNullOrEmpty(a?.sFinger) ? a?.sPassword : "",
                              timetable = b?.sTimeType ?? ""
                          });

                return d1.OrderBy(o => o.type);

            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReportsType2(searchreports data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var qcompany = new TCompany();
            // var tuserx = new List<MasterEntity.TUser>();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                qcompany = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
                // tuser = dbmaster.TUsers.Where(w => w.cType == "1" && w.nCompany == qcompany.nCompany).ToList();
            }

            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var schoolID = userData.CompanyID;

                var qry1 = dbschool.TEmployees.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID);

                if (!string.IsNullOrEmpty(data.etype))
                {
                    qry1 = qry1.Where(o => o.cType == data.etype);
                }

                var q1 =
                              (from a in qry1

                               from b1 in dbschool.TEmployeeTypes.Where(o => o.SchoolID == schoolID && (o.nTypeId2 ?? o.nTypeId) + "" == a.cType).DefaultIfEmpty()

                               from b2 in dbschool.TJobLists.Where(o => o.nJobid == a.nJobid && o.SchoolID == schoolID && o.cDel == false).DefaultIfEmpty()

                               from b3 in dbschool.TDepartments.Where(o => o.DepID == a.nDepartmentId && o.SchoolID == schoolID && o.cDel == false).DefaultIfEmpty()

                               from j in dbschool.TEmpSalaries.Where(o => o.SchoolID == schoolID && o.sEmp == a.sEmp && o.cDel == false).DefaultIfEmpty()
                                   // from b4 in dbschool.TTitleLists.Where(o => o.nTitleid + "" == a.sTitle && o.SchoolID == schoolID && o.cDel == false)
                                   //.DefaultIfEmpty()

                               join c in dbschool.TEmpAddresses.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals c.sEmp into addressList
                               join d in dbschool.TEmpEducationInfoes.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals d.sEmp into eduList
                               join e in dbschool.TEmpFamilies.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals e.sEmp into famList
                               join f in dbschool.TEmpHonors.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals f.sEmp into honorList
                               join g in dbschool.TEmpInsignias.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals g.sEmp into insiList
                               join h in dbschool.TEmployeeInfoes.Where(o => o.SchoolID == schoolID /*&& o.cDel == false*/) on a.sEmp equals h.sEmp into infoList
                               join i in dbschool.TEmpProfessionalLicenses.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals i.sEmp into licensList

                               join k in dbschool.TEmpTeachings.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals k.sEmp into teachList
                               join l in dbschool.TEmpTrainings.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals l.sEmp into trainList
                               join m in dbschool.TEmpSalaries.Where(o => o.SchoolID == schoolID && o.cDel == false) on a.sEmp equals m.sEmp into salaryList

                               where (a.sName + " " + a.sLastname).Contains(data.name) && j.WorkStatus != 2 //&& (salaryList == null || salaryList.Any( o =>o.WorkStatus != 2))

                               select new
                               {
                                   a,
                                   type = b1.Title,
                                   job = b2.jobDescription,
                                   dept = b3.departmentName,
                                   //title = b4.titleDescription,
                                   //dob = a.dBirth.Value.ToString("dd/MM/yyyy" , new CultureInfo("th-TH")),

                                   address = addressList.Select(i => i).ToList(),
                                   //addressDesc = addressList.Select(i => new Report2ModelAddress
                                   //{
                                   //    ID = i.ID,
                                   //    Tubmon = "",
                                   //    Amphur = "",
                                   //    Province = "",
                                   //}).ToList(),

                                   education = eduList.Select(i => i).ToList(),
                                   family = famList.Select(i => i).ToList(),
                                   honor = honorList.Select(i => i).ToList(),
                                   training = trainList.Select(i => i).ToList(),
                                   insignia = insiList.Select(i => i).ToList(),
                                   info = infoList.Select(i => i).ToList(),
                                   license = licensList.Select(i => i).ToList(),
                                   teaching = teachList.Select(i => i).ToList(),
                                   //salary = j
                                   salary = salaryList.Select(i => i).ToList(),
                               });



                //var master = dbschool.TMasterDatas.ToList();
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var usermaster = dbmaster.TUsers.Where(w => w.nCompany == userData.CompanyID && w.cType == "1" && w.cDel == null).ToList();

                    var d_provinces = dbmaster.provinces.Select(s => new ModelItem1 { Id = s.PROVINCE_ID + "", Name = s.PROVINCE_NAME }).ToList();
                    var d_districts = dbmaster.districts.Select(s => new ModelItem1 { Id = s.DISTRICT_ID + "", Name = s.DISTRICT_NAME }).ToList();
                    var d_amphurs = dbmaster.amphurs.Select(s => new ModelItem1 { Id = s.AMPHUR_ID + "", Name = s.AMPHUR_NAME }).ToList();

                    var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                    var nation = dbschool.TMasterDatas.Where(w => w.MasterType == "3").ToList();
                    var religion = dbschool.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                    var race = dbschool.TMasterDatas.Where(w => w.MasterType == "9").ToList();
                    var timetypes = dbschool.TTimetypes.Where(w => w.SchoolID == userData.CompanyID && w.cUserType == "2").ToList();
                    //var levels = dbschool.TLevels.Where(w => w.SchoolID == schoolID).ToList();
                    var levels = new List<EduLevel>() {
                        new EduLevel {
                            LevelID = 1 ,
                            LevelName ="ต่ำกว่าประถมศึกษา"
                        },
                        new EduLevel {
                            LevelID = 2 ,
                            LevelName ="ประถมศึกษา"
                        },
                        new EduLevel {
                            LevelID = 3 ,
                            LevelName ="มัธยมศึกษาหรือเทียบเท่า"
                        },
                        new EduLevel {
                            LevelID = 4 ,
                            LevelName ="ปริญญาตรี หรือเทียบเท่า"
                        },
                         new EduLevel {
                            LevelID = 5 ,
                            LevelName ="ปริญญาโท"
                        },
                         new EduLevel {
                            LevelID = 6 ,
                            LevelName ="ปริญญาเอก"
                        },
                            new EduLevel {
                            LevelID = 7 ,
                            LevelName ="ประกาศนียบัตรวิชาชีพ(ปวช.)"
                        },
                        new EduLevel {
                            LevelID = 8 ,
                            LevelName ="ประกาศนียบัตรวิชาชีพขั้นสูง(ปวส.)"
                        },
                        new EduLevel {
                            LevelID = 9 ,
                            LevelName ="ประกาศนียบัตรบัณฑิตวิชาชีพครู(ป.บัณฑิต)"
                        },
                    };


                    var d1 = from o in q1.ToList()
                             from b in usermaster.Where(i => i.sID == o.a.sEmp)//.DefaultIfEmpty()

                             select new
                             {
                                 emp = new
                                 {
                                     o.a.cSex,
                                     o.a.sName,
                                     o.a.sLastname,
                                     o.a.sIdentification,
                                     o.a.sHomeNumber,
                                     o.a.sMuu,
                                     o.a.Village,
                                     o.a.sSoy,
                                     o.a.Building,
                                     o.a.sRoad,
                                     o.a.nMoney,
                                     //o.a.sProvince,
                                     //o.a.sAumpher,
                                     //o.a.sTumbon,

                                     Tumbon = getAddress(d_districts, o.a.sTumbon + ""),
                                     Amphur = getAddress(d_amphurs, o.a.sAumpher + ""),
                                     Province = getAddress(d_provinces, o.a.sProvince + ""),

                                     o.a.sPost,
                                     o.a.sEmail,
                                     o.a.sPhone,
                                     o.a.sEmp
                                 },
                                 o.type,
                                 o.job,
                                 o.dept,

                                 dob = o.a.dBirth?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                 title = getTitlte(q_titles, o.a.sTitle),
                                 timetype = getTimeType(timetypes, o.a.nTimeType),
                                 bio = b.UseBiometric ? "ใช้" : "ไม่ใช้",
                                 password = string.IsNullOrEmpty(b.sFinger) ? b.sPassword : "",

                                 sTumbon = getAddress(d_districts, o.a.sTumbon + ""),
                                 sAumpher = getAddress(d_amphurs, o.a.sAumpher + ""),
                                 sProvince = getAddress(d_provinces, o.a.sProvince + ""),

                                 address = o.address.Select((i, j) => new
                                 {
                                     ID = j + 1,
                                     i.No,
                                     i.VillageNo,
                                     i.Village,
                                     i.Building,
                                     i.Alley,
                                     i.Road,
                                     //Tubmon = i.SubdistrictID,
                                     //Amphur = i.DistrictID,
                                     //Province = i.ProvinceID,
                                     i.Postcode,

                                     Tubmon = getAddress(d_districts, i.SubdistrictID + ""),
                                     Amphur = getAddress(d_amphurs, i.DistrictID + ""),
                                     Province = getAddress(d_provinces, i.ProvinceID + ""),
                                 }).OrderBy(y => y.ID),

                                 education = o.education.Select((i, j) => new
                                 {
                                     ID = j + 1,
                                     i.Institution,
                                     i.StudyYear,
                                     i.GraduationYear,
                                     //i.LevelID,
                                     i.Major,
                                     i.MinorSubject,

                                     Level = getLevel(levels, i.LevelID),
                                 }).OrderBy(y => y.ID),

                                 family = o.family.Select((i, j) => new
                                 {
                                     ID = j + 1,
                                     i.FamilyRelation,
                                     //i.TitleID,
                                     i.FirstName,
                                     i.LastName,
                                     //i.Birthday,
                                     i.PersonalStatus,
                                     i.LiveStatus,
                                     i.DeathStatus,
                                     i.FamilyCareer,
                                     //i.LevelID,

                                     Title = getTitlte(q_titles, i.TitleID + ""),
                                     Level = getLevel(levels, i.LevelID),
                                     dob = i.Birthday?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                 }).OrderBy(y => y.ID),

                                 honor = o.honor.Select((i, j) => new
                                 {
                                     ID = j + 1,
                                     i.Type,
                                     i.Department,
                                     i.Year,
                                 }).OrderBy(y => y.ID),

                                 insignia = o.insignia.Select((i, j) => new
                                 {
                                     ID = j + 1,
                                     i.Year,
                                     i.Grade,
                                     i.Position,
                                     i.BookNumber,
                                     i.Part,
                                     i.Duty,
                                     i.Number,
                                     Date = i.Date?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                 }).OrderBy(y => y.ID),

                                 info = o.info.Select((i, j) => new
                                 {
                                     ID = j + 1,
                                     i.Code,
                                     i.FirstNameEn,
                                     i.LastNameEn,
                                     i.PassportNumber,
                                     i.PassportCountry,
                                     i.BloodType,
                                     //i.Nationality,
                                     //i.Ethnicity,
                                     //i.Religion,
                                     i.PersonalStatus,
                                     i.SpouseFirstName,
                                     i.SpouseLastName,

                                     Religion = Common.geTReligion(religion, i.Religion),
                                     Ethnicity = Common.geTRace(race, i.Ethnicity),
                                     Nationality = Common.geTNation(nation, i.Nationality),
                                 }).OrderBy(y => y.ID),

                                 license = o.license.Select((i, j) => new
                                 {
                                     ID = j + 1,
                                     i.LicenseType,
                                     i.LicenseNo,
                                     i.LicenseName,
                                     IssuedDate = i.IssuedDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     ExpireDate = i.ExpireDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     i.AgencyIssued,
                                 }).OrderBy(y => y.ID),

                                 teaching = o.teaching.Select((i, j) => new
                                 {
                                     ID = j + 1,
                                     i.nYear,
                                     i.nTerm,
                                     i.courseTypeId,
                                     i.SUBJECT_ID,
                                     i.sClassID,
                                     i.sRoomID,
                                     i.HoursPerWeek,
                                     i.DirectTeaching,
                                     i.CompetentTeaching,
                                     i.WantTrain,
                                 }).OrderBy(y => y.ID),

                                 training = o.training.Select((i, j) => new
                                 {
                                     ID = j + 1,
                                     i.ProjectName,
                                     i.TrainingName,
                                     //i.StartDate,
                                     //i.EndDate,
                                     i.Place,
                                     i.Country,
                                     i.Expenses,
                                     i.Province,
                                     i.TrainingType,
                                     i.TrainingHours,

                                     StartDate = i.StartDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     EndDate = i.EndDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                 }).OrderBy(y => y.ID),

                                 salary = o.salary.Select((i, j) => new
                                 {
                                     ID = j + 1,
                                     i.WorkStatus,
                                     WorkInEducationDate = i.WorkInEducationDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     i.Salary,
                                     i.PositionMoney,
                                     RetirementDate = i.RetirementDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     i.RemainGovernmentYear,
                                     i.RemainGovernmentMonth,
                                     i.RemainGovernmentDay,
                                     i.Degree,
                                     GovernmentOrderDate = i.GovernmentOrderDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     WorkStartDate = i.WorkStartDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                     i.AcademicStandingMoney,
                                     i.NetSalary,
                                     i.DayQuit,
                                 }).OrderBy(y => y.ID),
                             };

                    return d1.OrderBy(o => o.type);
                    //foreach (var e in d1)
                    //{

                    //    //stReligion = Common.geTReligion(religion, a.a.sStudentReligion),
                    //    //                    stRace = Common.geTRace(race, a.a.sStudentRace),
                    //    //                    stNation = Common.geTNation(nation, a.a.sStudentNation),
                    //    //e.dob = e.emp.dBirth?.ToString("dd/MM/yyyy" , new CultureInfo("th-TH"));
                    //    //e.emp.sTitle = getTitlte(q_titles, e.emp.sTitle);
                    //    //e.timetype = getTimeType(timetypes, e.emp.nTimeType);
                    //    //e.bio = user.UseBiometric ? "ใช้" : "ไม่ใช้";
                    //    //e.password = string.IsNullOrEmpty(user.sFinger) ? user.sPassword : "";

                    //    //e.emp.sTumbon = getAddress(d_districts, e.emp.sTumbon + "");
                    //    //e.emp.sAumpher = getAddress(d_amphurs, e.emp.sAumpher + "");
                    //    //e.emp.sProvince = getAddress(d_provinces, e.emp.sProvince + "");

                    //    //foreach (var i in e.info)
                    //    //{
                    //    //    i.Religion = Common.geTReligion(religion, i.Religion);
                    //    //    i.Ethnicity = Common.geTRace(race, i.Ethnicity);
                    //    //    i.Nationality = Common.geTNation(nation, i.Nationality);
                    //    //}

                    //    //for (int i = 0; i < e.address.Count(); i++)
                    //    //{
                    //    //    var desc = new Report2ModelAddress();
                    //    //    desc.Tubmon = getAddress(d_districts, e.address[i].SubdistrictID + "");
                    //    //    desc.Amphur = getAddress(d_amphurs, e.address[i].DistrictID + "");
                    //    //    desc.Province = getAddress(d_provinces, e.address[i].ProvinceID + "");

                    //    //    e.addressDesc.Add(desc);
                    //    //}

                    //    //for (int i = 0; i < e.family.Count(); i++)
                    //    //{
                    //    //    var desc = new Report2ModelFamily();
                    //    //    desc.Title = getTitlte(q_titles, e.family[i].TitleID + "");
                    //    //    desc.Level = getLevel(levels, e.family[i].LevelID);
                    //    //    desc.dob = e.family[i].Birthday?.ToString("dd/MM/yyyy" , new CultureInfo("th-TH"));
                    //    //    e.familyDesc.Add(desc);
                    //    //}

                    //    //for (int i = 0; i < e.education.Count(); i++)
                    //    //{
                    //    //    var desc = new Report2ModelEdu();
                    //    //    desc.Level = getLevel(levels, e.education[i].LevelID);
                    //    //    e.educationDesc.Add(desc);
                    //    //}


                    //    //for (int i = 0; i < e.training.Count(); i++)
                    //    //{
                    //    //    var desc = new Report2ModelTrain();
                    //    //    desc.Level = getLevel(levels, e.training[i].LevelID);
                    //    //    e.trainDesc.Add(desc);
                    //    //}
                    //}

                }
            }
        }

        private static string getLevel(List<EduLevel> levels, int? id)
        {
            return levels.FirstOrDefault(o => o.LevelID == id)?.LevelName + "";
        }

        private static string getTimeType(List<TTimetype> timetypes, int? nTimeType)
        {
            return timetypes.FirstOrDefault(o => o.nTimeType == nTimeType)?.sTimeType + "";
        }

        public static string getAddress(List<ModelItem1> lst, string id)
        {
            return lst.FirstOrDefault(o => o.Id == id)?.Name + "";
        }

        public static string getTitlte(List<TTitleList> titles, string titlesId)
        {
            int nTitleid = 0;
            int.TryParse((titlesId ?? "0"), out nTitleid);
            var f_titles = titles.FirstOrDefault(f => f.nTitleid == nTitleid);
            if (f_titles == null) return titlesId;
            else return f_titles.titleDescription;
        }

        private class EduLevel
        {
            public int LevelID { get; set; }
            public string LevelName { get; set; }
        }
    }


    internal class Report2ModelAddress
    {
        public int ID { get; set; }
        public string Tubmon { get; set; }
        public string Amphur { get; set; }
        public string Province { get; set; }
    }

    internal class Report2ModelFamily
    {
        public string Level { get; set; }
        public string Title { get; set; }
        public string dob { get; internal set; }
    }

    internal class Report2ModelEdu
    {
        public string Level { get; set; }
    }

    internal class Report2ModelTrain
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    internal class Report2Model
    {
        public string type { get; set; }
        public string job { get; set; }
        public string dept { get; set; }
        public string title { get; set; }
        public TEmployee emp { get; set; }
        public List<TEmpAddress> address { get; set; }
        public List<Report2ModelAddress> addressDesc { get; internal set; }
        public List<TEmpEducationInfo> education { get; set; }
        public List<Report2ModelEdu> educationDesc { get; set; }
        public List<TEmpFamily> family { get; set; }
        public List<Report2ModelFamily> familyDesc { get; set; }
        public List<TEmpHonor> honor { get; set; }
        public List<TEmpInsignia> insignia { get; set; }
        public List<TEmployeeInfo> info { get; set; }
        public List<TEmpProfessionalLicense> license { get; set; }
        public List<TEmpTeaching> teaching { get; set; }
        public List<TEmpTraining> training { get; set; }
        public List<Report2ModelTrain> trainingDesc { get; set; }
        public string dob { get; internal set; }
        public string timetype { get; internal set; }
        public string password { get; internal set; }
        public string bio { get; internal set; }
    }
}

// //address = new

//     //c.ID,
//     c.No,
//     c.VillageNo,
//     c.Village,
//     c.Building,
//     c.Alley,
//     c.Road,
//     Tubmon = c.SubdistrictID,
//     Amphur = c.DistrictID,
//     Province = c.ProvinceID,
//     c.Postcode,

// //education = new

//     //d.ID,
//     d.Institution,
//     d.StudyYear,
//     d.GraduationYear,
//     d.LevelID,
//     d.Major,
//     d.MinorSubject,


//// family = new

//     // e.ID,
//     e.FamilyRelation,
//     e.TitleID,
//     e.FirstName,
//     e.LastName,
//     e.Birthday,
//     e.PersonalStatus,
//     e.LiveStatus,
//     e.DeathStatus,
//     e.FamilyCareer,
//    // e.LevelID,


// //honor = new

//     // f.ID,
//     f.Type,
//     f.Department,
//     f.Year,


// //insignia = new

//     // g.ID,
//    // g.Year,
//     g.Grade,
//     g.Position,
//     g.BookNumber,
//     g.Part,
//     g.Duty,
//     g.Number,


// //info = new

//    //  h.ID,
//     h.Code,
//     h.FirstNameEn,
//     h.LastNameEn,
//     h.PassportNumber,
//     h.PassportCountry,
//     h.BloodType,
//     h.Nationality,
//     h.Ethnicity,
//     h.Religion,
//    // h.PersonalStatus,
//     h.SpouseFirstName,
//     h.SpouseLastName,


// //license = new

//     // i.ID,
//     i.LicenseType,
//     i.LicenseNo,
//     i.LicenseName,
//     i.IssuedDate,
//     i.ExpireDate,
//     i.AgencyIssued,

// //teaching = new

//   //  k.ID,
//     k.nYear,
//     k.nTerm,
//     k.courseTypeId,
//     k.SUBJECT_ID,
//     k.sClassID,
//     k.sRoomID,
//     k.HoursPerWeek,
//     k.DirectTeaching,
//     k.CompetentTeaching,
//     k.WantTrain,


// //training = new

//     // l.ID,
//     l.ProjectName,
//     l.TrainingName,
//     l.StartDate,
//     l.EndDate,
//     l.Place,
//     l.Country,
//     l.Expenses,
//   //  l.Province,
//     l.TrainingType,
//     l.TrainingHours,


//address = addressList.Select(i => new 
//{
//    index = i.ID,
//    i.No,
//    i.VillageNo,
//    i.Village,
//    i.Building,
//    i.Alley,
//    i.Road,
//    Tubmon = i.SubdistrictID,
//    Amphur = i.DistrictID,
//    Province = i.ProvinceID,
//    i.Postcode,
//}),

//education = eduList.Select(i => i).ToList(),//eduList.Select(i => new
//{
//    index = i.ID,
//    i.Institution,
//    i.StudyYear,
//    i.GraduationYear,
//    i.LevelID,
//    i.Major,
//    i.MinorSubject,
//}),

//family = famList.Select(i => i).ToList(),
//{
//    index = i.ID,
//    i.FamilyRelation,
//    i.TitleID,
//    i.FirstName,
//    i.LastName,
//    i.Birthday,
//    i.PersonalStatus,
//    i.LiveStatus,
//    i.DeathStatus,
//    i.FamilyCareer,
//    i.LevelID,
//}),

// honor = honorList.Select(i => i).ToList(),
//{
//    index = i.ID,
//    i.Type,
//    i.Department,
//    i.Year,
//}),

//insignia = insiList.Select(i => i).ToList(),
//{
//    index = i.ID,
//    i.Year,
//    i.Grade,
//    i.Position,
//    i.BookNumber,
//    i.Part,
//    i.Duty,
//    i.Number,
//}),

// info = infoList.Select(i => i).ToList(),
//{
//    index = i.ID,
//    i.Code,
//    i.FirstNameEn,
//    i.LastNameEn,
//    i.PassportNumber,
//    i.PassportCountry,
//    i.BloodType,
//    i.Nationality,
//    i.Ethnicity,
//    i.Religion,
//    i.PersonalStatus,
//    i.SpouseFirstName,
//    i.SpouseLastName,
//}),

//license = licensList.Select(i => i).ToList(),
//{
//    index = i.ID,
//    i.LicenseType,
//    i.LicenseNo,
//    i.LicenseName,
//    i.IssuedDate,
//    i.ExpireDate,
//    i.AgencyIssued,
//}),

//teaching = teachList.Select(i => i).ToList(),
//{
//    index = i.ID,
//    i.nYear,
//    i.nTerm,
//    i.courseTypeId,
//    i.SUBJECT_ID,
//    i.sClassID,
//    i.sRoomID,
//    i.HoursPerWeek,
//    i.DirectTeaching,
//    i.CompetentTeaching,
//    i.WantTrain,
//}),

//training = trainList.Select(i => i).ToList(),
//{
//    index = i.ID,
//    i.ProjectName,
//    i.TrainingName,
//    i.StartDate,
//    i.EndDate,
//    i.Place,
//    i.Country,
//    i.Expenses,
//    i.Province,
//    i.TrainingType,
//    i.TrainingHours,
//})