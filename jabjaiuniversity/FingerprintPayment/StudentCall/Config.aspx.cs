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
using static FingerprintPayment.App_Code.StdCallingHub;
using System.Web.Services;
using System.Web.Script.Services;
using Microsoft.AspNet.SignalR;

namespace FingerprintPayment.StudentCall
{
    public partial class Config : BaseStudentCall
    {
        public List<ModelTree> TreeView { get; set; }
        //public List<TStudentCall_Gate> Gates { get; set; } = new List<TStudentCall_Gate>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userData = GetUserData();
                var schoolId = userData.CompanyID;

                using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
                {
                    var q = from a in dbschool.TLevels.Where(o => o.SchoolID == schoolId)
                            from b in dbschool.TSubLevels.Where(o => o.SchoolID == schoolId && o.nTLevel == a.LevelID)
                            from c in dbschool.TTermSubLevel2.Where(o => o.SchoolID == schoolId && o.nTSubLevel == b.nTSubLevel)
                            orderby a.sortValue ascending
                            select new
                            {
                                groupid = a.LevelID,
                                groupname = a.LevelName,

                                idLevel1 = b.nTSubLevel,
                                nameLevel1 = b.fullName,

                                idLevel2 = c.nTermSubLevel2,
                                nameLevel2 = b.SubLevel + "/" + c.nTSubLevel2
                            };

                    TreeView = q.AsEnumerable()
                        .GroupBy(o => new { o.groupid, o.groupname })
                        .Select(o => new ModelTree
                        {
                            id = o.Key.groupid,
                            text = o.Key.groupname,
                            children = o.GroupBy(i => new { i.nameLevel1, i.idLevel1 })
                            .Select(i => new ModelTree
                            {
                                id = i.Key.idLevel1,
                                text = i.Key.nameLevel1,
                                children = i.Select(j => new ModelTree
                                {
                                    id = j.idLevel2,
                                    text = j.nameLevel2,
                                    children = new List<ModelTree>()

                                }).ToList()

                            }).ToList()
                        }).ToList();


                }
            }

            //using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            //{
            //    var c = dbmaster.TStudentCall_Config.FirstOrDefault(o => o.SchoolId == UserData.CompanyID);

