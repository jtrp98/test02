using FingerprintPayment.Employees.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Employees
{
    public partial class EmpAdvancement : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPADVANCEMENTID";

        protected string CanResign = "true";

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

                    var userData = GetUserData();
                    int schoolID = userData.CompanyID;

                    using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        int eid = Convert.ToInt32(Request.QueryString["eid"]);

                        int countNotificationSetting = mctx.TNotificationSettings.Where(w => w.SchoolId == schoolID && w.StaffID == eid).Count();
                        if (countNotificationSetting > 0)
                        {
                            CanResign = "false";
                        }
                    }

                    MvContent.ActiveViewIndex = 1; break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        [WebMethod]
        public static string RemoveItem(int eid, int id)
        {
            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    TEmpSalary p = en.TEmpSalaries.First(f => f.SchoolID == schoolID && f.sEmp == eid && f.ID == id);

                    if (p != null)
                    {
                        //en.TEmpSalaries.Remove(p);
                        p.cDel = true;
                        p.UpdatedBy = userData.UserID;
                        p.UpdatedDate = DateTime.Now;

                        en.SaveChanges();
                    }
                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        // Form 2 Section
        [WebMethod]
        public static string SaveItem(List<string> data)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                //int empID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKey]);

                string[] key = data[0].Split('-');
                int empID = int.Parse(key[0]); // eid
                                               //int pID = int.Parse(key[1]); // pid
                int pID = 0;

                try
                {
                    int? WorkStatus = string.IsNullOrEmpty(data[1]) ? (int?)null : int.Parse(data[1]);
                    string Degree = data[2];
                    DateTime? OfficialInEducationDate = string.IsNullOrEmpty(data[3]) ? (DateTime?)null : DateTime.Parse(data[3], new CultureInfo("th-TH"));
                    DateTime? PackingDate = string.IsNullOrEmpty(data[4]) ? (DateTime?)null : DateTime.Parse(data[4], new CultureInfo("th-TH"));
                    decimal? Salary = string.IsNullOrEmpty(data[5]) ? (decimal?)null : decimal.Parse(data[5]);
                    DateTime? OfficialStartDate = string.IsNullOrEmpty(data[6]) ? (DateTime?)null : DateTime.Parse(data[6], new CultureInfo("th-TH"));
                    decimal? PositionMoney = string.IsNullOrEmpty(data[7]) ? (decimal?)null : decimal.Parse(data[7]);
                    decimal? AcademicStandingMoney = string.IsNullOrEmpty(data[8]) ? (decimal?)null : decimal.Parse(data[8]);
                    DateTime? RetirementDate = string.IsNullOrEmpty(data[9]) ? (DateTime?)null : DateTime.Parse(data[9], new CultureInfo("th-TH"));
                    decimal? NetSalary = string.IsNullOrEmpty(data[10]) ? (decimal?)null : decimal.Parse(data[10]);
                    //DateTime? RemainDate = string.IsNullOrEmpty(data[11]) ? (DateTime?)null : DateTime.Parse(data[11], new CultureInfo("th-TH"));
                    int? RemainYear = string.IsNullOrEmpty(data[12]) ? (int?)null : int.Parse(data[12]);
                    int? RemainMonth = string.IsNullOrEmpty(data[13]) ? (int?)null : int.Parse(data[13]);
                    int? RemainDay = string.IsNullOrEmpty(data[14]) ? (int?)null : int.Parse(data[14]);

                    DateTime? DayQuit = string.IsNullOrEmpty(data[15]) ? (DateTime?)null : DateTime.Parse(data[15], new CultureInfo("th-TH"));

                    var empSalaries = en.TEmpSalaries.Where(w => w.SchoolID == schoolID && w.sEmp == empID).FirstOrDefault();
                    if (empSalaries != null)
                    {
                        pID = empSalaries.ID;
                    }

                    //if (HttpContext.Current.Session[SessionForeignKey] == null)
                    if (pID == 0)
                    {
                        // Insert Section
                        int ID = (int)(en.TEmpSalaries.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Count() == 0 ? 1 : en.TEmpSalaries.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Max(m => m.ID) + 1);

                        // Get Item
                        TEmpSalary p = new TEmpSalary
                        {
                            sEmp = empID,
                            ID = ID,
                            WorkStatus = WorkStatus,
                            Degree = Degree,
                            WorkInEducationDate = OfficialInEducationDate,
                            GovernmentOrderDate = PackingDate,
                            Salary = Salary,
                            WorkStartDate = OfficialStartDate,
                            PositionMoney = PositionMoney,
                            AcademicStandingMoney = AcademicStandingMoney,
                            RetirementDate = RetirementDate,
                            NetSalary = NetSalary,
                            RemainGovernmentYear = RemainYear,
                            RemainGovernmentMonth = RemainMonth,
                            RemainGovernmentDay = RemainDay,
                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now,
                            DayQuit = DayQuit
                        };

                        //HttpContext.Current.Session[SessionForeignKey] = p.ID;

                        en.TEmpSalaries.Add(p);

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลบุคลากร(ตำแหน่งงาน) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + p.ID, HttpContext.Current.Request, 156, 2, 0, schoolID);
                    }
                    else
                    {
                        // Modify Section
                        //empID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);
                        //int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey]);
                        int ID = pID;

                        // Get Item
                        TEmpSalary pi = en.TEmpSalaries.First(f => f.SchoolID == schoolID && f.sEmp == empID && f.ID == ID);

                        pi.WorkStatus = WorkStatus;
                        pi.Degree = Degree;
                        pi.WorkInEducationDate = OfficialInEducationDate;
                        pi.GovernmentOrderDate = PackingDate;
                        pi.Salary = Salary;
                        pi.WorkStartDate = OfficialStartDate;
                        pi.PositionMoney = PositionMoney;
                        pi.AcademicStandingMoney = AcademicStandingMoney;
                        pi.RetirementDate = RetirementDate;
                        pi.NetSalary = NetSalary;
                        pi.RemainGovernmentYear = RemainYear;
                        pi.RemainGovernmentMonth = RemainMonth;
                        pi.RemainGovernmentDay = RemainDay;
                        pi.UpdatedBy = userData.UserID;
                        pi.UpdatedDate = DateTime.Now;
                        pi.DayQuit = DayQuit;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลบุคลากร(ตำแหน่งงาน) รหัสบุคลากร:" + pi.sEmp + ", รหัสรายการ:" + ID, HttpContext.Current.Request, 156, 3, 0, schoolID);
                    }

                    isComplete += "-" + empID;
                }
                catch (Exception err)
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod]
        public static string GetItem(int empID, int id)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    TEmpSalary p = en.TEmpSalaries.Where(w => w.SchoolID == schoolID && w.sEmp == empID && w.ID == id).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 15; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        //HttpContext.Current.Session[SessionPrimaryKey] = p.sEmp;
                        //HttpContext.Current.Session[SessionForeignKey] = p.ID;

                        dt.Rows[0]["F1"] = p.WorkStatus?.ToString();
                        dt.Rows[0]["F2"] = p.Degree;
                        dt.Rows[0]["F3"] = p.WorkInEducationDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F4"] = p.GovernmentOrderDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F5"] = p.Salary?.ToString("0.#");
                        dt.Rows[0]["F6"] = p.WorkStartDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F7"] = p.PositionMoney?.ToString("0.#");
                        dt.Rows[0]["F8"] = p.AcademicStandingMoney?.ToString("0.#");
                        dt.Rows[0]["F9"] = p.RetirementDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F10"] = p.NetSalary?.ToString("0.#");
                        //dt.Rows[0]["F11"] = (p.RetirementDate == null ? "" : (p.RetirementDate.Value - DateTime.Now).TotalDays.ToString("#,0"));

                        dt.Rows[0]["F12"] = p.RemainGovernmentYear?.ToString();
                        dt.Rows[0]["F13"] = p.RemainGovernmentMonth?.ToString();
                        dt.Rows[0]["F14"] = p.RemainGovernmentDay?.ToString();

                        dt.Rows[0]["F15"] = p.DayQuit?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch (Exception err)
                {
                    infor = "error";
                }

                //if (infor == "new" || infor == "error") HttpContext.Current.Session[SessionForeignKey] = null;

                return infor;
            }
        }

        [WebMethod]
        public static string ClearSessionID()
        {
            //HttpContext.Current.Session[SessionForeignKey] = null;

            return "";
        }

        #endregion
    }
}