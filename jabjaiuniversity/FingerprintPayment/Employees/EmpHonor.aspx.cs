using FingerprintPayment.Employees.CsCode;
using JabjaiEntity.DB;
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
    public partial class EmpHonor : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPHONORID";

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
        public static string RemoveItem(int eid, int id)
        {
            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            string isComplete = "complete";

            try
            {
                TEmpHonor p = en.TEmpHonors.First(f => f.sEmp == eid && f.ID == id);

                en.TEmpHonors.Remove(p);

                en.SaveChanges();
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
            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            string isComplete = "complete";

            try
            {
                if (HttpContext.Current.Session[SessionPrimaryKey] != null)
                {
                    int empID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                    int? Year = string.IsNullOrEmpty(data[1]) ? (int?)null : int.Parse(data[1]);
                    string Image = data[2];

                    if (HttpContext.Current.Session[SessionForeignKey] == null)
                    {
                        // Insert Section
                        int ID = (int)(en.TEmpHonors.Where(w => w.sEmp == empID).Count() == 0 ? 1 : en.TEmpHonors.Where(w => w.sEmp == empID).Max(m => m.ID) + 1);
                        //int No = (int)(en.TEmpHonors.Where(w => w.EmpID == empID).Count() == 0 ? 1 : en.TEmpHonors.Where(w => w.EmpID == empID).Max(m => m.No) + 1);

                        //// Get Item
                        //TEmpHonor p = new TEmpHonor
                        //{
                        //    EmpID = empID,
                        //    ID = ID,
                        //    No = No,
                        //    Year = Year,
                        //    Image = Image,

                        //    UpdateDate = DateTime.Now
                        //};

                        //en.TEmpHonors.Add(p);

                        //en.SaveChanges();

                    }
                    else
                    {
                        // Modify Section
                        int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey]);

                        //TEmpHonor pi = en.TEmpHonors.First(f => f.EmpID == empID && f.ID == ID);
                        //if (pi != null)
                        //{
                        //    pi.Year = Year;
                        //    pi.Image = Image;

                        //    pi.UpdateDate = DateTime.Now;

                        //    en.SaveChanges();
                        //}
                        //else
                        //{
                        //    isComplete = "warning";
                        //}
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
            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            string infor = "new";

            try
            {
                int iEmpID = 0;
                if (!int.TryParse(empID, out iEmpID)) { iEmpID = 0; }

                int iID = 0;
                if (!int.TryParse(id, out iID)) { iID = 0; }

                TEmpHonor p = en.TEmpHonors.Where(w => w.sEmp == iEmpID && w.ID == iID).FirstOrDefault();
                if (p != null)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable("Table1");
                    for (int i = 1; i <= 3; i++)
                    {
                        dt.Columns.Add("F" + i);
                    }

                    dt.Rows.Add();

                    HttpContext.Current.Session[SessionPrimaryKey] = p.sEmp;
                    HttpContext.Current.Session[SessionForeignKey] = p.ID;

                    dt.Rows[0]["F1"] = (p.Year == null ? null : p.Year.Value.ToString());
                    //dt.Rows[0]["F2"] = p.Image;

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

            if (infor == "new" || infor == "error") HttpContext.Current.Session[SessionForeignKey] = null;

            return infor;
        }

        [WebMethod]
        public static string ClearSessionID()
        {
            HttpContext.Current.Session[SessionForeignKey] = null;

            return "";
        }

        #endregion
    }
}