            //    BgFile = c?.BgCard;
            //}
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static object GetNewGate(byte? no)
        {
            var token = Guid.NewGuid().ToString();
            //var url = "https://system.schoolbright.co/StudentCall/View.aspx?token=" + token;

            return new GateModel1
            {
                id = token,
                gate = ++no,
                gateName = "",
                url = "",
                selected = "[]",
                shortUrl = "",//UrlHelper.CreateShortURL(url),
            };
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static object GetData()
        {
            var userData = GetUserData();
            var schoolId = userData.CompanyID;
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var d = dbmaster.TStudentCall_Config
                  .Where(o => o.SchoolId == schoolId)
                  .FirstOrDefault();

                var listGate = dbmaster.TStudentCall_Gate.Where(o => o.SchoolID == schoolId && o.IsDel == false)
                    .Select(o => new GateModel1
                    {
                        id = o.Token.ToString(),
                        gate = o.Gate,
                        gateName = o.GateName,
                        url = o.FullUrl,
                        selected = o.SelectedRoom,
                        shortUrl = o.ShortUrl,
                        Created = o.Created
                    })
                     .OrderBy(o => o.Created).ToList();

                if (listGate.Count == 0)
                {
                    var token = Guid.NewGuid().ToString();
                    //var url = "https://system.schoolbright.co/StudentCall/View.aspx?token=" + token;

                    listGate.Add(new GateModel1
                    {
                        id = token,
                        gate = (byte?)1,
                        gateName = "",
                        url = "",
                        selected = "[]",
                        shortUrl = "",//UrlHelper.CreateShortURL(url),
                    });
                }

                return new
                {
                    status = d?.IsActive ?? false,
                    radius = d?.Radius ?? 0,
                    gates = listGate,
                    time = ""
                };

                //using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                //{



                //}
            }

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object SaveData(FormSaveData data)
        {
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                var schoolId = userData.CompanyID;

                var d = dbmaster.TStudentCall_Config
                  .Where(o => o.SchoolId == schoolId)
                  .FirstOrDefault();

                if (d == null)
                {
                    d = new TStudentCall_Config();
                    d.SchoolId = schoolId;
                    d.IsActive = data.status;
                    d.Radius = data.radius;
                    d.CreateBy = userData.UserID;
                    d.Created = DateTime.Now;
                    d.ModifyBy = userData.UserID;
                    d.Modified = DateTime.Now;

                    dbmaster.TStudentCall_Config.Add(d);
                }
                else
                {
                    d.IsActive = data.status;
                    d.Radius = data.radius;
                    d.ModifyBy = userData.UserID;
                    d.Modified = DateTime.Now;
                }

                foreach (var g in data.delList)
                {
                    var gate = dbmaster.TStudentCall_Gate
                       .Where(o => o.SchoolID == schoolId && o.Token == g)
                       .FirstOrDefault();

                    gate.IsDel = true;
                    gate.Modified = DateTime.Now;
                    gate.ModifyBy = userData.UserID;
                }

                foreach (var g in data.gates)
                {
                    var gate = dbmaster.TStudentCall_Gate
                        .Where(o => o.SchoolID == schoolId && o.Token == g.token)
                        .FirstOrDefault();

                    var url = "https://system.schoolbright.co/StudentCall/View.aspx?token=" + g.token;

                    if (gate == null)
                    {
                        var obj = new TStudentCall_Gate();
                        obj.Token = g.token;
                        obj.SchoolID = schoolId;
                        obj.Gate = g.gate;
                        obj.GateName = g.gateName;
                        obj.SelectedRoom = g.selected;
                        obj.FullUrl = url;
                        obj.ShortUrl = UrlHelper.CreateShortURL(url);
                        obj.Created = DateTime.Now;
                        obj.CreateBy = userData.UserID;
                        obj.Modified = DateTime.Now;
                        obj.ModifyBy = userData.UserID;
                        obj.IsDel = false;

                        dbmaster.TStudentCall_Gate.Add(obj);
                    }
                    else
                    {
                        gate.GateName = g.gateName;
                        gate.SelectedRoom = g.selected;
                        gate.Modified = DateTime.Now;
                        gate.ModifyBy = userData.UserID;
                    }
                }

                dbmaster.SaveChanges();

                return new { status = "ok" };
            }
        }

        public class FormSaveData
        {
            public bool? status { get; set; }
            public double? radius { get; set; }
            public List<GateModel1> gates { get; set; }
            public List<Guid> delList { get; set; }
        }

        public class ModelTree
        {
            public int id { get; set; }
            public string text { get; set; }
            public List<ModelTree> children { get; set; }
        }

        public class GateModel1
        {
            public Guid token
            {
                get
                {
                    return new Guid(id);
                }
            }
            public string id { get; set; }
            public byte? gate { get; set; }
            public string gateName { get; set; }
            public string url { get; set; }
            public string selected { get; set; }
            public string shortUrl { get; set; }
            public DateTime? Created { get; internal set; }
        }
        //        [WebMethod()]
        //        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        //        public static object GetData(string date, int status, int lvl2, string name)
        //        {
        //            var schoolId = UserData.CompanyID;
        //            var toDay = DateTime.Now.Date;

        //            DateTime _date;

        //            if (DateTime.TryParseExact(date, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out _date))
        //            {

        //            }

        //            string sql = $@"-- StudentCall/Report
        //SELECT A.SchoolID,A.sID,C.Base64Sound 'Sound'
        //, A.sNickName 'NickName' , ISNULL(F.titleDescription,A.sStudentTitle) 'Title', A.sName 'FirstName' , A.sLastname  'LastName'
        //, E.fullName 'Level1', D.nTSubLevel2 'Level2' 
        //, A.sStudentID 'Code' , A.sStudentPicture 'Img'
        //, CASE WHEN B.Status = 1 THEN B.Created WHEN B.Status = 2 THEN B.Announced WHEN B.Status = 3 THEN B.Completed END 'tTime'
        //, B.Status
        //--, G.ParentName 'Receiver' 

