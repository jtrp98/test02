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
    public partial class EmpInsignia : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPINSIGNIAID";

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
                    // sltYear
                    for (int i = DateTime.Now.Year + 544; i >= 2500; i--)
                    {
                        this.ltrYear.Text += string.Format(@"<option value=""{0}"">{0}</option>", i);
                    }

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
                TEmpInsignia p = en.TEmpInsignias.First(f => f.SchoolID == userData.CompanyID && f.sEmp == eid && f.ID == id);

                if (p != null)
                {
                    p.cDel = true;
                    p.UpdateBy = userData.UserID;
                    p.UpdateDate = DateTime.Now;

                    en.SaveChanges();
                }

                database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลบุคลากร(ประวัติการรับเครื่องราชฯ) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + id, HttpContext.Current.Request, 156, 4, 0, userData.CompanyID);
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
                    int? Year = string.IsNullOrEmpty(data[1]) ? (int?)null : int.Parse(data[1]);
                    string Grade = data[2];
                    string Position = data[3];
                    string BookNumber = data[4];
                    string Part = data[5];
                    string Number = data[6];
                    DateTime? Date = string.IsNullOrEmpty(data[7]) ? (DateTime?)null : DateTime.Parse(data[7], new CultureInfo("th-TH"));

                    //if (HttpContext.Current.Session[SessionForeignKey] == null)
                    if (pID == 0)
                    {
                        // Insert Section
                        int ID = (int)(en.TEmpInsignias.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == empID).Count() == 0 ? 1 : en.TEmpInsignias.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == empID).Max(m => m.ID) + 1);

                        // Get Item
                        TEmpInsignia p = new TEmpInsignia
                        {
                            sEmp = empID,
                            ID = ID,
                            Year = Year,
                            Grade = Grade,
                            Position = Position,
                            BookNumber = BookNumber,
                            Part = Part,
                            Number = Number,
                            Date = Date,
                            SchoolID = userData.CompanyID,

                            CreatedDate = DateTime.Now,
                            CreatedBy = userData.UserID
                        };

                        en.TEmpInsignias.Add(p);

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลบุคลากร(ประวัติการรับเครื่องราชฯ) รหัสบุคลากร:" + p.sEmp + ", รหัสรายการ:" + p.ID, HttpContext.Current.Request, 156, 2, 0, userData.CompanyID);
                    }
                    else
                    {
                        // Modify Section
                        //int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey]);
                        int ID = pID;

                        TEmpInsignia pi = en.TEmpInsignias.First(f => f.SchoolID == userData.CompanyID && f.sEmp == empID && f.ID == ID);
                        if (pi != null)
                        {
                            pi.Year = Year;
                            pi.Grade = Grade;
                            pi.Position = Position;
                            pi.BookNumber = BookNumber;
                            pi.Part = Part;
                            pi.Number = Number;
                            pi.Date = Date;

                            pi.UpdateDate = DateTime.Now;
                            pi.UpdateBy = userData.UserID;

                            en.SaveChanges();

                            database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลบุคลากร(ประวัติการรับเครื่องราชฯ) รหัสบุคลากร:" + pi.sEmp + ", รหัสรายการ:" + ID, HttpContext.Current.Request, 156, 3, 0, userData.CompanyID);
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

                    TEmpInsignia p = en.TEmpInsignias.Where(w => w.SchoolID == schoolID && w.sEmp == iEmpID && w.ID == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 7; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        //HttpContext.Current.Session[SessionPrimaryKey] = p.EmpID;
                        //HttpContext.Current.Session[SessionForeignKey] = p.ID;

                        dt.Rows[0]["F1"] = p.Year?.ToString();
                        dt.Rows[0]["F2"] = p.Grade;
                        dt.Rows[0]["F3"] = p.Position;
                        dt.Rows[0]["F4"] = p.BookNumber;
                        dt.Rows[0]["F5"] = p.Part;
                        dt.Rows[0]["F6"] = p.Number;
                        dt.Rows[0]["F7"] = p.Date?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

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