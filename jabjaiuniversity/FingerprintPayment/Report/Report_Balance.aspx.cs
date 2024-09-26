using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;

using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    public partial class Report_Balance : BehaviorGateway
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

                    using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        string sEntities = Session["sEntities"].ToString();
                        var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                        hdfschoolname.Value = tCompany.sCompany;
                        var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)), userData);

                        fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");

                        List<TYear> tylist = new List<TYear>();
                        TYear ty = new TYear();
                        foreach (var a in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).ToList())
                        {
                            ty = new TYear();
                            ty = a;
                            tylist.Add(ty);
                        }
                        var newList = tylist.OrderByDescending(x => x.numberYear).ToList();
                        ddlYear.DataSource = newList;
                        ddlYear.DataTextField = "numberYear";
                        ddlYear.DataValueField = "numberYear";
                        ddlYear.DataBind();
                    }
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object Report_Student_Balance(Search search)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            var userData = GetUserData();
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            using (var historydb = new JabjaiSchoolHistoryEntity.JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
            {
                List<NewMessage> newMessages = new List<NewMessage>();

                //search.tErm_Id = string.Format("TS{0:0000000}", int.Parse(search.tErm_Id));
                var titles = db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                DateTime? Dstart = search.dStart.Value.AddDays(+1);

                //student
                if (search.rRport_tYpe == 0)
                {
                    //search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("MM/dd/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));

                    var StudentData = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                       join c in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals c.nTermSubLevel2
                                       join d in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on c.nTSubLevel equals d.nTSubLevel
                                       join h in db.TLevels.Where(w => w.SchoolID == userData.CompanyID) on d.nTLevel equals h.LevelID
                                       where (!search.sUbLV_Id.HasValue || search.sUbLV_Id == d.nTSubLevel)
                                       && (!search.sUbLV2_Id.HasValue || search.sUbLV2_Id == c.nTermSubLevel2)
                                       select new
                                       {
                                           a.sID,
                                           a.sStudentID,
                                           a.nStudentNumber,
                                           a.sStudentTitle,
                                           a.sName,
                                           a.sLastname,

                                           groupclassID = h.LevelID,
                                           groupsuperName = h.LevelName,
                                           groupsortValue = h.sortValue,
                                           classid = d.nTSubLevel,
                                           classname = d.SubLevel,
                                           roomid = c.nTermSubLevel2,
                                           roomname = c.nTSubLevel2
                                       }).ToList();

                    var MessageList = (from a in db.TMessage_User
                                       join b in db.TMessageBoxes on a.message_id equals b.nMessageID
                                       where a.SchoolID == userData.CompanyID && b.SchoolID == userData.CompanyID && b.cDel == false
                                       && (b.dSend.Value <= Dstart)
                                       && (b.nType == 2 || b.nType == 3 || b.nType == 12)
                                       && (a.user_type == search.rRport_tYpe)
                                       select new
                                       {
                                           a.UserID,
                                           a.user_type,
                                           b.nMessageID,
                                           b.nType,
                                           b.dSend,
                                           b.sMessage
                                       }).ToList();

                    var historyMessageList = (from a in db.TMessage_User
                                              join b in db.TMessageBoxes on a.message_id equals b.nMessageID
                                              where a.SchoolID == userData.CompanyID && b.SchoolID == userData.CompanyID && b.cDel == false
                                                     && (b.dSend.Value <= Dstart && b.dSend.Value.Year == search.numberYear)
                                              && (b.nType == 2 || b.nType == 3 || b.nType == 12)
                                              && (a.user_type == search.rRport_tYpe)
                                              select new
                                              {
                                                  a.UserID,
                                                  a.user_type,
                                                  b.nMessageID,
                                                  b.nType,
                                                  b.dSend,
                                                  b.sMessage
                                              }).ToList();

                    if (historyMessageList != null && historyMessageList.Count > 0)
                    {
                        MessageList.AddRange(historyMessageList);
                    }

                    char[] separating1 = { ',' };
                    char[] separating2 = { '.' };
                    foreach (var data in MessageList)
                    {
                        string[] Message = data.sMessage.Split(separating1);
                        var MessageMoney = Message[0];
                        var MessageBalance = Message[1];

                        string[] Balance = MessageBalance.Split(separating2);
                        var BalanceArray0 = Balance[0];

                        newMessages.Add(new NewMessage
                        {
                            UserID = data.UserID,
                            UserType = data.user_type,
                            MessageID = data.nMessageID,
                            MessageType = data.nType,
                            dSend = data.dSend,
                            Money = MessageMoney,
                            Balance = BalanceArray0
                        });
                    }

                    var StudentMessage = (from a in newMessages
                                          join b in StudentData on new { userid = a.UserID, usertype = a.UserType }
                                          equals new { userid = b.sID, usertype = 0 }
                                          select new
                                          {
                                              a.UserID,
                                              a.UserType,
                                              a.dSend,
                                              a.MessageID,
                                              a.MessageType,
                                              a.Money,
                                              a.Balance,

                                              b.sID,
                                              b.sStudentID,
                                              b.nStudentNumber,
                                              b.sStudentTitle,
                                              b.sName,
                                              b.sLastname,
                                              b.groupclassID,
                                              b.groupsuperName,
                                              b.classid,
                                              b.classname,
                                              b.roomid,
                                              b.roomname
                                          }).ToList();

                    if (!string.IsNullOrEmpty(search.StudentFullName))
                    {
                        StudentMessage = StudentMessage.Where(w => (w.sName + " " + w.sLastname).Contains(search.StudentFullName)).ToList();
                    }

                    var LaYer0s = (from a in StudentMessage
                                   group a by new { a.groupclassID, a.classid, a.classname } into gb0
                                   select new LaYer0
                                   {
                                       ClassID = gb0.Key.classid,
                                       ClassName = gb0.Key.classname,
                                       LaYer1s = (from a in gb0
                                                  where a.classid == gb0.Key.classid
                                                  group a by new { a.roomid, a.roomname } into gb1
                                                  select new LaYer1
                                                  {
                                                      RoomName = gb1.Key.roomname,
                                                      RoomFullName = gb0.Key.classname + "/" + gb1.Key.roomname,
                                                      LaYer2s = (from a in gb1
                                                                 where a.roomid == gb1.Key.roomid
                                                                 group a by new { a.UserID, a.sID, a.nStudentNumber, a.sStudentID } into gb2
                                                                 orderby gb2.Key.nStudentNumber, gb2.Key.sStudentID.Length, gb2.Key.sStudentID
                                                                 select new LaYer2
                                                                 {
                                                                     StudentNumber = gb2.Key.nStudentNumber,
                                                                     StudentID = gb2.Key.sStudentID,
                                                                     LaYer3s = (from a in gb2
                                                                                where a.MessageID == gb2.Max(m => m.MessageID)
                                                                                group a by new { a.sStudentID, a.sStudentTitle, a.sName, a.sLastname, a.dSend, a.Balance } into gb3
                                                                                select new LaYer3
                                                                                {
                                                                                    StudentID = gb3.Key.sStudentID,
                                                                                    StudentTitle = getTitlte(titles, gb3.Key.sStudentTitle),
                                                                                    StudentFullName = gb3.Key.sName + " " + gb3.Key.sLastname,
                                                                                    StudentdSend = gb3.Key.dSend.Value.ToString("dd/MM/yyyy"),
                                                                                    StudentBalance = Convert.ToDecimal(gb3.Key.Balance)
                                                                                }).ToList()
                                                                 }).ToList()
                                                  }).OrderBy(o => o.RoomName, new SemiNumericComparer()).ToList()
                                   }).OrderBy(o => o.ClassName, new SemiNumericComparer()).ToList();

                    var HeaderText = "รายงานจำนวนยอดเงินคงเหลือนักเรียน";

                    return new view
                    {
                        LaYer0s = LaYer0s,
                        HeaderText = HeaderText
                    };

                }
                else if (search.rRport_tYpe == 1)
                {

                    var MessageList = (from a in db.TMessage_User
                                       join b in db.TMessageBoxes on a.message_id equals b.nMessageID
                                       where a.SchoolID == userData.CompanyID && b.SchoolID == userData.CompanyID && b.cDel == false
                                       && (b.dSend.Value <= Dstart)
                                       && (b.nType == 2 || b.nType == 3 || b.nType == 12)
                                       && (a.user_type == search.rRport_tYpe)

                                       orderby a.UserID, b.dSend
                                       select new
                                       {
                                           a.UserID,
                                           a.user_type,
                                           b.nMessageID,
                                           b.sMessage,
                                           b.nType,
                                           b.dSend
                                       }).ToList();

                    var historyMessageList = (from a in db.TMessage_User
                                              join b in db.TMessageBoxes on a.message_id equals b.nMessageID
                                              where a.SchoolID == userData.CompanyID && b.SchoolID == userData.CompanyID && b.cDel == false
                                                     && (b.dSend.Value <= Dstart)
                                              && (b.nType == 2 || b.nType == 3 || b.nType == 12)
                                              && (a.user_type == search.rRport_tYpe)

                                              orderby a.UserID, b.dSend
                                              select new
                                              {
                                                  a.UserID,
                                                  a.user_type,
                                                  b.nMessageID,
                                                  b.sMessage,
                                                  b.nType,
                                                  b.dSend
                                              }).ToList();
                    if (historyMessageList != null && historyMessageList.Count > 0)
                    {
                        MessageList.AddRange(historyMessageList);
                    }

                    char[] separating1 = { ',' };
                    char[] separating2 = { '.' };
                    foreach (var data in MessageList)
                    {
                        string[] Message = data.sMessage.Split(separating1);
                        var MessageMoney = Message[0];
                        var MessageBalance = Message[1];

                        string[] Balance = MessageBalance.Split(separating2);
                        var BalanceArray0 = Balance[0];

                        newMessages.Add(new NewMessage
                        {
                            UserID = data.UserID,
                            UserType = data.user_type,
                            MessageID = data.nMessageID,
                            MessageType = data.nType,
                            dSend = data.dSend,
                            Money = MessageMoney,
                            Balance = BalanceArray0
                        });
                    }

                    var EmployeeMessage = (from a in newMessages
                                           join c in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on new { userid = a.UserID, usertype = a.UserType }
                                           equals new { userid = c.sEmp, usertype = 1 }

                                           select new
                                           {
                                               a.UserID,
                                               a.UserType,
                                               a.dSend,
                                               a.MessageID,
                                               a.MessageType,
                                               a.Money,
                                               a.Balance,
                                               c.sEmp,
                                               c.sTitle,
                                               c.sName,
                                               c.sLastname
                                           }).ToList();

                    if (!string.IsNullOrEmpty(search.EmpFullName))
                    {
                        EmployeeMessage = EmployeeMessage.Where(w => (w.sName + " " + w.sLastname).Contains(search.EmpFullName)).ToList();
                    }

                    var empLevel0s = (from a in EmployeeMessage
                                      group a by new { a.UserID, a.sEmp } into gb0
                                      orderby gb0.Key.sEmp
                                      select new EmpLevel0
                                      {
                                          UserID = gb0.Key.UserID,
                                          empLevel1s = (from a in gb0
                                                        where a.MessageID == gb0.Max(m => m.MessageID)
                                                        group a by new { a.sEmp, a.sTitle, a.dSend, a.Balance, a.sName, a.sLastname } into gb1
                                                        select new EmpLevel1
                                                        {
                                                            EmpID = gb1.Key.sEmp,
                                                            EmpdSend = gb1.Key.dSend.Value.ToString("dd/MM/yyyy"),
                                                            EmpTitle = getTitlte(titles, gb1.Key.sTitle),
                                                            EmpFullName = gb1.Key.sName + " " + gb1.Key.sLastname,
                                                            EmpBalance = Convert.ToDecimal(gb1.Key.Balance)
                                                        }).ToList()
                                      }).ToList();

                    var EmpHeaderText = "รายงานจำนวนยอดเงินคงเหลือบุคลากร";

                    return new viewEmp
                    {
                        empLevel0s = empLevel0s,
                        EmpHeaderText = EmpHeaderText
                    };
                }
                else
                {
                    //Get the Thai Year
                    var searchDate = (search.dStart != null) ? DateTime.Now : (DateTime)search.dStart;
                    ThaiBuddhistCalendar cal = new ThaiBuddhistCalendar();
                    var searchYear = cal.GetYear(searchDate);

                    var previouseYearterm = ServiceHelper.GetTermsByNumberYear((search.numberYear - 1), false, userData.CompanyID);

                    var term = ServiceHelper.GetTermsByNumberYear(search.numberYear, false, userData.CompanyID);

                    var lastDateOfAcademicYear = new DateTime();
                    var firstDateOfAcademicYear = new DateTime();

                    if (term != null && term.Count > 0)
                    {
                        //Ex: 2563 - Term 2 End date is March 31 - 2021.   2564 Term 1 Start date is May 20 - 2021.
                        //Is there any transaction may available between this date then will be included in the next academic year
                        if (previouseYearterm != null && previouseYearterm.Count > 1)
                        {
                            firstDateOfAcademicYear = previouseYearterm[1].dEnd ?? new DateTime();
                            firstDateOfAcademicYear = firstDateOfAcademicYear.AddDays(+1);
                        }
                        else if (previouseYearterm != null && previouseYearterm.Count == 1)
                        {
                            firstDateOfAcademicYear = previouseYearterm[0].dEnd ?? new DateTime();
                            firstDateOfAcademicYear = firstDateOfAcademicYear.AddDays(+1);
                        }
                        else
                        {
                            firstDateOfAcademicYear = term[0].dStart ?? new DateTime();
                        }
                        if (term.Count > 1)
                        {
                            lastDateOfAcademicYear = term[1].dEnd ?? new DateTime();
                        }
                        else
                        {
                            lastDateOfAcademicYear = term[0].dEnd ?? new DateTime();
                        }
                    }


                    if (search.numberYear != searchYear)
                    {
                        search.dStart = lastDateOfAcademicYear;
                        Dstart = lastDateOfAcademicYear.AddDays(+1);
                    }

                    var tbalanceAmountLastYear = db.GetSchoolBalanceAmount(userData.CompanyID, firstDateOfAcademicYear, (search.rRport_tYpe == 3) ? false : true).ToList();

                    var tbalanceAmount = db.GetSchoolBalanceAmount(userData.CompanyID, Dstart, (search.rRport_tYpe == 3) ? false : true).ToList();

                    var balanceAmounts = new List<BalanceAmount>();
                    if (tbalanceAmountLastYear != null && tbalanceAmount != null)
                    {

                        balanceAmounts = (from a in tbalanceAmount
                                          join ly in tbalanceAmountLastYear on a.UserID equals ly.UserID into balance
                                          from b in balance.DefaultIfEmpty()
                                          select new BalanceAmount
                                          {
                                              TotalBalanceTopUp = (a.TotalBalanceTopUp ?? 0) - (b?.TotalBalanceTopUp ?? 0),
                                              TotalSales = (a.TotalSales ?? 0) - (b?.TotalSales ?? 0),
                                              TotalWithdrawal = (a.TotalWithdrawal ?? 0) - (b?.TotalWithdrawal ?? 0),
                                              cType = a.cType ?? 0,
                                              FinalBalance = a.BalanceAmount ?? 0,
                                              DateToday = search.dStart.Value.ToString("dd/MM/yyyy"),
                                              StudentID = a.StudentID ?? string.Empty,
                                              FullName = a.FullName,
                                              UserID = a.UserID ?? 0,
                                          }).ToList();
                        //balanceAmounts = (from a in tbalanceAmount
                        //                  select new BalanceAmount
                        //                  {
                        //                      TotalBalanceTopUp = a.TotalBalanceTopUp ?? 0,
                        //                      TotalSales = a.TotalSales ?? 0,
                        //                      TotalWithdrawal = a.TotalWithdrawal ?? 0,
                        //                      cType = a.cType ?? 0,
                        //                      FinalBalance = a.BalanceAmount ?? 0,
                        //                      DateToday = (search.numberYear != search.dStart.Value.Year) ? lastDateOfYear.ToString("dd/MM/yyyy") :  search.dStart.Value.ToString("dd/MM/yyyy"),
                        //                      StudentID = a.StudentID ?? string.Empty,
                        //                      FullName = a.FullName,
                        //                      UserID = a.UserID ?? 0,
                        //                  }).ToList();
                    }

                    var listHeaderText = "รายงานรวมยอดเงินคงเหลือ";

                    return new ViewDataList
                    {
                        listHeaderText = listHeaderText,
                        balanceAmounts = balanceAmounts
                    };
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object Report_Student_Balance_old(Search search)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            var userData = GetUserData();
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {

                List<NewMessage> newMessages = new List<NewMessage>();

                //search.tErm_Id = string.Format("TS{0:0000000}", int.Parse(search.tErm_Id));
                var titles = db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                DateTime? Dstart = search.dStart.Value.AddDays(+1);

                //student
                if (search.rRport_tYpe == 0)
                {
                    //search.dStart = DateTime.ParseExact(search.dStart.Value.ToString("MM/dd/yyyy"), "dd/MM/yyyy", new CultureInfo("en-us"));

                    var StudentData = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                       join c in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals c.nTermSubLevel2
                                       join d in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on c.nTSubLevel equals d.nTSubLevel
                                       join h in db.TLevels.Where(w => w.SchoolID == userData.CompanyID) on d.nTLevel equals h.LevelID
                                       where (!search.sUbLV_Id.HasValue || search.sUbLV_Id == d.nTSubLevel)
                                       && (!search.sUbLV2_Id.HasValue || search.sUbLV2_Id == c.nTermSubLevel2)
                                       select new
                                       {
                                           a.sID,
                                           a.sStudentID,
                                           a.nStudentNumber,
                                           a.sStudentTitle,
                                           a.sName,
                                           a.sLastname,

                                           groupclassID = h.LevelID,
                                           groupsuperName = h.LevelName,
                                           groupsortValue = h.sortValue,
                                           classid = d.nTSubLevel,
                                           classname = d.SubLevel,
                                           roomid = c.nTermSubLevel2,
                                           roomname = c.nTSubLevel2
                                       }).ToList();

                    var MessageList = (from a in db.TMessage_User
                                       join b in db.TMessageBoxes on a.message_id equals b.nMessageID
                                       where a.SchoolID == userData.CompanyID && b.SchoolID == userData.CompanyID && b.cDel == false
                                       && (b.dSend.Value <= Dstart)
                                       && (b.nType == 2 || b.nType == 3 || b.nType == 12)
                                       && (a.user_type == search.rRport_tYpe)
                                       select new
                                       {
                                           a.UserID,
                                           a.user_type,
                                           b.nMessageID,
                                           b.nType,
                                           b.dSend,
                                           b.sMessage
                                       }).ToList();

                    char[] separating1 = { ',' };
                    char[] separating2 = { '.' };
                    foreach (var data in MessageList)
                    {
                        string[] Message = data.sMessage.Split(separating1);
                        var MessageMoney = Message[0];
                        var MessageBalance = Message[1];

                        string[] Balance = MessageBalance.Split(separating2);
                        var BalanceArray0 = Balance[0];

                        newMessages.Add(new NewMessage
                        {
                            UserID = data.UserID,
                            UserType = data.user_type,
                            MessageID = data.nMessageID,
                            MessageType = data.nType,
                            dSend = data.dSend,
                            Money = MessageMoney,
                            Balance = BalanceArray0
                        });
                    }

                    var StudentMessage = (from a in newMessages
                                          join b in StudentData on new { userid = a.UserID, usertype = a.UserType }
                                          equals new { userid = b.sID, usertype = 0 }
                                          select new
                                          {
                                              a.UserID,
                                              a.UserType,
                                              a.dSend,
                                              a.MessageID,
                                              a.MessageType,
                                              a.Money,
                                              a.Balance,

                                              b.sID,
                                              b.sStudentID,
                                              b.nStudentNumber,
                                              b.sStudentTitle,
                                              b.sName,
                                              b.sLastname,
                                              b.groupclassID,
                                              b.groupsuperName,
                                              b.classid,
                                              b.classname,
                                              b.roomid,
                                              b.roomname
                                          }).ToList();

                    if (!string.IsNullOrEmpty(search.StudentFullName))
                    {
                        StudentMessage = StudentMessage.Where(w => (w.sName + " " + w.sLastname).Contains(search.StudentFullName)).ToList();
                    }

                    var LaYer0s = (from a in StudentMessage
                                   group a by new { a.groupclassID, a.classid, a.classname } into gb0
                                   select new LaYer0
                                   {
                                       ClassID = gb0.Key.classid,
                                       ClassName = gb0.Key.classname,
                                       LaYer1s = (from a in gb0
                                                  where a.classid == gb0.Key.classid
                                                  group a by new { a.roomid, a.roomname } into gb1
                                                  select new LaYer1
                                                  {
                                                      RoomName = gb1.Key.roomname,
                                                      RoomFullName = gb0.Key.classname + "/" + gb1.Key.roomname,
                                                      LaYer2s = (from a in gb1
                                                                 where a.roomid == gb1.Key.roomid
                                                                 group a by new { a.UserID, a.sID, a.nStudentNumber, a.sStudentID } into gb2
                                                                 orderby gb2.Key.nStudentNumber, gb2.Key.sStudentID.Length, gb2.Key.sStudentID
                                                                 select new LaYer2
                                                                 {
                                                                     StudentNumber = gb2.Key.nStudentNumber,
                                                                     StudentID = gb2.Key.sStudentID,
                                                                     LaYer3s = (from a in gb2
                                                                                where a.MessageID == gb2.Max(m => m.MessageID)
                                                                                group a by new { a.sStudentID, a.sStudentTitle, a.sName, a.sLastname, a.dSend, a.Balance } into gb3
                                                                                select new LaYer3
                                                                                {
                                                                                    StudentID = gb3.Key.sStudentID,
                                                                                    StudentTitle = getTitlte(titles, gb3.Key.sStudentTitle),
                                                                                    StudentFullName = gb3.Key.sName + " " + gb3.Key.sLastname,
                                                                                    StudentdSend = gb3.Key.dSend.Value.ToString("dd/MM/yyyy"),
                                                                                    StudentBalance = Convert.ToDecimal(gb3.Key.Balance)
                                                                                }).ToList()
                                                                 }).ToList()
                                                  }).OrderBy(o => o.RoomName, new SemiNumericComparer()).ToList()
                                   }).OrderBy(o => o.ClassName, new SemiNumericComparer()).ToList();

                    var HeaderText = "รายงานจำนวนยอดเงินคงเหลือนักเรียน";

                    return new view
                    {
                        LaYer0s = LaYer0s,
                        HeaderText = HeaderText
                    };

                }
                else if (search.rRport_tYpe == 1)
                {

                    var MessageList = (from a in db.TMessage_User
                                       join b in db.TMessageBoxes on a.message_id equals b.nMessageID
                                       where a.SchoolID == userData.CompanyID && b.SchoolID == userData.CompanyID && b.cDel == false
                                       && (b.dSend.Value <= Dstart)
                                       && (b.nType == 2 || b.nType == 3 || b.nType == 12)
                                       && (a.user_type == search.rRport_tYpe)

                                       orderby a.UserID, b.dSend
                                       select new
                                       {
                                           a.UserID,
                                           a.user_type,
                                           b.nMessageID,
                                           b.sMessage,
                                           b.nType,
                                           b.dSend
                                       }).ToList();

                    char[] separating1 = { ',' };
                    char[] separating2 = { '.' };
                    foreach (var data in MessageList)
                    {
                        string[] Message = data.sMessage.Split(separating1);
                        var MessageMoney = Message[0];
                        var MessageBalance = Message[1];

                        string[] Balance = MessageBalance.Split(separating2);
                        var BalanceArray0 = Balance[0];

                        newMessages.Add(new NewMessage
                        {
                            UserID = data.UserID,
                            UserType = data.user_type,
                            MessageID = data.nMessageID,
                            MessageType = data.nType,
                            dSend = data.dSend,
                            Money = MessageMoney,
                            Balance = BalanceArray0
                        });
                    }

                    var EmployeeMessage = (from a in newMessages
                                           join c in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on new { userid = a.UserID, usertype = a.UserType }
                                           equals new { userid = c.sEmp, usertype = 1 }

                                           select new
                                           {
                                               a.UserID,
                                               a.UserType,
                                               a.dSend,
                                               a.MessageID,
                                               a.MessageType,
                                               a.Money,
                                               a.Balance,
                                               c.sEmp,
                                               c.sTitle,
                                               c.sName,
                                               c.sLastname
                                           }).ToList();

                    if (!string.IsNullOrEmpty(search.EmpFullName))
                    {
                        EmployeeMessage = EmployeeMessage.Where(w => (w.sName + " " + w.sLastname).Contains(search.EmpFullName)).ToList();
                    }

                    var empLevel0s = (from a in EmployeeMessage
                                      group a by new { a.UserID, a.sEmp } into gb0
                                      orderby gb0.Key.sEmp
                                      select new EmpLevel0
                                      {
                                          UserID = gb0.Key.UserID,
                                          empLevel1s = (from a in gb0
                                                        where a.MessageID == gb0.Max(m => m.MessageID)
                                                        group a by new { a.sEmp, a.sTitle, a.dSend, a.Balance, a.sName, a.sLastname } into gb1
                                                        select new EmpLevel1
                                                        {
                                                            EmpID = gb1.Key.sEmp,
                                                            EmpdSend = gb1.Key.dSend.Value.ToString("dd/MM/yyyy"),
                                                            EmpTitle = getTitlte(titles, gb1.Key.sTitle),
                                                            EmpFullName = gb1.Key.sName + " " + gb1.Key.sLastname,
                                                            EmpBalance = Convert.ToDecimal(gb1.Key.Balance)
                                                        }).ToList()
                                      }).ToList();

                    var EmpHeaderText = "รายงานจำนวนยอดเงินคงเหลือบุคลากร";

                    return new viewEmp
                    {
                        empLevel0s = empLevel0s,
                        EmpHeaderText = EmpHeaderText
                    };
                }
                else
                {

                    List<UserListLevel> userListLevels = new List<UserListLevel>();

                    var money = db.TMoneys.Where(w => w.dMoney <= Dstart && w.dayCancal == null && w.SchoolID == userData.CompanyID).Sum(s => s.nMoney);
                    var SumMoney = money == null ? 0 : money;

                    var sale = db.TSells.Where(w => w.dSell <= Dstart && w.dayCancal == null && w.SchoolID == userData.CompanyID).Sum(s => s.nTotal);
                    var SumSale = sale == null ? 0 : sale;

                    var Withdrawal = db.TWithdrawals.Where(w => w.dWithdrawal <= Dstart && w.dCanCel == null && w.SchoolID == userData.CompanyID).Sum(s => s.nMoney);
                    var SumWithdrawal = Withdrawal == null ? 0 : Withdrawal;

                    var BalanceTotal = (SumMoney - SumSale + SumWithdrawal);


                    //----------------------


                    //var MessageList = (from a in db.TMessage_User
                    //                   join b in db.TMessageBoxes on a.message_id equals b.nMessageID

                    //                   where (b.dSend.Value <= Dstart)
                    //                   && (b.nType == 2 || b.nType == 3 || b.nType == 12)

                    //                   orderby a.UserID, b.dSend
                    //                   select new
                    //                   {
                    //                       a.UserID,
                    //                       a.user_type,
                    //                       b.nMessageID,
                    //                       b.sMessage,
                    //                       b.nType,
                    //                       b.dSend
                    //                   }).ToList();

                    //char[] separating1 = { ',' };
                    //char[] separating2 = { '.' };
                    //foreach (var data in MessageList)
                    //{
                    //    string[] Message = data.sMessage.Split(separating1);
                    //    var MessageMoney = Message[0];
                    //    var MessageBalance = Message[1];

                    //    string[] Balance = MessageBalance.Split(separating2);
                    //    var BalanceArray0 = Balance[0];

                    //    newMessages.Add(new NewMessage
                    //    {
                    //        UserID = data.UserID,
                    //        UserType = data.user_type,

                    //        MessageID = data.nMessageID,
                    //        MessageType = data.nType,
                    //        dSend = data.dSend,
                    //        Money = MessageMoney,
                    //        Balance = BalanceArray0
                    //    });
                    //}

                    //var dbTuser = db.TUsers.ToList();
                    //var dbTemployee = db.TEmployees.ToList();

                    //var UserMessage = (from a in newMessages

                    //                   join b in dbTuser on new { userid = a.UserID, usertype = a.UserType }
                    //                   equals new { userid = b.sID, usertype = 0 } into AB
                    //                   from JAB in AB.DefaultIfEmpty()

                    //                   join c in dbTemployee on new { userid = a.UserID, usertype = a.UserType }
                    //                   equals new { userid = c.sEmp, usertype = 1 } into AC
                    //                   from JAC in AC.DefaultIfEmpty()

                    //                   select new
                    //                   {
                    //                       a.UserID,
                    //                       a.UserType,
                    //                       UserName = JAB == null && JAC == null ? " " : JAB != null ? JAB.sName + " " + JAB.sLastname : JAC.sName + " " + JAC.sLastname,
                    //                       a.dSend,
                    //                       a.MessageID,
                    //                       a.MessageType,
                    //                       a.Balance
                    //                   }).ToList();

                    //var ListMessage0 = (from a in UserMessage
                    //                    group a by new { a.UserID } into gb0
                    //                    orderby gb0.Key.UserID
                    //                    select new
                    //                    {
                    //                        gb0.Key.UserID,
                    //                        ListMessage1 = (from a in gb0
                    //                                        where a.MessageID == gb0.Max(m => m.MessageID)
                    //                                        group a by new { a.MessageID, a.Balance } into gb1
                    //                                        select new
                    //                                        {
                    //                                            Balance = Convert.ToDecimal(gb1.Key.Balance)
                    //                                        }).ToList()
                    //                    }).ToList();

                    //decimal Total = 0;
                    //foreach (var userData in ListMessage0)
                    //{
                    //    var Balance = userData.ListMessage1.Select(s => s.Balance).Sum();
                    //    Total = Convert.ToDecimal(Total) + Balance;
                    //}

                    userListLevels.Add(new UserListLevel
                    {
                        TotalBalance = BalanceTotal.Value
                    });

                    var userLevel0s = (from a in userListLevels

                                       select new UserLevel0
                                       {
                                           TotalBalance = a.TotalBalance,
                                           DateToday = search.dStart.Value.ToString("dd/MM/yyyy")
                                       }).ToList();

                    var listHeaderText = "รายงานรวมยอดเงินคงเหลือ";

                    return new ViewDataList
                    {
                        listHeaderText = listHeaderText,
                        userLevel0s = userLevel0s
                    };
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void export_data(Search search)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }

            if (search.rRport_tYpe == 0)
            {
                var ReportView = Report_Student_Balance(search) as view;
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานยอดเงินคงเหลือนักเรียน");
                    var worksheet = excel.Workbook.Worksheets["รายงานยอดเงินคงเหลือนักเรียน"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                 
                    var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                    DateTime dayStart = DateTime.Today;
                    DateTime dayEnd = DateTime.Today;

                    int Rows = 6;
                    int Index = 1;
                    SetHeader(worksheet, "A1:F1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, "A2:F2", true, ReportView.HeaderText, 15, ExcelHorizontalAlignment.Center);

                    SetHeader(worksheet, "A3:F3", true,
                        "พิมวันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")), 15, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, "A4:F4", true,
                        "เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), 15, ExcelHorizontalAlignment.Right);

                    string[] Header = { "ชั้นเรียน", "ลำดับ", "รหัสนักเรียน", "วันที่", "ชื่อ - นามสกุล", "ยอดเงินคงเหลือ" };
                    int Columuns = 1;
                    foreach (string DataHeadder in Header)
                    {
                        SetTableHeader(worksheet.Cells[5, Columuns++], false, DataHeadder, ExcelHorizontalAlignment.Center);
                    }

                    Decimal TotalBalance = 0;
                    foreach (var DataStudent0 in ReportView.LaYer0s)
                    {
                        if (DataStudent0.LaYer1s.Count() == 0) continue;
                        foreach (var DataStudent1 in DataStudent0.LaYer1s)
                        {
                            if (DataStudent1.LaYer2s.Count() == 0) continue;
                            int rowsEnd = Rows + (DataStudent1.LaYer2s.Count() - 1);
                            SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], false, DataStudent1.RoomFullName, ExcelHorizontalAlignment.Center);

                            foreach (var DataStudent2 in DataStudent1.LaYer2s)
                            {
                                if (DataStudent2.LaYer3s.Count() == 0) continue;

                                SetTableRows(worksheet.Cells[Rows, 2, rowsEnd, 2], false, string.Format("{0:#,0}", Index++), ExcelHorizontalAlignment.Center);
                                SetTableRows(worksheet.Cells[Rows, 3, rowsEnd, 3], false, DataStudent2.StudentID, ExcelHorizontalAlignment.Center);

                                foreach (var DataStudent3 in DataStudent2.LaYer3s)
                                {
                                    SetTableRows(worksheet.Cells[Rows, 4, rowsEnd, 4], false, DataStudent3.StudentdSend, ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[Rows, 5, rowsEnd, 5], false, DataStudent3.StudentTitle + DataStudent3.StudentFullName, ExcelHorizontalAlignment.Center);
                                    SetTableRows(worksheet.Cells[Rows, 6, rowsEnd, 6], false, string.Format("{0:#,0.00}", DataStudent3.StudentBalance), ExcelHorizontalAlignment.Center);
                                    Rows += 1;
                                }
                                TotalBalance += DataStudent2.LaYer3s.Sum(s => s.StudentBalance);
                            }

                        }

                        SetTableFooter(worksheet.Cells[Rows, 1, Rows, 5], true, "จำนวนรวม", ExcelHorizontalAlignment.Right);
                        SetTableFooter(worksheet.Cells[Rows, 6, Rows++, 6], true, string.Format("{0:#,0.00}", TotalBalance), ExcelHorizontalAlignment.Center);

                    }

                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 10;
                    worksheet.Column(3).Width = 15;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 50;
                    worksheet.Column(5).Width = 28;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                    HttpContext.Current.Response.ContentType = "application/text";
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
                }
            }
            else if (search.rRport_tYpe == 1)
            {
                var EmReportView = Report_Student_Balance(search) as viewEmp;

                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานยอดเงินคงเหลือบุคลากร");
                    var worksheet = excel.Workbook.Worksheets["รายงานยอดเงินคงเหลือบุคลากร"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                   
                    var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                    int Rows = 6;
                    int Index = 1;
                    SetHeader(worksheet, "A1:E1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, "A2:E2", true, EmReportView.EmpHeaderText, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, "A3:E3", true,
                        "พิมวันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")), 15, ExcelHorizontalAlignment.Right);
                    SetHeader(worksheet, "A4:E4", true,
                        "เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), 15, ExcelHorizontalAlignment.Right);

                    string[] Header = { "ลำดับ", "วันที่", "รหัสบุคลากร", "ชื่อ - นามสกุล", "ยอดเงินคงเหลือ" };
                    int Columuns = 1;
                    foreach (string DataHeadder in Header)
                    {
                        SetTableHeader(worksheet.Cells[5, Columuns++], false, DataHeadder, ExcelHorizontalAlignment.Center);
                    }

                    Decimal TotalBalance = 0;
                    foreach (var DataEmployee0 in EmReportView.empLevel0s)
                    {
                        if (DataEmployee0.empLevel1s.Count() == 0) continue;
                        int rowsEnd = Rows + (DataEmployee0.empLevel1s.Count() - 1);
                        foreach (var DataEmployee1 in DataEmployee0.empLevel1s)
                        {
                            SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], false, string.Format("{0:#,0}", Index++), ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 2, rowsEnd, 2], false, DataEmployee1.EmpdSend, ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 3, rowsEnd, 3], false, string.Format("{0:#,0}", DataEmployee1.EmpID), ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 4, rowsEnd, 4], false, DataEmployee1.EmpTitle + DataEmployee1.EmpFullName, ExcelHorizontalAlignment.Center);
                            SetTableRows(worksheet.Cells[Rows, 5, rowsEnd, 5], false, string.Format("{0:#,0.00}", DataEmployee1.EmpBalance), ExcelHorizontalAlignment.Center);
                            Rows += 1;
                        }

                        TotalBalance += DataEmployee0.empLevel1s.Sum(s => s.EmpBalance) ?? 0;
                    }

                    SetTableFooter(worksheet.Cells[Rows, 1, Rows, 4], true, "จำนวนรวม", ExcelHorizontalAlignment.Right);
                    SetTableFooter(worksheet.Cells[Rows, 5, Rows++, 5], true, string.Format("{0:#,0.00}", TotalBalance), ExcelHorizontalAlignment.Center);


                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 25;
                    worksheet.Column(4).Width = 40;
                    worksheet.Column(5).Width = 25;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                    HttpContext.Current.Response.ContentType = "application/text";
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
                }
            }
            else if (search.rRport_tYpe == 3)
            {
                var ReportViewList = Report_Student_Balance(search) as ViewDataList;

                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานยอดเงินคงทั้งหมด");
                    var worksheet = excel.Workbook.Worksheets["รายงานยอดเงินคงทั้งหมด"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                 
                    var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                    SetHeader(worksheet, "A1:E1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, "A2:E2", true, ReportViewList.listHeaderText, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, "A4:E4", true,
                        "พิมวันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")) + " เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), 13, ExcelHorizontalAlignment.Right);

                    string[] Header = { "วันที่", "ยอดเติม", "ยอดใช้จ่าย", "ยอดถอน", "ยอดคงเหลือ" };
                    int Columuns = 1;
                    foreach (string DataHeader in Header)
                    {
                        SetTableHeader(worksheet.Cells[5, Columuns++], false, DataHeader, ExcelHorizontalAlignment.Center);
                    }

                    int Rows = 6; /*เริ่มแถวที่6*/
                    int rowsEnd = Rows + (ReportViewList.balanceAmounts.Count() - 1);
                    foreach (var DataViewList in ReportViewList.balanceAmounts)
                    {

                        SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], false, DataViewList.DateToday, ExcelHorizontalAlignment.Center);
                        SetTableNumberColumn(worksheet.Cells[Rows, 2, rowsEnd, 2], false, DataViewList.TotalBalanceTopUp, ExcelHorizontalAlignment.Right);
                        SetTableNumberColumn(worksheet.Cells[Rows, 3, rowsEnd, 3], false, DataViewList.TotalSales, ExcelHorizontalAlignment.Right);
                        SetTableNumberColumn(worksheet.Cells[Rows, 4, rowsEnd, 4], false, DataViewList.TotalWithdrawal, ExcelHorizontalAlignment.Right);
                        SetTableNumberColumn(worksheet.Cells[Rows, 5, rowsEnd, 5], false, DataViewList.FinalBalance, ExcelHorizontalAlignment.Right);
                        Rows += 1; /*จำนวนแถว+1ไปเลื่อยๆ*/
                    }

                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(2).Style.Numberformat.Format = "0.00";
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(3).Style.Numberformat.Format = "0.00";
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(4).Style.Numberformat.Format = "0.00";
                    worksheet.Column(5).Width = 20;
                    worksheet.Column(5).Style.Numberformat.Format = "0.00";

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                    HttpContext.Current.Response.ContentType = "application/text";
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
                }
            }
            else if (search.rRport_tYpe == 4)
            {
                var ReportViewList = Report_Student_Balance(search) as ViewDataList;

                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานยอดเงินคงทั้งหมด");
                    var worksheet = excel.Workbook.Worksheets["รายงานยอดเงินคงทั้งหมด"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                  
                    var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                    SetHeader(worksheet, "A1:I1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, "A2:I2", true, ReportViewList.listHeaderText, 15, ExcelHorizontalAlignment.Center);
                    SetHeader(worksheet, "E4:I4", true,
                        "พิมวันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")) + " เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), 13, ExcelHorizontalAlignment.Right);


                    string[] Header = { "ลำดับ", "วันที่", "รหัสนักเรียน", "ชื่อ-สกุล", "นักเรียน / ครู", "ยอดเติม", "ยอดใช้จ่าย", "ยอดถอน", "ยอดคงเหลือ" };
                    int Columuns = 1;
                    foreach (string DataHeader in Header)
                    {
                        SetTableHeader(worksheet.Cells[5, Columuns++], false, DataHeader, ExcelHorizontalAlignment.Center);
                    }
                    int rowCount = 1;

                    int Rows = 6; /*เริ่มแถวที่6*/
                    int rowsEnd = Rows + (ReportViewList.balanceAmounts.Count());
                    decimal totalBalanceTopUp = 0;
                    decimal totalSales = 0;
                    decimal totalWithdrawal = 0;
                    decimal finalBalance = 0;
                    foreach (var DataViewList in ReportViewList.balanceAmounts)
                    {
                        SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], false, rowCount.ToString(), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 2, rowsEnd, 2], false, DataViewList.DateToday, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 3, rowsEnd, 3], false, DataViewList.StudentID, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 4, rowsEnd, 4], false, DataViewList.FullName, ExcelHorizontalAlignment.Left);
                        SetTableRows(worksheet.Cells[Rows, 5, rowsEnd, 5], false, DataViewList.Type, ExcelHorizontalAlignment.Center);
                        SetTableNumberColumn(worksheet.Cells[Rows, 6, rowsEnd, 6], false, DataViewList.TotalBalanceTopUp, ExcelHorizontalAlignment.Right);
                        SetTableNumberColumn(worksheet.Cells[Rows, 7, rowsEnd, 7], false, DataViewList.TotalSales, ExcelHorizontalAlignment.Right);
                        SetTableNumberColumn(worksheet.Cells[Rows, 8, rowsEnd, 8], false, DataViewList.TotalWithdrawal, ExcelHorizontalAlignment.Right);
                        SetTableNumberColumn(worksheet.Cells[Rows, 9, rowsEnd, 9], false, DataViewList.FinalBalance, ExcelHorizontalAlignment.Right);

                        totalBalanceTopUp += DataViewList.TotalBalanceTopUp;
                        totalSales += DataViewList.TotalSales;
                        totalWithdrawal += DataViewList.TotalWithdrawal;
                        finalBalance += DataViewList.FinalBalance;
                        Rows += 1; /*จำนวนแถว+1ไปเลื่อยๆ*/
                        rowCount += 1;
                    }
                    SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], false, "", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 2, rowsEnd, 2], false, "", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 3, rowsEnd, 3], false, "", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 4, rowsEnd, 4], false, "", ExcelHorizontalAlignment.Left);
                    SetTableRows(worksheet.Cells[Rows, 5, rowsEnd, 5], false, "รวม", ExcelHorizontalAlignment.Center);
                    SetTableRows(worksheet.Cells[Rows, 6, rowsEnd, 6], false, string.Format("{0:0.00}", totalBalanceTopUp), ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[Rows, 7, rowsEnd, 7], false, string.Format("{0:0.00}", totalSales), ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[Rows, 8, rowsEnd, 8], false, string.Format("{0:0.00}", totalWithdrawal), ExcelHorizontalAlignment.Right);
                    SetTableRows(worksheet.Cells[Rows, 9, rowsEnd, 9], false, string.Format("{0:0.00}", finalBalance), ExcelHorizontalAlignment.Right);


                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(1).Width = 10;
                    worksheet.Column(2).Width = 10;
                    worksheet.Column(3).Width = 10;
                    worksheet.Column(4).Width = 30;
                    worksheet.Column(5).Width = 10;
                    worksheet.Column(6).Width = 10;
                    worksheet.Column(6).Style.Numberformat.Format = "0.00";
                    worksheet.Column(7).Width = 10;
                    worksheet.Column(7).Style.Numberformat.Format = "0.00";
                    worksheet.Column(8).Width = 10;
                    worksheet.Column(8).Style.Numberformat.Format = "0.00";
                    worksheet.Column(9).Width = 10;
                    worksheet.Column(9).Style.Numberformat.Format = "0.00";
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                    HttpContext.Current.Response.ContentType = "application/text";
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
                }
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

        private static void SetTableNumberColumn(ExcelRange Cells, bool Merge, decimal strValues, ExcelHorizontalAlignment excelHorizontal)
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
        private static string getTitlte(List<TTitleList> titles, string titlesId)
        {
            int nTitleid = 0;
            int.TryParse((titlesId ?? "0"), out nTitleid);
            var f_titles = titles.FirstOrDefault(f => f.nTitleid == nTitleid);
            if (f_titles == null) return titlesId;
            else return f_titles.titleDescription;
        }

        public class Search
        {
            public string yEar_Id { get; set; }
            public string tErm_Id { get; set; }
            public int? sUbLV_Id { get; set; }
            public int? sUbLV2_Id { get; set; }
            public int rRport_tYpe { get; set; }
            public DateTime? dStart { get; set; }
            public string EmpFullName { get; set; }
            public string StudentFullName { get; set; }

            public int numberYear { get; set; }
        }
        public class view
        {
            public string HeaderText { get; set; }
            public List<LaYer0> LaYer0s { get; set; }
        }

        public class NewMessage
        {
            public int UserID { get; set; }
            public int UserType { get; set; }
            public int MessageID { get; set; }
            public int? MessageType { get; set; }
            public string Money { get; set; }
            public string Balance { get; set; }
            public DateTime? dSend { get; set; }

        }
        public class LaYer0
        {
            public List<LaYer1> LaYer1s { get; set; }
            public int ClassID { get; set; }
            public string ClassName { get; set; }
        }
        public class LaYer1
        {
            public List<LaYer2> LaYer2s { get; set; }
            public string RoomFullName { get; set; }
            public string RoomName { get; set; }
        }
        public class LaYer2
        {
            public List<LaYer3> LaYer3s { get; set; }
            public int? StudentNumber { get; set; }
            public string StudentID { get; set; }
        }
        public class LaYer3
        {
            public string StudentID { get; set; }
            public string StudentTitle { get; set; }
            public string StudentFullName { get; set; }
            public string StudentdSend { get; set; }
            public decimal StudentBalance { get; set; }
        }

        public class viewEmp
        {
            public List<EmpLevel0> empLevel0s { get; set; }
            public string EmpHeaderText { get; set; }
        }
        public class EmpLevel0
        {
            public List<EmpLevel1> empLevel1s { get; set; }
            public int UserID { get; set; }
        }
        public class EmpLevel1
        {
            public int EmpID { get; set; }
            public string EmpTitle { get; set; }
            public string EmpFullName { get; set; }
            public string EmpdSend { get; set; }
            public decimal? EmpBalance { get; set; }
        }

        public class UserListLevel
        {
            public decimal TotalBalance { get; set; }
        }
        public class ViewDataList
        {
            public string listHeaderText { get; set; }
            public List<UserLevel0> userLevel0s { get; set; }
            public List<BalanceAmount> balanceAmounts { get; set; }
        }
        public class UserLevel0
        {
            public decimal TotalBalance { get; set; }
            public string DateToday { get; set; }
        }

        public class BalanceAmount
        {
            public decimal TotalBalanceTopUp { get; set; }
            public decimal TotalSales { get; set; }
            public decimal TotalWithdrawal { get; set; }
            public decimal FinalBalance { get; set; }
            public string FullName { get; set; }
            public string StudentID { get; set; }
            public int UserID { get; set; }
            public int cType { get; set; }
            public string Type => (cType == 1) ? "ครู" : "นักเรียน";
            public string DateToday { get; set; }
        }
    }
}