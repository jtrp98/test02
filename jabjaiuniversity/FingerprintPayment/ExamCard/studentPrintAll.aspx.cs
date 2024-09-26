using FluentDateTime;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.ExamCard
{
    public partial class studentPrintAll : System.Web.UI.Page
    {
        public static string EmpDirectorName = "";
        public static string EmpDirectorSignature = "";
        public static string TextHead1 = "";
        public static string TextHead2 = "";
        public static string sTudentID = "";
        public static string sTFullname = "";
        public static string sTClass = "";
        public static string sTMajor = "";
        public static string imageSchool = "";
        public static int? sTYear = 0;
        public static string sTTerm = "";

        public static string Class = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
               
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var schooldata = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).FirstOrDefault();

                imageSchool = schooldata.sImage;

                if (schooldata.nSchoolHeadid != null)
                {
                    var getEMP = _dbMaster.TUsers.Where(w => w.nCompany == schooldata.nCompany).Where(w => w.nSystemID == schooldata.nSchoolHeadid).Where(w => w.cType == "1").FirstOrDefault();
                    var ImageUserSignature = getEMP.UserSignature == null ? "../images/emptySignature.jpg" : getEMP.UserSignature;
                    EmpDirectorName = getEMP.sName + " " + getEMP.sLastname;
                    EmpDirectorSignature = ImageUserSignature;
                }


                int sID = 0;
                Int32.TryParse(Request.QueryString["sID"], out sID);

                var Xam = 0; /*ประเภทการสอบ*/
                Int32.TryParse(Request.QueryString["exam"], out Xam);

                var yEar = 0;
                Int32.TryParse(Request.QueryString["yEar"], out yEar);
                //var Styear = _db.TYears.Where(w => w.nYear == yEar).FirstOrDefault();

                //var tErm = 0;
                //Int32.TryParse(Request.QueryString["tErm"], out tErm);
                //var rEquestTErm = string.Format("TS{0:0000000}", tErm);
                //var tTErm = _db.TB_StudentViews.Where(w => w.nTerm.Trim() == rEquestTErm).FirstOrDefault();
                string rEquestTErm = (string)Request.QueryString["tErm"];

                /*dd/mm/yy*/

                var QueryString_dStart = Request.QueryString["dStart"];
                string[] sAryStart = QueryString_dStart.Split('/');
                var yEarStartCut = 0;
                Int32.TryParse(sAryStart[2], out yEarStartCut);

                string dStart = QueryString_dStart;//sAryStart[0] + "/" + sAryStart[1] + "/" + (yEarStartCut);


                var QueryString_dEnd = Request.QueryString["dEnd"];
                string[] sAryEnd = QueryString_dEnd.Split('/');
                var yEarEndCut = 0;
                Int32.TryParse(sAryEnd[2], out yEarEndCut);

                string dEnd = QueryString_dEnd;// sAryEnd[0] + "/" + sAryEnd[1] + "/" + (yEarEndCut );

                var headTXT = "";
                if (Xam == 1)
                {
                    headTXT = "กลางภาค";
                }
                else
                {
                    headTXT = "ปลายภาค";
                }

                TextHead1 = headTXT;
                TextHead2 = "ประจำวันที่ " + dStart + " - " + dEnd;

                var q_title = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                var q_student = (from a in _db.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                 join b in _db.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                                 join c in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on b.nTermSubLevel2 equals c.nTermSubLevel2
                                 join d in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on c.nTSubLevel equals d.nTSubLevel
                                 join i in _db.TLevels.Where(w => w.SchoolID == userData.CompanyID) on d.nTLevel equals i.LevelID
                                 join j in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on b.nTerm equals j.nTerm
                                 join k in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on j.nYear equals k.nYear

                                 join f in _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID) on c.nBranchSpecId equals f.BranchSpecId into jcf
                                 from jf in jcf.DefaultIfEmpty()

                                 join g in _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID) on jf.BranchSubjectId equals g.BranchSubjectId into jfg
                                 from jg in jfg.DefaultIfEmpty()

                                 join h in _db.TBranches.Where(w => w.SchoolID == userData.CompanyID) on jg.BranchId equals h.BranchId into jhg
                                 from jh in jhg.DefaultIfEmpty()

                                 where a.sID == sID && b.nTerm == rEquestTErm

                                 select new StudentlistData
                                 {
                                     s_ID = a.sID,
                                     StudentID = a.sStudentID == null ? " " : a.sStudentID,
                                     StudentClass = d.SubLevel + "/" + c.nTSubLevel2,
                                     StudentYear = k.numberYear,
                                     StudentTerm = j.sTerm,
                                     StudentFullName = a.sName + " " + a.sLastname,
                                     StudentTitle = a.sStudentTitle,
                                     StudentMajor = jf.BranchSpecName == null ? " " : jf.BranchSpecName,
                                     Class = d.SubLevel
                                     //StudentMajor = jf.BranchSpecName == null ? "ไม่พบสาขา" : jf.BranchSpecName
                                 }).FirstOrDefault();

                sTudentID = q_student.StudentID;
                sTFullname = geTitelName(q_title, q_student.StudentTitle) + q_student.StudentFullName;
                sTClass = q_student.StudentClass;
                sTMajor = q_student.StudentMajor;
                sTYear = q_student.StudentYear;
                sTTerm = q_student.StudentTerm;
                Class = q_student.Class;

                //tXtMajor.InnerText = q_student.StudentMajor;

                if (Class == "ปวช. 1" || Class == "ปวช. 2" || Class == "ปวช. 3" || Class == "ปวส. 1" || Class == "ปวส. 2")
                {
                    cLassMajor.Attributes.Remove("hidden");
                }

            }
        }

        private static string geTitelName(List<TTitleList> titleLists, string titelId)
        {
            int title_Id = 0;
            int.TryParse(titelId, out title_Id);
            if (title_Id == 0) return titelId;
            else
            {
                var f_title = titleLists.FirstOrDefault(f => f.nTitleid == title_Id);
                return f_title == null ? titelId : f_title.titleDescription;
            }
        }




        public class StudentlistData
        {
            public int? s_ID { get; set; }
            public string StudentID { get; set; }
            public string StudentFullName { get; set; }
            public int? StudentNumber { get; set; }
            public string StudentClass { get; set; }
            public int? StudentYear { get; set; }
            public string StudentTerm { get; set; }
            public string StudentMajor { get; set; }
            public string StudentTitle { get; set; }
            public string Class { get; set; }
        }

        public class SchoollistData
        {
            public int? s_ID { get; set; }
            public string StudentID { get; set; }
            public string StudentFullName { get; set; }
            public int? StudentNumber { get; set; }
            public string StudentClass { get; set; }
        }



    }
}