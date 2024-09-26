using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JabjaiMainClass;
using MasterEntity;
using JabjaiEntity.DB;
using System.Web.Script.Services;
using System.Web.Services;
using System.Globalization;
using FingerprintPayment.Helper;
using FluentDateTimeOffset;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace FingerprintPayment.Report
{
    public partial class AmountStudent_Report : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                //JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
                //DataTable dtYear = fcommon.LinqToDataTable(_db.TYears.Where(w => w.SchoolID == userData.CompanyID).OrderByDescending(o => o.numberYear).ToList());

                ////fcommon.ListDataTableToDropDownList(dtYear, ddlyear, "เลือกปีการศึกษา", "nYear", "numberYear");
                ////ddlyear.SelectedValue = DateTime.Now.Year.ToString();

                //using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                //{
                //    string sEntities = Session["sEntities"].ToString();
                //    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                //    hdfschoolname.Value = tCompany.sCompany;
                //    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);

                //    fcommon.LinqToDropDownList(q, ddlsublevel, "เลือกระดับชั้น", "class_id", "class_name");
                //}
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object report_AmountStudent(Search search)
        {

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();

            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {


                //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var userData = GetUserData();

                var tTuser = db.Database.SqlQuery<JabjaiEntity.DB.TUser>($"SELECT * FROM TUser WHERE SchoolID = {userData.CompanyID} AND ISNULL(TUser.cDel,'0') != '1' ").ToList();

                var q_title = db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                var nation = db.TMasterDatas.Where(w => w.MasterType == "3").ToList();
                var religion = db.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                var race = db.TMasterDatas.Where(w => w.MasterType == "9").ToList();

                //var qryTSL = db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nDeleted != 1);

                //if (search.sUbLV_Id.HasValue)
                //{
                //    qryTSL = qryTSL.Where(o => o.nTSubLevel == search.sUbLV_Id);
                //}

                var thisDay = DateTime.Today;

                var term = db.TTerms.Where(o => o.SchoolID == userData.CompanyID && o.nTerm == search.tErm_Id).FirstOrDefault();

                //var qrySV = db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == search.tErm_Id && w.cDel != "1");
                //var qryTSL2 = db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID);

                var qry = $@"
SELECT A.sID ,  A.nTSubLevel , A.SubLevel , A.nTSubLevel2, A.nTermSubLevel2,  A.nTerm
, A.sName  , A.sLastname  , A.moveInDate , A.MoveOutDate 
, A2.dBirth , A2.sStudentReligion , A2.sStudentRace ,A2.nSonNumber , A2.cSex
, ISNULL(F1.PROVINCE_ID ,9999)'ProvinceID' , F1.PROVINCE_NAME 'ProvinceName'
, ISNULL(F2.AMPHUR_ID,9999) 'AmphurID' , F2.AMPHUR_NAME 'AmphurName'
,  ISNULL(F3.DISTRICT_ID,9999) 'TumbonID' , F3.DISTRICT_NAME 'TumbonName'

, E2.LevelID , E2.LevelName , E2.sortValue
, C.sName + ' ' + C.sLastname 'Teacher'
FROM JabjaiSchoolSingleDB.dbo.TB_StudentViews  A
JOIN JabjaiSchoolSingleDB.dbo.TUser A2 ON A.SchoolID = A2.SchoolID AND A.sId = A2.sID
LEFT JOIN JabjaiSchoolSingleDB.dbo.TClassMember B ON A.SchoolID = B.SchoolID AND A.nTerm = B.nTerm AND A.nTermSubLevel2 = B.nTermSubLevel2
LEFT JOIN JabjaiSchoolSingleDB.dbo.TEmployees C ON C.SchoolID = B.SchoolID AND C.sEmp = B.nTeacherHeadid
LEFT JOIN JabjaiSchoolSingleDB.dbo.TTermSubLevel2 E ON E.SchoolID = A.SchoolID AND E.nTermSubLevel2 = A.nTermSubLevel2
LEFT JOIN JabjaiSchoolSingleDB.dbo.TSubLevel E1 ON E1.SchoolID = A.SchoolID AND E1.nTSubLevel = A.nTSubLevel
LEFT JOIN JabjaiSchoolSingleDB.dbo.TLevel E2 ON E2.SchoolID = E1.SchoolID	 AND E2.LevelID = E1.nTLevel


LEFT JOIN JabjaiMasterSingleDB.dbo.province F1 ON F1.PROVINCE_ID = A2.sStudentProvince
LEFT JOIN JabjaiMasterSingleDB.dbo.amphur F2 ON F2.AMPHUR_ID = A2.sStudentAumpher
LEFT JOIN JabjaiMasterSingleDB.dbo.district F3 ON F3.DISTRICT_ID = A2.sStudentTumbon

WHERE A.SchoolID={userData.CompanyID} 
AND ISNULL(A.cDel,'0') != '1' 
AND A.nTerm='{search.tErm_Id}' 
AND E.nWorkingStatus = 1
AND A.sID IN (SELECT sID FROM JabjaiMasterSingleDB.dbo.TUser WHERE nCompany={userData.CompanyID} AND cType = '0')

";
                StudentLogic studentLogic = new StudentLogic(db);
                string termID = studentLogic.GetTermId(DateTime.Now, userData);

                //current term
                if ((thisDay >= term.dStart && thisDay <= term.dEnd) || termID == search.tErm_Id)
                {
                    qry += @" 
 
AND (
	((ISNULL(A.nStudentStatus, 0)=0 OR A.nStudentStatus=4) AND ISNULL(A.moveInDate, CONVERT(DATE, GETDATE())) <= CONVERT(DATE, GETDATE())) 
	OR ((A.nStudentStatus=1 OR A.nStudentStatus=2) AND CONVERT(DATE, GETDATE()) < ISNULL(A.MoveOutDate, CONVERT(DATE, GETDATE())))
	--OR ISNULL(A.nStudentStatus, 0) IN (3, 5, 6)
) ";
                }
                else//old term
                {
                    qry += @" AND ISNULL(A.nStudentStatus, 0) IN (0,4) ";
                }

                if (!string.IsNullOrEmpty(search.LevelID))
                {
                    var arr = search.LevelID.Split(',');
                    qry += $" AND A.nTSublevel IN ({(string.Join(",", arr))})";
                }

                var StudentData = db.Database.SqlQuery<StudentViewModel>(qry).ToList();

                //var sHistory = (from a in qrySV

                //                from e1 in db.TClassMembers
                //                .Where(o => o.SchoolID == userData.CompanyID && o.nTerm == a.nTerm && o.nTermSubLevel2 == a.nTermSubLevel2).DefaultIfEmpty()

                //                from e2 in db.TEmployees
                //                .Where(o => o.SchoolID == userData.CompanyID && o.sEmp == e1.nTeacherHeadid).DefaultIfEmpty()

                //                select new
                //                {
                //                    a.sID,
                //                    a.nTSubLevel,
                //                    a.nTSubLevel2,
                //                    a.nTermSubLevel2,
                //                    a.nTerm,
                //                    teacher = e2.sName + " " + e2.sLastname,
                //                }).ToList();

                var ListData = (
                    from a in StudentData
                        // from a in tTuser.Where(o => o.sID == b.sID)
                        //from c in qryTSL2.Where( o => o.nTermSubLevel2 == b.nTermSubLevel2)
                        //join c in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nWorkingStatus == 1) on b.nTermSubLevel2 equals c.nTermSubLevel2
                        //join d in qryTSL on b.nTSubLevel equals d.nTSubLevel
                        //from h in db.TLevels.Where(w => w.SchoolID == userData.CompanyID && w.LevelID == a.nTSubLevel) 

                        //join tP in masterEntities.provinces on a.sStudentProvince equals tP.PROVINCE_ID.ToString() into JAP
                        //from JtP in JAP.DefaultIfEmpty()
                        //join tA in masterEntities.amphurs on a.sStudentAumpher equals tA.AMPHUR_ID.ToString() into JAA
                        //from JtA in JAA.DefaultIfEmpty()
                        //join tD in masterEntities.districts on a.sStudentTumbon equals tD.DISTRICT_ID.ToString() into JAD
                        //from JtD in JAD.DefaultIfEmpty()

                    join tF in db.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on a.sID equals tF.sID into JAF
                    from JtF in JAF.DefaultIfEmpty()

                        //where /*d.nTSubLevel == search.sUbLV_Id &&*/ g.nTerm == search.tErm_Id
                    select new
                    {
                        aID = a.sID,
                        aName = a.sName,
                        aLasname = a.sLastname,
                        teacher = a.Teacher ?? "-",

                        aSex = a.cSex,
                        cDateMoveIn = a.moveInDate.HasValue ? a.moveInDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : "ไม่พบข้อมูล",
                        cBirthDay = a.dBirth == null ? DateTime.Now : a.dBirth,
                        cReligion = a.sStudentReligion,
                        cRace = a.sStudentRace,
                        cChildren = a.nSonNumber.ToString() == null ? "" : a.nSonNumber.ToString(),

                        groupclassid = a.LevelID,
                        groupsupername = a.LevelName,
                        groupsortvalue = a.sortValue,

                        classid = a.nTSubLevel,
                        classname = a.SubLevel,

                        roomid = a.nTermSubLevel2,
                        roomname = a.nTSubLevel2,

                        cProvinceID = a.ProvinceID + "",//? "9999" : a.PROVINCE_ID.ToString(),
                        cProvinceNAME = a.ProvinceName ?? "ไม่ระบุข้อมูล",
                        cAmphurID = a.AmphurID + "",
                        cAmphurNAME = a.AmphurName ?? "ไม่ระบุข้อมูล",
                        cDistrictID = a.TumbonID + "",
                        cDistrictNAME = a.TumbonName ?? "ไม่ระบุข้อมูล",

                        cFamilyJob = JtF == null ? "" : JtF.sFamilyJob,
                        cFamilyIncome = JtF == null ? "ไม่พบข้อมูล" : JtF.nFamilyIncome.ToString(),
                        cFamilyEducation = JtF == null ? "ไม่พบข้อมูล" : JtF.sFamilyGraduated.ToString(),
                        cFamilyReligion = JtF == null ? "99" : JtF.sFamilyReligion,
                        cFamilyStatus = JtF == null ? "ไม่พบข้อมูล" : JtF.familyStatus.ToString(),

                        cFatherIncome = JtF == null ? null : JtF.nFatherIncome,
                        cFatherEducation = JtF == null ? null : JtF.sFatherGraduated,
                        cFatherReligion = JtF == null ? "999" : JtF.sFatherReligion
                    }).ToList();


                var mEmList = (from a in ListData
                               orderby a.aID
                               select new
                               {
                                   studentID = a.aID,
                                   studentName = a.aName,
                                   studentlastName = a.aLasname,
                                   teacher = a.teacher,
                                   AGE = DateTime.Now.Year - a.cBirthDay.Value.Year,
                                   Sex = a.aSex,
                                   DateMoveIn = a.cDateMoveIn,
                                   Religion = Common.geTReligion(religion, (a.cReligion == null || a.cReligion == "") ? "999" : a.cReligion),
                                   Race = Common.geTRace(race, (a.cRace == null || a.cRace == "") ? "000" : a.cRace),
                                   Children = a.cChildren,
                                   ProvinceId = a.cProvinceID,
                                   ProvinceName = a.cProvinceNAME,
                                   AmphurId = a.cAmphurID,
                                   AmphurName = a.cAmphurNAME,
                                   DistrictId = a.cDistrictID,
                                   DistrictName = a.cDistrictNAME,

                                   GroupClassId = a.groupclassid,
                                   GroupClassname = a.groupsupername,
                                   GroupClassvalue = a.groupsortvalue,
                                   ClassId = a.classid,
                                   ClassName = a.classname,
                                   RoomId = a.roomid,
                                   RoomName = a.roomname,

                                   FamilyJob = a.cFamilyJob == "" ? "ไม่ระบุข้อมูล" : a.cFamilyJob,
                                   FamilyIncome = a.cFamilyIncome,
                                   FamilyEducation = a.cFamilyEducation,
                                   FamilyReligion = Common.geTReligion(religion, a.cFamilyReligion),
                                   FamilyStatus = a.cFamilyStatus,

                                   FatherIncome = a.cFatherIncome == null ? 0 : (a.cFatherIncome * 12),
                                   FatherEducation = a.cFatherEducation == null ? 0 : a.cFatherEducation,
                                   FatherReligion = Common.geTReligion(religion, (a.cFatherReligion == null || a.cFatherReligion == "") ? "999" : a.cFatherReligion)
                               }).ToList();

                if (search.sOrt_tYpe == "1")
                {
                    var Level0s = (from a in mEmList
                                   group a by new { a.GroupClassvalue, a.ClassId, a.ClassName } into gb0
                                   //orderby new { gb0.Key.ClassName }
                                   select new Level0
                                   {
                                       GroupsortValue = gb0.Max(i => i.GroupClassvalue),
                                       ClassID = gb0.Key.ClassId,
                                       ClassName = gb0.Key.ClassName,
                                       Level1s = (from a1 in gb0
                                                  where a1.ClassId == gb0.Key.ClassId
                                                  group a1 by new { a1.AGE } into gb1
                                                  orderby gb1.Key.AGE
                                                  select new Level1
                                                  {
                                                      StudentAge = gb1.Key.AGE,
                                                      male = gb1.Where(w => w.AGE == gb1.Key.AGE).Count(c => c.Sex == "0"),
                                                      female = gb1.Where(w => w.AGE == gb1.Key.AGE).Count(c => c.Sex == "1")
                                                  }).ToList()
                                   })
                                   .OrderBy(o => o.GroupsortValue)
                                   .ThenBy(o => o.ClassName)
                                   .ToList();

                    string HeaderText = "รายงานสถิติแสดงจำนวนนักเรียนชาย-หญิง (แยกตามอายุ)";

                    return new view
                    {
                        Level0s = Level0s,
                        HeaderText = HeaderText
                    };

                }
                else if (search.sOrt_tYpe == "2")
                {
                    var religions = (from a in mEmList
                                     group a by new { a.Religion } into gb1
                                     orderby gb1.Key.Religion
                                     select new ReligionLevel
                                     {
                                         Religion = gb1.Key.Religion,
                                         male = gb1.Where(w => w.Religion == gb1.Key.Religion).Count(c => c.Sex == "0"),
                                         female = gb1.Where(w => w.Religion == gb1.Key.Religion).Count(c => c.Sex == "1")
                                     }).ToList();

                    string HeaderText = "รายงานสถิติแสดงจำนวนนักเรียนชาย-หญิง (แยกตามศาสนา)";

                    return new view
                    {
                        religions = religions,
                        HeaderText = HeaderText
                    };

                }
                else if (search.sOrt_tYpe == "3")
                {
                    var Level0s = (from a in mEmList
                                   group a by new { a.ClassId, a.ClassName } into gb0
                                   //orderby gb0.Key.ClassId
                                   select new Level0
                                   {
                                       GroupsortValue = gb0.Max(i => i.GroupClassvalue),
                                       ClassID = gb0.Key.ClassId,
                                       ClassName = gb0.Key.ClassName,
                                       Level1s = (from a in gb0
                                                  where gb0.Key.ClassId == a.ClassId
                                                  group a by new { a.RoomId, a.RoomName } into gb1
                                                  orderby gb1.Key.RoomName.Length, gb1.Key.RoomName
                                                  select new Level1
                                                  {
                                                      RoomName = gb0.Key.ClassName + "/" + gb1.Key.RoomName,
                                                      cDw = gb1.Count(c => c.FatherIncome >= 1 && c.FatherIncome < 150000),
                                                      cBtw = gb1.Count(c => c.FatherIncome >= 150000 && c.FatherIncome <= 300000),
                                                      cUp = gb1.Count(c => c.FatherIncome > 300000),
                                                      None = gb1.Count(c => c.FatherIncome == 0),
                                                      sStuAmount = gb1.Select(s => s.studentID).Count()
                                                  }).ToList()
                                   })
                                   .OrderBy(o => o.GroupsortValue)
                                   .ThenBy(o => o.ClassName)
                                   .ToList();

                    var HeaderText = "รายงานนักเรียนจำแนกตามรายได้บิดา";

                    return new view
                    {
                        Level0s = Level0s,
                        HeaderText = HeaderText
                    };
                }
                else if (search.sOrt_tYpe == "4")
                {
                    var Level0s = (from a in mEmList
                                   group a by new { a.ClassId, a.ClassName } into gb0
                                   //orderby gb0.Key.ClassId
                                   select new Level0
                                   {
                                       GroupsortValue = gb0.Max(i => i.GroupClassvalue),
                                       ClassID = gb0.Key.ClassId,
                                       ClassName = gb0.Key.ClassName,
                                       Level1s = (from a in gb0
                                                  where gb0.Key.ClassId == a.ClassId
                                                  group a by new { a.RoomId, a.RoomName } into gb1
                                                  orderby gb1.Key.RoomName.Length, gb1.Key.RoomName
                                                  select new Level1
                                                  {
                                                      RoomName = gb0.Key.ClassName + "/" + gb1.Key.RoomName,
                                                      sStuAmount = gb1.Select(s => s.studentID).Count(),
                                                      None = gb1.Count(c => c.FatherEducation == 0),
                                                      Kindergarten = gb1.Count(c => c.FatherEducation == 1),
                                                      Primary = gb1.Count(c => c.FatherEducation == 2),
                                                      HighSchool = gb1.Count(c => c.FatherEducation == 3),
                                                      Bachelor = gb1.Count(c => c.FatherEducation == 4),
                                                      Master = gb1.Count(c => c.FatherEducation == 5),
                                                      Doctor = gb1.Count(c => c.FatherEducation == 6)
                                                  }).ToList()
                                   }).OrderBy(o => o.GroupsortValue)
                                   .ThenBy(o => o.ClassName)
                                   .ToList();

                    var HeaderText = "รายงานนักเรียนจำแนกตามการศึกษาบิดา";

                    return new view
                    {
                        Level0s = Level0s,
                        HeaderText = HeaderText
                    };
                }
                else if (search.sOrt_tYpe == "5")
                {
                    string HeaderText = "รายงานนักเรียน แยกตามศาสนาบิดา";
                    var Level0s = (from a0 in mEmList
                                   group a0 by new { a0.GroupClassId, a0.GroupClassname, a0.GroupClassvalue } into gb0
                                   orderby gb0.Key.GroupClassvalue
                                   select new Level0
                                   {
                                       GroupClassID = gb0.Key.GroupClassId,
                                       GroupClassName = gb0.Key.GroupClassname,
                                       GroupsortValue = gb0.Max(i => i.GroupClassvalue),
                                       Level1s = (from a1 in gb0
                                                  where a1.GroupClassId == gb0.Key.GroupClassId
                                                  group a1 by new { a1.FatherReligion } into gb1
                                                  orderby gb1.Key.FatherReligion
                                                  select new Level1
                                                  {
                                                      Religion = gb1.Key.FatherReligion,
                                                      male = gb1.Where(w => w.FatherReligion == gb1.Key.FatherReligion).Count(c => c.Sex == "0"),
                                                      female = gb1.Where(w => w.FatherReligion == gb1.Key.FatherReligion).Count(c => c.Sex == "1")
                                                  }).ToList()
                                   }).ToList();
                    return new view
                    {
                        Level0s = Level0s,
                        HeaderText = HeaderText
                    };
                }
                else if (search.sOrt_tYpe == "6")
                {
                    var province = (from a in mEmList
                                    group a by new { a.ProvinceId, a.ProvinceName } into gb0
                                    select new ProvinceLevel
                                    {
                                        ProvinceId = gb0.Key.ProvinceId,
                                        ProvinceName = gb0.Key.ProvinceName,
                                        male = gb0.Where(w => w.ProvinceId == gb0.Key.ProvinceId).Count(c => c.Sex == "0"),
                                        female = gb0.Where(w => w.ProvinceId == gb0.Key.ProvinceId).Count(c => c.Sex == "1"),
                                        sStuAmount = gb0.Select(s => s.studentID).Count()
                                    }).OrderBy(o => o.ProvinceId, new SemiNumericComparer()).ToList();

                    string HeaderText = "รายงานนักเรียนจำแนกตามจังหวัด";

                    return new view
                    {
                        province = province,
                        HeaderText = HeaderText
                    };
                }
                else if (search.sOrt_tYpe == "7")
                {
                    var province = (from a in mEmList
                                    group a by new { a.ProvinceId, a.ProvinceName } into gb0
                                    select new ProvinceLevel
                                    {
                                        ProvinceId = gb0.Key.ProvinceId,
                                        ProvinceName = gb0.Key.ProvinceName,
                                        amphur = (from a in gb0
                                                  where a.ProvinceId == gb0.Key.ProvinceId
                                                  group a by new { a.AmphurId, a.AmphurName } into gb1
                                                  select new AmphurLevel
                                                  {
                                                      AmphurId = gb1.Key.AmphurId,
                                                      AmphurName = gb1.Key.AmphurName,
                                                      male = gb1.Where(w => w.AmphurId == gb1.Key.AmphurId).Count(c => c.Sex == "0"),
                                                      female = gb1.Where(w => w.AmphurId == gb1.Key.AmphurId).Count(c => c.Sex == "1"),
                                                      sStuAmount = gb1.Select(s => s.studentID).Count()
                                                  }).OrderBy(o => o.AmphurId, new SemiNumericComparer()).ToList()
                                    }).OrderBy(o => o.ProvinceId, new SemiNumericComparer()).ToList();

                    string HeaderText = "รายงานนักเรียนจำแนกตามอำเภอ";


                    return new view
                    {
                        province = province,
                        HeaderText = HeaderText
                    };
                }
                else if (search.sOrt_tYpe == "8")
                {
                    var province = (from a in mEmList
                                    group a by new { a.ProvinceId, a.ProvinceName } into gb0
                                    select new ProvinceLevel
                                    {
                                        ProvinceId = gb0.Key.ProvinceId,
                                        ProvinceName = gb0.Key.ProvinceName,
                                        amphur = (from a in gb0
                                                  where a.ProvinceId == gb0.Key.ProvinceId
                                                  group a by new { a.AmphurId, a.AmphurName } into gb1
                                                  select new AmphurLevel
                                                  {
                                                      AmphurId = gb1.Key.AmphurId,
                                                      AmphurName = gb1.Key.AmphurName,
                                                      district = (from a in gb1
                                                                  where a.AmphurId == gb1.Key.AmphurId
                                                                  group a by new { a.DistrictId, a.DistrictName } into gb2
                                                                  select new DistrictLevel
                                                                  {
                                                                      DistrictId = gb2.Key.DistrictId,
                                                                      DistrictName = gb2.Key.DistrictName,
                                                                      male = gb2.Where(w => w.DistrictId == gb2.Key.DistrictId).Count(c => c.Sex == "0"),
                                                                      female = gb2.Where(w => w.DistrictId == gb2.Key.DistrictId).Count(c => c.Sex == "1"),
                                                                      sStuAmount = gb1.Select(s => s.studentID).Count()
                                                                  }).OrderBy(o => o.DistrictId, new SemiNumericComparer()).ToList()
                                                  }).OrderBy(o => o.AmphurId, new SemiNumericComparer()).ToList()
                                    }).OrderBy(o => o.ProvinceId, new SemiNumericComparer()).ToList();

                    string HeaderText = "รายงานนักเรียนจำแนกตามตำบล";

                    return new view
                    {
                        province = province,
                        HeaderText = HeaderText
                    };
                }
                else if (search.sOrt_tYpe == "9")
                {
                    var Level0s = (from a in mEmList
                                   group a by new { a.Race } into gb0
                                   orderby gb0.Key.Race, gb0.Key.Race.Length
                                   select new Level0
                                   {
                                       Race = gb0.Key.Race,
                                       male = gb0.Where(w => w.Race == gb0.Key.Race).Count(c => c.Sex == "0"),
                                       female = gb0.Where(w => w.Race == gb0.Key.Race).Count(c => c.Sex == "1"),
                                       sStuAmount = gb0.Select(s => s.studentID).Count(),
                                   }).ToList();

                    string HeaderText = "รายงานนักเรียนจำแนกตามเชื้อชาติ";

                    return new view
                    {
                        Level0s = Level0s,
                        HeaderText = HeaderText
                    };
                }
                else if (search.sOrt_tYpe == "10")
                {
                    var arr = search.LevelID?.Split(',').Select(o => int.Parse(o));

                    var Query = (from a in tTuser
                                 join b in db.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on a.sID equals b.sID
                                 join c in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on b.nTermSubLevel2 equals c.nTermSubLevel2
                                 join d in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && arr.Contains(w.nTSubLevel)) on c.nTSubLevel equals d.nTSubLevel
                                 join e in db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on b.nTerm.Trim() equals e.nTerm.Trim()
                                 join f in db.TYears.Where(w => w.SchoolID == userData.CompanyID) on e.nYear equals f.nYear
                                 join h in db.TLevels.Where(w => w.SchoolID == userData.CompanyID) on d.nTLevel equals h.LevelID
                                 //where (!search.tLeveL_Id.HasValue || search.tLeveL_Id == h.nTLevel)
                                 select new
                                 {
                                     h.sortValue,
                                     a.sID,
                                     a.sStudentID,
                                     a.sName,
                                     a.sLastname,
                                     a.sStudentTitle,
                                     b.nTerm,
                                     b.nHistoryId,
                                     c.nTermSubLevel2,
                                     d.nTSubLevel,
                                     d.SubLevel,
                                     d.fullName,
                                     h.LevelID,
                                     h.LevelName
                                 }).ToList().OrderBy(o => o.nHistoryId).GroupBy(g => g.sID)
                                 .Select(s => new { s, Count = s.Count() })
                                 .SelectMany(sm => sm.s.Select(s => s)
                                 .Zip(Enumerable.Range(1, sm.Count), (row, index) =>
                                 new
                                 {
                                     rn = index,
                                     row.sID,
                                     row.sStudentTitle,
                                     row.sName,
                                     row.sLastname,
                                     row.sStudentID,
                                     row.nTSubLevel,
                                     row.SubLevel,
                                     row.nTerm,
                                     row.nHistoryId,
                                     row.LevelID,
                                     row.LevelName,
                                     row.sortValue,
                                 }).Where(w => w.rn == 1)).ToList();
                    int aIndex = 1;
                    var Level0s = (from a in Query
                                       //where (!search.sUbLV_Id.HasValue || search.sUbLV_Id == a.nTSubLevel)
                                       //where a.rn == 1
                                   group a by new { a.nTSubLevel, a.SubLevel } into gb0
                                   // orderby gb0.Key.nTSubLevel
                                   select new Level0
                                   {
                                       GroupsortValue = gb0.Max(i => i.sortValue),
                                       ClassID = gb0.Key.nTSubLevel,
                                       ClassName = gb0.Key.SubLevel,
                                       Level1s = (from a in gb0
                                                  orderby a.sStudentID.Length, a.sStudentID
                                                  select new Level1
                                                  {
                                                      //index = a.rn,
                                                      //Id = a.sID,
                                                      //HistoryId = a.nHistoryId
                                                      Index = aIndex++,
                                                      StudentId = a.sStudentID,
                                                      FullName = Common.geTitelName(q_title, a.sStudentTitle) + " " + a.sName.Trim() + " " + a.sLastname.Trim(),
                                                  }).ToList()
                                   })
                                    .OrderBy(o => o.GroupsortValue)
                                   .ThenBy(o => o.ClassName)
                                   .ToList();

                    string HeaderText = "รายงานนักเรียนตามวันที่เข้าเรียน";

                    return new view
                    {
                        Level0s = Level0s,
                        HeaderText = HeaderText
                    };
                }
                else if (search.sOrt_tYpe == "11")
                {
                    var d1 = mEmList
                        .GroupBy(o => new
                        {
                            o.ClassId,
                            o.ClassName,
                            o.RoomName,
                        })
                        .Select(o => new
                        {
                            ClassId = o.Key.ClassId,
                            ClassName = $"{o.Key.ClassName}",
                            Room = $"{o.Key.ClassName}/{o.Key.RoomName}",
                            //RoomCount = mEmList.Where(i => i.ClassName == o.Key.ClassName).Count(),
                            Male = o.Count(i => i.Sex == "0"),
                            Female = o.Count(i => i.Sex == "1"),
                            NonSex = o.Count(i => i.Sex == null),
                            All = o.Count(),
                            GroupClassvalue = o.Min(i => i.GroupClassvalue),
                        })
                        .GroupBy(o => new
                        {
                            o.ClassName
                        })
                        .Select(o => new
                        {
                            ClassId = o.Min(i => i.ClassId),
                            o.Key.ClassName,
                            RoomCount = o.Count(),
                            Male = o.Sum(i => i.Male),
                            Female = o.Sum(i => i.Female),
                            NonSex = o.Sum(i => i.NonSex),
                            All = o.Sum(i => i.Male + i.Female + i.NonSex),
                            GroupClassvalue = o.Max(i => i.GroupClassvalue),
                            Type = 0,
                        })
                        .ToList();

                    var d2 = d1
                        .GroupBy(o => new
                        {
                            o.GroupClassvalue,
                        })
                        .Select(o => new
                        {
                            ClassId = o.Max(i => i.ClassId) + 1,
                            ClassName = $"รวม",
                            RoomCount = d1.Where(i => i.GroupClassvalue == o.Key.GroupClassvalue)
                            .Sum(i => i.RoomCount),
                            Male = o.Sum(i => i.Male),
                            Female = o.Sum(i => i.Female),
                            NonSex = o.Sum(i => i.NonSex),
                            All = o.Sum(i => i.Male + i.Female + i.NonSex),
                            GroupClassvalue = o.Key.GroupClassvalue + 10,
                            Type = 1
                        })
                        .ToList();

                    var d3 = d2
                       .GroupBy(o => new
                       {
                           o.ClassName,
                       })
                       .Select(o => new
                       {
                           ClassId = o.Max(i => i.ClassId) + 1,
                           ClassName = $"รวมทั้งหมด",
                           RoomCount = d2.Sum(i => i.RoomCount),
                           Male = o.Sum(i => i.Male),
                           Female = o.Sum(i => i.Female),
                           NonSex = o.Sum(i => i.NonSex),
                           All = o.Sum(i => i.Male + i.Female + i.NonSex),
                           GroupClassvalue = o.Max(i => i.GroupClassvalue) + 100,
                           Type = 2
                       })
                       .ToList();

                    var d = d1.Concat(d2).Concat(d3)
                        .ToList()
                        .OrderBy(o => o.GroupClassvalue)
                        .ThenBy(o => o.ClassName)
                        .ThenBy(o => o.Type);


                    return new
                    {
                        data = d,
                        HeaderText = "รายงานจำนวนนักเรียน"
                    };
                }
                else if (search.sOrt_tYpe == "12")
                {
                    var d1 = mEmList
                        .GroupBy(o => new
                        {
                            o.ClassId,
                            o.ClassName,
                            o.RoomId,
                            o.RoomName,
                            //o.teacher
                        })
                       .OrderBy(o => o.Key.ClassId).ThenBy(o => o.Key.RoomId)
                       .Select(o => new
                       {
                           ClassId = o.Key.ClassId + 0.0,
                           SortKey = Regex.Replace($"{o.Key.ClassName}/{o.Key.RoomName}", "[^0-9]", ""),
                           SortKey2 = Regex.Replace($"{o.Key.ClassName}/{o.Key.RoomName}", "[^0-9]", "").Length,
                           ClassName = o.Key.ClassName,
                           RoomName = $"{o.Key.ClassName}/{o.Key.RoomName}",
                           Male = o.Count(i => i.Sex == "0"),
                           Female = o.Count(i => i.Sex == "1"),
                           NonSex = o.Count(i => i.Sex == null),
                           All = o.Count(),
                           GroupClassvalue = o.Max(i => i.GroupClassvalue),
                           Teacher = o.Max(i => i.teacher),
                           Type = 0,
                       })
                        .ToList();
                    //0989474952
                    var d2 = d1
                        .GroupBy(o => new
                        {
                            o.ClassName,
                        })
                        .Select(o => new
                        {
                            ClassId = o.Max(i => i.ClassId) + 0.5,
                            SortKey = $"{o.Max(i => i.SortKey)}",
                            SortKey2 = 99,
                            ClassName = $"รวม",
                            RoomName = $"รวม",
                            Male = o.Sum(i => i.Male),
                            Female = o.Sum(i => i.Female),
                            NonSex = o.Sum(i => i.NonSex),
                            All = o.Sum(i => i.Male + i.Female + i.NonSex),
                            GroupClassvalue = o.Max(i => i.GroupClassvalue),
                            Teacher = "",
                            Type = 1,
                        })
                        .ToList();

                    var d3 = d2
                    .GroupBy(o => new
                    {
                        o.ClassName,
                    })
                    .Select(o => new
                    {
                        ClassId = o.Max(i => i.ClassId) + 1.0,
                        SortKey = $"รวมทั้งหมด",
                        SortKey2 = 999,
                        ClassName = $"รวมทั้งหมด",
                        RoomName = $"รวมทั้งหมด",
                        Male = o.Sum(i => i.Male),
                        Female = o.Sum(i => i.Female),
                        NonSex = o.Sum(i => i.NonSex),
                        All = o.Sum(i => i.Male + i.Female + i.NonSex),
                        GroupClassvalue = o.Max(i => i.GroupClassvalue) + 100,
                        Teacher = "",
                        Type = 2,
                    })
                    .ToList();

                    return new
                    {
                        data = d1.Concat(d2).Concat(d3)
                        .OrderBy(o => o.GroupClassvalue)
                        .ThenBy(o => o.ClassId)
                        .ThenBy(o => o.SortKey2)
                        .ThenBy(o => o.SortKey)
                        .ThenBy(o => o.Type),
                        HeaderText = "รายงานจำนวนนักเรียน (แสดงชั้นห้อง)"
                    };

                }
                else if (search.sOrt_tYpe == "13")
                {
                    var q1 = from a in db.TCurriculums.Where(o => o.SchoolId == userData.CompanyID && o.IsActive == true && o.cDel == false && o.nYear == search.yEar_Id)
                             from b in db.TPlans.Where(o => o.SchoolID == userData.CompanyID && o.IsActive == true && o.cDel == false && o.CurriculumId == a.CurriculumId)
                             from c in db.TPlanTermSubLevel2.Where(o => o.SchoolID == userData.CompanyID && o.IsActive == true && o.cDel == false && o.PlanId == b.PlanId)
                             select new
                             {
                                 b.PlanId,
                                 b.PlanName,
                                 b.nTSubLevel,
                                 c.nTermSubLevel2,
                             };
                    var plan = q1.Distinct().ToList();

                    var d1 = (from a in mEmList
                              from b in plan.Where(o => o.nTermSubLevel2 == a.RoomId)
                              select new
                              {
                                  a.ClassId,
                                  a.ClassName,
                                  a.RoomId,
                                  a.RoomName,
                                  b.PlanId,
                                  b.PlanName,
                                  a.Sex,
                                  a.GroupClassvalue,
                              })
                               .GroupBy(o => new
                               {
                                   o.ClassId,
                                   o.ClassName,
                                   o.RoomId,
                                   o.RoomName,
                                   o.PlanId,
                                   o.PlanName,
                               })
                                .Select(o => new
                                {
                                    ClassId = o.Key.ClassId + 0.0,
                                    ClassName = $"{o.Key.ClassName}",
                                    Room = $"{o.Key.ClassName}/{o.Key.RoomName}",
                                    Plan = o.Key.PlanName,
                                    Male = o.Count(i => i.Sex == "0"),
                                    Female = o.Count(i => i.Sex == "1"),
                                    All = o.Count(),
                                    GroupClassvalue = o.Min(i => i.GroupClassvalue),
                                    Type = 0,
                                    SortKey = Regex.Replace($"{o.Key.ClassName}/{o.Key.RoomName}", "[^0-9]", ""),
                                    SortKey2 = Regex.Replace($"{o.Key.ClassName}/{o.Key.RoomName}", "[^0-9]", "").Length,
                                })
                                .ToList();

                    var d2 = d1
                   .GroupBy(o => new
                   {
                       o.GroupClassvalue,
                   })
                   .Select(o => new
                   {
                       ClassId = o.Max(i => i.ClassId) + 0.5,
                       ClassName = $"",
                       Room = $"",
                       Plan = $"รวม",
                       Male = o.Sum(i => i.Male),
                       Female = o.Sum(i => i.Female),
                       All = o.Sum(i => i.Male + i.Female),
                       GroupClassvalue = o.Max(i => i.GroupClassvalue),
                       Type = 1,
                       SortKey = $"{o.Max(i => i.SortKey)}",
                       SortKey2 = 99,
                   })
                   .ToList();

                    var d3 = d2
                       .GroupBy(o => new
                       {
                           o.ClassName,
                       })
                       .Select(o => new
                       {
                           ClassId = o.Max(i => i.ClassId) + 1.0,
                           ClassName = $"",
                           Room = $"",
                           Plan = $"รวมทั้งหมด",
                           Male = o.Sum(i => i.Male),
                           Female = o.Sum(i => i.Female),
                           All = o.Sum(i => i.Male + i.Female),
                           GroupClassvalue = o.Max(i => i.GroupClassvalue) + 100,
                           Type = 2,
                           SortKey = $"รวมทั้งหมด",
                           SortKey2 = 999,
                       })
                       .ToList();

                    return new
                    {
                        data = d1.Concat(d2).Concat(d3)
                        .ToList()
                        .OrderBy(o => o.GroupClassvalue)
                        .ThenBy(o => o.ClassId)
                        .ThenBy(o => o.SortKey2)
                        .ThenBy(o => o.SortKey)
                        .ThenBy(o => o.Type),
                        HeaderText = "รายงานจำนวนนักเรียนแยกแผนการเรียน"
                    };

                }
                else
                {
                    //addCondition
                    return new view
                    {

                    };

                }
            }

        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void export10_data(Search search)
        {
            var report = report_AmountStudent(search) as view;
            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("รายงาน");
                var worksheet = excel.Workbook.Worksheets["รายงาน"];
                string entities = HttpContext.Current.Session["sEntities"].ToString();

                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                var tToday = DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th"));
                var tNow = DateTime.Now.ToString("HH:mm:ss");

                SetHeader(worksheet, "A1:D1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A2:D2", true, report.HeaderText, 12, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A3:D3", true, "พิมพ์วันที่ :" + " " + tToday, null, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, "A4:D4", true, "เวลา :" + " " + tNow, null, ExcelHorizontalAlignment.Right);

                SetHeader(worksheet, "A5:D5", true, "", null, ExcelHorizontalAlignment.Center);

                string[] strHeader = { "ระดับชั้น", "ลำดับ", "รหัส", "ชื่อ-นามสกุล" };
                int Columuns = 1;
                foreach (string str in strHeader)
                {
                    SetTableHeader(worksheet.Cells[6, Columuns++], false, str, ExcelHorizontalAlignment.Center);
                }

                int Rows = 7;
                int Index = 1;

                foreach (var Level0 in report.Level0s)
                {
                    if (Level0.Level1s.Count() == 0) continue;
                    int rowsEnd = Rows + (Level0.Level1s.Count() - 1);
                    SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], true, Level0.ClassName, ExcelHorizontalAlignment.Center);
                    //SetTableRows(worksheet.Cells[Rows, 2, rowsEnd, 2], true, string.Format("{0:#,0}", Index++), ExcelHorizontalAlignment.Center);
                    foreach (var Level1 in Level0.Level1s)
                    {
                        SetTableRows(worksheet.Cells[Rows, 2], false, string.Format("{0:#,0}", Index++), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 3], false, Level1.StudentId, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 4], false, Level1.FullName, ExcelHorizontalAlignment.Center);
                        Rows += 1;
                    }
                }

                worksheet.Cells.AutoFitColumns();
                worksheet.Column(1).Width = 15;
                worksheet.Column(2).Width = 15;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 35;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                HttpContext.Current.Response.ContentType = "application/text";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

        }
        private static void SetHeader(ExcelWorksheet excelWorksheet, string Cells, bool Merge, string strValues, int? fontSize, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = excelWorksheet.Cells[Cells])
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Font.Size = fontSize ?? 10;
            }
        }
        private static void SetTableHeader(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                rng.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
            }
        }
        private static void SetTableRows(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            }
        }
        private static void SetTableFooter(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(0, 217, 217, 217);
            }
        }



        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void pdf10_data(Search search)
        {
            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken.userData();
                if (token.CheckToken(HttpContext.Current))
                {
                    userData = token.getTokenValues(HttpContext.Current);
                }

                string entities = HttpContext.Current.Session["sEntities"].ToString();

                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                // Create PDF document
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Example.pdf");

                Document pdfDoc = new Document(PageSize.A4, 10, 5, 20, 50);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.AddAuthor("Me");

                pdfDoc.Open();

                var report = report_AmountStudent(search) as view;


                //pdfDoc.Add(GetHeader(tCompany.sCompany));


                foreach (var Level0 in report.Level0s)
                {

                }


                pdfDoc.Close();

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
                                                                           //HttpContext.Current.Response.End();
            }
        }

        //private PdfPTable GetHeader(string school_name, string report_name, string name, string lastname, string code)
        //{
        //    //var report = report_AmountStudent(search) as view;
        //    string entities = HttpContext.Current.Session["sEntities"].ToString();
        //    JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read);
        //    var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

        //    var tToday = DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th"));
        //    var tNow = DateTime.Now.ToString("HH:mm:ss");

        //    PdfPTable headerTable = new PdfPTable(1);
        //    headerTable.WidthPercentage = 100;
        //    headerTable.AddCell(AddCell(new PdfPCell(new Phrase(tCompany.sCompany, h1)), Element.ALIGN_CENTER));
        //    //headerTable.AddCell(AddCell(new PdfPCell(new Phrase(report.HeaderText, bold)), Element.ALIGN_CENTER));
        //    headerTable.AddCell(AddCell(new PdfPCell(new Phrase(("พิมวันนที่ :" + " " + tToday + "เวลา : " + " " + tNow), smallBold)), Element.ALIGN_RIGHT));

        //    return headerTable;
        //}

        private PdfPCell AddCell(PdfPCell pdfPCell, int horizontalAlignment)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.VerticalAlignment = Element.ALIGN_BOTTOM;
            TableCell.Border = Rectangle.NO_BORDER;
            return TableCell;
        }



        private static BaseFont bf_bold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew_bold-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private Font h1 = new Font(bf_bold, 14);
        private Font bold = new Font(bf_bold, 12);
        private Font smallBold = new Font(bf_bold, 10);

        // Normal
        private static BaseFont bf_normal = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private Font normal = new Font(bf_normal, 10);
        private Font smallNormal = new Font(bf_normal, 8);
        private Font Header_smallNormal = new Font(bf_normal, 8, 0 /*,new BaseColor(255, 255, 255)*/);




        //------------------------------------------------------pdf---------------------------------------------------

        private class view
        {
            public string HeaderText { get; set; }

            public List<Level0> Level0s { get; set; }
            public List<ProvinceLevel> province { get; set; }
            public List<ReligionLevel> religions { get; set; }
        }
        private class Level0
        {
            public List<Level1> Level1s { get; set; }
            public int? GroupClassID { get; set; }
            public string GroupClassName { get; set; }
            public int? GroupsortValue { get; set; }

            //-----
            public int? ClassID { get; set; }
            public string ClassName { get; set; }
            //-----
            public string Race { get; set; }
            public int male { get; set; }
            public int female { get; set; }
            public int sStuAmount { get; set; }
        }
        private class Level1
        {
            public List<Level2> Level2s { get; set; }
            public int ClassID { get; set; }
            public string ClassName { get; set; }
            public string RoomName { get; set; }
            public string Religion { get; set; }
            public int male { get; set; }
            public int female { get; set; }
            public int Index { get; set; }
            public string ProvinceId { get; set; }
            public string ProvinceName { get; set; }
            public string FullName { get; set; }
            public string StudentId { get; set; }
            public int StudentAge { get; set; }
            public int None { get; set; } /*ไม่ระบุ*/
            //-----เงินเดือนพ่อ
            public int cDw { get; set; }
            public int cBtw { get; set; }
            public int cUp { get; set; }
            public int sStuAmount { get; set; }
            //-----การศึกษาพ่อ
            public int Kindergarten { get; set; }
            public int Primary { get; set; }
            public int HighSchool { get; set; }
            public int Bachelor { get; set; }
            public int Master { get; set; }
            public int Doctor { get; set; }
        }
        private class Level2
        {
            public int RoomID { get; set; }
            public string RoomName { get; set; }
            public string ClassFullName { get; set; }
            public int male { get; set; }
            public int female { get; set; }
            public int StudentAge { get; set; }
            public int sStuAmount { get; set; }
            public string AmphurId { get; set; }
            public string AmphurName { get; set; }
        }
        public class Search
        {
            public int yEar_Id { get; set; }
            public string tErm_Id { get; set; }
            //public int? sUbLV_Id { get; set; }
            public string sOrt_tYpe { get; set; }

            public int? tLeveL_Id { get; set; }
            public string LevelID { get; set; }
            //public DateTime? dStart { get; set; }
        }


        public class ProvinceLevel
        {
            public List<AmphurLevel> amphur { get; set; }
            public string ProvinceId { get; set; }
            public string ProvinceName { get; set; }
            public int male { get; set; }
            public int female { get; set; }
            public int sStuAmount { get; set; }
        }
        public class AmphurLevel
        {
            public List<DistrictLevel> district { get; set; }
            public string AmphurId { get; set; }
            public string AmphurName { get; set; }
            public int male { get; set; }
            public int female { get; set; }
            public int sStuAmount { get; set; }
        }
        public class DistrictLevel
        {
            public string DistrictId { get; set; }
            public string DistrictName { get; set; }
            public int male { get; set; }
            public int female { get; set; }
            public int sStuAmount { get; set; }
        }


        public class ReligionLevel
        {
            public string Religion { get; set; }
            public int male { get; set; }
            public int female { get; set; }
        }


        public class maxjana
        {

        }

        private class StudentViewModel
        {
            public int sID { get; set; }
            public int? nTSubLevel { get; set; }
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public int? nTermSubLevel2 { get; set; }
            public string nTerm { get; set; }

            public string sName { get; set; }
            public string sLastname { get; set; }
            public DateTime? moveInDate { get; set; }
            public DateTime? MoveOutDate { get; set; }

            public DateTime? dBirth { get; set; }
            public string sStudentReligion { get; set; }
            public string sStudentRace { get; set; }
            public int? nSonNumber { get; set; }
            public string cSex { get; set; }

            public int? ProvinceID { get; set; }
            public string ProvinceName { get; set; }
            public int? AmphurID { get; set; }
            public string AmphurName { get; set; }
            public int? TumbonID { get; set; }
            public string TumbonName { get; set; }

            public int? LevelID { get; set; }
            public string LevelName { get; set; }
            public int? sortValue { get; set; }

            public string Teacher { get; set; }
        }
    }
}