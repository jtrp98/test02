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
    public partial class EmpNameChange : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPNAMECHANGEID";

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
            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            string isComplete = "complete";

            try
            {
                if (HttpContext.Current.Session[SessionPrimaryKey] != null)
                {
                    int empID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                    string OldFirstName = data[1];
                    string OldLastName = data[2];
                    string NewFirstName = data[3];
                    string NewLastName = data[4];
                    DateTime? ChangeDate = string.IsNullOrEmpty(data[5]) ? (DateTime?)null : DateTime.Parse(data[5], new CultureInfo("th-TH"));
                    string ChangePlace = data[6];

                    if (HttpContext.Current.Session[SessionForeignKey] == null)
                    {
                        // Insert Section
                        //int ID = (int)(en.TEmpNameChanges.Where(w => w.EmpID == empID).Count() == 0 ? 1 : en.TEmpNameChanges.Where(w => w.EmpID == empID).Max(m => m.ID) + 1);

                        //// Get Item
                        //TEmpNameChange p = new TEmpNameChange
                        //{
                        //    EmpID = empID,
                        //    ID = ID,
                        //    OldFirstName = OldFirstName,
                        //    OldLastName = OldLastName,
                        //    NewFirstName = NewFirstName,
                        //    NewLastName = NewLastName,
                        //    ChangeDate = ChangeDate,
                        //    ChangePlace = ChangePlace,

                        //    UpdateDate = DateTime.Now
                        //};

                        //en.TEmpNameChanges.Add(p);

                        //en.SaveChanges();

                    }
                    else
                    {
                        // Modify Section
                        int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey]);

                        //TEmpNameChange pi = en.TEmpNameChanges.First(f => f.EmpID == empID && f.ID == ID);
                        //if (pi != null)
                        //{
                        //    pi.OldFirstName = OldFirstName;
                        //    pi.OldLastName = OldLastName;
                        //    pi.NewFirstName = NewFirstName;
                        //    pi.NewLastName = NewLastName;
                        //    pi.ChangeDate = ChangeDate;
                        //    pi.ChangePlace = ChangePlace;

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

                //TEmpNameChange p = en.TEmpNameChanges.Where(w => w.EmpID == iEmpID && w.ID == iID).FirstOrDefault();
                //if (p != null)
                //{
                //    DataSet ds = new DataSet();
                //    DataTable dt = new DataTable("Table1");
                //    for (int i = 1; i <= 6; i++)
                //    {
                //        dt.Columns.Add("F" + i);
                //    }

                //    dt.Rows.Add();

                //    HttpContext.Current.Session[SessionPrimaryKey] = p.EmpID;
                //    HttpContext.Current.Session[SessionForeignKey] = p.ID;

                //    dt.Rows[0]["F1"] = p.OldFirstName;
                //    dt.Rows[0]["F2"] = p.OldLastName;
                //    dt.Rows[0]["F3"] = p.NewFirstName;
                //    dt.Rows[0]["F4"] = p.NewLastName;
                //    dt.Rows[0]["F5"] = (p.ChangeDate == null ? null : p.ChangeDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                //    dt.Rows[0]["F6"] = p.ChangePlace;

                //    ds.Tables.Add(dt);

                //    infor = ds.GetXml();
                //}
                //else
                //{
                //    infor = "new";
                //}
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
            HttpContext.Current.Session[SessionPrimaryKey] = null;

            return "";
        }

        #endregion
    }
}