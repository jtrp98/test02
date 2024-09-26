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
    public partial class EmpTraining : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPTRAININGID";

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
                    break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        [WebMethod]
        public static string RemoveItem(int eid, int id)
        {
            var userData = GetUserData();
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read));

            string isComplete = "complete";

            try
            {
                TEmpTraining p = en.TEmpTrainings.First(f => f.SchoolID == userData.CompanyID && f.sEmp == eid && f.ID == id);

                if (p != null)
                {
                    p.cDel = true;
                    p.UpdateBy = userData.UserID;
                    p.UpdateDate = DateTime.Now;

                    en.SaveChanges();
                }

                database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลบุคลากร(ประวัติการศึกษา อบรม ดูงาน) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + id, HttpContext.Current.Request, 156, 4, 0, userData.CompanyID);
            }
            catch
            {
                isComplete = "error";
            }

            return isComplete;
        }

        [WebMethod]
        public static string SaveItem(List<string> data)
        {
            var userData = GetUserData();
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read));

            string isComplete = "complete";

            //int empID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKey]);

            string[] key = data[0].Split('-');
            int empID = int.Parse(key[0]); // eid
            int pID = int.Parse(key[1]); // pid

            try
            {
                //if (HttpContext.Current.Session[SessionPrimaryKey] != null)
                if (empID != 0)
                {
                    string ProjectName = data[1];
                    string TrainingName = data[2];
                    DateTime? StartDate = string.IsNullOrEmpty(data[3]) ? (DateTime?)null : DateTime.Parse(data[3], new CultureInfo("th-TH"));
                    DateTime? EndDate = string.IsNullOrEmpty(data[4]) ? (DateTime?)null : DateTime.Parse(data[4], new CultureInfo("th-TH"));
                    string Place = data[5];
                    string Province = data[6];
                    string Country = data[7];
                    Decimal? Expenses = string.IsNullOrEmpty(data[8]) ? (Decimal?)null : decimal.Parse(data[8]);
                    string TraininType = data[9];
                    Decimal? TrainingHours = string.IsNullOrEmpty(data[10]) ? (Decimal?)null : decimal.Parse(data[10]);

                    //if (HttpContext.Current.Session[SessionForeignKey] == null)
                    if (pID == 0)
                    {
                        // Insert Section
                        int ID = (int)(en.TEmpTrainings.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == empID).Count() == 0 ? 1 : en.TEmpTrainings.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == empID).Max(m => m.ID) + 1);

                        // Get Item
                        TEmpTraining p = new TEmpTraining
                        {
                            sEmp = empID,
                            ID = ID,
                            ProjectName = ProjectName,
                            TrainingName = TrainingName,
                            StartDate = StartDate,
                            EndDate = EndDate,
                            Place = Place,
                            Province = Province,
                            Country = Country,
                            Expenses = Expenses,
                            SchoolID = userData.CompanyID,
                            TrainingType = TraininType,
                            TrainingHours = TrainingHours,

                            CreatedDate = DateTime.Now,
                            CreatedBy = userData.UserID
                        };

                        en.TEmpTrainings.Add(p);

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลบุคลากร(ประวัติการศึกษา อบรม ดูงาน) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + p.ID, HttpContext.Current.Request, 156, 2, 0, userData.CompanyID);
                    }
                    else
                    {
                        // Modify Section
                        //int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey]);
                        int ID = pID;

                        TEmpTraining pi = en.TEmpTrainings.First(f => f.SchoolID == userData.CompanyID && f.sEmp == empID && f.ID == ID);
                        if (pi != null)
                        {
                            pi.ProjectName = ProjectName;
                            pi.TrainingName = TrainingName;
                            pi.StartDate = StartDate;
                            pi.EndDate = EndDate;
                            pi.Place = Place;
                            pi.Province = Province;
                            pi.Country = Country;
                            pi.Expenses = Expenses;
                            pi.TrainingType = TraininType;
                            pi.TrainingHours = TrainingHours;

                            pi.UpdateDate = DateTime.Now;
                            pi.UpdateBy = userData.UserID;

                            en.SaveChanges();

                            database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลบุคลากร(ประวัติการศึกษา อบรม ดูงาน) รหัสบุคลากร:" + pi.sEmp + ", รหัสรายการ:" + ID, HttpContext.Current.Request, 156, 3, 0, userData.CompanyID);
                        }
                        else
                        {
                            isComplete = "warning";
                        }
                    }
                }
                else
                {
                    isComplete = "error";
                }
            }
            catch
            {
                isComplete = "error";
            }

            return isComplete;
        }

        [WebMethod]
        public static string GetItem(string empID, string id)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iEmpID = 0;
                    if (!int.TryParse(empID, out iEmpID)) { iEmpID = 0; }

                    int iID = 0;
                    if (!int.TryParse(id, out iID)) { iID = 0; }

                    TEmpTraining p = en.TEmpTrainings.Where(w => w.SchoolID == schoolID && w.sEmp == iEmpID && w.ID == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 10; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        //HttpContext.Current.Session[SessionPrimaryKey] = p.EmpID;
                        //HttpContext.Current.Session[SessionForeignKey] = p.ID;

                        dt.Rows[0]["F1"] = p.ProjectName;
                        dt.Rows[0]["F2"] = p.TrainingName;
                        dt.Rows[0]["F3"] = p.StartDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F4"] = p.EndDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F5"] = p.Place;
                        dt.Rows[0]["F6"] = p.Province;
                        dt.Rows[0]["F7"] = p.Country;
                        dt.Rows[0]["F8"] = p.Expenses?.ToString("0.#");
                        dt.Rows[0]["F9"] = p.TrainingType;
                        dt.Rows[0]["F10"] = p.TrainingHours?.ToString("0.#");

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