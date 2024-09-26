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

namespace FingerprintPayment.AssetManagement
{
    public partial class TransactionData : AssetManagementGateway
    {
        private static string SessionPrimaryKey = "LEARNINGCENTERID";

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
        public static string RemoveItem(int id)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    TLearningCenter p = en.TLearningCenters.First(f => f.SchoolID == schoolID && f.LearningCenterID == id);

                    en.TLearningCenters.Remove(p);

                    en.SaveChanges();
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

                try
                {
                    int? Type = string.IsNullOrEmpty(data[1]) ? (int?)null : int.Parse(data[1]);
                    string Name = data[2];
                    string Detail = data[3];
                    string Admin = data[4];

                    int ID = 0;

                    if (HttpContext.Current.Session[SessionPrimaryKey] == null)
                    {
                        // Insert Section
                        //int ID = (int)(en.TLearningCenters.Where(w=>w.SchoolID == schoolID).Count() == 0 ? 1 : en.TLearningCenters.Where(w => w.SchoolID == schoolID).Max(m => m.LearningCenterID) + 1);

                        // Get Item
                        TLearningCenter p = new TLearningCenter
                        {
                            //ID = ID,
                            Type = Type,
                            Name = Name,
                            Detail = Detail,
                            Admin = Admin,
                            SchoolID = schoolID,

                            UpdateDate = DateTime.Now
                        };

                        en.TLearningCenters.Add(p);

                        en.SaveChanges();
                    }
                    else
                    {
                        // Modify Section
                        ID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                        int c = en.TLearningCenters.Where(w => w.SchoolID == schoolID && w.LearningCenterID != ID && w.Name == Name).Count();
                        if (c == 0)
                        {
                            // Get Item
                            TLearningCenter pi = en.TLearningCenters.First(f => f.SchoolID == schoolID && f.LearningCenterID == ID);

                            pi.Type = Type;
                            pi.Name = Name;
                            pi.Detail = Detail;
                            pi.Admin = Admin;

                            pi.UpdateDate = DateTime.Now;

                            en.SaveChanges();
                        }
                        else
                        {
                            isComplete = "warning-Duplicate Name: " + Name;
                        }
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
        public static string GetItem(string id)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iID = 0;
                    if (!int.TryParse(id, out iID)) { iID = 0; }

                    TLearningCenter p = en.TLearningCenters.Where(w => w.SchoolID == schoolID && w.LearningCenterID == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 4; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        HttpContext.Current.Session[SessionPrimaryKey] = p.LearningCenterID;

                        dt.Rows[0]["F1"] = (p.Type == null ? null : p.Type.Value.ToString());
                        dt.Rows[0]["F2"] = p.Name;
                        dt.Rows[0]["F3"] = p.Detail;
                        dt.Rows[0]["F4"] = p.Admin;

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

                if (infor == "new" || infor == "error") HttpContext.Current.Session[SessionPrimaryKey] = null;

                return infor;
            }
        }

        [WebMethod]
        public static string ViewItem(string id)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iID = 0;
                    if (!int.TryParse(id, out iID)) { iID = 0; }

                    TLearningCenter p = en.TLearningCenters.Where(w => w.SchoolID == schoolID && w.LearningCenterID == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 4; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F1"] = (p.Type == null ? "-" : (p.Type == 1 ? "แหล่งเรียนรู้ภายใน" : "แหล่งเรียนรู้ภายนอก"));
                        dt.Rows[0]["F2"] = (string.IsNullOrEmpty(p.Name) ? "-" : p.Name);
                        dt.Rows[0]["F3"] = (string.IsNullOrEmpty(p.Detail) ? "-" : p.Detail);
                        dt.Rows[0]["F4"] = (string.IsNullOrEmpty(p.Admin) ? "-" : p.Admin);

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

                return infor;
            }
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