        //FROM JabjaiSchoolSingleDB.dbo.TUser A
        //INNER JOIN JabjaiMasterSingleDB.dbo.TStudentCall B on A.SchoolID = B.SchoolID and A.sID = B.sID 
        //LEFT JOIN JabjaiMasterSingleDB.dbo.TSound_Student C on A.SchoolID = C.SchoolID and A.sID = C.sID and A.nTermSubLevel2 = C.nTermSubLevel2
        //INNER JOIN JabjaiSchoolSingleDB.dbo.TTermSubLevel2 D on A.SchoolID = D.SchoolID and A.nTermSubLevel2 = D.nTermSubLevel2
        //INNER JOIN JabjaiSchoolSingleDB.dbo.TSubLevel E on D.SchoolID = E.SchoolID and D.nTSubLevel = E.nTSubLevel
        //LEFT JOIN  JabjaiSchoolSingleDB.dbo.TTitleList F on A.SchoolID = F.SchoolID and A.sStudentTitle   = CAST( F.nTitleid as nvarchar)
        //--LEFT JOIN JabjaiMasterSingleDB.dbo.TParent_Card G on A.SchoolID = G.SchoolID and A.sID = G.sID  and G.IsDel = 0 and G.IsActive = 1 

        //WHERE A.SchoolID = {schoolId} and B.CallDate = '{_date.ToString("yyyyMMdd")}' ";

        //            if (status != 0)
        //            {
        //                sql += " B.Status = " + status;
        //            }

        //            if (lvl2 != 0)
        //            {
        //                sql += " A.nTermSubLevel2 = " + lvl2;
        //            }

        //            if (!string.IsNullOrEmpty(name))
        //            {
        //                sql += $" (A.sStudentID + ' ' + A.sName + ' ' + A.sLastname) like '%{name}%' ";
        //            }

        //            using (JabJaiMasterEntities dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
        //            {
        //                var lst = dbmaster.Database.SqlQuery<ModelStudent>(sql).ToList();

        //                //foreach (var i in lst)
        //                //{
        //                ////    i.Level1 = i.Level1.Replace("ศึกษาปีที่", "")
        //                ////     .Replace("ประกาศนียบัตรวิชาชีพชั้นสูง", "ป ว ส")
        //                ////     .Replace("ประกาศนียบัตรวิชาชีพ", "ป ว ช");
        //                //    i.FullName = $"{i.Title} {i.FirstName} {i.LastName} ";
        //                //    i.Level = $"{i.Level1}/{i.Level2}";
        //                //    i.Time = i.tTime.ToString(@"hh\:mm\:ss");

        //                //}

        //                //return new
        //                //{
        //                //    status1 = lst.Where(o => o.Status == 1).OrderByDescending(o => o.Time),
        //                //    status2 = lst.Where(o => o.Status == 2).OrderByDescending(o => o.Time),
        //                //    status3 = lst.Where(o => o.Status == 3).OrderByDescending(o => o.Time),
        //                //};

        //                var d = _date.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

        //                return new
        //                {
        //                    data = lst.Select((o, i) => new
        //                    {
        //                        no = i + 1,
        //                        level = $"{o.Level1}/{o.Level2}",
        //                        code = o.Code,
        //                        title = o.Title,
        //                        fullName = $"{o.FirstName} {o.LastName}".Trim(),
        //                        status = o.Status == 1 ? "รอประกาศ" : (o.Status == 2 ? "ประกาศแล้ว" : "รับแล้ว"),
        //                        date1 = $"{_date}<br/>{o.tTime.ToString(@"hh\:mm\:ss")}",
        //                        //date2 = $"{_date}<br/>{o.tTime.ToString(@"hh\:mm\:ss")}",
        //                        isAnnouce = _date.Date == toDay.Date ? 1 : 0,
        //                    })
        //                };
        //            }

