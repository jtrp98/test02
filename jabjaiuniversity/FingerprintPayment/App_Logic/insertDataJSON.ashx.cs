using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using SchoolBright.Business.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using urbanairship;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for insertDataJSON
    /// </summary>
    public class insertDataJSON : IHttpHandler, IRequiresSessionState
    {
        string ErrorMessage = "";
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            //string json =  new StreamReader(context.Request.InputStream).ReadToEnd();
            //IHomeWork Ihomework = JsonConvert.DeserializeObject<IHomeWork>(json);
            string mode = context.Request["mode"];
            mode = string.IsNullOrEmpty(mode) ? context.Request[GetString4Params("mode")] : mode;
            string Result = "";
            switch (mode)
            {
                case "homework":
                    Result = InsertHomeWork(context);
                    break;
                case "product":
                    Result = InsertProduct(context);
                    break;
                case "teacher":
                    Result = InsertDatateacher(context);
                    break;
                case "topup":
                    Result = Topup(context);
                    break;
                case "menu":
                    Result = UpdateMenu(context);
                    break;
                default:
                    Result = InsertDataSchedule(context);
                    break;

            }
            HttpStatusCode StatusCode = 0;
            switch (Result)
            {
                case "0": StatusCode = HttpStatusCode.OK; break;
                case "1": StatusCode = HttpStatusCode.InternalServerError; break;
                case "2": StatusCode = HttpStatusCode.Forbidden; break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)StatusCode;
            context.Response.Write(ErrorMessage);
            context.Response.Flush(); // Sends all currently buffered output to the client.
            context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

        }

        private string UpdateMenu(HttpContext context)
        {
            IUpdateMenu objUpdateMenu = Deserialize<IUpdateMenu>(context);
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"] + "";
                var qcomapny = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                var f_user = dbmaster.TUsers.FirstOrDefault(f => f.nSystemID == objUpdateMenu.user_id && f.nCompany == qcomapny.nCompany && f.cType == "1");
                var q_permission = dbmaster.permissions.Where(w => w.user_id == f_user.sID && w.type == "W").ToList();
                var q_add = new List<permission>();

                foreach (var data in objUpdateMenu.menu.OrderBy(o => o.menuid))
                {
                    var f_permission = q_permission.FirstOrDefault(f => f.menu_id == data.menuid);
                    if (f_permission == null)
                    {
                        q_add.Add(new permission
                        {
                            user_id = f_user.sID,
                            actvice = data.value,
                            menu_id = data.menuid,
                            type = "W"
                        });
                    }
                    else
                    {
                        f_permission.actvice = data.value;
                    }
                }

                dbmaster.permissions.AddRange(q_add);
                database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                        "ทำการแก้ไขค่าสิทธิ์ : " + f_user.sName + " " + f_user.sLastname,
                        HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 16, 3, 0);

                return dbmaster.SaveChanges() == 0 ? "1" : "2";
            }

        }

        private string Topup(HttpContext context)
        {
            try
            {
                int studentid = int.Parse(context.Request["id"]);
                int money = int.Parse(context.Request["money"]);
                int employess = int.Parse(HttpContext.Current.Session["sEmpID"] + "");
                string sEntities = HttpContext.Current.Session["sEntities"] + "";
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    var qstudent = dbschool.TUser.FirstOrDefault(f => f.sID == studentid);
                    decimal balance = qstudent.nMoney.Value;
                    dbschool.TMoneys.Add(new TMoney
                    {
                        cType = "0",
                        nMoney = money,
                        dMoney = DateTime.Now,
                        nBalance = balance,
                        sEmp = employess,
                        sID = studentid,
                        SchoolID = userData.CompanyID
                    });
                    qstudent.nMoney += money;
                    dbschool.SaveChanges();
                    database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                        "ทำการเติมเงิน : " + qstudent.sName + qstudent.sLastname,
                        HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, -1, 2, 0);

                    #region send notification
                    using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        notification _notification = new notification();
                        var qcompany = dbmaster.TCompanies.FirstOrDefault(x => x.sEntities == sEntities);
                        var quser = dbmaster.TUsers.FirstOrDefault(s => s.nSystemID == studentid && s.nCompany == qcompany.nCompany);
                        _notification.user = quser.sID.ToString();
                        _notification.title = quser.sName + " " + quser.sLastname;
                        int nMessageID = 1;
                        //if (dbmaster.TMessages.Count() > 0) nMessageID = dbmaster.TMessages.Max(M => M.nMessageID) + 1;
                        _notification.message = "เติมเงินในระบบจับจ่าย จำนวน " + money
                            + " บาท วันที่ " + DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-us"))
                            + " ยอดคงเหลือ " + money + balance
                            + " บาท หากไม่ถูกต้องติดต่อหมายเลข 091-8233139";

                        nMessageID = messagebox.insert_message(
                            new messagebox.MessageBox
                            {
                                message_type = 3,
                                send_time = DateTime.Now,
                                message = money + "," + (money + balance),
                                title = "เติมเงินในระบบ",
                                school_id = qcompany.nCompany,
                                user_messagebox = new List<messagebox.user_messagebox>() {
                                new messagebox.user_messagebox {
                                    user_id   = quser.sID,
                                }
                                },
                            });

                        _notification.action = "vnd.jabjai.jabjaiapp://deeplink/topup?message_id=" + nMessageID;

                        dbmaster.SaveChanges();
                        //pushdata.push(_notification);
                        return dbschool.SaveChanges() == 0 ? "1" : "0";
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.StackTrace;
                return "0";
            }
        }

        private string InsertDatateacher(HttpContext context)
        {
            UserInfo objUser = Deserialize<UserInfo>(context);
            int SubLevel2Id = int.Parse(context.Request["id"]);
            string TremId = context.Request["idterm"];
            string teacherId = context.Request["teacherid"];
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"] + "",ConnectionDB.Read)))
            {
                try
                {
                    //int TremTimeId = 0;
                    var tTermTimeTable = dbschool.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID).ToList();
                    int nTeacher = int.Parse(teacherId);
                    if (nTeacher > 0)
                    {

                        if (tTermTimeTable.Where(w => w.nTermSubLevel2 == SubLevel2Id && w.nTerm == TremId).Count() > 0)
                        {
                            foreach (var data in tTermTimeTable.Where(w => w.nTermSubLevel2 == SubLevel2Id && w.nTerm == TremId))
                            {
                                data.nTeacher = nTeacher;
                            }
                        }
                        else
                        {
                            //if (tTermTimeTable.ToList().Count == 0) TremTimeId = 1;
                            //TremTimeId = tTermTimeTable.Max(max => max.nTermTable) + 1;
                            dbschool.TTermTimeTables.Add(new TTermTimeTable
                            {
                                //nTermTable = TremTimeId,
                                nTerm = TremId,
                                nTermSubLevel2 = SubLevel2Id,
                                nTeacher = nTeacher,
                                SchoolID = userData.CompanyID
                            });
                        }
                    }
                    else
                    {
                        if (tTermTimeTable.Where(w => w.nTermSubLevel2 == SubLevel2Id && w.nTerm == TremId).Count() > 0)
                        {
                            foreach (var data in tTermTimeTable.Where(w => w.nTermSubLevel2 == SubLevel2Id && w.nTerm == TremId))
                            {
                                data.nTeacher = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }
                return dbschool.SaveChanges() == 0 ? "0" : "1";
            }
        }

        private string InsertProduct(HttpContext context)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"] + "", ConnectionDB.Read)))
            {
                int nProduct = 1;
                string sBarCode = context.Request[GetString4Params("barCode")];
                if (!string.IsNullOrEmpty(context.Request[GetString4Params("productid")]))
                {
                    nProduct = int.Parse(context.Request[GetString4Params("productid")]);
                }

                //if (dbschool.TProducts.Count(c => c.SchoolID == userData.CompanyID && string.IsNullOrEmpty(c.cDel) && c.sBarCode == sBarCode && c.nProductID != nProduct) > 0) return "2";
                TProduct _Product = new TProduct();
                int nType = int.Parse(context.Request[GetString4Params("type")]);

                var f_type = dbschool.TTypes.FirstOrDefault(f => f.nTypeID == nType);
                var countProduct = (from a in dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID && (w.cDel ?? "0") == "0")
                                    join b in dbschool.TTypes.Where(w => w.SchoolID == userData.CompanyID && (w.cDel ?? "0") == "0") on a.nType equals b.nTypeID
                                    where b.shop_id == f_type.shop_id && a.sBarCode == sBarCode && a.nProductID != nProduct
                                    select new
                                    {
                                        a.nProductID
                                    }).Count();

                if (!string.IsNullOrEmpty(context.Request[GetString4Params("productid")]))
                {
                    _Product = dbschool.TProducts.FirstOrDefault(f => f.nProductID == nProduct);
                    _Product.sProduct = context.Request[GetString4Params("productname")];
                    _Product.sBarCode = context.Request[GetString4Params("barCode")];
                    _Product.nCost = (context.Request[GetString4Params("cost")]).ToNumber<decimal>();
                    _Product.nPrice = decimal.Parse(context.Request[GetString4Params("price")]);
                    _Product.nType = int.Parse(context.Request[GetString4Params("type")]);
                    _Product.nWarn = int.Parse(context.Request[GetString4Params("warn")]);
                    _Product.cStock = context.Request[GetString4Params("stock")];
                    _Product.UnitID = context.Request[GetString4Params("unit")].ToNumber<int>();
                    _Product.sProduct = _Product.sProduct.Replace("'", "");// SB-708

                    database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                        "ทำการแก้ไขสินค้า " + _Product.sProduct,
                        HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 7, 2, 0);

                }
                else
                {
                    //var tProduct = dbschool.TProducts.Where(w => w.SchoolID == userData.CompanyID).ToList();
                    //if (tProduct.Count > 0) nProduct = tProduct.Max(max => max.nProductID) + 1;
                    //_Product.nProductID = nProduct;
                    _Product.sProduct = context.Request[GetString4Params("productname")];
                    _Product.sBarCode = context.Request[GetString4Params("barCode")];
                    _Product.nCost = (context.Request[GetString4Params("cost")]).ToNumber<decimal>(); ;
                    _Product.nPrice = decimal.Parse(context.Request[GetString4Params("price")]);
                    _Product.nType = int.Parse(context.Request[GetString4Params("type")]);
                    _Product.nWarn = int.Parse(context.Request[GetString4Params("warn")]);
                    _Product.cStock = context.Request[GetString4Params("stock")];
                    _Product.SchoolID = userData.CompanyID;
                    _Product.UnitID = context.Request[GetString4Params("unit")].ToNumber<int>();

                    if (countProduct > 0)
                    {
                        return "2";
                    }

                    _Product.sProduct = _Product.sProduct.Replace("'", "");// SB-708

                    dbschool.TProducts.Add(_Product);
                    database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                 "ทำการเพิ่มสินค้า " + _Product.sProduct,
                 HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 7, 3, 0);
                }
                try
                {


                    dbschool.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return "1";
                }

                Request_API request = new Request_API(API_Type.Payment_API);

                var f2 = dbschool.TTypes.FirstOrDefault(f => f.nTypeID == _Product.nType);

                string Json = "{" +
                    $"\"nProductID\" : \"{_Product.nProductID}\", " +
                    $"\"sProduct\" : \"{_Product.sProduct}\", " +
                    $"\"sBarCode\" : \"{_Product.sBarCode}\", " +
                    $"\"nNumber\" : \"{_Product.nNumber}\", " +
                    $"\"nCost\" : \"{_Product.nCost}\", " +
                    $"\"nPrice\" : \"{_Product.nPrice}\", " +
                    $"\"nBalance\" : \"{_Product.nBalance}\", " +
                    $"\"nType\" : \"{_Product.nType}\", " +
                    $"\"cDel\" : \"{_Product.cDel}\", " +
                    $"\"nWarn\" : \"{_Product.nWarn}\", " +
                    $"\"cStock\" : \"{_Product.cStock}\", " +
                    $"\"SchoolID\" : \"{_Product.SchoolID}\", " +
                    $"\"CreatedBy\" : \"{_Product.CreatedBy}\", " +
                    $"\"UpdatedBy\" : \"{_Product.UpdatedBy}\", " +
                    $"\"CreatedDate\" : \"{_Product.CreatedDate}\", " +
                    $"\"UpdatedDate\" : \"{_Product.UpdatedDate}\", " +
                    $"\"ShopID\" : \"{f2.shop_id}\", " +
                    "}";
                string response = request.POST(Json, $"/api/shop/pos/product", ("addormodify product").ToUpper(), userData.CompanyID).resultDes;

                return "0";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string InsertDataHomeWork(HttpContext context)
        {
            try
            {
                DateTime dayOrder = DateTime.Now; //DateTime.ParseExact(context.Request[GetString4Params("dayOrder")], "dd/MM/yyyy", new CultureInfo("en-us"));
                DateTime dayNotification = DateTime.ParseExact(context.Request["dayNotification"], "dd/MM/yyyy", new CultureInfo("en-us"));
                DateTime dayStart = DateTime.ParseExact(context.Request["dayStart"], "dd/MM/yyyy", new CultureInfo("en-us"));
                DateTime dayEnd = DateTime.ParseExact(context.Request["dayEnd"], "dd/MM/yyyy", new CultureInfo("en-us"));
                string planeid = context.Request["planeid"];
                string homeworkdetail = context.Request["homeworkdetail"];
                string SendData = context.Request["SendData"];
                string ArrayLevel = context.Request["ArrayLevel"];
                string ArrayUser = context.Request["ArrayUser"];
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                DateTime scheduled_time = DateTime.Now;
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {

                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);

                    using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                    {
                        //int nHomeWork = db.THomeworks.Count() == 0 ? 1 : db.THomeworks.Max(max => max.nHomeWork) + 1;
                        List<int> ArrayUserId = new List<int>();
                        int sEmp = int.Parse(HttpContext.Current.Session["sEmpID"].ToString());

                        int sPlaneId = 0;
                        int.TryParse(planeid, out sPlaneId);
                        THomework homework = new THomework
                        {
                            dEnd = dayEnd,
                            dStart = dayStart,
                            dNotification = dayNotification,
                            dOrder = dayOrder,
                            sHomeworkDetail = homeworkdetail,
                            sPlaneID = sPlaneId,
                            sEmp = sEmp,
                            SchoolID = userData.CompanyID
                        };
                        db.THomeworks.Add(homework);

                        if (context.Request.Files.Count > 0)
                        {
                            for (int i = 0; i < context.Request.Files.Count; i++)
                            {
                                HttpPostedFile PostedFile = context.Request.Files[i];
                                string link = AzureStorage.UploadFileNotAsync(PostedFile, "homework", qcompany.nCompany);

                                db.THomeWorkFiles.Add(new THomeWorkFile
                                {
                                    ContentType = PostedFile.ContentType,
                                    nHomeWorkId = homework.nHomeWork,
                                    sFileName = link,
                                    SchoolID = userData.CompanyID
                                });
                            }
                        }

                        db.SaveChanges();

                        if (SendData == "0")
                        {
                            foreach (string StrLevel in ArrayLevel.Split(','))
                            {
                                if (string.IsNullOrEmpty(StrLevel)) continue;
                                int nTermSubLevel2 = int.Parse(StrLevel);
                                foreach (var dataUser in db.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2 == nTermSubLevel2).ToList())
                                {
                                    ArrayUserId.Add(dataUser.sID);
                                    db.THomework_User.Add(new THomework_User
                                    {
                                        nHomeWork = homework.nHomeWork,
                                        sID = dataUser.sID,
                                        SchoolID = userData.CompanyID
                                    });
                                }
                            }
                        }
                        else if (SendData == "1")
                        {
                            foreach (string StrUser in ArrayUser.Split(','))
                            {
                                if (!string.IsNullOrEmpty(StrUser))
                                {
                                    int UserId = int.Parse(StrUser);
                                    ArrayUserId.Add(UserId);
                                    db.THomework_User.Add(new THomework_User
                                    {
                                        nHomeWork = homework.nHomeWork,
                                        sID = UserId,
                                        SchoolID = userData.CompanyID
                                    });
                                }
                            }
                        }

                        if (context.Request.Files.Count > 0)
                        {
                            for (int i = 0; i < context.Request.Files.Count; i++)
                            {
                                HttpPostedFile PostedFile = context.Request.Files[i];
                                string link = AzureStorage.UploadFileNotAsync(PostedFile, "homework", qcompany.nCompany);
                                db.THomeWorkFiles.Add(new THomeWorkFile
                                {
                                    ContentType = PostedFile.ContentType,
                                    nHomeWorkId = homework.nHomeWork,
                                    sFileName = link,
                                    SchoolID = userData.CompanyID
                                });
                            }
                        }

                        db.SaveChanges();
                        using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                        {
                            string UserNotification = "";
                            int nMessageID = 0;
                            string HomeWorkTitle = "การบ้านวิชา " + db.TPlanes.FirstOrDefault(f => f.sPlaneID == sPlaneId).sPlaneName;

                            var tCompany = dbMaster.TCompanies;
                            var tUser = dbMaster.TUsers;

                            foreach (var dataUser in (from a in tUser.Where(w => ArrayUserId.Contains(w.nSystemID.Value) && w.cType == "0")
                                                      join b in tCompany on a.nCompany equals b.nCompany
                                                      where b.sEntities == sEntities
                                                      select new { a.sID, a.sName, a.sLastname }))
                            {
                                UserNotification += (string.IsNullOrEmpty(UserNotification) ? "" : ",") + '"' + dataUser.sID + '"';
                                //dbMaster.TMessages.Add(new TMessage
                                //{
                                //    dSend = scheduled_time,
                                //    UserID = dataUser.sID,
                                //    nType = 5,
                                //    nMessageID = nMessageID,
                                //    nStatus = 0,
                                //    homework_id = homework.nHomeWork
                                //});

                            }

                            //pushdata.push("[" + UserNotification + "]", homeworkdetail, HomeWorkTitle, scheduled_time, nMessageID, qcompany.nCompany);
                            //pushdata.scheduled("[" + UserNotification + "]", homeworkdetail, HomeWorkTitle, scheduled_time, nMessageID, qcompany.nCompany);
                            dbMaster.SaveChanges();
                        }

                        database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                            "ทำการสั่งการบ้านวิชา " + db.TPlanes.FirstOrDefault(f => f.sPlaneID == sPlaneId).sPlaneName,
                            HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 3, 2, 0);

                        return "1";
                    }
                }
            }
            catch (Exception ex)
            {
                return "0";
            }

        }

        private string InsertHomeWork(HttpContext context)
        {
            try
            {
                #region get values from httpcontext
                homeworkupdate _homeworkupdate = new homeworkupdate();
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                string arraylevel = context.Request["ArrayLevel"].ToString();
                string arrayuser = context.Request["ArrayUser"].ToString();
                DateTime dayOrder = DateTime.Now; //DateTime.ParseExact(context.Request[GetString4Params("dayOrder")], "dd/MM/yyyy", new CultureInfo("en-us"));
                HttpRequest request = HttpContext.Current.Request;

                _homeworkupdate.daynotification = DateTime.ParseExact(context.Request["dayNotification"], "dd/MM/yyyy", new CultureInfo("en-us"));
                _homeworkupdate.daystart = DateTime.ParseExact(context.Request["dayStart"], "dd/MM/yyyy", new CultureInfo("en-us"));
                _homeworkupdate.dayend = DateTime.ParseExact(context.Request["dayEnd"], "dd/MM/yyyy", new CultureInfo("en-us"));
                _homeworkupdate.planeid = int.Parse(context.Request["planeid"]);
                _homeworkupdate.sendtype = context.Request["SendData"];
                _homeworkupdate.homeworkdetail = context.Request["homeworkdetail"];
                _homeworkupdate.arraylevel = string.IsNullOrEmpty(arraylevel) ? null : arraylevel.Split(',').Select(Int32.Parse).ToList();
                _homeworkupdate.arrayuser = string.IsNullOrEmpty(arrayuser) ? null : arrayuser.Split(',').Select(Int32.Parse).ToList();
                _homeworkupdate.teacherid = int.Parse(HttpContext.Current.Session["sEmpID"].ToString());
                #endregion

                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    _homeworkupdate.schoolid = qcompany.nCompany;
                    Double _span = (_homeworkupdate.daynotification.Value - DateTime.Now).TotalMinutes;
                    var qCompany = dbmaster.TCompanies.Where(w => w.nCompany == _homeworkupdate.schoolid).FirstOrDefault();
                    int nMessageID = 0;
                    var user_messagebox = new List<messagebox.user_messagebox>();
                    int user_type = 0;
                    using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(qcompany.sEntities,ConnectionDB.Read)))
                    {
                        int nHomeWork = db.THomeworks.Count() == 0 ? 1 : db.THomeworks.Max(max => max.nHomeWork) + 1;
                        List<int> ArrayUserId = new List<int>();

                        //int planeId = 0;
                        //int.TryParse(_homeworkupdate.planeid, out planeId);
                        db.THomeworks.Add(new THomework
                        {
                            dEnd = _homeworkupdate.dayend,
                            dStart = _homeworkupdate.daystart,
                            dNotification = _homeworkupdate.daynotification,
                            dOrder = DateTime.Now,
                            nHomeWork = nHomeWork,
                            sHomeworkDetail = _homeworkupdate.homeworkdetail,
                            sPlaneID = _homeworkupdate.planeid,
                            sEmp = _homeworkupdate.teacherid,
                            SchoolID = userData.CompanyID
                        });

                        if (context.Request.Files.Count > 0)
                        {
                            for (int i = 0; i < context.Request.Files.Count; i++)
                            {
                                HttpPostedFile PostedFile = context.Request.Files[i];
                                string link = AzureStorage.UploadFileNotAsync(PostedFile, "homework", qcompany.nCompany);
                                db.THomeWorkFiles.Add(new THomeWorkFile
                                {
                                    ContentType = PostedFile.ContentType.ToString(),
                                    nHomeWorkId = nHomeWork,
                                    sFileName = link,
                                    SchoolID = userData.CompanyID
                                });
                            }
                        }

                        db.SaveChanges();

                        var tUser = dbmaster.TUsers.Where(w => w.nCompany == qCompany.nCompany && w.cType == "0").
                            Select(s => new
                            {
                                s.nSystemID,
                                s.cType,
                                s.sName,
                                s.sLastname,
                                s.nCompany,
                                s.sID
                            }).ToList();

                        if (_homeworkupdate.sendtype == "0")
                        {
                            var qstudent = db.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList();
                            var q = (from a in qstudent
                                     join b in tUser on a.sID equals b.nSystemID
                                     where _homeworkupdate.arraylevel.Contains(a.nTermSubLevel2.Value)
                                     select new
                                     {
                                         b.sID,
                                         b.nSystemID
                                     }).ToList();

                            foreach (var dataUser in q)
                            {
                                ArrayUserId.Add(dataUser.sID);
                                db.THomework_User.Add(new THomework_User
                                {
                                    nHomeWork = nHomeWork,
                                    sID = dataUser.nSystemID.Value,
                                    SchoolID = userData.CompanyID
                                });

                                db.SaveChanges();
                            }
                        }
                        else if (_homeworkupdate.sendtype == "1")
                        {
                            foreach (int UserId in _homeworkupdate.arrayuser)
                            {
                                var sID = tUser.FirstOrDefault(f => f.nSystemID == UserId);
                                ArrayUserId.Add(sID.sID);
                                db.THomework_User.Add(new THomework_User
                                {
                                    nHomeWork = nHomeWork,
                                    sID = sID.nSystemID.Value,
                                    SchoolID = userData.CompanyID
                                });
                                db.SaveChanges();

                            }
                        }

                        string UserNotification = "";
                        string HomeWorkTitle = "การบ้านวิชา " + db.TPlanes.FirstOrDefault(f => f.sPlaneID == _homeworkupdate.planeid).sPlaneName;

                        foreach (var dataUser in (from a in tUser
                                                  where ArrayUserId.Contains(a.sID)
                                                  select new { a.sID, a.sName, a.sLastname }))
                        {
                            UserNotification += (string.IsNullOrEmpty(UserNotification) ? "" : ",") + '"' + dataUser.sID + '"';

                            user_messagebox.Add(new messagebox.user_messagebox
                            {
                                user_id = dataUser.sID,
                                user_type = user_type
                            });
                        }

                        nMessageID = messagebox.insert_message(
                            new messagebox.MessageBox
                            {
                                message_type = messagebox.HomeWork,
                                title = HomeWorkTitle,
                                message_type_id = nHomeWork,
                                school_id = _homeworkupdate.schoolid,
                                user_messagebox = user_messagebox,
                                send_time = DateTime.Now
                            });

                        //HostingEnvironment.QueueBackgroundWorkItem(ct => pushdata.push("[" + UserNotification + "]", _homeworkupdate.homeworkdetail, HomeWorkTitle, _homeworkupdate.daynotification.Value.AddMinutes(-_span), nMessageID, qcompany.nCompany));

                        //HostingEnvironment.QueueBackgroundWorkItem(ct => pushdata.scheduled("[" + UserNotification + "]", _homeworkupdate.homeworkdetail, HomeWorkTitle, DateTime.UtcNow.AddMinutes(_span), nMessageID, qcompany.nCompany));

                        string sEmpID = _homeworkupdate.teacherid.Value.ToString();
                        var qplane = db.TPlanes.FirstOrDefault(f => f.sPlaneID == _homeworkupdate.planeid);

                        database.InsertLog(sEmpID,
                            "ทำการแก้ไขสั่งการบ้านวิชา " + qplane.courseCode + " - " + qplane.sPlaneName, sEntities, request, 3, 2, 0);

                        return "1";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Source : {0} \r\n StackTrace : {1} \r\n Message : {2} \r\n", ex.Source.ToString(), ex.StackTrace.ToString(), ex.Message.ToString());
                return "0";
            }
        }

        public string GetString4Params(string Parameter)
        {
            return string.Format("newObject[{0}]", Parameter);
        }
        public string InsertDataSchedule(HttpContext context)
        {
            int SubLevel2Id = int.Parse(context.Request["id"]);
            string TremId = context.Request["idterm"];
            int sScheduleID = int.Parse(context.Request["scheduleid"]);
            try
            {
                //deserialize the object
                //UserInfo objUser = Deserialize<UserInfo>(context);


                using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"] + "", ConnectionDB.Read)))
                {
                    int TremTimeId = 0;
                    var tTermTimeTable = db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID).ToList();
                    if (tTermTimeTable.Where(w => w.nTermSubLevel2 == SubLevel2Id && w.nTerm == TremId).Count() > 0)
                    {
                        foreach (var data in tTermTimeTable.Where(w => w.nTermSubLevel2 == SubLevel2Id && w.nTerm == TremId))
                        {
                            TremTimeId = data.nTermTable;
                        }
                    }
                    else
                    {
                        //if (tTermTimeTable.ToList().Count == 0) TremTimeId = 1;
                        //else TremTimeId = tTermTimeTable.Max(max => max.nTermTable) + 1;
                        var ttermtimetables = new TTermTimeTable
                        {
                            //nTermTable = TremTimeId,
                            nTerm = TremId,
                            nTermSubLevel2 = SubLevel2Id,
                            SchoolID = userData.CompanyID
                        };
                        db.TTermTimeTables.Add(ttermtimetables);
                        db.SaveChanges();
                        TremTimeId = ttermtimetables.nTermTable;
                    }

                    bool Active = bool.Parse(context.Request["active"]);
                    TimeSpan dTimeEnd_IN = Active ? TimeSpan.Parse(context.Request["dTimeEnd_IN"]) : TimeSpan.Zero;
                    TimeSpan dTimeEnd_OUT = Active ? TimeSpan.Parse(context.Request["dTimeEnd_OUT"]) : TimeSpan.Zero;
                    TimeSpan dTimeHalf = Active ? TimeSpan.Parse(context.Request["dTimeHalf"]) : TimeSpan.Zero;
                    TimeSpan dTimeStart_IN = Active ? TimeSpan.Parse(context.Request["dTimeStart_IN"]) : TimeSpan.Zero;
                    TimeSpan dTimeStart_OUT = Active ? TimeSpan.Parse(context.Request["dTimeStart_OUT"]) : TimeSpan.Zero;
                    TimeSpan tEnd = TimeSpan.Parse(context.Request["tEnd"]);
                    TimeSpan tStart = TimeSpan.Parse(context.Request["tStart"]);
                    int nPlaneDay = int.Parse(context.Request["nPlaneDay"]);
                    int nTimeLate = Active ? int.Parse(context.Request["nTimeLate"]) : 0;
                    int? sEmp = null;
                    if (!string.IsNullOrEmpty(context.Request["sEmp"])) sEmp = int.Parse(context.Request["sEmp"]);
                    string sPlaneID = context.Request["sPlaneID"];
                    string sClassID = string.IsNullOrEmpty(context.Request["sClassID"]) ? null : context.Request["sClassID"];
                    bool Calculate = bool.Parse(context.Request["calculate"]);
                    int planeid = 0;
                    int.TryParse(sPlaneID, out planeid);
                    var dateTime = DateTime.Now;

                    if (sScheduleID == 0)
                    {
                        //if (db.TSchedules.Count() > 0)
                        //    sScheduleID = db.TSchedules.Max(max => max.sScheduleID) + 1;
                        //else sScheduleID = 1;
                        var schedules = new TSchedule
                        {
                            dTimeEnd_IN = dTimeEnd_IN,
                            dTimeEnd_OUT = dTimeEnd_OUT,
                            dTimeHalf = dTimeHalf,
                            dTimeStart_IN = dTimeStart_IN,
                            dTimeStart_OUT = dTimeStart_OUT,
                            tEnd = tEnd,
                            tStart = tStart,
                            nPlaneDay = nPlaneDay,
                            sPlaneID = planeid,
                            nTermTable = TremTimeId,
                            //sScheduleID = sScheduleID,
                            sEmp = sEmp,
                            sClassID = sClassID,
                            nTimeLate = nTimeLate,
                            cActive = Active,
                            calculate = Calculate,
                            SchoolID = userData.CompanyID,
                            CreatedBy = userData.UserID,
                            CreatedDate = dateTime
                        };
                        if (planeid != 0)
                        {
                            db.TSchedules.Add(schedules);
                            db.SaveChanges();

                            sScheduleID = schedules.sScheduleID;
                        }
                    }
                    else
                    {
                        TSchedule tSchedule = db.TSchedules.FirstOrDefault(f => f.sScheduleID == sScheduleID);
                        tSchedule.dTimeEnd_IN = dTimeEnd_IN;
                        tSchedule.dTimeEnd_OUT = dTimeEnd_OUT;
                        tSchedule.dTimeHalf = dTimeHalf;
                        tSchedule.dTimeStart_IN = dTimeStart_IN;
                        tSchedule.dTimeStart_OUT = dTimeStart_OUT;
                        tSchedule.nTimeLate = nTimeLate;
                        tSchedule.sEmp = sEmp;
                        tSchedule.sClassID = sClassID;
                        tSchedule.cActive = Active;
                        tSchedule.sPlaneID = planeid;
                        tSchedule.cActive = Active;
                        tSchedule.calculate = Calculate;
                        tSchedule.SchoolID = userData.CompanyID;
                        tSchedule.UpdatedBy = userData.UserID;
                        tSchedule.UpdatedDate = dateTime;
                        db.SaveChanges();


                    }
                    var tPlane = db.TPlanes.AsQueryable().Where(w => w.SchoolID == userData.CompanyID && w.sPlaneID == planeid).FirstOrDefault();
                    var teacherName = string.Empty;
                    var tEmployee = db.TEmployees.AsQueryable().Where(w => w.SchoolID == userData.CompanyID && w.sEmp == sEmp).FirstOrDefault();
                    teacherName = (tEmployee != null) ? string.Format("Teacher ID: {0}, Name: {1} {2}", sEmp, tEmployee.sName, tEmployee.sLastname) : string.Empty;
                    var newData = string.Format("ทำการแก้ไขตารางสอนวิชา ชื่อวิชา:{0}-{1}, ชื่อผู้สอน:{2}, รหัสกำหนดการ:{3}", tPlane.courseCode, tPlane.sPlaneName, teacherName, sScheduleID);

                    database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                       newData,
                        HttpContext.Current.Session["sEntities"].ToString(),
                        HttpContext.Current.Request, 1, 3, 0);

                }
                return "0";
            }
            catch (Exception ex)
            {
                string parameters = string.Format("sScheduleID:{0},SubLevel2Id:{1},TremId{2}", sScheduleID, SubLevel2Id, TremId);

                Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "insertDataJSON.InsertDataSchedule", parameters, "", null);
                ErrorMessage = string.Format("Source : {0} \r\n StackTrace : {1} \r\n Message : {2} \r\n", ex.Source.ToString(), ex.StackTrace.ToString(), ex.Message.ToString());
                return "1";
            }
        }
        /// <summary>
        /// This function will take httpcontext object and will read the input stream
        /// It will use the built in JavascriptSerializer framework to deserialize object based given T object type value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public T Deserialize<T>(HttpContext context)
        {
            //read the json string
            string jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();

            //cast to specified objectType
            var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);

            //return the object
            return obj;
        }

    }

    // we create a class object to hold the JSON value
    public class UserInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
    }

    public class IUpdateMenu
    {
        public int user_id { get; set; }
        public List<menu> menu { get; set; }
    }

    public class menu
    {
        public int menuid { get; set; }
        public int value { get; set; }
    }

    class IHomeWork
    {
        public DateTime dayNotification { get; set; }
        public DateTime dayStart { get; set; }
        public DateTime dayEnd { get; set; }
        public string planeid { get; set; }
        public string homeworkdetail { get; set; }
        public string SendData { get; set; }
        public string ArrayLevel { get; set; }
        public string ArrayUser { get; set; }
        public MultipartFileData file { get; set; }
    }

    class Fileupload
    {
        public string linkurl { get; set; }
        public string ContentType { get; set; }
    }

    class homeworkupdate
    {
        public List<int> arraylevel { get; set; }
        public List<int> arrayuser { get; set; }
        public string sendtype { get; set; }
        public DateTime? dayend { get; set; }
        public DateTime? daynotification { get; set; }
        //public DateTime? dayorder { get; set; }
        public DateTime? daystart { get; set; }
        public string homeworkdetail { get; set; }
        public int planeid { get; set; }
        //public DateTime? scheduled_time { get; set; }
        public int schoolid { get; set; }
        public int? teacherid { get; set; }
    }
}