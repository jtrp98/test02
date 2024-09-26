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
    public partial class ParentCard : BaseStudentCall
    {
        //protected override int MenuID => PermissionHelper.MENUID_STUDENTCALL_MAIN;

        public TStudentCall_Config Config = new TStudentCall_Config();

        //public string BgFile { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                List<TSubLevel> SubLevel = new List<TSubLevel>();
                TSubLevel sub = new TSubLevel();

                using (var db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    SubLevel = db.TSubLevels.Where(w => w.SchoolID == UserData.CompanyID).Where(w => w.nWorkingStatus == 1).ToList();
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

            }

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var c = dbmaster.TStudentCall_Config.FirstOrDefault(o => o.SchoolId == UserData.CompanyID);
                if (c != null)
                    Config = c;
                //BgFile = c?.BgCard;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static object SaveData(string type, object val)
        {
            var userData = GetUserData();

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var config = dbmaster.TStudentCall_Config.FirstOrDefault(o => o.SchoolId == userData.CompanyID);

                if (config == null)
                {
                    config = new TStudentCall_Config();
                    config.SchoolId = userData.CompanyID;
                    config.CreateBy = userData.UserID;
                    config.Created = DateTime.Now;

                    dbmaster.TStudentCall_Config.Add(config);
                }

                config.ModifyBy = userData.UserID;
                config.Modified = DateTime.Now;

                switch (type)
                {
                    case "level":
                        config.IsShowLevel = (bool?)val;
                        break;
                    case "lastname":
                        config.IsShowLastName = (bool?)val;
                        break;
                    case "parent":
                        config.IsShowParent = (bool?)val;
                        break;
                    case "cardType":
                        config.CardType = Convert.ToByte(val);
                        break;
                    case "nameType":
                        config.NameType = Convert.ToByte(val);
                        break;
                    default:
                        break;
                }

                try
                {
                    dbmaster.SaveChanges();
                    return new { status = "ok" };
                }
                catch
                {
                    return new { status = "fail" };
                }
            }


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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static object IsValid(string nfc, int sid, int no)
        {
            var userData = GetUserData();

            var schoolId = userData.CompanyID;

            /*
             0 = empty
             1 = invalid
             2 = duplicate
             3 = valid
             */
            if (string.IsNullOrEmpty(nfc))
            {
                return new
                {
                    status = 0
                };
            }

            try
            {
                nfc = fcommon.FormatStudentCardNumber(nfc);
            }
            catch
            {
                return new
                {
                    status = 0
                };
            }

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var lst = (from a in dbmaster.TParent_Card.Where(o => o.SchoolID == schoolId && o.NFCEncrypt == nfc && o.IsDel == false)
                           from b in dbmaster.TUsers.Where(o => o.nCompany == a.SchoolID && o.sID == a.sID && o.cDel != "1")
                           select new
                           {
                               a.sID,
                               a.No,
                               fullName = b.sName + " " + b.sLastname,
                           }).ToList();

                var lst1 = lst.Where(o => o.sID == sid && o.No != no);

                if (lst1.Count() > 0)
                {
                    return lst1.Select(o => new
                    {
                        status = 2,
                        o.fullName,
                    }).FirstOrDefault();
                }

                var lst2 = lst.Where(o => o.sID != sid);

                if (lst2.Count() > 0)
                {
                    return lst2.Select(o => new
                    {
                        status = 2,
                        o.fullName,
                    }).FirstOrDefault();
                }
            }

            return new
            {
                status = 3
            };
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static object IsBarcodeValid(string barcode, int sid, int no)
        {
            var userData = GetUserData();

            var schoolId = userData.CompanyID;

            /*
             0 = empty
             1 = invalid
             2 = duplicate
             3 = valid
             */
            if (string.IsNullOrEmpty(barcode))
            {
                return new
                {
                    status = 0
                };
            }

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var lst = (from a in dbmaster.TParent_Card.Where(o => o.SchoolID == schoolId && o.Barcode == barcode && o.IsDel == false)
                           from b in dbmaster.TUsers.Where(o => o.nCompany == a.SchoolID && o.sID == a.sID && o.cDel != "1")
                           select new
                           {
                               a.sID,
                               a.No,
                               fullName = b.sName + " " + b.sLastname,
                           }).ToList();

                var lst1 = lst.Where(o => o.sID == sid && o.No != no);

                if (lst1.Count() > 0)
                {
                    return lst1.Select(o => new
                    {
                        status = 2,
                        o.fullName,
                    }).FirstOrDefault();
                }

                var lst2 = lst.Where(o => o.sID != sid);

                if (lst2.Count() > 0)
                {
                    return lst2.Select(o => new
                    {
                        status = 2,
                        o.fullName,
                    }).FirstOrDefault();
                }
            }

            return new
            {
                status = 3
            };
        }

        //protected static JWTToken.userData GetUserData()
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken.userData();
        //    if (token.CheckToken(HttpContext.Current))
        //    {
        //        userData = token.getTokenValues(HttpContext.Current);
        //    }
        //    else
        //    {
        //        HttpContext.Current.Response.Redirect("~/Default.aspx");
        //    }

        //    return userData;
        //}

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static object ToggleCard(int sid, byte no)
        {
            var userData = GetUserData();

            var schoolId = userData.CompanyID;

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var d = dbmaster.TParent_Card
                    .Where(o => o.SchoolID == schoolId
                        && o.sID == sid
                        && o.No == no)
                    .FirstOrDefault();

                if (d != null)
                {
                    d.IsActive = d.IsActive.HasValue ? !d.IsActive : null;
                }
                else
                {
                    var card = new TParent_Card();
                    card.SchoolID = schoolId;
                    card.sID = sid;
                    card.No = no;
                    card.IsActive = true;
                    card.IsDel = false;
                    card.CreateBy = userData.UserID;
                    card.ModifyBy = userData.UserID;
                    card.Created = DateTime.Now;
                    card.Modified = DateTime.Now;

                    dbmaster.TParent_Card.Add(card);
                }

                dbmaster.SaveChanges();

                return new { status = "success" };
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static object SaveCard(int sid, byte no, bool status, string nfc, byte type, string parent, string barcode)
        {
            var userData = GetUserData();

            // var userData = GetUserData();
            var schoolId = userData.CompanyID;
            //long _nfc;
            // bool isValid = long.TryParse(nfc, out _nfc);


            //if (isValid)
            {
                try
                {
                    var isDupNFC = false;
                    var isDupBarcode = false;

                    var nfcEn = "";
                    try
                    {
                        nfcEn = fcommon.FormatStudentCardNumber(nfc);
                    }
                    catch
                    {
                        nfcEn = "";
                    }

                    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        if (!string.IsNullOrEmpty(nfcEn.Trim()))
                        {
                            var lst = (from a in dbmaster.TParent_Card.Where(o => o.SchoolID == schoolId && o.NFCEncrypt == nfc && o.IsDel == false)
                                       from b in dbmaster.TUsers.Where(o => o.nCompany == a.SchoolID && o.sID == a.sID && o.cDel != "1")
                                       select new
                                       {
                                           a.sID,
                                           a.No,
                                           fullName = b.sName + " " + b.sLastname,
                                       }).ToList();

                            //self check
                            var countNFC = lst.Where(o => o.sID == sid && o.No != no).Count();

                            if (countNFC > 0)
                            {
                                isDupNFC = true;
                            }
                            else
                            {
                                //other one check
                                var _count = lst.Where(o => o.sID != sid).Count();

                                if (_count > 0)
                                {
                                    isDupNFC = true;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(barcode.Trim()))
                        {
                            var lst = (from a in dbmaster.TParent_Card.Where(o => o.SchoolID == schoolId && o.Barcode == barcode && o.IsDel == false)
                                       from b in dbmaster.TUsers.Where(o => o.nCompany == a.SchoolID && o.sID == a.sID && o.cDel != "1")
                                       select new
                                       {
                                           a.sID,
                                           a.No,
                                           fullName = b.sName + " " + b.sLastname,
                                       }).ToList();

                            var countBarcode = lst.Where(o => o.sID == sid && o.No != no).Count();

                            if (countBarcode > 0)
                            {
                                isDupBarcode = true;
                            }
                            else
                            {
                                //other one check
                                var _count = lst.Where(o => o.sID != sid).Count();

                                if (_count > 0)
                                {
                                    isDupBarcode = true;
                                }
                            }
                        }

                        var q = dbmaster.TParent_Card
                            .Where(o => o.SchoolID == schoolId && o.sID == sid && o.No == no)
                            .FirstOrDefault();

                        if (q == null)
                        {
                            var card = new TParent_Card();
                            card.SchoolID = schoolId;
                            card.sID = sid;
                            card.No = no;
                            if (!isDupNFC)
                            {
                                if (!string.IsNullOrEmpty(nfcEn))
                                {
                                    card.NFC = nfc;
                                    card.NFCEncrypt = nfcEn;
                                }
                            }
                            card.Type = type;
                            //card.ParentName = parent;
                            card.IsActive = status;
                            card.IsDel = false;
                            card.CreateBy = userData.UserID;
                            card.ModifyBy = userData.UserID;
                            card.Created = DateTime.Now;
                            card.Modified = DateTime.Now;

                            if (!isDupBarcode)
                            {
                                if (!string.IsNullOrEmpty(barcode))
                                {
                                    card.Barcode = barcode;
                                }
                            }

                            dbmaster.TParent_Card.Add(card);

                            var name = dbmaster.TUsers.Where(o => o.sID == sid && o.nCompany == schoolId).Select(o => o.sName + " " + o.sLastname).FirstOrDefault();

                            database.InsertLog(userData.UserID + "", $"บันทึก บัตรผู้ปกครอง NFC:'{card.NFC}' Barcode:{card.Barcode} บัตรที่:{no} นักเรียน ({sid}):{name}", HttpContext.Current.Request, 181, 0, 0, userData.CompanyID);
                        }
                        else
                        {
                            var oldNFC = q.NFC;
                            var oldBarcode = q.Barcode;
                            if (!isDupNFC)
                            {
                                //if (!string.IsNullOrEmpty(nfcEn))
                                //{
                                //    q.NFC = nfc;
                                //    q.NFCEncrypt = nfcEn;
                                //}
                                q.NFC = nfc;
                                q.NFCEncrypt = nfcEn;
                            }

                            if (!isDupBarcode)
                            {
                                q.Barcode = barcode;
                            }

                            q.IsActive = status;
                            q.Type = type;
                            q.Modified = DateTime.Now;
                            q.ModifyBy = userData.UserID;

                            var name = dbmaster.TUsers.Where(o => o.sID == sid && o.nCompany == schoolId).Select(o => o.sName + " " + o.sLastname).FirstOrDefault();

                            database.InsertLog(userData.UserID + "", $"บันทึก บัตรผู้ปกครอง NFC:'{oldNFC}' -> '{q.NFC}' Barcode:{oldBarcode} -> {q.Barcode} บัตรที่:{no} นักเรียน ({sid}):{name}", HttpContext.Current.Request, 181, 0, 0, userData.CompanyID);

                        }

                        dbmaster.SaveChanges();

                        using (var dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                        {
                            var family = dbSchool.TFamilyProfiles.Where(o => o.SchoolID == schoolId && o.sID == sid).FirstOrDefault();
                            var isNeedAdd = false;

                            if (family == null)
                            {
                                isNeedAdd = true;
                                family = new TFamilyProfile();
                                family.sID = sid;
                                family.sDeleted = "false";
                                family.SchoolID = schoolId;
                            }

                            var arr = parent.Split(' ');
                            switch (type)
                            {
                                case 1:
                                    family.sFatherFirstName = arr.ElementAtOrDefault(0) + "";
                                    family.sFatherLastName = arr.ElementAtOrDefault(1) + "";
                                    break;

                                case 2:
                                    family.sMotherFirstName = arr.ElementAtOrDefault(0) + "";
                                    family.sMotherLastName = arr.ElementAtOrDefault(1) + "";
                                    break;

                                case 3:
                                    family.sFamilyName = arr.ElementAtOrDefault(0) + "";
                                    family.sFamilyLast = arr.ElementAtOrDefault(1) + "";
                                    break;

                                default:
                                    break;
                            }

                            if (isNeedAdd)
                                dbSchool.TFamilyProfiles.Add(family);

                            dbSchool.SaveChanges();
                        }

                        return new
                        {
                            status = true,
                            typeNFC = (isDupNFC ? 1 : 0),
                            typeBarcode = (isDupBarcode ? 1 : 0),
                            encrypt = nfcEn
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new { status = false, msg = ex.Message };
                }
            }
            //return new { status = false, msg = "-" };
        }
    }
}