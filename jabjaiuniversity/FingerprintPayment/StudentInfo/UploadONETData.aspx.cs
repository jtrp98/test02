using FingerprintPayment.Class;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class UploadONETData : StudentGateway
    {
        public static decimal StudentImportPercentage = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                // Get current year
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTerm = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });
                int yearID = 0;
                var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
                if (term != null)
                {
                    yearID = term.nYear.Value;
                }

                var listYear = en.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false).OrderByDescending(x => x.numberYear).ToList();
                foreach (var l in listYear)
                {
                    if (l.nYear == yearID)
                    {
                        this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                    }
                    else
                    {
                        if (yearID == 0)
                        {
                            this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                        }
                        else
                        {
                            this.ltrYear.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.nYear, l.numberYear);
                        }
                    }

                    if (yearID == 0) yearID = l.nYear;
                }
            }
        }

        [WebMethod]
        public static string UpdateMonitor()
        {
            bool isComplete = false;
            decimal percentage = 0;

            percentage = StudentImportPercentage;
            if (percentage >= 100)
            {
                isComplete = true;
            }

            var result = new { isComplete, percentage };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string UploadExcelData(string byteData, string year, int level)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            int userID = userData.UserID;

            byte[] bytes = Convert.FromBase64String(byteData);
            string fileName = string.Format(@"{0}_{1}_{2}.xls", schoolID, userID, DateTime.Now.Ticks);
            string filePath = HttpContext.Current.Server.MapPath("~/Upload/Student/" + fileName);
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Student")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Student"));
            }

            // Save file in File folder.
            File.WriteAllBytes(filePath, bytes);

            bool success = true;
            string code = "200";
            string message = "Success to upload data.";

            FileInfo template = new FileInfo(filePath);
            using (var package = new ExcelPackage(template))
            {
                var workbook = package.Workbook;

                // Sheet 1
                var worksheet = workbook.Worksheets.First();

                // level : 1 = P.6, 2 = M.3, 3 = M.6
                // P.6 >> start row: 12, เลขที่นั่งสอบ(B), เลขประจำตัวประชาชน(C), 61(G), 64(H), 65(I), 63(J), ร้อยละ 30 ผลการทดสอบ O-NET(O)
                // M.3 >> start row: 11, เลขที่นั่งสอบ(B), เลขประจำตัวประชาชน(C), 91(E), 94(F), 95(G), 93(H), ร้อยละ 30 ผลการทดสอบ O-NET(M)
                // M.6 >> start row: 11, เลขที่นั่งสอบ(B), เลขประจำตัวประชาชน(C), 01(E), 04(F), 05(G), 02(H), 03(I), ร้อยละ 30 ผลการทดสอบ O-NET(O)

                int rowCount = worksheet.Dimension.End.Row;

                StudentImportPercentage = 0M;

                bool isContinue = true;
                int rowIndex = 0;
                string levelName = "";
                string levelNameEn = "";
                switch (level)
                {
                    case 1: rowIndex = 12; levelName = "ป.6"; levelNameEn = "P.6"; break;
                    case 2: rowIndex = 11; levelName = "ม.3"; levelNameEn = "M.3"; break;
                    case 3: rowIndex = 11; levelName = "ม.6"; levelNameEn = "M.6"; break;
                }

                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                   
                        

                        try
                        {
                            while (isContinue)
                            {
                                StudentImportPercentage = Convert.ToDecimal((rowIndex / (rowCount * 1M)) * 100);

                                int iYear = int.Parse(year) - 543;
                                string seatNo = worksheet.Cells["B" + rowIndex].Value.ToString();
                                string identification = worksheet.Cells["C" + rowIndex].Value.ToString();

                                int sID = 0;
                                var user = en.TUser.Where(w => w.SchoolID == schoolID && w.sIdentification == identification.Trim() && (w.cDel == null || w.cDel == "G")).FirstOrDefault();
                                if (user != null)
                                {
                                    sID = user.sID;
                                }

                                int nTSubLevel = 0;
                                var subLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.SubLevel == levelName && w.nWorkingStatus == 1).FirstOrDefault();
                                if (subLevel != null)
                                {
                                    nTSubLevel = subLevel.nTSubLevel;
                                }

                                bool isNewRow = en.TONETScores.Where(w => w.SchoolID == schoolID && w.Year == iYear && w.sID == sID && w.Identification == identification.Trim() && w.nTSubLevel == nTSubLevel).Count() == 0;
                                TONETScore oNetScore;
                                if (isNewRow)
                                {
                                    oNetScore = new TONETScore()
                                    {
                                        Year = iYear,
                                        sID = sID,
                                        Identification = identification,
                                        nTSubLevel = nTSubLevel,
                                        Class = levelNameEn,
                                        SeatNo = seatNo,
                                        UpdateDate = DateTime.Now,
                                        UpdateBy = userID,
                                        SchoolID = schoolID
                                    };
                                }
                                else
                                {
                                    oNetScore = en.TONETScores.First(w => w.SchoolID == schoolID && w.Year == iYear && w.sID == sID && w.Identification == identification.Trim() && w.nTSubLevel == nTSubLevel);

                                    oNetScore.UpdateDate = DateTime.Now;
                                    oNetScore.UpdateBy = userID;
                                }

                                switch (level)
                                {
                                    case 1:
                                        oNetScore.SubjectScore61 = decimal.Parse(worksheet.Cells["G" + rowIndex].Value.ToString());
                                        oNetScore.SubjectScore64 = decimal.Parse(worksheet.Cells["H" + rowIndex].Value.ToString());
                                        oNetScore.SubjectScore65 = decimal.Parse(worksheet.Cells["I" + rowIndex].Value.ToString());
                                        oNetScore.SubjectScore63 = decimal.Parse(worksheet.Cells["J" + rowIndex].Value.ToString());
                                        //oNetScore.Result30Percent = decimal.Parse(worksheet.Cells["O" + rowIndex].Value.ToString());
                                        oNetScore.ResultPercent = decimal.Parse(worksheet.Cells["O" + rowIndex].Value.ToString());
                                        oNetScore.Multiplier = 30;
                                        break;
                                    case 2:
                                        oNetScore.SubjectScore91 = decimal.Parse(worksheet.Cells["E" + rowIndex].Value.ToString());
                                        oNetScore.SubjectScore94 = decimal.Parse(worksheet.Cells["F" + rowIndex].Value.ToString());
                                        oNetScore.SubjectScore95 = decimal.Parse(worksheet.Cells["G" + rowIndex].Value.ToString());
                                        oNetScore.SubjectScore93 = decimal.Parse(worksheet.Cells["H" + rowIndex].Value.ToString());
                                        //oNetScore.Result30Percent = decimal.Parse(worksheet.Cells["M" + rowIndex].Value.ToString());
                                        oNetScore.ResultPercent = decimal.Parse(worksheet.Cells["M" + rowIndex].Value.ToString());
                                        oNetScore.Multiplier = 30;
                                        break;
                                    case 3:
                                        oNetScore.SubjectScore01 = decimal.Parse(worksheet.Cells["E" + rowIndex].Value.ToString());
                                        oNetScore.SubjectScore04 = decimal.Parse(worksheet.Cells["F" + rowIndex].Value.ToString());
                                        oNetScore.SubjectScore05 = decimal.Parse(worksheet.Cells["G" + rowIndex].Value.ToString());
                                        oNetScore.SubjectScore02 = decimal.Parse(worksheet.Cells["H" + rowIndex].Value.ToString());
                                        oNetScore.SubjectScore03 = decimal.Parse(worksheet.Cells["I" + rowIndex].Value.ToString());
                                        //oNetScore.Result30Percent = decimal.Parse(worksheet.Cells["O" + rowIndex].Value.ToString());
                                        oNetScore.ResultPercent = decimal.Parse(worksheet.Cells["O" + rowIndex].Value.ToString());
                                        oNetScore.Multiplier = 30;
                                        break;
                                }

                                if (isNewRow)
                                {
                                    en.TONETScores.Add(oNetScore);
                                }

                                // Save data to database
                                en.SaveChanges();

                                // Next row
                                rowIndex++;

                                // Check blank to exist loop
                                if (worksheet.Cells["B" + rowIndex].Value == null || string.IsNullOrEmpty(worksheet.Cells["B" + rowIndex].Value.ToString()))
                                {
                                    isContinue = false;
                                }

                            }

                            StudentImportPercentage = 100M;

                           

                        }
                        catch (Exception err)
                        {
                            success = false;
                            code = "205";
                            message = "error: " + err.Message + ", :line " + ComFunction.GetLineNumberError(err);

                          

                            string logMessagePattern = @"[SchoolEntities:{0}], [Filename:{1}], [ErrorLine:{2}], [ErrorMessage:{3}]";
                            string errorMessage = err.Message;
                            string innerExceptionMessage = "";
                            while (err.InnerException != null) { innerExceptionMessage += ", " + err.InnerException.Message; err = err.InnerException; }
                            string logMessageDebug = string.Format(logMessagePattern, userData.Entities, fileName, ComFunction.GetLineNumberError(err), errorMessage + ", " + innerExceptionMessage);

                            int? schID = schoolID;
                            int? empID = userID;

                            ComFunction.InsertLogDebug(schID, null, empID, logMessageDebug);
                        }

                        Thread.Sleep(1000);
                    
                }
            }

            var result = new { success, code, message };

            return JsonConvert.SerializeObject(result);
        }

    }
}