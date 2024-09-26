using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterOnline03 : RegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisterOnlineEntities"] != null)
            {
                int schoolID = Convert.ToInt32(Session["RegisterOnlineSchoolID"]);
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    string sqlQuery = string.Format(@"SELECT DISTINCT CAST([Year]-543 AS VARCHAR(4)) 'id', CAST([Year] AS VARCHAR(4)) 'name' FROM TRegisterSetup WHERE SchoolID = {0} AND cDel = 0 ORDER BY CAST([Year] AS VARCHAR(4)) DESC", schoolID);
                    List<EntityDropdown> resultYear = en.Database.SqlQuery<EntityDropdown>(sqlQuery).ToList();

                    if (resultYear.Count > 0)
                    {
                        foreach (var r in resultYear)
                        {
                            this.ltrYear.Text += string.Format(@"<option value=""{0}"" data-year-be=""{1}"">{1}</option>", r.id, r.name);
                        }
                    }

                    sqlQuery = string.Format(@"
SELECT DISTINCT r.StudentType 'ID', (CASE r.StudentType WHEN 1 THEN N'นักเรียนใหม่' WHEN 2 THEN N'นักเรียนรักษาสิทธิ์' ELSE '' END) 'StudentType'
FROM TRegisterSetup r
WHERE r.SchoolID = {0} AND r.cDel = 0 AND (GETDATE() < DATEADD(ms, -3, CONVERT(VARCHAR(10), DATEADD(d, 1, r.EndDate), 111)) OR r.EndDate IS NULL)", schoolID);
                    List<EntityStudentTypeSetup> resultType = en.Database.SqlQuery<EntityStudentTypeSetup>(sqlQuery).ToList();

                    if (resultType.Count > 0)
                    {
                        foreach (var r in resultType)
                        {
                            this.ltrStudentType.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.ID, r.StudentType);
                        }
                    }

                    // Branch 1
                    sqlQuery = string.Format(@"
SELECT CAST(BranchSpecId AS VARCHAR(10)) 'id', BranchSpecName 'name' 
FROM TBranchSpec bs 
LEFT JOIN TBranchSubject bs2 ON bs.BranchSubjectId = bs2.BranchSubjectId
LEFT JOIN TBranch b ON bs2.BranchId = b.BranchId
WHERE bs.SchoolID = {0} AND bs.cDel IS NULL AND b.nTLevel IN (SELECT LevelID FROM TLevel WHERE SchoolID = {0} AND LevelName = 'ปวช.')", schoolID);
                    List<EntityDropdown> resultBranch1 = en.Database.SqlQuery<EntityDropdown>(sqlQuery).ToList();
                    foreach (var r in resultBranch1)
                    {
                        this.ltrOptionBranch1.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.id, r.name);
                    }

                    // Branch 2
                    sqlQuery = string.Format(@"
SELECT CAST(BranchSpecId AS VARCHAR(10)) 'id', BranchSpecName 'name' 
FROM TBranchSpec bs 
LEFT JOIN TBranchSubject bs2 ON bs.BranchSubjectId = bs2.BranchSubjectId
LEFT JOIN TBranch b ON bs2.BranchId = b.BranchId
WHERE bs.SchoolID = {0} AND bs.cDel IS NULL AND b.nTLevel IN (SELECT LevelID FROM TLevel WHERE SchoolID = {0} AND LevelName = 'ปวส.')", schoolID);
                    List<EntityDropdown> resultBranch2 = en.Database.SqlQuery<EntityDropdown>(sqlQuery).ToList();
                    foreach (var r in resultBranch2)
                    {
                        this.ltrOptionBranch2.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.id, r.name);
                    }

                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityStudentTypeSetup> LoadStudentType(int year, int yearBE)
        {
            List<EntityStudentTypeSetup> result = null;

            if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
            {
                int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    string sqlQuery = string.Format(@"
SELECT DISTINCT r.StudentType 'ID', (CASE r.StudentType WHEN 1 THEN N'นักเรียนใหม่' WHEN 2 THEN N'นักเรียนรักษาสิทธิ์' ELSE '' END) 'StudentType' 
FROM TRegisterSetup r LEFT JOIN TSubLevel s ON r.nTSubLevel = s.nTSubLevel
WHERE r.SchoolID = {0} AND r.cDel = 0 AND (GETDATE() < DATEADD(ms, -3, CONVERT(VARCHAR(10), DATEADD(d, 1, r.EndDate), 111)) OR r.EndDate IS NULL) AND r.Year = {1}", schoolID, yearBE);
                    result = en.Database.SqlQuery<EntityStudentTypeSetup>(sqlQuery).ToList();

                    HttpContext.Current.Session["RegisterOnlineYear"] = year;
                    HttpContext.Current.Session["RegisterOnlineYearBE"] = yearBE;
                }
            }

            return result;
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityPlanSetupClass> LoadClass(int yearBE, int studentType)
        {
            List<EntityPlanSetupClass> result = null;

            if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
            {
                int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    string sqlQuery = string.Format(@"
SELECT DISTINCT r.nTSubLevel, s.SubLevel 
FROM TRegisterSetup r LEFT JOIN TSubLevel s ON r.nTSubLevel = s.nTSubLevel
WHERE r.SchoolID = {0} AND r.cDel = 0 AND (GETDATE() < DATEADD(ms, -3, CONVERT(VARCHAR(10), DATEADD(d, 1, r.EndDate), 111)) OR r.EndDate IS NULL) AND r.Year = {1} AND r.StudentType = '{2}'", schoolID, yearBE, studentType);
                    result = en.Database.SqlQuery<EntityPlanSetupClass>(sqlQuery).ToList();
                }
            }

            return result;
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityPlanSetupEduProgram> LoadPlan(int yearBE, int studentType, int classLevel)
        {
            List<EntityPlanSetupEduProgram> result = null;

            if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
            {
                int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    string sqlQuery = string.Format(@"
SELECT DISTINCT r.RegisterPlanSetupID 'ID', IIF(r.RegisterPlanSetupID=0, 'ทั้งหมด', p.PlanName) 'PlanName', r.StudentMax 'RegisterMax', ISNULL(r2.RegisterAmount, 0) 'RegisterAmount'
FROM TRegisterSetup r 
LEFT JOIN TRegisterPlanSetup p ON r.RegisterPlanSetupID = p.RegisterPlanSetupID AND r.nTSubLevel = p.nTSubLevel
LEFT JOIN 
(
	SELECT registerYear+543 'registerYear', StudentType, optionLevel, RegisterPlanSetupID, COUNT(*) 'RegisterAmount' 
	FROM TPreRegister 
	WHERE SchoolID={0} AND cDel IS NULL 
	GROUP BY registerYear, StudentType, optionLevel, RegisterPlanSetupID
) r2 ON r.Year = r2.registerYear AND r.StudentType = r2.StudentType AND r.nTSubLevel = r2.optionLevel AND r.RegisterPlanSetupID = r2.RegisterPlanSetupID
WHERE r.SchoolID = {0} AND r.cDel = 0 AND (GETDATE() < DATEADD(ms, -3, CONVERT(VARCHAR(10), DATEADD(d, 1, r.EndDate), 111)) OR r.EndDate IS NULL) AND r.Year = {1} AND r.StudentType = '{2}' AND r.nTSubLevel = {3}", schoolID, yearBE, studentType, classLevel);
                    result = en.Database.SqlQuery<EntityPlanSetupEduProgram>(sqlQuery).ToList();
                }
            }

            return result;
        }

        [WebMethod(EnableSession = true)]
        public static object LoadBackupPlans(int yearBE, int studentType, int classLevel)
        {
            bool success = true;
            string message = "Success";

            List<BackupPlans> backupPlans = null;

            try
            {
                if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
                {
                    int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
                    using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        string query = string.Format(@"
SELECT BackupPlans
FROM TRegisterSetup
WHERE SchoolID = {0} AND cDel = 0 AND (GETDATE() < DATEADD(ms, -3, CONVERT(VARCHAR(10), DATEADD(d, 1, EndDate), 111)) OR EndDate IS NULL) AND Year = {1} AND StudentType = '{2}' AND nTSubLevel = {3} AND RegisterPlanSetupID = 0", schoolID, yearBE, studentType, classLevel);
                        string jsonBackupPlans = ctx.Database.SqlQuery<string>(query).FirstOrDefault();
                        backupPlans = JsonConvert.DeserializeObject<List<BackupPlans>>(jsonBackupPlans);
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            return JsonConvert.SerializeObject(new { success, message, data = backupPlans });
        }

        [WebMethod(EnableSession = true)]
        public static string CheckRegisteredFullAmount(int yearBE, int studentType, int classLevel, int plan)
        {
            bool success = true;
            string message = "Check Successfully";
            bool registeredFullAmount = true;
            int fullAmount = 0;

            if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
            {
                int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    string sqlQuery = string.Format(@"
SELECT DISTINCT r.RegisterPlanSetupID 'ID', IIF(r.RegisterPlanSetupID=0, 'ทั้งหมด', p.PlanName) 'PlanName', r.StudentMax 'RegisterMax', ISNULL(r2.RegisterAmount, 0) 'RegisterAmount'
FROM TRegisterSetup r 
LEFT JOIN TRegisterPlanSetup p ON r.RegisterPlanSetupID = p.RegisterPlanSetupID AND r.nTSubLevel = p.nTSubLevel
LEFT JOIN 
(
	SELECT registerYear+543 'registerYear', StudentType, optionLevel, RegisterPlanSetupID, COUNT(*) 'RegisterAmount' 
	FROM TPreRegister 
	WHERE SchoolID={0} AND cDel IS NULL 
	GROUP BY registerYear, StudentType, optionLevel, RegisterPlanSetupID
) r2 ON r.Year = r2.registerYear AND r.StudentType = r2.StudentType AND r.nTSubLevel = r2.optionLevel AND r.RegisterPlanSetupID = r2.RegisterPlanSetupID
WHERE r.SchoolID = {0} AND r.cDel = 0 AND (GETDATE() < DATEADD(ms, -3, CONVERT(VARCHAR(10), DATEADD(d, 1, r.EndDate), 111)) OR r.EndDate IS NULL) AND r.Year = {1} AND r.StudentType = '{2}' AND r.nTSubLevel = {3} AND r.RegisterPlanSetupID = {4}", schoolID, yearBE, studentType, classLevel, plan);
                    EntityPlanSetupEduProgram planStatistics = en.Database.SqlQuery<EntityPlanSetupEduProgram>(sqlQuery).FirstOrDefault();
                    if (planStatistics != null)
                    {
                        registeredFullAmount = planStatistics.RegisterMax <= planStatistics.RegisterAmount;
                        fullAmount = planStatistics.RegisterMax;
                    }
                    else
                    {
                        success = false;
                    }
                }
            }
            else
            {
                success = false;
            }

            var result = new { success, message, registeredFullAmount, fullAmount };

            return JsonConvert.SerializeObject(result);
        }

        public class BackupPlans
        {
            [JsonProperty(PropertyName = "no")]
            public int No { get; set; }

            [JsonProperty(PropertyName = "planId")]
            public int? PlanID { get; set; }

            [JsonProperty(PropertyName = "planName")]
            public string PlanName { get; set; }
        }

    }
}