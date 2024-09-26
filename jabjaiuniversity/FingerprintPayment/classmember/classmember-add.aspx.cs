using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class classmember_add : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);
            if (!IsPostBack)
            {
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {

                    string idlv = Request.QueryString["idlv"];
                    string idlv2 = Request.QueryString["idlv2"];
                    string ddlyear = Request.QueryString["year"];
                    string ddlterm = Request.QueryString["term"];
                    int id = 0;
                    Int32.TryParse(Request.QueryString["id"], out id);
                    string sname = Request.QueryString["sname"];
                    string sEntities = Session["sEntities"] + "";

                    if (ddlyear == null)
                        ddlyear = "99999999";
                    int? useryear = Int32.Parse(ddlyear);
                    int? ntermsublv2 = 0;
                    int nyear = 0;
                    string nterm = "";
                    int ntermtable = 0;


                    foreach (var year in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).Where(w => w.numberYear == useryear))
                    {
                        nyear = year.nYear;
                    }

                    foreach (var term in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sTerm == ddlterm && w.nYear == nyear && w.cDel == null))
                    {
                        nterm = term.nTerm;
                    }

                    int idlv2n = Int32.Parse(idlv2);

                    var data = _db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n).FirstOrDefault();

                    if (data != null)
                    {
                        if (data.nTeacherHeadid != null)
                        {
                            var name1 = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == data.nTeacherHeadid).FirstOrDefault();
                            head.Value = name1.sName + " " + name1.sLastname;
                        }

                        if (data.nTeacherAssistOne != null)
                        {
                            var name2 = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == data.nTeacherAssistOne).FirstOrDefault();
                            help1.Value = name2.sName + " " + name2.sLastname;
                        }
                        if (data.nTeacherAssistTwo != null)
                        {
                            var name3 = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == data.nTeacherAssistTwo).FirstOrDefault();
                            help2.Value = name3.sName + " " + name3.sLastname;
                        }
                    }

                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string ddlyear = Request.QueryString["year"];
                string ddlterm = Request.QueryString["term"];
                int id = 0;
                Int32.TryParse(Request.QueryString["id"], out id);
                string sname = Request.QueryString["sname"];
                string sEntities = Session["sEntities"] + "";

                string SQL = "";
                SQL = @"select TB1.*,SubLevel,TB2.nTSubLevel2
                    from TUser AS TB1 left join TTermSubLevel2 AS TB2 on TB1.nTermSubLevel2 = TB2.nTermSubLevel2
                    left join TSubLevel AS TB3 on TB3.nTSubLevel = TB2.nTSubLevel
                    where cDel IS NULL ";

                if (!string.IsNullOrEmpty(idlv2)) SQL += " AND TB1.nTermSubLevel2 = " + idlv2;
                else if (!string.IsNullOrEmpty(idlv)) SQL += " AND TB2.nTSubLevel = " + idlv;
                if (!string.IsNullOrEmpty(ddlyear)) SQL += " AND sName + ' ' + sLastname LIKE '%" + sname + "%'";


                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                //var x = fcommon.Get_Data(_conn, SQL);


                if (ddlyear == null)
                    ddlyear = "99999999";
                int? useryear = Int32.Parse(ddlyear);
                int? ntermsublv2 = 0;
                int nyear = 0;
                string nterm = "";
                int ntermtable = 0;


                foreach (var year in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).Where(w => w.numberYear == useryear))
                {
                    nyear = year.nYear;
                }

                foreach (var term in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sTerm == ddlterm && w.nYear == nyear && w.cDel == null))
                {
                    nterm = term.nTerm;
                }

                int idlv2n = Int32.Parse(idlv2);

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var data = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == id).FirstOrDefault();

                //int countmember = 1;
                //foreach (var a in _db.TClassMembers.ToList())
                //{
                //    countmember = countmember + 1;
                //}

                TClassMember member = new TClassMember();



                if (txtSearch.Text != "" && txtSearch.Text != null)
                {
                    string fullName = txtSearch.Text;
                    var names = fullName.Split(' ');
                    string firstName = names[0];



                    if (names.Length > 1)
                    {
                        string lastName = names[1];
                        var emp1 = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sName == firstName && w.sLastname == lastName).FirstOrDefault();
                        if (emp1 != null)
                            member.nTeacherHeadid = emp1.sEmp;
                    }
                    else
                    {
                        var emp2 = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sName == firstName).FirstOrDefault();
                        if (emp2 != null)
                            member.nTeacherHeadid = emp2.sEmp;
                    }
                }

                if (txtSearch2.Text != "" && txtSearch2.Text != null)
                {
                    string fullName = txtSearch2.Text;
                    var names = fullName.Split(' ');
                    string firstName = names[0];



                    if (names.Length > 1)
                    {
                        string lastName = names[1];
                        var emp1 = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sName == firstName && w.sLastname == lastName).FirstOrDefault();
                        if (emp1 != null)
                            member.nTeacherAssistOne = emp1.sEmp;
                    }
                    else
                    {
                        var emp2 = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sName == firstName).FirstOrDefault();
                        if (emp2 != null)
                            member.nTeacherAssistOne = emp2.sEmp;
                    }
                }

                if (txtSearch3.Text != "" && txtSearch3.Text != null)
                {
                    string fullName = txtSearch3.Text;
                    var names = fullName.Split(' ');
                    string firstName = names[0];

                    if (names.Length > 1)
                    {
                        string lastName = names[1];
                        var emp1 = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sName == firstName && w.sLastname == lastName).FirstOrDefault();
                        if (emp1 != null)
                            member.nTeacherAssistTwo = emp1.sEmp;
                    }
                    else
                    {
                        var emp2 = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sName == firstName).FirstOrDefault();
                        if (emp2 != null)
                            member.nTeacherAssistTwo = emp2.sEmp;
                    }
                }

                //member.nClassMemberid = countmember;            
                member.nTermSubLevel2 = idlv2n;
                member.nTerm = nterm;
                member.SchoolID = userData.CompanyID;

                var check = _db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                if (check == null)
                {
                    _db.TClassMembers.Add(member);
                }
                else
                {
                    if (txtSearch.Text != "" && txtSearch.Text != null)
                    {
                        check.nTeacherHeadid = member.nTeacherHeadid;
                    }
                    else
                    {
                        check.nTeacherHeadid = null;
                    }

                    if (txtSearch2.Text != "" && txtSearch2.Text != null)
                    {
                        check.nTeacherAssistOne = member.nTeacherAssistOne;
                    }
                    else
                    {
                        check.nTeacherAssistOne = null;
                    }

                    if (txtSearch3.Text != "" && txtSearch3.Text != null)
                    {
                        check.nTeacherAssistTwo = member.nTeacherAssistTwo;
                    }
                    else
                    {
                        check.nTeacherAssistTwo = null;
                    }
                }
                _db.SaveChanges();
                string aaa = "";
                string bbb = "";

                if (idlv != "" && idlv2 != "")
                    Response.Redirect("classmember.aspx?idlv=" + idlv + "&idlv2=" + idlv2 + "&year=" + ddlyear + "&term=" + ddlterm);
                else if (idlv != "" && idlv2 == "")
                    Response.Redirect("classmember.aspx?idlv=" + idlv + "&idlv2=" + aaa + "&year=" + ddlyear + "&term=" + ddlterm);
                else Response.Redirect("classmember.aspx?idlv=" + aaa + "&idlv2=" + bbb + "&year=" + ddlyear + "&term=" + ddlterm);


            }

        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            string aaa = "";
            string bbb = "";
            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];

            Response.Redirect("classmember.aspx");
        }

    }
}