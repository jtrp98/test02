using FingerprintPayment.Models;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;


namespace FingerprintPayment
{
    public partial class classmember : System.Web.UI.Page
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

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
           
                btnSave.Click += new EventHandler(btnSave_Click);
                btnCancle.Click += new EventHandler(btnCancle_Click);
                string sEntities = Session["sEntities"].ToString();

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                if (!IsPostBack)
                {
                    var emp = (from a in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)
                               from b in _db.TEmpSalaries.Where(o => o.sEmp == a.sEmp && o.SchoolID == userData.CompanyID).DefaultIfEmpty()
                               select new { a.sEmp, a.sName, a.sLastname, WorkStatus = b.WorkStatus ?? 1 })
                               .Where( o => o.WorkStatus == 1)
                               .AsEnumerable()
                                .Select(o => new ListItem
                                {
                                    Text = o.sName + " " + o.sLastname,
                                    Value = o.sEmp + ""
                                })
                                .ToList();
                    var empList = emp.OrderBy(x => x.Text).ToList();
                    empList.Insert(0, new ListItem() { Text = "- กรุณาเลือก -", Value = "" });
                    var newList = empList.ToArray();
                    OpenData();

                    txtSearch.Items.AddRange(newList);
                    txtSearch2.Items.AddRange(newList);
                    txtSearch3.Items.AddRange(newList);

                    string yearr = Request.QueryString["year"];
                    DropDownList1.SelectedValue = yearr;
                    string termm = Request.QueryString["term"];
                    DropDownList2.SelectedValue = termm;

                    //fcommon.ListDBToDropDownList(_conn, ddlsublevel, "select * from TSubLevel", "- ทั้งหมด -", "nTSubLevel", "SubLevel");
                    var item4 = new ListItem
                    {
                        Text = "ทั้งหมด",
                        Value = ""
                    };
                    ddlsublevel.Items.Add(item4);

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

                    var q = _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(x => x.numberYear).ToList();
                    fcommon.LinqToDropDownList(q, DropDownList1, "", "nYear", "numberYear");
                }
            }
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            OpenData();
        }

        protected List<member> returnlist()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());

                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string ddlyear = Request.QueryString["year"];
                string ddlterm = Request.QueryString["term"];
                string sname = Request.QueryString["sname"];

                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {

                    int count = 1;
                    member member = new member();
                    List<member> memberlist = new List<member>();

                    if (idlv == null && idlv2 == null && DropDownList1 == null && string.IsNullOrEmpty(ddlterm))
                    {
                        return null;
                    }

                    if (ddlyear == null)
                        ddlyear = "99999999";
                    int? useryear = Int32.Parse(ddlyear);
                    int? ntermsublv2 = 0;
                    int nyear = 0;
                    string nterm = "";
                    int ntermtable = 0;
                    List<string> planIdlist = new List<string>();

                    foreach (var term in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null && w.nTerm == ddlterm))
                    {
                        nterm = term.nTerm;
                    }

                    var q_employees = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList();

                    string SQL = "", condition = " T1.nTerm = '" + ddlterm + "' ";
                    if (!string.IsNullOrEmpty(idlv2)) condition += " AND B.nTermSubLevel2 = " + idlv2;
                    else if (!string.IsNullOrEmpty(idlv)) condition += " AND A.nTSubLevel = " + idlv;

                    SQL += @"SELECT ROW_NUMBER() OVER (ORDER BY SubLevel,sort,nTSubLevel2) AS 'number',* FROM 
