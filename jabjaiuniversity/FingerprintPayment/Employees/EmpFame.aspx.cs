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
    public partial class EmpFame : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPFAMEID";

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
                    MvContent.ActiveViewIndex = 1; break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        [WebMethod]
        public static string SaveItem(List<string> data)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                string[] key = data[0].Split('-');
                int empID = int.Parse(key[0]); // eid
                int pID = int.Parse(key[1]); // pid

                try
                {
                    //if (HttpContext.Current.Session[SessionPrimaryKey] != null)
                    if (empID != 0)
                    {
                        //int empID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                        string Type = data[1];
                        string Department = data[2];
                        int? Year = string.IsNullOrEmpty(data[3]) ? (int?)null : int.Parse(data[3]);

                        //if (HttpContext.Current.Session[SessionForeignKey] == null)
                        if (pID == 0)
                        {
                            // Insert Section
                            int ID = (int)(en.TEmpHonors.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Count() == 0 ? 1 : en.TEmpHonors.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Max(m => m.ID) + 1);

                            // Get Item
                            TEmpHonor p = new TEmpHonor
                            {
                                sEmp = empID,
                                ID = ID,
                                Type = Type,
                                Department = Department,
                                Year = Year,
                                SchoolID = schoolID,

                                CreatedDate = DateTime.Now,
                                CreatedBy = userData.UserID
                            };

                            en.TEmpHonors.Add(p);

                            en.SaveChanges();

                            database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลบุคลากร(ข้อมูลเกียรติคุณ) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + ID, HttpContext.Current.Request, 156, 2, 0, schoolID);
                        }
                        else
                        {
                            // Modify Section
                            //int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey]);
                            int ID = pID;

                            TEmpHonor pi = en.TEmpHonors.First(f => f.SchoolID == schoolID && f.sEmp == empID && f.ID == ID);
                            if (pi != null)
                            {
                                pi.Type = Type;
                                pi.Department = Department;
                                pi.Year = Year;

                                pi.UpdateDate = DateTime.Now;
                                pi.UpdateBy = userData.UserID;

                                en.SaveChanges();

                                database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลบุคลากร(ข้อมูลเกียรติคุณ) รหัสบุคลากร:" + pi.sEmp + ", รหัสรายการ:" + ID, HttpContext.Current.Request, 156, 3, 0, schoolID);
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
        }

        [WebMethod]
        public static string GetItem(string empID, string id)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iEmpID = 0;
                    if (!int.TryParse(empID, out iEmpID)) { iEmpID = 0; }

                    int iID = 0;
                    if (!int.TryParse(id, out iID)) { iID = 0; }

                    TEmpHonor p = en.TEmpHonors.Where(w => w.SchoolID == schoolID && w.sEmp == iEmpID && w.ID == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 3; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        //HttpContext.Current.Session[SessionPrimaryKey] = p.EmpID;
                        //HttpContext.Current.Session[SessionForeignKey] = p.ID;

                        dt.Rows[0]["F1"] = p.Type;
                        dt.Rows[0]["F2"] = p.Department;
                        dt.Rows[0]["F3"] = p.Year;

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