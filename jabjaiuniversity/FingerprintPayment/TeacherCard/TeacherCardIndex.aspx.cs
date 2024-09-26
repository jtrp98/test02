using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using FingerprintPayment.Helper;
using TemplateBuilder;


namespace FingerprintPayment.TeacherCard
{
    public partial class TeacherCardIndex : System.Web.UI.Page
    {
        protected string EXP_DATE = "";
        protected bool IsReportBuilder = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current))
            { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            if (!this.IsPostBack)
            {
                JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));

                using (JabJaiMasterEntities Master_db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = Master_db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    hdfschoolname.Value = tCompany.sCompany;
                }

                var TeacherCardContentUpdown = db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "ContentAlignmentTopBottom").FirstOrDefault();
                if (TeacherCardContentUpdown == null)
                    select_UpDown.SelectedValue = "0";
                else
                    select_UpDown.SelectedValue = TeacherCardContentUpdown.elementValue;


                var TeacherCardContentLeftRight = db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "ContentAlignmentLeftRight").FirstOrDefault();
                if (TeacherCardContentLeftRight == null)
                    select_LeftRight.SelectedValue = "0";
                else
                    select_LeftRight.SelectedValue = TeacherCardContentLeftRight.elementValue;


                var CHECK_ShowNameEng = db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "ShowNameEng").FirstOrDefault();
                if (CHECK_ShowNameEng == null)
                    ShowNameEng.SelectedValue = "0";
                else
                    ShowNameEng.SelectedValue = CHECK_ShowNameEng.elementValue;


                var CHECK_ShowPosition = db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "ShowPosition").FirstOrDefault();
                if (CHECK_ShowPosition == null)
                    ShowPosition.SelectedValue = "0";
                else
                    ShowPosition.SelectedValue = CHECK_ShowPosition.elementValue;


                var CHECK_ShowEmpID = db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "ShowEmpID").FirstOrDefault();
                if (CHECK_ShowEmpID == null)
                    ShowEmpID.SelectedValue = "0";
                else
                    ShowEmpID.SelectedValue = CHECK_ShowEmpID.elementValue;


                var CHECK_ShowIdentity = db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "ShowIdentity").FirstOrDefault();
                if (CHECK_ShowIdentity == null)
                    ShowIdentity.SelectedValue = "0";
                else
                    ShowIdentity.SelectedValue = CHECK_ShowIdentity.elementValue;


                var CardType = db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID && w.elementName == "CardType").FirstOrDefault();
                if (CardType != null)
                    select_tYpe.SelectedValue = CardType.elementValue;


                eMp_Type.DataSource = Common.GetEmployeeTypeToDDL(userData.CompanyID);
                eMp_Type.DataBind();

                using (var context = new TemplateBuilderContext())
                {
                    var forms = context.TblReportForm
                       .Where(o => ("," + o.sSchoolIDs + ",").Contains("," + userData.CompanyID + ",") && o.sFormType == "บัตรบุคลากร" && o.isShown == true)
                       .ToList();

                    if (forms.Count > 0)
                    {
                        IsReportBuilder = true;
                        foreach (var f in forms)
                        {
                            ddlCardForm2.Items.Add(new ListItem(f.sFormName, f.nFormID + ""));
                        }

                        var NewCardType = db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID && w.elementName == "NewCardType").FirstOrDefault();
                        if (NewCardType != null)
                            ddlCardForm2.SelectedValue = NewCardType.elementValue;

                    }
                    else
                    {
                        IsReportBuilder = false;
                    }
                }




            }


        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object List_DataEmp(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {

                var Title = db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                var typeLists = db.TEmployeeTypes
                          .Where(o => o.SchoolID == userData.CompanyID && o.IsActive == true && o.IsDel == false)
                          .Select(o => new TypeList
                          {
                              cTypeID = (o.nTypeId2 ?? o.nTypeId) + "",
                              cTypeName = o.Title,
                          })
                          .OrderBy(o => o.cTypeID)
                          .ToList();


                string sqlCondition = "";
                if (!string.IsNullOrEmpty(search.Emp_Name))
                {
                    sqlCondition += string.Format(@" AND (e.sName LIKE N'%{0}%' OR e.sLastname LIKE N'%{0}%' OR e.sName+' '+e.sLastname = N'{0}')", search.Emp_Name.Replace("'", "''"));
                }
                else
                {
                    if (!string.IsNullOrEmpty(search.eMp_Type))
                    {
                        sqlCondition += string.Format(@" AND e.cType = '{0}'", search.eMp_Type);
                    }
                }

                if (!string.IsNullOrEmpty(search.eMp_Type))
                {
                    sqlCondition += string.Format(@" AND e.cType = '{0}'", search.eMp_Type);
                }


                string sqlQueryFilte1 = string.Format(@"
SELECT A.*
FROM
(
    SELECT ROW_NUMBER() OVER(ORDER BY EmpID ASC) AS RowNumber, T.*
	FROM (
		SELECT e.sEmp 'EmpID', e.sIdentification, e.cType, e.sTitle, e.sName, e.sLastname, i.Code
		FROM TEmployees e
		LEFT JOIN TEmployeeInfo i ON e.sEmp = i.sEmp AND e.SchoolID = i.SchoolID
        LEFT JOIN TEmpSalary C ON e.sEmp = C.sEmp AND e.SchoolID = C.SchoolID

		WHERE isnull(e.cDel , '0') = '0' 
        AND   (
           ( ISNULL(C.WorkStatus,1) = 1 AND CAST( GETDATE() AS Date ) >= CAST( ISNULL(C.WorkStartDate,GETDATE()) AS Date ) )
           OR ( (C.WorkStatus = 2 OR C.WorkStatus = 3) AND CAST( GETDATE() AS Date ) <  CAST(C.DayQuit AS Date ) )
          )
        AND e.SchoolID = {0} {1} 
	) AS T
) AS A", userData.CompanyID, sqlCondition);


                List<Lists> Lists = db.Database.SqlQuery<Lists>(sqlQueryFilte1).ToList();

                List<EmpList> empLists = new List<EmpList>();

                foreach (var i in Lists)
                {
                    empLists.Add(new EmpList
                    {
                        EmpID = i.EmpID,
                        Code = i.Code == null ? "-" : i.Code,
                        EmpType = gEtTypeEmploYee(typeLists, i.cType),
                        EmpFullName = Common.geTitelName(Title, i.sTitle) + i.sName + " " + i.sLastname
                    });
                }

                //var Test = (from a in empLists
                //            select new
                //            {
                //                EmpID = a.EmpID,
                //                Code = a.Code,
                //                EmpType = a.EmpType,
                //                EmpFullName = a.EmpFullName,
                //            }).ToList();

                return new View01
                {
                    empLists = empLists
                };
            }
        }


        [WebMethod]
        public static string[] GetTecherName(string keyword)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {

                string empIDMaster = "";
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sql = string.Format(@"SELECT TOP 100 sID FROM TUser WHERE nCompany = {0} AND cType = '1' AND username LIKE N'%{1}%'", schoolID, keyword);
                    List<int> empIDs = dbMaster.Database.SqlQuery<int>(sql).ToList();
                    empIDMaster = string.Join(",", empIDs);
                }

                string sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname
FROM TEmployees
WHERE cDel IS NULL AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') 
AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sPhone LIKE N'%{1}%' OR sEmp IN (SELECT TOP 100 sEmp FROM TEmployeeInfo WHERE cDel = 0 AND SchoolID = {0} AND Code LIKE N'%{1}%') {2}) 
GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, keyword, (string.IsNullOrEmpty(empIDMaster) ? "" : string.Format(@"OR sEmp IN ({0})", empIDMaster)));
                List<string> result = _db.Database.SqlQuery<string>(sqlQuery).ToList();

                return result.ToArray();
            }
        }


        public static string gEtTypeEmploYee(List<TypeList> typeLists, string TypelId)
        {
            string empType = "";
            var f_type = typeLists.FirstOrDefault(f => f.cTypeID == TypelId);
            if (f_type != null)
            {
                empType = f_type.cTypeName;
            }

            return empType;
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

        public class Search
        {
            public string eMp_Type { get; set; }
            public string Emp_Name { get; set; }
        }

        public class TypeList
        {
            public string cTypeID { get; set; }
            public string cTypeName { get; set; }
        }
        public class View01
        {
            public List<EmpList> empLists { get; set; }
        }
        public class EmpList
        {
            public int EmpID { get; set; }
            public string Code { get; set; }
            public string EmpType { get; set; }
            public string EmpFullName { get; set; }
        }

        public class Lists
        {
            public int EmpID { get; set; }
            public int ID { get; set; }
            public string sIdentification { get; set; }
            public string cType { get; set; }
            public string sTitle { get; set; }
            public string sName { get; set; }
            public string sLastname { get; set; }
            public string Code { get; set; }
        }

    }

}