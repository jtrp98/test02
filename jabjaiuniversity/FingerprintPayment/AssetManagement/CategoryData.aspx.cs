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

namespace FingerprintPayment.AssetManagement
{
    public class AssetManagementGateway : System.Web.UI.Page
    {
        private JWTToken.userData userData;
        protected JWTToken.userData UserData { get { return userData; } }

        protected override void OnLoad(EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        public static JWTToken.userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }

            return userData;
        }
    }

    public partial class CategoryData : AssetManagementGateway
    {
        private static string SessionPrimaryKey = "CATEGORYID";

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
                    int amountExist = en.TAssetProducts.Where(w => w.AssetProductId == id).Count();
                    if (amountExist == 0)
                    {
                        TAssetCategory p = en.TAssetCategories.First(f => f.SchoolID == schoolID && f.AssetCategoryId == id);

                        en.TAssetCategories.Remove(p);

                        en.SaveChanges();
                    }
                    else
                    {
                        int empID = Convert.ToInt32(HttpContext.Current.Session["sEmpID"]);

                        TAssetCategory pi = en.TAssetCategories.First(f => f.SchoolID == schoolID && f.AssetCategoryId == id);

                        pi.Flag = 2;

                        pi.UpdateDate = DateTime.Now;
                        pi.UpdateBy = empID;

                        en.SaveChanges();
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
        public static string SaveItem(List<string> data)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    //int empID = Convert.ToInt32(HttpContext.Current.Session["sEmpID"]);

                    string Code = data[1];
                    string Category = data[2];

                    int ID = 0;

                    if (HttpContext.Current.Session[SessionPrimaryKey] == null)
                    {
                        // Insert Section
                        //int ID = (int)(en.TAssetCategories.Count() == 0 ? 1 : en.TAssetCategories.Max(m => m.ID) + 1);

                        // Get Item
                        TAssetCategory p = new TAssetCategory
                        {
                            //AssetCategoryId = ID,
                            Code = Code,
                            Category = Category,
                            Flag = 1,
                            SchoolID = schoolID,

                            UpdateDate = DateTime.Now,
                            UpdateBy = userData.UserID
                        };

                        en.TAssetCategories.Add(p);

                        en.SaveChanges();
                    }
                    else
                    {
                        // Modify Section
                        ID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                        int c = en.TAssetCategories.Where(w => w.SchoolID == schoolID && w.AssetCategoryId != ID && w.Code == Code).Count();
                        if (c == 0)
                        {
                            // Get Item
                            TAssetCategory pi = en.TAssetCategories.First(f => f.SchoolID == schoolID && f.AssetCategoryId == ID);

                            pi.Code = Code;
                            pi.Category = Category;

                            pi.UpdateDate = DateTime.Now;
                            pi.UpdateBy = userData.UserID;

                            en.SaveChanges();
                        }
                        else
                        {
                            isComplete = "warning-Duplicate Code: " + Code;
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

                    TAssetCategory p = en.TAssetCategories.Where(w => w.SchoolID == schoolID && w.AssetCategoryId == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 2; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        HttpContext.Current.Session[SessionPrimaryKey] = p.AssetCategoryId;

                        dt.Rows[0]["F1"] = p.Code;
                        dt.Rows[0]["F2"] = p.Category;

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