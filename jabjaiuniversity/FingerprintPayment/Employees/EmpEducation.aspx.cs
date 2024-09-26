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
    public partial class EmpEducation : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPEDUCATIONID";

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
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    TEmpEducationInfo p = en.TEmpEducationInfoes.First(f => f.SchoolID == schoolID && f.sEmp == eid && f.ID == id);

                    if (p != null)
                    {
                        p.cDel = true;
                        p.UpdateBy = userData.UserID;
                        p.UpdateDate = DateTime.Now;

                        en.SaveChanges();
                    }

                    database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลบุคลากร(ข้อมูลการศึกษา) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + id, HttpContext.Current.Request, 156, 4, 0, schoolID);
                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod]
        public static string RemoveItem2(int eid, int id)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    TEmpHonor p = en.TEmpHonors.First(f => f.SchoolID == schoolID && f.sEmp == eid && f.ID == id);

                    if (p != null)
                    {
                        p.cDel = true;
                        p.UpdateBy = userData.UserID;
                        p.UpdateDate = DateTime.Now;

                        en.SaveChanges();
                    }

                    database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลบุคลากร(ข้อมูลเกียรติคุณ) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + id, HttpContext.Current.Request, 156, 4, 0, schoolID);
                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
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

                //int empID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKey]);

                string[] key = data[0].Split('-');
                int empID = int.Parse(key[0]); // eid
                int pID = int.Parse(key[1]); // pid

                try
                {
                    //if (HttpContext.Current.Session[SessionPrimaryKey] != null)
                    if (empID != 0)
                    {
                        string Institution = data[1];
                        int? StudyYear = string.IsNullOrEmpty(data[2]) ? (int?)null : int.Parse(data[2]);
                        int? GraduationYear = string.IsNullOrEmpty(data[3]) ? (int?)null : int.Parse(data[3]);
                        int? Level = string.IsNullOrEmpty(data[4]) ? (int?)null : int.Parse(data[4]);
                        string Major = data[5];
                        string MinorSubject = data[6];

                        //if (HttpContext.Current.Session[SessionForeignKey] == null)
                        if (pID == 0)
                        {
                            // Insert Section
                            int ID = (int)(en.TEmpEducationInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Count() == 0 ? 1 : en.TEmpEducationInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Max(m => m.ID) + 1);

                            // Get Item
                            TEmpEducationInfo p = new TEmpEducationInfo
                            {
                                sEmp = empID,
                                ID = ID,
                                Institution = Institution,
                                StudyYear = StudyYear,
                                GraduationYear = GraduationYear,
                                LevelID = Level,
                                Major = Major,
                                MinorSubject = MinorSubject,
                                SchoolID = schoolID,

                                CreatedDate = DateTime.Now,
                                CreatedBy = userData.UserID
                            };

                            en.TEmpEducationInfoes.Add(p);

                            en.SaveChanges();

                            database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลบุคลากร(ข้อมูลการศึกษา) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + p.ID, HttpContext.Current.Request, 156, 2, 0, schoolID);
                        }
                        else
                        {
                            // Modify Section
                            //int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey]);
                            int ID = pID;

                            TEmpEducationInfo pi = en.TEmpEducationInfoes.First(f => f.SchoolID == schoolID && f.sEmp == empID && f.ID == ID);
                            if (pi != null)
                            {
                                pi.Institution = Institution;
                                pi.StudyYear = StudyYear;
                                pi.GraduationYear = GraduationYear;
                                pi.LevelID = Level;
                                pi.Major = Major;
                                pi.MinorSubject = MinorSubject;

                                pi.UpdateDate = DateTime.Now;
                                pi.UpdateBy = userData.UserID;

                                en.SaveChanges();

                                database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลบุคลากร(ข้อมูลการศึกษา) รหัสบุคลากร:" + pi.sEmp + ", รหัสรายการ:" + ID, HttpContext.Current.Request, 156, 3, 0, schoolID);
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

                    TEmpEducationInfo p = en.TEmpEducationInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == iEmpID && w.ID == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 6; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        //HttpContext.Current.Session[SessionPrimaryKey] = p.sEmp;
                        //HttpContext.Current.Session[SessionForeignKey] = p.ID;

                        dt.Rows[0]["F1"] = p.Institution;
                        dt.Rows[0]["F2"] = p.StudyYear;
                        dt.Rows[0]["F3"] = p.GraduationYear;
                        dt.Rows[0]["F4"] = p.LevelID?.ToString();
                        dt.Rows[0]["F5"] = p.Major;
                        dt.Rows[0]["F6"] = p.MinorSubject;

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