        //            //return true;
        //        }

        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod()]
        //public static object IsValid(string nfc, int sid, int no)
        //{
        //    var schoolId = UserData.CompanyID;

        //    try
        //    {
        //        nfc = fcommon.FormatStudentCardNumber(nfc);
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
        //    {
        //        var count2 = dbmaster.TParent_Card
        //            .Where(o => o.NFCEncrypt == nfc && o.SchoolID == schoolId && o.IsDel == false && o.sID == sid && o.No != no/* && o.IsActive == true*/ /*&& o.sID != sid && o.No != no*/)
        //            .Count();

        //        if (count2 > 0)
        //        {
        //            return false;
        //        }

        //        var count1 = dbmaster.TParent_Card
        //            .Where(o => o.NFCEncrypt == nfc && o.SchoolID == schoolId && o.IsDel == false && o.sID != sid/* && o.IsActive == true*/ /*&& o.sID != sid && o.No != no*/)
        //            .Count();

        //        if (count1 > 0)
        //        {
        //            return false;
        //        }


        //    }

        //    return true;
        //}

        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod()]
        //public static object SaveCard(int sid, byte no, bool status, string nfc, byte type, string parent)
        //{
        //    var schoolId = UserData.CompanyID;
        //    long _nfc;
        //    bool isValid = long.TryParse(nfc, out _nfc);


        //    if (isValid)
        //    {
        //        try
        //        {
        //            var isDup = false;
        //            var nfcEn = fcommon.FormatStudentCardNumber(nfc);

        //            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
        //            {

        //                var count2 = dbmaster.TParent_Card
        //                   .Where(o => o.NFCEncrypt == nfcEn && o.SchoolID == schoolId && o.IsDel == false && o.sID == sid && o.No != no)
        //                   .Count();

        //                if (count2 > 0)
        //                {
        //                    isDup = true;
        //                }
        //                else
        //                {
        //                    var count1 = dbmaster.TParent_Card
        //                       .Where(o => o.NFCEncrypt == nfcEn && o.SchoolID == schoolId && o.IsDel == false && o.sID != sid)
        //                       .Count();

        //                    if (count1 > 0)
        //                    {
        //                        isDup = true;
        //                    }
        //                }


        //                var q = dbmaster.TParent_Card
        //                    .Where(o => o.SchoolID == schoolId && o.sID == sid && o.No == no)
        //                    .FirstOrDefault();

        //                if (q == null)
        //                {
        //                    var card = new TParent_Card();
        //                    card.SchoolID = schoolId;
        //                    card.sID = sid;
        //                    card.No = no;
        //                    if (!isDup)
        //                    {
        //                        card.NFC = nfc;
        //                        card.NFCEncrypt = nfcEn;
        //                    }
        //                    card.Type = type;
        //                    //card.ParentName = parent;
        //                    card.IsActive = status;
        //                    card.IsDel = false;
        //                    card.CreateBy = UserData.UserID;
        //                    card.ModifyBy = UserData.UserID;
        //                    card.Created = DateTime.Now;
        //                    card.Modified = DateTime.Now;

        //                    dbmaster.TParent_Card.Add(card);
        //                }
        //                else
        //                {
        //                    if (!isDup)
        //                    {
        //                        q.NFC = nfc;
        //                        q.NFCEncrypt = nfcEn;
        //                    }
        //                    q.IsActive = status;
        //                    q.Type = type;
        //                    q.Modified = DateTime.Now;
        //                    q.ModifyBy = UserData.UserID;
        //                }

        //                dbmaster.SaveChanges();

        //                using (var dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
        //                {
        //                    var family = dbSchool.TFamilyProfiles.Where(o => o.sID == sid).FirstOrDefault();
        //                    var arr = parent.Split(' ');

        //                    switch (type)
        //                    {
        //                        case 1:
        //                            family.sFatherFirstName = arr.ElementAtOrDefault(0) + "";
        //                            family.sFatherFirstName = arr.ElementAtOrDefault(1) + "";
        //                            break;

        //                        case 2:
        //                            family.sMotherFirstName = arr.ElementAtOrDefault(0) + "";
        //                            family.sMotherLastName = arr.ElementAtOrDefault(1) + "";
        //                            break;

        //                        case 3:
        //                            family.sFamilyName = arr.ElementAtOrDefault(0) + "";
        //                            family.sFamilyLast = arr.ElementAtOrDefault(1) + "";
        //                            break;

        //                        default:
        //                            break;
        //                    }

        //                    dbSchool.SaveChanges();
        //                }

        //                return new { status = true, type = (isDup ? 1 : 0) };
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return new { status = false, msg = ex.Message };
        //        }
        //    }
        //    return new { status = false, msg = "-" };
        //}
    }
}