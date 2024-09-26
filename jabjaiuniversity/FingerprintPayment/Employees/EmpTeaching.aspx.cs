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
    public partial class EmpTeaching : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPTEACHINGID";

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
                //TEmpTeaching p = en.TEmpTeachings.First(f => f.EmpID == eid && f.ID == id);

                //en.TEmpTeachings.Remove(p);

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

            int empID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKey]);

            try
            {
                if (HttpContext.Current.Session[SessionPrimaryKey] != null)
                {

                    int? Year = string.IsNullOrEmpty(data[1]) ? (int?)null : int.Parse(data[1]);
                    string Term = data[2];
                    int? CourseType = string.IsNullOrEmpty(data[3]) ? (int?)null : int.Parse(data[3]);
                    int? Subject = string.IsNullOrEmpty(data[4]) ? (int?)null : int.Parse(data[4]);
                    string Class = data[5];
                    string Room = data[6];
                    int? HoursPerWeek = string.IsNullOrEmpty(data[7]) ? (int?)null : int.Parse(data[7]);
                    int? DirectTeaching = string.IsNullOrEmpty(data[8]) ? (int?)null : int.Parse(data[8]);
                    int? CompetentTeaching = string.IsNullOrEmpty(data[9]) ? (int?)null : int.Parse(data[9]);
                    int? WantTrain = string.IsNullOrEmpty(data[10]) ? (int?)null : int.Parse(data[10]);

                    if (HttpContext.Current.Session[SessionForeignKey] == null)
                    {
                        // Insert Section
                        //int ID = (int)(en.TEmpTeachings.Where(w => w.EmpID == empID).Count() == 0 ? 1 : en.TEmpTeachings.Where(w => w.EmpID == empID).Max(m => m.ID) + 1);

                        //// Get Item
                        //TEmpTeaching p = new TEmpTeaching
                        //{
                        //    EmpID = empID,
                        //    ID = ID,
                        //    nYear = Year,
                        //    nTerm = Term,
                        //    courseTypeId = CourseType,
                        //    SUBJECT_ID = Subject,
                        //    sClassID = Class,
                        //    sRoomID = Room,
                        //    HoursPerWeek = HoursPerWeek,
                        //    DirectTeaching = DirectTeaching,
                        //    CompetentTeaching = CompetentTeaching,
                        //    WantTrain = WantTrain,

                        //    UpdateDate = DateTime.Now
                        //};

                        //en.TEmpTeachings.Add(p);

                        //en.SaveChanges();

                    }
                    else
                    {
                        // Modify Section
                        int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey]);

                        //TEmpTeaching pi = en.TEmpTeachings.First(f => f.EmpID == empID && f.ID == ID);
                        //if (pi != null)
                        //{
                        //    pi.nYear = Year;
                        //    pi.nTerm = Term;
                        //    pi.courseTypeId = CourseType;
                        //    pi.SUBJECT_ID = Subject;
                        //    pi.sClassID = Class;
                        //    pi.sRoomID = Room;
                        //    pi.HoursPerWeek = HoursPerWeek;
                        //    pi.DirectTeaching = DirectTeaching;
                        //    pi.CompetentTeaching = CompetentTeaching;
                        //    pi.WantTrain = WantTrain;

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

                //TEmpTeaching p = en.TEmpTeachings.Where(w => w.EmpID == iEmpID && w.ID == iID).FirstOrDefault();
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

                //    dt.Rows[0]["F1"] = (p.nYear == null ? null : p.nYear.Value.ToString());
                //    dt.Rows[0]["F2"] = p.nTerm;
                //    dt.Rows[0]["F3"] = (p.courseTypeId == null ? null : p.courseTypeId.Value.ToString());
                //    dt.Rows[0]["F4"] = (p.SUBJECT_ID == null ? null : p.SUBJECT_ID.Value.ToString());
                //    dt.Rows[0]["F5"] = p.sClassID;
                //    dt.Rows[0]["F6"] = p.sRoomID;
                //    dt.Rows[0]["F7"] = (p.HoursPerWeek == null ? null : p.HoursPerWeek.Value.ToString());
                //    dt.Rows[0]["F8"] = (p.DirectTeaching == null ? null : p.DirectTeaching.Value.ToString());
                //    dt.Rows[0]["F9"] = (p.CompetentTeaching == null ? null : p.CompetentTeaching.Value.ToString());
                //    dt.Rows[0]["F10"] = (p.WantTrain == null ? null : p.WantTrain.Value.ToString());

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