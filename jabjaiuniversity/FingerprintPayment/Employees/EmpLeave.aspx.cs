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
    public partial class EmpLeave : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPLEAVEID";

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
                //TEmpLeave p = en.TEmpLeaves.First(f => f.EmpID == eid && f.ID == id);

                //en.TEmpLeaves.Remove(p);

                //en.SaveChanges();
            }
            catch
            {
                isComplete = "error";
            }

            return isComplete;
        }

        [WebMethod]
        public static string RemoveItem2(int eid, int id)
        {
            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            string isComplete = "complete";

            try
            {
                //TEmpNameChange p = en.TEmpNameChanges.First(f => f.EmpID == eid && f.ID == id);

                //en.TEmpNameChanges.Remove(p);

                //en.SaveChanges();
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
                    int? Late = string.IsNullOrEmpty(data[2]) ? (int?)null : int.Parse(data[2]);
                    int? Sick = string.IsNullOrEmpty(data[3]) ? (int?)null : int.Parse(data[3]);
                    int? Errand = string.IsNullOrEmpty(data[4]) ? (int?)null : int.Parse(data[4]);
                    int? Lacking = string.IsNullOrEmpty(data[5]) ? (int?)null : int.Parse(data[5]);
                    int? ToGovernor = string.IsNullOrEmpty(data[6]) ? (int?)null : int.Parse(data[6]);
                    int? Holiday = string.IsNullOrEmpty(data[7]) ? (int?)null : int.Parse(data[7]);
                    int? Maternity = string.IsNullOrEmpty(data[8]) ? (int?)null : int.Parse(data[8]);
                    int? Ordain = string.IsNullOrEmpty(data[9]) ? (int?)null : int.Parse(data[9]);
                    int? ContinueStudy = string.IsNullOrEmpty(data[10]) ? (int?)null : int.Parse(data[10]);
                    
                    if (HttpContext.Current.Session[SessionForeignKey] == null)
                    {
                        // Insert Section
                        //int ID = (int)(en.TEmpLeaves.Where(w => w.EmpID == empID).Count() == 0 ? 1 : en.TEmpLeaves.Where(w => w.EmpID == empID).Max(m => m.ID) + 1);

                        //// Get Item
                        //TEmpLeave p = new TEmpLeave
                        //{
                        //    EmpID = empID,
                        //    ID = ID,
                        //    Year = Year,
                        //    Late = Late,
                        //    Sick = Sick,
                        //    Errand = Errand,
                        //    Lacking = Lacking,
                        //    ToGovernor = ToGovernor,
                        //    Holiday = Holiday,
                        //    Maternity = Maternity,
                        //    Ordain = Ordain,
                        //    ContinueStudy = ContinueStudy,

                        //    UpdateDate = DateTime.Now
                        //};

                        //en.TEmpLeaves.Add(p);

                        //en.SaveChanges();

                    }
                    else
                    {
                        // Modify Section
                        int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey]);

                        //TEmpLeave pi = en.TEmpLeaves.First(f => f.EmpID == empID && f.ID == ID);
                        //if (pi != null)
                        //{
                        //    pi.Year = Year;
                        //    pi.Late = Late;
                        //    pi.Sick = Sick;
                        //    pi.Errand = Errand;
                        //    pi.Lacking = Lacking;
                        //    pi.ToGovernor = ToGovernor; ;
                        //    pi.Holiday = Holiday;
                        //    pi.Maternity = Maternity;
                        //    pi.Ordain = Ordain;
                        //    pi.ContinueStudy = ContinueStudy;

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

                //TEmpLeave p = en.TEmpLeaves.Where(w => w.EmpID == iEmpID && w.ID == iID).FirstOrDefault();
                //if (p != null)
                //{
                //    DataSet ds = new DataSet();
                //    DataTable dt = new DataTable("Table1");
                //    for (int i = 1; i <= 10; i++)
                //    {
                //        dt.Columns.Add("F" + i);
                //    }

                //    dt.Rows.Add();

                //    HttpContext.Current.Session[SessionPrimaryKey] = p.EmpID;
                //    HttpContext.Current.Session[SessionForeignKey] = p.ID;

                //    dt.Rows[0]["F1"] = (p.Year == null ? null : p.Year.Value.ToString());
                //    dt.Rows[0]["F2"] = (p.Late == null ? null : p.Late.Value.ToString());
                //    dt.Rows[0]["F3"] = (p.Sick == null ? null : p.Sick.Value.ToString());
                //    dt.Rows[0]["F4"] = (p.Errand == null ? null : p.Errand.Value.ToString());
                //    dt.Rows[0]["F5"] = (p.Lacking == null ? null : p.Lacking.Value.ToString());
                //    dt.Rows[0]["F6"] = (p.ToGovernor == null ? null : p.ToGovernor.Value.ToString());
                //    dt.Rows[0]["F7"] = (p.Holiday == null ? null : p.Holiday.Value.ToString());
                //    dt.Rows[0]["F8"] = (p.Maternity == null ? null : p.Maternity.Value.ToString());
                //    dt.Rows[0]["F9"] = (p.Ordain == null ? null : p.Ordain.Value.ToString());
                //    dt.Rows[0]["F10"] = (p.ContinueStudy == null ? null : p.ContinueStudy.Value.ToString());

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
            HttpContext.Current.Session[SessionForeignKey] = null;

            return "";
        }

        #endregion
    }
}