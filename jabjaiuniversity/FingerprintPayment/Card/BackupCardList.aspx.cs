using FingerprintPayment.Card.CsCode;
using FingerprintPayment.Memory;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiTopup;
using MasterEntity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Card
{
    public partial class BackupCardList : CardGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadBackupCard(string searchName)
        {
            //var jsonStream = "";
            //HttpContext.Current.Request.InputStream.Position = 0;
            //using (var inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            //{
            //    jsonStream = inputStream.ReadToEnd();
            //}
            //var serializer = new JavaScriptSerializer();
            //dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            //int draw = Convert.ToInt32(jsonObject["draw"]);
            //int pageIndex = Convert.ToInt32(jsonObject["page"]);
            //int pageSize = Convert.ToInt32(jsonObject["length"]);
            //string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            //string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            //string sortBy = "CardID";
            //switch (sortIndex)
            //{
            //    case "1": sortBy = "CardName"; break;
            //    case "2": sortBy = "BarCode"; break;
            //    case "3": sortBy = "NFC"; break;
            //    case "4": sortBy = "Money"; break;
            //    case "5": sortBy = "UserType"; break;
            //    case "6": sortBy = "UserName"; break;
            //    case "7": sortBy = "BorrowingDate"; break;
            //}
            //sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            ////
            //string searchName = Convert.ToString(jsonObject["searchName"]);

            var json = QueryEngine.LoadBackupCardJsonData(GetUserData().CompanyID, searchName);

            return json;
        }

        [WebMethod]
        public static string RemoveItem(string cid)
        {
           

                JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                bool success = true;
                int statusCode = 200;
                string message = "Send Successfully";

                try
                {
                    // School Data
                    Guid guidCId = Guid.Parse(cid);
                    var pi = en.TBackupCards.First(f => f.SchoolID == schoolID && f.CardID == guidCId && f.cDel == false);
                    if (pi != null)
                    {
                        int c = en.TBackupCardHistories.Where(w => w.SchoolID == schoolID && w.CardID == guidCId && w.BorrowingDate != null && w.ReturnDate == null).Count();
                        if (c == 0)
                        {
                            pi.cDel = true;
                            pi.UpdatedBy = userData.UserID;
                            pi.UpdatedDate = DateTime.Now;

                            en.SaveChanges();
                            // JabjaiMainClass.Autocompletes.TopupMoney.Remove(cid);

                            try
                            {
                                JabjaiMainClass.Autocompletes.TopupMoney.topupMoney.RemoveAll(x => x.User_Id.ToUpper() == cid.ToUpper());
                            }
                            catch
                            {

                            }



                            database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลบัตรสำรอง " + pi.CardID + " " + pi.CardName, userData.Entities, HttpContext.Current.Request, 175, 4, 0);
                        }
                        else
                        {
                            success = false;
                            statusCode = 401;
                            message = "ไม่สามารถลบรายการนี้ได้ บัตรสำรองนี้ถูกใช้งานอยู่";
                        }
                    }
                    else
                    {
                        success = false;
                        statusCode = 401;
                        message = "ไม่พบบัตรสำรอง";
                    }
                }
                catch (Exception error)
                {
                    success = false;
                    statusCode = 500;
                    message = error.Message;
                }

                var result = new { success, statusCode, message };

                return JsonConvert.SerializeObject(result);
            }
        }

        [WebMethod]
        public static string[] GetBackupCardName(string keyword)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string sqlQuery = string.Format(@"
SELECT DISTINCT TOP 20 bc.CardName
FROM TBackupCard bc 
LEFT JOIN TBackupCardHistory bch ON bc.CardID = bch.CardID AND bc.SchoolID = bch.SchoolID
WHERE bc.cDel = 0 AND bc.SchoolID = {0} AND (bch.UserName LIKE '%{1}%' OR bc.CardName LIKE '%{1}%' OR bc.BarCode LIKE '%{1}%' OR bc.NFC LIKE '%{1}%')", schoolID, keyword);
                List<string> result = dbschool.Database.SqlQuery<string>(sqlQuery).ToList();

                return result.ToArray();
            }
        }

        [WebMethod]
        public static List<ListUserName> GetUserName(string keyword, string userType)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string sqlQuery = "";
                switch (userType)
                {
                    case "0":
                        sqlQuery = string.Format(@"
SELECT TOP 20 sID 'id', sName+' '+sLastname 'name'
FROM TUser
WHERE cDel IS NULL AND (nStudentStatus = 0 OR nStudentStatus IS NULL) AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%')
ORDER BY sName, sLastname", schoolID, keyword);
                        break;
                    case "1":
                        sqlQuery = string.Format(@"
SELECT TOP 20 sEmp 'id', sName+' '+sLastname 'name'
FROM TEmployees
WHERE cDel IS NULL AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%')
ORDER BY sName, sLastname", schoolID, keyword);
                        break;
                }
                List<ListUserName> result = dbschool.Database.SqlQuery<ListUserName>(sqlQuery).ToList();

                return result;
            }
        }

        public class ListUserName
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        [WebMethod]
        public static object SaveAddBackupCard(NewBackupCard newBackupCard)
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;

                using(JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    

                    var nfcEncrypt = (fcommon.FormatStudentCardNumber(newBackupCard.nfc) + "").ToUpper();

                    var c1 = dbMaster.TUser_Card
                        .Where(w => w.SchoolID == schoolID && w.IsDel == false && (w.NFCEncrypt.ToUpper() == nfcEncrypt || w.NFCEncryptReverse.ToUpper() == nfcEncrypt))
                        .Count();

                    var c2 = dbSchool.TUser.Where(w => w.SchoolID == schoolID && (w.sStudentID == newBackupCard.barCode.Trim() || w.sStudentID == newBackupCard.nfc.Trim()) && w.cDel == null).Count();

                    var c = dbSchool.TBackupCards.Where(w => w.SchoolID == schoolID && w.cDel == false
                    && (w.CardName == newBackupCard.cardName.Trim() || w.BarCode == newBackupCard.barCode.Trim() || w.NFCEncrypt.ToUpper() == nfcEncrypt || w.NFCEncryptReverse.ToUpper() == nfcEncrypt))
                        .Count();

                    if (c == 0 && c1 == 0 && c2 == 0)
                    {
                        TBackupCard backupCard = new TBackupCard
                        {
                            CardID = Guid.NewGuid(),
                            SchoolID = schoolID,
                            CardName = newBackupCard.cardName,
                            BarCode = string.IsNullOrEmpty(newBackupCard.barCode) ? null : newBackupCard.barCode,
                            NFC = string.IsNullOrEmpty(newBackupCard.nfc) ? null : newBackupCard.nfc,
                            NFCEncrypt = string.IsNullOrEmpty(newBackupCard.nfc) ? null : fcommon.FormatStudentCardNumber(newBackupCard.nfc),
                            Insurance = string.IsNullOrEmpty(newBackupCard.insurance) ? (decimal?)null : Convert.ToDecimal(newBackupCard.insurance),
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now,
                            cDel = false
                        };

                        backupCard.NFCEncryptReverse = fcommon.NFCEncryptRevese(backupCard.NFCEncrypt);
                        backupCard.NFCReverse = fcommon.DeFormatStudentCardNumber(backupCard.NFCEncryptReverse);

                        dbSchool.TBackupCards.Add(backupCard);

                        dbSchool.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลบัตรสำรอง " + backupCard.CardName + ", รหัส " + (string.IsNullOrEmpty(newBackupCard.barCode) ? newBackupCard.nfc : newBackupCard.barCode), userData.Entities, HttpContext.Current.Request, 175, 2, 0);
                    }
                    else
                    {
                        success = false;
                        statusCode = 401;
                        message = "ไม่สามารถบันทึกรายการนี้ได้เนื่องจาก: ชื่อบัตร/รหัสบัตร/รหัสบาร์โค้ด มีอยู่ในระบบแล้ว (" + (c1 != 0 ? "ข้อมูลบัตรนักเรียน" : (c != 0 ? "ข้อมูลบัตรสำรอง" : "รหัสนักเรียน")) + ")";
                    }
                }
            }
            catch (Exception error)
            {
                success = false;
                statusCode = 500;
                message = error.Message;
            }

            var result = new { success, statusCode, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveEditBackupCard(NewBackupCard newBackupCard)
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    Guid guidCId = Guid.Parse(newBackupCard.cid);
                    var c = dbSchool.TBackupCards.Where(w => w.SchoolID == schoolID && w.cDel == false && w.CardName == newBackupCard.cardName.Trim() && w.CardID != guidCId).Count();
                    if (c == 0)
                    {
                        TBackupCard backupCard = dbSchool.TBackupCards.First(f => f.CardID == guidCId);
                        if (backupCard != null)
                        {
                            backupCard.CardName = newBackupCard.cardName;

                            // Check card is borrow flag
                            int c1 = dbSchool.TBackupCardHistories.Where(w => w.SchoolID == schoolID && w.CardID == guidCId && w.ReturnDate == null && w.cDel == false).Count();
                            if (c1 == 0)
                            {
                                backupCard.Insurance = string.IsNullOrEmpty(newBackupCard.insurance) ? (decimal?)null : Convert.ToDecimal(newBackupCard.insurance);
                            }

                            dbSchool.SaveChanges();
                            //JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(schoolID, backupCard.CardID.ToString(), "2");
                        }

                        database.InsertLog(userData.UserID.ToString(), "แก้ไขข้อมูลบัตรสำรอง " + backupCard.CardName, userData.Entities, HttpContext.Current.Request, 175, 3, 0);
                    }
                    else
                    {
                        success = false;
                        statusCode = 401;
                        message = "ไม่สามารถแก้ไชรายการนี้ได้เนื่องจาก: ชื่อบัตรซ้ำกับบัตรสำรองอื่นในระบบ";
                    }
                }
            }
            catch (Exception error)
            {
                success = false;
                statusCode = 500;
                message = error.Message;
            }

            var result = new { success, statusCode, message };

            return JsonConvert.SerializeObject(result);
        }

        public class NewBackupCard
        {
            public string cid { get; set; }
            public string cardName { get; set; }
            public string nfc { get; set; }
            public string barCode { get; set; }
            public string insurance { get; set; }
        }

        [WebMethod]
        public static object GetBackupCardInfo(string chid)
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";
            var info = new BackupCardInfo();

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    Guid guidChId = Guid.Parse(chid);
                    var i = dbSchool.TBackupCardHistories.Where(w => w.SchoolID == schoolID && w.cDel == false && w.ReturnDate == null && w.CardHistoryID == guidChId).FirstOrDefault();
                    if (i != null)
                    {
                        switch (i.UserType)
                        {
                            case 0: info.userType = "นักเรียน"; break;
                            case 1: info.userType = "อาจารย์"; break;
                            case 2: info.userType = "บุคคลภายนออก"; break;
                        }
                        info.userName = i.UserName;
                        info.balance = 0;
                        var ii = dbSchool.TBackupCards.Where(w => w.CardID == i.CardID).FirstOrDefault();
                        if (ii != null)
                        {
                            info.balance = ii.Money;
                        }
                        info.insurance = i.Insurance;
                    }
                    else
                    {
                        success = false;
                        statusCode = 401;
                        message = "ไม่พบการยืมบัตรสำรองใบนี้";
                    }
                }
            }
            catch (Exception error)
            {
                success = false;
                statusCode = 500;
                message = error.Message;
            }

            var result = new { success, statusCode, message, info };

            return JsonConvert.SerializeObject(result);
        }

        public class BackupCardInfo
        {
            public string userType { get; set; }
            public string userName { get; set; }
            public decimal? balance { get; set; }
            public decimal? insurance { get; set; }
        }

        [WebMethod]
        public static object SaveReturnBackupCard(ReturnBackupCard returnBackupCard)
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                
                    try
                    {
                        Guid guidChId = Guid.Parse(returnBackupCard.chid);
                        TBackupCardHistory backupCardHistory = dbSchool.TBackupCardHistories.Where(w => w.SchoolID == schoolID && w.cDel == false && w.ReturnDate == null && w.CardHistoryID == guidChId).FirstOrDefault();
                        if (backupCardHistory != null)
                        {
                            backupCardHistory.ReturnDate = DateTime.Now;
                            backupCardHistory.UpdatedBy = userData.UserID;
                            backupCardHistory.UpdatedDate = DateTime.Now;
                            dbSchool.SaveChanges();

                            //TBackupCard backupCard = dbSchool.TBackupCards.Where(w => w.CardID == backupCardHistory.CardID).FirstOrDefault();

                            //returnBackupCard.balance += backupCardHistory.Insurance;

                            // Save data to TWithdrawal
                            WithdrawalLogic logic = new WithdrawalLogic(new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)));
                            TWithdrawal withdrawal = logic.BackupCardUpdata(new TWithdrawal
                            {
                                nMoney = returnBackupCard.balance,
                                userAdd = userData.UserID,
                                dWithdrawal = DateTime.Now,
                                userType = backupCardHistory.UserType.Value.ToString(),
                                UserID = backupCardHistory.UserID,
                                CardHistoryID = backupCardHistory.CardHistoryID,
                                sWithdrawalType = "WBS01",
                                SchoolID = schoolID,
                                CreatedBy = userData.UserID,
                                CreatedDate = DateTime.Now
                            }, backupCardHistory);

                            //// Update Backup Card Money
                            //if (backupCard != null)
                            //{
                            //    if (!backupCard.Money.HasValue) backupCard.Money = 0;
                            //    backupCard.Money -= returnBackupCard.balance;
                            //    dbSchool.SaveChanges();
                            //}

                            if (withdrawal == null)
                            {
                                // fail
                                success = false;
                                statusCode = 401;
                                message = "ไม่สามารถคืนบัตรสำรองใบนี้ได้เนื่องจาก Withdrawal Function Fail";

                               
                            }
                            else
                            {
                                TBackupCard backupCard = dbSchool.TBackupCards.Where(w => w.CardID == backupCardHistory.CardID && w.cDel == false).FirstOrDefault();

                                TempCard tempCard = new TempCard();
                                tempCard.AddOrModifyTempCard(new TempCard
                                {
                                    BarCode = backupCard.BarCode,
                                    CardID = backupCard.CardID,
                                    CardName = "",
                                    Money = 0,
                                    NFC = backupCard.NFC,
                                    SchoolID = backupCard.SchoolID,
                                    UserID = backupCardHistory.UserID ?? 0,
                                    UserType = backupCardHistory.UserType ?? 2,
                                    cDel = true,
                                    ReturnDate = backupCardHistory.ReturnDate,
                                    NFCEncrypt = backupCard.NFCEncrypt
                                }, userData.AuthKey, userData.AuthValue);

                                // success
                               
                                //JabjaiMainClass.Autocompletes.TopupMoney.Remove(backupCard.CardID.ToString());

                                try
                                {
                                    string userid = backupCard.CardID.ToString();
                                    JabjaiMainClass.Autocompletes.TopupMoney.topupMoney.RemoveAll(x => x.User_Id.ToUpper() == userid.ToUpper());
                                }
                                catch
                                {

                                }


                            }
                        }
                        else
                        {
                            success = false;
                            statusCode = 401;
                            message = "ไม่พบการยืมบัตรสำรองใบนี้";
                        }
                    }
                    catch (Exception error)
                    {
                        success = false;
                        statusCode = 500;
                        message = error.Message;

                       
                    }
                
            }

            var result = new { success, statusCode, message };

            return JsonConvert.SerializeObject(result);
        }

        public class ReturnBackupCard
        {
            public string chid { get; set; }
            public decimal? balance { get; set; }
        }

        [WebMethod]
        public static object SaveBorrowBackupCard(BorrowBackupCard borrowBackupCard)
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";
            string keyword = "";

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                TBackupCardHistory backupCardHistory = new TBackupCardHistory();
              
                    try
                    {
                        Guid guidCId = Guid.Parse(borrowBackupCard.cid);
                        var c = dbSchool.TBackupCardHistories.Where(w => w.SchoolID == schoolID && w.cDel == false && w.ReturnDate == null && w.CardID == guidCId).Count();
                        if (c == 0)
                        {
                            TBackupCard backupCard = dbSchool.TBackupCards.Where(w => w.CardID == guidCId && w.cDel == false).FirstOrDefault();
                            if (backupCard != null)
                            {
                                borrowBackupCard.balance -= backupCard.Insurance;
                            }

                            backupCardHistory = new TBackupCardHistory
                            {
                                CardHistoryID = Guid.NewGuid(),
                                SchoolID = schoolID,
                                CardID = guidCId,
                                UserType = Convert.ToInt32(borrowBackupCard.userType),
                                UserID = string.IsNullOrEmpty(borrowBackupCard.userID) ? (int?)null : Convert.ToInt32(borrowBackupCard.userID),
                                UserName = borrowBackupCard.userName,
                                BorrowingDate = DateTime.Now,
                                CreatedBy = userData.UserID,
                                CreatedDate = DateTime.Now,
                                cDel = false,
                                Insurance = backupCard.Insurance
                            };
                            dbSchool.TBackupCardHistories.Add(backupCardHistory);
                            dbSchool.SaveChanges();

                            //TBackupCard backupCard = dbSchool.TBackupCards.Where(w => w.CardID == backupCardHistory.CardID).FirstOrDefault();
                            //if (backupCard != null)
                            //{
                            //    if (!backupCard.Money.HasValue) backupCard.Money = 0;
                            //    backupCard.Money += borrowBackupCard.balance;
                            //    dbSchool.SaveChanges();
                            //}

                            // Save data to TMoney 
                            TMoney money = BackupCardTopup.UpdateData(
                                new TMoney
                                {
                                    cType = borrowBackupCard.userType,
                                    sID = string.IsNullOrEmpty(borrowBackupCard.userID) ? 0 : Convert.ToInt32(borrowBackupCard.userID),
                                    nMoney = borrowBackupCard.balance,
                                    CardHistoryID = backupCardHistory.CardHistoryID,
                                    topup_type = "WBS1",
                                    sEmp = userData.UserID,
                                    SchoolID = schoolID,
                                    CreatedBy = userData.UserID,
                                    CreatedDate = DateTime.Now
                                }, backupCardHistory, userData.Entities);

                            if (money == null)
                            {
                                // fail
                                success = false;
                                statusCode = 401;
                                message = "ไม่สามารถยืมบัตรสำรองใบนี้ได้เนื่องจาก Topup Function Fail";

                                
                            }
                            else
                            {
                                TempCard tempCard = new TempCard();
                                tempCard.AddOrModifyTempCard(new TempCard
                                {
                                    BarCode = backupCard.BarCode,
                                    CardID = backupCard.CardID,
                                    CardName = backupCard.CardName,
                                    Money = money.nMoney ?? 0,
                                    NFC = backupCard.NFC,
                                    SchoolID = backupCard.SchoolID,
                                    UserID = backupCardHistory.UserID ?? 0,
                                    UserType = backupCardHistory.UserType ?? 2,
                                    cDel = backupCard.cDel,
                                    ReturnDate = backupCardHistory.ReturnDate,
                                    NFCEncrypt = backupCard.NFCEncrypt
                                }, userData.AuthKey, userData.AuthValue);

                                // success
                              
                            }


                            keyword = backupCard.BarCode != null ? backupCard.BarCode.ToUpper() : "";

                            if (keyword == "")
                                keyword = backupCard.NFC != null ? backupCard.NFC.ToUpper() : "";
                            else
                                keyword += ' ' + backupCard.NFC != null ? backupCard.NFC.ToUpper() : "";

                            if (keyword == "")
                                keyword = backupCard.NFCEncrypt != null ? backupCard.NFCEncrypt.ToUpper() : "";
                            else
                                keyword += ' ' + backupCard.NFCEncrypt != null ? backupCard.NFCEncrypt.ToUpper() : "";

                            if (keyword == "")
                                keyword = backupCard.NFCReverse != null ? backupCard.NFCReverse.ToUpper() : "";
                            else
                                keyword += ' ' + backupCard.NFCReverse != null ? backupCard.NFCReverse.ToUpper() : "";

                            if (keyword == "")
                                keyword = backupCard.NFCEncryptReverse != null ? backupCard.NFCEncryptReverse.ToUpper() : "";
                            else
                                keyword += ' ' + backupCard.NFCEncryptReverse != null ? backupCard.NFCEncryptReverse.ToUpper() : "";


                        }
                        else
                        {
                            success = false;
                            statusCode = 401;
                            message = "ไม่สามารถทำการยืมบัตรสำรองใบนี้เนื่องจาก บัตรสำรองนี่ถูกยืมไปแล้ว";
                        }
                    }
                    catch (Exception error)
                    {
                        success = false;
                        statusCode = 500;
                        message = error.Message;

                        
                    }
                

                JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(schoolID, backupCardHistory.CardID.ToString(), "2", keyword);
            }

            var result = new { success, statusCode, message };

            return JsonConvert.SerializeObject(result);
        }

        #region Api Memory
        [WebMethod]
        public static object SaveBorrowBackupCard_v2(BorrowBackupCard borrowBackupCard)
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";
            string keyword = "";

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            Guid CardID = Guid.Empty;
            if (string.IsNullOrEmpty(borrowBackupCard.userID)) borrowBackupCard.userID = "0";

            try
            {
                CardID = new Guid(borrowBackupCard.cid);
            }
            catch
            {

            }
            string JSon = "{" +
                $"\"SchoolID\" : {userData.CompanyID}," +
                $"\"CardID\" : \"{borrowBackupCard.cid}\", " +
                $"\"UserType\" : \"{borrowBackupCard.userType}\"," +
                $"\"UserID\" : {borrowBackupCard.userID}," +
                $"\"UserName\" : \"{borrowBackupCard.userName}\"," +
                $"\"BorrowingDate\" : \"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff")}\" ," +
                $"\"nMoney\" : {borrowBackupCard.balance}," +
                $"\"sEmp\" : {userData.UserID}" + "}";

            Request_API request = new Request_API(API_Type.Payment_API);
            var d = request.POST(JSon, $"/Api/shop/pos/borrowcard", "Borrow Card", userData.CompanyID, userData.AuthKey, userData.AuthValue);

            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                if (CardID != Guid.Empty)
                {
                    TBackupCard backupCard = dbSchool.TBackupCards.Where(w => w.CardID == CardID && w.cDel == false).FirstOrDefault();

                    if (backupCard != null)
                    {
                        keyword = backupCard.BarCode != null ? backupCard.BarCode.ToUpper() : "";

                        if (keyword == "")
                            keyword = backupCard.NFC != null ? backupCard.NFC.ToUpper() : "";
                        else
                            keyword += ' ' + backupCard.NFC != null ? backupCard.NFC.ToUpper() : "";


                        if (keyword == "")
                            keyword = backupCard.NFCEncrypt != null ? backupCard.NFCEncrypt.ToUpper() : "";
                        else
                            keyword += ' ' + backupCard.NFCEncrypt != null ? backupCard.NFCEncrypt.ToUpper() : "";

                        if (keyword == "")
                            keyword = backupCard.NFCReverse != null ? backupCard.NFCReverse.ToUpper() : "";
                        else
                            keyword += ' ' + backupCard.NFCReverse != null ? backupCard.NFCReverse.ToUpper() : "";

                        if (keyword == "")
                            keyword = backupCard.NFCEncryptReverse != null ? backupCard.NFCEncryptReverse.ToUpper() : "";
                        else
                            keyword += ' ' + backupCard.NFCEncryptReverse != null ? backupCard.NFCEncryptReverse.ToUpper() : "";
                    }
                }

            }

            JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(schoolID, borrowBackupCard.cid, "2", keyword);
            var d1 = JsonConvert.DeserializeObject<TM_Result>(d.resultDes);

            success = d1.success;
            message = d1.message;
            statusCode = d1.statusCode;

            var result = new { success, statusCode, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveReturnBackupCard_v2(ReturnBackupCard returnBackupCard)
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                
                    try
                    {
                        Guid guidChId = Guid.Parse(returnBackupCard.chid);
                        TBackupCardHistory backupCardHistory = dbSchool.TBackupCardHistories.Where(w => w.SchoolID == schoolID && w.cDel == false && w.ReturnDate == null && w.CardHistoryID == guidChId).FirstOrDefault();

                        Request_API request = new Request_API(API_Type.Payment_API);
                        var d = request.POST(null, $"/Api/shop/pos/returncard?CardHistoryID={returnBackupCard.chid}&sEmp={userData.UserID}", "Return Card", userData.CompanyID, userData.AuthKey, userData.AuthValue);

                        var d1 = JsonConvert.DeserializeObject<TM_Result>(d.resultDes);

                        success = d1.success;
                        message = d1.message;
                        statusCode = d1.statusCode;

                        // JabjaiMainClass.Autocompletes.TopupMoney.Remove(backupCardHistory.CardID.ToString());
                        // JabjaiMainClass.Autocompletes.TopupMoney.topupMoney.Where(x => x.User_Id == backupCardHistory.CardID.ToString()).ToList().ForEach(x => x.Active = false);
                        try
                        {

                            string userid = backupCardHistory.CardID.ToString();
                            JabjaiMainClass.Autocompletes.TopupMoney.topupMoney.RemoveAll(x => x.User_Id.ToUpper() == userid.ToUpper());
                        }
                        catch
                        {

                        }

                    }
                    catch (Exception error)
                    {
                        success = false;
                        statusCode = 500;
                        message = error.Message;

                       
                    }
                
            }

            var result = new { success, statusCode, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveAddBackupCard_v2(NewBackupCard newBackupCard) //new card add
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;

                var encrypt = fcommon.FormatStudentCardNumber(newBackupCard.nfc);
                var encryptReverse = fcommon.NFCEncryptRevese(encrypt);
                var NFCReverse = fcommon.DeFormatStudentCardNumber(encryptReverse);

                string JSon = "{" +
                              $"\"SchoolID\" : {userData.CompanyID}," +
                              $"\"CardName\" : \"{newBackupCard.cardName}\", " +
                              $"\"BarCode\" : \"{newBackupCard.barCode}\"," +
                              $"\"NFC\" : \"{newBackupCard.nfc}\"," +
                              $"\"NFCEncrypt\" : \"{encrypt}\"," +
                              $"\"NFCEncryptReverse\" : \"{encryptReverse}\"," +
                              $"\"NFCReverse\" : \"{NFCReverse}\"," +
                              $"\"Money\" : 0 ," +
                              $"\"cDel\" : 0 ," +
                              $"\"CardID\" : \"00000000-0000-0000-0000-000000000000\" ," +
                              $"\"Insurance\" : {newBackupCard.insurance} ," +
                              $"\"CreatedBy\" : {userData.UserID}," +
                              $"\"CreatedDate\" : \"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff")}\" ," + "}";

                Request_API request = new Request_API(API_Type.Payment_API);
                var d = request.POST(JSon, $"/Api/shop/pos/addormodifytempcard", "AddOrModify Temp Card", userData.CompanyID, userData.AuthKey, userData.AuthValue);

                var d1 = JsonConvert.DeserializeObject<TM_Result>(d.resultDes);

                success = d1.success;
                message = d1.message;
                statusCode = d1.statusCode;
            }
            catch (Exception error)
            {
                success = false;
                statusCode = 500;
                message = error.Message;
            }

            var result = new { success, statusCode, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveEditBackupCard_v2(NewBackupCard newBackupCard)
        {
            bool success = true;
            int statusCode = 200;
            string message = "Send Successfully";
            var userData = GetUserData();
            int schoolID = userData.CompanyID;

            try
            {
                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    Guid guidChId = Guid.Parse(newBackupCard.cid);
                    var c = dbSchool.TBackupCards.FirstOrDefault(w => w.SchoolID == schoolID && w.cDel == false && w.CardID == guidChId);

                    string JSon = "{" +
                               $"\"SchoolID\" : {userData.CompanyID}," +
                               $"\"CardName\" : \"{newBackupCard.cardName}\", " +
                               $"\"BarCode\" : \"{c.BarCode}\"," +
                               $"\"NFC\" : \"{c.NFC}\"," +
                               $"\"NFCEncrypt\" : \"{c.NFCEncrypt}\"," +
                               $"\"NFCEncryptReverse\" : \"{c.NFCEncryptReverse}\"," +
                               $"\"NFCReverse\" : \"{c.NFCReverse}\"," +
                               $"\"Money\" : {c.Money ?? 0} ," +
                               $"\"cDel\" : 0 ," +
                               $"\"CardID\" : \"{c.CardID}\" ," +
                               $"\"Insurance\" : {newBackupCard.insurance} ," +
                               $"\"CreatedBy\" : {userData.UserID}," +
                               $"\"CreatedDate\" : \"{(c.CreatedDate ?? DateTime.Now).ToString("MM/dd/yyyy HH:mm:ss.fffff")}\" ," +
                               $"\"UpdatedBy\" : {userData.UserID}," +
                               $"\"UpdatedDate\" : \"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff")}\" " + "}";

                    Request_API request = new Request_API(API_Type.Payment_API);

                    var d = request.POST(JSon, $"/Api/shop/pos/addormodifytempcard", "AddOrModify Temp Card", userData.CompanyID, userData.AuthKey, userData.AuthValue);

                    var d1 = JsonConvert.DeserializeObject<TM_Result>(d.resultDes);

                    success = d1.success;
                    message = d1.message;
                    statusCode = d1.statusCode;
                }
            }
            catch (Exception error)
            {
                success = false;
                statusCode = 500;
                message = error.Message;
            }

            var result = new { success, statusCode, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static string RemoveItem_v2(string cid)
        {
            

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using(JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                bool success = true;
                int statusCode = 200;
                string message = "Send Successfully";

                try
                {
                    using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        Guid guidChId = Guid.Parse(cid);
                        var c = dbSchool.TBackupCards.FirstOrDefault(w => w.SchoolID == schoolID && w.cDel == false && w.CardID == guidChId);

                        string JSon = "{" +
                                   $"\"SchoolID\" : {userData.CompanyID}," +
                                   $"\"CardName\" : \"{c.CardName}\", " +
                                   $"\"BarCode\" : \"{c.BarCode}\"," +
                                   $"\"NFC\" : \"{c.NFC}\"," +
                                   $"\"NFCEncrypt\" : \"{c.NFCEncrypt}\"," +
                                   $"\"Money\" : {c.Money ?? 0} ," +
                                   $"\"cDel\" : 1 ," +
                                   $"\"CardID\" : \"{c.CardID}\" ," +
                                   $"\"Insurance\" : {c.Insurance} ," +
                                   $"\"CreatedBy\" : {userData.UserID}," +
                                   $"\"CreatedDate\" : \"{(c.CreatedDate ?? DateTime.Now).ToString("MM/dd/yyyy HH:mm:ss.fffff")}\" ," +
                                   $"\"UpdatedBy\" : {userData.UserID}," +
                                   $"\"UpdatedDate\" : \"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff")}\" " + "}";

                        Request_API request = new Request_API(API_Type.Payment_API);

                        var d = request.POST(JSon, $"/Api/shop/pos/addormodifytempcard", "AddOrModify Temp Card", userData.CompanyID, userData.AuthKey, userData.AuthValue);

                        var d1 = JsonConvert.DeserializeObject<TM_Result>(d.resultDes);

                        success = d1.success;
                        message = d1.message;
                        statusCode = d1.statusCode;
                        //JabjaiMainClass.Autocompletes.TopupMoney.Remove(c.CardID.ToString());

                        try
                        {


                            string userid = c.CardID.ToString();
                            JabjaiMainClass.Autocompletes.TopupMoney.topupMoney.RemoveAll(x => x.User_Id.ToUpper() == userid.ToUpper());
                        }
                        catch
                        {

                        }
                    }
                }
                catch (Exception error)
                {
                    success = false;
                    statusCode = 500;
                    message = error.Message;
                }

                var result = new { success, statusCode, message };

                return JsonConvert.SerializeObject(result);
            }
        }
        #endregion

        public class BorrowBackupCard
        {
            public string cid { get; set; }
            public string userType { get; set; }
            public string userID { get; set; }
            public string userName { get; set; }
            public decimal? balance { get; set; }
        }

        public class TM_Result
        {
            public bool success { get; set; }
            public int statusCode { get; set; } = 200;
            public string message { get; set; }
        }
    }
}