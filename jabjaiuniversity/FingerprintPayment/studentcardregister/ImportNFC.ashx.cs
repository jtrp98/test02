using FingerprintPayment.Memory;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;
using TUser = JabjaiEntity.DB.TUser;

namespace FingerprintPayment.VisitHousePage.Web
{
    /// <summary>
    /// Summary description for SaveData
    /// </summary>
    public class ImportNFC : IHttpHandler, IRequiresSessionState
    {

        private class ExcelModel
        {
            public string Code { get; set; }
            public string NFC { get; set; }
            public string Note { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            context.Response.ContentType = "text/plain";

            var uploadedFile = context.Request.Files; //only uploading one file
            var type = context.Request.Form["type"];

            int id;
            string status = "";
            var error = new List<string>();
            var notFound = new List<string>();
            var duplicate = new List<string>();
            var schoolid = userData.CompanyID;

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var card = new List<TUser_Card>();
                var now = DateTime.Now;
                now = now.AddMilliseconds(-now.Millisecond).AddMilliseconds(666);

                if (type == "student")
                {
                    //var temp = new List<ModelTemp2>();
                    var user = ctx.TUser.Where(o => o.SchoolID == schoolid && o.cDel != "1").ToList();

                    var lstToAdd = new List<TUser_Card>();

                    card = dbmaster.TUser_Card
                        .Where(o => o.SchoolID == schoolid && o.IsDel == false && o.IsActive == true)
                        .ToList();

                    //string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "import", $"student{schoolid}.xlsx");
                    var f = uploadedFile.Get(0);
                    using (var package = new ExcelPackage(f.InputStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.ElementAt(0);

                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            string Code = worksheet.Cells[row, 1].Value?.ToString();
                            string NFC = worksheet.Cells[row, 2].Value?.ToString();
                            string FreeText = worksheet.Cells[row, 3].Value?.ToString();

                            if (string.IsNullOrWhiteSpace(Code) || string.IsNullOrWhiteSpace(NFC))
                                continue;

                            try
                            {
                                var _user = user.Where(o => o.sStudentID == Code + "").FirstOrDefault();

                                var isDup1 = card.Where(o => o.sID != _user.sID && (o.NFC == NFC
                                      || o.NFCEncrypt == NFC
                                      || o.NFCEncryptReverse == NFC
                                      || o.NFCReverse == NFC)).Count() > 0;

                                var isDup2 = lstToAdd.Where(o => (o.NFC == NFC)).Count() > 0;

                                if (isDup1 || isDup2)
                                {
                                    duplicate.Add(NFC);
                                    continue;
                                }

                                if (_user != null)
                                {
                                    var _card = card.Where(o => o.sID == _user.sID).FirstOrDefault();
                                    var _nfc = "";
                                    var _nfcEn = "";

                                    if (IsHexNumber(NFC))
                                    {
                                        _nfcEn = NFC;
                                        _nfc = fcommon.DeFormatStudentCardNumber(NFC + "");
                                    }
                                    else
                                    {
                                        _nfc = NFC + "";
                                        _nfcEn = fcommon.FormatStudentCardNumber(NFC + "");
                                    }

                                    if (_card == null)
                                    {
                                        _card = new TUser_Card()
                                        {
                                            sID = _user.sID,
                                            SchoolID = schoolid,
                                            No = 1,
                                            NFC = _nfc,
                                            NFCEncrypt = _nfcEn,
                                            //NFC = fcommon.DeFormatStudentCardNumber(NFC + ""),
                                            //NFCEncrypt = NFC + "",
                                            CreateBy = userData.UserID,
                                            ModifyBy = userData.UserID,
                                            Created = now,
                                            Modified = now,
                                            IsDel = false,
                                            IsActive = true,
                                            FreeText = FreeText,
                                        };

                                        _card.NFCEncryptReverse = fcommon.NFCEncryptRevese(_card.NFCEncrypt);
                                        _card.NFCReverse = fcommon.DeFormatStudentCardNumber(_card.NFCEncryptReverse);

                                        lstToAdd.Add(_card);
                                        //dbmaster.TUser_Card.Add(c);
                                    }
                                    else
                                    {
                                        //_card.NFC = _nfc + "";
                                        //_card.NFCEncrypt = _nfcEn;
                                        //_card.NFCEncryptReverse = fcommon.NFCEncryptRevese(_card.NFCEncrypt);
                                        //_card.NFCReverse = fcommon.DeFormatStudentCardNumber(_card.NFCEncryptReverse);
                                        //_card.Modified = now;
                                        //_card.FreeText = FreeText;
                                    }
                                }
                                else
                                {
                                    notFound.Add(Code);
                                }

                            }
                            catch
                            {
                                error.Add(Code + "");
                            }
                        }

                        //var grp = lstToAdd.GroupBy(o => o.NFC).Where(o => o.Count() > 1).Select(o => o.Key).ToList();
                        //var toRem = lstToAdd.Where(o => grp.Contains(o.NFC)).ToList();
                        //lstToAdd.Remove()
                        dbmaster.TUser_Card.AddRange(lstToAdd);
                        dbmaster.SaveChanges();

                        database.InsertLog(userData.UserID + "", $"นำเข้าเลขบัตรนักเรียน", HttpContext.Current.Request, 14, 0, 0, userData.CompanyID);

                        var userCardList = dbmaster.TUser_Card.Where(o => o.SchoolID == userData.CompanyID).ToList();
                        HostingEnvironment.QueueBackgroundWorkItem(ct =>
                        {
                            foreach (var sid in lstToAdd.GroupBy(o => o.sID))
                            {
                                var studentId = sid.Key;
                                var q_user = user.Where(o => o.sID == studentId).FirstOrDefault();
                                TopupTempCard topupTempCard = new TopupTempCard();
                                List<TUser_Card> user_Cards = userCardList.Where(w => w.sID == studentId).ToList();
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


                                    JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(userData.CompanyID, studentId + "", "0", keyword);
                                }
                            }
                        });
                    }
                }
                else if (type == "employee")
                {
                    var employee = new List<TEmployeeInfo>();
                    employee = ctx.TEmployeeInfoes.Where(o => o.SchoolID == schoolid && o.cDel == false).ToList();

                    var lstToAdd = new List<TUser_Card>();


                    card = dbmaster.TUser_Card
                     .Where(o => o.SchoolID == schoolid && o.IsDel == false && o.IsActive == true)
                     .ToList();

                    var f = uploadedFile.Get(0);
                    using (var package = new ExcelPackage(f.InputStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.ElementAt(0);

                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            string Code = worksheet.Cells[row, 1].Value?.ToString();
                            string NFC = worksheet.Cells[row, 2].Value?.ToString();
                            string FreeText = worksheet.Cells[row, 3].Value?.ToString();

                            if (string.IsNullOrWhiteSpace(Code) || string.IsNullOrWhiteSpace(NFC))
                                continue;

                            try
                            {
                                var _user = employee.Where(o => o.Code == Code.Trim() + "").FirstOrDefault();

                                var isDup = card.Where(o => o.sID != _user.sEmp && (o.NFC == NFC
                                    || o.NFCEncrypt == NFC
                                    || o.NFCEncryptReverse == NFC
                                    || o.NFCReverse == NFC)).Count() > 0;

                                var isDup2 = lstToAdd.Where(o => o.NFC == NFC).Count() > 0;

                                if (isDup || isDup2)
                                {
                                    duplicate.Add(NFC);
                                    continue;
                                }

                                if (_user != null)
                                {
                                    var _card = card.Where(o => o.sID == _user.sEmp).FirstOrDefault();

                                    var _nfc = "";
                                    var _nfcEn = "";

                                    if (IsHexNumber(NFC))
                                    {
                                        _nfcEn = NFC;
                                        _nfc = fcommon.DeFormatStudentCardNumber(NFC + "");
                                    }
                                    else
                                    {
                                        _nfc = NFC + "";
                                        _nfcEn = fcommon.FormatStudentCardNumber(NFC + "");
                                    }

                                    if (_card == null)
                                    {
                                        _card = new TUser_Card()
                                        {
                                            sID = _user.sEmp,
                                            SchoolID = schoolid,
                                            No = 1,
                                            NFC = _nfc,
                                            NFCEncrypt = _nfcEn,
                                            //NFC = fcommon.DeFormatStudentCardNumber(NFC + ""),
                                            //NFCEncrypt = NFC + "",
                                            CreateBy = userData.UserID,
                                            ModifyBy = userData.UserID,
                                            Created = now,
                                            Modified = now,
                                            IsDel = false,
                                            IsActive = true,
                                            FreeText = FreeText,
                                        };

                                        _card.NFCEncryptReverse = fcommon.NFCEncryptRevese(_card.NFCEncrypt);
                                        _card.NFCReverse = fcommon.DeFormatStudentCardNumber(_card.NFCEncryptReverse);

                                        lstToAdd.Add(_card);
                                    }
                                    else
                                    {
                                        //_card.NFC = _nfc + "";
                                        //_card.NFCEncrypt = _nfcEn;
                                        //_card.NFCEncryptReverse = fcommon.NFCEncryptRevese(_card.NFCEncrypt);
                                        //_card.NFCReverse = fcommon.DeFormatStudentCardNumber(_card.NFCEncryptReverse);
                                        //_card.Modified = now;
                                        //_card.FreeText = FreeText;
                                    }
                                }
                                else
                                {
                                    notFound.Add(Code);
                                }
                            }
                            catch
                            {
                                error.Add(Code);
                            }
                        }
                    }

                    dbmaster.TUser_Card.AddRange(lstToAdd);
                    dbmaster.SaveChanges();

                    database.InsertLog(userData.UserID + "", $"นำเข้าเลขบัตรบุคลากร", HttpContext.Current.Request, 13, 0, 0, userData.CompanyID);

                    var userCardList = dbmaster.TUser_Card.Where(o => o.SchoolID == userData.CompanyID).ToList();

                    HostingEnvironment.QueueBackgroundWorkItem(ct =>
                    {
                        foreach (var sid in lstToAdd.GroupBy(o => o.sID))
                        {
                            var studentId = sid.Key;
                            var q_user = employee.Where(o => o.sEmp == studentId).FirstOrDefault();
                            TopupTempCard topupTempCard = new TopupTempCard();
                            List<TUser_Card> user_Cards = userCardList.Where(w => w.sID == studentId).ToList();
                            topupTempCard.AddOrModifyUserCard(user_Cards, userData.CompanyID, userData.AuthKey, userData.AuthValue);

                            foreach (TUser_Card userCard in user_Cards)
                            {
                                string keyword = q_user.Code != null ? q_user.Code.ToUpper() : "";

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


                                JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(userData.CompanyID, studentId + "", "1", keyword);
                            }
                        }
                    });
                }
            }


            context.Response.Expires = -1;
            //context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            context.Response.Write(JsonConvert.SerializeObject(new
            {
                status = "ok",
                duplicate = string.Join(",", duplicate),
                notFound = string.Join(",", notFound),
                error = string.Join(",", error)
            }));
            context.Response.End();
        }

        bool IsHexNumber(string value)
        {
            // Try to parse the string as a hexadecimal number
            if (int.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out _))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}