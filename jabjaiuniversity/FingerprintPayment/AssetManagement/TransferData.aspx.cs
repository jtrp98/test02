using FingerprintPayment.AssetManagement.CsCode;
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
using static FingerprintPayment.AssetManagement.CsCode.GlobalFunction;

namespace FingerprintPayment.AssetManagement
{
    public partial class TransferData : AssetManagementGateway
    {
        private static string SessionPrimaryKeyYear = "YEARID";
        private static string SessionPrimaryKey = "TRANSFERID";

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
        public static string RemoveItem(int year, int id)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    try
                    {
                        TAssetTransfer p = en.TAssetTransfers.First(f => f.SchoolID == schoolID && f.Year == year && f.AssetTransferId == id);

                        GlobalFunction.RecordTransaction((int)AssetTransactionType.TRANSFER, id, p.AssetProductId, p.DepIDTransfer, p.DepIDRequest, p.Amount, null, p.UnitID, null, (int)AssetAction.DELETE);

                        en.TAssetTransfers.Remove(p);

                        en.SaveChanges();

                        //The Transaction will be completed  
                        transScope.Complete();
                    }
                    catch
                    {
                        isComplete = "error";

                        transScope.Dispose();
                    }
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

                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    try
                    {
                        string Code = string.IsNullOrEmpty(data[1]) ? null : data[1];
                        int? Category = string.IsNullOrEmpty(data[2]) ? (int?)null : int.Parse(data[2]);
                        int? Product = string.IsNullOrEmpty(data[3]) ? (int?)null : int.Parse(data[3]);
                        int? Amount = string.IsNullOrEmpty(data[4]) ? (int?)null : int.Parse(data[4]);
                        int? Unit = string.IsNullOrEmpty(data[5]) ? (int?)null : int.Parse(data[5]);
                        int? DepartmentRequest = string.IsNullOrEmpty(data[6]) ? (int?)null : int.Parse(data[6]);
                        int? DepartmentTransfer = string.IsNullOrEmpty(data[7]) ? (int?)null : int.Parse(data[7]);

                        int ID = 0;

                        if (HttpContext.Current.Session[SessionPrimaryKey] == null)
                        {
                            // Insert Section
                            int Year = int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));
                            //int ID = (int)(en.TAssetTransfers.Where(w => w.Year == Year).Count() == 0 ? 1 : en.TAssetTransfers.Where(w => w.Year == Year).Max(m => m.ID) + 1);

                            // Get Item
                            TAssetTransfer p = new TAssetTransfer
                            {
                                Year = Year,
                                //ID = ID,
                                AssetProductId = Product,
                                Amount = Amount,
                                UnitID = Unit,
                                DepIDRequest = DepartmentRequest,
                                DepIDTransfer = DepartmentTransfer,
                                SchoolID = schoolID,

                                DateStamp = DateTime.Now,
                                UpdateDate = DateTime.Now
                            };

                            en.TAssetTransfers.Add(p);

                            en.SaveChanges();

                            GlobalFunction.RecordTransaction((int)AssetTransactionType.TRANSFER, p.AssetTransferId, p.AssetProductId, p.DepIDTransfer, p.DepIDRequest, p.Amount, null, p.UnitID, null, (int)AssetAction.INSERT);
                        }
                        else
                        {
                            // Modify Section
                            int Year = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKeyYear]);
                            ID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                            // Get Item
                            TAssetTransfer pi = en.TAssetTransfers.First(f => f.SchoolID == schoolID && f.Year == Year && f.AssetTransferId == ID);

                            int? oldAmount = pi.Amount;
                            int? oldUnit = pi.UnitID;
                            int? oldDepIDTransfer = pi.DepIDTransfer;
                            int? oldDepIDRequest = pi.DepIDRequest;


                            pi.AssetProductId = Product;
                            pi.Amount = Amount;
                            pi.UnitID = Unit;
                            pi.DepIDRequest = DepartmentRequest;
                            pi.DepIDTransfer = DepartmentTransfer;

                            pi.UpdateDate = DateTime.Now;

                            GlobalFunction.RecordTransaction((int)AssetTransactionType.TRANSFER, ID, pi.AssetProductId, oldDepIDRequest, oldDepIDTransfer, pi.Amount, oldAmount, pi.UnitID, oldUnit, (int)AssetAction.UPDATE);
                            GlobalFunction.RecordTransaction((int)AssetTransactionType.TRANSFER, ID, pi.AssetProductId, pi.DepIDTransfer, pi.DepIDRequest, pi.Amount, null, pi.UnitID, null, (int)AssetAction.UPDATE);

                            en.SaveChanges();
                        }

                        //The Transaction will be completed  
                        transScope.Complete();
                    }
                    catch
                    {
                        isComplete = "error";

                        transScope.Dispose();
                    }
                }

                return isComplete;
            }
        }

        [WebMethod]
        public static string GetItem(string year, string id)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iYear = 0;
                    if (!int.TryParse(year, out iYear)) { iYear = 0; }
                    int iID = 0;
                    if (!int.TryParse(id, out iID)) { iID = 0; }

                    TAssetTransfer p = en.TAssetTransfers.Where(w => w.SchoolID == schoolID && w.Year == iYear && w.AssetTransferId == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 7; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        HttpContext.Current.Session[SessionPrimaryKeyYear] = p.Year;
                        HttpContext.Current.Session[SessionPrimaryKey] = p.AssetTransferId;

                        string cateCode = "";
                        string cateID = "";
                        TAssetProduct assetProduct = en.TAssetProducts.Where(w => w.AssetProductId == p.AssetProductId).FirstOrDefault();
                        if (assetProduct != null)
                        {
                            TAssetCategory assetCategory = en.TAssetCategories.Where(w => w.AssetCategoryId == assetProduct.AssetCategoryId).FirstOrDefault();
                            if (assetCategory != null)
                            {
                                cateCode = assetCategory.Code;
                                cateID = assetCategory.AssetCategoryId.ToString();
                            }
                        }

                        dt.Rows[0]["F1"] = cateCode;
                        dt.Rows[0]["F2"] = cateID;
                        dt.Rows[0]["F3"] = p.AssetProductId?.ToString();
                        dt.Rows[0]["F4"] = p.Amount?.ToString();
                        dt.Rows[0]["F5"] = p.UnitID?.ToString();
                        dt.Rows[0]["F6"] = p.DepIDRequest?.ToString();
                        dt.Rows[0]["F7"] = p.DepIDTransfer?.ToString();

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

                if (infor == "new" || infor == "error")
                {
                    HttpContext.Current.Session[SessionPrimaryKeyYear] = null;
                    HttpContext.Current.Session[SessionPrimaryKey] = null;
                }

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
            HttpContext.Current.Session[SessionPrimaryKeyYear] = null;
            HttpContext.Current.Session[SessionPrimaryKey] = null;

            return "";
        }

        #endregion
    }
}