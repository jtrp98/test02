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
    public partial class studentCardIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
                DataTable dtYear = fcommon.LinqToDataTable(_db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.numberYear).ToList());

                //fcommon.ListDataTableToDropDownList(dtYear, ddlyear, "เลือกปีการศึกษา", "nYear", "numberYear");
                //ddlyear.SelectedValue = DateTime.Now.Year.ToString();
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
        public static object return_data(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    //search.tErm_Id = string.Format("TS{0:0000000}", int.Parse(search.tErm_Id));
                    var q_student = (from a in db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID)

                                     where search.sUbLV2_Id == a.nTermSubLevel2 && (a.cDel ?? "0") != "1"
                                     && a.nTerm.Trim() == search.tErm_Id
                                     && (!search.student_id.HasValue || search.student_id == a.sID)

                                     orderby new { a.nStudentNumber, a.sStudentID }
                                     select new
                                     {
                                         a.sID,
                                         a.sStudentID,
                                         a.sName,
                                         a.sLastname,
                                         a.nStudentNumber,
                                         a.SubLevel,
                                         a.nTSubLevel2,
                                         a.nTerm
                                     }).ToList();

                    var q_dtail = (from a in q_student

                                   select new StudentlistData
                                   {
                                       s_ID = a.sID,
                                       StudentID = a.sStudentID == null ? " " : a.sStudentID,
                                       StudentFullName = a.sName + " " + a.sLastname,
                                       StudentNumber = a.nStudentNumber,
                                       StudentClass = a.SubLevel + "/" + a.nTSubLevel2,
                                       StudentTerm = a.nTerm
                                   }).AsQueryable().ToList();

                    return new views01 { data = q_dtail };

                }
            }
        }

        public class views01
        {
            public List<StudentlistData> data { get; set; }
        }

        public class Search
        {
            public int? yEar_Id { get; set; } /*ปี*/
            public string tErm_Id { get; set; } /*เทอม*/
            public string sUbLV_Id { get; set; } /*ระดับการศึกษา*/
            public int? sUbLV2_Id { get; set; } /*ห้อง*/
            public int? student_id { get; set; }
        }

        public class StudentlistData
        {
            public int? s_ID { get; set; }
            public string StudentID { get; set; }
            public string StudentFullName { get; set; }
            public int? StudentNumber { get; set; }
            public string StudentClass { get; set; }
            public string StudentTerm { get; set; }
        }









    }




}