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
    public partial class EmpFamily : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPFAMILYID";

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

                    // Init data dropdown
                    int schoolID = UserData.CompanyID;
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {

                        // sltTitle
                        var titleLists = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1").ToList();
                        foreach (var r in titleLists)
                        {
                            this.ltrTitle.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTitleid, r.titleDescription);
                        }

                        //// sltLevel
                        //var levels = en.TLevels.Where(w => w.SchoolID == schoolID).ToList();
                        //foreach (var r in levels)
                        //{
                        //    this.ltrLevel.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.LevelID, r.LevelName);
                        //}

                        break;
                    }
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
                    TEmpFamily p = en.TEmpFamilies.First(f => f.SchoolID == schoolID && f.sEmp == eid && f.ID == id);

                    if (p != null)
                    {
                        p.cDel = true;
                        p.UpdateBy = userData.UserID;
                        p.UpdateDate = DateTime.Now;

                        en.SaveChanges();
                    }

                    database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลบุคลากร(ข้อมูลครอบครัว) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + id + " ฃื่อสมาชิกในครอบครัว:" + p.FirstName + " " + p.LastName, HttpContext.Current.Request, 156, 4, 0, schoolID);
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
                        string FamilyRelation = data[1];
                        int? Title = string.IsNullOrEmpty(data[2]) ? (int?)null : int.Parse(data[2]);
                        string FirstName = data[3];
                        string LastName = data[4];
                        DateTime? Birthday = string.IsNullOrEmpty(data[5]) ? (DateTime?)null : DateTime.Parse(data[5], new CultureInfo("th-TH"));
                        int? PersonalStatus = string.IsNullOrEmpty(data[6]) ? (int?)null : int.Parse(data[6]);
                        string LiveStatus = data[7];
                        string DeathStatus = data[8];
                        string FamilyCareer = data[9];
                        //int? Level = string.IsNullOrEmpty(data[10]) ? (int?)null : int.Parse(data[10]);
                        int? EducationBackground = string.IsNullOrEmpty(data[10]) ? (int?)null : int.Parse(data[10]);

                        //if (HttpContext.Current.Session[SessionForeignKey] == null)
                        if (pID == 0)
                        {
                            // Insert Section
                            int ID = (int)(en.TEmpFamilies.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Count() == 0 ? 1 : en.TEmpFamilies.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Max(m => m.ID) + 1);

                            // Get Item
                            TEmpFamily p = new TEmpFamily
                            {
                                sEmp = empID,
                                ID = ID,
                                FamilyRelation = FamilyRelation,
                                TitleID = Title,
                                FirstName = FirstName,
                                LastName = LastName,
                                Birthday = Birthday,
                                PersonalStatus = PersonalStatus,
                                LiveStatus = LiveStatus,
                                DeathStatus = DeathStatus,
                                FamilyCareer = FamilyCareer,
                                //LevelID = Level,
                                EducationBackground = EducationBackground,
                                SchoolID = schoolID,

                                CreatedDate = DateTime.Now,
                                CreatedBy = userData.UserID
                            };

                            en.TEmpFamilies.Add(p);

                            en.SaveChanges();

                            database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลบุคลากร(ข้อมูลครอบครัว) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + p.ID + ", ฃื่อสมาชิกในครอบครัว:" + p.FirstName + " " + p.LastName, HttpContext.Current.Request, 156, 2, 0, schoolID);
                        }
                        else
                        {
                            // Modify Section
                            //int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey]);
                            int ID = pID;

                            TEmpFamily pi = en.TEmpFamilies.First(f => f.SchoolID == schoolID && f.sEmp == empID && f.ID == ID);
                            if (pi != null)
                            {
                                pi.FamilyRelation = FamilyRelation;
                                pi.TitleID = Title;
                                pi.FirstName = FirstName;
                                pi.LastName = LastName;
                                pi.Birthday = Birthday;
                                pi.PersonalStatus = PersonalStatus;
                                pi.LiveStatus = LiveStatus;
                                pi.DeathStatus = DeathStatus;
                                pi.FamilyCareer = FamilyCareer;
                                //pi.LevelID = Level;
                                pi.EducationBackground = EducationBackground;

                                pi.UpdateDate = DateTime.Now;
                                pi.UpdateBy = userData.UserID;

                                en.SaveChanges();

                                database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลบุคลากร(ข้อมูลครอบครัว) รหัสบุคลากร:" + pi.sEmp + ", รหัสรายการ:" + ID + ", ฃื่อสมาชิกในครอบครัว:" + pi.FirstName + " " + pi.LastName, HttpContext.Current.Request, 156, 3, 0, schoolID);
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

                    TEmpFamily p = en.TEmpFamilies.Where(w => w.SchoolID == schoolID && w.sEmp == iEmpID && w.ID == iID).FirstOrDefault();
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

                        dt.Rows[0]["F1"] = p.FamilyRelation;
                        dt.Rows[0]["F2"] = p.TitleID?.ToString();
                        dt.Rows[0]["F3"] = p.FirstName;
                        dt.Rows[0]["F4"] = p.LastName;
                        dt.Rows[0]["F5"] = p.Birthday?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F6"] = p.PersonalStatus?.ToString();
                        dt.Rows[0]["F7"] = p.LiveStatus;
                        dt.Rows[0]["F8"] = p.DeathStatus;
                        dt.Rows[0]["F9"] = p.FamilyCareer;
                        //dt.Rows[0]["F10"] = p.LevelID?.ToString();
                        dt.Rows[0]["F10"] = p.EducationBackground?.ToString();

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