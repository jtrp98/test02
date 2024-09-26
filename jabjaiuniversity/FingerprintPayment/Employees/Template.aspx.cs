using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Employees
{
    public partial class Template : System.Web.UI.Page
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey = "FOREIGNKEY_EMPEDUCATIONID";
        private static string SessionForeignKey2 = "FOREIGNKEY_EMPFAMEID";

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
            //ArifeenStreamingEntities en = new ArifeenStreamingEntities();

            string isComplete = "complete";

            //try
            //{
            //    PubGShowcaseItem p = en.PubGShowcaseItem.First(f => f.Id == id);

            //    // Delete Item Images
            //    string imageFile = HttpContext.Current.Server.MapPath(string.Format(itemImagePath, p.Id, p.Image));
            //    if (File.Exists(imageFile))
            //    {
            //        File.Delete(imageFile);
            //    }

            //    en.PubGShowcaseItem.Remove(p);

            //    en.SaveChanges();
            //}
            //catch
            //{
            //    isComplete = "error";
            //}

            return isComplete;
        }

        [WebMethod]
        public static string RemoveItem2(int eid, int id)
        {
            //ArifeenStreamingEntities en = new ArifeenStreamingEntities();

            string isComplete = "complete";

            //try
            //{
            //    PubGShowcaseItem p = en.PubGShowcaseItem.First(f => f.Id == id);

            //    // Delete Item Images
            //    string imageFile = HttpContext.Current.Server.MapPath(string.Format(itemImagePath, p.Id, p.Image));
            //    if (File.Exists(imageFile))
            //    {
            //        File.Delete(imageFile);
            //    }

            //    en.PubGShowcaseItem.Remove(p);

            //    en.SaveChanges();
            //}
            //catch
            //{
            //    isComplete = "error";
            //}

            return isComplete;
        }

        [WebMethod]
        public static string SaveItem(string type, string videoUrl, string description)
        {
            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            string isComplete = "complete";

            try
            {
                var uID = Convert.ToInt16(HttpContext.Current.Session["USERID"]);

                if (HttpContext.Current.Session[SessionPrimaryKey] == null)
                {
                    // Insert Section
                    //int id = (int)(en.TEmployee2.Count() == 0 ? 1 : en.TEmployee2.Max(m => m.ID) + 1);

                    //// Get Item
                    //TEmployee2 p = new TEmployee2
                    //{
                    //    ID = id,
                    //    UpdateDate = DateTime.Now
                    //};

                    //en.TEmployee2.Add(p);

                    //en.SaveChanges();

                }
                else
                {
                    // Modify Section
                    int id = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                    //int c = en.TEmployee2.Where(w => w.ID != id).Count();
                    //if (c == 0)
                    //{
                    //    // Get Item
                    //    TEmployee2 pi = en.TEmployee2.First(f => f.ID == id);
                    //    pi.UpdateDate = DateTime.Now;

                    //    en.SaveChanges();
                    //}
                    //else
                    //{
                    //    isComplete = "warning-Duplicate name: " + description;
                    //}

                }
            }
            catch
            {
                isComplete = "error";
            }

            return isComplete;
        }

        [WebMethod]
        public static string GetItem(string empID)
        {
            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            string infor = "new";

            try
            {
                int iEmpID = 0;
                if (!int.TryParse(empID, out iEmpID)) { iEmpID = 0; }

                //TEmployee2 p = en.TEmployee2.Where(w => w.ID == iEmpID).FirstOrDefault();
                //if (p != null)
                //{
                //    DataSet ds = new DataSet();
                //    DataTable dt = new DataTable("Table1");
                //    dt.Columns.Add("F1");
                //    dt.Columns.Add("F2");
                //    dt.Columns.Add("F3");

                //    dt.Rows.Add();

                //    HttpContext.Current.Session[SessionPrimaryKey] = p.ID;

                //    dt.Rows[0]["F1"] = p.IDCardNumber;
                //    dt.Rows[0]["F2"] = p.IDCardNumber;
                //    dt.Rows[0]["F3"] = p.IDCardNumber;

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

            if (infor == "error") HttpContext.Current.Session[SessionPrimaryKey] = null;

            return infor;
        }

        [WebMethod]
        public static string ClearValue()
        {
            HttpContext.Current.Session[SessionPrimaryKey] = null;

            return "";
        }

        #endregion
    }
}