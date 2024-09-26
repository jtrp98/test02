using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
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
    public partial class RegisterPlanData : PreRegisterGateway
    {
        private static string SessionPrimaryKey = "REGISTERPLAN_RID";
        private static string SessionPrimaryKey2 = "REGISTERPLAN_LID";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        #region Method

        private void InitialPage()
        {
            string v = Request.QueryString["v"];
            switch (v)
            {
                case "list":
                    MvContent.ActiveViewIndex = 0; break;
                case "form":
                    MvContent.ActiveViewIndex = 1;

                    int schoolID = UserData.CompanyID;
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        var result = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1 && w.cDel == false).Select(s => new { s.nTSubLevel, s.SubLevel }).ToList();
                        foreach (var r in result)
                        {
                            this.ltrLevel.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTSubLevel, r.SubLevel);
                        }
                    }

                    break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadRegisterPlan()
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

            string sortBy = "LevelID";
            switch (sortIndex)
            {
                case "1": sortBy = "LevelName"; break;
                case "2": sortBy = "PlanName"; break;
                case "3": sortBy = "PlanCode"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            int schoolID = GetUserData().CompanyID;
            var json = QueryEngine.LoadRegisterPlanJsonData(draw, pageIndex, pageSize, sortBy, schoolID);

            return json;
        }

        [WebMethod]
        public static string RemoveItem(int rid, int lid)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

            bool success = true;
            string message = "Save Successfully";

            try
            {
                TRegisterPlanSetup p = en.TRegisterPlanSetups.First(f => f.SchoolID == schoolID && f.RegisterPlanSetupID == rid && f.nTSubLevel == lid);

                if (p != null)
                {
                    // Check delete plan code, no delete if use plan code in generate seat no
                    bool existUsagePlanCode = false;
                    if (!string.IsNullOrEmpty(p.PlanCode))
                    {
                        int countUser = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.optionLevel == lid && w.RegisterPlanSetupID == rid && w.ExamSeatNo.StartsWith(p.PlanCode)).Count();
                        existUsagePlanCode = countUser > 0;
                    }

                    if (!existUsagePlanCode)
                    {
                        p.cDel = true;
                        p.UpdateBy = userData.UserID;
                        p.UpdateDate = DateTime.Now;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลตั้งค่าแผนหลักสูตร รหัส:" + p.RegPlanID + ", รหัสแผน:" + rid + ", รหัสชั้นเรียน:" + lid, HttpContext.Current.Request, 164, 4, 0, schoolID);
                    }
                    else
                    {
                        success = false;
                        message = "รหัสแผนถูกนำไปใช้ในการสร้างเลขที่นั่งสอบแล้ว";
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static string SaveItem(List<string> data)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;

            bool success = true;
            string message = "Save Successfully";

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    int LevelID = int.Parse(data[1]);
                    string PlanName = data[2];
                    string PlanCode = data[3];

                    int RPID = 0;
                    int PID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKey]);

                    // Check exist plan code
                    bool existPlanCode = en.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.PlanCode == PlanCode && w.nTSubLevel != LevelID && w.RegisterPlanSetupID != PID && w.cDel == false).Count() > 0;
                    if (existPlanCode)
                    {
                        throw new Exception("รหัสแผนนี่มีในระบบแล้ว");
                    }

                    bool existPlanName = en.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.PlanName == PlanName && w.nTSubLevel == LevelID && w.RegisterPlanSetupID != PID && w.cDel == false).Count() > 0;
                    if (existPlanName)
                    {
                        throw new Exception("ชื่อแผนในระดับชั้นนี่มีในระบบแล้ว");
                    }


                    if (HttpContext.Current.Session[SessionPrimaryKey] == null)
                    {
                        // Insert Section
                        PID = (int)(en.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.nTSubLevel == LevelID).Count() == 0 ? 1 : en.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.nTSubLevel == LevelID).Max(m => m.RegisterPlanSetupID) + 1);

                        // Get Item
                        TRegisterPlanSetup p = new TRegisterPlanSetup
                        {
                            RegisterPlanSetupID = PID,
                            nTSubLevel = LevelID,
                            PlanName = PlanName,
                            PlanCode = PlanCode,
                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now
                        };

                        en.TRegisterPlanSetups.Add(p);

                        en.SaveChanges();

                        RPID = p.RegPlanID;

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลตั้งค่าแผนหลักสูตร รหัส:" + RPID + ", รหัสแผน:" + PID + ", รหัสชั้นเรียน:" + LevelID, HttpContext.Current.Request, 164, 2, 0, schoolID);
                    }
                    else
                    {
                        // Modify Section
                        PID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKey]);
                        int LID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKey2]);

                        // Get Item
                        TRegisterPlanSetup pi = en.TRegisterPlanSetups.First(f => f.SchoolID == schoolID && f.RegisterPlanSetupID == PID && f.nTSubLevel == LID);

                        // Check modify plan code, no modify if use plan code in generate seat no
                        bool existUsagePlanCode = false;
                        if (!string.IsNullOrEmpty(pi.PlanCode))
                        {
                            int countUser = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.optionLevel == LID && w.RegisterPlanSetupID == PID && w.ExamSeatNo.StartsWith(pi.PlanCode) && w.cDel == null).Count();
                            existUsagePlanCode = countUser > 0;
                        }

                        if (!existUsagePlanCode)
                        {
                            pi.PlanName = PlanName;
                            pi.PlanCode = PlanCode;

                            pi.UpdateDate = DateTime.Now;
                            pi.UpdateBy = userData.UserID;

                            en.SaveChanges();

                            database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลตั้งค่าแผนหลักสูตร รหัส:" + RPID + ", รหัสแผน:" + PID + ", รหัสชั้นเรียน:" + LevelID, HttpContext.Current.Request, 164, 3, 0, schoolID);
                        }
                        else
                        {
                            success = false;
                            message = "รหัสแผนถูกนำไปใช้ในการสร้างเลขที่นั่งสอบแล้ว";
                        }
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static string GetItem(int rid, int lid)
        {
            int schoolID = GetUserData().CompanyID;

            string infor = "new";
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    //int iRID = 0;
                    //if (!int.TryParse(rid, out iRID)) { iRID = 0; }
                    //int iLID = 0;
                    //if (!int.TryParse(lid, out iLID)) { iLID = 0; }

                    TRegisterPlanSetup p = en.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.RegisterPlanSetupID == rid && w.nTSubLevel == lid).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 3; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        HttpContext.Current.Session[SessionPrimaryKey] = p.RegisterPlanSetupID;
                        HttpContext.Current.Session[SessionPrimaryKey2] = p.nTSubLevel;

                        dt.Rows[0]["F1"] = p.nTSubLevel;
                        dt.Rows[0]["F2"] = p.PlanName;
                        dt.Rows[0]["F3"] = p.PlanCode;

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
            HttpContext.Current.Session[SessionPrimaryKey2] = null;

            return "";
        }

        #endregion
    }
}