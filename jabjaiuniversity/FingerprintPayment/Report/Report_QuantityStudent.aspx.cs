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


namespace FingerprintPayment.Report
{
    public partial class Report_QuantityStudent : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                var userData = GetUserData();
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
                }
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object Report_Quantity_ClassRoom(Search search)
        {
            //Type1
            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {
                var userData = GetUserData();

                var tUser = db.Database.SqlQuery<JabjaiEntity.DB.TUser>($"SELECT * FROM TUser WHERE SchoolID = {userData.CompanyID}  AND ISNULL(cDel,'0') = '0' AND ISNULL(nStudentStatus, 0) = 0 ").ToList();

                var StudentList = (from a in tUser
                                   join b in db.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                                   join c in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on b.nTermSubLevel2 equals c.nTermSubLevel2
                                   join d in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on c.nTSubLevel equals d.nTSubLevel
                                   join e in db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on b.nTerm equals e.nTerm
                                   join f in db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on e.nYear equals f.nYear
                                   join h in db.TLevels.Where(w => w.SchoolID == userData.CompanyID) on d.nTLevel equals h.LevelID

                                   where (!search.sUbLV_Id.HasValue || search.sUbLV_Id == d.nTSubLevel)
                                   && b.nTerm.Trim() == search.tErm_Id
                                   && c.nWorkingStatus == 1 && c.nTermSubLevel2Status == "1"
                                   && d.nWorkingStatus == 1
                                   select new
                                   {
                                       studentID = a.sID,
                                       studentName = a.sName,
                                       studentLast = a.sLastname,
                                       studentSex = a.cSex,
                                       roomid = c.nTermSubLevel2,
                                       roomname = c.nTSubLevel2,
                                       classid = d.nTSubLevel,
                                       classname = d.SubLevel,
                                       groupclassID = h.LevelID,
                                       groupsuperName = h.LevelName,
                                       groupsortValue = h.sortValue
                                   }).ToList();

                if (search.sOrt_tYpe == "1")
                {
                    var Level0s = (from a in StudentList
                                   group a by new { a.groupclassID, a.groupsuperName } into gb0
                                   select new Level0
                                   {
                                       GroupClassID = gb0.Key.groupclassID,
                                       GroupClassName = gb0.Key.groupsuperName,
                                       Level1s = (from a in gb0
                                                  group a by new { a.classid, a.classname } into gb1
                                                  orderby gb1.Key.classname
                                                  select new Level1
                                                  {
                                                      ClassID = gb1.Key.classid,
                                                      ClassName = gb1.Key.classname,
                                                      TotalMale = gb1.Count(c => c.studentSex == "0"),
                                                      TotalFemale = gb1.Count(c => c.studentSex == "1"),
                                                      TotalRoom = (from a in gb0
                                                                   where a.classid == gb1.Key.classid
                                                                   group a by new { a.roomid, a.roomname } into gb2
                                                                   select new { key = gb2.Key }).Count()
                                                  }).ToList()
                                   }).ToList();

                    string headerText = "รายงานสถิติแสดงจำนวนนักเรียนชาย-หญิง";

                    return new view
                    {
                        level0s = Level0s,
                        headerText = headerText
                    };

                }
                else
                {
                    var Level0s = (from a in StudentList
                                   group a by new { a.groupclassID, a.groupsuperName, a.groupsortValue } into gb0
                                   orderby gb0.Key.groupsortValue
                                   select new Level0
                                   {
                                       GroupClassID = gb0.Key.groupclassID,
                                       GroupClassName = gb0.Key.groupsuperName,
                                       GroupsortValue = gb0.Key.groupsortValue,
                                       Level1s = (from a1 in gb0
                                                  where a1.groupclassID == gb0.Key.groupclassID
                                                  group a1 by new { a1.classid, a1.classname, a1.groupsortValue } into gb1
                                                  orderby gb1.Key.classid
                                                  select new Level1
                                                  {
                                                      ClassID = gb1.Key.classid,
                                                      ClassName = gb1.Key.classname,
                                                      GroupsortValue = gb1.Key.groupsortValue,
                                                      Level2s = (from a2 in gb1
                                                                 where a2.classid == gb1.Key.classid
                                                                 group a2 by new { a2.roomid, a2.roomname } into gb2
                                                                 select new Level2
                                                                 {
                                                                     RoomID = gb2.Key.roomid,
                                                                     RoomName = gb2.Key.roomname,
                                                                     ClassFullName = gb1.Key.classname + "/" + gb2.Key.roomname,
                                                                     male = gb2.Where(w => w.roomid == gb2.Key.roomid).Count(c => c.studentSex == "0"),
                                                                     female = gb2.Where(w => w.roomid == gb2.Key.roomid).Count(c => c.studentSex == "1"),
                                                                     SumStudent = gb2.Where(w => w.roomid == gb2.Key.roomid).Count()
                                                                 }).OrderBy(o => o.RoomName, new SemiNumericComparer()).ToList()
                                                  }).ToList()
                                   }).ToList();

                    string headerText = "รายงานสถิติแสดงจำนวนนักเรียนชาย-หญิง(แสดงชั้นห้อง)";

                    return new view
                    {
                        level0s = Level0s,
                        headerText = headerText
                    };
                }
            }
        }



        public class view
        {
            public List<Level0> level0s { get; set; }
            public string headerText { get; set; }
        }
        public class Level0
        {
            public List<Level1> Level1s { get; set; }
            public int GroupClassID { get; set; }
            public string GroupClassName { get; set; }
            public int? GroupsortValue { get; set; }
        }
        public class Level1
        {
            public List<Level2> Level2s { get; set; }
            public int ClassID { get; set; }
            public string ClassName { get; set; }
            public int? GroupsortValue { get; set; }

            public int TotalMale { get; set; }
            public int TotalFemale { get; set; }
            public int TotalRoom { get; set; }
        }
        public class Level2
        {
            public int RoomID { get; set; }
            public string RoomName { get; set; }
            public string ClassFullName { get; set; }
            public int male { get; set; }
            public int female { get; set; }
            public int SumStudent { get; set; }
        }
        public class Search
        {
            public string yEar_Id { get; set; }
            public string tErm_Id { get; set; }
            public int? sUbLV_Id { get; set; }
            public string sOrt_tYpe { get; set; }
        }




    }


}