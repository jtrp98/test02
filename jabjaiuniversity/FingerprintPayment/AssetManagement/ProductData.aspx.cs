using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.AssetManagement
{
    public partial class ProductData : AssetManagementGateway
    {
        private static string SessionPrimaryKey = "PRODUCTID";

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
                    int amountExist = en.TAssetTransactions.Where(w => w.SchoolID == schoolID && w.AssetProductId == id).Count();
                    if (amountExist == 0)
                    {
                        TAssetProduct p = en.TAssetProducts.First(f => f.SchoolID == schoolID && f.AssetProductId == id);

                        en.TAssetProducts.Remove(p);

                        en.SaveChanges();
                    }
                    else
                    {
                        TAssetProduct pi = en.TAssetProducts.First(f => f.SchoolID == schoolID && f.AssetProductId == id);

                        pi.Flag = 2;

                        pi.UpdateDate = DateTime.Now;
                        pi.UpdateBy = userData.UserID;

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
                    int empID = userData.UserID;

                    string Code = data[1];
                    int? Category = string.IsNullOrEmpty(data[2]) ? (int?)null : int.Parse(data[2]);
                    string Type = data[3];
                    string Product = data[4];
                    int? Unit = string.IsNullOrEmpty(data[5]) ? (int?)null : int.Parse(data[5]);

                    int ID = 0;

                    if (HttpContext.Current.Session[SessionPrimaryKey] == null)
                    {
                        // Insert Section
                        //int ID = (int)(en.TAssetProducts.Count() == 0 ? 1 : en.TAssetProducts.Max(m => m.ID) + 1);

                        // Get Item
                        TAssetProduct p = new TAssetProduct
                        {
                            //AssetProductId = ID,
                            AssetCategoryId = Category,
                            Type = Type,
                            Product = Product,
                            UnitID = Unit,
                            Flag = 1,
                            SchoolID = schoolID,

                            UpdateDate = DateTime.Now,
                            UpdateBy = empID
                        };

                        en.TAssetProducts.Add(p);

                        en.SaveChanges();
                    }
                    else
                    {
                        // Modify Section
                        ID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                        int c = en.TAssetProducts.Where(w => w.SchoolID == schoolID && w.AssetProductId != ID && w.Product == Product).Count();
                        if (c == 0)
                        {
                            // Get Item
                            TAssetProduct pi = en.TAssetProducts.First(f => f.SchoolID == schoolID && f.AssetProductId == ID);

                            pi.AssetCategoryId = Category;
                            pi.Type = Type;
                            pi.Product = Product;
                            pi.UnitID = Unit;

                            pi.UpdateDate = DateTime.Now;
                            pi.UpdateBy = empID;

                            en.SaveChanges();
                        }
                        else
                        {
                            isComplete = "warning-Duplicate Product: " + Product;
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

                    TAssetProduct p = en.TAssetProducts.Where(w => w.SchoolID == schoolID && w.AssetProductId == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 5; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        HttpContext.Current.Session[SessionPrimaryKey] = p.AssetProductId;

                        string assetCateCode = "";
                        TAssetCategory assetCategory = en.TAssetCategories.Where(w => w.SchoolID == schoolID && w.AssetCategoryId == p.AssetCategoryId).FirstOrDefault();
                        if (assetCategory != null)
                        {
                            assetCateCode = assetCategory.Code;
                        }

                        dt.Rows[0]["F1"] = assetCateCode;
                        dt.Rows[0]["F2"] = p.AssetCategoryId?.ToString();
                        dt.Rows[0]["F3"] = p.Type;
                        dt.Rows[0]["F4"] = p.Product;
                        dt.Rows[0]["F5"] = p.UnitID?.ToString();

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