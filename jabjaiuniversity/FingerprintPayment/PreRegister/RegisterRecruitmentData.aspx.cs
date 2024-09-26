using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterRecruitmentData : PreRegisterGateway
    {
        private static string SessionPrimaryKey = "REGISTERRECRUITMENT_ID";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        #region Method

        private void InitialPage()
        {
            int schoolID = UserData.CompanyID;
            string v = Request.QueryString["v"];
            switch (v)
            {
                case "list":
                    MvContent.ActiveViewIndex = 0;

                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        var countPlan = en.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.cDel == false).Count();
                        if (countPlan > 0)
                        {
                            this.ltrButtonAdd.Text = @"<button type=""button"" class=""btn btn-success"" data-toggle=""modal"" data-target=""#modalShowForm"" form-name=""RegisterRecruitmentData.aspx"" form-action=""form"" form-title=""เพิ่มตั้งค่าการรับสมัคร"">ตั้งค่าการรับสมัคร</button>";
                        }
                        else
                        {
                            this.ltrButtonAdd.Text = @"<button type=""button"" class=""btn btn-default"" data-toggle=""modal"" data-target=""#modalWarning"" data-title=""Warning Dialog"" data-message=""กรุณาตั้งค่าแผน!"">ตั้งค่าการรับสมัคร</button>";
                        }
                    }

                    break;
                case "form":
                    MvContent.ActiveViewIndex = 1;

                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        var rYear = en.TYears.Where(w => w.SchoolID == schoolID && w.YearStatus != "0" && w.cDel == false).OrderByDescending(o => o.nYear).Select(s => new { s.nYear, s.numberYear }).ToList();
                        foreach (var r in rYear)
                        {
                            this.ltrYear.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nYear, r.numberYear);
                        }

                        var rLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1 && w.cDel == false).Select(s => new { s.nTSubLevel, s.SubLevel }).ToList();
                        foreach (var r in rLevel)
                        {
                            this.ltrLevel.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTSubLevel, r.SubLevel);
                        }

                        using (PeakengineEntities peakEngine = Connection.PeakengineEntities(ConnectionDB.Read))
                        {
                            //                            string query = string.Format(@"
                            //SELECT CAST(pg.PaymentGroupID AS VARCHAR(10)) 'id', pg.GroupName+' (ค่าเทอม '+CAST(FORMAT(SUM(pgl.Price), 'N') AS VARCHAR(10))+' บาท)' 'name'
                            //FROM PeakengineSingleDB.dbo.Product_Group pg 
                            //LEFT JOIN PeakengineSingleDB.dbo.Product_Group_List pgl ON pg.PaymentGroupID = pgl.PaymentGroupID
                            //WHERE pg.Del IS NULL AND pg.school_id = {0}
                            //GROUP BY pg.PaymentGroupID, pg.GroupName", schoolID);


                            string query = string.Format(@"
                            select CAST(a.AccountPaymentGroupId AS VARCHAR(10)) 'id', a.GroupName+ ' (ค่าเทอม '+CAST(FORMAT(SUM(b.Price), 'N') AS VARCHAR(10))+' บาท)' 'name'
                            from AccountingDB.dbo.AccountPaymentGroup a left join AccountingDB.dbo.AccountPaymentGroupDetail b
                            on a.AccountPaymentGroupId =b.AccountPaymentGroupId where isnull( a.DeleteDate,'' ) = '' and a.SchoolId={0}
                            group by a.AccountPaymentGroupId, a.GroupName", schoolID);

                            var rPaymentGroup = en.Database.SqlQuery<EntityDropdown>(query).ToList();
                            foreach (var r in rPaymentGroup)
                            {
                                this.ltrPaymentGroup.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.id, r.name);
                            }
                        }
                    }

                    break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityPlanSetupEduProgram> LoadPlan(int classLevel)
        {
            List<EntityPlanSetupEduProgram> result = null;

            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string sqlQuery = string.Format(@"
SELECT DISTINCT RegisterPlanSetupID 'ID', PlanName
FROM TRegisterPlanSetup  
WHERE SchoolID = {0} AND cDel = 0 AND nTSubLevel = {1}", schoolID, classLevel);
                result = en.Database.SqlQuery<EntityPlanSetupEduProgram>(sqlQuery).ToList();
            }

            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadRegisterRecruitment()
        {
            var jsonStream = "";
            HttpContext.Current.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonStream = inputStream.ReadToEnd();
            }
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int draw = Convert.ToInt32(jsonObject["draw"]);
            int pageIndex = Convert.ToInt32(jsonObject["page"]);
            int pageSize = Convert.ToInt32(jsonObject["length"]);
            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            string sortBy = "ID";
            switch (sortIndex)
            {
                case "1": sortBy = "Year"; break;
                case "2": sortBy = "StudentTypeName"; break;
                case "3": sortBy = "MasterCode"; break;
                case "4": sortBy = "PlanName"; break;
                case "5": sortBy = "EndDate"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            int schoolID = GetUserData().CompanyID;
            var json = QueryEngine.LoadRegisterRecruitmentJsonData(draw, pageIndex, pageSize, sortBy, schoolID);

            return json;
        }

        [WebMethod]
        public static string RemoveItem(int id)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;

            string isComplete = "complete";
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    TRegisterSetup p = en.TRegisterSetups.First(f => f.SchoolID == schoolID && f.RegisterSetupID == id);

                    if (p != null)
                    {
                        //en.TRegisterSetups.Remove(p);
                        p.cDel = true;
                        p.UpdateBy = userData.UserID;
                        p.UpdateDate = DateTime.Now;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลตั้งค่าการรับสมัคร รหัส:" + id, HttpContext.Current.Request, 165, 4, 0, schoolID);
                    }
                }
                catch
                {
                    isComplete = "error";
                }
            }

            return isComplete;
        }

        [WebMethod]
        public static string SaveItem(List<string> data)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;

            string isComplete = "complete";
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    int? YearID = string.IsNullOrEmpty(data[1]) ? (int?)null : int.Parse(data[1]);
                    int? Year = string.IsNullOrEmpty(data[2]) ? (int?)null : int.Parse(data[2]);
                    string StudentType = data[3];
                    int? LevelID = string.IsNullOrEmpty(data[4]) ? (int?)null : int.Parse(data[4]);
                    int? PlanID = string.IsNullOrEmpty(data[5]) ? (int?)null : int.Parse(data[5]);
                    int? StudentMax = string.IsNullOrEmpty(data[6]) ? (int?)null : int.Parse(data[6]);
                    DateTime? DocumentDate = string.IsNullOrEmpty(data[7]) ? (DateTime?)null : DateTime.Parse(data[7], new CultureInfo("th-TH"));
                    DateTime? EndDate = string.IsNullOrEmpty(data[8]) ? (DateTime?)null : DateTime.Parse(data[8], new CultureInfo("th-TH"));
                    int? Fee = string.IsNullOrEmpty(data[9]) ? (int?)null : int.Parse(data[9]);

                    if (HttpContext.Current.Session[SessionPrimaryKey] == null)
                    {
                        // Insert Section
                        //int ID = (int)(en.TRegisterSetups.Count() == 0 ? 1 : en.TRegisterSetups.Max(m => m.ID) + 1);

                        // Get Item
                        TRegisterSetup p = new TRegisterSetup
                        {
                            //ID = ID,
                            nYear = YearID,
                            Year = Year,
                            StudentType = StudentType,
                            nTSubLevel = LevelID,
                            RegisterPlanSetupID = PlanID,
                            StudentMax = StudentMax,
                            SubmitDocumentDate = DocumentDate,
                            EndDate = EndDate,
                            Fee = Fee,

                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now
                        };

                        en.TRegisterSetups.Add(p);

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลตั้งค่าการรับสมัคร รหัส:" + p.RegisterSetupID, HttpContext.Current.Request, 165, 2, 0, schoolID);
                    }
                    else
                    {
                        // Modify Section
                        int ID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKey]);

                        // Get Item
                        TRegisterSetup pi = en.TRegisterSetups.First(f => f.SchoolID == schoolID && f.RegisterSetupID == ID);

                        pi.nYear = YearID;
                        pi.Year = Year;
                        pi.StudentType = StudentType;
                        pi.nTSubLevel = LevelID;
                        pi.RegisterPlanSetupID = PlanID;
                        pi.StudentMax = StudentMax;
                        pi.SubmitDocumentDate = DocumentDate;
                        pi.EndDate = EndDate;
                        pi.Fee = Fee;

                        pi.UpdateDate = DateTime.Now;
                        pi.UpdateBy = userData.UserID;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลตั้งค่าการรับสมัคร รหัส:" + ID, HttpContext.Current.Request, 165, 3, 0, schoolID);
                    }
                }
                catch (Exception err)
                {
                    isComplete = "error-" + err.Message;
                }
            }

            return isComplete;
        }

        [WebMethod]
        public static string GetItem(int id)
        {
            int schoolID = GetUserData().CompanyID;

            string infor = "new";
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    TRegisterSetup p = en.TRegisterSetups.Where(w => w.SchoolID == schoolID && w.RegisterSetupID == id).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 21; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        HttpContext.Current.Session[SessionPrimaryKey] = p.RegisterSetupID;

                        dt.Rows[0]["F1"] = p.nYear;
                        dt.Rows[0]["F2"] = p.Year;
                        dt.Rows[0]["F3"] = p.StudentType;
                        dt.Rows[0]["F4"] = p.nTSubLevel;
                        dt.Rows[0]["F5"] = p.RegisterPlanSetupID;
                        dt.Rows[0]["F6"] = p.StudentMax;
                        dt.Rows[0]["F7"] = p.SubmitDocumentDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F8"] = p.EndDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F9"] = p.Fee;

                        dt.Rows[0]["F10"] = p.ExamAnnounce;
                        dt.Rows[0]["F11"] = p.MeetingDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F12"] = p.MeetingTime;
                        dt.Rows[0]["F13"] = p.MeetingPlace;

                        dt.Rows[0]["F14"] = p.AttachmentsPassExam;
                        string fileNamePassExam = string.IsNullOrEmpty(p.AttachmentsPassExam) ? "" : Path.GetFileName(p.AttachmentsPassExam);
                        dt.Rows[0]["F15"] = fileNamePassExam;

                        dt.Rows[0]["F16"] = p.AttachmentsFailExam;
                        string fileNameFailExam = string.IsNullOrEmpty(p.AttachmentsFailExam) ? "" : Path.GetFileName(p.AttachmentsFailExam);
                        dt.Rows[0]["F17"] = fileNameFailExam;
                        dt.Rows[0]["F18"] = p.PaymentGroupID;

                        dt.Rows[0]["F19"] = p.IsActiveBackupPlan;
                        dt.Rows[0]["F20"] = p.OrderPlans;
                        dt.Rows[0]["F21"] = p.BackupPlans;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }
            }

            if (infor == "new" || infor == "error") HttpContext.Current.Session[SessionPrimaryKey] = null;

            return infor;
        }

        [WebMethod]
        public static string ClearSessionID()
        {
            HttpContext.Current.Session[SessionPrimaryKey] = null;

            return "";
        }

        #endregion
    }
}