(SELECT A.SubLevel,B.nTSubLevel2,A.SubLevel + ' / ' + B.nTSubLevel2 'room'
,CASE WHEN ISNUMERIC(nTSubLevel2) = 1 THEN nTSubLevel2 ELSE 500 END AS sort
,E1.sName + ' ' + E1.sLastname AS teacherHead,E2.sName + ' ' + E2.sLastname AS teacherAssistOne,E3.sName + ' ' + E3.sLastname AS teacherAssistTwo,T1.nTerm
FROM TSubLevel AS A
INNER JOIN TTermSubLevel2 AS B ON A.nTSubLevel = B.nTSubLevel AND A.SchoolID = B.SchoolID AND A.nWorkingStatus = 1 AND B.nWorkingStatus = 1
INNER JOIN TTerm AS T1 ON T1.SchoolID = B.SchoolID
LEFT OUTER JOIN TClassMember AS C ON B.nTermSubLevel2 = C.nTermSubLevel2 AND T1.nTerm = C.nTerm AND B.SchoolID = C.SchoolID 
LEFT OUTER JOIN TEmployees AS E1 ON C.nTeacherHeadid = E1.sEmp AND E1.SchoolID = C.SchoolID
LEFT OUTER JOIN TEmployees AS E2 ON C.nTeacherAssistOne = E2.sEmp AND E2.SchoolID = C.SchoolID
LEFT OUTER JOIN TEmployees AS E3 ON C.nTeacherAssistTwo = E3.sEmp AND E3.SchoolID = C.SchoolID
WHERE  " + condition + " AND B.nWorkingStatus = 1 AND B.nTermSubLevel2Status = 1) TB ";

                    memberlist = _db.Database.SqlQuery<member>(SQL).ToList();


                    return memberlist;
                }
            }
        }

        private void OpenData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            string idlv = Request.QueryString["idlv"];
            string ddlyear = Request.QueryString["year"];
            string ddlterm = Request.QueryString["term"];
            string sname = Request.QueryString["sname"];
            string idlv2 = Request.QueryString["idlv2"];

            if ((idlv2 != null && idlv2 != "") && (idlv != null && idlv != "") && (ddlyear != null && ddlyear != "") && (ddlterm != null && ddlterm != ""))
            {

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
                string idlv2 = room.Text;
                string ddlyear = Request.QueryString["year"];
                string ddlterm = Request.QueryString["term"];
                int id = 0;
                Int32.TryParse(Request.QueryString["id"], out id);
                string sname = Request.QueryString["sname"];
                string sEntities = Session["sEntities"] + "";

                if (ddlyear == null)
                    ddlyear = "99999999";
                int? useryear = Int32.Parse(ddlyear);
                int nyear = 0;
                List<string> nterm = new List<string>();
                //string nterm2 = "";

                foreach (var year in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).Where(w => w.numberYear == useryear))
                {
                    nyear = year.nYear;
                }

                nterm = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.nYear == nyear && w.cDel == null).Select(s => s.nTerm).ToList();

                int idlv2n = Int32.Parse(idlv2);

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var data = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == id).FirstOrDefault();

                int countID = _db.TClassMembers.Count() == 0 ? 0 : _db.TClassMembers.Max(max => max.nClassMemberid);
                countID = countID + 1;

                foreach (var TremId in nterm)
                {
                    TClassMember member = new TClassMember();
                    if (txtSearch.SelectedValue != "-1")
                    {
                        int x1 = Int32.Parse(txtSearch.SelectedValue);
                        member.nTeacherHeadid = x1;
                    }
                    else member.nTeacherHeadid = null;

                    if (txtSearch2.SelectedValue != "-1")
                    {
                        int x2 = Int32.Parse(txtSearch2.SelectedValue);
                        member.nTeacherAssistOne = x2;
                    }
                    else member.nTeacherAssistOne = null;

                    if (txtSearch3.SelectedValue != "-1")
                    {
                        int x3 = Int32.Parse(txtSearch3.SelectedValue);
                        member.nTeacherAssistTwo = x3;
                    }
                    else member.nTeacherAssistTwo = null;
                    member.nTermSubLevel2 = idlv2n;

                    var check = _db.TClassMembers.Where(w => w.nTerm == TremId && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                    if (check == null)
                    {
                        member.nClassMemberid = countID;
                        member.nTerm = TremId;
                        member.SchoolID = userData.CompanyID;

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
                }

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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static BaseResultModel SaveData(TM_ClassMember classMember)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                BaseResultModel baseResult = new BaseResultModel();
                try
                {
                    int? nTeacherHeadid, nTeacherAssistOne, nTeacherAssistTwo;

                    if (classMember.nTeacherHeadid > 0)
                        nTeacherHeadid = classMember.nTeacherHeadid;
                    else
                        nTeacherHeadid = null;

                    if (classMember.nTeacherAssistOne > 0)
                        nTeacherAssistOne = classMember.nTeacherAssistOne;
                    else
                        nTeacherAssistOne = null;

                    if (classMember.nTeacherAssistTwo > 0)
                        nTeacherAssistTwo = classMember.nTeacherAssistTwo;
                    else
                        nTeacherAssistTwo = null;

                    using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                    {
                        entities.Configuration.ProxyCreationEnabled = false;

                        var term = entities.TTerms.AsNoTracking()
                            .Where(o => o.SchoolID == userData.CompanyID && o.nTerm == classMember.nTerm)
                            .FirstOrDefault();

                        var termlist = entities.TTerms.AsNoTracking()
                            .Where(o => o.SchoolID == userData.CompanyID && o.nYear == term.nYear && o.dStart >= term.dStart)
                            .ToList();

                        foreach (var t in termlist)
                        {
                            var f_data = entities.TClassMembers
                                 .Where(f => f.SchoolID == userData.CompanyID
                                     && f.nTermSubLevel2 == classMember.nTermSubLevel2
                                     && f.nTerm == t.nTerm)
                                 .FirstOrDefault();

                            if (f_data == null)
                            {
                                f_data = new TClassMember();
                                f_data.nClassMemberid = entities.TClassMembers.Max(m => m.nClassMemberid) + 1;
                                f_data.nTeacherHeadid = nTeacherHeadid;
                                f_data.nTeacherAssistOne = nTeacherAssistOne;
                                f_data.nTeacherAssistTwo = nTeacherAssistTwo;
                                f_data.SchoolID = userData.CompanyID;
                                f_data.nTerm = t.nTerm;
                                f_data.nTermSubLevel2 = classMember.nTermSubLevel2;
                                f_data.cDel = false;
                                f_data.CreatedDate = DateTime.Now;
                                f_data.CreatedBy = userData.UserID;
                                entities.TClassMembers.Add(f_data);
                                entities.SaveChanges();
                            }
                            else
                            {
                                var serializer = new JavaScriptSerializer();
                                serializer.RegisterConverters(new[] { new DateTimeConverter() });
                                string json = serializer.Serialize(f_data);

                                TB_HistorySetting _HistorySetting = new TB_HistorySetting()
                                {
                                    Fd_SettingData = json,
                                    Fd_UpdatedDate = DateTime.Now,
                                    Fd_FunctionName = "Class Members Settings",
                                    Fd_HistoryID = Guid.NewGuid(),
                                    Fd_SchoolID = userData.CompanyID,
                                    Fd_UpdatedBy = userData.UserID
                                };

                                entities.TB_HistorySetting.Add(_HistorySetting);

                                f_data.nTeacherHeadid = nTeacherHeadid;
                                f_data.nTeacherAssistOne = nTeacherAssistOne;
                                f_data.nTeacherAssistTwo = nTeacherAssistTwo;
                                f_data.UpdatedDate = DateTime.Now;
                                f_data.UpdatedBy = userData.UserID;
                                entities.SaveChanges();


                            }
                        }

                        baseResult = new BaseResultModel
                        {
                            Message = "Success",
                            Result = classMember,
                            StatusCode = "200"
                        };
                    }
                }
                catch (Exception e)
                {
                    baseResult = new BaseResultModel
                    {
                        Message = "UPDATE ERROR",
                        SystemErrorMessage = e,
                        StatusCode = "500"
                    };
                }

                return baseResult;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static BaseResultModel LoadData(string idlv, string idlv2, string ddlterm, int year)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                List<member> memberlist = new List<member>();

                if (idlv == null && idlv2 == null && string.IsNullOrEmpty(ddlterm))
                {
                    return null;
                }

                BaseResultModel baseResult = new BaseResultModel();
                try
                {

                    string SQL = "", condition = "";

                    if (!string.IsNullOrEmpty(ddlterm))
                    {
                        condition = " T1.nTerm = '" + ddlterm + "' ";
                    }
                    else
                    {
                        var terms = _db.TTerms
                            .Where(o => o.SchoolID == userData.CompanyID && o.nYear == year && o.cDel != "1")
                            .Select(o => "'" + o.nTerm + "'")
                            .ToList();

                        condition = $" T1.nTerm in ({string.Join(",", terms)})";
                    }

                    if (!string.IsNullOrEmpty(idlv2)) condition += " AND B.nTermSubLevel2 = " + idlv2;
                    else if (!string.IsNullOrEmpty(idlv)) condition += " AND A.nTSubLevel = " + idlv;

                    SQL += @"SELECT ROW_NUMBER() OVER (ORDER BY SubLevel,sort,nTSubLevel2) AS 'number',* FROM 
(SELECT A.SubLevel,B.nTSubLevel2,A.SubLevel + ' / ' + B.nTSubLevel2 'room'
,E.sortValue sort ,B.nTermSubLevel2 AS 'idlv2'
,E1.sName + ' ' + E1.sLastname AS teacherHead,E2.sName + ' ' + E2.sLastname AS teacherAssistOne,E3.sName + ' ' + E3.sLastname AS teacherAssistTwo,T1.nTerm , T1.sTerm 'term' 
FROM TSubLevel AS A
INNER JOIN TTermSubLevel2 AS B ON A.nTSubLevel = B.nTSubLevel AND A.SchoolID = B.SchoolID AND A.nWorkingStatus = 1 AND B.nWorkingStatus = 1
INNER JOIN TTerm AS T1 ON T1.SchoolID = B.SchoolID
LEFT OUTER JOIN TClassMember AS C ON B.nTermSubLevel2 = C.nTermSubLevel2 AND T1.nTerm = C.nTerm AND B.SchoolID = C.SchoolID 
LEFT OUTER JOIN TEmployees AS E1 ON C.nTeacherHeadid = E1.sEmp AND E1.SchoolID = C.SchoolID AND E1.cDel IS NULL
LEFT OUTER JOIN TEmployees AS E2 ON C.nTeacherAssistOne = E2.sEmp AND E2.SchoolID = C.SchoolID AND E2.cDel IS NULL
LEFT OUTER JOIN TEmployees AS E3 ON C.nTeacherAssistTwo = E3.sEmp AND E3.SchoolID = C.SchoolID AND E3.cDel IS NULL 
LEFT JOIN TLevel E ON A.nTLevel = E.LevelID AND A.SchoolID = E.SchoolID
WHERE  " + condition + " AND B.nWorkingStatus = 1 AND B.nTermSubLevel2Status = 1) TB ";

                    memberlist = _db.Database.SqlQuery<member>(SQL).ToList();


                    foreach (var member in memberlist)
                    {
                        var str = Regex.Replace(member.room, "[^0-9]", "");

                        int? numeric = null;
                        if (!string.IsNullOrEmpty(str))
                            numeric = int.Parse(str);

                        member.sort2 = numeric;
                    }
                    memberlist = memberlist.OrderBy(o => o.sort).ThenBy(o => o.sort2).ThenBy(o => o.term).ToList();

                    int index = 1;
                    foreach (var item in memberlist)
                    {
                        item.number = index++;
                    }

                    baseResult = new BaseResultModel
                    {
                        Message = "Success",
                        Result = memberlist,
                        StatusCode = "200"
                    };
                }

                catch (Exception e)
                {
                    baseResult = new BaseResultModel
                    {
                        //Message = "UPDATE ERROR",
                        SystemErrorMessage = e,
                        StatusCode = "500"
                    };
                }

                return baseResult;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static BaseResultModel CopyData(int yearFrom, int yearTo, string termFrom, string termTo, int? level, int? room)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (var _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                entities.Configuration.ProxyCreationEnabled = false;
                BaseResultModel baseResult = new BaseResultModel();
             
                    try
                    {
                        var dataTermFrom = entities.TTerms
                            .Where(o => o.SchoolID == userData.CompanyID && o.nYear == yearFrom && o.nTerm == termFrom)
                            .Select(o => new { o.nTerm, o.sTerm })
                            .OrderBy(o => o.sTerm)
                            .FirstOrDefault();

                        var dataTermTo = entities.TTerms
                            .Where(o => o.SchoolID == userData.CompanyID && o.nYear == yearTo)
                            .Select(o => new { o.nTerm, o.sTerm })
                            .OrderBy(o => o.sTerm)
                            .ToList();

                        if (!string.IsNullOrEmpty(termTo))
                        {
                            dataTermTo = dataTermTo.Where(o => o.nTerm == termTo).ToList();
                        }

                        var roomList = new List<int>();

                        if (room.HasValue)
                        {
                            roomList.Add(room.Value);
                        }
                        else
                        {
                            if (level.HasValue)
                            {
                                var r = entities.TTermSubLevel2
                                    .Where(o => o.SchoolID == userData.CompanyID && o.nTSubLevel == level)
                                    .Select(o => o.nTermSubLevel2)
                                    .ToList();

                                roomList.AddRange(r);
                            }
                        }

                        var arr = dataTermTo.Select(i => i.nTerm);

                        var qryRemoveTo = entities.TClassMembers
                         .Where(o => o.SchoolID == userData.CompanyID && arr.Contains(o.nTerm));

                        if (roomList.Count > 0)
                        {
                            qryRemoveTo = qryRemoveTo.Where(o => roomList.Contains(o.nTermSubLevel2.Value));
                        }

                        var removeTo = qryRemoveTo.ToList();

                        entities.TClassMembers.RemoveRange(removeTo);

                        var lst2Add = new List<TClassMember>();

                        var qryClassFrom = entities.TClassMembers
                                .Where(o => o.SchoolID == userData.CompanyID && o.nTerm == dataTermFrom.nTerm);

                        if (roomList.Count > 0)
                        {
                            qryClassFrom = qryClassFrom.Where(o => roomList.Contains(o.nTermSubLevel2.Value));
                        }
                        var classFrom = qryClassFrom.ToList();

                        foreach (var term in dataTermTo)
                        {
                            foreach (var copyTo in classFrom)
                            {
                                var obj = CloneObject<TClassMember, TClassMember>(copyTo);
                                obj.nTerm = term.nTerm;
                                lst2Add.Add(obj);
                            }
                        }

                        entities.TClassMembers.AddRange(lst2Add);
                        entities.SaveChanges();
                      
                        baseResult.Message = "SUCCESS";
                        return baseResult;
                    }

                    catch (Exception e)
                    {
                       
                        baseResult = new BaseResultModel
                        {
                            Message = "UPDATE ERROR",
                            SystemErrorMessage = e,
                            StatusCode = "500"
                        };
                    }
                
                return baseResult;
            }
        }

        private static T1 CloneObject<T1, T2>(T2 source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T1>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        public class TM_ClassMember
        {
            public Nullable<int> nTeacherHeadid { get; set; }
            public Nullable<int> nTeacherAssistOne { get; set; }
            public Nullable<int> nTeacherAssistTwo { get; set; }
            public Nullable<int> nTermSubLevel2 { get; set; }
            public string nTerm { get; set; }
        }

        public class member
        {
            public Int64 number { get; set; }
            public int? userid { get; set; }
            public string yearnumber { get; set; }
            public string termnumber { get; set; }
            public string Name { get; set; }
            public string description { get; set; }
            public int idlv2 { get; set; }
            public string year { get; set; }
            public string idlv { get; set; }
            public string teacherHead { get; set; }
            public string teacherAssistOne { get; set; }
            public string teacherAssistTwo { get; set; }
            public string room { get; set; }
            public string term { get; set; }
            public string nTerm { get; set; }
            public string toWebservice { get; set; }
            public int? sort { get; set; }
            public int? sort2 { get; set; }
        }

        public class cmember
        {
            public string head { get; set; }
            public string one { get; set; }
            public string two { get; set; }
        }

        public class ddlterm
        {
            public string sTerm { get; set; }
        }
    }
}