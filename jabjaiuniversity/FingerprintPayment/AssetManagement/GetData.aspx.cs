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
    public partial class GetData : AssetManagementGateway
    {
        private static string SessionPrimaryKeyYear = "YEARID";
        private static string SessionPrimaryKey = "GETID";

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
                        TAssetGet p = en.TAssetGets.First(f => f.SchoolID == schoolID && f.Year == year && f.AssetGetId == id);

                        GlobalFunction.RecordTransaction(userData, (int)AssetTransactionType.GET, id, p.AssetProductId, p.DepID, null, p.Amount, null, p.UnitID, null, (int)AssetAction.DELETE);

                        en.TAssetGets.Remove(p);

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
                        int empID = userData.UserID;

                        string Source = string.IsNullOrEmpty(data[1]) ? null : data[1];
                        string Code = string.IsNullOrEmpty(data[2]) ? null : data[2];
                        int? Category = string.IsNullOrEmpty(data[3]) ? (int?)null : int.Parse(data[3]);
                        int? Product = string.IsNullOrEmpty(data[4]) ? (int?)null : int.Parse(data[4]);
                        int? Amount = string.IsNullOrEmpty(data[5]) ? (int?)null : int.Parse(data[5]);
                        int? Unit = string.IsNullOrEmpty(data[6]) ? (int?)null : int.Parse(data[6]);
                        int? Department = string.IsNullOrEmpty(data[7]) ? (int?)null : int.Parse(data[7]);
                        int? Receiver = string.IsNullOrEmpty(data[8]) ? (int?)null : int.Parse(data[8]);
                        DateTime? DateStamp = string.IsNullOrEmpty(data[9]) ? (DateTime?)null : DateTime.Parse(data[9], new CultureInfo("th-TH"));

                        int ID = 0;

                        if (HttpContext.Current.Session[SessionPrimaryKey] == null)
                        {
                            // Insert Section
                            int Year = int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));
                            //int ID = (int)(en.TAssetGets.Where(w => w.Year == Year).Count() == 0 ? 1 : en.TAssetGets.Where(w => w.Year == Year).Max(m => m.ID) + 1);

                            // Get Item
                            TAssetGet p = new TAssetGet
                            {
                                Year = Year,
                                //ID = ID,
                                Source = Source,
                                AssetProductId = Product,
                                Amount = Amount,
                                UnitID = Unit,
                                DepID = Department,
                                Receiver = Receiver,
                                DateStamp = DateStamp,
                                SchoolID = schoolID,

                                UpdateDate = DateTime.Now,
                                UpdateBy = empID
                            };

                            en.TAssetGets.Add(p);

                            en.SaveChanges();

                            GlobalFunction.RecordTransaction(userData, (int)AssetTransactionType.GET, p.AssetGetId, p.AssetProductId, p.DepID, null, p.Amount, null, p.UnitID, null, (int)AssetAction.INSERT);
                        }
                        else
                        {
                            // Modify Section
                            int Year = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKeyYear]);
                            ID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                            // Get Item
                            TAssetGet pi = en.TAssetGets.First(f => f.SchoolID == schoolID && f.Year == Year && f.AssetGetId == ID);

                            int? oldDepartment = pi.DepID;
                            int? oldAmount = pi.Amount;
                            int? oldUnit = pi.UnitID;

                            pi.Source = Source;
                            pi.AssetProductId = Product;
                            pi.Amount = Amount;
                            pi.UnitID = Unit;
                            pi.DepID = Department;
                            pi.Receiver = Receiver;
                            pi.DateStamp = DateStamp;

                            pi.UpdateDate = DateTime.Now;
                            pi.UpdateBy = empID;

                            GlobalFunction.RecordTransaction(userData, (int)AssetTransactionType.GET, ID, pi.AssetProductId, pi.DepID, oldDepartment, pi.Amount, oldAmount, pi.UnitID, oldUnit, (int)AssetAction.UPDATE);

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

                    TAssetGet p = en.TAssetGets.Where(w => w.SchoolID == schoolID && w.Year == iYear && w.AssetGetId == iID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 9; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        HttpContext.Current.Session[SessionPrimaryKeyYear] = p.Year;
                        HttpContext.Current.Session[SessionPrimaryKey] = p.AssetGetId;

                        string assetCateCode = "";
                        string assetCateID = "";
                        TAssetProduct assetProduct = en.TAssetProducts.Where(w => w.AssetProductId == p.AssetProductId).FirstOrDefault();
                        if (assetProduct != null)
                        {
                            TAssetCategory assetCategory = en.TAssetCategories.Where(w => w.AssetCategoryId == assetProduct.AssetCategoryId).FirstOrDefault();
                            if (assetCategory != null)
                            {
                                assetCateCode = assetCategory.Code;
                                assetCateID = assetCategory.AssetCategoryId.ToString();
                            }
                        }

                        dt.Rows[0]["F1"] = p.Source;
                        dt.Rows[0]["F2"] = assetCateCode;
                        dt.Rows[0]["F3"] = assetCateID;
                        dt.Rows[0]["F4"] = p.AssetProductId?.ToString();
                        dt.Rows[0]["F5"] = p.Amount?.ToString();
                        dt.Rows[0]["F6"] = p.UnitID?.ToString();
                        dt.Rows[0]["F7"] = p.DepID?.ToString();
                        dt.Rows[0]["F8"] = p.Receiver?.ToString();
                        dt.Rows[0]["F9"] = p.DateStamp?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

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