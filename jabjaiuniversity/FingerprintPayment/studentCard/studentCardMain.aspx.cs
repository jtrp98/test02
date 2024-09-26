using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using FingerprintPayment.Helper;
using System.Globalization;
using TemplateBuilder;

namespace FingerprintPayment.studentCard
{
    public partial class studentCardMain : System.Web.UI.Page
    {
        protected string EXP_DATE = "";
        protected bool IsReportBuilder = false;
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            //btnConfig.Click += new EventHandler(btnConfig_Click);
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");


            string comid = Request.QueryString["comid"];
            int sEmpID = int.Parse(Session["sEmpID"] + "");

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (var context = new TemplateBuilderContext())
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());

                if (!IsPostBack)
                {
                  

                    var tCompany = dbmaster.TCompanies.Where(w => w.nCompany == userData.CompanyID).FirstOrDefault();

                    var listYear = _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.numberYear).ToList();
                    DataTable dtYear = fcommon.LinqToDataTable(listYear);

                    fcommon.ListDataTableToDropDownList(dtYear, ddlyear, "เลือกปีการศึกษา", "nYear", "numberYear");

                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);

                    //OpenData();

                    string idlv = Request.QueryString["idlv"];
                    string idlv2 = Request.QueryString["idlv2"];
                    string name = Request.QueryString["name"];

                    var forms = context.TblReportForm
                       .Where(o => ("," + o.sSchoolIDs + ",").Contains("," + userData.CompanyID + ",") && o.sFormType == "บัตรนักเรียน" && o.isShown == true)
                       .ToList();

                    if (forms.Count > 0)
                    {
                        IsReportBuilder = true;
                        foreach (var f in forms)
                        {
                            ddlCardForm2.Items.Add(new ListItem(f.sFormName, f.nFormID + ""));
                        }
                    }
                    else
                    {
                        IsReportBuilder = false;
                    }


                    List<TSubLevel> SubLevel = new List<TSubLevel>();
                    TSubLevel sub = new TSubLevel();

                    //printok.Attributes["class"] = "col-xs-5 button-section hidden";
                    //Div1.Attributes["class"] = "col-xs-5 button-section hidden";
                    //printerror.Attributes["class"] = "col-xs-5 button-section";

                    PicProfileUpDown.SelectedValue = "0";
                    PicProfileLeftRight.SelectedValue = "0";
                    BarcodeUpDown.SelectedValue = "0";
                    BarcodeRightLeft.SelectedValue = "0";

                    ContentAlignmentTopBottom.SelectedValue = "0";
                    ContentAlignmentLeftRight.SelectedValue = "0";

                    var cardInfoes = _db.TStudentCardInfoes.Where(o => o.SchoolID == userData.CompanyID).ToList();

                    var optionPatternContent = cardInfoes.Where(w => w.elementName == "PatternContent").FirstOrDefault();
                    if (optionPatternContent != null)
                        PatternContent.SelectedValue = optionPatternContent.elementValue;

                    var PicProfileUD = cardInfoes.Where(w => w.elementName == "PicProfileUpDown").FirstOrDefault();
                    if (PicProfileUD != null)
                        PicProfileUpDown.SelectedValue = PicProfileUD.elementValue;

                    var PicProfileLR = cardInfoes.Where(w => w.elementName == "PicProfileLeftRight").FirstOrDefault();
                    if (PicProfileLR != null)
                        PicProfileLeftRight.SelectedValue = PicProfileLR.elementValue;

                    var positionTopBottom = cardInfoes.Where(w => w.elementName == "BarcodeQRTopBottom").FirstOrDefault();
                    if (positionTopBottom != null)
                        BarcodeUpDown.SelectedValue = positionTopBottom.elementValue;

                    var positionLeftRigh = cardInfoes.Where(w => w.elementName == "BarcodeQRLeftRight").FirstOrDefault();
                    if (positionLeftRigh != null)
                        BarcodeRightLeft.SelectedValue = positionLeftRigh.elementValue;

                    var TopBottom = cardInfoes.Where(w => w.elementName == "ContentAlignmentTopBottom").FirstOrDefault();
                    if (TopBottom != null)
                        ContentAlignmentTopBottom.SelectedValue = TopBottom.elementValue;

                    var LeftRight = cardInfoes.Where(w => w.elementName == "ContentAlignmentLeftRight").FirstOrDefault();
                    if (LeftRight != null)
                        ContentAlignmentLeftRight.SelectedValue = LeftRight.elementValue;

                    var cardType = cardInfoes.Where(w => w.elementName == "CardType").FirstOrDefault();
                    if (cardType != null)
                        optionCourse.SelectedValue = cardType.elementValue;

                    var newCardType = cardInfoes.Where(w => w.elementName == "NewCardType").FirstOrDefault();
                    if (newCardType != null)
                        ddlCardForm2.SelectedValue = newCardType.elementValue;

                    var educationType = cardInfoes.Where(w => w.elementName == "EducationType").FirstOrDefault();
                    if (educationType != null)
                        optionClass.SelectedValue = educationType.elementValue;

                    var FontColor = cardInfoes.Where(w => w.elementName == "FontColor").FirstOrDefault();
                    if (FontColor != null)
                        optionColorNickName.SelectedValue = FontColor.elementValue;


                    var ShowName = cardInfoes.Where(w => w.elementName == "ShowName").FirstOrDefault();
                    if (ShowName != null)
                        optionShowName.SelectedValue = ShowName.elementValue;

                    var ShowNickName = cardInfoes.Where(w => w.elementName == "ShowNickName").FirstOrDefault();
                    if (ShowNickName != null)
                        optionNickName.SelectedValue = ShowNickName.elementValue;

                    var ShowBrithday = cardInfoes.Where(w => w.elementName == "Brithday").FirstOrDefault();
                    if (ShowBrithday != null)
                        optionboxBrithday.SelectedValue = ShowBrithday.elementValue;

                    var ShowNumberPhone = cardInfoes.Where(w => w.elementName == "NumberPhone").FirstOrDefault();
                    if (ShowNumberPhone != null)
                        optionnumberPhone.SelectedValue = ShowNumberPhone.elementValue;

                    var jType1 = cardInfoes.Where(w => w.elementName == "JourneyType1" && w.cDel == null).FirstOrDefault();
                    if (jType1 != null && jType1.elementValue == "1")
                        chkJourneyType1.Checked = true;

                    var jType2 = cardInfoes.Where(w => w.elementName == "JourneyType2" && w.cDel == null).FirstOrDefault();
                    if (jType2 != null && jType2.elementValue == "1")
                        chkJourneyType2.Checked = true;

                    var enddate = cardInfoes.Where(w => w.elementName == "CardExpireDate" && w.cDel == null).FirstOrDefault();
                    var printDate = cardInfoes.Where(w => w.elementName == "CardPrintDate" && w.cDel == null).FirstOrDefault();
                    var pidPosition = cardInfoes.Where(w => w.elementName == "PIDPosition" && w.cDel == null)
                        .Select(o => o.elementValue)
                        .FirstOrDefault();

                    if (!string.IsNullOrEmpty(pidPosition))
                    {
                        ddlPIDPosition.SelectedValue = pidPosition;
                    }

                    if (enddate != null)
                    {
                        if (name == "")
                        {
                            printok.Attributes["class"] = "col-xs-5 button-section";
                            Div1.Attributes["class"] = "col-xs-5 button-section hidden";
                            printerror.Attributes["class"] = "col-xs-5 button-section hidden";
                        }
                        else
                        {
                            Div1.Attributes["class"] = "col-xs-5 button-section hidden";
                            printok.Attributes["class"] = "col-xs-5 button-section hidden";
                            printerror.Attributes["class"] = "col-xs-5 button-section hidden";
                        }

                        //string day = enddate.date.Value.Day.ToString();
                        //if (day.Length == 1)
                        //    day = "0" + day;
                        //string month2 = enddate.date.Value.Month.ToString();
                        //if (month2.Length == 1)
                        //    month2 = "0" + month2;
                        //string year2 = enddate.date.Value.Year.ToString();

                        startDay.Text = enddate.date?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        startDay2.Text = enddate.date?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    }
                    else
                    {
                        if (name == "")
                        {
                            printok.Attributes["class"] = "col-xs-5 button-section hidden";
                            Div1.Attributes["class"] = "col-xs-5 button-section";
                            printerror.Attributes["class"] = "col-xs-5 button-section hidden";
                        }
                        else
                        {
                            Div1.Attributes["class"] = "col-xs-5 button-section hidden";
                            printok.Attributes["class"] = "col-xs-5 button-section hidden";
                            printerror.Attributes["class"] = "col-xs-5 button-section hidden";
                        }
                    }

                    if (printDate != null)
                    {
                        txtPrintDate.Text = printDate.date?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        txtPrintDate2.Text = printDate.date?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    }

                    foreach (var a in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nWorkingStatus == 1))
                    {
                        sub = new TSubLevel();
                        sub = a;
                        SubLevel.Add(sub);
                    }

                    foreach (var t in SubLevel)
                    {
                        var item = new ListItem
                        {
                            Text = t.SubLevel,
                            Value = t.nTSubLevel.ToString()
                        };
                        ddlsublevel.Items.Add(item);
                    }

                    ddlsublevel.SelectedValue = idlv;
                    ddlsublevel2.SelectedValue = idlv2;
                    txtddl2.Text = idlv2;

                }
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object List_DataStudent(Search search)
        {
            var schoolID = GetUserData().CompanyID;

            
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {


                var titlelist = _db.TTitleLists.Where(w => w.SchoolID == schoolID).ToList();

                string sqlCondition = "";

                if (!string.IsNullOrEmpty(search.term)) { sqlCondition += string.Format(@" AND a.nTerm = '{0}' ", search.term.Trim()); }
                if (search.level.HasValue) { sqlCondition += string.Format(@" AND a.nTSubLevel = '{0}' ", search.level); }
                if (!string.IsNullOrEmpty(search.className.ToString())) { sqlCondition += string.Format(@" AND a.nTermSubLevel2 = {0}", search.className.ToString()); }
                if (!string.IsNullOrEmpty(search.fullname))
                { sqlCondition += string.Format(@" AND (REPLACE(A.sName+A.sLastname, ' ', '') like N'%{0}%' OR REPLACE(a.sStudentID,' ', '') LIKE N'%{0}%')", search.fullname.Replace(" ", "")); }


                string sqlQueryFilter = string.Format(@"
SELECT *
FROM TB_StudentViews a
WHERE a.cDel IS NULL AND ISNULL(a.nStudentStatus, 0) = 0 AND a.SchoolID = {0} {1}
", schoolID, sqlCondition);

                List<TB_StudentViews> tB_StudentViews = _db.Database.SqlQuery<TB_StudentViews>(sqlQueryFilter).ToList();
                List<Studentlist> studentlists = new List<Studentlist>();

                foreach (var av in tB_StudentViews.OrderBy(o => o.nStudentNumber))
                {
                    studentlists.Add(new Studentlist
                    {
                        sid = av.sID,
                        studentID = av.sStudentID,
                        studentnumber = av.nStudentNumber,
                        fullname = av.sName + " " + av.sLastname,
                        fullClassname = av.SubLevel + "/" + av.nTSubLevel2,
                        nTerm = av.nTerm,
                        nTermSubLevel2 = av.nTermSubLevel2,
                        nTSubLevel = av.nTSubLevel
                    });
                }

                return new View { Studentlists = studentlists };
            }
        }

        [WebMethod]
        public static string[] GetStudentName(string keyword)
        {
            int schoolID = GetUserData().CompanyID;
            JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

            string sqlQuery = string.Format(@"
SELECT TOP 10 TRIM(sName)+' '+TRIM(sLastname) 
FROM TUser
WHERE cDel IS NULL AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%')
GROUP BY sName, sLastname
ORDER BY sName, sLastname", schoolID, keyword);
            List<string> result = dbschool.Database.SqlQuery<string>(sqlQuery).ToList();

            return result.ToArray();
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
            public int? year { get; set; }
            public string term { get; set; }
            public int? level { get; set; }
            public int? className { get; set; }
            public string fullname { get; set; }
        }

        public class View
        {
            public List<Studentlist> Studentlists { get; set; }
        }

        public class Studentlist
        {
            public int sid { get; set; }
            public string studentID { get; set; }
            public int? studentnumber { get; set; }
            public string fullname { get; set; }
            public string fullClassname { get; set; }
            public int nTSubLevel { get; set; }
            public int nTermSubLevel2 { get; set; }
            public string nTerm { get; set; }
        }

    }
}
