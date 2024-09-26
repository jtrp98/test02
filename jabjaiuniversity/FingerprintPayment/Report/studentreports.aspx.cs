using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Helper;
using System.Diagnostics;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using ScottPlot.Drawing.Colormaps;

namespace FingerprintPayment.Report
{
    public partial class studentreports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                //JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
                //DataTable dtYear = fcommon.LinqToDataTable(dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID).OrderByDescending(o => o.numberYear).ToList());

                //fcommon.ListDataTableToDropDownList(dtYear, ddlyear, "เลือกปีการศึกษา", "nYear", "numberYear");
                //ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                //using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                //{
                //    string sEntities = Session["sEntities"].ToString();
                //    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                //    hdfschoolname.Value = tCompany.sCompany;
                //    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);

                //    fcommon.LinqToDropDownList(q, ddlsublevel, "เลือกชั้นเรียน", "class_id", "class_name");
                //}
            }
        }

        public static string CalcIncomSalary(double? income)
        {
            //ต่ำกว่า 50,000 / 50,001 - 100,000 / 200,001- 300,000/ มากกว่า300,000 )
            if (income.HasValue)
            {
                return (income.Value * 12).ToString("#,0.#");
                //if (income < 50000)
                //{
                //    return "ต่ำกว่า 50,000";
                //}
                //else if (income < 100000)
                //{
                //    return "50,001 - 100,000";
                //}
                //else if (income < 300000)
                //{
                //    return "100,001- 300,000";
                //}
                //else
                //{
                //    return "มากกว่า 300,000";
                //}
            }
            else
            {
                return "-";
            }

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReports_Detail(searchreports_data data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            // dynamic rss = new JObject();

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            //var level2Id = 0;
            //var levelId = 0;
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                // var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var MasterTuser = dbmaster.TUsers.Where(w => w.nCompany == userData.CompanyID && w.cType == "0").ToList();

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {

                    var q2 = dbschool.TB_StudentViews.Where(o => o.SchoolID == userData.CompanyID
                        && (o.cDel ?? "0") != "1"
                        && data.status.Contains(o.nStudentStatus ?? 0)
                        //&& (o.nStudentStatus ?? 0) == 0 )  ) 
                        );

                    if (!string.IsNullOrEmpty(data.studentname))
                    {
                        q2 = q2.Where(w => (w.sName + " " + w.sLastname).Contains(data.studentname));
                    }

                    if (data.level2.HasValue)//(!string.IsNullOrEmpty(data.level2))
                    {
                        q2 = q2.Where(w => w.nTermSubLevel2 == data.level2);
                    }

                    if (data.level.HasValue)
                    {
                        q2 = q2.Where(w => w.nTSubLevel == data.level);
                    }

                    if (!string.IsNullOrEmpty(data.term_id))
                    {
                        q2 = q2.Where(w => w.nTerm == data.term_id);
                    }

                    var qry = (from studentview in q2

                               from user in dbschool.TUser.Where(o => o.sID == studentview.sID && o.SchoolID == studentview.SchoolID).DefaultIfEmpty()

                               let shi = dbschool.TStudentHealthInfoes.Where(o => o.sID == studentview.sID && o.SchoolID == studentview.SchoolID && o.sDeleted == "false").FirstOrDefault()
                               // from shi in dbschool.TStudentHealthInfoes.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID && o.sDeleted == "false").DefaultIf

                               from jf in dbschool.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID && w.sID == studentview.sID).DefaultIfEmpty()

                               from c in dbschool.TClassMembers.Where(o => o.SchoolID == userData.CompanyID && o.nTerm == studentview.nTerm && o.nTermSubLevel2 == studentview.nTermSubLevel2).DefaultIfEmpty()

                               from d in dbschool.TEmployees.Where(o => o.sEmp == c.nTeacherHeadid).DefaultIfEmpty()

                               from h in dbschool.TLevels.Where(w => w.SchoolID == userData.CompanyID && w.LevelID == studentview.nTLevel).DefaultIfEmpty()

                               select new
                               {
                                   user = user,
                                   sv = studentview,
                                   family = jf,
                                   studentview.SubLevel,
                                   studentview.nTSubLevel2,
                                   teacher = d.sName + " " + d.sLastname,
                                   shi,
                                   sortValue = h.sortValue,
                               })
                               .ToList();

                    var d_provinces = dbmaster.provinces.Select(s => new ModelItem1 { Id = s.PROVINCE_ID + "", Name = s.PROVINCE_NAME }).ToList();
                    var d_districts = dbmaster.districts.Select(s => new ModelItem1 { Id = s.DISTRICT_ID + "", Name = s.DISTRICT_NAME }).ToList();
                    var d_amphurs = dbmaster.amphurs.Select(s => new ModelItem1 { Id = s.AMPHUR_ID + "", Name = s.AMPHUR_NAME }).ToList();
                    var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();
                    var nation = dbschool.TMasterDatas.Where(w => w.MasterType == "3").ToList();
                    var religion = dbschool.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                    var race = dbschool.TMasterDatas.Where(w => w.MasterType == "9").ToList();

                    string query = string.Format(@"
SELECT shg1.*
FROM TStudentHealthGrowth shg1 INNER JOIN
(
    SELECT ROW_NUMBER() OVER(PARTITION BY nHealthID ORDER BY nTSubLevel DESC, NewnMonth DESC) 'SEQ', shg0.*
    FROM
    (
        SELECT nHealthID, nTSubLevel, nMonth, (CASE nMonth WHEN 5 THEN 1 WHEN 8 THEN 2 WHEN 11 THEN 3 WHEN 2 THEN 4 END) 'NewnMonth'
        FROM TStudentHealthGrowth
        WHERE SchoolID = {0} AND Weight > 0 AND Height > 0 {1}
    ) shg0
) shg2
ON shg1.nHealthID = shg2.nHealthID AND shg1.nTSubLevel = shg2.nTSubLevel AND shg1.nMonth = shg2.nMonth
WHERE shg2.SEQ = 1 AND SchoolID = {0}", userData.CompanyID, data.level.HasValue ? " AND nTSubLevel = " + data.level : "");

                    var studentHealthGrowth = dbschool.Database.SqlQuery<TStudentHealthGrowth>(query).ToList();


                    var query2 = string.Format($@"

SELECT u.sID , ISNULL(C.NFC1, '')+','+ISNULL(C.NFC2, '')+','+ISNULL(C.NFC3, '') 'Card'

FROM TUser u LEFT JOIN
(
	SELECT c.sID
	, MAX(CASE WHEN c.[No] = 1 THEN c.NFC END) 'NFC1', MAX(CASE WHEN c.[No] = 2 THEN c.NFC END) 'NFC2', MAX(CASE WHEN c.[No] = 3 THEN c.NFC END) 'NFC3'
    , MAX(CASE WHEN c.[No] = 1 THEN c.NFCReverse END) 'NFCRe1', MAX(CASE WHEN c.[No] = 2 THEN c.NFCReverse END) 'NFCRe2', MAX(CASE WHEN c.[No] = 3 THEN c.NFCReverse END) 'NFCRe3'
	, MAX(CASE WHEN c.[No] = 1 THEN c.NFCEncrypt END) 'ENNFC1', MAX(CASE WHEN c.[No] = 2 THEN c.NFCEncrypt END) 'ENNFC2', MAX(CASE WHEN c.[No] = 3 THEN c.NFCEncrypt END) 'ENNFC3'
	, MAX(CASE WHEN c.[No] = 1 THEN c.NFCEncryptReverse END) 'ENNFCRe1', MAX(CASE WHEN c.[No] = 2 THEN c.NFCEncryptReverse END) 'ENNFCRe2', MAX(CASE WHEN c.[No] = 3 THEN c.NFCEncryptReverse END) 'ENNFCRe3'
	, MAX(CASE WHEN c.[No] = 1 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive1', MAX(CASE WHEN c.[No] = 2 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive2', MAX(CASE WHEN c.[No] = 3 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive3'
	, MAX(CASE WHEN c.[No] = 1 THEN a.sName + ' ' + a.sLastname END) 'UpdateBy1', MAX(CASE WHEN c.[No] = 2 THEN a.sName + ' ' + a.sLastname END) 'UpdateBy2', MAX(CASE WHEN c.[No] = 3 THEN a.sName + ' ' + a.sLastname END) 'UpdateBy3'
		, MAX(CASE WHEN c.[No] = 1 THEN c.Modified END) 'UpdateDate1', MAX(CASE WHEN c.[No] = 2 THEN c.Modified END) 'UpdateDate2', MAX(CASE WHEN c.[No] = 3 THEN c.Modified END) 'UpdateDate3'
	FROM TUser_Card c 
    LEFT JOIN TUser a ON C.SchoolID = a.nCompany AND c.ModifyBy = a.sID
	WHERE c.SchoolID={userData.CompanyID} AND c.IsDel=0  AND ISNULL(c.NFC, '') <> '' 

	GROUP BY c.sID
) C ON u.sID = C.sID
--/studentcardregister/studentcardregister.aspx/returnlist()[1]
WHERE u.nCompany={userData.CompanyID} AND u.cType='0'");

                    var studentCard = dbmaster.Database.SqlQuery<StudentCard>(query2).ToList();

                    var d1 = qry

                        .GroupBy(o => new
                        {
                            o.teacher,
                            o.SubLevel,
                            o.nTSubLevel2,
                            o.sortValue,
                        })
                        .Select(o => new
                        {
                            teacher = o.Key.teacher,
                            level1 = o.Key.SubLevel,
                            level2 = o.Key.nTSubLevel2,
                            sortValue = o.Key.sortValue,

                            students = (from a in o

                                        from e in MasterTuser.Where(i => i.sID == a.user.sID).DefaultIfEmpty()

                                        from c in studentHealthGrowth.Where(i => i.nHealthID == a?.shi?.nHealthID).DefaultIfEmpty()

                                        from d in studentCard.Where(i => i.sID == a.user.sID).DefaultIfEmpty()

                                        select new
                                        {
                                            levelId = a.sv.nTSubLevel,
                                            level2Id = a.sv.nTermSubLevel2,
                                            tterm = a.sv.nTerm.Trim(),
                                            levelname = a.sv.SubLevel,
                                            level2name = a.sv.SubLevel + "/" + a.sv.nTSubLevel2,

                                            //ข้อมูลนักเรียน
                                            student_number = a.sv.nStudentNumber,
                                            studentStatus = (a.sv?.nStudentStatus ?? 0),
                                            sidentification = a.user == null ? "" : a.user.sIdentification,
                                            studentId = a.sv.sStudentID,
                                            studentPassWord = e == null ? "" : (e.sPassword == null ? "0" : e.sPassword),

                                            stMoveIn = a.sv.moveInDate.HasValue ? a.sv.moveInDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : string.Empty,
                                            studentsex = a.user.cSex,
                                            titleDes = getTitlte(q_titles, a.user.sStudentTitle),
                                            studentname = a.user.sName == null ? "" : a.user.sName,
                                            studentlastname = a.user.sLastname == null ? "" : a.user.sLastname,
                                            stNickName = a.user.sNickName == null ? "" : a.user.sNickName,
                                            studentnameEN = a.user.sStudentNameEN == null ? "" : a.user.sStudentNameEN,
                                            studentlastnameEN = a.user.sStudentLastEN == null ? "" : a.user.sStudentLastEN,
                                            stNickNameEN = a.user.sNickNameEN == null ? "" : a.user.sNickNameEN,

                                            birth = a.user.dBirth.HasValue ? a.user.dBirth.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : string.Empty,

                                            stReligion = Common.geTReligion(religion, a.user.sStudentReligion),
                                            stNation = Common.geTNation(nation, a.user.sStudentNation),
                                            stRace = Common.geTRace(race, a.user.sStudentRace),


                                            stSonTotal = a.family == null ? null : a.family.nSonTotal,
                                            stSonNumber = a.user.nSonNumber == null ? null : a.user.nSonNumber,
                                            stRelativeHere = a.family == null ? null : a.family.nRelativeStudyHere,
                                            phone = a.user.sPhone == null ? "" : a.user.sPhone,
                                            stEmail = a.user.sEmail == null ? "" : a.user.sEmail,
                                            money = string.Format("{0:#,#0}", a.user.nMoney),

                                            //ที่อยู่ตามทะเบียนบ้าน
                                            stHomeRegistCode = a.user.sStudentHomeRegisterCode == null ? "" : a.user.sStudentHomeRegisterCode,
                                            homeRegistNumber = a.family == null ? "" : a.family.houseRegistrationNumber,
                                            homeRegistSoy = a.family == null ? "" : a.family.houseRegistrationSoy,
                                            homeRegistMuu = a.family == null ? "" : a.family.houseRegistrationMuu,
                                            homeRegistRoad = a.family == null ? "" : a.family.houseRegistrationRoad,
                                            homeRegistTumbon = GetAddress(d_districts, a.family.houseRegistrationTumbon?.ToString()),
                                            homeRegistAumpher = GetAddress(d_amphurs, a.family.houseRegistrationAumpher?.ToString()),
                                            homeRegistProvince = GetAddress(d_provinces, a.family.houseRegistrationProvince?.ToString()),
                                            homeRegistPost = a.family == null ? "" : a.family.houseRegistrationPost,
                                            homeRegistPhone = a.family == null ? "" : a.family.houseRegistrationPhone,
                                            bornFrom = a.family == null ? "" : a.family.bornFrom,
                                            bornFromTumbon = GetAddress(d_districts, a.family.bornFromTumbon?.ToString()),
                                            bornFromAumpher = GetAddress(d_amphurs, a.family.bornFromAumpher?.ToString()),
                                            bornFromProvince = GetAddress(d_provinces, a.family.bornFromProvince?.ToString()),
                                            //ที่อยู่ปัจจุบัน
                                            homeNumber = a.user.sStudentHomeNumber == null ? "" : a.user.sStudentHomeNumber,
                                            muu = a.user.sStudentMuu == null ? "" : a.user.sStudentMuu,
                                            soy = a.user.sStudentSoy == null ? "" : a.user.sStudentSoy,
                                            road = a.user.sStudentRoad == null ? "" : a.user.sStudentRoad,
                                            tumbon = GetAddress(d_districts, a.user.sStudentTumbon ?? ""),
                                            aumpher = GetAddress(d_amphurs, a.user.sStudentAumpher ?? ""),
                                            provin = GetAddress(d_provinces, a.user.sStudentProvince ?? ""),
                                            post = a.user.sStudentPost == null ? "" : a.user.sStudentPost,
                                            stHousePhone = a.user.sStudentHousePhone == null ? "" : a.user.sStudentHousePhone,

                                            //อาศัยอยู่กับ
                                            ststayWithName = a.family == null ? "" : a.family.stayWithName,
                                            ststayWithLast = a.family == null ? "" : a.family.stayWithLast,
                                            ststayHomeType = a.family == null ? null : a.family.HomeType,
                                            ststayWithEmail = a.family == null ? "" : a.family.stayWithEmail,
                                            ststayWithEmergency = a.family == null ? "" : a.family.stayWithEmergencyCall,

                                            //เพื่อนใกล้บ้าน
                                            friNearHomename = a.family == null ? "" : a.family.friendName,
                                            friNearHomelast = a.family == null ? "" : a.family.friendLastName,
                                            friNearHomephone = a.family == null ? "" : a.family.friendPhone,

                                            //โรงเรียนเดิม
                                            stOldSchoolName = a.user.oldSchoolName == null ? "" : a.user.oldSchoolName,
                                            stOldSchoolTumbon = GetAddress(d_districts, a.user.oldSchoolTumbon == null ? "" : a.user.oldSchoolTumbon),
                                            stOldSchoolAumpher = GetAddress(d_amphurs, a.user.oldSchoolAumpher == null ? "" : a.user.oldSchoolAumpher),
                                            stOldSchoolProvince = GetAddress(d_provinces, a.user.oldSchoolProvince == null ? "" : a.user.oldSchoolProvince),
                                            stOldSchoolGraduated = a.user.oldSchoolGraduated == null ? "" : a.user.oldSchoolGraduated,
                                            stOldSchoolGPA = a.user.oldSchoolGPA == null ? null : a.user.oldSchoolGPA,
                                            stmoveOutReason = a.user.moveOutReason == null ? "" : a.user.moveOutReason,

                                            //ข้อมูลภูมิลำเนา
                                            stOldhome = a.family == null ? "" : a.family.houseRegistrationNumber,
                                            stOldmuu = a.family == null ? "" : a.family.houseRegistrationMuu,
                                            stOldsoy = a.family == null ? "" : a.family.houseRegistrationSoy,
                                            stOldroad = a.family == null ? "" : a.family.houseRegistrationRoad,
                                            stOldtumbon = GetAddress(d_districts, a.family == null ? "" : a.family.houseRegistrationTumbon.ToString()),
                                            stOldaumper = GetAddress(d_amphurs, a.family == null ? "" : a.family.houseRegistrationAumpher.ToString()),
                                            stOldprovince = GetAddress(d_provinces, a.family == null ? "" : a.family.houseRegistrationProvince.ToString()),
                                            stOldpostcode = a.family == null ? "" : a.family.houseRegistrationPost,
                                            stOldphone = a.family == null ? "" : a.family.houseRegistrationPhone,

                                            //ข้อมูลผู้ปกครอง
                                            famRelate = a.family == null ? "" : a.family.sFamilyRelate,
                                            famTitle = a.family == null ? "" : getTitlte(q_titles, a.family.sFamilyTitle),
                                            famName = a.family == null ? "" : a.family.sFamilyName,
                                            famlastname = a.family == null ? "" : a.family.sFamilyLast,
                                            famNameEN = a.family == null ? "" : a.family.sFamilyNameEN,
                                            famlastnameEN = a.family == null ? "" : a.family.sFamilyLastEN,
                                            famBirday = a.family == null ? "" : a.family.dFamilyBirthDay.HasValue ? a.family.dFamilyBirthDay.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : string.Empty,
                                            famReligion = a.family == null ? "" : Common.geTReligion(religion, a.family.sFamilyReligion),
                                            famRace = a.family == null ? "" : Common.geTRace(race, a.family.sFamilyRace),
                                            famNation = a.family == null ? "" : Common.geTNation(nation, a.family.sFamilyNation),
                                            famhome = a.family == null ? "" : a.family.sFamilyHomeNumber,
                                            fammuu = a.family == null ? "" : a.family.sFamilyMuu,
                                            famsoy = a.family == null ? "" : a.family.sFamilySoy,
                                            famroad = a.family == null ? "" : a.family.sFamilyRoad,
                                            famtumbon = GetAddress(d_districts, a.family == null ? "" : a.family.sFamilyTumbon),
                                            famaumper = GetAddress(d_amphurs, a.family == null ? "" : a.family.sFamilyAumpher),
                                            famprovince = GetAddress(d_provinces, a.family == null ? "" : a.family.sFamilyProvince),
                                            fampostcode = a.family == null ? "" : a.family.sFamilyPost,
                                            famphone1 = a.family == null ? "" : a.family.sPhoneOne,
                                            famphone2 = a.family == null ? "" : a.family.sPhoneTwo,
                                            famphone3 = a.family == null ? "" : a.family.sPhoneThree,
                                            famstatus = a.family == null ? null : a.family.familyStatus,
                                            fameducation = a.family == null ? null : a.family.sFamilyGraduated,
                                            famJob = a.family == null ? "" : a.family.sFamilyJob,
                                            famJobTower = a.family == null ? "" : a.family.sFamilyWorkPlace,
                                            famJobSalaryMonth = a.family == null ? "-" : a.family.nFamilyIncome?.ToString("#,0.#"),
                                            famJobSalary = a.family == null ? "-" : CalcIncomSalary(a.family.nFamilyIncome),//string.Format("{0:#,#0}", a.family.nFamilyIncome),
                                            famWithdrawMoney = a.family == null ? null : a.family.nFamilyRequestStudyMoney,

                                            //ข้อมูลบิดา
                                            faterTitle = a.family == null ? "" : getTitlte(q_titles, a.family.sFatherTitle),
                                            faterName = a.family == null ? "" : a.family.sFatherFirstName,
                                            faterLastname = a.family == null ? "" : a.family.sFatherLastName,
                                            faterNameEN = a.family == null ? "" : a.family.sFatherNameEN,
                                            faterLastnameEN = a.family == null ? "" : a.family.sFatherLastEN,
                                            faterBirday = a.family == null ? null : a.family.dFatherBirthDay.HasValue ? a.family.dFatherBirthDay.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : string.Empty,
                                            faterReligion = a.family == null ? "" : Common.geTReligion(religion, a.family.sFatherReligion),
                                            faterRace = a.family == null ? "" : Common.geTRace(race, a.family.sFatherRace),
                                            faterNation = a.family == null ? "" : Common.geTNation(nation, a.family.sFatherNation),
                                            faterhome = a.family == null ? "" : a.family.sFatherHomeNumber,
                                            fatermuu = a.family == null ? "" : a.family.sFatherMuu,
                                            fatersoy = a.family == null ? "" : a.family.sFatherSoy,
                                            faterroad = a.family == null ? "" : a.family.sFatherRoad,
                                            fatertumbon = GetAddress(d_districts, a.family == null ? "" : a.family.sFatherTumbon),
                                            fateraumper = GetAddress(d_amphurs, a.family == null ? "" : a.family.sFatherAumpher),
                                            faterprovince = GetAddress(d_provinces, a.family == null ? "" : a.family.sFatherProvince),
                                            faterpostcode = a.family == null ? "" : a.family.sFatherPost,
                                            faterphone1 = a.family == null ? "" : a.family.sFatherPhone,
                                            faterphone2 = a.family == null ? "" : a.family.sFatherPhone2,
                                            faterphone3 = a.family == null ? "" : a.family.sFatherPhone3,
                                            fatereducation = a.family == null ? null : a.family.sFatherGraduated,
                                            faterJob = a.family == null ? "" : a.family.sFatherJob,
                                            faterJobTower = a.family == null ? "" : a.family.sFatherWorkPlace,
                                            faterJobSalaryMonth = a.family == null ? "-" : a.family.nFatherIncome?.ToString("#,0.#"),
                                            faterJobSalary = a.family == null ? "-" : CalcIncomSalary(a.family.nFatherIncome),//null : string.Format("{0:#,#0}", a.family.nFatherIncome),

                                            //ข้อมูลมารดา
                                            moterTitle = a.family == null ? "" : getTitlte(q_titles, a.family.sMotherTitle),
                                            moterName = a.family == null ? "" : a.family.sMotherFirstName,
                                            moterLastname = a.family == null ? "" : a.family.sMotherLastName,
                                            moterNameEN = a.family == null ? "" : a.family.sMotherNameEN,
                                            moterLastnameEN = a.family == null ? "" : a.family.sMotherLastEN,
                                            moterBirday = a.family == null ? null : a.family.dMotherBirthDay.HasValue ? a.family.dMotherBirthDay.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : string.Empty,
                                            moterReligion = a.family == null ? "" : Common.geTReligion(religion, a.family.sMotherReligion),
                                            moterRace = a.family == null ? "" : Common.geTRace(race, a.family.sMotherRace),
                                            moterNation = a.family == null ? "" : Common.geTNation(nation, a.family.sMotherNation),
                                            moterhome = a.family == null ? "" : a.family.sMotherHomeNumber,
                                            motermuu = a.family == null ? "" : a.family.sMotherMuu,
                                            motersoy = a.family == null ? "" : a.family.sMotherSoy,
                                            moterroad = a.family == null ? "" : a.family.sMotherRoad,
                                            motertumbon = GetAddress(d_districts, a.family == null ? "" : a.family.sMotherTumbon),
                                            moteraumper = GetAddress(d_amphurs, a.family == null ? "" : a.family.sMotherAumpher),
                                            moterprovince = GetAddress(d_provinces, a.family == null ? "" : a.family.sMotherProvince),
                                            moterpostcode = a.family == null ? "" : a.family.sMotherPost,
                                            moterphone1 = a.family == null ? "" : a.family.sMotherPhone,
                                            moterphone2 = a.family == null ? "" : a.family.sMotherPhone2,
                                            moterphone3 = a.family == null ? "" : a.family.sMotherPhone3,
                                            motereducation = a.family == null ? null : a.family.sMotherGraduated,
                                            moterJob = a.family == null ? "" : a.family.sMotherJob,
                                            moterJobTower = a.family == null ? "" : a.family.sMotherWorkPlace,
                                            moterJobSalaryMonth = a.family == null ? "-" : a.family.nMotherIncome?.ToString("#,0.#"),
                                            moterJobSalary = a.family == null ? "-" : CalcIncomSalary(a.family.nMotherIncome),//? null : string.Format("{0:#,#0}", a.family.nMotherIncome),

                                            //ข้อมูลสุขภาพ
                                            stdWeight = c?.Weight?.ToString("0.00"),
                                            stdHeight = c?.Height?.ToString("0.00"),
                                            stdBlood = a.shi?.sBlood,
                                            stdSickFood = a.shi?.sSickFood,
                                            stdSickDruq = a.shi?.sSickDrug,
                                            stdSickOther = a.shi?.sSickOther,
                                            stdSickNormal = a.shi?.sSickNormal,
                                            stdSickDanger = a.shi?.sSickDanger,

                                            JourneyType = a.user.JourneyType.HasValue
                                            ? (a.user.JourneyType == 1 ? "ไป-กลับ" : a.user.DormitoryName)
                                            : "",
                                            CardNFC = string.Join(",", d.Card.Split(',').Where(i => !string.IsNullOrEmpty(i)))
                                        }
                                       ).OrderBy(i => i.student_number).ThenBy(i => i.studentId)

                        });

                    return d1.OrderBy(o => o.level1).ThenBy(o => o.level2);

                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReportsType2(searchreports_data data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var s = new TCompany();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                s = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
            }

            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var arrDisplay = new List<int?>() { 0, 4, null };
                var qry1 = from a in dbschool.TB_StudentViews
                            .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == data.year && o.nTerm == data.term_id
                              && (o.cDel ?? "0") != "1" && data.status.Contains(o.nStudentStatus ?? 0))

                           from b in dbschool.TClassMembers
                            .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                            .DefaultIfEmpty()
                           from c in dbschool.TEmployees
                            .Where(o => o.sEmp == b.nTeacherHeadid)
                            .DefaultIfEmpty()

                           select new
                           {
                               a.sName,
                               a.sLastname,
                               student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                               studentEn = a.sStudentNameEN + " " + a.sStudentLastEN,
                               a.nTerm,
                               a.SubLevel,
                               a.nTSubLevel,
                               a.nTSubLevel2,
                               a.nTermSubLevel2,
                               a.sStudentID,
                               a.nStudentNumber,
                               a.sTerm,
                               a.numberYear,
                               a.titleDescription,

                               teacher = c.sName + " " + c.sLastname,
                           };

                if (!string.IsNullOrEmpty(data.studentname))
                {
                    qry1 = qry1.Where(o => o.student.Contains(data.studentname));
                }

                if (data.level.HasValue)
                    qry1 = qry1.Where(o => o.nTSubLevel == data.level);

                if (data.level2.HasValue)
                    qry1 = qry1.Where(o => o.nTermSubLevel2 == data.level2);

                var studentList = qry1.ToList();

                return new
                {
                    school = new
                    {
                        logo = s.sImage,
                        nameTH = s.sCompany,
                        nameEN = s.sNameEN,
                        website = s.sWebsite,
                        phone1 = s.sPhoneOne,
                        phone2 = s.sPhoneTwo,
                        fax = s.sFax,
                        address = s.sAddress,
                    },

                    info = studentList
                    .GroupBy(o => new
                    {
                        o.teacher,
                        o.SubLevel,
                        o.nTSubLevel2,
                    })
                    .Select(o => new
                    {
                        room = new
                        {
                            o.Key.teacher,
                            level1 = o.Key.SubLevel,
                            level2 = o.Key.nTSubLevel2,

                            students = o.Select(i => new
                            {
                                title = i.titleDescription,
                                fname = i.sName,
                                lname = i.sLastname,
                                no = i.nStudentNumber,
                                code = i.sStudentID,
                                name = i.student,
                                nameEn = i.studentEn,
                            })
                            .OrderBy(i => i.no)
                        },
                    })
                    .OrderBy(o => o.room.level1).ThenBy(o => o.room.level2)

                    //.AsEnumerable()
                    //.Select((o, i) => new
                    //{
                    //    index = i++,
                    //    No = o.nStudentNumber,
                    //    code = o.sStudentID,
                    //    student = o.student,
                    //    level = o.SubLevel,
                    //    level2 = o.nTSubLevel2,
                    //    teacher = o.teacher,
                    //})

                };
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReportsType15(searchreports_data data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {

                var d_provinces = dbmaster.provinces
                    .Select(o => new ModelItem1 { Id = o.PROVINCE_ID + "", Name = o.PROVINCE_NAME }).ToList();
                var d_districts = dbmaster.districts
                    .Select(o => new ModelItem1 { Id = o.DISTRICT_ID + "", Name = o.DISTRICT_NAME }).ToList();
                var d_amphurs = dbmaster.amphurs
                    .Select(o => new ModelItem1 { Id = o.AMPHUR_ID + "", Name = o.AMPHUR_NAME }).ToList();
                var s = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
                var arrDisplay = new List<int?>() { 0, 4, null };

                var qry1 = from a in dbschool.TB_StudentViews
                            .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == data.year && o.nTerm == data.term_id
                              && (o.cDel ?? "0") != "1" && data.status.Contains(o.nStudentStatus ?? 0))

                           from b in dbschool.TClassMembers
                            .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                            .DefaultIfEmpty()

                           from c in dbschool.TEmployees
                            .Where(o => o.sEmp == b.nTeacherHeadid)
                            .DefaultIfEmpty()

                           from family in dbschool.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID && w.sID == a.sID).DefaultIfEmpty()

                           select new
                           {
                               a.titleDescription,
                               a.sName,
                               a.sLastname,
                               student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                               //studentEn = a.sStudentNameEN + " " + a.sStudentLastEN,
                               a.nTerm,
                               a.SubLevel,
                               a.nTSubLevel,
                               a.nTSubLevel2,
                               a.nTermSubLevel2,
                               a.sStudentID,
                               a.nStudentNumber,
                               a.sTerm,
                               a.numberYear,

                               teacher = c.sName + " " + c.sLastname,

                               number = family.houseRegistrationNumber,
                               soy = family.houseRegistrationSoy,
                               muu = family.houseRegistrationMuu,
                               road = family.houseRegistrationRoad,
                               tumbon = family.houseRegistrationTumbon,
                               aumpher = family.houseRegistrationAumpher,
                               province = family.houseRegistrationProvince,
                               family.houseRegistrationPost,
                               family.houseRegistrationPhone,
                               //homeRegistTumbon = GetAddress(d_districts, family.houseRegistrationTumbon),
                               //homeRegistAumpher = GetAddress(d_amphurs, family.houseRegistrationAumpher),
                               //homeRegistProvince = GetAddress(d_provinces, family.houseRegistrationProvince),
                           };

                if (!string.IsNullOrEmpty(data.studentname))
                    qry1 = qry1.Where(o => o.student.Contains(data.studentname));

                if (data.level.HasValue)
                    qry1 = qry1.Where(o => o.nTSubLevel == data.level);

                if (data.level2.HasValue)
                    qry1 = qry1.Where(o => o.nTermSubLevel2 == data.level2);

                var studentList = qry1.ToList();

                return new
                {
                    school = new
                    {
                        logo = s.sImage,
                        nameTH = s.sCompany,
                        nameEN = s.sNameEN,
                        website = s.sWebsite,
                        phone1 = s.sPhoneOne,
                        phone2 = s.sPhoneTwo,
                        fax = s.sFax,
                        address = s.sAddress,
                    },

                    info = studentList
                    .GroupBy(o => new
                    {
                        o.teacher,
                        o.SubLevel,
                        o.nTSubLevel2,
                    })
                    .Select(o => new
                    {
                        room = new
                        {
                            o.Key.teacher,
                            level1 = o.Key.SubLevel,
                            level2 = o.Key.nTSubLevel2,

                            students = o.Select(i => new
                            {
                                no = i.nStudentNumber,
                                code = i.sStudentID,
                                name = i.student,
                                title = i.titleDescription,
                                fname = i.sName,
                                lname = i.sLastname,

                                number = i.number,
                                soy = string.IsNullOrEmpty(i.soy) ? "" : "ซอย" + i.soy,
                                muu = string.IsNullOrEmpty(i.muu) ? "" : "หมู่" + i.muu,
                                road = string.IsNullOrEmpty(i.road) ? "" : "ถนน" + i.road,
                                //ตำบล/เเขวง อำเภอ/เขต จังหวัด  โทร                               
                                tumbon = i.tumbon.HasValue ? (i.province == 1 ? "เเขวง" : "ตำบล") + GetAddress(d_districts, i.tumbon + "") : "",
                                aumpher = i.aumpher.HasValue ? (i.province == 1 ? "เขต" : "อำเภอ") + GetAddress(d_amphurs, i.aumpher + "") : "",
                                province = i.province.HasValue ? (i.province == 1 ? "" : "จังหวัด") + GetAddress(d_provinces, i.province + "") : "",
                                post = i.houseRegistrationPost,
                                phone = string.IsNullOrEmpty(i.houseRegistrationPhone) ? "" : "โทร" + i.houseRegistrationPhone,

                            })
                            .OrderBy(i => i.no)
                        },
                    })
                    .OrderBy(o => o.room.level1).ThenBy(o => o.room.level2)

                };
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReportsType16(searchreports_data data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {

                var d_provinces = dbmaster.provinces
                    .Select(o => new ModelItem1 { Id = o.PROVINCE_ID + "", Name = o.PROVINCE_NAME }).ToList();
                var d_districts = dbmaster.districts
                    .Select(o => new ModelItem1 { Id = o.DISTRICT_ID + "", Name = o.DISTRICT_NAME }).ToList();
                var d_amphurs = dbmaster.amphurs
                    .Select(o => new ModelItem1 { Id = o.AMPHUR_ID + "", Name = o.AMPHUR_NAME }).ToList();
                var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                var s = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
                var arrDisplay = new List<int?>() { 0, 4, null };

                var qry1 = from a in dbschool.TB_StudentViews
                            .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == data.year && o.nTerm == data.term_id
                              && (o.cDel ?? "0") != "1" && data.status.Contains(o.nStudentStatus ?? 0))

                           from a1 in dbschool.TUser.Where(o => o.SchoolID == a.SchoolID && o.sID == a.sID)

                           from b in dbschool.TClassMembers
                            .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                            .DefaultIfEmpty()

                           from c in dbschool.TEmployees
                            .Where(o => o.sEmp == b.nTeacherHeadid)
                            .DefaultIfEmpty()

                           from family in dbschool.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID && w.sID == a.sID).DefaultIfEmpty()

                           select new
                           {
                               //a.titleDescription,
                               //a.sName,
                               //a.sLastname,
                               student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                               //studentEn = a.sStudentNameEN + " " + a.sStudentLastEN,
                               a.nTerm,
                               a.SubLevel,
                               a.nTSubLevel,
                               a.nTSubLevel2,
                               a.nTermSubLevel2,
                               a.sStudentID,
                               a.nStudentNumber,
                               a.sTerm,
                               a.numberYear,

                               teacher = c.sName + " " + c.sLastname,

                               a1.dBirth,
                               a1.oldSchoolName,
                               a1.oldSchoolGraduated,
                               a1.moveOutReason,
                               a1.moveInDate,

                               number = a1.sStudentHomeNumber,
                               soy = a1.sStudentSoy,
                               muu = a1.sStudentMuu,
                               road = a1.sStudentRoad,
                               province = a1.sStudentProvince,
                               aumpher = a1.sStudentAumpher,
                               tumbon = a1.sStudentTumbon,
                               a1.sStudentPost,
                               a1.sPhone,

                               family.sFatherTitle,
                               family.sFatherFirstName,
                               family.sFatherLastName,
                               family.sFatherJob,

                               family.sMotherTitle,
                               family.sMotherFirstName,
                               family.sMotherLastName,
                               family.sMotherJob,
                              
                               //number = family.houseRegistrationNumber,
                               //soy = family.houseRegistrationSoy,
                               //muu = family.houseRegistrationMuu,
                               //road = family.houseRegistrationRoad,
                               tumbonHome = family.houseRegistrationTumbon,
                               aumpherHome = family.houseRegistrationAumpher,
                               provinceHome = family.houseRegistrationProvince,
                               //family.houseRegistrationPost,
                               //family.houseRegistrationPhone,
                               //homeRegistTumbon = GetAddress(d_districts, family.houseRegistrationTumbon),
                               //homeRegistAumpher = GetAddress(d_amphurs, family.houseRegistrationAumpher),
                               //homeRegistProvince = GetAddress(d_provinces, family.houseRegistrationProvince),
                           };

                if (!string.IsNullOrEmpty(data.studentname))
                    qry1 = qry1.Where(o => o.student.Contains(data.studentname));

                if (data.level.HasValue)
                    qry1 = qry1.Where(o => o.nTSubLevel == data.level);

                if (data.level2.HasValue)
                    qry1 = qry1.Where(o => o.nTermSubLevel2 == data.level2);

                var studentList = qry1.ToList();

                return new
                {
                    school = new
                    {
                        logo = s.sImage,
                        nameTH = s.sCompany,
                        nameEN = s.sNameEN,
                        website = s.sWebsite,
                        phone1 = s.sPhoneOne,
                        phone2 = s.sPhoneTwo,
                        fax = s.sFax,
                        address = s.sAddress,
                    },

                    info = studentList
                    .GroupBy(o => new
                    {
                        o.teacher,
                        o.SubLevel,
                        o.nTSubLevel2,
                    })
                    .Select(o => new
                    {
                        room = new
                        {
                            o.Key.teacher,
                            level1 = o.Key.SubLevel,
                            level2 = o.Key.nTSubLevel2,

                            students = o.Select(i => new
                            {
                                no = i.nStudentNumber,
                                code = i.sStudentID,
                                name = i.student,
                                firstDate = i.moveInDate?.ToString("วันที่ dd เดือน MMMM พ.ศ. yyyy", new CultureInfo("th-TH")),
                                birthDay = i.dBirth?.ToString("วันที่ dd เดือน MMMM พ.ศ. yyyy",new CultureInfo("th-TH")),
                                oldSchool = i.oldSchoolName,
                                oldDegree = oldgraduate(i.oldSchoolGraduated),
                                reason = i.moveOutReason,
                                i.number,
                                soy = string.IsNullOrEmpty(i.soy) ? "" : "ซอย" + i.soy,
                                muu = string.IsNullOrEmpty(i.muu) ? "" : "หมู่" + i.muu,
                                road = string.IsNullOrEmpty(i.road) ? "" : "ถนน" + i.road,
                                //ตำบล/เเขวง อำเภอ/เขต จังหวัด  โทร                               
                                tumbon = !string.IsNullOrEmpty(i.tumbon) ? (i.province == "1" ? "เเขวง" : "ตำบล") + GetAddress(d_districts, i.tumbon + "") : "",
                                aumpher = !string.IsNullOrEmpty(i.aumpher) ? (i.province == "1" ? "เขต" : "อำเภอ") + GetAddress(d_amphurs, i.aumpher + "") : "",
                                province = !string.IsNullOrEmpty(i.province) ? (i.province == "1" ? "" : "จังหวัด") + GetAddress(d_provinces, i.province + "") : "",
                                post = i.sStudentPost,
                                phone = string.IsNullOrEmpty(i.sPhone) ? "" : "โทร" + i.sPhone,

                                tumbonHome = (i.tumbonHome.HasValue) ? (i.provinceHome == 1 ? "เเขวง" : "ตำบล") + GetAddress(d_districts, i.tumbonHome + "") : "",
                                aumpherHome = (i.aumpherHome.HasValue) ? (i.provinceHome == 1 ? "เขต" : "อำเภอ") + GetAddress(d_amphurs, i.aumpherHome + "") : "",
                                provinceHome = (i.provinceHome.HasValue) ? (i.provinceHome == 1 ? "" : "จังหวัด") + GetAddress(d_provinces, i.provinceHome + "") : "",

                                father =  getTitlte(q_titles, i.sFatherTitle) + " " + i.sFatherFirstName + " " + i.sFatherLastName,
                                mother = getTitlte(q_titles, i.sMotherTitle) + " " + i.sMotherFirstName+ " " + i.sMotherLastName,
                                fatherJob = i.sFatherJob,
                                motherJob = i.sMotherJob,
                            })
                            .OrderBy(i => i.no)
                        },
                    })
                    .OrderBy(o => o.room.level1).ThenBy(o => o.room.level2)

                };
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReportsType12(searchreports_data data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var s = new TCompany();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                s = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
            }

            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var arrDisplay = new List<int?>() { 0, 4, null };
                var qry1 = from a in dbschool.TB_StudentViews
                            .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == data.year && o.nTerm == data.term_id
                              && (o.cDel ?? "0") != "1" && data.status.Contains(o.nStudentStatus ?? 0))
                           from b in dbschool.TClassMembers
                            .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                            .DefaultIfEmpty()
                           from c in dbschool.TEmployees
                            .Where(o => o.sEmp == b.nTeacherHeadid)
                            .DefaultIfEmpty()

                           select new
                           {
                               student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                               a.nTerm,
                               a.SubLevel,
                               a.nTSubLevel,
                               a.nTSubLevel2,
                               a.nTermSubLevel2,
                               a.sStudentID,
                               a.nStudentNumber,
                               a.sTerm,
                               a.numberYear,
                               a.titleDescription,
                               teacher = c.sName + " " + c.sLastname,
                           };

                if (!string.IsNullOrEmpty(data.studentname))
                {
                    qry1 = qry1.Where(o => o.student.Contains(data.studentname));
                }

                if (data.level.HasValue)
                    qry1 = qry1.Where(o => o.nTSubLevel == data.level);

                if (data.level2.HasValue)
                    qry1 = qry1.Where(o => o.nTermSubLevel2 == data.level2);

                var studentList = qry1.ToList();

                return new
                {
                    school = new
                    {
                        logo = s.sImage,
                        nameTH = s.sCompany,
                        nameEN = s.sNameEN,
                        website = s.sWebsite,
                        phone1 = s.sPhoneOne,
                        phone2 = s.sPhoneTwo,
                        fax = s.sFax,
                        address = s.sAddress,
                    },

                    info = studentList
                    .GroupBy(o => new
                    {
                        o.teacher,
                        o.SubLevel,
                        o.nTSubLevel2,
                    })
                    .Select(o => new
                    {
                        room = new
                        {
                            o.Key.teacher,
                            level1 = o.Key.SubLevel,
                            level2 = o.Key.nTSubLevel2,

                            students = o.Select(i => new
                            {
                                no = i.nStudentNumber,
                                code = i.sStudentID,
                                name = i.student,
                            })
                            .OrderBy(i => i.no)
                        },
                    })
                    .OrderBy(o => o.room.level1).ThenBy(o => o.room.level2)

                    //.AsEnumerable()
                    //.Select((o, i) => new
                    //{
                    //    index = i++,
                    //    No = o.nStudentNumber,
                    //    code = o.sStudentID,
                    //    student = o.student,
                    //    level = o.SubLevel,
                    //    level2 = o.nTSubLevel2,
                    //    teacher = o.teacher,
                    //})

                };
            }
        }

        public static string GetAddress(List<ModelItem1> lst, string id)
        {
            return lst.FirstOrDefault(o => o.Id == id)?.Name + "";
        }

        //private string GetAmphur()
        //{
        //    return "";
        //}
        //private string GetProvince()
        //{
        //    return "";
        //}

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReportsType3(searchreports_data data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var s = new TCompany();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                s = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
            }

            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var arrDisplay = new List<int?>() { 0, 4, null };
                var q1 = from a in dbschool.TB_StudentViews
                            .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == data.year && o.nTerm == data.term_id
                            && data.status.Contains(o.nStudentStatus ?? 0)
                            && (o.cDel ?? "0") != "1")
                         from b in dbschool.TClassMembers
                          .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                          .DefaultIfEmpty()
                         from c in dbschool.TEmployees.Where(o => o.sEmp == b.nTeacherHeadid).DefaultIfEmpty()
                         from d in dbschool.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).DefaultIfEmpty()
                         select new ModelReportType3
                         {
                             title = a.titleDescription,
                             fname = a.sName,
                             lname = a.sLastname,
                             student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                             nTerm = a.nTerm,
                             SubLevel = a.SubLevel,
                             nTSubLevel = a.nTSubLevel,
                             nTSubLevel2 = a.nTSubLevel2,
                             nTermSubLevel2 = a.nTermSubLevel2,
                             sStudentID = a.sStudentID,
                             nStudentNumber = a.nStudentNumber,
                             numberYear = a.numberYear,
                             sTerm = a.sTerm,
                             sNickName = d.sNickName,
                             dBirth = d.dBirth,
                             cSex = d.cSex,
                             sIdentification = d.sIdentification,
                             teacher = c.sName + " " + c.sLastname,
                         };

                if (!string.IsNullOrEmpty(data.studentname))
                {
                    q1 = q1.Where(o => o.student.Contains(data.studentname));
                }

                if (data.level.HasValue)
                    q1 = q1.Where(o => o.nTSubLevel == data.level);

                if (data.level2.HasValue)
                    q1 = q1.Where(o => o.nTermSubLevel2 == data.level2);

                var studentList = q1.ToList();

                studentList.ForEach(o =>
                {
                    o.age = CalcAge(o.dBirth);
                });

                return new
                {
                    school = new
                    {
                        logo = s.sImage,
                        nameTH = s.sCompany,
                        nameEN = s.sNameEN,
                        website = s.sWebsite,
                        phone1 = s.sPhoneOne,
                        phone2 = s.sPhoneTwo,
                        fax = s.sFax,
                        email = s.sEmailOne ?? s.sEmailTwo ?? "-",
                        address = s.sAddress ?? "-",
                    },

                    info = studentList
                    .GroupBy(o => new
                    {
                        o.teacher,
                        o.sTerm,
                        o.numberYear,
                        o.SubLevel,
                        o.nTSubLevel2,
                    })
                    .Select(o => new
                    {
                        room = new
                        {
                            o.Key.teacher,
                            year = o.Key.numberYear,
                            term = o.Key.sTerm,
                            level1 = o.Key.SubLevel,
                            level2 = o.Key.nTSubLevel2,
                            male = o.Count(i => i.cSex == "0"),
                            female = o.Count(i => i.cSex == "1"),

                            students = o.Select(i => new
                            {
                                no = i.nStudentNumber,
                                code = i.sStudentID,
                                title = i.title,
                                fname = i.fname,
                                lname = i.lname,
                                name = i.student,
                                nick = i.sNickName ?? "",
                                id = i.sIdentification,
                                date = i.dBirth?.ToString(@"dd MMMM yyyy", new CultureInfo("th-TH")),
                                ageYear = i.age?.Year,
                                ageMonth = i.age?.Month,
                            })
                            .OrderBy(i => i.no)
                        },
                    })
                    .OrderBy(o => o.room.level1).ThenBy(o => o.room.level2)

                    //.AsEnumerable()
                    //.Select((o, i) => new
                    //{
                    //    index = i++,
                    //    No = o.nStudentNumber,
                    //    code = o.sStudentID,
                    //    student = o.student,
                    //    level = o.SubLevel,
                    //    level2 = o.nTSubLevel2,
                    //    teacher = o.teacher,
                    //})

                };
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReportsType6(searchreports_data data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var s = new TCompany();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                s = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
            }

            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var q1 = from a in dbschool.TB_StudentViews
                            .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == data.year && o.nTerm == data.term_id && (o.cDel ?? "0") != "1" && data.status.Contains(o.nStudentStatus ?? 0))
                         from b in dbschool.TClassMembers
                          .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                          .DefaultIfEmpty()
                         from c in dbschool.TEmployees
                          .Where(o => o.sEmp == b.nTeacherHeadid)
                          .DefaultIfEmpty()

                         from d in dbschool.TFamilyProfiles.Where(o => o.SchoolID == userData.CompanyID && o.sID == a.sID).DefaultIfEmpty()
                         from e in dbschool.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).DefaultIfEmpty()

                         select new
                         {
                             student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                             a.nTerm,
                             a.SubLevel,
                             a.nTSubLevel,
                             a.nTSubLevel2,
                             a.nTermSubLevel2,
                             a.sStudentID,
                             a.nStudentNumber,
                             a.numberYear,
                             a.sTerm,
                             e.sNickName,
                             e.dBirth,
                             e.cSex,
                             e.sIdentification,
                             teacher = c.sName + " " + c.sLastname,

                             d.sMotherIdCardNumber,
                             d.sMotherTitle,
                             mother = d.sMotherFirstName + " " + d.sMotherLastName,

                             d.sFatherIdCardNumber,
                             d.sFatherTitle,
                             father = d.sFatherFirstName + " " + d.sFatherLastName,

                             d.sFamilyIdCardNumber,
                             d.sFamilyTitle,
                             family = d.sFamilyName + " " + d.sFamilyLast,
                         };

                if (!string.IsNullOrEmpty(data.studentname))
                {
                    q1 = q1.Where(o => o.student.Contains(data.studentname));
                }

                if (data.level.HasValue)
                    q1 = q1.Where(o => o.nTSubLevel == data.level);

                if (data.level2.HasValue)
                    q1 = q1.Where(o => o.nTermSubLevel2 == data.level2);

                var studentList = q1.ToList();
                var titleList = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                return new
                {
                    school = new
                    {
                        logo = s.sImage,
                        nameTH = s.sCompany,
                        nameEN = s.sNameEN,
                        website = s.sWebsite,
                        phone1 = s.sPhoneOne,
                        phone2 = s.sPhoneTwo,
                        fax = s.sFax,
                        email = s.sEmailOne ?? s.sEmailTwo ?? "-",
                        address = s.sAddress ?? "-",
                    },

                    info = studentList
                    .GroupBy(o => new
                    {
                        o.teacher,
                        o.sTerm,
                        o.numberYear,
                        o.SubLevel,
                        o.nTSubLevel2,
                    })
                    .Select(o => new
                    {
                        room = new
                        {
                            o.Key.teacher,
                            year = o.Key.numberYear,
                            term = o.Key.sTerm,
                            level1 = o.Key.SubLevel,
                            level2 = o.Key.nTSubLevel2,
                            male = o.Count(i => i.cSex == "0"),
                            female = o.Count(i => i.cSex == "1"),

                            students = o.Select(i => new
                            {
                                no = i.nStudentNumber,
                                code = i.sStudentID,
                                name = i.student,
                                id = i.sIdentification,
                                familyid = i.sFamilyIdCardNumber ?? "-",
                                family = string.IsNullOrWhiteSpace(i.family) ? "-" : getTitlte(titleList, i.sFamilyTitle) + " " + i.family,
                                motherid = i.sMotherIdCardNumber ?? "-",
                                mother = string.IsNullOrWhiteSpace(i.mother) ? "-" : getTitlte(titleList, i.sMotherTitle) + " " + i.mother,
                                fatherid = i.sFatherIdCardNumber ?? "-",
                                father = string.IsNullOrWhiteSpace(i.father) ? "-" : getTitlte(titleList, i.sFatherTitle) + " " + i.father,
                            })
                            .OrderBy(i => i.no)
                        },
                    })
                    .OrderBy(o => o.room.level1).ThenBy(o => o.room.level2)

                    //.AsEnumerable()
                    //.Select((o, i) => new
                    //{
                    //    index = i++,
                    //    No = o.nStudentNumber,
                    //    code = o.sStudentID,
                    //    student = o.student,
                    //    level = o.SubLevel,
                    //    level2 = o.nTSubLevel2,
                    //    teacher = o.teacher,
                    //})

                };
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReportsType9(searchreports_data data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var s = new TCompany();
            var tuser = new List<MasterEntity.TUser>();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                s = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
                // tuser = dbmaster.TUsers.Where(o => o.nCompany == userData.CompanyID).ToList();
            }

            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var q1 = from a in dbschool.TB_StudentViews
                            .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == data.year && o.nTerm == data.term_id && (o.cDel ?? "0") != "1" && data.status.Contains(o.nStudentStatus ?? 0))

                         from b in dbschool.TClassMembers
                          .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                          .DefaultIfEmpty()

                         from c in dbschool.TEmployees
                          .Where(o => o.sEmp == b.nTeacherHeadid)
                          .DefaultIfEmpty()

                         from d in dbschool.TUser
                         .Where(o => o.SchoolID == a.SchoolID && o.sID == a.sID)
                         .DefaultIfEmpty()

                         select new
                         {
                             a.sID,
                             student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                             a.nTerm,
                             a.SubLevel,
                             a.nTSubLevel,
                             a.nTSubLevel2,
                             a.nTermSubLevel2,
                             a.sStudentID,
                             a.nStudentNumber,
                             a.numberYear,
                             a.sTerm,
                             d.sNickName,
                             d.dBirth,
                             d.cSex,
                             d.sIdentification,
                             teacher = c.sName + " " + c.sLastname,
                             picture = d.sStudentPicture,
                         };

                if (!string.IsNullOrEmpty(data.studentname))
                {
                    q1 = q1.Where(o => o.student.Contains(data.studentname));
                }

                if (data.level.HasValue)
                    q1 = q1.Where(o => o.nTSubLevel == data.level);

                if (data.level2.HasValue)
                    q1 = q1.Where(o => o.nTermSubLevel2 == data.level2);

                var studentList = q1.ToList();

                return new
                {
                    school = new
                    {
                        logo = s.sImage,
                        nameTH = s.sCompany,
                        nameEN = s.sNameEN,
                        website = s.sWebsite,
                        phone1 = s.sPhoneOne,
                        phone2 = s.sPhoneTwo,
                        fax = s.sFax,
                        email = s.sEmailOne ?? s.sEmailTwo ?? "-",
                        address = s.sAddress ?? "-",
                    },

                    info = studentList
                    .GroupBy(o => new
                    {
                        o.teacher,
                        o.sTerm,
                        o.numberYear,
                        o.SubLevel,
                        o.nTSubLevel2,
                    })
                    .Select(o => new
                    {
                        room = new
                        {
                            o.Key.teacher,
                            year = o.Key.numberYear,
                            term = o.Key.sTerm,
                            level1 = o.Key.SubLevel,
                            level2 = o.Key.nTSubLevel2,
                            male = o.Count(i => i.cSex == "0"),
                            female = o.Count(i => i.cSex == "1"),

                            students = o.Select(i => new
                            {
                                no = i.nStudentNumber,
                                code = i.sStudentID,
                                name = i.student,
                                nick = i.sNickName,
                                id = i.sIdentification,
                                date = i.dBirth?.ToString(@"dd MMM yyyy", new CultureInfo("th-TH")),
                                picture = i.picture ?? "",
                            })
                            .OrderBy(i => i.no)
                        },
                    })
                    .OrderBy(o => o.room.level1).ThenBy(o => o.room.level2)

                };
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SearchReportsType10(searchreports_data data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var s = new TCompany();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                s = dbmaster.TCompanies.Where(o => o.nCompany == userData.CompanyID).FirstOrDefault();
            }

            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var q1 = from a in dbschool.TB_StudentViews
                            .Where(o => o.SchoolID == userData.CompanyID && o.numberYear == data.year && o.nTerm == data.term_id && (o.cDel ?? "0") != "1" && data.status.Contains(o.nStudentStatus ?? 0))
                         from b in dbschool.TClassMembers
                          .Where(o => o.SchoolID == a.SchoolID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2)
                          .DefaultIfEmpty()
                         from c in dbschool.TEmployees.Where(o => o.sEmp == b.nTeacherHeadid).DefaultIfEmpty()
                         from d in dbschool.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).DefaultIfEmpty()

                         select new
                         {
                             a.sID,
                             student = a.titleDescription + " " + a.sName + " " + a.sLastname,
                             a.nTerm,
                             a.SubLevel,
                             a.nTSubLevel,
                             a.nTSubLevel2,
                             a.nTermSubLevel2,
                             a.sStudentID,
                             a.nStudentNumber,
                             a.numberYear,
                             a.sTerm,
                             d.sNickName,
                             d.dBirth,
                             d.cSex,
                             d.sIdentification,
                             teacher = c.sName + " " + c.sLastname,
                         };

                if (!string.IsNullOrEmpty(data.studentname))
                {
                    q1 = q1.Where(o => o.student.Contains(data.studentname));
                }

                if (data.level.HasValue)
                    q1 = q1.Where(o => o.nTSubLevel == data.level);

                if (data.level2.HasValue)
                    q1 = q1.Where(o => o.nTermSubLevel2 == data.level2);

                var studentList = q1.ToList();

                return new
                {
                    school = new
                    {
                        logo = s.sImage,
                        nameTH = s.sCompany,
                        nameEN = s.sNameEN,
                        website = s.sWebsite,
                        phone1 = s.sPhoneOne,
                        phone2 = s.sPhoneTwo,
                        fax = s.sFax,
                        email = s.sEmailOne ?? s.sEmailTwo ?? "-",
                        address = s.sAddress ?? "-",
                    },

                    info = studentList
                    .GroupBy(o => new
                    {
                        o.teacher,
                        o.sTerm,
                        o.numberYear,
                        o.nTSubLevel,
                        o.SubLevel,
                        o.nTSubLevel2,
                        o.nTermSubLevel2
                    })
                    .Select(o => new
                    {
                        room = new
                        {
                            o.Key.teacher,
                            year = o.Key.numberYear,
                            term = o.Key.sTerm,
                            lvl1id = o.Key.nTSubLevel,
                            level1 = o.Key.SubLevel,
                            lvl2id = o.Key.nTermSubLevel2,
                            level2 = o.Key.nTSubLevel2,
                            male = o.Count(i => i.cSex == "0"),
                            female = o.Count(i => i.cSex == "1"),

                            students = o.Select(i => new
                            {
                                sid = i.sID,
                                no = i.nStudentNumber,
                                code = i.sStudentID,
                                name = i.student,
                                nick = i.sNickName ?? "",
                                id = i.sIdentification,
                                date = i.dBirth?.ToString(@"dd MMM yyyy", new CultureInfo("th-TH"))
                            })
                            .OrderBy(i => i.no)
                        },
                    })
                    .OrderBy(o => o.room.level1).ThenBy(o => o.room.level2)

                    //.AsEnumerable()
                    //.Select((o, i) => new
                    //{
                    //    index = i++,
                    //    No = o.nStudentNumber,
                    //    code = o.sStudentID,
                    //    student = o.student,
                    //    level = o.SubLevel,
                    //    level2 = o.nTSubLevel2,
                    //    teacher = o.teacher,
                    //})

                };
            }
        }

        private static string getTitlte(List<TTitleList> titles, string titlesId)
        {
            int nTitleid = 0;
            int.TryParse((titlesId ?? "0"), out nTitleid);
            var f_titles = titles.FirstOrDefault(f => f.nTitleid == nTitleid);
            if (f_titles == null) return titlesId;
            else return f_titles.titleDescription;
        }

        private static ModelAge CalcAge(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                return null;


            //DateTime birthDate = new DateTime(1956, 8, 27);

            var dateTimeToday = DateTime.UtcNow;
            if (birthDate > dateTimeToday)
                return null;

            var difference = dateTimeToday.Subtract(birthDate.Value);
            var firstDay = new DateTime(1, 1, 1);
            int totalYears = (firstDay + difference).Year - 1;
            int totalMonths = (totalYears * 12) + (firstDay + difference).Month - 1;
            int runningMonths = totalMonths - (totalYears * 12);
            int runningDays = (dateTimeToday - birthDate.Value.AddMonths((totalYears * 12) + runningMonths)).Days;

            return new ModelAge() { Year = totalYears, Month = runningMonths, Days = runningDays };
        }

        private static string oldgraduate(string index)
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

        public class searchreports_data
        {
            public int? level { get; set; }
            public int? level2 { get; set; }
            public string term_id { get; set; }
            public string studentname { get; set; }
            public int? year_Id { get; set; }
            public int year { get; set; }
            public string dStart { get; set; }
            public string dEnd { get; set; }
            public List<int> status { get; set; }
        }

        private class StudentCard
        {
            public int sID { get; set; }
            public string Card { get; set; }
        }

        internal class StudentInfoReport
        {
            public int levelId { get; set; }
            public int level2Id { get; set; }
            public string tterm { get; set; }
            public string levelname { get; set; }
            public string level2name { get; set; }
            public int? student_number { get; set; }
            public int? studentStatus { get; set; }
            public string sidentification { get; set; }
            public string studentId { get; set; }
            public string stMoveIn { get; set; }
            public string studentsex { get; set; }
            public string titleDes { get; set; }
            public string studentname { get; set; }
            public string studentlastname { get; set; }
            public string stNickName { get; set; }
            public string studentnameEN { get; set; }
            public string studentlastnameEN { get; set; }
            public string stNickNameEN { get; set; }
            public string birth { get; set; }
            public string stReligion { get; set; }
            public string stNation { get; set; }
            public string stRace { get; set; }
            public int? stSonTotal { get; set; }
            public int? stSonNumber { get; set; }
            public int? stRelativeHere { get; set; }
            public string phone { get; set; }
            public string stEmail { get; set; }
            public string money { get; set; }
            public string stHomeRegistCode { get; set; }
            public string homeNumber { get; set; }
            public string muu { get; set; }
            public string soy { get; set; }
            public string road { get; set; }
            public string tumbon { get; set; }
            public string aumpher { get; set; }
            public string provin { get; set; }
            public string post { get; set; }
            public string stHousePhone { get; set; }
            public string ststayWithName { get; set; }
            public string ststayWithLast { get; set; }
            public int? ststayHomeType { get; set; }
            public string ststayWithEmail { get; set; }
            public string ststayWithEmergency { get; set; }
            public string friNearHomename { get; set; }
            public string friNearHomelast { get; set; }
            public string friNearHomephone { get; set; }
            public string stOldSchoolName { get; set; }
            public string stOldSchoolTumbon { get; set; }
            public string stOldSchoolAumpher { get; set; }
            public string stOldSchoolProvince { get; set; }
            public string stOldSchoolGraduated { get; set; }
            public decimal? stOldSchoolGPA { get; set; }
            public string stmoveOutReason { get; set; }
            public string stOldhome { get; set; }
            public string stOldmuu { get; set; }
            public string stOldsoy { get; set; }
            public string stOldroad { get; set; }
            public string stOldtumbon { get; set; }
            public string stOldaumper { get; set; }
            public string stOldprovince { get; set; }
            public string stOldpostcode { get; set; }
            public string stOldphone { get; set; }
            public string famRelate { get; set; }
            public string famTitle { get; set; }
            public string famName { get; set; }
            public string famlastname { get; set; }
            public string famNameEN { get; set; }
            public string famlastnameEN { get; set; }
            public string famBirday { get; set; }
            public string famReligion { get; set; }
            public string famRace { get; set; }
            public string famNation { get; set; }
            public string famhome { get; set; }
            public string fammuu { get; set; }
            public string famsoy { get; set; }
            public string famroad { get; set; }
            public string famtumbon { get; set; }
            public string famaumper { get; set; }
            public string famprovince { get; set; }
            public string fampostcode { get; set; }
            public string famphone1 { get; set; }
            public string famphone2 { get; set; }
            public string famphone3 { get; set; }
            public int? famstatus { get; set; }
            public int? fameducation { get; set; }
            public string famJob { get; set; }
            public string famJobTower { get; set; }
            public string famJobSalary { get; set; }
            public int? famWithdrawMoney { get; set; }
            public string faterTitle { get; set; }
            public string faterName { get; set; }
            public string faterLastname { get; set; }
            public string faterNameEN { get; set; }
            public string faterLastnameEN { get; set; }
            public string faterBirday { get; set; }
            public string faterReligion { get; set; }
            public string faterRace { get; set; }
            public string faterNation { get; set; }
            public string faterhome { get; set; }
            public string fatermuu { get; set; }
            public string fatersoy { get; set; }
            public string faterroad { get; set; }
            public string fatertumbon { get; set; }
            public string fateraumper { get; set; }
            public string faterprovince { get; set; }
            public string faterpostcode { get; set; }
            public string faterphone1 { get; set; }
            public string faterphone2 { get; set; }
            public string faterphone3 { get; set; }
            public int? fatereducation { get; set; }
            public string faterJob { get; set; }
            public string faterJobTower { get; set; }
            public string faterJobSalary { get; set; }
            public string moterTitle { get; set; }
            public string moterName { get; set; }
            public string moterLastname { get; set; }
            public string moterNameEN { get; set; }
            public string moterLastnameEN { get; set; }
            public string moterBirday { get; set; }
            public string moterReligion { get; set; }
            public string moterRace { get; set; }
            public string moterNation { get; set; }
            public string moterhome { get; set; }
            public string motermuu { get; set; }
            public string motersoy { get; set; }
            public string moterroad { get; set; }
            public string motertumbon { get; set; }
            public string moteraumper { get; set; }
            public string moterprovince { get; set; }
            public string moterpostcode { get; set; }
            public string moterphone1 { get; set; }
            public string moterphone2 { get; set; }
            public string moterphone3 { get; set; }
            public int? motereducation { get; set; }
            public string moterJob { get; set; }
            public string moterJobTower { get; set; }
            public string moterJobSalary { get; set; }
            public decimal? stdWeight { get; set; }
            public decimal? stdHeight { get; set; }
            public string stdBlood { get; set; }
            public string stdSickFood { get; set; }
            public string stdSickDruq { get; set; }
            public string stdSickOther { get; set; }
            public string stdSickNormal { get; set; }
            public string stdSickDanger { get; set; }
        }

        public class ModelAge
        {

            public int Year { get; set; }
            public int Month { get; set; }
            public int Days { get; set; }
        }

        public class ModelReportType3
        {
            public string student { get; set; }
            public string nTerm { get; set; }
            public string SubLevel { get; set; }
            public int nTSubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public int nTermSubLevel2 { get; set; }
            public string sStudentID { get; set; }
            public int? nStudentNumber { get; set; }
            public int? numberYear { get; set; }
            public string sTerm { get; set; }
            public string sNickName { get; set; }
            public DateTime? dBirth { get; set; }
            public ModelAge age { get; set; }
            public string cSex { get; set; }
            public string sIdentification { get; set; }
            public string teacher { get; set; }
            public string fname { get; internal set; }
            public string title { get; internal set; }
            public string lname { get; internal set; }
        }
    }



    public class ModelItem1
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}