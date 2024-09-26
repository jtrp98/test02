using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using FingerprintPayment.Class;
using Antlr.Runtime.Misc;
using iTextSharp.text.pdf.parser;
using System.Linq.Expressions;
using FingerprintPayment.Memory;
using System.Data.Entity;
using System.Web.Hosting;
using JabjaiMainClass.Models;

namespace FingerprintPayment.studentCard
{
    public partial class studentcardregister : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            string comid = Request.QueryString["comid"];
            int sEmpID = int.Parse(Session["sEmpID"] + "");

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());

                if (!IsPostBack)
                {
                    //ImportOldData();

                    ViewState["pagesize"] = 100;

                    OpenData();
                    string type = Request.QueryString["type"];
                    string idlv = Request.QueryString["idlv"];
                    string idlv2 = Request.QueryString["idlv2"];
                    string name = Request.QueryString["name"];
                    string year = Request.QueryString["year"];
                    List<TSubLevel> SubLevel = new List<TSubLevel>();
                    TSubLevel sub = new TSubLevel();
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
                    ddlType.SelectedValue = type;
                    //tags.Text = name;
                    TeacherStudentAutocomplete.SetText(name);
                }
            }
        }

        private List<studentlist2> returnlist()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            List<studentlist2> StudentList = new List<studentlist2>();

            string type = Request.QueryString["type"] + "";
            string sname = Request.QueryString["name"];
            string nfc = Request.QueryString["nfc"];
            TeacherStudentAutocomplete.SetText(sname);
            //tags.Text = sname;
            txtNFC.Text = nfc;
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //studentlist2 std = new studentlist2();        

                if (type == "1" || type == "")
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var query = string.Format($@"

SELECT u.sID--, u.sFinger, u.sFinger2, u.dBirth, u.sEmail, u.sPhone
, ISNULL(C.NFC1, '')+','+ISNULL(C.NFC2, '')+','+ISNULL(C.NFC3, '') 'Card'
, ISNULL(C.ENNFC1, '')+','+ISNULL(C.ENNFC2, '')+','+ISNULL(C.ENNFC3, '') 'EnCard'
, ISNULL(C.NFCRe1, '')+','+ISNULL(C.NFCRe2, '')+','+ISNULL(C.NFCRe3, '') 'CardRe'
, ISNULL(C.ENNFCRe1, '')+','+ISNULL(C.ENNFCRe2, '')+','+ISNULL(C.ENNFCRe3, '') 'EnCardRe'
, ISNULL(C.IsActive1, '')+','+ISNULL(C.IsActive2, '')+','+ISNULL(C.IsActive3, '') 'Status'
, ISNULL(C.UpdateBy1, '')+','+ISNULL(C.UpdateBy2, '')+','+ISNULL(C.UpdateBy3, '') 'UpdateBy'
, ISNULL(FORMAT (C.UpdateDate1, 'dd/MM/yyyy hh:mm', 'th-TH') , '')+','+ISNULL(FORMAT (C.UpdateDate2, 'dd/MM/yyyy hh:mm', 'th-TH'), '')+','+ISNULL(FORMAT (C.UpdateDate3, 'dd/MM/yyyy hh:mm','th-TH'), '')'UpdateDate'
, ISNULL(C.FreeText1, '')+','+ISNULL(C.FreeText2, '')+','+ISNULL(C.FreeText3, '') 'FreeText'

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
    , MAX(CASE WHEN c.[No] = 1 THEN c.[FreeText] END) 'FreeText1', MAX(CASE WHEN c.[No] = 2 THEN c.[FreeText] END) 'FreeText2', MAX(CASE WHEN c.[No] = 3 THEN c.[FreeText] END) 'FreeText3'
	FROM TUser_Card c 
    LEFT JOIN TUser a ON C.SchoolID = a.nCompany AND c.ModifyBy = a.sID
	WHERE c.SchoolID={userData.CompanyID} AND c.IsDel=0  AND ISNULL(c.NFC, '') <> '' 

	GROUP BY c.sID
) C ON u.sID = C.sID
--/studentcardregister/studentcardregister.aspx/returnlist()[1]
WHERE u.nCompany={userData.CompanyID} AND u.cType='0'");

                    //{(string.IsNullOrEmpty(nfc) ? " AND ISNULL(c.NFC, '') <> '' " : " AND c.NFC like '%" + nfc + "%'")}
                    var TUserMaster = dbmaster.Database.SqlQuery<CardRegisterModel>(query).ToList();

                    using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
                    {
                        string ddlSubLV2 = Request.QueryString["idlv2"];

                        int idlv = int.Parse(string.IsNullOrEmpty(Request.QueryString["idlv"]) ? "0" : Request.QueryString["idlv"].ToString());
                        int idlv2 = int.Parse(string.IsNullOrEmpty(ddlSubLV2) ? "0" : ddlSubLV2);

                        var date = DateTime.Now;
                        ThaiBuddhistCalendar cal = new ThaiBuddhistCalendar();
                        StudentLogic logic = new StudentLogic(db);
                        var termData = logic.GetLastTermId(DateTime.Today, userData);

                        //DateTime dStart = termData.dStart ?? DateTime.Now;
                        //var dStart = db.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(c => c.dStart <= dateTime && c.dEnd >= dateTime).Select(s => (s.dStart.HasValue) ? (DateTime)s.dStart : DateTime.Now).FirstOrDefault();
                        //DateTime thaiDateTime = new DateTime(cal.GetYear(dStart), cal.GetMonth(dStart), dStart.Day);

                        var qry1 = (from a in db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == termData && (w.cDel ?? "0") != "1")
                                    select new
                                    {
                                        a.sID,
                                        a.nTermSubLevel2,
                                        a.nTSubLevel,
                                        a.sName,
                                        a.sLastname,
                                        a.sStudentID,
                                        a.nTimeType,
                                        a.nStudentNumber,
                                        a.SubLevel,
                                        a.nTSubLevel2,
                                        a.nStudentStatus,
                                        a.numberYear,
                                    });

                        if (idlv2 > 0) qry1 = qry1.Where(w => w.nTermSubLevel2 == idlv2);
                        else if (idlv > 0) qry1 = qry1.Where(w => w.nTSubLevel == idlv);

                        if (!string.IsNullOrEmpty(sname))
                        {
                            qry1 = qry1.Where(w => (w.sName + " " + w.sLastname).Contains(sname)
                            || w.sStudentID.Contains(sname)
                            );
                        }

                        var tUser = qry1.ToList();

                        int rowindex = 1;
                        var data = (from a in tUser
                                        //join b in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
                                        //join c in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
                                    join master in TUserMaster on a.sID equals master.sID
                                    join d in db.TTimetypes.Where(w => w.SchoolID == userData.CompanyID) on a.nTimeType equals d.nTimeType into dd
                                    //where a.nTerm == termData.nTerm && (a.cDel ?? "0") != "1"
                                    from jd in dd.DefaultIfEmpty()
                                    select new studentlist2
                                    {
                                        sNo = a.nStudentNumber,
                                        sName = string.Format("{0} {1}", string.IsNullOrEmpty(a.sName) ? "" : a.sName, string.IsNullOrEmpty(a.sLastname) ? "" : a.sLastname),
                                        sLastName = string.IsNullOrEmpty(a.sLastname) ? "" : a.sLastname,
                                        nTSubLevel2 = a.nTermSubLevel2,
                                        sIdentification = string.IsNullOrEmpty(a.sStudentID) ? "" : a.sStudentID.Trim(),
                                        sId = a.sID,
                                        fingerStatus = master.sFinger != null && master.sFinger2 != null || tCompany.sotfware == true,
                                        birthday = master.dBirth?.ToString("dd/MM/yyyy"),
                                        email = master.sEmail,
                                        phone = master.sPhone,
                                        nTermSubLevel2 = a.nTermSubLevel2,
                                        nTSubLevel = a.nTSubLevel,
                                        timetype = jd == null ? "" : jd.sTimeType,
                                        classroom = a.SubLevel?.Trim() + " / " + a.nTSubLevel2,
                                        status = student_status(a.nStudentStatus),
                                        Year = a.numberYear,
                                        Card = master?.Card,
                                        StatusCard = master?.Status,
                                        EnCard = master?.EnCard,
                                        CardRe = master?.CardRe,
                                        EnCardRe = master?.EnCardRe,
                                        UpdateBy = master?.UpdateBy,
                                        UpdateDate = master?.UpdateDate,
                                        FreeText = master?.FreeText,
                                    })
                                    .DistinctBy(row => row.sId)
                                    //.OrderBy( o => o.sNo)
                                    .ToList();


                        //if (idlv2 > 0) data = data.Where(w => w.nTermSubLevel2 == idlv2).ToList();
                        //else if (idlv > 0) data = data.Where(w => w.nTSubLevel == idlv).ToList();

                        //if (!string.IsNullOrEmpty(sname))
                        //{
                        //    data = data.Where(w => (w.sName + " " + w.sLastName).Contains(sname)
                        //    || w.sIdentification.Contains(sname)
                        //    ).ToList();
                        //}

                        if (!string.IsNullOrEmpty(nfc))
                        {
                            data = data.Where(w => w.Card.Contains($"{nfc}")).ToList();
                        }

                        StudentList = (from a in data
                                       orderby a.sNo
                                       select new studentlist2
                                       {
                                           number = rowindex++,
                                           sName = a.sName,
                                           sLastName = a.sLastName,
                                           nTSubLevel2 = a.nTermSubLevel2,
                                           sIdentification = a.sIdentification,
                                           sId = a.sId,
                                           fingerStatus = a.fingerStatus,
                                           birthday = a.birthday,
                                           email = a.email,
                                           phone = a.phone,
                                           nTermSubLevel2 = a.nTermSubLevel2,
                                           nTSubLevel = a.nTSubLevel,
                                           timetype = a.timetype,
                                           classroom = a.classroom,
                                           status = a.status,
                                           Card = a.Card,
                                           CardRe = a.CardRe,
                                           EnCard = a.EnCard,
                                           EnCardRe = a.EnCardRe,
                                           StatusCard = a.StatusCard,
                                           UpdateBy = a.UpdateBy,
                                           UpdateDate = a.UpdateDate,
                                           FreeText = a.FreeText,
                                       }).ToList();

                    }
                }
                else
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var query = string.Format($@"

SELECT u.sID--, u.sFinger, u.sFinger2, u.dBirth, u.sEmail, u.sPhone
, ISNULL(C.NFC1, '')+','+ISNULL(C.NFC2, '')+','+ISNULL(C.NFC3, '') 'Card'
, ISNULL(C.ENNFC1, '')+','+ISNULL(C.ENNFC2, '')+','+ISNULL(C.ENNFC3, '') 'EnCard'
, ISNULL(C.NFCRe1, '')+','+ISNULL(C.NFCRe2, '')+','+ISNULL(C.NFCRe3, '') 'CardRe'
, ISNULL(C.ENNFCRe1, '')+','+ISNULL(C.ENNFCRe2, '')+','+ISNULL(C.ENNFCRe3, '') 'EnCardRe'
, ISNULL(C.IsActive1, '')+','+ISNULL(C.IsActive2, '')+','+ISNULL(C.IsActive3, '') 'Status'
, ISNULL(C.UpdateBy1, '')+','+ISNULL(C.UpdateBy2, '')+','+ISNULL(C.UpdateBy3, '') 'UpdateBy'
, ISNULL(FORMAT (C.UpdateDate1, 'dd/MM/yyyy hh:mm', 'th-TH') , '')+','+ISNULL(FORMAT (C.UpdateDate2, 'dd/MM/yyyy hh:mm', 'th-TH'), '')+','+ISNULL(FORMAT (C.UpdateDate3, 'dd/MM/yyyy hh:mm','th-TH'), '')'UpdateDate'
, ISNULL(C.FreeText1, '')+','+ISNULL(C.FreeText2, '')+','+ISNULL(C.FreeText3, '') 'FreeText'
FROM TUser u  LEFT JOIN
(
	SELECT c.sID
	, MAX(CASE WHEN c.[No] = 1 THEN c.NFC END) 'NFC1', MAX(CASE WHEN c.[No] = 2 THEN c.NFC END) 'NFC2', MAX(CASE WHEN c.[No] = 3 THEN c.NFC END) 'NFC3'
    , MAX(CASE WHEN c.[No] = 1 THEN c.NFCReverse END) 'NFCRe1', MAX(CASE WHEN c.[No] = 2 THEN c.NFCReverse END) 'NFCRe2', MAX(CASE WHEN c.[No] = 3 THEN c.NFCReverse END) 'NFCRe3'
	, MAX(CASE WHEN c.[No] = 1 THEN c.NFCEncrypt END) 'ENNFC1', MAX(CASE WHEN c.[No] = 2 THEN c.NFCEncrypt END) 'ENNFC2', MAX(CASE WHEN c.[No] = 3 THEN c.NFCEncrypt END) 'ENNFC3'
	, MAX(CASE WHEN c.[No] = 1 THEN c.NFCEncryptReverse END) 'ENNFCRe1', MAX(CASE WHEN c.[No] = 2 THEN c.NFCEncryptReverse END) 'ENNFCRe2', MAX(CASE WHEN c.[No] = 3 THEN c.NFCEncryptReverse END) 'ENNFCRe3'
	, MAX(CASE WHEN c.[No] = 1 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive1', MAX(CASE WHEN c.[No] = 2 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive2', MAX(CASE WHEN c.[No] = 3 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive3'
	, MAX(CASE WHEN c.[No] = 1 THEN a.sName + ' ' + a.sLastname END) 'UpdateBy1', MAX(CASE WHEN c.[No] = 2 THEN a.sName + ' ' + a.sLastname END) 'UpdateBy2', MAX(CASE WHEN c.[No] = 3 THEN a.sName + ' ' + a.sLastname END) 'UpdateBy3'
	, MAX(CASE WHEN c.[No] = 1 THEN c.Modified END) 'UpdateDate1', MAX(CASE WHEN c.[No] = 2 THEN c.Modified END) 'UpdateDate2', MAX(CASE WHEN c.[No] = 3 THEN c.Modified END) 'UpdateDate3'
    , MAX(CASE WHEN c.[No] = 1 THEN c.[FreeText] END) 'FreeText1', MAX(CASE WHEN c.[No] = 2 THEN c.[FreeText] END) 'FreeText2', MAX(CASE WHEN c.[No] = 3 THEN c.[FreeText] END) 'FreeText3'

	FROM TUser_Card c 
    LEFT JOIN TUser a ON C.SchoolID = a.nCompany AND c.ModifyBy = a.sID
	WHERE c.SchoolID={userData.CompanyID} AND c.IsDel=0  {(string.IsNullOrEmpty(nfc) ? " AND ISNULL(c.NFC, '') <> '' " : " AND c.NFC like '%" + nfc + "%'")}

	GROUP BY c.sID
) C ON u.sID = C.sID
--/studentcardregister/studentcardregister.aspx/returnlist()[1]
WHERE u.nCompany={userData.CompanyID} AND u.cType='1'");
                    var TUserMaster = dbmaster.Database.SqlQuery<CardRegisterModel>(query).ToList();

                    using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
                    {
                        int rowindex = 1;

                        var aa = db.TEmployees.Where(o => o.cDel != "1" && o.SchoolID == userData.CompanyID)
                            .Select(o => new
                            {
                                o.sEmp,
                                o.sName,
                                o.sLastname,
                                o.nTimeType,
                            })
                            .ToList();
                        var bb = db.TEmployeeInfoes.Where(o => o.SchoolID == userData.CompanyID)
                            .Select(o => new
                            {
                                o.sEmp,
                                o.Code,
                            })
                            .ToList();

                        var data = (from a in aa
                                    from master in TUserMaster.Where(o => o.sID == a.sEmp).DefaultIfEmpty()
                                    from b in bb.Where(o => o.sEmp == a.sEmp).DefaultIfEmpty()

                                    select new studentlist2
                                    {
                                        sName = string.Format("{0} {1}", string.IsNullOrEmpty(a.sName) ? "" : a.sName, string.IsNullOrEmpty(a.sLastname) ? "" : a.sLastname),
                                        sLastName = a.sLastname + "",

                                        sIdentification = b?.Code ?? "", // master.sIdentification,
                                        sId = a.sEmp,
                                        fingerStatus = master?.sFinger != null && master?.sFinger2 != null || tCompany.sotfware == true,
                                        birthday = master?.dBirth?.ToString("dd/MM/yyyy"),
                                        email = master?.sEmail,
                                        phone = master?.sPhone,

                                        timetype = a.nTimeType + "",
                                        Card = master?.Card,
                                        StatusCard = master?.Status,
                                        EnCard = master?.EnCard,
                                        CardRe = master?.CardRe,
                                        EnCardRe = master?.EnCardRe,
                                        UpdateBy = master?.UpdateBy,
                                        UpdateDate = master?.UpdateDate,
                                        FreeText = master?.FreeText,
                                    })
                                    //.DistinctBy(row => row.sId)
                                    .ToList();


                        //if (idlv2 > 0) data = data.Where(w => w.nTermSubLevel2 == idlv2).ToList();
                        //else if (idlv > 0) data = data.Where(w => w.nTSubLevel == idlv).ToList();


                        if (!string.IsNullOrEmpty(sname))
                        {
                            data = data.Where(w => (w.sName + " " + w.sLastName).Contains(sname) || w.sIdentification.Contains(sname)).ToList();
                        }

                        if (!string.IsNullOrEmpty(nfc))
                        {
                            data = data.Where(w => w.Card.Contains($"{nfc}")).ToList();
                        }


                        StudentList = (from a in data
                                       orderby a.sIdentification
                                       select new studentlist2
                                       {
                                           number = rowindex++,
                                           sName = a.sName,
                                           sLastName = a.sLastName,
                                           nTSubLevel2 = a.nTermSubLevel2,
                                           sIdentification = a.sIdentification == "" ? "-" : a.sIdentification,
                                           sId = a.sId,
                                           fingerStatus = a.fingerStatus,
                                           birthday = a.birthday,
                                           email = a.email,
                                           phone = a.phone,
                                           nTermSubLevel2 = a.nTermSubLevel2,
                                           nTSubLevel = a.nTSubLevel,
                                           timetype = a.timetype,
                                           classroom = a.classroom,
                                           status = a.status,
                                           Card = a.Card,
                                           CardRe = a.CardRe,
                                           EnCard = a.EnCard,
                                           EnCardRe = a.EnCardRe,
                                           StatusCard = a.StatusCard,
                                           UpdateBy = a.UpdateBy,
                                           UpdateDate = a.UpdateDate,
                                           FreeText = a.FreeText,
                                       }).ToList();

                    }
                }

            }
            return StudentList;
        }

        private string student_status(int? nStudentStatus)
        {
            switch (nStudentStatus)
            {
                case 0: return "กำลังศึกษา";
                case 1: return "จำหน่าย";
                case 2: return "ลาออก";
                case 3: return "พักการเรียน";
                case 4: return "สำเร็จการศึกษา";
                case 5: return "ขาดการติดต่อ";
                case 6: return "พ้นสภาพ";
                default: return "กำลังศึกษา";
            }
        }

        private void OpenData()
        {
            dgd.DataSource = returnlist();
            //dgd.PageSize = (ViewState["pagesize"] != null && !string.IsNullOrEmpty(Convert.ToString(ViewState["pagesize"])) ? int.Parse(ViewState["pagesize"].ToString()) : 100);
            //dgd.PagerSettings.Visible = true;
            dgd.DataBind();
        }

        protected void CustomersGridView_DataBound(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            //GridViewRow pagerRow = dgd.BottomPagerRow;
            //if (pagerRow != null)
            //{
            //    // Retrieve the DropDownList and Label controls from the row.
            //    DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            //    DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
            //    Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

            //    //int num = 20;
            //    //int num2 = 50;
            //    //int num3 = 100;
            //    int num = 100;
            //    int num2 = 200;
            //    int num3 = 300;

            //    ListItem item2 = new ListItem(num.ToString());
            //    pageList2.Items.Add(item2);
            //    if (num == dgd.PageSize)
            //    {
            //        item2.Selected = true;
            //    }
            //    ListItem item3 = new ListItem(num2.ToString());
            //    pageList2.Items.Add(item3);
            //    if (num2 == dgd.PageSize)
            //    {
            //        item3.Selected = true;
            //    }
            //    ListItem item4 = new ListItem(num3.ToString());
            //    pageList2.Items.Add(item4);
            //    if (num3 == dgd.PageSize)
            //    {
            //        item4.Selected = true;
            //    }
            //    if (pageList != null)
            //    {

            //        // Create the values for the DropDownList control based on 
            //        // the  total number of pages required to display the data
            //        // source.
            //        for (int i = 0; i < dgd.PageCount; i++)
            //        {

            //            // Create a ListItem object to represent a page.
            //            int pageNumber = i + 1;
            //            ListItem item = new ListItem(pageNumber.ToString());

            //            // If the ListItem object matches the currently selected
            //            // page, flag the ListItem object as being selected. Because
            //            // the DropDownList control is recreated each time the pager
            //            // row gets created, this will persist the selected item in
            //            // the DropDownList control.   
            //            if (i == dgd.PageIndex)
            //            {
            //                item.Selected = true;
            //            }

            //            // Add the ListItem object to the Items collection of the 
            //            // DropDownList.
            //            pageList.Items.Add(item);

            //        }
            //    }

            //    if (pageLabel != null)
            //    {

            //        // Calculate the current page number.
            //        int currentPage = dgd.PageIndex + 1;

            //        // Update the Label control with the current page information.
            //        pageLabel.Text = "หน้าที่ " + currentPage.ToString() +
            //          " จากทั้งหมด " + dgd.PageCount.ToString() + " หน้า ";

            //    }
            //}
        }

        //protected void AddStudentCard(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(hdnStdId.Value))
        //        {

        //            int sId = int.Parse(hdnStdId.Value);
        //            int cardNumber;
        //            bool isNumeric = int.TryParse(txtStudentCardNumber.Text, out cardNumber);

        //            if (isNumeric)
        //            {
        //                JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read);
        //                string sEntities = Session["sEntities"].ToString();
        //                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
        //                var q_usermaster = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.nSystemID == sId && w.cType == "0").FirstOrDefault();
        //                q_usermaster.NFC = FormatStudentCardNumber(txtStudentCardNumber.Text);
        //                _dbMaster.SaveChanges();
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}

        //private static string FormatStudentCardNumber(String input)
        //{
        //    input = String.Format("{0:x}", long.Parse(input));
        //    var result = string.Empty;
        //    while (input.Length > 0)
        //    {
        //        result += (input.Length > 0) ? input.Substring(((input.Length - 2) >= 0 ? (input.Length - 2) : 0), (input.Length < 2) ? input.Length : 2) : String.Empty;
        //        input = input.Substring(0, ((input.Length - 2) >= 0 ? (input.Length - 2) : 0));
        //    }
        //    return result;
        //}

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object UpdateStudentCard(int studentId, string card1, string card2, string card3
            , string freetext1, string freetext2, string freetext3)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            try
            {
                if (studentId > 0)
                {
                    long cardNumber1, cardNumber2, cardNumber3;
                    bool isValidCard1 = long.TryParse(card1, out cardNumber1);
                    bool isValidCard2 = long.TryParse(card2, out cardNumber2);
                    bool isValidCard3 = long.TryParse(card3, out cardNumber3);

                    var lstCard = new List<CardModel>();
                    lstCard.Add(new CardModel()
                    {
                        index = 1,
                        cardEncrypt = isValidCard1 ? fcommon.FormatStudentCardNumber(card1) : "",
                        card = card1,
                        IsValid = isValidCard1,
                        FreeText = freetext1,
                    });
                    lstCard.Add(new CardModel()
                    {
                        index = 2,
                        cardEncrypt = isValidCard2 ? fcommon.FormatStudentCardNumber(card2) : "",
                        card = card2,
                        IsValid = isValidCard2,
                        FreeText = freetext2,
                    });
                    lstCard.Add(new CardModel()
                    {
                        index = 3,
                        cardEncrypt = isValidCard3 ? fcommon.FormatStudentCardNumber(card3) : "",
                        card = card3,
                        IsValid = isValidCard3,
                        FreeText = freetext3,
                    });




                    using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
                    {
                        string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                        var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                        var q_usermaster = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.nSystemID == studentId).FirstOrDefault();
                        var q_user = dbSchool.TUser.Where(w => w.SchoolID == tCompany.nCompany && w.sID == studentId).FirstOrDefault();

                        var _cards = _dbMaster.TUser_Card.Where(o => o.SchoolID == userData.CompanyID && o.sID == studentId && o.IsDel == false).ToList();
                        var _str = new List<string>();
                        var nfcCardOnForm = new List<string>();
                        foreach (var c in lstCard)
                        {
                            if (!string.IsNullOrEmpty(c.card))
                            {
                                var objOldCard = _cards.Where(o => o.SchoolID == userData.CompanyID && o.sID == studentId && o.No == c.index && o.IsDel == false).FirstOrDefault();
                                var objCard = _cards.Where(o => o.SchoolID == userData.CompanyID && o.sID == studentId && o.NFCEncrypt.ToUpper() == c.cardEncrypt.ToUpper() && o.IsDel == false).FirstOrDefault();

                                var cardCountInSchool = (from a in _dbMaster.TUser_Card
                                                            .Where(o => o.SchoolID == userData.CompanyID
                                                                && (o.NFCEncrypt.ToUpper() == c.cardEncrypt.ToUpper() || o.NFCEncryptReverse.ToUpper() == c.cardEncrypt.ToUpper())
                                                                && o.IsDel == false)
                                                         from b in _dbMaster.TUsers
                                                             .Where(o => o.nCompany == a.SchoolID && o.sID == a.sID && (o.cDel ?? "0") == "0")
                                                         select a.sID)
                                                        .Count();

                                var backupCardCount = dbSchool.TBackupCards.Where(o => o.SchoolID == userData.CompanyID && o.NFCEncrypt.ToUpper() == c.cardEncrypt.ToUpper() && o.cDel == false).Count();

                                if (objCard == null && cardCountInSchool == 0 && backupCardCount == 0)
                                {
                                    nfcCardOnForm.Add(c.cardEncrypt);

                                    byte cardNo = c.index;
                                    if (objOldCard == null)
                                    {
                                        _str.Add($"เพิ่มบัตร {c.cardEncrypt}");
                                    }
                                    else
                                    {
                                        _str.Add($"แก้ไขบัตร {objOldCard.NFCEncrypt} เป็น {c.cardEncrypt}");
                                    }

                                    _dbMaster.TUser_Card.Add(new TUser_Card
                                    {
                                        sID = studentId,
                                        SchoolID = userData.CompanyID,
                                        No = c.index,
                                        NFC = c.card,
                                        NFCEncrypt = c.cardEncrypt,
                                        NFCEncryptReverse = c.cardEncryptRevese,
                                        NFCReverse = c.cardRevese,
                                        Created = DateTime.Now,
                                        Modified = DateTime.Now,
                                        CreateBy = userData.UserID,
                                        ModifyBy = userData.UserID,
                                        IsDel = false,
                                        IsActive = true,
                                        FreeText = c.FreeText,
                                    });
                                }
                                else
                                {
                                    if (objOldCard != null)
                                    {
                                        // กรณีเพิ่มไม่สำเร็จ; แก้ไขเลขบัตรใหม่ในช่องที่มีเลขบัตรอยู่แล้ว แต่เลขบัตรใหม่ที่รกอกมีซ้ำ(ถูกใช้ไปแล้ว).
                                        nfcCardOnForm.Add(objOldCard.NFCEncrypt);
                                    }
                                    else
                                    {
                                        // กรณีเปลี่ยนลำดับของเลขบัตร
                                        nfcCardOnForm.Add(c.cardEncrypt);

                                        var userCard = _dbMaster.TUser_Card
                                            .Where(w => w.SchoolID == userData.CompanyID && w.sID == studentId && w.NFCEncrypt == c.cardEncrypt)
                                            .FirstOrDefault();

                                        if (userCard != null)
                                        {
                                            userCard.No = c.index;
                                            userCard.Modified = DateTime.Now;
                                            userCard.ModifyBy = userData.UserID;
                                            userCard.FreeText = c.FreeText;
                                        }
                                    }
                                }
                            }
                        }

                        // Set IsDel = false that not used
                        _dbMaster.TUser_Card.Where(w => w.SchoolID == userData.CompanyID && w.sID == studentId && !nfcCardOnForm.Contains(w.NFCEncrypt) && w.IsDel == false)
                            .ToList().ForEach(e =>
                            {
                                e.IsDel = true;
                                e.IsActive = false;
                                e.ModifyBy = userData.UserID;
                                e.Modified = DateTime.Now;
                            });

                        _dbMaster.SaveChanges();

                        database.InsertLog(userData.UserID + "", studentId + " " + string.Join(",", _str), HttpContext.Current.Request,
                         (q_usermaster.cType == "0" ? 14 : 13)
                         , 0, 0, userData.CompanyID);

                        HostingEnvironment.QueueBackgroundWorkItem(ct =>
                        {
                            TopupTempCard topupTempCard = new TopupTempCard();
                            List<TUser_Card> user_Cards = new List<TUser_Card>();
                            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                            {
                                user_Cards = masterEntities.TUser_Card.Where(w => w.sID == studentId).ToList();
                            }

                            topupTempCard.AddOrModifyUserCard(user_Cards, userData.CompanyID, userData.AuthKey, userData.AuthValue);

                            foreach (TUser_Card userCard in user_Cards)
                            {

                                string keyword = q_user.sStudentID != null ? q_user.sStudentID.ToUpper() : "";

                                if (keyword == "")
                                    keyword = userCard.NFC != null ? userCard.NFC.ToUpper() : "";
                                else
                                    keyword += ' ' + userCard.NFC != null ? userCard.NFC.ToUpper() : "";

                                if (keyword == "")
                                    keyword = userCard.NFCEncrypt != null ? userCard.NFCEncrypt.ToUpper() : "";
                                else
                                    keyword += ' ' + userCard.NFCEncrypt != null ? userCard.NFCEncrypt.ToUpper() : "";

                                if (keyword == "")
                                    keyword = userCard.NFCReverse != null ? userCard.NFCReverse.ToUpper() : "";
                                else
                                    keyword += ' ' + userCard.NFCReverse != null ? userCard.NFCReverse.ToUpper() : "";

                                if (keyword == "")
                                    keyword = userCard.NFCEncryptReverse != null ? userCard.NFCEncryptReverse.ToUpper() : "";
                                else
                                    keyword += ' ' + userCard.NFCEncryptReverse != null ? userCard.NFCEncryptReverse.ToUpper() : "";

                                if (userCard.IsDel == false && userCard.IsActive == true)
                                {
                                    JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(userData.CompanyID, studentId + "", q_usermaster.cType, keyword);
                                }
                                else
                                {
                                    JabjaiMainClass.Autocompletes.TopupMoney.Remove(new AC_TopupMoney
                                    {
                                        SchoolID = userData.CompanyID,
                                        User_Id = studentId + "",
                                        User_Type = q_usermaster.cType,
                                        KEYWORD = keyword
                                    });
                                }
                            }


                        });

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string logMessagePattern = @"[SchoolEntities:{0}], [CardData:{1}], [ErrorLine:{2}], [ErrorMessage:{3}]";
                string errorMessage = ex.Message;
                string innerExceptionMessage = "";
                while (ex.InnerException != null) { innerExceptionMessage += ", " + ex.InnerException.Message; ex = ex.InnerException; }
                string logMessageDebug = string.Format(logMessagePattern, userData.Entities, studentId + "-" + card1 + "-" + card2 + "-" + card3, ComFunction.GetLineNumberError(ex), errorMessage + ", " + innerExceptionMessage);

                int? sEmpID = userData.UserID;

                ComFunction.InsertLogDebug(null, null, sEmpID, logMessageDebug);

                return true;
            }

            return false;
        }

        public void nextbutton_Click(Object sender, EventArgs e)
        {
            dgd.DataSource = returnlist();
            dgd.PageIndex = dgd.PageIndex + 1;
            if (dgd.PageIndex > dgd.PageCount)
                dgd.PageIndex = dgd.PageCount;
            dgd.DataBind();
        }

        public void backbutton_Click(Object sender, EventArgs e)
        {
            dgd.DataSource = returnlist();
            if (dgd.PageIndex > 0)
                dgd.PageIndex = dgd.PageIndex - 1;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {
            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            dgd.DataSource = returnlist();
            dgd.PageIndex = pageList.SelectedIndex;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged2(Object sender, EventArgs e)
        {

            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
            ViewState["pagesize"] = Int32.Parse(pageList2.SelectedValue);
            dgd.PageIndex = 0;
            dgd.DataSource = returnlist();
            dgd.PageSize = Int32.Parse(pageList2.SelectedValue);
            dgd.DataBind();
        }

        public string GetToggleButton(string card, string status, string sid, string index)
        {
            var c = card.Split(',').ElementAtOrDefault(Convert.ToInt32(index) - 1);
            var s = status.Split(',').ElementAtOrDefault(Convert.ToInt32(index) - 1);

            if (!string.IsNullOrEmpty(c))
            {
                //return "<input class='card-status-toggle' type='checkbox'  data-sid='" + sid + "'  data-index='" + index + "'  data-toggle='toggle' data-on='เปิดใช้งาน' data-off='ปิดใช้งาน' data-onstyle='success' data-offstyle='danger' " + (s == "1" ? "checked='true'" : "") + ">";

                return $@" <label class='el-switch el-switch-lg'><input data-sid='" + sid + "'  data-index='" + index + "' class='card-status switch-button' type='checkbox' hidden data-toggle='toggle' data-on='เปิดใช้งาน' data-off='ปิดใช้งาน' data-onstyle='success' data-offstyle='danger' " + (s == "1" ? " checked='true' " : "") + " /><span class='el-switch-style'></span></label>";
            }

            return "";
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SwitchCardStatus(int sid, int index, bool status)
        {
            string keyword = "";
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var item = dbmaster.TUser_Card.Where(o => o.sID == sid && o.No == index && o.IsDel == false).FirstOrDefault();
                var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.nCompany == item.SchoolID);
                item.IsActive = status;

                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(f_company.nCompany, ConnectionDB.Read)))
                {



                    var q_user = dbSchool.TUser.Where(x => x.sID == sid).FirstOrDefault();

                    if (q_user != null)
                    {
                        keyword = q_user.sStudentID;
                    }



                    dbmaster.SaveChanges();

                    TopupTempCard topupTempCard = new TopupTempCard();
                    List<TUser_Card> user_Cards = dbmaster.TUser_Card.Where(w => w.sID == item.sID).ToList();
                    topupTempCard.AddOrModifyUserCard(user_Cards, item.SchoolID, userData.AuthKey, userData.AuthValue);

                    foreach (TUser_Card userCard in user_Cards)
                    {

                        keyword = q_user.sStudentID != null ? q_user.sStudentID.ToUpper() : "";

                        if (keyword == "")
                            keyword = userCard.NFC != null ? userCard.NFC.ToUpper() : "";
                        else
                            keyword += ' ' + userCard.NFC != null ? userCard.NFC.ToUpper() : "";

                        if (keyword == "")
                            keyword = userCard.NFCEncrypt != null ? userCard.NFCEncrypt.ToUpper() : "";
                        else
                            keyword += ' ' + userCard.NFCEncrypt != null ? userCard.NFCEncrypt.ToUpper() : "";

                        if (keyword == "")
                            keyword = userCard.NFCReverse != null ? userCard.NFCReverse.ToUpper() : "";
                        else
                            keyword += ' ' + userCard.NFCReverse != null ? userCard.NFCReverse.ToUpper() : "";

                        if (keyword == "")
                            keyword = userCard.NFCEncryptReverse != null ? userCard.NFCEncryptReverse.ToUpper() : "";
                        else
                            keyword += ' ' + userCard.NFCEncryptReverse != null ? userCard.NFCEncryptReverse.ToUpper() : "";


                        if (userCard.IsDel == false && userCard.IsActive == true)
                        {
                            JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(f_company.nCompany, sid + "", q_user.cType, keyword);
                        }

                    }

                    return true;
                }
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            OpenData();
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object IsCardValid(string card)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            try
            {
                card = fcommon.FormatStudentCardNumber(card).ToUpper();
            }
            catch
            {
                return false;
            }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var lst = dbmaster.TUser_Card
                    .Where(o => (o.NFCEncrypt.ToUpper() == card || o.NFCEncryptReverse.ToUpper() == card) && o.SchoolID == userData.CompanyID && o.IsDel == false)
                    .Select(o => o.sID)
                    .Distinct()
                    .ToList();

                if (lst.Count > 0)
                {
                    var user = dbmaster.TUsers
                        .Where(o => lst.Contains(o.sID) && (o.cDel ?? "0") == "0" && o.nCompany == userData.CompanyID)
                        .Select(o => o.sName + " " + o.sLastname)
                        .ToList();

                    if (user.Count > 0)
                        return new { isvalid = false, info = string.Join(",", user), type = "tuser" };
                }
            }

            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {
                var lst = ctx.TBackupCards
                    .Where(o => o.NFCEncrypt.ToUpper() == card && o.SchoolID == userData.CompanyID && o.cDel == false)
                    .Select(o => o.CardName)
                    .Distinct()
                    .ToList();

                if (lst.Count > 0)
                {
                    return new { isvalid = false, info = string.Join(",", lst), type = "backup" };
                }
            }

            return new { isvalid = true };
        }

        class studentlist2
        {
            //public int? sID { get; set; }
            //public string sName { get; set; }
            //public string note { get; set; }
            //public string sstudentid { get; set; }
            //public string number { get; set; }
            //public string year { get; set; }
            //public string idlv { get; set; }
            //public int? idlv2 { get; set; }
            //public string term { get; set; }
            //public int? sort2 { get; set; }
            //public int sort1int { get; set; }
            //public string sort1txt { get; set; }
            //public string stdId { get; set; }
            //public string stdYear { get; set; }
            //public string stdTerm { get; set; }
            //public DateTime? dBirth { get; set; }
            //public string NFC { get; set; }

            public int? nTermSubLevel2;
            public int? nTSubLevel;
            public int number { get; set; }
            public int? sNo { get; internal set; }
            public string sName { get; set; }
            public string sLastName { get; set; }
            public string sIdentification { get; set; }
            public int? sId { get; set; }
            public string SubLevel { get; set; }
            public int? nTSubLevel2 { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public bool? fingerStatus { get; set; }
            public string birthday { get; set; }
            public string classroom { get; set; }
            public string room { get; set; }
            public string timetype { get; set; }
            public string status { get; set; }
            public int? Year { get; set; }
            public string Card { get; internal set; }
            public string CardRe { get; set; }
            public string EnCard { get; internal set; }
            public string EnCardRe { get; set; }
            public string StatusCard { get; internal set; }
            public string UpdateBy { get; internal set; }
            public string UpdateDate { get; internal set; }
            public string FreeText { get; internal set; }
        }
        public class ddlterm
        {
            public string sTerm { get; set; }
        }
        public class ddlUser
        {
            public string sName { get; set; }
            public string sLast { get; set; }
        }
        public class CardModel
        {
            public byte index { get; set; }
            public string card { get; set; }
            public string cardEncrypt { get; set; }
            public string cardEncryptRevese
            {
                get
                {
                    if (!string.IsNullOrEmpty(cardEncrypt))
                    {
                        return fcommon.NFCEncryptRevese(cardEncrypt);
                    }

                    return "";
                }
            }
            public string cardRevese
            {
                get
                {
                    if (!string.IsNullOrEmpty(cardEncrypt))
                    {
                        var a = fcommon.NFCEncryptRevese(cardEncrypt);

                        return fcommon.DeFormatStudentCardNumber(a);

                    }

                    return "";
                }
            }
            public bool IsValid { get; set; }
            public string FreeText { get; set; }
        }
        public class CardRegisterModel
        {
            public int sID { get; set; }
            public string sFinger { get; set; }
            public string sFinger2 { get; set; }
            public DateTime? dBirth { get; set; }
            public string sEmail { get; set; }
            public string sPhone { get; set; }
            public string Card { get; set; }
            public string EnCard { get; set; }
            public string CardRe { get; set; }
            public string EnCardRe { get; set; }
            public string Status { get; set; }
            public int SchoolID { get; set; }
            public string sIdentification { get; set; }
            public string UpdateBy { get; set; }
            public string UpdateDate { get; set; }
            public string FreeText { get; set; }
        }

        protected void dgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
            }
        }




        //protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlType.SelectedValue == "1")
        //    {
        //        PanelStudent.Visible = true;
        //        PanelTeacher.Visible = false;
        //    }
        //    else
        //    {
        //        PanelStudent.Visible = false;
        //        PanelTeacher.Visible = true;
        //    }
        //}
    }
}