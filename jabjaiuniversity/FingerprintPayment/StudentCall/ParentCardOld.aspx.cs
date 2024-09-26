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

namespace FingerprintPayment.StudentCall
{
    //this code was copy from studentcardregister.aspx
    public partial class ParentCardOld : BaseStudentCall
    {

        public string BgFile { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //    JWTToken token = new JWTToken();
            //    var userData = new JWTToken().UserData;
            //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            //    if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            //    JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read);
            //    string comid = Request.QueryString["comid"];
            //    int sEmpID = int.Parse(Session["sEmpID"] + "");

            //    using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) { 
            //    SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());

            if (!IsPostBack)
            {
                //ImportOldData();

                ViewState["pagesize"] = 100;
                //JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read);

                //string type = Request.QueryString["type"];
                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string name = Request.QueryString["name"];
                //string year = Request.QueryString["year"];
                string clicked = Request.QueryString["c"];

                List<TSubLevel> SubLevel = new List<TSubLevel>();
                TSubLevel sub = new TSubLevel();

                using (var db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    SubLevel = db.TSubLevels.Where(w => w.SchoolID == UserData.CompanyID).Where(w => w.nWorkingStatus == 1).ToList();
                    //foreach (var a in db.TSubLevels.Where(w => w.SchoolID == UserData.CompanyID).Where(w => w.nWorkingStatus == 1))
                    //{
                    //    sub = new TSubLevel();
                    //    sub = a;
                    //    SubLevel.Add(sub);
                    //}
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

                if (string.IsNullOrEmpty(clicked))//(string.IsNullOrEmpty(idlv) && string.IsNullOrEmpty(idlv2) && string.IsNullOrEmpty(name))
                    return;

                OpenData();

                ddlsublevel.SelectedValue = idlv;
                ddlsublevel2.SelectedValue = idlv2;
                //ddlType.SelectedValue = type;
                tags.Text = name;
            }

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var c = dbmaster.TStudentCall_Config.FirstOrDefault(o => o.SchoolId == UserData.CompanyID);

                BgFile = c?.BgCard;
            }
        }


        private List<studentlist2> returnlist()
        {
            List<studentlist2> StudentList = new List<studentlist2>();

            //string type = Request.QueryString["type"] + "";
            string sname = Request.QueryString["name"];
            tags.Text = sname;
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //studentlist2 std = new studentlist2();

                // if (type == "1" || type == "")
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var query = string.Format(@"
--/studentcall/ParentCard.aspx/returnlist()[1]

  SELECT u.sID, u.sFinger, u.sFinger2, u.dBirth, u.sEmail, u.sPhone
  , ISNULL(C.NFC1, '') 'Card1' ,ISNULL(C.NFC2, '') 'Card2',ISNULL(C.NFC3, '') 'Card3'
  , ISNULL(C.IsActive1, '') 'Status1' , ISNULL(C.IsActive2, '') 'Status2' , ISNULL(C.IsActive3, '') 'Status3'
  ,  ISNULL(C.P1, '') 'Parent1' ,ISNULL(C.P2, '') 'Parent2',ISNULL(C.P3, '') 'Parent3'
FROM JabjaiMasterSingleDB.dbo.TUser u LEFT JOIN
(
	
	SELECT c.sID 
	, MAX(CASE WHEN c.[No] = 1 THEN 
	(CASE WHEN c.Type = 1 THEN b.sFatherFirstName + ' ' + b.sFatherLastName WHEN c.Type = 2 THEN b.sMotherFirstName + ' ' + b.sMotherLastName WHEN c.Type = 3 THEN b.sFamilyName + ' ' + b.sFamilyLast END) END) 'P1'
	, MAX(CASE WHEN c.[No] = 2 THEN 
	(CASE WHEN c.Type = 1 THEN b.sFatherFirstName + ' ' + b.sFatherLastName WHEN c.Type = 2 THEN b.sMotherFirstName + ' ' + b.sMotherLastName WHEN c.Type = 3 THEN b.sFamilyName + ' ' + b.sFamilyLast END) END) 'P2'
	, MAX(CASE WHEN c.[No] = 3 THEN 
	(CASE WHEN c.Type = 1 THEN b.sFatherFirstName + ' ' + b.sFatherLastName WHEN c.Type = 2 THEN b.sMotherFirstName + ' ' + b.sMotherLastName WHEN c.Type = 3 THEN b.sFamilyName + ' ' + b.sFamilyLast END) END) 'P3'
	, MAX(CASE WHEN c.[No] = 1 THEN c.NFC END) 'NFC1'
	, MAX(CASE WHEN c.[No] = 2 THEN c.NFC END) 'NFC2'
	, MAX(CASE WHEN c.[No] = 3 THEN c.NFC END) 'NFC3'
	, MAX(CASE WHEN c.[No] = 1 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive1'
	, MAX(CASE WHEN c.[No] = 2 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive2'
	, MAX(CASE WHEN c.[No] = 3 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive3'
	FROM JabjaiMasterSingleDB.dbo.TParent_Card c 
	LEFT JOIN [JabjaiSchoolSingleDB].[dbo].[TFamilyProfile] b on c.sid = b.sid
	WHERE c.SchoolID={0} AND c.IsDel=0 AND c.IsActive = 1 --AND ISNULL(c.NFC, '') <> ''
	GROUP BY c.sID
) C ON u.sID = C.sID
WHERE u.nCompany={0} AND u.cType='0' ", UserData.CompanyID);
                    var TUserMaster = dbmaster.Database.SqlQuery<CardRegisterModel>(query).ToList();

                    //var TUserMaster = (from a in dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0")//.ToList();
                    //                   from b in dbmaster.TUser_Card.Where(o => o.SchoolID == a.nCompany && o.sID == a.sID && o.IsDel == false).DefaultIfEmpty()
                    //                   select new
                    //                   {
                    //                       a.sID,
                    //                       a.sFinger,
                    //                       a.sFinger2,
                    //                       a.dBirth,
                    //                       a.sEmail,
                    //                       a.sPhone,
                    //                       b.No,
                    //                       b.NFC,
                    //                       IsActive = b.IsActive == true ? "1" : "0",
                    //                   })
                    //                  .GroupBy(o => new
                    //                  {
                    //                      o.sID,
                    //                  })
                    //                  .AsEnumerable()
                    //                  .Select(o => new
                    //                  {
                    //                      sID = o.Key.sID,
                    //                      sFinger = o.Max(i => i.sFinger),
                    //                      sFinger2 = o.Max(i => i.sFinger2),
                    //                      dBirth = o.Max(i => i.dBirth),
                    //                      sEmail = o.Max(i => i.sEmail),
                    //                      sPhone = o.Max(i => i.sPhone),
                    //                      Card = o.OrderBy(i => i.No).Select(i => i.NFC).Aggregate((a, b) => a + "," + b),
                    //                      Status = o.OrderBy(i => i.No).Select(i => i.IsActive).Aggregate((a, b) => a + "," + b),
                    //                  })
                    //                  .ToList();


                    using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
                    {
                        string ddlSubLV2 = Request.QueryString["idlv2"];

                        int idlv = int.Parse(string.IsNullOrEmpty(Request.QueryString["idlv"]) ? "0" : Request.QueryString["idlv"].ToString());
                        int idlv2 = int.Parse(string.IsNullOrEmpty(ddlSubLV2) ? "0" : ddlSubLV2);

                        var date = DateTime.Now;
                        ThaiBuddhistCalendar cal = new ThaiBuddhistCalendar();

                        var dStart = db.TTerms
                            .Where(f => f.dStart <= date && f.dEnd >= date && f.SchoolID == UserData.CompanyID && f.cDel == null)
                            .Select(s => s.dStart)
                            .FirstOrDefault();

                        if (dStart == null)
                            dStart = db.TTerms.Where(f => f.dStart >= date && f.SchoolID == UserData.CompanyID && f.cDel == null)
                                .OrderBy(o => o.dEnd)
                                .Select(s => s.dStart)
                                .FirstOrDefault();

                        if (dStart == null)
                            dStart = db.TTerms
                                .Where(f => f.dEnd <= date && f.SchoolID == UserData.CompanyID && f.cDel == null)
                                .OrderByDescending(o => o.dEnd)
                                .Select(s => s.dStart)
                                .FirstOrDefault();

                        dStart = dStart ?? DateTime.Now;
                        //var dStart = db.TTerms.Where(w => w.SchoolID == UserData.CompanyID).Where(c => c.dStart <= dateTime && c.dEnd >= dateTime).Select(s => (s.dStart.HasValue) ? (DateTime)s.dStart : DateTime.Now).FirstOrDefault();
                        DateTime thaiDateTime = new DateTime(cal.GetYear(dStart.Value), cal.GetMonth(dStart.Value), dStart.Value.Day);

                        var tUser = (from a in db.TB_StudentViews.Where(w => w.SchoolID == UserData.CompanyID)
                                     where (idlv == 0 || a.nTSubLevel == idlv) && (idlv2 == 0 || a.nTermSubLevel2 == idlv2) && a.numberYear == thaiDateTime.Year && (a.cDel ?? "0") != "1"
                                     select a).AsQueryable().ToList();

                        int rowindex = 1;
                        var data = (from a in tUser
                                    join b in db.TTermSubLevel2.Where(w => w.SchoolID == UserData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
                                    join c in db.TSubLevels.Where(w => w.SchoolID == UserData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
                                    join master in TUserMaster on a.sID equals master.sID
                                    join d in db.TTimetypes.Where(w => w.SchoolID == UserData.CompanyID) on b.nTimeType equals d.nTimeType into dd
                                    where a.numberYear == thaiDateTime.Year && (a.cDel ?? "0") != "1"
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
                                        birthday = master.dBirth.Value.ToString("dd/MM/yyyy"),
                                        email = master.sEmail,
                                        phone = master.sPhone,
                                        nTermSubLevel2 = a.nTermSubLevel2,
                                        nTSubLevel = c.nTSubLevel,
                                        timetype = jd == null ? "" : jd.sTimeType,
                                        classroom = c.SubLevel.Trim() + " / " + b.nTSubLevel2,
                                        status = student_status(a.nStudentStatus),
                                        Year = a.numberYear,
                                        Card = master.Card1,
                                        StatusCard = master.Status1,
                                        Receiver = master.Parent1,

                                    })
                                    .DistinctBy(row => row.sId)
                                    .AsQueryable()
                                    .ToList();


                        if (idlv2 > 0) data = data.Where(w => w.nTermSubLevel2 == idlv2).ToList();
                        else if (idlv > 0) data = data.Where(w => w.nTSubLevel == idlv).ToList();

                        if (!string.IsNullOrEmpty(sname)) data = data.Where(w => (w.sName + " " + w.sLastName).Contains(sname) || w.sIdentification.Contains(sname)).ToList();


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
                                           StatusCard = a.StatusCard,
                                           Receiver = a.Receiver,
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
            dgd.PageSize = (ViewState["pagesize"] != null && !string.IsNullOrEmpty(Convert.ToString(ViewState["pagesize"])) ? int.Parse(ViewState["pagesize"].ToString()) : 100);
            dgd.PagerSettings.Visible = false;

            dgd.DataBind();
        }

        protected void CustomersGridView_DataBound(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = dgd.BottomPagerRow;
            if (pagerRow != null)
            {
                // Retrieve the DropDownList and Label controls from the row.
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
                DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
                Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

                //int num = 20;
                //int num2 = 50;
                //int num3 = 100;
                int num = 100;
                int num2 = 200;
                int num3 = 300;

                ListItem item2 = new ListItem(num.ToString());
                pageList2.Items.Add(item2);
                if (num == dgd.PageSize)
                {
                    item2.Selected = true;
                }
                ListItem item3 = new ListItem(num2.ToString());
                pageList2.Items.Add(item3);
                if (num2 == dgd.PageSize)
                {
                    item3.Selected = true;
                }
                ListItem item4 = new ListItem(num3.ToString());
                pageList2.Items.Add(item4);
                if (num3 == dgd.PageSize)
                {
                    item4.Selected = true;
                }
                if (pageList != null)
                {

                    // Create the values for the DropDownList control based on 
                    // the  total number of pages required to display the data
                    // source.
                    for (int i = 0; i < dgd.PageCount; i++)
                    {

                        // Create a ListItem object to represent a page.
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());

                        // If the ListItem object matches the currently selected
                        // page, flag the ListItem object as being selected. Because
                        // the DropDownList control is recreated each time the pager
                        // row gets created, this will persist the selected item in
                        // the DropDownList control.   
                        if (i == dgd.PageIndex)
                        {
                            item.Selected = true;
                        }

                        // Add the ListItem object to the Items collection of the 
                        // DropDownList.
                        pageList.Items.Add(item);

                    }
                }

                if (pageLabel != null)
                {

                    // Calculate the current page number.
                    int currentPage = dgd.PageIndex + 1;

                    // Update the Label control with the current page information.
                    pageLabel.Text = "หน้าที่ " + currentPage.ToString() +
                      " จากทั้งหมด " + dgd.PageCount.ToString() + " หน้า ";

                }
            }
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
        public static object UpdateStudentCard(int studentId, string card1, string family)
        {
            try
            {
                var userData = GetUserData();
                if (studentId > 0)
                {
                    long cardNumber1, cardNumber2, cardNumber3;
                    bool isValidCard1 = long.TryParse(card1, out cardNumber1);
                    //bool isValidCard2 = long.TryParse(card2, out cardNumber2);
                    //bool isValidCard3 = long.TryParse(card3, out cardNumber3);

                    var lstCard = new List<CardModel>();
                    lstCard.Add(new CardModel()
                    {
                        index = 1,
                        cardEncrypt = isValidCard1 ? fcommon.FormatStudentCardNumber(card1) : "",
                        card = card1,
                        IsValid = isValidCard1,
                    });
                    //lstCard.Add(new CardModel()
                    //{
                    //    index = 2,
                    //    cardEncrypt = isValidCard2 ? fcommon.FormatStudentCardNumber(card2) : "",
                    //    card = card2,
                    //    IsValid = isValidCard2,
                    //});
                    //lstCard.Add(new CardModel()
                    //{
                    //    index = 3,
                    //    cardEncrypt = isValidCard3 ? fcommon.FormatStudentCardNumber(card3) : "",
                    //    card = card3,
                    //    IsValid = isValidCard3,
                    //});


                    using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
                    {

                        string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                        var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                        var q_usermaster = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.nSystemID == studentId).FirstOrDefault();

                        var _cards = _dbMaster.TParent_Card.Where(o => o.SchoolID == userData.CompanyID && o.sID == studentId && o.IsDel == false).ToList();
                        var _str = new List<string>();
                        var nfcCardOnForm = new List<string>();
                        foreach (var c in lstCard)
                        {
                            if (!string.IsNullOrEmpty(c.card))
                            {
                                var objOldCard = _cards.Where(o => o.SchoolID == userData.CompanyID && o.sID == studentId && o.No == c.index && o.IsDel == false).FirstOrDefault();
                                var objCard = _cards.Where(o => o.SchoolID == userData.CompanyID && o.sID == studentId && o.NFCEncrypt?.ToUpper() == c.cardEncrypt?.ToUpper() && o.IsDel == false).FirstOrDefault();
                                var cardCountInSchool = _dbMaster.TParent_Card.Where(o => o.SchoolID == userData.CompanyID && o.NFCEncrypt.ToUpper() == c.cardEncrypt.ToUpper() && o.IsDel == false).Count();
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

                                    _dbMaster.TParent_Card.Add(new TParent_Card
                                    {
                                        sID = studentId,
                                        SchoolID = userData.CompanyID,
                                        No = c.index,
                                        NFC = c.card,
                                        NFCEncrypt = c.cardEncrypt,
                                        Created = DateTime.Now,
                                        Modified = DateTime.Now,
                                        CreateBy = userData.UserID,
                                        ModifyBy = userData.UserID,
                                        IsDel = false,
                                        IsActive = true,
                                        ParentName = family,
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

                                        var userCard = _dbMaster.TParent_Card
                                            .Where(w => w.SchoolID == userData.CompanyID && w.sID == studentId && w.NFCEncrypt == c.cardEncrypt)
                                            .FirstOrDefault();

                                        if (userCard != null)
                                        {
                                            userCard.No = c.index;
                                            userCard.Modified = DateTime.Now;
                                            userCard.ModifyBy = userData.UserID;
                                            userCard.ParentName = family;
                                        }
                                    }
                                }
                            }
                        }

                        // Set IsDel = false that not used
                        _dbMaster.TParent_Card.Where(w => w.SchoolID == userData.CompanyID && w.sID == studentId && !nfcCardOnForm.Contains(w.NFCEncrypt) && w.IsDel == false)
                            .ToList().ForEach(e => { e.IsDel = true; e.IsActive = false; e.ModifyBy = userData.UserID; e.Modified = DateTime.Now; });

                        _dbMaster.SaveChanges();

                        database.InsertLog(userData.UserID + "", studentId + " " + string.Join(",", _str), userData.Entities, HttpContext.Current.Request,
                            (q_usermaster.cType == "0" ? 14 : 13)
                            , 0, 0);

                        //TopupTempCard topupTempCard = new TopupTempCard();
                        //List<TParent_Card> user_Cards = _dbMaster.TParent_Card.Where(w => w.sID == studentId).ToList();
                        //topupTempCard.AddOrModifyUserCard(user_Cards, UserData.CompanyID);
                        //JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(UserData.CompanyID, studentId + "", "1");

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
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
                return "<input class='card-status-toggle' type='checkbox'  data-sid='" + sid + "'  data-index='" + index + "'  data-toggle='toggle' data-on='เปิดใช้งาน' data-off='ปิดใช้งาน' data-onstyle='success' data-offstyle='danger' " + (s == "1" ? "checked='true'" : "") + ">";
            }

            return "";
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SwitchCardStatus(int sid, int index, bool status)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var item = dbmaster.TParent_Card.Where(o => o.sID == sid && o.No == index && o.IsDel == false).FirstOrDefault();
                var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.nCompany == item.SchoolID);
                item.IsActive = status;

                dbmaster.SaveChanges();

                //TopupTempCard topupTempCard = new TopupTempCard();
                //List<TParent_Card> user_Cards = dbmaster.TParent_Card.Where(w => w.sID == item.sID).ToList();
                //topupTempCard.AddOrModifyUserCard(user_Cards, item.SchoolID);
                //JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(f_company.nCompany, sid + "", "1");

                return true;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object GetFamilyList(int sid)
        {
            var userData = GetUserData();

            using (var dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var family = dbSchool.TFamilyProfiles.Where(o => o.sID == sid)
                    .Select(o => new
                    {
                        father = o.sFatherFirstName + " " + o.sFatherLastName + "",
                        mother = o.sMotherFirstName + " " + o.sMotherLastName + "",
                        family = o.sFamilyName + " " + o.sFamilyLast + "",
                    }).FirstOrDefault();

                return family;
            }
        }

        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static bool UploadFile(string fileName, byte[] fileContent)
        //{
        //    //string filesDirectory = ConfigurationManager.AppSettings["store_directory"];
        //    //System.IO.File.WriteAllBytes(string.Format("{0}{1}", filesDirectory, fileName), fileContent);

        //    return true;
        //}

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            OpenData();
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
            public bool fingerStatus { get; set; }
            public string birthday { get; set; }
            public string classroom { get; set; }
            public string room { get; set; }
            public string timetype { get; set; }
            public string status { get; set; }
            public int? Year { get; set; }
            public string Card { get; internal set; }
            public string StatusCard { get; internal set; }
            public string Receiver { get; internal set; }
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
            public bool IsValid { get; set; }
        }

        public class CardRegisterModel
        {
            public int sID { get; set; }
            public string sFinger { get; set; }
            public string sFinger2 { get; set; }
            public DateTime? dBirth { get; set; }
            public string sEmail { get; set; }
            public string sPhone { get; set; }
            public string Card1 { get; set; }
            public string Card2 { get; set; }
            public string Card3 { get; set; }
            public string Status1 { get; set; }
            public string Status2 { get; set; }
            public string Status3 { get; set; }
            public int SchoolID { get; set; }
            public string sIdentification { get; set; }
            public string Parent1 { get; set; }
            public string Parent2 { get; set; }
            public string Parent3 { get; set; }

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static bool IsCardValid(string card)
        {

            try
            {
                card = fcommon.FormatStudentCardNumber(card).ToUpper();
            }
            catch
            {
                return false;
            }

            var userData = GetUserData();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var count = dbmaster.TParent_Card
                    .Where(o => o.NFCEncrypt.ToUpper() == card && o.SchoolID == userData.CompanyID && o.IsDel == false)
                    .Count();

                if (count > 0)
                {
                    return false;
                }
            }

            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var count = ctx.TBackupCards
                    .Where(o => o.NFCEncrypt.ToUpper() == card && o.SchoolID == userData.CompanyID && o.cDel == false)
                    .Count();

                if (count > 0)
                {
                    return false;
                }
            }

            return true;
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